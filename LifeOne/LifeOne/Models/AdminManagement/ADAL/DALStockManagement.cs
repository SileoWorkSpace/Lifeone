using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.QUtility.DAL;
using Dapper;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.Manager;
using System.Data.SqlClient;
using LifeOne.Models.API.DAL;
using System.Data;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALStockManagement
    {
        public static List<MAdminStockManagement> DALGetStockManagement(MAdminStockManagement obj)
        {
            try
            {
                string _qry = "GetFAvailableStockReport @ProcId="+obj.ProcId+",@Fk_MemId="+SessionManager.Fk_MemId+",@Page = " + obj.Page + ", @Size = " + SessionManager.Size + ",@Fk_FranchiseLoginId='"+obj.LoginId+"'";
                List<MAdminStockManagement> _iresult = DBHelperDapper.DAGetDetailsInList<MAdminStockManagement>(_qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<ProductStockReport> DALGetStockManagementReport(ProductStockReport obj)
        {
            List<ProductStockReport> objlist = new List<ProductStockReport>();
            try
            {
                SqlParameter[] param =
                {
            new SqlParameter("@ProductName",string.IsNullOrEmpty(obj.ProductName)?null:obj.ProductName)

                };
                DataSet ds = DBHelper.ExecuteQuery("GetProductDetails",param);
                if(ds !=null && ds.Tables[0]!=null && ds.Tables[0].Rows.Count>0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {

                        objlist.Add(new ProductStockReport
                        {
                            BalanceQuantity=dr["BalanceQuantity"].ToString(),
                            OrderQuantity=dr["OrderQuantity"].ToString(),
                            Price=dr["price"].ToString(),
                            ProductName=dr["ProductName"].ToString(),
                            ProductQuantity=dr["ProductQuantity"].ToString(),
                            BV = dr["BV"].ToString(),
                            DP = dr["DP"].ToString(),
                            GST= dr["gst"].ToString(),

                        });
                    }


                }
                
                return objlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<MOrder> BindStockHistory(long PrdId, string LoginId , int?page)
        {
            try
            {
                string _qury = "Proc_GetStockHistory @PrdId=" + PrdId + ",@Type=1,@Fk_MemId="+SessionManager.Fk_MemId+ ",@Page=" + page + ",@Size=" + SessionManager.Size + ",@Fk_FranchiseLoginId='" + LoginId + "'";
                List<MOrder> _iresult = DBHelperDapper.DAGetDetailsInList<MOrder>(_qury).ToList();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<ProductStockReport> BindStockHistoryBYProductName(string ProductName)
        {

            List<ProductStockReport> objlist = new List<ProductStockReport>();

            try
            {
                SqlParameter[] param =
                {

                    new SqlParameter("@ProductName",ProductName)

                };

                DataSet ds = DBHelper.ExecuteQuery("ViewOrderHistoryByProductName", param);
                if(ds !=null && ds.Tables[0].Rows.Count>0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        objlist.Add(new ProductStockReport
                        {
                            ProductName=dr["ProductName"].ToString(),
                            OrderQuantity=dr["Quantity"].ToString(),
                            OrderDate=dr["CreatedDate"].ToString(),
                            BV=dr["BV"].ToString(),
                            Price = dr["Amount"].ToString(),
                            LoginId=dr["LoginId"].ToString()

                        });

                    }


                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objlist;
        }
    }
}