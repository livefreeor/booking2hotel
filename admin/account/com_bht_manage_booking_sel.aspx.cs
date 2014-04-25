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
    public partial class admin_account_com_bht_manage_booking_sel : Hotels2BasePage
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
                    IList<object> iListBooking = cComBooking.GetBookingToPaymentList(intProductId);

                    StringBuilder result = new StringBuilder();

                    result.Append("<table id=\"table_result\" class=\"tbl_account\" width=\"100%\" cellspacing=\"1\" cellpadding=\"0\">");
                    result.Append("<thead>");
                    result.Append("<tr  style=\"text-align:center;\">");
                    result.Append("<th><input type=\"checkbox\" id=\"main_check\" /></th>");
                    result.Append("<th>No.</th><th>Booking No.</th><th>Name</th><th>Check In</th><th>Check Out</th>");
                    result.Append("<th>Price</th><th>Com</th><th>Net Price</th>");
                    result.Append("</tr>");
                    result.Append("</thead>");
                    result.Append("<tbody>");
                    if (iListBooking.Count > 0)
                    {
                        int countDue = 0;
                        int countNotDue = 0;

                        result.Append("<tr class=\"total\"><td colspan=\"9\" align=\"center\">In DueDate</td></tr>");

                        foreach (Com_Booking_list com in iListBooking)
                        {
                            if (com.DateCheckin.Subtract(DateTime.Now.Hotels2ThaiDateTime()).Days <= com.DuePayment)
                            {
                                Account_Commission_Engine cCombht = new Account_Commission_Engine(com.BookingId, com.ComVal, com.ComCat); 
                             
                                cCombht.CalculateCommissionBhtManage();
                               
                                countDue = countDue + 1;
                                if(countDue%2 == 0)
                                    result.Append("<tr class=\"row_odd\">");
                                else
                                    result.Append("<tr >");
                               
                               

                                result.Append("<td align=\"center\"><input type=\"checkbox\" name=\"booking_checked\"  value=\"" + com.BookingId + "\" /></td>");
                                result.Append("<td align=\"center\">" + countDue + "</td>");
                                result.Append("<td align=\"center\"><a href=\"/admin/account/account_booking_detail.aspx?bid=" + com.BookingId + "\" target=\"_blank\" />" + com.BookingHotelId + "</a></td>");
                                result.Append("<td>" + com.CustomerName + "</td>");
                                result.Append("<td align=\"center\">" + com.DateCheckin.ToString("dd-MMM-yyyy") + "</td>");
                                result.Append("<td align=\"center\">" + com.DateCheckOut.ToString("dd-MMM-yyyy") + "</td>");
                                result.Append("<td align=\"center\">" + cCombht.Price.ToString("#,##0.00") + "</td>");
                                result.Append("<td align=\"center\">" + cCombht.PriceCom.ToString("#,##0.00") + "</td>");
                                result.Append("<td align=\"center\">" + cCombht.PriceSup.ToString("#,##0.00") + "</td>");
                                result.Append("</tr>");
                            }
                           
                        }

                        result.Append("<tr class=\"total\"><td colspan=\"9\" align=\"center\">Not Due</td></tr>");

                        foreach (Com_Booking_list com in iListBooking)
                        {
                            if (com.DateCheckin.Subtract(DateTime.Now.Hotels2ThaiDateTime()).Days > com.DuePayment)
                            {

                                countNotDue = countNotDue + 1;
                              
                                Account_Commission_Engine cCombht = new Account_Commission_Engine(com.BookingId, com.ComVal, com.ComCat);
                                cCombht.CalculateCommissionBhtManage();

                                if (countNotDue % 2 == 0)
                                    result.Append("<tr class=\"row_odd\">");
                                else
                                    result.Append("<tr >");
                               
                                
                                    result.Append("<td align=\"center\"><input type=\"checkbox\" name=\"booking_checked\" value=\"" + com.BookingId + "\" /></td>");
                                    result.Append("<td align=\"center\">" + countNotDue + "</td>");
                                    result.Append("<td align=\"center\"><a href=\"/admin/account/account_booking_detail.aspx?bid=" + com.BookingId + "\" target=\"_blank\" />" + com.BookingHotelId + "</a></td>");
                                    result.Append("<td>" + com.CustomerName + "</td>");
                                    result.Append("<td align=\"center\">" + com.DateCheckin.ToString("dd-MMM-yyyy") + "</td>");
                                    result.Append("<td align=\"center\">" + com.DateCheckOut.ToString("dd-MMM-yyyy") + "</td>");
                                    result.Append("<td align=\"center\">" + cCombht.Price.ToString("#,##0.00") + "</td>");
                                    result.Append("<td align=\"center\">" + cCombht.PriceCom.ToString("#,##0.00") + "</td>");
                                    result.Append("<td align=\"center\">" + cCombht.PriceSup.ToString("#,##0.00") + "</td>");
                                    result.Append("</tr>");
                            }
                           
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

            StringBuilder result = new StringBuilder();


            Com_Booking_list cComBooking = new Com_Booking_list();
            string BookingPayment = Request.Form["booking_checked"];

           

            IList<object> iListBooking = cComBooking.GetBookingToPaymentList(BookingPayment);


            result.Append("<table id=\"table_result\" class=\"tbl_account\" width=\"100%\" cellspacing=\"1\" cellpadding=\"0\">");
            result.Append("<thead>");
            result.Append("<tr style=\"text-align:center;\">");
            
            result.Append("<th>No.</th><th>Booking No.</th><th>Name</th><th>Check In</th><th>Check Out</th>");
            result.Append("<th>Price</th><th>Com</th><th>Net Price</th>");
            result.Append("</tr>");
            result.Append("</thead>");
            result.Append("<tbody>");
            if (iListBooking.Count > 0)
            {


                int countDue = 0;

                decimal PriceSupTotal = 0;

                foreach (Com_Booking_list com in iListBooking)
                {
                    //if (com.DateCheckin.Subtract(DateTime.Now.Hotels2ThaiDateTime()).Days <= com.DuePayment)
                    //{
                        Account_Commission_Engine cCombht = new Account_Commission_Engine(com.BookingId, com.ComVal, com.ComCat);
                        cCombht.CalculateCommissionBhtManage();
                        countDue = countDue + 1;

                    if (countDue % 2 == 0)
                            result.Append("<tr class=\"row_odd\">");
                    else
                        result.Append("<tr >");
                               

                        

                        result.Append("<td align=\"center\"><input type=\"checkbox\" name=\"booking_sel_checked\" style=\"display:none;\" checked=\"checked\" value=\"" + com.BookingId + "\" />" + countDue + "</td>");
                        result.Append("<td align=\"center\"><a href=\"/admin/account/account_booking_detail.aspx?bid=" + com.BookingId + "\" target=\"_blank\" />" + com.BookingHotelId + "</a></td>");
                        result.Append("<td>" + com.CustomerName + "</td>");
                        result.Append("<td align=\"center\">" + com.DateCheckin.ToString("dd-MMM-yyyy") + "<input type=\"hidden\" name=\"hd_date_checkin_" + com.BookingId + "\" value=\"" + com.DateCheckin.ToString("yyyy-MM-dd") + "\" ></td>");
                        result.Append("<td align=\"center\">" + com.DateCheckOut.ToString("dd-MMM-yyyy") + "<input type=\"hidden\" name=\"hd_date_checkout_" + com.BookingId + "\" value=\"" + com.DateCheckOut.ToString("yyyy-MM-dd") + "\" ></td>");

                        result.Append("<td align=\"right\">" + cCombht.Price.ToString("#,##0.00") + "<input type=\"hidden\" name=\"hd_price_amount_" + com.BookingId + "\" value=\"" + cCombht.Price + "\" ></td>");
                        result.Append("<td align=\"right\">" + cCombht.PriceCom.ToString("#,##0.00") + "<input type=\"hidden\" name=\"hd_priceCOm_amount_" + com.BookingId + "\" value=\"" + cCombht.PriceCom + "\" ></td>");
                        result.Append("<td align=\"right\">" + cCombht.PriceSup.ToString("#,##0.00") + "<input type=\"hidden\" name=\"hd_priceSUp_amount_" + com.BookingId + "\" value=\"" + cCombht.PriceSup + "\" ><input type=\"hidden\" name=\"hd_com_cat_" + com.BookingId + "\" value=\"" + com.ComCat + "\" ><input type=\"hidden\" name=\"hd_com_val_" + com.BookingId + "\" value=\"" + com.ComVal + "\" ></td>");
                        result.Append("</tr>");

                        PriceSupTotal = PriceSupTotal + cCombht.PriceSup;
                    //}


                }

                result.Append("<tr class=\"total\"><td  colspan=\"7\" align=\"right\">รวมจำนวนเงิน</td><td align=\"right\" style=\"font-weight:bold;\">" + PriceSupTotal.ToString("#,##0.00") + "</td></tr>");

            }
            else
            {
                result.Append("<tr><td class=\"total\" colspan=\"5\">No hotel to Payment</td></tr>");
            }

            result.Append("</tbody>");
            result.Append("</table>");


            ltPaylistSum.Text = result.ToString();


            int intProductId = int.Parse(this.qProductId);
            Account_deposit cDep = new Account_deposit();
            IList<object> iListDep = cDep.getDepositByProductID_available(intProductId);
            gvDeposit.DataSource = iListDep;
            gvDeposit.DataBind();
            if (iListDep.Count > 0)
                gvDeposit.HeaderRow.TableSection = TableRowSection.TableHeader;

            Production.Product cProduct = new Production.Product();
            cProduct = cProduct.GetProductById(intProductId);
            SupplierAccount cSupAcc = new SupplierAccount();

            IList<object> iListHotelBank = cSupAcc.getSupplierAccountAllBySupplierID(cProduct.SupplierPrice);
            GvHotelBank.DataSource = iListHotelBank;
            GvHotelBank.DataBind();
            if (iListHotelBank.Count  >0)
             GvHotelBank.HeaderRow.TableSection = TableRowSection.TableHeader;

            Account_bhtbank cBhtBank = new Account_bhtbank();

            IList<object> iListBhtBank = cBhtBank.getBhtBankAccount();
            GvbhtBank.DataSource = iListBhtBank;
            GvbhtBank.DataBind();
            if (iListBhtBank.Count > 0)
                GvbhtBank.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            panelPaymentSel.Visible = true;
            panelPaymentSummary.Visible = false;
        }

        protected void gvDeposit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int DepID = (int)gvDeposit.DataKeys[e.Row.RowIndex].Value;

                CheckBox checkBox = e.Row.FindControl("chkDepSel") as CheckBox;

                TextBox txtDep = e.Row.FindControl("txtDepUse") as TextBox;

                txtDep.Attributes.Add("OnKeyUp", "DepositTotal();");
                checkBox.Attributes.Add("onclick", "DepositTotal();");
            }
        }
        protected void GvHotelBank_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
                HyperLink ln = e.Row.FindControl("lnedit_hotel_bank") as HyperLink;
                ln.NavigateUrl = "/admin/product/product.aspx?page=pi&pdcid=29&pid=" + this.qProductId;
