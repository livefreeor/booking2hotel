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
    public partial class admin_ajax_gala_editForm : Hotels2BasePageExtra_Ajax
    {
        public string qOptionId 
        {
            get { return Request.QueryString["oid"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Response.Write(getGalaEditform());
                //Response.Write("llll");
                Response.Flush();
            }
        }
        public string getGalaEditform()
        {
            StringBuilder result = new StringBuilder();
            try
            {
                ProductGalaExtra cOptionGalaExtra = new ProductGalaExtra();
                cOptionGalaExtra = cOptionGalaExtra.GetGalaExtraNetListByOptionId(this.CurrentProductActiveExtra, this.CurrentSupplierId, int.Parse(this.qOptionId));

                string GalaForAdult = string.Empty;
                string GalaForChild = string.Empty;
                if (cOptionGalaExtra.ForAdult)
                    GalaForAdult = "checked=\"checked\"";
                 

                if (cOptionGalaExtra.ForChild)
                    GalaForChild = "checked=\"checked\"";

                result.Append("<form id=\"gala_edit_form\" action=\"\" >");

                result.Append("<div class=\"formbox_head\">Gala dinner Edit</div>");
                result.Append("<div class=\"formbox_body\" id=\"formbox_body\">");
                result.Append("<input type=\"hidden\" id=\"hd_option_Id\" name=\"hd_option_Id\" value=\""+cOptionGalaExtra.OptionId+"\" />");
                result.Append("<input type=\"hidden\" id=\"hd_price_Id\" name=\"hd_price_Id\" value=\"" + cOptionGalaExtra.PriceId + "\" />");
                result.Append("<input type=\"hidden\" id=\"hd_condition_Id\" name=\"hd_condition_Id\" value=\"" + cOptionGalaExtra.ContidionId + "\" />");
                result.Append("<table id=\"tbl_payment_list\" cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:left;\">");
                result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Gala Date</td><td>&nbsp;<input typ=\"text\" id=\"date_gala_form\" name=\"date_gala_form\" class=\"Extra_textbox\" value=\"" + cOptionGalaExtra.DatePrice.ToString("yyyy-MM-dd") + "\"  /></td></tr>");

                result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Rate For</td><td>");
                result.Append("<p style=\"margin:0px 0px 0px 0px;padding:0px 0px 0px 0px;\"><input type=\"radio\" name=\"gala_for\" value=\"0\" " + GalaForAdult + " />&nbsp;For adult</p>");
                result.Append("<p style=\"margin:0px 0px 0px 0px;padding:0px 0px 0px 0px;\"><input type=\"radio\" name=\"gala_for\" value=\"1\" " + GalaForChild + " />&nbsp;For child</p>");
                result.Append("");
                result.Append("</td></tr>");

                result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Amount</td><td>&nbsp;<input typ=\"text\" id=\"gala_amount\" name=\"gala_amount\" class=\"Extra_textbox\" value=\""+cOptionGalaExtra.Price.ToString("#,0")+"\"  /></td></tr>");
                result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Title</td><td>&nbsp;<input type=\"text\" class=\"Extra_textbox\" id=\"gala_title\" style=\"width:300px;\" name=\"gala_title\" value=\""+cOptionGalaExtra.Title+"\" /></td></tr>");

                result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Gala Detail</td><td>&nbsp;<textarea rows=\"4\" cols=\"20\" class=\"Extra_textbox\" style=\"width:300px;\" name=\"gala_detail\" >" + cOptionGalaExtra.Detail + "</textarea></td></tr>");

                result.Append("</table>");
                result.Append("</div>");
                result.Append("<div class=\"formbox_buttom\" id=\"formbox_buttom\"><input type=\"button\" value=\"Save\" onclick=\"UpdateGala();\"   />&nbsp;<input type=\"button\" value=\"Cancel\" onclick=\"DarkmanPopUp_Close();\"  style=\" width:80px\" /></div>");

                result.Append("</form>");

            }
            catch (Exception ex)
            {
                result.Append(ex.Message);
            }

            return result.ToString();
        }


        
    }
}