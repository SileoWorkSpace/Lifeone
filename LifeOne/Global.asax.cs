using LifeOne.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace LifeOne
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)        
        {
            Exception exception = Server.GetLastError();
           
            Response.Clear();
            HttpContext con = HttpContext.Current;
            con.Request.Url.ToString();
            /*Error details will be sent to the developer team along with page url and error. So That they can resolved ASAP*/

           // Response.Redirect("/Home/CustomErrorPage");

        }
    }
}
