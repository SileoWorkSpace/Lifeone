using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using LifeOne.Filters;
using ClosedXML.Excel;
using System.IO;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.FranchiseManagement.FService;

namespace LifeOne.Areas.Admin.Controllers
{
  
    //[MenuPermissionFilter]
    [SessionTimeoutAttributeAdmin]
    public class FranchiseOrderRequestController : Controller
    {
        // GET: Admin/FranchiseOrderRequest/ViewNewRequest

        public ActionResult ViewNewRequest(MFAOrderRequest _param, int? Page)
        {

            //DataSet dataSet = _param.GetNewOrderRequestServices;
            //.dtLifeTimeReward = dataSet.Tables[0];
            //["LifeTimeRewardList"] = dataSet.Tables[0];
            if (!PermissionManager.IsActionPermit("Order Request", ViewOptions.FormView))
            {
                return Redirect("/admin");
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
                _param.ProcId = 1;
                //FOrderRequestService db = new FOrderRequestService();
               // MFAOrderRequest obj = new MFAOrderRequest();
            
                List<MFAOrderRequest> _obj = FOrderRequestService.GetNewOrderRequestService(_param);
                _param.MFAOrderRequestList = _obj;
                if(_param.IsExport==1)
                {
                    _param.MFAOrderRequestList = DALFOrderRequest.DALGetNewFOrderRequest(_param);
                    DataTable dt = new DataTable("Order Request");
                    dt.Columns.AddRange(new DataColumn[7] {
                                                     new DataColumn("LoginID"),
                                                     new DataColumn("FName"),
                                                     new DataColumn("Contact"),
                                                     new DataColumn("CompanyName"),
                                                     new DataColumn("TotalItems"),
                                                     new DataColumn("PaymentStatus"),
                                                     new DataColumn("OrderNo")


            });
                    if (_param.MFAOrderRequestList.Count > 0)
                    {
                        foreach (var customer in _param.MFAOrderRequestList)
                        {
                            dt.Rows.Add(customer.LoginID, customer.FName, customer.Contact, customer.CompanyName, customer.TotalItems, customer.PaymentStatus, customer.OrderNo);

                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt);
                            using (MemoryStream stream = new MemoryStream())
                            {
                                wb.SaveAs(stream);
                                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Order Request.xlsx");
                            }
                        }
                    }
                }
                else
                {
                    int totalRecords = 0;
                    if (_param.MFAOrderRequestList.Count > 0)
                    {
                        totalRecords = Convert.ToInt32(_param.MFAOrderRequestList[0].ToatlRecord);
                        var pager = new Pager(totalRecords, _param.Page, SessionManager.Size);
                        _param.Pager = pager;
                    }
                }
                
                
                return View(_param);
            }
            catch (Exception)
            {
                throw;
            }
        }


       
       

        public JsonResult ViewNewRequestDetail(string RequestId)
        {
            List<MFAOrderRequestDetail> _obj = FOrderRequestService.GetNewOrderRequestItemService(long.Parse(RequestId));
            return Json(_obj, JsonRequestBehavior.AllowGet);
        }  

