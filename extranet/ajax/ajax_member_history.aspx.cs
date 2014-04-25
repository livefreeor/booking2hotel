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
    public partial class ajax_member_history : Hotels2BasePageExtra_Ajax
    {

        public string qPage { get { return Request.QueryString["page"]; } }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                try
                {

                    int intMemberId = int.Parse(Request.Form["cus_id"]);
                    if (string.IsNullOrEmpty(this.qPage))
                    {
                        Response.Write(BookingListGen(intMemberId,1));
                    }
                    else
                    {
                        Response.Write(BookingListGen(intMemberId, int.Parse(this.qPage)));
                    }
                    Response.Flush();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message + "--" + ex.StackTrace);
                    Response.End();
                }
                
            }
        }


        public string BookingListGen(int intMemberId, int intPageStart)
        {
            StringBuilder BookingListResult = new StringBuilder();

            //try
            //{
            //decimal douPriceTotal = 0;
            //decimal douPriceSupplierTotal = 0;
            BookingList cBookingList = new BookingList();
            IList<object> ListBookingProductList = cBookingList.GetBookingList_member_history(intPageStart, "date_submit", intMemberId, this.CurrentProductActiveExtra);


            
            BookingListResult.Append("<table class=\"tbl_acknow\" id=\"tbl_booking_list_status_99\" cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" >\r\n");
            BookingListResult.Append("<tr class=\"header_field\">\r\n");
            BookingListResult.Append("<td width=\"6%\">ID</td><td width=\"10%\" >Customer</td><td width=\"27%\" >Product</td><td width=\"12%\">Receive</td>\r\n");
            BookingListResult.Append("<td width=\"13%\">In-Out</td><td width=\"8%\">Price</td>\r\n");
          
            BookingListResult.Append("<td>Booking Status</td>");
            BookingListResult.Append("<td>Status</td>");
            BookingListResult.Append("</tr>\r\n");

            decimal PriceSales = 0;
            
           // decimal PriceTotal = 0;
            foreach (BookingList BpItem in ListBookingProductList)
            {
                

                BookingListResult.Append("<tr style=\"background-color:#ffffff; height:40px;\" class=\"" + BpItem.BookingID + "\" id=\"" + BpItem.BookingProductId + "\">\r\n");

                BookingListResult.Append("<td><a href=\"../ordercenter/booking_detail.aspx?bid=" + BpItem.BookingID + this.AppendQueryString + "\" target=\"_blank\">" + BpItem.BookingHotelID + "</a></td>\r\n");
                BookingListResult.Append("<td style=\"text-align:left;padding:0px 0px 0px 5px; \">" + BpItem.BookingName + "</td>\r\n");
                if (BpItem.SupplierCatId == 1)
                    BookingListResult.Append("<td style=\"text-align:left;padding:0px 0px 0px 5px; \">" + BpItem.ProductTitle + "<br/><span style=\"font-size:11px;color:#ef2d2d;\">[" + BpItem.SupplierTitle + "]</span></td>\r\n");
                else
                    BookingListResult.Append("<td style=\"text-align:left;padding:0px 0px 0px 5px; \">" + BpItem.ProductTitle + "</td>\r\n");

                BookingListResult.Append("<td style=\"font-size:11px;text-align:left;padding:0px 0px 0px 2px;\">" + BpItem.BookingReceive.ToString("ddd,MMM dd, yyyy") + "</td>\r\n");
                if (!string.IsNullOrEmpty(BpItem.PriceSales))
                    PriceSales = decimal.Parse(BpItem.PriceSales.Split(';')[0]);
                //PriceSup = decimal.Parse(BpItem.PriceSales.Split(';')[1]);
                
                BookingListResult.Append("<td style=\"font-size:11px;text-align:left;padding:0px 0px 0px 2px;\"><span style=\"color:#3f5d9d;\">" + BpItem.CheckIn.ToString("ddd,MMM dd, yyyy") + "</span><br/><span style=\"color:#ef2d2d;\">" + BpItem.CheckOut.ToString("ddd,MMM dd, yyyy") + "</span></td>\r\n");
                BookingListResult.Append("<td style=\"color:#3f5d9d;\">" + PriceSales.Hotels2Currency() + "</td>\r\n");


                string strStatus = "";
                string StatusColor = "";
                if (BpItem.BookingStatus)
                {

                    strStatus = "Inactive";
                    StatusColor = "#ff0000;";

                }
                else
                {
                    strStatus = "Active";
                    StatusColor = "#46a11a;";
                }
                BookingListResult.Append("<td>" + BpItem.StatusTitle + "</td>");

                BookingListResult.Append("<td align=\"center\" ><strong style=\"color:" + StatusColor + "\">" + strStatus + "</strong></td>");

                BookingListResult.Append("</tr>\r\n");

                
            }
            
            BookingListResult.Append("</table>\r\n");

            int Total = cBookingList.GetBookingList_member_history_count(intMemberId, this.CurrentProductActiveExtra);

        


            BookingListResult.Append("<table cellpadding=\"0\" cellspacing=\"0\"  style=\"width:100%;\" >");
            BookingListResult.Append("<tr >");
            BookingListResult.Append("<td width=\"60%\" style=\"text-align:left;\"><p class=\"bookingList_Page\">" + getPageList(Total, intPageStart) + "</p></td>");
            // result.Append("<td>" + Total + "</td>");
            //BookingListResult.Append("<td  width=\"40%\" style=\"text-align:right;padding:0px 10px 0px 0px\"><p class=\"bookingList_priceTotal\"><span>Total: </span> " + Price.ToString("#,###.00") + "</p> </td>");
            BookingListResult.Append("</tr>");
            BookingListResult.Append("</table>");


            return BookingListResult.ToString();
        }


        



        
    

    }
}
