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

public partial class ajax_booking_payment_detail : System.Web.UI.Page
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
            //Response.Write("HELLO");
            //Response.Flush();
            Response.Write(getBookingPaymentDetailInsertForm());
            Response.Flush();
        }
    }
    public string getBookingPaymentDetailInsertForm()
    {
        BookingPaymentCat cPaymentCat = new BookingPaymentCat();
        Gateway cGateWay = new Gateway();
        BookingPayment cBookingPayment = new BookingPayment();
        cBookingPayment = cBookingPayment.GetBookingPaymentByID(int.Parse(this.qBookingPaymentId));

        string strCusName = string.Empty;
        string strCusEmail = string.Empty;
        string strInformBy = string.Empty;
        string strAmount = string.Empty;
        string strBank = string.Empty;
        string strComment = string.Empty;
        if (!string.IsNullOrEmpty(cBookingPayment.Comment))
        {
            string[] arrTransferDetail = cBookingPayment.Comment.Split('|');
            strCusName = arrTransferDetail[0];
            strCusEmail = arrTransferDetail[1];
            strInformBy = arrTransferDetail[2];
            strBank = arrTransferDetail[3];
            strAmount = arrTransferDetail[4];
            strComment = arrTransferDetail[5];
            
        }
        //BookingPaymentDisplay cPaymant = new BookingPaymentDisplay();
        //cPaymant = cPaymant.GEtPaymentByBookingId_Latest(int.Parse(this.qBookingId));
        StringBuilder result = new StringBuilder();

        result.Append("<form id=\"paymeny_insert_form\" action=\"\" >");

        result.Append("<div class=\"formbox_head\">Payment Transfer Detail# BookingID: " + this.qBookingId + "</div>");
        result.Append("<div class=\"formbox_body\">");
        result.Append("<table id=\"tbl_payment_list\" cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:left;\">");
        result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Customer Name</td><td>&nbsp;" + strCusName + "</td></tr>");
        result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Email</td><td>&nbsp;" + strCusEmail + "</td></tr>");

        result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Inform By</td><td>&nbsp;" + strInformBy + "</td></tr>");
        result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Bank</td><td>&nbsp;" + strBank + "</td></tr>");
        result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Amount</td><td>&nbsp;" + strAmount + "</td></tr>");
        result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Comment</td><td>&nbsp;" + strComment + "</td></tr>");
        result.Append("</table>");
        result.Append("</div>");
        result.Append("<div class=\"formbox_buttom\"><input type=\"button\" value=\"Close\" onclick=\"DarkmanPopUp_Close();\" class=\"btStyleWhite\" style=\" width:80px\" /></div>");
        
        result.Append("</form>");
       

        return result.ToString();
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