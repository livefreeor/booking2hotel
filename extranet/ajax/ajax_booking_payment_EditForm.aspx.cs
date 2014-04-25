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

public partial class ajax_booking_payment_EditForm : System.Web.UI.Page
{
    
    public string qBookingPaymentId
    {
        get
        {
            return Request.QueryString["pmid"];
        }
    }
    public string qBookingLangId
    {
        get
        {
            return Request.QueryString["lang"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            Response.Write(getBookingPayMentInsert());
            Response.Flush();
        }
    }
    public string getBookingPayMentInsert()
    {
        BookingPaymentDisplay cPaymentDisplay = new BookingPaymentDisplay();
        BookingPaymentCat cPaymentCat = new BookingPaymentCat();
        Gateway cGateWay = new Gateway();
        StringBuilder result = new StringBuilder();
        cPaymentDisplay = cPaymentDisplay.GEtPaymentByPaymentId(int.Parse(this.qBookingPaymentId));
        result.Append("<form id=\"paymeny_insert_form\" action=\"\" >");

        result.Append("<div class=\"formbox_head\">Insert New Payment</div>");
        result.Append("<div class=\"formbox_body\">");
        result.Append("<table id=\"tbl_payment_list\" cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:left;\">");
        result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Title</td><td>&nbsp;<input type=\"text\" id=\"payment_title\"  class=\"Extra_textbox_form\" value=\"" + cPaymentDisplay.Title + "\" style=\"width:150px;\" name=\"payment_title\" /></td></tr>");
        
        result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Payment Cat</td><td>&nbsp;");

        if (cPaymentDisplay.ConfirmPayment.HasValue)
            result.Append("<select id=\"payment_cat\" name=\"payment_cat\" class=\"Extra_Drops_small_form\"  disabled=\"disabled\" >");
        else
            result.Append("<select id=\"payment_cat\" name=\"payment_cat\" class=\"Extra_Drops_small_form\"  style=\" width:250px;\" >");

        foreach (KeyValuePair<byte, string> catItem in cPaymentCat.GetPaymentCatList())
        {
            if (this.qBookingLangId == "1" && catItem.Key != 2)
            {
                if (catItem.Key == cPaymentDisplay.CatId)
                    result.Append("<option value=\"" + catItem.Key + "\" selected=\"selected\" >" + catItem.Value + "</option>");
                else
                    result.Append("<option value=\"" + catItem.Key + "\">" + catItem.Value + "</option>");
            }
            if (this.qBookingLangId == "2" )
            {
                if (catItem.Key == cPaymentDisplay.CatId)
                    result.Append("<option value=\"" + catItem.Key + "\" selected=\"selected\" >" + catItem.Value + "</option>");
                else
                    result.Append("<option value=\"" + catItem.Key + "\">" + catItem.Value + "</option>");
            }

        }

        result.Append("</select>");
        result.Append("</td></tr>");

        //result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;GateWay Bank</td><td>&nbsp;");
        //if (cPaymentDisplay.ConfirmPayment.HasValue)
        //    result.Append("<select id=\"payment_gateway\" name=\"payment_gateway\"  class=\"Extra_Drops_small_form\" disabled=\"disabled\" >");
        //else
        //    result.Append("<select id=\"payment_gateway\" name=\"payment_gateway\"  class=\"Extra_Drops_small_form\" >");

        //foreach (KeyValuePair<byte, string> gatewayItem in cGateWay.getGateWayList())
        //{
        //    if (gatewayItem.Key == cPaymentDisplay.GateWayId)
        //        result.Append("<option value=\"" + gatewayItem.Key + "\" selected=\"selected\">" + gatewayItem.Value + "</option>");
        //    else
        //        result.Append("<option value=\"" + gatewayItem.Key + "\">" + gatewayItem.Value + "</option>");
        //}
        //result.Append("</select>");
        //result.Append("</td></tr>");

        result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Amount</td><td>&nbsp;<input type=\"text\" id=\"payment_amount\" value=\"" + cPaymentDisplay.Amount.Hotels2Currency() + "\" class=\"Extra_textbox_form\" style=\"width:80px;\" name=\"payment_amount\" /></td></tr>");
        //result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Comment</td><td>&nbsp;<textarea rows=\"4\" cols=\"20\" class=\"TextBox_Extra_normal_small\" style=\"width:300px;\" name=\"payment_comment\"></textarea></td></tr>");
        result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Payment Status</td> ");

        result.Append("<td>");
        if (cPaymentDisplay.Status)
        {
            if (cPaymentDisplay.ConfirmPayment.HasValue)
            {
                result.Append("<input type=\"radio\" name=\"payment_status\" disabled=\"disabled\" value=\"True\" checked=\"checked\" /> Active <br/>");
                result.Append("<input type=\"radio\" name=\"payment_status\" disabled=\"disabled\" value=\"False\" /> Inactive");
            }
            else
            {
                result.Append("<input type=\"radio\" name=\"payment_status\" value=\"True\" checked=\"checked\" /> Active <br/>");
                result.Append("<input type=\"radio\" name=\"payment_status\" value=\"False\" /> Inactive");
            }
        }
        else
        {
            result.Append("<input type=\"radio\" name=\"payment_status\"   value=\"True\"  /> Active <br/>");
            result.Append("<input type=\"radio\" name=\"payment_status\"  value=\"False\" checked=\"checked\" /> Inactive");
        }
        result.Append("</td></tr>");
       

        
        result.Append("</table>");
        result.Append("</div>");
        result.Append("<div class=\"formbox_buttom\"><input type=\"button\" value=\"Save\" onclick=\"EditSavePayment('" + this.qBookingPaymentId + "');return false;\"  class=\"btStyleGreen\" />&nbsp;<input type=\"button\" value=\"Cancel\" onclick=\"DarkmanPopUp_Close();\" class=\"btStyleWhite\" style=\" width:80px\" /></div>");
        
        result.Append("</form>");
       

        return result.ToString();
    }

    
   
}