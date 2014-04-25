using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Booking;

namespace Hotels2thailand.UI
{
    public partial class vGenerator_acknowledge_pcs : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {
                if (this.IsStaffAdd())
                {
                   
                    string bookingSelect = Request.Form["booking_id"];
                    string[] arrBooking = bookingSelect.Split(',');
                   
                    string booking_acknowledge_id = string.Empty;
                    BookingAcknowledge objBooking = new BookingAcknowledge();
                    for (int countBook = 0; countBook < arrBooking.Count(); countBook++)
                    {
                        
                        objBooking.BookingID = int.Parse(arrBooking[countBook]);
                        objBooking.AcknowledgeID = Request.Form["ack_" + arrBooking[countBook]];
                        objBooking.StatusExtranetID = byte.Parse(Request.Form["status_ack"]);
                        objBooking.StaffID = this.CurrentStaffId;
                        objBooking.Insert(objBooking);
                    }

                    Response.Write("complete");
                }
                else
                {
                    Response.Write("method_invalid");
                }
                Response.End();

            }
            
        }
    }
}