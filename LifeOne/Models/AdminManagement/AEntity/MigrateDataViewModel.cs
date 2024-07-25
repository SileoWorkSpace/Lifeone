using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MigrateDataViewModel
    {


        public static string connectionString1 = "Data Source=103.14.96.166;Initial Catalog=DATA FOR APP;Persist Security Info=True;User ID=admin1app;Password=ghejk12@";
        //public static string connectionString3 = "Data Source=34.121.160.49;Initial Catalog=LifeoneWellness;Persist Security Info=True;User ID=sa;Password=Swagatam123@";

        //public static string connectionString2 = "Data Source=LifeOne.c3x5wsjwx2rt.ap-south-1.rds.amazonaws.com;Initial Catalog=LifeOne;Persist Security Info=True;User ID=familynprod;Password=FamilynProd123@";        
        public static string connectionString2 =System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        public string _loginid;
        public bool GetValuefromDB1()
        {

            try
            {
                string result;
                DataSet ds = new DataSet();
                SqlConnection _objconnection = new SqlConnection(connectionString1);

                SqlCommand _cmd = new SqlCommand("select  Id,LoginID,Name,sponserid,[Mobile No] as Mobile,[Pan No] as PanNo ,[Account Type] as AccountType,[Promotion Achieved] as Promotion from Admissions where IsUpDated=0 order by ID ", _objconnection);
                _cmd.CommandType = CommandType.Text;

                _objconnection.Open();
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _objconnection.Close();
                _da.Fill(ds);
                int valu = Convert.ToInt32(ds.Tables[0].Compute("Max(Id)", string.Empty));
                using (StringWriter sw = new StringWriter())
                {
                    ds.Tables[0].WriteXml(sw);
                    result = sw.ToString();
                };

                if (!String.IsNullOrEmpty(result))
                {
                    _objconnection = new SqlConnection(connectionString2);

                    _cmd = new SqlCommand("Proc_insertNewMembers", _objconnection);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.AddWithValue("@data", result);
                    _objconnection.Open();
                    _da = new SqlDataAdapter(_cmd);
                    _objconnection.Close();
                    _da.Fill(ds);

                    _objconnection = new SqlConnection(connectionString1);
                    _cmd = new SqlCommand("update Admissions set IsUpdated=1 where IsUpDated=0 and ID<=" + valu, _objconnection);
                    _objconnection.Open();
                    _cmd.ExecuteNonQuery();
                    _objconnection.Close();

                }

                return true;
            }
            catch (Exception ex) { return false; }

        }

        public static AddUpdateDeleteModel AddMember(long FkmemId)
        {
            AddUpdateDeleteModel model = new AddUpdateDeleteModel();
            try
            {
                var QuaryParameter = new DynamicParameters();
                QuaryParameter.Add("@FkmemId", FkmemId);
                AddMemberDetailViewModel data = DBHelperDapper.DAAddAndReturnModel<AddMemberDetailViewModel>("Proc_getMemberDetailForKaryon", QuaryParameter);

                DataSet ds = new DataSet();
                SqlCommand _cmd = new SqlCommand();                
                SqlConnection _objconnection = new SqlConnection(connectionString1);

                _cmd = new SqlCommand("Proc_AddMemberDetail", _objconnection);
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.AddWithValue("@FK_MemId", data.FK_MemId);
                _cmd.Parameters.AddWithValue("@Fk_SponsorId", data.Fk_SponsorId);
                _cmd.Parameters.AddWithValue("@LoginId", data.LoginId);
                _cmd.Parameters.AddWithValue("@Password", data.Password);
                _cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                _cmd.Parameters.AddWithValue("@LastName", data.LastName);
                _cmd.Parameters.AddWithValue("@DOB", data.DOB);
                _cmd.Parameters.AddWithValue("@State", data.State);
                _cmd.Parameters.AddWithValue("@City", data.City);
                _cmd.Parameters.AddWithValue("@PinCode", data.PinCode);
                _cmd.Parameters.AddWithValue("@Mobile", data.Mobile);
                _cmd.Parameters.AddWithValue("@Email", data.Email);
                _cmd.Parameters.AddWithValue("@MemberAccNo", data.MemberAccNo);
                _cmd.Parameters.AddWithValue("@MemberBankName", data.MemberBankName);
                _cmd.Parameters.AddWithValue("@MemberBranch", data.MemberBranch);
                _cmd.Parameters.AddWithValue("@BankAccName", data.BankAccName);
                _cmd.Parameters.AddWithValue("@BankHolderName", data.BankHolderName);
                _cmd.Parameters.AddWithValue("@IFSCCode", data.IFSCCode);
                _cmd.Parameters.AddWithValue("@PanCard", data.PanCard);
                _cmd.Parameters.AddWithValue("@AddressProofNo", data.AddressProofNo);
                _cmd.Parameters.AddWithValue("@IsBlocked", data.IsBlocked);
                _cmd.Parameters.AddWithValue("@JoiningDate", data.JoiningDate);
                _cmd.Parameters.AddWithValue("@CreatedDate", data.CreatedDate);
                _cmd.Parameters.AddWithValue("@TempPermanent", data.TempPermanent);
                //_cmd.Parameters.AddWithValue("@PermanentDate", data.PermanentDate);
                _cmd.Parameters.AddWithValue("@IsBusinessId", data.IsBusinessId);
                _cmd.Parameters.AddWithValue("@Iskyc", data.Iskyc);
                _cmd.Parameters.AddWithValue("@IsFranchise", data.IsFranchise);
                _cmd.Parameters.AddWithValue("@IsFamilyn", data.IsFamilyn);
                _cmd.Parameters.AddWithValue("@SponsorID", data.InviteCode);
                _objconnection.Open();

                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _objconnection.Close();
                _da.Fill(ds);
            }
            catch (Exception ex) { model.Msg = ex.Message; }
            return model;
        }
    }
}