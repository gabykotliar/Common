using System.Web;

namespace Common.Web.Mvc.Routing
{
    internal class RewritedHttpContextBase : HttpContextBase
    {
        private readonly HttpRequestBase mockHttpRequestBase;

        public RewritedHttpContextBase(string appRelativeUrl)
        {
            mockHttpRequestBase = new MockHttpRequestBase(appRelativeUrl);
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