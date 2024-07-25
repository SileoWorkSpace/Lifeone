using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeOne.Models.API;
using System.Configuration;
using LifeOne.Models.FranchiseManagement.FService;  
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.FranchiseManagement.FDAL;
using LifeOne.Models.AdminManagement.ADAL;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using SelectPdf;

namespace LifeOne.Areas.Admin.Controllers
{
    //[SessionTimeoutAttributeAdmin]
    public class ManageAssociateOrderedController : Controller
    {
        // GET: Admin/ManageAssociateOrdered/OrderedDetails
        string Aeskey = ConfigurationManager.AppSettings["Aeskey"].ToString();
        public ActionResult OrderedDetails(MFAOrderRequest _param, int? Page)
        {

            if (!PermissionManager.IsActionPermit("Member Orders", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            try
            {
                _param.ParentId = 0; // 0 for admin
                _param.ParentTypeId = Convert.ToInt32(SessionManager.Usertype);
                if (Page == null || Page == 0)
                {
                    Page = 1;
                }
                _param.Page = Page;
                List<MFAOrderRequest> _obj = FOrderRequestService.GetAsociateNewOrderRequestService(_param);
                _param.MFAOrderRequestList = _obj;
                //foreach (var item in _param.MFAOrderRequestList)
                //{
                //    var data = FOrderRequestService.GetFPaymentDetailService(item.ReqId).FirstOrDefault();
                //    item.PayMode = data.PayMode;
                //    item.PaymentDate = data.PaymentDate;
                //}
                //

                    int totalRecords = 0;
                    if (_param.MFAOrderRequestList != null && _param.MFAOrderRequestList.Count > 0)            
                    {

                    totalRecords = Convert.ToInt32(_param.MFAOrderRequestList[0].ToatlRecord);
                        var pager = new Pager(totalRecords, _param.Page, SessionManager.Size);
                        _param.Pager = pager;
                    }
                    else
                    {
                        _param.MFAOrderRequestList = new List<MFAOrderRequest>();
                    }
                    return View(_param);
                }
         


            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult ExportToExcelOrderedDetails(MFAOrderRequest _param, int? Page)
        {
            _param.ParentId = 0; // 0 for admin
            _param.ParentTypeId = Convert.ToInt32(SessionManager.Usertype);
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            _param.Page = Page;
            _param.MFAOrderRequestList = FOrderRequestService.GetAsociateNewOrderRequestService(_param);
            DataTable dt = new DataTable("Ordered Details");
            dt.Columns.AddRange(new DataColumn[6] {
                                                     new DataColumn("Order No"),
                                                     new DataColumn("Associate Name / Id"),
                                                     new DataColumn("Total Qty"),
                                                     new DataColumn("Total Amount	Request Dt."),
                                                     new DataColumn("Order Sts."),
                                                     new DataColumn("Shipping Dt./Sts.")

            });
            if (_param.MFAOrderRequestList.Count > 0)
            {
                foreach (var customer in _param.MFAOrderRequestList)
                {
                    dt.Rows.Add(
                    customer.OrderNo,
                    customer.FName,
                    customer.TotalItems,
                    customer.TotalAmt,
                    customer.OrderStatus,
                    customer.ShippingStstId == "1" ? "Order Shipped" : "Pending"
                    );

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "OrderedDetails.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("/OrderedDetails");
            }
            // return View();
        }
        public JsonResult GetAssignFranchOrdrInfoamtion(string _FkBID)
        {
            try
            {
                List<MBindAssignOrdertoFranchise> MemberList = AddShippingDetailsService.GetAssignFranchOrdrInfoamtion(_FkBID);
                return Json(MemberList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult ApproveOrderStockOfFranchise(List<MBindAssignOrdertoFranchise> _param)
        {
            MBindAssignOrdertoFranchise model = new MBindAssignOrdertoFranchise();
            model.xml = "<Result>";
            try
            {
                _param.ForEach(x =>
                {
                    model.xml += "<data>" +
                    "<FranchiseId>" + x.FranchiseId + "</FranchiseId>" +
                    "<PrdId>" + x.PrductId + "</PrdId>" +
                    "<PkRedId>" + x.FK_FbSID + "</PkRedId>" +
                    "<Remarks>" + x.Remarks + "</Remarks>" +
                    "<Fk_MemId>" + SessionManager.Fk_MemId + "</Fk_MemId>" +
                    "</data>";
                });
                model.xml += "</Result>";
                model.CustomType = _param.FirstOrDefault().CustomType;
                int _iresult = AddShippingDetailsService.ApproveOrderStockService(model);
                return Json(_iresult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult MemberTaxInvoice(string OrderId)
        {
            try
            {
                OrderId = DALCommon.GlobalDecrypText(OrderId);
                MAssociateAdminInvoice obj = new MAssociateAdminInvoice();
                var key = AesEncryptDecrypt.DecryptString(Aeskey, OrderId);
                var list = LifeOne.Models.AdminManagement.ADAL.DBHelperDapper.GetMemberOrderInvoice(key);
                obj = list._MemberDetail;
                obj.MemberOrderDetailList = list._OrderDetail;
                return View(obj);
            }
            catch (Exception)
            {

                throw;
            }
        }


        #region OrderForCustomer

        public ActionResult OrderForCustomer(int? page, MOrder obj)
        {
            if (!PermissionManager.IsActionPermit("Ordered List", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            obj = AdminFranchiseOrderService.SupplyOrderStatusService(page, obj);
            return View(obj);
        }
        public JsonResult ViewSupplyItemDetail(string RequestId)
        {
            List<MOrder> _obj = AdminFranchiseOrderService.ViewSupplyItemService(long.Parse(RequestId));
            return Json(_obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddOrderForCustomer()
        {
            ComboPackage obj = new ComboPackage();
            try
            {
                ViewBag.ProductCategory = DALBindCommonDropdown.BindDropdown(2, 0);

                ViewBag.PackageList = DALBindCommonDropdown.PackageList();


                ViewBag.Product = obj.PackageList();
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult GetProductDetails(MFranchiseorderRequest _data)
        {
            
            try
            {
                List<AdminProductDetail> obj = new DALFranchise().AdminProductDetails(_data);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult CustomerDetail()
        {
            if (!PermissionManager.IsActionPermit("Create Order", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            ViewBag.ProductCategory = DALBindCommonDropdown.BindDropdown(2, 0);
            ViewBag.Product = DALBindCommonDropdown.BindDropdown(1, 0);
            return View();
        }
        public JsonResult BindCustomerDetails(MFCoutomer obj)
        {
            try
            {
                var result = new DALFranchise().BindCustomerDetails(obj);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public JsonResult CustomerDetailforUpgradePackage(MFCoutomer obj)
        {
            try
            {
                var result = new DALFranchise().UpgradePackageCustomerDetails(obj);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public JsonResult AddProduct(MFranchiseorderRequest _data)
        {
            try
            {
                MSimpleString obj = AdminFranchiseOrderService.AddFCustomerProductService(_data);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult GetOrderTemp(MSimpleString _data)
        {
            try
            {
                MFranchiseorderRequest obj = new MFranchiseorderRequest();
                List<MFranchiseorderRequest> _objlst = DALFranchise.GetFCustomerOrderTemp(_data);
                return Json(_objlst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult DeleteProductTemp(MFranchiseorderRequest _data)
        {
            try
            {
                MSimpleString _objlst = DALFranchise.DeleteFCustomerProductTemp(_data);
                return Json(_objlst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult SaveProduct(MFranchiseorderRequest _data)
        {
            try
            {
                MSimpleString result = AdminFranchiseOrderService.SaveFCustomerProductService(_data);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult BindUpdatedFigureAmtCustomer(string ReqId, string PK_KeyId, string Quantity, string TotalPv, string TotalAmt, string PrdId, string reqQty)
        {
            try
            {
                var list = DALFranchise.BindUpdatedFigureAmtCustomer(ReqId, long.Parse(PK_KeyId), Convert.ToInt32(Quantity), Convert.ToInt32(TotalPv), Convert.ToDecimal(TotalAmt), Convert.ToInt32(PrdId), Convert.ToInt32(reqQty));
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult CustomermemberWalletBalance(int Fk_MemID)
        {
            try
            {
                MCustomerWalletBalnce Balance = ChildFranchiseService.GetCusetomerMemberWalletBalanceService(Fk_MemID);
                return Json(Balance, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult CustomerTaxInvoice(string OrderId)
        {
            OrderId = AesEncryptDecrypt.DecryptString(Aeskey, OrderId);
            OrderConfirmationModel orderConfirmation = new OrderConfirmationModel();
            List<ItemsModel> OrderItems = new List<ItemsModel>();
            OrderDAL orderDAL = new OrderDAL();
            DataSet dsOrder = orderDAL.GetCustomerInvoiceByOrderId(OrderId);
            if (dsOrder.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsOrder.Tables[0].Rows.Count; i++)
                {
                    orderConfirmation.BName = dsOrder.Tables[0].Rows[i]["BName"].ToString();
                    orderConfirmation.BAddress = dsOrder.Tables[0].Rows[i]["BAddress"].ToString();
                    orderConfirmation.BMobileNo = dsOrder.Tables[0].Rows[i]["BMobileNo"].ToString();
                    orderConfirmation.ProductName = dsOrder.Tables[1].Rows[i]["ProductName"].ToString();
                    orderConfirmation.Total = dsOrder.Tables[0].Rows[i]["FinalAmt"].ToString();
                    orderConfirmation.TransportMode = dsOrder.Tables[0].Rows[i]["TransportMode"].ToString();

                    orderConfirmation.OrderNo = dsOrder.Tables[0].Rows[i]["OrderNo"].ToString();
                    orderConfirmation.OrderDate = dsOrder.Tables[0].Rows[i]["OrderDate"].ToString();
                    orderConfirmation.InvoiceDate = dsOrder.Tables[1].Rows[i]["InvoiceDate"].ToString();
                    orderConfirmation.InvoiceNo = dsOrder.Tables[1].Rows[i]["InvoiceNo"].ToString();
                    orderConfirmation.CourierName = dsOrder.Tables[0].Rows[i]["CourierName"].ToString();

                    orderConfirmation.CompanyName = dsOrder.Tables[0].Rows[i]["CompanyName"].ToString();
                    orderConfirmation.OfficeAddress = dsOrder.Tables[0].Rows[i]["OfficeAddress"].ToString();
                    orderConfirmation.OfficeContactNo = dsOrder.Tables[0].Rows[i]["OfficeContactNo"].ToString();
                    orderConfirmation.GSTIN = dsOrder.Tables[0].Rows[i]["CompanyGST"].ToString();
                    orderConfirmation.Website = dsOrder.Tables[0].Rows[i]["Website"].ToString();
                    orderConfirmation.PAN = dsOrder.Tables[0].Rows[i]["Pancard"].ToString();
                    orderConfirmation.OfficeEmailId = dsOrder.Tables[0].Rows[i]["OfficeEmailId"].ToString();
                    orderConfirmation.LoginId = dsOrder.Tables[0].Rows[i]["LoginId"].ToString();

                    ItemsModel obj = new ItemsModel();

                    int qty = 0;
                    decimal TotalAmount = 0;

                    TotalAmount = Convert.ToDecimal(orderConfirmation.Total);

                    long finalamount = (long)Convert.ToDouble(TotalAmount);
                    orderConfirmation.AmountInWords = DALCommon.ConvertNumbertoWords(finalamount);
                    if (finalamount == 0.00)
                    {
                        Session["words"] = null;
                        orderConfirmation.AmountInWords = "ZERO";
                        Session["AmtInWords"] = orderConfirmation.AmountInWords;
                    }
                    else
                    {
                        orderConfirmation.AmountInWords = Session["words"].ToString();
                        Session["AmtInWords"] = orderConfirmation.AmountInWords;
                    }
                    for (int j = 0; j < dsOrder.Tables[1].Rows.Count; j++)
                    {

                        OrderItems.Add(new ItemsModel
                        {
                            ProductUrl = "",
                            GSTNo = dsOrder.Tables[1].Rows[j]["GSTNo"].ToString(),
                            ProductName = dsOrder.Tables[1].Rows[j]["ProductName"].ToString(),
                            ProductAmt = dsOrder.Tables[1].Rows[j]["DealerPrice"].ToString(),
                            ProductQty = dsOrder.Tables[1].Rows[j]["ProductQty"].ToString(),
                            Discount = "0.00",
                            Tax = dsOrder.Tables[1].Rows[j]["DealerPrice"].ToString(),
                            CGST = dsOrder.Tables[1].Rows[j]["CGST"].ToString(),
                            SGST = dsOrder.Tables[1].Rows[j]["SGST"].ToString(),
                            IGST = dsOrder.Tables[1].Rows[j]["IGST"].ToString(),
                            CGSTAmount = dsOrder.Tables[1].Rows[j]["CGSTAmt"].ToString(),
                            SGSTAmount = dsOrder.Tables[1].Rows[j]["SGSTAmt"].ToString(),
                            IGSTAmount = dsOrder.Tables[1].Rows[j]["IGSTAmt"].ToString(),
                            //TotalProductAmt = dsOrder.Tables[1].Rows[j]["OrderAmount"].ToString(),
                            // BaseAmount = obj.BaseAmount,
                            TexableAmount = Convert.ToDecimal(dsOrder.Tables[1].Rows[j]["TaxableAmount"].ToString()),
                            TotalAmount = Convert.ToDecimal(orderConfirmation.Total),
                            SubTotal = Convert.ToDecimal(dsOrder.Tables[1].Rows[j]["SubTotal"].ToString()),
                            PointValue = Convert.ToDecimal(dsOrder.Tables[1].Rows[j]["PointValue"].ToString()),
                            AmountInWords = Session["AmtInWords"].ToString()
                        });
                    }
                    qty += int.Parse(dsOrder.Tables[1].Rows[i]["ProductQty"].ToString());
                    orderConfirmation.TotalProductQty = qty.ToString();
                    orderConfirmation.OrderItems = OrderItems;
                    orderConfirmation.VendorName = OrderItems[0].VendorName;
                }
                return View(orderConfirmation);
            }
            return View(orderConfirmation);
        }


        public JsonResult BindOrbit()
        {
            try
            {
                List<SelectListItem> Lst = DALBindCommonDropdown.BindDropdown(7, 0);
                return Json(Lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult CheckProduct(string prdId)
        {
            try
            {
                MFranchiseorderRequest data = DALFranchise.CheckProduct(prdId);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult BindProduct(MFranchiseorderRequest _data)
        {
            try
            {
                List<SelectListItem> Lst = DALBindCommonDropdown.BindDropdown(1, _data.CategoryId);
                return Json(Lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        [AllowAnonymous]
        public ActionResult Invoice(string OrderId)
        {
            try
            {
                //OrderId = DALCommon.GlobalDecrypText(OrderId);
                MInvoice obj = new MInvoice();
                //var key = AesEncryptDecrypt.DecryptString(Aeskey, OrderId);
                var list = LifeOne.Models.AdminManagement.ADAL.DBHelperDapper.GetInvocice(OrderId);
                obj = list._MemberDetail;
                obj.lst = list._OrderDetail;  
                return View(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [AllowAnonymous]
        public ActionResult Invoice_Karyon(string OrderId)
        {
            try
            {
                //OrderId = DALCommon.GlobalDecrypText(OrderId);
                MInvoice obj = new MInvoice();
                //var key = AesEncryptDecrypt.DecryptString(Aeskey, OrderId);
                var list = LifeOne.Models.AdminManagement.ADAL.DBHelperDapper.GetInvocice_Karyon(OrderId);
                obj = list._MemberDetail;
                obj.lst = list._OrderDetail;
                return View(obj);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [AllowAnonymous]
        public ActionResult InvoiceMobile(string OrderId)
        {

            if (OrderId != null || OrderId != "")
            {
                string msg = "Please see the attached invoice";
                HtmlToPdf htmlToPdf = new HtmlToPdf();
                //htmlToPdf.Options.MarginBottom = 5;
                //htmlToPdf.Options.MarginTop = 5;
                //htmlToPdf.Options.MarginLeft = 15;
                //htmlToPdf.Options.MarginRight = 15;
                //PdfDocument pdfDocument = htmlToPdf.ConvertUrl("https://localhost:44380" + "/Admin/ManageAssociateOrdered/Invoice?orderId=" + OrderId);
                PdfDocument pdfDocument = htmlToPdf.ConvertUrl("http://familynbusiness.com" + "/Admin/ManageAssociateOrdered/Invoice?orderId=" + OrderId);
                string guid = Guid.NewGuid().ToString().Substring(0, 7);
                pdfDocument.Save(HttpContext.ApplicationInstance.Response, false, "Invoice_" + guid + ".pdf");
                pdfDocument.Close();

            }
            return View();
        }
        public ActionResult OrderReport()
        {
            if (!PermissionManager.IsActionPermit("Other Shopping", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            return View();
        }

        public JsonResult BindOrderReport(DateTime? FromDate, DateTime? ToDate, int OrderType, int? Page, string OrderNo)
        {
            return Json(DALFranchise.BindOrderReport(FromDate, ToDate, OrderType, Page, Convert.ToInt32(OrderNo)));
        }
        public JsonResult GetOrderDetails(int OrderNo, int OrderType)
        {
            return Json(DALFranchise.GetOrderDetails(OrderNo, OrderType));
        }

        [AllowAnonymous]
        public ActionResult InvoiceMobile_Karyon(string OrderId)
        {

            if (OrderId != null || OrderId != "")
            {
                string msg = "Please see the attached invoice";
                HtmlToPdf htmlToPdf = new HtmlToPdf();
                //htmlToPdf.Options.MarginBottom = 5;
                //htmlToPdf.Options.MarginTop = 5;
                //htmlToPdf.Options.MarginLeft = 15;
                //htmlToPdf.Options.MarginRight = 15;
                //PdfDocument pdfDocument = htmlToPdf.ConvertUrl("https://localhost:44380" + "/Admin/ManageAssociateOrdered/Invoice?orderId=" + OrderId);
                PdfDocument pdfDocument = htmlToPdf.ConvertUrl("http://familynbusiness.com" + "/Admin/ManageAssociateOrdered/Invoice_Karyon?orderId=" + OrderId);
                string guid = Guid.NewGuid().ToString().Substring(0, 7);
                pdfDocument.Save(HttpContext.ApplicationInstance.Response, false, "Invoice_" + guid + ".pdf");
                pdfDocument.Close();

            }
            return View();
        }

        public ActionResult ExportToExcelOrderForCustomer(int? page, MOrder obj)
        {
            var customers = AdminFranchiseOrderService.SupplyOrderStatusService(page, obj);
            DataTable dt = new DataTable("Customer Orders");
            dt.Columns.AddRange(new DataColumn[10] {
                                                     new DataColumn("Order No"),
                                                     new DataColumn("Supply To"),
                                                     new DataColumn("Payment Type"),
                                                     new DataColumn("Item Qty."),
                                                     new DataColumn("PV"),
                                                     new DataColumn("Order Amt."),
                                                     new DataColumn("Discount Amt."),
                                                     new DataColumn("Final Amt."),
                                                     new DataColumn("Order Type"),
                                                     new DataColumn("Ordered Dt.")


            });
            if (customers.LstOrder.Count > 0)
            {
                foreach (var customer in customers.LstOrder)
                {
                    dt.Rows.Add(
                    customer.OrderNo,
                    customer.SupplyToName,
                    customer.PaymentType,
                    customer.ItemQty,
                    customer.PointValue,
                    Convert.ToDecimal(customer.Amount) + customer.DiscountAmt,
                    customer.DiscountAmt,
                    (Convert.ToDecimal(customer.Amount) + customer.DiscountAmt) - customer.DiscountAmt,
                    "Box",
                    customer.ApproveDate
                    );

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomerOrders.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("/OrderForCustomer");
            }
            // return View();
        }

        /*Updated on 11/01/2021*/
        public JsonResult GetShippingInformationForAssociate(string RequestId)
        {
            try
            {
                List<MShowSHippingInfoToAssociate> MemberList = AddShippingDetailsService.GetShippingInforForAssociateServie(RequestId);
                return Json(MemberList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public JsonResult ViewRequestItemDetail(string RequestId)
        {
            try
            {
                List<MOrder> _obj = FranchiseOrderService.ViewRequestItemService(long.Parse(RequestId));
                return Json(_obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public JsonResult ViewRequestItemDetailFranchiseWise(string RequestId, int FranchiseId)
        {
            try
            {
                List<MOrder> _obj = FranchiseOrderService.ViewRequestItemServiceFranchiseWise(FranchiseId, long.Parse(RequestId));
                return Json(_obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public JsonResult UpdateInvoiceDate(string ReqId, string InvoiceDate)
        {
            try
            {
                UpdateInvoicedateReponse reponse = DALShippingInformation.DALUpdateInvoiceDate(Convert.ToInt16(ReqId), InvoiceDate);
                return Json(reponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPost]
        public JsonResult AdminOorrderBilling(string fk_memid, string TransactionNo, string Paymentmode, string BankName, string ReceiptImage,string Remark, List<Models.AdminManagement.AEntity.OrderDetails> OrderDetails, string OrderType, string PackageId)
        {
            AdminOrder model = new AdminOrder();
            model.Fk_MemId = fk_memid;
            model.TransactionNo = TransactionNo;
            model.Paymentmode = Paymentmode;
            model.BankName = BankName;
            model.ReceiptImage = ReceiptImage;
            model.Remark = Remark;
            model.OrderDetails = OrderDetails;
            model.OrderType =Convert.ToInt32(OrderType);
            model.PackageId = Convert.ToInt32(PackageId);
            model.CreatedBy =Convert.ToInt32(SessionManager.Fk_MemId);

            AdminOorrderBilling obj = new AdminOorrderBilling();
            AdminOrder Result= obj.Orderbiilling(model);

            return Json(Result,JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AdminUpgradeOrderBilling(string fk_memid,string PackageId)
        {
            AdminOrder model = new AdminOrder();
            model.Fk_MemId = fk_memid;
            model.PackageId =Convert.ToInt32(PackageId);
            model.CreatedBy = Convert.ToInt32(SessionManager.Fk_MemId);

            AdminOorrderBilling obj = new AdminOorrderBilling();
            AdminOrder Result = obj.UpgradePackage(model);

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

    }
}