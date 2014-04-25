using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Booking;

public partial class test_ffff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write(Hotels2thailand.Hotels2String.getImageGateWay("3,10025753;3,10025780;"));
        Response.End();
        //BookingList cbooking = new BookingList();
        //Response.Write(cbooking.GetBookingListOrderCenter_SumPrice(14, 15)[0]);
        //    Response.End();
    }
}