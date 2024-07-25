using LifeOne.Models.FranchiseManagement.FDAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.FranchiseManagement.FEntity;

namespace LifeOne.Models.FranchiseManagement.FService
{
  

    public class FranchiseDashboardService
    {

        //public dynamic AdminDashboard()
        //{

        //    dynamic result;
        //    try
        //    {
        //        result = DBHelperDapper.GetAdminDashboard();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex;
        //    }
        //}
        public static List<MAdminDashbord> InvokeDashbordDetails(MAdminDashbord _model)
        {

            List<MAdminDashbord> obj = DALFranchiseDashboard.GetDashboardList(_model);
            return obj;
        }

        public static List<MAdminDashbord> InvokeRecentJoiners(MAdminDashbord _model)
        {

            List<MAdminDashbord> obj = DALFranchiseDashboard.GetRecentList(_model);
            return obj;
        }

        public static List<AdminDashboardChart> InvokeMemberJoiners(MAdminDashbord _model)
        {

            List<AdminDashboardChart> obj = DALFranchiseDashboard.GetMemberList(_model);
            return obj;
        }
        public static List<MemberChart> InvokeDatewiseJoiners(MAdminDashbord _model)
        {

            List<MemberChart> obj = DALFranchiseDashboard.GetDateWiseMemberList(_model);
            return obj;
        }
        public static FranchiseDashboardViewModel FranchiseDahsboard()
        {

            FranchiseDashboardViewModel obj = DALFranchiseDashboard.FranchiseDahsboard();
            return obj;
        }
      

    }


}