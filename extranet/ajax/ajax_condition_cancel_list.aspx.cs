using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_condition_cancel_list : Hotels2BasePageExtra_Ajax
    {
        public string qOptionId
        {
            get { return Request.QueryString["oid"]; }
        }

        public string qConditionId
        {
            get { return Request.QueryString["cdid"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qConditionId))
                {
                    Response.Write(GetCancelList(int.Parse(this.qConditionId)));
                    //Response.Write(Request.QueryString["cdid"]);
                }
                Response.End();
            }
        }

        public string GetCancelList(int intConditionId)
        {
            ProductCondition_Cancel_Extra cCancelExtra = new ProductCondition_Cancel_Extra();
            List<object> Listcancel = cCancelExtra.GetProductCancelExtraListByConditionID(intConditionId);
            string ListItem = string.Empty;
            foreach (ProductCondition_Cancel_Extra cancel in Listcancel)
            {
                int intCancelId = cancel.CancelID;

                ListItem =  ListItem + "<div class=\"period_list_item\" id=\"period_list_item_" + intCancelId + "\" >";
                ListItem = ListItem + "<input type=\"checkbox\" checked=\"checked\" value=\"" + intCancelId + "\" style=\"display:none;\" name=\"period_list_Checked_toupdate\" />";
                ListItem = ListItem + "<table style=\"width:100%\" align=\"center\" cellpadding=\"0\" cellspacing=\"3\">";
                ListItem = ListItem + "<tr style=\"background-color:#3b5998;color:#ffffff;font-weight:bold;height:15px;line-height:15px;\">";
                ListItem = ListItem + "<td width=\"20%\"  align=\"center\">Date From</td><td  width=\"20%\" align=\"center\">Date To</td>";
                ListItem = ListItem + "<td width=\"40%\" align=\"center\">Cancellation Rule(s)</td><td width=\"10%\" align=\"center\">Delete</td></tr>";
                ListItem = ListItem + "<tr>";
                ListItem = ListItem + "<td align=\"center\" style=\"background-color:#edeff4;  padding:2px;\"><input type=\"textbox\" class=\"Extra_textbox\" name=\"date_from_" + intCancelId + "\" id=\"date_from_" + intCancelId + "\" value=\"" + cancel.DateStart.ToString("yyyy-MM-dd") + "\" /></td>";
                ListItem = ListItem + "<td align=\"center\" style=\"background-color:#edeff4;  padding:2px;\"><input type=\"textbox\" class=\"Extra_textbox\" name=\"date_to_" + intCancelId + "\" id=\"date_to_" + intCancelId + "\" / value=\"" + cancel.DateEnd.ToString("yyyy-MM-dd") + "\" /></td>";
                ListItem = ListItem + "<td align=\"center\">";


                ListItem = ListItem + "<table cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center; margin:0px 0px 0px 0px;\">";
                ListItem = ListItem + "<tr style=\"background-color:#edeff4;color:#333333;font-weight:bold;height:15px;line-height:15px;\"><td align=\"center\" width=\"40%\" >Day Cancel</td>";
                ListItem = ListItem + "<td align=\"center\" width=\"30%\">Night(s) charge</td><td align=\"center\" width=\"30%\" colspan=\"2\" >Percent Charge</td></tr>";
                ListItem = ListItem + "</table>";

                ProductCondition_Cancel_Extra_Rule cCancelrule  = new ProductCondition_Cancel_Extra_Rule();
                List<object> ListCancelrule = cCancelrule.getCencelRuleExtranetbyCancelId(intCancelId);
                ListItem = ListItem + "<div id=\"cancel_list_" + intCancelId + "\" >";
                int count = 0;
                foreach(ProductCondition_Cancel_Extra_Rule rule in ListCancelrule)
                {

                    ListItem = ListItem + "<div class=\"cancel_list_item\" id=\"cancel_list_item_" + intCancelId + "_" + rule.CanceRuleId + "\" >";


                    ListItem = ListItem + "<input type=\"checkbox\" checked=\"checked\" value=\"" + rule.CanceRuleId + "\" name=\"cencel_list_Checked_toupdate" + intCancelId + "\" style=\"display:none;\" />";

                    ListItem = ListItem + "<table cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center; margin:0px 0px 0px 0px;\">";
                    ListItem = ListItem + "<tr style=\"background-color:#ffffff;\" >";
                    ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 0px 0px; padding:2px 0px 0px 0px;\" width=\"40%\">";
                    ListItem = ListItem + "<select id=\"drop_daycancel\" class=\"Extra_Drop\" name=\"drop_daycancel_" + intCancelId + "_" + rule.CanceRuleId + "\" >";
                    
                    for (int i = 0; i <= 120; i++) {
                        string Ischecked = string.Empty;
                        if (i == rule.DayCancel)
                            Ischecked = "selected=\"selected\"";


                        if (i == 0) {
                            ListItem = ListItem + "<option value=\"" + i + "\" " + Ischecked + " >no-show</option>";
                        }
                        else {
                            ListItem = ListItem + "<option value=\"" + i + "\" " + Ischecked + " >" + i + "</option>";
                        }
                    }

                    ListItem = ListItem + "</select>";
                    ListItem = ListItem + "</td>";
                    ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 0px 0px; padding:2px 0px 0px 0px;\" width=\"30%\">";
                    ListItem = ListItem + "<input type=\"text\" id=\"day_charge\" value=\"" + rule.Chargenight+ "\" style=\"width:20px;\" name=\"txt_day_charge" + intCancelId + "_" + rule.CanceRuleId + "\" class=\"Extra_textbox\"  />";
                    ListItem = ListItem + "</td>";
                    ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 0px 0px; padding:2px 0px 0px 0px;\" width=\"25%\">";
                    ListItem = ListItem + "<input type=\"text\" id=\"percent_charge\" value=\""+rule.ChargePercent+"\" style=\"width:22px;\" name=\"txt_per_charge" + intCancelId + "_" + rule.CanceRuleId + "\" class=\"Extra_textbox\" />";
                    ListItem = ListItem + "</td>";
                    if (count > 0)
                    {
                        ListItem = ListItem + "<td width=\"5%\"><img src=\"../../images_extra/del.png\" style=\"cursor:pointer;\" onclick=\"removeEle('cancel_list_item_" + intCancelId + "_" + rule.CanceRuleId + "');return false;\" /></td>";
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

                ListItem = ListItem + "<a href=\"\" style=\" width:100%; text-align:center;\" onclick=\"addRule('" + intCancelId + "','cancel_list_" + intCancelId + "');return false;\" >add rules</a>";
                ListItem = ListItem + "</td>";

                ListItem = ListItem + "<td align=\"center\" style=\"background-color:#edeff4; padding:2px;\"><img src=\"../../images_extra/bin.png\" style=\"cursor:pointer;\"  onclick=\"removeEle('period_list_item_" + intCancelId + "');return false;\" /></td>";
                ListItem = ListItem + "</tr>";
                ListItem = ListItem + "</table>";
                ListItem = ListItem + "</div>";

            }

            return ListItem;
        }

        
    }
}