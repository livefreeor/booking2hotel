using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using System.Data.SqlClient;
using System.Web.Configuration;

/// <summary>
/// Summary description for IPtoCounrty
/// </summary>
public static class IPtoCounrty
{
    public static short GetCountryID(string IPReferer)
    {
        short result = 0;
        string ipRefer = IPReferer;
        string[] ipReferSplit = ipRefer.Split('.');
        string connString = WebConfigurationManager.ConnectionStrings["booking2hotelXConnectionString"].ConnectionString;
        string sqlCommand = "spIPtoCountry " + ipReferSplit[0] + "," + ipReferSplit[1] + "," + ipReferSplit[2] + "," + ipReferSplit[3];
        using (SqlConnection cn = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand(sqlCommand, cn);
            cn.Open();
            result = (short)cmd.ExecuteScalar();

        }
        return result;
    }
}