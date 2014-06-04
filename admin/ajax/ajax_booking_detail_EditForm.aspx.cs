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
    public partial class ajax_booking_detail_EditForm : System.Web.UI.Page
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
                Response.Write(getBookingProductSupplier());
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
            DateTime dDAteTime = new DateTime(9999, 9, 9, 0, 0, 0);
            if (dDate.HasValue)
            {
                dDAteTime = (DateTime)dDate;

            }
            return dDAteTime;
        }
        public string getBookingProductSupplier()
        {
            BookingdetailDisplay cBookingDetail = new BookingdetailDisplay();
            Status cStatus = new Status();
            cBookingDetail = cBookingDetail.GetBookingDetailListByBookingId(int.Parse(this.qBookingId));
            Hotels2BasePage basePage = new Hotels2BasePage();
            Country cCountry = new Country();

            string PhoneCountryCode = string.Empty;
            string PhoneLocalCode = string.Empty;
            string PhoneNum = string.Empty;

            string MobileCountryCode = string.Empty;
            string MobileLocalCode = string.Empty;
            string MobileNum = string.Empty;

            string FAxCountryCode = string.Empty;
            string FAxLocalCode = string.Empty;
            string FAxNum = string.Empty;

            

            string[] Phone = cBookingDetail.GetBookingPhoneByBookingId(int.Parse(this.qBookingId), 1);
            if (PhoneCountryCode != null)
            {
                PhoneCountryCode = Phone[0];
                PhoneLocalCode = Phone[1];
                PhoneNum = Phone[2];
            }

            string[] PhoneMobile = cBookingDetail.GetBookingPhoneByBookingId(int.Parse(this.qBookingId), 2);
            if (PhoneMobile != null)
            {
                MobileCountryCode = PhoneMobile[0];
                MobileLocalCode = PhoneMobile[1];
                MobileNum = PhoneMobile[2]; 
            }

            string[] PhoneFAx = cBookingDetail.GetBookingPhoneByBookingId(int.Parse(this.qBookingId), 3);
            if (PhoneFAx != null)
            {
                FAxCountryCode = PhoneFAx[0];
                FAxLocalCode = PhoneFAx[1];
                FAxNum = PhoneFAx[2]; 
            }
            StringBuilder result = new StringBuilder();
          
            

            result.Append("<form id=\"Booking_detail_Edit_form\" action=\"\" >");
            result.Append("<div class=\"formbox_head\">Insert New Payment</div>");
            result.Append("<div class=\"formbox_body\">");
            string FullName = "Full name";
        
            result.Append("<table cellpadding=\"0\" id=\"booking_detail\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center; font-size:12px;\">");
        
        
            if (cBookingDetail.CusId.HasValue)
                FullName = FullName + "&nbsp;<img src=\"/images/member.png\" alt=\"member\" />";
            result.Append("<tr class=\"trRowalten\"><td >" + FullName + "</td><td style=\"color:#333333\">");

            result.Append("");
            result.Append("");

            result.Append("<select class=\"DropDownStyleCustom\" name=\"customer_prefix\" >");
            PrefixName cPrefix = new PrefixName();
            List<object> ListProfix = cPrefix.GetPrefixAll();

            foreach (PrefixName pre in ListProfix)
            {
                if (pre.PrefixID == cBookingDetail.PrefixId)
                    result.Append("<option value=\"" + pre.PrefixID + "\" selected=\"selected\">" + pre.Title + "</option>");
                else
                    result.Append("<option value=\"" + pre.PrefixID + "\">" + pre.Title + "</option>");
            }
           
            result.Append("");
            result.Append("");
            result.Append("</select>&nbsp;");

            result.Append("<input type=\"text\" name=\"booking_name\" value=\"" + cBookingDetail.FullName + "\" class=\"TextBox_Extra_normal_small\" style=\"width:200px;\" /></td></tr>");

            result.Append("<tr class=\"trRow\"><td >Email</td><td style=\"color:#333333\"><input type=\"text\" name=\"booking_email\" value=\"" + cBookingDetail.Email + "\" class=\"TextBox_Extra_normal_small\" style=\"width:200px;\" /></td></tr>");

            result.Append("<tr class=\"trRowalten\" ><td  >Phone</td><td style=\"color:#333333\">");
            result.Append("<input type=\"text\" name=\"booking_phone_country_code\" value=\"" + PhoneCountryCode + "\" class=\"TextBox_Extra_normal_small\" style=\"width:30px;\" />");
            result.Append("&nbsp;<input type=\"text\" name=\"booking_phone_local_code\" value=\"" + PhoneLocalCode + "\" class=\"TextBox_Extra_normal_small\" style=\"width:20px;\" />");
            result.Append("&nbsp;<input type=\"text\" name=\"booking_phone_number\" value=\"" + PhoneNum + "\" class=\"TextBox_Extra_normal_small\" style=\"width:150px;\" />");
            result.Append("</td></tr>");
            result.Append("<tr class=\"trRow\"><td >Mobile</td><td style=\"color:#333333\">");
            result.Append("<input type=\"text\" name=\"booking_mobile_country_code\" value=\"" + MobileCountryCode + "\" class=\"TextBox_Extra_normal_small\" style=\"width:30px;\" />");
            result.Append("&nbsp;<input type=\"text\" name=\"booking_mobile_local_code\" value=\"" + MobileLocalCode + "\" class=\"TextBox_Extra_normal_small\" style=\"width:20px;\" />");
            result.Append("&nbsp;<input type=\"text\" name=\"booking_mobile_number\" value=\"" + MobileNum + "\" class=\"TextBox_Extra_normal_small\" style=\"width:150px;\" />");
            result.Append("</td></tr>");
            result.Append("<tr class=\"trRowalten\" ><td  >Fax</td><td style=\"color:#333333\">");
            result.Append("<input type=\"text\" name=\"booking_fax_country_code\" value=\"" + FAxCountryCode + "\" class=\"TextBox_Extra_normal_small\" style=\"width:30px;\" />");
            result.Append("&nbsp;<input type=\"text\" name=\"booking_fax_local_code\" value=\"" + FAxLocalCode + "\" class=\"TextBox_Extra_normal_small\" style=\"width:20px;\" />");
            result.Append("&nbsp;<input type=\"text\" name=\"booking_fax_number\" value=\"" + FAxNum + "\" class=\"TextBox_Extra_normal_small\" style=\"width:150px;\" />");
            result.Append("</td></tr>");


            result.Append("<tr class=\"trRow\"><td >Country</td><td style=\"color:#333333\">");
            result.Append("");
            result.Append("");
            result.Append("<select id=\"booking_country\" name=\"booking_country\"  class=\"DropDownStyleCustom\" >");


            foreach (KeyValuePair<string, string> country in cCountry.GetCountryAll())
            {
                if (country.Key == cBookingDetail.CountryId.ToString())
                    result.Append("<option value=\"" + country.Key + "\" selected=\"selected\">" + country.Value + "</option>");
                else
                    result.Append("<option value=\"" + country.Key + "\">" + country.Value + "</option>");
            }
            result.Append("</select>");
            result.Append("");
            result.Append("");
            result.Append("");
            result.Append("");
            result.Append("");
            result.Append("");
            result.Append("</td></tr>");

            result.Append("<tr class=\"trRowalten\"><td>Arrival Flight</td><td style=\"color:#333333;height:50px;\"><span style=\"font-weight:bold\">Nubmer:</span>");

            result.Append("<input type=\"text\" name=\"flight_num_arr\" value=\"" + cBookingDetail.F_arr_No + "\" style=\"margin:5px 0px 0px 0px;width:150px;\" class=\"TextBox_Extra_normal_small\" /><br/>");
            result.Append("&nbsp;<span style=\"font-weight:bold\">Time:</span>");

            result.Append("<input type=\"text\" id=\"txtDateflighArr\"  class=\"TextBox_Extra_normal_small\" value=\"" + DateTimeCheck(cBookingDetail.F_arr_Time) + "\" style=\"width:100px;margin:5px 0px 0px 0px\" name=\"txtDateflighArr\" />");
            result.Append("<div style=\"display:block;margin:5px 0px 5px 50px;\">");
            result.Append("<select id=\"Arr_Hours\" name=\"Arr_Hours\"  class=\"DropDownStyleCustom\" >");


            foreach (KeyValuePair<int, string> num in basePage.dicGetTimEHrs(23))
            {
                if (num.Key == DateTimeCheckTime(cBookingDetail.F_arr_Time).Hour)
                    result.Append("<option value=\"" + num.Key + "\" selected=\"selected\">" + num.Value + "</option>");
                else
                    result.Append("<option value=\"" + num.Key + "\">" + num.Value + "</option>");
            }
            result.Append("</select>");
            result.Append("&nbsp;<select id=\"Arr_Mins\" name=\"Arr_Mins\"  class=\"DropDownStyleCustom\" >");

            foreach (KeyValuePair<int, string> num in basePage.dicGetTimEHrs(59))
            {
                if (num.Key == DateTimeCheckTime(cBookingDetail.F_arr_Time).Minute)
                    result.Append("<option value=\"" + num.Key + "\" selected=\"selected\">" + num.Value + "</option>");
                else
                    result.Append("<option value=\"" + num.Key + "\">" + num.Value + "</option>");
            }
            result.Append("</select>");
            result.Append("</div>");
            result.Append("</td></tr>");

            result.Append("<tr class=\"trRow\"><td>Departure Flight</td><td style=\"color:#333333;height:50px;\"><span style=\"font-weight:bold\">Nubmer:</span>");

            result.Append("<input type=\"text\" name=\"flight_num_dep\" value=\"" + cBookingDetail.F_Dep_No + "\" style=\"margin:5px 0px 0px 0px;width:150px;\" class=\"TextBox_Extra_normal_small\"  /><br/>");
            result.Append("&nbsp;<span style=\"font-weight:bold\">Time:</span>");

            result.Append("<input type=\"text\" id=\"txtDateflighDep\"  class=\"TextBox_Extra_normal_small\" value=\"" + DateTimeCheck(cBookingDetail.F_Dep_Time) + "\" style=\"width:100px;margin:5px 0px 0px 0px\" name=\"txtDateflighDep\" />");
            result.Append("<div style=\"display:block;margin:5px 0px 5px 50px;\">");
            result.Append("<select id=\"Dep_Hours\" name=\"Dep_Hours\" class=\"DropDownStyleCustom\" >");


            foreach (KeyValuePair<int, string> num in basePage.dicGetTimEHrs(23))
            {
                if (num.Key == DateTimeCheckTime(cBookingDetail.F_Dep_Time).Hour)
                    result.Append("<option value=\"" + num.Key + "\" selected=\"selected\">" + num.Value + "</option>");
                else
                    result.Append("<option value=\"" + num.Key + "\">" + num.Value + "</option>");
            }
            result.Append("</select>");
            result.Append("&nbsp;<select id=\"Dep_Mins\" name=\"Dep_Mins\"  class=\"DropDownStyleCustom\" >");

            foreach (KeyValuePair<int, string> num in basePage.dicGetTimEHrs(59))
            {
                if (num.Key == DateTimeCheckTime(cBookingDetail.F_arr_Time).Minute)
                    result.Append("<option value=\"" + num.Key + "\" selected=\"selected\">" + num.Value + "</option>");
                else
                    result.Append("<option value=\"" + num.Key + "\">" + num.Value + "</option>");
            }
            result.Append("</select>");
            result.Append("</div>");
            result.Append("</td></tr>");

        
            result.Append("");
            result.Append("");
            result.Append("");
            
            result.Append("</table>");
            result.Append("</div>");
            result.Append("<div class=\"formbox_buttom\"><input type=\"button\" value=\"Save\" onclick=\"BookingDetailSave();return false;\"  class=\"btStyleGreen\" />&nbsp;<input type=\"button\" value=\"Cancel\" onclick=\"DarkmanPopUp_Close();\" class=\"btStyleWhite\" style=\" width:80px\" /></div>");
        
            result.Append("</form>");
            return result.ToString();
        }

       
    }
}