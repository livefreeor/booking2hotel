using System;
using System.Text;
using System.Collections;
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
    public partial class ajax_booking_list_sum : Hotels2BasePageExtra_Ajax
    {
        public string qBokingStatus
        {
            get
            {
                return Request.QueryString["bs"];
            }
        }
        public string qBokingProductStatus
        {
            get
            {
                return Request.QueryString["bps"];
            }
        }

        public string qBokingListype
        {
            get
            {
                return Request.QueryString["lTpye"];
            }
        }

        public string qBokingOrderBy
        {
            get
            {
                return Request.QueryString["order"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            Response.Write(BookingSum(this.qBokingStatus, this.qBokingProductStatus, this.qBokingListype));
            Response.End();

        }


        public string BookingSum(string strBookingStatus, string strBookingProductStatus, string BookingListType)
    {
        StringBuilder result = new StringBuilder();
        try
        {
            BookingList cBookingList = new BookingList();
            decimal Price = 0;
            decimal SupplierPrice = 0;
            int Total = 0;
            ArrayList arrPrice = new ArrayList();
            short shrBookingProductStatus = short.Parse(strBookingProductStatus);
            short shrBookingStatus = short.Parse(strBookingStatus);
            switch (byte.Parse(BookingListType))
            {
                case 1:
                    Total = cBookingList.GetBookingListOrderCenter_Count(shrBookingStatus, shrBookingProductStatus,  this.CurrentProductActiveExtra);
                    arrPrice = cBookingList.GetBookingListOrderCenter_SumPrice(shrBookingStatus, shrBookingProductStatus, this.CurrentProductActiveExtra);

                    
                    break;
                case 2:

                    Total = cBookingList.GetBookingList_Status_OrderOpen_Count(shrBookingStatus, shrBookingProductStatus);
                    arrPrice = cBookingList.GetBookingList_Status_OrderOpen_SumPrice(shrBookingStatus, shrBookingProductStatus);
                    //Price = (decimal)arrPrice[0];
                    //SupplierPrice = (decimal)arrPrice[1];
                    break;
                case 3:

                    Total = cBookingList.GetBookingList_Status_CheckinDueDate_Count(shrBookingStatus, shrBookingProductStatus, 0);
                    arrPrice = cBookingList.GetBookingList_Status_CheckinDueDate_SumPrice(shrBookingStatus, shrBookingProductStatus, 0);
                    //Price = (decimal)arrPrice[0];
                    //SupplierPrice = (decimal)arrPrice[1];
                    break;
                case 4:

                    Total = cBookingList.GetBookingListOrderCenterDuplicate_Count(shrBookingStatus, shrBookingProductStatus);
                    arrPrice = cBookingList.GetBookingListOrderCenterDuplicate_SumPrice(shrBookingStatus, shrBookingProductStatus);
                    //Price = (decimal)arrPrice[0];
                    //SupplierPrice = (decimal)arrPrice[1];
                    break;

            }

            Price = (decimal)arrPrice[0];
            SupplierPrice = (decimal)arrPrice[1];

            result.Append("<table cellpadding=\"0\" cellspacing=\"0\"  style=\"width:100%;border-bottom:1px solid #cccccc;\" >");
            result.Append("<tr>");
            result.Append("<td width=\"60%\" style=\"text-align:left;\"><p class=\"bookingList_Page\">" + getPageListBooking(Total, strBookingStatus, strBookingProductStatus, BookingListType) + "</p></td>");
            // result.Append("<td>" + Total + "</td>");
            result.Append("<td  width=\"40%\" style=\"text-align:right;padding:0px 10px 0px 0px\"><p class=\"bookingList_priceTotal\"><span>Total: </span> " + Price.ToString("#,###.00") + "</p> </td>");
            result.Append("</tr>");
            result.Append("</table>");
            
        }
        catch (Exception ex)
        {
            result.Append("error: Sum: " + ex);
        }
        


        return result.ToString();
    }


        public string getPageListBooking(int Total, string strStatusbooking, string strStatusbookingProduct, string BookingListType)
        {
            string result = string.Empty;
            if (Total > 25)
            {
                int totalPage = (int)Math.Ceiling((double)Total / 25);
                result = result + "";
                for (int i = 1; i <= totalPage; i++)
                {
                    int IndexPage = (25 * i) - 25;

                    result = result + "<a id=\"strStatusbookingProduct_" + i + "\" class=\"page_list\"  title=\"" + i + "\" href=\"#\" onclick=\"getBookingPage('" + IndexPage + "','" + strStatusbookingProduct + "','" + strStatusbooking + "','" + BookingListType + "','" + i + "','" + this.qBokingOrderBy + "');return false;\">" + i + "</a>";
                }
                result = result + "<input type=\"hidden\" value=\"1\" id=\"hd_page_" + strStatusbookingProduct + "\" />";
               
            }

            return result;
        }
    }
}
