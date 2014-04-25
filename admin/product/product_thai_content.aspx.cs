using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
namespace Hotels2thailand.UI
{
    public partial class admin_product_thai_content : Hotels2BasePage
    {
        private string connString = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            string sqlCommand = string.Empty;
            string result = string.Empty;

            using (SqlConnection cn = new SqlConnection(connString))
            {
                sqlCommand = "select product_id,product_code,title";
                sqlCommand = sqlCommand + " from tbl_product where cat_id=29";
                sqlCommand = sqlCommand + " and status=1 and destination_id=30";
                sqlCommand = sqlCommand + " order by destination_id,title";
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                result = result + "<table class=\"tblListResult\" cellpadding=\"0\" cellspacing=\"2\">";
                result = result + "<tr><th>Code</th><th>Title</th><th></th></tr>";
                int count = 0;
                string strColor = string.Empty;
                while (reader.Read())
                {
                    if (count % 2 == 0)
                        strColor = "rowOdd";
                    else
                        strColor = "rowEven";

                    result = result + "<tr class=\"" + strColor + "\"><td>" + reader["product_code"] + "</td><td>" + reader["title"] + "</td><td><a href=\"product_thai_content_add.aspx?pid=" + reader["product_id"] + "\" target=\"_blank\">Add Content</a></td></tr>";

                    count = count + 1;
                }
                result = result + "</table>";
                lblresult.Text = result;
            }
        }
    }
}