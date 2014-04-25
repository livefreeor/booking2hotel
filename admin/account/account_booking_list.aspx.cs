using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Account;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_account_account_booking_list : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Product cProduct = new Product();
                dropProduct.DataSource = cProduct.GetProductAll();
                dropProduct.DataTextField = "Title";
                dropProduct.DataValueField = "ProductID";
                dropProduct.DataBind();

                DateTime dDate = DateTime.Now.Hotels2ThaiDateTime();

                dropMonthStart.DataSource = Hotels2DateTime.GetMonthList();
                dropMonthStart.DataTextField = "Value";
                dropMonthStart.DataValueField = "Key";
                dropMonthStart.DataBind();
                dropMonthStart.SelectedValue = dDate.Month.ToString();

                dropMonthEnd.DataSource = Hotels2DateTime.GetMonthList();
                dropMonthEnd.DataTextField = "Value";
                dropMonthEnd.DataValueField = "Key";
                dropMonthEnd.DataBind();

                dropMonthEnd.SelectedValue = dDate.Month.ToString();

                dropYeatStart.DataSource = Hotels2DateTime.GetYearList();
                dropYeatStart.DataTextField = "Value";
                dropYeatStart.DataValueField = "Key";
                dropYeatStart.DataBind();

                dropYeatStart.SelectedValue = dDate.Year.ToString();

                dropYearEnd.DataSource = Hotels2DateTime.GetYearList();
                dropYearEnd.DataTextField = "Value";
                dropYearEnd.DataValueField = "Key";
                dropYearEnd.DataBind();

                dropYearEnd.SelectedValue = dDate.Year.ToString();


                BindReport();
            }
        }

        public void BindReport()
        {
            int intProductId = int.Parse(dropProduct.SelectedValue);
            string strdateMonthStart = dropMonthStart.SelectedValue;
            string strdateYearStart = dropYeatStart.SelectedValue;
            string strdateMonthEnd = dropMonthEnd.SelectedValue;
            string strdateYearEnd = dropYearEnd.SelectedValue;
            byte bytOrderby = byte.Parse(dropOrderby.SelectedValue);

            GetReportList(intProductId, bytOrderby, strdateMonthStart, strdateMonthEnd, strdateYearStart, strdateYearEnd);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindReport();
        }

        public void GetReportList(int intProductId, byte bytOrderby, string strdateMonthStart, string strdateMonthEnd, string strdateYearStart, string strdateYearEnd)
        {
            StringBuilder result = new StringBuilder();
            AccountPriceReport cAccreport = new AccountPriceReport();


            DateTime dDAtestart = new DateTime(int.Parse(strdateYearStart), int.Parse(strdateMonthStart), 1);

            DateTime dDateEndTmp = new DateTime(int.Parse(strdateYearEnd), int.Parse(strdateMonthEnd), 1);
            DateTime dDateEnd = new DateTime(int.Parse(strdateYearEnd), int.Parse(strdateMonthEnd), dDateEndTmp.AddMonths(1).AddDays(-1).Day);

            
            //IList<object> iListReport = cAccreport.GetReport();

            result.Append("<table id=\"table_result\" width=\"100%\" cellspacing=\"1\" cellpadding=\"0\">");
            result.Append("<tr>");
            result.Append("<th>No.</th><th>Order Id</th><th>Name</th><th>In/Out</th><th>Status</th><th>Flag</th><th>Price</th>");
            result.Append("</tr>");
            int count = 1;

           // string flag = "<label></label>";

            foreach (AccountPriceReport report in cAccreport.GetReport(intProductId, dDAtestart, dDateEnd, bytOrderby))
            {
                result.Append("<tr>");
                result.Append("<td>" + count + "</td>");
                result.Append("<td>" + report.BookingHotelId + "</td>");
                result.Append("<td align=\"left\">&nbsp;&nbsp" + report.BookingName + "</td>");
                result.Append("<td>" + report.DateCheckIn.ToString("MMM dd, yyyy") + "&nbsp;-&nbsp;"+report.DateCheckOut.ToString("MMM dd, yyyy")+"</td>");
                result.Append("<td>" + report.StatusProcessTitle + "</td>");
                result.Append("<td>" + (report.Status == true?  "<label style=\"color:red;\">Close</label>" :  "<label style=\"color:green\">Open</label>") + "</td>");
                result.Append("<td>" + report.Price.ToString("0,000.00") + "</td>");
                result.Append("</tr>");
                count = count + 1;
            }
            result.Append("");
            result.Append("");
            result.Append("");
            result.Append("");
            result.Append("</table>");

            ListREsult.Text = result.ToString();
           
        }
        protected void dropOrderby_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindReport();
        }
}
}