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
    public partial class admin_ajax_package_update_condition : Hotels2BasePageExtra_Ajax
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
            
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffEdit())
                {
                    if (!string.IsNullOrEmpty(this.qConditionId))
                    {
                        string result = "False";
                        try
                        {


                            int intConditionId = int.Parse(this.qConditionId);
                            //DateTime dStayStart = Request.Form["hd_Stay_DateStart"].Hotels2DateSplitYear("-");
                            //decimal decPrice = decimal.Parse(Request.Form["ctl00$ContentPlaceHolder1$txtPrice"]);
                            //GetProductCancelExtraListByConditionID
                            int intProductId = this.CurrentProductActiveExtra;
                            // ##3## UPdate Cancellation


                            ProductConditionExtra cCondition = new ProductConditionExtra();
                            cCondition = cCondition.getConditionByConditionId(intConditionId);
                            
                            byte bytNumGuest =  byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_adult_child"]);
                            byte bytNumadult = 0;
                            byte bytnumChild = 0;
                            if (cCondition.IsAdult)
                                bytNumadult = bytNumGuest;
                            else
                                bytnumChild = bytNumGuest;

                            cCondition.UpdateConditionExtra_Package(intProductId, cCondition.ConditionId, bytNumadult, bytnumChild);

                            //byte bytConditionName = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$conditionTitle"]);
                            if (cCondition.ConditionNameId != 1)
                            {
                                try
                                {

                                   // string PeriodListinsert = Request.Form["period_list_Checked"];
                                    //string PeriodListupdate = Request.Form["period_list_Checked_toupdate"];
                                    ProductCondition_Cancel_Extra cCancelExtra = new ProductCondition_Cancel_Extra();
                                    ProductCondition_Cancel_Extra_Rule cRule = new ProductCondition_Cancel_Extra_Rule();
                                    List<object> Listcancel = cCancelExtra.GetProductCancelExtraListByConditionID(intConditionId);

                                    //if (!string.IsNullOrEmpty(PeriodListupdate))
                                    //{
                                        

                                        foreach (ProductCondition_Cancel_Extra cancelToupdate in Listcancel)
                                        {
                                            
                                            //update Cancel Rule
                                            ProductCondition_Cancel_Extra_Rule cCancelRule = new ProductCondition_Cancel_Extra_Rule();
                                            List<object> ruleList = cCancelRule.getCencelRuleExtranetbyCancelId(cancelToupdate.CancelID);


                                            string cancelruleUpdate = Request.Form["cencel_list_Checked_toupdate"];

                                            string cancelruleInsert = Request.Form["cencel_list_Checked"];

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
                                                    byte DayCancel = byte.Parse(Request.Form["drop_daycancel_"+ rule]);
                                                    byte DayCharge = byte.Parse(Request.Form["txt_day_charge_" +  rule]);
                                                    byte PerCharge = byte.Parse(Request.Form["txt_per_charge_" +  rule]);

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
                                                    byte DayCancel = byte.Parse(Request.Form["drop_daycancel_" +  rule]);
                                                    byte DayCharge = byte.Parse(Request.Form["txt_day_charge_" +  rule]);
                                                    byte PerCharge = byte.Parse(Request.Form["txt_per_charge_" +  rule]);

                                                    cRule.InsertNewCancelRule(this.CurrentProductActiveExtra, cancelToupdate.CancelID, DayCancel, PerCharge, DayCharge);
                                                }

                                            }
                                            //------------
                                        }

                                   
                                    result = "True";
                                }
                                catch (Exception ex)
                                {
                                    Response.Write("##3## error: Update Canceltion " + ex.Message + "<br/>" + ex.StackTrace);
                                    Response.End();
                                }
                            }

                            ProductPriceExtra_period cPrice = new ProductPriceExtra_period();

                            //cPrice = cPrice.getPricePackageByConditionId(intConditionId);
                            cPrice.UPdatePriceExtra_period_package(intProductId, this.CurrentSupplierId, intConditionId, this.CurrentStaffId);
                            result = "True";

                            

                        }
                        catch (Exception ex)
                        {
                            Response.Write("error:" + ex.Message + "<br/>" + ex.StackTrace);
                            Response.End();
                        }

                        Response.Write(result);
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("method_invalid");
                }
               
            }
            
        }


        
    }
}