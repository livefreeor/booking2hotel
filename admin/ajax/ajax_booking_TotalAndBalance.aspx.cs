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


public partial class ajax_booking_TotalAndBalance : System.Web.UI.Page
{
    public string qBookingId
    {
        get
        {
            return Request.QueryString["bid"];
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            Response.Write(GenTotalBalance());
            Response.Flush();
        }
    }

    public string GenTotalBalance()
    {
        BookingTotalAndBalance cTotal = new BookingTotalAndBalance();
        StringBuilder result = new StringBuilder();
        cTotal =  cTotal.CalcullatePriceTotalByBookingId(int.Parse(this.qBookingId));
        decimal Balance = cTotal.getbalanceByBookingId(int.Parse(this.qBookingId));
        result.Append("<p class=\"main_total\">Grand Total : <span class=\"booking_total_1\">" + cTotal.SumPrice.Hotels2Currency() + "&nbsp;<span class=\"booking_total_2\">[&nbsp;" + cTotal.SumPriceSupplier.Hotels2Currency() + "&nbsp;]</span>&nbsp;</span></p>");
        result.Append("<p class=\"main_paid\">Paid : <span class=\"booking_total_1\">" + GrandPaidTotal() + "&nbsp;</p>");
        if (Balance < 0)
            result.Append("<p class=\"Blance\" style=\"background-color:#ef2d2d;border:1px solid #ae0909;\">");
        else
            result.Append("<p class=\"Blance\" style=\"background-color:#72ac58;border:1px solid #2c5115;\">");
        result.Append("Balance : &nbsp;(&nbsp;" + Balance.Hotels2Currency() + "&nbsp;)&nbsp;");
        result.Append("</p>");
        return result.ToString();
    }

    protected string GrandPaidTotal()
    {
        BookingTotalAndBalance cBookingtotalPrice = new BookingTotalAndBalance();
        BookingdetailDisplay cBookingDetailDisplay = new BookingdetailDisplay();
        string result = "0";
        decimal Total = cBookingtotalPrice.GetPriceTotalPaidByBookingId(int.Parse(this.qBookingId));
        result = Total.Hotels2Currency();
        //result = cBookingDetailDisplay.GetPriceTotalByBookingId(this.BookingId).Hotels2Currency();

        return result.ToString();
    }
    protected string GrandRequestTotal()
    {
        BookingTotalAndBalance cBookingtotalPrice = new BookingTotalAndBalance();
        BookingdetailDisplay cBookingDetailDisplay = new BookingdetailDisplay();
        string result = "0";
        decimal Total = cBookingtotalPrice.getbalanceByBookingId(int.Parse(this.qBookingId)) * -1;
        result = Total.Hotels2Currency();
        //result = cBookingDetailDisplay.GetPriceTotalByBookingId(this.BookingId).Hotels2Currency();

        return result.ToString();
    }
}