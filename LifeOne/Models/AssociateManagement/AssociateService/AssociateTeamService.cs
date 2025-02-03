using LifeOne.Models.API;
using LifeOne.Models.AssociateManagement.AssociateDAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateService
{
    public class AssociateTeamService
    {
        public static List<DirectAPI> GetDirect(int? Fk_MemId,string SearchLoginId, int? Page, int PageSize)
        {
            List<DirectAPI> obj = DALAssociateTeam.GetDirect(Fk_MemId,SearchLoginId, Page, PageSize);
            return obj;
        }
        public static List<DirectAPI> GetDirectList(int? Fk_MemId, string SearchLoginId,string Status,string FromDate,string ToDate)
        {
            List<DirectAPI> obj = DALAssociateTeam.GetDirectList(Fk_MemId, SearchLoginId, Status, FromDate,ToDate);
            return obj;
        }
        
        public static List<DownlineAPI> GetDownline(int? Fk_MemId, string SearchLoginId, int? Page,int PageSize)
        {
            List<DownlineAPI> obj = DALAssociateTeam.GetDownline(Fk_MemId, SearchLoginId, Page,PageSize);
            return obj;
        }

        public static List<DownlineAPI> GetDownlineList(int? Fk_MemId, string SearchLoginId,string Status, string FromDate, string ToDate, int? Page, int PageSize,string Leg,string Pk_PackageID)
        {
            List<DownlineAPI> obj = DALAssociateTeam.GetDownlineList(Fk_MemId, SearchLoginId, Status, FromDate, ToDate, Page, PageSize, Leg, Pk_PackageID);
            return obj;
        }

        public static List<MemberKYC> GetMemberKYCListDownline(MemberKYC obj)
        {
            List<MemberKYC> objRes = new List<MemberKYC>();
            objRes = DALAssociateTeam.GetMemberKYCListDownline(obj);
            return objRes;
        }
        public static List<Member> GetMemberById(string LoginId)
        {
            List<Member> objRes = new List<Member>();
            objRes = DALAssociateTeam.GetMemberById(LoginId);
            return objRes;
        }
        public static List<FranchiseList> getFranchiseDetail(FranchiseList obj)
        {
            List<FranchiseList> objRes = new List<FranchiseList>();
            objRes = DALAssociateTeam.GetFranchiseDetail(obj);
            return objRes;
        }

        public static List<FranchiseList> getFranchiseDetailV2(FranchiseList obj)
        {
            List<FranchiseList> objRes = new List<FranchiseList>();
            objRes = DALAssociateTeam.GetFranchiseDetailV2(obj);
            return objRes;
        }
        public static List<DistributorDetails> DistributorDetails(DistributorDetails obj)
        {
            List<DistributorDetails> objRes = new List<DistributorDetails>();
            objRes = DALAssociateTeam.DistributorDetails(obj);
            return objRes;
        }
        

    }
}