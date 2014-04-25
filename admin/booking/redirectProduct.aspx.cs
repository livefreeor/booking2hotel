using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Affiliate;
using Hotels2thailand;
using Hotels2thailand.DataAccess;
using System.Web.Configuration;
using System.Data.SqlClient;

public partial class admin_report_redirectProduct : System.Web.UI.Page
{
    private string connString = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        SiteStat_Tracker.ClearCookie_Only();
        int productID = int.Parse(Request.QueryString["pid"]);
        string sqlCommand = string.Empty;

        using (SqlConnection cn = new SqlConnection(connString))
        {
            sqlCommand = "select d.folder_destination+'-hotels/'+pc.file_name_main as file_path";
            sqlCommand = sqlCommand+" from tbl_product p,tbl_destination d,tbl_product_content pc";
            sqlCommand = sqlCommand + " where p.product_id=pc.product_id and p.destination_id=d.destination_id and pc.lang_id=1 and p.product_id="+productID;


            SqlCommand cmd = new SqlCommand(sqlCommand, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Response.Redirect("/"+reader["file_path"].ToString());
            }
        }
    }
}