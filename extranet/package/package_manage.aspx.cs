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
    
    public partial class extranet_package_package_manage : Hotels2BasePageExtra
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                dropConditiontitleDataBind();
                dropRoomDataBind();
                ConditionDataBind();

                if (!string.IsNullOrEmpty(this.qOptionId))
                {
                    loadPackageEdit();
                }
                else
                {
                    GetCancellationDefault();
                }
            }
        }

        public void dropConditiontitleDataBind()
        {
            ProductConditionNameExtra cConditionTitle = new ProductConditionNameExtra();
            conditionTitle.DataSource = cConditionTitle.GetConditionNameList(1);
            conditionTitle.DataTextField = "Title";
            conditionTitle.DataValueField = "ConditionNameId";
            conditionTitle.DataBind();

        }
        public void dropRoomDataBind()
        {
            Option cOption = new Option();

            dropPackage.DataSource = cOption.GetProductOptionByProductId_PackageOnlyExtranet(this.CurrentProductActiveExtra, this.CurrentSupplierId, true);
            dropPackage.DataTextField = "Title";
            dropPackage.DataValueField = "OptionID";
            dropPackage.DataBind();

        }
        public void ConditionDataBind()
        {
            
            dropNight.DataSource = this.dicGetNumber(10);
            dropNight.DataTextField = "Value";
            dropNight.DataValueField = "Key";
            dropNight.DataBind();

            ListItem iItem = new ListItem("Day Trip", "1");
            dropNight.Items.Insert(0, iItem);

            drop_adult_child.DataSource = this.dicGetNumber(10);
            drop_adult_child.DataTextField = "Value";
            drop_adult_child.DataValueField = "Key";
            drop_adult_child.DataBind();
            drop_adult_child.SelectedValue = "1";

            

            //drop_breakfast.Items.RemoveAt(0);
            ListItem newList = new ListItem("Room only", "0");
            //drop_breakfast.Items.Insert(0, newList);

        }
        public void GetCancellationDefault()
        {
            string ListItem = string.Empty;

            ListItem = ListItem + "<div class=\"cancel_list_item\" id=\"cancel_list_item_1\">";


            ListItem = ListItem + "<input type=\"checkbox\" checked=\"checked\" value=\"1\" name=\"cencel_list_Checked\" style=\"display:none;\" />";

            ListItem = ListItem + "<table cellpadding=\"5\" cellspacing=\"1\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center; margin:0px 0px 0px 0px;\">";
            ListItem = ListItem + "<tr style=\"background-color:#ffffff;\" >";
            ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 2px 0px; padding:2px 0px 2px 0px;\" width=\"30%\">";
            ListItem = ListItem + "<select id=\"drop_daycancel_1\" class=\"Extra_Drop\" name=\"drop_daycancel_1\" >";

            for (int i = 0; i <= 120; i++)
            {
                string Ischecked = string.Empty;
                

                if (i == 0)
                {
                    ListItem = ListItem + "<option value=\"" + i + "\" selected=\"selected\" >no-show</option>";
                }
                else
                {
                    ListItem = ListItem + "<option value=\"" + i + "\"  >" + i + "</option>";
                }
            }

            ListItem = ListItem + "</select>";
            ListItem = ListItem + "</td>";
            ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 2px 0px; padding:2px 0px 2px 0px;\" width=\"30%\">";
            ListItem = ListItem + "<input type=\"text\" id=\"txt_day_charge_1\" maxlength=\"2\" value=\"0\" style=\"width:20px;\" name=\"txt_day_charge_1\" class=\"Extra_textbox\"  />";
            ListItem = ListItem + "</td>";
            ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 2px 0px; padding:2px 0px 2px 0px;\" width=\"30%\">";
            ListItem = ListItem + "<input type=\"text\" id=\"txt_per_charge_1\" maxlength=\"3\" value=\"0\" style=\"width:22px;\" name=\"txt_per_charge_1\" class=\"Extra_textbox\" />";
            ListItem = ListItem + "</td>";
            ListItem = ListItem + "<td width=\"10%\"></td>";
            ListItem = ListItem + "</tr>";
            ListItem = ListItem + "</table>";
            ListItem = ListItem + "</div>";

            ltCancelDefault.Text = ListItem;



        }


        public string GenPackageDetailXML()
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(Request.Form["packagelist"]))
            {
                string[] benefitVal = Request.Form["packagelist"].Split(',');


                result = result + "<PromotionShow>";
                result = result + "<head>Special Benefit</head>";
                result = result + "<List>";

                foreach (string benefit in benefitVal)
                {
                    result = result + "<item>" + Request.Form["package_" + benefit].ToString().Hotels2SPcharacter_remove() + "</item>";
                }

                result = result + "</List>";
                result = result + "</PromotionShow>";

            }
            return result;
        }


        public void loadPackageEdit()
        {

        }

        //public void btnLoad_package_Onclick(object sender, EventArgs e)
        //{
        //    string result = "False";
            
        //    ProductOptionContent cOptionContent = new ProductOptionContent();
        //    ProductPriceExtra_period cPricePeriod = new ProductPriceExtra_period();
        //    ProductConditionNameExtra cConditionNameExtra = new ProductConditionNameExtra();
        //    ProductConditionExtra cContentExtra = new ProductConditionExtra();
        //    ProductConditionContentExtra cConditionPolicy = new ProductConditionContentExtra();
        //    ProductCondition_Cancel_Extra cCancellationExtra = new ProductCondition_Cancel_Extra();
        //    ProductCondition_Cancel_Extra_Rule cRule = new ProductCondition_Cancel_Extra_Rule();
        //    PoductPriceExtra cPrice = new PoductPriceExtra();

            
        //    byte bytNumadult = 0;
        //    byte bytNumChild = 0;
        //    byte bytNumExtra = 0;
        //    byte bytNumBreakfast = 0;
        //    int intProductId = this.CurrentProductActiveExtra;
        //    short SupplierId = this.CurrentSupplierId;
        //    string PackageTitle = string.Empty;
        //    int OptionID = 0;
        //    int Condition_id = 0;
        //    string strOptionDetail = Request.Form["packagelist"];
        //    byte bytConditionName = byte.Parse(conditionTitle.SelectedValue);
        //    string ConditionTitle = string.Empty;
        //    DateTime dBookingStart = Request.Form["hd_Booking_DateStart"].Hotels2DateSplitYear("-");
        //    DateTime dBookingEnd = Request.Form["hd_Booking_DateEnd"].Hotels2DateSplitYear("-");
        //    DateTime dStatyStart = Request.Form["hd_Stay_DateStart"].Hotels2DateSplitYear("-");
        //    DateTime dStayEnd = Request.Form["hd_Stay_DateEnd"].Hotels2DateSplitYear("-");
        //    bool bolIsAdult = true;

        //    if (Request.Form["chk_adult_child"] == "1")
        //    {
        //        bolIsAdult = false;
        //        bytNumChild = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_adult_child"]);
        //        bytNumadult = 0;
        //    }
        //    else
        //    {
        //        bytNumadult = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_adult_child"]);
        //        bytNumChild = 0;
        //    }

        //    decimal decPrice = 0;
        //    if(!string.IsNullOrEmpty(txtPrice.Text))
        //        decPrice = decimal.Parse(txtPrice.Text);
            
        //    if ( string.IsNullOrEmpty(this.qOptionId))
        //    {

        //        //if(Create New All Package)
        //        if (hd_isCurrent.Value == "false")
        //        {
        //            PackageTitle = txtPackage.Text;
                    
        //            Option cOption = new Option
        //            {
        //                Title = PackageTitle,
        //                CategoryID = 57,
        //                ProductID = intProductId,
        //                Night = byte.Parse(dropNight.SelectedValue),
        //                BookintDateStart = dBookingStart,
        //                BookingDAteEnd = dBookingEnd,
        //                StayDateStart = dStatyStart,
        //                StayDateEnd = dStayEnd,
        //            };

        //            //Insert OPtion
        //            OptionID = cOption.InsertOption(cOption);

        //            cOptionContent.InsertOptionContentExtra(this.CurrentProductActiveExtra, OptionID, 1, PackageTitle, strOptionDetail);
        //            //Insert MApping Option Supplier 
        //            cOption.insertOptionMappingSupplier_ExtraNet(intProductId, OptionID, SupplierId);



        //        }


        //        //if Create From Current Package
        //        if (hd_isCurrent.Value == "true")
        //        {
        //            OptionID = int.Parse(dropPackage.SelectedValue);
        //        }

        //            ConditionTitle = cConditionNameExtra.GetConditionNameById(bytConditionName, 1).Title;
        //            // byte bytConditionName = byte.Parse(conditionTitle.SelectedValue);

        //            //bytNumadultChild = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_adult_child"]);

                     
        //            //bytNumChild = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_child"]);
        //            //bytNumExtra = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_extrabed"]);
        //            //bytNumBreakfast = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_breakfast"]);
                   
                    
        //            try
        //            {

        //                //##2## insert to Condition

        //                Condition_id = cContentExtra.insertNewCondition_Package(intProductId, OptionID, ConditionTitle,
        //                    bytNumBreakfast, bytNumadult, bytNumChild, bytNumExtra, true, bytConditionName, bolIsAdult);

        //                result = "True";
        //            }
        //            catch (Exception ex)
        //            {
        //                Response.Write(ex.Message + "##2##");
        //                Response.End();
        //            }

        //            try
        //            {

        //                //##3## insert to Condition Content
        //                int IsConditionContent_Insert_Completed = cContentExtra.InsertConditionContentExtra(intProductId, Condition_id, 1, ConditionTitle);

        //                result = "True";
        //            }
        //            catch (Exception ex)
        //            {
        //                Response.Write(ex.Message + "##3##");
        //                Response.End();
        //            }


        //            //===============================
        //            int intCancelId = 0;
        //            //Cancellation

        //            // Save Not nonrefund Only
        //            if (bytConditionName != 1)
        //            {
        //                try
        //                {

        //                    //##5## Insert Cancellation 'tbl_product_option_condition_cancel_extra_net'
        //                    intCancelId = cCancellationExtra.InsertNewConditionCancel(intProductId, Condition_id, dStatyStart, dStayEnd, "");

        //                    result = "True";
        //                }
        //                catch (Exception ex)
        //                {
        //                    Response.Write(ex.Message + "##5##");
        //                    Response.End();
        //                }


        //                string RuleList = Request.Form["cencel_list_Checked"];
        //                if (!string.IsNullOrEmpty(RuleList))
        //                {
        //                    try
        //                    {
        //                        foreach (string cancelItem in RuleList.Split(','))
        //                        {

        //                            byte DayCancel = byte.Parse(Request.Form["drop_daycancel_" + cancelItem]);
        //                            byte DayCharge = byte.Parse(Request.Form["txt_day_charge_" + cancelItem]);
        //                            byte PerCharge = byte.Parse(Request.Form["txt_per_charge_" + cancelItem]);

        //                            //##6## Insert Cancellation Rult '' tbl_product_option_condition_cancel_content_extra_net''
        //                            cRule.InsertNewCancelRule(intProductId, intCancelId, DayCancel, PerCharge, DayCharge);
        //                        }
        //                        result = "True";
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Response.Write(ex.Message + "##6##");
        //                        Response.End();
        //                    }


        //                }

        //            }


        //            if (!string.IsNullOrEmpty(txtPrice.Text))
        //            {
        //                cPricePeriod.InsertPriceExtra_period(intProductId, SupplierId, Condition_id, decPrice, this.CurrentStaffId, dStatyStart, dStayEnd);
        //                //Price.InsertPriceExtra_Loadtariff(this.CurrentProductActiveExtra, this.CurrentSupplierId, Condition_id, this.CurrentStaffId);
        //                //##7## Insert Price '' tbl_product_option_condition_price_extranet
        //                // cPrice.InsertPriceExtra(ProductID, Condition_id, totalPrice, dDateCurrent);

        //                result = "True";

        //            }

        //            //INSERT LOAD TARIFF COMPLATED !! 

                

        //    }
        //    else
        //    {

        //    }

        //    if (result == "True")
        //    {
        //        Response.Redirect("package.aspx?" + this.AppendCurrentQueryString().Hotels2LeftClr(1));
        //    }
        //}
    }
}