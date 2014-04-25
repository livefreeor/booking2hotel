using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Account;
using Hotels2thailand.Front;
using Hotels2thailand.Production;
using Hotels2thailand.Booking;
using Hotels2thailand.Staffs;



public partial class admin_account_invoice_hotel_manage_print_booking_list : System.Web.UI.Page
{
    public string qProductId { get { return Request.QueryString["pid"]; } }
    public string qPaymentID{ get { return Request.QueryString["pay"]; } }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(this.qPaymentID) && !string.IsNullOrEmpty(this.qProductId))
            {
                int intPaymentID = int.Parse(this.qPaymentID);
                Response.Write(GetPagePrint(intPaymentID));
                Response.End();
            }
            
        }
    }

     public string GetPagePrint(int intPaymentID)
    {


        string Tmp = string.Empty;

        StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath("~/admin/account/account_paper_temp/bht_invoice_comission_booking_list.html"));
        Tmp = objReader.ReadToEnd();
        objReader.Close();

        Product cProduct = new Product();
        ProductContent cProductContent = new ProductContent();
        int inProductid = int.Parse(this.qProductId);
        cProduct = cProduct.GetProductById(inProductid);
        cProductContent = cProductContent.GetProductContentById(inProductid, 1);
        
        Account_payment cPayment = new Account_payment();
        cPayment = cPayment.getAccountPayment(intPaymentID);


        StringBuilder result = new StringBuilder();
        int intPaymentId = intPaymentID;

        Com_Booking_list cComBooking = new Com_Booking_list();
        Account_payment_Booking cAccountPaymentBooking = new Account_payment_Booking();

        

        string BookingPayment = Request.Form["booking_checked"];

        IList<object> iListBooking = cAccountPaymentBooking.getPaymentBookingList(intPaymentId);

        result.Append("<table id=\"table_result\" class=\"tbl_account\" width=\"100%\" cellspacing=\"1\" cellpadding=\"0\">");
        result.Append("<thead>");
        result.Append("<tr style=\"text-align:center;\">");

        result.Append("<th>No.</th><th>Booking No.</th><th>Name</th><th>Check In</th><th>Check Out</th><th>Status</th>");
        result.Append("<th>Price</th><th>Com</th><th>Com total</th>");
        result.Append("</tr>");
        result.Append("</thead>");

        result.Append("<tbody>");

        decimal PriceComTotal = 0;
        decimal PriceTotal = 0;
        if (iListBooking.Count > 0)
        {
            int countDue = 0;

            
            int intProductId = int.Parse(this.qProductId);
            BookingProductDisplay cBookingProduct = new BookingProductDisplay();
            foreach (Account_payment_Booking com in iListBooking)
            {

                BookingdetailDisplay cBookingDetail = com.ClassBookingDetail;
                int intBookingId = cBookingDetail.BookingId;
                int intHotelBookingId = cBookingDetail.BookingHotelId;

                cBookingProduct = cBookingProduct.getBookingProductDisplayByBookingProductId(cBookingDetail.BookingProductId);

                // Account_Commission_Engine cCombht = new Account_Commission_Engine(intBookingId, com.ComVal, com.ComCat);
                //cCombht.CalculateCommissionHotelManage(iListBooking, intProductId);
                countDue = countDue + 1;


                if (countDue % 2 == 0)
                    result.Append("<tr class=\"row_odd\">");
                else
                    result.Append("<tr >");

                DateTime dDateCheckin = (DateTime)cBookingProduct.DateTimeCheckIn;
                DateTime dDateCheckOut = (DateTime)cBookingProduct.DateTimeCheckOut;

                result.Append("<td align=\"center\">" + countDue + "</td>");
                result.Append("<td align=\"center\">" + intHotelBookingId + "/" + intBookingId + "</td>");
                result.Append("<td>" + cBookingDetail.FullName + "</td>");
                result.Append("<td align=\"center\">" + dDateCheckin.ToString("dd-MMM-yyyy") + "</td>");
                result.Append("<td align=\"center\">" + dDateCheckOut.ToString("dd-MMM-yyyy") + "</td>");
                result.Append("<td align=\"center\"> " + cBookingDetail.StatusTitle + "</td>");
                result.Append("<td align=\"right\">" + com.PriceAmount.ToString("#,##0.00") + "</td>");
                result.Append("<td align=\"center\">" + com.ComVal.ToString("0") + "</td>");
                result.Append("<td align=\"right\">" + ((decimal)com.ComAmount).ToString("#,##0.00") + "</td>");
                result.Append("</tr>");

                PriceComTotal = PriceComTotal + (decimal)com.ComAmount;
                PriceTotal = PriceTotal + (decimal)com.PriceAmount;
            }

            result.Append("<tr class=\"total\"><td  colspan=\"6\" align=\"right\">รวมจำนวนเงิน</td><td align=\"right\" style=\"font-weight:bold;\">" + PriceTotal.ToString("#,##0.00") + "</td><td align=\"right\">Commission total</td><td align=\"right\">" + PriceComTotal.ToString("#,##0.00") + "</td></tr>");

        }
        else
        {
            result.Append("<tr><td class=\"total\" colspan=\"10\">No hotel to Payment</td></tr>");
        }


        result.Append("</tbody>");
        result.Append("</table>");

      

        Tmp = Tmp.Replace("<!--##@BookingList##-->", result.ToString());


        StringBuilder resultComcal = new StringBuilder();

        resultComcal.Append("<table width=\"95%\" cellspacing=\"1\" cellpadding=\"0\">");
        resultComcal.Append("<tr>");
        resultComcal.Append("<td><strong>>>Commssion Fee</strong></td>");
        resultComcal.Append("<td style=\"width:300px\"></td>");
        resultComcal.Append("<td align=\"right\">" + PriceComTotal.ToString("#,##0.00") + " &nbsp;Baht</td>");
        resultComcal.Append("</tr>");

        if (cPayment.Vat.HasValue && cPayment.Vat > 0)
        {
            resultComcal.Append("<tr><td style=\"height:10px;\"></td></tr>");
          
            resultComcal.Append("<tr>");
            resultComcal.Append("<td><strong>" + cPayment.Vat + " % (VAT) on Commission Fee</strong></td>");
            resultComcal.Append("<td style=\"width:300px\"></td>");
            resultComcal.Append("<td align=\"right\">" + PriceComTotal.ToString("#,##0.00") + " * " + cPayment.Vat + "%</td>");
            resultComcal.Append("</tr>");
            resultComcal.Append("<tr><td colspan=\"3\" align=\"right\" style=\"text-decoration:underline;font-weight:bold;\">" + ((decimal)cPayment.VatTotal).ToString("#,##0.00") + "&nbsp;Baht</td></tr>");


            if (cPayment.WithholdingTax.HasValue && cPayment.WithholdingTax > 0)
            {
                resultComcal.Append("<tr><td style=\"height:10px;\"></td></tr>");

                resultComcal.Append("<tr>");
                resultComcal.Append("<td><strong>" + cPayment.WithholdingTax + " % (Withholdding Tax) on Commission Fee</strong></td>");
                resultComcal.Append("<td style=\"width:300px\"></td>");
                resultComcal.Append("<td align=\"right\">" + PriceComTotal.ToString("#,##0.00") + " * " + cPayment.WithholdingTax + "%</td>");
                resultComcal.Append("</tr>");
                resultComcal.Append("<tr><td colspan=\"3\" align=\"right\" style=\"text-decoration:underline;font-weight:bold;\">" + ((decimal)cPayment.WithholdingTaxTotal).ToString("#,##0.00") + "&nbsp;Baht</td></tr>");
            }


            resultComcal.Append("<tr><td style=\"height:10px;\"></td></tr>");

            resultComcal.Append("<tr>");
            resultComcal.Append("<td style=\"font-size:16px\"align=\"left\"><strong>>>Commission</strong></td>");
            resultComcal.Append("<td style=\"width:300px\"></td>");
            resultComcal.Append("<td align=\"right\"></td>");
            resultComcal.Append("</tr>");
            resultComcal.Append("<tr>");
            resultComcal.Append("<td style=\"font-size:16px\"align=\"left\"></td>");
            resultComcal.Append("<td style=\"width:300px\">Cash</td>");
            resultComcal.Append("<td align=\"right\">Commission Fee + " + cPayment.Vat + "% VAT </td>");
            resultComcal.Append("</tr>");
            resultComcal.Append("<tr><td colspan=\"3\" align=\"right\" style=\"text-decoration:underline;font-weight:bold;\">" + ((decimal)cPayment.VatTotal + PriceComTotal).ToString("#,##0.00") + "&nbsp;Baht</td></tr>");
            resultComcal.Append("<tr><td style=\"height:10px;\" colspan=\"3\"></td></tr>");

            resultComcal.Append("<tr>");
            resultComcal.Append("<td style=\"font-size:16px\"align=\"right\"><strong>>>Commission Paid</strong></td>");
            resultComcal.Append("<td style=\"width:300px\"></td>");
            resultComcal.Append("<td align=\"right\"></td>");
            resultComcal.Append("</tr>");

            resultComcal.Append("<tr>");
            resultComcal.Append("<td style=\"font-size:16px\"align=\"right\"></td>");
            resultComcal.Append("<td style=\"width:300px\" align=\"right\"><span style=\"float:left;margin-left:20px;\">=</span>Cash - " + cPayment.WithholdingTax + " % (Withholding Tax)</td>");
            resultComcal.Append("<td align=\"right\"></td>");
            resultComcal.Append("</tr>");

            resultComcal.Append("<tr>");
            resultComcal.Append("<td style=\"font-size:16px\"align=\"right\"></td>");
            resultComcal.Append("<td  align=\"right\" style=\"width:300px;text-decoration:underline;font-weight:bold;\"><span style=\"float:left;margin-left:20px;\">=</span>" + ((decimal)(((decimal)cPayment.VatTotal + PriceComTotal) - cPayment.WithholdingTaxTotal)).ToString("#,##0.00") + "&nbsp;Baht</td>");
            resultComcal.Append("<td align=\"right\"></td>");
            resultComcal.Append("</tr>");
            
            
        }
        resultComcal.Append("</table>");


        Tmp = Tmp.Replace("<!--##@Comcalculate##-->", resultComcal.ToString());
        return Tmp;

    }

    // public string GetBookingList(int intPaymentID)
    //{
    //    StringBuilder result = new StringBuilder();
    //    int intPaymentId = intPaymentID;

    //    Com_Booking_list cComBooking = new Com_Booking_list();
    //    Account_payment_Booking cAccountPaymentBooking = new Account_payment_Booking();

    //    Account_payment cPayment = new Account_payment();
    //    cPayment = cPayment.getAccountPayment(intPaymentId);




    //    string BookingPayment = Request.Form["booking_checked"];

    //    IList<object> iListBooking = cAccountPaymentBooking.getPaymentBookingList(intPaymentId);

    //    result.Append("<table id=\"table_result\" class=\"tbl_account\" width=\"100%\" cellspacing=\"1\" cellpadding=\"0\">");
    //    result.Append("<thead>");
    //    result.Append("<tr style=\"text-align:center;\">");

    //    result.Append("<th>No.</th><th>Booking No.</th><th>Name</th><th>Check In</th><th>Check Out</th><th>Status</th>");
    //    result.Append("<th>Price</th><th>Com</th><th>Com total</th>");
    //    result.Append("</tr>");
    //    result.Append("</thead>");

    //    result.Append("<tbody>");

    //    if (iListBooking.Count > 0)
    //    {
    //        int countDue = 0;

    //        decimal PriceComTotal = 0;
    //        decimal PriceTotal = 0;
    //        int intProductId = int.Parse(this.qProductId);
    //        BookingProductDisplay cBookingProduct = new BookingProductDisplay();
    //        foreach (Account_payment_Booking com in iListBooking)
    //        {

    //            BookingdetailDisplay cBookingDetail = com.ClassBookingDetail;
    //            int intBookingId = cBookingDetail.BookingId;
    //            int intHotelBookingId = cBookingDetail.BookingHotelId;

    //            cBookingProduct = cBookingProduct.getBookingProductDisplayByBookingProductId(cBookingDetail.BookingProductId);

    //            // Account_Commission_Engine cCombht = new Account_Commission_Engine(intBookingId, com.ComVal, com.ComCat);
    //            //cCombht.CalculateCommissionHotelManage(iListBooking, intProductId);
    //            countDue = countDue + 1;


    //            if (countDue % 2 == 0)
    //                result.Append("<tr class=\"row_odd\">");
    //            else
    //                result.Append("<tr >");

    //            DateTime dDateCheckin = (DateTime)cBookingProduct.DateTimeCheckIn;
    //            DateTime dDateCheckOut = (DateTime)cBookingProduct.DateTimeCheckOut;

    //            result.Append("<td align=\"center\">" + countDue + "</td>");
    //            result.Append("<td align=\"center\">" + intHotelBookingId + "</td>");
    //            result.Append("<td>" + cBookingDetail.FullName + "</td>");
    //            result.Append("<td align=\"center\">" + dDateCheckin.ToString("dd-MMM-yyyy") + "</td>");
    //            result.Append("<td align=\"center\">" + dDateCheckOut.ToString("dd-MMM-yyyy") + "</td>");
    //            result.Append("<td align=\"center\"> " + cBookingDetail.StatusTitle + "</td>");
    //            result.Append("<td align=\"right\">" + com.PriceAmount.ToString("#,##0.00") + "</td>");
    //            result.Append("<td align=\"center\">" + com.ComVal.ToString("0") + "</td>");
    //            result.Append("<td align=\"right\">" + ((decimal)com.ComAmount).ToString("#,##0.00") + "</td>");
    //            result.Append("</tr>");

    //            PriceComTotal = PriceComTotal + (decimal)com.ComAmount;
    //            PriceTotal = PriceTotal + (decimal)com.PriceAmount;
    //        }

    //        result.Append("<tr class=\"total\"><td  colspan=\"6\" align=\"right\">รวมจำนวนเงิน</td><td align=\"right\" style=\"font-weight:bold;\">" + PriceTotal.ToString("#,##0.00") + "</td><td align=\"right\">Commission total</td><td align=\"right\">" + PriceComTotal.ToString("#,##0.00") + "</td></tr>");

    //    }
    //    else
    //    {
    //        result.Append("<tr><td class=\"total\" colspan=\"10\">No hotel to Payment</td></tr>");
    //    }


    //    result.Append("</tbody>");
    //    result.Append("</table>");

    //    return result.ToString(); 


    //   // ltPaylistSum.Text = result.ToString();


    //    //if (cPayment.VatTotal.HasValue)
    //    //{
    //    //    bank_sel.Attributes.Add("style", "display:none;");
    //    //    vat_value.Attributes.Add("style", "display:block;");

    //    //    if (cPayment.WithholdingTaxTotal.HasValue && cPayment.WithholdingTaxTotal > 0)
    //    //    {
    //    //        chktax.Checked = true;
    //    //    }
    //    //    else
    //    //    {
    //    //        chktax.Checked = false;
    //    //    }
    //    //    raidoVat.SelectedValue = "1";
    //    //}
    //    //else
    //    //{
    //    //    vat_value.Attributes.Add("style", "display:none;");
    //    //    bank_sel.Attributes.Add("style", "display:block;");

    //    //    raidoBank.SelectedValue = cPayment.BankId.ToString();
    //    //    raidoVat.SelectedValue = "0";
    //    //}


    //    //hyinvoice.NavigateUrl = "invoice_hotel_manage_print.aspx?pid=" + this.qProductId + "&pay=" + this.qPaymentId;
    //    //hybookingList.NavigateUrl = "invoice_hotel_manage_print_booking_list.aspx?pid=" + this.qProductId + "&pay=" + this.qPaymentId;

    //}
}