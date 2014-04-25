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


    public partial class vtest_BookingProductEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(int.Parse(Request.Form["pid"]));
            //Response.End();
            
            ProductPrice pricelist = new ProductPrice(int.Parse(Request.Form["pid"]),byte.Parse(Request.Form["cid"]), DateTime.Parse(Request.Form["dateStart"]), DateTime.Parse(Request.Form["dateEnd"]));

            pricelist.LoadPrice();
            string bookingForm = string.Empty;

            bookingForm = bookingForm + "<form action=\"BookingProductAddInfo.aspx\" name=\"bookingForm\" id=\"bookingForm\" method=\"post\">\n";
            bookingForm = bookingForm + pricelist.BookingProductEdit(int.Parse(Request.Form["bpid"]));
            bookingForm = bookingForm + "</form>\n";
            //BookingProductEdit.Text = bookingForm; 
            Response.Write(bookingForm);
            //Response.Write(this.CurrentStaffId);
        }
    }
