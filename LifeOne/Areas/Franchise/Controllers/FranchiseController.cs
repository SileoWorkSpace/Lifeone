using LifeOne.Filters;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.FranchiseManagement.FService;
using LifeOne.Models.AdminManagement.AService;

using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using LifeOne.Models;

namespace LifeOne.Areas.Franchise.Controllers
{
    [SessionTimeoutAttributeFranchise]
    public class FranchiseController : Controller
    {
        // GET: Franchise/Franchise
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FranchiseDashboard()
        {
            dynamic mymodal = new ExpandoObject();
            MAdminDashbord _model = new MAdminDashbord();
            var list = FranchiseDashboardService.InvokeDashbordDetails(_model);
            var list1 = FranchiseDashboardService.InvokeRecentJoiners(_model);
            var list2 = FranchiseDashboardService.InvokeMemberJoiners(_model);
            var List = FranchiseDashboardService.FranchiseDahsboard();
            List<object> members = new List<object>();
            if (list2.Count > 0)
            {
                Random random = new Random();
                int randomNumber;
                foreach (var item in list2)
                {
                    randomNumber = random.Next(100000, 500000);
                    members.Add(new object[]{
                            item.JoiningDate, item.totalMembers,"#"+randomNumber.ToString()
                    });
                }
            }

            mymodal.TotalMember = list.ToList();
            mymodal.RecentJoiners = list1.ToList();
            mymodal.Utility = list2.ToList();

            ViewBag.DataPoints = mymodal.Utility;
            return View(List);
        }
        public ActionResult FranchiseChildDashboard()
        {
            dynamic mymodal = new ExpandoObject();
            MAdminDashbord _model = new MAdminDashbord();
            var list = FranchiseDashboardService.InvokeDashbordDetails(_model);
            var list1 = FranchiseDashboardService.InvokeRecentJoiners(_model);
            var list2 = FranchiseDashboardService.InvokeMemberJoiners(_model);
            List<object> members = new List<object>();
            if (list2.Count > 0)
            {
                Random random = new Random();
                int randomNumber;
                foreach (var item in list2)
                {
                    randomNumber = random.Next(100000, 500000);
                    members.Add(new object[]{
                            item.JoiningDate, item.totalMembers,"#"+randomNumber.ToString()
                    });
                }
            }

            mymodal.TotalMember = list.ToList();
            mymodal.RecentJoiners = list1.ToList();
            mymodal.Utility = list2.ToList();

            ViewBag.DataPoints = mymodal.Utility;
            return View(mymodal);
        }
        public ActionResult TransferInvoice()
        {

            return View();
        }
        public ActionResult FranchiseSell(MAdminFranchiseSell obj, long? Id, int? Page)
        {
            ViewBag.FranchiseDll = DALBindCommonDropdown.BindDropdown(11, 0);
            obj.fk_MemId = SessionManager.FranchiseFk_MemId;
            obj.ProcId = 1;
            obj.objList = FranchiseSellService.GetFranchiseSellService(obj);
            obj.fk_MemId = Id;
            return View(obj);
        }
        public ActionResult FranchisePurchaseHistory(MAdminFranchisePurchaseHistory obj, long? Id, int? Page)
        {
            ViewBag.FranchiseDll = DALBindCommonDropdown.BindDropdown(11, 0);
            if (SessionManager.Usertype == "3")
                obj.fk_MemId = SessionManager.FranchiseFk_MemId;
            if (Id == null)
                Id = obj.fk_MemId == null ? 0 : obj.fk_MemId;
            else
                obj.fk_MemId = Id;

            obj.ProcId = 1;
            obj.objList = FranchisePurchaseHistoryService.GetFranchisePurchaseHistoryService(obj);
            obj.fk_MemId = Id;
            return View(obj);
        }
        public ActionResult FranchisePurchaseHistoryByInvoice(MAdminFranchisePurchaseHistory obj, long? Id, int? Page)
        {
            if (SessionManager.Usertype != "3")
            {
                obj.fk_MemId = SessionManager.FranchiseFk_MemId;
            }
            if (Id == null)
            {

                Id = obj.fk_MemId == null ? 0 : obj.fk_MemId;
            }
            else
            {
                obj.fk_MemId = Id;
            }
            obj.ProcId = 2;
            obj.objList = FranchisePurchaseHistoryService.GetFranchisePurchaseHistoryService(obj);
            obj.fk_MemId = Id;
            //if (model.Mobile != null)
            //{
            //    obj.Mobile = model.Mobile;
            //}
            //if (model.LoginId != null)
            //{
            //    obj.LoginId = model.LoginId;
            //}
            //if (model.Name != null)
            //{
            //    obj.Name = model.Name;
            //}
            //int totalRecords = 0;
            //if (obj.Objlist.Count > 0)
            //{

            //    totalRecords = Convert.ToInt32(obj.Objlist[0].TotalRecord);
            //    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
            //    obj.Pager = pager;
            //}
            //else
            //{
            //    obj.Objlist = new List<MAdminMemberBankDetails>();
            //}
            return View(obj);

        }
        public ActionResult FranchiseSellDateWise(MAdminFranchiseSell obj, long? Id, int? Page)
        {
            obj.fk_MemId = SessionManager.FranchiseFk_MemId;
            obj.ProcId = 2;
            obj.objList = FranchiseSellService.GetFranchiseSellService(obj);
            //if (Page == null || Page == 0)
            //{
            //    Page = 1;
            //}
            //model.Page = Page;

            //if (model.Mobile != null)
            //{
            //    obj.Mobile = model.Mobile;
            //}
            //if (model.LoginId != null)
            //{
            //    obj.LoginId = model.LoginId;
            //}
            //if (model.Name != null)
            //{
            //    obj.Name = model.Name;
            //}
            //int totalRecords = 0;
            //if (obj.Objlist.Count > 0)
            //{

            //    totalRecords = Convert.ToInt32(obj.Objlist[0].TotalRecord);
            //    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
            //    obj.Pager = pager;
            //}
            //else
            //{
            //    obj.Objlist = new List<MAdminMemberBankDetails>();
            //}
            return View(obj);

        }
        public JsonResult FranchiseSellDetailsByInvoiceNo(string Fk_Memid, string InvoiceNo)
        {

            MAdminFranchiseSell obj = new MAdminFranchiseSell();

            try
            {
                obj.fk_MemId = long.Parse(Fk_Memid);
                obj.InvoiceNo = InvoiceNo;

                obj.ProcId = 3;
                obj.objList = FranchiseSellService.GetFranchiseSellService(obj);

            }
            catch (Exception)
            {
                throw;
            }
            return Json(obj.objList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ExportToExcelFranchiseSell(MAdminFranchiseSell obj, long? Id, int? Page)
        {
            obj.fk_MemId = SessionManager.FranchiseFk_MemId;
            obj.ProcId = 1;
            var customers = FranchiseSellService.GetFranchiseSellService(obj);
            DataTable dt = new DataTable("FranchiseSaleDetails");
            dt.Columns.AddRange(new DataColumn[3] {
                                                     new DataColumn("Sell Date"),
                                                     new DataColumn("Product Amount"),
                                                     new DataColumn("Total Pv")
            });

            if (customers.Count > 0)
            {
                customers.ForEach(customer =>
                {
                    dt.Rows.Add(customer.Saledate, customer.TotalPv, customer.TotalAmt);
                });
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FranchiseSaleDetails.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("FranchiseSell");
            }
        }
        public ActionResult ExportToExcelFranchiseSellDateWise(MAdminFranchiseSell obj, long? Id, int? Page)
        {
            obj.fk_MemId = SessionManager.FranchiseFk_MemId;
            obj.ProcId = 2;
            var customers = FranchiseSellService.GetFranchiseSellService(obj);
            DataTable dt = new DataTable("FranchiseSellDateWiseDetails");
            dt.Columns.AddRange(new DataColumn[7] {
                                                     new DataColumn("Sale Date"),
                                                     new DataColumn("Invoice No"),
                                                     new DataColumn("Name"),
                                                     new DataColumn("Quantity"),
                                                     new DataColumn("Paid Amount"),
                                                     new DataColumn("Payment Mode"),
                                                     new DataColumn("Status")
            });

            if (customers.Count > 0)
            {
                customers.ForEach(customer =>
                {
                    dt.Rows.Add(customer.Saledate, customer.InvoiceNo, customer.Name, customer.Quantity, customer.PaidAmt, customer.PayMode, customer.PaymentStatus);
                });
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FranchiseSellDateWiseDetails.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("FranchiseSellDateWise");
            }
        }
        public ActionResult ExportToExcelFranchisePurchaseHistory(MAdminFranchisePurchaseHistory obj, long? Id, int? Page)
        {
            if (SessionManager.Usertype == "3")
                obj.fk_MemId = SessionManager.FranchiseFk_MemId;
            if (Id == null)
                Id = obj.fk_MemId == null ? 0 : obj.fk_MemId;
            else
                obj.fk_MemId = Id;

            obj.ProcId = 1;
            var customers = FranchisePurchaseHistoryService.GetFranchisePurchaseHistoryService(obj);
            DataTable dt = new DataTable("FranchisePurchaseHistoryDetails");
            dt.Columns.AddRange(new DataColumn[4] {
                                                     new DataColumn("Date"),
                                                     new DataColumn("Invoice No"),
                                                     new DataColumn("Paid Amount"),
                                                     new DataColumn("Total P.V.")
            });

            if (customers.Count > 0)
            {
                customers.ForEach(customer =>
                {
                    dt.Rows.Add(customer.Date, customer.InvoiceNo, customer.PaidAmt, customer.TotalPv);
                });
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FranchisePurchaseHistoryDetails.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("FranchiseSellDateWise");
            }
        }
        public ActionResult ExportToExcelPaidDetails()
        {
            var customers = FranchiseDashboardService.FranchiseDahsboard();
            DataTable dt = new DataTable("PaidDetails");
            dt.Columns.AddRange(new DataColumn[5] {
                                                     new DataColumn("Invoice No"),
                                                     new DataColumn("Name"),
                                                     new DataColumn("Paid Amount"),
                                                     new DataColumn("Total P.V."),
                                                     new DataColumn("Status")
            });

            if (customers.FranchisePaidList.Count > 0)
            {
                customers.FranchisePaidList.ForEach(customer =>
                {
                    dt.Rows.Add(customer.InvoiceNo, customer.Name, customer.PaidAmt, customer.TotalPv, customer.Status);
                });
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PaidDetails.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("FranchiseDashboard");
            }

        }

        public ActionResult FranchiseEWalletRequest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FranchiseEWalletRequest(FranchiseEWalletRequest obj, HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                obj.FileUpload = "/EwalletRequestFiles/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(Path.Combine(Server.MapPath(obj.FileUpload)));
            }

            obj.AddedBy = SessionManager.FranchiseFk_MemId;
            DataTable dt = obj.FranchiseEWalletReq(obj);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Code"].ToString() == "1")
                {
                    ViewBag.Alert = "E-Wallet Request Save successfully";
                }
                else if (dt.Rows[0]["Code"].ToString() == "0")
                {
                    ViewBag.Alert = dt.Rows[0]["Msg"].ToString();
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
            return View();
        }

        public ActionResult TopupByFranchise(TopupByFranchise topupByFranchise)
        {
            ViewBag.Colortxt = "alert-success";
            try
            {
                Models.AssociateManagement.AssociateEntity.PackageMaster para = new Models.AssociateManagement.AssociateEntity.PackageMaster();
                ViewBag.ddlPackageList = Models.AssociateManagement.AssociateEntity.BindPckageMaster.BindPackageMasterFranchise();
                ProductsDetail product = new ProductsDetail();
                List<SelectListItem> ddlProduct = new List<SelectListItem>();

                DataSet dataSet = product.GetAllProductsForFranchisee();

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)

                {
                    ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
                    foreach (DataRow r in dataSet.Tables[0].Rows)
                    {

                        ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });

                    }

                }
                ViewBag.ddlProduct = ddlProduct;
                topupByFranchise.Fk_FranchiseId = SessionManager.FranchiseFk_MemId;
                if (topupByFranchise.FK_MemId > 0)
                {
                    topupByFranchise.Fk_FranchiseId = SessionManager.FranchiseFk_MemId;
                    DataSet ds = topupByFranchise.DeleteActivatedMembersProductTemp();
                    topupByFranchise.dtDetails = ds.Tables[0];
                    if (ds != null)
                    {

                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {

                            topupByFranchise.txtLoginId = Session["MemLoginId"].ToString();
                            topupByFranchise.FK_PackageId = int.Parse(Session["FK_PackageId"].ToString());
                            topupByFranchise.txtFK_PackageId = int.Parse(Session["FK_PackageId"].ToString());

                            ViewBag.PaidAmount = ds.Tables[0].Compute("Sum(Price)", "").ToString();
                            ViewBag.BV = ds.Tables[0].Compute("Sum(TotalBV)", "").ToString();
                            ViewBag.Name = ds.Tables[1].Rows[0]["Name"].ToString();
                        }
                        else
                        {
                            topupByFranchise.txtLoginId = Session["MemLoginId"].ToString();
                            topupByFranchise.FK_PackageId = int.Parse(Session["FK_PackageId"].ToString());
                            ViewBag.PaidAmount = 0;
                            ViewBag.BV = 0;
                            ViewBag.Name = ds.Tables[1].Rows[0]["Name"].ToString();
                        }
                        TempData["txtFK_PackageId"] = int.Parse(Session["FK_PackageId"].ToString());
                        TempData["PaymentMode"] = Session["PaymentMode"].ToString();
                        TempData["ChequeDDNo"] = Session["ChequeDDNo"];
                        TempData["ChequeDDDate"] = Session["ChequeDDDate"];
                        TempData["BankName"] = Session["BankName"];

                        return View(topupByFranchise);

                    }

                }
                else
                {
                    TempData["PaymentMode"] = "0";

                    DataSet ds1 = topupByFranchise.DeleteActivateMembersTempbyId();
                    return View(topupByFranchise);

                }

                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult TopupByFranchise(TopupByFranchise obj, string btnAdd)
        {
            try
            {
                ViewBag.ddlPackageList = Models.AssociateManagement.AssociateEntity.BindPckageMaster.BindPackageMasterAdmin();
                ProductsDetail product = new ProductsDetail();
                List<SelectListItem> ddlProduct = new List<SelectListItem>();

                //DataSet dataSet = product.GetAllProducts();
                DataSet dataSet = product.GetAllProductsForFranchisee();

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)

                {
                    ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
                    foreach (DataRow r in dataSet.Tables[0].Rows)
                    {

                        ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });

                    }

                }
                ViewBag.ddlProduct = ddlProduct;

