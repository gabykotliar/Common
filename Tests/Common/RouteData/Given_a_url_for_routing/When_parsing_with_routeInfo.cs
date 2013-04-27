using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Common.RouteData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.Common.RouteData.Given_a_url_for_routing
{
    [TestClass]
    class When_parsing_with_routeInfo
    {
        [TestInitialize]
        public void SetupTest()
        {
            SetHttpContext();
        }

        [TestMethod]
        public void When_using_LogIfException_exception_is_handled()
        {
            var uri = new Uri("error/NotFound404");
            var routeInfo = new RouteInfo(uri, HttpContext.Current.Request.ApplicationPath);

            // Here it is...  
            var routeData = routeInfo.RouteData;
        }

        private void SetHttpContext()
        {
            var context = new Mock<HttpContextBase>();

            context.SetupGet(ctx => ctx.Request.ApplicationPath).Returns("Scrumboard");

        }
    }
}
