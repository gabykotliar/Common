namespace Common.Persistence.NHibernate
{
    using Common;
    using global::NHibernate;
    using StructureMap;
    using System;
    using System.Linq;
    using System.Web;

    public class SessionModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.EndRequest += this.ContextEndRequest;
        }

        public void Dispose()
        {            
        }

        public void ContextEndRequest(Object sender, EventArgs e)
        {
            var request = ((HttpApplication)sender).Request;

            if (!RequestRequiresSessionUpdate(request)) return;

            try
            {
                FlushSession();
            }
            catch (Exception ex)
            {
                AppServices.Log.Exception(ex).WithMessage("NHibernate Session Module. Error trying to flush the session.");
            }
        }

        private static bool RequestRequiresSessionUpdate(HttpRequest request)
        {
            var safeMethods = new[] { "HEAD", "GET", "OPTIONS", "TRACE" };

            var isSafe = safeMethods.Contains(request.HttpMethod);

            return !isSafe;
        }

        private static void FlushSession()
        {
            var session = ObjectFactory.GetInstance<ISession>();
            
            if (session == null) return;            

            if (session.Transaction != null && session.Transaction.IsActive)
            {
                session.Transaction.Rollback();
            }
            else
            {
                session.Flush();
            }

            session.Close();
        }
    }
}
