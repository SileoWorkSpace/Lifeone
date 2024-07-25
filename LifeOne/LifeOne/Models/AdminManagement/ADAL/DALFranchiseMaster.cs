using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALFranchiseMaster
    {
        public static  FranchiseResponseMaster Save(MAdminFranchise obj)
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
                //queryParameters.Add("@P_City", obj.P_City);
                queryParameters.Add("@P_City", string.Empty);
                queryParameters.Add("@P_Tehsil", obj.P_Tehsil);
                queryParameters.Add("@P_Zone", obj.P_Zone);
                queryParameters.Add("@P_District", obj.P_City);
                queryParameters.Add("@Category", obj.Category);
                queryParameters.Add("@Cr_Address", obj.Cr_Address);
                queryParameters.Add("@Cr_PinCode", obj.Cr_PinCode);
                queryParameters.Add("@Cr_State", obj.Cr_State);
                //queryParameters.Add("@Cr_City", obj.Cr_City);
                queryParameters.Add("@Cr_City", string.Empty);
                queryParameters.Add("@Cr_Tehsil", obj.Co_Tehsil);                
                queryParameters.Add("@Cr_Zone", obj.Zone);
                queryParameters.Add("@Cr_District", obj.Cr_City);


                queryParameters.Add("@CreatedBy", obj.CreatedBy);
                queryParameters.Add("@Issame", obj.Issame);
                queryParameters.Add("@ProfilePic", obj.ProfilePic);
                queryParameters.Add("@BLoginID", obj.LoginID);

                FranchiseResponseMaster _iresult = DBHelperDapper.DAAddAndReturnModel<FranchiseResponseMaster>("FranchiseRegistration", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static FranchiseResponseMaster UpdateFranchiseMaster(MAdminFranchise obj)
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
                queryParameters.Add("@P_City", obj.Cr_City);
                queryParameters.Add("@P_Tehsil", obj.P_Tehsil);
                queryParameters.Add("@Cr_Address", obj.Cr_Address);
                queryParameters.Add("@Cr_PinCode", obj.Cr_PinCode);
                queryParameters.Add("@Cr_State", obj.Cr_State);                
                queryParameters.Add("@Cr_City", obj.Cr_City);
                queryParameters.Add("@Cr_Tehsil", obj.Co_Tehsil);
                queryParameters.Add("@P_State", obj.P_State);
                queryParameters.Add("@CreatedBy", obj.CreatedBy);
                queryParameters.Add("@ProfilePic", obj.ProfilePic);
                queryParameters.Add("@Category", obj.Category);

                queryParameters.Add("@P_Zone", obj.P_Zone);
                queryParameters.Add("@Cr_Zone", obj.Zone);
                queryParameters.Add("@P_District", obj.P_City);
                queryParameters.Add("@Cr_District", obj.Cr_City);
                FranchiseResponseMaster _iresult = DBHelperDapper.DAAddAndReturnModel<FranchiseResponseMaster>("FranchiseRegistrationUpdate", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<MAdminFranchise> GetFranchiseList(MAdminFranchise obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Page", obj.Page);
                queryParameters.Add("@Size", obj.Size);
                queryParameters.Add("@FranchiseType", obj.FranchiseType);
                queryParameters.Add("@Contact", obj.Contact);
                queryParameters.Add("@PersonName", obj.PersonName);
                queryParameters.Add("@LoginID", obj.LoginID);
                queryParameters.Add("@P_PinCode", obj.P_PinCode);
                queryParameters.Add("@IsExport", obj.IsExport);

                List<MAdminFranchise> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminFranchise>("FranchiseList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MAdminFranchise GetFranchiseEdit(MAdminFranchise obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@PKFranchiseRegistrationId", obj.PKFranchiseRegistrationId);
                 
                MAdminFranchise _iresult = DBHelperDapper.DAAddAndReturnModel<MAdminFranchise>("FranchiseList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static FranchiseResponseMaster FranchiseDelete(string id)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FranchiseId", id);

                FranchiseResponseMaster _iresult = DBHelperDapper.DAAddAndReturnModel<FranchiseResponseMaster>("DeleteFranchise", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static FranchiseResponseMaster FranchiseBlockUnblock(string id,long Createdby)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@PK_FranchiseRegistrationId", id);
                queryParameters.Add("@CreatedBy", Createdby);

                FranchiseResponseMaster _iresult = DBHelperDapper.DAAddAndReturnModel<FranchiseResponseMaster>("FranchiseBlockUnblock", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MAdminFranchise> GetFranchiseChildList(MAdminFranchise obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@PKFranchiseRegistrationId", obj.PKFranchiseRegistrationId);
                List<MAdminFranchise> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminFranchise>("GetChildFranchisebyId", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MAdminFranchise GetFranchiseDetailsByLoginId(MAdminFranchise obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@BID", obj.LoginID);

                MAdminFranchise _iresult = DBHelperDapper.DAAddAndReturnModel<MAdminFranchise>("GetMembersDetailsById", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}