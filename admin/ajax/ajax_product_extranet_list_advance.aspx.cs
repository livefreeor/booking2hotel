using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.Suppliers;
using Hotels2thailand;

public partial class ajax_product_extranet_list_advance : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.IsPostBack)
        {
            //Response.Write("HHH");
            //Response.End();
            Response.Write(SupplierAndProductExtranetList());
            Response.End();
            
        }
    }



    public string SupplierAndProductExtranetList()
    {
        short shrDestinationId = short.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropDestination"]);
        byte bytStatusProcess = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropStatusProcess"]);
        string strKeyword = Request.Form["ctl00$ContentPlaceHolder1$txtSearch"];
        ProductListAdmin cProductList = new ProductListAdmin();
        IList<object> iProductList = cProductList.getProductListAdVance(29,strKeyword);

        
        StringBuilder result = new StringBuilder();


        result.Append("<table cellpadding=\"0\" cellspacing=\"1\" width=\"100%\" style=\"background-color:#d8dfea;\" >");
        result.Append("<tr style=\"background-color:#3f5d9d;height:25px;color:#ffffff;text-align:center;\"><th width=\"5%\">Gateway</th><th width=\"5%\">No.</th><th width=\"10%\">Code</th><th width=\"50%\">Hotel Name</th><th width=\"10%\">Status</th><th width=\"10%\">Manage</th></tr>");

           
            //result.Append("<img src=\"http://www.booking2hotels.com/images/supplier.png\" />"+sup.SupplierTitle + "</td></tr>");
            string Rowcolor = "";
            int count = 0;

            if (iProductList.Count > 0)
            {
                foreach (ProductListAdmin product in iProductList)
                {
                    count = count + 1;
                    Rowcolor = "#eceff5";
                    if (count % 2 == 0)
                        Rowcolor = "#ffffff";


                    string UrlExtranet = "/extranet/mainextra.aspx?pid=" + product.ProductId + "&supid=" + product.SupplierId;
                    string UrlManage = "/admin/extranet/extranetManage.aspx?pid=" + product.ProductId + "&supid=" + product.SupplierId;
                    string UrlProductManage = "/admin/product/product.aspx?pid=" + product.ProductId + "&supid=" + product.SupplierId + "&pdcid=29";

                    result.Append("<tr id=\"product_row_" + product.ProductId + "\"  title=\"" + product.ProductId + "\" class=\"product_row\"  onmouseover=\"changein(this);\" onmouseout=\"changeout(this);\" style=\"height:25px;background-color:" + Rowcolor + ";\">");
                    result.Append("<td align=\"center\">" + Hotels2String.GetImgBank(product.GateWayId, product.ManageID, product.BookingType) + "</td>");
                    result.Append("<td align=\"center\">" + count + "</td>");
                    result.Append("<td align=\"center\"><a href=\"" + UrlProductManage + "\" target=\"_Blank\" >" + product.ProductCode + "</a></td>");
                    result.Append("<td style=\"text-align:left;\">&nbsp;&nbsp;<a href=\"" + product.WebsiteName + "\" target=\"_Blank\" >" + product.Producttitle + "</a></td>");
                    result.Append("<td align=\"center\" style=\"font-weight:bold;color:#9b0b0b;\">" + product.StatusTitle + "</td>");



                    result.Append("<td align=\"center\"><a href=\"" + UrlExtranet + "\" target=\"_Blank\" >Manage</a></td>");


                    result.Append("</tr>");

                }
            }
            else
            {
                result.Append("<tr><td colspan=\"6\">No result</td></tr>");
            }
         
        
       

        result.Append("");
        result.Append("</table>");


        return result.ToString();
    }


    
    
}