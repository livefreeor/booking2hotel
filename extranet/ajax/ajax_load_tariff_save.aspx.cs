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
    public partial class admin_ajax_load_tariff_save : Hotels2BasePageExtra_Ajax
    {
        public string qConditionId
        {
            get { return Request.QueryString["cdid"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffAdd())
                {
                    //Response.Write(Request.Form["ctl00$ContentPlaceHolder1$sur_checked"]);
                    Response.Write(SaveEditConditionControl());
                    //Response.Write("--");
                }
                else
                {
                    Response.Write("method_invalid");
                }
                
                Response.End();
            }
        }

        public string SaveEditConditionControl()
        {
            string result = "False";
            ProductConditionNameExtra cConditionNameExtra = new ProductConditionNameExtra();
            byte bytNumadult = 0;
            byte bytNumChild = 0;
            byte bytNumExtra = 0;
            byte bytNumBreakfast = 0;
            int ProductID = this.CurrentProductActiveExtra;
            int OptionID = int.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropRoom"]);
            byte bytConditionName = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$conditionTitle"]);
            string ConditionTitle = string.Empty;
            try
            {
                
                //Condition
                

                ConditionTitle = cConditionNameExtra.GetConditionNameById(bytConditionName, 1).Title;
                // byte bytConditionName = byte.Parse(conditionTitle.SelectedValue);

                bytNumadult = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_adult"]);
                bytNumChild = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_child"]);
                bytNumExtra = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_extrabed"]);
                bytNumBreakfast = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_breakfast"]);
                //=================================================


                //try
                //{

                //    //##1## Insert to Option WeekDayAll
                //    ProductOptionIsWeekdayAll cWeekDayAll = new ProductOptionIsWeekdayAll();
                //    cWeekDayAll.InsertIsWeekdayExtra(ProductID, OptionID, true, true, true, true, true, true, true);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message + "##1##");
                Response.End();
            }
            ProductConditionExtra cContentExtra = new ProductConditionExtra();
            int Condition_id = 0;
            try
            {

                //##2## insert to Condition

                Condition_id = cContentExtra.insertNewCondition(ProductID, OptionID, ConditionTitle,
                    bytNumBreakfast, bytNumadult, bytNumChild, bytNumExtra, true, bytConditionName);

                result = "True";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message + "##2##");
                Response.End();
            }

            try
            {

                //##3## insert to Condition Content
                int IsConditionContent_Insert_Completed = cContentExtra.InsertConditionContentExtra(ProductID, Condition_id, 1, ConditionTitle);

                result = "True";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message + "##3##");
                Response.End();
            }



            //policy
            if (!string.IsNullOrEmpty(Request.Form["policylist"]))
            {
                ProductConditionContentExtra cConditionPolicy = new ProductConditionContentExtra();
                foreach (string PolicyItem in Request.Form["policylist"].ToString().Split(','))
                {
                    string PolicyTitle = Request.Form["policy_type_" + PolicyItem];
                    string Policy = Request.Form["policy_" + PolicyItem];


                    //##4## Insert to Policy  ''tbl_product_option_condition_content_extra_net''
                    cConditionPolicy.insertConditionDetail(ProductID, Condition_id, 1, PolicyTitle, Policy);

                }


            }

            //===============================
            int intCancelId = 0;
            //Cancellation

            // Save Not nonrefund Only
            if (bytConditionName != 1)
            {
                string PeriodList = Request.Form["period_list_Checked"];

                if (!string.IsNullOrEmpty(PeriodList))
                {

                    ProductCondition_Cancel_Extra cCancellationExtra = new ProductCondition_Cancel_Extra();
                    ProductCondition_Cancel_Extra_Rule cRule = new ProductCondition_Cancel_Extra_Rule();


                    foreach (string PeriodItem in PeriodList.Split(','))
                    {

                        DateTime dDateStart = Request.Form["hd_date_from_" + PeriodItem].Hotels2DateSplitYear("-");
                        DateTime dDateEnd = Request.Form["hd_date_to_" + PeriodItem].Hotels2DateSplitYear("-");

                        try
                        {

                            //##5## Insert Cancellation 'tbl_product_option_condition_cancel_extra_net'
                            intCancelId = cCancellationExtra.InsertNewConditionCancel(ProductID, Condition_id, dDateStart, dDateEnd, "");

                            result = "True";
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message + "##5##");
                            Response.End();
                        }


                        string RuleList = Request.Form["cencel_list_Checked_" + PeriodItem];
                        if (!string.IsNullOrEmpty(RuleList))
                        {
                            try
                            {
                                foreach (string cancelItem in RuleList.Split(','))
                                {

                                    byte DayCancel = byte.Parse(Request.Form["drop_daycancel_" + PeriodItem + "_" + cancelItem]);
                                    byte DayCharge = byte.Parse(Request.Form["txt_day_charge" + PeriodItem + "_" + cancelItem]);
                                    byte PerCharge = byte.Parse(Request.Form["txt_per_charge" + PeriodItem + "_" + cancelItem]);

                                    //##6## Insert Cancellation Rult '' tbl_product_option_condition_cancel_content_extra_net''
                                    cRule.InsertNewCancelRule(ProductID, intCancelId, DayCancel, PerCharge, DayCharge);
                                }
                                result = "True";
                            }
                            catch (Exception ex)
                            {
                                Response.Write(ex.Message + "##6##");
                                Response.End();
                            }


                        }
                    }
                }
            }
            

            PoductPriceExtra cPrice = new PoductPriceExtra();

            if (!string.IsNullOrEmpty(Request.Form["rate_result_checked"]))
            {
                try
                {
                    cPrice.InsertPriceExtra_Loadtariff(this.CurrentProductActiveExtra, this.CurrentSupplierId, Condition_id, this.CurrentStaffId);
                    //##7## Insert Price '' tbl_product_option_condition_price_extranet
                   // cPrice.InsertPriceExtra(ProductID, Condition_id, totalPrice, dDateCurrent);

                    result = "True";
                }
                catch (Exception ex)
                {
                    Response.Write("##7##--" + ex.Message + "<br/>" + ex.StackTrace);
                    Response.End();
                }
               
            }

            //INSERT LOAD TARIFF COMPLATED !! 







            return result;
        }
    }
}