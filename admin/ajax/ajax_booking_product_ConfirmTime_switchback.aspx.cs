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
    public partial class ajax_booking_product_ConfirmTime_switchback : System.Web.UI.Page
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
            
            return cProduct.UpdateBookingProductConfirmCheckInTimeRollBack(int.Parse(this.qBookingProductId));
        }



    }
}