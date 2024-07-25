using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class KYC
    {
        public string response { get; set; }
        public string filepath { get; set; }
        public string msg { get; set; }
    }
    public class UploadKYC
    {
        public string response { get; set; }
        public string pancard { get; set; }
        public string aadharfront { get; set; }
        public string aadharback { get; set; }
        public string cheque { get; set; }

        public string profilepic { get; set; }

        public string type { get; set; }
        public string fk_memId { get; set; }
    }
    public class MemberKYC
    {
        public string Name { get; set; }
        public string LoginId { get; set; }
        public string Mobile { get; set; }
        public bool IsAadharApproved { get; set; }
        public bool IsAadharReject { get; set; }
        public string AdhaarCardAttachBack { get; set; }
        public string IsApproveAddressBackProof { get; set; }
        public string IsAddressBackProofReject { get; set; }
        public string AddressProofRejectRemark { get; set; }
        public string AddressProofBackRejectRemark { get; set; }
        public string AdhaarCardAttach { get; set; }
        public long TotalRecords { get; set; }
        public string JoiningDate { get; set; }
        public int Page { get; set; }
        public int size { get; set; }
        public Pager Pager { get; set; }
        public string AdharCardNumber { get; set; }
        public List<MemberKYC> List { get; set; }
        public string response { get; set; }
        public int Code { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public int TotalRecord { get; set; }
        public string date { get; set; }
        public string PanCard { get; set; }
        public string PanCardURL { get; set; }
        public string PanCardRejectRemark { get; set; }
        public string BankDocumentURL { get; set; }
        public string BankRejectRemark { get; set; }
        public string AdharCardRejectRemark { get; set; }
        public bool ApproveKyc { get; set; }
        public bool RejectKyc { get; set; }
        public string AddressFrontUrl { get; set; }
        public string AddressBackUrl { get; set; }
        public bool IsAddressApproved { get; set; }
        public bool IsAddressReject { get; set; }
        public bool IsPanApproved { get; set; }
        public bool IsPanCardReject { get; set; }
        public bool IsBankReject { get; set; }
        public bool IsBankApproved { get; set; }
        public string MemberAccNo { get; set; }
    }
    public class VerifyPanOrVoterid
    {
        public string Name { get; set; }
        public string LoginId { get; set; }
        public string Mobile { get; set; }
        public long TotalRecords { get; set; }
        public string JoiningDate { get; set; }
        public int Page { get; set; }
        public int size { get; set; }
        public Pager Pager { get; set; }
        public List<VerifyPanOrVoterid> List { get; set; }
        public string response { get; set; }
        public int Flag { get; set; }
        public string Msg { get; set; }
        public string Remark { get; set; }
        public int TotalRecord { get; set; }
        public string date { get; set; }
        public string PanCard { get; set; }
        public string PanCardRejectRemark { get; set; }
        public string Type { get; set; }
        public string VoterIdNo { get; set; }
        public string VoterIdImage { get; set; }
        public bool Iskyc { get; set; }
        public long Fk_MemId { get; set; }
        public string Status { get; set; }
        public string VerifyStatus { get; set; }
        public string GSTNo { get; set; }
        public string GSTImage { get; set; }
        public string AddressProofStatus { get; set; }
        public string AddressProofBackStatus { get; set; }
        public string BankStatus { get; set; }
        public string PanCardStatus { get; set; }
        public string IsPanCardRejected { get; set; }
        public string IsPanApproved { get; set; }
        public int Rownumber { get; set; }
        public string IfscCode { get; set; }
        public string BankImage { get; set; }
        public string AccountHolder { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string AadharNumber { get; set; }
        public string AadharImage { get; set; }
        public string AadharBackImage { get; set; }
        public string PanImage { get; set; }
        public string IsAadharApproved { get; set; }
        public string IsAadharBackApproved { get; set; }
        public string IsBankApproved { get; set; }
        public long AddedBy { get; set; }



    }
    public class FileUploadResponse
    {
        public string response { get; set; }
        public string filepath { get; set; }
        public string msg { get; set; }
    }
}