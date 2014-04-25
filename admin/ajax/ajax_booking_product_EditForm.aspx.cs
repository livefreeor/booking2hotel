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
    public partial class ajax_booking_product_EditForm : System.Web.UI.Page
    {

        public string qBookingProductID
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
                Response.Write(getBookingProductEdit());
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
        public string getBookingProductEdit()
        {
            Hotels2BasePage basePage = new Hotels2BasePage();
            BookingProductDisplay cBookingProductID = new BookingProductDisplay();
            cBookingProductID = cBookingProductID.getBookingProductDisplayByBookingProductId(int.Parse(this.qBookingProductID));
            Gateway cGateWay = new Gateway();
            StringBuilder result = new StringBuilder();

            result.Append("<form id=\"booking_product_edit_form\" action=\"\" >");

            result.Append("<div class=\"formbox_head\">Booking Product Edit</div>");
            result.Append("<div class=\"formbox_body\">");
            result.Append("<table id=\"tbl_payment_list\" cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:left;\">");
            result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Check in Date</td><td>&nbsp;<input type=\"text\" id=\"txtDateStart\"  class=\"TextBox_Extra_normal_small\" value=\"" + DateTimeCheck(cBookingProductID.DateTimeCheckIn) + "\" style=\"width:150px;\" name=\"txtDateStart\" /></td></tr>");
            result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Check Out Date</td><td>&nbsp;<input type=\"text\" id=\"txtDateEnd\"  class=\"TextBox_Extra_normal_small\" value=\"" + DateTimeCheck(cBookingProductID.DateTimeCheckOut) + "\" style=\"width:150px;\" name=\"txtDateEnd\" /></td></tr>");
            result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Adult</td><td>&nbsp;");
            result.Append("<select id=\"num_adult\" name=\"num_adult\" class=\"DropDownStyleCustom\" >");

            foreach (KeyValuePair<int, string> num in basePage.dicGetNumberstart0(150))
            {
                if (num.Key == cBookingProductID.NumAdult)
                    result.Append("<option value=\"" + num.Key + "\" selected=\"selected\">" + num.Value + "</option>");
                else
                    result.Append("<option value=\"" + num.Key + "\">" + num.Value + "</option>");

            }
            result.Append("</select>");
            result.Append("</td></tr>");
            result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Child</td><td>&nbsp;");
            result.Append("<select id=\"num_child\" name=\"num_child\"  class=\"DropDownStyleCustom\" >");

            foreach (KeyValuePair<int, string> num in basePage.dicGetNumberstart0(10))
            {
                if (num.Key == cBookingProductID.NumChild)
                    result.Append("<option value=\"" + num.Key + "\" selected=\"selected\">" + num.Value + "</option>");
                else
                    result.Append("<option value=\"" + num.Key + "\">" + num.Value + "</option>");
            }
            result.Append("</select>");
            result.Append("</td></tr>");
            result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Golfer</td><td>&nbsp;");
            result.Append("<select id=\"num_golf\" name=\"num_golf\"  class=\"DropDownStyleCustom\" >");

            foreach (KeyValuePair<int, string> num in basePage.dicGetNumberstart0(10))
            {
                if (num.Key == cBookingProductID.NunGolf)
                    result.Append("<option value=\"" + num.Key + "\" selected=\"selected\">" + num.Value + "</option>");
                else
                    result.Append("<option value=\"" + num.Key + "\">" + num.Value + "</option>");
            }
            result.Append("</select>");
            result.Append("</td></tr>");
            result.Append("</table>");
            result.Append("</div>");
            result.Append("<div class=\"formbox_buttom\"><input type=\"button\" value=\"Save\" onclick=\"EditProductSave('" + cBookingProductID.BookingProductId + "');\"  class=\"btStyleGreen\" />&nbsp;<input type=\"button\" value=\"Cancel\" onclick=\"DarkmanPopUp_Close();\" class=\"btStyleWhite\" style=\" width:80px\" /></div>");

            result.Append("</form>");


            return result.ToString();
        }



    }
}