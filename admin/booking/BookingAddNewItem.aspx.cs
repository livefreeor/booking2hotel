using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Booking;
using Hotels2thailand;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;

public partial class vtest_BookingAddNewItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int bookingProductID = int.Parse(Request.QueryString["bpId"]);

        DataConnect objConn = new DataConnect();
        //string sqlCommand = "select bp.product_id,bp.date_time_check_in,bp.date_time_check_out";
        //sqlCommand = sqlCommand + " from tbl_booking_product bp";
        //sqlCommand = sqlCommand + " where booking_product_id=" + bookingProductID;

        string sqlCommand = "select bp.product_id,p.cat_id,bp.date_time_check_in,bp.date_time_check_out";
        sqlCommand = sqlCommand + " from tbl_booking_product bp,tbl_product p";
        sqlCommand = sqlCommand + " where bp.product_id=p.product_id and  booking_product_id=" + bookingProductID;


        SqlDataReader reader = objConn.GetDataReader(sqlCommand);

        if (reader.Read())
        {
            ProductPrice pricelist = new ProductPrice((int)reader["product_id"],(byte)reader["cat_id"], (DateTime)reader["date_time_check_in"], (DateTime)reader["date_time_check_out"]);
            pricelist.LoadPrice();
            string bookingForm = string.Empty;

            bookingForm = bookingForm + "<form action=\"BookingAddNewItemInfo.aspx\" name=\"bookingForm\" id=\"bookingForm\" method=\"post\">\n";
            bookingForm = bookingForm + pricelist.BookingAddNewItem(bookingProductID);
            bookingForm = bookingForm + "</form>\n";
            Response.Write(bookingForm);
        }
    }
}