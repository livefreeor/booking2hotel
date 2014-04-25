using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using System.Text;
using Hotels2thailand.Production;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_holiday_surcharge_list : Hotels2BasePageExtra_Ajax
    {
        public string qDateFrom
        {
            get { return Request.QueryString["dFrom"]; }
        }

        public string qDateTo
        {
            get { return Request.QueryString["dTo"]; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                //Response.Write("HELLO");
                Response.Write(GetHolidayList());
                Response.End();
                
            }
        }


        public string GetHolidayList()
        {
            StringBuilder result = new StringBuilder();

            try
            {
                DateTime dDAteFrom = this.qDateFrom.Hotels2DateSplitYear("-");
                DateTime dDAteTo = this.qDateTo.Hotels2DateSplitYear("-");
                ProductsupplementExtranet cSupExtra = new ProductsupplementExtranet();
                IList<object> iListHoliday = cSupExtra.GetSupplementDateByDateRange(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteFrom, dDAteTo,  true);
                //result.Append("<form id=\"form_holiday_surcharge\" action=\"\" >");

                //result.Append("<div class=\"formbox_head\">Holidays charge list</div>");
                //result.Append("<div class=\"formbox_body\" id=\"formbox_body\">");
                if (iListHoliday.Count > 0)
                {
                    
                    result.Append("<table cellspacing=\"2\" class=\"tbl_acknow\"  width=\"80%\" >");
                    result.Append("<tr class=\"header_field\">");
                    result.Append("<th>Date</th><th>Holiday</th><th>Amount</th>");
                    result.Append("</tr>");


                    foreach (ProductsupplementExtranet holiday in iListHoliday)
                    {
                        result.Append("<tr bgcolor=\"#ffffff\" align=\"center\">");
                        result.Append("<td>" + holiday.DateTitle + "<input type=\"hidden\" name=\"hd_date_supplement_title_" + holiday.SupplementID + "\" id=\"hd_date_supplement_title_" + holiday.SupplementID + "\" value=\"" + holiday.DateTitle + "(" + holiday.DateSupplement.ToString("dd-MMM-yyyy") + "):\" /></td>");
                        result.Append("<td>" + holiday.DateSupplement.ToString("dd-MMM-yyyy") + "");
                        result.Append("<input type=\"hidden\" name=\"hd_date_supplement_" + holiday.SupplementID + "\" id=\"hd_date_supplement_" + holiday.SupplementID + "\" value=\"" + holiday.DateSupplement.ToString("yyyy-MM-dd") + "\" />");
                        result.Append("<input type=\"checkbox\" name=\"chk_supplement\" checked=\"checked\" style=\"display:none;\" value=\"" + holiday.SupplementID + "\" />");
                        result.Append("</td>");
                        result.Append("<td><input type=\"text\" class=\"Extra_textbox_yellow\" name=\"supplement_amount_" + holiday.SupplementID + "\" id=\"supplement_amount_" + holiday.SupplementID + "\" /></td>");
                    }
                    
                    result.Append("</table>");
                    
                }
                else
                {
                    result.Append("<div class=\"box_empty\">");
                    result.Append("<p><label>Sorry! </label>no holiday please insert before</p>");
                    result.Append("");
                    result.Append("");
                    result.Append("<div>");
                }
                //result.Append("</div>");
                //result.Append("<div class=\"formbox_buttom\" id=\"formbox_buttom\"><input type=\"button\" value=\"Save\" onclick=\"CopyHoliday();\"   />&nbsp;<input type=\"button\" value=\"Cancel\" onclick=\"DarkmanPopUp_Close();\"  style=\" width:80px\" /></div>");
                //result.Append("</form>");
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.Message);
                Response.End();
            }

            return result.ToString();
        }

        
        
    }
}