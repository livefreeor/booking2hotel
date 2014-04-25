using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_meal_insertbox : Hotels2BasePageExtra_Ajax
    {
        public string qoptionCat
        {
            get { return Request.QueryString["opcat"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Response.Write(getInsertBox());
                Response.End();
            }
        }

        public string getInsertBox()
        {
            StringBuilder result = new StringBuilder();
            try
            {
                // select Extrabed From ThisProduct 
                Option cOption = new Option();
                //List<object> Extrabed = cOption.GetProductOptionByCatIdAndProductId(this.CurrentProductActiveExtra, 39);
                //cOption = cOption.GetProductOptionTop1_Extrnet(this.CurrentProductActiveExtra, 39, this.CurrentSupplierId);

                byte bytOptionCat = byte.Parse(this.qoptionCat);
                IList<object> ListOption = cOption.GetProductOptionByCurrentSupplierANDProductIdANDCATID_OpenOnly(this.CurrentSupplierId, this.CurrentProductActiveExtra, bytOptionCat);


                result.Append("<form id=\"extra_bed_insertform\" action=\"\" >");
                if (ListOption.Count > 0)
                {
                    //result.Append("<input type=\"hidden\" name=\"hd_optionId\" id=\"hd_optionId\"  value=\"" + cOption.OptionID + "\" />");
                    
                    result.Append("<table  width=\"100%\">");
                   
                    result.Append("<tr>");
                    result.Append("<td align=\"left\"style=\"width:130px;\" ><label>Current Meal</lable></td>");
                    result.Append("<td align=\"left\"><select id=\"drop_option\" name=\"drop_option\"  style=\"width:450px;\" class=\"Extra_Drop\">");
                    foreach (Option option in ListOption)
                    {
                        result.Append("<option value=\""+option.OptionID+"\">"+option.Title+"</option>");
                        
                    }
                    
                    result.Append("</select>");
                    result.Append("</td>");
                    result.Append("</tr>");
                    result.Append("<tr>");
                    result.Append("<td colspan=\"2\" align=\"left\">");
                    result.Append("<table width=\"100%\">");
            result.Append("<tr>");
            result.Append("<td align=\"left\"><label>Date Range From </label></td>");
            result.Append("<td><input type=\"text\" id=\"rate_DateStart\" name=\"rate_DateStart\" readonly=\"readonly\" class=\"Extra_textbox\" style=\"width:120px;\"/></td>");
                result.Append("<td align=\"right\"><label> To </label></td>");
                result.Append("<td><input type=\"text\" id=\"rate_DateEnd\" name=\"rate_DateEnd\" readonly=\"readonly\" class=\"Extra_textbox\" style=\"width:120px;\"   /></td>");
                result.Append("<td align=\"right\"><label>Amount</label></td>");
                result.Append("<td>");
                result.Append("<input type=\"text\" id=\"rate_amount\" name=\"rate_amount\" class=\"Extra_textbox_yellow\" style=\"width:80px;\" /></td>");
               
                result.Append("<td align=\"right\">");

                result.Append("<input type=\"checkbox\" id=\"sur_checked\" name=\"sur_checked\" value=\"1\" onclick=\"SurCharge_Checked();\" />");
                result.Append("</td>");
                result.Append("<td align=\"left\"><label>Surcharge includes</label></td>");
                result.Append("<td><input type=\"button\" id=\"Button2\" value=\"Save\" onclick=\"AddRate(); return false;\" class=\"Extra_Button_small_blue\" /></td>");
            result.Append("</tr>");
      
            result.Append("<tr>");
                result.Append("<td colspan=\"10\">");
                    result.Append("<div id=\"surcharge_amount\" style=\"display:none;\" >");
                        result.Append("<div id=\"dayofweek_surcharge\">");
                        result.Append("<table>");
                            result.Append("<tr>");
                                result.Append("<td><label > Nomal Day Surcharge</label></td>");
                                result.Append("<td >");
                                result.Append("<div class=\"day_list\" id=\"day_list\">");
                                    result.Append("<p><input type=\"checkbox\" id=\"Sun\" value=\"0\" name=\"dayofWeek\" />Sun</p>");
                                    result.Append("<p><input type=\"checkbox\" id=\"Mon\" value=\"1\" name=\"dayofWeek\"/>Mon</p>");
                                    result.Append("<p><input type=\"checkbox\" id=\"Tue\" value=\"2\" name=\"dayofWeek\"/>Tue</p>");
                                    result.Append("<p><input type=\"checkbox\" id=\"Wed\" value=\"3\" name=\"dayofWeek\" />Wed</p>");
                                    result.Append("<p><input type=\"checkbox\" id=\"Thu\" value=\"4\" name=\"dayofWeek\" />Thu</p>");
                                    result.Append("<p><input type=\"checkbox\" id=\"Fri\" value=\"5\" name=\"dayofWeek\" />Fri</p>");
                                    result.Append("<p><input type=\"checkbox\" id=\"Sat\" value=\"6\" name=\"dayofWeek\" />Sat</p>");
                                    result.Append("</div>");
                                result.Append("</td>");

                                result.Append("<td ><label>Amount</label></td>");
                                result.Append("<td>");
                                result.Append("    <input type=\"text\" id=\"sur_amount\" name=\"sur_amount\" class=\"Extra_textbox_yellow\" style=\"width:80px; padding:2px;\" />");
                                result.Append("</td>");
                            result.Append("</tr>");
                        result.Append("</table>");
                        result.Append("</div>");
                        result.Append("<div id=\"holiday_surcharge\">");
                            result.Append("<p class=\"holiday_surcharge_head\"><label> Holiday surcharge</label></p>");
                            result.Append("<div id=\"holiday_surcharge_charge\">");
                             
                            result.Append("</div>");
                        result.Append("</div>");
                        result.Append("<div style= \"text-align:right\">");
                        result.Append("<input type=\"button\" id=\"Button3\" value=\"Save\" onclick=\"AddRate(); return false;\" class=\"Extra_Button_small_blue\" />");
                        result.Append("</div>");
                    result.Append("</div>");
                    result.Append("</td>");
            result.Append("</tr>");
            
            result.Append("</table>");
                    result.Append("</td>");
                    result.Append("</tr>");
                    result.Append("</table>");
                    
                }

                else
                {
                    result.Append("<div id=\"empty_extrabed\">");
                    result.Append("<p>there are no any item ; Please Insert new before </p>");
                    result.Append("");
                    result.Append("</div>");
                }

                result.Append("</form>");
            }
            catch (Exception ex)
            {
                Response.Write("error : " + ex.Message);
            }

            

            return result.ToString();
        }
    }
}