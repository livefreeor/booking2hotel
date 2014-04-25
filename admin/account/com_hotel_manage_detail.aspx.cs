using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Account;
using Hotels2thailand.Suppliers;
using Hotels2thailand.Production;
using Hotels2thailand.Booking;
using Hotels2thailand.Front;


namespace Hotels2thailand.UI
{
    public partial class admin_account_com_hotel_manage_detail : Hotels2BasePage
    {
        public string qPaymentId { get { return Request.QueryString["pay"]; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qPaymentId) && !string.IsNullOrEmpty(this.qProductId))
                {
                    GetBookingList();
                }
            }
        }

        public void GetBookingList()
        {
            StringBuilder result = new StringBuilder();
            int intPaymentId = int.Parse(this.qPaymentId);

            Com_Booking_list cComBooking = new Com_Booking_list();
            Account_payment_Booking cAccountPaymentBooking = new Account_payment_Booking();

            Account_payment cPayment = new Account_payment();
            cPayment = cPayment.getAccountPayment(intPaymentId);




            liter.Text = this.qPaymentId;

            string BookingPayment = Request.Form["booking_checked"];

            IList<object> iListBooking = cAccountPaymentBooking.getPaymentBookingList(intPaymentId);

            result.Append("<table id=\"table_result\" class=\"tbl_account\" width=\"100%\" cellspacing=\"1\" cellpadding=\"0\">");
            result.Append("<thead>");
            result.Append("<tr style=\"text-align:center;\">");

            result.Append("<th>Checked</th><th>No.</th><th>Booking No.</th><th>Name</th><th>Check In</th><th>Check Out</th>");
            result.Append("<th>Price</th><th>Com</th><th>Com total</th>");
            result.Append("</tr>");
            result.Append("</thead>");

            result.Append("<tbody>");

            if (iListBooking.Count > 0)
            {
                int countDue = 0;

                decimal PriceComTotal = 0;
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

                    result.Append("<td align=\"center\"><input type=\"checkbox\" name=\"booking_sel_checked\"  checked=\"checked\" value=\"" + intBookingId + "\" /></td><td>"+ countDue +"</td>");
                    result.Append("<td align=\"center\"><a href=\"/admin/account/account_booking_detail.aspx?bid=" + intBookingId + "\" target=\"_blank\" />" + intHotelBookingId + "</a></td>");
                    result.Append("<td>" + cBookingDetail.FullName + "</td>");
                    result.Append("<td align=\"center\">" + dDateCheckin.ToString("dd-MMM-yyyy") + "<input type=\"hidden\" name=\"hd_date_checkin_" + intBookingId + "\" value=\"" + dDateCheckin.ToString("yyyy-MM-dd") + "\" ></td>");
                    result.Append("<td align=\"center\">" + dDateCheckOut.ToString("dd-MMM-yyyy") + "<input type=\"hidden\" name=\"hd_date_checkout_" + intBookingId + "\" value=\"" + dDateCheckOut.ToString("yyyy-MM-dd") + "\" ></td>");

                    result.Append("<td align=\"right\">" + com.PriceAmount.ToString("#,##0.00") + "<input type=\"hidden\" name=\"hd_price_amount_" + intBookingId + "\" value=\"" + com.PriceAmount + "\" ></td>");
                    result.Append("<td align=\"right\">" + com.ComVal.ToString("0") + "<input type=\"hidden\" name=\"hd_priceCOm_Val" + intBookingId + "\" value=\"" + com.ComVal + "\" ></td>");
                    result.Append("<td align=\"right\">" + ((decimal)com.ComAmount).ToString("#,##0.00") + "<input type=\"hidden\" name=\"hd_priceCOm_amount_" + intBookingId + "\" value=\"" + com.ComAmount + "\" ><input type=\"hidden\" name=\"hd_com_cat_" + intBookingId + "\" value=\"" + com.ComCat + "\" ><input type=\"hidden\" name=\"hd_com_val_" + intBookingId + "\" value=\"" + com.ComVal + "\" ></td>");
                    result.Append("</tr>");

                    PriceComTotal = PriceComTotal + (decimal)com.ComAmount;
                    
                }

                result.Append("<tr class=\"total\"><td  colspan=\"8\" align=\"right\">รวมจำนวนเงิน</td><td align=\"right\" style=\"font-weight:bold;\">" + PriceComTotal.ToString("#,##0.00") + "</td></tr>");

            }
            else
            {
                result.Append("<tr><td class=\"total\" colspan=\"10\">No hotel to Payment</td></tr>");
            }

            result.Append("</tbody>");
            result.Append("</table>");


            ltPaylistSum.Text = result.ToString();


            if (cPayment.VatTotal.HasValue )
            {
                bank_sel.Attributes.Add("style", "display:none;");
                vat_value.Attributes.Add("style", "display:block;");

                if (cPayment.WithholdingTaxTotal.HasValue && cPayment.WithholdingTaxTotal > 0)
                {
                    chktax.Checked = true;
                }
                else
                {
                    chktax.Checked = false;
                }
                raidoVat.SelectedValue = "1";
            }
            else
            {
                vat_value.Attributes.Add("style", "display:none;");
                bank_sel.Attributes.Add("style", "display:block;");

                raidoBank.SelectedValue = cPayment.BankId.ToString();
                raidoVat.SelectedValue = "0";
            }


            hyinvoice.NavigateUrl = "invoice_hotel_manage_print.aspx?pid=" + this.qProductId + "&pay=" + this.qPaymentId ;
            hybookingList.NavigateUrl = "invoice_hotel_manage_print_booking_list.aspx?pid=" + this.qProductId + "&pay=" + this.qPaymentId;

        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["booking_sel_checked"]) && !string.IsNullOrEmpty(this.qProductId))
            {
                
                int intProductId = int.Parse(this.qProductId);
                int intPaymentId = int.Parse(this.qPaymentId); 
                decimal decPriceTotal = 0;
                decimal decComTotal = 0;
                //decimal decCostTotel = 0;
                //decimal decDepositTotal = 0;
                //default Value KindID = Com hotel Payment (bht manage);
                //byte bytKindId = 3;


                string[] arrBookingID = Request.Form["booking_sel_checked"].Split(',');

                


                //Sum Price ,Cost ,Com for hotel Payment
                for (int i = 0; i < arrBookingID.Count(); i++)
                {
                    //Response.Write(Request.Form["hd_price_amount_" + arrBookingID[i]]);
                    int intBookingID = int.Parse(arrBookingID[i]);
                    decPriceTotal = decPriceTotal + decimal.Parse(Request.Form["hd_price_amount_" + arrBookingID[i]]);
                    decComTotal = decComTotal + decimal.Parse(Request.Form["hd_priceCOm_amount_" + arrBookingID[i]]);
                    //decCostTotel = decCostTotel + decimal.Parse(Request.Form["hd_priceSUp_amount_" + arrBookingID[i]]);

                }

                


                string strAccountName = string.Empty;
                string strAccountNum = string.Empty;
                string strAccountBanch = string.Empty;
                string strAccountType = string.Empty;

                decimal? decVatTotal = null;
                decimal? decTaxTotal = null;
                byte? bytVat = null;
                byte? bytTax = null;
                byte? bytBankId = null;


                if (raidoVat.SelectedValue == "1")
                {
                    bytVat = byte.Parse(txtVatval.Text);
                    decVatTotal = (decComTotal * (byte)bytVat) / 100;


                    if (chktax.Checked)
                    {
                        bytTax = byte.Parse(txtTaxValue.Text);
                        decTaxTotal = (decComTotal * (byte)bytTax) / 100;
                    }
                }
                else
                {
                    bytBankId = byte.Parse(raidoBank.SelectedValue);
                }
                bool bolUpdate = false;

                Account_payment cAccountPayment = new Account_payment();
              

                cAccountPayment = cAccountPayment.getAccountPayment(intPaymentId);
                cAccountPayment.PriceTotal = decPriceTotal;
                cAccountPayment.ComTotal = decComTotal;
                cAccountPayment.Vat = bytVat;
                cAccountPayment.WithholdingTax = bytTax;
                cAccountPayment.VatTotal = decVatTotal;
                cAccountPayment.WithholdingTaxTotal = decTaxTotal;
                cAccountPayment.BankId = bytBankId;
                bolUpdate = cAccountPayment.UpdatePayment(cAccountPayment);

                
                //if Insert Payment Completed
                if (bolUpdate)
                {
                    Account_payment_Booking cAccountBooking = new Account_payment_Booking();
                    IList<object> iListbooking = cAccountBooking.getPaymentBookingList(intPaymentId);

                    // DEfine Instant And Assing Value for Hotel Payment BOoking 
                    foreach (Account_payment_Booking item in iListbooking)
                    {
                    
                        int count = 0;
                        for (int i = 0; i < arrBookingID.Count(); i++)
                        {
                            int intBookingID = int.Parse(arrBookingID[i]);
                            if (intBookingID == item.BookingID)
                            {
                               
                                count = count + 1;

                                //Response.Write(intBookingID + "---" + item.BookingID + "<br/>");
                            }
                        }

                        
                        if (count == 0)
                        {

                            cAccountBooking.DisableBookingPayment(intPaymentId, item.BookingID);
                        }

                     

                        //decimal dePriceAmont = decimal.Parse(Request.Form["hd_price_amount_" + arrBookingID[i]]);
                        //decimal decComAmount = decimal.Parse(Request.Form["hd_priceCOm_amount_" + arrBookingID[i]]);
                        ////decimal decCostAmount = decimal.Parse(Request.Form["hd_priceSUp_amount_" + arrBookingID[i]]);

                        //byte bytComCat = byte.Parse(Request.Form["hd_com_cat_" + arrBookingID[i]]);
                        //decimal decComVal = decimal.Parse(Request.Form["hd_com_val_" + arrBookingID[i]]);


                        
                        // Check Kind for transaction fee if is the Monthly rate of Commission
                        // And Set ComVal = Transaction fee 3%;
                        //Response.Write(decComVal);

                        //if (bytComCat == 2)
                        //{
                        //    bytKindId = 1;
                        //    decComVal = 3;
                        //}


                        //Account_payment_Booking cAccountPaymentBooking = new Account_payment_Booking
                        //{
                        //    PaymentID = intPaymentID,
                        //    BookingID = intBookingID,
                        //    KindID = bytKindId,
                        //    ComCat = bytComCat,
                        //    PriceAmount = dePriceAmont,
                        //    ComAmount = decComAmount,
                        //    CostAmount = 0,
                        //    ComVal = decComVal

                        //};

                        //cAccountPaymentBooking.InsertAccountPaymentBooking(cAccountPaymentBooking);
                    }
                 

                }

                Response.Redirect(Request.Url.ToString());
                Response.End();

                //lnPrint.NavigateUrl = "invoice_hotel_manage_print.aspx?pid=" + this.qProductId + "&pay=" + intPaymentID;
                //panelPaymentSummary.Visible = false;
                //panelCompleted.Visible = true;

            }
        }
    }
}