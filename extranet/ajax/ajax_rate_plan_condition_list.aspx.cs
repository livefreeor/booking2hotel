using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;
using Hotels2thailand;
using System.Reflection;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_rate_plan_condition_list : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Response.Write(GetRateplanList());
                
                Response.End();
            }
        }


        public string GetRateplanList()
        {
            StringBuilder result = new StringBuilder();
            try
            {
                string strPlanIdValuesKey = string.Empty;
                Option cOption = new Option();
                ProductCondition_rate_plan cRatePlanList = new ProductCondition_rate_plan();
                IDictionary<byte, string> IdicCountry = cRatePlanList.getCountryRatePlan(this.CurrentProductActiveExtra, this.CurrentSupplierId);
                result.Append("<table  class=\"tbl_acknow\" cellspacing=\"2\">");
                result.Append("<tr class=\"header_field\"><th>No.</th><th>Country</th><th>Plan detail</th><th>Delete</th></tr>");
                ProductConditionExtra cConditionExtra = new ProductConditionExtra();
                IList<object> iListOption = cOption.GetProductOptionByProductId_RoomOnlyExtranet(this.CurrentProductActiveExtra, this.CurrentSupplierId);
                int Count = 1;
                ProductCondition_rate_plan_cat cRatePlanCat = new ProductCondition_rate_plan_cat();
                List<object> listRateplanCat = cRatePlanCat.GetRatePlanListAll();
                
                foreach (KeyValuePair<byte, string> country in IdicCountry)
                {
                    result.Append("<tr bgcolor=\"#ffffff\" align=\"center\" id=\"country_count_" + country.Key + "\">");
                    result.Append("<td >" + Count + "</td>");
                    result.Append("<td>" + country.Value + "</td>");
                    result.Append("<td><a href=\"\" onclick=\"return false;\" class=\"tooltip\" >Plan detail");
                    result.Append("<span class=\"tooltip_content\" >");
                    IList<object> iListRatePlan = cRatePlanList.getRatePlanList(this.CurrentProductActiveExtra, this.CurrentSupplierId, country.Key, 1);

                    

                    result.Append("<div id=\"condition_box_result\" >");
                    result.Append("<table width=\"100%\">");

                    int coditionCount = 0;
                   
                    foreach (Option option in iListOption)
                    {
                        List<object> ConditionExtraList = cConditionExtra.getConditionListByOptionId(option.OptionID, 1);

                        coditionCount = ConditionExtraList.Count();
                        int countItem = 0;
                        foreach (ProductConditionExtra conditionList in ConditionExtraList)
                        {
                            foreach (ProductCondition_rate_plan ratePlan in iListRatePlan)
                            {
                                if (ratePlan.ConditionId == conditionList.ConditionId)
                                {
                                    countItem = countItem + 1;
                                    break;
                                }
                            }
                            //if (iListRatePlan.Where(r => (int)r.GetType().GetProperty("ConditionId").GetValue(r, null) == conditionList.ConditionId).Count() > 0)
                            //{
                                
                            //}
                        }
                        result.Append("<tr>");
                        result.Append("<td>");
                        if (coditionCount > 0 && countItem > 0)
                        {
                            result.Append("<p class=\"room_title\"><input type=\"checkbox\" title=\"" + option.Title + "\"  style=\"display:none;\" value=\"" + option.OptionID + "\" id=\"chk_room_" + option.OptionID + "\" name=\"checkbox_room_check\" />");
                            result.Append("<label style=\"font-weight:bold;\" >" + option.Title + "</label></p>");
                            result.Append("<div id=\"condition_list" + option.OptionID + "\" class=\"condition_select_list\"  >");
                        }
                       
                        if (ConditionExtraList.Count() > 0)
                        {
                            result.Append("<table>");

                           
                            foreach (ProductConditionExtra conditionList in ConditionExtraList)
                            {
                                int count = 0;
                                foreach (ProductCondition_rate_plan ratePlan in iListRatePlan)
                                {
                                    if (ratePlan.ConditionId == conditionList.ConditionId)
                                    {
                                        count = count + 1;
                                    }
                                }


                                if (count > 0)
                                {

                                    ProductCondition_rate_plan plan = (ProductCondition_rate_plan)iListRatePlan.SingleOrDefault(r => (int)r.GetType().GetProperty("ConditionId").GetValue(r, null) == conditionList.ConditionId && (byte)r.GetType().GetProperty("CountryId").GetValue(r, null) == country.Key);


                                    result.Append("<tr><td style=\"font-size:11px;\"><img src=\"/images/ico-square-small.png\" />&nbsp;");

                                    result.Append("<label id=\"checkCon_" + conditionList.ConditionId + "\">" + conditionList.Title + "&nbsp;(For Adult " + conditionList.NumAult + ")</label>");
                                    result.Append("</td>");

                                    result.Append("<td>");
                                    result.Append("<label style=\"font-size:11px;\"><strong>Type:</strong>&nbsp; " + plan.RateplanCatTitle + "</label>");

                                    result.Append("</td>");
                                    result.Append("<td>");
                                    result.Append("<label style=\"font-size:11px;\"><strong>Amount :</strong>&nbsp;"+plan.Ratevalue.ToString("#.#")+"</label>");

                                    result.Append("</td>");
                                    result.Append("</tr>");

                                    strPlanIdValuesKey = strPlanIdValuesKey + plan.RatePlanId + ",";
                                }

                            }
                            result.Append("</table>");
                        }

                        //

                        
                        result.Append("</div>");
                        result.Append("<div></div>");
                        result.Append("</td>");
                        result.Append("</tr>");
                    }
                    result.Append("");
                    result.Append("");
                    result.Append("</table>");

                   

                    result.Append("</div>");



                    result.Append("");
                    result.Append("");
                    result.Append("</span></a>");
                    result.Append("</td>");
                    result.Append("<td><img src=\"/images_extra/bin.png\" style=\"cursor:pointer;\" onclick=\"deleterateplan('" + strPlanIdValuesKey.Hotels2RightCrl(1) + "','" + country.Key + "','" + country.Value + "');\" /></td>");
                    result.Append("</tr>");

                    Count = Count + 1;
                    strPlanIdValuesKey = string.Empty;
                }




                result.Append("</table>");
            }
            catch (Exception ex)
            {
                Response.Write("error: " + ex.Message);
                Response.End();
            }

            return result.ToString();
        }
    }
}