                Models.FranchiseManagement.FEntity.TopupByFranchise para = new Models.FranchiseManagement.FEntity.TopupByFranchise();

                para.FK_MemId = obj.FK_MemId;
                para.WalletPayment = obj.WalletPayment;
                para.FK_PackageId = obj.FK_PackageId;
                para.PaidAmount = Convert.ToDecimal(obj.txtPaidAmount);
                para.Fk_FranchiseId = SessionManager.FranchiseFk_MemId;
                para.BankName = string.IsNullOrEmpty(obj.BankName) ? null : (obj.BankName);
                para.BankBranch = string.IsNullOrEmpty(obj.BankBranch) ? null : (obj.BankBranch);
                para.ChequeDDNo = string.IsNullOrEmpty(obj.ChequeDDNo) ? null : (obj.ChequeDDNo);
                para.ChequeDDDate = string.IsNullOrEmpty(obj.ChequeDDDate) ? null : DALCommon.ConvertToSystemDate(obj.ChequeDDDate, "dd/MM/yyyy");
                para.PaymentMode = obj.PaymentMode;

                Session["FK_PackageId"] = obj.FK_PackageId;
                Session["MemLoginId"] = obj.txtLoginId;
                Session["PaymentMode"] = string.IsNullOrEmpty(obj.PaymentMode) ? "0" : obj.PaymentMode;
                Session["ChequeDDNo"] = obj.ChequeDDNo;
                Session["ChequeDDDate"] = obj.ChequeDDDate;
                Session["BankName"] = obj.BankName;
                TempData["PaymentMode"] = string.IsNullOrEmpty(obj.PaymentMode) ? "0" : obj.PaymentMode;
                TempData["BankName"] = obj.BankName;
                TempData["ChequeDDNo"] = obj.ChequeDDNo;
                TempData["ChequeDDDate"] = obj.ChequeDDDate;
                TempData["WalletPayment"] = obj.WalletPayment;


