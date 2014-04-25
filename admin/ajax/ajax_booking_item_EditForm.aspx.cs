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
using Hotels2thailand;

namespace Hotels2thailand.UI
{
    public partial class ajax_booking_item_EditForm : System.Web.UI.Page
    {

        public string qBookingItemId
        {
            get
            {
                return Request.QueryString["biid"];
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
                Response.Write(getBookingItemEdit());
                Response.Flush();
            }
        }
        
        public string getBookingItemEdit()
        {
            Hotels2BasePage basePage = new Hotels2BasePage();
            BookingItemDisplay cBookingItemCheck = null;
            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            cBookingItemCheck = cBookingItem.getBookingItemByBookingItemId(int.Parse(this.qBookingItemId));

            if (cBookingItemCheck == null)
                cBookingItem = cBookingItem.getBookingItemByBookingItemId(int.Parse(this.qBookingItemId), 1);
            else
                cBookingItem = cBookingItemCheck;

            //Response.Write("SER");
            //Response.End();
            //else
            //{
            //    Response.Write("SER");
            //    Response.End();
            //}
            
            StringBuilder result = new StringBuilder();

            result.Append("<form id=\"product_item_Edit_Form\" action=\"\" >");

            result.Append("<div class=\"formbox_head\">Booking Product Item Edit</div>");
            result.Append("<div class=\"formbox_body\">");
            result.Append("<p>" + cBookingItem.OptionTitle+ "</p>");
            if (cBookingItem.PromotionID.HasValue && cBookingItem.PromotionCat.HasValue && string.IsNullOrEmpty(cBookingItem.PromotionDetail))
            {
                result.Append(Hotels2XMLContent.Hotels2XMlReaderPomotionDetail(cBookingItem.PromotionDetail));

            }
            result.Append("<table id=\"tbl_payment_list\" cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:left;\">");
            result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Price</td><td>&nbsp;<input type=\"text\" id=\"item_price\"  class=\"TextBox_Extra_normal_small\" value=\"" + cBookingItem.Price.Hotels2Currency() + "\" style=\"width:150px;\" name=\"item_price\" /></td></tr>");
            result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Own</td><td>&nbsp;<input type=\"text\" id=\"item_priceSup\"  class=\"TextBox_Extra_normal_small\" value=\"" + cBookingItem.PriceSupplier.Hotels2Currency() + "\" style=\"width:150px;\" name=\"item_priceSup\" /></td></tr>");
            result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Unit</td><td>&nbsp;");
            result.Append("<select id=\"item_unit\" name=\"item_unit\" class=\"DropDownStyleCustom\" >");

            foreach (KeyValuePair<int, string> num in basePage.dicGetNumberstart0(100))
            {
                if (num.Key == cBookingItem.Unit)
                    result.Append("<option value=\"" + num.Key + "\" selected=\"selected\">" + num.Value + "</option>");
                else
                    result.Append("<option value=\"" + num.Key + "\">" + num.Value + "</option>");

            }
            result.Append("</select>");
            result.Append("</td></tr>");
            result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Close Item</td> ");

            result.Append("<td>");
            if (cBookingItem.Status)
            {
                result.Append("<input type=\"radio\" name=\"Item_status\" value=\"True\" checked=\"checked\" /> Yes <br/>");
                result.Append("<input type=\"radio\" name=\"Item_status\" value=\"False\" /> No");
            }
            else
            {
                result.Append("<input type=\"radio\" name=\"Item_status\" value=\"True\"  /> Yes <br/>");
                result.Append("<input type=\"radio\" name=\"Item_status\" value=\"False\" checked=\"checked\" /> No");
            }
            result.Append("</td></tr>");
            result.Append("</table>");
            result.Append("</div>");
            result.Append("<div class=\"formbox_buttom\"><input type=\"button\" value=\"Save\" onclick=\"EditProductItemSave('" + cBookingItem.BookingItemID + "', '" + cBookingItem.BookingProductID + "');\"  class=\"btStyleGreen\" />&nbsp;<input type=\"button\" value=\"Cancel\" onclick=\"DarkmanPopUp_Close();\" class=\"btStyleWhite\" style=\" width:80px\" /></div>");

            result.Append("</form>");


            return result.ToString();
        }



    }
}