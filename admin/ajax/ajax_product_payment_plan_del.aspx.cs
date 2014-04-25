using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;


public partial class ajax_product_payment_plan_del : System.Web.UI.Page
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
        if (!string.IsNullOrEmpty(this.qProductId))
        {
            //Response.Write(Request.Url.ToString());
            Response.Write(InsertNewPayment());
            Response.End();
        }
    }

    public string InsertNewPayment()
    {
        ProductPaymentPlan cPayment = new ProductPaymentPlan();
        string result = string.Empty;
        try
        {
            cPayment.DeletePaymentPlan(int.Parse(this.qPaymentPlanId), int.Parse(this.qProductId));
            result = "True";
        }catch(Exception ex)
        {
            result = ex.Message;
        }

        return result;
        
        
    }
    

    
}