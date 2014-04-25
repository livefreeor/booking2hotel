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
    public partial class admin_ajax_extrabed_save_insertbox : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.Page.IsPostBack)
            {
                if (this.IsStaffAdd())
                {
                    
                   Response.Write(insertNewExtraBed());
                    //Response.Write("HELLO");
                }
                else
                {
                    Response.Write("method_invalid");
                }
                Response.End();
                
            }
        }


        public string insertNewExtraBed()
        {
            string Iscompleted = "False";

             int OptionId  = 0;

             if (!string.IsNullOrEmpty(Request.Form["drop_option"]))
             {
                 OptionId = int.Parse(Request.Form["drop_option"]);
             }

             DateTime dDateStart = Request.Form["hd_extrabed_date_start"].Hotels2DateSplitYear("-");
             DateTime dDateend = Request.Form["hd_extrabed_date_end"].Hotels2DateSplitYear("-");

             
             
            // check date_end must more than date_start
             if (dDateend.CompareTo(dDateStart) < 0)
             {
                 Iscompleted = "date_invalid";
                 Response.Write(Iscompleted);
                 Response.End();
             }

            ProductConditionExtra cContentExtra = new ProductConditionExtra();
            int Condition_id = 0;
            cContentExtra = cContentExtra.getTopConditionByOptionId(OptionId);
            if (cContentExtra == null)
            {
                try
                {
                    ProductConditionExtra cContentExtraInsert = new ProductConditionExtra();
                    //##2## insert to Condition=====================================================================
                    Condition_id = cContentExtraInsert.insertNewCondition(this.CurrentProductActiveExtra, OptionId, "Extrabed Condition",
                        1, 1, 0, 1, true,1);
                    //==============================================================================================

                    // Insert Condition Content ====================================================================
                    int IsConditionContent_Insert_Completed = cContentExtraInsert.InsertConditionContentExtra(this.CurrentProductActiveExtra, Condition_id, 1, "Extrabed Condition");
                    //==============================================================================================

                    Iscompleted = "True";
                }
                catch (Exception ex)
                {
                    Iscompleted = "error: ##2## , insert to Condition= " + ex.Message;
                    Response.Write(Iscompleted);
                    Response.End();
                }
            }
            else
            {
                Condition_id = cContentExtra.ConditionId;
            }

            
            //##3## Insert Rate Period 
            try
            {
                PoductPriceExtra cPriceExtra = new PoductPriceExtra();

               Iscompleted =  cPriceExtra.InsertPriceExtra_extrabed(this.CurrentProductActiveExtra, this.CurrentSupplierId, Condition_id, dDateStart, dDateend, this.CurrentStaffId);
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