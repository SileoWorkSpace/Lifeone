
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Models.Common
{
    public class MagentProductOrder
    {

        public string id { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public string status { get; set; }
        public string type_id { get; set; }
        public string gallery { get; set; }
        public string BP { get; set; }
        public string DP { get; set; }
        public string gst { get; set; }
        public string hsn { get; set; }
        public string rate { get; set; }
        public string sku { get; set; }
        public string quantity { get; set; }

        public List<ComboPack> option { get; set; }
    }

    //public class ComboPack
    //{

    //    public string sku { get; set; }
    //    public string link_type { get; set; }
    //    public string linked_product_sku { get; set; }
    //    public string linked_product_type { get; set; }
    //    public string position { get; set; }

    //    public string[] extension_attributes { get; set; }
    //    ////    public string qty { get; set; }



    //}



    public class ComboPack
    {
        public string sku { get; set; }
        public string link_type { get; set; }
        public string linked_product_sku { get; set; }
        public string linked_product_type { get; set; }
        public int position { get; set; }
        public Extension_Attributes extension_attributes { get; set; }
    }

    public class Extension_Attributes
    {
        public int qty { get; set; }
    }




    public class ComboPackage
    {

        public string Price { get; set; }
        public string PackageName { get; set; }
        public string id { get; set; }

        public List<SelectListItem> PackageList()
        {
            List<SelectListItem> obj = new List<SelectListItem>();
            List<ComboPackage> lst = new List<ComboPackage>();
            try
            {

                lst = DBHelperDapper.DAGetDetailsInList<ComboPackage>("PackaageList");
                if(lst !=null & lst.Count>0)
                {
                    foreach (var item in lst as IEnumerable<ComboPackage>)
                    {
                        obj.Add(new SelectListItem { 
                        
                         Text=item.PackageName,
                         Value=item.id
                        
                        });




                    }

                }

            }
        catch(Exception ex)
            {

            }
            return obj;


        }
        public List<SelectListItem> UpgradePackageList()
        {
            List<SelectListItem> obj = new List<SelectListItem>();
            List<ComboPackage> lst = new List<ComboPackage>();
            try
            {

                lst = DBHelperDapper.DAGetDetailsInList<ComboPackage>("UpgradepackageList");
                if (lst != null & lst.Count > 0)
                {
                    foreach (var item in lst as IEnumerable<ComboPackage>)
                    {
                        obj.Add(new SelectListItem
                        {

                            Text = item.PackageName,
                            Value = item.id

                        });




                    }

                }

            }
            catch (Exception ex)
            {

            }
            return obj;


        }

    }
   

    public class MagentProductOrderLs
    {

        public List<MagentProductOrder> product_list { get; set; }




        public void SaveMegentoStock()
        {
            MagentProductOrderLs MagentoProduct = null;

            DataTable dt1 = new DataTable("TempMagentoProductItem");

            dt1.Columns.Add("sku");
            dt1.Columns.Add("link_type");
            dt1.Columns.Add("linked_product_sku");
            dt1.Columns.Add("linked_product_type");
            dt1.Columns.Add("position");
            dt1.Columns.Add("qty");
            DataRow dr1;

            DataTable dt = new DataTable("TempMagentoProduct");

            dt.Columns.Add("id");
            dt.Columns.Add("name");
            dt.Columns.Add("Price");
            dt.Columns.Add("Status");
            dt.Columns.Add("Type_Id");
            dt.Columns.Add("Gallery");
            dt.Columns.Add("BP");
            dt.Columns.Add("DP");
            dt.Columns.Add("gst");
            dt.Columns.Add("hsn");
            dt.Columns.Add("rate");
            dt.Columns.Add("sku");
            dt.Columns.Add("quantity");
            DataRow dr;


            List<MagentProductOrder> list = new List<MagentProductOrder>();

            string url = "https://shop.lifeonewellness.com/api/products.php";

            if (!string.IsNullOrEmpty(url))
            {

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.DefaultConnectionLimit = 9999;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json";


                WebResponse response = request.GetResponse();

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var ApiStatus = reader.ReadToEnd();
                    MagentoProduct = JsonConvert.DeserializeObject<MagentProductOrderLs>(ApiStatus);
                    if (MagentoProduct != null && MagentoProduct.product_list != null && MagentoProduct.product_list.Count > 0)
                    {

                        foreach (var item in MagentoProduct.product_list as IEnumerable<MagentProductOrder>)
                        {
                            dr = dt.NewRow();
                            dr["id"] = item.id;
                            dr["name"] = item.name;
                            dr["Price"] = item.price;
                            dr["Status"] = item.status;
                            dr["Type_Id"] = item.type_id;
                            dr["Gallery"] = item.gallery;
                            dr["BP"] = item.BP;
                            dr["DP"] = item.DP;
                            dr["gst"] = item.gst;
                            dr["hsn"] = item.hsn;
                            dr["rate"] = item.rate;
                            dr["sku"] = item.sku;
                            dr["quantity"] = item.quantity;

                            dt.Rows.Add(dr);

                            if (item.option.Count > 0 && item.option != null)
                            {
                                foreach (var item1 in item.option as IEnumerable<ComboPack>)
                                {
                                    dr1 = dt1.NewRow();
                                    dr1["sku"] = item1.sku;
                                    dr1["link_type"] = item1.link_type;
                                    dr1["linked_product_sku"] = item1.linked_product_sku;
                                    dr1["linked_product_type"] = item1.linked_product_type;
                                    dr1["position"] = item1.position;
                                    dr1["qty"] = item1.extension_attributes.qty;
                                    dt1.Rows.Add(dr1);
                                }

                            }



                        }

                    }


                }

                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {

                    SqlParameter[] param =
                    {

                      new SqlParameter("@MagentoProductOrder",dt),
                      new SqlParameter("@TeempProductItem",dt1)
                    };


                    DataSet ds = DBHelper.ExecuteQuery("SaveProductDetails", param);


                }


            }


        }





    }
}