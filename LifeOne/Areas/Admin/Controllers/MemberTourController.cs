using ClosedXML.Excel;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.API;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Admin.Controllers
{
    [SessionTimeoutAttributeAdmin]
    public class MemberTourController : Controller
    {
        // GET: Admin/MemberTour
        string Aeskey = ConfigurationManager.AppSettings["Aeskey"].ToString();
        public ActionResult TourRequestList(TourDetailViewModel reqModel)
        {
            if (!PermissionManager.IsActionPermit("Approve Tour", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            TourDetailViewModel model = new TourDetailViewModel();
            model = DALTourDetail.TourDetailList(reqModel);

            if (model.TourDetailList.Count > 0)
                model.Pager = new Pager(model.TourDetailList[0].TotalRecords, reqModel.Page, SessionManager.Size);
            else
                model.TourDetailList = new List<TourDetailViewModel>();
            return View(model);
        }
        public ActionResult ApproveMemberTour(TourDetailViewModel reqModel)
        {
            if (!PermissionManager.IsActionPermit("Approve Tour", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            TourDetailViewModel model = new TourDetailViewModel();
            model = DALTourDetail.ApproveMemberTour(reqModel);
            var data = model.TourDetailList.GroupBy(x => x.Tour_PKid).Select(x => new Tour_PassengerDetailViewModel
            {
                Tour_PKid = x.FirstOrDefault().Tour_PKid,
                TotalPassenger = x.FirstOrDefault().TotalPassenger,
                TotalDays = x.FirstOrDefault().TotalDays,
                TotalNights = x.FirstOrDefault().TotalNights,
                TourID = x.FirstOrDefault().TourID,
                LoginId = x.FirstOrDefault().LoginId,
                Name = x.FirstOrDefault().Name,
                Mobile = x.FirstOrDefault().Mobile,
                TourType = x.FirstOrDefault().TourType,
                Source = x.FirstOrDefault().Source,
                Destination = x.FirstOrDefault().Destination,
                Your_Budget = x.FirstOrDefault().Your_Budget,
                TourStatus = x.FirstOrDefault().TourStatus,
                From_Dt=x.FirstOrDefault().From_Dt,
                To_Dt=x.FirstOrDefault().To_Dt,
                HotelCategory = x.FirstOrDefault().HotelCategory,
                CompleteUpComing = x.FirstOrDefault().CompleteUpComing,
                ReturnAirTour = x.FirstOrDefault().ReturnAirTour,
                IsPaid = x.FirstOrDefault().IsPaid,
                PassengerList = x.ToList().Select(y => new PassengerDetailModel
                {
                    Name = y.PassengerName,
                    Gender = y.Gender,
                    Age = y.Age,
                    PassengerType = y.PassengerType
                }).ToList()
            }).FirstOrDefault();

            return View(data);
        }

        [HttpPost]
        public JsonResult ApproveMemberTourRequest(TourDetailViewModel reqModel)
        {
            TourDetailViewModel model = new TourDetailViewModel();
            model = DALTourDetail.ApproveMemberTourRequest(reqModel);
            TempData["code"] = model.Flag.ToString();
            TempData["msg"] = model.Remark;
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeclineMemberTourRequest(TourDetailViewModel reqModel)
        {
            TourDetailViewModel model = new TourDetailViewModel();
            model = DALTourDetail.DeclinedMemberTourRequest(reqModel);
            TempData["code"] = model.Flag.ToString();
            TempData["msg"] = model.Remark;
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportToExcelTourRequests()
        {
            TourDetailViewModel reqModel = new TourDetailViewModel();
            var customers = DALTourDetail.ExportToExcelTourRequests(reqModel);
            DataTable dt = new DataTable("TourRequestList");
            dt.Columns.AddRange(new DataColumn[10] {
                                                     new DataColumn("LoginId"),
                                                     new DataColumn("Name"),
                                                     new DataColumn("Mobile No"),
                                                     new DataColumn("Tour ID"),
                                                     new DataColumn("Tour Type"),
                                                     new DataColumn("Source"),
                                                     new DataColumn("Destination"),
                                                     new DataColumn("From Date-To Date"),
                                                     new DataColumn("Passenger"),
                                                     new DataColumn("Budget")


            });

            if (customers.TourDetailList.Count > 0)
            {
                foreach (var customer in customers.TourDetailList)
                {
                    dt.Rows.Add(customer.LoginId, customer.Name, customer.Mobile, customer.TourID, customer.Source, customer.Destination, customer.From_Dt+" "+customer.To_Dt, customer.TotalPassenger, customer.Your_Budget);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MemberTourRequestList.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("/Admin-TourRequest");
            }
            // return View();
        }
    }
}