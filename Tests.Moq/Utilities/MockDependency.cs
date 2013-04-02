using System;
using Moq;

namespace Tests.Moq.Utilities
{
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