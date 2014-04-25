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
    public partial class admin_ajax_extrabed_del : Hotels2BasePageExtra_Ajax
    {
        public string qDateStartPeriod
        {
            get{ return Request.QueryString["ds"];}
        }

        public string qDateEndPeriod
        {
            get{return Request.QueryString["dn"];}
        }

        public string qConId
        {
            get{return Request.QueryString["conid"];}
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (this.IsstaffDelete())
                {
                    if(!string.IsNullOrEmpty(this.qDateStartPeriod) && !string.IsNullOrEmpty(this.qDateEndPeriod) 
                        && !string.IsNullOrEmpty(this.qConId))
                    {
                        //Response.Write(this.qDateStartPeriod + "---" + this.qDateEndPeriod + "----" + this.qConId);
                        Response.Write(DelExtraBed());
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

        public string DelExtraBed()
        {
            string result = "False";
            PoductPriceExtra cPriceExtra = new PoductPriceExtra();
            try
            {
                result = cPriceExtra.DeletePriceExtra_extrabed(this.CurrentProductActiveExtra, this.CurrentSupplierId, int.Parse(this.qConId), this.qDateStartPeriod.Hotels2DateSplitYear("-"), this.qDateEndPeriod.Hotels2DateSplitYear("-"), this.CurrentStaffId);

            }
            catch (Exception ex)
            {
                Response.Write("error:" + ex.Message);
            }
            return result;
        }

    }
}