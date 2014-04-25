using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;
using Hotels2thailand;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_rate_plan_condition_delete : Hotels2BasePageExtra_Ajax
    {
        public string qRateplanIdList
        {
            get { return Request.QueryString["plid"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qRateplanIdList))
                {
                    if (this.IsstaffDelete())
                    {

                        Response.Write(Updatestatus());

                    }
                    else
                    {
                        Response.Write("method_invalid");
                    }
                }
                Response.End();
            }
        }


        public string Updatestatus()
        {
            string result = "False";

            try
            {
                ProductCondition_rate_plan cRatePlan = new ProductCondition_rate_plan();
                cRatePlan.UpdateRatePlanStatus(this.qRateplanIdList,this.CurrentProductActiveExtra,false);
                result = "True";
            }
            catch (Exception ex)
            {
                Response.Write("error:" + ex.Message);
                Response.End();
            }
            

            return result;
        }
       
    }
}