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


public partial class ajax_booking_product_list_detail_noteSave : System.Web.UI.Page
{
    public string qBookingProductId
    {
        get
        {
            return Request.QueryString["bpid"];
        }
    }
    public string qProductId
    {
        get
        {
            return Request.QueryString["pid"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            Response.Write(getBookingProductCommentSave());
            Response.Flush();
        }
    }

    public bool getBookingProductCommentSave()
    {
        
        Product cProduct = new Product();
        BookingItemDisplay cBookingItem = new BookingItemDisplay();
        string Comment = Request.Form["product_note_" + this.qBookingProductId];

        bool result = cProduct.UpdateProductComment(int.Parse(this.qProductId), Comment);
        
        return result;
    }

   
}