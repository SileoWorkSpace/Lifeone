using LifeOne.Models.QUtility.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LifeOne.Models.Common;

namespace LifeOne.Models.QUtility.DAL
{
    public class DALBeneficiary : DBHelper
    {

        public AddBeneficiaryDetilsResponse DALAddBeneficiaryDetils(RequestModel _objRequestModel)
        {
            AddBeneficiaryDetilsResponse _paramresponse = new AddBeneficiaryDetilsResponse();
            try
            {
                AddBeneficiaryDetils _param = DALCommon.CustomDeserializeObjectWithDecryptString<AddBeneficiaryDetils>(_objRequestModel.body);
                DataSet dsResult = AddBeneficiaryDetils(_param);
                if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    _paramresponse.Status = Convert.ToString(dsResult.Tables[0].Rows[0]["Status"].ToString());
                    _paramresponse.Msg = Convert.ToString(dsResult.Tables[0].Rows[0]["Msg"].ToString());
                }
            }
            catch (Exception ex)
            {
                _paramresponse.Status = ex.Message.ToString();
                _paramresponse.Msg = "Error";

            }
            return _paramresponse;
        }
        public DataSet AddBeneficiaryDetils(AddBeneficiaryDetils OBJ)
        {
            try
            {
                SqlParameter[] para = {
                                new SqlParameter("@RemitterId",OBJ.RemitterId),
                                new SqlParameter("@BeneficiaryId",OBJ.BeneficiaryId),
                                new SqlParameter("@BeneficiaryFirstName",OBJ.BeneficiaryFirstName),
                                new SqlParameter("@BeneficiaryLastName",OBJ.BeneficiaryLastName),
                                new SqlParameter("@BeneficiaryAccountNo",OBJ.BeneficiaryAccountNo),
                                new SqlParameter("@BeneficiaryIFSC",OBJ.BeneficiaryIFSC),
                                new SqlParameter("@BeneficiaryBankName",OBJ.BeneficiaryBankName),
                                new SqlParameter("@BeneficiaryMobileNo",OBJ.BeneficiaryMobileNo)
                                 };
                DataSet dsResult = ExecuteQuery("AddBeneficiaryDetails", para);
                return dsResult;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ResponseModel GetBeneficiaryDetils(RequestModel _objrequestdetails)
        {
            List<BeneficiaryResponse> list = new List<BeneficiaryResponse>();
            try
            {
                BeneficiaryModel _param = DALCommon.CustomDeserializeObjectWithDecryptString<BeneficiaryModel>(_objrequestdetails.body);
                SqlParameter[] para = { new SqlParameter("@RemitterId", _param.RemitterId) };
                DataSet dsResult = ExecuteQuery("GetBeneficiaryDetails", para);
                if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in (dsResult.Tables[0].Rows))
                    {
                        list.Add(new BeneficiaryResponse
                        {
                            RemitterId = row["RemitterId"].ToString(),
                            BeneficiaryId = row["BeneficiaryId"].ToString(),
                            BeneficiaryFirstName = row["BeneficiaryFirstName"].ToString(),
                            BeneficiaryLastName = row["BeneficiaryLastName"].ToString(),
                            BeneficiaryAccountNo = row["BeneficiaryAccountNo"].ToString(),
                            BeneficiaryIFSC = row["BeneficiaryIFSC"].ToString(),
                            BeneficiaryBankName = row["BeneficiaryBankName"].ToString(),
                            BeneficiaryMobileNo = row["BeneficiaryMobileNo"].ToString(),
                            OntimeTransLimit = Convert.ToDecimal(row["OntimeTransLimit"].ToString()),
                            MOnthlyTransLimit = Convert.ToDecimal(row["MOnthlyTransLimit"].ToString()),
                            Surcharge = Convert.ToDecimal(row["Surcharge"].ToString()),
                            Expend = Convert.ToDecimal(row["Expend"].ToString()),
                            Status = "Successful"
                        });
                    }
                }
                string onjresponseModel = DALCommon.CustomSerializeObjectWithEncryptList<BeneficiaryResponse>(list);
                ResponseModel _objresponsemodel = new ResponseModel();
                _objresponsemodel.body = onjresponseModel;
                return _objresponsemodel;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ResponseModel DALFundTransferLogResponse(RequestModel _objRequestModel)
        {
            FundTransferLogResponse obj = new FundTransferLogResponse();
            ResponseModel _objresponsemodel = new ResponseModel();
            try
            {
                List<FundTransferAPILog> objList = new List<FundTransferAPILog>();

                FundTransferLog _param = DALCommon.CustomDeserializeObjectWithDecryptString<FundTransferLog>(_objRequestModel.body);
                DataSet dsresult = LogFundTransfer(_param);
                if (dsresult != null && dsresult.Tables.Count > 0)
                {
                    if (dsresult != null && dsresult.Tables.Count > 0 && dsresult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        #region Get Address
                        foreach (DataRow row in (dsresult.Tables[0].Rows))
                        {
                            obj.Response = "Success";
                            obj.TotalAmount = Convert.ToDecimal(dsresult.Tables[0].Rows[0]["TotalAmount"].ToString());
                            obj.MonthlyAmount = Convert.ToDecimal(dsresult.Tables[0].Rows[0]["MonthlyAmount"].ToString());
                            obj.FundTransferLog = objList;
                        }
                        #endregion
                        foreach (DataRow row in (dsresult.Tables[0].Rows))
                        {
                            objList.Add(new FundTransferAPILog
                            {//TransAmount,TransNo,Transdate,BeneficiaryName,Remarks,TransStatus
                                TransAmount = row["TransAmount"].ToString(),
                                TransNo = row["TransNo"].ToString(),
                                Transdate = row["Transdate"].ToString(),
                                BeneficiaryName = row["BeneficiaryName"].ToString(),
                                Remarks = row["Remarks"].ToString(),
                                TransStatus = row["TransStatus"].ToString()
                            });
                        }
                    }
                    else if (dsresult != null && dsresult.Tables.Count > 0 && dsresult.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        obj.Response = "Error";
                    }
                    string onjresponseModel = DALCommon.CustomSerializeObjectWithEncryptString<FundTransferLogResponse>(obj);

                    _objresponsemodel.body = onjresponseModel;


                }

            }
            catch (Exception ex)
            {
                obj.Response = ex.Message.ToString();
                //  obj.Msg = "Error";
            }
            return _objresponsemodel;

        }
        public DataSet LogFundTransfer(FundTransferLog _fundTransferLog)
        {
            SqlParameter[] para =
            {
                new SqlParameter ("@Fk_MemId",_fundTransferLog.FK_MemID),
                new SqlParameter ("@Type",_fundTransferLog.Type)

            };
            DataSet dsresult = ExecuteQuery("FundTransferLog", para);
            return dsresult;
        }

           public DataSet AddWalletAmount(LifeOne.Models.LifeOnee.WalletModel OBJ)
        {
            SqlParameter[] para = {
                                new SqlParameter("@Fk_MemberId",OBJ.Fk_MemId),
                                new SqlParameter("@CreditAmount",OBJ.CreditAmount ),
                                new SqlParameter("@DebitAmount",OBJ.DebitAmount ),
                                new SqlParameter("@beneficiaryid",OBJ.benId ),
                                new SqlParameter("@TransNo",OBJ.TransNo ),
                                new SqlParameter("@TransDate",OBJ.TransDate),
                                new SqlParameter("@TransAmount",OBJ.TransAmount ),
                                new SqlParameter("@TransStatus",OBJ.TransStatus ),
                                new SqlParameter("@BeneficiaryName",OBJ.BeneficiaryName ),
                                new SqlParameter("@RemittersMobileNo",OBJ.NUMBER ),
                                //new SqlParameter("@Session",OBJ.SessionNo),
                                new SqlParameter("@Session",OBJ.SessionNo??string.Empty),
                                new SqlParameter("@AuthCode",OBJ.AuthCode ),
                                new SqlParameter("@Error",OBJ.ERROR??string.Empty ),
                                new SqlParameter("@Result",OBJ.RESULT ),
                                new SqlParameter("@ErrMsg",OBJ.ErrorMsg),
                                new SqlParameter("@AddInfo",OBJ.AddInfo),
                                new SqlParameter("@TokenNo",OBJ.ValidateToken)
                                       };
            DataSet dsResult = ExecuteQuery("SP_Add_WalletDetails", para);
            return dsResult;
        }

    }
}