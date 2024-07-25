using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALSupport
    {
        public static List<Support> SupportList(Support _model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@LoginId", _model.LoginId ?? string.Empty);
            queryParameters.Add("@Mobile", _model.Mobile ?? string.Empty);
            queryParameters.Add("@TicketNo", _model.TicketNumber ?? string.Empty);
            queryParameters.Add("@Name", _model.Name ?? string.Empty);
            queryParameters.Add("@Page", _model.Page);
            queryParameters.Add("@Size", _model.Size);
            queryParameters.Add("@UserType", _model.UserType);
            queryParameters.Add("@EmpFk_MemID", _model.EmpFk_MemID);
            //List<Support> _iresult = DBHelperDapper.DAAddAndReturnModelList<Support>("GetSupportList", queryParameters);
            List<Support> _iresult = DBHelperDapper.DAAddAndReturnModelList<Support>("Proc_SupportList", queryParameters);
            return _iresult;
        }
        
        public static List<SupportHistoryModel> BindAssociateMessageHistory(long Fk_SupportId)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Fk_SupportId", Fk_SupportId);
            List<SupportHistoryModel> _iresult = DBHelperDapper.DAAddAndReturnModelList<SupportHistoryModel>("Proc_RequestSupportList", queryParameters);
            return _iresult;
        }
        
        public static SupportHistoryModel CloseRequest(int SupportId)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@SupportId", SupportId);
            SupportHistoryModel _iresult = DBHelperDapper.DAAddAndReturnModel<SupportHistoryModel>("Proc_CloseSupportRequest", queryParameters);
            return _iresult;
        }

        public static SupportResponse SaveSupportReply(string Message, int id, string Path, long repliedby)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@id", id);
            queryParameters.Add("@Message", Message ?? string.Empty);
            queryParameters.Add("@repliedby", repliedby);
            queryParameters.Add("@Path", Path);
            SupportResponse _iresult = DBHelperDapper.DAAddAndReturnModel<SupportResponse>("SaveSupportReply", queryParameters);
            return _iresult;
        }

        public static SupportResponse SupportRequest(SupportRequest _model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Fk_SupportId", _model.Fk_SupportId);
            queryParameters.Add("@Fk_MemId", _model.Fk_MemId);
            queryParameters.Add("@Subject", _model.Subject);
            queryParameters.Add("@Message", _model.Message);
            queryParameters.Add("@CreatedBy", _model.Fk_MemId);
            queryParameters.Add("@ImageUrl", _model.DocumentPath);
            queryParameters.Add("Fk_MemLoginId", SessionManager.LoginId);
            //SupportResponse _iresult = DBHelperDapper.DAAddAndReturnModel<SupportResponse>("RequestSupport", queryParameters);
            SupportResponse _iresult = DBHelperDapper.DAAddAndReturnModel<SupportResponse>("Proc_RequestSupport", queryParameters);
            return _iresult;
        }

        public static SupportHistoryModel AddAssociateMessageHistory(SupportHistoryModel _model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Fk_SupportId", _model.SupportId);
            queryParameters.Add("@Fk_MemId", _model.Fk_MemId);
            queryParameters.Add("@Msg", _model.Msg);
            queryParameters.Add("@Login", _model.LoginId);
            queryParameters.Add("@Fk_MemLoginId", _model.Fk_MemLoginId);
            queryParameters.Add("@MsgBy", _model.MsgBy);
            SupportHistoryModel _iresult = DBHelperDapper.DAAddAndReturnModel<SupportHistoryModel>("Proc_AddSupportMsg", queryParameters);
            return _iresult;
        }

        public static Support_EmployeeListModel BindEmployeSupportList(string LoginId)
        {            
            return DBHelperDapper.BindEmployeSupportList(LoginId);            
        }

        public static SupportHistoryModel Assign_Employee(string xmldata)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@xmldata", xmldata);
            SupportHistoryModel _iresult = DBHelperDapper.DAAddAndReturnModel<SupportHistoryModel>("Proc_AssignEmployee", queryParameters);
            return _iresult;
        }
    }
}