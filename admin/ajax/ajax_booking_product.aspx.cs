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


public partial class ajax_booking_product : System.Web.UI.Page
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
        BookingProductList cBookingProductID = new BookingProductList();
        SupplierContactPhoneEmail cSupplierContact = new SupplierContactPhoneEmail();
        cBookingProductID = cBookingProductID.getProductByBookingProductID(int.Parse(this.qBookingProductId));
        
        string strDateCheckIn = "N/A";
        string strDateCheckOut = "N/A";

        string strTimeCheckInRequest = "N/A";
        string strTimeCheckInConfirm = "N/A";
        string strPaymentBy = "no data";
        string PhoneFaxAccount = cSupplierContact.GetstringContact(cBookingProductID.BookingSupplier, "2", "3");

        //-- Email Account ssc.department_id In( 2) 
        string EmailAcc = cSupplierContact.GetstringContactEmail(cBookingProductID.BookingSupplier, "2");
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
            
        if (cBookingProductID.DateTimeCheckIn.HasValue)
        {
            DateTime dDateCheckIn = (DateTime)cBookingProductID.DateTimeCheckIn;
            strDateCheckIn = dDateCheckIn.ToString("ddd, MMM dd, yyyy");

            strTimeCheckInRequest = dDateCheckIn.ToString("HH:mm");
        }

        if (cBookingProductID.DateTimeCheckOut != null)
        {
            DateTime dDateCheckOut = (DateTime)cBookingProductID.DateTimeCheckOut;
            strDateCheckOut = dDateCheckOut.ToString("ddd, MMM dd, yyyy");
        }

        if (cBookingProductID.DateTimeConfirmCheckIn.HasValue)
        {
            DateTime dDateConfirm = (DateTime)cBookingProductID.DateTimeConfirmCheckIn;
            strTimeCheckInConfirm = dDateConfirm.ToString("HH:mm");
        }

        //result.Append("<div class=\"booking_product_confirm_pan\" id=\"booking_product_confirm_pan_" + cBookingProductID.BookingProductId+ "\">");

        result.Append("<div class=\"booking_product_information\">");
        result.Append("<p class=\"date_check\"><span>Check in time :</span> " + strDateCheckIn + "&nbsp;&nbsp;<span>Check out time :</span> " + strDateCheckOut + "</p>");
        result.Append("<p class=\"adult\"><span>Adult: </span>" + cBookingProductID.NumAdult + "<span> Child:</span> " + cBookingProductID.NumChild + "<span> Golfer: </span>" + cBookingProductID.NunGolf + "</p>");
        result.Append("</div>");
        result.Append("<div style=\"clear:both\"></div>");

        result.Append("<table  class=\"tbl_booking_product_list_confirm\" id=\"tbl_booking_product_list_confirm_" + cBookingProductID.BookingProductId + "\" cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#e8bb06\" border=\"0\" style=\"text-align:center;\">");
        result.Append("<tr style=\"background-color:#ffcc01;color:#6d5908;font-weight:bold;height:10px;line-height:10px;\">");
        if (cBookingProductID.ProductCategory == 39 || cBookingProductID.ProductCategory == 40 || cBookingProductID.ProductCategory == 32)
            result.Append("<td width=\"10%\"><img src=\"../../images/flag.png\" />&nbsp;Request time:</td><td width=\"10%\"><img src=\"../../images/flag.png\" />&nbsp;Confirm time:</td>");

        result.Append("<td width=\"10%\">Availability :</td><td width=\"10%\">Fax Confirmed :</td><td width=\"10%\"d>Confirm Check In :</td><td width=\"10%\">Payment : <label>[" + strPaymentBy + "]</label></td><td width=\"10%\">Receipt Check : </td>");
        result.Append("</tr>");
        result.Append("<tr  style=\"background-color:#ffffff; height:25px;\">");
        if (cBookingProductID.ProductCategory == 39 || cBookingProductID.ProductCategory == 40 || cBookingProductID.ProductCategory == 32)
        {
            result.Append("<td>" + strTimeCheckInRequest + "</td>");
            result.Append("<td>" + PicStatusNameConfirmTime(cBookingProductID.DateTimeConfirmCheckIn, cBookingProductID.BookingProductId) + "</td>");
        }



        result.Append("<td>" + PicStatusNameConfirm(cBookingProductID.ConfirmAvailable, 8, cBookingProductID.BookingProductId) + "</td>");
        result.Append("<td>" + PicStatusNameConfirm(cBookingProductID.ConfirmFax, 9, cBookingProductID.BookingProductId) + "</td>");
        result.Append("<td>" + PicStatusNameConfirm(cBookingProductID.ConfirmCheckIn, 11, cBookingProductID.BookingProductId) + "</td>");
        result.Append("<td>" + PicStatusNameConfirm(cBookingProductID.ConfirmPaymentSupplier, 10, cBookingProductID.BookingProductId) + "</td>");
        result.Append("<td>" + PicStatusNameConfirm(cBookingProductID.ConfirmReceiveReciept, 3, cBookingProductID.BookingProductId) + "</td>");
        result.Append("</tr>");
        result.Append("</table>");


        //result.Append("</div>");
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


    
    
}