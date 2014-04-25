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
    public partial class admin_ajax_meal_insert_new : Hotels2BasePageExtra_Ajax
    {
        
        public string qOptionId
        {
            get { return Request.QueryString["oid"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffAdd())
                {
                    //if (!string.IsNullOrEmpty(this.qOptionId))
                    //{
                        string result = "False";
                        try
                        {

                            

                            ProductOptionContent cOptionContent = new ProductOptionContent();
                            ProductPriceExtra_period cPricePeriod = new ProductPriceExtra_period();
                            ProductConditionNameExtra cConditionNameExtra = new ProductConditionNameExtra();
                            ProductConditionExtra cContentExtra = new ProductConditionExtra();
                            ProductConditionContentExtra cConditionPolicy = new ProductConditionContentExtra();
                            ProductCondition_Cancel_Extra cCancellationExtra = new ProductCondition_Cancel_Extra();
                            ProductCondition_Cancel_Extra_Rule cRule = new ProductCondition_Cancel_Extra_Rule();
                            ProductPriceExtra_period cPrice = new ProductPriceExtra_period();


                            byte bytNumadult = 0;
                            byte bytNumChild = 0;
                            byte bytNumExtra = 0;
                            byte bytNumBreakfast = 0;
                            int intProductId = this.CurrentProductActiveExtra;
                            short SupplierId = this.CurrentSupplierId;
                            string PackageTitle = string.Empty;
                            int OptionID = 0;
                            int Condition_id = 0;
                         
                            string ConditionTitle = string.Empty;
                           

                            if (string.IsNullOrEmpty(this.qOptionId))
                            {


                                OptionID = int.Parse(Request.Form["drop_option"]);
                                ConditionTitle = "Meal Condition";
                                bytNumBreakfast = 0;
                                bytNumadult = 1;
                                bytNumChild = 0;
                                bytNumExtra = 0;

                                try
                                {
                                    //##2## insert to Condition
                                    ProductConditionExtra Topcondition = cContentExtra.getTopConditionByOptionId(OptionID);
                                    if (Topcondition == null)
                                    {
                                        Condition_id = cContentExtra.insertNewCondition(intProductId, OptionID, ConditionTitle,
                                            bytNumBreakfast, bytNumadult, bytNumChild, bytNumExtra, true, 1);

                                        //##3## insert to Condition Content
                                        int IsConditionContent_Insert_Completed = cContentExtra.InsertConditionContentExtra(intProductId, Condition_id, 1, ConditionTitle);
                                    }
                                    else
                                    {
                                        Condition_id = Topcondition.ConditionId;
                                    }

                                    result = "True";
                                }
                                catch (Exception ex)
                                {
                                    Response.Write(ex.Message + "##2##");
                                    Response.End();
                                }

                               


                                //if (!string.IsNullOrEmpty(Request.Form["rate_result_checked"]))
                                //{
                                    

                                //}

                                try
                                {
                                    cPrice.InsertPriceExtra_Period_loadNewMeal(this.CurrentProductActiveExtra, this.CurrentSupplierId, Condition_id, this.CurrentStaffId);

                                    result = "True";
                                }
                                catch (Exception ex)
                                {
                                    Response.Write("##7##--" + ex.Message + "<br/>" + ex.StackTrace);
                                    Response.End();
                                }
                                //INSERT LOAD TARIFF COMPLATED !! 



                            }
                            else
                            {

                            }
                          

                            

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
               
            //}
            
        }


        
    }
}