using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.TravelModel.Entity;
using LifeOne.Models.TravelModel.API;
using LifeOne.Models.TravelModel.DAL;
using System.Data;

namespace LifeOne.Models.TravelModel.Service
{
    public class BookingHistoryService
    {
        public ResponseModel BookingHistoryAPIService(RequestModel _obj)
        {
            DALCommon _objDALCommon = new DALCommon();
            try
            {
                List<BusModel.GetBookingHistorydetail> objGetDepartment = new List<BusModel.GetBookingHistorydetail>();
                BusModel.BookingStatus objDepartmentStatus = new BusModel.BookingStatus();
                BookingParameter _param = DALCommon.CustomDeserializeObjectWithDecryptString<BookingParameter>(_obj.body);
                DataSet dsResult = DALBookingHistory.GetBookingHistory(_param);
                try
                {
                    if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                    {
                        #region User Status
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {
                            objDepartmentStatus.Response = "Success";
                            objDepartmentStatus.GetBookingHistorydetail = objGetDepartment;
                        }
                        #endregion
                        foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                        {
                            objGetDepartment.Add(new BusModel.GetBookingHistorydetail
                            {
                                Remarks = row1["Remarks"].ToString(),
                                SeatNo = row1["SeatNo"].ToString(),
                                SeatAmt = row1["SeatAmt"].ToString(),
                                SeatCode = row1["SeatCode"].ToString(),
                                PassengerId = row1["PassengerId"].ToString(),
                                Pk_FlightResponseId = row1["Pk_FlightResponseId"].ToString(),
                                HermesPNR = row1["HermesPNR"].ToString(),
                                AirlineName = row1["AirlineName"].ToString(),
                                AirlinePNR = row1["AirlinePNR"].ToString(),
                                BookingType = row1["BookingType"].ToString(),
                                Origin = row1["Origin"].ToString(),
                                Destination = row1["Destination"].ToString(),
                                BoookingDate = row1["BoookingDate"].ToString(),
                                AirlineCode = row1["AirlineCode"].ToString(),
                                TerminalName = row1["TerminalName"].ToString(),
                                TotalAMount = row1["TotalAMount"].ToString(),
                                DepartingDate = row1["DepartingDate"].ToString(),
                                UserTrackId = row1["UserTrackId"].ToString(),
                            });
                        }
                    }
                    else
                    {
                        objDepartmentStatus.Response = "Data Not Found";
                    }
                }
                catch (Exception ex)
                {
                    objDepartmentStatus.Response = "Error";
                }

                /*encrypt and return API response*/
                ResponseModel __rsponsedetails = new ResponseModel();
                __rsponsedetails.body = DALCommon.CustomSerializeObjectWithEncryptString<BusModel.BookingStatus>(objDepartmentStatus);
                return __rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}