using LifeOne.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateEntity
{
    public class KYCDetails
    {
        public string PanNumber { get; set; }
        public string AdharNo { get; set; }
        public string AccountNo { get; set; }
        public string ChequeNo { get; set; }
        public string Branch { get; set; }
        public string BankName { get; set; }
        public string Ifsc { get; set; }
        public string ProfilePic { get; set; }
        public string profileimagepath { get; set; }
        public string pancardimagepath { get; set; }
        public string PanImage { get; set; }
        public string PanCardUrl { get; set; }
        public string AdharImg { get; set; }
        public string AdharBackImg { get; set; }
        public string ChequeBookImg { get; set; }
        public long Fk_MemId { get; set; }
        public string MemId { get; set; }
        public string Fk_MemberReqId { get; set; }
        public long CreatedBy { get; set; }
        public string Status { get; set; }
        public string PanStatus { get; set; }
        public string AdharStatus { get; set; }
       
        public string ChequeStatus { get; set; }
        public string addProofImg { get; set; }
        public string AddressStatus { get; set; }
        public string AddressBackStatus { get; set; }
        public string Action { get; set; }
        public string LoginId { get; set; }
        public string IsDeleted { get; set; }
        public string Display { get; set; }
        public string AddressFrontUrl { get; set; }
        public string AddressBackUrl { get; set; }
        public string BankDocumentURL { get; set; }
        public string aadharfrontimagepath { get; set; }
        public string aadharbackimagepath { get; set; }
        public string chequeimagepath { get; set; }
        public string AadharStatus { get; set; }

        public string BankStatus { get; set; }
        public string ProfileStatus { get; set; }

        public string PanCard { get; set; }
        public string AddressProofNo { get; set; }
        public string MemberAccNo { get; set; }

        public KYCDetails KycDetail { get; set; }
        public List<KYCDetails> lstKyc { get; set; }
        public List<MemberKYC> lstKycDown { get; set; }
    }

    public class KYCResponse
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
    }
}