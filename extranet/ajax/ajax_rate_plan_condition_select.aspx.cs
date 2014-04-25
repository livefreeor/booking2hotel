using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;
using Hotels2thailand;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_rate_plan_condition_select : Hotels2BasePageExtra_Ajax
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

            ProductCondition_rate_plan_cat cRatePlanCat = new ProductCondition_rate_plan_cat();
            List<object> listRateplanCat = cRatePlanCat.GetRatePlanListAll();

            int coditionCount = 0;
            int AlertConditionCount = 0;
            result.Append("<div id=\"condition_box\" >");
            result.Append("<table width=\"100%\">");


            foreach (Option option in iListOption)
            {
                List<object> ConditionExtraList = cConditionExtra.getConditionListByOptionId(option.OptionID, 1);

                coditionCount = ConditionExtraList.Count();

                result.Append("<tr>");
                result.Append("<td >");
                if (coditionCount > 0)
                {
                    result.Append("<p class=\"room_title\"><input type=\"checkbox\" title=\"" + option.Title + "\"  style=\"display:none;\" value=\"" + option.OptionID + "\" id=\"chk_room_" + option.OptionID + "\" name=\"checkbox_room_check\" />");
                    result.Append("<label >" + option.Title + "</label></p>");
                    result.Append("<div id=\"condition_list" + option.OptionID + "\" class=\"condition_select_list\"  >");
                }
                else
                {
                    result.Append("<p class=\"room_title\"><input type=\"checkbox\" title=\"" + option.Title + "\"  style=\"display:none;\" value=\"" + option.OptionID + "\" id=\"chk_room_" + option.OptionID + "\" name=\"checkbox_room_check\" disabled=\"disabled\" />");
                    result.Append("<label style=\"color:#ccccc1;\">" + option.Title + "</label>");
                    result.Append("<label style=\"color:#f03d25;font-size:11px;\">&nbsp;***</label></p>");
                    result.Append("<div id=\"condition_list" + option.OptionID + "\" class=\"condition_select_list\"  >");
                    AlertConditionCount = AlertConditionCount + 1;
                }

                result.Append("<table>");


                //Pease create the conditions of this room type before you load your rate.
               
                if (ConditionExtraList.Count() > 0)
                {
                    foreach (ProductConditionExtra conditionList in ConditionExtraList)
                    {
                        result.Append("<tr><td style=\"width:400px\">");
                        result.Append("<input type=\"checkbox\" value=\"" + conditionList.ConditionId + "\"  title=\"" + conditionList.Title + "\" id=\"chk_condition_" + conditionList.ConditionId + "\" name=\"checkbox_condition_check\" onclick=\"inputrate(this);\" />");
                        result.Append("<label id=\"checkCon_" + conditionList.ConditionId + "\">" + conditionList.Title + Hotels2String.AppendConditionDetailExtraNet(conditionList.NumAult, conditionList.Breakfast) + "</label>");
                        result.Append("<lable id=\"checkCon_Alert_" + conditionList.ConditionId + "\" style=\"display:none;color:red;font-size:11px;\">**</label>");

                        result.Append("</td>");

                        result.Append("<td>");
                        result.Append("<label style=\"font-size:11px;\">Type:&nbsp;</label><select class=\"Extra_Drop\" disabled=\"disabled\" id=\"rate_plan_cat_" + conditionList.ConditionId + "\" name=\"rate_plan_cat_" + conditionList.ConditionId + "\">");
                        foreach (ProductCondition_rate_plan_cat cat in listRateplanCat)
                        {
                            result.Append("<option value=\""+cat.RatePlancatId+"\">"+cat.Title+"</option>");
                        }
                        result.Append("");
                        result.Append("");
                        result.Append("");
                        result.Append("</select>");
                        result.Append("</td>");
                        result.Append("<td>");
                        result.Append("<label style=\"font-size:11px;\">Amount :&nbsp;</label>");
                        result.Append("<input type=\"text\" disabled=\"disabled\" name=\"rate_plane_value_" + conditionList.ConditionId + "\" class=\"Extra_textbox\" />");
                        result.Append("");
                        result.Append("</td>");
                        result.Append("</tr>");

                    }
                }

                //

                result.Append("</table>");
                result.Append("</div>");
                result.Append("<div></div>");
                result.Append("</td>");
                result.Append("</tr>");
            }
            result.Append("");
            result.Append("");
            result.Append("</table>");

            result.Append("<br/>");

            if (AlertConditionCount > 0)
            {
                result.Append("<p id=\"condition_alert_room\" style=\"margin:0px 0px 0px 0px ; padding:0px 0px 0px 0px;color:#f03d25;font-size:11px; \" >*** Pease create the conditions of this room type before you load your rate.</p>");
            }

            result.Append("<p id=\"condition_alert\"  style=\"margin:0px 0px 0px 0px ;padding:0px 0px 0px 0px;display:none;color:#f03d25;font-size:11px; \">** This condition can not apply with this promotion.<br/>Minimum night and period of stay has been added in this condition. Please recheck.</p>");

            result.Append("<br/>");
            result.Append("<div><input class=\"Extra_Button_small_green\" onclick=\"SaveRatePlan();\" type=\"button\" value=\"Save\" /></div>");

            result.Append("</div>");

            return result.ToString();
           
        }
    }
}