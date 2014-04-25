using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Booking;
using Hotels2thailand.Production;
using Hotels2thailand;

namespace Hotels2thailand.UI
{
    public partial class ajax_booking_product_ConfirmTimeSave : System.Web.UI.Page
    {

        public string qBookingProductId
        {
            get
            {
                return Request.QueryString["bpid"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Response.Write(getBookingConfirmTime());
                Response.Flush();
            }
        }
        
        public bool getBookingConfirmTime()
        {
            BookingProductDisplay cProduct = new BookingProductDisplay();
            DateTime dDAteCheckIn = DateTime.Parse(Request.Form["hd_txtDateStart"]);
            int Hrs = int.Parse(Request.Form["Hours"]);
            int Mins = int.Parse(Request.Form["Mins"]);

            DateTime dDateTimeRequest = new DateTime(dDAteCheckIn.Year, dDAteCheckIn.Month, dDAteCheckIn.Day, Hrs, Mins,0);
            return cProduct.UpdateBookingProductConfirmCheckInTime(int.Parse(this.qBookingProductId), dDateTimeRequest);
        }



    }
}