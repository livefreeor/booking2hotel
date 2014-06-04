using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using System.Text;
using Hotels2thailand.Production;
using Hotels2thailand.Member ;
using Hotels2thailand;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_member_detail : Hotels2BasePageExtra_Ajax
    {
        
        public string qmemberId
        {
            get { return Request.QueryString["mid"]; }

        }

        


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                Response.Write(memberdetail());
                Response.End();



                //try
                //{
                   
                //}
                //catch (Exception ex)
                //{
                //    Response.Write(ex.Message + "--" + ex.StackTrace);
                //    Response.End();
                //}
                
                
            }
        }

        

        public string memberdetail()
        {
            StringBuilder result = new StringBuilder();

            int intProduct = this.CurrentProductActiveExtra;
            int CusId = int.Parse(this.qmemberId);

            Country cCountry = new Country();

            Member_customer cMember = new Member_customer();
            cMember = cMember.getMemberById(CusId);

            string atrDatebirth = (cMember.DateBirth == null ? "-" : ((DateTime)cMember.DateBirth).ToString("dd MMM, yyyy"));
            string atrDatebirthform = (cMember.DateBirth == null ? "-" : ((DateTime)cMember.DateBirth).ToString("yyyy-MM-dd"));

            int intYear = 0;
            int intDay = 0;
            int intMonth = 0;

            if (cMember.DateBirth != null)
            {
                intYear = ((DateTime)cMember.DateBirth).Year;
                intDay = ((DateTime)cMember.DateBirth).Day;
                intMonth = ((DateTime)cMember.DateBirth).Month;
            }
            string strDatesubmit = cMember.DateSubmit.ToString("dd MMM, yyyy");
            string strDatesubmitform =cMember.DateSubmit.ToString("yyyy-MM-dd");

            string strPhone = (string.IsNullOrEmpty(cMember.CusPhone) ? "-" : cMember.CusPhone);


            string strDisplaynone = "style=\"display:none;width:200px\"";
            string strActive = "";
            string strblock = "";
            string strClassBlock = "";
            string strActiveLink = "";
            if (cMember.Status)
            {
                strClassBlock = "cus_detail_block";
                strblock = "<a href=\"\" id=\"btn_block\">Block this customer</a>";
            }
            else
            {
                strClassBlock = "cus_detail_block_blocked";
                strblock = "<a href=\"\" id=\"btn_block\">Unblock</a>";
            }

            if (cMember.Isactive)
            {
                strActiveLink = "";
                strActive = "<span class=\"member_status_active\">Active</span>";
            }
            else{
                strActiveLink = "&nbsp;&nbsp;|&nbsp;&nbsp;<a href=\"\" id=\"btn_active_cus\">Active this customer now</a>";
                strActive = "<span class=\"member_status_intactive\">Inactive</span>";
            }


            result.Append("<div id=\"cus_detail_block\" class=\"" + strClassBlock + "\">");
            result.Append("<input type=\"hidden\" name=\"cus_id\" value=\""+cMember.CustomerID+"\" />");
            result.Append("<input type=\"hidden\" id=\"hd_isactive\" name=\"hd_isactive\" value=\"" + cMember.Isactive + "\" />");
            result.Append("<input type=\"hidden\" id=\"hd_isblocked\" name=\"hd_isblocked\" value=\"" + cMember.Status + "\" />");
            result.Append("<table>");

            result.Append("<tr>");
            result.Append("<td class=\"tb_title\">Name:</td>");
            result.Append("<td class=\"tb_res edit_mode\"><span>" + cMember.FullName + "</span><input class=\"Extra_textbox\"  type=\"text\" " + strDisplaynone + " name=\"txt_full_name\" value=\"" + cMember.FullName + "\"></td>");
            result.Append("<td class=\"tb_title\" style=\"text-align:center\">Country:</td>");
            result.Append("<td class=\"tb_res edit_mode\"  style=\"text-align:left\"><span>" + cMember.CountryTitle + "</span>");

            result.Append("<select " + strDisplaynone + " name=\"sel_country\"  class=\"Extra_Drop\">");
            string select = "";
            foreach (KeyValuePair<string, string> country in cCountry.GetCountryAll())
            {
                if (country.Key == cMember.CountryID.ToString())
                    select = "selected=\"selected\"";

                result.Append("<option  value=\"" + country.Key + "\" " + select + ">" + country.Value + "</option>");
                select = "";
            }
            
            
            result.Append("");
            result.Append("</select>");
            result.Append("</td>");
            result.Append("</tr>");
            result.Append("<tr>");
            result.Append("<td class=\"tb_title\">Email:</td><td class=\"tb_res edit_mode\"><span>" + cMember.Email + "</span><input type=\"text\" " + strDisplaynone + " name=\"txt_email\" value=\"" + cMember.Email + "\" class=\"Extra_textbox\"  /></td>");
            result.Append("<td class=\"tb_title\" style=\"text-align:center\">Birth Day:</td>");
            result.Append("<td class=\"tb_res edit_mode\"  style=\"text-align:left;width:240px;\"><span>" + atrDatebirth + "</span>" + BirthDateSel(intYear,intMonth,intDay) + "</td>");
            result.Append("</tr>");
            result.Append("<tr>");
            result.Append("<td class=\"tb_title\">Phone:</td><td class=\"tb_res edit_mode\"><span>" + strPhone + "</span><input type=\"text\" " + strDisplaynone + " name=\"txt_phone\" value=\"" + cMember.CusPhone + "\" class=\"Extra_textbox\"  /></td>");
            result.Append("</tr>");
            result.Append("<tr>");
            result.Append("<td class=\"tb_title\">Member Since:</td><td class=\"tb_res\"><span>" + strDatesubmit + "</span><input type=\"text\" " + strDisplaynone + " name=\"txt_datesubmit\" id=\"txt_datesubmit\" value=\"" + strDatesubmitform + "\" class=\"Extra_textbox\"  /></td>");
            result.Append("</tr>");
            result.Append("<tr>");
            result.Append(" <td class=\"tb_title\">Status:</td><td class=\"tb_res\">" + strActive + "</td>");
            result.Append("</tr>");
            result.Append("<tr><td class=\"tb_res\" colspan=\"2\"><a href=\"" + CusId + "\" id=\"link_reset_pass\" >Reset Password</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href=\"\" id=\"edit_mode_click\">Edit</a>&nbsp;&nbsp;|&nbsp;&nbsp;" + strblock + strActiveLink + "</td></tr>");

            result.Append("</table>");

            result.Append("<div id=\"btnedit\"><input type=\"button\" id=\"SaveCus_edit\" value=\"Save\"  class=\"Extra_Button_small_green\"  />&nbsp;");
            result.Append("<input type=\"button\" id=\"cancelEdit\" value=\"Cancel\"  class=\"Extra_Button_small_white\"  /></div>");
            result.Append("</div>");
            return result.ToString();
        }


        public string BirthDateSel(int intY, int intMonth, int intDay)
        {
            StringBuilder result = new StringBuilder();
            result.Append("<div id=\"div_date_birth\" style=\"display:none;\">");
            result.Append("<select name=\"sel_bh_month\" style=\"display:none;width:100px;\" class=\"Extra_Drop\">");
            if(intMonth == 0)
                result.Append("<option selected=\"selected\" value=\"0\">Month:</option>");
            else
                result.Append("<option value=\"0\">Month:</option>");
            foreach (KeyValuePair<string, string> month in Hotels2thailand.Hotels2DateTime.GetMonthList())
            {
                if ( int.Parse(month.Key) == intMonth)
                    result.Append("<option selected=\"selected\" value=\"" + month.Key + "\">" + month.Value + "</option>");
                else
                    result.Append("<option value=\"" + month.Key + "\">" + month.Value + "</option>");
                    
            }
            
            result.Append("</select>");

            result.Append("&nbsp;<select name=\"sel_bh_day\" style=\"display:none;60px;\" class=\"Extra_Drop\">");
            if (intMonth == 0)
                result.Append("<option selected=\"selected\" value=\"0\">Day:</option>");
            else
                result.Append("<option value=\"0\">Day:</option>");
            foreach (KeyValuePair<int, string> day in this.dicGetNumber(31))
            {
                if (day.Key == intDay)
                    result.Append("<option selected=\"selected\" value=\"" + day.Key + "\">" + day.Value + "</option>");
                else
                    result.Append("<option value=\"" + day.Key + "\">" + day.Value + "</option>");

            }

            result.Append("</select>");

            int dYear = DateTime.Now.Year;
            result.Append("&nbsp;<select name=\"sel_bh_year\" style=\"display:none;60px;\" class=\"Extra_Drop\" class=\"Extra_Drop\">");
            if (intMonth == 0)
                result.Append("<option selected=\"selected\" value=\"0\">Year:</option>");
            else
                result.Append("<option value=\"0\">Year:</option>");
            foreach (KeyValuePair<int, string> year in this.dicGetNumber((dYear-107),dYear))
            {
                if (year.Key == intY)
                    result.Append("<option selected=\"selected\" value=\"" + year.Key + "\">" + year.Value + "</option>");
                else
                    result.Append("<option value=\"" + year.Key + "\">" + year.Value + "</option>");

            }

            result.Append("</select>");
            result.Append("</div>");

            return result.ToString();
        }
    }
}