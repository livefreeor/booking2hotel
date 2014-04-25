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
    public partial class ajax_booking_closeAndActivity : System.Web.UI.Page
    {

        public string qBookingId
        {
            get
            {
                return Request.QueryString["bid"];
            }
        }
        //public string qBookingProductId
        //{
        //    get
        //    {
        //        return Request.QueryString["bpid"];
        //    }
        //}


        //public string qBookingType
        //{
        //    get
        //    {
        //        return Request.QueryString["bt"];
        //    }
        //}
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Response.Write(getBookingActivityInsert());
                Response.Flush();
            }
        }

        public string getBookingActivityInsert()
        {
            
            StringBuilder result = new StringBuilder();

            result.Append("<form id=\"closeBooking_form\" action=\"\" >");
            //result.Append("<input type=\"hidden\" value=\""+this.qBookingType+"\" name=\"bookingType\" />");
            //result.Append("<input type=\"hidden\" value=\"" + this.qBookingProductId + "\" name=\"bookingProductId\" />");
            
            result.Append("<div class=\"formbox_head\">Please insert why to close this booking??</div>");
            result.Append("<div class=\"formbox_body\">");
            result.Append("<table id=\"tbl_payment_list\" cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:left;\">");
            result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Activity</td><td>&nbsp;<textarea rows=\"8\" cols=\"20\" class=\"TextBox_Extra_normal_small\" style=\"width:350px;\" name=\"activity_detail\"></textarea></td></tr>");
            result.Append("</table>");
            result.Append("</div>");

            result.Append("<div class=\"formbox_buttom\"><input type=\"button\" value=\"Close Now\" onclick=\"CloseBookingANdInsertnewActivity();return false;\"  class=\"btStyleGreen\" />&nbsp;<input type=\"button\" value=\"Cancel\" onclick=\"DarkmanPopUp_Close();\" class=\"btStyleWhite\" style=\" width:80px\" /></div>");
            
            result.Append("</form>");


            return result.ToString();
        }



    }
}