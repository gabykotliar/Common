using Common.Log;
using global::StructureMap;

namespace Common.Infrastructure.IoC.StructureMap
{
    public class StructureMapFactory : Factory
    {
        public T GetInstance<T>()
        {
            return ObjectFactory.GetInstance<T>();
        }
    }
}
