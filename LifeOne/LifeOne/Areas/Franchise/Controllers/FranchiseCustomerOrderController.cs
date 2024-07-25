using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeOne.Filters;
using LifeOne.Models.FranchiseManagement.FDAL;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.FranchiseManagement.FService;
using LifeOne.Models.Common;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.ADAL;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using LifeOne.Models.Manager;

namespace LifeOne.Areas.Franchise.Controllers
{
  [SessionTimeoutAttributeFranchise]
    public class FranchiseCustomerOrderController : Controller
    {
        // GET: /Franchise/FranchiseCustomerOrder/AddOrderForCustomer

        public ActionResult ViewFranchiseSupplyOrderStatus(int? page, MOrder obj)
        {
            obj = FranchiseOrderService.SupplyOrderStatusService(page, obj);
            return View(obj);
        }
        public ActionResult ExportToExcelBillingOrdered(int? page, MOrder obj)
        {
            if (page == null || page == 0)
            {
                page = 1;
            }
            obj.Page = page;
            var customers = FranchiseOrderService.ExportToExcelBillingOrdered(page, obj);
            DataTable dt = new DataTable("Order List");
            dt.Columns.AddRange(new DataColumn[11] {
                                                     new DataColumn("Order No"),
                                                     new DataColumn("Supply To"),
                                                     new DataColumn("Payment Type"),
                                                     new DataColumn("TransactionNo"),
                                                     new DataColumn("Item Qty."),
                                                     new DataColumn("PV"),
                                                     new DataColumn("Order Amt.(₹)"),
                                                     new DataColumn("Discount Amt.(₹)"),
                                                     new DataColumn("Final Amt.(₹)"),
                                                     new DataColumn("Order Type"),
                                                     new DataColumn("Ordered Dt.")
            });

            if (customers.LstOrder.Count > 0)
            {
                foreach (var customer in customers.LstOrder)
                {
                    dt.Rows.Add(customer.OrderNo, customer.SupplyToName, customer.PaymentType,customer.TransactionNo, customer.ItemQty, customer.PointValue, (Convert.ToDecimal(customer.Amount) + customer.DiscountAmt), customer.DiscountAmt, ((Convert.ToDecimal(customer.Amount) + customer.DiscountAmt) - customer.DiscountAmt), "Box", customer.ApproveDate);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Order List.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("ViewFranchiseSupplyOrderStatus", "FranchiseCustomerOrder");
            }
        }
        public JsonResult ViewSupplyItemDetail(string RequestId)
        {
            List<MOrder> _obj = FranchiseOrderService.ViewSupplyItemService(long.Parse(RequestId));
            return Json(_obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddOrderForCustomer()
        {
            try
            {
                ViewBag.ProductCategory = DALBindCommonDropdown.BindDropdown(2, 0);
                //ViewBag.Product = DALBindCommonDropdown.BindDropdown(1, 0);
                ViewBag.Product = DALBindCommonDropdown.BindFranchiseProductDropdown(1, 0, SessionManager.FranchiseFk_MemId); 
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult CustomerDetail()
        {
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

        public JsonResult BindCustomerDetailByMobileNO(MFCoutomer obj)
        {
            try
            {
                var result = new DALFranchise().BindCustomerDetailByMobileNO(obj);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        public JsonResult PerfomanceMasterList()
        {
            try
            {
                var result = new DALFranchise().PerfomanceMasterList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        public JsonResult UpdatePerformanceLevel(MSimpleString model)
        {
            try
            {
                var result = new DALFranchise().UpdatePerformanceLevel(model);
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
                MSimpleString obj = FranchiseOrderService.AddFCustomerProductService(_data);
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
                MSimpleString result = FranchiseOrderService.SaveFCustomerProductService(_data);
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
        [HttpPost]
        public JsonResult GetFranchiseEwallet(int Fk_MemID)
        {
            try
            {
                FranchiseEWalletledger franchiseEWalletledger = new FranchiseEWalletledger();
                franchiseEWalletledger.FranchiseId = SessionManager.FranchiseFk_MemId;
                DataSet dataSet = franchiseEWalletledger.GetFranchiseEwalletBalance();
                franchiseEWalletledger.Balance = dataSet.Tables[0].Rows[0]["Balance"].ToString();
                return Json(franchiseEWalletledger, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult FranchiseCustomerTaxInvoice(string OrderId)
        {
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
                    orderConfirmation.HSNNo = dsOrder.Tables[1].Rows[i]["HSNNo"].ToString();
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
                    orderConfirmation.FreeAmount = dsOrder.Tables[0].Rows[i]["FreeAmount"].ToString();
                    orderConfirmation.CompanyLogo = dsOrder.Tables[0].Rows[i]["CompanyLogo"].ToString();
                    ///orderConfirmation.StateCode = dsOrder.Tables[0].Rows[i]["StateCode"].ToString();

                    ItemsModel obj = new ItemsModel();

                    int qty = 0;
                    decimal TotalAmount = 0;

                    TotalAmount = Convert.ToDecimal(orderConfirmation.Total);

                    long finalamount = (long)Convert.ToDouble(TotalAmount);
                    orderConfirmation.AmountInWords = dsOrder.Tables[0].Rows[i]["AmountInWords"].ToString();

                    for (int j = 0; j < dsOrder.Tables[1].Rows.Count; j++)
                    {

                        OrderItems.Add(new ItemsModel
                        {
                            ProductUrl = "",
                            GSTNo = dsOrder.Tables[1].Rows[j]["GSTNo"].ToString(),
                            ProductName = dsOrder.Tables[1].Rows[j]["ProductName"].ToString(),
                            HSNNo = dsOrder.Tables[1].Rows[j]["HSNNo"].ToString(),
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
                            AmountInWords = orderConfirmation.AmountInWords
                        });
                    }
                    qty += int.Parse(dsOrder.Tables[1].Rows[i]["ProductQty"].ToString());
                    int totalPointValue= Convert.ToInt32(dsOrder.Tables[1].Rows[i]["PV"].ToString());
                    if (totalPointValue == 0)
                    {
                        ViewBag.BVTotAmtInWord = "ZERO";
                    }
                    else
                    {
                        ViewBag.BVTotAmtInWord = DALCommon.ConvertNumbertoWords(totalPointValue);
                    }
                    orderConfirmation.TotalProductQty = qty.ToString();
                    orderConfirmation.OrderItems = OrderItems;
                    orderConfirmation.VendorName = OrderItems[0].VendorName;
                    ViewBag.PaymentMode = dsOrder.Tables[1].Rows[0]["PayMode"].ToString();
                    ViewBag.BankName = dsOrder.Tables[1].Rows[0]["BankName"].ToString();
                    ViewBag.TransactionNo = dsOrder.Tables[1].Rows[0]["TransactionNo"].ToString();
                    ViewBag.PaymentDate = dsOrder.Tables[1].Rows[0]["PaymentDate"].ToString();
                }
                return View(orderConfirmation);
            }
            return View(orderConfirmation);
        }
        //ActivatedCustomerTaxInvoice
        public ActionResult ActivatedCustomerTaxInvoice(string Pk_TransId)
        {
            ActivatedCustomerTaxInvoice orderConfirmation = new ActivatedCustomerTaxInvoice();
            List<InvoiceProduct> OrderItems = new List<InvoiceProduct>();
            OrderDAL orderDAL = new OrderDAL();
            DataSet dsOrder = orderDAL.ActivatedCustomerInvoiceById(Pk_TransId);
            Int32 TotalAmount = 0;
            if (dsOrder.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsOrder.Tables[0].Rows.Count; i++)
                {
                    orderConfirmation.LoginId = dsOrder.Tables[0].Rows[i]["LoginId"].ToString();
                    orderConfirmation.DisplayName = dsOrder.Tables[0].Rows[i]["DisplayName"].ToString();
                    orderConfirmation.Amount = Convert.ToDecimal(dsOrder.Tables[0].Rows[i]["Amount"].ToString());
                    orderConfirmation.BV = Convert.ToDecimal(dsOrder.Tables[0].Rows[i]["BV"].ToString());
                    orderConfirmation.ActivationDate = dsOrder.Tables[0].Rows[i]["InvoiceDate"].ToString();
                    orderConfirmation.InnvoiceNo = dsOrder.Tables[0].Rows[i]["InnvoiceNo"].ToString();
                    orderConfirmation.CompanyName = dsOrder.Tables[0].Rows[i]["CompanyName"].ToString();
                    orderConfirmation.OfficeAddress = dsOrder.Tables[0].Rows[i]["OfficeAddress"].ToString();
                    orderConfirmation.OfficeEmailId = dsOrder.Tables[0].Rows[i]["OfficeEmailId"].ToString();
                    orderConfirmation.OfficeContactNo = dsOrder.Tables[0].Rows[i]["OfficeContactNo"].ToString();
                    orderConfirmation.CompanyGST = dsOrder.Tables[0].Rows[i]["CompanyGST"].ToString();
                    orderConfirmation.Website = dsOrder.Tables[0].Rows[i]["Website"].ToString();
                    orderConfirmation.Pancard = dsOrder.Tables[0].Rows[i]["Pancard"].ToString();
              

                    orderConfirmation.City = dsOrder.Tables[0].Rows[i]["City"].ToString();
                    orderConfirmation.State = dsOrder.Tables[0].Rows[i]["State"].ToString();
                    orderConfirmation.Pincode = dsOrder.Tables[0].Rows[i]["Pincode"].ToString();

                    orderConfirmation.custAddress = dsOrder.Tables[0].Rows[i]["custAddress"].ToString();
                    orderConfirmation.custCity = dsOrder.Tables[0].Rows[i]["custCity"].ToString();
                    orderConfirmation.custPincode = dsOrder.Tables[0].Rows[i]["custPincode"].ToString();
                    orderConfirmation.custState = dsOrder.Tables[0].Rows[i]["custState"].ToString();
                    orderConfirmation.mobile = dsOrder.Tables[0].Rows[i]["mobile"].ToString();
                    orderConfirmation.CompanyLogo = dsOrder.Tables[0].Rows[i]["CompanyLogo"].ToString();

                    for (i = 0; i < dsOrder.Tables[1].Rows.Count; i++)
                    {

                        OrderItems.Add(new InvoiceProduct
                        {
                            ProductName = dsOrder.Tables[1].Rows[i]["ProductName"].ToString(),
                            Qty = dsOrder.Tables[1].Rows[i]["Qty"].ToString(),
                            Rate = dsOrder.Tables[1].Rows[i]["Rate"].ToString(),
                            SGST = dsOrder.Tables[1].Rows[i]["SGST"].ToString(),
                            CGST = dsOrder.Tables[1].Rows[i]["CGST"].ToString(),
                            IGST = dsOrder.Tables[1].Rows[i]["IGST"].ToString(),
                            Tax = dsOrder.Tables[1].Rows[i]["Tax"].ToString(),
                            TaxableAmt = dsOrder.Tables[1].Rows[i]["TaxableAmt"].ToString(),
                            SGSTAmt = dsOrder.Tables[1].Rows[i]["SGSTAmt"].ToString(),
                            CGSTAmt = dsOrder.Tables[1].Rows[i]["CGSTAmt"].ToString(),
                            IGSTAmt = dsOrder.Tables[1].Rows[i]["IGSTAmt"].ToString(),
                            TotalTaxAmt = dsOrder.Tables[1].Rows[i]["TaxAmt"].ToString(),
                            TotAmount = dsOrder.Tables[1].Rows[i]["TotAmount"].ToString(),
                            HSNNo = dsOrder.Tables[1].Rows[i]["HSNNo"].ToString(),


                    });



                        TotalAmount += Convert.ToInt32(dsOrder.Tables[1].Rows[i]["TotAmount"]);
                    }


                    orderConfirmation.OrderItems = OrderItems;
                    if (TotalAmount == 0)
                    {
                        ViewBag.TotAmtInWord = "ZERO";
                    }
                    else
                    {
                        ViewBag.TotAmtInWord = DALCommon.ConvertNumbertoWords(TotalAmount);
                    }
                    ViewBag.BVAmountInWords = dsOrder.Tables[0].Rows[0]["BVAmountInWords"].ToString();
                    ViewBag.TotalBV = dsOrder.Tables[0].Rows[0]["TotalBV"].ToString();
                    ViewBag.PayMentMode = dsOrder.Tables[0].Rows[0]["PayMentMode"].ToString();
                    ViewBag.TransactionNo = dsOrder.Tables[0].Rows[0]["TransactionNo"].ToString();
                    ViewBag.BankName = dsOrder.Tables[0].Rows[0]["BankName"].ToString();
                    ViewBag.PaymentDate = dsOrder.Tables[0].Rows[0]["PaymentDate"].ToString();
                    
                }
                return View(orderConfirmation);
            }
            return View(orderConfirmation);
        }


        public ActionResult ViewFranchiseOrderStatus(long SBSId)
        {
            MOrder obj = new MOrder();

            obj.PK_RId = SBSId;
             obj = FranchiseOrderService.Get_Current_SuppliedOrderList(obj);
            return View(obj);
            
        }
        
    }
}