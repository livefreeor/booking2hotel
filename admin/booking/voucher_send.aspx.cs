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
    public partial class admin_voucher_send : Hotels2BasePage
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

                string mailcontent = string.Empty;
                BookingdetailDisplay cBookingDetail = new BookingdetailDisplay();
                if (cBookingDetail.GetBookingDetailListByBookingId(int.Parse(this.strBookingId)).PaymentTypeID == 2)
                    mailcontent = cMailBooking.getMailSendVoucher_Booknow();
                else
                    mailcontent = cMailBooking.getMailSendVoucher();
                
                editor.Content = mailcontent;

                txtMailTO.Text = cMailBooking.GetEmailBooking();
                txtBcc.Text = cMailBooking.Bcc;
                txtSubject.Text = cMailBooking.GetSubject(MailCat.ComfirmBooking);
                
            }
        }


        public void submit_click(object sender, EventArgs e)
        {
            int intBookingId = int.Parse(this.strBookingId);

            BookingMailEngine cMailBooking = new BookingMailEngine(intBookingId);
            string VoucherBody = editor.Content.Replace("http://174.36.32.56", "http://www.hotels2thailand.com");
            VoucherBody = VoucherBody.Replace("http://174.36.32.32", "http://www.hotels2thailand.com");

            string Bcc = txtBcc.Text;
            BookingStaff cBookingStaff = new BookingStaff();
            string hotelStaff = string.Empty;
            foreach (string mail in cBookingStaff.GetStringEmailSendMail(intBookingId))
            {
                hotelStaff = hotelStaff + mail + ";";
            }

            string staffEmail = hotelStaff.Trim();

           

            if (Request.QueryString["pid"] == "3605")
                Bcc = Bcc + ";bht-reservation@hotels2thailand.com";

            

            bool Issent = Hotels2MAilSender.SendmailBooking(cMailBooking.cProductBookingEngine.Email, cMailBooking.MailNameDisplayDefault, txtMailTO.Text, txtSubject.Text, Bcc, VoucherBody);

            if (String.IsNullOrEmpty(Request.QueryString["resnd"]))
            {

                string StrMailStaff = string.Empty;
                string strProduct_id = Request.QueryString["pid"];
                if (strProduct_id == "3605" || strProduct_id == "3692")
                {
                    BookingdetailDisplay cBookingDetail = new BookingdetailDisplay();
                    if (!string.IsNullOrEmpty(staffEmail))
                    {

                        StrMailStaff = cMailBooking.getMailSendVoucher("supplier_price_show");
                        Issent = Hotels2MAilSender.SendmailBooking(cMailBooking.cProductBookingEngine.Email, cMailBooking.MailNameDisplayDefault, staffEmail, txtSubject.Text, "", StrMailStaff);
                    }
                } 
                else
                {

                    if (!string.IsNullOrEmpty(staffEmail))
                    {
                        Issent = Hotels2MAilSender.SendmailBooking(cMailBooking.cProductBookingEngine.Email, cMailBooking.MailNameDisplayDefault, staffEmail, txtSubject.Text, "", VoucherBody);
                    }
                }
            }

            

           
            //Issent = Hotels2MAilSender.SendmailBooking(BookingMailEngine.MailNameDisplayDefault, txtBcc.Text, txtSubject.Text, "peerapong@hotels2thailand.com", BookingMailEngine.removeEmailTrack(VoucherBody));

            if (Issent)
            {
                
                ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>DarkmanPopUpComfirm('450','Mail was sent successfully.<br/> would like to close this window now !?','SentConpleted();');</script>", false);
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>DarkmanPopUpAlert('450','Send fail !!<br/> Please Contact R&D Team!!');</script>", false);
            }
        }

        

    }
}