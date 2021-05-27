using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LibraryOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Chi tiết tài liệu
            routes.MapRoute(
                name: "Document detail",
                url: "{Metatitle}-{ID}",
                defaults: new { controller = "Document", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "LibraryOnline.Controllers" }
            );

            //tài liệu theo danh mục
            routes.MapRoute(
                name: "Document category",
                url: "danh-muc/{link}-{ID}",
                defaults: new { controller = "Home", action = "GetDocByCate", id = UrlParameter.Optional },
                namespaces: new[] { "LibraryOnline.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "LibraryOnline.Controllers" }
            );
        }
    }
}
