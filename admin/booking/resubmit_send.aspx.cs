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
    public partial class admin_resubmit_send : Hotels2BasePage
    {
        //booking_id
        public string strBookingId
        {
            get
            {
                return Request.QueryString["bid"];
            }
        }

        public string qPaymentID
        {
            get
            {
                return Request.QueryString["payid"];
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                

                BookingdetailDisplay cBookingDetail = new BookingdetailDisplay();
                if (cBookingDetail.GetBookingDetailListByBookingId(int.Parse(this.strBookingId)).PaymentTypeID == 2)
                {
                    panelMailOption.Visible = true;
                    panelMail.Visible = false;

                }
                else
                {
                    BookingMailEngine cMailBooking = new BookingMailEngine(int.Parse(this.strBookingId));
                    txtSubject.Text = cMailBooking.GetSubject(MailCat.Resubmit);
                    editor.Content = cMailBooking.getMailResubmit(int.Parse(this.qPaymentID));
                    txtMailTO.Text = cMailBooking.GetEmailBooking();
                    txtBcc.Text = cMailBooking.Bcc;
                }


                
                
                
            }
        }

        
        public void submit_click(object sender, EventArgs e)
        {
            int intBookingId = int.Parse(this.strBookingId);
            BookingMailEngine cMailBooking = new BookingMailEngine(intBookingId);
            string ResubmitBody = editor.Content.Replace("http://174.36.32.56", "http://www.hotels2thailand.com");
            ResubmitBody = ResubmitBody.Replace("http://174.36.32.32", "http://www.hotels2thailand.com");

            string Bcc = txtBcc.Text;


            bool Issent = Hotels2MAilSender.SendmailBooking(cMailBooking.cProductBookingEngine.Email, cMailBooking.MailNameDisplayDefault, txtMailTO.Text, txtSubject.Text, "", ResubmitBody);
            Issent = Hotels2MAilSender.SendmailBooking(cMailBooking.cProductBookingEngine.Email, cMailBooking.MailNameDisplayDefault, Bcc, txtSubject.Text, "", ResubmitBody);

            BookingActivityDisplay cBookingactivity = new BookingActivityDisplay();
            Issent = (cBookingactivity.InsertAutoActivity(BookingActivityType.resubmit, intBookingId, this.qPaymentID) == 1);
            //Issent = Hotels2MAilSender.SendmailBooking(BookingMailEngine.MailNameDisplayDefault, txtBcc.Text, txtSubject.Text, "peerapong@hotels2thailand.com", BookingMailEngine.removeEmailTrack(ResubmitBody));
            if (Issent)
            {
                //ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>window.close();if (window.opener && !window.opener.closed) {window.opener.location.reload();}</script>", false);
                ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>DarkmanPopUpComfirm('450','Mail was sent successfully.<br/> would like to close this window now !?','window.close();');</script>", false);
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>DarkmanPopUpAlert('450','Send fail !!<br/> Please Contact R&D Team!!');</script>", false);
               // Sent.Text = "incompleted!! Please Contact R&D ";
            }
        }

        

        

        protected void submit_booknow_online_click(object sender, EventArgs e)
        {
            BookingMailEngine cMailBooking = new BookingMailEngine(int.Parse(this.strBookingId));

            string strReplace = "<tr><td style=\"height:25px;\"></td></tr>";

            txtSubject.Text = "Hotels2Thailand.com (ORDER ID:" + this.strBookingId + ") : Your credit card is DECLINED.";
            editor.Content = cMailBooking.getMailResubmit_Booknow_online(int.Parse(this.qPaymentID)).Replace(strReplace, " ");
            txtMailTO.Text = cMailBooking.GetEmailBooking();
            txtBcc.Text = "sent@hotels2thailand.com;sent2@hotels2thailand.com";
            panelMailOption.Visible = false;
            panelMail.Visible = true;
        }

        protected void submit_booknow_offline_click(object sender, EventArgs e)
        {
            BookingMailEngine cMailBooking = new BookingMailEngine(int.Parse(this.strBookingId));
            string strReplace = "<tr><td style=\"height:25px;\"></td></tr>";
            txtSubject.Text = "Hotels2Thailand.com (ORDER ID:" + this.strBookingId + ") : Your credit card is DECLINED.";
            editor.Content = cMailBooking.getMailResubmit_Booknow_offline().Replace(strReplace, " "); ;
            txtMailTO.Text = cMailBooking.GetEmailBooking();
            txtBcc.Text = "sent@hotels2thailand.com;sent2@hotels2thailand.com";
            panelMailOption.Visible = false;
            panelMail.Visible = true;
        }
}
}