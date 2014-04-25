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
    public partial class ajax_booking_product_head : System.Web.UI.Page
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
            BookingProductDisplay cBookingProduct = new BookingProductDisplay();

            ProductBookingEngine cProductEngine = new ProductBookingEngine();

            Status cStatus = new Status();

            cBookingDetail = cBookingDetail.GetBookingDetailListByBookingId(BookingId);
            cBookingProduct = cBookingProduct.getBookingProductDisplayByBookingProductId(cBookingDetail.BookingProductId);
            string AppendQueryString = string.Empty;
            //if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qSupplierId))
            //    AppendQueryString = "&pid=" + this.qProductId + "&supid=" + this.qSupplierId;
            result.Append("<p class=\"booking_id_head\"><label style=\"font-size:14px;\">" + cBookingProduct.ProductTitle + "</label><br/><label style=\"font-weight:normal\">" + cBookingProduct.ProductAddress + "</label><br/>Phone:<label style=\"font-weight:normal\">" + cBookingProduct.ProductPhone + "</label>&nbsp;,Email: &nbsp;<label style=\"font-weight:normal\">" + cBookingProduct.ProductEmail + "</label></p>");
            
            if (string.IsNullOrEmpty(cBookingDetail.HOtelIdNo))
                result.Append("<p class=\"booking_id_head\">Booking ID: " + cBookingDetail.BookingHotelId + "/" + cBookingDetail.BookingId + "</p>");
            else
                result.Append("<p class=\"booking_id_head\">Booking ID: " + cBookingDetail.BookingHotelId + "/" + cBookingDetail.BookingId + "&nbsp;&nbsp;&nbsp;[ Hotel Booking No.: " + cBookingDetail.HOtelIdNo + " ]</p>");


            //result.Append("<p class=\"booking_id_head\"> "+cBookingProduct.ProductAddress+" </p>");
            //result.Append("<p class=\"booking_id_head\">"+cBookingProduct.ProductPhone+" </p>");
            //result.Append("<p>" + cBookingProduct.ProductTitle + "</p>");
            return result.ToString();
        }
    }
}