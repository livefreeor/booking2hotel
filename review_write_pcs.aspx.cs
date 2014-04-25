using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Reviews;
using Hotels2thailand.Production;

public partial class review_write_pcs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int? cusID = null;
        //if(!string.IsNullOrEmpty(Request.Form["cus_id"]))
        //{
        //    cusID = int.Parse(Request.Form["cus_id"]);
        //}

        int ProductID = int.Parse(Request.QueryString["pid"]);
        //Response.Write(Request.Form["product"]);
        //Response.End();
        //byte CountryID=byte.Parse(Request.Form["country_id"]);
        //byte Recommend=byte.Parse(Request.Form["review_recommend"]);
        //byte ReviewFrom=byte.Parse(Request.Form["review_from"]);
        //byte Category = byte.Parse(Request.QueryString["category"]);
        string ReviewTitle = Request.QueryString["review_title"];
        string ReviewDetail = Request.QueryString["review_detail"];
        string Positive = "";
        string Negative = "";
        string CustomerName = Request.QueryString["cus_name"];
        string CustomerFrom = Request.QueryString["cus_from"];


       int ret = ReviewManage.HotelREviewInsert(
                  ProductID,
                  cusID,
                  null,
                  1,
                  1,
                  ReviewTitle,
                  ReviewDetail,
                  Positive,
                  Negative,
                  CustomerName,
                  CustomerFrom,
                  byte.Parse(Request.QueryString["rate_overall"]),
                  byte.Parse(Request.QueryString["rate_service"]),
                  byte.Parse(Request.QueryString["rate_location"]),
                  byte.Parse(Request.QueryString["rate_rooms"]),
                  byte.Parse(Request.QueryString["rate_cleanliness"]),
                  byte.Parse(Request.QueryString["rate_value_for_money"])
                  );
       StringBuilder result = new StringBuilder();
       if (ret == 1)
       {
           Product cProduct = new Product();
           cProduct = cProduct.GetProductById(ProductID);
           result.Append("<div id=\"review_thanks\">");
           result.Append("<h1>Write review for " + cProduct.Title+ " Complete</h1>");
           result.Append("<p>Thank you. We have received your review. In general, reviews will appear within 2-4 business days. However, reviews that violate our Travel Review Guidelines may be removed or rejected without notice, as determined by Hotels2Thailand.com in its sole discretion.</p>");
           result.Append("");
           result.Append("");
           result.Append("");
           result.Append("</div>");
       }
       else
       {
       }


       Response.Write("$('#review_block_main').html('" + result.ToString() + "');");
        Response.End();

        


        
    }
}