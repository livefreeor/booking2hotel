using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Booking;
using Hotels2thailand.Account;
using Hotels2thailand;

namespace Hotels2thailand.UI
{
    public partial class admin_account_account_deposit_detail : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qProductId))
                {
                    int intProductId = int.Parse(this.qProductId);
                    GetHotelList(intProductId);
                    Production.Product cProduct = new Production.Product();
                    cProduct = cProduct.GetProductById(intProductId);

                    lblHotelname.Text = cProduct.Title;
                    
                }

            }
        }


        public void GetHotelList(int intProductId)
        {

            StringBuilder result = new StringBuilder();


            
            Account_deposit cDEp = new Account_deposit();
            IList<object> iList = cDEp.getDepositByProductID_available(intProductId);

            
             //= cDepPosit.GetDepositHotel();


            result.Append("<table id=\"table_result\" class=\"tbl_acknows\" width=\"100%\" cellspacing=\"1\" cellpadding=\"0\">");
            result.Append("<thead class=\"header_fields\" style=\"text-align:center;\">");

            result.Append("<th>No.</th><th>Deposit Code</th><th>BookingId</th><th>Hotel Name</th>");
            result.Append("<th>Customer Name</th><th>Check In-Out</th><th>Date Record</th>");
            result.Append("<th>Booking Toal</th><th>Deposit Amount</th><th>Deposit Balance</th><th>Staff record</th><th>Hotel staff ref.</th>");
            result.Append("</thead>");

            result.Append("<tbody>");

            if (iList.Count > 0)
            {
                int count  = 0;


                foreach (Account_deposit dep in iList)
                {
                    string strCusName = "N/A";
                    string DateCheckin = "N/A";
                    string DateCheckOut = "N/A";


                    if (!string.IsNullOrEmpty(dep.CustomerName))
                        strCusName = dep.CustomerName;
                    count = count + 1;


                    if (dep.DateCheckIn.HasValue)
                        DateCheckin = ((DateTime)dep.DateCheckIn).ToString("dd-MMM-yy");

                    if (dep.DateCheckout.HasValue)
                        DateCheckOut = ((DateTime)dep.DateCheckout).ToString("dd-MMM-yy");

                    result.Append("<tr>"); if (count % 2 == 0)
                            result.Append("<tr class=\"row_odd\">");
                        else
                            result.Append("<tr class=\"row_event\">");

                    result.Append("<td align=\"center\" rowspan=\"2\">" + count + "</td>");
                    result.Append("<td align=\"center\">DEP" + dep.DepositID + "</td>");
                    result.Append("<td align=\"center\"><a href=\"/admin/account/account_bookings_list.aspx?bid=" + dep.BookingID + "\" target=\"_Blank\" />"+dep.BookingID+"</a></td>");

                    result.Append("<td>" + dep.cProduct.Title + "</td>");
                    result.Append("<td>" + strCusName + "</td>");
                    result.Append("<td align=\"center\">" + DateCheckin + "<br/>"+DateCheckOut+"</td>");

                    result.Append("<td  align=\"center\">" + dep.DateSubmit.ToString("dd-MMM-yy") + "</td>");
                    result.Append("<td align=\"center\">" + dep.AmountBooking.ToString("#,###.00") + "</td>");
                    result.Append("<td align=\"center\">" + dep.Amount.ToString("#,###.00") + "</td>");
                    result.Append("<td align=\"center\">" + dep.DepositUsed.ToString("#,###.00") + "</td>");
                    result.Append("<td align=\"center\">" + dep.StaffName + "</td>");
                    result.Append("<td align=\"center\">" + dep.StaffHotel + "</td>");
                        
                   result.Append("</tr>");

                   result.Append("<tr class=\"row_event\"><td colspan=\"11\">");
                   
                   if (dep.IlistDepRepay.Count > 0)
                   {

                       foreach (Deposit_repay derp in dep.IlistDepRepay)
                       {
                           string DatePay = string.Empty;
                           if (derp.ComfirmDeposit.HasValue)
                               DatePay = ((DateTime)derp.ComfirmDeposit).ToString("dd-MMM-yyyy");
                           result.Append("<p class=\"transaction_dep\">");

                           result.Append(derp.PaymentId + ", Date Payment : " + DatePay + " Amount: " + derp.Amount.ToString("#,###.00"));
                           result.Append("");
                           result.Append("</p>");
                       }
                   }
                   else
                   {
                       result.Append("<p class=\"transaction_dep\">");
                       result.Append("No transaction");
                       result.Append("</p>");
                      
                   }
                   
                   
                   
                   result.Append("</td></tr>");
                }

            }
            else
            {
                result.Append("<tr><td class=\"no_result\" colspan=\"5\">No Deposit</td></tr>");
            }

            result.Append("<tbody>");
            result.Append("</table>");

            dep_list.Text = result.ToString();
        }
        
}
}