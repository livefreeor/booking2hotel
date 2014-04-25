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
    public partial class ajax_booking_product_ConfirmTimeEditForm : System.Web.UI.Page
    {

        public string qBookingId
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
        protected string DateTimeCheck(DateTime? dDate)
        {
            string dDAtestring = DateTime.Now.ToString("yyyy-MM-dd");
            if (dDate.HasValue)
            {
                DateTime dDAteTime = (DateTime)dDate;
                dDAtestring = dDAteTime.ToString("yyyy-MM-dd");
            }
            return dDAtestring;
        }

        protected DateTime DateTimeCheckTime(DateTime? dDate)
        {
            DateTime dDAteTime = new DateTime(9999,9,9,0,0,0);
           if (dDate.HasValue)
            {
                dDAteTime = (DateTime)dDate;
                
            }
           return dDAteTime;
        }
        public string getBookingConfirmTime()
        {
            Hotels2BasePage basePage = new Hotels2BasePage();
            BookingProductDisplay cBookingProductID = new BookingProductDisplay();
            cBookingProductID = cBookingProductID.getBookingProductDisplayByBookingProductId(int.Parse(this.qBookingId));
            
            StringBuilder result = new StringBuilder();

            result.Append("<form id=\"booking_ConfirmTime_form\" action=\"\" >");

            result.Append("<div class=\"formbox_head\">Booking Product Edit</div>");
            result.Append("<div class=\"formbox_body\">");
            result.Append("<table id=\"tbl_payment_list\" cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:left;\">");
            result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Check in Date</td><td>&nbsp;<input type=\"text\" id=\"txtDateStart\"  class=\"TextBox_Extra_normal_small\" value=\"" + DateTimeCheck(cBookingProductID.DateTimeCheckIn) + "\" style=\"width:150px;\" name=\"txtDateStart\" /></td></tr>");
            
            result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Child</td><td>&nbsp;");
            result.Append("<select id=\"Hours\" name=\"Hours\"  class=\"DropDownStyleCustom\" >");

            foreach (KeyValuePair<int, string> num in basePage.dicGetNumber(30))
            {
                if (num.Key == DateTimeCheckTime(cBookingProductID.DateTimeCheckIn).Hour)
                    result.Append("<option value=\"" + num.Key + "\" selected=\"selected\">" + num.Value + "</option>");
                else
                    result.Append("<option value=\"" + num.Key + "\">" + num.Value + "</option>");
            }
            result.Append("</select>");
            result.Append("<select id=\"Mins\" name=\"Mins\"  class=\"DropDownStyleCustom\" >");

            foreach (KeyValuePair<int, string> num in basePage.dicGetTimEHrs(60))
            {
                if (num.Key == DateTimeCheckTime(cBookingProductID.DateTimeCheckIn).Minute)
                    result.Append("<option value=\"" + num.Key + "\" selected=\"selected\">" + num.Value + "</option>");
                else
                    result.Append("<option value=\"" + num.Key + "\">" + num.Value + "</option>");
            }
            result.Append("</select>");
            result.Append("</td></tr>");
            result.Append("</table>");
            result.Append("</div>");
            result.Append("<div class=\"formbox_buttom\"><input type=\"button\" value=\"Save\" onclick=\"ConfirmTimeCheckIn('" + cBookingProductID.BookingProductId + "');\"  class=\"btStyleGreen\" />&nbsp;<input type=\"button\" value=\"Cancel\" onclick=\"DarkmanPopUp_Close();\" class=\"btStyleWhite\" style=\" width:80px\" /></div>");

            result.Append("</form>");


            return result.ToString();
        }



    }
}