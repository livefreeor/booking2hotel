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
    public partial class admin_ajax_package_cancellation : Hotels2BasePageExtra_Ajax
    {
        
        public string qOptionId
        {
            get { return Request.QueryString["oid"]; }
        }

        public string qConditionId
        {
            get { return Request.QueryString["con"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.Page.IsPostBack)
            {
                if (this.IsStaffEdit())
                {
                    if (!string.IsNullOrEmpty(this.qConditionId))
                    {
                        try
                        {

                            int intCOnditionId = int.Parse(this.qConditionId);

                            string ListItem = string.Empty;

                            ProductConditionExtra cCOndition = new ProductConditionExtra();
                            cCOndition = cCOndition.getConditionByConditionId(intCOnditionId);

                            //Response.Write(cCOndition.ConditionNameId);
                            //Response.End();

                            if (cCOndition.ConditionNameId != 1 && cCOndition.ConditionNameId != 5)
                            {
                                ProductCondition_Cancel_Extra cCancelExtra = new ProductCondition_Cancel_Extra();
                                List<object> Listcancel = cCancelExtra.GetProductCancelExtraListByConditionID(intCOnditionId);

                                foreach (ProductCondition_Cancel_Extra cancel in Listcancel)
                                {
                                    int intCancelId = cancel.CancelID;

                                    ProductCondition_Cancel_Extra_Rule cCancelrule = new ProductCondition_Cancel_Extra_Rule();
                                    List<object> ListCancelrule = cCancelrule.getCencelRuleExtranetbyCancelId(intCancelId);
                                    //ListItem = ListItem + "<div id=\"cancel_list_" + intCancelId + "\" >";
                                    int count = 0;



                                    foreach (ProductCondition_Cancel_Extra_Rule rule in ListCancelrule)
                                    {
                                        int intRuleId = rule.CanceRuleId;

                                        ListItem = ListItem + "<div class=\"cancel_list_item\" id=\"cancel_list_item_" + intRuleId + "\">";


                                        ListItem = ListItem + "<input type=\"checkbox\" checked=\"checked\" value=\"" + intRuleId + "\" name=\"cencel_list_Checked_toupdate\" style=\"display:none;\" />";

                                        ListItem = ListItem + "<table cellpadding=\"5\" cellspacing=\"1\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center; margin:0px 0px 0px 0px;\">";
                                        ListItem = ListItem + "<tr style=\"background-color:#ffffff;\" >";
                                        ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 2px 0px; padding:2px 0px 2px 0px;\" width=\"30%\">";
                                        ListItem = ListItem + "<select id=\"drop_daycancel_" + intRuleId + "\" class=\"Extra_Drop\" name=\"drop_daycancel_" + intRuleId + "\" >";

                                        for (int i = 0; i <= 120; i++)
                                        {
                                            string Ischecked = string.Empty;
                                            if (i == rule.DayCancel)
                                                Ischecked = "selected=\"selected\"";


                                            if (i == 0)
                                            {
                                                ListItem = ListItem + "<option value=\"" + i + "\" " + Ischecked + " >no-show</option>";
                                            }
                                            else
                                            {
                                                ListItem = ListItem + "<option value=\"" + i + "\" " + Ischecked + " >" + i + "</option>";
                                            }
                                        }

                                        ListItem = ListItem + "</select>";
                                        ListItem = ListItem + "</td>";
                                        ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 2px 0px; padding:2px 0px 2px 0px;\" width=\"30%\">";
                                        ListItem = ListItem + "<input type=\"text\" id=\"txt_day_charge_" + intRuleId + "\" value=\"" + rule.Chargenight + "\" style=\"width:20px;\" name=\"txt_day_charge_" + intRuleId + "\" class=\"Extra_textbox\"  />";
                                        ListItem = ListItem + "</td>";
                                        ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 2px 0px; padding:2px 0px 2px 0px;\" width=\"30%\">";
                                        ListItem = ListItem + "<input type=\"text\" id=\"txt_per_charge_" + intRuleId + "\" value=\"" + rule.ChargePercent + "\" style=\"width:22px;\" name=\"txt_per_charge_" + intRuleId + "\" class=\"Extra_textbox\" />";
                                        ListItem = ListItem + "</td>";
                                        if (count > 0)
                                        {
                                            ListItem = ListItem + "<td width=\"10%\"><img src=\"../../images_extra/del.png\" style=\"cursor:pointer;\" onclick=\"remove('cancel_list_item_" + intRuleId + "');return false;\" /></td>";
                                        }
                                        else
                                        {
                                            ListItem = ListItem + "<td width=\"10%\"></td>";
                                        }

                                        ListItem = ListItem + "</tr>";
                                        ListItem = ListItem + "</table>";
                                        ListItem = ListItem + "</div>";

                                        count = count + 1;
                                    }
                                }
                            }
                            

                            Response.Write(ListItem);

                        }
                        catch (Exception ex)
                        {
                            Response.Write("error:" + ex.Message + ex.StackTrace);
                            
                        }
                       
                    }
                }
                else
                {
                    Response.Write("method_invalid");
                }
                Response.End();
            }
            
        }


        
    }
}