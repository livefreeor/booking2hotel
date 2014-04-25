using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;
using System.Web.Configuration;
/// <summary>
/// Summary description for DropdownUtility
/// </summary>
/// 

public static class DropdownUtility
{
    public static string DropDownSQL(string strCommand,string ddName,int selectDefault)
    {
        string connString = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;
        using (SqlConnection cn = new SqlConnection(connString))
        {

            SqlCommand cmd = new SqlCommand(strCommand, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            string ddList = "<select id=\"" + ddName + "\" name=\"" + ddName + "\" runat=\"server\">\n";

            while (reader.Read())
            {
                ddList = ddList + "<option value=\"" + reader[0] + "\">" + reader[1] + "</option>\n";
            }
            ddList = ddList + "</select>\n";
            return ddList;
        }
        
    }

    public static string Number(string ddName,int numMin,int numMax,int numDefault)
    {
        string ddList = "<select id=\"" + ddName + "\" name=\"" + ddName + "\" runat=\"server\">\n";
        for (int count = numMin;count <= numMax; count++)
        {
            if(count==numDefault)
            {
                ddList = ddList + "<option value=\"" + count + "\" selected=\"selected\">" + count + "</option>\n";
            }else{
                ddList = ddList + "<option value=\"" + count + "\">" + count + "</option>\n";
            }
            
        }
        ddList = ddList + "</select>\n";
        return ddList;
    }

    public static string TeeOffTime(string ddName, int numMin, int numMax, int numDefault,int timeType)
    {
        string ddList = "<select id=\"" + ddName + "\" name=\"" + ddName + "\" runat=\"server\">\n";

        if (timeType == 1)
        {
            ddList = ddList + "<option value=\"\">HH</option>\n";
        }
        else {
            ddList = ddList + "<option value=\"\">MM</option>\n";
        }

        for (int count = numMin; count <= numMax; count++)
        {
            ddList = ddList + "<option value=\"" + count + "\">" + count + "</option>\n";
        }
        ddList = ddList + "</select>\n";
        return ddList;
    }

    public static string SmokeType(string ddName)
    {
        string ddList = "<select id=\"" + ddName + "\" name=\"" + ddName + "\" runat=\"server\">\n";
        ddList = ddList + "<option value=\"3\">No Preference</option>\n";
        ddList = ddList + "<option value=\"1\">Non-Smoking</option>\n";
        ddList = ddList + "<option value=\"2\">Smoking</option>\n";
        
        ddList = ddList + "</select>\n";

        return ddList;
    }
    public static string FloorType(string ddName)
    {
        string ddList = "<select id=\"" + ddName + "\" name=\"" + ddName + "\" runat=\"server\">\n";
        ddList = ddList + "<option value=\"3\">No Preference</option>\n";
        ddList = ddList + "<option value=\"1\">High Floor</option>\n";
        ddList = ddList + "<option value=\"2\">Low Floor</option>\n";
        
        ddList = ddList + "</select>\n";

        return ddList;
    }
    public static string BedType(string ddName)
    {
        string ddList = "<select id=\"" + ddName + "\" name=\"" + ddName + "\" runat=\"server\">\n";
        ddList = ddList + "<option value=\"3\">No Preference</option>\n";
        ddList = ddList + "<option value=\"1\">1 King size bed</option>\n";
        ddList = ddList + "<option value=\"2\">Twin beds</option>\n";
        
        ddList = ddList + "</select>\n";

        return ddList;
    }
    public static string CountryList(string ddName,Dictionary<byte, string> countryList)
    {
        string ddList = "<select id=\"" + ddName + "\" name=\"" + ddName + "\" class=\"required\">\n";
        ddList = ddList + "<option value=\"\">Select Country of Passport</option>\n";
        foreach (KeyValuePair<byte, string> country in countryList)
        {
            ddList = ddList + "<option value=\"" + country.Key + "\">" + country.Value + "</option>\n";
        }
        ddList = ddList + "</select>\n";
        return ddList;
    }
    
    public static string DestinationList(string title,string strCommand,int displayType)
    { 
        string connString = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;
        using (SqlConnection cn = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand(strCommand, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            string dropdown = "<select name=\"" + title + "\" id=\"" + title + "\">\n";
            //string dropdown = string.Empty;
            while (reader.Read())
            {
                dropdown = dropdown + "<option value=\"" + reader[0] + "\">" + reader[1] + "</option>\n";
            }
            dropdown = dropdown + "</select>\n";

            return dropdown;
        }
        
    }
    
}