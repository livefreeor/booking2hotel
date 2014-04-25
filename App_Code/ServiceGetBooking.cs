using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Hotels2thailand.Booking;
using Hotels2thailand;
using System.Configuration;

/// <summary>
/// Summary description for ServiceGetBooking
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class ServiceGetBooking : System.Web.Services.WebService {

    public ServiceGetBooking () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string SayHello(string firstName, string lastName)
    {
        return "Hello " + firstName + " " + lastName;
    }
    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    //[System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    public string GetBookingDetail()
    {
        BookingdetailDisplay cBooking = new BookingdetailDisplay();
        return "HELLO";
        //return HttpContext.Current.Request.UrlReferrer.ToString();
        //return Hotels2JSON.HotelsToJSON(cBooking.GetBookingDetailListByBookingId(intBookingId));
    }
}
