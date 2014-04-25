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
    public partial class admin_ajax_rate_control_save_edit : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffEdit())
                {
                    Response.Write(UpdateAndInsertRate());
                }
                else
                {
                    Response.Write("method_invalid");
                }
                Response.End();
                
            }
        }

        public string UpdateAndInsertRate()
        {
            string result = "False";

            PoductPriceExtra cPriceExtra = new PoductPriceExtra();
            try
            {
                cPriceExtra.UpdatePriceExtra_rateControl(this.CurrentProductActiveExtra, this.CurrentSupplierId, this.CurrentStaffId);
                
            }
            catch (Exception ex)
            {
                Response.Write("error#1#: Updaterate ----" + ex.Message + "<br/>" + ex.StackTrace);
                Response.End();
            }

            try
            {
                result = cPriceExtra.InsertPriceExtraRateControl(this.CurrentProductActiveExtra, this.CurrentSupplierId, this.CurrentStaffId);
            }
            catch (Exception ex)
            {
                Response.Write("error#2#: Insert rate blank " + ex.Message + "<br/>" + ex.StackTrace);
                Response.End();
            }

                
           

            return result.ToString();

        }
    }
}