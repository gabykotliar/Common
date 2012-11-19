using System;
using System.Configuration;

using Common.Caching;
using Common.Logging;

namespace Common
{
    public static class AppServices
    {
        private static readonly object lockRef = new object();

        private static Factory factory;

        private static Log log;
        private static Cache cache;

        public static void SetFactory(Factory factory)
        {
            AppServices.factory = factory;
        }

        public static void SetCache(Cache cache)
        {
            AppServices.cache = cache;
        }

        public static Log Log
        {
            get
            {
                if (log != null) return log;

                try
                {
                    lock (lockRef)
                    {
                        if (log != null) return log;

                        if (factory == null) throw new ConfigurationErrorsException("If a specific logger is not provided you need to provide a Factory by calling SetFactory(Factory factory)");

                        log = new Log(factory.GetInstance<Logger>());
                    }
                }
                catch (ConfigurationErrorsException)
                {
                    throw;
                }                
                catch (Exception e)
                {
                    throw new AppServicesException("An instance of Logger could not be created.", e);
                }

                return log;
            }
        }

        public static Cache Cache
        {
            get
            {
                if (cache != null) return cache;

                try
                {
                    lock (lockRef)
                    {
                        if (cache != null) return cache;

                        if (cache == null) throw new ConfigurationErrorsException("If a specific cache is not provided you need to provide a Factory by calling SetFactory(Factory factory)");

                        cache = factory.GetInstance<Cache>();
                    }
                }
                catch(ConfigurationErrorsException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw new AppServicesException("An instance of Cache could not be created.", e);
                }

                return cache;
            }
        }
    }
}
