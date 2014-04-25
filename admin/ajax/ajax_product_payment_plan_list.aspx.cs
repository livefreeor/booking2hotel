using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;


public partial class ajax_product_payment_plan_list : System.Web.UI.Page
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
            Response.Write(GetpaymentList());
            Response.End();
        }
    }

    public string GetpaymentList()
    {
        ProductPaymentPlan Payment = new ProductPaymentPlan();
        List<object> ListPayment = Payment.GetProductPaymentPlanListByProductId(int.Parse(this.qProductId));

        StringBuilder result = new StringBuilder();
        result.Append("<table cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center\">");
        result.Append("<tr style=\"background-color:#3f5d9d;color:#ffffff;font-weight:bold\">");
        result.Append("<td>DateStart- DatEnd</td><td>DayAdvance</td><td>Daypayment</td>");
        result.Append("<td></td>");
        foreach (ProductPaymentPlan item in ListPayment)
        {
            result.Append("<tr style=\"background-color:#ffffff;\">");
            result.Append("<td><input type=\"text\" class=\"TextBox_Extra_normal\" style=\" width:120px; padding:2px;\" name=\"plan_Datestart_" + item.PaymentPlanID + "\" id=\"plan_Datestart_" + item.PaymentPlanID + "\" value=\"" + item.DateStart.ToString("yyyy-MM-dd") + "\"/>&nbsp;-&nbsp;");
            result.Append("<input type=\"text\" class=\"TextBox_Extra_normal\" style=\" width:120px; padding:2px;\" name=\"plan_DateEnd_" + item.PaymentPlanID + "\" id=\"plan_DateEnd_" + item.PaymentPlanID + "\"  value=\"" + item.DateEnd.ToString("yyyy-MM-dd") + "\" /></td>");
            result.Append("<td><select name=\"day_advance_" + item.PaymentPlanID + "\" class=\"DropDownStyleCustom\">");
            for (int i = 1; i <= 90; i++)
            {
                if (i == item.DayAdvance)
                    result.Append("<option value=\"" + i + "\" selected=\"selected\">" + i + "</option>");
                else
                    result.Append("<option value=\"" + i + "\">" + i + "</option>");
            }
            result.Append("</select></td>");
            result.Append("<td><select name=\"day_payment_" + item.PaymentPlanID + "\" class=\"DropDownStyleCustom\">");
            for (int i = 1; i <= 90; i++)
            {
                if (i == item.DayPayment)
                    result.Append("<option value=\"" + i + "\" selected=\"selected\">" + i + "</option>");
                else
                    result.Append("<option value=\"" + i + "\">" + i + "</option>");
            }
            result.Append("</select></td>");
           

            result.Append("<td><input type=\"button\" value=\"Save\" class=\"btStyleGreen\" onclick=\"PaymentPlanUpdate('" + item.PaymentPlanID + "');return false;\" />&nbsp;<input type=\"button\" value=\"Delete\" onclick=\"PaymentPlanDelete('" + item.PaymentPlanID + "');return false;\" class=\"btStyleRed\" style=\"width:100px;\" /></td>");
            result.Append("</tr>");
        }
        result.Append("");
        result.Append("");
        result.Append("</table>");

       return result.ToString();
    }
    

    
}