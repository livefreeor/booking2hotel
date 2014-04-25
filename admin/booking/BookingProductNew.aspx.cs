using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;
using Hotels2thailand.Front;

public partial class admin_booking_BookingProductNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write(int.Parse(Request.Form["pid"]));
        //Response.End();
        int productID=int.Parse(Request.Form["pid"]);
        int bookingID = int.Parse(Request.Form["bid"]);
        ProductPrice pricelist = new ProductPrice(productID, byte.Parse(Request.Form["cid"]), DateTime.Parse(Request.Form["dateStart"]), DateTime.Parse(Request.Form["dateEnd"]));

        pricelist.LoadPrice();
        string bookingForm = string.Empty;

        bookingForm = bookingForm + "<form action=\"BookingProductNewInfo.aspx\" name=\"bookingForm\" id=\"bookingForm\" method=\"post\">\n";
        bookingForm = bookingForm + pricelist.BookingProductAddNew(bookingID);
        bookingForm = bookingForm + "</form>\n";
        //BookingProductEdit.Text = bookingForm; 
        Response.Write(bookingForm);
        //Response.Write(this.CurrentStaffId);
    }
}