using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.Controllers;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http;
using System.Net.Sockets;

using System.Configuration;

namespace LifeOne.Models.API
{

    public class AuthorizeIPAddressAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            string ipAddress = HttpContext.Current.Request.UserHostAddress;
            if (!IsIpAddressAllowed(ipAddress.Trim()))
            {
                throw new HttpException(401, "Forbidden");
            }
            base.OnActionExecuting(context);
        }
        private bool IsIpAddressAllowed(string IpAddress)
        {
            if (!string.IsNullOrWhiteSpace(IpAddress))
            {
                string[] addresses = Convert.ToString(ConfigurationManager.AppSettings["AllowedIPAddresses"]).Split(',');
                return addresses.Where(a => a.Trim().Equals(IpAddress, StringComparison.InvariantCultureIgnoreCase)).Any();
            }
            return false;
        }
    }
}