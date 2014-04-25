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
    public partial class admin_ajax_rate_control_searchbox : Hotels2BasePageExtra_Ajax
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

            Option cOption = new Option();

            List<object> ListRoom = cOption.GetProductOptionByProductId_RoomOnlyExtranet(this.CurrentProductActiveExtra, this.CurrentSupplierId);

            result.Append("<form id=\"rate_control_form_search\" action=\"\" >");
            result.Append("<table width=\"100%\">");
            result.Append("<tr>");
            
           
            result.Append("<td><label>Room Type</label></td><td>");
            result.Append("<select name=\"rate_control_room_type\"  id=\"rate_control_room_type\" class=\"Extra_Drop\" style=\"width:350px;\">");

            foreach (Option optionItem in ListRoom)
            {
                result.Append("<option value=\""+optionItem.OptionID+"\" >"+optionItem.Title+"</option>");
            }
            
            result.Append("</select>");
            result.Append("</td>");

            result.Append("<td style=\"width:50px;\"><label>Condition</label></td><td style=\"width:370px;\">");
            result.Append("<div id=\"rate_control_condition\"></div>");
            result.Append("</td>");
            result.Append("</tr>");

            result.Append("<tr><td colspan=\"4\">");
            result.Append("<table>");
            result.Append("<tr>");
            result.Append("<td><label>Date Range from</label></td>");
            result.Append("<td><input type=\"text\" readonly=\"readonly\" id=\"rate_control_date_start\" class=\"Extra_textbox\"   /></td>");
            result.Append("<td><label>To</label></td>");
            result.Append("<td><input  type=\"text\" readonly=\"readonly\" id=\"rate_control_date_end\" class=\"Extra_textbox\" /></td>");
            result.Append("<td><input type=\"button\"  onclick=\"ratecontrolSearch();return false;\" value=\"Search\" class=\"Extra_Button_small_blue\" /></td>");
            result.Append("<td></td>");
            result.Append("</tr>");
            result.Append("</table>");
            result.Append("</td></tr>");
            result.Append("</table>");
            result.Append("</form>");
            return result.ToString();
        }
    }
}