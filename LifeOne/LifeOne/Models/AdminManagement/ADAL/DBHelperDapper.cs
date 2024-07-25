using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.FranchiseManagement.FEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DBHelperDapper
    {

        private static string connectionString = string.Empty;
        public static string connection()
        {
            try
            {
                return connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            }
            catch (Exception)
            {
                //todo error handling  mechanism
                throw;
            }
        }

        /*getting details fro datatbase list*/
        public static List<TClass> DAGetDetailsInList<TClass>(string _qry)
        {
            using (SqlConnection con = new SqlConnection(connection()))
            {
                try
                {
                    IList<TClass> myList = SqlMapper.Query<TClass>(con, _qry).ToList();
                    return myList.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public static TClass DASave<TClass>(string _qry)
        {
            using (SqlConnection con = new SqlConnection(connection()))
            {
                try
                {
                    TClass myList = SqlMapper.Query<TClass>(con, _qry).FirstOrDefault();
                    return myList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static TClass DAGetDetails<TClass>(string _qry)
        {
            using (SqlConnection con = new SqlConnection(connection()))
            {
                try
                {
                    TClass myList = SqlMapper.Query<TClass>(con, _qry).FirstOrDefault();
                    return myList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /*getting details*/
        //public async Task<List<TClass>> DAGetDetailsInListAsync<TClass>(string _qry)
        //{
        //    using (SqlConnection con = new SqlConnection(connection()))
        //    {
        //        try
        //        {
        //            IList<TClass> myList = SqlMapper.Query<TClass>(con, _qry).ToList();
        //            return await Task.Run(() => myList.ToList());
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}


        public static int DAAdd<T>(string Procname, T param)
        {
            int _iresult = 0;
            using (SqlConnection con = new SqlConnection(connection()))
            {
                try
                {
                    _iresult = con.Execute(Procname, param, commandType: System.Data.CommandType.StoredProcedure);
                    return _iresult;
                }
                catch (Exception ex)
                {
                    return _iresult;
                }
            }
        }

        public static int DAAdd(string Procname, DynamicParameters param)
        {
            int _iresult = 0;
            using (SqlConnection con = new SqlConnection(connection()))
            {
                try
                {
                    _iresult = con.Execute(Procname, param, commandType: System.Data.CommandType.StoredProcedure);
                    return _iresult;
                }
                catch (Exception ex)
                {
                    return _iresult;
                }
            }
        }

        //public static TClass DAAddAndReturnModel<TClass>(string _qry, DynamicParameters param)
        //{
        //    using (SqlConnection con = new SqlConnection(connection()))
        //    {
        //        try
        //        {
        //            TClass myList = SqlMapper.Query<TClass>(con, _qry, param).FirstOrDefault();
        //            return myList;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}
        public static List<TClass> DAReturnList<TClass>(string _qry, DynamicParameters param)
        {
            using (SqlConnection con = new SqlConnection(connection()))
            {
                try
                {
                    List<TClass> myList = SqlMapper.Query<TClass>(con, _qry, param).ToList();
                    return myList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public static TClass DAAddAndReturnModel<TClass>(string _procame, DynamicParameters param)
        {
            TClass _objMOdel;

            try
            {
                using (SqlConnection objConnection = new SqlConnection(connection()))
                {
                    if (objConnection.State == System.Data.ConnectionState.Closed)
                    {
                        objConnection.Open();
                    }
                    _objMOdel = SqlMapper.Query<TClass>(objConnection, _procame, param, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                    if (objConnection.State == System.Data.ConnectionState.Open)
                    { 
                        objConnection.Close();
                    }
                }
                return _objMOdel;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static List<T> DAAddAndReturnModelList<T>(string spName, DynamicParameters p)
        {
            List<T> recordList = new List<T>();
            using (SqlConnection objConnection = new SqlConnection(connection()))
            {
                objConnection.Open();
                recordList = SqlMapper.Query<T>(objConnection, spName, p, commandType: System.Data.CommandType.StoredProcedure).ToList();
                objConnection.Close();
            }
            return recordList;
        }

        public static dynamic GetAssociateTree(string LoginId)
        {
            using (SqlConnection objConnection = new SqlConnection(connection()))
            {
                try
                {
                    TreeRequest objcls = new TreeRequest();
                    objcls.LoginID = LoginId;
                    var reader = objConnection.QueryMultiple("dbo.GenealogyTree", objcls, commandType: CommandType.StoredProcedure);
                    var papersetinformation = reader.Read<TreeModel>().FirstOrDefault();
                    var papetHMLlevel = reader.Read<TreeModel>().ToList();
                    var genericResult = new
                    {
                        _papersetinformation = papersetinformation,
                        _papersetHMLlevel = papetHMLlevel
                    };
                    return genericResult;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static dynamic GetAsscoaiteDashboard(long Fk_MemId)
        {
            using (SqlConnection objConnection = new SqlConnection(connection()))
            {
                try
                {
                    AssociateDashboardReq objcls = new AssociateDashboardReq();
                    objcls.Fk_MemId = Fk_MemId;
                    var reader = objConnection.QueryMultiple("dbo.AssociateDashboard_V2", objcls, commandType: CommandType.StoredProcedure);
                    var AssociateDashBoardData = reader.Read<AssociateDashboardWeb>().FirstOrDefault();
                    var AssociateDetails = reader.Read<AssociateDetails>().ToList();
                    var payoutStatements = reader.Read<PayoutStatement>().ToList();
                    var payoutSummaries = reader.Read<PayoutSummary>().ToList();
                    var NewsDetails = reader.Read<NewsDetails>().ToList();
                    var genericResult = new
                    {
                        _AssociateDashBoardData = AssociateDashBoardData,
                        _AssociateDetails = AssociateDetails,
                        _payoutStatements = payoutStatements,
                        _payoutSummaries=payoutSummaries,
                        _NewsDetails = NewsDetails
                    };
                    return genericResult;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static dynamic GetAsscoaiteDashboardStatistics(long Fk_MemId)
        {
            using (SqlConnection objConnection = new SqlConnection(connection()))
            {
                try
                {
                    AssociateDashboardReq objcls = new AssociateDashboardReq();
                    objcls.Fk_MemId = Fk_MemId;
                    var reader = objConnection.QueryMultiple("dbo.Proc_AssociateDashboard", objcls, commandType: CommandType.StoredProcedure);
                    var AssociateDashBoardData = reader.Read<AssociateDashBoard>().FirstOrDefault();
                    var NewsDetails = reader.Read<NewsDetails>().ToList();
                    var genericResult = new
                    {
                        _AssociateDashBoardData = AssociateDashBoardData,
                        _NewsDetails = NewsDetails
                    };
                    return genericResult;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static dynamic GetUIDAsscoaiteDashboard(long Fk_MemId)
        {
            using (SqlConnection objConnection = new SqlConnection(connection()))
            {
                try
                {
                    AssociateDashboardReq objcls = new AssociateDashboardReq();
                    objcls.Fk_MemId = Fk_MemId;
                    var reader = objConnection.QueryMultiple("dbo.UidDahboard", objcls, commandType: CommandType.StoredProcedure);
                    var AssociateDashBoardData = reader.Read<AssociateDashBoard>().FirstOrDefault();
                    var AssociateDetails = reader.Read<AssociateDetails>().ToList();
                    var NewsDetails = reader.Read<NewsDetails>().ToList();
                    var genericResult = new
                    {
                        _AssociateDashBoardData = AssociateDashBoardData,
                        _AssociateDetails = AssociateDetails,
                        _NewsDetails = NewsDetails
                    };
                    return genericResult;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static dynamic GetAdminDashboard()
        {
            using (SqlConnection objConnection = new SqlConnection(connection()))
            {
                try
                {
                    var reader = objConnection.QueryMultiple("dbo.Proc_AdminDashboard", commandType: CommandType.StoredProcedure);
                    var AdmindashboardData = reader.Read<MAdminDashbord>().FirstOrDefault();
                    var ActiveIncative = reader.Read<ActiveIncative>().FirstOrDefault();
                    var OrderStatus = reader.Read<OrderStatus>().FirstOrDefault();
                    var RepurchaseStatus = reader.Read<RepurchaseStatus>().FirstOrDefault();
                    var BonusStatus = reader.Read<BonusStatus>().FirstOrDefault();
                    var FranchiseOrderStatus = reader.Read<FranchiseOrderStatus>().FirstOrDefault();
                    var MtotalRechargeAndAmountCreated = reader.Read<MtotalRechargeAndAmountCreated>().ToList();
                    var WeeklyMemberKYCModel = reader.Read<WeeklyMemberKYCModel>().ToList();
                    var ManageWalletPayment = reader.Read<ManageWalletPayment>().FirstOrDefault();


                    var genericResult = new
                    {
                        _AdmindashboardData = AdmindashboardData,
                        _ActiveIncative = ActiveIncative,
                        _OrderStatus = OrderStatus,
                        _RepurchaseStatus = RepurchaseStatus,
                        _BonusStatus = BonusStatus,
                        _FranchiseOrderStatus = FranchiseOrderStatus,
                        MtotalRechargeAndAmountCreated = MtotalRechargeAndAmountCreated,
                        WeeklyMemberKYCModel = WeeklyMemberKYCModel,
                        ManageWalletPayment = ManageWalletPayment,
                    };
                    return genericResult;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static dynamic GetMemberOrderInvoice(string OrderId)
        {
            using (SqlConnection objConnection = new SqlConnection(connection()))
            {
                try
                {
                    MemberOrderId objcls = new MemberOrderId();
                    objcls.OrderId = long.Parse(OrderId);
                    var reader = objConnection.QueryMultiple("dbo.Proc_GetMemberOrderInvoice", objcls, commandType: CommandType.StoredProcedure);
                    var MemberDetail = reader.Read<MAssociateAdminInvoice>().FirstOrDefault();
                    var MemberOrderDetail = reader.Read<MemberOrderDetail>().ToList();

                    var genericResult = new
                    {
                        _MemberDetail = MemberDetail,
                        _OrderDetail = MemberOrderDetail,

                    };
                    return genericResult;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static dynamic DALGetShippingInvoice(long FranchiseId, int FK_FbSID)
        {
            using (SqlConnection objConnection = new SqlConnection(connection()))
            {
                try
                {
                    ShippingDetailViewModel model = new ShippingDetailViewModel();
                    ShippingDetailModel objcls = new ShippingDetailModel();
                    objcls.FK_FbSID = FK_FbSID;
                    objcls.FranchiseId = FranchiseId;
                    var reader = objConnection.QueryMultiple("dbo.GetTransferInvoiceDetails", objcls, commandType: CommandType.StoredProcedure);
                    var memberDetail = reader.Read<ShippingDetailViewModel>().FirstOrDefault();
                    var invoiceDetailList = reader.Read<ShippingDetailViewModel>().ToList();
                    model.MemberDetails = memberDetail;
                    model.ShippingDetailList = invoiceDetailList;
                    return model;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static dynamic GetInvocice(string OrderId)
        {
            using (SqlConnection objConnection = new SqlConnection(connection()))
            {
                try
                {
                    MemberOrderId objcls = new MemberOrderId();
                    objcls.OrderId = long.Parse(OrderId);
                    var reader = objConnection.QueryMultiple("Proc_GetMemberOrderInvoiceOnline", objcls, commandType: CommandType.StoredProcedure);
                    var MemberDetail = reader.Read<MInvoice>().FirstOrDefault();
                    var MemberOrderDetail = reader.Read<InvoiceOrderDetails>().ToList();

                    var genericResult = new
                    {
                        _MemberDetail = MemberDetail,
                        _OrderDetail = MemberOrderDetail,

                    };
                    return genericResult;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static dynamic GetInvocice_Karyon(string OrderId)
        {
            using (SqlConnection objConnection = new SqlConnection(connection()))
            {
                try
                {
                    MemberOrderId objcls = new MemberOrderId();
                    objcls.OrderId = long.Parse(OrderId);
                    var reader = objConnection.QueryMultiple("dbo.Proc_GetMemberOrderInvoiceOnline_KaryOn", objcls, commandType: CommandType.StoredProcedure);
                    var MemberDetail = reader.Read<MInvoice>().FirstOrDefault();
                    var MemberOrderDetail = reader.Read<InvoiceOrderDetails>().ToList();

                    var genericResult = new
                    {
                        _MemberDetail = MemberDetail,
                        _OrderDetail = MemberOrderDetail,

                    };
                    return genericResult;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static Support_EmployeeListModel BindEmployeSupportList(string LoginId)
        {
            using (SqlConnection objConnection = new SqlConnection(connection()))
            {
                try
                {
                    Support_EmployeeListModel model = new Support_EmployeeListModel();
                    Support_EmployeeRequestModel objcls = new Support_EmployeeRequestModel();
                    objcls.LoginID = LoginId;
                    var reader = objConnection.QueryMultiple("Proc_EmployeeList", objcls, commandType: CommandType.StoredProcedure);
                    model.EmployeeList = reader.Read<Support_EmployeeListModel>().ToList();
                    model.SupportList = reader.Read<Support>().ToList();
                    return model;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //public static dynamic GetInvocice_Karyon(string OrderId)
        //{
        //    using (SqlConnection objConnection = new SqlConnection(connection()))
        //    {
        //        try
        //        {
        //            MemberOrderId objcls = new MemberOrderId();
        //            objcls.OrderId = long.Parse(OrderId);
        //            var reader = objConnection.QueryMultiple("dbo.Proc_GetMemberOrderInvoiceOnline_KaryOn", objcls, commandType: CommandType.StoredProcedure);
        //            var MemberDetail = reader.Read<MInvoice>().FirstOrDefault();
        //            var MemberOrderDetail = reader.Read<InvoiceOrderDetails>().ToList();
        //            var genericResult = new
        //            {
        //                _MemberDetail = MemberDetail,
        //                _OrderDetail = MemberOrderDetail,

        //            };
        //            return genericResult;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}

        public static dynamic UtilityPaymentList()
        {
            using (SqlConnection objConnection = new SqlConnection(connection()))
            {
                try
                {
                    var reader = objConnection.QueryMultiple("dbo.Proc_UtilityPaymentList", commandType: CommandType.StoredProcedure);
                    var MtotalRechargeAndAmountCreated = reader.Read<MtotalRechargeAndAmountCreated>().ToList();

                    var genericResult = new
                    {
                        MtotalRechargeAndAmountCreated = MtotalRechargeAndAmountCreated
                    };
                    return genericResult;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }




      



    }
}