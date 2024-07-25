using ClosedXML.Excel;
using LifeOne.Models.API;
using LifeOne.Models.API.DAL;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Admin.Controllers
{
    [SessionTimeoutAttributeAdmin]
    public class AdminProductsController : Controller
    {
        // GET: Admin/AdminProducts
        public ActionResult AdminProductList(ProductViewModel model)
        {
            if (!PermissionManager.IsActionPermit("Karyon Products", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            int totalRecords = 0;
            if (model.page <= 0)
                model.page = 1;


            CustomerDb objcusdb = new CustomerDb();
            var res = objcusdb.GetProductList(model);
            model.ProductList = res.GroupBy(x => x.ProductId).Select(x => new ProductViewModel
            {
                ToatlRecord = x.FirstOrDefault().ToatlRecord,
                ProductId = x.FirstOrDefault().ProductId,
                MagentoProdId = x.FirstOrDefault().MagentoProdId,
                ProductName = x.FirstOrDefault().ProductName,
                ProductImage = x.FirstOrDefault().ProductImage,
                MRP = x.FirstOrDefault().MRP,
                Units = x.FirstOrDefault().Units,
                PV = x.FirstOrDefault().PV,
                BV = x.FirstOrDefault().BV,
                DP = x.FirstOrDefault().DP,
                ProductUnitSizeList = x.Select(y => new ProductUnitSizeViewModel
                {
                    KeyId = y.KeyId,
                    OptionTypeId = y.OptionTypeId,
                    Price = y.Price + x.FirstOrDefault().MRP,
                    Size = y.Size
                }).ToList()
            }).ToList();
            if (model.ProductList.Count > 0)
            {
                totalRecords = Convert.ToInt32(res[0].ToatlRecord);
                var pager = new Pager(totalRecords, model.page, SessionManager.Size);
                model.Pager = pager;
            }
            return View(model);
        }

        public ActionResult ExportToExcelProductList(ProductViewModel model)
        {
            if (model.page <= 0)
                model.page = 1;
            CustomerDb objcusdb = new CustomerDb();
            var customers = objcusdb.GetProductList(model);
            model.ProductList = customers.GroupBy(x => x.ProductId).Select(x => new ProductViewModel
            {
                ProductId = x.FirstOrDefault().ProductId,
                MagentoProdId = x.FirstOrDefault().MagentoProdId,
                ProductName = x.FirstOrDefault().ProductName,
                ProductImage = x.FirstOrDefault().ProductImage,
                MRP = x.FirstOrDefault().MRP,
                Units = x.FirstOrDefault().Units,
                PV = x.FirstOrDefault().PV,
                BV = x.FirstOrDefault().BV,
                DP = x.FirstOrDefault().DP,
                ProductUnitSizeList = x.Select(y => new ProductUnitSizeViewModel
                {
                    KeyId = y.KeyId,
                    OptionTypeId = y.OptionTypeId,
                    Price = y.Price + x.FirstOrDefault().MRP,
                    Size = y.Size
                }).ToList()
            }).ToList();
            DataTable dt = new DataTable("ProductList");
            dt.Columns.AddRange(new DataColumn[6] {
                                                     new DataColumn("ProductName"),
                                                     new DataColumn("ProductImage"),
                                                     new DataColumn("MRP"),
                                                     new DataColumn("PV"),
                                                     new DataColumn("BV"),
                                                     new DataColumn("DP")
            });

            if (model.ProductList.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.ProductName, customer.ProductImage, customer.MRP, customer.PV, customer.BV, customer.DP);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ProductList.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("/Product-List");
            }
        }
    }
}