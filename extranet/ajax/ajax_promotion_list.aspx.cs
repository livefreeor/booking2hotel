using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_promotion_list : Hotels2BasePageExtra_Ajax
    {
        public string qProstatus
        {
            get { return Request.QueryString["status"]; }
        }
        public string qProExpired
        {
            get { return Request.QueryString["exp"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.Page.IsPostBack)
            {

                if (!string.IsNullOrEmpty(this.qProstatus) && string.IsNullOrEmpty(this.qProExpired))
                {
                   Response.Write(PromotionList(this.qProstatus,""));
                    //Response.Write("HELLO");
                }

                if (string.IsNullOrEmpty(this.qProstatus) && !string.IsNullOrEmpty(this.qProExpired))
                {
                    Response.Write(PromotionList("",this.qProExpired));
                }
                
                Response.End();
            }
            
        }


        public string PromotionList(string strStatus,string Expired)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                PromotionExxtranet cProextranet = new PromotionExxtranet();
                PromotionDayuseExtranet cProDayUse = new PromotionDayuseExtranet();

                PromotionGroupItem cProgroupItem = new PromotionGroupItem();
                IList<object> iListPromotionList = null;
                string isActive = "Active";

                if (string.IsNullOrEmpty(Expired))
                {
                    bool bolStatus = true;

                    if (strStatus == "0")
                    {
                        bolStatus = false;
                        isActive = "Inactive";
                    }

                    iListPromotionList = cProextranet.getPromotionListExtranet(this.CurrentProductActiveExtra, this.CurrentSupplierId, bolStatus);
                }
                else
                {
                    isActive = "Expired in 3 month";
                    iListPromotionList = cProextranet.getPromotionListExtranetExprired(this.CurrentProductActiveExtra, this.CurrentSupplierId, true, byte.Parse(Expired));
                }


                result.Append("<p class=\"pro_status_title\">" + isActive + "</p>");
                
                if (iListPromotionList.Count > 0)
                {

                    result.Append("<table width=\"100%\" class=\"tbl_acknow\" cellspacing=\"2\" >");
                    result.Append("<tr class=\"header_field\" align=\"center\">");
                    result.Append("<th style=\"width:10%;\">Code</th><th style=\"width:10%;\">Booking date </th><th style=\"width:10%;\">Period stay</th>");
                    result.Append("<th style=\"width:10%;\">Pro type</th><th style=\"width:40%;\">Benefit</th><th style=\"width:10%;\">condition</th><th style=\"width:10%;\">Datesubmit</th><th style=\"width:10%;\">edit</th>");
                    result.Append("</tr>");

                    int count = 1;

                    foreach (PromotionExxtranet promotion in iListPromotionList)
                    {

                        cProDayUse = cProDayUse.getUseDatePromotionTop1byPromotionID(promotion.PromotionId);

                        cProgroupItem = cProgroupItem.getProgroupItemById(promotion.ProgroupItemId, 1);
                       
                        string bgcolor = "bgcolor=\"#ffffff\"";

                        string hotelCode = "<input type=\"input\" style=\"width:80px;text-align:center;\" class=\"Extra_textbox\" name=\"pro_code_hotel_" + promotion.PromotionId + "\" id=\"" + promotion.PromotionId + "\" value=\"" + promotion.ProCode + "\" />";
                        //if (string.IsNullOrEmpty(promotion.ProCode))
                        //    hotelCode = "<input type=\"input\" OnBlur=\"InsertProCode(this);\"  style=\"width:80px;\" class=\"Extra_textbox\" name=\"pro_code_hotel_" + promotion.PromotionId + "\" id=\"" + promotion.PromotionId + "\" />";
                        //else 
                        //    hotelCode = "<br/><strong>" +promotion.ProCode + "</strong>";

                        if (count % 2 == 0)
                            bgcolor = "bgcolor=\"#f2f2f2\"";

                        result.Append("<tr " + bgcolor + " id=\"row_promotion_" + promotion.PromotionId + "\"  align=\"center\">");
                        result.Append("<td>PE" + promotion.PromotionId + hotelCode + "</td>");
                        result.Append("<td>" + promotion.Datebookingstart.ToString("dd-MMM-yyyy") + " <br/>" + promotion.DatebookingEnd.ToString("dd-MMM-yyyy") + "</td>");

                        result.Append("<td>" + cProDayUse.DateUseStart.ToString("dd-MMM-yyyy") + "<br/>" + cProDayUse.DateUseEnd.ToString("dd-MMM-yyyy") + "</td>");

                        result.Append("<td>" + cProgroupItem.ProGroupTitle + "</td>");
                        result.Append("<td  align=\"left\" > " + promotion.ProTitle + "</td>");
                        result.Append("<td ><a href=\"\" onclick=\"return false;\" class=\"tooltip\">Detail");
                        result.Append("<span class=\"tooltip_content\">");
                        result.Append(GetRoomAndconditionSelect(promotion.PromotionId));
                        result.Append("");
                        result.Append("");
                        result.Append("");
                        result.Append("</span>");
                        result.Append("</a></td>");
                        result.Append("<td>" + promotion.DateSubmit.ToString("dd-MMM-yyyy") + "</td>");

                        
                        if (promotion.Status)
                        {
                            result.Append("<td><img src=\"../../images_extra/edit.png\" class=\"image_button\" onclick=\"managePromotion('" + promotion.PromotionId + "','" + cProgroupItem.ProGroup + "');\"/><img alt=\"click to inactivate\" title=\"click to inactivate\" src=\"../../images_extra/bin.png\" class=\"image_button\" onclick=\"DarkmanPopUpComfirm(400,'Are you sure to inactivate this promotion?' ,'UpdateStatusPrmotion(" + promotion.PromotionId + ")');return false;\" /></td>");
                        }
                        else
                        {
                            result.Append("<td><img src=\"../../images_extra/edit.png\" class=\"image_button\" onclick=\"managePromotion('" + promotion.PromotionId + "','" + cProgroupItem.ProGroup + "');\"/><img alt=\"click to activate\" title=\"click to activate\" src=\"../../images_extra/active.png\" class=\"image_button\" onclick=\"DarkmanPopUpComfirm(400,'Are you sure to activate this promotion?' ,'UpdateStatusPrmotionAndCheck(" + promotion.PromotionId + ")');return false;\" /></td>");
                        }
                        result.Append("</tr>");

                        count = count + 1;
                    }
                    result.Append("</table>");
                }
                else
                {
                    result.Append("<div  class=\"box_empty\">");
                    result.Append("");
                    result.Append("<p><label>Sorry!</label> There are no promotions to display.</p>");
                    result.Append("");
                    result.Append("</div>");
                }

                result.Append("");
                result.Append("");
                result.Append("");
                result.Append("");
            }
            catch (Exception ex)
            {
                Response.Write("error: " + ex.Message);
                Response.End();
            }

            return result.ToString();
        }

        public string GetRoomAndconditionSelect(int intPromotionId)
        {
            StringBuilder result = new StringBuilder();

            PromotionConditionActive cConditionPromotionActive = new PromotionConditionActive();
            IList<object> iListConditionPro = cConditionPromotionActive.getActiveConditionPromotion(intPromotionId, this.CurrentProductActiveExtra,
                this.CurrentSupplierId, 1);


            if (iListConditionPro.Count > 0)
            {
                var optionIdList = iListConditionPro.Select(s => s.GetType().GetProperty("OptionId").GetValue(s, null)).Distinct();

                result.Append("<table>");

                string optiontitle = string.Empty;
                foreach (int optionId in optionIdList)
                {
                    cConditionPromotionActive = (PromotionConditionActive)iListConditionPro
                        .FirstOrDefault(o => (int)o.GetType().GetProperty("OptionId")
                        .GetValue(o, null) == optionId);

                    result.Append("<tr>");
                    result.Append("<td>");
                    result.Append("<label style=\"font-weight:bold;\">" + cConditionPromotionActive.OptionTitle + "</label>");
                    result.Append("<table>");
                    var conditionList = iListConditionPro.Where(o => (int)o.GetType().GetProperty("OptionId").GetValue(o, null) == cConditionPromotionActive.OptionId);
                    foreach (var condition in conditionList)
                    {
                        result.Append("<tr><td><img src=\"/images/ico-square-small.png\" />&nbsp;");
                        cConditionPromotionActive = (PromotionConditionActive)condition;
                        result.Append(cConditionPromotionActive.ConditionTitle + Hotels2String.AppendConditionDetailExtraNet(cConditionPromotionActive.NumAdult, cConditionPromotionActive.NumABF));
                        result.Append("");
                        result.Append("</td></tr>");
                    }

                    result.Append("</table>");
                    result.Append("</td>");
                    result.Append("</tr>");
                }
                result.Append("</table>");
            }
            



            return result.ToString();



        }
    }
}