using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Booking;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;
using System.Collections;

namespace Hotels2thailand.UI
{
    public partial class ajax_booking_ack_save : Hotels2BasePageExtra_Ajax
    {
        public string qBookingProductId
        {
            get { return Request.QueryString["bpid"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {
                if (this.IsStaffEdit())
                {
                    
                    Response.Write(Acksave());
                    
                }
                else
                {
                    Response.Write("method_invalid");
                }
                Response.End();

            }
            
        }

        public string Acksave()
        {
            string result = "False";
            try
            {
                BookingAcknowledge cBookingAck = new BookingAcknowledge();
                cBookingAck.AcknowledgeID = Request.Form["txtAcknowId"];
                cBookingAck.BookingID = int.Parse(Request.Form["hd_bookingid"]);
                cBookingAck.StaffID = this.CurrentStaffId;
                cBookingAck.StatusExtranetID = byte.Parse(Request.Form["hd_status_Extra_id"]);

                //Response.Write(Request.Form["txtAcknowId"] + "//" + Request.Form["hd_bookingid"] + "// " + this.CurrentStaffId + "//" + Request.Form["hd_status_Extra_id"] + "<br/>");
                //Response.Write(cBookingAck.AcknowledgeID + "//" + cBookingAck.BookingID + "// " + cBookingAck.StaffID + "//" + cBookingAck.StatusExtranetID);
                //Response.End();

                int insert = cBookingAck.InsertByStaff(cBookingAck);
                if (insert > 0)
                    result = "True";
                else
                    result = "not";
                
            }
            catch (Exception ex)
            {
                Response.Write("error: " + ex.Message);
                Response.End();
            }

            return result;
        }
        
    }
}