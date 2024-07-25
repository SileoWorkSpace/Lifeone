using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using LifeOne.Models.TravelModel.Entity;


namespace LifeOne.Models.TravelModel.DAL
{
    public class DALCancelTicket
    {
        public static DataSet SaveCancelRequestAndResponse(RequestModel _objRequestModel, FlightAPI.CancelTicketReponse _objResponseModel)
        {
            try
            {
                DataSet ds = null;
                FlightAPI.RootObjectTicket obj = DALCommon.CustomDeserializeObjectWithDecryptString<FlightAPI.RootObjectTicket>(_objRequestModel.body);
                BookedHistory objcancel = new BookedHistory();
                objcancel.HermesPNR = obj.CancellationInput.HermesPNR;
                objcancel.AirlinePNR = obj.CancellationInput.AirlinePNR;
                objcancel.CancellationType = "FULL";
                if (_objResponseModel.ResponseStatus == "1")
                {
                    objcancel.Remarks = _objResponseModel.CancellationOutput.CancellationDetails.FullCancellationRemarks;
                    ds = objcancel.SaveCancelTicket();
                }
                else
                {
                    objcancel.Remarks = _objResponseModel.FailureRemarks;
                }                
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }      
    }
}