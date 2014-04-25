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
    public partial class ajax_booking_history : Hotels2BasePageExtra_Ajax
    {
        public string qBokingId
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
                try
                {
                    string strEmail = string.Empty;
                    string NameFull = string.Empty;
                    BookingdetailDisplay cBooking = new BookingdetailDisplay();
                    if (int.Parse(this.qBokingId) > 200000)
                    {
                        cBooking = cBooking.GetBookingDetailListByBookingId(int.Parse(this.qBokingId));
                        strEmail = cBooking.Email;
                        NameFull = cBooking.FullName;
                    }
                    else
                    {
                        Bookinghistory cBookinghistory = new Bookinghistory();
                        strEmail = cBookinghistory.GetEmailByOrderID(int.Parse(this.qBokingId));

                    }

                    //Response.Write("HELLO");

                    Response.Write(BookingListGen(strEmail, NameFull, int.Parse(this.qBokingId)));
                    //Response.Write(BookingListGen_Old(strEmail, int.Parse(this.qBokingId)));

                    Response.Flush();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message + "--" + ex.StackTrace);
                    Response.End();
                }
                
            }
        }


        public string BookingListGen(string strEmail, string strNameFull, int intBookingId)
        {
            StringBuilder BookingListResult = new StringBuilder();

            //try
            //{
            //decimal douPriceTotal = 0;
            //decimal douPriceSupplierTotal = 0;
            BookingList cBookingList = new BookingList();
            IList<object> ListBookingProductList = cBookingList.GetBookingListHistory(intBookingId, strEmail, strNameFull, 1, 1, "date_submit", this.CurrentProductActiveExtra);


            BookingListResult.Append("<p class=\"BookingStatusProductHead\">:&nbsp;:&nbsp;Booking history &nbsp;:&nbsp;:</p>\r\n");
            BookingListResult.Append("<table class=\"tbl_acknow\" id=\"tbl_booking_list_status_99\" cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" >\r\n");
            BookingListResult.Append("<tr class=\"header_field\">\r\n");
            BookingListResult.Append("<td width=\"6%\">ID</td><td width=\"10%\" >Customer</td><td width=\"27%\" >Product</td><td width=\"12%\">Receive</td>\r\n");
            BookingListResult.Append("<td width=\"13%\">In-Out</td><td width=\"8%\">Price</td>\r\n");
            //BookingListResult.Append("<td width=\"2%\">2</td><td width=\"2%\">3</td><td width=\"2%\">4</td><td width=\"2%\">5</td><td width=\"2%\">6</td><td width=\"2%\">7</td><td width=\"2%\">8</td>\r\n");
            BookingListResult.Append("<td>Booking Tap</td>");
            BookingListResult.Append("<td>Status</td>");
            BookingListResult.Append("</tr>\r\n");

            decimal PriceSales = 0;
            
            //decimal PriceTotal = 0;
            foreach (BookingList BpItem in ListBookingProductList)
            {
                //PriceTotal = 0;
                if (!BpItem.BookingStatus)
                    BookingListResult.Append("<tr style=\"background-color:#ffffff; height:40px;\" class=\"" + BpItem.BookingID + "\" id=\"" + BpItem.BookingProductId + "\">\r\n");
                else
                    BookingListResult.Append("<tr style=\"background-color:#f1f1f1;color:#aeb1b8; height:40px;\" class=\"" + BpItem.BookingID + "\" id=\"" + BpItem.BookingProductId + "\">\r\n");



                BookingListResult.Append("<td><a href=\"booking_detail.aspx?bid=" + BpItem.BookingID + this.AppendQueryString + "\" target=\"_blank\">" + BpItem.BookingHotelID + "</a></td>\r\n");
                BookingListResult.Append("<td style=\"text-align:left;padding:0px 0px 0px 5px; \">" + BpItem.BookingName + "</td>\r\n");
                if (BpItem.SupplierCatId == 1)
                    BookingListResult.Append("<td style=\"text-align:left;padding:0px 0px 0px 5px; \">" + BpItem.ProductTitle + "<br/><span style=\"font-size:11px;color:#ef2d2d;\">[" + BpItem.SupplierTitle + "]</span></td>\r\n");
                else
                    BookingListResult.Append("<td style=\"text-align:left;padding:0px 0px 0px 5px; \">" + BpItem.ProductTitle + "</td>\r\n");

                BookingListResult.Append("<td style=\"font-size:11px;text-align:left;padding:0px 0px 0px 2px;\">" + BpItem.BookingReceive.ToString("ddd,MMM dd, yyyy") + "</td>\r\n");
                if (!string.IsNullOrEmpty(BpItem.PriceSales))
                    PriceSales = decimal.Parse(BpItem.PriceSales.Split(';')[0]);
                //PriceSup = decimal.Parse(BpItem.PriceSales.Split(';')[1]);
                if (BpItem.Status)
                {
                    BookingListResult.Append("<td style=\"font-size:11px;text-align:left;padding:0px 0px 0px 2px;\"><span style=\"color:#3f5d9d;\">" + BpItem.CheckIn.ToString("ddd,MMM dd, yyyy") + "</span><br/><span style=\"color:#ef2d2d;\">" + BpItem.CheckOut.ToString("ddd,MMM dd, yyyy") + "</span></td>\r\n");
                    BookingListResult.Append("<td style=\"color:#3f5d9d;\">" + PriceSales.Hotels2Currency() + "</td>\r\n");
                    //BookingListResult.Append("<td style=\"color:#ef2d2d;\">" + PriceSup.Hotels2Currency() + "</td>\r\n");
                }
                else
                {
                    BookingListResult.Append("<td style=\"font-size:11px;text-align:left;padding:0px 0px 0px 2px;\"><span style=\"color:#aeb1b8;\">" + BpItem.CheckIn.ToString("ddd,MMM dd, yyyy") + "</span><br/><span style=\"color:#aeb1b8;\">" + BpItem.CheckOut.ToString("ddd,MMM dd, yyyy") + "</span></td>\r\n");
                    BookingListResult.Append("<td style=\"color:#aeb1b8;\">" + PriceSales.Hotels2Currency() + "</td>\r\n");
                    //BookingListResult.Append("<td style=\"color:#aeb1b8;\">" + PriceSup.Hotels2Currency() + "</td>\r\n");
                }

                ////foreach (BookingList price in ListBookingProductList)
                ////{
                ////    if (BpItem.BookingID == price.BookingID)
                ////    {
                ////        PriceTotal = PriceTotal + decimal.Parse(price.PriceSales.ToString().Split(';')[0]);
                ////    }
                ////}

                //BookingListResult.Append("<td><img src=\"../../images/" + PicStatusName_ByBookingBalance(BpItem.Payment, BpItem.PaymentTypeID, PriceTotal) + "\"/></td>\r\n");
                
                string strStatus = "Active";
                string StatusColor = "#46a11a";
                if (BpItem.Status)
                {
                   
                    strStatus = "Inactive";
                    StatusColor = "#ff0000;";

                }
                BookingListResult.Append("<td>" + BpItem.StatusTitle + "</td>");

                BookingListResult.Append("<td align=\"center\" ><strong style=\"color:" + StatusColor + "\">" + strStatus + "</strong></td>");

                BookingListResult.Append("</tr>\r\n");

                
            }
            
            BookingListResult.Append("</table>\r\n");

            int Total = cBookingList.GetBookingListHistory_Count(intBookingId, strEmail, strNameFull, 1, 1, "date_submit", this.CurrentProductActiveExtra);
            BookingListResult.Append("<table cellpadding=\"0\" cellspacing=\"0\"  style=\"width:100%;border-bottom:1px solid #cccccc;\" >");
            BookingListResult.Append("<tr style=\"background-color:#faffbd;\">");
            BookingListResult.Append("<td width=\"60%\" style=\"text-align:left;\"><p class=\"bookingList_Page\">" + getPageList(Total, strEmail, strNameFull, "1") + "</p></td>");
            // result.Append("<td>" + Total + "</td>");
            //BookingListResult.Append("<td  width=\"40%\" style=\"text-align:right;padding:0px 10px 0px 0px\"><p class=\"bookingList_priceTotal\"><span>Total: </span> " + Price.ToString("#,###.00") + "</p> </td>");
            BookingListResult.Append("</tr>");
            BookingListResult.Append("</table>");


            return BookingListResult.ToString();
        }


        public string PicStatusName_ByBookingBalance(decimal SumBookingPaid, byte bytPaymentPlanType, decimal PriceSales)
        {

            string imageName = "false.png";
            if (bytPaymentPlanType == 2)
                imageName = "book_now_nocharge.png";
            if (SumBookingPaid > 0)
            {

                if (SumBookingPaid == PriceSales)
                    imageName = "true.png";
                if (SumBookingPaid < PriceSales)
                    imageName = "credit.png";
                if (SumBookingPaid > PriceSales)
                    imageName = "true_plus.png";
            }


            return imageName;
        }



        public string getPageList(int Total, string strEmail, string strNameFull, string BookingListType)
        {
            string result = string.Empty;
            if (Total > 50)
            {
                int totalPage = (int)Math.Ceiling((double)Total / 50);
                result = result + "";
                for (int i = 1; i <= totalPage; i++)
                {
                    int IndexPage = (50 * i) - 50;

                    result = result + "<a id=\"strStatusbookingProduct_" + i + "\" class=\"page_list\"  title=\"" + i + "\" href=\"\" onclick=\"bookingHistory('" + i + "');return false;\">" + i + "</a>";
                }
                

                //result = result + "Next >>>";
            }

            return result;
        }
    

    }
}
