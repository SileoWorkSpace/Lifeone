using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.FranchiseManagement.FEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FDAL
{
    
    public class DALChildFranchise
    {
        public static ChildFranchiseResponseMaster Save(MChildFranchise obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FK_UserTypeID", obj.FK_UserTypeID);
                queryParameters.Add("@Fk_ParentID", obj.Fk_ParentID);
                queryParameters.Add("@FranchiseType", obj.FranchiseType);
                queryParameters.Add("@Password", obj.Password);
                queryParameters.Add("@NormalPassword", obj.NormalPassword);
                queryParameters.Add("@PersonName", obj.PersonName);
                queryParameters.Add("@EmailAddress", obj.EmailAddress);
                queryParameters.Add("@Contact", obj.Contact);
                queryParameters.Add("@Dateofbirth", obj.Dateofbirth);
                queryParameters.Add("@FatherName", obj.FatherName);
                queryParameters.Add("@P_Address", obj.P_Address);
                queryParameters.Add("@P_PinCode", obj.P_PinCode);
                queryParameters.Add("@P_State", obj.P_State);
                queryParameters.Add("@P_City", obj.P_City);
                queryParameters.Add("@P_Tehsil", obj.P_Tehsil);
                queryParameters.Add("@Cr_Address", obj.Cr_Address);
                queryParameters.Add("@Cr_PinCode", obj.Cr_PinCode);
                queryParameters.Add("@Cr_State", obj.Cr_State);
                queryParameters.Add("@Cr_City", obj.Cr_City);
                queryParameters.Add("@Cr_Tehsil", obj.Cr_Tehsil);
                queryParameters.Add("@P_State", obj.P_State);
                queryParameters.Add("@CreatedBy", obj.CreatedBy);
                queryParameters.Add("@Issame", obj.Issame);
                ChildFranchiseResponseMaster _iresult = DBHelperDapper.DAAddAndReturnModel<ChildFranchiseResponseMaster>("ChildFranchiseRegistration", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ChildFranchiseResponseMaster UpdateChildFranchiseMaster(MChildFranchise obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@PKFranchiseRegistrationId", obj.PKFranchiseRegistrationId);
                queryParameters.Add("@FK_UserTypeID", obj.FK_UserTypeID);
                queryParameters.Add("@Fk_ParentID", obj.Fk_ParentID);
                queryParameters.Add("@FranchiseType", obj.FranchiseType);
                queryParameters.Add("@Password", obj.Password);
                queryParameters.Add("@PersonName", obj.PersonName);
                queryParameters.Add("@EmailAddress", obj.EmailAddress);
                queryParameters.Add("@Contact", obj.Contact);
                queryParameters.Add("@Dateofbirth", obj.Dateofbirth);
                queryParameters.Add("@FatherName", obj.FatherName);
                queryParameters.Add("@P_Address", obj.P_Address);
                queryParameters.Add("@P_PinCode", obj.P_PinCode);
                queryParameters.Add("@P_State", obj.P_State);
                queryParameters.Add("@P_City", obj.P_City);
                queryParameters.Add("@P_Tehsil", obj.P_Tehsil);
                queryParameters.Add("@Cr_Address", obj.Cr_Address);
                queryParameters.Add("@Cr_PinCode", obj.Cr_PinCode);
                queryParameters.Add("@Cr_State", obj.Cr_State);
                queryParameters.Add("@Cr_City", obj.Cr_City);
                queryParameters.Add("@Cr_Tehsil", obj.Cr_Tehsil);
                queryParameters.Add("@P_State", obj.P_State);
                queryParameters.Add("@CreatedBy", obj.CreatedBy);
                ChildFranchiseResponseMaster _iresult = DBHelperDapper.DAAddAndReturnModel<ChildFranchiseResponseMaster>("ChildFranchiseRegistrationUpdate", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<MChildFranchise> GetChildFranchiseList(MChildFranchise obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                List<MChildFranchise> _iresult = DBHelperDapper.DAAddAndReturnModelList<MChildFranchise>("ChildFranchiseList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MCustomerWalletBalnce GetCustomerWalletBalance(int Fk_MemId)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Fk_MmeId", Fk_MemId);
                MCustomerWalletBalnce _iresult = DBHelperDapper.DAAddAndReturnModel<MCustomerWalletBalnce>("GetEWalletBalance", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        
        public static MChildFranchise GetChildFranchiseEdit(MChildFranchise obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@PKFranchiseRegistrationId", obj.PKFranchiseRegistrationId);

                MChildFranchise _iresult = DBHelperDapper.DAAddAndReturnModel<MChildFranchise>("ChildFranchiseList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ChildFranchiseResponseMaster ChildFranchiseDelete(string id)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FranchiseId", id);

                ChildFranchiseResponseMaster _iresult = DBHelperDapper.DAAddAndReturnModel<ChildFranchiseResponseMaster>("DeleteFranchise", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ChildFranchiseResponseMaster ChildFranchiseBlockUnblock(string id,long CreatedBy)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@PK_FranchiseRegistrationId", id);
                queryParameters.Add("@CreatedBy", CreatedBy);

                ChildFranchiseResponseMaster _iresult = DBHelperDapper.DAAddAndReturnModel<ChildFranchiseResponseMaster>("FranchiseBlockUnblock", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<MChildFranchise> GetChildChildFranchiseList(MChildFranchise obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                List<MChildFranchise> _iresult = DBHelperDapper.DAAddAndReturnModelList<MChildFranchise>("ChildFranchiseList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}