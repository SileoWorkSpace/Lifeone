using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class AdminKYCService
    {

        public List<MemberKYC> GetMemberKYCList(MemberKYC _obj)
        {
            List<MemberKYC> _objResponseModel = new List<MemberKYC>();
            try
            {
                _objResponseModel = AdminDALKYC.GetMemberKYCList(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public List<MemberKYC> ApprveDeclineKYC(MemberKYC _obj)
        {
            List<MemberKYC> _objResponseModel = new List<MemberKYC>();
            try
            {
                _objResponseModel = AdminDALKYC.ApprveDeclineKYC(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public List<VerifyPanOrVoterid> GetMemberPanOrVoterId(VerifyPanOrVoterid _obj)
        {
            List<VerifyPanOrVoterid> _objResponseModel = new List<VerifyPanOrVoterid>();
            try
            {
                _objResponseModel = AdminDALKYC.GetMemberPanOrVoterID(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public VerifyPanOrVoterid ApprovePanOrVoterId(VerifyPanOrVoterid _obj)
        {
            VerifyPanOrVoterid _objResponseModel = new VerifyPanOrVoterid();
            try
            {
                _objResponseModel = AdminDALKYC.ApprovePanOrVoterId(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}