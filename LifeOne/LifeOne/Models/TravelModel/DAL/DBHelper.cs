using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;



namespace LifeOne.Models.TravelModel.DAL
{
    public class DBHelper
    {
        public string Response { get; set; }

        private static string connectionString = string.Empty;
        public static string Aeskey = string.Empty;

        //public static string _dbCompanyId = string.Empty;

        static DBHelper()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                Aeskey = ConfigurationManager.AppSettings["AeskeyUtility"].ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int ExecuteNonQuery(string commandText, params SqlParameter[] commandParameters)
        {
            int k = 0;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(commandParameters);
                    connection.Open();
                    k = command.ExecuteNonQuery();
                }
                return k;
            }
            catch
            {
                return k;
            }
        }
        public static DataSet ExecuteQuery(string commandText, params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Code");
                dt.Columns.Add("Remark");

                DataRow dr = dt.NewRow();
                dr["Code"] = "0";
                dr["Remark"] = ex.Message;
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);

            }
            return ds;
        }
    }
}