using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using System.Text;
using Hotels2thailand.Production;
using Hotels2thailand.Booking;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_dash_board_booking : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                Response.Write(bookingresult());
                Response.End();
                
            }
        }


        public string bookingresult()
        {
            StringBuilder result = new StringBuilder();
            try
            {
                
                BookingAcknowledge cBookingAcknowledge = new BookingAcknowledge();
                int intWaitForAck = cBookingAcknowledge.CountBookingExtraNet(1, this.CurrentProductActiveExtra, this.CurrentSupplierId);
                int intWaintForCancel = cBookingAcknowledge.CountBookingExtraNet(3, this.CurrentProductActiveExtra, this.CurrentSupplierId);

                string queryString = "";
                if (!string.IsNullOrEmpty(Request.QueryString["pid"]) && !string.IsNullOrEmpty(Request.QueryString["supid"]))
                    queryString = "&pid=" + Request.QueryString["pid"] + "&supid=" + Request.QueryString["supid"];

                result.Append("<div style=\"border-bottom:1px solid #e2e2e2\" class=\"block\">");
                result.Append("<p class=\"topic\"><img src=\"http://www.hotels2thailand.com/images_extra/dot_yellow.png\" />");
                result.Append("<a href=\"ordercenter/acknowledge_control.aspx?ack_tpye=1" + queryString + "\" target=\"_Blank\" >&nbsp;Waiting for Confirm Acknowledge");
                result.Append("</a></p>");
                result.Append("<p class=\"booking_sum\"><span class=\"result\">" + intWaitForAck + "</span>&nbsp;booking</p>");
                result.Append("</div>");
                result.Append("<div class=\"block\">");
                result.Append("<p class=\"topic\"><img src=\"http://www.hotels2thailand.com/images_extra/dot_yellow.png\" />");
                result.Append("<a href=\"ordercenter/acknowledge_control.aspx?ack_tpye=3" + queryString + "\" target=\"_Blank\" >");
                result.Append("&nbsp;Waiting for Confirm Cancel");
                result.Append("</a></p>");
                result.Append("<p class=\"booking_sum\"><span class=\"result\">" + intWaintForCancel + "</span>&nbsp;booking</p>");
                result.Append("</div>");
               
            }
            catch (Exception ex)
            {
                Response.Write("error:" + ex.Message);
                Response.End();
            }

            return result.ToString();
           
        }
        
        
    }
}