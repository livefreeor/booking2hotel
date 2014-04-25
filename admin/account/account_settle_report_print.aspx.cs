using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Account;

namespace Hotels2thailand.UI
{
    public partial class admin_account_settle_report_print : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //report_type.Value = Request.Form["t"];

            //Response.Write(Request.Form["t"]);
            //Response.End();

            string cssdefault = "<script language=\"javascript\" type=\"text/javascript\" src=\"/scripts/jquery-1.6.1.js\" ></script><script language=\"javascript\" type=\"text/javascript\" src=\"/scripts/darkman_utility.js\" ></script><link href=\"/css/Master_style.css\" type=\"text/css\" rel=\"stylesheet\" /><link href=\"/css/staffstyle.css\" type=\"text/css\" rel=\"stylesheet\" /><link href=\"/css/supplierstyle.css\" type=\"text/css\" rel=\"stylesheet\" /><link href=\"/css/productstyle.css\" type=\"text/css\" rel=\"stylesheet\" /><link href=\"/css/productstyle2.css\" type=\"text/css\" rel=\"stylesheet\" /><link href=\"/css/option_style.css\" type=\"text/css\" rel=\"stylesheet\" /><link href=\"/css/promotion.css\" type=\"text/css\" rel=\"stylesheet\" /><link href=\"/css/option_section/option_picture_style.css\" rel=\"Stylesheet\" /><link href=\"/css/option_section/option_rate_style.css\" rel=\"Stylesheet\" /><link href=\"/css/calendar.css\" rel=\"Stylesheet\" />";
            string result = "<html><head>" + cssdefault + "<style type=\"text/css\">" + Request.Form["s"] + "</style></head>";
            result = result + "<script language=\"javascript\" type=\"text/javascript\">";
            result = result + "$(document).ready(function () {";
            result = result + "var reportType = GetValueQueryString(\"t\");";
            result = result + "if (reportType == \"acc\") {";
            result = result + "$(\"input[name='checkbox_checked']:checked\").each(function () {";
            result = result + "var Id = $(this).val(); var PriceVal = $(\"#price_\" + Id).html().replace(',', '');";
            result = result + "var arrVal = PriceVal.split('.');";
            result = result + "PriceVal = (parseFloat(PriceVal)).formatMoney(2, '.', ',');";
            result = result + "$(\"#price_\" + Id).parent().html(PriceVal);";
            result = result + "});";
            result = result + "}";
            result = result + "if (reportType == \"bht\") {";
            result = result + "$(\"input[name='checkbox_checked']:checked\").each(function () {";
            result = result + "var Id = $(this).val();";
            result = result + "var PriceVal = $(\"#price_\" + Id).html().replace(',', '');";
            result = result + "var CostVal = $(\"#cost_\" + Id).html().replace(',', '');";
            result = result + "var arrVal = PriceVal.split('.');";
            result = result + "var arrValCost = CostVal.split('.');";
            result = result + "PriceVal = (parseFloat(PriceVal)).formatMoney(2, '.', ',');";
            result = result + "CostVal = (parseFloat(CostVal)).formatMoney(2, '.', ',');";
            result = result + "$(\"#price_\" + Id).parent().html(PriceVal);";
            result = result + "$(\"#cost_\" + Id).parent().html(CostVal);";
            result = result + "});";
            result = result + "}";
            result = result + "";
            result = result + "});";
            result = result + "</script>";
            result = result + "</head>";
            result = result + "";
            result = result + "";
            result = result + "";

            result = result + "<body style=\"background-image:url(/image/none.ng)\" ><div id=\"report_list\" style=\"width:850px\" >" + Request.Form["p"] + "</div></body></html>";
            Response.Write(result);
            Response.End();
            
            
        }

       
    }
}