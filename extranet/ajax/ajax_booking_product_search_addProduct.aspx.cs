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


public partial class ajax_booking_product_search_addProduct : System.Web.UI.Page
{
    public string qProductCatId
    {
        get
        {
            return Request.QueryString["cid"];
        }
    }
    public string qDestinationId
    {
        get
        {
            return Request.QueryString["desId"];
        }
    }
    public string qKeyWordSearch
    {
        get
        {
            return Request.QueryString["Key"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            
            if (!string.IsNullOrEmpty(this.qKeyWordSearch))
            {
                byte intProductCatId = byte.Parse(this.qProductCatId);
                int intDesId = Convert.ToInt32(this.qDestinationId);

                ProductListAdmin cProductList = new ProductListAdmin();
                IList<object> Result = cProductList.getProductListAdVance(intProductCatId, this.qKeyWordSearch);
                StringBuilder HotelResult = new StringBuilder();
                HotelResult.Append("<div>");
                HotelResult.Append("");
                foreach (ProductListAdmin hotelList in Result)
                {
                    HotelResult.Append("<p><input type=\"radio\" name=\"pid\" onclick=\"ChangProductCat('" + hotelList .ProductCatId + "');\" value=\"" + hotelList.ProductId + "\">" + hotelList.Producttitle + "</p>");
                }
                HotelResult.Append("</div>");

                Response.Write(HotelResult.ToString());
                Response.End();
            }
            
        }
    }

   

    
    
}