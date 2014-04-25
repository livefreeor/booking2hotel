using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand;
using Hotels2thailand.Booking;

public partial class admin_booking_TrackBooking : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        BookingTracking track = new BookingTracking();

        string strEntry = "";
        string strFootPrint = "";
        string strOtherVisit = "";
        string strBookingID = Request.QueryString["bookingID"];
        string strEntryID = Request.QueryString["entryID"];

        if (!String.IsNullOrEmpty(strBookingID))
        {
            strEntry = track.TrackEntry(strBookingID, 1);
            strFootPrint = track.TrackFootPrint(strBookingID, 1);
            strOtherVisit = track.TrackOtherVisit(strBookingID, 1);
        }
        else
        {
            strEntry = track.TrackEntry(strEntryID, 2);
            strFootPrint = track.TrackFootPrint(strEntryID, 2);
            strOtherVisit = track.TrackOtherVisit(strEntryID, 2);
        }

        liEntry.Text = strEntry;
        liFootPrint.Text = strFootPrint;
        liOtherVisit.Text = strOtherVisit;

    }
}