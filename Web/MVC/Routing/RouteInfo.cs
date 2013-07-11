using System;
using System.Web.Routing;

namespace Common.Web.Mvc.Routing
{
    public class RouteInfo
    {
        /// <summary>
        /// Creates route data based on a url expressed as a string.
        /// </summary>
        /// <param name="appRelativeUrl">
        /// The relative path of the route you want to parse as to be provided to <see cref="System.Web.HttpRequestBase.AppRelativeCurrentExecutionFilePath"/>.
        /// 
        /// As on documentation:
        /// Summary:
        /// When overridden in a derived class, gets the virtual path of the application root and makes it relative by using the tilde (~) notation for the application root (as in "~/page.aspx").
        /// 
        /// Returns:
        /// The virtual path of the application root for the current request with the tilde operator (~) added.
        /// </param>
        public static RouteData GetRouteDataByUrl(string appRelativeUrl)
        {
            if (!appRelativeUrl.StartsWith(@"~/")) throw new ArgumentException("The value of the specified URL is invalid. It should start with '~/'as it uses System.Web.HttpRequestBase.AppRelativeCurrentExecutionFilePath. Please check the documentation for that property ", "appRelativeUrl");

            return RouteTable.Routes.GetRouteData(new RewritedHttpContextBase(appRelativeUrl));
        }
    }
}