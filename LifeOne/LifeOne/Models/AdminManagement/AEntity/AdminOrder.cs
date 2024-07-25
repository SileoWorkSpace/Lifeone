using Dapper;
using Org.BouncyCastle.Asn1.Crmf;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.API.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class AdminOrder
    {
        public string Code { get; set; }
        public string Msg { get; set; }
        public string Message { get; set; }
        public string Paymentmode { get; set; }
        public string TransactionNo { get; set; }
        public string PaymentMode { get; set; }
        public string BankName { get; set; }
        public string ReceiptImage { get; set; }
        public string Fk_MemId { get; set; }
        public string BranchName { get; set; }
        public string ChequeDate { get; set; }
        public string ChequeNo { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public string TotalAmount { get; set; }
        public string Amount { get; set; }
        public string Remark { get; set; }
        public int CreatedBy { get; set; }
        public int OrderType { get; set; }
        public int PackageId { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }





    }

    public class OrderDetails
    {

        public string productname { get; set; }
        public string quatity { get; set; }
        public string image { get; set; }
        public string price { get; set; }
        public string producttype { get; set; }
        public string cashbackper { get; set; }
        public string DP { get; set; }
        public string BV { get; set; }
        

    }
    public class InvoiceDetail
    {

        public string LoginId { get; set; }
        public string Name { get; set; }
        public string OrderNo { get; set; }
        public string InvoiceDate { get; set; }
        public string Mobile { get; set; }
        public string State { get; set; }
        public string StateCode { get; set; }

        public string Address { get; set; }
        public string InWordBV { get; set; }
        public string InWordDP { get; set; }
        public string EmployeeId { get; set; }
        public string PayMode { get; set; }
        public string BankName { get;  set; }
        public string PaymentDate { get;  set; }
        public string TransactionNo { get;  set; }

    }
    public class InvoiceItemList
    {
        public string ProductName { get; set; }
        public string HSNCode { get; set; }
        public string Qty { get; set; }

        public string MRP { get; set; }
        public string BV { get; set; }
        public string DP { get; set; }
        public string SingleBV { get; set; }
        public string SingleDP { get; set; }
        public string InvoiceDate { get; set; }
        public decimal GSTAmount { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal GSTPer { get; set; }
    }

    public class PrintInvoice
    {
        public List<InvoiceItemList> ItemList { get; set; }
        public InvoiceDetail OrderDetails { get; set; }
        public string CompanyName { get; set; }
        public string OfficeAddress { get; set; }
        public string OfficeContactNo { get; set; }
        public string GSTIN { get; set; }
        public string Website { get; set; }
        public string PAN { get; set; }
        public string OfficeEmailId { get; set; }
        public string CompanyLogo { get; set; }


        public PrintInvoice PrintInvoiceDetail(string PkOrderId,string Fk_MemId)
        {
            PrintInvoice obj = new PrintInvoice();
            InvoiceDetail obj1 = new InvoiceDetail();
            List<InvoiceItemList> lst = new List<InvoiceItemList>();
            try
            {
                SqlParameter[] param =
                {


                    new SqlParameter("@orderId",PkOrderId),
                     new SqlParameter("@MemberId",Fk_MemId)

                };
                DataSet ds = DBHelper.ExecuteQuery("PrintInvoice", param);
                if(ds !=null && ds.Tables[0].Rows.Count>0)
                {
                    foreach (DataRow dr1 in ds.Tables[0].Rows)
                    {

                        obj1.LoginId = dr1["LoginId"].ToString();
                        obj1.Name = dr1["Name"].ToString();
                        obj1.OrderNo = dr1["OrderNo"].ToString();
                        obj1.InvoiceDate = dr1["InvoiceDate"].ToString();
                        obj1.Mobile = dr1["Mobile"].ToString();
                        obj1.State = dr1["State"].ToString();
                        obj1.Address = dr1["Address1"].ToString();
                        obj1.InWordBV = dr1["InWordBV"].ToString();
                        obj1.InWordDP = dr1["InWordDP"].ToString();
                        obj1.EmployeeId = dr1["EmployeeId"].ToString();
                        obj1.PayMode = dr1["PayMode"].ToString();
                        obj1.StateCode = dr1["StateCode"].ToString();
                        obj1.PayMode = dr1["PayMode"].ToString();
                        obj1.BankName = dr1["BankName"].ToString();
                        obj1.PaymentDate = dr1["PaymentDate"].ToString();
                        obj1.TransactionNo = dr1["TransactionNo"].ToString();


                    }

                }
                obj.OrderDetails = obj1;

                if (ds != null && ds.Tables[1].Rows.Count > 0)
                { 
                foreach(DataRow dr in ds.Tables[1].Rows)
                    {

                        lst.Add(new InvoiceItemList { 
                        
                        BV=dr["BV"].ToString(),
                        DP= dr["DP"].ToString(),
                        InvoiceDate= dr["CreatedDate"].ToString(),
                        MRP= dr["Amount"].ToString(),
                        ProductName= dr["ProductName"].ToString(),
                        Qty= dr["Quatity"].ToString(),
                        SingleBV = dr["SingleBV"].ToString(),
                         SingleDP = dr["SingleDP"].ToString(),
                          GSTAmount =Convert.ToDecimal(dr["GSTAmount"].ToString()),
                          TaxableAmount = Convert.ToDecimal(dr["TaxableAmount"].ToString()),
                          GSTPer = Convert.ToDecimal(dr["GSTPer"].ToString()),
                            HSNCode = dr["HSNCode"].ToString()


                        });


                    }

                    obj.ItemList = lst;
                }
                }
            catch(Exception ex)
            {


            }

            return obj;

        }
        public PrintInvoice PrintInvoiceDetailOld(string PkOrderId, string Fk_MemId)
        {
            PrintInvoice obj = new PrintInvoice();
            InvoiceDetail obj1 = new InvoiceDetail();
            List<InvoiceItemList> lst = new List<InvoiceItemList>();
            try
            {
                SqlParameter[] param =
                {


                    new SqlParameter("@orderId",PkOrderId),
                     new SqlParameter("@MemberId",Fk_MemId)

                };
                DataSet ds = DBHelper.ExecuteQuery("PrintInvoiceOld", param);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr1 in ds.Tables[0].Rows)
                    {

                        obj1.LoginId = dr1["LoginId"].ToString();
                        obj1.Name = dr1["Name"].ToString();
                        obj1.OrderNo = dr1["OrderNo"].ToString();
                        obj1.InvoiceDate = dr1["InvoiceDate"].ToString();
                        obj1.Mobile = dr1["Mobile"].ToString();
                        obj1.State = dr1["State"].ToString();
                        obj1.Address = dr1["Address1"].ToString();
                        obj1.InWordBV = dr1["InWordBV"].ToString();
                        obj1.InWordDP = dr1["InWordDP"].ToString();
                        obj1.EmployeeId = dr1["EmployeeId"].ToString();
                        obj1.PayMode = dr1["PayMode"].ToString();
                        obj1.StateCode = dr1["StateCode"].ToString();
                        obj1.PayMode = dr1["PayMode"].ToString();
                        obj1.BankName = dr1["BankName"].ToString();
                        obj1.PaymentDate = dr1["PaymentDate"].ToString();
                    


                    }

                }
                obj.OrderDetails = obj1;

                if (ds != null && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {

                        lst.Add(new InvoiceItemList
                        {

                            BV = dr["BV"].ToString(),
                            DP = dr["DP"].ToString(),
                            InvoiceDate = dr["CreatedDate"].ToString(),
                            MRP = dr["Amount"].ToString(),
                            ProductName = dr["ProductName"].ToString(),
                            Qty = dr["Quatity"].ToString(),
                            SingleBV = dr["SingleBV"].ToString(),
                            SingleDP = dr["SingleDP"].ToString(),
                            GSTAmount = Convert.ToDecimal(dr["GSTAmount"].ToString()),
                            TaxableAmount = Convert.ToDecimal(dr["TaxableAmount"].ToString()),
                            GSTPer = Convert.ToDecimal(dr["GSTPer"].ToString()),
                            HSNCode = dr["HSNCode"].ToString()


                        });


                    }

                    obj.ItemList = lst;
                }

                if (ds != null && ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr2 in ds.Tables[2].Rows)
                    {

                        obj.CompanyName = dr2["CompanyName"].ToString();
                        obj.OfficeAddress = dr2["OfficeAddress"].ToString();
                        obj.OfficeContactNo = dr2["OfficeContactNo"].ToString();
                        obj.GSTIN = dr2["CompanyGST"].ToString();
                        obj.Website = dr2["Website"].ToString();
                        obj.PAN = dr2["Pancard"].ToString();
                        obj.OfficeEmailId = dr2["OfficeEmailId"].ToString();
                        obj.CompanyLogo = dr2["CompanyLogo"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {


            }

            return obj;

        }
        public PrintInvoice PrintAssociateInvoiceDetail(string PkOrderId,long fk_memid)
        {
            PrintInvoice obj = new PrintInvoice();
            InvoiceDetail obj1 = new InvoiceDetail();
            List<InvoiceItemList> lst = new List<InvoiceItemList>();
            try
            {
                SqlParameter[] param =
                {


                    new SqlParameter("@orderId",PkOrderId),
                    new SqlParameter("@MemberId",fk_memid)

                };
                DataSet ds = DBHelper.ExecuteQuery("PrintInvoice", param);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr1 in ds.Tables[0].Rows)
                    {

                        obj1.LoginId = dr1["LoginId"].ToString();
                        obj1.Name = dr1["Name"].ToString();
                        obj1.OrderNo = dr1["OrderNo"].ToString();
                        obj1.InvoiceDate = dr1["InvoiceDate"].ToString();
                        obj1.Mobile = dr1["Mobile"].ToString();
                        obj1.State = dr1["State"].ToString();
                        obj1.Address = dr1["Address1"].ToString();
                        obj1.InWordBV = dr1["InWordBV"].ToString();
                        obj1.InWordDP = dr1["InWordDP"].ToString();
                        obj1.EmployeeId = dr1["EmployeeId"].ToString();

                    }

                }
                obj.OrderDetails = obj1;

                if (ds != null && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {

                        lst.Add(new InvoiceItemList
                        {

                            BV = dr["BV"].ToString(),
                            DP = dr["DP"].ToString(),
                            InvoiceDate = dr["CreatedDate"].ToString(),
                            MRP = dr["Amount"].ToString(),
                            ProductName = dr["ProductName"].ToString(),
                            Qty = dr["Quatity"].ToString(),
                            SingleBV = dr["SingleBV"].ToString(),
                            SingleDP = dr["SingleDP"].ToString(),
                            GSTAmount = Convert.ToDecimal(dr["GSTAmount"].ToString()),
                            TaxableAmount = Convert.ToDecimal(dr["TaxableAmount"].ToString()),
                            GSTPer = Convert.ToDecimal(dr["GSTPer"].ToString())


                        });


                    }

                    obj.ItemList = lst;
                }
            }
            catch (Exception ex)
            {


            }

            return obj;

        }

    }


    public class DeleteOrder
    {

        public string OrderNo { get; set; }
        public string Remark { get; set; }
        public string Code { get; set; }


    }

    public class OrderList
    {
        public Common.Pager Pager;

        public string  IGST { get; set; }                          

        public string LoginId { get; set; }
        public string Name { get; set; }
        public string OrderNo { get; set; }
        public string TotalAmount { get; set; }
        public string Status { get; set; }
        public string CreationDate { get; set; }
        public string BV { get; set; }
        public string DP { get; set; }
        public string TotalQty { get; set; }
        public string pk_orderid { get; set; }
        public string fk_memId { get; set; }
        public string Remark { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string OrderDate { get; set; }
        public string RepurchaseStatus { get; set; }
        public string UpgradeStatus { get; set; }
        public string HSNCode { get;  set; }
        public string PlaceOfSupply { get; set; }
        public string CGST { get;  set; }
        public string SGST { get; set; }
        public string TotalAmt { get; set; }
        public string TaxableAmount { get; set; }
        public string TaxAmt { get; set; }
        public int? Page { get; set; }
        public string TotalRecords { get; set; }
        public int Size { get; set; }
        public int IsExport { get; set; }
       // public Pager Pager { get; set; }
        public string StateCode { get; set; }

      public List<OrderList> orderLists { get; set; }
    }

    public class AdminOorrderBilling
    {
       
        public AdminOrder Orderbiilling(AdminOrder moodel)
        {
            AdminOrder obj = new AdminOrder();
            DataTable dt = ToDataTable(moodel.OrderDetails);


            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Fk_MemID", moodel.Fk_MemId),
                     new SqlParameter("@TransactionNo", moodel.TransactionNo),
                     new SqlParameter("@TotalAmount", moodel.TotalAmount),
                      new SqlParameter("@Amount", moodel.Amount),
                     new SqlParameter("@PaymentMode", moodel.Paymentmode),
                      new SqlParameter("@BankName", moodel.BankName),
                      new SqlParameter("@ReciptImage", moodel.ReceiptImage),
                       new SqlParameter("@xmldata", dt),
                        new SqlParameter("@ShippingAddress", moodel.ShippingAddress),
                        new SqlParameter("@BillingAddress", moodel.BillingAddress),
                          new SqlParameter("@Remark", moodel.Remark),
                            new SqlParameter("@createdby", moodel.CreatedBy),
                               new SqlParameter("@OrderType", moodel.OrderType),
                            new SqlParameter("@PackageId", moodel.PackageId)

                };

                //DataSet ds = DBHelper.ExecuteQuery("AdminOrder", param);
                DataSet ds = DBHelper.ExecuteQuery("AdminOrder", param);
                if(ds !=null && ds.Tables[0].Rows.Count>0 && ds.Tables[0].Rows[0]["Flag"].ToString()=="1")
                {
                    obj.Code = "1";
                    obj.Message = "Order successfully Done";

                }
                else
                {
                    obj.Code = "0";
                    obj.Message = ds.Tables[0].Rows[0]["Msg"].ToString();
                }
               

            }
            catch (Exception ex)
            {



            }

            return obj;


        }


        public AdminOrder UpgradePackage(AdminOrder moodel)
        {
            AdminOrder obj = new AdminOrder();
         


            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Fk_MemID", moodel.Fk_MemId),
                     new SqlParameter("@createdby", moodel.CreatedBy),
                   new SqlParameter("@PackageId", moodel.PackageId)

                };


                DataSet ds = DBHelper.ExecuteQuery("UpgradePackage", param);
                if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                {
                    obj.Code = "1";
                    obj.Message =ds.Tables[0].Rows[0]["Msg"].ToString();

                }
                else
                {
                    obj.Code = "0";
                    obj.Message = ds.Tables[0].Rows[0]["Msg"].ToString();
                }
               

            }
            catch (Exception ex)
            {



            }

            return obj;


        }


        public List<OrderList> OrderList(OrderList model)
        {
            List<OrderList> lst = new List<OrderList>();
            try
            {
                SqlParameter[] param =
                                {

                    new SqlParameter("@LoginId",string.IsNullOrEmpty(model.LoginId)?null:model.LoginId),
                     new SqlParameter("@FromDate",string.IsNullOrEmpty(model.FromDate)?null:model.FromDate),
                      new SqlParameter("@ToDate",string.IsNullOrEmpty(model.ToDate)?null:model.ToDate),
                      new SqlParameter("@Page",model.Page),
                      new SqlParameter("@Size",model.Size),
                      new SqlParameter("@IsExport",model.IsExport),

                };
                DataSet ds = DBHelper.ExecuteQuery("OrderList", param);
                if(ds !=null && ds.Tables[0]!=null && ds.Tables[0].Rows.Count>0)
                {
                    foreach(DataRow  dr in ds.Tables[0].Rows)
                    {
                        lst.Add(new AEntity.OrderList { 
                        
                        LoginId=dr["LoginId"].ToString(),
                            Name = dr["FirstName"].ToString(),
                            OrderNo = dr["OrderNo"].ToString(),
                            BV = dr["BV"].ToString(),
                            DP = dr["DP"].ToString(),
                            TotalAmount = dr["TotalAmount"].ToString(),
                            Status = dr["Status"].ToString(),
                            CreationDate = dr["CreatedDate"].ToString(),
                            TotalQty = dr["TotalQty"].ToString(),
                            pk_orderid=dr["pk_orderid"].ToString(),
                            Remark = dr["Remark"].ToString(),
                            UpgradeStatus = dr["UpgradeStatus"].ToString(),
                            RepurchaseStatus = dr["RepurchaseStatus"].ToString(),
                            TotalRecords = dr["TotalRecords"].ToString()
                        });


                    }


                }
            }
            catch(Exception ex)
            {



            }
            return lst;


        }
        public DeleteOrder DeleteOrder(DeleteOrder model)
        {
            DeleteOrder lst = new DeleteOrder();
            try
            {
                SqlParameter[] param =
                                {

                    new SqlParameter("@Pk_OrderId",model.OrderNo)

                };
                DataSet ds = DBHelper.ExecuteQuery("DeleteOrderInvoice", param);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Code"].ToString()=="1")
                {
                    lst.Code = ds.Tables[0].Rows[0]["Code"].ToString();
                    lst.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
                else
                {
                    lst.Code = ds.Tables[0].Rows[0]["Code"].ToString();
                    lst.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();

                }
            }
            catch (Exception ex)
            {



            }
            return lst;


        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

    }
}