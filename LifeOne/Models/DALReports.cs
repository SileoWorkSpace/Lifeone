using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.API.DAL;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models
{
    public class DALReports
    {

        public static List<Reports> GetReports(Reports _param)
        {
            try
            {
                string LoginId = "";
                if (_param.LoginId == "" || _param.LoginId == null)
                {
                    LoginId = null;
                }
                else
                {
                    LoginId = _param.LoginId;
                }
                string _qury = string.Empty;
                List<Reports> _iresult = null;
                var queryParameters = new DynamicParameters();



                queryParameters.Add("@Page",_param.Page);
                queryParameters.Add("@Size",SessionManager.Size);
                queryParameters.Add("@OpCode",_param.OpCode);
                queryParameters.Add("@OrderNo",_param.OrderNo);
                queryParameters.Add("@LoginId",_param.LoginId);
                queryParameters.Add("@FromDate",_param.FromDate);
                queryParameters.Add("@ToDate",_param.ToDate);
                queryParameters.Add("@IsExport",_param.IsExport);


                _iresult = DBHelperDapper.DAAddAndReturnModelList<Reports>("GetGstReport", queryParameters);


              
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public static List<Reports> GetDirectBusiness(Reports _param)
        {
            try
            {
                string LoginId = "";
                if (_param.LoginId == "" || _param.LoginId == null)
                {
                    LoginId = null;
                }
                else
                {
                    LoginId = _param.LoginId;
                }
                string _qury = string.Empty;
                List<Reports> _iresult = null;
                var queryParameters = new DynamicParameters();



                queryParameters.Add("@Page", _param.Page);
                queryParameters.Add("@Size", SessionManager.Size);

                queryParameters.Add("@LoginId", _param.LoginId);
                queryParameters.Add("@FromDate", _param.FromDate);
                queryParameters.Add("@ToDate", _param.ToDate);


                _iresult = DBHelperDapper.DAAddAndReturnModelList<Reports>("GetDirectBusiness", queryParameters);



                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Reports> GetFranchiseSales(Reports _param)
        {
            try
            {
                string LoginId = "";
                if (_param.LoginId == "" || _param.LoginId == null)
                {
                    LoginId = null;
                }
                else
                {
                    LoginId = _param.LoginId;
                }
                string _qury = string.Empty;
                List<Reports> _iresult = null;
                var queryParameters = new DynamicParameters();



                queryParameters.Add("@Page", _param.Page);
                queryParameters.Add("@Size", SessionManager.Size);

                queryParameters.Add("@LoginId", _param.LoginId);
                queryParameters.Add("@Name", _param.Name);
                queryParameters.Add("@Contact", _param.Contact);
                queryParameters.Add("@FromDate", _param.FromDate);
                queryParameters.Add("@ToDate", _param.ToDate);


                _iresult = DBHelperDapper.DAAddAndReturnModelList<Reports>("GetFranchiseSales", queryParameters);



                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static List<Reports> GetFranchiseWalletLedgerForAdmin(Reports _param)
        {
            try
            {
                string LoginId = "";
                if (_param.LoginId == "" || _param.LoginId == null)
                {
                    LoginId = null;
                }
                else
                {
                    LoginId = _param.LoginId;
                }
                string _qury = string.Empty;
                List<Reports> _iresult = null;
                var queryParameters = new DynamicParameters();



                queryParameters.Add("@Page", _param.Page);
                queryParameters.Add("@Size", SessionManager.Size);

                queryParameters.Add("@LoginId", _param.LoginId);
                queryParameters.Add("@FromDate", _param.FromDate);
                queryParameters.Add("@ToDate", _param.ToDate);


                _iresult = DBHelperDapper.DAAddAndReturnModelList<Reports>("GetFranchiseWalletLedgerForAdmin", queryParameters);



                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static List<AdminStock> GetFranchiseStockForAdmin(AdminStock _param)
        {
            try
            {
               
                string _qury = string.Empty;
                List<AdminStock> _iresult = null;
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Page", _param.Page);
                queryParameters.Add("@Size", SessionManager.Size);
                queryParameters.Add("@Pk_ProductId", _param.Pk_ProductId);
                queryParameters.Add("@checkStatus", _param.Checked);
                queryParameters.Add("@Fk_FranchiseId", _param.FranchiseId);
                

                _iresult = DBHelperDapper.DAAddAndReturnModelList<AdminStock>("GetFranchiseStockForAdmin", queryParameters);



                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Reports> GetPackageGstReport(Reports _param)
        {
            try
            {
             
                string _qury = string.Empty;
                List<Reports> _iresult = null;
                var queryParameters = new DynamicParameters();



                queryParameters.Add("@Page", _param.Page);
                queryParameters.Add("@Size", SessionManager.Size);

                queryParameters.Add("@LoginId", _param.LoginId);
                queryParameters.Add("@FromDate", _param.FromDate);
                queryParameters.Add("@ToDate", _param.ToDate);


                _iresult = DBHelperDapper.DAAddAndReturnModelList<Reports>("GetPackageGstReport", queryParameters);



                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
    }

}