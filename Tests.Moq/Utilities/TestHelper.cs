using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.Moq.Utilities
{
    public abstract class TestHelper<T> where T : class
    {
        private Dictionary<Type, object> _mockedInstances = new Dictionary<Type, object>();
        private MockHelper _mockHelper;

        /// <summary>
        /// the class under test
        /// </summary>
        protected T ClassBeingTested { get; set; }

        /// <summary>
        /// returns a reference to the MoQ Mock object that wraps each dependent object
        /// 
        /// Use this to get at the dependencies of the class and set up return values
        /// or make assertions about the controller's interactions.
        /// </summary>
        /// <typeparam name="dependencyT"></typeparam>
        /// <returns></returns>
        protected Mock<dependencyT> Instance<dependencyT>() where dependencyT : class
        {
            if (!_mockedInstances.ContainsKey(typeof (dependencyT)))
                _mockedInstances.Add(typeof (dependencyT), MockHelper.CreateMock<dependencyT>());
            return _mockedInstances[typeof (dependencyT)] as Mock<dependencyT>;
        }

        [ClassInitialize]
        public void SetupFixture()
        {
            Before_all();
        }

        protected virtual void Before_all()
        {
        }

        [TestInitialize]
        public void Before_Each_Test()
        {
            _mockedInstances.Clear();
            ClassBeingTested = BuildService();
            Before_each();
        }

        protected MockHelper MockHelper
        {
            get { return _mockHelper ?? (_mockHelper = new MockHelper()); }
        }

        protected virtual void Before_each()
        {
        }

        protected virtual T BuildService()
        {
            var result = MockHelper.BuildServiceWithMocks<T>();

            foreach (var mockedDependency in result.MockedDependencies)
                _mockedInstances.Add(mockedDependency.Type, mockedDependency.Mock);

            return result.Service;
        }

        protected paramT Any<paramT>()
        {
            return It.IsAny<paramT>();
        }

        protected void ShouldHaveCalled<depT>(Expression<Action<depT>> call) where depT : class
        {
            Instance<depT>().Verify(call);
        }

        protected void ShouldThrow<exceptionT>(Action action) where exceptionT : Exception
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.GetType() == typeof (exceptionT),
                              "incorrect type of exception was thrown... expected " + typeof (exceptionT).Name +
                              " but caught " + ex.GetType().Name);

                return;
            }

            Assert.Fail("no exception was thrown... expected a " + typeof (exceptionT).Name);
        }

        [TestCleanup]
        public void After_each_test()
        {
            Cleanup();
        }

        public virtual void Cleanup()
        {
        }
    }
}
