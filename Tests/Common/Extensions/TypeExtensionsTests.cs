using Common.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Common.Extensions
{
    [TestClass]
    public class TypeExtensionsTests
    {
        [TestMethod]
        public void ConcreteTypeIsConcreteTest()
        {
            typeof(ConcreteType).IsConcreteType().Should().BeTrue();
        }

        [TestMethod]
        public void AbstractTypeIsNotConcreteTest()
        {
            typeof(AbstractType).IsConcreteType().Should().BeFalse();
        }

        [TestMethod]
        public void AnInterfaceIsNotConcreteTest()
        {
            typeof(IType).IsConcreteType().Should().BeFalse();
        }

        [TestMethod]
        public void AValueTypeNotConcreteTest()
        {
            typeof(int).IsConcreteType().Should().BeFalse();
        }
    }

    internal class ConcreteType
    {
    }

    internal abstract class AbstractType
    {
    }

    internal interface IType
    {
    }
}
