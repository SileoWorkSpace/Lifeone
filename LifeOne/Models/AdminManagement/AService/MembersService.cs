using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class MembersService
    {

        public static List<MMemberList> InvokeMemberDetails(MMemberList _model)
        {

            List<MMemberList> obj = DALMembers.GetMemberList(_model);
            return obj;
        }
        public static List<MMemberList> DownloadMemberList()
        {

            List<MMemberList> obj = DALMembers.DownloadMemberList();
            return obj;
        }
        

        public static MembersResponse MemberAssociateByAdmin(long FK_MemId, bool status)
        {

            MembersResponse obj = DALMembers.MemberAssociateByAdmin(FK_MemId, status);
            return obj;
        }

        public static MembersResponse BlockRefereral(long FK_MemId, bool Status, string remark)
        {
            MembersResponse obj = DALMembers.DalBlockReffereal(FK_MemId, Status, remark);
            return obj;
        }

        //public static List<MMembes> GetMemberById(string LoginId)
        //{

        //    List<MMembes> obj = DALMembers.GetMemberById(LoginId);
        //    return obj;
        //}

        public static List<MMemberList> GetRecentMemberList(MMemberList _model)
        {

            List<MMemberList> obj = DALMembers.GetRecentMemberList(_model);
            return obj;
        }
        public static List<MemberTestimonialModel> GetTestimonialDetail(MemberTestimonialModel _model, int i = 0)
        {
            List<MemberTestimonialModel> obj = new List<MemberTestimonialModel>();
            if (i >= 1)
                obj = DALMembers.ExportToExcelTestimonials(_model);
            else
                obj = DALMembers.GetTestimonialDetail(_model);
            return obj;
        }

        public static AddUpdateDeleteModel SaveTestimonialStatus(MemberTestimonialModel _model)
        {
            AddUpdateDeleteModel obj = DALMembers.SaveTestimonialStatus(_model);
            return obj;
        }
        public static MembersResponse UpdateMemberTestimonialStatus(long FK_MemId, bool status)
        {
            MembersResponse obj = DALMembers.UpdateMemberTestimonialStatus(FK_MemId, status);
            return obj;
        }

        public static List<TestimonialPermissionViewModel> TestimonialPermissionLeaders(TestimonialPermissionViewModel _model)
        {
            return DALMembers.TestimonialPermissionLeaders(_model);
        }
    }
}