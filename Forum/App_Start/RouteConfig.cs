using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Forum
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "NewPost",
                url: "topic/{idTopic}/newpost",
                defaults: new { controller = "Forum", action = "NewPost"}
            );
            routes.MapRoute(
                name: "NewTopic",
                url: "section/{idSection}/newtopic",
                defaults: new { controller = "Forum", action = "NewTopic"}
            );
            routes.MapRoute(
                name: "Section",
                url: "section/{idSection}/{page}",
                defaults: new { controller = "Forum", action = "Section", page = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Topic",
                url: "topic/{idTopic}/{page}",
                defaults: new { controller = "Forum", action = "Topic", page = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Forum", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
