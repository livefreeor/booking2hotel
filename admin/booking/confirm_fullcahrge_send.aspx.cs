using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Hotels2thailand.Booking;

namespace Hotels2thailand.UI
{
    public partial class admin_confirm_fullcahrge_send : Hotels2BasePage
    {
        //booking_id
        public string strBookingId
        {
            get
            {
                return Request.QueryString["bid"];
            }
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                BookingMailEngine cMailBooking = new BookingMailEngine(int.Parse(this.strBookingId));

                
                BookingdetailDisplay cBookingDetail = new BookingdetailDisplay();

                editor.Content = cMailBooking.getMailSend_Booknow_fullCharge_Completed();

                txtMailTO.Text = cMailBooking.GetEmailBooking();
                txtBcc.Text = "sent@hotels2thailand.com;sent2@hotels2thailand.com";
                txtSubject.Text = "Reservation from hotels2thailand.com (ORDER ID:" + this.strBookingId + ") 100% Successful Payment is CONFIRMED";
                
            }
        }

        //protected void ContentChanged(object sender, EventArgs e)
        //{
            
        //}

        public void submit_click(object sender, EventArgs e)
        {
            string VoucherBody = editor.Content.Replace("http://174.36.32.56", "http://www.hotels2thailand.com");
            VoucherBody = VoucherBody.Replace("http://174.36.32.32", "http://www.hotels2thailand.com");

            bool Issent = Hotels2MAilSender.SendmailBooking("reservation@hotels2thailand.com", txtMailTO.Text, txtSubject.Text, txtBcc.Text, VoucherBody);
            if (Issent)
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>DarkmanPopUpComfirm('450','Mail was sent successfully.<br/> would like to close this window now !?','window.close();');</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>DarkmanPopUpAlert('450','Send fail !!<br/> Please Contact R&D Team!!');</script>", false);
            }
        }

        

    }
}