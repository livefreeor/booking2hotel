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
    public partial class admin_ajax_meal_del : Hotels2BasePageExtra_Ajax
    {
        public string qPeriodId
        {
            get{ return Request.QueryString["prid"];}
        }

        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (this.IsstaffDelete())
                {
                    if (!string.IsNullOrEmpty(this.qPeriodId))
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

        public bool DelExtraBed()
        {
            bool result = false;
            

            ProductPriceExtra_period cPricePeriod = new ProductPriceExtra_period();
            int Period_id = int.Parse(this.qPeriodId);
            try
            {
                result = cPricePeriod.UpdatePriceExtra_periodById(Period_id);

            }
            catch (Exception ex)
            {
                Response.Write("error:" + ex.Message);
            }
            return result;
        }

    }
}