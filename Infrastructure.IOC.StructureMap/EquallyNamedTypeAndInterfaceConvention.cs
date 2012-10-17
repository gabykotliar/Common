using System;
using Common.Extensions;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Common.Infrastructure.IoC.StructureMap
{
    public class EquallyNamedTypeAndInterfaceConvention : IRegistrationConvention
    {
        private Type type;
        private Type implementedInterface;        

        public void Process(Type scanedType, Registry registry)
        {
            type = scanedType;

            if (type.IsConcreteType() && HasInterfaceWithTheSameName())
                RegisterType(registry);
        }

        private void RegisterType(Registry registry)
        {
            var interfaceType = type.GetInterface(type.Name);

            registry.AddType(interfaceType, type);
        }

        private bool HasInterfaceWithTheSameName()
        {
            FindInterface(type.Name);

            return implementedInterface != null;
        }

        private void FindInterface(string name)
        {
            implementedInterface = type.GetInterface(name);
        }
    }
}
