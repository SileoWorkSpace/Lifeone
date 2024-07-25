using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    //public class KaryonProduct
    //{
    //    public int id { get; set; }
    //    public string sku { get; set; }
    //    public string name { get; set; }
    //    public decimal price { get; set; }
    //    public int status { get; set; }
    //    public string type_id { get; set; }
    //    public string gallery { get; set; }
    //    public decimal dp { get; set; }
    //    public decimal bv { get; set; }
    //    public decimal cashback { get; set; }
    //    public decimal gst { get; set; }
    //    public string hsn { get; set; }
    //    public List<optionViewModel> option { get; set; }
    //}
    public class KaryonProduct
    {
        public int id { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int status { get; set; }
        public string type_id { get; set; }
        public string gallery { get; set; }
        public decimal dp { get; set; }
        public decimal dp1 { get; set; }
        public decimal dp2 { get; set; }
        public decimal dp3 { get; set; }
        public decimal bv { get; set; }
        public decimal bv1 { get; set; }
        public decimal bv2 { get; set; }
        public decimal bv4 { get; set; }
        public decimal bottles { get; set; }
        public decimal bottles1 { get; set; }
        public decimal bottles2 { get; set; }
        public decimal bottles4 { get; set; }
        public decimal cashback { get; set; }
        public decimal gst { get; set; }
        public string hsn { get; set; }
        public string product_code { get; set; }
        public int Fk_OrbitType { get; set; }

        
        public List<optionViewModel> option { get; set; }
    }


    public class optionViewModel
    {
        public string product_sku { get; set; }
        public string option_id { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string sort_order { get; set; }
        public string is_require { get; set; }
        public string max_characters { get; set; }
        public string image_size_x { get; set; }
        public string image_size_y { get; set; }
        public List<valuesViewModel> values { get; set; }
    }


    public class valuesViewModel
    {
        public string title { get; set; }
        public int sort_order { get; set; }
        public int price { get; set; }
        public string price_type { get; set; }
        public string option_type_id { get; set; }
    }

    public class viewModel
    {
        public List<KaryonProduct> product_list { get; set; }

    }

    public class CategoryViewModel
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class CategoryListViewModel
    {
        public string response { get; set; }
        public List<CategoryViewModel> data { get; set; }
    }

    public class CropProductsViewModel
    {
        public string id { get; set; }
        public string crop_name { get; set; }
        public string crop_id { get; set; }
    }

    public class CropProductListViewModel
    {
        public string response { get; set; }
        public List<CropProductsViewModel> data { get; set; }
    }
}