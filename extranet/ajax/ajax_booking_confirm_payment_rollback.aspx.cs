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

public partial class ajax_booking_confirm_payment_rollback : System.Web.UI.Page
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

    public string qPaymentType
    {
        get
        {
            return Request.QueryString["pType"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(this.qBookingId))
            {
                int intBookingid = int.Parse(this.qBookingId);
                BookingPayment_bookings cConfirmPayment = new BookingPayment_bookings();
                BookingActivityDisplay cBookingActivity = new BookingActivityDisplay();
                //Response.Write(Request.Url.ToString());
                if (this.qPaymentType == "1")
                {
                    Response.Write(cConfirmPayment.UpdateSwitchBackConfirmPayment(int.Parse(this.qBookingId), int.Parse(this.qBookingPaymentId)));
                    //auto activity
                    cBookingActivity.InsertAutoActivity(BookingActivityType.Confirmpayment_rollback, intBookingid, qBookingPaymentId);
                }
                if (this.qPaymentType == "2")
                {
                    Response.Write(cConfirmPayment.UpdateSwitchBackConfirmSettle(int.Parse(this.qBookingId), int.Parse(this.qBookingPaymentId)));
                }
                Response.End();
            }
        }
    }
   
    

   
}