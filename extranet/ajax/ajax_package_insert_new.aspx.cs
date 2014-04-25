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
    public partial class admin_ajax_package_insert_new : Hotels2BasePageExtra_Ajax
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
                            string strOptionDetail = Request.Form["txt_package_detail"];
                            byte bytConditionName = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$conditionTitle"]);
                            string ConditionTitle = string.Empty;
                            DateTime dBookingStart = Request.Form["hd_Booking_DateStart"].Hotels2DateSplitYear("-");
                            DateTime dBookingEnd = Request.Form["hd_Booking_DateEnd"].Hotels2DateSplitYear("-");
                            DateTime dStatyStart = Request.Form["hd_Stay_DateStart"].Hotels2DateSplitYear("-");
                            DateTime dStayEnd = Request.Form["hd_Stay_DateEnd"].Hotels2DateSplitYear("-");
                            bool bolIsAdult = true;

                            if (Request.Form["chk_adult_child"] == "1")
                            {
                                bolIsAdult = false;
                                bytNumChild = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_adult_child"]);
                                bytNumadult = 0;
                            }
                            else
                            {
                                bytNumadult = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_adult_child"]);
                                bytNumChild = 0;
                            }

                            decimal decPrice = 0;
                            if (!string.IsNullOrEmpty(Request.Form["ctl00$ContentPlaceHolder1$txtPrice"]))
                                decPrice = decimal.Parse(Request.Form["ctl00$ContentPlaceHolder1$txtPrice"]);

                            if (string.IsNullOrEmpty(this.qOptionId))
                            {

                                //if(Create New All Package)
                                if (Request.Form["ctl00$ContentPlaceHolder1$hd_isCurrent"] == "false")
                                {
                                    PackageTitle = Request.Form["ctl00$ContentPlaceHolder1$txtPackage"];

                                    Option cOption = new Option
                                    {
                                        Title = PackageTitle,
                                        CategoryID = 57,
                                        ProductID = intProductId,
                                        Night = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropNight"]),
                                        BookintDateStart = dBookingStart,
                                        BookingDAteEnd = dBookingEnd,
                                        StayDateStart = dStatyStart,
                                        StayDateEnd = dStayEnd,
                                    };

                                    //Insert OPtion
                                    OptionID = cOption.InsertOption(cOption);

                                    //Response.Write(strOptionDetail);
                                    //Response.End();
                                    cOptionContent.InsertOptionContentExtra(this.CurrentProductActiveExtra, OptionID, 1, PackageTitle, strOptionDetail);
                                    //Insert MApping Option Supplier 
                                    cOption.insertOptionMappingSupplier_ExtraNet(intProductId, OptionID, SupplierId);



                                }


                                //if Create From Current Package
                                if (Request.Form["ctl00$ContentPlaceHolder1$hd_isCurrent"] == "true")
                                {
                                    OptionID = int.Parse( Request.Form["ctl00$ContentPlaceHolder1$dropPackage"]);
                                }

                                ConditionTitle = cConditionNameExtra.GetConditionNameById(bytConditionName, 1).Title;
                                // byte bytConditionName = byte.Parse(conditionTitle.SelectedValue);

                                //bytNumadultChild = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_adult_child"]);


                                //bytNumChild = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_child"]);
                                //bytNumExtra = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_extrabed"]);
                                //bytNumBreakfast = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_breakfast"]);


                                try
                                {

                                    //##2## insert to Condition

                                    Condition_id = cContentExtra.insertNewCondition_Package(intProductId, OptionID, ConditionTitle,
                                        bytNumBreakfast, bytNumadult, bytNumChild, bytNumExtra, true, bytConditionName, bolIsAdult);

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
                                    int IsConditionContent_Insert_Completed = cContentExtra.InsertConditionContentExtra(intProductId, Condition_id, 1, ConditionTitle);

                                    result = "True";
                                }
                                catch (Exception ex)
                                {
                                    Response.Write(ex.Message + "##3##");
                                    Response.End();
                                }


                                //===============================
                                int intCancelId = 0;
                                //Cancellation

                                // Save Not nonrefund Only
                                if (bytConditionName != 1)
                                {
                                    try
                                    {

                                        //##5## Insert Cancellation 'tbl_product_option_condition_cancel_extra_net'
                                        intCancelId = cCancellationExtra.InsertNewConditionCancel(intProductId, Condition_id, dStatyStart, dStayEnd, "");

                                        result = "True";
                                    }
                                    catch (Exception ex)
                                    {
                                        Response.Write(ex.Message + "##5##");
                                        Response.End();
                                    }


                                    string RuleList = Request.Form["cencel_list_Checked"];
                                    if (!string.IsNullOrEmpty(RuleList))
                                    {
                                        try
                                        {
                                            foreach (string cancelItem in RuleList.Split(','))
                                            {

                                                byte DayCancel = byte.Parse(Request.Form["drop_daycancel_" + cancelItem]);
                                                byte DayCharge = byte.Parse(Request.Form["txt_day_charge_" + cancelItem]);
                                                byte PerCharge = byte.Parse(Request.Form["txt_per_charge_" + cancelItem]);

                                                //##6## Insert Cancellation Rult '' tbl_product_option_condition_cancel_content_extra_net''
                                                cRule.InsertNewCancelRule(intProductId, intCancelId, DayCancel, PerCharge, DayCharge);
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


                                //if (!string.IsNullOrEmpty(Request.Form["ctl00$ContentPlaceHolder1$txtPrice"]))
                                //{
                                //    cPricePeriod.InsertPriceExtra_period(intProductId, SupplierId, Condition_id, decPrice, this.CurrentStaffId, dStatyStart, dStayEnd);
                                //    //Price.InsertPriceExtra_Loadtariff(this.CurrentProductActiveExtra, this.CurrentSupplierId, Condition_id, this.CurrentStaffId);
                                //    //##7## Insert Price '' tbl_product_option_condition_price_extranet
                                //    // cPrice.InsertPriceExtra(ProductID, Condition_id, totalPrice, dDateCurrent);

                                //    result = "True";

                                //}


                                if (!string.IsNullOrEmpty(Request.Form["rate_result_checked"]))
                                {
                                    try
                                    {
                                        cPrice.InsertPriceExtra_Period_loadNewPackage(this.CurrentProductActiveExtra, this.CurrentSupplierId, Condition_id, this.CurrentStaffId);
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