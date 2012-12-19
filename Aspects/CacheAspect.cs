using System;
using System.Text;

using PostSharp.Aspects;

namespace Common.Aspects
{
    [Serializable]
    public class CacheAspect : OnMethodBoundaryAspect
    {
        public string Profile { get; set; }
        public string SlidingExpiryWindow { get; set; }

        public override void OnEntry(MethodExecutionArgs args)
        {
            try
            {
                var returnValue = AppServices.Cache.Get<object>(BuildKey(args));

                if (returnValue == null) return;

                args.ReturnValue = returnValue;
                args.FlowBehavior = FlowBehavior.Return;
            }
            catch (Exception)
            {
                // TODO: Add log    
                throw;
            }
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            // TODO: Add log
            try
            {
                if (!string.IsNullOrWhiteSpace(SlidingExpiryWindow))
                    AppServices.Cache.Add(BuildKey(args), args.ReturnValue, TimeSpan.FromSeconds(int.Parse(SlidingExpiryWindow)));
                else if (!string.IsNullOrWhiteSpace(Profile))
                    AppServices.Cache.Add(BuildKey(args), args.ReturnValue, Profile);
                else
                    AppServices.Cache.Add(BuildKey(args), args.ReturnValue);
            }
            catch (Exception)
            {
                // TODO: Add log    
                throw;
            }            
        }        

        private static string BuildKey(MethodExecutionArgs args)
        {
            var i = 0;
            var paramsKeySection = new StringBuilder();

            foreach (var parameterInfo in args.Method.GetParameters())
            {
                paramsKeySection.AppendFormat("{0}={1}&", parameterInfo.Name, args.Arguments[0]);
                i++;
            }

            var key = string.Concat(args.Method.DeclaringType.FullName, ".", args.Method.Name,"?", paramsKeySection.ToString());

            return key;
        }
    }
}
