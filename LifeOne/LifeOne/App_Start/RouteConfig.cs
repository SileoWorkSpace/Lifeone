using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LifeOne
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(name: "Admin", url: "Admin/{id}", defaults: new
            {
                controller = "Home",
                action = "AdminLogin",
                id = UrlParameter.Optional
            });

            routes.MapRoute(name: "Franchise_Login", url: "franchise_Login/{id}", defaults: new
            {
                controller = "Home",
                action = "FranchiseLogin",
                id = UrlParameter.Optional
            });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                 // defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 // new string[] { "LifeOne.Controllers" }
                 namespaces: new[] { "LifeOne.Controllers" }
            ); 
        }
    }
}


