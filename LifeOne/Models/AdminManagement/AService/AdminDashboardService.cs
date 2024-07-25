using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Spreadsheet;

namespace LifeOne.Models.AdminManagement.AService
{
    public class AdminDashboardService
    {
        public dynamic AdminDashboard()
        {

            dynamic result;
            try
            {
                result = DBHelperDapper.GetAdminDashboard();
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public DataSet GetAdminDashboard()
        {
            try
            {
               
                DataSet ds = Connection.ExecuteQuery("Proc_AdminDashboard");
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<MAdminDashbord> InvokeDashbordDetails(MAdminDashbord _model)
        {

            List<MAdminDashbord> obj = DALAdminDashboard.GetDashboardList(_model);
            return obj;
        }

        public static List<MAdminDashbord> InvokeRecentJoiners(MAdminDashbord _model)
        {

            List<MAdminDashbord> obj = DALAdminDashboard.GetRecentList(_model);
            return obj;
        }

        public static List<AdminDashboardChart> InvokeMemberJoiners(MAdminDashbord _model)
        {

            List<AdminDashboardChart> obj = DALAdminDashboard.GetMemberList(_model);
            return obj;
        }
        public static List<MemberChart> InvokeDatewiseJoiners(MAdminDashbord _model)
        {

            List<MemberChart> obj = DALAdminDashboard.GetDateWiseMemberList(_model);
            return obj;
        }
        public static FranchiseDashboardViewModel FranchiseDahsboard()
        {

            FranchiseDashboardViewModel obj = DALAdminDashboard.FranchiseDahsboard();
            return obj;
        }
        public dynamic UtilityPaymentList()
        {

            dynamic result;
            try
            {
                result = DBHelperDapper.UtilityPaymentList();
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}