using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Log
{
    public static class Configuration
    {
        internal static Factory Factory { get; private set; }

        public static void SetFactory(Factory factory)
        {
            Factory = factory;
        }
    }
}
