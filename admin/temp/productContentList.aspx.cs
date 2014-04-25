using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand;
using Hotels2thailand.DataAccess;
using System.Web.Configuration;
using System.Data.SqlClient;
public partial class temp_productContentList : System.Web.UI.Page
{
    private string connString = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        string sqlCommand = string.Empty;
        string result = string.Empty;

        using (SqlConnection cn = new SqlConnection(connString))
        {
            sqlCommand = "select product_id,product_code,title";
            sqlCommand = sqlCommand+" from tbl_product where cat_id=29";
            sqlCommand = sqlCommand+" and status=1 and destination_id=30";
            sqlCommand = sqlCommand+" order by destination_id,title";
            SqlCommand cmd = new SqlCommand(sqlCommand, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            result = result + "<table class=\"tblListResult\" cellpadding=\"0\" cellspacing=\"2\">";
            result = result + "<tr><th>Code</th><th>Title</th><th></th></tr>";
            while(reader.Read())
            {
                result = result + "<tr><td>" + reader["product_code"] + "</td><td>" + reader["title"] + "</td><td><a href=\"contentAdd.aspx?pid=" + reader["product_id"] + "\" target=\"_blank\">Add Content</a></td></tr>";
            } 
            result = result + "</table>";
            Response.Write(result);
        }
    }
}