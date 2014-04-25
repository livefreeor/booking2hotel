using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Account;
using System.Text;
using Hotels2thailand;



namespace Hotels2thailand.UI
{
    public partial class admin_account_com_sales_commission : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                

                DateTime cDateStart = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 1);

                DateTime cDateEnd = cDateStart.AddMonths(1).AddDays(-1);

                DropYear.DataSource = Hotels2thailand.Hotels2DateTime.GetYearList();
                DropYear.DataTextField = "Value";
                DropYear.DataValueField = "Key";
                DropYear.DataBind();


               dropMonth.DataSource = Hotels2thailand.Hotels2DateTime.GetMonthList();
               dropMonth.DataTextField = "Value";
               dropMonth.DataValueField = "Key";
               dropMonth.DataBind();

               DropYear.SelectedValue = cDateStart.Year.ToString();

               dropMonth.SelectedValue = cDateEnd.Month.ToString();
                GenCommissionList(cDateStart, cDateEnd);
            }
        }


        public void GenCommissionList(DateTime dDateStart, DateTime dDateEnd)
        {
            StringBuilder resultOld = new StringBuilder();
            StringBuilder resultNew = new StringBuilder();
            StringBuilder result = new StringBuilder();
            StringBuilder resultHead = new StringBuilder();

            Account_Sales_manage cAccount = new Account_Sales_manage();
            IList<object> iList = cAccount.getCom(dDateStart, dDateEnd);

           
            resultHead.Append("<tr>");
            resultHead.Append("<th>No.</th><th>BookingID</th><th>Booking HotelID</th>");
            resultHead.Append("<th>Cusname</th><th>Check-In</th><th>Check-Out</th><th>Booking recieve</th><th>Unit</th>");
            resultHead.Append("<th>ComVal</th><th>Price</th><th>Price sup</th><th>Com</th><th>Com sales</th>");
            resultHead.Append("</tr>");

            int CountOld = 1;
            int CountNew = 1;
            decimal TotalComOld = 0;
            decimal TotalCOmNew = 0;
            foreach (Account_Sales_manage com in iList)
            {
                string[] strItem = com.Price.Split('&');
                decimal Price = decimal.Parse(strItem[0]);
                decimal PriceSup = decimal.Parse(strItem[1]);
                decimal PriceCom = Price - PriceSup;
                decimal SaleCom = ((PriceCom - ((Price * 3) / 100)) * 30)/100;
                //decimal SaleCom = ((PriceCom - ((Price * 3) / 100)) * 30) / 100;
                int intNight = com.CheckOut.Subtract(com.Checkin).Days;
                int Unit = int.Parse(strItem[2]) * intNight;

                if (com.DateBooking < new DateTime(2013, 11, 15, 19, 0, 0))
                {
                    resultOld.Append("<tr>");
                    resultOld.Append("<td>" + CountOld + "</td><td>"+com.BookingId+"</td><td>"+com.BookingHotelID+"</td>");
                    resultOld.Append("<td>" + com.CusName + "</td><td>" + com.Checkin.ToString("dd-MMM-yyyy") + "</td><td>" + com.CheckOut.ToString("dd-MMM-yyyy") + "</td><td>" + com.DateBooking.ToString("dd-MMM-yyyy") + "</td><td>" + Unit + "</td>");
                    resultOld.Append("<td>" + com.CommissionVal.ToString("#,##0.00") + "</td><td>" + Price.ToString("#,##0.00") + "</td><td>" + PriceSup.ToString("#,##0.00") + "</td><td>" + PriceCom.ToString("#,##0.00") + "</td><td>" + SaleCom.ToString("#,##0.00") + "</td>");
                    resultOld.Append("</tr>");
                    TotalComOld = TotalComOld + SaleCom;
                    CountOld = CountOld + 1;
                }else
                {
                    resultNew.Append("<tr>");
                    resultNew.Append("<td>" + CountNew + "</td><td>" + com.BookingId + "</td><td>" + com.BookingHotelID + "</td>");
                    resultNew.Append("<td>" + com.CusName + "</td><td>" + com.Checkin.ToString("dd-MMM-yyyy") + "</td><td>" + com.CheckOut.ToString("dd-MMM-yyyy") + "</td><td>" + com.DateBooking.ToString("dd-MMM-yyyy") + "</td><td>" + Unit + "</td>");
                    resultNew.Append("<td>" + com.CommissionVal.ToString("#,##0.00") + "</td><td>" + Price.ToString("#,##0.00") + "</td><td>" + PriceSup.ToString("#,##0.00") + "</td><td>" + PriceCom.ToString("#,##0.00") + "</td><td>" + (Unit*150).ToString("#,##0.00") + "</td>");
                    resultNew.Append("</tr>");
                    TotalCOmNew = TotalCOmNew + (Unit * 150);
                    CountNew = CountNew + 1;
                }
            }
            result.Append("<h1>Old Commission</h1>");
            result.Append("<table class=\"tabledesign\" >");
            result.Append(resultHead.ToString() + resultOld.ToString());
            result.Append("<tr><td colspan=\"12\">Total:</td><td>" + TotalComOld.ToString("#,##0.00") + "</td></tr>");
            result.Append("</table>");
            result.Append("<h1>New Commission</h1>");
            result.Append("<table class=\"tabledesign\" >");
            result.Append(resultHead.ToString() + resultNew.ToString());
            result.Append("<tr><td colspan=\"12\">Total</td><td>" + TotalCOmNew.ToString("#,##0.00") + "</td></tr>");
            result.Append("</table>");
            ltroldCom.Text = result.ToString();

        }
        protected void btnGO_Click(object sender, EventArgs e)
        {
            Response.Write(DropYear.SelectedValue);
            Response.Write(dropMonth.SelectedValue);
            DateTime cDateStart = new DateTime(int.Parse(DropYear.SelectedValue), int.Parse(dropMonth.SelectedValue), 1);

            DateTime cDateEnd = cDateStart.AddMonths(1).AddDays(-1);

            
            GenCommissionList(cDateStart, cDateEnd);
        }
}
}
