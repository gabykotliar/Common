using System;
using System.Web.Mvc;
using System.Web.Routing;
using Common.Web.Mvc.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Common.RouteData.Given_a_url_for_routing
{
    [TestClass]
    public class WhenParsingWithRouteInfo
    {
        [ClassInitialize]
        public static void SetupTest(TestContext context)
        {
            SetRoutes();
        }

        private static void SetRoutes()
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            RouteTable.Routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void InvalidUrlShouldThrowAnException()
        {
            RouteInfo.GetRouteDataByUrl(@"error/notFound404");
        }

        [TestMethod]
        public void ValuesShouldBeParsedCorrectly()
        {
            var routeData = RouteInfo.GetRouteDataByUrl(@"~/error/notFound404");

            Assert.AreEqual(routeData.Values["controller"], "error");
            Assert.AreEqual(routeData.Values["action"], "notFound404");
        }
    }
}
