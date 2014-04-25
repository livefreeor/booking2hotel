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

public partial class temp_contentAdd : System.Web.UI.Page
{
    private string connString = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        int productID = int.Parse(Request.QueryString["pid"]);

        string latitude = string.Empty;
        string longitude = string.Empty;

        string sqlCommand = string.Empty;
        string result = string.Empty;
        using (SqlConnection cn = new SqlConnection(connString))
        {
            sqlCommand = "select top 1 coor_lat,coor_long from tbl_product where product_id="+productID;

            SqlCommand cmd = new SqlCommand(sqlCommand, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                latitude = reader["coor_lat"].ToString();
                longitude = reader["coor_long"].ToString();
            }
        }

        using (SqlConnection cn = new SqlConnection(connString))
        {
            sqlCommand = "select  product_id,title,title_second,address,detail_short,detail,direction ";
            sqlCommand = sqlCommand + " from tbl_product_content  where product_id=" + productID + " and lang_id=1";
            sqlCommand = sqlCommand+" union";
            sqlCommand = sqlCommand + " select  product_id,title,title_second,address,detail_short,detail,direction";
            sqlCommand = sqlCommand + " from tbl_product_content  where product_id=" + productID + " and lang_id=2";

            SqlCommand cmd = new SqlCommand(sqlCommand, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            result = result + "<form action=\"contentAddPcs.aspx\" method=\"post\">\n";
            result = result + "<table id=\"listItem\" cellspacing=\"2\" cellpadding=\"5\">\n";
            result = result + "<tr><th>Englist</th>\n";
            result = result + "<th>Thai</th></tr>\n";

            int countContent=1;
            while (reader.Read())
            {
                if(countContent==1)
                {
                    result = result + "<tr>\n";
                    result = result + "<td valign=\"top\">\n";
                    result = result + "Title:<br/>";
                    result = result + "<input type=\"text\" name=\"title_default\" class=\"text400\" value=\"" + reader["title"] + "\"/><br />\n";
                    result = result + "Title Second:<br />";
                    result = result + "<input type=\"text\" name=\"title_second_default\"  class=\"text400\"  value=\"" + reader["title_second"] + "\"/><br />\n";
                    result = result + "Address:<br />\n";
                    result = result + "<textarea class=\"textArea400\" name=\"address_default\">" + reader["address"] + "</textarea><br />\n";
                    result = result + "Short Detail:<br />\n";
                    result = result + "<textarea class=\"textArea400\" name=\"detail_short_default\">" + reader["detail_short"] + "</textarea><br />\n";
                    result = result + "Detail:<br />";
                    result = result + "<textarea class=\"textArea400\" name=\"detail_default\">" + reader["detail"] + "</textarea><br />\n";
                    result = result + "Direction:<br />\n";
                    result = result + "<textarea class=\"textArea400\" name=\"direction_default\">" + reader["direction"] + "</textarea><br />\n";
                    result = result + "</td>\n";
                }else{
                    result = result + "<td>\n";
                    result = result + "Title:<br/>\n";
                    result = result + "<input type=\"text\" class=\"text400\" name=\"title\" value=\"" + reader["title"] + "\"/><br />\n";
                    result = result + "Title Second:<br />";
                    result = result + "<input type=\"text\"  class=\"text400\" name=\"title_second\"  value=\"" + reader["title_second"] + "\"/><br />\n";
                    result = result + "Address:<br />\n";
                    result = result + "<textarea class=\"textArea400\" name=\"address\">" + reader["address"] + "</textarea><br />\n";
                    result = result + "Short Detail:<br />\n";
                    result = result + "<textarea class=\"textArea400\" name=\"detail_short\">" + reader["detail_short"] + "</textarea><br />\n";
                    result = result + "Detail:<br />\n";
                    result = result + "<textarea class=\"textArea400\" name=\"detail\">" + reader["detail"] + "</textarea><br />\n";
                    result = result + "Direction:<br />\n";
                    result = result + "<textarea class=\"textArea400\" name=\"direction\">" + reader["direction"] + "</textarea><br />\n";
                    result = result + "Latitude:<br/>\n";
                    result = result + "<input type=\"text\" class=\"text400\" name=\"latitude\" value=\"" + latitude + "\"/><br />\n";
                    result = result + "Longitude:<br />";
                    result = result + "<input type=\"text\"  class=\"text400\" name=\"longitude\"  value=\"" + longitude + "\"/><br />\n";
                    result = result + "</td>\n";
                    result = result + "</tr>\n";
                    result = result + "<tr><td colspan=\"2\" align=\"center\"><input type=\"submit\" name=\"submit\" value=\"Save\" /></td>\n";

                    result = result + "</tr>\n";

                    result = result + "</table>";
                }
                countContent = countContent + 1;

            }

            //No thai content
            if (countContent == 2)
            {
                result = result + "<td>\n";
                result = result + "Title:<br/>\n";
                result = result + "<input type=\"text\" class=\"text400\"\" name=\"title\"/><br />\n";
                result = result + "Title Second:<br />\n";
                result = result + "<input type=\"text\" class=\"text400\"\" name=\"title_second\"/><br />\n";
                result = result + "Address:<br />\n";
                result = result + "<textarea class=\"textArea400\" name=\"address\"></textarea><br />\n";
                result = result + "Short Detail:<br />";
                result = result + "<textarea class=\"textArea400\" name=\"detail_short\"></textarea><br />";
                result = result + "Detail:<br />";
                result = result + "<textarea class=\"textArea400\" name=\"detail\"></textarea><br />";
                result = result + "Direction:<br />";
                result = result + "<textarea class=\"textArea400\" name=\"direction\"></textarea><br />";
                result = result + "Latitude:<br/>\n";
                result = result + "<input type=\"text\" class=\"text400\" name=\"latitude\" value=\"" + latitude + "\"/><br />\n";
                result = result + "Longitude:<br />";
                result = result + "<input type=\"text\"  class=\"text400\" name=\"longitude\"  value=\"" + longitude + "\"/><br />\n";

                result = result + "</td>\n";
                result = result + "</tr>\n";
                result = result + "<tr><td colspan=\"2\" align=\"center\"><input type=\"submit\" name=\"submit\" value=\"Save\" /></td>\n";
                result = result + "</tr>\n";
                result = result + "</table>";
                result = result + "<input type=\"hidden\" name=\"action\" value=\"insert\"/>";
            }
            else {
                result = result + "<input type=\"hidden\" name=\"action\" value=\"update\"/>";
            }
            result = result + "<input type=\"hidden\" name=\"product_id\" value=\"" + productID + "\"/>";
            result = result + "</form>\n";
            lblMain.Text = result;
        }
    }
}