
using LifeOne.Models.API.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LifeOne.Filters
{
    public class ValidCustomer : ActionFilterAttribute
    {
        Resultresponse objresult = new Resultresponse();
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            CustomerDb Objdb = new CustomerDb();
            IEnumerable<string> DeviceType=null;
            if (actionContext.Request.Headers.Contains("DeviceType"))
            {
                DeviceType = actionContext.Request.Headers.GetValues("DeviceType");
            }
            if (DeviceType!=null)
            {

            }
            else
            {
                base.OnActionExecuting(actionContext);
                //if (actionContext.Request.Headers.Contains("androidId"))
                //{
                //    IEnumerable<string> headerValues = actionContext.Request.Headers.GetValues("androidId");
                //    var androidId = headerValues.FirstOrDefault();

                //    var res = Objdb.ClientIdWithandroid(androidId);
                //    if (res != null)
                //    {
                //        base.OnActionExecuting(actionContext);
                //    }
                //    else
                //    {
                //        objresult.response = "error";
                //        objresult.exception = "You are unauthorized to access this resource";
                //        string result = EncryptResult.EncryptData(objresult);
                //        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, new { body = result });
                //    }

                //}
                //else
                //{

                //    objresult.response = "error";
                //    objresult.exception = "Unregistered bussiness flow";
                //    string result = EncryptResult.EncryptData(objresult);
                //    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, new { body = result });

                //}
            }

        }
    }


}