;            }
        }
        protected void GvbhtBank_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }




        protected void btnPayment_Click(object sender, EventArgs e)
        {
            //Response.Write(Request.Form["booking_sel_checked"]);
            if(!string.IsNullOrEmpty(Request.Form["booking_sel_checked"]) && !string.IsNullOrEmpty(this.qProductId))
            {
                int intPaymentID = 0;
                int intProductId = int.Parse(this.qProductId);
                
                decimal decPriceTotal = 0;
                decimal decComTotal = 0;
                decimal decCostTotel = 0;
                decimal decDepositTotal = 0;
                //default Value KindID = Com hotel Payment (bht manage);
                byte bytKindId = 2;

                if (!string.IsNullOrEmpty(depositSummary.Text))
                    decDepositTotal = decimal.Parse(depositSummary.Text);
          

                string[] arrBookingID = Request.Form["booking_sel_checked"].Split(',');

                //IList<Account_payment_Booking> ilistPaymentBooking = null;
                //IList<Deposit_repay> iListDepositRepay = null;

               
               //Sum Price ,Cost ,Com for hotel Payment
                for (int i = 0; i < arrBookingID.Count(); i++)
                {
                    //Response.Write(Request.Form["hd_price_amount_" + arrBookingID[i]]);
                    int intBookingID = int.Parse(arrBookingID[i]);
                    decPriceTotal = decPriceTotal + decimal.Parse(Request.Form["hd_price_amount_" + arrBookingID[i]]);
                    decComTotal = decComTotal + decimal.Parse(Request.Form["hd_priceCOm_amount_" + arrBookingID[i]]);
                    decCostTotel = decCostTotel + decimal.Parse(Request.Form["hd_priceSUp_amount_" + arrBookingID[i]]);
                   
                }

               //-----------------------------------------

                //hotel Payment Create   
                // BHT MANAGE == 2
                // Commission CAt = FALT RATE == 1

                byte? byeBankId = null;
                string strAccountName = string.Empty;
                string strAccountNum = string.Empty;
                string strAccountBanch = string.Empty;
                string strAccountType = string.Empty;


                foreach (GridViewRow acGvRow in GvHotelBank.Rows)
                {
                    if (acGvRow.RowType == DataControlRowType.DataRow)
                    {
                        RadioButton radio = GvHotelBank.Rows[acGvRow.RowIndex].Cells[0].FindControl("rioHotelBank") as RadioButton;
                        if (radio.Checked)
                        {
                            HiddenField hdBankId = GvHotelBank.Rows[acGvRow.RowIndex].Cells[0].FindControl("hd_bankId") as HiddenField;
                            Label lblAccountName = GvHotelBank.Rows[acGvRow.RowIndex].Cells[2].FindControl("lblAccountName") as Label;
                            Label lblAccountNum = GvHotelBank.Rows[acGvRow.RowIndex].Cells[3].FindControl("lblAccountNum") as Label;
                            Label lblAccountBranch = GvHotelBank.Rows[acGvRow.RowIndex].Cells[4].FindControl("lblAccountBranch") as Label;

                            Label lblAccountType = GvHotelBank.Rows[acGvRow.RowIndex].Cells[4].FindControl("lblAccountType") as Label;

                            byeBankId = byte.Parse(hdBankId.Value);
                            strAccountName = lblAccountName.Text;
                            strAccountNum = lblAccountNum.Text;
                            strAccountBanch = lblAccountBranch.Text;
                            strAccountType = lblAccountType.Text;
                        }
                    }
                }

                Account_payment cAccountPayment = new Account_payment { 
                    ProductId= intProductId,
                    ManageId = 2,
                    
                    StaffId = this.CurrentStaffId,
                    PriceTotal = decPriceTotal,
                    ComTotal = decComTotal,
                    CostTotal = decCostTotel,
                    DateSubmit = DateTime.Now.Hotels2ThaiDateTime(),
                    DepositTotal = decDepositTotal,
                    Status = true,
                    BankId = byeBankId,
                    AccountName = strAccountName,
                    AccountNum = strAccountNum,
                    AccountBranch = strAccountBanch,
                    AccountType = strAccountType
                         
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
                        decimal decCostAmount = decimal.Parse(Request.Form["hd_priceSUp_amount_" + arrBookingID[i]]);
                        
                        byte bytComCat = byte.Parse(Request.Form["hd_com_cat_" + arrBookingID[i]]);
                        decimal decComVal = decimal.Parse(Request.Form["hd_com_val_" + arrBookingID[i]]);

                        // Check Kind for transaction fee if is the Monthly rate of Commission
                        // And Set ComVal = Transaction fee 3%;
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
                            CostAmount = decCostAmount,
                            ComVal = decComVal

                        };

                        cAccountPaymentBooking.InsertAccountPaymentBooking(cAccountPaymentBooking);
                    }


                    // Calculate Deposit 
                    if (decDepositTotal > 0)
                    {
                        foreach (GridViewRow gvRow in gvDeposit.Rows)
                        {
                            if (gvRow.RowType == DataControlRowType.DataRow)
                            {

                                int DepositID = (int)gvDeposit.DataKeys[gvRow.RowIndex].Value;
                                CheckBox chkDep = gvDeposit.Rows[gvRow.RowIndex].Cells[0].FindControl("chkDepSel") as CheckBox;
                                TextBox cDepAmount = gvDeposit.Rows[gvRow.RowIndex].Cells[7].FindControl("txtDepUse") as TextBox;
                                if (chkDep.Checked)
                                {

                                    Deposit_repay cDepRepay = new Deposit_repay
                                    {
                                        PaymentId = intPaymentID,
                                        Amount = decimal.Parse(cDepAmount.Text),
                                        DepositID = DepositID
                                    };
                                    cDepRepay.InsertDepositRepay(cDepRepay);
                                }

                            }
                        }
                    }

                    byte bytAccountID = 0;
                    foreach (GridViewRow gvRowbht in GvbhtBank.Rows)
                    {
                        if (gvRowbht.RowType == DataControlRowType.DataRow)
                        {
                            RadioButton radio = GvbhtBank.Rows[gvRowbht.RowIndex].Cells[0].FindControl("rioBhtbank") as RadioButton;
                            if(radio.Checked)
                            {

                                bytAccountID = (byte)GvbhtBank.DataKeys[gvRowbht.RowIndex].Value; 
                            }
                        }
                    }
                    Account_bht_bank_payment cbhtBank = new Account_bht_bank_payment
                    {
                        AccountID = bytAccountID,
                        PaymentID = intPaymentID
                    };

                    cbhtBank.InsertBankPayment(cbhtBank);
                   
                }

                lnPrint.NavigateUrl = "slip_print.aspx?pid=" + this.qProductId + "&pay=" + intPaymentID;
                panelPaymentSummary.Visible = false;
                panelCompleted.Visible = true;

              
                
            }
            
           
        }


}
}
