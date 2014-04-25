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
using Hotels2thailand.Suppliers;
using Hotels2thailand;


public partial class ajax_booking_product_list : System.Web.UI.Page
{
    public string qBookingId
    {
        get
        {
            return Request.QueryString["bid"];
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
        BookingProductList cBookingProduct = new BookingProductList();
        SupplierContactPhoneEmail cSupplierContact = new SupplierContactPhoneEmail();
        Status cStatus = new Status();
        int count = 1;
        foreach (BookingProductList pItem in cBookingProduct.getProductListShowFirstByBookingId(int.Parse(this.qBookingId)))
        {
            string strDateCheckIn = "N/A";
            string strDateCheckOut = "N/A";

            string strTimeCheckInRequest = "N/A";
            string strTimeCheckInConfirm = "N/A";
            string strPaymentBy = "no data";

            string PhoneFaxAccount = cSupplierContact.GetstringContact(pItem.BookingSupplier, "2", "3");

            //-- Email Account ssc.department_id In( 2) 
            string EmailAcc = cSupplierContact.GetstringContactEmail(pItem.BookingSupplier, "2");
            string[] arrEmail = EmailAcc.Split(',');
            int isOk = 0;
            foreach (string mail in EmailAcc.Split(','))
            {
                if (mail.IndexOf('@') > -1)
                {
                    isOk = isOk + 1;
                }
            }
            if (EmailAcc != "N/A" && arrEmail.Count() == isOk)
            {
                strPaymentBy = "Email";
            }
            else
            {
                if (PhoneFaxAccount != "N/A")
                {
                    strPaymentBy = "Fax";
                }
            }
            

            if (pItem.DateTimeCheckIn.HasValue)
            {
                DateTime dDateCheckIn = (DateTime)pItem.DateTimeCheckIn;
                strDateCheckIn = dDateCheckIn.ToString("ddd, MMM dd, yyyy");

                strTimeCheckInRequest = dDateCheckIn.ToString("HH:mm");
            }

            if (pItem.DateTimeCheckOut != null)
            {
                DateTime dDateCheckOut = (DateTime)pItem.DateTimeCheckOut;
                strDateCheckOut = dDateCheckOut.ToString("ddd, MMM dd, yyyy");
            }

            if (pItem.DateTimeConfirmCheckIn.HasValue)
            {
                DateTime dDateConfirm = (DateTime)pItem.DateTimeConfirmCheckIn;
                strTimeCheckInConfirm = dDateConfirm.ToString("HH:mm"); 
            }


            if (pItem.Status)
                result.Append("<div class=\"booking_product_item\"  id=\"" + pItem.BookingProductId + "\">");
            else
                result.Append("<div class=\"booking_product_item\" style=\"background-color:#edeff4;filter:alpha(opacity=30);opacity:0.3;\" id=\"" + pItem.BookingProductId + "\" >");

            result.Append("<input type=\"hidden\" id=\"hd_" + pItem.BookingProductId + "\" value=\"" + pItem.Status + "\" />");

            result.Append("<div class=\"booking_product_item_head\">");
            result.Append("<p><span>" + count + ".&nbsp;[" + pItem.ProductCode + "] : </span>" + pItem.ProductTitle + "</p>");
            result.Append("</div>");

            // Confirm Box -- Table Design==========================================================
            result.Append("<div class=\"booking_product_item_status_main\" id=\"booking_product_item_status_main_" + pItem.BookingProductId + "\">");

                    // Drop Booking Product Status -- DropDownList==========================================================
                    result.Append("<div class=\"booking_product_item_status\" >");

                    result.Append("<select id=\"status_process_" + pItem.BookingProductId + "\" name=\"status_process_" + pItem.BookingProductId + "\" class=\"drop_booking_style\" style=\"width:230px\"  >");
                    foreach (KeyValuePair<string, string> sitem in cStatus.GetStatusByCatId(3))
                    {
                        if (sitem.Key == pItem.StatusId.ToString())
                            result.Append("<option value=\"" + sitem.Key + "\" selected=\"selected\">" + sitem.Value + "</option>");
                        else
                            result.Append("<option value=\"" + sitem.Key + "\">" + sitem.Value + "</option>");

                    }
                    result.Append("</select>");
                    result.Append("<input type=\"button\" onclick=\"UpdateStatus('" + pItem.BookingProductId + "','product');return false;\"  value=\"Go\" class=\"btStyle\" style=\"width:50px;\" />");
                    result.Append("</div>");
                    //=====================================================================================

                    // Print Manu -- Div Design==========================================================
                    result.Append("<div class=\"booking_product_id_print_pan\"><span>Print : </span>");
                    result.Append("<a href=\"print_booking_display.aspx?bpid=" + pItem.BookingProductId + "&bid=" + this.qBookingId + "\" target=\"_blank\">Booking Print&Mail</a>|");
                    result.Append("<a href=\"voucher_admin_preview.aspx?bpid=" + pItem.BookingProductId + "\" target=\"_blank\" >Voucher</a>|");
                    result.Append("<a href=\"print_booking_slip.aspx?bpid=" + pItem.BookingProductId + "\" target=\"_blank\">Slip Payment</a>|");

                    result.Append("<a href=\"Javascript:popup('trackBooking.aspx?bookingID="+this.qBookingId+"', 800, 1000);\" >Booking Track</a>|");
                    result.Append("</div>");
                    //=====================================================================================

            result.Append("<div style=\"clear:both\"></div>");
            result.Append("</div>");
            //=====================================================================================


            // Confirm Box -- Table Design==========================================================
            result.Append("<div class=\"booking_product_confirm_pan\" id=\"booking_product_confirm_pan_" + pItem.BookingProductId + "\">");
            result.Append("<div class=\"booking_product_information\">");
            result.Append("<p class=\"date_check\"><span>Check in time :</span> " + strDateCheckIn + "&nbsp;&nbsp;<span>Check out time :</span> " + strDateCheckOut + "</p>");
            result.Append("<p class=\"adult\"><span>Adult: </span>" + pItem.NumAdult + "<span> Child:</span> " + pItem.NumChild + "<span> Golfer: </span>" + pItem.NunGolf + "</p>");
            result.Append("</div>");
            result.Append("<div style=\"clear:both\"></div>");

            result.Append("<table  class=\"tbl_booking_product_list_confirm\" id=\"tbl_booking_product_list_confirm_" + pItem.BookingProductId + "\" cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#e8bb06\" border=\"0\" style=\"text-align:center;\">");
            
            result.Append("<tr style=\"background-color:#ffcc01;color:#6d5908;font-weight:bold;height:10px;line-height:10px;\">");
                if (pItem.ProductCategory == 39 || pItem.ProductCategory == 40 || pItem.ProductCategory == 32)
                    result.Append("<td width=\"10%\"><img src=\"../../images/flag.png\" />&nbsp;Request time:</td><td width=\"10%\"><img src=\"../../images/flag.png\" />&nbsp;Confirm time:</td>");

                result.Append("<td width=\"10%\">Avaibility :</td><td width=\"10%\">Fax Confirmed :</td><td width=\"10%\"d>Confirm Check In :</td><td width=\"10%\">Payment : <label>[" + strPaymentBy + "]</label></td><td width=\"10%\">Receipt Check : </td>");
            result.Append("</tr>");
            result.Append("<tr  style=\"background-color:#ffffff; height:25px;\">");
            if (pItem.ProductCategory == 39 || pItem.ProductCategory == 40 || pItem.ProductCategory == 32)
            {
                result.Append("<td>" + strTimeCheckInRequest + "</td>");
                result.Append("<td>" + PicStatusNameConfirmTime(pItem.DateTimeConfirmCheckIn, pItem.BookingProductId) + "</td>");
            }

            result.Append("<td>" + PicStatusNameConfirm(pItem.ConfirmAvailable, 8, pItem.BookingProductId) + "</td>");
            result.Append("<td>" + PicStatusNameConfirm(pItem.ConfirmFax, 9, pItem.BookingProductId) + "</td>");
            result.Append("<td>" + PicStatusNameConfirm(pItem.ConfirmCheckIn, 11, pItem.BookingProductId) + "</td>");
            result.Append("<td>" + PicStatusNameConfirm(pItem.ConfirmPaymentSupplier, 10, pItem.BookingProductId) + "</td>");
            result.Append("<td>" + PicStatusNameConfirm(pItem.ConfirmReceiveReciept, 3, pItem.BookingProductId) + "</td>");
            result.Append("</tr>");
            result.Append("</table>");
            result.Append("</div>");
            //=====================================================================================
            result.Append("<div style=\"clear:both\"></div>");

            // PricrTotal -- Tag P Design==========================================================
            result.Append("<p style=\"margin:5px 0px 0px;\" class=\"product_item_total\" id=\"product_item_total_" + pItem.BookingProductId + "\">Product Item Total : <span class=\"product_item_total_1\">" + pItem.TotalPriceSales.Hotels2Currency() + "&nbsp;<span class=\"product_item_total_2\">[&nbsp;" + pItem.TotalPriceSupplier.Hotels2Currency() + "&nbsp;]</span>&nbsp;</span></p>");
            //=====================================================================================


            // Main Menu Manage -- Tag P Design==========================================================
            result.Append("<p class=\"menu_booking_manage\">");
            result.Append("<a href=\"\" onclick=\"ProductEdit('" + pItem.BookingProductId + "');return false;\">Edit</a> ");
            
            
            
            result.Append("</p>");

            //=====================================================================================
            result.Append("<div style=\"clear:both\"></div>");

            // BookingProduct Item 'Blank : Wait for Ajax inClude' -- Tag P Design==========================================================
            if (pItem.Status)
                result.Append("<div id=\"booking_product_detail_" + pItem.BookingProductId + "\" class=\"booking_product_detail\" style=\"display:block;\">");
            else
                result.Append("<div id=\"booking_product_detail_" + pItem.BookingProductId + "\" class=\"booking_product_detail\" style=\"display:none;\">");

                    // BookingProduct Item 'Blank : Wait for Ajax inClude' -- Tag P Design==========================================================
                    result.Append("<div id=\"booking_product_detail_item_" + pItem.BookingProductId + "\" class=\"booking_product_detail_item\"></div>");

                    

                    // BookingProduct Note Tag P Design==========================================================
                    result.Append("<div class=\"booking_product_detail_SA\">");
                    result.Append("<div class=\"booking_product_detail_notetAndsupplier\">");
                    // BookingProduct Note Tag P Design==========================================================
                    result.Append("<div id=\"booking_product_detail_note_" + pItem.BookingProductId + "\" class=\"booking_product_detail_note\"></div>");
                    // BookingProduct Supplier Tag P Design==========================================================
                    result.Append("<div id=\"booking_product_detail_supplier_" + pItem.BookingProductId + "\" class=\"booking_product_detail_supplier\"></div>");
                    result.Append("</div>");
                    result.Append("<div id=\"booking_product_detail_activity_" + pItem.BookingProductId + "\" class=\"booking_product_detail_activity\"></div>");
                    result.Append("<div style=\"clear:both;\"></div>");
                    result.Append("</div>");
                    //=====================================================================================

            result.Append("<div style=\"clear:both;\"></div>");
            result.Append("</div>");
            //=====================================================================================


            // Menu Button Manage Close Edit ' -- Div P Design==========================================================

            //result.Append("<div style=\"clear:both;\"></div>");
            //result.Append("<p class=\"cloasPan\" ><a href=\"\" onclick=\"closbookingProductDetail('" + pItem.BookingProductId + "');return false;\">Hide</a></p>");

           //to disable
            int actionType = 0;
           if (pItem.Status)
               result.Append("<p><a href=\"\" onclick=\"DarkmanPopUpComfirm(400,'Would You Like To Close This Product Now!!??' ,'closeOpenProduct(" + pItem.BookingProductId + "," + actionType +");');return false;\">[Close Product]</a></p>");
           else
           {
               //to enable
               actionType = 1;
               result.Append("<p><a href=\"\" onclick=\"DarkmanPopUpComfirm(400,'Would You Like To Open This Product Now!!??' ,'closeOpenProduct(" + pItem.BookingProductId + "," + actionType + ");');return false;\">[Open Product]</a></p>");
               result.Append("<p class=\"cloasPan\" ><a href=\"\" id=\"hide_" + pItem.BookingProductId + "\" onclick=\"closbookingProductDetail('" + pItem.BookingProductId + "');return false;\">Show</a><a href=\"\" id=\"show_" + pItem.BookingProductId + "\" style=\"display:none;\" onclick=\"OpenbookingProductDetail('" + pItem.BookingProductId + "');return false;\">hide</a></p>");
           }
            //result.Append(SwicthStatus(pItem.Status));
            result.Append("</div>");
            //=====================================================================================
            count = count + 1;
        }
        return result.ToString();
    }
    public string PicStatusNameConfirmTime(DateTime? DateConfirmTime, int intBookingProductId)
    {
        string imageName = string.Empty;
        if (DateConfirmTime.HasValue)
        {
            //<img src=\"../../images/refresh.png\" onclick=\"DarkmanPopUpComfirm(400,'Would You Like To Swicth back Now!!??' ,'confirmswitchbackPayment(" + intPaymentId + "," + paymentType + ");');return false;\"  style=\"cursor:pointer;\"
            DateTime dDate = (DateTime)DateConfirmTime;
            imageName = "<img src=\"../../images/true.png\"/></br>" + dDate.ToString("HH:mm") + "<img src=\"../../images/refresh.png\" onclick=\"DarkmanPopUpComfirm(400,'Would You Like To Swicth back Now!!?' ,'ConfirmTimeSwitchBack(" + intBookingProductId + ")');return false;\" style=\"cursor:pointer;\" />";
        }

        else
            imageName = "<img src=\"../../images/false.png\"/></br><a href=\"\" onclick=\"BookingConfirmTime('" + intBookingProductId + "');return false;\">Confirm Now</a>";

        return imageName;
    }
    public string PicStatusNameConfirm(DateTime? DateConfirm, byte ConfirmCat, int intBookingProductId)
    {
        string imageName = string.Empty;
        if (DateConfirm.HasValue)
        {
            DateTime dDate = (DateTime)DateConfirm;
            //imageName = "<img src=\"../../images/true.png\"/></br>" + dDate.ToString("ddd, MMM dd, yyyy ; hh:mm tt") + "<img src=\"../../images/refresh.png\" onclick=\"confirmswitchback('" + intBookingProductId + "');return false;\" style=\"cursor:pointer;\" />";
            imageName = "<img src=\"../../images/true.png\"/></br>" + dDate.ToString("ddd, MMM dd, yyyy ; HH:mm") + "<img src=\"../../images/refresh.png\" onclick=\"DarkmanPopUpComfirm(400,'Would You Like To Swicth back Now!!?' ,'ConfirmSwitchBackBooking(" + ConfirmCat + "," + intBookingProductId + ")');return false;\" style=\"cursor:pointer;\" />";
        }
            
        else
            imageName = "<img src=\"../../images/false.png\"/></br><a href=\"\" onclick=\"DarkmanPopUpComfirm(400,'Would You Like To Confirm Now!!??' ,'BookingConfirm(" + ConfirmCat + "," + intBookingProductId + ")');return false;\">Confirm Now</a>";
        
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