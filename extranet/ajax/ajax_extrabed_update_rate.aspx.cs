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
    public partial class admin_ajax_extrabed_update_rate : Hotels2BasePageExtra_Ajax
    {

        public string qPriceId
        {
            get { return Request.QueryString["pri"]; }
        }
        public string qDateStartPeriod
        {
            get { return Request.QueryString["ds"]; }
        }

        public string qDateEndPeriod
        {
            get { return Request.QueryString["dn"]; }
        }

        public string qConId
        {
            get { return Request.QueryString["conid"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffEdit())
                {
                    if (!string.IsNullOrEmpty(this.qPriceId) && !string.IsNullOrEmpty(this.qDateStartPeriod) && !string.IsNullOrEmpty(this.qDateEndPeriod)
                        && !string.IsNullOrEmpty(this.qConId))
                    {
                        Response.Write(UpdateRate());
                    }
                    else
                    {
                        Response.Write("method");
                    }
                  
                }
                else
                {
                    Response.Write("method_invalid");
                }
                Response.End();
                
            }
        }

        public string UpdateRate()
        {
            string result = "False";
            PoductPriceExtra cPriceExtra = new PoductPriceExtra();
            try
            {
                decimal RateUpdate = decimal.Parse(Request.Form["price_" + qPriceId]);
                cPriceExtra.UPDATEPriceExtra_extrbed(this.CurrentProductActiveExtra, this.CurrentSupplierId, int.Parse(this.qConId), this.qDateStartPeriod.Hotels2DateSplitYear("-"),this.qDateEndPeriod.Hotels2DateSplitYear("-"), this.CurrentStaffId, RateUpdate);

                result = "True";
            }
            catch (Exception ex)
            {
                Response.Write("error:" + ex.Message);
            }
            return result;
        }

    }
}