        public JsonResult ViewNewRequestDetailFranchise(string RequestId)
        {
            List<MFAOrderRequestDetail> _obj = FOrderRequestService.GetNewOrderRequestItemFranchiseService(long.Parse(RequestId), SessionManager.Fk_MemId);
            return Json(_obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportToExcelViewNewRequestList(MFAOrderRequest _param, int? Page)
        {
            _param.ParentId = 0; // 0 for admin
            _param.ParentTypeId = Convert.ToInt32(SessionManager.Usertype);
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            _param.ProcId = 4;
            _param.Page = Page;
            List<MFAOrderRequest> _obj = FOrderRequestService.GetNewOrderRequestService(_param);
            var customers = _obj;
            DataTable dt = new DataTable("ViewNewRequestList");
            dt.Columns.AddRange(new DataColumn[9] {
                                                     new DataColumn("Franchise Id"),
                                                     new DataColumn("Franchise Name"),
                                                     new DataColumn("Firm Name"),
                                                      new DataColumn("Mobile No."),
                                                     new DataColumn("Total Qty"),
                                                     new DataColumn("Total Amount"),
                                                     new DataColumn("Request Dt."),
                                                     new DataColumn("Order Sts."),
                                                     new DataColumn("Payment Approval"),
                                                    


            });

            if (customers.Count > 0)
            {
                customers.ForEach(customer =>
                {
                    dt.Rows.Add(customer.LoginID,customer.FName,customer.CompanyName,customer.Contact, customer.TotalItems, customer.TotalAmt, customer.ReqDate, customer.OrderStatus, customer.PaymentStatus);
                });
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ViewNewRequestList.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("ViewNewRequest", "FranchiseOrderRequest");
            }
            // return View();
        }
        public JsonResult ApproveNewRequestDetail(List<MFAOrderRequestDetail> productlist)
        {
            XmlModel obj = new XmlModel();
            var productDetail = "<ProductDetail>";
            if (productlist != null && productlist.Count > 0)
            {
                for (int i = 0; i < productlist.Count; i++)
                {
                    productDetail += "<ProductList><PrdId>" + productlist[i].FK_PrdId + "</PrdId>";
                    productDetail += "<ApproveQty>" + productlist[i].ApprovalQty + "</ApproveQty>";
                    productDetail += "<Remark>" + productlist[i].PrdWiseRmrk + "</Remark>";
                    productDetail += "<StatusId>" + productlist[i].PrdWiseStsId + "</StatusId>";
                    productDetail += "<FK_ReqId>" + productlist[i].Fk_RId + "</FK_ReqId>";
                    productDetail += "<FK_FId>" + productlist[i].FK_FId + "</FK_FId>";
                    productDetail += "<AdminId>" + SessionManager.Fk_MemId + "</AdminId>";
                    productDetail += "</ProductList>";
                }
                productDetail += "</ProductDetail>";
            }
            obj.XmlData = productDetail;
            var list = FOrderRequestService.ApprveProductService(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ViewPaymentDetail(string RequestId)
        {
            List<MFAPaymentStatus> _obj = FOrderRequestService.GetFPaymentDetailService(long.Parse(RequestId));
            return Json(_obj, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ApproveDeclinePaymentStatus(string ReqId, string status)
        {

            var list = FOrderRequestService.ApprveDeclinepaymentService(long.Parse(ReqId), Convert.ToInt32(status));
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        public ActionResult TaxInvoice(string OrderId)
        {
            OrderConfirmationModel orderConfirmation = new OrderConfirmationModel();
            List<ItemsModel> OrderItems = new List<ItemsModel>();
            OrderDAL orderDAL = new OrderDAL();
            DataSet dsOrder = orderDAL.GetOrderDetailsByOrderId(OrderId);
            if (dsOrder.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsOrder.Tables[0].Rows.Count; i++)
                {
                    orderConfirmation.BName = dsOrder.Tables[0].Rows[i]["BName"].ToString();
                    orderConfirmation.BAddress = dsOrder.Tables[0].Rows[i]["BAddress"].ToString();
                    orderConfirmation.BMobileNo = dsOrder.Tables[0].Rows[i]["BMobileNo"].ToString();
                    orderConfirmation.BillingPincode = dsOrder.Tables[0].Rows[i]["BillingPincode"].ToString();
                    orderConfirmation.SName = dsOrder.Tables[0].Rows[i]["SName"].ToString();
                    orderConfirmation.SAddress = dsOrder.Tables[0].Rows[i]["SAddress"].ToString();
                    orderConfirmation.SMobileNo = dsOrder.Tables[0].Rows[i]["SMobileNo"].ToString();
                    orderConfirmation.VAddress = dsOrder.Tables[1].Rows[i]["Address"].ToString();
                    orderConfirmation.VGSTNO = dsOrder.Tables[1].Rows[i]["GSTNo"].ToString();
                    orderConfirmation.VCity = dsOrder.Tables[1].Rows[i]["City"].ToString();
                    orderConfirmation.ShippingCharges = dsOrder.Tables[0].Rows[i]["ShippingCharges"].ToString();
                    orderConfirmation.ProductName = dsOrder.Tables[1].Rows[i]["ProductName"].ToString();
                    orderConfirmation.Total = dsOrder.Tables[0].Rows[i]["FinalAmt"].ToString();
                    orderConfirmation.TransportMode = dsOrder.Tables[0].Rows[i]["TransportMode"].ToString();
                    orderConfirmation.State = dsOrder.Tables[0].Rows[i]["State"].ToString();
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

                    ItemsModel obj = new ItemsModel();

                    int qty = 0;
                    decimal TotalAmount = 0;
                    //decimal
                    //       cgstamount = Convert.ToDecimal(dsOrder.Tables[1].Rows[i]["CGSTAmt"].ToString()),
                    //       sgstamount = Convert.ToDecimal(dsOrder.Tables[1].Rows[i]["SGSTAmt"].ToString()),
                    //       igstamount = Convert.ToDecimal(dsOrder.Tables[1].Rows[i]["IGSTAmt"].ToString()),
                    //       orderamt = Convert.ToDecimal(dsOrder.Tables[0].Rows[i]["OrderAmt"].ToString());

                    //if (dsOrder.Tables[0].Rows[i]["State"].ToString().ToUpper() == "DELHI")
                    //{
                    //    obj.BaseAmount = Convert.ToDecimal(dsOrder.Tables[1].Rows[i]["OrderAmount"].ToString().Replace(".0000", ".00"));
                    //    orderConfirmation.BaseAmount = obj.BaseAmount - (cgstamount + sgstamount);
                    //    obj.BaseAmount = orderConfirmation.BaseAmount;
                    //}
                    //else
                    //{
                    //    obj.BaseAmount = Convert.ToDecimal(dsOrder.Tables[1].Rows[i]["OrderAmount"].ToString().Replace(".0000", ".00"));
                    //    orderConfirmation.BaseAmount = obj.BaseAmount - (igstamount);
                    //    obj.BaseAmount = orderConfirmation.BaseAmount;
                    //}

                    TotalAmount = Convert.ToDecimal(orderConfirmation.Total);

                    long finalamount = (long)Convert.ToDouble(TotalAmount);
                    ConvertNumbertoWords(finalamount);
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
                            Address = dsOrder.Tables[1].Rows[j]["Address"].ToString(),
                            City = dsOrder.Tables[1].Rows[j]["City"].ToString(),
                            GSTNo = dsOrder.Tables[1].Rows[j]["GSTNo"].ToString(),
                            ProductName = dsOrder.Tables[1].Rows[j]["ProductName"].ToString(),
                            ProductImage = dsOrder.Tables[1].Rows[j]["ImageNames"].ToString(),
                            VendorName = dsOrder.Tables[1].Rows[j]["FirmName"].ToString(),
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
                            BaseAmount = obj.BaseAmount,
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


        public ActionResult OrderInfromationKaryOn(string OrderId)
        {
            OrderConfirmationModel orderConfirmation = new OrderConfirmationModel();
            List<ItemsModel> OrderItems = new List<ItemsModel>();
            OrderDAL orderDAL = new OrderDAL();
            DataSet dsOrder = orderDAL.GetOrderDetailsKaryon(OrderId);
            if (dsOrder.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsOrder.Tables[0].Rows.Count; i++)
                {
                    orderConfirmation.BName = dsOrder.Tables[0].Rows[i]["BName"].ToString();
                    orderConfirmation.BAddress = dsOrder.Tables[0].Rows[i]["BAddress"].ToString();
                    orderConfirmation.BMobileNo = dsOrder.Tables[0].Rows[i]["BMobileNo"].ToString();
                    orderConfirmation.BillingPincode = dsOrder.Tables[0].Rows[i]["BillingPincode"].ToString();
                    orderConfirmation.SName = dsOrder.Tables[0].Rows[i]["SName"].ToString();
                    orderConfirmation.SAddress = dsOrder.Tables[0].Rows[i]["SAddress"].ToString();
                    orderConfirmation.SMobileNo = dsOrder.Tables[0].Rows[i]["SMobileNo"].ToString();
                    orderConfirmation.VAddress = dsOrder.Tables[1].Rows[i]["Address"].ToString();
                    orderConfirmation.VGSTNO = dsOrder.Tables[1].Rows[i]["GSTNo"].ToString();
                    orderConfirmation.VCity = dsOrder.Tables[1].Rows[i]["City"].ToString();
                    orderConfirmation.ShippingCharges = dsOrder.Tables[0].Rows[i]["ShippingCharges"].ToString();
                    orderConfirmation.ProductName = dsOrder.Tables[1].Rows[i]["ProductName"].ToString();
                    orderConfirmation.Total = dsOrder.Tables[0].Rows[i]["FinalAmt"].ToString();
                    orderConfirmation.TransportMode = dsOrder.Tables[0].Rows[i]["TransportMode"].ToString();
                    orderConfirmation.State = dsOrder.Tables[0].Rows[i]["State"].ToString();
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

                    ItemsModel obj = new ItemsModel();
                    int qty = 0;
                    decimal TotalAmount = 0;
                    //decimal
                    //       cgstamount = Convert.ToDecimal(dsOrder.Tables[1].Rows[i]["CGSTAmt"].ToString()),
                    //       sgstamount = Convert.ToDecimal(dsOrder.Tables[1].Rows[i]["SGSTAmt"].ToString()),
                    //       igstamount = Convert.ToDecimal(dsOrder.Tables[1].Rows[i]["IGSTAmt"].ToString()),
                    //       orderamt = Convert.ToDecimal(dsOrder.Tables[0].Rows[i]["OrderAmt"].ToString());

                    //if (dsOrder.Tables[0].Rows[i]["State"].ToString().ToUpper() == "DELHI")
                    //{
                    //    obj.BaseAmount = Convert.ToDecimal(dsOrder.Tables[1].Rows[i]["OrderAmount"].ToString().Replace(".0000", ".00"));
                    //    orderConfirmation.BaseAmount = obj.BaseAmount - (cgstamount + sgstamount);
                    //    obj.BaseAmount = orderConfirmation.BaseAmount;
                    //}
                    //else
                    //{
                    //    obj.BaseAmount = Convert.ToDecimal(dsOrder.Tables[1].Rows[i]["OrderAmount"].ToString().Replace(".0000", ".00"));
                    //    orderConfirmation.BaseAmount = obj.BaseAmount - (igstamount);
                    //    obj.BaseAmount = orderConfirmation.BaseAmount;
                    //}

                    TotalAmount = Convert.ToDecimal(orderConfirmation.Total);

                    long finalamount = (long)Convert.ToDouble(TotalAmount);
                    ConvertNumbertoWords(finalamount);
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
                            Address = dsOrder.Tables[1].Rows[j]["Address"].ToString(),
                            City = dsOrder.Tables[1].Rows[j]["City"].ToString(),
                            GSTNo = dsOrder.Tables[1].Rows[j]["GSTNo"].ToString(),
                            ProductName = dsOrder.Tables[1].Rows[j]["ProductName"].ToString(),
                            ProductImage = dsOrder.Tables[1].Rows[j]["ImageNames"].ToString(),
                            VendorName = dsOrder.Tables[1].Rows[j]["FirmName"].ToString(),
                            ProductAmt = dsOrder.Tables[1].Rows[j]["DealerPrice"].ToString(),
                            ProductQty = dsOrder.Tables[1].Rows[j]["ProductQty"].ToString(),
                            boxqty = dsOrder.Tables[1].Rows[j]["boxqty"].ToString(),
                            Discount = "0.00",
                            Tax = dsOrder.Tables[1].Rows[j]["DealerPrice"].ToString(),
                            CGST = dsOrder.Tables[1].Rows[j]["CGST"].ToString(),
                            SGST = dsOrder.Tables[1].Rows[j]["SGST"].ToString(),
                            IGST = dsOrder.Tables[1].Rows[j]["IGST"].ToString(),
                            CGSTAmount = dsOrder.Tables[1].Rows[j]["CGSTAmt"].ToString(),
                            SGSTAmount = dsOrder.Tables[1].Rows[j]["SGSTAmt"].ToString(),
                            IGSTAmount = dsOrder.Tables[1].Rows[j]["IGSTAmt"].ToString(),
                            //TotalProductAmt = dsOrder.Tables[1].Rows[j]["OrderAmount"].ToString(),
                            BaseAmount = obj.BaseAmount,
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
        public string ConvertNumbertoWords(long number)
        {
            if (number == 0) return "ZERO";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LAKES ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            //if ((number / 10) > 0)
            //{
            //    words += ConvertNumbertoWords(number / 10) + " RUPEES ";
            //    number %= 10;
            //}
            if (number > 0)
            {
                //if (words != "") words += "AND ";
                var unitsMap = new[]
                {
            "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
        };
                var tensMap = new[]
                {
            "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            Session["words"] = words;
            return words;
        }


        public JsonResult GetEditFranchiseItemInformation(string RequestId)
        {
            try
            {
                List<MAddShippingInformation> MemberList = AddShippingDetailsService.GetNewOrderRequestService(RequestId);
                return Json(MemberList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult GetEditActivatedMembersItemInformation(string RequestId)
        {
            try
            {
                List<MAddShippingInformation> MemberList = AddShippingDetailsService.GetEditOrderRequestService(RequestId);
                return Json(MemberList, JsonRequestBehavior.AllowGet);
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
                _param.CreatedBy = SessionManager.Fk_MemId.ToString();
                int _iresult = AddShippingDetailsService.AddShippingInformationService(_param);
                return Json(_iresult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }       
         public JsonResult AddActiveMemberShippingInformation(MAddShippingInformation _param)
        {
            try
            {
                _param.CreatedBy = SessionManager.Fk_MemId.ToString();
                int _iresult = AddShippingDetailsService.AddActiveMemberShippingInformationService(_param);
                return Json(_iresult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }       



         public ActionResult GetShipingInfroamtion(MAddShippingInformation _param) 
  
        {
                              
            DALShippingInformation dALShippingInformation = new DALShippingInformation();
            DataSet dataset = dALShippingInformation.GETShippingInformation(_param);

            _param.DocNo = (dataset.Tables[0].Rows[0]["DocNo"].ToString());
            _param.DocDate = dataset.Tables[0].Rows[0]["DocDate"].ToString();
            _param.NoOfCarton = dataset.Tables[0].Rows[0]["NoOfCarton"].ToString();
            _param.MobileNo = dataset.Tables[0].Rows[0]["MobileNo"].ToString();
            _param.VehicalNo = dataset.Tables[0].Rows[0]["VehicalNo"].ToString();
            _param.ModeOfTrip = dataset.Tables[0].Rows[0]["ModeOfTrip"].ToString();
            _param.PersonName = dataset.Tables[0].Rows[0]["PersonName"].ToString();
            _param.DeliveryAddress = dataset.Tables[0].Rows[0]["DeliveryAddress"].ToString();
            _param.State = dataset.Tables[0].Rows[0]["State"].ToString();
            _param.City = dataset.Tables[0].Rows[0]["City"].ToString();
            _param.PinCode = dataset.Tables[0].Rows[0]["PinCode"].ToString();
            _param.CourierName = dataset.Tables[0].Rows[0]["CourierName"].ToString();
            _param.RefNo = dataset.Tables[0].Rows[0]["RefNo"].ToString();
            _param.FreightCharge = dataset.Tables[0].Rows[0]["FreightCharge"].ToString();
            _param.Discount = dataset.Tables[0].Rows[0]["Discount"].ToString();
            
            _param.ElectronicRefNo = dataset.Tables[0].Rows[0]["ElectronicRefNo"].ToString();

            return Json(_param, JsonRequestBehavior.AllowGet);
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

        //Cancel Order

        public JsonResult FranchiseCancelOrder(string OrderNo)
        {
            var list = FOrderRequestService.FranchiseCancelOrder(OrderNo);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult jsonFranchiseCancelOrderAfterApproved(string OrderNo)
        {
            var list = DALFOrderRequest.FranchiseCancelOrderAfterApproved(OrderNo);
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        public JsonResult jsonFranchiseOrderItemCancel(int Pk_OrderRequestItemsRId,int Pk_RId)
        {
            var list = DALFOrderRequest.FranchiseCancelledOrderItem(Pk_OrderRequestItemsRId, Pk_RId);
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        public ActionResult FranchiseApprovedOrder(MFAOrderRequest _param, int? Page)
        {
            if (!PermissionManager.IsActionPermit("Approved Orders", ViewOptions.FormView))
            {
                return Redirect("/admin");
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
                _param.ProcId = 2;
                List<MFAOrderRequest> _obj = FOrderRequestService.GetNewOrderRequestService(_param);
                _param.MFAOrderRequestList = _obj;
                int totalRecords = 0;
                if (_param.MFAOrderRequestList.Count > 0)
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

        public ActionResult FranchiseApprovedOrderHistory(MFAOrderRequest _param, int? Page)
        {
            if (!PermissionManager.IsActionPermit("Stock Request Approval", ViewOptions.FormView))
            {
                return Redirect("/admin");
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
                _param.ProcId = 1;
                List<MFAOrderRequest> _obj = FOrderRequestService.GetNewOrderRequestService(_param);
                _param.MFAOrderRequestList = _obj;
                int totalRecords = 0;
                if (_param.MFAOrderRequestList.Count > 0)
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


        public ActionResult FranchiseCancelOrderDetails(MFAOrderRequest _param, int? Page)
        {
            if (!PermissionManager.IsActionPermit("Cancelled Orders", ViewOptions.FormView))
            {
                return Redirect("/admin");
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
                _param.ProcId = 3;
                List<MFAOrderRequest> _obj = FOrderRequestService.GetNewOrderRequestService(_param);
                _param.MFAOrderRequestList = _obj;
                int totalRecords = 0;
                if (_param.MFAOrderRequestList.Count > 0)
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

        public ActionResult ExportToExcelViewApproveRequestList(MFAOrderRequest _param, int? Page)
        {
            _param.ParentId = 0; // 0 for admin
            _param.ParentTypeId = Convert.ToInt32(SessionManager.Usertype);
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            _param.ProcId = 5;
            _param.Page = Page;
            List<MFAOrderRequest> _obj = FOrderRequestService.GetNewOrderRequestService(_param);
            var customers = _obj;
            DataTable dt = new DataTable("ViewApproveRequestList");
            dt.Columns.AddRange(new DataColumn[12] {
                                                     new DataColumn("Franchise Id"),
                                                     new DataColumn("Franchise Name"),
                                                     new DataColumn("Firm Name"),
                                                     new DataColumn("Mobile No."),
                                                     new DataColumn("Total Qty"),
                                                     new DataColumn("Total Amount"),
                                                     new DataColumn("Request Dt."),
                                                     new DataColumn("Order Sts."),
                                                     new DataColumn("Shipping Sts."),
                                                     new DataColumn("Shipped Dt."),
                                                     new DataColumn("Payment Approval"),
                                                     new DataColumn("Approved Dt.")


            });

            if (customers.Count > 0)
            {
                customers.ForEach(customer =>
                {
                    dt.Rows.Add(customer.LoginID,customer.FName,customer.CompanyName,customer.Contact, customer.TotalItems, customer.TotalAmt, customer.ReqDate, customer.OrderStatus, customer.ShippingStstId == "1" ? "Order Shipped" : "", customer.ShippingDate, customer.PaymentStatus, customer.ApproveDate);

                });
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ApproveOrderDetails.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("FranchiseApprovedOrder", "FranchiseOrderRequest");
            }
            // return View();
        }
        public ActionResult ExportToExcelCancelRequestList(MFAOrderRequest _param, int? Page)
        {
            _param.ParentId = 0; // 0 for admin
            _param.ParentTypeId = Convert.ToInt32(SessionManager.Usertype);
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            _param.ProcId = 6;
            _param.Page = Page;
            List<MFAOrderRequest> _obj = FOrderRequestService.GetNewOrderRequestService(_param);
            var customers = _obj;
            DataTable dt = new DataTable("ViewCancelRequestList");
            dt.Columns.AddRange(new DataColumn[10] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Franchise Name"),
                                                       new DataColumn("Firm Name"),
                                                      new DataColumn("Mobile No."),
                                                     new DataColumn("Total Qty"),
                                                     new DataColumn("Total Amount"),
                                                     new DataColumn("Request Dt."),
                                                     new DataColumn("Order Sts."),
                                                     new DataColumn("Payment Approval"),
                                                     new DataColumn("Approved Dt.")


            });

            if (customers.Count > 0)
            {
                customers.ForEach(customer =>
                {
                    dt.Rows.Add(customer.LoginID,customer.FName, customer.CompanyName, customer.Contact, customer.TotalItems, customer.TotalAmt, customer.ReqDate, customer.OrderStatus,  customer.PaymentStatus, customer.ApproveDate);

                });
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CancelOrderDetails.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("FranchiseCancelOrderDetails", "FranchiseOrderRequest");
            }
            // return View();
        }


    }
}