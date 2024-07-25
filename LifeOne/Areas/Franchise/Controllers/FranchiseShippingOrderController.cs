using ClosedXML.Excel;
using LifeOne.Filters;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.Common;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.FranchiseManagement.FService;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Franchise.Controllers
{
 [SessionTimeoutAttributeFranchise]
    public class FranchiseShippingOrderController : Controller
    {
        // GET: Franchise/FranchiseShippingOrder/ViewShippingDetails
        public ActionResult ViewShippingDetails(MAssignShippingInfo _objparam, int? Page)
        {
            try
            {
                if (Page == null || Page == 0)
                {
                    Page = 1;
                }
                _objparam.Page = Page;

                _objparam.objlist = AssignShippingService.AssignOrderShippingService(_objparam);
                //if (_objparam.Name != null)
                //{
                //    _objparam.Name = _objparam.Name;
                //}
                int totalRecords = 0;
                if (_objparam.objlist.Count > 0)
                {

                    totalRecords = Convert.ToInt32(_objparam.objlist[0].TotalRecord);
                    var pager = new Pager(totalRecords, _objparam.Page, SessionManager.Size);
                    _objparam.Pager = pager;
                }
                else
                {
                    _objparam.objlist = new List<MAssignShippingInfo>();
                }
                return View(_objparam);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult ExportToExcelShippingDetails(MAssignShippingInfo _objparam, int? Page)
        {
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            _objparam.Page = Page;

            var customers = AssignShippingService.AssignOrderShippingService(_objparam);
            DataTable dt = new DataTable("ShippingDetails");
            dt.Columns.AddRange(new DataColumn[6] {
                                                     new DataColumn("Transfer Invoice NO"),
                                                     new DataColumn("Customer"),
                                                     new DataColumn("Address Details"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("Email"),
                                                     new DataColumn("Shipping Status")
            });
            customers = customers.GroupBy(x => x.FK_FbSID).Select(g => g.First()).ToList();
            if (customers.Count > 0)
            {
                customers.ForEach(customer =>
                {
                    dt.Rows.Add(customer.TransferInvoiceNo, customer.FirstName, customer.Address1+ "\n" +customer.OtherAdrInfo, customer.mobile,customer.Email,customer.ShippingStatus=="1"? customer.ShippingDate +"\n"+"Shipped": "Pending");
                });
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ShippingDetails.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("FranchiseSellDateWise");
            }
        }


        // GET: Franchise/FranchiseShippingOrder/ViewShippingDetails
        public JsonResult JsonViewShippingDetails(MAssignShippingInfo _objparam)
        {
            try
            {
                List<MAssignShippingInfo> _objlst = AssignShippingService.ViewAssignProductDetailsService(_objparam);
                return Json(_objlst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult AddShipingInfroamtion(MAddShippingInformation _param)
        {
            try
            {
                _param.Fk_FId = SessionManager.FranchiseFk_MemId.ToString();
                int _iresult = AddShippingDetailsService.AddAssignShippingInformationService(_param);
                return Json(_iresult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult GetEditFranchiseItemInformation(string RequestId)
        {
            try
            {
                long FId = SessionManager.FranchiseFk_MemId;
                List<MAddShippingInformation> MemberList = AddShippingDetailsService.GetAssignEditOrderService(RequestId,FId);
                return Json(MemberList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult GetShippingInvoice(int FK_FbSID)
        {
            try
            {
                long FranchiseId = SessionManager.FranchiseFk_MemId;
                ShippingDetailViewModel model = AddShippingDetailsService.GetShippingInvoice(FranchiseId, FK_FbSID);
                return View(model);

            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}