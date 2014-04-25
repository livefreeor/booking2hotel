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
using Hotels2thailand.ProductOption;
using Hotels2thailand;


public partial class ajax_booking_product_list_detail : System.Web.UI.Page
{
    public string qBookingProductId
    {
        get
        {
            return Request.QueryString["bpid"];
        }
    }

    public string qBookingLang
    {
        get { return Request.QueryString["lang"]; }
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
        try
        {
            BookingItemDisplay cBookingItem = new BookingItemDisplay();

        int intBookingProductId = int.Parse(this.qBookingProductId);

        BookingProductDisplay cBookingProductDis = new BookingProductDisplay();
        cBookingProductDis = cBookingProductDis.getBookingProductDisplayByBookingProductId(intBookingProductId);
        byte intBookingLang = cBookingProductDis.BookingLang;

        List<object> cBookingItemListDefault = cBookingItem.getBookingItemListByBookingProductId(intBookingProductId, 1);
        List<object> cBookingItemListReal = cBookingItem.getBookingItemListByBookingProductId(intBookingProductId, intBookingLang);

        List<object> cBookingItemList = null;
        if (cBookingItemListDefault.Count == cBookingItemListReal.Count)
            cBookingItemList = cBookingItemListReal;
        else
            cBookingItemList = cBookingItemListDefault;

        

        int count = 1;

        result.Append("<table class=\"tbl_booking_product_list_item_detail\" id=\"tbl_booking_product_list_item_detail_"+this.qBookingProductId+"\" cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center;\">");
        result.Append("<tr style=\"background-color:#3f5d9d;color:#ffffff;font-weight:bold;height:10px;line-height:10px;\"><td>Product Item</td>");
        result.Append("<td>Inside Allotment </td>");
        result.Append("<td>Quantity</td><td>Price</td><td>Own</td></tr>");
        string strbullet = "greenbt.png";

        
        foreach (BookingItemDisplay pItem in cBookingItemList)
        {
            if (pItem.Status)
                result.Append("<tr style=\"background-color:#ffffff; height:25px;\" class=\"enable_display\">");
            else
            {
                strbullet = "greenbt-gray.png";
                result.Append("<tr style=\"background-color:#ebebe4; height:25px;\" class=\"disable_display\">");
            }

            result.Append("<td style=\"text-align:left;padding:0px 0px 0px 4px;font-weight:bold\">");

            result.Append("<img src=\"../../images/" + strbullet + "\" alt=\"bullet\" /><a href=\"\"  onclick=\"UpdateItem('" + pItem.BookingItemID + "','" + pItem.BookingProductID + "', '" + intBookingLang + "');return false;\"  >&nbsp;" + Hotels2String.GetOptionTitle(pItem.OptionTitle, pItem.BookingOptionTitle) + "</a>");


            if (pItem.IsExtraNet && pItem.NumAdult.HasValue && pItem.NumChild.HasValue)
            {

                if (pItem.OptionCAtID == 38)
                {
                    result.Append("<br/>" + pItem.ConditionTitle + Hotels2String.AppendConditionDetailExtraNet((byte)pItem.NumAdult, pItem.BreakfastBookingItem));
                }
                else
                    result.Append("&nbsp;" + Hotels2String.AppendConditionDetailExtraNet((byte)pItem.NumAdult, pItem.BreakfastBookingItem));
                
            }
            else
            {

                if (pItem.OptionCAtID == 52 || pItem.OptionCAtID == 53 || pItem.OptionCAtID == 54 || pItem.OptionCAtID == 43 || pItem.OptionCAtID == 44)
                {
                    result.Append("<br/>" + pItem.ConditionTitle);
                }


                if (pItem.NumAdult.HasValue && pItem.NumChild.HasValue)
                {
                    if (pItem.NumAdult > 0 && pItem.NumChild == 0)
                        result.Append("&nbsp;(For " + pItem.NumAdult + " Adult )&nbsp;");
                    if (pItem.NumChild > 0 && pItem.NumAdult == 0)
                        result.Append("&nbsp;(For " + pItem.NumChild + " Child )&nbsp;");
                    if (pItem.NumChild > 0 && pItem.NumAdult > 0)
                        result.Append("&nbsp;(For " + pItem.NumAdult + " Adult & " + pItem.NumChild + " Children)&nbsp;");
                }
                else
                {
                    if (pItem.Condition_NumAdult > 0 && pItem.Condition_NumChild == 0)
                        result.Append("&nbsp;(For " + pItem.Condition_NumAdult + " Adult )&nbsp;");
                    if (pItem.Condition_NumChild > 0 && pItem.Condition_NumAdult == 0)
                        result.Append("&nbsp;(For " + pItem.Condition_NumChild + " Child)&nbsp;");
                    if (pItem.Condition_NumChild > 0 && pItem.Condition_NumAdult > 0)
                        result.Append("&nbsp;(For " + pItem.Condition_NumChild + " Adult & " + pItem.Condition_NumAdult + "Children)&nbsp;");
                }
                

                if (pItem.OptionCAtID == 38)
                {
                    if (pItem.BreakfastBookingItem > 0)
                        result.Append("&nbsp;<strong>(&nbsp;Breakfast included&nbsp; " + pItem.BreakfastBookingItem + " &nbsp;Pax &nbsp;)</strong>");
                    else
                        result.Append("&nbsp;<strong>(Room Only)</strong>");
                }
            }

            
            result.Append("");

            if (pItem.PromotionID.HasValue && !string.IsNullOrEmpty(pItem.PromotionDetail))
            {
                result.Append(Hotels2XMLContent.Hotels2XMlReaderPomotionDetail(pItem.PromotionDetail));

            }
            

            result.Append("</td>");
            Allotment cAllot = new Allotment();

            if (pItem.StatusAllot)
                result.Append("<td><strong style=\"color:#608000;\">Allotment used</strong></td>");
            else
            {
                if (cAllot.CheckAllotAvaliable(pItem.SupplierId, pItem.OptionID, pItem.Unit, pItem.dDateStart, pItem.dDateEnd))
                    result.Append("<td><strong>Yes</strong></td>");
                else
                    result.Append("<td><strong>No</strong></td>"); 
            }
              

            result.Append("<td>"+ pItem.Unit +"</td>");
            result.Append("<td>" + pItem.Price.Hotels2Currency() + "</td>");
            result.Append("<td>"+pItem.PriceSupplier.Hotels2Currency());

            
            
            result.Append("</td>");
            result.Append("</tr>");
            count = count + 1;
        }
        result.Append("<tr style=\"background-color:#ffffff; height:25px;\"><td colspan=\"5\"><p class=\"product_item_total\" id=\"product_item_total_duplicate_" + this.qBookingProductId + "\"></p></td>");
        result.Append("</table>");
        result.Append("");
        }
        catch (Exception ex)
        {
            Response.Write("error: " + ex.Message);
            Response.End();
        }
        
        return result.ToString();
    }

