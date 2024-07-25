using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LifeOne.Models.BillAvenueUtility.Entity;
using LifeOne.Models.Common;

namespace LifeOne.Models.BillAvenueUtility.DAL
{
    public class DALTransRequestAndResponse
    {
        public DataSet SaveTransRequest(MTransRequest _paramRequest)
        {
            SqlParameter[] para = {
                                      new SqlParameter("@TransReqXml", _paramRequest.TransReqXML),
                                      new SqlParameter("@TransReqEncType", _paramRequest.TransReqEncType),
                                      new SqlParameter("@Url", _paramRequest.Url),
                                      new SqlParameter("@AccessCode", _paramRequest.AccessCode),
                                      new SqlParameter("@RequestId", _paramRequest.RequestId),
                                      new SqlParameter("@BillerId", _paramRequest.BillerId),
                                      new SqlParameter("@EnvId", _paramRequest.EnvId),
                                      new SqlParameter("@InstituteId", _paramRequest.InstituteId),
                                      new SqlParameter("@WorkingKey", _paramRequest.WorkingKey),
                                      new SqlParameter("@IPAdress", _paramRequest.IPAdress),
                                      new SqlParameter("@AndroidId", _paramRequest.AndroidId),
                                      new SqlParameter("@DeviceId", _paramRequest.DeviceId),
                                      new SqlParameter("@CompanyId", _paramRequest.CompanyId),
                                      new SqlParameter("@Fk_MemId", _paramRequest.FK_memId),
                                      new SqlParameter("@CreatedBy", _paramRequest.FK_memId),
                                      new SqlParameter("@ProcId", _paramRequest.ProcId),
                                      new SqlParameter("@APIName", _paramRequest.APIName)

                                 };
            DataSet ds = DBHelper.ExecuteQuery(ProcedureName.SaveTransRequest, para);

            return ds;

        }
        public DataSet SaveTransResponse(MTransRequest _paramRequest)
        {
            SqlParameter[] para = {
                                      new SqlParameter("@TransReqXml", _paramRequest.TransReqXML),
                                      new SqlParameter("@TransReqEncType", _paramRequest.TransReqEncType),
                                      new SqlParameter("@Url", _paramRequest.Url),
                                      new SqlParameter("@AccessCode", _paramRequest.AccessCode),
                                      new SqlParameter("@DeviceId", _paramRequest.DeviceId),
                                      new SqlParameter("@RequestId", _paramRequest.RequestId),
                                      new SqlParameter("@BillerId", _paramRequest.BillerId),
                                      new SqlParameter("@EnvId", _paramRequest.EnvId),
                                      new SqlParameter("@InstituteId", _paramRequest.InstituteId),
                                      new SqlParameter("@WorkingKey", _paramRequest.WorkingKey),
                                      new SqlParameter("@IPAdress", _paramRequest.IPAdress),
                                      new SqlParameter("@AndroidId", _paramRequest.AndroidId),
                                      new SqlParameter("@CompanyId", _paramRequest.CompanyId),
                                      new SqlParameter("@Fk_MemId", _paramRequest.CreatedBy),
                                      new SqlParameter("@CreatedBy", _paramRequest.CreatedBy),
                                      new SqlParameter("@ProcId", _paramRequest.ProcId),
                                      new SqlParameter("@APIName", _paramRequest.APIName)

                                 };
            DataSet ds = DBHelper.ExecuteQuery(ProcedureName.SaveTransRequest, para);

            return ds;
        }
    }
}