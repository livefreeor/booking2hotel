using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Booking;



public partial class ajax_booking_payment_editsave : System.Web.UI.Page
{

    public string qBookingPaymentId
    {
        get
        {
            return Request.QueryString["pmid"];
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            Response.Write(getBookingPayMentEdit());
            Response.Flush();
        }
    }
    public bool getBookingPayMentEdit()
    {
        //Response.Write(Request.Form["payment_cat"]);
        //Response.End();
        byte bytPaymenyCat = byte.Parse(Request.Form["payment_cat"]);
        string Title = Request.Form["payment_title"];
        //byte bytGateWay = byte.Parse(Request.Form["payment_gateway"]);
        //string strComment = Request.Form["payment_comment"];
        decimal Amount = 0;
        if(!string.IsNullOrEmpty(Request.Form["payment_amount"]))
            Amount = decimal.Parse(Request.Form["payment_amount"]);
        bool bolStatus = bool.Parse(Request.Form["payment_status"]);

        int intBookingPaymentID = int.Parse(this.qBookingPaymentId);
        BookingPayment_bookings cBookingPayment = new BookingPayment_bookings();
       

        int intBookingPaymentId = int.Parse(this.qBookingPaymentId);


        bool result = cBookingPayment.UpdateBookingPaymentExtra(intBookingPaymentId, Title, bytPaymenyCat, Amount, "", bolStatus);

        


        return result;
        
    }

    
   
}