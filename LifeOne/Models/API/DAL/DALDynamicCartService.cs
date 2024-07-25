using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;


namespace LifeOne.Models.API.DAL
{
    public class DALDynamicCartService
    {
        public static DataSet ReturnProductMasterAndCartInfromation(string _area, string _selectedPrdCode, string LanguageCode, string _Date)
        {
            try
            {
                SqlParameter[] para ={

                                   new SqlParameter("@CropCode",_selectedPrdCode),
                                   new SqlParameter("@Area",_area),
                                   new SqlParameter("@LanguageCode",LanguageCode),
                                   new SqlParameter("@Date",_Date),
                       };
                DataSet ds = DBHelper.ExecuteQuery("Proc_getCropDetailWithProduct", para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}