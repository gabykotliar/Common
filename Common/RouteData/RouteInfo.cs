using System;
using System.Web;
using System.Web.Routing;

namespace Common.RouteData
{
    public class RouteInfo
    {
        public static System.Web.Routing.RouteData GetRouteDataByUrl(string url)
        {
            return RouteTable.Routes.GetRouteData(new RewritedHttpContextBase(url));
        }
    }

    internal class RewritedHttpContextBase : HttpContextBase
    {
        private readonly HttpRequestBase mockHttpRequestBase;

        public RewritedHttpContextBase(string appRelativeUrl)
        {
            this.mockHttpRequestBase = new MockHttpRequestBase(appRelativeUrl);
        }


        public override HttpRequestBase Request
        {
            get { return mockHttpRequestBase; }
        }

        private class MockHttpRequestBase : HttpRequestBase
        {
            private readonly string appRelativeUrl;

            public MockHttpRequestBase(string appRelativeUrl)
            {
                this.appRelativeUrl = appRelativeUrl;
            }

            public override string AppRelativeCurrentExecutionFilePath
            {
                get { return appRelativeUrl; }
            }

            public override string PathInfo
            {
                get { return ""; }
            }
        }
    }
}