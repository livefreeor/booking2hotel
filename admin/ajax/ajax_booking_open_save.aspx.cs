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

namespace Hotels2thailand.UI
{
    public partial class ajax_booking_open_save : System.Web.UI.Page
    {

        public string qBookingId
        {
            get
            {
                return Request.QueryString["bid"];
            }
        }
        
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Response.Write(OpenBooking());
                Response.Flush();
            }
        }

        public bool OpenBooking()
        {
            
            bool Isclose = false;
            BookingdetailDisplay cBooking = new BookingdetailDisplay();
            BookingActivityDisplay cBookingactivity = new BookingActivityDisplay();
            int intBookingId = int.Parse(this.qBookingId);

            Isclose = cBooking.UpdateBookingstatus(intBookingId, false);
            cBookingactivity.InsertAutoActivity(BookingActivityType.OpenBooking, intBookingId);
            return Isclose;
        }



    }
}