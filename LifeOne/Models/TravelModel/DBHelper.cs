using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.TravelModel
{
    public class DBHelper
    {
        public static string connectionstring = string.Empty;
        static DBHelper()
        {
            try
            {
                connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            }
            catch (Exception ex)
            {
            }
            
        }
        public static DataSet ExecuteQuery(string commandText, params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                using (var connection = new SqlConnection(connectionstring))
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

        public int ExecuteNonQuery(string commandText, params SqlParameter[] commandParameters)
        {
            int k = 0;
            using (var connection = new SqlConnection(connectionstring))
            using (var command = new SqlCommand(commandText, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(commandParameters);
                connection.Open();
                k = command.ExecuteNonQuery();
            }
            return k;
        }
    }
}