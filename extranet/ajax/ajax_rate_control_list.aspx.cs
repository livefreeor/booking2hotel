using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_rate_control_list : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Response.Write(ratecontrolsearchBox());
                Response.End();
            }
        }

        public string ratecontrolsearchBox()
        {
            StringBuilder result = new StringBuilder();
            try
            {
               
                DateTime dDateStart = Request.Form["hd_rate_control_date_start"].Hotels2DateSplitYear("-");
                DateTime dDateend = Request.Form["hd_rate_control_date_end"].Hotels2DateSplitYear("-");


                result.Append("<table  cellspacing=\"2\" class=\"tbl_acknow\" >");
                result.Append("<tr class=\"header_field\" >");
                result.Append("<th>Date</th>");
                result.Append("<th>Day</th>");
                result.Append("<th>Rate</th>");
                result.Append("<th></th>");
                result.Append("</tr>");
                result.Append("<tr align=\"center\" bgcolor=\"#f5f6f6\">");
                result.Append("<td></td>");
                result.Append("<td></td>");
                result.Append("<td><input type=\"text\" class=\"Extra_textbox_yellow\" id=\"rate_auto_fill\" /></td>");
                result.Append("<td><input type=\"button\" value=\"Auto Fill\" class=\"Extra_Button_small_blue\" onclick=\"Rateautofill();return false;\" /></td>");
                result.Append("</tr>");

                int intConditionId = int.Parse(Request.Form["rate_control_condition"]);

                int DateDiff = dDateend.Subtract(dDateStart).Days;
                DateTime dDateCurrent = DateTime.Now;

                PoductPriceExtra cPriceExtra = new PoductPriceExtra();
                List<object> ListPrice = cPriceExtra.getPriceExtraAll(intConditionId);
                
                for (int days = 0; days <= DateDiff; days++)
                {
                    dDateCurrent = dDateStart.AddDays(days);

                    string decPriceRate = string.Empty;
                    DateTime dDatePrice = dDateCurrent;
                    string CheckedBoxKeyValue = string.Empty;
                    string Key = string.Empty;
                    string[] DayShortName = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
                    int count = 0;
                    foreach (PoductPriceExtra PriceItem in ListPrice)
                    {
                        if (PriceItem.DatePrice.Date == dDateCurrent.Date)
                        {
                            decPriceRate = PriceItem.Price.ToString("#,0");
                            Key = PriceItem.PriceId.ToString();
                            CheckedBoxKeyValue = "<input type=\"checkbox\" value=\"" + PriceItem.PriceId + "\" checked=\"checked\" name=\"chk_to_update\" style=\"display:none;\"/>";
                            count = count + 1;
                            break;
                            
                        }
                    }
                   
                    if (count == 0)
                    {
                        Key = intConditionId + "_" + dDatePrice.ToString("yyyy-MM-dd");
                        CheckedBoxKeyValue = "<input type=\"checkbox\" value=\"" + Key + "\" checked=\"checked\" name=\"chk_to_insert\" style=\"display:none;\"/>";
                    }
                    

                    result.Append("<tr bgcolor=\"#ffffff\" align=\"center\">");
                    result.Append("<td>" + dDatePrice.ToString("dd-MMM-yyyy") + CheckedBoxKeyValue + "</td>");

                    result.Append("<td>" + DayShortName[(int)dDatePrice.DayOfWeek] + "</td>");
                    result.Append("<td>");
                    result.Append("<input type=\"text\" class=\"Extra_textbox_yellow\" id=\"rate_control_price_" + Key + "\" name=\"rate_control_price_" + Key + "\" value=\"" + decPriceRate + "\" />");
                    result.Append("</td>");
                    result.Append("<td></td>");
                    result.Append("</tr>");
                }

                result.Append("</table>");


                result.Append("<div id=\"condition_manage_save\" style=\"text-align:center; margin:15px 0px 0px ; padding:10px; border:1px solid #f7f3da; background-color:#fbfbf9;\">");
    //result.Append("<p>*Please check information and rate above before click to save</p>");

    result.Append("<input type=\"button\" value=\"Save\" onclick=\"SaveRateEdit();return false;\"  class=\"Extra_Button_green\" >&nbsp;");
    result.Append("<input type=\"button\" value=\"Reset\" onclick=\"resetAutofill();return false;\" class=\"Extra_Button_small_white\" />");
    
    result.Append("</div>");
                result.Append("<table>");
                result.Append("<tr>");
                result.Append("<td></td>");
                result.Append("<td></td>");

                result.Append("</tr>");
                result.Append("</table>");
               
            }
            catch (Exception ex)
            {
                Response.Write("error : " + ex.Message);
                Response.End();
            }

            return result.ToString();
        }
    }
}