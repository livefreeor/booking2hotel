using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using System.Text;
using Hotels2thailand.Production;
using Hotels2thailand.Member ;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_member_price_condition_sel : Hotels2BasePageExtra_Ajax
    {
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                Response.Write(GetRoomAndConditionSelect());
                Response.End();
            }
        }

        public string GetRoomAndConditionSelect()
        {
            StringBuilder result = new StringBuilder();

            Option cOption = new Option();
            ProductConditionExtra cConditionExtra = new ProductConditionExtra();
            IList<object> iListOption = cOption.GetProductOptionByProductId_RoomOnlyExtranet(this.CurrentProductActiveExtra, this.CurrentSupplierId);
            int coditionCount = 0;

            result.Append("<div id=\"date_insert_box\">");
            result.Append("<table>");
            result.Append("<tr>");
            result.Append("<td><span class=\"datetitle\">Date-Start</span><input type=\"text\" id=\"member_date_start\" name=\"member_date_start\" class=\"Extra_textbox\" /></td>");
            result.Append("<td style=\"width:15px\"></td>");
            result.Append("<td><span class=\"datetitle\">Date-End</span><input type=\"text\" id=\"member_date_end\" class=\"Extra_textbox\" name=\"member_date_end\" /></</td>");
            result.Append("<td style=\"width:15px\"></td>");
            result.Append("<td><span class=\"datetitle\">Amount</span><input type=\"text\" id=\"member_amount\" class=\"Extra_textbox_yellow\" name=\"member_amount\" style=\"width:70px\" />&nbsp;<label style=\"font-weight:bold;\">(%)</label></td>");
            
            result.Append("</tr>");
            
            result.Append("</table>");
            result.Append("</div>");

            result.Append("<table width=\"100%\">");
            foreach (Option option in iListOption)
            {
                List<object> ConditionExtraList = cConditionExtra.getConditionListByOptionId(option.OptionID, 1);

                coditionCount = ConditionExtraList.Count();

                result.Append("<tr>");
                result.Append("<td>");
                if (coditionCount > 0)
                {
                    result.Append("<p class=\"room_title\"><input type=\"checkbox\" title=\"" + option.Title + "\" value=\"" + option.OptionID + "\" id=\"chk_room_" + option.OptionID + "\" name=\"checkbox_room_check\" />");
                    result.Append("<label >" + option.Title + "</label></p>");
                   
                }
                else
                {
                    result.Append("<p class=\"room_title\"><input type=\"checkbox\" title=\"" + option.Title + "\" value=\"" + option.OptionID + "\" id=\"chk_room_" + option.OptionID + "\" name=\"checkbox_room_check\" disabled=\"disabled\" />");
                    result.Append("<label style=\"color:#ccccc1;\">" + option.Title + "</label>");
                    result.Append("<label style=\"color:#f03d25;font-size:11px;\">***</label></p>");
                    
                }

                result.Append("<div id=\"condition_list" + option.OptionID + "\" class=\"condition_select_list\"  >");
                result.Append("<table>");


                //Pease create the conditions of this room type before you load your rate.

                if (ConditionExtraList.Count() > 0)
                {
                    foreach (ProductConditionExtra conditionList in ConditionExtraList)
                    {
                        result.Append("<tr><td>");
                        result.Append("<input type=\"checkbox\" value=\"" + conditionList.ConditionId + "\"  title=\"" + conditionList.Title + "\" id=\"chk_condition_" + conditionList.ConditionId + "\" name=\"checkbox_condition_check\" />");
                        result.Append("<label id=\"checkCon_" + conditionList.ConditionId + "\">" + conditionList.Title + Hotels2String.AppendConditionDetailExtraNet(conditionList.NumAult, conditionList.Breakfast) + "</label>");
                        result.Append("<lable id=\"checkCon_Alert_" + conditionList.ConditionId + "\" style=\"display:none;color:red;font-size:11px;\">**</label>");

                        result.Append("</td></tr>");

                    }
                }


                result.Append("</table>");
                result.Append("</div>");
                result.Append("<div class=\"line\"></div>");
                result.Append("</td>");
                result.Append("</tr>");
            }
            result.Append("");
            result.Append("");
            result.Append("</table>");
            
            result.Append("<div id=\"member_price_insert\"><input type=\"button\" id=\"save_price_member\" value=\"Save\"  class=\"Extra_Button_small_green\"  />&nbsp;");
            result.Append("<input type=\"button\" id=\"cancelPrice_member\" value=\"Cancel\"  class=\"Extra_Button_small_white\"  /></div>");
            result.Append("</div>");
            return result.ToString();
            //dropRoom.DataTextField = "Title";
            //dropRoom.DataValueField = "OptionID";
        }

    }
}