using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateEntity
{
    public class TreeRequest
    {
        public string LoginID { get; set; }
        public string SessionLoginId { get; set; }
    }

    public class TreeModel
    {
        public string LoginId { get; set; }
        public string MemberName { get; set; }
        public string SponsorId { get; set; }
        public string JoiningDate { get; set; }
        public string SearchLoginId { get; set; }
        public string ParentId { get; set; }
        public int MemId { get; set; }
        public string SponsorLoginId { get; set; }
        public string Status { get; set; }
        public int LeftNoneActive { get; set; }
        public int RightNoneActive { get; set; }
        public int ActiveTotalLeft { get; set; }
        public int InactiveTotalLeft { get; set; }
        public int ActiveTotalRight { get; set; }
        public int InactiveTotalRight { get; set; }
        public int TotalActiveInActiveLeftRight { get; set; }


        public string ActiveInActiveIcon { get; set; }
        public string Leg { get; set; }
        public int Level { get; set; }
        public string IsLeftLeg { get; set; }
        public string IsRightLeg { get; set; }
        public int TotalNonActive { get; set; }
        public string MemberStatus { get; set; }
        public string UpGradeDate { get; set; }
        public int AllLeg1 { get; set; }
        public int AllLeg2 { get; set; }
        public int PermanentLeg1 { get; set; }
        public int PermanentLeg2 { get; set; }
        public int TotalActive { get; set; }
        public decimal LeftBP { get; set; }
        public decimal RightBP { get; set; }
        public decimal PaidLeftBP { get; set; }
        public decimal PaidRightBP { get; set; }
        public decimal TotalBusiness { get; set; }
        public string ProfilePic { get; set; }
        public decimal TotalBV { get; set; }
        public string ParentLoginId { get; set; }
        public string Url { get; set; }
        public string PackageName { get; set; }
        public string PerformanceLevel { get; set; }

        public List<TreeModel> TreeList { get; set; }




    }
    public class Support_EmployeeRequestModel
    {
        public string LoginID { get; set; }        
    }
}