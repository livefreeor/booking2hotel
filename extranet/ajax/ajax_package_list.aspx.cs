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
    public partial class admin_ajax_package_list : Hotels2BasePageExtra_Ajax
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
                
                IList<object> iListOptionPackage = null;
                Option cOption = new Option();


                string isActive = "Active";

                if (string.IsNullOrEmpty(Expired))
                {
                    bool bolStatus = true;

                    if (strStatus == "0")
                    {
                        bolStatus = false;
                        isActive = "Inactive";
                    }
                    
                    
                    iListOptionPackage = cOption.GetProductOptionByProductId_PackageOnlyExtranet(this.CurrentProductActiveExtra, this.CurrentSupplierId, bolStatus);

                }
                else
                {
                    isActive = "Expired in 3 month";
                    iListOptionPackage = cOption.GetProductOptionByProductId_PackageOnlyExtranet_Expired(this.CurrentProductActiveExtra, this.CurrentSupplierId, true, byte.Parse(Expired));
                }


                result.Append("<p class=\"pro_status_title\">" + isActive + "</p>");
                
                if (iListOptionPackage.Count > 0)
                {

                    result.Append("<table width=\"100%\" class=\"tbl_acknow\" cellspacing=\"2\" >");
                    result.Append("<tr class=\"header_field\" align=\"center\">");
                    result.Append("<th style=\"width:3%;\">Code</th><th style=\"width:66%;\">Package</th><th style=\"width:10%;\">Booking date</th>");
                    result.Append("<th style=\"width:10%;\">Period stay</th><th style=\"width:8%;\">Datesubmit</th><th style=\"width:3%;\">Bin</th>");
                    result.Append("</tr>");

                    int count = 1;
                    ProductConditionExtra cConditionextra = new ProductConditionExtra();
                    ProductOptionContent cOptionCOntent = new ProductOptionContent();

                    

                    foreach (Option package in iListOptionPackage)
                    {

                        //cProDayUse = cProDayUse.getUseDatePromotionTop1byPromotionID(promotion.PromotionId);

                        //cProgroupItem = cProgroupItem.getProgroupItemById(promotion.ProgroupItemId, 1);
                       
                        string bgcolor = "bgcolor=\"#ffffff\"";


                        cOptionCOntent = cOptionCOntent.GetProductOptionContentById(package.OptionID, 1);

                        string PackageTitleLang = cOptionCOntent.Title;

                        if (count % 2 == 0)
                            bgcolor = "bgcolor=\"#f2f2f2\"";

                        result.Append("<tr " + bgcolor + " id=\"row_promotion_" + package.OptionID + "\"  align=\"center\">");
                        result.Append("<td>PE" + package.OptionID + "</td>");
                        result.Append("<td  align=\"left\" ><a href=\"\"  onclick=\"return false;\" class=\"tooltip\"> " + PackageTitleLang);

                        result.Append("<span class=\"tooltip_content\">");
                        //result.Append(GetRoomAndconditionSelect(package.PromotionId));
                        //result.Append("TEST");
                        
                        result.Append(cOptionCOntent.Detail);
                        result.Append("");
                        result.Append("</span>");
                        result.Append("</a>");
                        
                        result.Append("");
                        List<object> ListCondition = cConditionextra.getConditionListByOptionId(package.OptionID, 1);


                        if (ListCondition.Count() > 0)
                        {

                            result.Append("<table width=\"100%\" style=\"border:0px\" " + bgcolor + " cellspacing=\"0\" cellpadding=\"2\">");
                            
                            result.Append("");

                            int RowCount = 1;


                            foreach (ProductConditionExtra cCondition in ListCondition)
                            {
                                string endCode = cCondition.ConditionId.ToString() + Hotels2String.Hotels2RandomStringNuM(20);
                                endCode = endCode.Hotel2EncrytedData_SecretKey();

                                result.Append("<tr align=\"center\">");
                                //result.Append("<td style=\"border:0px\" ></td>");


                                result.Append("<td  style=\"border:0px;width:75%\" align=\"left\"><img src=\"/images/greenbt.png\" /><a href=\"\" style=\"font-size:11px;font-weight:normal;text-decoration:none;\" onclick=\"return false;\" class=\"tooltip\" >" + cCondition.Title + "<strong>" + Hotels2String.AppendConditionDetailExtraNet_Package(cCondition.IsAdult) + "</strong>");
                                result.Append("<span class=\"tooltip_content\" >");
                                ProductCondition_Cancel_Extra cConditionCancel = new ProductCondition_Cancel_Extra();
                                List<object> ListCancel = cConditionCancel.GetProductCancelExtraListByConditionID(cCondition.ConditionId);
                                result.Append("<table >");

                                int rowCount = 0;
                                foreach (ProductCondition_Cancel_Extra cancel in ListCancel)
                                {
                                    string rowcolor = "#edeff4";

                                    result.Append("<tr bgcolor=\"" + rowcolor + "\">");
                                    result.Append("<td style=\"border:0px;width:20%\">");
                                    result.Append("<label style=\"font-weight:bold;\">" + cancel.DateStart.ToString("dd-MMM-yyy") + "&nbsp; - &nbsp;" + cancel.DateEnd.ToString("dd-MMM-yyy") + "</label>");
                                    result.Append("");
                                    result.Append("<table style=\"font-size:11px;\">");
                                    ProductCondition_Cancel_Extra_Rule cCancelRule = new ProductCondition_Cancel_Extra_Rule();
                                    List<object> listRule = cCancelRule.getCencelRuleExtranetbyCancelId(cancel.CancelID);

                                    int count_rule = 1;
                                    foreach (ProductCondition_Cancel_Extra_Rule rule in listRule)
                                    {

                                        result.Append("<tr>");
                                        result.Append("<td style=\"border:0px;width:5%\"> <img src=\"/images/ico-square-small.png\" />&nbsp;");
                                        result.Append("" + Hotels2String.CancellationGenerateWording(true, rule.DayCancel, 0, 0, rule.ChargePercent, rule.Chargenight) + "");
                                        result.Append("</td>");
                                        result.Append("</tr>");

                                        count_rule = count_rule + 1;
                                    }
                                    result.Append("</table>");
                                    result.Append("</td>");

                                    rowCount = rowCount + 1;
                                }

                                result.Append("</tr>");
                                result.Append("</table>");
                                result.Append("</span></a>");
                               
                                //num guest
                                result.Append("&nbsp;&nbsp;");
                                //result.Append("<td style=\"border:0px\" align=\"left\">");
                                if (cCondition.IsAdult)
                                {
                                    if (cCondition.NumAult <= 3)
                                        result.Append("<img src=\"http://www.booking2hotels.com/images_extra/adult_small_" + cCondition.NumAult + ".png\" alt=\"For adult " + cCondition.NumAult + "\" title=\"For adult " + cCondition.NumAult + "\" />");
                                    else
                                        result.Append("<img src=\"http://www.booking2hotels.com/images_extra/adult_small_more.png\" alt=\"For adult " + cCondition.NumAult + "\" title=\"For adult " + cCondition.NumAult + "\" />");
                                }
                                else
                                {
                                    if (cCondition.NumChild <= 3)
                                        result.Append("<img src=\"http://www.booking2hotels.com/images_extra/adult_small_" + cCondition.NumChild + ".png\" alt=\"For child " + cCondition.NumChild + "\" title=\"For child " + cCondition.NumChild + "\" />");
                                    else
                                        result.Append("<img src=\"http://www.booking2hotels.com/images_extra/adult_small_more.png\" alt=\"For child " + cCondition.NumChild + "\" title=\"For child " + cCondition.NumChild + "\" />");
                                }
                                result.Append("</td>");

                                ProductPriceExtra_period cPricePeriod = new ProductPriceExtra_period();

                                cPricePeriod = cPricePeriod.getPricePackageByConditionId(cCondition.ConditionId);
                                decimal decPrice = 0;
                                if (cPricePeriod != null)
                                    decPrice = cPricePeriod.Price;

                                result.Append("<td style=\"border:0px\"><a href=\"\" style=\"font-size:11px;\" onclick=\"showhide('price_detail_" + cCondition.ConditionId + "');return false;\" >price detail");
                                
                                result.Append("");
                                result.Append("</a></td>");
                                    


                                
                               // result.Append("<td><img src=\"../../images_extra/edit_small.png\" style=\"cursor:pointer;\" onclick=\"EditCondition('" + endCode + "');\" /></td>");
                                    result.Append("<td style=\"border:0px\"><img src=\"../../images_extra/bin_small.png\" style=\"cursor:pointer;\" onclick=\"DarkmanPopUpComfirm(400,'Are you sure you want to delete? OK, Cancel' ,'delCondition(" + cCondition.ConditionId + ")');return false;\" /></td>");
                                result.Append("");
                                result.Append("</tr>");

                                RowCount = RowCount + 1;
                                result.Append("<tr>");
                                result.Append("<td colspan=\"3\">");
                                result.Append("<div id=\"price_detail_" + cCondition.ConditionId + "\" style=\"display:none;\">");

                                result.Append(GetPeriodPrice(cCondition.ConditionId));
                                
                                result.Append("</div>");
                                result.Append("</tr>");
                            }
                            result.Append("</table>");
                            result.Append("");
                            result.Append("");
                            result.Append("");
                        }
                        else
                        {
                            result.Append("<div class=\"box_empty\">");
                            result.Append("");
                            result.Append("<p><label>Sorry!</label> No Condition for this room type</p>");
                            result.Append("");
                            result.Append("</div>");
                        }
                        result.Append("");
                        result.Append("");
                        result.Append("");
                        result.Append("</td>");
                        result.Append("<td>" + ((DateTime)package.BookintDateStart).ToString("dd-MMM-yyyy") + " <br/>" + ((DateTime)package.BookingDAteEnd).ToString("dd-MMM-yyyy") + "</td>");

                        result.Append("<td>" + ((DateTime)package.StayDateStart).ToString("dd-MMM-yyyy") + "<br/>" + ((DateTime)package.StayDateEnd).ToString("dd-MMM-yyyy") + "</td>");

                        
                        result.Append("<td>" + package.DateSubmit.ToString("dd-MMM-yyyy") + "</td>");

                        result.Append("<td align=\"center\">");
                        result.Append("<img  src=\"../../images_extra/edit.png\" class=\"image_button\" onclick=\"managePackage('" + package.OptionID + "');\"/>");
                        if (package.Status)
                        {
                            
                             result.Append("<img alt=\"click to inactivate\" title=\"click to inactivate\" src=\"../../images_extra/bin.png\" class=\"image_button_normal\" onclick=\"DarkmanPopUpComfirm(400,'Are you sure to inactivate this Package?' ,'UpdateStatusPackage(" + package.OptionID + ")');return false;\" />");
                        }
                        else
                        {
                            result.Append("<img alt=\"click to activate\" title=\"click to activate\" src=\"../../images_extra/active.png\" class=\"image_button_normal\" onclick=\"DarkmanPopUpComfirm(400,'Are you sure to activate this Package?' ,'UpdateStatusPackageAndCheck(" + package.OptionID + ")');return false;\" />");
                        } 
                        result.Append("</td>");
                        result.Append("</tr>");

                        count = count + 1;
                    }
                    result.Append("</table>");
                }
                else
                {
                    result.Append("<div  class=\"box_empty\">");
                    result.Append("");
                    result.Append("<p><label>Sorry!</label> There are no package to display.</p>");
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
                Response.Write("error: " + ex.Message + "<br/>" + ex.StackTrace);
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

        public string GetPeriodPrice(int intConditionId)
        {

            string strListItem = "";

            //try
            //{
            
            ProductPriceExtra_period cProductPricePeriod = new ProductPriceExtra_period();
            ProductsupplementExtranet cDateSupplement = new ProductsupplementExtranet();
            ProductPriceExtra_period_longweek_end cPriceLongweekend = new ProductPriceExtra_period_longweek_end();

            strListItem = strListItem + "<div id=\"rate_load_head_ex\" >";
            strListItem = strListItem + "<table width=\"100%\" cellspacing=\"0\" cellspacing=\"0\" >";
            strListItem = strListItem + "<tr bgcolor=\"#96b4f3\" align=\"center\"><td width=\"15%\" style=\"font-size:9px;\" >from</td><td width=\"15%\" style=\"font-size:9px;\">to</td>";
            strListItem = strListItem + "<td width=\"10%\" style=\"font-size:9px;\">amount</td>";
            strListItem = strListItem + "<td width=\"10%\" style=\"font-size:9px;\">surcharge</td><td width=\"25%\" style=\"font-size:9px;\">day charge</td><td style=\"font-size:9px;\" width=\"10%\">H.C.</td>";
            strListItem = strListItem + "</tr>";
            strListItem = strListItem + "</table>";
            strListItem = strListItem + "</div>";


            foreach (ProductPriceExtra_period priceperiod in cProductPricePeriod.getPricePackageListByConditionId(intConditionId))
            {
                int pricePeriodId = priceperiod.Price_Period_Id;
                string DateFrom = priceperiod.DateStart.ToString("dd-MMM-yyyy");
                string DateTo = priceperiod.DateEnd.ToString("dd-MMM-yyyy");

                string hd_DateFrom = priceperiod.DateStart.ToString("yyyy-MM-dd");
                string hd_Dateto = priceperiod.DateEnd.ToString("yyyy-MM-dd");


                string cColorSun = ""; string cColorMon = ""; string cColorTue = ""; string cColorWed = ""; string cColorThu = "";
                string cColorFri = ""; string cColorSat = "";
                string DaySurVal = "";
                if (priceperiod.IsSub_Sun) { cColorSun = "style=\"color:#a20c0c;font-weight:bold\""; DaySurVal = "0,"; }
                if (priceperiod.IsSub_Mon) { cColorMon = "style=\"color:#a20c0c;font-weight:bold\""; DaySurVal = DaySurVal + "1,"; }
                if (priceperiod.IsSub_Tue) { cColorTue = "style=\"color:#a20c0c;font-weight:bold\""; DaySurVal = DaySurVal + "2,"; }
                if (priceperiod.IsSub_Wed) { cColorWed = "style=\"color:#a20c0c;font-weight:bold\""; DaySurVal = DaySurVal + "3,"; }
                if (priceperiod.IsSub_Thu) { cColorThu = "style=\"color:#a20c0c;font-weight:bold\""; DaySurVal = DaySurVal + "4,"; }
                if (priceperiod.IsSub_Fri) { cColorFri = "style=\"color:#a20c0c;font-weight:bold\""; DaySurVal = DaySurVal + "5,"; }
                if (priceperiod.IsSub_Sat) { cColorSat = "style=\"color:#a20c0c;font-weight:bold\""; DaySurVal = DaySurVal + "6,"; }
                string amount = priceperiod.Price.ToString("0.##");
                string surAmount = priceperiod.Supplement.ToString("0.##");
                string holidaySur = "No";
                //string holidaySur = "No";

                IList<object> iListLongweekend = cPriceLongweekend.GetLongWeekEndList_ByConditionId(intConditionId, priceperiod.DateStart, priceperiod.DateEnd);



                string holidaySurchargeHdList = "";

                string strholidayDetail = "<table style=\"width:100%;font-size:11px;\" cellspacing=\"0\" cellspacing=\"0\">";

                foreach (ProductPriceExtra_period_longweek_end longweekend in iListLongweekend)
                {
                    holidaySurchargeHdList = holidaySurchargeHdList + longweekend.DateSupplement.ToString("yyyy-MM-dd") + ";" + longweekend.Supplement + "#";

                    strholidayDetail = strholidayDetail + "<tr>";
                    strholidayDetail = strholidayDetail + "<td>" + longweekend.DateTitle + "(" + longweekend.DateSupplement.ToString("dd-MMM-yyyy") + "): </td>";
                    strholidayDetail = strholidayDetail + "<td><strong>" + longweekend.Supplement.ToString("#.##") + "</strong> Baht of Surcharge</td>";

                    strholidayDetail = strholidayDetail + "</tr>";

                }
                strholidayDetail = strholidayDetail + "</table>";
                if (iListLongweekend.Count() > 0)
                {
                    holidaySur = "<a href=\"\" onclick=\"return false;\" style=\"font-weight:bold;color:#ba0c0c;font-size:11px;\"  class=\"tooltip\" >Yes";
                    holidaySur = holidaySur + "<span class=\"tooltip_content\">";
                    holidaySur = holidaySur + strholidayDetail;
                    holidaySur = holidaySur + "</span>";
                    holidaySur = holidaySur + "</a>";
                }

                strListItem = strListItem + "<div class=\"rate_result_list_ex\" id=\"rate_result_list_" + pricePeriodId + "\" >";
                strListItem = strListItem + "<input type=\"checkbox\" id=\"checked_rate_result_" + pricePeriodId + "\" style=\"display:none;\" value=\"" + pricePeriodId + "\" name=\"rate_result_checked\" checked=\"checked\" />";
                strListItem = strListItem + "<table width=\"100%\" cellspacing=\"0\" cellspacing=\"0\">";
                strListItem = strListItem + "<tr align=\"center\">";
                strListItem = strListItem + "<td width=\"15%\">" + DateFrom + "<input type=\"hidden\" name=\"hd_rate_date_form_" + pricePeriodId + "\" value=\"" + hd_DateFrom + "\" /></td>";
                strListItem = strListItem + "<td width=\"15%\">" + DateTo + "<input type=\"hidden\" name=\"hd_rate_date_To" + pricePeriodId + "\" value=\"" + hd_Dateto + "\" /></td>";
                strListItem = strListItem + "<td width=\"10%\">" + amount + "<input type=\"hidden\" name=\"hd_amount" + pricePeriodId + "\" value=\"" + amount + "\" /></td>";

                strListItem = strListItem + "<td width=\"10%\">" + surAmount + "<input type=\"hidden\" name=\"hd_surAmount" + pricePeriodId + "\" value=\"" + surAmount + "\" /></td>";
                strListItem = strListItem + "<td width=\"25%\" ><div class=\"day_of_week_show_ex\" id=\"day_of_week_show_" + pricePeriodId + "\"><" + cColorSun + "p>S</p><p " + cColorMon + ">M</p><p " + cColorTue + ">T</p><p " + cColorWed + ">W</p><p " + cColorThu + ">T</p><p " + cColorFri + ">F</p><p " + cColorSat + ">S</p></div><div style=\"clear:both;\"></div><input type=\"hidden\" name=\"hd_day_checked_sur" + pricePeriodId + "\" value=\"" + DaySurVal + "\" /></td>";
                strListItem = strListItem + "<td width=\"10%\">" + holidaySur + "<input type=\"hidden\" name=\"hd_holiday_Sur" + pricePeriodId + "\" value=\"" + holidaySurchargeHdList + "\" /></td>";

                strListItem = strListItem + "</tr>";
                strListItem = strListItem + "</table>";
                strListItem = strListItem + "</div>";
            }
            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex.Message + "---" + ex.StackTrace);
            //    Response.End();
            //}



            return strListItem;
        }
    }
}