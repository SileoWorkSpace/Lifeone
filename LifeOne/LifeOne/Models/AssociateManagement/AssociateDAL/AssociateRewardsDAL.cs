using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateDAL
{
    public class AssociateRewardsDAL
    {
        public List<RewardsModel> Rewards(RewardsModel obj)
        {
            List<RewardsModel> objlist = new List<RewardsModel>();
            SqlParameter[] Para =
            {
                new SqlParameter ("@FKMemId",obj.FkMemId),
                new SqlParameter ("@Fk_SetRewardId",obj.FkSetRewardId),
                new SqlParameter ("@Action",obj.Action)
                //new SqlParameter ("@remarks",obj.Remarks)
            };
            DataSet ds = DBHelper.ExecuteQuery("webGetRewardAchiever", Para);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objlist.Add(new RewardsModel
                    {
                        RewardName = dr["RewardName"].ToString(),
                        TargetPV = Convert.ToDecimal(dr["TargetPV"].ToString()),
                        LeftBusiness = Convert.ToDecimal(dr["LeftBusiness"].ToString()),
                        RightBusiness = Convert.ToDecimal(dr["RightBusiness"].ToString()),
                        BalanceLeft = dr["BalanceLeft"].ToString(),
                        BalanceRight = dr["BalanceRight"].ToString(),
                        RewardAmount = Convert.ToDecimal(dr["RewardAmount"].ToString()),
                        Status = dr["RewardStatus"].ToString(),
                        AchievedOn = dr["QDate"].ToString(),
                        Rcolor = dr["Rcolor"].ToString(),
                        IsVisible = Convert.ToBoolean(dr["IsVisible"].ToString()),
                        PK_RId = Convert.ToInt32(dr["PK_MemberRewardID"].ToString()),
                        RewardImg = dr["RewardImg"].ToString(),
                        Rank = dr["Ranks"].ToString(),
                        Designation = dr["Designation"].ToString(),
                        Recognition = dr["Recognition"].ToString(),
                        TargetPoint = dr["RawardTarget"].ToString()
                        


                    });
                }
            }

            return objlist;
        }

        #region Action Page Rewards Update Claim And Skip Process
        public RewardsModel ClaimAndSkip(RewardsModel mdl)
        {
            try
            {
                SqlParameter[] para =
                 {
                   new SqlParameter("@FKMemId", mdl.FkMemId),
                   new SqlParameter("@Fk_SetRewardId", mdl.PK_RId),
                   new SqlParameter("@Action", mdl.Action)
                };

                DataSet ds = DBHelper.ExecuteQuery("webGetRewardAchiever", para);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        mdl.Msg = "1";
                    }
                    else
                    {
                        mdl.Msg = "2";
                    }
                }

                return mdl;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public List<RewardsModel> AssociateBonaza(RewardsModel obj)
        {

            List<RewardsModel> objlist = new List<RewardsModel>();

            try
            {

              
                SqlParameter[] Para =
                {
                new SqlParameter ("@FKMemId",obj.FkMemId),
                new SqlParameter ("@Fk_SetRewardId",obj.FkSetRewardId),
                new SqlParameter ("@Action",obj.Action)
                //new SqlParameter ("@remarks",obj.Remarks)
                 };
                DataSet ds = DBHelper.ExecuteQuery("BonanzaRewardAchiever", Para);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        objlist.Add(new RewardsModel
                        {
                            RewardName = dr["BonanzaName"].ToString(),
                            RewardAmount = Convert.ToDecimal(dr["BusinessAmount"].ToString()),
                            Status = dr["RewardStatus"].ToString(),
                            AchievedOn = dr["QDate"].ToString(),
                            Rcolor = dr["Rcolor"].ToString(),
                            IsVisible = Convert.ToBoolean(dr["IsVisible"].ToString()),
                            PK_RId = Convert.ToInt32(dr["PK_MemberRewardID"].ToString()),
                            RewardImg = dr["BonanzaImg"].ToString(),
                            TargetPoint = dr["BonanzaTarget"].ToString(),
                            CurrentBusiness = Convert.ToDecimal(dr["CurrentBusiness"].ToString()),
                            BalanceBusiness = Convert.ToDecimal(dr["BalanceTarget"].ToString()),
                            FromDate = dr["FromDate"].ToString(),
                            ToDate = dr["ToDate"].ToString(),





                        });
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;

            }

            return objlist;
        }


        public MAdminUploadKyc AdminUploadKyc(MAdminUploadKyc model)
        {
            try
            {
                var Param = new DynamicParameters();
                 Param.Add("@fk_memId", model.Fk_MemId);
                Param.Add("@PanCardNo", model.PanCardNo);
                Param.Add("@pancard",   model.PanUrl);
           Param.Add("@AddressProofNo" ,model.AddressProofNo);
              Param.Add("@aadharfront", model.AddressFrontUrl);
               Param.Add("@aadharback", model.AddressBackUrl);
                 Param.Add("@BankAcNo", model.BankACNo);
                   Param.Add("@cheque", model.BankUrl);


                MAdminUploadKyc modal = DBHelperDapper.DAAddAndReturnModel<MAdminUploadKyc>("AdminUploadKYC", Param);
                return modal;

            }
            catch(Exception ex)
            {
                throw ex;


            }


        }
        public MAdminUploadKyc ViewEditUpdateProfile(MAdminUploadKyc model)
        {
            try
            {
                var Param = new DynamicParameters();
                Param.Add("@fk_memId", model.Fk_MemId);
                Param.Add("@type", model.type);
                Param.Add("@PanNo", model.PanCardNo==""?null: model.PanCardNo);
                Param.Add("@AddressProofNo", model.AddressProofNo==""?null: model.AddressProofNo);
                Param.Add("@BankDocumentNo", model.BankACNo==""?null: model.BankACNo);
                Param.Add("@PanUrl", model.PanUrl==""?null : model.PanUrl);
                Param.Add("@AddressFrontUrl", model.AddressFrontUrl==""?null: model.AddressFrontUrl);
                Param.Add("@AddressBackUrl", model.AddressBackUrl==""?null: model.AddressBackUrl);
                Param.Add("@BankUrl", model.BankUrl==""?null: model.BankUrl);
                Param.Add("@IfscCode", model.IfscCode == "" ? null : model.IfscCode);
                MAdminUploadKyc modal = DBHelperDapper.DAAddAndReturnModel<MAdminUploadKyc>("GetKycDetailById", Param);
                return modal;

            }
            catch (Exception ex)
            {
                throw ex;


            }


        }
        public MAdminUploadKyc ViewKYCProfile(MAdminUploadKyc model)
        {
            try
            {
                var Param = new DynamicParameters();
                Param.Add("@Fk_MemId", model.Fk_MemId);
                //Param.Add("@type", model.type);


                MAdminUploadKyc modal = DBHelperDapper.DAAddAndReturnModel<MAdminUploadKyc>("GetKycDetailsById", Param);
                return modal;

            }
            catch (Exception ex)
            {
                throw ex;


            }


        }

        public MAdminUploadKyc AdminReKYC(MAdminUploadKyc model)
        {
            MAdminUploadKyc obj = new MAdminUploadKyc();
            try
            {
                SqlParameter[] param =
                {

                    new SqlParameter("@Fk_MemId",model.Fk_MemId),
                    new SqlParameter("@ActionType",model.type)
                };
                DataSet ds = DBHelper.ExecuteQuery("AdminReKYC", param);
                if(ds !=null && ds.Tables[0]!=null && ds.Tables[0].Rows[0]["Code"].ToString()=="1")
                {

                    obj.Code =Convert.ToInt32(ds.Tables[0].Rows[0]["Code"].ToString());
                    obj.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();


                }

            }
            catch(Exception ex)
            {
                throw ex;

            }
            return obj;

        }


        public RewardsModel BonanzaClaimAndSkip(RewardsModel mdl)
        {
            try
            {
                SqlParameter[] para =
                 {
                   new SqlParameter("@FKMemId", mdl.FkMemId),
                   new SqlParameter("@Fk_SetRewardId", mdl.PK_RId),
                   new SqlParameter("@Action", mdl.Action)
                };

                DataSet ds = DBHelper.ExecuteQuery("BonanzaRewardAchiever", para);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        mdl.Msg = "1";
                    }
                    else
                    {
                        mdl.Msg = "2";
                    }
                }

                return mdl;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        #endregion

        #region

        public List<RewardsModel> AdminRewards(RewardsModel obj)
        {
            List<RewardsModel> objlist = new List<RewardsModel>();
            SqlParameter[] Para =
            {
                new SqlParameter ("@LoginId",obj.LoginId),
                new SqlParameter ("@Fk_SetRewardId",obj.FkSetRewardId),
                new SqlParameter ("@Action",obj.Action)
                //new SqlParameter ("@remarks",obj.Remarks)
            };
            DataSet ds = DBHelper.ExecuteQuery("webGetRewardAchieverByAdmin", Para);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objlist.Add(new RewardsModel
                    {
                        RewardName = dr["RewardName"].ToString(),
                        TargetPV = Convert.ToDecimal(dr["TargetPV"].ToString()),
                        LeftBusiness = Convert.ToDecimal(dr["LeftBusiness"].ToString()),
                        RightBusiness = Convert.ToDecimal(dr["RightBusiness"].ToString()),
                        BalanceLeft = dr["BalanceLeft"].ToString(),
                        BalanceRight = dr["BalanceRight"].ToString(),
                        RewardAmount = Convert.ToDecimal(dr["RewardAmount"].ToString()),
                        Status = dr["RewardStatus"].ToString(),
                        AchievedOn = dr["QDate"].ToString(),
                        Rcolor = dr["Rcolor"].ToString(),
                        IsVisible = Convert.ToBoolean(dr["IsVisible"].ToString()),
                        PK_RId = Convert.ToInt32(dr["PK_MemberRewardID"].ToString()),
                        RewardImg = dr["RewardImg"].ToString(),
                        Rank = dr["Ranks"].ToString(),
                        Designation = dr["Designation"].ToString(),
                        Recognition = dr["Recognition"].ToString(),
                        TargetPoint = dr["RawardTarget"].ToString()



                    });
                }
            }

            return objlist;
        }

        public RewardsModel AdminClaimAndSkip(RewardsModel mdl)
        {
            try
            {
                SqlParameter[] para =
                 {
                   new SqlParameter("@LoginId", mdl.LoginId),
                   new SqlParameter("@Fk_SetRewardId", mdl.PK_RId),
                   new SqlParameter("@Action", mdl.Action)
                };

                DataSet ds = DBHelper.ExecuteQuery("webGetRewardAchieverByAdmin", para);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        mdl.Msg = "1";
                    }
                    else
                    {
                        mdl.Msg = "2";
                    }
                }

                return mdl;
            }
            catch (Exception ex)
            {
                return null;
            }
        }




        public List<RewardsModel> AdminBonanza(RewardsModel obj)
        {

            List<RewardsModel> objlist = new List<RewardsModel>();

            try
            {


                SqlParameter[] Para =
                {
                new SqlParameter ("@LoginId",obj.LoginId),
                new SqlParameter ("@Fk_SetRewardId",obj.FkSetRewardId),
                new SqlParameter ("@Action",obj.Action)
                //new SqlParameter ("@remarks",obj.Remarks)
            };
                DataSet ds = DBHelper.ExecuteQuery("BonanzaRewardAchieverByAdmin", Para);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        objlist.Add(new RewardsModel
                        {
                            RewardName = dr["BonanzaName"].ToString(),
                            RewardAmount = Convert.ToDecimal(dr["BusinessAmount"].ToString()),
                            Status = dr["RewardStatus"].ToString(),
                            AchievedOn = dr["QDate"].ToString(),
                            Rcolor = dr["Rcolor"].ToString(),
                            IsVisible = Convert.ToBoolean(dr["IsVisible"].ToString()),
                            PK_RId = Convert.ToInt32(dr["PK_MemberRewardID"].ToString()),
                            RewardImg = dr["BonanzaImg"].ToString(),
                            TargetPoint = dr["BonanzaTarget"].ToString(),
                            CurrentBusiness = Convert.ToDecimal(dr["CurrentBusiness"].ToString()),
                            BalanceBusiness = Convert.ToDecimal(dr["BalanceTarget"].ToString()),
                            FromDate = dr["FromDate"].ToString(),
                            ToDate = dr["ToDate"].ToString(),





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


      



        #endregion



    }
}