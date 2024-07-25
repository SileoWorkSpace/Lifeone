using LifeOne.Models.API.DAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models
{         
    public class Modelreward : Products
    {

        public decimal BonusAmt { get; set; }
        public decimal RewardAmount { get; set; }
        public decimal TargetPV { get; set; }
        public decimal LeftBusiness { get; set; }
        public decimal RightBusiness { get; set; }
        public string BalanceLeft { get; set; }
        public string BalanceRight { get; set; }
        public string Recognition { get; set; }
        public string PaymentStatus { get; set; }
        public string Status { get; set; }
        public string AchievedOn { get; set; }
        public string Rank { get; set; }
        public string Remarks { get; set; }
        public string Rcolor { get; set; }
        public string ReferBy { get; set; }
        public bool IsVisible { get; set; }
        public string Action { get; set; }
        public string Designation { get; set; }
        public int FkMemId { get; set; }
        public string RewardImage { get; set; }
        public int FkSetRewardId { get; set; }
        public string RewardName { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal Currenttarget { get; set; }
        public string Balancetarget { get; set; }
        public int PK_RId { get; set; }
        public string Msg { get; set; }
        public string LoginId { get; set; }
        public string RewardImg { get; set; }
        public string Tour { get; set; }
        public string ChequeDDBankName { get; set; }
        public string BankBranch { get; set; }
        public string ChequeDDNo { get; set; }
        public string ChequeDDDate { get; set; }
        public string PaidAmount { get; set; }
        public string PaymentModeName { get; set; }
        public string target_point { get; set; }
        public string AchievePoint { get; set; }
        public string PinImage { get; set; }
        
        public List<Modelreward> Rewardslst { get; set; }

        public List<Modelreward> RewardDetailsForAdmin()
        {
            List<Modelreward> datalist = new List<Modelreward>();
            SqlParameter[] para = new SqlParameter[] {
            new SqlParameter ("@fk_memid",FkMemId)
           
            };
            DataSet ds = DBHelper.ExecuteQuery("get_reward",para);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    datalist.Add(new Modelreward
                    {
                        RewardName = dr["pin_name"].ToString(),
                        target_point = dr["target_point"].ToString(),
                        LeftBusiness = Convert.ToDecimal(dr["left_count"].ToString()),
                        RightBusiness = Convert.ToDecimal(dr["right_count"].ToString()),
                        RewardAmount = Convert.ToDecimal(dr["reward_amt"].ToString()),
                        BonusAmt = Convert.ToDecimal(dr["bonus_amt"].ToString()),
                        AchievePoint = dr["achieved_point"].ToString(),
                        AchievedOn = dr["achieved_date"].ToString(),
                        RewardImg = dr["ImagePath"].ToString(),
                        Tour = dr["tour_name"].ToString(),
                        Status = dr["status"].ToString(),
                        PinImage = dr["pin_image"].ToString(),

                    });
                }
            }
            return datalist;

        }

        public List<Modelreward> GetPinRecognition() 
        {
            List<Modelreward> pilist = new List<Modelreward>();
            SqlParameter[] para = new SqlParameter[] {
            new SqlParameter ("@fk_memid",FkMemId)

            };
            DataSet ds = DBHelper.ExecuteQuery("get_reward", para);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    pilist.Add(new Modelreward 
                    {
                        RewardName = dr["pin_name"].ToString(),
                        target_point = dr["target_point"].ToString(),
                        LeftBusiness = Convert.ToDecimal(dr["left_count"].ToString()),
                        RightBusiness = Convert.ToDecimal(dr["right_count"].ToString()),
                        RewardAmount = Convert.ToDecimal(dr["reward_amt"].ToString()),
                        BonusAmt = Convert.ToDecimal(dr["bonus_amt"].ToString()),
                        AchievePoint = dr["achieved_point"].ToString(),
                        AchievedOn = dr["achieved_date"].ToString(),
                        RewardImg = dr["ImagePath"].ToString(),
                        Tour = dr["tour_name"].ToString(),
                        Status = dr["status"].ToString(),
                        PinImage = dr["pin_image"].ToString(),


                    });

                }
            }
            return pilist;

        }

    }
    public class RewardDeatilsAdmin
        {
            public List<RewardDeatilsAdmin> RewardlistAdmin { get; set; }
            public string LoginId { get; set; }
            public string Fk_memId { get; set; }
            public string Name { get; set; }
            public string Rewards { get; set; }
            public string QDate { get; set; }
            public string Rank { get; set; }
            public string DDate { get; set; }
            public string PK_MEmberREwardID { get; set; }
            public string Requestdate { get; set; }
            public string PaymentMode { get; set; }
            public string FromDate { get; set; }

            public string ToDate { get; set; }

            public string Status { get; set; }

        }
 
}