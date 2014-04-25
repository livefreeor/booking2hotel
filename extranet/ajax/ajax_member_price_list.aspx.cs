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
    public partial class admin_ajax_member_price_list : Hotels2BasePageExtra_Ajax
    {
        
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                //try
                //{
                    
                //}
                //catch (Exception ex)
                //{
                //    Response.Write(ex.Message + "--" + ex.StackTrace);
                //    Response.End();
                //}

                Response.Write(MemberPriceList());
                Response.End();

                
                
                
            }
        }

        

        public string MemberPriceList()
        {
            StringBuilder result = new StringBuilder();

            MemberPrice cMemberPrice = new MemberPrice();
          IList<object> ListMember =   cMemberPrice.GetMemberPriceList(this.CurrentProductActiveExtra);

          int intMemberCount = ListMember.Count;
            
            if (intMemberCount > 0)
            {

                result.Append("<div>");
                result.Append("<table class=\"darkman_tbls\" >");
                result.Append("<tr class=\"darkman_th\">");
                result.Append("<th style=\"width:10%\">No.</th>");
                result.Append("<th style=\"width:20%\">Date Start</th>");
                result.Append("<th style=\"width:20%\">Date End</th>");
                result.Append("<th style=\"width:20%\">Amount (%)</th>");
                result.Append("<th style=\"width:10%\">Condition</th>");
                result.Append("<th style=\"width:20%\">Edit</th>");
                result.Append("</tr>");

                int count = 1;
                string strEvent = "";
                
                
                //Response.Write(Isactive + "--" + Status + "---" + intProduct + "---" + intPageNum);
                //Response.End();
                string AppendQueryString = string.Empty;
                if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qSupplierId))
                    AppendQueryString = "&pid=" + this.qProductId + "&supid=" + this.qSupplierId;


                Option cOption = new Option();
                ProductConditionExtra cConditionExtra = new ProductConditionExtra();
                IList<object> iListOption = cOption.GetProductOptionByProductId_RoomOnlyExtranet(this.CurrentProductActiveExtra, this.CurrentSupplierId);

                foreach (MemberPrice member in ListMember)
                {

                    if ((count % 2) == 0)
                    strEvent = "darkman_row_event";
                else
                    strEvent = "darkman_row";

                    result.Append("<tr class=\"" + strEvent + " rowstyle\">");
                    result.Append("<td colspan=\"6\">");
                    result.Append("<div id=\"div_tmp_" + member.DiscountId + "\" class=\"tmp_block\" >dasdasdasd</div>");
                    result.Append("<div id=\"discount_main_block_list_" + member.DiscountId + "\"  class=\"discount_main_block_list\">");
                    result.Append("<table cellpadding=\"0\" class=\"main_edit_table\" width=\"100%\" cellspacing=\"0\" id=\"main_edit_table_"+member.DiscountId+"\">");
                    result.Append("<tr>");
                    result.Append("<td style=\"width:10%\">" + count + "</td>");

                    result.Append("<td class=\"edit_mode\" style=\"width:20%\">");
                    result.Append("<input style=\"display:none;\"checked=\"checked\" type=\"checkbox\" value=\""+member.DiscountId+"\"  name=\"chk_discount_id\"/>");
                    result.Append("<span id=\"span_"+member.DiscountId+"\">" + member.DateStart.ToString("dd-MMM-yyyy")+ "</span>");
                    result.Append("<input type=\"text\" style=\"display:none;\" class=\"Extra_textbox\" value=\"" + member.DateStart.ToString("yyyy-MM-dd") + "\"  name=\"date_start_" + member.DiscountId + "\" id=\"date_start_" + member.DiscountId + "\" />");
                    result.Append("</td>");



                    result.Append("<td class=\"edit_mode\" style=\"width:20%\">");
                    
                    result.Append("<span id=\"span_" + member.DiscountId + "\">" + member.DateEnd.ToString("dd-MMM-yyyy") + "</span>");
                    result.Append("<input type=\"text\" style=\"display:none;\" class=\"Extra_textbox\" value=\"" + member.DateEnd.ToString("yyyy-MM-dd") + "\"  name=\"date_end_" + member.DiscountId + "\" id=\"date_end_" + member.DiscountId + "\" />");
                    result.Append("</td>");



                    result.Append("<td class=\"edit_mode\" style=\"width:20%\">");

                    result.Append("<span id=\"span_" + member.DiscountId + "\">" + member.DiscountAmount.ToString("0") + "</span>");
                    result.Append("<input type=\"text\" class=\"Extra_textbox_yellow\"  style=\"display:none;width:70px;\"  value=\"" + member.DiscountAmount.ToString("#.##") + "\"  name=\"discount_amount_" + member.DiscountId + "\" />");
                    result.Append("</td>");

                    result.Append("<td style=\"width:10%\" align=\"center\"><a href=\"" + member.DiscountId + "\" class=\"link_condition_down\"  id=\"link_condition_" + member.DiscountId + "\"></a></td>");

                    result.Append("<td style=\"width:20%\">");
                    result.Append("<a href=\""+member.DiscountId+"\" id=\"link_remove_" + member.DiscountId + "\"><img src=\"/images_extra/bin.png\"></a>");
                    result.Append("<a href=\""+member.DiscountId+"\" id=\"link_edit_" + member.DiscountId + "\"><img src=\"/images_extra/edit.png\"></a>");
                    result.Append("</td>");
                    result.Append("</tr>");
                    result.Append("</table>");

                    result.Append("<div id=\"condition_pan_"+member.DiscountId+"\" class=\"condition_pan\">");
                    result.Append("");
                    result.Append("");

                    result.Append("<table width=\"100%\">");

                    int coditionCount = 0;
                    foreach (Option option in iListOption)
                    {
                        List<object> ConditionExtraList = cConditionExtra.getConditionListByOptionId(option.OptionID, 1);

                        coditionCount = ConditionExtraList.Count();

                        result.Append("<tr>");
                        result.Append("<td>");
                        if (coditionCount > 0)
                        {
                            result.Append("<p class=\"room_title\"><input style=\"display:none;\" type=\"checkbox\" title=\"" + option.Title + "\" value=\"" + option.OptionID + "_"+member.DiscountId+"\" id=\"chk_room_" + option.OptionID + "_"+member.DiscountId+"\" name=\"checkbox_room_check\" />");
                            result.Append("<label >" + option.Title + "</label></p>");
                           
                        }
                        else
                        {
                            result.Append("<p class=\"room_title\"><input style=\"display:none;\" type=\"checkbox\" title=\"" + option.Title + "\" value=\"" + option.OptionID + "_"+member.DiscountId+"\" id=\"chk_room_" + option.OptionID + "_"+member.DiscountId+"\" name=\"checkbox_room_check\" disabled=\"disabled\" />");
                            result.Append("<label style=\"color:#ccccc1;\">" + option.Title + "</label>");
                            result.Append("<label style=\"color:#f03d25;font-size:11px;\">***</label></p>");
                            
                        }
                        
                        result.Append("<div id=\"condition_list_" + option.OptionID + "_"+member.DiscountId + "\" class=\"condition_select_list\"  >");
                        result.Append("<table id=\"tbl_condition_list_"+option.OptionID+"_"+member.DiscountId + "\">");


                        //Pease create the conditions of this room type before you load your rate.
                        
                        if (ConditionExtraList.Count() > 0)
                        {
                            foreach (ProductConditionExtra conditionList in ConditionExtraList)
                            {
                                string Ischecked = "";
                                string image = "images/greenbt-gray.png";
                                string color = "style=\"color:#ccccc1;";
                                foreach (int conditionSel in member.ConditionSel)
                                {
                                    if (conditionSel == conditionList.ConditionId)
                                    {
                                        Ischecked = "checked=\"checked\"";
                                        image = "images/greenbt.png";
                                        color = "style=\"color:#000000;";
                                    }
                                    
                                }

                                result.Append("<tr><td>");
                                result.Append("<img class=\"dot_list\" src=\"/" + image + "\"/>&nbsp;<input style=\"display:none;\" type=\"checkbox\" " + Ischecked + " value=\"" + conditionList.ConditionId + "\"  title=\"" + conditionList.Title + "\" id=\"chk_condition_" + conditionList.ConditionId + "\" name=\"checkbox_condition_check_"+member.DiscountId+"\" />");
                                result.Append("<label " + color + " id=\"checkCon_" + conditionList.ConditionId + "\">" + conditionList.Title + Hotels2String.AppendConditionDetailExtraNet(conditionList.NumAult, conditionList.Breakfast) + "</label>");
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
                    result.Append("<div id=\"btn_price_edit_" + member.DiscountId+ "\" class=\"btn_price_edit\"><input type=\"button\" id=\"SaveCus_edit\" value=\"Save\" style=\"display:none;\" onclick=\"SaveEdit("+member.DiscountId+");\" class=\"Extra_Button_small_green btn_edit_discount\"  />&nbsp;");
                    result.Append("<input type=\"button\"  style=\"display:none;\" id=\"" + member.DiscountId + "\"   value=\"Cancel\"  class=\"Extra_Button_small_white cancelEdit\"  />");
                    result.Append("<div class=\"link_edit_hide\"><a href=\"" + member.DiscountId + "\"  class=\"condition_pan_edit\" id=\"condition_pan_edit_"+member.DiscountId+"\">Edit</a>");
                    result.Append("&nbsp;|&nbsp;<a href=\"" + member.DiscountId + "\"  class=\"condition_pan_hide\" id=\"condition_pan_hide_" + member.DiscountId + "\">Hide</a></div>");
                    result.Append("</div>");
                    result.Append("");
                    result.Append("");
                    result.Append("<div style=\"clear:both;\"></div>");
                   
                    
                    result.Append("</div>");

                    result.Append("</div>");
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
                result.Append("<p><label>Sorry!</label> There are no Member to display.</p>");
                result.Append("");
                result.Append("</div>");
            }
            return result.ToString();
        }
    }
}