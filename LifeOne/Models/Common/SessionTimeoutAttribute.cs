using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LifeOne.Models.Common
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["AssociateFk_MemId"] == null || HttpContext.Current.Session["IsBusinessId"] == null)
            {
                HttpContext.Current.Session["AssociateFk_MemId"] = "Your Session Is Expired Now, Please Login Again";
                filterContext.Result = new RedirectResult("/Home/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class SessionTimeoutAttributeFranchise : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["FranchiseFk_MemId"] == null)
            {

                HttpContext.Current.Session["FranchiseFk_MemId"] = "Your Session Is Expired Now, Please Login Again";
                filterContext.Result = new RedirectResult("/Home/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class SessionTimeoutAttributeAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        
        
        {
            if (HttpContext.Current.Session["Fk_MemId"] == null)
            {
                HttpContext.Current.Session["Fk_MemId"] = "Your Session Is Expired Now, Please Login Again";
                filterContext.Result = new RedirectResult("/Home/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
       
    }
}