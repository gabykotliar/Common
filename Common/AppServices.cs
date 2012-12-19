using System;
using System.Configuration;

using Common.Caching;
using Common.Configuration;
using Common.Logging;

namespace Common
{
    public static class AppServices
    {
        private static readonly object LockRef = new object();

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
                    lock (LockRef)
                    {
                        if (log != null) return log;

                        CheckFactoryFor("logger");

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
                    lock (LockRef)
                    {
                        if (cache != null) return cache;

                        CheckFactoryFor("cache");

                        cache = AppServicesConfiguration.Instance.Caching.Disabled 
                                    ? new DisabledCache() 
                                    : factory.GetInstance<Cache>();
                    }
                }
                catch (ConfigurationErrorsException)
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

        private static void CheckFactoryFor(string target)
        {
            if (factory == null)
                throw new ConfigurationErrorsException(string.Format("If a specific {0} is not provided you need to provide a Factory by calling SetFactory(Factory factory)", target));
        }
    }
}
