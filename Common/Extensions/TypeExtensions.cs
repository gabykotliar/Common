using System;

namespace Common.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsConcreteType(this Type type)
        {
            return !type.IsAbstract && type.IsClass;
        }
    }
}
