using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class SupportService
    {

        public List<Support> GetSupportList(Support _obj)
        {
            List<Support> _objResponseModel = new List<Support>();
            try
            {
                _objResponseModel = DALSupport.SupportList(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public List<SupportHistoryModel> BindAssociateMessageHistory(long Fk_SupportId)
        {
            List<SupportHistoryModel> _objResponseModel = new List<SupportHistoryModel>();
            try
            {
                _objResponseModel = DALSupport.BindAssociateMessageHistory(Fk_SupportId);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }


        public SupportHistoryModel CloseRequest(int SupportId)
        {
            SupportHistoryModel _objResponseModel = new SupportHistoryModel();
            try
            {
                if (SupportId > 0)
                    _objResponseModel = DALSupport.CloseRequest(SupportId);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public SupportHistoryModel AddAssociateMessageHistory(SupportHistoryModel _obj)
        {
            SupportHistoryModel _objResponseModel = new SupportHistoryModel();
            try
            {
                _objResponseModel = DALSupport.AddAssociateMessageHistory(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public SupportResponse SupportRequest(SupportRequest _obj)
        {
            SupportResponse _objResponseModel = new SupportResponse();
            try
            {
                _objResponseModel = DALSupport.SupportRequest(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public SupportResponse SaveSupportReply(string Message, int id, string Path, long repliedby)
        {
            SupportResponse _objResponseModel = new SupportResponse();
            try
            {
                _objResponseModel = DALSupport.SaveSupportReply(Message, id, Path, repliedby);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public Support_EmployeeListModel BindEmployeSupportList(string LoginId)
        {
            Support_EmployeeListModel _objResponseModel = new Support_EmployeeListModel();
            try
            {
                _objResponseModel = DALSupport.BindEmployeSupportList(LoginId);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public SupportHistoryModel Assign_Employee(string xmldata)
        {
            SupportHistoryModel _objResponseModel = new SupportHistoryModel();
            try
            {
                _objResponseModel = DALSupport.Assign_Employee(xmldata);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}