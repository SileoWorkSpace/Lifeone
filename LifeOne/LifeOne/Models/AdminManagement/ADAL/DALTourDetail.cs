using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.API;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALTourDetail
    {
        public static TourDetailViewModel TourDetailList(TourDetailViewModel model)
        {
            try
            {
                TourDetailViewModel _iresult = new TourDetailViewModel();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@LoginId", model.LoginId);
                queryParameters.Add("@Mobile", model.Mobile);
                queryParameters.Add("@Name", model.Name);
                queryParameters.Add("@Size", SessionManager.Size);
                queryParameters.Add("@Page", model.Page == null ? 1 : model.Page);
                _iresult.TourDetailList = DBHelperDapper.DAAddAndReturnModelList<TourDetailViewModel>("Proc_TourRequestList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static TourDetailViewModel ApproveMemberTour(TourDetailViewModel model)
        {
            try
            {
                TourDetailViewModel _iresult = new TourDetailViewModel();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Pk_TourID", model.Tour_PKid);
                _iresult.TourDetailList = DBHelperDapper.DAAddAndReturnModelList<TourDetailViewModel>("Proc_TourPassengerDetail", queryParameters);
                //var data=_iresult.TourDetailList.GroupBy(x=>x.Tour_PKid).Select
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static TourDetailViewModel APITourDetail(TourDetailViewModel model)
        {
            try
            {
                TourDetailViewModel _iresult = new TourDetailViewModel();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Fk_MemId", model.Fk_MemId);
                _iresult.TourDetailList = DBHelperDapper.DAAddAndReturnModelList<TourDetailViewModel>("Proc_APITourPassengerDetail", queryParameters);
                //var data=_iresult.TourDetailList.GroupBy(x=>x.Tour_PKid).Select
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static TourDetailViewModel ApproveMemberTourRequest(TourDetailViewModel model)
        {
            try
            {
                TourDetailViewModel _iresult = new TourDetailViewModel();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Pk_TourID", model.Tour_PKid);
                queryParameters.Add("@BookingAmount", model.BookingAmount);
                queryParameters.Add("@Approval_Remark", model.Approval_Remark);
                queryParameters.Add("@ApprovedBy", Convert.ToInt32(SessionManager.Fk_MemId));
                _iresult= DBHelperDapper.DAAddAndReturnModel<TourDetailViewModel>("Proc_UpdateMemberTour", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static TourDetailViewModel DeclinedMemberTourRequest(TourDetailViewModel model)
        {
            try
            {
                TourDetailViewModel _iresult = new TourDetailViewModel();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Pk_TourID", model.Tour_PKid);
                queryParameters.Add("@BookingAmount", model.BookingAmount);
                queryParameters.Add("@Approval_Remark", model.Approval_Remark);
                queryParameters.Add("@ApprovedBy", Convert.ToInt32(SessionManager.Fk_MemId));
                _iresult = DBHelperDapper.DAAddAndReturnModel<TourDetailViewModel>("Proc_DeclinedMemberTour", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static TourDetailViewModel AddTourRequest(AddTourDetailViewModel model)
        {
            try
            {
                string xml = "<result>";
                if (model.PassengerList != null && model.PassengerList.Count > 0)
                {
                    model.PassengerList.ForEach(x =>
                    {
                        xml += "<data>" +
                            "<Name>" + x.Name + "</Name>" +
                            "<Age>" + x.Age + "</Age>" +
                            "<Gender>" + x.Gender + "</Gender>" +
                            "<PassengerType>" + x.PassengerType + "</PassengerType>" +
                            "</data>";
                    });
                    xml+= "</result>";
                }

                TourDetailViewModel _iresult = new TourDetailViewModel();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FK_MemId", model.FK_MemId);
                queryParameters.Add("@Destination", model.Destination);
                queryParameters.Add("@From_Dt", model.From_Dt);
                queryParameters.Add("@To_Dt", model.To_Dt);
                queryParameters.Add("@TotalPassenger", model.TotalPassenger);
                queryParameters.Add("@Hotel_Category", model.Hotel_Category);
                queryParameters.Add("@Domestic_International", model.TourType);
                queryParameters.Add("@Return_Air_tour", model.Return_Air_tour);
                queryParameters.Add("@Adults", model.Adults);
                queryParameters.Add("@Child_below_12Years", model.Infants);
                queryParameters.Add("@Child_above_12Years", model.Children);
                queryParameters.Add("@TotalDays", model.TotalDays);
                queryParameters.Add("@TotalNights", model.TotalNights);
                queryParameters.Add("@Your_Budget", model.Your_Budget);
                queryParameters.Add("@Source_Place", model.Source_Place);
                queryParameters.Add("@xmldata", xml);

                _iresult = DBHelperDapper.DAAddAndReturnModel<TourDetailViewModel>("Proc_AddTourRequest", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static TourDetailViewModel ExportToExcelTourRequests(TourDetailViewModel model)
        {
            try
            {
                TourDetailViewModel _iresult = new TourDetailViewModel();
                var queryParameters = new DynamicParameters();
                //queryParameters.Add("@LoginId", model.LoginId);
                //queryParameters.Add("@Mobile", model.Mobile);
                //queryParameters.Add("@Name", model.Name);
                //queryParameters.Add("@Size", SessionManager.Size);
                //queryParameters.Add("@Page", model.Page == null ? 1 : model.Page);
                _iresult.TourDetailList = DBHelperDapper.DAAddAndReturnModelList<TourDetailViewModel>("ExportToExcelTourRequests", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public static List<TourDestination> TourDestinationList()
        {
            try
            {
                
                List<TourDestination> DestinationList = DBHelperDapper.DAGetDetailsInList<TourDestination>("Proc_Destination");
                return DestinationList;

            }
            catch(Exception ex)
            {

                throw ex;
            }



        }
        public static TourOnlinePayment TourOnlinePayment(TourOnlinePayment model)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Fk_MemId", model.Fk_MemId);
                parameter.Add("@TourId", model.TourId);
                parameter.Add("@PaymentAmount", model.PaidAmount);
                parameter.Add("@TransactionNo", model.TransactionNo);
                parameter.Add("@PStatus", model.Status);
                parameter.Add("@PaymentDate", model.PaymentDate);
                parameter.Add("@PaymentId", model.PaymentId);
                TourOnlinePayment obj = DBHelperDapper.DAAddAndReturnModel<TourOnlinePayment>("Proc_FamilynTourTransaction", parameter);
                return obj;

            }
            catch(Exception ex)
            {

                throw ex;
            }


        }
    }
}