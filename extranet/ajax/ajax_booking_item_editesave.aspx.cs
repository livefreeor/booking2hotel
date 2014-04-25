using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Booking;
using Hotels2thailand.Affiliate;
using Hotels2thailand;


public partial class ajax_booking_item_editesave : System.Web.UI.Page
{
    
    public string qBookingItemId
    {
        get
        {
            return Request.QueryString["biid"];
        }
    }
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
            Response.Write(getBookingItemEditSave());
            Response.Flush();
        }
    }

    public bool getBookingItemEditSave()
    {
        
        decimal decprice = decimal.Parse(Request.Form["item_price"]);
        decimal decpriceSup = decimal.Parse(Request.Form["item_priceSup"]);
        byte bytUnit = byte.Parse(Request.Form["item_unit"]);
        bool ItemStatus = bool.Parse(Request.Form["Item_status"]);
        

        BookingItemDisplay cBookingItemDis = new BookingItemDisplay();
        
        bool result = cBookingItemDis.UpdateBookingItem(int.Parse(this.qBookingItemId), decprice, decpriceSup, bytUnit, ItemStatus);
        AffiliateCalculate objAff = new AffiliateCalculate(int.Parse(this.qBookingId));
        objAff.ReCalculateCommission();

        return result;
        

    }

    
   
}