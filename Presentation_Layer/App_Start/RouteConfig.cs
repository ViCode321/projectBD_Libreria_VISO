using System.Web.Mvc;
using System.Web.Routing;

namespace Presentation_Layer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Search",
                url: "Search",
                defaults: new { controller = "Home", action = "Search" }
            );

            routes.MapRoute(
                name: "Sale",
                url: "Sale",
                defaults: new { controller = "Home", action = "Sale" }
            );

            routes.MapRoute(
                name: "Insert",
                url: "Insert",
                defaults: new { controller = "Home", action = "Insert" }
            );

            routes.MapRoute(
                name: "Update",
                url: "Update",
                defaults: new { controller = "Home", action = "Update" }
            );
        }
    }
}
