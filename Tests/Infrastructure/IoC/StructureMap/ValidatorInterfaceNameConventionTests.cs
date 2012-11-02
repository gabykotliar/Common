using System;
using Common.Infrastructure.IoC.StructureMap;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap.Configuration.DSL;

namespace Tests.Infrastructure.IoC.StructureMap
{
    [TestClass]
    public class ValidatorInterfaceNameConventionTests
    {
        [TestMethod]
        [Ignore]
        public void ProcessImplementingClassShouldAddTheTypeToTheRegistryTest()
        {
            var convention = new ValidatorInterfaceNameConvention();
            var registry = new Registry();

            convention.Process(typeof(GenericParameterHelperValidator), registry);

            Assert.Inconclusive("Check for the type being added correctly is pending");
        }
    }

    public interface IValidator<T>
    {
    }

    public class GenericParameterHelperValidator : IValidator<GenericParameterHelper>
    {
    }
}
