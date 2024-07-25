using Dapper;
using LifeOne.Models.BillAvenueUtility.Entity.PrepaidRecharge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.Common;


namespace LifeOne.Models.BillAvenueUtility.DAL
{
    public class DALPrepaidCircle
    {

        public static  List<MPrepaidCircle> GetAllCircle()
        {
            try
            {
                List<MPrepaidCircle> _iresult = new List<MPrepaidCircle>();                
                _iresult = DBHelperDapper.DAGetDetailsInList<MPrepaidCircle>("GetAllPrepaidCircle");
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}