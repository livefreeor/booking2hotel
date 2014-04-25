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
using Hotels2thailand;
using Hotels2thailand.Staffs;

namespace Hotels2thailand.UI
{
    public partial class ajax_booking_Acknowledge_ResProcess : System.Web.UI.Page
    {
        public string qBookingId
        {
            get
            {
                return Request.QueryString["bid"];
            }
        }

        public string qProcess
        {
            get { return Request.QueryString["prc"]; }
        }

        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }


        //public short Current_StaffID
        //{
        //    get
        //    {
        //        Hotels2thailand.UI.Hotels2BasePage cBasePage = new Hotels2thailand.UI.Hotels2BasePage();
        //        return cBasePage.CurrentStaffId;
        //    }
        //}

        //public Staff Current_Staffobj
        //{
        //    get
        //    {
        //        Hotels2thailand.UI.Hotels2BasePage cBasePage = new Hotels2thailand.UI.Hotels2BasePage();
        //        return cBasePage.CurrentStaffobj;
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                
                if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qBookingId) && !string.IsNullOrEmpty(this.qProcess))
                {
                    Response.Write(UpdatetblBookingAcknowLedge());
                    
                    Response.End();
                }
                

            }
           
        }


        public string UpdatetblBookingAcknowLedge()
        {
            string result = "False";
            try
            {
                int intBookingId = int.Parse(this.qBookingId);
                byte intProcesstatus = byte.Parse(this.qProcess);
                int intProductId = int.Parse(this.qProductId);

                BookingdetailDisplay cbookingDetail = new BookingdetailDisplay();
                cbookingDetail.UpdateBookingStatusExtranet(intBookingId, intProcesstatus);

                BookingAcknowledge objBooking = new BookingAcknowledge();
                objBooking.BookingID = intBookingId;
                objBooking.StatusExtranetID = intProcesstatus;
                objBooking.Insert(objBooking);

                MailAcknowledge mailStaff = new MailAcknowledge();
                mailStaff.SendMailToHotelStaff(intProductId, intBookingId, intProcesstatus);

                result = "True";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.End();
            }

            return result;
        }

    }
}