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
    public partial class admin_account_com_hotel_manage_sel : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qProductId))
                {
                    int intProductId = int.Parse(this.qProductId);
                    Production.Product cProduct = new Production.Product();
                    cProduct = cProduct.GetProductById(intProductId);

                    ltHotelName.Text = cProduct.Title;

                    Com_Booking_list cComBooking = new Com_Booking_list();
                    IList<object> iListBooking = cComBooking.GetBookingToPaymentList_hotel_manage(intProductId);

                    StringBuilder result = new StringBuilder();

                    result.Append("<table id=\"table_result\" class=\"tbl_account\" width=\"100%\" cellspacing=\"1\" cellpadding=\"0\">");
                    result.Append("<thead>");
                    result.Append("<tr  style=\"text-align:center;\">");
                    result.Append("<th><input type=\"checkbox\" id=\"main_check\" /></th>");
                    result.Append("<th>No.</th><th>Booking No.</th><th>Name</th><th>Status</th><th>Check In</th><th>Check Out</th>");
                    result.Append("<th>Price</th><th>Commission(%)</th><th>Com total</th>");
                    result.Append("</tr>");
                    result.Append("</thead>");
                    result.Append("<tbody>");
                    if (iListBooking.Count > 0)
                    {
                        int countDue = 0;
                        //int countNotDue = 0;

                        result.Append("<tr class=\"total\"><td colspan=\"10\" align=\"center\">In DueDate</td></tr>");

                        foreach (Com_Booking_list com in iListBooking)
                        {
                           // if (com.DateCheckin.Subtract(DateTime.Now.Hotels2ThaiDateTime()).Days <= com.DuePayment)
                            //{
                                Account_Commission_Engine cCombht = new Account_Commission_Engine(com.BookingId, com.ComVal, com.ComCat);
                                
                                //cCombht.LoadPriceTotalAllBooking_for_CommssionStep(iListBooking, intProductId);
                                cCombht.CalculateCommissionHotelManage(iListBooking, intProductId);
                               
                                countDue = countDue + 1;
                                if(countDue%2 == 0)
                                    result.Append("<tr class=\"row_odd\">");
                                else
                                    result.Append("<tr >");
                               
                                result.Append("<td align=\"center\"><input type=\"checkbox\" name=\"booking_checked\" checked=\"checked\"  value=\"" + com.BookingId + "\" /></td>");
                                result.Append("<td align=\"center\">" + countDue + "</td>");
                                result.Append("<td align=\"center\"><a href=\"/admin/account/account_booking_detail.aspx?bid=" + com.BookingId + "\" target=\"_blank\" />" + com.BookingHotelId + "</a></td>");
                                result.Append("<td>" + com.CustomerName + "</td>");
                                result.Append("<td align=\"center\">" + com.BookingStatusTitle + "</td>");
                                result.Append("<td align=\"center\">" + com.DateCheckin.ToString("dd-MMM-yyyy") + "</td>");
                                result.Append("<td align=\"center\">" + com.DateCheckOut.ToString("dd-MMM-yyyy") + "</td>");
                                result.Append("<td align=\"center\">" + cCombht.Price.ToString("#,##0.00") + "</td>");
                                result.Append("<td align=\"center\">" + cCombht.ComVal.ToString("0") + "</td>");
                                result.Append("<td align=\"center\">" + cCombht.PriceCom.ToString("#,##0.00") + "</td>");
                               
                                result.Append("</tr>");

                           // }

                        }
                    }
                    else
                    {
                        result.Append("<tr><td class=\"total\" colspan=\"5\">No hotel to Payment</td></tr>");
                    }

                    result.Append("</tbody>");
                    result.Append("</table>");

                    ltBookingList.Text = result.ToString();
                }
            }
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            
            panelPaymentSel.Visible = false;
            panelPaymentSummary.Visible = true;
            string strProductId = this.qProductId;

            StringBuilder result = new StringBuilder();


            Com_Booking_list cComBooking = new Com_Booking_list();
            string BookingPayment = Request.Form["booking_checked"];



            IList<object> iListBooking = cComBooking.GetBookingToPaymentList_hotel_manage(BookingPayment, strProductId);


            result.Append("<table id=\"table_result\" class=\"tbl_account\" width=\"100%\" cellspacing=\"1\" cellpadding=\"0\">");
            result.Append("<thead>");
            result.Append("<tr style=\"text-align:center;\">");

            result.Append("<th>No.</th><th>Booking No.</th><th>Name</th><th>Status</h><th>Check In</th><th>Check Out</th>");
            result.Append("<th>Price</th><th>Commission(%)</th><th>Com total</th>");
            result.Append("</tr>");
            result.Append("</thead>");
            result.Append("<tbody>");

            if (iListBooking.Count > 0)
            {


                int countDue = 0;

                decimal PriceComTotal = 0;
                int intProductId = int.Parse(this.qProductId);
                foreach (Com_Booking_list com in iListBooking)
                {
                    //if (com.DateCheckin.Subtract(DateTime.Now.Hotels2ThaiDateTime()).Days <= com.DuePayment)
                    //{
                    Account_Commission_Engine cCombht = new Account_Commission_Engine(com.BookingId, com.ComVal, com.ComCat);
                    cCombht.CalculateCommissionHotelManage(iListBooking, intProductId);
                    countDue = countDue + 1;

                   
                    if (countDue % 2 == 0)
                        result.Append("<tr class=\"row_odd\">");
                    else
                        result.Append("<tr >");
                               


                    result.Append("<td align=\"center\"><input type=\"checkbox\" name=\"booking_sel_checked\" style=\"display:none;\" checked=\"checked\" value=\"" + com.BookingId + "\" />" + countDue + "</td>");
                    result.Append("<td align=\"center\"><a href=\"/admin/account/account_booking_detail.aspx?bid=" + com.BookingId + "\" target=\"_blank\" />" + com.BookingHotelId + "</a></td>");
                    result.Append("<td>" + com.CustomerName + "</td>");
                    result.Append("<td align=\"center\">" + com.BookingStatusTitle + "</td>");
                    result.Append("<td align=\"center\">" + com.DateCheckin.ToString("dd-MMM-yyyy") + "<input type=\"hidden\" name=\"hd_date_checkin_" + com.BookingId + "\" value=\"" + com.DateCheckin.ToString("yyyy-MM-dd") + "\" ></td>");
                    result.Append("<td align=\"center\">" + com.DateCheckOut.ToString("dd-MMM-yyyy") + "<input type=\"hidden\" name=\"hd_date_checkout_" + com.BookingId + "\" value=\"" + com.DateCheckOut.ToString("yyyy-MM-dd") + "\" ></td>");

                    result.Append("<td align=\"right\">" + cCombht.Price.ToString("#,##0.00") + "<input type=\"hidden\" name=\"hd_price_amount_" + com.BookingId + "\" value=\"" + cCombht.Price + "\" ></td>");
                    result.Append("<td align=\"right\">" + cCombht.ComVal.ToString("0") + "<input type=\"hidden\" name=\"hd_priceCOm_Val" + com.BookingId + "\" value=\"" + cCombht.ComVal + "\" ></td>");
                    result.Append("<td align=\"right\">" + cCombht.PriceCom.ToString("#,##0.00") + "<input type=\"hidden\" name=\"hd_priceCOm_amount_" + com.BookingId + "\" value=\"" + cCombht.PriceCom + "\" ><input type=\"hidden\" name=\"hd_com_cat_" + com.BookingId + "\" value=\"" + com.ComCat + "\" ><input type=\"hidden\" name=\"hd_com_val_" + com.BookingId + "\" value=\"" + cCombht.ComVal + "\" ></td>");
                    result.Append("</tr>");

                    PriceComTotal = PriceComTotal + cCombht.PriceCom;
                    //}

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
        }
        protected void btnPayment_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["booking_sel_checked"]) && !string.IsNullOrEmpty(this.qProductId))
            {
                int intPaymentID = 0;
                int intProductId = int.Parse(this.qProductId);

                decimal decPriceTotal = 0;
                decimal decComTotal = 0;
                //decimal decCostTotel = 0;
                //decimal decDepositTotal = 0;
                //default Value KindID = Com hotel Payment (bht manage);
                byte bytKindId = 3;

                
                string[] arrBookingID = Request.Form["booking_sel_checked"].Split(',');

                //IList<Account_payment_Booking> ilistPaymentBooking = null;
                //IList<Deposit_repay> iListDepositRepay = null;

                

                //Sum Price ,Cost ,Com for hotel Payment
               // Response.Write(arrBookingID.Count() + "<br/>");
                //Response.Write(Request.Form["hd_price_amount_257310"]);
                //Response.Flush();
                for (int i = 0; i < arrBookingID.Length; i++)
                {
                    //Response.Write(i);
                    //Response.Flush();
                    //Response.Write(Request.Form["hd_price_amount_" + arrBookingID[i]]);
                    int intBookingID = int.Parse(arrBookingID[i]);
                    decPriceTotal = decPriceTotal + decimal.Parse(Request.Form["hd_price_amount_" + arrBookingID[i]]);
                    decComTotal = decComTotal + decimal.Parse(Request.Form["hd_priceCOm_amount_" + arrBookingID[i]]);
                    //decCostTotel = decCostTotel + decimal.Parse(Request.Form["hd_priceSUp_amount_" + arrBookingID[i]]);

                }

                //-----------------------------------------

                //hotel Payment Create   
                // BHT MANAGE == 2
                // Commission CAt = FALT RATE == 1

                
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
                
                
                Account_payment cAccountPayment = new Account_payment
                {
                    ProductId = intProductId,
                    ManageId = 1,
                    
                    StaffId = this.CurrentStaffId,
                    PriceTotal = decPriceTotal,
                    ComTotal = decComTotal,
                    CostTotal = 0,
                    DateSubmit = DateTime.Now.Hotels2ThaiDateTime(),
                    Status = true,
                    BankId = bytBankId,
                    Vat = bytVat,
                    WithholdingTax = bytTax,
                    VatTotal = decVatTotal,
                    WithholdingTaxTotal = decTaxTotal
                    
                };

                intPaymentID = cAccountPayment.InsertPayment(cAccountPayment);
                //if Insert Payment Completed
                if (intPaymentID > 0)
                {

                    // DEfine Instant And Assing Value for Hotel Payment BOoking 
                    for (int i = 0; i < arrBookingID.Count(); i++)
                    {
                        //Response.Write(Request.Form["hd_price_amount_" + arrBookingID[i]]);
                        int intBookingID = int.Parse(arrBookingID[i]);

                        decimal dePriceAmont = decimal.Parse(Request.Form["hd_price_amount_" + arrBookingID[i]]);
                        decimal decComAmount = decimal.Parse(Request.Form["hd_priceCOm_amount_" + arrBookingID[i]]);
                        //decimal decCostAmount = decimal.Parse(Request.Form["hd_priceSUp_amount_" + arrBookingID[i]]);

                        byte bytComCat = byte.Parse(Request.Form["hd_com_cat_" + arrBookingID[i]]);
                        decimal decComVal = decimal.Parse(Request.Form["hd_com_val_" + arrBookingID[i]]);

                        // Check Kind for transaction fee if is the Monthly rate of Commission
                        // And Set ComVal = Transaction fee 3%;
                        //Response.Write(decComVal);
                        
                        if (bytComCat == 2)
                        {
                            bytKindId = 1;
                            decComVal = 3;
                        }


                        Account_payment_Booking cAccountPaymentBooking = new Account_payment_Booking
                        {
                            PaymentID = intPaymentID,
                            BookingID = intBookingID,
                            KindID = bytKindId,
                            ComCat = bytComCat,
                            PriceAmount = dePriceAmont,
                            ComAmount = decComAmount,
                            CostAmount = 0,
                            ComVal = decComVal

                        };

                        cAccountPaymentBooking.InsertAccountPaymentBooking(cAccountPaymentBooking);
                    }


                }

                lnPrint.NavigateUrl = "invoice_hotel_manage_print.aspx?pid=" + this.qProductId + "&pay=" + intPaymentID;
                lnPrintBooking.NavigateUrl = "invoice_hotel_manage_print_booking_list.aspx?pid=" + this.qProductId + "&pay=" + intPaymentID;
                panelPaymentSummary.Visible = false;
                panelCompleted.Visible = true;

            }
        }

        
        protected void btnback_Click(object sender, EventArgs e)
        {
            panelPaymentSel.Visible = true;
            panelPaymentSummary.Visible = false;
        }
}
}