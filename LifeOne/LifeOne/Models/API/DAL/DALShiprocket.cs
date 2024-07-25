using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API.DAL
{
    public class DALShiprocket
    {
        public static ShiprocketViewModel ShiprocketOrderList(ShiprocketViewModel req)
        {
            ShiprocketViewModel _response = new ShiprocketViewModel();
            try
            {
                var QuaryParameter = new DynamicParameters();
                QuaryParameter.Add("@ShiprocketOrder", req.ShiprocketOrder);
                QuaryParameter.Add("@ShiprocketProductOrder", req.ShiprocketProductOrder);
                QuaryParameter.Add("@ShiprocketShipments", req.ShiprocketShipments);

                _response = DBHelperDapper.DAAddAndReturnModel<ShiprocketViewModel>("Proc_ShiprocketOrderList", QuaryParameter);
            }
            catch (Exception ex)
            {
                _response.Flag = 0;
                _response.Msg = ex.Message;
            }
            return _response;
        }
        public static ShiprocketViewModel GetShiprocketOrderID(string chnelOrdrId)
        {
            ShiprocketViewModel _response = new ShiprocketViewModel();
            try
            {
                var QuaryParameter = new DynamicParameters();
                QuaryParameter.Add("@channel_order_id", chnelOrdrId);
                _response = DBHelperDapper.DAAddAndReturnModel<ShiprocketViewModel>("Proc_ShipRocketOrderID", QuaryParameter);
            }
            catch (Exception ex)
            {
                _response.Flag = 0;
                _response.Msg = ex.Message;
            }
            return _response;
        }
    }
}