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
    public partial class admin_ajax_condition_edit_save : Hotels2BasePageExtra_Ajax
    {
        public string qConditionId
        {
            get { return Request.QueryString["cdid"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffEdit())
                {
                    Response.Write(SaveEditConditionControl(int.Parse(this.qConditionId)));
                    //Response.Write("--");
                }
                else
                {
                    Response.Write("method_invalid");
                }
                
                Response.End();
            }
        }

        public string SaveEditConditionControl(int intConditionId)
        {
            string result = "False";
                
            // ##1## UPdateConditionDetail 
            try
            {
                
                byte Adult = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_adult"]);
                byte Child = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_child"]);
                byte Abf = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_breakfast"]);
                byte Extra = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_extrabed"]);
                
                ProductConditionExtra cConditionExtra = new ProductConditionExtra();
                cConditionExtra.UpdateConditionExtra(this.CurrentProductActiveExtra, intConditionId, Abf, Adult, Child, Extra, true);


                result = "True";
            }
            catch (Exception ex)
            {
                Response.Write("##1## error: Update Condition Detail " + ex.Message);
                Response.End();
            }
           
            // ##2## Update Policy
            try
            {
                string PolicyInsert = Request.Form["policylist"];
                string PolicyUpdate = Request.Form["policylist_to_update"];
                ProductConditionContentExtra cPolicyExtra = new ProductConditionContentExtra();
                List<object> ListPolicy = cPolicyExtra.GetListConditionDetail_policyByConditionId(intConditionId, 1);
                
                if (!string.IsNullOrEmpty(PolicyUpdate))
                {
                    string[] arrPolicyUpdate = PolicyUpdate.Split(',');

                    object IshavePolicy = null;
                    foreach (ProductConditionContentExtra policy in ListPolicy)
                    {
                        IshavePolicy = arrPolicyUpdate.SingleOrDefault(po => int.Parse(po) == policy.ContentId);
                       if (IshavePolicy == null)
                           cPolicyExtra.UpdateConditionStatusExtra(this.CurrentProductActiveExtra, policy.ContentId, false);
                    }

                    foreach (string policyupdate in arrPolicyUpdate)
                    {
                        string PolicyType = Request.Form["policy_type_" + policyupdate];
                        string Policy = Request.Form["policy_" + policyupdate];
                        cPolicyExtra.UpdateConditionExtra(this.CurrentProductActiveExtra, int.Parse(policyupdate), PolicyType, Policy);
                    }
                }
                else
                {
                    foreach (ProductConditionContentExtra policy in ListPolicy)
                    {
                        cPolicyExtra.UpdateConditionStatusExtra(this.CurrentProductActiveExtra, policy.ContentId, false);
                    }
                }


                if (!string.IsNullOrEmpty(PolicyInsert))
                {
                    string[] arrPolicyInsert = PolicyInsert.Split(',');
                    ProductConditionContentExtra cConditionPolicy = new ProductConditionContentExtra();
                    foreach (string policyinsert in arrPolicyInsert)
                    {
                        string PolicyTitle = Request.Form["policy_type_" + policyinsert];
                        string Policy = Request.Form["policy_" + policyinsert];

                        cConditionPolicy.insertConditionDetail(this.CurrentProductActiveExtra, intConditionId, 1, PolicyTitle, Policy);
                    }
                }

                result = "True";
            }
            catch (Exception ex)
            {
                Response.Write("##2## error: Update Policy " + ex.Message);
                Response.End();
            }

            // ##3## UPdate Cancellation
            byte bytConditionName = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$hd_ConditionName"]);
            if (bytConditionName != 1)
            {
                try
                {

                    string PeriodListinsert = Request.Form["period_list_Checked"];
                    string PeriodListupdate = Request.Form["period_list_Checked_toupdate"];
                    ProductCondition_Cancel_Extra cCancelExtra = new ProductCondition_Cancel_Extra();
                    ProductCondition_Cancel_Extra_Rule cRule = new ProductCondition_Cancel_Extra_Rule();
                    List<object> Listcancel = cCancelExtra.GetProductCancelExtraListByConditionID(intConditionId);

                    if (!string.IsNullOrEmpty(PeriodListupdate))
                    {
                        string[] arrperiodlistupdate = PeriodListupdate.Split(',');

                        object IshaveCancel = null;
                        foreach (ProductCondition_Cancel_Extra cancelToupdate in Listcancel)
                        {
                            IshaveCancel = arrperiodlistupdate.SingleOrDefault(ce => int.Parse(ce) == cancelToupdate.CancelID);
                            if (IshaveCancel == null)
                                cCancelExtra.UpdateConditionCancelExtraNetStatus(this.CurrentProductActiveExtra, cancelToupdate.CancelID, false);
                        }


                        foreach (string cancelupdate in arrperiodlistupdate)
                        {
                            DateTime dDateStart = Request.Form["hd_date_from_" + cancelupdate].Hotels2DateSplitYear("-");
                            DateTime dDateEnd = Request.Form["hd_date_to_" + cancelupdate].Hotels2DateSplitYear("-");
                            // Update Cancel
                            cCancelExtra.UpdateConditionCancelExtraNet(this.CurrentProductActiveExtra, int.Parse(cancelupdate), dDateStart, dDateEnd);
                            //-----------------

                            int intCancelId = int.Parse(cancelupdate);
                            //update Cancel Rule
                            ProductCondition_Cancel_Extra_Rule cCancelRule = new ProductCondition_Cancel_Extra_Rule();
                            List<object> ruleList = cCancelRule.getCencelRuleExtranetbyCancelId(intCancelId);


                            string cancelruleUpdate = Request.Form["cencel_list_Checked_toupdate" + intCancelId];

                            string cancelruleInsert = Request.Form["cencel_list_Checked_" + intCancelId];

                            if (!string.IsNullOrEmpty(cancelruleUpdate))
                            {
                                string[] arrRuleLsit = cancelruleUpdate.Split(',');

                                //---------------
                                object IshaveRule = null;
                                foreach (ProductCondition_Cancel_Extra_Rule objrule in ruleList)
                                {
                                    IshaveRule = arrRuleLsit.SingleOrDefault(r => int.Parse(r) == objrule.CanceRuleId);
                                    if (IshaveRule == null)
                                        cCancelRule.UpdateCancelRuleByCanCelIdStatus(this.CurrentProductActiveExtra, objrule.CanceRuleId, false);
                                }

                                foreach (string rule in arrRuleLsit)
                                {
                                    byte DayCancel = byte.Parse(Request.Form["drop_daycancel_" + intCancelId + "_" + rule]);
                                    byte DayCharge = byte.Parse(Request.Form["txt_day_charge" + intCancelId + "_" + rule]);
                                    byte PerCharge = byte.Parse(Request.Form["txt_per_charge" + intCancelId + "_" + rule]);

                                    cCancelRule.UpdateCancelRuleByCanCelIdANdDayCancel(this.CurrentProductActiveExtra, int.Parse(rule), DayCancel, DayCharge, PerCharge);
                                }
                            }
                            else
                            {
                                foreach (ProductCondition_Cancel_Extra_Rule objrule in ruleList)
                                {
                                    cCancelRule.UpdateCancelRuleByCanCelIdStatus(this.CurrentProductActiveExtra, objrule.CanceRuleId, false);
                                }
                            }

                            if (!string.IsNullOrEmpty(cancelruleInsert))
                            {

                                string[] arrRuleLsit = cancelruleInsert.Split(',');

                                foreach (string rule in arrRuleLsit)
                                {
                                    byte DayCancel = byte.Parse(Request.Form["drop_daycancel_" + intCancelId + "_" + rule]);
                                    byte DayCharge = byte.Parse(Request.Form["txt_day_charge" + intCancelId + "_" + rule]);
                                    byte PerCharge = byte.Parse(Request.Form["txt_per_charge" + intCancelId + "_" + rule]);

                                    cRule.InsertNewCancelRule(this.CurrentProductActiveExtra, intCancelId, DayCancel, PerCharge, DayCharge);
                                }

                            }
                            //------------
                        }

                    }
                    else
                    {
                        foreach (ProductCondition_Cancel_Extra cancelToupdate in Listcancel)
                        {
                            cCancelExtra.UpdateConditionCancelExtraNetStatus(this.CurrentProductActiveExtra, cancelToupdate.CancelID, false);
                        }
                    }


                    if (!string.IsNullOrEmpty(PeriodListinsert))
                    {
                        string[] arrperiodlistinsert = PeriodListinsert.Split(',');

                        foreach (string cancelinsert in arrperiodlistinsert)
                        {
                            DateTime dDateStart = Request.Form["hd_date_from_" + cancelinsert].Hotels2DateSplitYear("-");
                            DateTime dDateEnd = Request.Form["hd_date_to_" + cancelinsert].Hotels2DateSplitYear("-");
                            int intCancelId = cCancelExtra.InsertNewConditionCancel(this.CurrentProductActiveExtra, intConditionId, dDateStart, dDateEnd, "");

                            string RuleUpdate = Request.Form["cencel_list_Checked_" + cancelinsert];


                            if (!string.IsNullOrEmpty(RuleUpdate))
                            {
                                string[] arrRuleLsit = RuleUpdate.Split(',');

                                foreach (string rule in arrRuleLsit)
                                {
                                    byte DayCancel = byte.Parse(Request.Form["drop_daycancel_" + cancelinsert + "_" + rule]);
                                    byte DayCharge = byte.Parse(Request.Form["txt_day_charge" + cancelinsert + "_" + rule]);
                                    byte PerCharge = byte.Parse(Request.Form["txt_per_charge" + cancelinsert + "_" + rule]);

                                    cRule.InsertNewCancelRule(this.CurrentProductActiveExtra, intCancelId, DayCancel, PerCharge, DayCharge);
                                }
                            }
                        }
                    }
                    result = "True";
                }
                catch (Exception ex)
                {
                    Response.Write("##3## error: Update Canceltion " + ex.Message);
                    Response.End();
                }
            }


            // ##4## UPdate Price (Optional)!!
            
            try
            {

                if (!string.IsNullOrEmpty(Request.Form["rate_result_checked"]))
                {
                    PoductPriceExtra cPriceExtra = new PoductPriceExtra();
                    cPriceExtra.InsertPriceExtranet_ConditionControl(this.CurrentProductActiveExtra, this.CurrentSupplierId, intConditionId, this.CurrentStaffId);
                }

            }catch (Exception ex)
            {
                Response.Write("##7## INSERT RATE error : " + ex.Message );
                Response.End();
            }
               
            return result;
        }
    }
}