using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Booking;
using Hotels2thailand.Account;
using Hotels2thailand;

namespace Hotels2thailand.UI
{
    public partial class admin_account_account_deposit : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                GetHotelList();
            }
        }


        public void GetHotelList()
        {
            StringBuilder result = new StringBuilder();

            Deposit_HotelList cDepPosit = new Deposit_HotelList();
            IList<object> iList = cDepPosit.GetDepositHotel().OrderByDescending(dep => (dep as Deposit_HotelList).DepositBalance).ToList();


            result.Append("<table id=\"table_result\" class=\"tbl_acknows\" width=\"100%\" cellspacing=\"1\" cellpadding=\"0\">");
            result.Append("<tr class=\"header_fields\" style=\"text-align:center;\">");

            result.Append("<th>No.</th><th>Hotel Code</th><th>Hotel Name</th><th>Balance</th>");
            //result.Append("<th>Price</th><th>Com</th><th>Net Price</th>");
            result.Append("</tr>");


            if (iList.Count > 0)
            {
                int count  = 0;


                foreach (Deposit_HotelList dep in iList)
                {

                    count = count + 1;

                    result.Append("<tr>"); if (count % 2 == 0)
                            result.Append("<tr class=\"row_odd\">");
                        else
                            result.Append("<tr class=\"row_event\">");

                    result.Append("<td align=\"center\">" + count + "</td>");
                    result.Append("<td align=\"center\"><a href=\"/admin/account/account_deposit_detail.aspx?pid=" + dep.ProductId + "\" target=\"_blank\" />" + dep.ProductCode + "</a></td>");
                        result.Append("<td>" + dep.ProductTitle + "</td>");
                        result.Append("<td align=\"center\">" + dep.DepositBalance.ToString("#,##0.00") + "</td>");
                        
                        result.Append("</tr>");
                    


                }


            }
            else
            {
                result.Append("<tr><td class=\"no_result\" colspan=\"5\">No Deposit</td></tr>");
            }
            result.Append("</table>");

            dep_hotelList.Text = result.ToString();
        }
        protected void btnDepSave_Click(object sender, EventArgs e)
        {
            int intBookingId = int.Parse(txtBookingId.Text);

            BookingdetailDisplay cBookingDetail = new BookingdetailDisplay();
            BookingProductDisplay cBookingProduct = new BookingProductDisplay();
            cBookingDetail = cBookingDetail.GetBookingDetailListByBookingId(intBookingId);
            cBookingProduct = cBookingProduct.getBookingProductDisplayByBookingProductId(cBookingDetail.BookingProductId);

            Account_deposit cAvDep = new Account_deposit { 
             BookingID= intBookingId,
             ProductID = cBookingProduct.ProductID,
             DateCheckIn = cBookingProduct.DateTimeCheckIn,
             DateCheckout = cBookingProduct.DateTimeCheckOut,
             CustomerName = cBookingDetail.FullName,
              DateSubmit = DateTime.Now.Hotels2ThaiDateTime(),
              Amount = decimal.Parse(txtAmount.Text),
              AmountBooking = cBookingProduct.TotalPriceSales,
               Status = true,
             Comment = txtComment.Text,
                 StaffId = this.CurrentStaffId,
                StaffHotel = txtHotelstaff.Text
            };
            int DepId = cAvDep.inserDeposit(cAvDep);

            if (DepId != 0)
                Response.Redirect(Request.Url.ToString());
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            panelBookingDetail.Visible = true;

            int intBookingId = int.Parse(txtBookingId.Text);

            BookingdetailDisplay cBookingDetail = new BookingdetailDisplay();
            BookingProductDisplay cBookingProduct = new BookingProductDisplay();
            cBookingDetail = cBookingDetail.GetBookingDetailListByBookingId(intBookingId);
            cBookingProduct = cBookingProduct.getBookingProductDisplayByBookingProductId(cBookingDetail.BookingProductId);

            lblBookingId.Text = cBookingDetail.BookingId + "[" + cBookingDetail.BookingHotelId+"]";
            lblhotelName.Text = cBookingProduct.ProductTitle;
            lnbookingDetail.NavigateUrl = "account_booking_detail.aspx?bid=" + intBookingId;
            lblCustomerName.Text = cBookingDetail.FullName;
            lblChekinout.Text = ((DateTime)cBookingProduct.DateTimeCheckIn).ToString("ddd dd-MMM-yyyy") + "&nbsp; -&nbsp;  " + ((DateTime)cBookingProduct.DateTimeCheckOut).ToString("ddd dd-MMM-yyyy");
            lblbookingAmount.Text = cBookingProduct.TotalPriceSales.ToString("#,###.00");
            txtAmount.Text = cBookingProduct.TotalPriceSales.ToString("#,###.00"); ;
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            deposit_insert.Visible = true;
            deposit_hotel_list.Visible = false;
        }
}
}