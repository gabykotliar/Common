using System;
using Common.Extensions;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Common.Infrastructure.IoC.StructureMap
{
    public class ValidatorInterfaceNameConvention : IRegistrationConvention
    {
        private Type type;

        public void Process(Type type, Registry registry)
        {
            this.type = type;

            if (!this.type.IsConcreteType()) return;

            var validatorInterface = this.type.GetInterface("IValidator`1");

            if (validatorInterface == null) return;

            // TODO: check if it's necesary to add this validation: if (!type.Name.StartsWith(validatorInterface.GenericTypeArguments[0].Name)) return;
            // TODO: check if it's necesary to add this validation: if (!type.Name.EndsWith("Validator")) return;
            
            registry.AddType(validatorInterface, this.type);
        }
    }
}
