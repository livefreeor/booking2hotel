using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hotels2thailand;
using Hotels2thailand.Booking;
using System.Web.Script.Serialization;
using System.Text;
using Newtonsoft.Json;



public partial class test_json : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //FrontPromotionList cFron = new FrontPromotionList();
        BookingList cBookingList= new BookingList();
        IList<object> iList = cBookingList.GetBookingListOrderCenter(68, 10, 1, 1, "date_submit", 3449);
        //IList<object> iListPro = cFron.GetPromotionList(3449, "110.171.168.232", 1);
        //JsonSerializer serializer = new JsonSerializer();
       //JavaScriptSerializer serializer = new JavaScriptSerializer();
        //string body = JsonConvert.SerializeObject(iListPro, Formatting.Indented);
       //var converted = serializer.Deserialize<IList<FrontPromotionList>>(serializer.Serialize(iListPro));


       //IList<FrontPromotionList> people =
       // new JavaScriptSerializer().Deserialize<IList<FrontPromotionList>>(serializer.Serialize(iListPro));
      
        //Response.Write(iListPro.Count());

        Response.Write(iList.HotelsToJSON());
        Response.End();
    }

    
}