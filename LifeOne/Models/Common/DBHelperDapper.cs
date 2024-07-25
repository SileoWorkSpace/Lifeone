using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Web;
using System.Data.SqlClient;
using System.Threading;
using DocumentFormat.OpenXml.Office.Word;

namespace LifeOne.Models.Common
{

    public class DBHelperDapper
    {
        private static string connectionString = string.Empty;
        public static string connection()
        {
            try
            {
                return connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
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
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    else
                    {
                        con.Open();
                    }
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
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    else
                    {
                       
                      
                        con.Open();
                    }
                    TClass myList = SqlMapper.Query<TClass>(con, _qry).FirstOrDefault();
                    return myList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
      

        public static int DAAdd<T>(string Procname, T param)
        {
            int _iresult = 0;
            using (SqlConnection con = new SqlConnection(connection()))
            {
                try
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    else
                    {
                        con.Open();
                    }
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
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    else
                    {
                        con.Open();
                    }
                    _iresult = con.Execute(Procname, param, commandType: System.Data.CommandType.StoredProcedure);
                    return _iresult;
                }
                catch (Exception ex)
                {
                    return _iresult;
                }
            }
        }

        public static TClass DAAddAndReturnModel<TClass>(string _procame, DynamicParameters param)
        {
            TClass _objMOdel;
          
                try
                {
                    using (SqlConnection objConnection = new SqlConnection(connection()))
                    {
                    if (objConnection.State == System.Data.ConnectionState.Open)
                    {
                        objConnection.Close();
                    }
                    else
                    {
                        objConnection.Open();
                    }
                    _objMOdel = SqlMapper.Query<TClass>(objConnection, _procame, param, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                    }
                    return _objMOdel;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            
        }


        public static List<T> DAAddAndReturnModelList<T>(string spName, DynamicParameters p)
        {
            List<T> recordList = new List<T>();
            using (SqlConnection objConnection = new SqlConnection(connection()))
            {
                objConnection.Open();
                recordList = SqlMapper.Query<T>(objConnection, spName, p, commandType: System.Data.CommandType.StoredProcedure).ToList();
                objConnection.Close();
            }
            return recordList;
        }

    }
}