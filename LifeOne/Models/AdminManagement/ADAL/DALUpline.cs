using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Manager;
namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALUpline
    {
        public static List<MUpline> DalUpline(MUpline _model)
        {
            try
            {
                List<MUpline> _iresult = new List<MUpline>();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@LoginId", _model.LoginId);
                queryParameters.Add("@Page", _model.Page);
                queryParameters.Add("@PageSize", SessionManager.Size);
                _iresult = DBHelperDapper.DAAddAndReturnModelList<MUpline>("Proc_UplineReport", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}