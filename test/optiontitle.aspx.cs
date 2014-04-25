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
using System.Configuration;


public partial class test_optiontitle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            string _connectionString = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT  p.product_code,p.title As ptitle,opc.title FROM tbl_product_option_content opc , tbl_product p , tbl_product_option op WHERE op.option_id = opc.option_id AND op.product_id = p.product_id AND opc.lang_id = 2", cn);
                cn.Open();
                IDataReader reader = cmd.ExecuteReader();
                Regex reg = new Regex("^-?[0-9.]+$");
                while (reader.Read())
                {
                    //if (decimal.Parse(reader[2].ToString().Split('.')[0]) > decimal.Parse(reader[3].ToString().Split('.')[0]))
                    //{
                    if (reader["title"].ToString().IndexOf("อาหารเช้า") > 0)
                    {
                        Response.Write(reader["product_code"].ToString() + "---" + reader["ptitle"].ToString() + "---" + reader["title"].ToString() + "<br/>");
                        Response.Flush();
                    }
                    //if (decimal.Parse(reader["coor_lat"].ToString().Split('.')[0]) > decimal.Parse(reader["coor_long"].ToString().Split('.')[0]))
                    //{
                    //    Response.Write(reader["product_id"].ToString() + "---" + reader["product_code"].ToString() + "---" + reader["title"].ToString() + "---" + reader["coor_lat"].ToString() + "---" + reader["coor_long"].ToString() + "<br/>");
                    //    Response.Flush();
                    //}

                    //}

                }
            }
        }
    }
}