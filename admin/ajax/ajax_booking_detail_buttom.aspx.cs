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


public partial class ajax_booking_detail_buttom : System.Web.UI.Page
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
            Response.Write(getBookingDetailButtom());
            Response.Flush();
        }
    }
    public string getBookingDetailButtom()
    {
        StringBuilder result = new StringBuilder();

        BookingTotalAndBalance cTotalBalance = new BookingTotalAndBalance();
        BookingPaymentDisplay cPaymentDisplay = new BookingPaymentDisplay();
        BookingdetailDisplay cBookingDetail = new BookingdetailDisplay();
        cBookingDetail = cBookingDetail.GetBookingDetailListByBookingId(int.Parse(this.qBookingId));
        decimal decBalance = cTotalBalance.getbalanceByBookingId(int.Parse(this.qBookingId));

        int intPayment = cPaymentDisplay.GEtPaymentByBookingId(int.Parse(this.qBookingId)).Count();
        //result.Append("<%--<a href=\"\" onclick=\"window.open('voucher_send.aspx?bid=" + this.qBookingId + "','_blank');return false;\" >Send Voucher to customer</a>--%>");
        result.Append("<p><asp:Literal ID=\"litSenVoucher\" runat=\"server\"></asp:Literal>");
        Response.Write("HELLO");
        Response.End();
        if (cBookingDetail.PaymentTypeID == 1)
        {
            int PaymentNotConfirm = cPaymentDisplay.GEtPaymentByBookingIdNotConfirm(int.Parse(this.qBookingId));
            if (PaymentNotConfirm > 0)
            {
                
                result.Append("<a href=\"\" onclick=\"CannotSendVoucher();return false;\" style=\"background:#f5f6f6;color:#333333;\">Send Voucher to customer</a>");
            }
              else
                result.Append("<a href=\"\" onclick=\"window.open('voucher_send.aspx?bid=" + this.qBookingId + "','_blank');return false;\" >Send Voucher to customer</a>");
                
        }

        if (cBookingDetail.PaymentTypeID == 2)
        {
           
            

            if (intPayment > 0)
                result.Append("<a href=\"\" onclick=\"window.open ('voucher_send.aspx?bid=" + this.qBookingId + "','_blank');return false;\" >Send Voucher to customer</a>");
            else
                result.Append("<a href=\"\" onclick=\"CannotSendVoucher();return false;\" style=\"background:#f5f6f6;color:#333333;\">Send Voucher to customer</a>");

            if (decBalance >= 0)
                result.Append("<a href=\"\" onclick=\"window.open('confirm_fullcahrge_send.aspx?bid=" + this.qBookingId + "','_blank');return false;\" >Send full charge completed</a>");
            else
                result.Append("<a href=\"\" onclick=\"CannotSendVoucher();return false;\" style=\"background:#f5f6f6;color:#333333;\">Send full charge completed</a>");

            result.Append("<a href=\"\" style=\" background:#ffee7b;color:#333333;\" onclick=\"window.open('fullybook_send.aspx?bid=" + this.qBookingId + "','_blank');return false;\" >Send mail fullbook and offer</a>");
            result.Append("<a href=\"\" style=\" background:#ffee7b;color:#333333;\" onclick=\"window.open('cancellation_send.aspx?bid=" + this.qBookingId + "','_blank');return false;\" >Send mail cancel</a>");


        }
        


        result.Append("<a href=\"\" onclick=\"window.open('review_send.aspx?bid=" + this.qBookingId + "','_blank');return false;\"  style=\"width:150px;\">Send Review</a>");

        if (!cBookingDetail.Status)
            result.Append("<a href=\"\" onclick=\"closebooking();return false;\" style=\" background:#ef2d2d\" >Close this Booking</a>");
        else
            result.Append("<a href=\"\" onclick=\"Openbooking();return false;\" style=\" background:#3f5d9d\" >Open this Booking</a>");

        result.Append("</p>");
        result.Append("<div style=\"clear:both\"></div>");

        if (cBookingDetail.ActiveExtranet)
        {
            result.Append("<div class=\"Button_extranet_panel\" style=\"text-align:center; margin:5px 0px 0px 0px;\" >");
            result.Append("<p style=\"font-size:14px;color:#333333; border:1px solid #d8dfea; height:30px;line-height:30px;background-color:#f4f4ff;\">Extra Net</p>");

            if (cBookingDetail.PaymentTypeID == 1)
            {
                //if (cBookingDetail.AckCountConfirm == 0)
                //    result.Append("<a href=\"\" onclick=\"ProcessAcknow('" + this.qBookingId + "', '" + cBookingDetail.ExtranetProductId + "','1');return false;\"  style=>Request Acknowledge Confirm</a>");
                //else
                //    result.Append("<a href=\"\" onclick=\"DarkmanPopUpAlert(400, 'Sorry! Update Already');return false;\" style=\"background:#f5f6f6;color:#333333;\">Request Acknowledge Confirm</a>");


                //if (cBookingDetail.AckCountCancel == 0)
                //    result.Append("<a href=\"\" onclick=\"ProcessAcknow('" + this.qBookingId + "', '" + cBookingDetail.ExtranetProductId + "','3');return false;\"  style=>Request Acknowledge cancel</a>");
                //else
                //    result.Append("<a href=\"\" onclick=\"DarkmanPopUpAlert(400, 'Sorry! Update Already');return false;\" style=\"background:#f5f6f6;color:#333333;\">Request Acknowledge cancel</a>");
                if (decBalance >= 0)
                    result.Append("<a href=\"\" onclick=\"ProcessAcknow('" + this.qBookingId + "', '" + cBookingDetail.ExtranetProductId + "','1');return false;\"  style=>Request Acknowledge Confirm</a>");
                else
                    result.Append("<a href=\"\" onclick=\"DarkmanPopUpAlert(400, 'Sorry! please Check Payment completed before');return false;\" style=\"background:#f5f6f6;color:#333333;\">Request Acknowledge Confirm</a>");


                if (decBalance >= 0)
                    result.Append("<a href=\"\" onclick=\"ProcessAcknow('" + this.qBookingId + "', '" + cBookingDetail.ExtranetProductId + "','3');return false;\"  style=>Request Acknowledge cancel</a>");
                else
                    result.Append("<a href=\"\" onclick=\"DarkmanPopUpAlert(400, 'Sorry! please Check Payment completed before');return false;\" style=\"background:#f5f6f6;color:#333333;\">Request Acknowledge cancel</a>");

            }

            if (cBookingDetail.PaymentTypeID == 2)
            {
                if (intPayment > 0)
                    result.Append("<a href=\"\" onclick=\"ProcessAcknow('" + this.qBookingId + "', '" + cBookingDetail.ExtranetProductId + "','1');return false;\"  style=>Request Acknowledge Confirm</a>");
                else
                    result.Append("<a href=\"\" onclick=\"DarkmanPopUpAlert(400, 'Sorry! Please Check Payment');return false;\" style=\"background:#f5f6f6;color:#333333;\">Request Acknowledge Confirm</a>");


                if (intPayment > 0)
                    result.Append("<a href=\"\" onclick=\"ProcessAcknow('" + this.qBookingId + "', '" + cBookingDetail.ExtranetProductId + "','3');return false;\"  style=>Request Acknowledge cancel</a>");
                else
                    result.Append("<a href=\"\" onclick=\"DarkmanPopUpAlert(400, 'Sorry! Please Check Payment');return false;\" style=\"background:#f5f6f6;color:#333333;\">Request Acknowledge cancel</a>");
            }
           

            result.Append("");
            result.Append("");
            result.Append("");
            result.Append("");
            result.Append("</div>");
        }

       

        return result.ToString();
    }

   
}