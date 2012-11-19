using System;
using System.Configuration.Provider;
using System.Runtime.Serialization;

namespace Common
{
    public class AppServicesException : ProviderException
    {
        public AppServicesException()
        {
        }

        public AppServicesException(string message)
            : base(message)
        {
        }

        public AppServicesException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected AppServicesException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
