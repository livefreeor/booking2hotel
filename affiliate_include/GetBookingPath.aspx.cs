using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;

public partial class affiliate_include_GetBookingPath : System.Web.UI.Page
{
    private string connString = WebConfigurationManager.ConnectionStrings["booking2hotelXConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        int hotelID=int.Parse(Request.QueryString["pid"]);
        string folderName = string.Empty;

        using (SqlConnection cn = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand("select top 1 folder from tbl_product_booking_engine where product_id=" + hotelID, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                folderName = reader["folder"].ToString();
                Response.Write(folderName+"_book.html");
            }
            else {
                Response.Write("error");
            }
            
        }
    }
}