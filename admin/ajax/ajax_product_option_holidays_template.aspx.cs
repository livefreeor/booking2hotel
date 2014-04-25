using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class ajax_product_option_holidays_template : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        public string qyear
        {
            get { return Request.QueryString["y"]; }
        }
        public short getCurrentSupplier
        {
            get
            {
                Product cProduct = new Product();
                cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                return cProduct.SupplierPrice;
            }
        }
        //public string qProductCat
        //{
        //    get { return Request.QueryString["pdcid"]; }
        //}

        //public byte Current_StaffLangId
        //{
        //    get 
        //    { 
        //        Hotels2BasePage cBasePage = new Hotels2BasePage();
        //        return cBasePage.CurrenStafftLangId;
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.qProductId))
            {
                if (!string.IsNullOrEmpty(this.qyear))
                {
                    Response.Write(TemplateGenerate(this.qyear));
                }
                else
                {
                    Response.Write(TemplateGenerate());
                }
                Response.End();
            }
            
        }

        public string TemplateGenerate(string year)
        {
            StringBuilder strResult = new StringBuilder();
            DateTime dDateYear = new DateTime(int.Parse(year), 9, 9);
            ProductOptioPublicHolidays cPublicHolidays = new ProductOptioPublicHolidays();

            strResult.Append("<table cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center\">");
            strResult.Append("<tr style=\"background-color:#3f5d9d;color:#ffffff;font-weight:bold\">");
            strResult.Append("<td  width=\"5%\"><input type=\"checkbox\" id=\"headcheck\" name=\"headcheck\" onclick=\"CheckboxChecked('headcheck','PublicHolidaysTemplate');\"></td><td  width=\"50%\" >Title</td><td  width=\"20%\">Date</td><td  width=\"25%\">Amount</td>");
            strResult.Append("</tr>");

            foreach (ProductOptioPublicHolidays hol in cPublicHolidays.getAllPublicHolidaysByYear(dDateYear))
            {
                strResult.Append("<tr style=\"background-color:#ffffff;\">");
                strResult.Append("<td><input type=\"checkbox\" name=\"list_check_" + hol.HolidayId + "\" id=\"list_check_" + hol.HolidayId + "\" value=\""+hol.HolidayId+"\"></td>");
                strResult.Append("<td style=\"text-align:left;padding:0px 0px 0px 5px\">" + hol.Title + "</td>");
                strResult.Append("<td>" + hol.HolidayDate.ToString("dd-MMM-yyyy") + "</td>");
                //strResult.Append("<td><input type=\"text\" name=\"txtDateStart_" + hol.HolidayId + "\" id=\"txtDateStart_" + hol.HolidayId + "\" value=\"" + hol.HolidayDate.ToString("yyyy-MM-dd") + "\" class=\"TextBox_Extra_normal\" style=\" width:120px; padding:2px;\" /></td>");
                strResult.Append("<td><input type=\"text\" name=\"txtAmount_" + hol.HolidayId + "\" id=\"txtAmount_" + hol.HolidayId + "\" class=\"TextBox_Extra\" value=\"0\" style=\" width:120px; padding:2px;\" /></td>");
                //strResult.Append("<td><input type=\"button\" value=\"Check\" onclick=\"check('txtDateStart_" + hol.HolidayId + "');\"></td>");
                strResult.Append("</tr>");
            }
            strResult.Append("</table><br/>");

            strResult.Append("<input type=\"button\" value=\"Save\" class=\"btStyleGreen\" onclick=\"InsertOptionTemplate();return fasle;\" \">");
            
            //GVPublicHolidaysTemplate.DataSource = cPublicHolidays.getAllPublicHolidaysByYear(DateTime.Now.Year);
            //GVPublicHolidaysTemplate.DataBind();
            return strResult.ToString();
        }

        public string TemplateGenerate()
        {
            StringBuilder strResult = new StringBuilder();
            strResult.Append("<p>Year Select &nbsp;");
            strResult.Append("<select name=\"dropyear\" id=\"dropyear\" class=\"dropStyle\" style=\"width:200px;\" onchange=\"YearChang();\">");

            foreach (KeyValuePair<string, string> item in Hotels2DateTime.GetYearList())
            {
                if (item.Key == DateTime.Now.Year.ToString())
                    strResult.Append("<option value=\"" + item.Key + "\" selected=\"selected\" >" + item.Key + "</option>");
                else
                    strResult.Append("<option value=\"" + item.Key + "\">" + item.Key + "</option>");
            }

            strResult.Append("</select>");
            strResult.Append("</p>");

            ProductOptioPublicHolidays cPublicHolidays = new ProductOptioPublicHolidays();
            strResult.Append("<div class=\"PublicHolidaysTemplate\" id=\"PublicHolidaysTemplate\" >");
            strResult.Append("<table cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center\">");
            strResult.Append("<tr style=\"background-color:#3f5d9d;color:#ffffff;font-weight:bold\">");
            strResult.Append("<td  width=\"5%\"><input type=\"checkbox\" id=\"headcheck\" name=\"headcheck\" onclick=\"CheckboxChecked('headcheck','PublicHolidaysTemplate');\" ></td><td  width=\"50%\" >Title</td><td  width=\"20%\">Date</td><td  width=\"25%\">Amount</td>");
            strResult.Append("</tr>");

            foreach (ProductOptioPublicHolidays hol in cPublicHolidays.getAllPublicHolidaysByYear(DateTime.Now))
            {
                strResult.Append("<tr style=\"background-color:#ffffff;\">");
                strResult.Append("<td><input type=\"checkbox\" name=\"list_check_" + hol.HolidayId + "\" id=\"list_check_" + hol.HolidayId + "\" value=\"" + hol.HolidayId + "\"></td>");
                strResult.Append("<td style=\"text-align:left;padding:0px 0px 0px 5px\">" + hol.Title + "</td>");
                strResult.Append("<td>" + hol.HolidayDate.ToString("dd-MMM-yyyy") + "</td>");
                //strResult.Append("<td><input type=\"text\" name=\"txtDateStart_" + hol.HolidayId + "\" id=\"txtDateStart_" + hol.HolidayId + "\" value=\"" + hol.HolidayDate.ToString("yyyy-MM-dd") + "\" class=\"TextBox_Extra_normal\" style=\" width:120px; padding:2px;\" /></td>");
                strResult.Append("<td><input type=\"text\" name=\"txtAmount_" + hol.HolidayId + "\" id=\"txtAmount_" + hol.HolidayId + "\" class=\"TextBox_Extra\" value=\"0\" style=\" width:120px; padding:2px;\" /></td>");
                //strResult.Append("<td><input type=\"button\" value=\"Check\" onclick=\"check('txtDateStart_" + hol.HolidayId + "');\"></td>");
                strResult.Append("</tr>");
            }
            strResult.Append("</table><br/>");

            strResult.Append("<input type=\"button\" value=\"Save\" class=\"btStyleGreen\" onclick=\"InsertOptionTemplate();return fasle;\"  \">");
            strResult.Append("</div>");
            //GVPublicHolidaysTemplate.DataSource = cPublicHolidays.getAllPublicHolidaysByYear(DateTime.Now.Year);
            //GVPublicHolidaysTemplate.DataBind();
            return strResult.ToString();
        }

        
    }
}