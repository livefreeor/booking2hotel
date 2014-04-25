using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Booking;
using Hotels2thailand.Production;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class ajax_booking_product_head : Hotels2BasePageExtra_Ajax
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Response.Write(BookingHead());
                Response.End();
            }
        }

        public string BookingHead()
        {
            int BookingId = int.Parse(Request.QueryString["bid"]);
            StringBuilder result = new StringBuilder();
            BookingdetailDisplay cBookingDetail = new BookingdetailDisplay();
            
            Status cStatus = new Status();

            cBookingDetail = cBookingDetail.GetBookingDetailListByBookingId(BookingId);
            string AppendQueryString = string.Empty;
            if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qSupplierId))
                AppendQueryString = "&pid=" + this.qProductId + "&supid=" + this.qSupplierId;

            
            if (string.IsNullOrEmpty(cBookingDetail.HOtelIdNo))
                result.Append("<p class=\"booking_id_head\">Booking ID: " + cBookingDetail.BookingHotelId + "</p>");
            else
                result.Append("<p class=\"booking_id_head\">Booking ID: " + cBookingDetail.BookingHotelId + "&nbsp;&nbsp;&nbsp;[ Hotel Booking No.: " + cBookingDetail.HOtelIdNo + " ]</p>");
           

            return result.ToString();
        }
    }
}