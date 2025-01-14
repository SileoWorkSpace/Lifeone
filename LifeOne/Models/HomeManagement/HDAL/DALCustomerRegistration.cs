
using LifeOne.Models.HomeManagement.HEntity;
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.Manager;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.API;
using Dapper;

namespace LifeOne.Models.HomeManagement.HDAL
{
    public class DALCustomerRegistration
    {
        public static MCustomerRegistration GetSponserDetailByReferalCode(string Fk_SponserId)
        {
            try
            {
                string _qury = "GetRefferalDetails @InviteCode=" + Fk_SponserId + "";
                MCustomerRegistration _iresult = DBHelperDapper.DAGetDetailsInList<MCustomerRegistration>(_qury).FirstOrDefault();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MplaceUnderId InvokePlaceHolderId(string Fk_SponserId)
        {
            try
            {
                //call the api Here
                
                string _qury = "Proc_CheckSponsor @SponsorId='" + Fk_SponserId + "'";
                MplaceUnderId _iresult = DBHelperDapper.DAGetDetailsInList<MplaceUnderId>(_qury).FirstOrDefault();
                return _iresult;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static MplaceUnderId InvokePlaceHolderIdWithParent(string Fk_SponserId,string Fk_PlaceId)
        {
            try
            {
                //call the api Here                
                string _qury = "Proc_CheckParent @SponsorId='" + Fk_SponserId + "',@ParentId='" + Fk_PlaceId + "'";
                MplaceUnderId _iresult = DBHelperDapper.DAGetDetailsInList<MplaceUnderId>(_qury).FirstOrDefault();
                return _iresult;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static MCustomerRegistration DistrictDetails(string Pincode)
        {
            try
            {
                string _qury = "Proc_GetAreaDetailByPincode @Pincode=" + Pincode + "";
                MCustomerRegistration _iresult = DBHelperDapper.DAGetDetailsInList<MCustomerRegistration>(_qury).FirstOrDefault();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static MSimpleString CustomerPrimaryDetails(MCustomerRegistration _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Fk_MemId", _param.Fk_MemId);
                queryParameters.Add("@Password", _param.Password);
                queryParameters.Add("@InvitedBy", _param.InviteCode);
                queryParameters.Add("@Name", _param.Name);
                queryParameters.Add("@DOB", _param.DOB);
                queryParameters.Add("@Gender", _param.Gender);
                queryParameters.Add("@MarritalStatus", _param.MaritalStatus);
                queryParameters.Add("@Address1", _param.Address1 ?? string.Empty);
                queryParameters.Add("@Address2", _param.Address2 ?? string.Empty);
                queryParameters.Add("@LandMark", _param.LandMark ?? string.Empty);
                queryParameters.Add("@VillageName", _param.VillageName ?? string.Empty);
                queryParameters.Add("@Country", _param.CountryId ?? string.Empty);
                queryParameters.Add("@State", _param.StateName);
                queryParameters.Add("@City", _param.DistrictName);
                queryParameters.Add("@Tehsil", _param.Tehsil);
                queryParameters.Add("@PinCode", _param.Pincode);
                queryParameters.Add("@Mobile", _param.MobileNo);
                queryParameters.Add("@Email", _param.EmailId);
                //queryParameters.Add("@AccountPurpose", _param.AccountPurpose);
                queryParameters.Add("@Leg", _param.Leg);
                queryParameters.Add("@RegistrationType", _param.RegType);
                queryParameters.Add("@OrganisationType", _param.OrgType ?? string.Empty);
                queryParameters.Add("@CustAadharNo", _param.AadharNo == "undefined" ? string.Empty : _param.AadharNo);
                queryParameters.Add("@CustAadharImg", _param.CustAadharImage == "undefined" ? string.Empty : _param.CustAadharImage);
                queryParameters.Add("@CustPanNo", _param.PanNo == "undefined" ? string.Empty : _param.PanNo);
                queryParameters.Add("@CustGstNo", _param.GSTNo ?? string.Empty);
                queryParameters.Add("@CommencementDate", _param.CommencementDate ?? string.Empty);
                queryParameters.Add("@SpouseName", _param.SpouseName ?? string.Empty);
                queryParameters.Add("@SpouseAadharNo", _param.SpouseAadharNo);
                queryParameters.Add("@SpouseAadharImg", _param.SpouseAadharImages);
                queryParameters.Add("@SpousePanNo", _param.SpousePanNo ?? string.Empty);
                queryParameters.Add("@NormalPassword", _param.NormalPassword);
                queryParameters.Add("@StateId", _param.StateId);
                queryParameters.Add("@DistrictId", _param.DistrictId);
                queryParameters.Add("@Zone", _param.Zone);
                //-------------New Fileds------------------
                queryParameters.Add("@AccountType", _param.AccountType);
                queryParameters.Add("@DocType", _param.DocType);
                queryParameters.Add("@DocNo", _param.DocNo);
                queryParameters.Add("@DocFile", _param.FilePath);


                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("Proc_CustomerRegistration", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static MSimpleString CustomerUpdatePrimaryDetails(MCustomerRegistration _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Fk_MemId", _param.Fk_MemId);
                queryParameters.Add("@Password", _param.Password);
                queryParameters.Add("@InvitedBy", _param.InviteCode);
                queryParameters.Add("@Name", _param.Name);
                queryParameters.Add("@DOB", _param.DOB);
                queryParameters.Add("@profilepic", _param.profilepic);
                queryParameters.Add("@Gender", _param.Gender);
                queryParameters.Add("@MarritalStatus", _param.MaritalStatus);
                queryParameters.Add("@MarriageAnniversary", _param.MarriageAnniversary);
                queryParameters.Add("@Mobile", _param.MobileNo);
                queryParameters.Add("@Email", _param.EmailId);
                queryParameters.Add("@AccountPurpose", _param.AccountPurpose);
                queryParameters.Add("@RegistrationType", _param.RegType);
                queryParameters.Add("@OrganisationType", _param.OrgType ?? string.Empty);
                queryParameters.Add("@CustAadharNo", _param.AadharNo);
                queryParameters.Add("@CustAadharImg", _param.CustAadharImage);
                queryParameters.Add("@CustPanNo", _param.PanNo);
                queryParameters.Add("@CustGstNo", _param.GSTNo ?? string.Empty);
                queryParameters.Add("@CommencementDate", _param.CommencementDate ?? string.Empty);
                queryParameters.Add("@NormalPassword", _param.NormalPassword);
                queryParameters.Add("@Address1", _param.Address1);
                queryParameters.Add("@Address2", _param.Address2);
                queryParameters.Add("@LandMark", _param.LandMark);
                queryParameters.Add("@Country", _param.CountryId);
                queryParameters.Add("@State", _param.StateName);
                queryParameters.Add("@City", _param.DistrictName);
                queryParameters.Add("@Tehsil", _param.Tehsil);
                queryParameters.Add("@PinCode", _param.Pincode);
                queryParameters.Add("@Fk_MemId", _param.Fk_MemId);
                queryParameters.Add("@VillageName", _param.VillageName);
                queryParameters.Add("@Zone", _param.Zone);
                queryParameters.Add("@Districtname", _param.DistrictName);
                queryParameters.Add("@NomineeName", _param.NomineeName);
                queryParameters.Add("@RelationWithNominee", _param.RelationWithNominee);
                queryParameters.Add("@BankAccountNo", _param.BankAccountNo);
                queryParameters.Add("@IFSCCode", _param.IFSCCode);
                queryParameters.Add("@BankName", _param.BankName);
                queryParameters.Add("@BranchName", _param.BranchName);
                queryParameters.Add("@LastName", _param.LastName);
                queryParameters.Add("@FathersName", _param.FathersName);

                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("Proc_CustomerRegistration", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MSimpleString UpdateProfile(MCustomerRegistration _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@profilepic", _param.profilepic);
                queryParameters.Add("@LoginId", _param.LoginId);
                

                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("update_profile", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static MSimpleString CustomerAddressDetails(MCustomerRegistration _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Address1", _param.Address1);
                queryParameters.Add("@Address2", _param.Address2);
                queryParameters.Add("@LandMark", _param.LandMark);
                queryParameters.Add("@Country", _param.CountryId);
                queryParameters.Add("@State", _param.StateName);
                queryParameters.Add("@City", string.Empty);
                queryParameters.Add("@Tehsil", _param.Tehsil);
                queryParameters.Add("@PinCode", _param.Pincode);
                queryParameters.Add("@Fk_MemId", _param.Fk_MemId);
                queryParameters.Add("@VillageName", _param.VillageName);
                queryParameters.Add("@Zone", _param.Zone);
                queryParameters.Add("@Districtname", _param.DistrictName);
                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("CustomerAddressDetails", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static MSimpleString CustomerFamilyDetails(MCustomerRegistration _param, string XmlData)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@SpouseName", _param.SpouseName);
                queryParameters.Add("@SpouseAadharNo", _param.SpouseAadharNo);
                queryParameters.Add("@SpouseAadharImg", _param.SpouseAadharImages);
                queryParameters.Add("@SpousePanNo", _param.SpousePanNo);
                queryParameters.Add("@FamilyDetails", XmlData);
                queryParameters.Add("@Fk_MemId", _param.Fk_MemId);
                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("CustomerFamilyDetails", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static MSimpleString CustomerFarmDetails(MCustomerRegistration _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@TotalLand", _param.TotalLand);
                queryParameters.Add("@FruitImages", _param.FruitImages);
                queryParameters.Add("@FruitVegetablesImages", _param.FruitVegetablesImages);
                queryParameters.Add("@LeafVegetablesImage", _param.LeafVegetablesImage);
                queryParameters.Add("@GrainImages", _param.GrainImages);
                queryParameters.Add("@PulseCropImages", _param.PulseCropImages);
                queryParameters.Add("@Fk_MemId", _param.Fk_MemId);
                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("CustomerFarmDetails", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static MCustomerRegistration GetCustomerDetailsById(int? Fk_MemId)
        {
            try
            {
                string _qury = "UserDetailsByIdNew @Fk_MemId=" + Fk_MemId + "";
                MCustomerRegistration _iresult = DBHelperDapper.DAGetDetailsInList<MCustomerRegistration>(_qury).FirstOrDefault();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<ChildDetails> GetChildDetailsById(int? Fk_MemId)
        {
            try
            {
                string _qury = "GetChildDetailsById @Fk_MemId=" + Fk_MemId + "";
                List<ChildDetails> _iresult = DBHelperDapper.DAGetDetailsInList<ChildDetails>(_qury).ToList();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static FranchiseRegistrationResponse SaveFranchiseApplicationForm(FranchiseApplicationFormModel req)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@AssociateId", req.AssociateId);
            queryParameters.Add("@AssociateName", req.AssociateName);
            queryParameters.Add("@MobileNo", req.MobileNo);
            queryParameters.Add("@EmailId", req.EmailId);
            queryParameters.Add("@AppliedForPinCode", req.AppliedForPinCode);
            queryParameters.Add("@AreaName", req.AreaName);
            queryParameters.Add("@Village", req.Village);
            queryParameters.Add("@Taluka", req.Taluka);
            queryParameters.Add("@District", req.District);
            queryParameters.Add("@PinCode", req.PinCode);
            queryParameters.Add("@State", req.State);
            queryParameters.Add("@FranchiseName", req.FranchiseName);
            queryParameters.Add("@DocData", req.DocXml);
            queryParameters.Add("@BankData", req.BankXML);
            queryParameters.Add("@Address", req.Address);
            queryParameters.Add("@DocumentType", req.DocType);
            FranchiseRegistrationResponse _iresult = DBHelperDapper.DAAddAndReturnModel<FranchiseRegistrationResponse>("Proc_InserFranchiseRequest", queryParameters);
            return _iresult;
        }
        public static MemberDetailsForFranchiseModel GetMemberDetailsForFranchiseApplicationForm(string BID)
        {
            try
            {
                string _qury = "GetMembersDetailsByIdForFranchiseRequest @BID=" + BID + "";
                MemberDetailsForFranchiseModel _iresult = DBHelperDapper.DAGetDetailsInList<MemberDetailsForFranchiseModel>(_qury).FirstOrDefault();
                return _iresult;
            }
            catch (Exception ex)
            {
                return new MemberDetailsForFranchiseModel { Message = "Enter valid id" };
            }
        }
        public static FranchiseApplicationFormReportModel BindFranchiseApplicationForm(FranchiseApplicationFormReportModel model)
        {
            try
            {
                int totalRecords = 0;
                string _qury = string.Empty;
                FranchiseApplicationFormReportModel _response = new FranchiseApplicationFormReportModel();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@procId", model.procId);
                queryParameters.Add("@PageNo", model.page == 0 ? 1 : model.page);
                queryParameters.Add("@PageSize", SessionManager.Size);
                queryParameters.Add("@AppliedForPinCode", model.AppliedForPinCode == 0 ? null : model.AppliedForPinCode);
                queryParameters.Add("@AssociateName", string.IsNullOrEmpty(model.AssociateName) ? null : model.AssociateName);
                queryParameters.Add("@MobileNo", string.IsNullOrEmpty(model.MobileNo) ? null : model.MobileNo);
                queryParameters.Add("@EmailId", string.IsNullOrEmpty(model.EmailId) ? null : model.EmailId);
                queryParameters.Add("@LoginId", string.IsNullOrEmpty(model.LoginId) ? null : model.LoginId);
                //queryParameters.Add("@Page", model.page);
                //queryParameters.Add("@Size",  model.Size);
                //queryParameters.Add("@IsExport", model.IsExport);
                _response._DataList = DBHelperDapper.DAAddAndReturnModelList<FranchiseApplicationFormReportModel>("Proc_getFreanchiseRequest", queryParameters);
                if (_response._DataList.Count > 0)
                {
                    totalRecords = Convert.ToInt32(_response._DataList[0].ToatlRecords);
                    var pager = new Pager(totalRecords, model.page == 0 ? 1 : model.page, SessionManager.Size);
                    _response.Pager = pager;
                }
                return _response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<FranchiseApplicationFormReportModel> BindFranchiseApplicationFormList(FranchiseApplicationFormReportModel model)
        {
            try
            {
                int totalRecords = 0;
                string _qury = string.Empty;
                List<FranchiseApplicationFormReportModel> _objlist = new List<FranchiseApplicationFormReportModel>();
                FranchiseApplicationFormReportModel _response = new FranchiseApplicationFormReportModel();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@procId", model.procId);
                queryParameters.Add("@PageNo", model.page == 0 ? 1 : model.page);
                queryParameters.Add("@PageSize", 2000);
                queryParameters.Add("@AppliedForPinCode", model.AppliedForPinCode);
                queryParameters.Add("@AssociateName", model.AssociateName);
                queryParameters.Add("@MobileNo", model.MobileNo);
                queryParameters.Add("@EmailId", model.EmailId);
                _response._DataList = DBHelperDapper.DAAddAndReturnModelList<FranchiseApplicationFormReportModel>("Proc_getFreanchiseRequest", queryParameters);
                _objlist = _response._DataList;
                return _objlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static FranchiseApprovalResponseModel ApproveFranchiseApproval(int? KeyId, int? Status, string Remark)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@FranchiseId", KeyId);
            queryParameters.Add("@Remark", Remark);
            queryParameters.Add("@Isapproved", Status);
            queryParameters.Add("@CreatedBy", SessionManager.Fk_MemId);
            FranchiseApprovalResponseModel _iresult = DBHelperDapper.DAAddAndReturnModel<FranchiseApprovalResponseModel>("Proc_ApproveFranchiseRequest", queryParameters);
            return _iresult;
        }
        public static FranchiseApplicationFormReportModel GetOtherFranchiseApprovalDetails(int KeyId)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@KeyId", KeyId);
                FranchiseApplicationFormReportModel _iresult = DBHelperDapper.DAAddAndReturnModel<FranchiseApplicationFormReportModel>("ProGetOtherFranchiseApprovalDetails", queryParameters);
                return _iresult;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static FranchiseApplicationFormReportModel CheckFranchiseApprovalByPincode(int? pincode)
        {
            try
            {
                FranchiseApplicationFormReportModel _response = new FranchiseApplicationFormReportModel();
                string _qury = "Proc_ChkFranchiseRequest @AppliedForPinCode=" + pincode + "";
                _response = DBHelperDapper.DAGetDetailsInList<FranchiseApplicationFormReportModel>(_qury).FirstOrDefault();
                return _response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static FranchiseRegistrationResponse SaveFranchiseApplicationForm_V1(FranchiseApplicationFormModel req)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@AssociateId", req.AssociateId);
            queryParameters.Add("@AssociateName", req.AssociateName);
            queryParameters.Add("@MobileNo", req.MobileNo);
            queryParameters.Add("@EmailId", req.EmailId);
            queryParameters.Add("@AppliedForPinCode", req.AppliedForPinCode);
            queryParameters.Add("@AreaName", req.AreaName);
            queryParameters.Add("@Village", req.Village);
            queryParameters.Add("@Taluka", req.Taluka);
            queryParameters.Add("@DistrictId", req.DistrictId);
            queryParameters.Add("@PinCode", req.PinCode);
            queryParameters.Add("@StateID", req.StateId);
            queryParameters.Add("@FranchiseName", req.FranchiseName);
            queryParameters.Add("@DocData", req.DocXml);
            queryParameters.Add("@BankData", req.BankXML);
            queryParameters.Add("@Address", req.Address);
            queryParameters.Add("@DocumentType", req.DocType);
            queryParameters.Add("@FranchiseTypeId", req.FranchiseTypeId);
            
            FranchiseRegistrationResponse _iresult = DBHelperDapper.DAAddAndReturnModel<FranchiseRegistrationResponse>("Proc_InserFranchiseRequestForStateDistrict", queryParameters);
            return _iresult;
        }
        public static FranchiseReqListResponse DeleteFranchiseRequestById(int? Fk_MemId)
        {
            try
            {
                FranchiseReqListResponse _iresult = new FranchiseReqListResponse();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@KeyId", Fk_MemId);
                queryParameters.Add("@FkTypeId", 0);
                queryParameters.Add("@LoginId", null);
                queryParameters.Add("@ProcId", 5);
                queryParameters.Add("@CreatedBy", SessionManager.Fk_MemId);
                _iresult = DBHelperDapper.DAAddAndReturnModel<FranchiseReqListResponse>("Proc_AllowFranchiseRegistration", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static FranchiseReqListResponse FranchiseReqUpdateDetails(FranchiseReqList _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@LoginId", _param.LoginId);
                queryParameters.Add("@FkTypeId", _param.FK_FranchiseTypeId);
                queryParameters.Add("@CreatedBy", SessionManager.Fk_MemId);
                queryParameters.Add("@KeyId", _param.KeyId);
                queryParameters.Add("@ProcId", 4);
                FranchiseReqListResponse _iresult = DBHelperDapper.DAAddAndReturnModel<FranchiseReqListResponse>("Proc_AllowFranchiseRegistration", queryParameters);

                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<FranchiseReqListResponse> AllowFranchiseRegistrationList(FranchiseReqListResponse model)
        {
            try
            {
                FranchiseReqListResponse _iresult = new FranchiseReqListResponse();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@ProcId", 2);
                queryParameters.Add("@LoginId", model.LoginId);
                queryParameters.Add("@Name", model.AssociateName);
                _iresult.FranchiseReqLists = DBHelperDapper.DAAddAndReturnModelList<FranchiseReqListResponse>("Proc_AllowFranchiseRegistration", queryParameters);
                return _iresult.FranchiseReqLists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static FranchiseReqListResponse AllowFranchiseRegistrationrequest(FranchiseReqListResponse req)
        {
            try
            {
                FranchiseReqListResponse _response = new FranchiseReqListResponse();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FkTypeId", req.TypeName);
                queryParameters.Add("@LoginId", req.LoginId);
                queryParameters.Add("@Fk_StateId", req.StateId);
                queryParameters.Add("@Fk_DistrictId", req.DistrictId);
                queryParameters.Add("@CreatedBy", SessionManager.Fk_MemId);                
                queryParameters.Add("@ProcId", req.ProcID);
                queryParameters.Add("@KeyId", req.KeyId);
                _response = DBHelperDapper.DAAddAndReturnModel<FranchiseReqListResponse>("Proc_AllowFranchiseRegistration", queryParameters);
                if (_response.Status)
                {
                    string Message = string.Empty;
                    Message = _response.Msg;
                }
                return _response;
            }
            catch (Exception e)
            {
                return new FranchiseReqListResponse { Flag = 0, Status = false, Msg = e.Message, Response = "failed" };
            }
        }
        public static FranchiseReqListResponse AllowFranchiseRegistrationDetailById(FranchiseReqListResponse model)
        {
            try
            {
                FranchiseReqListResponse _iresult = new FranchiseReqListResponse();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@ProcId", 3);
                queryParameters.Add("@KeyId", model.KeyId);
                _iresult = DBHelperDapper.DAAddAndReturnModel<FranchiseReqListResponse>("Proc_AllowFranchiseRegistration", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}