using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;


public partial class ajax_product_payment_plan_save : System.Web.UI.Page
{
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
            cPayment.InsertNewPaymentPlan(int.Parse(this.qProductId), DateTime.Parse(Request.Form["hd_plan_Datestart"]),
                DateTime.Parse(Request.Form["hd_plan_DateEnd"]), byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropDayAdVance"]),
                byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropDayPayment"]));
            result = "True";
        }catch(Exception ex)
        {
            result = ex.Message;
        }

        return result;
        
        
    }
    

    
}