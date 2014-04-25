using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Booking;
using Hotels2thailand;


public partial class ajax_booking_product_editesave : System.Web.UI.Page
{
    
    public string qBookingProductId
    {
        get
        {
            return Request.QueryString["bpid"];
        }
    }

    public string qBookingId
    {
        get { return Request.QueryString["bid"]; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            Response.Write(getBookingPayMentInsert());
            Response.Flush();
        }
    }
    public bool getBookingPayMentInsert()
    {
        
        DateTime dDateStart = Request.Form["hd_txtDateStart"].Hotels2DateSplitYear("-");
        DateTime dDateEnd = Request.Form["hd_txtDateEnd"].Hotels2DateSplitYear("-");

        byte bytAdult = byte.Parse(Request.Form["num_adult"]);
        byte bytChild = byte.Parse(Request.Form["num_child"]);
        byte bytGolf = byte.Parse(Request.Form["num_golf"]);
        
        BookingProductDisplay cBookingProductDis = new BookingProductDisplay();
        bool result = cBookingProductDis.UpdateBookingProductByBookingProductId(int.Parse(this.qBookingId),int.Parse(this.qBookingProductId), dDateStart, dDateEnd, bytAdult, bytChild, bytGolf);
        return result;
        

    }

    
   
}