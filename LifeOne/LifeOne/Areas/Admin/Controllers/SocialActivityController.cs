using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.API;
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Admin.Controllers
{
    [SessionTimeoutAttributeAdmin]
    public class SocialActivityController : Controller
    {
        // GET: Admin/SocialActivity
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SendSMSList()
        {
            MSendSMS _result = new MSendSMS();
            try
            {
                SendSMSService _objService = new SendSMSService();

                _result.LstSendSms = _objService.GetListForSendSMS(_result);

            }
            catch (Exception)
            {
                throw;
            }
            return View(_result);
        }

        public ActionResult SendNotificationList()
        {
            MSendNotification _result = new MSendNotification();
            try
            {
                SendNotificationService _objService = new SendNotificationService();

                _result.LstSendNotification = _objService.GetListForSendNotification(_result);

            }
            catch (Exception)
            {
                throw;
            }
            return View(_result);
        }


        [HttpPost]
        public JsonResult SendSMSDetails(string Fk_MemId)
        {
            MSendSMS _result = new MSendSMS();
            try
            {
                _result.FK_MemId = Convert.ToInt64(Fk_MemId);
                SendSMSService _objService = new SendSMSService();

                _result.LstSendSms = _objService.GetDetailsForSendSMS(_result);

            }
            catch (Exception)
            {
                throw;
            }
            return Json(_result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SendSMSUser(string Fk_MemId, string SMSContent, List<string> Mobile)
        {
            MSendSMS _result = new MSendSMS();
            SMSResponse res = new SMSResponse();
            try
            {
                if (Mobile != null && Mobile.Count > 0)
                {
                    for (var i = 0; i < Mobile.Count; i++)
                    {
                      string a = Mobile[0];
                       LifeOne.Models.API.SMS.SendSMS(Mobile[i], SMSContent);
                    }
                }
                SendSMSService _objService = new SendSMSService();
                _result.FK_MemId = Convert.ToInt64(Fk_MemId);
                res = _objService.UpdateSms(_result);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SendNotificationUser(string Fk_MemId, string Tittle,string Message, List<string> DeviceId)
        {
            MSendNotification _result = new MSendNotification();
            SendNotificationResponse res = new SendNotificationResponse();
            try
            {
                if (DeviceId != null && DeviceId.Count > 0)
                {
                    for (var i = 0; i < DeviceId.Count; i++)
                    {
                        string abc = SendNotification.SendNotificationForMobile(DeviceId[i], Tittle, Message);
                    }
                }
                SendNotificationService _objService = new SendNotificationService();
                _result.FK_MemId = Convert.ToInt64(Fk_MemId);
                res = _objService.UpdateNotification(_result);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SendNotificationDetails(string Fk_MemId)
        {
            MSendNotification _result = new MSendNotification();
            try
            {
                _result.FK_MemId = Convert.ToInt64(Fk_MemId);
                SendNotificationService _objService = new SendNotificationService();
                _result.LstSendNotification = _objService.GetDetailsForSendNotification(_result);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(_result, JsonRequestBehavior.AllowGet);
        }

        //Email 
        public ActionResult EmailRequest()
        {
            MEmailSend _result = new MEmailSend();
            try
            {
                EmailSendService _objService = new EmailSendService();
                _result.LstSendEmail = _objService.GetListForSendEmail(_result);
            }
            catch (Exception)
            {
                throw;
            }
            return View(_result);
        }

        [HttpPost]
        public JsonResult SendEmailDetails(string Fk_MemId)
        {
            MEmailSend _result = new MEmailSend();
            try
            {
                _result.FK_MemId = Convert.ToInt64(Fk_MemId);
                EmailSendService _objService = new EmailSendService();
                _result.LstSendEmail = _objService.GetDetailsForSendEmail(_result);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(_result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SendEmailUser(string Fk_MemId,string Subject, string Message,string FilePath, List<string> Email)
        {
            MEmailSend _result = new MEmailSend();
            EmailSendResponse res = new EmailSendResponse();
            try
            {
                if (Email != null && Email.Count > 0)
                {
                    for (var i = 0; i < Email.Count; i++)
                    {
                        Common.SendEmailByAPICommon(Email[i],"","",Subject,Message, FilePath);
                    }
                }
                EmailSendService _objService = new EmailSendService();
                _result.FK_MemId = Convert.ToInt64(Fk_MemId);
                res = _objService.UpdateEmail(_result);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}