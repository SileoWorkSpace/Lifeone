using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateEntity
{
    public class MemberOrderId {
        public long OrderId { get; set; }
    }
    public class MAssociateAdminInvoice
    {

       
        public string MemberName { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public double PaidAmt { get; set; }
        public string OrderNo { get; set; }
        public string OrderDate { get; set; }
        public double TotalPv { get; set; }
        public double AirOrbit { get; set; }
        public double WaterOrbit { get; set; }
        public double SpaceOrbit { get; set; }
        public double Qpv { get; set; }
        public string PayMode { get; set; }
        public string BankName { get; set; }
        public List<MemberOrderDetail> MemberOrderDetailList { get; set; }


    
    }
    public class MemberOrderDetail
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double PrdAmt { get; set; }
        public double PV { get; set; }
        public double BoxPv { get; set; }
        public double TotalPv { get; set; }
        public double TotalAmt { get; set; }
       
        public double Qpv { get; set; }
        public int CGST { get; set; }
        public int IGST { get; set; }
        public int SGST { get; set; }
        public double CGSTAmt { get; set; }
        public double SGSTAmt { get; set; }
        public double IGSTAmt { get; set; }
        public double TaxableAmt { get; set; }

    }
}