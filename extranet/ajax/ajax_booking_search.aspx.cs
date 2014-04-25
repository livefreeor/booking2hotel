using System;
using System.Text;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hotels2thailand.Booking;

namespace Hotels2thailand.UI
{
    public partial class ajax_booking_search : Hotels2BasePageExtra_Ajax
    {

        public string qSearchType
        {
            get
            {
                return Request.QueryString["sty"];
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                try
                {
                    Response.Write(Search());
                    Response.Flush();
                }
                catch(Exception ex)
                {
                    Response.Write(ex.Message + "<Br/>" + ex.StackTrace);
                    Response.Flush();
                }
                
            }
           
        }


        public string Search()
        {

            BookingSearch_extranet cBookingSearch = new BookingSearch_extranet();
            List<object> ListResult = null;
            int intProductId = this.CurrentProductActiveExtra;
            switch (int.Parse(this.qSearchType))
            {
                case 1:
                    ListResult = cBookingSearch.SearchResultbookingName(Request.Form["txtname_search"],intProductId);
                    break;
                case 2:
                    break;
                case 3:
                    ListResult = cBookingSearch.SearchResultbookingEmail(Request.Form["txtemail_search"], intProductId);
                    break;
                case 4:
                    ListResult = cBookingSearch.SearchResultProductAddress(Request.Form["txtAddress_search"], intProductId);
                    break;
                case 5:
                    ListResult = cBookingSearch.SearchResultProductName(Request.Form["txtproduct_search"], intProductId);
                    break;
                case 6:
                    ListResult = cBookingSearch.SearchResultbookingPayment(Request.Form["txtpayment_search"], intProductId);
                    break;
                case 7:
                    ListResult = cBookingSearch.SearchResultBookingId(Request.Form["txtbooking_id_search"], intProductId);
                    break;
                case 8:
                    ListResult = cBookingSearch.SearchResultBookingHotelId(Request.Form["txtBookingHotelId_search"], intProductId);
                    break;
                case 9:
                    ListResult = cBookingSearch.SearchResultBookingDate(Request.Form["hd_date_start"], Request.Form["hd_date_end"], intProductId);
                    break;
                case 10:
                    ListResult = cBookingSearch.SearchResultBookingCheckin(Request.Form["hd_check_in_date_start"], Request.Form["hd_check_in_date_end"], intProductId);
                    break;
                case 11:
                    ListResult = cBookingSearch.SearchResultBookingCheckOut(Request.Form["hd_check_out_date_start"], Request.Form["hd_check_out_date_end"], intProductId);
                    break;
                case 12:
                    ListResult = cBookingSearch.SearchResultBookingHotelINputID(Request.Form["txtBookingHotelId_intput_search"], intProductId);
                    break;


            }

            StringBuilder result = new StringBuilder();
            
            if (ListResult != null && ListResult.Count > 0)
            {
                result.Append("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"2\" class=\"tbl_acknow\">");
                result.Append("<tr class=\"header_field\" style=\"text-align:center;\" >");
                result.Append("<td width=\"5%\">Booking No.</td><td width=\"30%\">Hotel Name</td><td width=\"15%\">Check In/Check Out </td><td width=\"15%\">Full Name</td>");
                result.Append("<td width=\"10%\">Email</td><td width=\"15%\">Booking Date</td><td width=\"10%\">Status</td>");
                result.Append("</tr>");

                foreach (BookingSearch_extranet bookingList in ListResult)
                {
                    string RowColor = "#ffffff;";
                    string strStatus = "Active";
                    string StatusColor = "#46a11a";
                    if (bookingList.Status)
                    {
                        RowColor = "#f1f1f1;";
                        strStatus = "Inactive";
                        StatusColor = "#ff0000;";
                       
                    }
                        
                    result.Append("<tr style=\"background-color:" + RowColor + "\">");
                    result.Append("<td align=\"center\"><a href=\"booking_detail.aspx?bid=" + bookingList.BookingID + this.AppendQueryString + "\" target=\"_blank\">" + bookingList.BookingHotelId + "</a></td>");
                    result.Append("<td>" + bookingList.ProductTitle + "</td>");
                    result.Append("<td align=\"center\"><span style=\"color:#3f5d9d\">" + bookingList.CheckIn.ToString("MMM dd, yyyy") + "</span><br/><span style=\"color:#ffcc33\">" + bookingList.CheckOut.ToString("MMM dd, yyyy") + "</span></td>");
                    result.Append("<td>" + bookingList.BookingName + "</td>");
                    result.Append("<td align=\"center\">" + bookingList.Email + "</td>");
                    //result.Append("<td>" + bookingList.Address + "</td>");
                    result.Append("<td align=\"center\">" + bookingList.DateSubmit.ToString("MMM dd, yyyy") + "</td>");
                    result.Append("<td align=\"center\" ><strong style=\"color:" + StatusColor + "\">" + strStatus + "</strong></td>");
                    result.Append("</tr>");
                }
                result.Append("</table>");
            }
            else
            {
                
                    result.Append("<div  class=\"box_empty\">");
                    result.Append("");
                    result.Append("<p><label>Sorry!</label> There are no result to display.</p>");
                    result.Append("");
                    result.Append("</div>");
                
            }
            
            

            return result.ToString();
        }

    }
}