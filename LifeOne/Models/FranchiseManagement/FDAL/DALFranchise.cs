using Dapper;
using LifeOne.Models.Common;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.Manager;
using LifeOne.Models.QUtility.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Models.FranchiseManagement.FDAL
{
    public class DALFranchise
    {
        #region Franchise to Admin
        public MFranchiseorderRequest GetProductDetails(MFranchiseorderRequest _param)
        {
            try
            {
                string _qury;
                if (SessionManager.Usertype == "3")//For Admin
                {
                    _qury = "Proc_GetProductDetail @ProductId=" + _param.PrdId + ",@Fk_MemId=" + SessionManager.Fk_MemId + ",@Type=1";
                }
                else if(SessionManager.Usertype == "1") //for Associate
                {
                    _qury = "Proc_GetProductDetail @ProductId=" + _param.PrdId + ",@Fk_MemId=" + SessionManager.AssociateFk_MemId + ",@Type=1";
                }
                else ///For Franchisee
                {
                    _qury = "Proc_GetProductDetail @ProductId=" + _param.PrdId + ",@Fk_MemId=" + SessionManager.FranchiseFk_MemId + ",@Type=2";
                }
                
                MFranchiseorderRequest _iresult = DBHelperDapper.DAGetDetailsInList<MFranchiseorderRequest>(_qury).FirstOrDefault();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public MFranchiseorderRequest GetProductDetailsForAPI(MFranchiseorderRequest _param)
        {
            try
            {
                string _qury;
                _qury = "Proc_GetProductDetail @ProductId=" + _param.PrdId + ",@Fk_MemId=" + _param.FranchiseId + ",@Type=2";
                MFranchiseorderRequest _iresult = DBHelperDapper.DAGetDetailsInList<MFranchiseorderRequest>(_qury).FirstOrDefault();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public MFranchiseorderRequest GetAdminProductDetails(MFranchiseorderRequest _param)
        {
            try
            {
                string _qury;
                if (SessionManager.Usertype == "3")
                {
                    _qury = "Proc_GetProductDetail @ProductId=" + _param.PrdId + ",@Fk_MemId=" + SessionManager.Fk_MemId + ",@Type=1";
                }
                else
                {
                    _qury = "Proc_GetProductDetail @ProductId=" + _param.PrdId + ",@Fk_MemId=" + SessionManager.Fk_MemId + ",@Type=2";
                }
                MFranchiseorderRequest _iresult = DBHelperDapper.DAGetDetailsInList<MFranchiseorderRequest>(_qury).FirstOrDefault();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public  List<AdminProductDetail> AdminProductDetails(MFranchiseorderRequest _param)
        {
            try
            {
                string _qury;
                _qury = "AdminProductDetail @ProductId=" + _param.PrdId + "";
                List<AdminProductDetail> _iresult = DBHelperDapper.DAGetDetailsInList<AdminProductDetail>(_qury);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public MFranchiseorderRequest GetProductDetailsAssociates(MFranchiseorderRequest _param)
        {
            try
            {
                string _qury;
                if (SessionManager.Usertype == "3")
                {
                    _qury = "Proc_GetProductDetail @ProductId=" + _param.PrdId + ",@Fk_MemId=" + SessionManager.AssociateFk_MemId + ",@Type=1";
                }
                else
                {
                    _qury = "Proc_GetProductDetail @ProductId=" + _param.PrdId + ",@Fk_MemId=" + SessionManager.AssociateFk_MemId + ",@Type=2";
                }
                MFranchiseorderRequest _iresult = DBHelperDapper.DAGetDetailsInList<MFranchiseorderRequest>(_qury).FirstOrDefault();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public static MSimpleString AddProduct(MFranchiseorderRequest _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@RequestId", _param.ReqId);
                queryParameters.Add("@ProductId", _param.PrdId);
                queryParameters.Add("@PrdTypeId", _param.CategoryId);
                queryParameters.Add("@Amount", _param.Amount);
                queryParameters.Add("@PointValue", _param.PointValue);
                queryParameters.Add("@Quantity", _param.Quantity);
                queryParameters.Add("@TotalAmount", _param.TotalAmt);
                queryParameters.Add("@Fk_MemId", _param.Fk_Memid);
                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("Proc_InsertOrderTemp", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MOrder> BindStockHistory(long PrdId)
        {
            try
            {
                string _qury = "Proc_GetStockHistory @PrdId=" + PrdId + ",@Type=2,@Fk_MemId=" + SessionManager.FranchiseFk_MemId + "";
                List<MOrder> _iresult = DBHelperDapper.DAGetDetailsInList<MOrder>(_qury).ToList();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MFCoutomer BindCustomerDetails(MFCoutomer obj)
        {
            try
            {
                string _qury = "Proc_GetCustomerDetails @Mobile=" + obj.CustomerMobile + "";
                MFCoutomer _iresult = DBHelperDapper.DAGetDetailsInList<MFCoutomer>(_qury).FirstOrDefault();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public MFCoutomer UpgradePackageCustomerDetails(MFCoutomer obj)
        {
            MFCoutomer model = new MFCoutomer();
            List<SelectListItem> lst = new List<SelectListItem>();
            try
            {
                SqlParameter[] param =
                {

                    new SqlParameter("@Mobile",obj.CustomerMobile)

                };
                DataSet ds = DBHelper.ExecuteQuery("CheckCustomerDetailforUpgradePackage", param);
                if(ds !=null && ds.Tables[0] !=null && ds.Tables[0].Rows.Count>0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        model.CustomerName = dr["CustomerName"].ToString();
                        model.CustomerId =Convert.ToInt32(dr["CustomerId"].ToString());
                        model.CustomerMail = dr["CustomerMail"].ToString();
                        model.CustomerMobile = dr["CustomerMobile"].ToString();
                        model.FK_ProductId = dr["Fk_ProductId"].ToString();

                        model.PackageName = dr["PackageName"].ToString();
                        model.PackageAmount =Convert.ToDecimal( dr["PackageAmount"].ToString());
                        model.BP = Convert.ToDecimal(dr["BP"].ToString());
                        model.Code=Convert.ToInt32(dr["Code"].ToString());

                    }

                }
                if (ds != null && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                { 
                foreach(DataRow dr in ds.Tables[1].Rows)
                    {

                        lst.Add(new SelectListItem
                        {
                            Text=dr["Text"].ToString(),
                            Value=dr["Value"].ToString()

                        });

                    }
                
                }
                model.PackageList = lst;


                //    string _qury = "CheckCustomerDetailforUpgradePackage @Mobile=" + obj.CustomerMobile + "";
                //MFCoutomer _iresult = DBHelperDapper.DAGetDetailsInList<MFCoutomer>(_qury).FirstOrDefault();
                //return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return model;
        }
        public static List<MFranchiseItemStock> MyStockDetails(MFranchiseItemStock data)
        {
            try
            {
                string _qry = "GetFAvailableStockReport  @ProcId=0,@Fk_MemId=" + data.FranchiseId + ",@Page=" + data.Page + ",@Size=" + data.Size + "";
                List<MFranchiseItemStock> _iresult = DBHelperDapper.DAGetDetailsInList<MFranchiseItemStock>(_qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MOrder> DALGetOrderRequestItemDetail(long orderId)
        {
            try
            {
                string _qry = "Proc_GetOrderRequestAdmin @ProcId=2,@ReqId=" + orderId + "";
                List<MOrder> _iresult = DBHelperDapper.DAGetDetailsInList<MOrder>(_qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static List<MOrder> DALGetOrderRequestItemDetailFranchiseId(int FranchiseId, long orderId)
        {
            try
            {
                string _qry = "Proc_GetOrderRequestAdmin @ProcId=7,@FranchiseId=" + FranchiseId + ",@ReqId=" + orderId + "";
                List<MOrder> _iresult = DBHelperDapper.DAGetDetailsInList<MOrder>(_qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static object BindUpdatedFigureAmtCustomer(string ReqId, long PK_KeyId, int Quantity, int TotalPv, decimal TotalAmt, int PrdId, int reqQty)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@PK_KeyId", PK_KeyId);
                queryParameters.Add("@RequestId", ReqId);
                queryParameters.Add("@Qty", Quantity);
                queryParameters.Add("@TotalPv", TotalPv);
                queryParameters.Add("@TotalAmt", TotalAmt);
                queryParameters.Add("@Fk_MemId", SessionManager.FranchiseFk_MemId);
                queryParameters.Add("@ProductId", PrdId);
                queryParameters.Add("@reqQty", reqQty);

                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("Proc_CustomerUpdateOrderQtyTemp", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MOrder> GetAllRequestdOrderList(MOrder _param)
        {
            try
            {
                //string _qury = "Proc_FRequestdOrderList_Demo @Fk_FId="+_param.Fk_MemId+ ",@Page="+_param.Page+ ",@Size="+_param.Size+"";
                //var _iresult = DBHelperDapper.DAGetDetailsInList<MOrder>(_qury).ToList();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Fk_FId", _param.Fk_MemId);
                queryParameters.Add("@OrderNo", _param.OrderNo);
                //queryParameters.Add("@DateSearch", _param.ApproveDate);
                queryParameters.Add("@FromDate", _param.FromDate);
                queryParameters.Add("@ToDate", _param.ToDate);
                queryParameters.Add("@Page", _param.Page);
                queryParameters.Add("@Size", _param.Size == 0 ? SessionManager.Size : _param.Size);
                List<MOrder> _iresult = DBHelperDapper.DAAddAndReturnModelList<MOrder>("Proc_FRequestdOrderList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MFranchiseorderRequest> GetOrderTemp(MSimpleString _param)
        {
            try
            {
                string _qry = "Proc_GetOrderTemp " + _param.RequestId + "";
                List<MFranchiseorderRequest> _iresult = DBHelperDapper.DAGetDetailsInList<MFranchiseorderRequest>(_qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MSimpleString DeleteProductTemp(MFranchiseorderRequest _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@PK_KeyId", _param.PK_KeyId);
                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("Proc_DeleteOrderTemp", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MSimpleString DeleteProductTempAPI(MFranchiseorderRequest _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@PK_KeyId", _param.PK_KeyId);
                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("Proc_DeleteOrderTempAPI", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        
        public static MSimpleString SaveProduct(MFranchiseorderRequest _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@RequestId", _param.ReqId);
                queryParameters.Add("@FranchiseId", _param.FranchiseId);
                queryParameters.Add("@PaidAmt", _param.TotalItemAmt);
                queryParameters.Add("@TransactionId", _param.ChequeDDNo);
                queryParameters.Add("@PaymentDate", _param.ChequeDDDate);
                queryParameters.Add("@BankName", _param.BankName);
                queryParameters.Add("@ImagePath", _param.HdnPaymentSlip);
                queryParameters.Add("@Remark", _param.PaymentRemark);
                queryParameters.Add("@paymentMode", _param.PaymentMode);

                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("Proc_InsertOrderRequestFinal", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static MSimpleString SaveProductKaryon(MFranchiseorderRequest _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@RequestId", _param.ReqId);
                queryParameters.Add("@FranchiseId", _param.FranchiseId);
                queryParameters.Add("@PaidAmt", _param.TotalItemAmt);
                queryParameters.Add("@TransactionId", _param.ChequeDDNo);
                queryParameters.Add("@PaymentDate", _param.ChequeDDDate);
                queryParameters.Add("@BankName", _param.BankName);
                queryParameters.Add("@ImagePath", _param.HdnPaymentSlip);
                queryParameters.Add("@Remark", _param.PaymentRemark);
                queryParameters.Add("@paymentMode", _param.PaymentMode);

                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("Proc_InsertOrderRequestFinalKaryon", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static MSimpleString DALSaveAssociateProduct(MFranchiseorderRequest _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@RequestId", _param.ReqId);
                queryParameters.Add("@FranchiseId", _param.FranchiseId);
                queryParameters.Add("@PaidAmt", _param.TotalItemAmt);
                queryParameters.Add("@TransactionId", _param.ChequeDDNo);
                queryParameters.Add("@PaymentDate", _param.ChequeDDDate);
                queryParameters.Add("@BankName", _param.BankName);
                queryParameters.Add("@ImagePath", _param.HdnPaymentSlip);
                queryParameters.Add("@Remark", _param.PaymentRemark);
                queryParameters.Add("@paymentMode", _param.PaymentMode);
                queryParameters.Add("@XmlData", _param.XmlData);
                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("Proc_AssociateInsertOrderRequestFinal", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static MFranchiseorderRequest CheckProduct(string prdId)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@PrdId", prdId);

                MFranchiseorderRequest _iresult = DBHelperDapper.DAAddAndReturnModel<MFranchiseorderRequest>("Proc_GetProductStatus", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static object BindUpdatedFigureAmt(string ReqId, long PK_KeyId, int Quantity, int TotalPv, decimal TotalAmt)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@PK_KeyId", PK_KeyId);
                queryParameters.Add("@RequestId", ReqId);
                queryParameters.Add("@Qty", Quantity);
                queryParameters.Add("@TotalPv", TotalPv);
                queryParameters.Add("@TotalAmt", TotalAmt);


                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("Proc_UpdateOrderQtyTemp", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion region

        #region Franchise to Customer

        public static List<MOrder> GetAllSuppliedOrderList(MOrder _param)
        {
            try
            {
                string LoginId = "";
                if(_param.LoginID == "" || _param.LoginID==null)
                {
                    LoginId = "NA";
                }
                else
                {
                    LoginId = _param.LoginID;
                }
                string _qury = string.Empty;
                List<MOrder> _iresult = null;
                var queryParameters = new DynamicParameters();
                if (Convert.ToInt32(_param.PKFranchiseRegistrationId) > 0)
                {
                     //_qury = "Proc_FSupplyOrderList @Fk_FId=" + _param.PKFranchiseRegistrationId + ",@Page=" + _param.Page + ",@Size=" + _param.Size+ ",@LoginId=" + LoginId +"";


                   
                    queryParameters.Add("@Fk_FId", _param.PKFranchiseRegistrationId);
                    queryParameters.Add("@Page", _param.Page);
                    queryParameters.Add("@Size", SessionManager.Size);
                    queryParameters.Add("@LoginId", LoginId);
                    queryParameters.Add("@Fromdate", string.IsNullOrEmpty(_param.FromDate) ? null : _param.FromDate);
                    queryParameters.Add("@Todate", string.IsNullOrEmpty(_param.ToDate) ? null : _param.ToDate);
                    queryParameters.Add("@PaymentMode", _param.PayMode);
                    queryParameters.Add("@IsExport", _param.IsExport);
                   _iresult = DBHelperDapper.DAAddAndReturnModelList<MOrder>("Proc_FSupplyOrderList", queryParameters);

                  }
                  else
                {

                    queryParameters.Add("@Fk_FId", _param.Fk_MemId);
                    queryParameters.Add("@Page", _param.Page);
                    queryParameters.Add("@Size", SessionManager.Size);
                    queryParameters.Add("@LoginId", LoginId);
                    queryParameters.Add("@Fromdate", string.IsNullOrEmpty(_param.FromDate) ? null : _param.FromDate);
                    queryParameters.Add("@Todate", string.IsNullOrEmpty(_param.ToDate) ? null : _param.ToDate);
                    queryParameters.Add("@PaymentMode", _param.PayMode);
                    _iresult = DBHelperDapper.DAAddAndReturnModelList<MOrder>("Proc_FSupplyOrderList", queryParameters);


                    //_qury = "Proc_FSupplyOrderList @Fk_FId=" + _param.Fk_MemId + ",@Page=" + _param.Page + ",@Size=" + _param.Size +  ",@LoginId=" + LoginId +"";
                }
                //var _iresult = DBHelperDapper.DAGetDetailsInList<MOrder>(_qury).ToList();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MOrder> DALGetOrderSupplyItemDetail(long orderId)
        {
            try
            {
                string _qry = "Proc_GetOrderRequestAdmin @ProcId=4,@ReqId=" + orderId + "";
                List<MOrder> _iresult = DBHelperDapper.DAGetDetailsInList<MOrder>(_qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<MFranchiseorderRequest> GetFCustomerOrderTemp(MSimpleString _param)
        {
            try
            {
                string _qry = "Proc_GetCustomerOrderTemp @RequestId =" + _param.RequestId + ",@CustomerId=" + _param.CustomerId + "";
                List<MFranchiseorderRequest> _iresult = DBHelperDapper.DAGetDetailsInList<MFranchiseorderRequest>(_qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static MSimpleString DeleteFCustomerProductTemp(MFranchiseorderRequest _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@PK_KeyId", _param.PK_KeyId);
                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("Proc_DeleteCustomerOrderTemp", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MSimpleString AddFCustomerProduct(MFranchiseorderRequest _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@RequestId", _param.ReqId);
                queryParameters.Add("@ProductId", _param.PrdId);
                queryParameters.Add("@PrdTypeId", _param.CategoryId);
                queryParameters.Add("@Amount", _param.Amount);
                queryParameters.Add("@PointValue", _param.PointValue);
                queryParameters.Add("@Quantity", _param.Quantity);
                queryParameters.Add("@TotalAmount", _param.TotalAmt);
                queryParameters.Add("@DiscountAmt", _param.DiscountAmt);
                queryParameters.Add("@TotalPv ", _param.TotalPv);
                queryParameters.Add("@TotalStock ", _param.AvailableStock);
                queryParameters.Add("@Fk_MemId", _param.FranchiseId);
                queryParameters.Add("@IsBox ", _param.IsBox);
                queryParameters.Add("@BoxQty ", _param.BoxQty);
                queryParameters.Add("@BoxPV", _param.BoxPV);
                queryParameters.Add("@ReqBoxQty", _param.ReqBoxQty);
                queryParameters.Add("@CustomerId", _param.CustomerId);
                queryParameters.Add("@IsFree", _param.IsFree);

                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("Proc_InsertCustomerOrderTemp", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MSimpleString SaveFCustomerProduct(MFranchiseorderRequest _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@RequestId", _param.ReqId);
                queryParameters.Add("@CustomerId", _param.CustomerId);
                queryParameters.Add("@FranchiseId", _param.FranchiseId);
                queryParameters.Add("@PaidAmt", _param.TotalItemAmt);
                queryParameters.Add("@DueAmt", _param.DueAmt);
                queryParameters.Add("@TransactionId", _param.ChequeDDNo);
                queryParameters.Add("@PaymentDate", _param.ChequeDDDate);
                queryParameters.Add("@BankName", _param.BankName);
                queryParameters.Add("@ImagePath", _param.HdnPaymentSlip);
                queryParameters.Add("@Remark", _param.PaymentRemark);
                queryParameters.Add("@paymentMode", _param.PaymentMode);
                queryParameters.Add("@IsFree", _param.IsFree);


                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("Proc_InsertCustomerOrderRequestFinal", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion region

        public static MSimpleString DALReceiveFranchiseOrder(long FK_ReqId)
        {
            try
            {
                string _qry = "PROC_ReceiveFranchiseOrder @FK_ReqId=" + FK_ReqId + ",@FK_MemId=" + SessionManager.FranchiseFk_MemId + "";
                MSimpleString _iresult = DBHelperDapper.DAGetDetails<MSimpleString>(_qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static MOrderModel FranchisePersonalDetails(string FK_MemId)
        {
            try
            {
                string _qury = "ProGetFranchiseDetailsByLoginId @FranchiseId=" + FK_MemId + "";
                LifeOne.Models.Common.MOrderModel _iresult = DBHelperDapper.DAGetDetailsInList<LifeOne.Models.Common.MOrderModel>(_qury).FirstOrDefault();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MFCoutomer BindCustomerDetailByMobileNO(MFCoutomer obj)
        {
            try
            {
                string _qury = "Proc_CustomerDetailsByMobileNo @Mobile=" + obj.CustomerMobile + "";
                MFCoutomer _iresult = DBHelperDapper.DAGetDetailsInList<MFCoutomer>(_qury).FirstOrDefault();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MFCoutomer> PerfomanceMasterList()
        {
            try
            {
                string _qury = "Proc_PerfomanceMaster";
                List<MFCoutomer> _iresult = DBHelperDapper.DAGetDetailsInList<MFCoutomer>(_qury).ToList();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MSimpleString UpdatePerformanceLevel(MSimpleString _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@CustomerId", _param.CustomerId);
                queryParameters.Add("@PerformanceLevelID", _param.PerformanceLevelID);
                queryParameters.Add("@Fk_MemId", SessionManager.Fk_MemId);
                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("Proc_UpdatePerformanceLevel", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MOrder BindOrderReport(DateTime? FromDate, DateTime? ToDate, int OrderType, int? Page, int OrderNo)
        {
            try
            {
                MOrder _iresult = new MOrder();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FromDate", FromDate);
                queryParameters.Add("@ToDate", ToDate);
                queryParameters.Add("@OrderNo", OrderNo);
                queryParameters.Add("@OrderType", OrderType);
                _iresult.LstOrder = DBHelperDapper.DAAddAndReturnModelList<MOrder>("ProGetOnlineOrderDetails", queryParameters);
                int totalRecords = 0;
                if (_iresult.LstOrder != null && _iresult.LstOrder.Count() > 0)
                {
                    totalRecords = Convert.ToInt32(_iresult.LstOrder[0].TotalRecords);
                    var pager = new Common.Pager(totalRecords, Page, SessionManager.Size);
                    _iresult.Pager = pager;
                }
                else
                {
                    _iresult.LstOrder = null;
                }
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static List<OrderProductDetailsModel> GetOrderDetails(int OrderNo, int OrderType)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@OrderNo", OrderNo);
                queryParameters.Add("@OrderType", OrderType);
                List<OrderProductDetailsModel> _result = DBHelperDapper.DAAddAndReturnModelList<OrderProductDetailsModel>("ProGetProductDetails", queryParameters);                
                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        public static List<MOrder> Get_Current_SuppliedOrderList(MOrder _param)
        {
            try
            {
                string _qury = string.Empty;
                if (Convert.ToInt32(_param.PKFranchiseRegistrationId) > 0)
                {
                    _qury = "Proc_Current_FSupplyOrder @Fk_FId=" + _param.PKFranchiseRegistrationId + ",@FBSId=" + _param.PK_RId;
                }
                else
                {
                    _qury = "Proc_Current_FSupplyOrder @Fk_FId=" + _param.Fk_MemId + ",@FBSId=" + _param.PK_RId;
                }
                var _iresult = DBHelperDapper.DAGetDetailsInList<MOrder>(_qury).ToList();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MOrder DeleteCustomerOrder(MOrder _model)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@OrderNo", _model.OrderNo);
                queryParameters.Add("@CreatedBy", SessionManager.Fk_MemId);
                var data = DBHelperDapper.DAAddAndReturnModel<MOrder>("DeleteCustomerOrder", queryParameters);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        public static MOrder CancelPackageSale(MOrder _model)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Fk_InvId", _model.Fk_InvId);
                queryParameters.Add("@FK_MemID", _model.Fk_MemId);
                queryParameters.Add("@CreatedBy", SessionManager.Fk_MemId);
                var data = DBHelperDapper.DAAddAndReturnModel<MOrder>("CancelPackageSale", queryParameters);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static MSimpleString TempManageOrderbyAdmin(MFranchiseorderRequest _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                
                queryParameters.Add("@OpCode", _param.OpCode);
                queryParameters.Add("@ProductId", _param.PrdId);
                queryParameters.Add("@Amount", _param.Amount);
                queryParameters.Add("@BV", _param.PointValue);
                queryParameters.Add("@Quantity", _param.Quantity);
                queryParameters.Add("@TotalAmount", _param.TotalAmt);
                queryParameters.Add("@TotalBV ", _param.TotalPv);
                queryParameters.Add("@TotalStock ", _param.AvailableStock);
                queryParameters.Add("@Fk_MemId", _param.Fk_Memid);

                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("TempOrderByAdmin", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public static List<MFranchiseorderRequest> GetCustomerOrderTemp(string OpCode )
        {
            try
            {
                string _qry = "TempOrderByAdmin "+ "@OpCode=" + OpCode + "";
                List<MFranchiseorderRequest> _iresult = DBHelperDapper.DAGetDetailsInList<MFranchiseorderRequest>(_qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static MSimpleString SaveCustomerProduct(MFranchiseorderRequest _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
              
                queryParameters.Add("@TransactionId", _param.ChequeDDNo);
                queryParameters.Add("@PaymentDate", _param.ChequeDDDate);
                queryParameters.Add("@BankName", _param.BankName);
                queryParameters.Add("@ImagePath", _param.HdnPaymentSlip);
                queryParameters.Add("@Remark", _param.PaymentRemark);
                queryParameters.Add("@paymentMode", _param.PaymentMode);
                queryParameters.Add("@Name", _param.CustomerName);
                queryParameters.Add("@MobileNo", _param.CustomerMobile);
                queryParameters.Add("@EmailId", _param.CustomerEmail);
                queryParameters.Add("@PinCode", _param.CustomerPinCode);
                queryParameters.Add("@Address", _param.CustomerAddress);
                queryParameters.Add("@LoginId", _param.CustomerLoginId);
                queryParameters.Add("@AddedBy", _param.Fk_Memid);
                queryParameters.Add("@PaidAmount", _param.PaidAmount);
                queryParameters.Add("@GSTNo", _param.CustomerGstNo);
                queryParameters.Add("@UpiNumber", _param.UpiNumber);


                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("CustomerOrderByAdmin", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static MSimpleString DeleteCustomerProductTemp(MFranchiseorderRequest _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Pk_Id", _param.PK_KeyId);
                queryParameters.Add("@Fk_MemId", _param.Fk_Memid);
                queryParameters.Add("@OpCode", _param.OpCode);
                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("TempOrderByAdmin", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<MOrder> GetOpenOrderDetail(long orderId)
        {
            try
            {
                string _qry = "GetOpenOrderList @OpCode=2,@Fk_MemId=" + SessionManager.Fk_MemId + ",@Pk_orderId=" + orderId + "";
                List<MOrder> _iresult = DBHelperDapper.DAGetDetailsInList<MOrder>(_qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}