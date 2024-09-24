using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.AssociateManagement.AssociateService;
using LifeOne.Models.API.DAL;
using LifeOne.Models.API;
using System.Data;
using LifeOne.Models.Manager;
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Associate.Controllers
{
    public class TopupByAssociateController : Controller
    {        
        public ActionResult Index()
        {
            return View();
        }         
        public ActionResult TopupbyAssociate()
        {
            MemberTopup para = new MemberTopup();
            PackageMaster model = new PackageMaster();
            ViewBag.ddlPackageList = BindPckageMaster.BindPackageMasterAssociate();
            return View();            
        }
        [HttpPost]
        public ActionResult TopupbyAssociate(MemberTopup obj)
        {
            MemberTopup para = new MemberTopup();
            para.FK_MemId = SessionManager.AssociateFk_MemId;
            para.FK_PackageId = obj.FK_PackageId;
            para.PaidAmount = obj.PaidAmount;
            para.AddedBy = SessionManager.AssociateFk_MemId;  
            
            DataTable dt = obj.TopUpMemberByAssociate(para);
            if (dt.Rows.Count > 0)
            {              
                if (dt.Rows[0]["Msg"].ToString() == "1")
                {
                    ViewBag.Alert = "You are upgraded successfully";
                }
                else if (dt.Rows[0]["Msg"].ToString() == "0")
                {
                    ViewBag.Alert = dt.Rows[0]["Error"].ToString();
                }
                else
                {
                    ViewBag.Alert = "Error!!!";
                }
            }
            else
            {
                ViewBag.Alert = "Error!!!";
            }
          ViewBag.ddlPackageList = BindPckageMaster.BindPackageMasterAssociate();
            return View();
        }
    }
}