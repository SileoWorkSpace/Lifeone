using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FEntity
{
    public class MFranchiseProfile
    {
        public long PKFranchiseRegistrationId { get; set; }
        #region ParaForChangePassword
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }  
        public string Normalpassword { get; set; }
        #endregion

        #region ParaForProfile
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string PersonName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Mobile { get; set; }
        public string DOB { get; set; }
        public string FatherName { get; set; }
        public string PAddress { get; set; }
        public string Ppincode { get; set; }
        public string Pstate { get; set; }
        public string Pcity { get; set; }
        public string CAddress { get; set; }
        public string Cpincode { get; set; }
        public string Cstate { get; set; }
        public string Ccity { get; set; }        
        public string Issame { get; set; }
        public List<MFranchiseProfile> objList { get; set; }

        #endregion

    }
    public class ResponseFranchiseProfileModel
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
    }
}