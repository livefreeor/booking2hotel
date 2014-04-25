using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Hotels2thailand.Booking;
using Hotels2thailand;

/// <summary>
/// Summary description for servicetest
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class servicetest : System.Web.Services.WebService {

    public servicetest () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public bool AccountLogin(string username, string pass)
    {
        if (username == "admin" && pass == "pass")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [WebMethod]
    public string GetBookingDetail(int intBookingId)
    {
        BookingdetailDisplay cBooking = new BookingdetailDisplay();
        return Hotels2JSON.HotelsToJSON(cBooking.GetBookingDetailListByBookingId(intBookingId));
    }
}
