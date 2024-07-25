using System.Data.SqlClient;
using System.Data;
using System;
using LifeOne.Models;
using System.Collections.Generic;

public class AdminGraph
{
    public decimal TotalCount { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public string ClosingDate { get; set; }
    public List<AdminGraph> lstData { get;  set; }
    public List<AdminGraph> lstData1 { get;  set; }

    public DataSet GetGraphData()
    {
        try
        {
            DataSet dataSet = new DataSet();
           
            dataSet = Connection.ExecuteQuery("GetGraphData");
            return dataSet;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataSet GetClosingTime()
    {
        try
        {
            DataSet dataSet = new DataSet();

            dataSet = Connection.ExecuteQuery("GetClosingTime");
            return dataSet;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}