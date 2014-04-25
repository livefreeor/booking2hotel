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

public partial class ajax_booking_payment_detail_insertform : System.Web.UI.Page
{
    
    public string qBookingId
    {
        get
        {
            return Request.QueryString["bid"];
        }
    }
    public string qBookingPaymentId
    {
        get
        {
            return Request.QueryString["payid"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            //Response.Write(getBookingPayMentInsert());
            //Response.Flush();
            Response.Write(getBookingPaymentDetailInsertForm());
            Response.Flush();
        }
    }
    public string getBookingPaymentDetailInsertForm()
    {
        BookingPaymentCat cPaymentCat = new BookingPaymentCat();
        Gateway cGateWay = new Gateway();
        //BookingPaymentDisplay cPaymant = new BookingPaymentDisplay();
        //cPaymant = cPaymant.GEtPaymentByBookingId_Latest(int.Parse(this.qBookingId));

        BookingPayment cBookingPayment = new BookingPayment();
        cBookingPayment = cBookingPayment.GetBookingPaymentByID(int.Parse(this.qBookingPaymentId));

        BookingdetailDisplay cBookingDisplay = new BookingdetailDisplay();
        cBookingDisplay = cBookingDisplay.GetBookingDetailListByBookingId(int.Parse(this.qBookingId));

        string strCusName = string.Empty;
        string strCusEmail = string.Empty;
        string strInformBy = string.Empty;
        string strAmount = string.Empty;
        string strBank = string.Empty;
        string strComment = string.Empty;
        if (string.IsNullOrEmpty(cBookingPayment.Comment))
        {
            strCusName = cBookingDisplay.FullName;
            strCusEmail = cBookingDisplay.Email;
            
        }
        else
        {
            string[] arrTransferDetail = cBookingPayment.Comment.Split('|');
            strCusName = arrTransferDetail[0];
            strCusEmail = arrTransferDetail[1];
            strInformBy = arrTransferDetail[2];
            strBank = arrTransferDetail[3];
            strAmount = arrTransferDetail[4];
            strComment = arrTransferDetail[5];
        }

        StringBuilder result = new StringBuilder();

        result.Append("<form id=\"paymeny_insert_form\" action=\"\" >");

        result.Append("<div class=\"formbox_head\">Payment Transfer Detail# BookingID: " + this.qBookingId + "</div>");
        result.Append("<div class=\"formbox_body\">");
        result.Append("<table id=\"tbl_payment_list\" cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:left;\">");
        result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Customer Name</td><td>&nbsp;<input type=\"text\" id=\"cus_name\"  class=\"TextBox_Extra_normal_small\" value=\"" + strCusName + "\" style=\"width:150px;\" name=\"cus_name\" /></td></tr>");
        result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Email</td><td>&nbsp;");
        result.Append("<input type=\"text\" id=\"cus_email\"  class=\"TextBox_Extra_normal_small\" value=\"" + strCusEmail + "\" style=\"width:150px;\" name=\"cus_email\" />");
        result.Append("</td></tr>");
        result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Inform By</td><td>&nbsp;");
        result.Append("<select id=\"inform_by\" name=\"inform_by\"  class=\"DropDownStyleCustom\" >");
        result.Append("<option value=\"Phone\" " + IsSelected(strInformBy, "Phone") + " >Phone</option>");
        result.Append("<option value=\"Fax\" " + IsSelected(strInformBy, "Fax") + " >Fax</option>");
        result.Append("<option value=\"Email\" " + IsSelected(strInformBy, "Email") + " >Email</option>");
        result.Append("</select>");
        result.Append("</td></tr>");
        result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Bank</td><td>&nbsp;");
        result.Append("<select id=\"transfer_bank\" name=\"transfer_bank\"  class=\"DropDownStyleCustom\" >");
        result.Append("<option value=\"Kbank\" " + IsSelected(strBank, "Kbank") + " >Kbank</option>");
        result.Append("<option value=\"Krungsri\" " + IsSelected(strBank, "Krungsri") + " >Krungsri</option>");
        result.Append("<option value=\"Krungsri\" " + IsSelected(strBank, "KrungThai") + " >Krungsri</option>");
        result.Append("<option value=\"Bangkok Bank\" " + IsSelected(strBank, "Bangkok Bank") + " >Bangkok Bank</option>");
        result.Append("<option value=\"TMB\" " + IsSelected(strBank, "TMB") + " >TMB</option>");
        result.Append("<option value=\"SCB\" " + IsSelected(strBank, "SCB") + " >SCB</option>");
        result.Append("<option value=\"UOB\" " + IsSelected(strBank, "UOB") + " >UOB</option>");
        
        result.Append("</select>");
        result.Append("</td></tr>");
        result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Amount</td><td>&nbsp;<input type=\"text\" value=\"" + GrandRequestTotal()+ "\" id=\"payment_amount\"  class=\"TextBox_Extra_normal_small\" style=\"width:80px;\" name=\"payment_amount\" /></td></tr>");
        result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Comment</td><td>&nbsp;<textarea rows=\"4\" cols=\"20\" class=\"TextBox_Extra_normal_small\" style=\"width:300px;\" name=\"payment_comment\">" + strComment + "</textarea></td></tr>");
        result.Append("</table>");
        result.Append("</div>");
        result.Append("<div class=\"formbox_buttom\"><input type=\"button\" value=\"Save\" onclick=\"InsertPaymentDetail('" + qBookingPaymentId + "');\"  class=\"btStyleGreen\" />&nbsp;<input type=\"button\" value=\"Cancel\" onclick=\"DarkmanPopUp_Close();\" class=\"btStyleWhite\" style=\" width:80px\" /></div>");
        
        result.Append("</form>");
       

        return result.ToString();
    }


    protected string IsSelected(string val, string ItemVal)
    {
        string result = string.Empty;
        if (val == ItemVal)
            result = "selected=\"selected\"";
        return result;
    }
    protected string GrandRequestTotal()
    {
        BookingTotalAndBalance cBookingtotalPrice = new BookingTotalAndBalance();
        BookingdetailDisplay cBookingDetailDisplay = new BookingdetailDisplay();
        string result = "0";
        decimal Total = cBookingtotalPrice.getbalanceByBookingId(int.Parse(this.qBookingId)) * -1;
        result = Total.Hotels2Currency();
        //result = cBookingDetailDisplay.GetPriceTotalByBookingId(this.BookingId).Hotels2Currency();

        return result.ToString();
    }
}