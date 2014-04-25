using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Hotels2thailand.Front;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;

public partial class review_write : System.Web.UI.Page
{
    public string GetStreamReader(string path)
    {
        StreamReader objReader = new StreamReader(Server.MapPath(path));
        string read = objReader.ReadToEnd();
        objReader.Close();
        return read;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder result = new StringBuilder();

        result.Append("<form id=\"review_form_front\" action=\"\" method=\"\">");
       
        result.Append("<table width=\"600\" cellpadding=\"10\" cellspacing=\"1\" bgcolor=\"#efefef\" align=\"center\" id=\"tblReview\">");
        result.Append("<tr align=\"center\"><th></th><th>1</th><th>2</th><th>3</th><th>4</th><th>5</th><th>6</th><th>7</th><th>8</th><th>9</th><th>10</th></tr>");
        result.Append("<tr align=\"center\">");
        result.Append("<td align=\"left\">Overall</td>");
        result.Append("<td><input name=\"rate_overall\" value=\"1\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_overall\" value=\"2\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_overall\" value=\"3\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_overall\" value=\"4\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_overall\" value=\"5\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_overall\" value=\"6\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_overall\" value=\"7\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_overall\" value=\"8\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_overall\" value=\"9\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_overall\" value=\"10\" type=\"radio\"></td>");
        result.Append("</tr>");
        result.Append("<tr align=\"center\">");
        result.Append("<td align=\"left\">Service</td>");
        result.Append("<td><input name=\"rate_service\" value=\"1\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_service\" value=\"2\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_service\" value=\"3\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_service\" value=\"4\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_service\" value=\"5\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_service\" value=\"6\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_service\" value=\"7\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_service\" value=\"8\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_service\" value=\"9\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_service\" value=\"10\" type=\"radio\"></td>");
        result.Append("</tr>");
        result.Append("<tr align=\"center\">");
        result.Append("<td align=\"left\">Location</td>");
        result.Append("<td><input name=\"rate_location\" value=\"1\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_location\" value=\"2\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_location\" value=\"3\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_location\" value=\"4\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_location\" value=\"5\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_location\" value=\"6\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_location\" value=\"7\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_location\" value=\"8\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_location\" value=\"9\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_location\" value=\"10\" type=\"radio\"></td>");
        result.Append("</tr>");
        result.Append("<tr align=\"center\">");
        result.Append("<td align=\"left\">Cleanliness</td>");
        result.Append("<td><input name=\"rate_cleanliness\" value=\"1\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_cleanliness\" value=\"2\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_cleanliness\" value=\"3\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_cleanliness\" value=\"4\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_cleanliness\" value=\"5\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_cleanliness\" value=\"6\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_cleanliness\" value=\"7\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_cleanliness\" value=\"8\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_cleanliness\" value=\"9\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_cleanliness\" value=\"10\" type=\"radio\"></td>");
        result.Append("</tr>");
        result.Append("<tr align=\"center\">");
        result.Append("<td align=\"left\">Rooms</td>");
        result.Append("<td><input name=\"rate_rooms\" value=\"1\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_rooms\" value=\"2\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_rooms\" value=\"3\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_rooms\" value=\"4\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_rooms\" value=\"5\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_rooms\" value=\"6\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_rooms\" value=\"7\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_rooms\" value=\"8\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_rooms\" value=\"9\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_rooms\" value=\"10\" type=\"radio\"></td>");
        result.Append("</tr>");
        result.Append("<tr align=\"center\">");
        result.Append("<td align=\"left\">Value for money</td>");
        result.Append("<td><input name=\"rate_value_for_money\" value=\"1\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_value_for_money\" value=\"2\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_value_for_money\" value=\"3\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_value_for_money\" value=\"4\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_value_for_money\" value=\"5\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_value_for_money\" value=\"6\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_value_for_money\" value=\"7\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_value_for_money\" value=\"8\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_value_for_money\" value=\"9\" type=\"radio\"></td>");
        result.Append("<td><input name=\"rate_value_for_money\" value=\"10\" type=\"radio\"></td>");
        result.Append("</tr>");
        
        result.Append("<tr><td colspan=\"11\">");
        result.Append("<br />");
        result.Append("<span class=\"fnBold fnOrange\">* Review Title</span><br />");
        result.Append("<span>(Example: Excellent location with superb food.)</span>");



        result.Append("<input type=\"text\" name=\"review_title\" id=\"review_title\"  class=\"reviewText\" /><br /><br />");
        result.Append("<span class=\"fnBold fnOrange\">* Your Review</span><br />");
        result.Append("<span>(What did you like or dislike about this place and why?)</span>");
        result.Append("<textarea class=\"reviewTextArea\" name=\"review_detail\"></textarea><br /><br />");
        result.Append("<span class=\"fnBold fnOrange\">Your Name</span><br />");
        result.Append("<input type=\"text\" name=\"cus_name\" class=\"reviewText\" /><br /><br />");
        result.Append("<span class=\"fnBold fnOrange\">Where are you from?</span><br />");
        result.Append("<span>(Example: London, UK)</span>");
        result.Append("<input type=\"text\" name=\"cus_from\" class=\"reviewText\" /><br /><br />");
       

        result.Append("</td></tr>");
        result.Append("<tr><td colspan=\"11\" align=\"center\" class=\"footer\"><a href=\"javascript:void(0)\" id=\"btnReviewSubmit\" class=\"btnSend\">Send now</a></td></tr>");
        result.Append("</table>");
        

        result.Append("</form>");
        //result.Append("$(\"#review_block_main\")")
        Response.Write("jQuery(\"#review_block_main\").html('" + result.ToString() + "');");
        Response.Write("DataBindClick();");
        //Response.Write("document.write('" + result.ToString()+ "');");
        Response.End();
    }
}