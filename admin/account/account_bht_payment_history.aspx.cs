using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Account;
using Hotels2thailand.Booking;

namespace Hotels2thailand.UI
{
    public partial class admin_account_account_bht_payment_history : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                Account_bhtbank cBhtBank = new Account_bhtbank();
                //bolCompleted.Text = DateTime.Now.Hotels2ThaiDateTime().ToString("dd MMM yyyy");

                IList<object> iLisAcc = cBhtBank.getBhtBankAccountCurrentPaymentPaid();

                GvBhtAccount.DataSource = iLisAcc;
                GvBhtAccount.DataBind();
                if(iLisAcc.Count > 0)
                 GvBhtAccount.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }


        protected void GvBhtAccount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = e.Row.Cells[0].FindControl("GvbhtBank") as GridView;
                byte bytAccountId = (byte)GvBhtAccount.DataKeys[e.Row.RowIndex].Value;

                Label lblAccount = e.Row.Cells[0].FindControl("lblAccountTitle") as Label;

                string strBankName = DataBinder.Eval(e.Row.DataItem, "BankTitle").ToString();
                string strBankNum = DataBinder.Eval(e.Row.DataItem, "AccountNum").ToString();
                string strBankBranch = DataBinder.Eval(e.Row.DataItem, "AccountBranch").ToString();
                string strBankType = DataBinder.Eval(e.Row.DataItem, "AccountTypeTitle").ToString();

                lblAccount.Text = strBankName + "   " + strBankNum + "(" + strBankType + ")" + " " + strBankBranch;

                Account_payment cPayment = new Account_payment();
                IList<object> iListPaybht = cPayment.getAccountPaymentBybhtAccountIDHistory(bytAccountId);
                gv.DataSource = iListPaybht;
               gv.DataBind();
               if (iListPaybht.Count > 0)
               {
                   gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                   //gv.HeaderRow.TableSection = TableRowSection.TableBody;
                   gv.FooterRow.TableSection = TableRowSection.TableFooter;
               }
            }
        }

        private decimal decCostotal = 0;
        private decimal decFeeTotal = 0;
        private int count = 0;
        protected void GvbhtBank_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow )
            {
                decimal? objDeposit = (decimal)DataBinder.Eval(e.Row.DataItem, "DepositTotal");
                decimal? decCostAmont = (decimal)DataBinder.Eval(e.Row.DataItem, "CostTotal");

                if (decCostAmont.HasValue)
                {
                    if (objDeposit.HasValue)
                        decCostAmont = ((decimal)decCostAmont) - (decimal)objDeposit;
                }

                count += 1;
                if (count % 2 == 0)
                    e.Row.BackColor = System.Drawing.Color.GhostWhite;
               // e.Row.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
                decimal decFee = (decimal)DataBinder.Eval(e.Row.DataItem, "Fee");
                decFeeTotal += decFee;

               
                Label lblTotal = e.Row.Cells[2].FindControl("lblAmount") as Label;

                lblTotal.Text = ((decimal)decCostAmont).ToString("#,##0.00");

                decCostotal += ((decimal)decCostAmont);
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Attributes.Add("class", "total");
                e.Row.Cells[0].ColumnSpan = 3;

                e.Row.Cells[2].ColumnSpan = 3;
                //e.Row.Cells[3].ColumnSpan = 4;
                //now make up for the colspan from cell2
                //e.Row.Cells.RemoveAt(4);
                Label lblTotalCost = e.Row.FindControl("lblTotalCost") as Label;
                Label lblTotaFee = e.Row.FindControl("lblFeeamount") as Label;
                lblTotaFee.Text = decFeeTotal.ToString("#,##0.00");
                lblTotalCost.Text = decCostotal.ToString("#,##0.00");
                
            }
        }



        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    Button btn = (Button)sender;

        //    int intPaymentId = int.Parse(btn.CommandArgument);

        //    Account_payment cAcPayment = new Account_payment();
        //    if (cAcPayment.CancelPayment(intPaymentId))
        //        Response.Redirect(Request.Url.ToString());
        //}

        //protected void btnPaid_Click(object sender, EventArgs e)
        //{
        //    bool bolCompleted = false;
        //    Button btn = (Button)sender;

        //    decimal decFee = 0;
        //    int intPaymentId = int.Parse(btn.CommandArgument);

        //    GridViewRow ParentRow = (GridViewRow)btn.Parent.Parent.Parent.Parent.Parent.Parent;

        //    GridView GvCurrent = GvBhtAccount.Rows[ParentRow.RowIndex].Cells[0].FindControl("GvbhtBank") as GridView;
            
        //    byte AccountId = (byte)GvBhtAccount.DataKeys[ParentRow.RowIndex].Value;

        //    Account_payment cAcPayment = new Account_payment();
        //    Deposit_repay cDepRepay = new Deposit_repay();
        //    Account_bht_bank_payment cbhtPayment = new Account_bht_bank_payment();
        //    BookingConfirmEngine cConfirm = new BookingConfirmEngine();
        //    Account_payment_Booking cAcPayBooking = new Account_payment_Booking();
        //    BookingActivityDisplay cActivity = new BookingActivityDisplay();

        //    IList<object> PayBookingList = cAcPayBooking.getPaymentBookingList(intPaymentId);


        //    //TextBox txtFee = GvCurrent.FooterRow.FindControl("txtFeeAmount") as TextBox;
        //    //decFee = decimal.Parse(txtFee.Text);


        //    cAcPayment = cAcPayment.getAccountPayment(intPaymentId);


        //    bolCompleted = cAcPayment.MakePaymentCompleted(intPaymentId, DateTime.Now.Hotels2ThaiDateTime());
        //    if (bolCompleted)
        //    {
        //        if (cAcPayment.DepositTotal.HasValue)
        //        {
        //            if ((decimal)(cAcPayment.DepositTotal) != 0)
        //            {
        //                bolCompleted = cDepRepay.MakeDepositUsed(intPaymentId, DateTime.Now.Hotels2ThaiDateTime());
        //            }
        //        }


        //       bolCompleted =  cbhtPayment.MakebhtPaymentCompleted(AccountId, intPaymentId, decFee, DateTime.Now.Hotels2ThaiDateTime());
        //       int ret1 = 0;
        //       int ret2 = 0;

        //       foreach (Account_payment_Booking cAcBook in PayBookingList)
        //       {


        //           ret1 = cConfirm.UpdateConfirmByCat(cAcBook.BookingID, cAcBook.ClassBookingDetail.BookingProductId, 10);

        //          ret2 = cActivity.InsertAutoActivity(BookingActivityType.ConfirmPaymentSup, cAcBook.BookingID);

        //          if (ret1 == 1 && ret2 == 1)
        //               bolCompleted = true;

        //       }

        //       if (bolCompleted)
        //           Response.Redirect(Request.Url.ToString());


        //    }
        //}
}


}