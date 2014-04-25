using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Hotels2thailand.Production;
using Hotels2thailand.Front;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;

public partial class map_hotel : System.Web.UI.Page
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder result = new StringBuilder();


        Product cProduct = new Product();
       cProduct = cProduct.GetProductById(int.Parse(Request.QueryString["pid"]));




       result.Append("<div id=\"mapContent\" style=\"display:none\">");
       result.Append("<table class=\"mapContent\" cellpadding=\"5\" width=\"380\">");
       result.Append("<tr>");
       result.Append("<td><img src=\"/pictures/hotels/bangkok/sathorn/best_western_plus_grand_howard_thumb_45_40_1.jpg\"/></td>");
       result.Append("<td colspan=\"3\" style=\"border-bottom:1px solid #f2f2f2\"><h1><a href=\"bangkok-hotels/best-western-plus-grand-howard.asp\">BEST WESTERN PLUS Grand Howard</a></h1></td></tr>");


       result.Append("</table>");
       result.Append("</div>");


       result.Append("<div id=\"map_canvas\" style=\"width:650px; height:400px\"></div>");
       result.Append("<script language=\"javascript\" type=\"text/javascript\">generateMap(13.702077, 100.51611)</script>");

       result.Append("<div class=\"map_buttom\"></div>");

       Response.Write("$(\"#map_block_main\").html('" +result.ToString() +"')");
       //Response.Write(result.ToString());

       Response.End();
    }
}