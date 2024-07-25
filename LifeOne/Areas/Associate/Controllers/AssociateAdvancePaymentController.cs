using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.AssociateManagement.AssociateService;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Associate.Controllers
{
    public class AssociateAdvancePaymentController : Controller
    {
        AssociateAdvancePaymentService _Obj = new AssociateAdvancePaymentService();
        // GET: Associate/AssociateAdvan cePayment 
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdvancePaymentRequest()
        {
            return View();
        }
        public JsonResult SubmitAdvancePaymentRequest(AdvancePaymentModel req)
        {
            req.FK_MID = SessionManager.AssociateFk_MemId;
            return Json(_Obj.SubmitAdvancePaymentRequest(req));
        }
        public JsonResult GetAllAdvancePaymentRequest()
        {

            return Json(_Obj.GetAllAdvancePaymentRequest((int)SessionManager.AssociateFk_MemId));
        }

        public JsonResult CheckLoanEligibility()
        {
            return Json(_Obj.CheckLoanEligibility((int)SessionManager.AssociateFk_MemId));
        }
        public JsonResult ViewAdvancePaymenthistory(int ReqId = 0)
        {
            try
            {
                return Json(_Obj.ViewAdvancePaymenthistory(ReqId));
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}