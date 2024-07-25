using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class ShiprocketViewModel
    {
        public string ShiprocketOrder { get; set; }
        public string ShiprocketProductOrder { get; set; }
        public string ShiprocketShipments { get; set; }
        public string Msg { get; set; }
        public long ShiprocketId { get; set; }
        public int Flag { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Product
    {
        public int id { get; set; }
        public string channel_order_product_id { get; set; }
        public string name { get; set; }
        public string channel_sku { get; set; }
        public int quantity { get; set; }
        public int product_id { get; set; }
        public int available { get; set; }
        public string status { get; set; }
        public string price { get; set; }
        public string product_cost { get; set; }
        public string hsn { get; set; }
    }

    public class Shipment
    {
        public int id { get; set; }
        public string isd_code { get; set; }
        public string courier { get; set; }
        public object courier_id { get; set; }
        public string shipping_charges { get; set; }
        public object weight { get; set; }
        public string dimensions { get; set; }
        public string shipped_date { get; set; }
        public string pickup_scheduled_date { get; set; }
        public string pickup_token_number { get; set; }
        public string awb { get; set; }
        public string return_awb { get; set; }
        public string volumetric_weight { get; set; }
        public string pod { get; set; }
        public string etd { get; set; }
        public string saral_etd { get; set; }
        public string rto_delivered_date { get; set; }
        public string delivered_date { get; set; }
        public bool etd_escalation_btn { get; set; }
        public string rto_initiated_date { get; set; }
        public object package_images { get; set; }
        public object weight_action { get; set; }
    }

    public class Others
    {
        public string state { get; set; }
        public int store_id { get; set; }
        public int entity_id { get; set; }
        public string billing_city { get; set; }
        public string billing_name { get; set; }
        public string increment_id { get; set; }
        public string payment_code { get; set; }
        public string billing_phone { get; set; }
        public string billing_state { get; set; }
        public string currency_code { get; set; }
        public string billing_country { get; set; }
        public string billing_pincode { get; set; }
        public int shipping_amount { get; set; }
        public string shipping_method { get; set; }
        public string billing_isd_code { get; set; }
        public int total_qty_ordered { get; set; }
        public int highest_percentage { get; set; }
        public double? weight { get; set; }
        public string dimensions { get; set; }
        public string company_name { get; set; }
        public object billing_alternate_phone { get; set; }
    }

    public class PickupAddressDetail
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string address_2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public object address_type { get; set; }
        public string pin_code { get; set; }
        public string pickup_code { get; set; }
        public string lat { get; set; }
        public string @long { get; set; }
    }

    public class Datum
    {
        public int id { get; set; }
        public int channel_id { get; set; }
        public string channel_name { get; set; }
        public string base_channel_code { get; set; }
        public string channel_order_id { get; set; }
        public string customer_name { get; set; }
        public string customer_email { get; set; }
        public string customer_phone { get; set; }
        public string customer_address { get; set; }
        public string customer_address_2 { get; set; }
        public string customer_city { get; set; }
        public string customer_state { get; set; }
        public string customer_pincode { get; set; }
        public object customer_latitude { get; set; }
        public object customer_longitude { get; set; }
        public string customer_alternate_phone { get; set; }
        public string pickup_location { get; set; }
        public object package_instructions { get; set; }
        public object order_type { get; set; }
        public string payment_status { get; set; }
        public string total { get; set; }
        public string tax { get; set; }
        public string sla { get; set; }
        public string shipping_method { get; set; }
        public int expedited { get; set; }
        public string status { get; set; }
        public int status_code { get; set; }
        public object awd_etds { get; set; }
        public string master_status { get; set; }
        public string payment_method { get; set; }
        public bool change_payment_mode { get; set; }
        public int is_international { get; set; }
        public int purpose_of_shipment { get; set; }
        public string channel_created_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public List<Product> products { get; set; }
        public List<Shipment> shipments { get; set; }
        public List<string> activities { get; set; }
        public int allow_return { get; set; }
        public int is_incomplete { get; set; }
        public object errors { get; set; }
        public string pickup_exception_reason { get; set; }
        public int show_escalation_btn { get; set; }
        public string escalation_status { get; set; }
        public List<object> escalation_history { get; set; }
        public int pii_removed { get; set; }
        public bool allow_multiship { get; set; }
        public Others others { get; set; }
        public PickupAddressDetail pickup_address_detail { get; set; }
        public int re_escalate { get; set; }
        public object seller_request { get; set; }
    }

    public class Counts
    {
        public bool is_first { get; set; }
    }

    public class Links
    {
        public string next { get; set; }
    }

    public class Pagination
    {
        public int total { get; set; }
        public int count { get; set; }
        public int per_page { get; set; }
        public int current_page { get; set; }
        public int total_pages { get; set; }
        public Links links { get; set; }
    }

    public class Meta
    {
        public Counts counts { get; set; }
        public Pagination pagination { get; set; }
    }

    public class Root
    {
        public List<Datum> data { get; set; }
        public Meta meta { get; set; }
    }
}