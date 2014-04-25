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

namespace Hotels2thailand.UI
{
    public partial class ajax_booking_closeAndActivity_Save : System.Web.UI.Page
    {

        public string qBookingId
        {
            get
            {
                return Request.QueryString["bid"];
            }
        }
        
        

        public short Current_StaffID
        {
            get
            {
                Hotels2thailand.UI.Hotels2BasePage cBasePage = new Hotels2thailand.UI.Hotels2BasePage();
                return cBasePage.CurrentStaffId;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Response.Write(getBookingActivityInsert());
                Response.Flush();
            }
        }

        public bool getBookingActivityInsert()
        {
            int ret  = 0;
            bool Isclose = false;
            bool Iscompleted = false;
            BookingActivityDisplay cActivity = new BookingActivityDisplay();
            BookingdetailDisplay cBooking = new BookingdetailDisplay();
            string strDeail = Request.Form["activity_detail"];
            
            
            ret =  cActivity.InsertNewActivityBooking(int.Parse(this.qBookingId), strDeail);

            Isclose = cBooking.UpdateBookingstatus(int.Parse(this.qBookingId));

            if (ret == 1 && Isclose)
                Iscompleted = true;
            return Iscompleted;
        }



    }
}