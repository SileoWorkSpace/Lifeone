using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.API.DAL;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class AdminDALKYC
    {
        public static List<MemberKYC> GetMemberKYCList(MemberKYC _model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Mobile", _model.Mobile ?? string.Empty);
            queryParameters.Add("@Name", _model.Name ?? string.Empty);
            queryParameters.Add("@LoginId", _model.LoginId ?? string.Empty);
            queryParameters.Add("@Page", _model.Page);
            queryParameters.Add("@size", _model.size > 0 ? _model.size : SessionManager.Size);
            queryParameters.Add("@Status", _model.Status == "Approved" ? _model.Status : _model.Status == "Rejected" ? _model.Status : "Pending");
            List<MemberKYC> _iresult = DBHelperDapper.DAAddAndReturnModelList<MemberKYC>("GetMemberKYCList", queryParameters);
            return _iresult;
        }

        public static List<MemberKYC> ApprveDeclineKYC(MemberKYC _model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@LoginId", _model.LoginId ?? string.Empty);
            queryParameters.Add("@KYCType", _model.Status ?? string.Empty);
            queryParameters.Add("@ApproveKYC", _model.ApproveKyc);
            queryParameters.Add("@RejectKYC", _model.RejectKyc);
            queryParameters.Add("@Remark", _model.Remark ?? string.Empty);
            List<MemberKYC> _iresult = DBHelperDapper.DAAddAndReturnModelList<MemberKYC>("UpdateKYCDetail", queryParameters);
            return _iresult;
        }
        public static List<VerifyPanOrVoterid> GetMemberPanOrVoterID(VerifyPanOrVoterid _model)
        {
            var queryParameters = new DynamicParameters();
            List<VerifyPanOrVoterid> _iresult = new List<VerifyPanOrVoterid>();
            try
            {
                SqlParameter[] param =
                {

                    new SqlParameter("@VerifyStatus",_model.VerifyStatus),
                     new SqlParameter("@PanCard",string.IsNullOrEmpty(_model.PanCard)?null:_model.PanCard),
                      new SqlParameter("@Mobile",string.IsNullOrEmpty(_model.Mobile)?null:_model.Mobile),
                    new SqlParameter("@LoginId",string.IsNullOrEmpty(_model.LoginId)?null:_model.LoginId),
                     new SqlParameter("@Page",_model.Page),
                      new SqlParameter("@size",_model.size > 0 ? _model.size : SessionManager.Size)

            };
                DataSet ds = DBHelper.ExecuteQuery("GetAdminMemberPanOrVoterIdList", param);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        _iresult.Add(new VerifyPanOrVoterid
                        {
                            LoginId = dr["LoginId"].ToString(),
                            Name = dr["Name"].ToString(),
                            Mobile = dr["Mobile"].ToString(),
                            Fk_MemId = Convert.ToInt64(dr["Fk_MemId"].ToString()),
                            Iskyc = Convert.ToBoolean(dr["Iskyc"].ToString()),
                            VoterIdNo = dr["VoterIdNo"].ToString(),
                            AadharImage = dr["AddressFrontUrl"].ToString(),
                            AadharNumber = dr["AddressProofNo"].ToString(),
                            AadharBackImage = dr["AddressBackUrl"].ToString(),
                            PanImage = dr["PanCardURL"].ToString(),
                            BankImage = dr["BankDocumentUrl"].ToString(),
                            AccountNumber = dr["BankDocumentNo"].ToString(),
                            PanCard = dr["PanCard"].ToString(),
                            VerifyStatus = dr["VerifyStatus"].ToString(),
                            Type = dr["Type"].ToString(),
                            IsPanCardRejected = dr["IsPanCardReject"].ToString(),
                            IsPanApproved = dr["IsPanApproved"].ToString(),
                            PanCardRejectRemark = dr["PanCardRejectRemark"].ToString(),
                            date = dr["date"].ToString(),
                            AddressProofStatus = dr["AddressProofStatus"].ToString(),
                            AddressProofBackStatus = dr["AddressProofBackStatus"].ToString(),
                            BankStatus = dr["BankStatus"].ToString(),
                            PanCardStatus = dr["PanCardStatus"].ToString(),
                            TotalRecords = Convert.ToInt32(dr["TotalRecords"].ToString()),
                            Rownumber = Convert.ToInt32(dr["Rownumber"].ToString()),
                            IfscCode = dr["IfscCode"].ToString()


                        });


                    }

                }


                //_iresult = DBHelperDapper.DAAddAndReturnModelList<VerifyPanOrVoterid>("GetMemberPanOrVoterIdList", queryParameters);
            }
            catch (Exception ex)
            {


            }
            return _iresult;
        }
        public static VerifyPanOrVoterid ApprovePanOrVoterId(VerifyPanOrVoterid _model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Fk_MemId", _model.Fk_MemId);
            queryParameters.Add("@Type", _model.Type);
            queryParameters.Add("@Status", _model.Status);
            queryParameters.Add("@Remark", _model.Remark);
            VerifyPanOrVoterid _iresult = DBHelperDapper.DAAddAndReturnModel<VerifyPanOrVoterid>("ApproveDecinePanOrVoterId", queryParameters);
            return _iresult;
        }
    }
}