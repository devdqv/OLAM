using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OLAM
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "xuong-say-kho",
             url: "xuong-say-kho",
             defaults: new { controller = "Home", action = "XuongSayKho", id = UrlParameter.Optional }
         );

            routes.MapRoute(
               name: "xuong-tach-vo",
               url: "xuong-tach-vo",
               defaults: new { controller = "Home", action = "XuongTachVo", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
