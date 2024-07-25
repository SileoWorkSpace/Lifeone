
using ClosedXML.Excel;
using LifeOne.Filters;
using LifeOne.Models.Common;
using LifeOne.Models.FranchiseManagement.FDAL;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.FranchiseManagement.FService;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.Manager;

namespace LifeOne.Areas.Franchise.Controllers
{
    [SessionTimeoutAttributeFranchise]
    public class FranchiseOrderStatusController : Controller
    {
        // GET: Franchise/FranchiseOrderStatus
        //Show requsted Order Details
        public ActionResult ViewRequestdOrderStatus(int? page, MOrder obj)
        {
            obj = FranchiseOrderService.RequestdOrderStatusService(page, obj);
            return View(obj);
        }

        public ActionResult ExportToExcelRequestdOrderList(int? page, MOrder obj)
        {
            var customers = FranchiseOrderService.RequestdOrderStatusService(page, obj);
            DataTable dt = new DataTable("RequestdOrderList");
            dt.Columns.AddRange(new DataColumn[9] {
                                                     new DataColumn("Order No"),
                                                     new DataColumn("Qty."),
                                                     new DataColumn("Total PV"),
                                                     new DataColumn("Payment Type"),
                                                     new DataColumn("Order"),
                                                     new DataColumn("Order Status"),
                                                     new DataColumn("Payment Status"),
                                                     new DataColumn("Approved Dt"),
                                                     new DataColumn("Shipped Dt")
            });

            if (customers.LstOrder.Count > 0)
            {
                foreach (var customer in customers.LstOrder)
                {
                    dt.Rows.Add(customer.OrderNo, customer.ItemQty, customer.TotalPV, customer.PaymentType, customer.OrderDate + "\n" + customer.Amount, customer.OrderStatus, customer.PaymentStatus, customer.ApproveDate, customer.ShippedDate);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RequestdOrderList.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("ViewRequestdOrderStatus", "FranchiseOrderStatus");
            }
        }
        // Currnt Stock 
        // GET: Franchise/FranchiseOrderStatus/MyStockDetails
        public ActionResult MyStockDetails(int? page, MFranchiseItemStock obj)
        {
            try
            {
                if (page == null || page == 0)
                {
                    page = 1;
                }
                obj.Page = page;
                obj = FranchiseOrderService.MyStockDetails(obj);
                return View(obj);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public ActionResult ExportToExcelStockDetails(int? page, MFranchiseItemStock obj)
        {
            if (page == null || page == 0)
            {
                page = 1;
            }
            obj.Page = page;
            var customers = FranchiseOrderService.MyStockDetails(obj);
            DataTable dt = new DataTable("RequestdOrderList");
            dt.Columns.AddRange(new DataColumn[9] {
                                                     new DataColumn("Category"),
                                                     new DataColumn("Orbit Type"),
                                                     new DataColumn("Product Name"),
                                                     new DataColumn("MRP"),
                                                     new DataColumn("PV"),
                                                     new DataColumn("CGST"),
                                                     new DataColumn("IGST"),
                                                     new DataColumn("SGST"),
                                                     new DataColumn("Avl Stock/His.")
            });

            if (customers.LstStock.Count > 0)
            {
                foreach (var customer in customers.LstStock)
                {
                    dt.Rows.Add(customer.ProductType, customer.OrbitName, customer.ProductName, customer.Amount, customer.PointValue, customer.CGST, customer.IGST, customer.SGST, customer.AvailableStock);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StockDetails.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("MyStockDetails", "FranchiseOrderStatus");
            }
        }
        public ActionResult FranchiseBillingStockHistory(long id)
        {
            try
            {
                MFranchiseBillingStockHistory obj = new MFranchiseBillingStockHistory();
                obj.PrdId = id;
                obj = FranchiseBillingStockService.FranchiseBillingStock(obj);
                return View(obj);
            }
            catch (Exception)
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
        public JsonResult BindStockHistory(string PrdId)
        {
            try
            {
                List<MOrder> _obj = DALFranchise.BindStockHistory(long.Parse(PrdId));
                return Json(_obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult ReceiveFranchiseOrder(string RequestId)
        {
            try
            {
                MSimpleString _obj = FranchiseOrderService.ReceiveFranchiseOrder(long.Parse(RequestId));
                return Json(_obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }

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
                    orderConfirmation.CompanyLogo = dsOrder.Tables[0].Rows[i]["CompanyLogo"].ToString();

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
        public JsonResult AddShipingInfroamtion(MAddShippingInformation _param)
        {
            try
            {
                int _iresult = AddShippingDetailsService.AddShippingInformationService(_param);
                return Json(_iresult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult ViewNewRequestDetailFranchise(string RequestId)
        {
            List<MFAOrderRequestDetail> _obj = FOrderRequestService.GetNewOrderRequestItemFranchiseService(long.Parse(RequestId), SessionManager.FranchiseFk_MemId);
            return Json(_obj, JsonRequestBehavior.AllowGet);
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
    }
}