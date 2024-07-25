using LifeOne.Models.HomeManagement.HEntity;
using LifeOne.Models.TravelModel.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateEntity
{
    public class AssociateProfile
    {
        public int Fk_MemId { get; set; }
        public string InviteCode { get; set; }
        public long Fk_SponserId { get; set; }
        public string SponserName { get; set; }
        public string AccountPurpose { get; set; }
        public string Name { get; set; }
        public string RegType { get; set; }
        public string Gender { get; set; }
        public string OrgType { get; set; }
        public string DOB { get; set; }
        public string MarriageAnniversary { get; set; }
        public string CommencementDate { get; set; }
        public string MaritalStatus { get; set; }
        public string Password { get; set; }
        public string ConPassword { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string AadharNo { get; set; }
        public string CustAadharImage { get; set; }
        public string CustAadharImageStatus { get; set; }
        public string SpouseAadharImagesStatus { get; set; }
        public string PanNo { get; set; }
        public string GSTNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City{ get; set; }
        public string LandMark { get; set; }
        public string Pincode { get; set; }
        public string VillageName { get; set; }
        public string Tehsil { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string CountryId { get; set; }
        public string SpouseName { get; set; }
        public string SpouseAadharNo { get; set; }
        public string SpousePanNo { get; set; }
        public string SpouseAadharImages { get; set; }
        public string NormalPassword { get; set; }
        public string ChildName { get; set; }
        public string ChildRel { get; set; }
        public string ChildAadhar { get; set; }
        public string ChildPanNo { get; set; }
        public string ChildAadharImage { get; set; }
        public decimal TotalLand { get; set; }
        public string FruitImages { get; set; }
        public string FruitVegetablesImages { get; set; }
        public string LeafVegetablesImage { get; set; }
        public string GrainImages { get; set; }
        public string PulseCropImages { get; set; }
        public string profilepic { get; set; }
        public string Zone { get; set; }
		 public string BankAccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string FathersName { get; set; }
		
        public List<AssChildDetails> lstChild { get; set; }
        public List<AssociateProfile> LstProfile { get; set; }
    }

    public class AssChildDetails
    {
        public string ChildName { get; set; }
        public string Relation { get; set; }
        public string ChildAadhar { get; set; }
        public string ChildPanNo { get; set; }
        public string ChildAadharImage { get; set; }
    }

    public class WelcomeLetter
    {
        public string MemberName { get; set; }
        public string LoginId { get; set; }
        public string JoiningDate { get; set; }
        public string CustAadharNo { get; set; }
        public string CustPanNo { get; set; }
        public string VillageName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string profilepic { get; set; }
        public string Tehsil { get; set; }
        public string Mobile { get; set; }
        public string NormalPassword { get; set; }
        public string CompanyAddress1 { get; set; }
        public DataTable dtDetails { get; set; }       
        public DataSet getComapnyDetails()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para = {

                };
                ds = DBHelper.ExecuteQuery("Sp_GetCompanyInfo", para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;


        }

    }

}