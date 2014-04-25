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

public partial class ajax_booking_confirm : System.Web.UI.Page
{
   
    public string qBookingId
    {
        get
        {
            return Request.QueryString["bid"];
        }
    }

    public string qBookingProductId
    {
        get
        {
            return Request.QueryString["bpid"];
        }
    }

    public string qConfirmCat
    {
        get
        {
            return Request.QueryString["conc"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(this.qBookingId))
            {
                try
                {
                    BookingConfirmEngine cConfirm = new BookingConfirmEngine();
                    BookingActivityDisplay cBookingActivity = new BookingActivityDisplay();
                    //Response.Write(Request.Url.ToString());
                    if (!string.IsNullOrEmpty(this.qBookingProductId))
                    {
                        Response.Write(cConfirm.UpdateConfirmByCat(int.Parse(this.qBookingId), int.Parse(this.qBookingProductId), byte.Parse(this.qConfirmCat)));

                        if (this.qConfirmCat == "10")
                            cBookingActivity.InsertAutoActivity(BookingActivityType.ConfirmPaymentSup, int.Parse(this.qBookingId));
                    }
                    else
                    {
                        Response.Write(cConfirm.UpdateConfirmByCat(int.Parse(this.qBookingId), 0, byte.Parse(this.qConfirmCat)));
                        //auto activity

                        
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message + "<br/>" + ex.StackTrace);
                }
                Response.End();
            }
        }
    }
   
    

   
}