                if (!string.IsNullOrEmpty(btnAdd))
                {
                    para.Status = "Add";
                    para.ProductId = obj.ProductId;
                    para.Quantity = obj.Quantity;
                    DataSet ds = para.ActivateMembersTemp();
                    para.dtDetails = ds.Tables[0];

                    if (ds != null)
                    {
                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                        {
                            ViewBag.Name = ds.Tables[1].Rows[0]["Name"].ToString();
                        }
                        else
                        {
                            ViewBag.Name = "";
                        }

                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {
                            ViewBag.PaidAmount = ds.Tables[0].Compute("Sum(Price)", "").ToString();
                            ViewBag.BV = ds.Tables[0].Compute("Sum(TotalBV)", "").ToString();

                        }
                        else
                        {
                            ViewBag.PaidAmount = "";
                            ViewBag.BV = "";
                        }
                    }


                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {

                        TempData["Alert"] = ds.Tables[0].Rows[0]["Error"].ToString();
                        ViewBag.Colortxt = "alert-danger";

                    }
                    return View(para);

                }
                else
                {
                    para.PaymentMode = obj.PaymentMode;
                    ViewBag.PaymentMode = obj.PaymentMode;
                    Session["FK_PackageId"] = obj.FK_PackageId;
                    Session["MemLoginId"] = obj.txtLoginId;
                    Session["PaymentMode"] = obj.PaymentMode;
                    DataTable dt = obj.TopUpMemberByFranchise(para);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["Alert"] = "Associate successfully activated";
                            ViewBag.Colortxt = "alert-success";
                            Session["FK_PackageId"] = "";
                            Session["MemLoginId"] = "";
                            Session["PaymentMode"] = "";
                            Session["ChequeDDNo"] = "";
                            Session["ChequeDDDate"] = "";
                            Session["BankName"] = "";
                            TempData["PaymentMode"] = "";
                            TempData["BankName"] = "";
                            TempData["ChequeDDNo"] = "";
                            TempData["ChequeDDDate"] = "";
                            TempData["WalletPayment"] = "";
                            return RedirectToAction("TopupByFranchise");
                        }
                        else if (dt.Rows[0]["Msg"].ToString() == "0")
                        {
                            DataSet ds = para.GetActivateMembersTemp();
                            para.dtDetails = ds.Tables[0];
                            TempData["Alert"] = dt.Rows[0]["Error"].ToString();

                            ViewBag.Colortxt = "alert-danger";
                            if (ds != null)
                            {
                                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                                {
                                    ViewBag.Name = ds.Tables[1].Rows[0]["Name"].ToString();
                                }
                                else
                                {
                                    ViewBag.Name = "";
                                }

                                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                                {
                                    ViewBag.PaidAmount = ds.Tables[0].Compute("Sum(Price)", "").ToString();
                                    ViewBag.BV = ds.Tables[0].Compute("Sum(TotalBV)", "").ToString();
                                }
                                else
                                {
                                    ViewBag.PaidAmount = 0;
                                    ViewBag.BV = 0;
                                }
                            }
                            return View(para);
                        }
                        else
                        {
                            TempData["Alert"] = "Error!!!";
                            ViewBag.Colortxt = "alert-danger";
                        }
                    }
                    else
                    {
                        TempData["Alert"] = "Error!!!";
                        ViewBag.Colortxt = "alert-danger";
                    }
                }
                return View();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult DeleteTempActivationPro(string id, string FK_MemId)
        {
            TopupByFranchise topupByFranchise = new TopupByFranchise();
            topupByFranchise.FK_MemId = long.Parse(FK_MemId);
            topupByFranchise.ProductId = id;
            topupByFranchise.FK_PackageId = Convert.ToInt32(id);

            return RedirectToAction("TopupByFranchise", topupByFranchise);

        }
        public JsonResult GetProductMemeberWise(ProductsDetail product, string ProductType)
        {
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            try
            {
                product.Fk_MemId = SessionManager.FranchiseFk_MemId;
                DataSet dataSet;
                if (ProductType == "MemProduct")
                {
                    dataSet = product.GetProductMemeberWise();
                }
                else
                {
                    dataSet = product.GetAllProductsForFranchisee();
                }

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
                    foreach (DataRow r in dataSet.Tables[0].Rows)
                    {

                        ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });

                    }

                }
                ViewBag.ddlProduct = ddlProduct;
                return Json(ViewBag.ddlProduct, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public JsonResult ValFranchiseWalletAmount(ProductsDetail product, int ProductId, int Opcode)
        {
            try
            {
                product.Fk_MemId = SessionManager.FranchiseFk_MemId;
                product.OpCode = Opcode;
                product.Pk_ProductId = ProductId;
                product.LstOrder = product.ValFranchiseWalletAmount();
                return Json(product.LstOrder, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}