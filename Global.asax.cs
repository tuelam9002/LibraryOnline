using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LibraryOnline
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //public static StarDocs starDocs;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //setup 
            //starDocs = new StarDocs(
            //    new ConnectionInfo(new Uri("https://api.gnostice.com/stardocs/v1"),
            //    "2021010410000236","5a757cfc485d4245b813d4463a2e77a6"));
            //starDocs.Auth.loginApp();
        }
    }
}
