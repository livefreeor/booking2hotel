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


public partial class ajax_booking_payment_list : System.Web.UI.Page
{
    
    public string qBookingId
    {
        get
        {
            return Request.QueryString["bid"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            Response.Write(getBookingPayMent());
            Response.Flush();
        }
    }
    public string getBookingPayMent()
    {
        BookingPaymentDisplay cPaymentDisplay = new BookingPaymentDisplay();
        BookingPaymentCat cPaymentCat = new BookingPaymentCat();
        Gateway cGateWay = new Gateway();
        StringBuilder result = new StringBuilder();
        BookingdetailDisplay cBooking = new BookingdetailDisplay();
        cBooking = cBooking.GetBookingDetailListByBookingId(int.Parse(this.qBookingId));
        result.Append("<h4><img   src=\"../../images/content.png\" /> Booking Configuration</h4>");
        result.Append("<p class=\"contentheadedetail\">List Supplier of This Product, you can Change or Add Supplier to List&nbsp;<a href=\"\" onclick=\"newPaymentForm('" + cBooking .LangId+ "');return false;\">Make new payment to resubmit</a>&nbsp;");
        if (cBooking.PaymentTypeID == 2)
        {
            result.Append("&nbsp;|&nbsp;<a href=\"\" onclick=\"getCard('" + cBooking.FullName + "');return false;\"> Card Detail</a>");
        }
        
        result.Append("</p><br />");
        result.Append("<table id=\"tbl_payment_list\" cellpadding=\"0\" cellspacing=\"2\"  bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center;\">");
        result.Append("<tr style=\"background-color:#3f5d9d;color:#ffffff;font-weight:bold;height:10px;line-height:10px;\">");
        result.Append("<td style=\"width:2%\">No.</td><td style=\"width:13%\">Payment ID</td><td style=\"width:8%\">Payment method</td><td style=\"width:10%\">Bank</td>");
        result.Append("<td style=\"width:10%\">Amount</td style=\"width:10%\"><td style=\"width:15%\">Confirm payment</td><td style=\"width:15%\">Confirm Settle</td><td style=\"width:20%\">Menu</td></tr>");
        int count = 1;
        foreach (BookingPaymentDisplay paymentItem in cPaymentDisplay.GEtPaymentByBookingId(int.Parse(this.qBookingId)))
        {
            result.Append("<tr  style=\"background-color:#ffffff; height:25px;\" id=\"" + paymentItem.PaymentId + "\">");
            result.Append("<td>" + count + "</td>");
            result.Append("<td>" + paymentItem.PaymentId + "</br>" + paymentItem.Title + "<br/>" + paymentItem.DatePayment.ToString("dddd, MMM dd, yyyy: hh:mm tt") +"</td>");
            result.Append("<td><span>" + paymentItem.CatTitle + "</span>");
            
            result.Append("</td>");

            string GatwayTitle = paymentItem.GateWayTitle;
            if (paymentItem.CatId == 2)
                GatwayTitle = "None";

            result.Append("<td><span>" + GatwayTitle + "</span>");
            
            result.Append("</td>");
                


            result.Append("<td><span>" + paymentItem.Amount.Hotels2Currency() + "</span></td>");
            result.Append("<td>" + PicStatusNameConfirm(paymentItem.ConfirmPayment, paymentItem.PaymentId, 1, paymentItem.CatId, paymentItem.Comment) + "</td>");

            result.Append("<td>" + PicStatusNameConfirm(paymentItem.ConfirmSettle, paymentItem.PaymentId,2) + "</td>");
            result.Append("<td>");

            if (paymentItem.PaymentTypeId == 2)
            {
                if (paymentItem.ConfirmPayment.HasValue)
                    result.Append("<input type=\"button\" class=\"btStyleWhite_small\" style=\"width:60px;\" value=\"Charge\" id=\"charge_payment_" + paymentItem.PaymentId + "\"  onclick=\"Cannotresubmit();return false;\" />&nbsp;");
                else
                    result.Append("<input type=\"button\" class=\"btStyleGreen_small\" style=\"width:60px;\" value=\"Charge\" id=\"charge_payment_" + paymentItem.PaymentId + "\" onclick=\"window.open('http://www.hotels2thailand.com/booking_resubmit.aspx?pcode=" + EncodeId(paymentItem.PaymentId) + "','_blank');return false;\" />&nbsp;");

            }
           

            if (paymentItem.ConfirmPayment.HasValue)
                result.Append("<input type=\"button\" class=\"btStyleWhite_small\" style=\"width:80px;\" value=\"Resubmit\" id=\"resubmit_payment_" + paymentItem.PaymentId + "\"  onclick=\"Cannotresubmit();return false;\" />");
             else
                result.Append("<input type=\"button\" class=\"btStyleGreen_small\" style=\"width:80px;\" value=\"Resubmit\" id=\"resubmit_payment_" + paymentItem.PaymentId + "\"  onclick=\"window.open('resubmit_send.aspx?bid=" + this.qBookingId + "&payid=" + paymentItem.PaymentId + "','_blank');return false;\" />");



            result.Append("&nbsp;<input type=\"button\" class=\"btStyle_small\" style=\"width:50px;\" value=\"Edit\" id=\"resubmit_edit_" + paymentItem.PaymentId + "\"  onclick=\"Editpayment('" + paymentItem.PaymentId + "', '" + cBooking.LangId + "');return false;\" />");
            result.Append("</td>");
            result.Append("</tr>");
            count = count + 1;
        }

        result.Append("</table>");

        return result.ToString();
    }

    public string PicStatusNameConfirm(DateTime? DateConfirm, int intPaymentId, byte ConfirmType)
    {
        string imageName = string.Empty;
        if (DateConfirm.HasValue)
        {
            DateTime dDate = (DateTime)DateConfirm;
            imageName = "<img src=\"../../images/true.png\"/></br>" + dDate.ToString("ddd, MMM dd, yyyy ; HH:mm") + "<img src=\"../../images/refresh.png\" onclick=\"DarkmanPopUpComfirm(400,'Would you like to swicth back now!!??' ,'confirmswitchbackPayment(" + intPaymentId + "," + ConfirmType + ");');return false;\"  style=\"cursor:pointer;\" />";
        }

        else
            imageName = "<img src=\"../../images/false.png\"/></br><a href=\"\" onclick=\"DarkmanPopUpComfirm(400,'Would you like to confirm now!!??' ,'BookingPaymentConfirm(" + intPaymentId + "," + ConfirmType + ");');return false;\">Confirm Now</a>";

        return imageName;
    }
    public string PicStatusNameConfirm(DateTime? DateConfirm, int intPaymentId, byte ConfirmType, byte bytTransferType, string transferDetail)
    {
        string imageName = string.Empty;
        if (DateConfirm.HasValue)
        {
            DateTime dDate = (DateTime)DateConfirm;
            imageName = "<img src=\"../../images/true.png\"/></br>" + dDate.ToString("ddd, MMM dd, yyyy ; HH:mm") + "<img src=\"../../images/refresh.png\" onclick=\"DarkmanPopUpComfirm(400,'Would you like to swicth back now!!??' ,'confirmswitchbackPayment(" + intPaymentId + "," + ConfirmType + ");');return false;\"  style=\"cursor:pointer;\" />";
            if (bytTransferType == 2)
            {
                if (string.IsNullOrEmpty(transferDetail))
                    imageName = imageName + "<br/><a href=\"\" onclick=\"PaymentDetailInsert('" + intPaymentId + "');return false;\">please insert transfer detail!</a>";
                else
                    imageName = imageName + "<br/><a href=\"\" onclick=\"GetPaymentDetail('" + intPaymentId + "');return false;\">transfer detail</a>";
            }

        }
        else
        {
            if (bytTransferType == 2)
            {

                //CannotPaymentComfirm
                if (string.IsNullOrEmpty(transferDetail))
                {
                    imageName = "<img src=\"../../images/false_gray.png\"/></br><a href=\"\" style=\"color:#cccccc;\" onclick=\"CannotPaymentComfirm();return false;\">Confirm Now</a>";
                    imageName = imageName + "<br/><a href=\"\" onclick=\"PaymentDetailInsert('" + intPaymentId + "');return false;\">please insert transfer detail!</a>";
                }

                else
                {
                    imageName = "<img src=\"../../images/false.png\"/></br><a href=\"\" onclick=\"DarkmanPopUpComfirm(400,'Would you like to confirm now!!??' ,'BookingPaymentConfirm(" + intPaymentId + "," + ConfirmType + ");');return false;\">Confirm Now</a>";
                    imageName = imageName + "<br/><a href=\"\" onclick=\"PaymentDetailInsert('" + intPaymentId + "');return false;\">transfer detail</a>";
                }
                   

                
            }
            else
            {
                imageName = "<img src=\"../../images/false.png\"/></br><a href=\"\" onclick=\"DarkmanPopUpComfirm(400,'Would you like to confirm now!!??' ,'BookingPaymentConfirm(" + intPaymentId + "," + ConfirmType + ");');return false;\">Confirm Now</a>";
            }


            
            
        }

        return imageName;
    }

    protected string EncodeId(int IdtoEncod)
    {
        string Random = Hotels2String.Hotels2RandomStringNuM(20);
        string strToEndCode = IdtoEncod + Random;
        string EncodeResult = strToEndCode.Hotel2EncrytedData_SecretKey();
        return HttpUtility.UrlEncode(EncodeResult);
    }
   
}