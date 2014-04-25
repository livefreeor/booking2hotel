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
    public partial class admin_ajax_holiday_list : Hotels2BasePageExtra_Ajax
    {
        public string qYear
        {
            get { return Request.QueryString["y"]; }
        }

      


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qYear))
                {
                    //Response.Write("HELLO");
                    Response.Write(SupplementList());
                    Response.End();
                }
                
            }
        }

        //public string GetInsertForm()
        //{
        //    StringBuilder result = new StringBuilder();
        //    result.Append("<div id=\"holidayInsertForm\">");
        //    result.Append("<table>");
        //    result.Append("<td>Holiday title</td>");
        //    result.Append("<td><input type=\"textbox\" class=\"Extra_textbox\"  style=\"width:350px;\" id=\"holiday_title\" name=\"holiday_title\" /></td>");
        //    result.Append("<td>Holiday date</td>");
        //    result.Append("<td><input type=\"textbox\" class=\"Extra_textbox\" id=\"date_insert_holiday\" name=\"date_insert_holiday\" /></td>");
            
        //    result.Append("<td><input type=\"button\" value=\"Save\" onclick=\"insertHoliday();return false;\" /></td>");
        //    result.Append("");
        //    result.Append("");
        //    result.Append("</table>");
        //    result.Append("</div>");
        //    return result.ToString();
        //}

        public string SupplementList()
        {
            StringBuilder strResult = new StringBuilder();
            ProductsupplementExtranet cSupplement = new ProductsupplementExtranet();
            DateTime dDateYear = new DateTime(int.Parse(this.qYear), 9, 9);


            IList<object> iListHolidays = cSupplement.getOptionSuppleMentListCurrentYearBySupplierAndProductIdExtraNet(this.CurrentSupplierId, this.CurrentProductActiveExtra, dDateYear, true);

            strResult.Append("<h4><img   src=\"/images/content.png\" /> Holiday List</h4>");

            
            if (iListHolidays.Count() > 0)
            {

                strResult.Append("<div id=\"holidayCurrentList\">");
                strResult.Append("<table cellpadding=\"0\" cellspacing=\"2\" class=\"tbl_acknow\" >");
                strResult.Append("<tr class=\"header_field\" >");
                strResult.Append("<th  width=\"5%\">No.</th><th width=\"70%\">Title</th><th width=\"20%\" >Date</th><th  width=\"5%\" >Delete</th>");
                strResult.Append("</tr>");
                
                int countNum = 1;
                foreach (ProductsupplementExtranet Sup in iListHolidays)
                {
                    strResult.Append("<tr style=\"background-color:#ffffff;\">");
                    strResult.Append("<td   align=\"center\">" + countNum + "<input type=\"checkbox\" name=\"checked_holiday_list\" value=\"" + Sup.SupplementID + "\" checked=\"checked\" style=\"display:none;\" /></td>");
                    strResult.Append("<td><input type=\"text\" name=\"txtSuptitle_" + Sup.SupplementID + "\" id=\"txtSuptitle_" + Sup.SupplementID + "\" class=\"Extra_textbox\" value=\"" + Sup.DateTitle + "\" style=\" width:600px;\" /></td>");

                    strResult.Append("<td><input type=\"text\" name=\"txtDateStart_" + Sup.SupplementID + "\" id=\"txtDateStart_" + Sup.SupplementID + "\" value=\"" + Sup.DateSupplement.ToString("yyyy-MM-dd") + "\" class=\"Extra_textbox\" style=\" width:120px;\" /></td>");

                    strResult.Append("<td   align=\"center\"><input type=\"checkbox\" name=\"checkbin\" id=\"checkbin_" + Sup.SupplementID + "\" value=\"" + Sup.SupplementID + "\" ></td>");
                    strResult.Append("</tr>");

                    countNum = countNum + 1;
                }
                strResult.Append("</table><br/>");
                if (countNum - 1 > 0)
                {
                    strResult.Append("<input type=\"button\" value=\"Save\" class=\"Extra_Button_small_green\" onclick=\"SupplementUpdate();return false;\" />&nbsp;<input type=\"button\" value=\"Delete\" class=\"Extra_Button_small_red\"  onclick=\"SupplementUpdatestatus();return false;\" style=\"float:right;\" />");
                }
                strResult.Append("</div>");
            }
            else
            {
                strResult.Append("<div  class=\"box_empty\">");
                strResult.Append("");
                strResult.Append("<p><label>Sorry!</label> There are no Member to display.</p>");
                strResult.Append("");
                strResult.Append("</div>");
            }

            return strResult.ToString();
        }

        //public string GetHolidayList()
        //{
        //    StringBuilder result = new StringBuilder();

        //    try
        //    {
        //        DateTime dDAteFrom = this.qDateFrom.Hotels2DateSplitYear("-");
        //        DateTime dDAteTo = this.qDateTo.Hotels2DateSplitYear("-");
        //        ProductsupplementExtranet cSupExtra = new ProductsupplementExtranet();
        //        IList<object> iListHoliday = cSupExtra.GetSupplementDateByDateRange(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteFrom, dDAteTo);
        //        if (iListHoliday.Count > 0)
        //        {
        //            result.Append("<table cellspacing=\"2\" class=\"tbl_acknow\"  >");
        //            result.Append("<tr class=\"header_field\">");
        //            result.Append("<th>Date Holiday</th><th>Amount</th>");
        //            result.Append("</tr>");


        //            //result.Append("<tr bgcolor=\"#ffffff\">");
        //            //result.Append("<td>1");
        //            //result.Append("<input type=\"hidden\" name=\"hd_date_supplement_1\" id=\"hd_date_supplement_1\" value=\"2011-09-30\" />");
        //            //result.Append("<input type=\"checkbox\" name=\"chk_supplement\" checked=\"checked\" value=\"1\" />");
        //            //result.Append("</td>");
        //            //result.Append("<td><input type=\"text\" class=\"Extra_textbox\" value=\"50\" name=\"supplement_amount_1\" id=\"supplement_amount_1\" /></td>");


        //            //result.Append("<tr bgcolor=\"#ffffff\">");
        //            //result.Append("<td>2");
        //            //result.Append("<input type=\"hidden\" name=\"hd_date_supplement_2\" id=\"hd_date_supplement_2\" value=\"2011-09-31\" />");
        //            //result.Append("<input type=\"checkbox\" name=\"chk_supplement\" checked=\"checked\" value=\"2\" />");
        //            //result.Append("</td>");
        //            //result.Append("<td><input type=\"text\" class=\"Extra_textbox\" value=\"100\" name=\"supplement_amount_2\" id=\"supplement_amount_2\" /></td>");


        //            foreach (ProductsupplementExtranet holiday in iListHoliday)
        //            {
        //                result.Append("<tr bgcolor=\"#ffffff\">");
        //                result.Append("<td>" + holiday.DateSupplement.ToString("dd-MMM-yyyy") + "");
        //                result.Append("<input type=\"hidden\" name=\"hd_date_supplement_" + holiday.SupplementID + "\" id=\"hd_date_supplement_" + holiday.SupplementID + "\" value=\"" + holiday.DateSupplement.ToString("yyyy-MM-dd") + "\" />");
        //                result.Append("<input type=\"checkbox\" name=\"chk_supplement\" checked=\"checked\" value=\"" + holiday.SupplementID + "\" />");
        //                result.Append("</td>");
        //                result.Append("<td><input type=\"text\" class=\"Extra_textbox\" name=\"supplement_amount_" + holiday.SupplementID + "\" id=\"supplement_amount_" + holiday.SupplementID + "\" /></td>");
        //            }
                    
        //            result.Append("</table>");
        //        }
        //        else
        //        {
        //            result.Append("<div class=\"block_notice\">");
        //            result.Append("<p>No Holiday Please Insert Before</p>");
        //            result.Append("");
        //            result.Append("");
        //            result.Append("<div>");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("Error:" + ex.Message);
        //        Response.End();
        //    }

        //    return result.ToString();
        //}

        
        
    }
}