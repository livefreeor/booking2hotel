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
    public partial class admin_ajax_gala_save_insertbox : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffAdd())
                {

                    Response.Write(insertOptionGala());
                    //Response.Write("HELLO");
                }
                else
                {
                    Response.Write("method_invalid");
                }
                Response.End();
                
            }
        }


        public string insertOptionGala()
        {
            string Iscompleted = "False";
            string Galatitle = string.Empty;
            string GalaDetail  = string.Empty;
            decimal GalaRate = 0;

            if(!string.IsNullOrEmpty(Request.Form["gala_title"]))
                Galatitle = Request.Form["gala_title"];
            //else
            //{
            //    Response.Write("gala_title");
            //    Response.End();
            //}

            if(!string.IsNullOrEmpty(Request.Form["gala_detail"]))
                GalaDetail = Request.Form["gala_detail"];
            //else
            //{
            //    Response.Write("gala_detail");
            //    Response.End();
            //}


            if (!string.IsNullOrEmpty(Request.Form["gala_rate"]))
                GalaRate = decimal.Parse(Request.Form["gala_rate"]);
            //else
            //{
            //    Response.Write("gala_rate");
            //    Response.End();
            //}

            DateTime GalaDate = Request.Form["hd_gala_date"].Hotels2DateSplitYear("-");
            if (GalaDate.CompareTo(DateTime.Now.Date) < 0)
            {
                Response.Write("gala_date");
                Response.End();
            }

            bool Galaforadult  = true;
            bool GalaforChild = false;
            string GalaFor = "(for adult)";
            if(Request.Form["chk_adult_child"] == "1")
            {
                Galaforadult = false;
                GalaforChild = true;
                GalaFor = "(for children)";
            }
           


            Staff cStaff = this.CurrentStaffobj;
            short shrSupId = this.CurrentSupplierId;

             int OptionId  = 0;
             try
             {
                 Option cOption = new Option
                 {
                     CategoryID = 47,
                     ProductID = this.CurrentProductActiveExtra,
                     Title = Galatitle + GalaFor,
                     Priority = 0
                 };

                 OptionId = cOption.InsertOption(cOption);


                 Iscompleted = "True";
             }
             catch (Exception ex)
             {
                 Iscompleted = "error: ##1## , insert to Option = " + ex.Message;
                 Response.Write(Iscompleted);
                 Response.End();
             }

             try
             {

                 ProductOptionGala.InsertOptionGalaOnlyExtraNet(this.CurrentProductActiveExtra,OptionId, GalaDate, Galaforadult, GalaforChild);
                 
             }
             catch (Exception ex)
             {
                 Iscompleted = "error: ##1.1## , insert to OptionGala = " + ex.Message;
                 Response.Write(Iscompleted);
                 Response.End();
             }

             try
             {
                 ProductOptionContent cOptionContent = new ProductOptionContent();
                 cOptionContent.InsertOptionContentExtra(this.CurrentProductActiveExtra, OptionId, 1, Galatitle + GalaFor, GalaDetail);
                 
             }
             catch(Exception ex)
             {
                 Iscompleted = "error: ##1.2## , insert to OptionContent= " + ex.Message;
                 Response.Write(Iscompleted);
                 Response.End();
             }

             try
             {
                 // insert Mapping OptionSupplier
                 Option.insertOptionMappingSupplierExtra(this.CurrentProductActiveExtra, OptionId, shrSupId);
             }
             catch (Exception ex)
             {
                 Iscompleted = "error: ##1.3## , insert to Mapping OptionSupplier= " + ex.Message;
                 Response.Write(Iscompleted);
                 Response.End();
             }
             
            int Condition_id = 0;
           
            try
            {
                ProductConditionExtra cContentExtraInsert = new ProductConditionExtra();
                //##2## insert to Condition=====================================================================
                Condition_id = cContentExtraInsert.insertNewCondition(this.CurrentProductActiveExtra, OptionId, "Gala Condition",
                    1, 1, 0, 1, true,1);
                //==============================================================================================

                // Insert Condition Content ====================================================================
                int IsConditionContent_Insert_Completed = cContentExtraInsert.InsertConditionContentExtra(this.CurrentProductActiveExtra, Condition_id, 1, "Gala Condition");
                //==============================================================================================

                Iscompleted = "True";
            }
            catch (Exception ex)
            {
                Iscompleted = "error: ##2## , insert to Condition= " + ex.Message;
                Response.Write(Iscompleted);
                Response.End();
            }
           
            
            //##3## Insert Rate Period 
            try
            {
                PoductPriceExtra cPrice = new PoductPriceExtra();

                cPrice.InsertPriceExtra(this.CurrentProductActiveExtra, this.CurrentSupplierId, Condition_id, GalaRate, this.CurrentStaffId, GalaDate);

                Iscompleted = "True";
                
            }
            catch (Exception ex)
            {
                Iscompleted = "error: ##3## , Insert Rate Period  " + ex.Message;
                Response.Write(Iscompleted);
                Response.End();
            }

            return Iscompleted;
        }
    }
}