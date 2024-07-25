using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.API.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALFranchiseViewProfile
    {
        public MFranchiseViewProfile AdminViewFranchiseDetail(MFranchiseViewProfile model)
        {
            MFranchiseViewProfile obj = new MFranchiseViewProfile();
            List<MFranchiseProfilePaymentDetail> PayList = new List<MFranchiseProfilePaymentDetail>();
            List<MFranchiseProductDetail> FranchisePList = new List<MFranchiseProductDetail>();
            try
            {
              
                SqlParameter[] param =
                {
                   new SqlParameter("@Fk_FranchiseId",model.FranciseID)
                };

               DataSet  ds = DBHelper.ExecuteQuery("Proc_FranchiseViewProfile", param);
                if(ds !=null && ds.Tables[0]!=null && ds.Tables[0].Rows.Count>0)
                {

                    obj.PersonName = ds.Tables[0].Rows[0]["PersonName"].ToString();
                    obj.LoginID = ds.Tables[0].Rows[0]["LoginID"].ToString();
                    obj.EmailAddress = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                    obj.Contact = ds.Tables[0].Rows[0]["Contact"].ToString();
                    obj.DateOfBirth = ds.Tables[0].Rows[0]["DateOfBirth"].ToString();
                    obj.ProfilePic = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                    obj.FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString();
                    obj.P_Address = ds.Tables[0].Rows[0]["P_Address"].ToString();
                    obj.P_State = ds.Tables[0].Rows[0]["P_State"].ToString();
                    obj.P_City = ds.Tables[0].Rows[0]["P_City"].ToString();
                    obj.P_PinCode = ds.Tables[0].Rows[0]["P_PinCode"].ToString();
                    obj.Cr_Address = ds.Tables[0].Rows[0]["Cr_Address"].ToString();
                    obj.Cr_PinCode = ds.Tables[0].Rows[0]["Cr_PinCode"].ToString();
                    obj.AadharNumber = ds.Tables[0].Rows[0]["AadharNumber"].ToString();
                    obj.Cr_State= ds.Tables[0].Rows[0]["Cr_State"].ToString();
                    obj.Cr_City = ds.Tables[0].Rows[0]["Cr_City"].ToString();
                    obj.PanNumber = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                    obj.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                    obj.AccountNumber = ds.Tables[0].Rows[0]["AccountNumber"].ToString();
                    obj.IfscCode = ds.Tables[0].Rows[0]["IfscCode"].ToString();
                    obj.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                    obj.IsBlocked = ds.Tables[0].Rows[0]["IsBlocked"].ToString();
                    obj.JoiningDate = ds.Tables[0].Rows[0]["JoiningDate"].ToString();
                    obj.PKFranchiseRegistrationId=Convert.ToInt32(ds.Tables[0].Rows[0]["PKFranchiseRegistrationId"].ToString());



                }
                if(ds !=null && ds.Tables[1]!=null && ds.Tables[1].Rows.Count>0)
                {
                    foreach(DataRow dr in ds.Tables[1].Rows)
                    {

                        PayList.Add(new MFranchiseProfilePaymentDetail
                        {
                            Amount=Convert.ToDecimal(dr["Amount"].ToString()),
                            ApprovelRemark=dr["ApprovelRemark"].ToString(),
                            BranchName=dr["BranchName"].ToString(),
                            FranchisePaymentStatus=dr["FranchisePaymentStatus"].ToString(),
                            PaymentDate=dr["PaymentDate"].ToString(),
                            PaymentMode=dr["PaymentMode"].ToString(),
                            RequestRemark=dr["RequestRemark"].ToString(),
                            TransactionNo=dr["TransactionNo"].ToString()

                        });

                    }
                   


                }
                if (ds != null && ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                { 
                
                    foreach(DataRow dr in ds.Tables[2].Rows)
                    {
                        FranchisePList.Add(new MFranchiseProductDetail
                        {
                            AvailableQuantity=dr["AvailableQuantity"].ToString(),
                            MRP= Convert.ToDecimal(dr["MRP"].ToString()),
                            ProductDescription=dr["ProductDescription"].ToString(),
                            ProductImage=dr["ProductImage"].ToString(),
                            ProductName=dr["ProductName"].ToString(),
                            ProductType=dr["ProductType"].ToString(),
                            PV=Convert.ToDecimal(dr["PV"].ToString()),
                            SalesPrice= Convert.ToDecimal(dr["SalesPrice"].ToString())

                        });


                    }
                }
                obj.FranchisePaymentList = PayList;
                obj.FranchiseProductList = FranchisePList;



            }
            catch(Exception ex)
            {

            }
            return obj;


        }
    }
}