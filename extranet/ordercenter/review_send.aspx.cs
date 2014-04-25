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
    public partial class admin_review_send : Hotels2BasePageExtra
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
                string linereplace = "<tr><td style=\"height:25px;\"></td></tr>";
                editor.Content = cMailBooking.getMailReview().Replace(linereplace," ");
                txtMailTO.Text = cMailBooking.GetEmailBooking();
                //txtBcc.Text = "sent@hotels2thailand.com;sent2@hotels2thailand.com";
                txtSubject.Text = cMailBooking.GetSubject(MailCat.Review);

               // txtBcc.Visible = false;
            }
        }

        //protected void ContentChanged(object sender, EventArgs e)
        //{
            
        //}

        public void submit_click(object sender, EventArgs e)
        {
            int intBookingId = int.Parse(this.strBookingId);
            BookingMailEngine cMailBooking = new BookingMailEngine(intBookingId);
            string VoucherBody = editor.Content.Replace("http://174.36.32.56", "http://www.hotels2thailand.com");
            VoucherBody = VoucherBody.Replace("http://174.36.32.32", "http://www.hotels2thailand.com");

            string Bcc = cMailBooking.Bcc;

            
            bool Issent = Hotels2MAilSender.SendmailBooking(cMailBooking.cProductBookingEngine.Email, cMailBooking.MailNameDisplayDefault, txtMailTO.Text, txtSubject.Text, "", VoucherBody);

            //Response.Write(Issent);
            //Response.End();
            Issent = Hotels2MAilSender.SendmailBooking(cMailBooking.cProductBookingEngine.Email, cMailBooking.MailNameDisplayDefault, Bcc, txtSubject.Text, "", VoucherBody);

            
            BookingdetailDisplay cBooking = new BookingdetailDisplay();
            BookingActivityDisplay cBookingactivity = new BookingActivityDisplay();

            
            if (Issent)
            {
                cBookingactivity.InsertAutoActivity(BookingActivityType.CloseBookingAndreview, intBookingId);
                cBooking.UpdateBookingstatus(intBookingId, true);

                ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>DarkmanPopUpComfirm('450','Mail was sent and closed booking successfully.<br/> would like to close this window now !?','window.close();if (window.opener && !window.opener.closed) {window.opener.location.reload();}');</script>", false);
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>DarkmanPopUpAlert('450','Send fail !!<br/> Please Contact R&D Team!!');</script>", false);
               // Sent.Text = "incompleted!! Please Contact R&D ";
            }
        }

        

    }
}