using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeOne.Filters;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.Manager;
using LifeOne.Models.Common;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace LifeOne.Areas.Admin.Controllers
{

    [SessionTimeoutAttributeAdmin]
    public class StockManagementController : Controller
    {
        // GET: Admin/StockManagement

        public ActionResult StockReport(int? Page)
        {
            if (!PermissionManager.IsActionPermit("Stock Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            try
            {
                if (Page == null || Page == 0)
                {
                    Page = 1;
                }

                ProductStockReport objmodel = new ProductStockReport();
              
                List<ProductStockReport> _obj = StockManagementService.GetStockManagementReport(objmodel);
              ViewBag.StockReport = _obj;

               
                return View(objmodel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult StockReport(int? Page, ProductStockReport objmodel)
        {
            if (!PermissionManager.IsActionPermit("Stock Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            try
            {
                if (Page == null || Page == 0)
                {
                    Page = 1;
                }

             

                List<ProductStockReport> _obj = StockManagementService.GetStockManagementReport(objmodel);
                ViewBag.StockReport = _obj;
                return View(objmodel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult BindStockHistory(long PrdId, string LoginId, int? Page)
        {
            MOrder obj = new MOrder();
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            obj.Page = Page;
            obj.LstOrder = DALStockManagement.BindStockHistory(PrdId, LoginId, obj.Page);
            int totalRecords = 0;
            if (obj.LstOrder.Count > 0)
            {

                totalRecords = Convert.ToInt32(obj.LstOrder[0].TotalRecords);
                var pager = new Pager(totalRecords, obj.Page, SessionManager.Size);
                obj.Pager = pager;
            }
            else
            {
                obj.LstOrder = new List<MOrder>();
            }
            return View(obj);

        }

        public JsonResult BindHistoryByProductName(string ProductName)
        {
           

            List<ProductStockReport> lst = DALStockManagement.BindStockHistoryBYProductName(ProductName);

            return Json(lst,JsonRequestBehavior.AllowGet);

        }



        public ActionResult ExportToExcelStockReport(int? Page)
        {
            DataTable dt = new DataTable("Stock Detail");
            dt.Columns.AddRange(new DataColumn[10] {
                                                     new DataColumn("Category"),
                                                     new DataColumn("Orbit Type"),
                                                     new DataColumn("Product Name"),
                                                     new DataColumn("Total Qty"),
                                                     new DataColumn("MRP"),
                                                     new DataColumn("PV"),
                                                     new DataColumn("CGST"),
                                                     new DataColumn("IGST"),
                                                     new DataColumn("SGST"),
                                                     new DataColumn("Avl Stock/His.")


            });
            if (Page == null || Page == 0)
                Page = 1;
            MAdminStockManagement objmodel = new MAdminStockManagement();
            objmodel.Page = Page;
            objmodel.ProcId = 1;
            var customers = StockManagementService.GetStockManagementService(objmodel);
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.ProductType, customer.OrbitName, customer.ProductName, customer.TotalQty, customer.Amount, customer.PointValue, customer.CGST, customer.IGST, customer.SGST, customer.CurrentQty);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StockDetail.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("/Stock-Report");
            }
        }

    }
}