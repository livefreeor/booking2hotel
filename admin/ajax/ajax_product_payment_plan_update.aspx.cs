using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;


public partial class ajax_product_payment_plan_update : System.Web.UI.Page
{
    public string qPaymentPlanId
    {
        get { return Request.QueryString["paypid"]; }
    }
    public string qProductId
    {
        get { return Request.QueryString["pid"]; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.qPaymentPlanId))
        {
            //Response.Write(Request.Url.ToString());
            //Response.Write(byte.Parse(Request.Form["day_advance_" + this.qPaymentPlanId]));
            //Response.Write(byte.Parse(Request.Form["day_payment_" + this.qPaymentPlanId]));
            //Response.Write(Request.Form["hd_plan_Datestart_" + this.qPaymentPlanId]);
            //Response.Write(Request.Form["hd_plan_DateEnd_" + this.qPaymentPlanId]);
            Response.Write(UpdatePaymentPlan());
            Response.End();
        }
    }

    public string UpdatePaymentPlan()
    {
        ProductPaymentPlan cPayment = new ProductPaymentPlan();
        string result = string.Empty;
        try
        {
            cPayment.UpdateProductPaymentPlan(int.Parse(this.qPaymentPlanId), int.Parse(this.qProductId), DateTime.Parse(Request.Form["hd_plan_Datestart_" + this.qPaymentPlanId]),
                DateTime.Parse(Request.Form["hd_plan_DateEnd_" + this.qPaymentPlanId]), byte.Parse(Request.Form["day_advance_" + this.qPaymentPlanId]),
                byte.Parse(Request.Form["day_payment_" + this.qPaymentPlanId]));
            result = "True";
        }catch(Exception ex)
        {
            result = ex.Message;
        }

        return result;
        
        
    }
    

    
}