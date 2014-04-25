using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Booking;



public partial class ajax_booking_payment_detail_newsave : System.Web.UI.Page
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
            Response.Write(getBookingPayMentInsert());
            Response.Flush();
            //Response.Write("HELLO");
            //Response.Flush();
        }
    }
    public string getBookingPayMentInsert()
    {
        string IsInsertCom = "false";
        string CusName = Request.Form["cus_name"];
        string Email = Request.Form["cus_email"];
        string InformBy = Request.Form["inform_by"];
        string Bank = Request.Form["transfer_bank"];
        string strComment = Request.Form["payment_comment"];
        decimal Amount = 0;
        if(!string.IsNullOrEmpty(Request.Form["payment_amount"]))
            Amount = decimal.Parse(Request.Form["payment_amount"]);


        string DetailPaymentTransfer = CusName + "|" + Email + "|" + InformBy + "|" + Bank + "|" + Amount + "|" + strComment;
        BookingPayment_bookings cBookingPayment = new BookingPayment_bookings();
        try
        {
            bool result = cBookingPayment.UpdateBookingPaymentTransferDetail(int.Parse(this.qBookingId), int.Parse(this.qBookingPaymentId), DetailPaymentTransfer);
            IsInsertCom = "true";
            return IsInsertCom;
        }
        catch(Exception ex)
        {
            return IsInsertCom = ex.Message;
        }
        

    }

    
   
}