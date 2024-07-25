using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Web;
using System.Data.SqlClient;
using System.Threading;

namespace Karyon.Models.BillAvenueUtility.DAL
{

    public class DBHelperDapper
    {
        private static string connectionString = string.Empty;
        public static string connection()
        {
            try
            {
                return connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SubscriptionDB"].ToString();
            }
            catch (Exception)
            {
                //todo error handling  mechanism
                throw;
            }
        }

        /*getting details fro datatbase list*/
        public static List<TClass> DAGetDetailsInList<TClass>(string _qry)
        {
            using (SqlConnection con = new SqlConnection(connection()))
            {
                try
                {
                    IList<TClass> myList = SqlMapper.Query<TClass>(con, _qry).ToList();
                    return myList.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static TClass DAGetDetails<TClass>(string _qry)
        {
            using (SqlConnection con = new SqlConnection(connection()))
            {
                try
                {
                    TClass myList = SqlMapper.Query<TClass>(con, _qry).FirstOrDefault();
                    return myList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /*getting details*/
        //public async Task<List<TClass>> DAGetDetailsInListAsync<TClass>(string _qry)
        //{
        //    using (SqlConnection con = new SqlConnection(connection()))
        //    {
        //        try
        //        {
        //            IList<TClass> myList = SqlMapper.Query<TClass>(con, _qry).ToList();
        //            return await Task.Run(() => myList.ToList());
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}

        public static int DAAdd<T>(string Procname, T param)
        {
            int _iresult = 0;
            using (SqlConnection con = new SqlConnection(connection()))
            {
                try
                {
                    _iresult = con.Execute(Procname, param, commandType: System.Data.CommandType.StoredProcedure);
                    return _iresult;
                }
                catch (Exception ex)
                {
                    return _iresult;
                }
            }
        }

        public static int DAAdd(string Procname, DynamicParameters param)
        {
            int _iresult = 0;
            using (SqlConnection con = new SqlConnection(connection()))
            {
                try
                {
                    _iresult = con.Execute(Procname, param, commandType: System.Data.CommandType.StoredProcedure);
                    return _iresult;
                }
                catch (Exception ex)
                {
                    return _iresult;
                }
            }
        }
   

        public static TClass DAAddAndReturnModal<TClass>(string _qry, DynamicParameters param)
        {
            using (SqlConnection con = new SqlConnection(connection()))
            {
                try
                {
                    TClass myList = SqlMapper.Query<TClass>(con, _qry, param).FirstOrDefault();
                    return myList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}