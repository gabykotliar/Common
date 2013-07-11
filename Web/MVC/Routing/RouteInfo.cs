using System.Web.Routing;

namespace Common.Web.Mvc.Routing
{
    public class RouteInfo
    {
        public static System.Web.Routing.RouteData GetRouteDataByUrl(string url)
        {
            return RouteTable.Routes.GetRouteData(new RewritedHttpContextBase(url));
        }
    }
}