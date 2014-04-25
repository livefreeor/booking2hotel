using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;


public partial class ajax_report_product_list : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.IsPostBack)
        {
            Response.Write(ProductList());
            Response.End();
        }
    }


    public string ProductList()
    {
        StringBuilder result = new StringBuilder();
        try
        {
            Product_Report cProductReport = new Product_Report();

            byte bytProductCat = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropCat"]);
            short shrDestinationId = short.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropDes"]);

            byte byteReportType = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropReport_type"]);
            byte bytMonth = byte.Parse(Request.Form["selMonth"]);

            int intTotalProduct = 0;
            IList<object> iListProduct = null;
            switch (byteReportType)
            {
                //Product Online-Contract
                case 1:
                    if (shrDestinationId == 0)
                        intTotalProduct = cProductReport.GetCountProductAll_OnlineByProductCat(bytProductCat);
                    else
                        iListProduct = cProductReport.GetProductAll_OnlineByProductCat_Contract(bytProductCat, shrDestinationId, false);
                    break;
                //Product Online-Extranet
                case 2:
                    if (shrDestinationId == 0)
                        iListProduct = cProductReport.GetProductAll_OnlineByProductCat_Extranet(bytProductCat, shrDestinationId, true);
                    else
                        iListProduct = cProductReport.GetProductAll_OnlineByProductCat_Extranet(bytProductCat, shrDestinationId, false);
                    
                    break;
                //Product Whole Sales
                case 3:
                    if (shrDestinationId == 0)
                        iListProduct = cProductReport.GetProductAll_OnlineByProductCat_Contract_WholeSalse(bytProductCat, shrDestinationId, true);  
                    else
                        iListProduct = cProductReport.GetProductAll_OnlineByProductCat_Contract_WholeSalse(bytProductCat, shrDestinationId, false);
                    break;
                //Product Rate Expired-Contract
                case 4:
                    if (shrDestinationId == 0)
                        iListProduct = cProductReport.GetProductAll_OnlineByProductCat_Contract_Expired(bytProductCat, shrDestinationId, true);
                    else
                        iListProduct = cProductReport.GetProductAll_OnlineByProductCat_Contract_Expired(bytProductCat, shrDestinationId, false);
                    break;
                //Product Rate Expired-Extranet
                case 5:
                    if (shrDestinationId == 0)
                        iListProduct = cProductReport.GetProductAll_OnlineByProductCat_Extranet_Expired(bytProductCat, shrDestinationId, true);
                    else
                        iListProduct = cProductReport.GetProductAll_OnlineByProductCat_Extranet_Expired(bytProductCat, shrDestinationId, false);
                    break;
                //New Product of Month
                case 6:
                    if (shrDestinationId == 0)
                        iListProduct = cProductReport.GetProduct_New(bytProductCat, shrDestinationId, bytMonth, true);
                    else
                        iListProduct = cProductReport.GetProduct_New(bytProductCat, shrDestinationId, bytMonth, false);
                    break;
                //Product New Promotion of Month
                case 7:
                    if (shrDestinationId == 0)
                        iListProduct = cProductReport.GetProduct_NewPromotion_contract(bytProductCat, shrDestinationId, bytMonth, true);
                    else
                        iListProduct = cProductReport.GetProduct_NewPromotion_contract(bytProductCat, shrDestinationId, bytMonth, false);
                    break;

            }

            if (shrDestinationId == 0 && byteReportType == 1)
            {
                result.Append("<p class=\"total\">Total Product: " + intTotalProduct + "</p>");
            }
            else
            {
                int Total = iListProduct.Count();
                result.Append("<p class=\"total\">Total Product: " + Total + "</p>");

                result.Append("<div id=\"product_result\">");
                result.Append("<table cellpadding=\"0\" cellspacing=\"2\">");
                result.Append("<tr style=\"background-color:#3f5d9d\"><th style=\"width:5%\">No.</th><th style=\"width:5%\">Code</th><th style=\"width:40%\">Name</th><th style=\"width:40%\">Supplier</th><th style=\"width:10%\">Expired</th></tr>");
                if (Total > 0)
                {
                    int count = 0;
                    foreach (Product_Report product in iListProduct)
                    {
                        string bg = string.Empty;
                        if (count % 2 == 0)
                            bg = "#f2f2f2";
                        else
                            bg = "#ffffff";
                        result.Append("<tr style=\"background-color:"+bg+"\">");
                        result.Append("<td>" + (count + 1) + "</td>");
                        result.Append("<td >" + product.ProductCode + "</td>");
                        result.Append("<td style=\"text-align:left;padding-left:5px;\">" + product.ProductTitle + "</td>");
                        result.Append("<td style=\"text-align:left;padding-left:5px;\">" + product.Suppliertitle + "</td>");
                        if (product.dLastDateRate.HasValue)
                            result.Append("<td>" + ((DateTime)product.dLastDateRate).ToString("MMM dd, yyyy") + "</td>");
                        else
                            result.Append("<td>No rate</td>");

                        result.Append("</tr>");

                        count = count + 1;
                    }
                }
                else
                {
                    result.Append("<tr><td colspan=\"5\">No Record</td></tr>");
                }
                
                result.Append("</table>");
                result.Append("</div>");
            }
           

        }
        catch (Exception ex)
        {
            Response.Write("error: "  + ex.Message);
            Response.End();
        }
        return result.ToString();
    }
    
}