using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Filters
{
    public class AuthorizeAdmin : AuthorizeAttribute, IAuthorizationFilter
    {
        public override void OnAuthorization(AuthorizationContext filtercontext)
        {
            if (filtercontext != null)
            {
                if (filtercontext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || filtercontext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                {
                    return;
                }
                if (filtercontext != null && string.IsNullOrEmpty(SessionManager.Fk_MemId.ToString()) && string.IsNullOrEmpty (SessionManager.Usertype))
                {
                    filtercontext.Result = new RedirectResult("~/Home/Login");
                }
                if (filtercontext != null && SessionManager.Fk_MemId.ToString()=="0"  && string.IsNullOrEmpty(SessionManager.Usertype))
                {
                    filtercontext.Result = new RedirectResult("~/Home/Login");
                }
            }
        }
    }
}