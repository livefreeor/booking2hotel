using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class extranet_promotion_manage : Hotels2BasePageExtra
    {
        public string qProGroup 
        {
            get { return Request.QueryString["pg"]; }
        }

        public string qPro
        {
            get { return Request.QueryString["pro"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {
                lnLast.NavigateUrl = lnLast.NavigateUrl + AppendCurrentQueryString();
                lnEarly.NavigateUrl = lnEarly.NavigateUrl + AppendCurrentQueryString();
                lnFree.NavigateUrl = lnFree.NavigateUrl + AppendCurrentQueryString();
                lnMini.NavigateUrl = lnMini.NavigateUrl + AppendCurrentQueryString();
                lnDis.NavigateUrl = lnDis.NavigateUrl + AppendCurrentQueryString();

                if (!string.IsNullOrEmpty(this.qProGroup) && string.IsNullOrEmpty(this.qPro))
                {
                    lrtProSelect.Text = GetPromotionList();
                    roomSelect.Text = GetRoomAndConditionSelect("");
                    getPromotionCountry();
                }

                if (!string.IsNullOrEmpty(this.qPro) && !string.IsNullOrEmpty(this.qProGroup))
                {
                    PromotionGroupItem cProgroupItem = new PromotionGroupItem();
                    cProgroupItem = cProgroupItem.getProgroupItemByPromotionId(int.Parse(this.qPro), 1);

                    PromotionEditModeDataBind(cProgroupItem.Title);
                    lrtProSelect.Text = GetPromotionList();
                    roomSelect.Text = GetRoomAndConditionSelect(this.qPro);
                   
                }


                //if (string.IsNullOrEmpty(this.qProductId) && string.IsNullOrEmpty(this.qSupplierId))
                //{
                //    lnLast.Visible = false;
                //}
            }
           
        }

        public void getPromotionCountry()
        {
            Country cCountry = new Country();
            listCountry.DataSource = cCountry.GetCountryExtranetAll();
            listCountry.DataTextField = "Title";
            listCountry.DataValueField = "CountryID";
            listCountry.DataBind();
        }

        public void getPromotionCountryEditMode(int intPromotionId)
        {
            Country cCountry = new Country();
            listCountry.DataSource = cCountry.GetCountryPromotionExtranet(intPromotionId);
            listCountry.DataTextField = "Title";
            listCountry.DataValueField = "CountryID";
            listCountry.DataBind();

            string result = string.Empty;

            PromotionCountry cPromotionCountry = new PromotionCountry();
            IList<object> iListPromotionCountry = cPromotionCountry.GetPromotionCountry(intPromotionId);
            country_selected.DataSource = iListPromotionCountry;
            country_selected.DataTextField = "Title";
            country_selected.DataValueField = "CountryID";
            country_selected.DataBind();

            foreach (PromotionCountry item in iListPromotionCountry)
            {
                result = result + item.CountryID + ",";
            }

            
            hd_country_selected.Value = result.Hotels2RightCrl(1);

        }

        public string GetPromotionList()
        {
            StringBuilder result = new StringBuilder();
            PromotionGroupItem cProItem = new PromotionGroupItem();
            IList<object> iListProGroup =  cProItem.getProItemByProGroupAndLangId(byte.Parse(this.qProGroup),1);
            result.Append("<div class=\"promotion_group_item_selected_list\">");
            result.Append("<table>");
            string Checked = "";
            int count = 0;
            foreach (PromotionGroupItem item in iListProGroup)
            {
                if (count == 0)
                {
                    Checked = "checked=\"checked\"";
                }
                else
                {
                    Checked = "";
                }
                result.Append("<tr>");
                result.Append("<td><input type=\"radio\" value=\"" + item.ProItem + "\"  name=\"pro_item_select\" " + Checked + " /><td>");
                result.Append("<td >" + item.Title+ "</td>");
                result.Append("</tr>");

                count = count + 1;
            }
           
            result.Append("</table>");
           
            result.Append("</div>");
            result.Append("<div class=\"btn_goto_wiz\"><input type=\"button\" value=\"Go\" style=\"width:150px;margin:0 auto;\" class=\"Extra_Button_small_green\" onclick=\"GoTowizard();return false;\" /></div>");
           return  result.ToString();
        }

        public void PromotionEditModeDataBind(string ProGroupItemTitle)
        {
            int intPromotionId = int.Parse(this.qPro);

            PromotionExxtranet cProExtra = new PromotionExxtranet();
            cProExtra = cProExtra.getPromotionExtranetByPromotionId(intPromotionId);

            PromotionContentExtranet cProcontent = new PromotionContentExtranet();
            cProcontent = cProcontent.GetPromotionContentbyProIdANdLangId(intPromotionId, 1);

            PromotionDayuseExtranet cProuseDate = new PromotionDayuseExtranet();
            cProuseDate = cProuseDate.getUseDatePromotionTop1byPromotionID(intPromotionId);

            //PromotionGroupItem cProgroupItem = new PromotionGroupItem();
            //cProgroupItem = cProgroupItem.getProgroupItemById(cProExtra.ProgroupItemId,1);

            PromotionBenefitExtranet cProbenefit = new PromotionBenefitExtranet();
           cProbenefit =  cProbenefit.GetBenefitListByPromotionIdTOp1Extranet(intPromotionId);

            PromotioncancellExtranet cProcancel = new PromotioncancellExtranet();
            IList<object> iListCancel = cProcancel.getCencelRuleExtranetbyPromotionId(intPromotionId);

            //Response.Write(cProExtra.Datebookingstart.ToString("yyyy-MM-dd") + "---" + cProExtra.DatebookingEnd.ToString("yyyy-MM-dd"));
            //Response.End();
            //Step 1 


            booking_start.Value = cProExtra.Datebookingstart.ToString("yyyy-MM-dd");
            booking_End.Value = cProExtra.DatebookingEnd.ToString("yyyy-MM-dd");
            //LtrProitemTitleBooking.Text = ProGroupItemTitle;
            //Response.Write(booking_start.Text);
            //Response.End();
            //Step 2
            Stay_start.Value = cProuseDate.DateUseStart.ToString("yyyy-MM-dd");
            Stay_End.Value = cProuseDate.DateUseEnd.ToString("yyyy-MM-dd");
            //LtrProitemTitleStay.Text = ProGroupItemTitle;


            //Step3 Promotion Setting

            //ScriptManager.RegisterStartupScript(this, Page.GetType(), "1", "<script>DeleteHtmlStep('3');</script>", false);
            step3Edit.Text = GetStepPromotionSetting(cProExtra, cProbenefit);
            
            //Step 4 Benefit
            //Response.Write(cProcontent.Detail.Hotels2XMLReaderPromotionDetailExtranet().Count);
            //Response.End();
            ltrBenefit.Text = GetStepBenefit(cProcontent);
            
            //Step 5 Sensitive Time 
            //ScriptManager.RegisterStartupScript(this, Page.GetType(), "2", "<script>DeleteHtmlStep('5');</script>", false);
            step5Edit.Text = GetStepSensitiveTime(cProExtra, cProbenefit);


            //Step 7 Cancelltion
            ltrStep7.Text = GetStepCancelltion(iListCancel, cProExtra);

            //step8
            getPromotionCountryEditMode(intPromotionId);

            hd_promotion_group_item.Value = cProExtra.ProgroupItemId.ToString();


            ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>DefaultTitleBenefit('" + ProGroupItemTitle + "');</script>", false);
        }

       

        public string GetStepCancelltion(IList<object> iListCancel, PromotionExxtranet cProExtra)
        {
            //var random_cancel = makeid();

            string Iscancel = "";
            string IsnotCancel = "";

            if (cProExtra.Iscancelltion)
            {
                Iscancel = "class=\"checked\"";
            }
            else
            {
                IsnotCancel = "class=\"checked\"";
            }

            string ListItem ="";

            ListItem = ListItem + "<div class=\"step_body\" id=\"step_body_step7_edit\" >";
            ListItem = ListItem + "<p class=\"DefaultProtitle\"></p>";
            ListItem = ListItem + "<p><label>Do you want to use standard cancellation policy in this promotion ?</label></p>";
            ListItem = ListItem + "<table>";
            ListItem = ListItem + "<tr><td><input type=\"radio\" name=\"radiocancel\" value=\"0\"  title=\"Yes.\" onclick=\"closecancel();\" " + IsnotCancel + " /></td><td>Yes.</td></tr>";
            ListItem = ListItem + "<tr><td><input type=\"radio\" name=\"radiocancel\" value=\"1\"  title=\"No.\" onclick=\"opencancel();\" " + Iscancel + " /></td><td>No,I want to use new cancellation policy.</td></tr>";
            ListItem = ListItem + "</table>";

            if (cProExtra.Iscancelltion)
            {
                ListItem = ListItem + "<div id=\"cancelltionAdd_main\" >";


                //----------------

                ListItem = ListItem + "<div id=\"cancelltionAdd\">";

                ListItem = ListItem + "<table cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center; margin:0px 0px 0px 0px;\">";
                ListItem = ListItem + "<tr style=\"background-color:#edeff4;color:#333333;font-weight:bold;height:15px;line-height:15px;\"><td align=\"center\" width=\"40%\" >No.of Day (s) Cancel</td>";
                ListItem = ListItem + "<td align=\"center\" width=\"30%\">No. of Night (s) Charge</td><td align=\"center\" width=\"30%\" colspan=\"2\" >Percentage Charge </td></tr>";
                ListItem = ListItem + "</table>";

                int count = 0;
                foreach (PromotioncancellExtranet cancelid in iListCancel)
                {
                    ListItem = ListItem + "<div class=\"cancel_list_item\" id=\"cancel_list_item_" + cancelid.CancelruleId + "\" >";


                    ListItem = ListItem + "<input type=\"checkbox\" checked=\"checked\" value=\"" + cancelid.CancelruleId + "\" name=\"cencel_list_Checked_toUpdate\" style=\"display:none;\" />";

                    ListItem = ListItem + "<table cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center; margin:0px 0px 0px 0px;\">";
                    ListItem = ListItem + "<tr style=\"background-color:#ffffff;\" >";
                    ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 0px 0px; padding:2px 0px 0px 0px;\" width=\"40%\">";
                    ListItem = ListItem + "<select id=\"drop_daycancel_" + cancelid.CancelruleId + "\" class=\"Extra_Drop\" name=\"drop_daycancel_" + cancelid.CancelruleId + "\" >";

                    for (int i = 0; i <= 60; i++)
                    {
                        if (i == 0)
                        {
                            if (i == cancelid.Daycancel)
                                ListItem = ListItem + "<option value=\"" + i + "\" selected=\"selected\" >no-show</option>";
                            else
                                ListItem = ListItem + "<option value=\"" + i + "\">no-show</option>";
                        }
                        else
                        {
                            if (i == cancelid.Daycancel)
                                ListItem = ListItem + "<option value=\"" + i + "\"  selected=\"selected\" >" + i + "</option>";
                            else
                                ListItem = ListItem + "<option value=\"" + i + "\">" + i + "</option>";
                        }
                    }

                    ListItem = ListItem + "</select>";
                    ListItem = ListItem + "</td>";
                    ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 0px 0px; padding:2px 0px 0px 0px;\" width=\"30%\">";
                    ListItem = ListItem + "<input type=\"text\" maxlength=\"2\" id=\"txt_day_charge_" + cancelid.CancelruleId + "\" value=\"" + cancelid.ChargeNight + "\" style=\"width:20px;\" name=\"txt_day_charge_" + cancelid.CancelruleId + "\" class=\"Extra_textbox\"  />";
                    ListItem = ListItem + "</td>";
                    ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 0px 0px; padding:2px 0px 0px 0px;\" width=\"25%\">";
                    ListItem = ListItem + "<input type=\"text\" maxlength=\"3\" id=\"txt_per_charge_" + cancelid.CancelruleId + "\" value=\"" + cancelid.ChargePer + "\" style=\"width:22px;\" name=\"txt_per_charge_" + cancelid.CancelruleId + "\" class=\"Extra_textbox\" />";
                    ListItem = ListItem + "</td>";
                    if (count > 0)
                    {
                        ListItem = ListItem + "<td width=\"5%\"><img src=\"../../images_extra/del.png\" style=\"cursor:pointer;\" onclick=\"remove('cancel_list_item_" + cancelid.CancelruleId + "');return false;\" /></td>";
                    }
                    else
                    {
                        ListItem = ListItem + "<td width=\"5%\"></td>";
                    }
                    

                    
                    ListItem = ListItem + "</tr>";
                    ListItem = ListItem + "</table>";
                    ListItem = ListItem + "</div>";

                    count = count + 1;
                }

                ListItem = ListItem + "</div>";
                ListItem = ListItem + "<div class=\"add_rule\">";
                ListItem = ListItem + "<a href=\"\" style=\" width:100%; text-align:center;\" onclick=\"addRule();return false;\" >add rules</a>";
                ListItem = ListItem + "</div>";

                ListItem = ListItem + "</div>";
                //------------------
            }
            else
            {
                ListItem = ListItem + "<div id=\"cancelltionAdd_main\"  style=\"display:none\">";
                ListItem = ListItem + "</div>";
            }

            

           
            ListItem = ListItem + "</div>";
            //-------
            return ListItem;
        }

        public string GetStepBenefit(PromotionContentExtranet cProcontent)
        {
            ArrayList arrBenefit = cProcontent.Detail.Hotels2XMLReaderPromotionDetailExtranet();

            int random = 1;
            string  itemInsert = "";
            foreach (string Benefit in arrBenefit)
            {
                //string random = Hotels2String.Hotels2RandomStringNuM(5);
                itemInsert = itemInsert + "<div class=\"benefit_list_item\" id=\"benefit_list_item_" + random + "\" >";
                itemInsert = itemInsert + "<table width=\"100%\"><tr>";
                itemInsert = itemInsert + "<td width=\"10px\"><img src=\"../../images/greenbt.png\" /><input style=\"display:none;\" checked=\"checked\"  type=\"checkbox\" name=\"benefitList\" value=\"" + random + "\" /></td>";
                itemInsert = itemInsert + "<td>" + Benefit + "<input type=\"hidden\" name=\"hd_benefit_" + random + "\" value=\"" + Benefit + "\" /></td>";

                itemInsert = itemInsert + "<td width=\"5%\"><a href=\"\" style=\"font-size:11px;font-weight:normal;\" onclick=\"remove('benefit_list_item_" + random + "');return false;\">remove</a></td>";
                itemInsert = itemInsert + "</tr>";
                itemInsert = itemInsert + "</table></div>";

                random = random + 1;

            }

            return itemInsert;
        }

        public string GetStepSensitiveTime(PromotionExxtranet cProExtra, PromotionBenefitExtranet cBenefit)
        {
            StringBuilder result = new StringBuilder();

            string Suncheck = "";
            string Moncheck = "";
            string Tuecheck = "";
            string Wedcheck = "";
            string Thucheck = "";
            string Fricheck = "";
            string Satcheck = "";


            string IsWeekday = "";
            string IsNotWeekDAy = "";
            string Ishow = "style=\"display:none\"";
            if (cProExtra.IsSun && cProExtra.IsMon && cProExtra.IsTue && cProExtra.IsWed && cProExtra.IsThu && cProExtra.IsFri && cProExtra.IsSat)
            {
                IsWeekday = "class=\"checked\"";
                
            }
            else
            {
                Ishow = "style=\"display:block\"";
                IsNotWeekDAy = "class=\"checked\"";

                if (cProExtra.IsSun) { Suncheck = "checked=\"checked\""; }
                if (cProExtra.IsMon) { Moncheck = "checked=\"checked\""; }
                if (cProExtra.IsTue) { Tuecheck = "checked=\"checked\""; }
                if (cProExtra.IsWed) { Wedcheck = "checked=\"checked\""; }
                if (cProExtra.IsThu) { Thucheck = "checked=\"checked\""; }
                if (cProExtra.IsFri) { Fricheck = "checked=\"checked\""; }
                if (cProExtra.IsSat) { Satcheck = "checked=\"checked\""; }
            }


            string Isholidy = "";
            string IsnotHoliday = "";

            if (cProExtra.IsHolidayCharge == 1)
            {
                Isholidy = "class=\"checked\"";
            }
            else
            {
                IsnotHoliday = "class=\"checked\"";
            }

            result.Append("<div class=\"step_body\" id=\"step_body_step5_edit\" >");
            result.Append("<p class=\"DefaultProtitle\"></p>");
            result.Append("<p><label>Is this promotion valid to everyday of the week?</label></p>");
            result.Append("<table>");
            result.Append("<tr><td><input type=\"radio\" name=\"radioweekDay\" value=\"0\"  onclick=\"closedayofweek();\" " + IsWeekday + " /></td><td>Yes, this promotion is valid to everyday of the week.</td></tr>");
            result.Append("<tr><td><input type=\"radio\"  name=\"radioweekDay\" value=\"1\" onclick=\"opendayofweek();\" " + IsNotWeekDAy + " /></td><td>No, please select valid day </td></tr>");
            result.Append("</table>");
            result.Append("<div id=\"dayofweek\" " + Ishow + " >");
            result.Append("<table><tr>");
            result.Append("<td><label>What day? </label></td>");
            result.Append("<td><input type=\"checkbox\" name=\"check_dayofWeek\" " + Suncheck + " value=\"0\" title=\"Sun\" />Sun</td>");
            result.Append("<td><input type=\"checkbox\" name=\"check_dayofWeek\" " + Moncheck + " value=\"1\" title=\"Mon\" />Mon</td>");
            result.Append("<td><input type=\"checkbox\" name=\"check_dayofWeek\" " + Tuecheck + " value=\"2\" title=\"Tue\" />Tue</td>");
            result.Append("<td><input type=\"checkbox\" name=\"check_dayofWeek\" " + Wedcheck + " value=\"3\" title=\"Wed\" />Wed</td>");
            result.Append("<td><input type=\"checkbox\" name=\"check_dayofWeek\" " + Thucheck + " value=\"4\" title=\"Thu\" />Thu</td>");
            result.Append("<td><input type=\"checkbox\" name=\"check_dayofWeek\" " + Fricheck + " value=\"5\" title=\"Fri\" />Fri</td>");
            result.Append("<td><input type=\"checkbox\" name=\"check_dayofWeek\" " + Satcheck + " value=\"6\" title=\"Sat\" />Sat</td>");
            result.Append("</tr></table>");
            result.Append("</div>");
            result.Append("<div id=\"holiday_applicable\">");
             result.Append("<table>");
             result.Append("<tr>");
             result.Append("<td><label>Is public holiday applicable? </label></td>");
             result.Append("<td><input type=\"radio\" name=\"radioholiday\" value=\"0\"  title=\"Yes.\" " + Isholidy + " /> Yes</td>");
             result.Append("<td><input type=\"radio\" name=\"radioholiday\" value=\"1\" title=\"No.\" " + IsnotHoliday + " /> No</td>");
             result.Append("</tr>");
             result.Append("</table>");
             result.Append("</div>");

             result.Append("</div>");


            return result.ToString();
        }


        public string GetStepPromotionSetting(PromotionExxtranet cProExtra, PromotionBenefitExtranet cBenefit)
        {
            StringBuilder result = new StringBuilder();


            result.Append("<div class=\"step_body\">");
            result.Append("<p class=\"DefaultProtitle\"></p>");
            result.Append("<div id=\"pro_set_1\" class=\"proset_step3\">");
            result.Append("<table>");
            result.Append("<tr><td><label>Advance</label></td><td><select id=\"sel_advance_day\" class=\"Extra_Drop\" name=\"sel_advance_day\" >");
            for (int i = 1; i <= 90; i++)
            {
                
                if (i == cProExtra.DayAdVanceMin)
                    result.Append("<option value=\""+ i +"\" selected=\"selected\">"+i+"</option>");
                else
                    result.Append("<option value=\"" + i + "\">" + i + "</option>");
              
            }
            result.Append("</select></td><td><label>Day(s)</label></td></tr>");
            result.Append("");
            result.Append("</table>");
            result.Append("</div>");

            result.Append("<div id=\"pro_set_9\" class=\"proset_step3\">");
            result.Append("<table>");
            result.Append("<tr><td> <label>Within&nbsp;&nbsp;&nbsp;&nbsp;</label> </td><td><select id=\"sel_within_day\" style=\" width:50px;\" class=\"Extra_Drop\" name=\"sel_within_day\" >");
            for (int i = 1; i <= 30; i++)
            {

                if (i == cProExtra.DayLastminute && cProExtra.DayLastminute!= 999)
                    result.Append("<option value=\"" + i + "\" selected=\"selected\">" + i + "</option>");
                else
                    result.Append("<option value=\"" + i + "\">" + i + "</option>");

            }
            result.Append("</select></td><td><label>Day(s)</label></td></tr>");
            
            result.Append("</table>");
            result.Append("</div>");

            result.Append("<div id=\"pro_set_2\" class=\"proset_step3\">");
            result.Append("<table>");
            result.Append("<tr><td><label>Minimum</label></td><td><select id=\"sel_min_day\" class=\"Extra_Drop\" name=\"sel_min_day\">");
            for (int i = 1; i <= 10; i++)
            {
                if (i == cProExtra.DayMin)
                    result.Append("<option value=\"" + i + "\" selected=\"selected\">" + i + "</option>");
                else
                    result.Append("<option value=\"" + i + "\">" + i + "</option>");

            }
            result.Append("</select></td><td><label>Night(s)</label></td></tr>");
            result.Append("</table>");
            result.Append("</div>");

            result.Append("<div id=\"pro_set_3\" class=\"proset_step3\">");
            result.Append("<table>");
            result.Append("<tr><td><label>Discount</label></td><td><input type=\"text\" value=\"" + cBenefit.DiscountAmount.ToString("#,0") + "\" id=\"dis_percent\" class=\"Extra_textbox_yellow\" name=\"dis_percent\" /></td><td><label>(%)</label></td></tr>");
            result.Append("</table>");
            result.Append("</div>");

            result.Append("<div id=\"pro_set_4\" class=\"proset_step3\">");
            result.Append("<table>");
            result.Append("<tr><td><label>Discount</label></td><td><input type=\"text\" value=\"" + cBenefit.DiscountAmount.ToString("#,0") + "\" id=\"dis_baht\" class=\"Extra_textbox_yellow\" name=\"dis_baht\" /></td><td><label>Baht</label></td></tr>");
            result.Append("</table>");
            result.Append(" </div>");
            result.Append("<div id=\"pro_set_6\" class=\"proset_step3\">");
            result.Append("<table>");
            result.Append("<tr>");

            //bytDayMin = byteFreeNightStay;
            //bytStartDiscountnight = (byte)(byteFreeNightPay + 1);
            //numDiscountnight = (byte)(byteFreeNightStay - byteFreeNightPay);

            result.Append(" <td><label>Stay</label></td><td><input type=\"text\" value=\"" + cProExtra.DayMin + "\" id=\"free_night_stay\" class=\"Extra_textbox_yellow\" style=\"width:50px\" name=\"free_night_stay\" /></td><td><label>Night(s)</label></td>");
            result.Append("<td><label>Pay</label></td><td><input type=\"text\" value=\"" + (cBenefit.DaydiscountStart - 1) + "\" id=\"free_night_pay\" class=\"Extra_textbox_yellow\" style=\"width:50px\" name=\"free_night_pay\" /></td><td><label>Night(s)</label></td>");
            result.Append("</tr>");
            result.Append(" </table>");
            result.Append("</div>");

            result.Append("<div id=\"pro_set_7\" class=\"proset_step3\">");
            result.Append("<table>");
            result.Append("<tr><td><label>Plus</label></td><td><input type=\"text\" value=\"" + cProExtra.BreakfastCharge.ToString("#,0") + "\" id=\"com_abf\" class=\"Extra_textbox_yellow\" name=\"com_abf\" /></td><td><label>Baht on Free Nights</label></td></tr>");
            result.Append("</table>");
            result.Append("</div>");

            result.Append("<div id=\"pro_set_8\" class=\"proset_step3\">");
            result.Append("<table>");
            result.Append("<tr><td>Consecutive</td><td><select id=\"sel_consec_night\" class=\"Extra_Drop\" name=\"sel_consec_night\">");

            for (int i = 1; i <= 10; i++)
            {
                if (i == cProExtra.DayMin)
                    result.Append("<option value=\"" + i + "\" selected=\"selected\">" + i + "</option>");
                else
                    result.Append("<option value=\"" + i + "\">" + i + "</option>");

            }
            result.Append("</select></td><td>Night(s)</td></tr>");
            result.Append("</table>");
            result.Append("</div>");
            result.Append("<div id=\"pro_set_5\" class=\"proset_step3\">");
            result.Append(" <table>");
            result.Append("<tr><td>In case of long stay, booking is limited at </td><td><select id=\"limit_book\" class=\"Extra_Drop\" name=\"limit_book\">");
            result.Append("<option value=\"100\">Infinity</option>");

            for (int i = 1; i <= 10; i++)
            {
                if (i == cProExtra.MaxRepeatSet)
                    result.Append("<option value=\"" + i + "\" selected=\"selected\">" + i + "</option>");
                else
                    result.Append("<option value=\"" + i + "\">" + i + "</option>");

            }

            result.Append("</select></td><td>time (s) in one booking.</td></tr>");
            result.Append("</table>");
            result.Append("</div>");

            result.Append("</div>");

            return result.ToString();
        }

        public string GetRoomAndConditionSelect(string strPromotionId)
        {
            StringBuilder result = new StringBuilder();

            Option cOption = new Option();
            ProductConditionExtra cConditionExtra = new ProductConditionExtra();
           IList<object> iListOption = cOption.GetProductOptionByProductId_RoomOnlyExtranet(this.CurrentProductActiveExtra, this.CurrentSupplierId);
           int coditionCount = 0;
           result.Append("<p class=\"DefaultProtitle\"></p>");
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
                   result.Append("<div id=\"condition_list" + option.OptionID + "\" class=\"condition_select_list\"  >");
               }
               else
               {
                   result.Append("<p class=\"room_title\"><input type=\"checkbox\" title=\"" + option.Title + "\" value=\"" + option.OptionID + "\" id=\"chk_room_" + option.OptionID + "\" name=\"checkbox_room_check\" disabled=\"disabled\" />");
                   result.Append("<label style=\"color:#ccccc1;\">" + option.Title + "</label>");
                   result.Append("<label style=\"color:#f03d25;font-size:11px;\">***</label></p>");
                   result.Append("<div id=\"condition_list" + option.OptionID + "\" class=\"condition_select_list\"  >");
               }
               
               result.Append("<table>");

               
               //Pease create the conditions of this room type before you load your rate.

               if (ConditionExtraList.Count() > 0)
               {
                   foreach (ProductConditionExtra conditionList in ConditionExtraList)
                   {
                       if (!string.IsNullOrEmpty(strPromotionId))
                       {
                           PromotionConditionExtranet cProcondition = new PromotionConditionExtranet();
                           IList<object> iListCondition = cProcondition.getConditionByPromotionId(int.Parse(strPromotionId));
                           bool isCheck = false;
                           string strCheck = "";
                           foreach (PromotionConditionExtranet condition in iListCondition)
                           {

                               if (condition.ConditionId == conditionList.ConditionId)
                               {
                                   isCheck = true;
                                   break;
                               }

                           }
                           if (isCheck)
                               strCheck = "checked=\"checked\"";

                           result.Append("<tr><td>");
                           result.Append("<input type=\"checkbox\" " + strCheck + " value=\"" + conditionList.ConditionId + "\"  title=\"" + conditionList.Title + "\" id=\"chk_condition_" + conditionList.ConditionId + "\" name=\"checkbox_condition_check\" />");
                           result.Append("<label id=\"checkCon_" + conditionList.ConditionId + "\">" + conditionList.Title +  Hotels2String.AppendConditionDetailExtraNet(conditionList.NumAult, conditionList.Breakfast) + "</label>");
                           result.Append("<lable id=\"checkCon_Alert_" + conditionList.ConditionId + "\" style=\"display:none;color:red;font-size:11px;\">**</label>");
                           result.Append("</td></tr>");


                       }
                       else
                       {
                           result.Append("<tr><td>");
                           result.Append("<input type=\"checkbox\" value=\"" + conditionList.ConditionId + "\"  title=\"" + conditionList.Title + "\" id=\"chk_condition_" + conditionList.ConditionId + "\" name=\"checkbox_condition_check\" />");
                           result.Append("<label id=\"checkCon_" + conditionList.ConditionId + "\">" + conditionList.Title + Hotels2String.AppendConditionDetailExtraNet(conditionList.NumAult, conditionList.Breakfast) + "</label>");
                           result.Append("<lable id=\"checkCon_Alert_" + conditionList.ConditionId + "\" style=\"display:none;color:red;font-size:11px;\">**</label>");
                           
                           result.Append("</td></tr>");
                       }

                   }
               }

               //
               
               result.Append("</table>");
               result.Append("</div>");
               result.Append("<div class=\"line\"></div>");
               result.Append("</td>");
               result.Append("</tr>");
           }
           result.Append("");
           result.Append("");
           result.Append("</table>");
           if (coditionCount == 0)
           {
               result.Append("<p id=\"condition_alert_room\" style=\"margin:0px 0px 0px 0px ; padding:0px 0px 0px 0px;color:#f03d25;font-size:11px; \" >*** Pease create the conditions of this room type before you load your rate.</p>");
           }

           result.Append("<p id=\"condition_alert\"  style=\"margin:0px 0px 0px 0px ;padding:0px 0px 0px 0px;display:none;color:#f03d25;font-size:11px; \">** This condition can not apply with this promotion.<br/>Minimum night and period of stay has been added in this condition. Please recheck.</p>");
           return result.ToString();
            //dropRoom.DataTextField = "Title";
            //dropRoom.DataValueField = "OptionID";
        }
    }
}