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
    public partial class admin_ajax_extrabed_form_insertbox : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Response.Write(getExtraBed_InsertForm());
                Response.End();
            }
        }

        public string getExtraBed_InsertForm()
        {
           
            StringBuilder result = new StringBuilder();

            result.Append("<form id=\"extra_bed_insertform\" action=\"\" >");

            result.Append("<div class=\"formbox_head\">Insert new Extra Bed</div>");
            result.Append("<div class=\"formbox_body\">");

            result.Append("<p class=\"form_box_p\">");
            result.Append("<input type=\"checkbox\" name=\"check_extra_isabf\" />Breakfast&nbsp;&nbsp;");
            result.Append("<input type=\"checkbox\" name=\"check_extra_isforchild\" />For child&nbsp;&nbsp;");
            
            result.Append("<select name=\"extrabed_age_child\" id=\"extrabed_age_child\" class=\"Extra_Drop\">");
            for (int i = 1; i <= 15; i++)
            {
                result.Append("<option value=\"" + i + "\">" + i + "</option>");
            }
            result.Append("</select>&nbsp;&nbsp;");
            result.Append("Years old");
            result.Append("");
            result.Append("</p>");

            result.Append("<div class=\"form_box_p_border\">");
            result.Append("<table>");
            result.Append("<tr>");
            result.Append("<td>Date Range From</td>");
            result.Append("<td><input type=\"textbox\" id=\"value_date_start\" class=\"Extra_textbox\" /></td>");
            result.Append("<td>To</td>");
            result.Append("<td><input type=\"textbox\" id=\"value_date_end\"  class=\"Extra_textbox\" /></td>");
            result.Append("<td><input type=\"button\" value=\"Add\" id=\"btnaddValueExtraList\" onclick=\"addValueExtrabed();return false;\" /></td>");
            result.Append("</tr>");
            result.Append("</table>");
            result.Append("</div>");

            result.Append("<div id=\"dateInsertlist\">");

            result.Append("<div id=\"rate_load_head\" >");
            result.Append("<table width=\"100%\" >");
            result.Append("<tr bgcolor=\"#96b4f3\" align=\"center\"><td width=\"20%\">Date From</td><td width=\"20%\">Date To</td>");
            result.Append("<td width=\"20%\">Amount</td>");
            result.Append("<td width=\"20%\">Delete</td>");
            result.Append("</tr>");
            result.Append("</table>");
            result.Append("</div>");

            result.Append("</div>");

            result.Append("</div>");
            result.Append("<div class=\"formbox_buttom\" id=\"formbox_buttom\"><input type=\"button\" value=\"Save\" onclick=\"InsertnewExtrabedSave();\"  class=\"btStyleGreen\" />&nbsp;<input type=\"button\" value=\"Cancel\" onclick=\"DarkmanPopUp_Close();\" class=\"btStyleWhite\" style=\" width:80px\" /></div>");

            result.Append("</form>");


            return result.ToString();
        }

    }
}