    public string PicStatusNameConfirm(DateTime? DateConfirm)
    {
        string imageName = string.Empty;
        if (DateConfirm.HasValue)
        {
            DateTime dDate = (DateTime)DateConfirm;
            //imageName = "<img src=\"../../images/true.png\"/></br>" + dDate.ToString("ddd, MMM dd, yyyy ; hh:mm tt") + "<img src=\"../../images/refresh.png\" onclick=\"confirmswitchbackPayment('" + intPaymentId + "');return false;\" style=\"cursor:pointer;\" />";
            imageName = "<img src=\"../../images/true.png\"/></br>" + dDate.ToString("ddd, MMM dd, yyyy ; HH:mm");
        }
            
        else
            imageName = "<img src=\"../../images/false.png\"/></br><a href=\"\">Confirm Now</a>";
        
        return imageName;
    }

    public string SwicthStatus(bool status)
    {
        StringBuilder result = new StringBuilder();
        result.Append("<div style=\"float:right;\">");
        result.Append("<div style=\"margin:0px;padding:0px;width:130px;height:20px;border:1px solid #aeb1b6;background-color:#c9ccd0; font-size:14px;\">");
        if (status)
        {
            result.Append("<p style=\"margin:0px;padding:0px;background-color:#72ac58;color:#ffffff;font-weight:bold;width:90px;height:20px;float:left;line-height:20px;text-align:center;\">Enable</p>");
            result.Append("<p style=\"margin:0px;padding:0px;color:#ffffff;font-weight:bold;width:40px;height:20px;float:left;line-height:20px;text-align:center;\"></p>");
        }
        else
        {
            result.Append("<p style=\"margin:0px;padding:0px;color:#ffffff;font-weight:bold;width:40px;height:20px;float:left;line-height:20px;text-align:center;\"></p>");
            result.Append("<p style=\"margin:0px;padding:0px;background-color:#ef2d2d;color:#ffffff;font-weight:bold;width:90px;height:20px;float:left;line-height:20px;text-align:center;\">Disable</p>");
        }
        
        result.Append("</div>");
        result.Append("</div>");
        return result.ToString();
    }
    
}