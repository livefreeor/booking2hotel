using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Account;

namespace Hotels2thailand.UI
{
    public partial class admin_account_com_bht_manage : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
               
                
                Com_hotel_list Comhotel = new Com_hotel_list();
                IList<object> iList = Comhotel.GetComHotelLsit();

                StringBuilder result = new StringBuilder();
                
                result.Append("<table id=\"table_result\" class=\"tbl_account\" width=\"80%\" cellspacing=\"1\" cellpadding=\"0\">");
                result.Append("<thead>");
                result.Append("<tr class=\"header_field\" style=\"text-align:center;\">");
                result.Append("<th>No.</th><th>Hotel Code</th><th>Name</th><th>Due</th><th>No Due</th>");
                result.Append("</tr>");
                result.Append("</thead>");
                result.Append("<tbody>");
                if (iList.Count > 0)
                {
                    int count = 0;
                    foreach (Com_hotel_list com in iList)
                    {
                        count = count + 1;
                        result.Append("<tr>");
                        result.Append("<td>" + count + "</td>");
                        result.Append("<td><a href=\"com_bht_manage_booking_sel.aspx?pid="+com.ProductId+"\" target=\"_blank\"  />" + com.ProductCode + "</a></td>");
                        result.Append("<td>"+com.ProductTitle+"</td>");
                        result.Append("<td>"+com.BookingDue+"</td>");
                        result.Append("<td>"+com.BookingNotDue + "</td>");
                        result.Append("</tr>");
                    }
                }
                else
                {
                    result.Append("<tr><td colspan=\"5\">No hotel to Payment</td></tr>");
                }

                result.Append("</tbody>");
                result.Append("</table>");

                ltHotelList.Text = result.ToString();
            }
        }
        
}
}
