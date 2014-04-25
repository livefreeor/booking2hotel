using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand;
using Hotels2thailand.Booking;

public partial class test_testmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void btnGen_Onclick(object sender, EventArgs e)
    {
        int BookingId = int.Parse(txtBookingId.Text);
        BookingMailEngine cBookingMail = new BookingMailEngine(BookingId);

        switch (radMailType.SelectedValue)
        {
            case "1":
                lblREsult.Text = cBookingMail.getMailBookingRecieved();
                break;
            case "2":
                lblREsult.Text = cBookingMail.getMailBookingRecieved_hotel_notice();
                break;
            case "3":
                lblREsult.Text = cBookingMail.getMailResubmit(int.Parse(txtPayment.Text));
                break;
            case "4":
                lblREsult.Text = cBookingMail.getMailBookingRecieved_notice_offline_charge("jhhoihoihuyfrsstgpojpopoki9u8y87gokll");
                break;
            case "5":
                lblREsult.Text = cBookingMail.getMailBookingRecieved_Allot();
                break;
            case "6":
                lblREsult.Text = cBookingMail.Member_mail_resetPass(3449, 70243);
                break;
            case "8":
                lblREsult.Text = cBookingMail.getMailSendVoucher();
                break;
            case "9":
                lblREsult.Text = cBookingMail.getMailSendVoucher("supplier_price_show");
                break;
            case "10":
                lblREsult.Text = cBookingMail.getMailReview();
                break;
            //case "7":
            //    lblREsult.Text = cBookingMail.Member_mail_manaul_active(3449, 70243);
            //    break;
        }
        

    }
    protected void btnStaffMail_Click(object sender, EventArgs e)
    {
        BookingStaff cBookingStaff = new BookingStaff();
        int BookingId = int.Parse(txtBookingId.Text);
        string result = string.Empty;
        foreach (string arr in cBookingStaff.GetStringEmailSendMail(BookingId))
        {
            result = result + arr.ToString() + "<br/>";
        }
        lblREsult.Text = result;
    }
}