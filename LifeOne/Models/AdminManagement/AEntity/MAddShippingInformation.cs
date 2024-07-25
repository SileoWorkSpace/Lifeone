using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MAddShippingInformation
    {

        public string InvoiceId { get; set; }
        public string Fk_Rid { get; set; }
        public string Fk_FId { get; set; }
        public string Pk_TransId { get; set; }
        public string DocNo { get; set; }
        public string DocDate { get; set; }
        public string NoOfCarton { get; set; }
        public string ModeOfTrip { get; set; }
        public string VehicalNo { get; set; }
        public string DeliveryAddress { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string CreatedBy { get; set; }
        public string CourierName { get; set; }
        public string RefNo { get; set; }
        public string FreightCharge { get; set; }
        public string Discount { get; set; }
        public string PersonName { get; set; }
        public string MobileNo { get; set; }
       
        public string ElectronicRefNo { get; set; }


        public string PK_Uniid { get; set; }
        public string Fk_FbSID { get; set; }
        public string InvoiceTransferNo { get; set; }

    }

    public class MBindAssignOrdertoFranchise
    {
        public string TransferInvoiceNo { get; set; }
        public string PrdId { get; set; }
        public string PrductId { get; set; }
        public string FranchiseId { get; set; }
        public string FK_FbSID { get; set; }
        public string PersonName { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }
        public string ReqQty { get; set; }
        public string EmailAddress { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string CurrStatus { get; set; }
        public string Remarks { get; set; }
        public string xml { get; set; }

        public string CustomType { get; set; }
        public long Fk_memid { get; set; }



    }
    public class UpdateInvoicedateReponse
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
    }

}