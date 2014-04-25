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
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_room_manage_list : Hotels2BasePageExtra_Ajax
    {

        public string qStatusId {
            get { return Request.QueryString["status"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qStatusId))
                {
                  
                    Response.Write(GetRoomTypeList(this.qStatusId));

                    Response.Flush();
                }
               
            }
        }

        public string GetRoomTypeList(string strStatus)
        {

            bool bolStatus = false;
            if (strStatus == "1")
                bolStatus = true;
            if(strStatus == "0")
                bolStatus = false;

            
            StringBuilder result = new StringBuilder();

            result.Append("");
            result.Append("");
            result.Append("");
            result.Append("");
            result.Append("");
            result.Append("");
            result.Append("");
            result.Append("");

            Option cOPtion = new Option();
            IList<object> iListOptionRoom = cOPtion.GetProductOptionByProductId_Extranet_ByCat(this.CurrentProductActiveExtra, this.CurrentSupplierId, 38, bolStatus);


            IList<object> iListOptionExtraber = cOPtion.GetProductOptionByProductId_Extranet_ByCat(this.CurrentProductActiveExtra, this.CurrentSupplierId, 39, bolStatus);
            IList<object> iListOptionMeal = cOPtion.GetProductOptionByProductId_Extranet_ByCat(this.CurrentProductActiveExtra, this.CurrentSupplierId, 58, bolStatus);
            IList<object> iListOptionTransfer = cOPtion.GetProductOptionByProductId_Extranet_ByCat(this.CurrentProductActiveExtra, this.CurrentSupplierId, 44, bolStatus);
            result.Append("<h4><img id=\"Image4\" src=\"/images/content.png\" /> Product List</h4>");
            result.Append("<table width=\"100%\" class=\"tbl_acknow\" cellspacing=\"2\" cellpadding=\"0\" >");
            result.Append("<tr class=\"header_field\" align=\"center\">");
            result.Append("<th style=\"width:5%;\">No.</th><th style=\"width:70%;\">Product </th><th style=\"width:10%;text-align:center;\"><input type=\"button\" value=\"change now\" class=\"Extra_Button_small_green\" onclick=\"savePriority();\" /></th><th style=\"width:10%;text-align:center;\">Manage</th>");
            result.Append("</tr>");
            
            int count = 1;
            //HttpContext.Current.Response.Write(iListOptionRoom.Count);
            //HttpContext.Current.Response.End();
            if (iListOptionRoom.Count > 0)
            {
                result.Append("<tr bgcolor=\"#f0f0f0\" style=\"font-weight:bold;font-size:12px; color:#333333\"><td colspan=\"4\">Room Type</td></tr>");
                foreach (Option Room in iListOptionRoom)
                {
                    string bgcolor = "bgcolor=\"#ffffff\"";
                    if (count % 2 == 0)
                        bgcolor = "bgcolor=\"#F9F8E1\"";

                    result.Append("<tr " + bgcolor + ">");
                    result.Append("<td style=\"text-align:center;\">" + count + "</td>");
                    result.Append("<td>" + Room.Title + "</td>");
                    result.Append("<td style=\"text-align:center;\"><input type=\"text\" name=\"option_list_priority\" id=\"" + Room.OptionID + "\" class=\"Extra_textbox\" style=\"width:30px;\" value=\"" + Room.Priority + "\" /></td>");
                    result.Append("<td style=\"text-align:center;\"> <img src=\"/images_extra/edit.png\" style=\"cursor:pointer;\" onclick=\"javascript:popup('popup_edit_option_detail.aspx?oid=" + Room.OptionID + AppendQueryString + "',800,800)\" />  </td>");

                    result.Append("</tr>");
                    count = count + 1;
                }
            }

            if (iListOptionExtraber.Count > 0)
            {
                result.Append("<tr bgcolor=\"#f0f0f0\" style=\"font-weight:bold;font-size:12px;color:#333333\"><td colspan=\"4\">Extra bed</td></tr>");
                foreach (Option extra in iListOptionExtraber)
                {
                    string bgcolor = "bgcolor=\"#ffffff\"";
                    if (count % 2 == 0)
                        bgcolor = "bgcolor=\"#F9F8E1\"";

                    result.Append("<tr " + bgcolor + ">");
                    result.Append("<td style=\"text-align:center;\">" + count + "</td>");
                    result.Append("<td>" + extra.Title + "</td>");
                    result.Append("<td style=\"text-align:center;\"><input type=\"text\" name=\"option_list_priority\" id=\"" + extra .OptionID+ "\" class=\"Extra_textbox\" style=\"width:30px;\" value=\"" + extra.Priority + "\" /></td>");
                    result.Append("<td style=\"text-align:center;\"><img src=\"/images_extra/edit.png\" style=\"cursor:pointer;\" onclick=\"javascript:popup('popup_edit_option_detail.aspx?oid=" + extra.OptionID + AppendQueryString + "',600,800)\" />  </td>");

                    result.Append("</tr>");
                    count = count + 1;
                }

            }
            if (iListOptionMeal.Count > 0)
            {
                result.Append("<tr bgcolor=\"#f0f0f0\" style=\"font-weight:bold;font-size:12px;color:#333333\"><td colspan=\"4\">Meal</td></tr>");
                foreach (Option meal in iListOptionMeal)
                {
                    string bgcolor = "bgcolor=\"#ffffff\"";
                    if (count % 2 == 0)
                        bgcolor = "bgcolor=\"#F9F8E1\"";

                    result.Append("<tr " + bgcolor + ">");
                    result.Append("<td style=\"text-align:center;\">" + count + "</td>");
                    result.Append("<td>" + meal.Title + "</td>");
                    result.Append("<td style=\"text-align:center;\"><input type=\"text\" name=\"option_list_priority\" id=\"" + meal.OptionID + "\" class=\"Extra_textbox\" style=\"width:30px;\" value=\"" + meal.Priority + "\" /></td>");
                    result.Append("<td style=\"text-align:center;\"><img src=\"/images_extra/edit.png\" style=\"cursor:pointer;\" onclick=\"javascript:popup('popup_edit_option_detail.aspx?oid=" + meal.OptionID + AppendQueryString + "',600,800)\" />  </td>");

                    result.Append("</tr>");
                    count = count + 1;
                }

            }
           
            if (iListOptionTransfer.Count > 0)
            {
                result.Append("<tr bgcolor=\"#f0f0f0\" style=\"font-weight:bold;font-size:12px;color:#333333\"><td colspan=\"4\">Transfer</td></tr>");
                foreach (Option extra in iListOptionTransfer)
                {
                    string bgcolor = "bgcolor=\"#ffffff\"";
                    if (count % 2 == 0)
                        bgcolor = "bgcolor=\"#F9F8E1\"";

                    result.Append("<tr " + bgcolor + ">");
                    result.Append("<td style=\"text-align:center;\">" + count + "</td>");
                    result.Append("<td>" + extra.Title + "</td>");
                    result.Append("<td style=\"text-align:center;\"><input type=\"text\" name=\"option_list_priority\" id=\"" + extra.OptionID + "\" class=\"Extra_textbox\" style=\"width:30px;\" value=\"" + extra.Priority + "\" /></td>");
                    result.Append("<td style=\"text-align:center;\"><img src=\"/images_extra/edit.png\" style=\"cursor:pointer;\" onclick=\"javascript:popup('popup_edit_option_detail.aspx?oid=" + extra.OptionID + AppendQueryString + "',600,800)\" />  </td>");

                    result.Append("</tr>");
                    count = count + 1;
                }
            }
           
            result.Append("</tabel>");
            
            result.Append("");
            result.Append("");
            result.Append("");
            result.Append("");
            return result.ToString();
        }
        

        
    }
}