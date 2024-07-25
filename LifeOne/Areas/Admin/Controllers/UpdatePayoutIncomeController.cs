using ClosedXML.Excel;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace LifeOne.Areas.Admin.Controllers
{
    [SessionTimeoutAttributeAdmin]
    public class UpdatePayoutIncomeController : Controller
    {
        // GET: Admin/UpdatePayoutIncome/UpdateNow
        public 
            
            ActionResult UpdateNow()
        {
            if (!PermissionManager.IsActionPermit("Update Payout Income", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            return View();
        }

        public JsonResult PayoutNoList(MPayoutReport obj)
        {
            obj = AdminReportsService.GetPayoutReportService(1, obj);
            if (obj.Objlist.Count > 0)
            {
                var data = obj.Objlist.GroupBy(x => x.LoginId).Select(x => new
                {
                    DisplayName = x.FirstOrDefault().DisplayName,
                    PayoutNoList = x.ToList().OrderBy(z => z.PayoutNo).Select(y => y.PayoutNo).ToList()
                }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateNow(MPayoutReport model)
        {
            if (!PermissionManager.IsActionPermit("Update Payout Income", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            return View();
        }

        [HttpPost]
        public JsonResult IncomeReport(string LoginId, string PayoutNo)
        {
            try
            {

                MPayoutReport obj = new MPayoutReport();
                obj.LoginId = LoginId;
                obj.PayoutNo = Convert.ToInt16(PayoutNo);
                int? Page = 1;
                obj = AdminReportsService.GetPayoutReportService(Page, obj);
                return Json(obj.Objlist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult SavePayoutIncomeDetail(MPayoutReport model)
        {
            try
            {

                MPayoutReport obj = new MPayoutReport();
                model.UpdateBy = SessionManager.Fk_MemId;
                obj = AdminReportsService.SavePayoutIncomeDetail(model);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public ActionResult RepurchasePayoutReport()
        {
            MPayoutReport model = new MPayoutReport();

            if (!PermissionManager.IsActionPermit("Repurchase Payout Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            MPayoutReport  obj = AdminReportsService.GetRepurchasePayoutReportService(model);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindRepurchasePayoutMaster();
            Session["listPayoutdetails"] = model.Objlist;
           
            return View(obj);
        }


        [HttpPost]
        public ActionResult RepurchasePayoutReport(MPayoutReport model)
        {
           

            if (!PermissionManager.IsActionPermit("Repurchase Payout Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            MPayoutReport obj = AdminReportsService.GetRepurchasePayoutReportService(model);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindRepurchasePayoutMaster();
            Session["listPayoutdetails"] = model.Objlist;
            //DALAdminReports _obj = new DALAdminReports();
            if (model.IsExport == 1)
            {
               
                model.Objlist = DALAdminReports.GetRepurchasePayoutReport(model);
                DataTable dt = new DataTable("Repurchase Payout Report");
                dt.Columns.AddRange(new DataColumn[15]
                {
                                    new DataColumn("Login Id"),
                                    new DataColumn("Display Name"),
                                    new DataColumn("Payout No"),
                                    new DataColumn("Hybrid Matching Bonus"),
                                    new DataColumn("Leadership Bonus"),
                                    new DataColumn("House Fund"),
                                    new DataColumn("Director Club"),
                                    new DataColumn("ChairMan Club"),
                                    new DataColumn("LAR Bonus"),
                                    new DataColumn("SG Yearly Bonus"),
                                    new DataColumn("Gross Amount"),
                                    new DataColumn("Processing Fee"),
                                    new DataColumn("TDS Amount"),
                                    new DataColumn("Net Amount"),
                                    new DataColumn("Closing Date")
                });
                if (model.Objlist.Count > 0)
                {
                    foreach (var customer in model.Objlist)
                    {
                        dt.Rows.Add(customer.LoginId, customer.DisplayName, customer.PayoutNo, customer.HybridMatchingBonus, customer.LeadershipBonus, customer.HouseFund, customer.DirectorClub, customer.ChairManClub, customer.LARBonus, customer.SGYearlyBonus, customer.GrossAmount, customer.ProcessingFee, customer.TDSAmount, customer.NetAmount, customer.ClosingDate);
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);

                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Repurchase Payout Report.xlsx");
                        }
                    }
                }
            }

            return View(obj);
        }

       
        public ActionResult RepurchaseIncomeType(MPayoutReport model,string IncomeType)
        {
           
            
            if(IncomeType== "HMB")
            {
                if (!PermissionManager.IsActionPermit("Hybrid Matching Bonus Report", ViewOptions.FormView))
                {
                    return Redirect("/Home/adminlogin");
                }
                ViewBag.IncomeType = "Hybrid Matching Bonus";
                ViewBag.IncomeType1 = "HMB";
                model.IncomeType = "HMB";
            }
            else if (IncomeType == "LeadershipB")
            {
                if (!PermissionManager.IsActionPermit("Leadership Bonus Report", ViewOptions.FormView))
                {
                    return Redirect("/Home/adminlogin");
                }
                ViewBag.IncomeType = "Leadership Bonus";
                ViewBag.IncomeType1 = "LeadershipB";
                model.IncomeType = "LeadershipB";
            }
           else if (IncomeType == "HouseFund")
            {
                if (!PermissionManager.IsActionPermit("House Fund Report", ViewOptions.FormView))
                {
                    return Redirect("/Home/adminlogin");
                }
                ViewBag.IncomeType = "House Fund";
                ViewBag.IncomeType1 = "HouseFund";
                model.IncomeType = "HouseFund";
            }
           else if (IncomeType == "CarFund")
            {
                if (!PermissionManager.IsActionPermit("Car Club Report", ViewOptions.FormView))
                {
                    return Redirect("/Home/adminlogin");
                }
                ViewBag.IncomeType = "Car Club";
                ViewBag.IncomeType1 = "CarFund";
                model.IncomeType = "CarFund";
            }
            else if (IncomeType == "DirectorClub")
            {
                if (!PermissionManager.IsActionPermit("Director Club Report", ViewOptions.FormView))
                {
                    return Redirect("/Home/adminlogin");
                }
                ViewBag.IncomeType = "Director Club";
                ViewBag.IncomeType1 = "DirectorClub";
                model.IncomeType = "DirectorClub";
            }
           else if (IncomeType == "ChairmanClub")
            {
                if (!PermissionManager.IsActionPermit("Chairman Club Report", ViewOptions.FormView))
                {
                    return Redirect("/Home/adminlogin");
                }
                ViewBag.IncomeType = "Chairman Club";
                ViewBag.IncomeType1 = "ChairmanClub";
                model.IncomeType = "ChairmanClub";
            }
            else if (IncomeType == "LAR")
            {
                if (!PermissionManager.IsActionPermit("Lifetime Achievement Royalty Report", ViewOptions.FormView))
                {
                    return Redirect("/Home/adminlogin");
                }
                ViewBag.IncomeType = "LAR (Lifetime Achievement Royalty)";
                ViewBag.IncomeType1 = "LAR";
                model.IncomeType = "LAR";
            }
           else if (IncomeType == "SGYearlyBonus")
            {
                if (!PermissionManager.IsActionPermit("SG Yearly Bonus Report", ViewOptions.FormView))
                {
                    return Redirect("/Home/adminlogin");
                }
                ViewBag.IncomeType = "SG Yearly Bonus";
                ViewBag.IncomeType1 = "SGYearlyBonus";
                model.IncomeType = "SGYearlyBonus";
            }
            model = AdminReportsService.GetRepurchaseIncomePayoutReport(model);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindRepurchasePayoutMaster();

            if (model.IsExport == 1)
            {
               //HMB Paging and excel
                if (IncomeType == "HMB")
                {
                    DataTable dt = new DataTable("Hybrid Matching Bonus");
                    dt.Columns.AddRange(new DataColumn[11]
                    {
                             new DataColumn("Login Id"),
                             new DataColumn("Direct Seller"),
                             new DataColumn("Payout No"),
                             new DataColumn("Hybrid Matching Income"),
                             new DataColumn("Leadership Bonus"),
                             new DataColumn("House Fund"),
                             new DataColumn("Director Club"),
                             new DataColumn("Chairman Club"),
                             new DataColumn("LAR(Lifetime Achievement Royalty)"),
                             new DataColumn("SG Yearly Bonus"),
                             new DataColumn("Date"),
                    });
                    if (model.Objlist.Count > 0)
                    {
                        foreach (var customer in model.Objlist)
                        {
                            dt.Rows.Add(customer.LoginId, customer.FirstName, customer.PayoutNo, customer.HybridMatchingBonus, customer.LeadershipBonus, customer.HouseFund, customer.DirectorClub, customer.ChairManClub, customer.LARBonus, customer.SGYearlyBonus, customer.ClosingDate);
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                           
                            wb.Worksheets.Add(dt);
                            using (MemoryStream stream = new MemoryStream())
                            {
                              
                                wb.SaveAs(stream);

                                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Hybrid Matching Bonus.xlsx");
                            }
                        }
                    }
                    else
                    {
                        return Redirect("RepurchaseIncomeType");
                    }
                }
                //Leadership  bounus Paging and excel
                else if (IncomeType == "LeadershipB")
                {
                    DataTable dt = new DataTable("Leadership Bonus");
                    dt.Columns.AddRange(new DataColumn[5]
                    {
                             new DataColumn("Login Id"),
                             new DataColumn("Direct Seller"),
                             new DataColumn("Payout No"),
                            
                             new DataColumn("Leadership Bonus"),
                            
                             new DataColumn("Date"),
                    });
                    if (model.Objlist.Count > 0)
                    {
                        foreach (var customer in model.Objlist)
                        {
                            dt.Rows.Add(customer.LoginId, customer.FirstName, customer.PayoutNo, customer.LeadershipBonus, customer.ClosingDate);
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {

                            wb.Worksheets.Add(dt);
                            using (MemoryStream stream = new MemoryStream())
                            {

                                wb.SaveAs(stream);

                                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Leadership Bonus.xlsx");
                            }
                        }
                    }
                    else
                    {
                        return Redirect("RepurchaseIncomeType");
                    }
                }
                //House Fund Paging & Excel
                else if (IncomeType == "HouseFund")
                {
                    DataTable dt = new DataTable("House Fund Report");
                    dt.Columns.AddRange(new DataColumn[5]
                    {
                             new DataColumn("Login Id"),
                             new DataColumn("Direct Seller"),
                             new DataColumn("Payout No"),

                             new DataColumn("House Fund"),

                             new DataColumn("Date"),
                    });
                    if (model.Objlist.Count > 0)
                    {
                        foreach (var customer in model.Objlist)
                        {
                            dt.Rows.Add(customer.LoginId, customer.FirstName, customer.PayoutNo, customer.HouseFund, customer.ClosingDate);
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {

                            wb.Worksheets.Add(dt);
                            using (MemoryStream stream = new MemoryStream())
                            {

                                wb.SaveAs(stream);

                                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "House Fund Report.xlsx");
                            }
                        }
                    }
                    else
                    {
                        return Redirect("RepurchaseIncomeType");
                    }
                }

                //Director Club paging and excel
                else if (IncomeType == "DirectorClub")
                {
                    DataTable dt = new DataTable("Director Club");
                    dt.Columns.AddRange(new DataColumn[5]
                    {
                             new DataColumn("Login Id"),
                             new DataColumn("Direct Seller"),
                             new DataColumn("Payout No"),

                             new DataColumn("Director Club"),

                             new DataColumn("Date"),
                    });
                    if (model.Objlist.Count > 0)
                    {
                        foreach (var customer in model.Objlist)
                        {
                            dt.Rows.Add(customer.LoginId, customer.FirstName, customer.PayoutNo, customer.DirectorClub, customer.ClosingDate);
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {

                            wb.Worksheets.Add(dt);
                            using (MemoryStream stream = new MemoryStream())
                            {

                                wb.SaveAs(stream);

                                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Director Club.xlsx");
                            }
                        }
                    }
                    else
                    {
                        return Redirect("RepurchaseIncomeType");
                    }
                }
                //chairman paging and Excel
                else if (IncomeType == "ChairmanClub")
                {
                    DataTable dt = new DataTable("Chairman Club");
                    dt.Columns.AddRange(new DataColumn[5]
                    {
                             new DataColumn("Login Id"),
                             new DataColumn("Direct Seller"),
                             new DataColumn("Payout No"),

                             new DataColumn("Chairman Club"),

                             new DataColumn("Date"),
                    });
                    if (model.Objlist.Count > 0)
                    {
                        foreach (var customer in model.Objlist)
                        {
                            dt.Rows.Add(customer.LoginId, customer.FirstName, customer.PayoutNo, customer.ChairManClub, customer.ClosingDate);
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {

                            wb.Worksheets.Add(dt);
                            using (MemoryStream stream = new MemoryStream())
                            {

                                wb.SaveAs(stream);

                                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Chairman Club.xlsx");
                            }
                        }
                    }
                    else
                    {
                        return Redirect("RepurchaseIncomeType");
                    }
                }

                //LAR (Lifetime Achievement Royalty) pagging and Excel
                else if (IncomeType == "LAR")
                {
                    DataTable dt = new DataTable("LAR (Lifetime Achievement Royalty)");
                    dt.Columns.AddRange(new DataColumn[5]
                    {
                             new DataColumn("Login Id"),
                             new DataColumn("Direct Seller"),
                             new DataColumn("Payout No"),

                             new DataColumn("LAR (Lifetime Achievement Royalty)"),

                             new DataColumn("Date"),
                    });
                    if (model.Objlist.Count > 0)
                    {
                        foreach (var customer in model.Objlist)
                        {
                            dt.Rows.Add(customer.LoginId, customer.FirstName, customer.PayoutNo, customer.LARBonus, customer.ClosingDate);
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {

                            wb.Worksheets.Add(dt);
                            using (MemoryStream stream = new MemoryStream())
                            {

                                wb.SaveAs(stream);

                                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LAR (Lifetime Achievement Royalty).xlsx");
                            }
                        }
                    }
                    else
                    {
                        return Redirect("RepurchaseIncomeType");
                    }
                }


                //SG Yearly Bonus paging and Excel
                else if (IncomeType == "SGYearlyBonus")
                {
                    DataTable dt = new DataTable("SG Yearly Bonus");
                    dt.Columns.AddRange(new DataColumn[5]
                    {
                             new DataColumn("Login Id"),
                             new DataColumn("Direct Seller"),
                             new DataColumn("Payout No"),

                             new DataColumn("SG Yearly Bonus"),

                             new DataColumn("Date"),
                    });
                    if (model.Objlist.Count > 0)
                    {
                        foreach (var customer in model.Objlist)
                        {
                            dt.Rows.Add(customer.LoginId, customer.FirstName, customer.PayoutNo, customer.SGYearlyBonus, customer.ClosingDate);
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {

                            wb.Worksheets.Add(dt);
                            using (MemoryStream stream = new MemoryStream())
                            {

                                wb.SaveAs(stream);

                                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SG Yearly Bonus.xlsx");
                            }
                        }
                    }
                    else
                    {
                        return Redirect("RepurchaseIncomeType");
                    }
                }




                else
                {
                    return Redirect("RepurchaseIncomeType");
                }
            }
           
            else
            {
                return View(model);
            }
               
           
        }
        //[HttpPost]
        //public ActionResult RepurchaseIncomeType( string IncomeType)
        //{
        //    MPayoutReport model = new MPayoutReport();
        //    MPayoutReport obj = AdminReportsService.GetRepurchaseIncomePayoutReport(model);
           
        //    ViewBag.PayoutMaster = DALBindCommonDropdown.BindRepurchasePayoutMaster();
        //    Session["listPayoutdetails"] = model.Objlist;
        //    if(model.IsExport == 1)
        //    {
                
        //        model.Objlist = DALAdminReports.GetRepurchaseIncomePayoutReport(model);
        //        DataTable dt = new DataTable("HybrideMatchingBonus");
        //        dt.Columns.AddRange(new DataColumn[11]
        //        {
        //                     new DataColumn("Login Id"),
        //                     new DataColumn("Direct Seller Name"),
        //                     new DataColumn("Payout No"),
        //                     new DataColumn("Hybrid Matching Income"),
        //                     new DataColumn("Leadership Bonus"),
        //                     new DataColumn("House Fund"),
        //                     new DataColumn("Director Club"),
        //                     new DataColumn("Chairman Club"),
        //                     new DataColumn("LAR(Lifetime Achievement Royalty)"),
        //                     new DataColumn("SG Yearly Bonus"),
        //                     new DataColumn("Date"),
        //        });
        //        if(model.Objlist.Count > 0)
        //        {
        //            foreach (var customer in model.Objlist)
        //            {
        //                dt.Rows.Add(customer.LoginId, customer.DisplayName, customer.PayoutNo, customer.HybridMatchingBonus, customer.LeadershipBonus, customer.HouseFund, customer.DirectorClub, customer.ChairManClub, customer.LARBonus, customer.SGYearlyBonus, customer.ClosingDate);
        //            }
        //            using (XLWorkbook wb = new XLWorkbook())
        //            {
        //                wb.Worksheets.Add(dt);
        //                using (MemoryStream stream = new MemoryStream())
        //                {
        //                    wb.SaveAs(stream);

        //                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "HybrideMarchingBonus.xlsx");
        //                }
        //            }
        //        }
        //    }
        //    return View(obj);
        }
 }
