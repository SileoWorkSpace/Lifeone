using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateDAL
{
    public class DALKYC
    {

        public static KYCResponse SaveKYCDetails(KYCDetails obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Fk_MemId", obj.Fk_MemId);
                queryParameters.Add("@profilepic", obj.ProfilePic);
                queryParameters.Add("@pancard", obj.PanImage);
                queryParameters.Add("@aadharfront", obj.aadharfrontimagepath);
                queryParameters.Add("@aadharback", obj.aadharbackimagepath);
                queryParameters.Add("@cheque", obj.ChequeBookImg);
                queryParameters.Add("@type", obj.Action);
                queryParameters.Add("@PanCardNo", obj.PanCard??string.Empty);
                queryParameters.Add("@MemberAccNo", obj.MemberAccNo??string.Empty);
                queryParameters.Add("@AddressProofNo", obj.AddressProofNo?? string.Empty);
                queryParameters.Add("@Ifsc", obj.Ifsc ?? string.Empty);
                queryParameters.Add("@BankName", obj.BankName ?? string.Empty);
                KYCResponse _iresult = DBHelperDapper.DAAddAndReturnModel<KYCResponse>("UploadKYC", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<KYCDetails> GetKYCInfo(KYCDetails _model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Fk_MemId", _model.Fk_MemId);
             List<KYCDetails> _iresult = DBHelperDapper.DAAddAndReturnModelList<KYCDetails>("GetMemberKYCListAPI", queryParameters);
            return _iresult;
        }
    }
}