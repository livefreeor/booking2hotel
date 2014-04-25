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


public partial class ajax_booking_product_list_detail_note : System.Web.UI.Page
{
    public string qBookingProductId
    {
        get
        {
            return Request.QueryString["bpid"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            Response.Write(getBookingProductList());
            Response.Flush();
        }
    }

    public string getBookingProductList()
    {
        StringBuilder result = new StringBuilder();
        BookingItemDisplay cBookingItem = new BookingItemDisplay();
        string[] Products = cBookingItem.getBookingProductNoteByBookingProductId(int.Parse(this.qBookingProductId));
        result.Append("<form id=\"Product_note_form\" action=\"\" >");
        result.Append("<h4><img   src=\"../../images/content.png\" /> Product Note</h4>");
        result.Append("<p class=\"contentheadedetail\">List Supplier of This Product, you can Change or Add Supplier to List</p><br />");
        result.Append("<div class=\"Product_note_" + this.qBookingProductId + "\">");
        result.Append("<textarea  rows=\"5\" name=\"product_note_" + this.qBookingProductId + "\" class=\"TextBox_Extra_normal_small\" style=\"width:460px;\" >" + Products[1] + "</textarea>");
        result.Append("<input type=\"button\" value=\"Save\" onclick=\"SaveProductNote('" + this.qBookingProductId + "','" + Products[0] + "');return false;\"  class=\"btStyleGreen\" style=\"width:50px;display:block;margin:5px 0px 0px 0px;\"  />");
        
        result.Append("</div>");
        result.Append("</form>");
        return result.ToString();
    }

   
}