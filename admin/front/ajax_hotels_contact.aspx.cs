using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Reviews;
using Hotels2thailand.Production;
using Hotels2thailand;
using System.Text;
using Hotels2thailand.Front;
using Hotels2thailand.UI;


public partial class ajax_hotels_contact : System.Web.UI.Page
{
    public string CusId
    {
        get { return Request.QueryString["cus_id"]; }
    }

    public string LangId
    {
        get { return Request.QueryString["langid"]; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            Response.Write(getformContract());
            Response.End();
        }
    }

    public string getformContract()
    {
        StringBuilder result = new StringBuilder();
        result.Append("<form name=\"formcontact\" id=\"formcontact\" action=\"\" >");
        result.Append("<div class=\"boxField\">");
        result.Append("<table   class=\"table_site_contact\" >");
        result.Append("<tr>");
        result.Append("<td class=\"title\" align=\"right\" >Name&nbsp;</td>");
        result.Append("<td><input type=\"text\" id=\"txtName\" name=\"txtName\" class=\"txtsite_contact\" /></td>");
        result.Append("</tr>");
        result.Append("<tr>");
        result.Append("<td class=\"title\" align=\"right\">Email <span style=\" margin:0px; padding:0px; color:Red\">*</span>&nbsp;</td>");
        result.Append("<td><input type=\"text\" id=\"txtEmail\" name=\"txtEmail\" class=\"txtsite_contact\" />");
        result.Append("</td>");
        result.Append("</tr>");
        result.Append("<tr>");
        result.Append("<td class=\"title\" align=\"right\">Telephone&nbsp;</td>");
        result.Append("<td align=\"left\"><input type=\"text\" id=\"txtTel\" name=\"txtTel\" class=\"txtsite_contact\" /></td>");
        result.Append("</tr>");
        result.Append("<tr>");
        result.Append("<td class=\"title\" align=\"right\">Booking ID&nbsp;</td>");
        result.Append("<td align=\"left\"><input type=\"text\" id=\"txtbooking\" name=\"txtbooking\" class=\"txtsite_contact\" /></td>");
        result.Append("</tr>");
        result.Append("<tr>");
        result.Append("<td class=\"title\" align=\"right\"></td>");
        result.Append("<td align=\"left\"><div class=\"field_container\">");
        result.Append("<select id=\"mailType\" name=\"mailType\" class=\"dropsite_conatct\" >");
        result.Append("<option value=\"Ask for Enquiry\">Ask for Enquiry</option>");
        result.Append("<option value=\"Check Availability\">Check Availability</option>");
        result.Append("<option value=\"Cancel Booking\">Cancel Booking</option>");
        result.Append("<option value=\"Add Your Property\">Add Your Property</option>");
        result.Append("<option value=\"Payment Problem\">Payment Problem</option>");
        result.Append("<option value=\"Technical Support\">Technical Support</option>");
        result.Append("<option value=\"Others\">Others</option>");
        result.Append("</select>");
        result.Append("</div>");
        result.Append("</td>");
        result.Append(" </tr>");
        result.Append("<tr>");
        result.Append(" <td class=\"title\" align=\"right\">Message&nbsp;</td>");
        result.Append("<td><textarea  rows=\"6\" class=\"txtsite_contact\" id=\"txtMessage\" name=\"txtMessage\" cols=\"10\"></textarea>");
        result.Append("</td>");
        result.Append("</tr>");
                     
        result.Append("<tr>");
        result.Append("<td class=\"title\" align=\"right\"></td>");
        result.Append("<td>");
        result.Append("<div style=\" margin:10px 0px; padding:0px; width:350px;\"><img id=\"img_captcha\" src=\"../../captcha.aspx\" /></div>");
        result.Append("<p style=\" margin:0px; padding:0px; width:250px;\" >Can't Read this?</p>");
        result.Append("<p style=\" margin:0px; padding:0px; width:250px;\" ><img src=\"../../images/refresh.png\" title=\"Get New Words\"   alt=\"Get New Words\" /><a href=\"\" style=\"text-decoration:none;font-size:11px;\" onclick=\"RefreshImage();return false;\">Get New Words</a></p>");
        result.Append("</td>");
        result.Append("</tr>");
        result.Append("<tr>");
        result.Append("<td class=\"title\" align=\"right\">Please Type the letter : </td>");
        result.Append("<td><input type=\"text\" id=\"captcha_valid\" name=\"captcha_valid\" class=\"txtsite_contact\"  style=\"width:250px\" />");
        result.Append("</td>");
        result.Append(" </tr>");
        result.Append("</table>");
        result.Append("</div>");
        result.Append("<div class=\"contac_btn\">");
        result.Append("<input type=\"submit\" id=\"submit_tell_friend\"  name=\"submit_tell_friend\" value=\"\"  class=\"send\" />");
        result.Append("</div>");
        result.Append("</form>");

        return result.ToString();
    }
  
}