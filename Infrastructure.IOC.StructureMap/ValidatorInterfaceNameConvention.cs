using System;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using Common.Extensions;

namespace Common.Infrastructure.IoC.StructureMap
{
    public class ValidatorInterfaceNameConvention : IRegistrationConvention
    {
        private Type type;

        public void Process(Type scanedType, Registry registry)
        {
            type = scanedType;

            if (!type.IsConcreteType()) return;

            var validatorInterface = type.GetInterface("IValidator`1");

            if (validatorInterface == null) return;

            // TODO: check if it's necesary to add this validation: if (!type.Name.StartsWith(validatorInterface.GenericTypeArguments[0].Name)) return;
            // TODO: check if it's necesary to add this validation: if (!type.Name.EndsWith("Validator")) return;
            
            registry.AddType(validatorInterface, type);
        }
    }
}
