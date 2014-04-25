using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
public partial class admin_report_recommendReport : System.Web.UI.Page
{
    public bool checkDateTimeBetween(DateTime dateStart, DateTime dateEnd, DateTime dateCheck)
    {
        bool result = false;
        if (dateCheck.CompareTo(dateStart) >= 0 && dateCheck.CompareTo(dateEnd) <= 0)
        {
            result = true;
        }
        return result;
    }

    public string getQueryString(string queryName)
    {
        string result = queryName.Split('=')[1];
        return result;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime dateStart = Utility.ConvertDateInput(Request.QueryString["date_in"]);
        DateTime dateEnd = Utility.ConvertDateInput(Request.QueryString["date_out"]);

        byte qtyProduct = byte.Parse(Request.QueryString["qty"]);
        short destination = short.Parse(Request.QueryString["dest"]);
        short location = 0;
        string keyword = string.Empty;
        if(!string.IsNullOrEmpty(Request.QueryString["k"]))
        {
            keyword = Request.QueryString["k"];
        }
        if (!string.IsNullOrEmpty(Request.QueryString["loc"]) && destination != 0)
        {
            location = short.Parse(Request.QueryString["loc"]);
        }

        FrontProductAvailable test = new FrontProductAvailable(destination, location, qtyProduct, dateStart, dateEnd, 100, 10000);
        test.Keyword = keyword;
        List<FrontProductAvailable> results = test.LoadProductList();

        if(Request.QueryString["hasRoom"]=="1")
        {
            results = results.Where(x => x.HasAllotment).OrderBy(x => x.Title).ThenBy(x=>x.Priority).ThenBy(x => x.Price).ToList();
        }else{
            results = results.OrderBy(x => x.Title).ThenBy(x => x.Priority).ThenBy(x => x.Price).ToList();
        }
        string productDisplay = string.Empty;
        productDisplay = productDisplay + "<div style=\"text-align:left\"><table class=\"tblListResult\" cellpadding=\"0\" cellspacing=\"2\" align=\"center\">";
        productDisplay = productDisplay + "<tr><th>Room type</th><th>Avai.</th><th>Condition</th><th></th><th>Display Rate</th><th>Selling Rate</th><th>Hotel Rate</th><th>Grand Total</th></tr>";
        int OptionTemp = 0;
        int ProductTemp = 0;
        int RowSpan = 0;
        int intNight = dateEnd.Subtract(dateStart).Days;

        string ico_available = "";
        decimal priceDisplay = 0;
        decimal priceSeiling = 0;
        decimal priceNet = 0;
        decimal grandPrice = 0;
        string ico_product_type = "";

        foreach (FrontProductAvailable item in results)
        {
            priceDisplay = item.Price / intNight;
            grandPrice = (decimal)((double)item.Price * 1.177)*qtyProduct;
            priceSeiling = grandPrice / intNight/qtyProduct;
            ico_product_type = "";

            if (item.IsExtranet)
            {
                priceNet = (decimal)((double)item.NetPrice * (1 - ((double)item.Comission / 100)));
                priceNet = priceNet / intNight;
            }
            else
            {
                priceNet = item.NetPrice / intNight;
            }

            if (item.ProductID != ProductTemp)
            {
                if(item.IsExtranet)
                {
                    ico_product_type = ico_product_type + "<img src=\"/images/ico_customers.png\" title=\"Extranet\"/>";
                }
                if (item.IsBookNow>0)
                {
                    ico_product_type = ico_product_type + "<img src=\"/images/ico_clock.png\" title=\"Book now pay later\"/>";
                }
                productDisplay = productDisplay.Replace("###rowSpan###", RowSpan.ToString());
                productDisplay = productDisplay + "<tr><td colspan=\"7\" class=\"productTitle\">&bull; <strong><a href=\"redirectProduct.aspx?pid="+item.ProductID+"\" target=\"_blank\">" + item.Title + "</a></strong></td><td class=\"productTitle\" align=\"right\">" + ico_product_type + "</td></tr>";
            }
            if (item.OptionID != OptionTemp)
            {
                if (item.IsExtranet)
                {
                    ico_available = "<img src=\"/images/available.png\"/>";
                }
                else
                {
                    ico_available = "";
                }
                productDisplay = productDisplay.Replace("###rowSpan###", RowSpan.ToString());
                productDisplay = productDisplay + "<tr><td rowspan=\"###rowSpan###\">" + item.OptionTitle + "</td><td rowspan=\"###rowSpan###\" align=\"center\">" + ico_available + "</td><td>" + item.PolicyDisplay + "</td><td align=\"center\"><img src=\"/images/global_ico.png\"/></td><td align=\"center\">" + String.Format("{0:#,0}", priceDisplay) + "</td><td align=\"center\">" + String.Format("{0:#,0}", priceSeiling) + "</td><td align=\"center\">" + String.Format("{0:#,0}", priceNet) + "</td><td align=\"right\"><span class=\"priceGrandSeiling\">" + String.Format("{0:#,0}", grandPrice) + "</span><br/><span class=\"priceGrandNet\">" + String.Format("{0:#,0}", (priceNet * intNight * qtyProduct)) + "</span></td></tr>";
                RowSpan = 1;
            }
            else
            {
                productDisplay = productDisplay + "<tr><td>" + item.PolicyDisplay + "</td><td align=\"center\"><img src=\"/images/global_ico.png\"/></td><td align=\"center\">" + String.Format("{0:#,0}", priceDisplay) + "</td><td align=\"center\">" + String.Format("{0:#,0}", priceSeiling) + "</td><td align=\"center\">" + String.Format("{0:#,0}", priceNet) + "</td><td align=\"right\"><span class=\"priceGrandSeiling\">" + String.Format("{0:#,0}", grandPrice) + "</span><br/><span class=\"priceGrandNet\">" + String.Format("{0:#,0}", (priceNet * intNight * qtyProduct)) + "</span></td></tr>";
                RowSpan = RowSpan + 1;
            }
            OptionTemp = item.OptionID;
            ProductTemp = item.ProductID;
        }
        productDisplay = productDisplay.Replace("###rowSpan###", RowSpan.ToString());
        productDisplay = productDisplay + "</table></div>";
        Response.Write(productDisplay);
    }
}