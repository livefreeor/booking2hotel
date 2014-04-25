using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Booking;
using Hotels2thailand.Production;
using Hotels2thailand;

public partial class ajax_acc_booking_quick_close : System.Web.UI.Page
{
    public string qBokingStatus
        {
            get
            {
                return Request.QueryString["bs"];
            }
        }
        

        protected void Page_Load(object sender, EventArgs e)
        {
            
            //Isclose = cBooking.UpdateBookingstatus(int.Parse(this.qBookingId));
            if(this.Page.IsPostBack)
            {
                BookingdetailDisplay cBooking = new BookingdetailDisplay();

                string strBookingStatus = this.qBokingStatus;
                
                string strBookingId = Request.Form["booking_list_checked_" + strBookingStatus];
                
                if (!string.IsNullOrEmpty(strBookingId))
                {
                    //cStatus.UpdateBookingStatus(225875, 71);
                    bool result = false;

                    foreach (string bookingid in strBookingId.Split(','))
                    {

                        result = cBooking.UpdateBookingstatus(int.Parse(bookingid));
                    }

                    Response.Write(result);


                }
                else
                {
                    Response.Write("nosel");
                }

                Response.End();

            }
            //Response.Write(BookingSum(this.qBokingStatus, this.qBokingProductStatus, this.qBokingListype));
            //Response.End();

        }


       
}