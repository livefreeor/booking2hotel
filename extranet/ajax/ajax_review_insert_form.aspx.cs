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
    public partial class admin_ajax_review_insert_form : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffAdd())
                { 

                }
                else
                {
                    Response.Write("method_invalid");
                }
            }
        }


        public string ReviewInsertForm()
        {
            StringBuilder result = new StringBuilder();
            


             result.Append("<form id=\"review_insert_form\" action=\"\" method=\"post\">");
             result.Append("<div id=\"review_box\">");
             result.Append("<div class=\"review_box_header\">Review</div>   ");

             result.Append("<div class=\"review_rate_select_box\">");
             result.Append("<ul id=\"ratingPan\"><li><strong>Overall</strong><br/><input name=\"rate_overall\" type=\"radio\" class=\"star\" value=\"1\" />");
             result.Append("<input name=\"rate_overall\" type=\"radio\" class=\"star\" value=\"2\" />");
             result.Append("<input name=\"rate_overall\" type=\"radio\" class=\"star\" value=\"3\" />");
             result.Append("<input name=\"rate_overall\" type=\"radio\" class=\"star\" value=\"4\" />");
             result.Append("<input name=\"rate_overall\" type=\"radio\" class=\"star\" value=\"5\" />");
             result.Append("<input name=\"rate_overall\" type=\"radio\" class=\"star\" value=\"6\" />");
             result.Append("<input name=\"rate_overall\" type=\"radio\" class=\"star\" value=\"7\" />");
             result.Append("<input name=\"rate_overall\" type=\"radio\" class=\"star\" value=\"8\" />");
             result.Append("<input name=\"rate_overall\" type=\"radio\" class=\"star\" value=\"9\" />");
             result.Append("<input name=\"rate_overall\" type=\"radio\" class=\"star\" value=\"10\" />");
             result.Append("</li>");
             result.Append("<li><strong>Service</strong><br/><input name=\"rate_service\" type=\"radio\" class=\"star\" value=\"1\" />");
             result.Append("<input name=\"rate_service\" type=\"radio\" class=\"star\" value=\"2\" />");
             result.Append("<input name=\"rate_service\" type=\"radio\" class=\"star\" value=\"3\" />");
             result.Append("<input name=\"rate_service\" type=\"radio\" class=\"star\" value=\"4\" />");
             result.Append("<input name=\"rate_service\" type=\"radio\" class=\"star\" value=\"5\" />");
             result.Append("<input name=\"rate_service\" type=\"radio\" class=\"star\" value=\"6\" />");
             result.Append("input name=\"rate_service\" type=\"radio\" class=\"star\" value=\"7\" />");
             result.Append("<input name=\"rate_service\" type=\"radio\" class=\"star\" value=\"8\" />");
             result.Append("<input name=\"rate_service\" type=\"radio\" class=\"star\" value=\"9\" />");
             result.Append("<input name=\"rate_service\" type=\"radio\" class=\"star\" value=\"10\" />");
             result.Append("</li>");
             result.Append("<li><strong>Location</strong><br/><input name=\"rate_location\" type=\"radio\" class=\"star\" value=\"1\" />");
             result.Append("<input name=\"rate_location\" type=\"radio\" class=\"star\" value=\"2\" />");
             result.Append("<input name=\"rate_location\" type=\"radio\" class=\"star\" value=\"3\" />");
             result.Append("<input name=\"rate_location\" type=\"radio\" class=\"star\" value=\"4\" />");
             result.Append("<input name=\"rate_location\" type=\"radio\" class=\"star\" value=\"5\" />");
             result.Append("<input name=\"rate_location\" type=\"radio\" class=\"star\" value=\"6\" />");
             result.Append("<input name=\"rate_location\" type=\"radio\" class=\"star\" value=\"7\" />");
             result.Append("<input name=\"rate_location\" type=\"radio\" class=\"star\" value=\"8\" />");
             result.Append("<input name=\"rate_location\" type=\"radio\" class=\"star\" value=\"9\" />");
             result.Append("<input name=\"rate_location\" type=\"radio\" class=\"star\" value=\"10\" />");
             result.Append("</li>");
             result.Append("<li><strong>Cleanliness</strong><br/><input name=\"rate_cleanliness\" type=\"radio\" class=\"star\" value=\"1\" />");
             result.Append("<input name=\"rate_cleanliness\" type=\"radio\" class=\"star\" value=\"2\" />");
             result.Append("<input name=\"rate_cleanliness\" type=\"radio\" class=\"star\" value=\"3\" />");
             result.Append("<input name=\"rate_cleanliness\" type=\"radio\" class=\"star\" value=\"4\" />");
             result.Append("<input name=\"rate_cleanliness\" type=\"radio\" class=\"star\" value=\"5\" />");
             result.Append("<input name=\"rate_cleanliness\" type=\"radio\" class=\"star\" value=\"6\" />");
             result.Append("<input name=\"rate_cleanliness\" type=\"radio\" class=\"star\" value=\"7\" />");
             result.Append("<input name=\"rate_cleanliness\" type=\"radio\" class=\"star\" value=\"8\" />");
             result.Append("<input name=\"rate_cleanliness\" type=\"radio\" class=\"star\" value=\"9\" />");
             result.Append("<input name=\"rate_cleanliness\" type=\"radio\" class=\"star\" value=\"10\" />");
             result.Append("</li>");
             result.Append("<li><strong>Rooms</strong><br/><input name=\"rate_rooms\" type=\"radio\" class=\"star\" value=\"1\" />");
             result.Append("<input name=\"rate_rooms\" type=\"radio\" class=\"star\" value=\"2\" />");
             result.Append("<input name=\"rate_rooms\" type=\"radio\" class=\"star\" value=\"3\" />");
             result.Append("<input name=\"rate_rooms\" type=\"radio\" class=\"star\" value=\"4\" />");
             result.Append("<input name=\"rate_rooms\" type=\"radio\" class=\"star\" value=\"5\" />");
             result.Append("<input name=\"rate_rooms\" type=\"radio\" class=\"star\" value=\"6\" />");
             result.Append("<input name=\"rate_rooms\" type=\"radio\" class=\"star\" value=\"7\" />");
             result.Append("<input name=\"rate_rooms\" type=\"radio\" class=\"star\" value=\"8\" />");
             result.Append("<input name=\"rate_rooms\" type=\"radio\" class=\"star\" value=\"9\" />");
             result.Append("<input name=\"rate_rooms\" type=\"radio\" class=\"star\" value=\"10\" />");
             result.Append("</li>");
             result.Append("<li><strong>Value for money</strong><br/><input name=\"rate_value_for_money\" type=\"radio\" class=\"star\" value=\"1\" />");
             result.Append("<input name=\"rate_value_for_money\" type=\"radio\" class=\"star\" value=\"2\" />");
             result.Append("<input name=\"rate_value_for_money\" type=\"radio\" class=\"star\" value=\"3\" />");
             result.Append("<input name=\"rate_value_for_money\" type=\"radio\" class=\"star\" value=\"4\" />");
             result.Append("<input name=\"rate_value_for_money\" type=\"radio\" class=\"star\" value=\"5\" />");
             result.Append("<input name=\"rate_value_for_money\" type=\"radio\" class=\"star\" value=\"6\" />");
             result.Append("<input name=\"rate_value_for_money\" type=\"radio\" class=\"star\" value=\"7\" />");
             result.Append("<input name=\"rate_value_for_money\" type=\"radio\" class=\"star\" value=\"8\" />");
             result.Append("<input name=\"rate_value_for_money\" type=\"radio\" class=\"star\" value=\"9\" />");
             result.Append("<input name=\"rate_value_for_money\" type=\"radio\" class=\"star\" value=\"10\" />");
             result.Append("</li>");
             result.Append("</ul>");
             result.Append("<br class=\"clear-all\" />");
             result.Append("</div>");

             result.Append("<div class=\"rating_header_orage\">* Review Title<br /><span>(Example: Excellent location with supreb food.)</span>");
             result.Append("<br />");
             result.Append("<input type=\"text\" name=\"review_title\" id=\"review_title\" class=\"inputbox_rating\" />");
             result.Append("</div>");

             result.Append("<div class=\"rating_header_orage\">* Your Review <br /><span>(What did you like or dislike about this place and why?)</span><br />");
             result.Append("<textarea name=\"review_detail\" rows=\"5\" cols=\"45\"></textarea>");
             result.Append("</div>");
             result.Append("<div class=\"rating_header_orage\">&nbsp;&nbsp;Your Name<br />");
             result.Append("<input type=\"text\" name=\"cus_name\" value=\"\" class=\"inputbox_rating\"/>");
             result.Append("<br /></div>");

             result.Append("<div class=\"rating_header_orage\">&nbsp;&nbsp;Where are you from?  <br /><span>&nbsp;&nbsp;(Example: London, UK)</span><br />");
             result.Append("<input type=\"text\" name=\"cus_from\" value=\"\" class=\"inputbox_rating\"/>");
             result.Append("</div>");

             result.Append("<div id=\"rating_buttom\"> ");
             result.Append("<input type=\"hidden\" name=\"product\" value=\"624\"/><input type=\"hidden\" name=\"category\" value=\"29\"/>");
             result.Append("<br /><input type=\"submit\" name=\"btntest\" class=\"rating_sumbit\" value=\"\" /> </div>");

             result.Append("</div><!--review_box-->");
             result.Append("</form>");

             return result.ToString();



        }
    }
}