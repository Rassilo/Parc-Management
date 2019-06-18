using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ParcProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute( 
             "assign",
             "Article/Assign",
             new {   controller = "Article",
                 action = "Assign",
             });
            routes.MapRoute(
             "assignId",
             "Article/Assign/{id}",
             new  {   controller = "Article",
                 action = "AssignId",
             });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Article", action = "Index", id = UrlParameter.Optional }
            );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }
    }
}
