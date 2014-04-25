using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Booking;
using Hotels2thailand.Production;
using Hotels2thailand;
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_gala_edit_save : Hotels2BasePageExtra_Ajax
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (this.IsStaffAdd())
                {
                    try
                    {
                        Response.Write(GalaEditSave());
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                }
                else
                {
                    Response.Write("method_invalid");
                }
                
                Response.Flush();
            }
        }

        public string GalaEditSave()
        {
            string result = "False";

            int intOptionId = int.Parse(Request.Form["hd_option_Id"]);
            string strPriceId = Request.Form["hd_price_Id"].ToString();
            int intConditionId = int.Parse(Request.Form["hd_condition_Id"]);
            string GalaTitle = Request.Form["gala_title"];
            string GalaDetail = Request.Form["gala_detail"];
            DateTime dDateGala = Request.Form["hd_date_gala_form"].ToString().Hotels2DateSplitYear("-");
            decimal decGalaprice = decimal.Parse(Request.Form["gala_amount"]);
            bool galaForAdult = true;
            bool galaforChild = false;
            if(Request.Form["gala_for"] == "1")
            {
                galaForAdult = false;
                galaforChild = true;
            }

            try
            {
                Option cOption = new Option();
                cOption.UpdateOptionExtranet(this.CurrentProductActiveExtra, intOptionId, GalaTitle);
                result = "True";
            }
            catch(Exception ex)
            {
                Response.Write("error:##1## Option Title" + ex.Message);
                Response.End();
            }
            

            try
            {
                ProductOptionContent cOptionContent = new ProductOptionContent();
                cOptionContent.UpdateOptionContentLangExtranet(this.CurrentProductActiveExtra, intOptionId, 1, GalaTitle, GalaDetail);
                result = "True";

            }
            catch(Exception ex)
            {
                Response.Write("error:##2## Option Content" + ex.Message);
                Response.End();

            }

            try
            {
                ProductOptionGala cOptionGala = new ProductOptionGala();
                cOptionGala.UpdateOptionGalaExtranet(this.CurrentProductActiveExtra, intOptionId, dDateGala, galaForAdult, galaforChild);
                result = "True";
            }
            catch (Exception ex)
            {
                Response.Write("error:##3## Option Gala" + ex.Message);
                Response.End();

            }

            try
            {
                PoductPriceExtra cPriceExtra = new PoductPriceExtra();

                cPriceExtra.UpdatePriceExtra(this.CurrentProductActiveExtra, this.CurrentSupplierId, strPriceId, decGalaprice, dDateGala, this.CurrentStaffId, intConditionId);
                result = "True";
            }
            catch(Exception ex)
            {
                Response.Write("error:##4## Option Gala Price" + ex.Message);
                Response.End();
            }
            
            return result;
        }

        
    }
}