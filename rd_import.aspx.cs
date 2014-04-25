using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Booking;

public partial class rd_import : System.Web.UI.Page
{



    public int HotelID {
        get {
            return  int.Parse(txthotelId.Text.Trim());
        }
    }

    public int ProductId
    {
        get
        {
            return int.Parse(txtProductId.Text.Trim());
        }
    }

    public short SupplierID
    {
        get
        {
            return short.Parse(txtSupplier.Text.Trim());
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void btntblCustomer_Onclick (object sender, EventArgs e)
    {
        BookingImport cBookingim = new BookingImport();


        cBookingim.ImportCustomer(this.HotelID, this.ProductId);
    }
    protected void btnBooking_Click(object sender, EventArgs e)
    {
        BookingImport cBookingim = new BookingImport();
        cBookingim.ImportBooking(this.HotelID, this.ProductId, this.SupplierID);
        
    }

    public void btnReview_Onclick(object sender, EventArgs e)
    {
        BookingImport cBookingim = new BookingImport();
        cBookingim.InportREview(this.HotelID, this.ProductId);
    }
    protected void btnRecheck_Click(object sender, EventArgs e)
    {
        BookingImport cBookingim = new BookingImport();
        cBookingim.ReCheckPaymet(this.HotelID, this.ProductId);
    }
}