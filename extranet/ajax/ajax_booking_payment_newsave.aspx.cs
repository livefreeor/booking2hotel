using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Booking;



public partial class ajax_booking_payment_newsave : System.Web.UI.Page
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
            Response.Write(getBookingPayMentInsert());
            Response.Flush();
        }
    }
    public string getBookingPayMentInsert()
    {
        
        string IsInsertCom = "false";
        byte bytPaymenyCat = byte.Parse(Request.Form["payment_cat"]);
        string Title = Request.Form["payment_title"];
        //byte bytGateWay = byte.Parse(Request.Form["payment_gateway"]);
        //string strComment = Request.Form["payment_comment"];
        decimal Amount = 0;
        if(!string.IsNullOrEmpty(Request.Form["payment_amount"]))
            Amount = decimal.Parse(Request.Form["payment_amount"]);
        BookingPayment_bookings cBookingPayment = new BookingPayment_bookings();
        BookingPaymentDisplay cBookingPaymentDisplay = new BookingPaymentDisplay();
        try
        {
            int intBookingId = int.Parse(this.qBookingId);
            int result = cBookingPayment.InsertNewBookingPayment(intBookingId, Title,2, bytPaymenyCat, Amount);
            //cBookingPaymentDisplay.InsertBookingPaymentBank(result);
            if (result > 0)
                 IsInsertCom = "true";
            return IsInsertCom;
        }
        catch(Exception ex)
        {
            return IsInsertCom = ex.Message;
        }
        

    }

    
   
}