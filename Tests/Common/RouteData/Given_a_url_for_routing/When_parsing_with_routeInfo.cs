using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Common.RouteData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.Common.RouteData.Given_a_url_for_routing
{
    [TestClass]
    public class When_parsing_with_routeInfo
    {
        [TestInitialize]
        public void SetupTest()
        {
        }

        private void SetRoutes()
        {
            RouteTable.Routes.Add(
                new Route(
                    "{controller}/{action}",
                    new RouteValueDictionary { { "controller", "Error" }, { "action", "notFound404" } },
                    new MvcRouteHandler())
                );
        }

        [TestMethod]
        public void Values_should_be_parsed_correctly()
        {
            SetRoutes();
            
            var url = @"~/error/notFound404";
            
            var routeData = RouteInfo.GetRouteDataByUrl(url);

            Assert.AreEqual(routeData.Values["controller"], "error");
            Assert.AreEqual(routeData.Values["action"], "notFound404");
        }
    }
}
