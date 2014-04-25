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

public partial class ajax_booking_product_status : System.Web.UI.Page
{
    public string qBookingId
    {
        get
        {
            return Request.QueryString["bid"];
        }
    }
    public string qBookingProductId
    {
        get
        {
            return Request.QueryString["bpid"];
        }
    }

    public string qmethod
    {
        get
        {
            return Request.QueryString["method"];
        }
    }

   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(this.qBookingProductId))
            {
                BookingProductDisplay cBookingProduct = new BookingProductDisplay();

                Response.Write(this.qBookingId + "--" + this.qBookingProductId);
                try
                {
                    if (this.qmethod == "1")
                    {
                        Response.Write(cBookingProduct.UpdateBookingProductStatusByBookingProductId(int.Parse(this.qBookingId), int.Parse(this.qBookingProductId), true));
                    }
                    if (this.qmethod == "0")
                    {
                        Response.Write(cBookingProduct.UpdateBookingProductStatusByBookingProductId(int.Parse(this.qBookingId), int.Parse(this.qBookingProductId), false));
                    }
                }catch(Exception ex)
                {
                    Response.Write("error: " + ex.Message);
                    Response.End();
                }
                Response.End();
            }
        }
    }
   
    

   
}