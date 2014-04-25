using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.UI;
using System.Configuration;


public partial class test_latitude : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {

           string _connectionString = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;

            using(SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT product_id, product_code, title, coor_lat ,coor_long  FROM tbl_product WHERE  coor_lat IS NOT NULL  AND coor_long IS NOT NULL AND coor_lat <> '' AND coor_long <> ''", cn);
                cn.Open();
                IDataReader reader = cmd.ExecuteReader();
                Regex reg = new Regex("^-?[0-9.]+$");
                while (reader.Read())
                {
                    //if (decimal.Parse(reader[2].ToString().Split('.')[0]) > decimal.Parse(reader[3].ToString().Split('.')[0]))
                    //{
                    if (decimal.Parse(reader["coor_lat"].ToString().Split('.')[0]) > decimal.Parse(reader["coor_long"].ToString().Split('.')[0]))
                    {
                        Response.Write(reader["product_id"].ToString() + "---" + reader["product_code"].ToString() + "---" + reader["title"].ToString() + "---" + reader["coor_lat"].ToString() + "---" + reader["coor_long"].ToString() + "<br/>");
                        Response.Flush();
                    }
                        
                    //}
                    
                }
            }

        }
    }
}