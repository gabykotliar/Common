using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moq;

namespace Tests.Moq.Utilities
{
    public class MockHelper
    {
        private readonly IDictionary<Type, object> _userDefinedTypes = new Dictionary<Type, object>();
        public void RegisterUserDefinedService<T>(T instance)
        {
            _userDefinedTypes.Add(typeof(T), instance);
        }

        public void ClearUserDefinedServices()
        {
            _userDefinedTypes.Clear();
        }

        public MockServiceBuilderResult<T> BuildServiceWithMocks<T>()
        {
            var result = new MockServiceBuilderResult<T>();

            var ctorWithMostParameters = GetCtorWithMostParameters<T>();
            var parameters = ctorWithMostParameters.GetParameters();
            var ctorParamValues = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                Type parameterType = parameters[i].ParameterType;

                if (UserDefinedTypeExists(parameterType))
                {
                    ctorParamValues[i] = _userDefinedTypes[parameterType];
                    continue;
                }

                Mock mockedInstance = BuildMockedInstance(parameterType);

                result.AddMockedDependency(parameterType, mockedInstance);

                ctorParamValues[i] = mockedInstance.Object;
            }

            result.Service = (T)ctorWithMostParameters.Invoke(ctorParamValues);

            return result;
        }

        private bool UserDefinedTypeExists(Type type)
        {
            return _userDefinedTypes.ContainsKey(type);
        }


        private static ConstructorInfo GetCtorWithMostParameters<T>()
        {
            return typeof(T)
                .GetConstructors()
                .OrderBy(x => x.GetParameters().Length)
                .Last();
        }

        public static Mock BuildMockedInstance(Type parameterType)
        {
            // TODO: may need to deal with IEnumerable or array ctor args here.

            var mockType = typeof(Mock<>).MakeGenericType(new[] { parameterType });

            return Activator.CreateInstance(mockType) as Mock;
        }

        public Mock<T> CreateMock<T>() where T : class
        {
            return new Mock<T>();
        }
    }

    public class MockServiceBuilderResult<T>
    {

        public T Service;
        public List<MockDependency> MockedDependencies = new List<MockDependency>();

        public void AddMockedDependency(Type type, Mock mockedInstance)
        {
            MockedDependencies.Add(new MockDependency(type, mockedInstance));
        }
    }

    public class MockDependency
    {
        public Type Type { get; set; }
        public Mock Mock { get; set; }

        public MockDependency(Type type, Mock mockedInstance)
        {
            Type = type;
            Mock = mockedInstance;
        }
    }
}
