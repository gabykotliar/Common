using System;
using System.Collections.Generic;
using Moq;

namespace Tests.Moq.Utilities
{
    public class MockServiceBuilderResult<T>
    {
        public T Service;

        public List<MockDependency> MockedDependencies = new List<MockDependency>();

        public void AddMockedDependency(Type type, Mock mockedInstance)
        {
            MockedDependencies.Add(new MockDependency(type, mockedInstance));
        }
    }
}