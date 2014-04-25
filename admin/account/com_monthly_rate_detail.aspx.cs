using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Account;
using Hotels2thailand.Production;
using Hotels2thailand.Front;
using System.Text;

namespace Hotels2thailand.UI
{
    public partial class admin_account_com_monthly_rate_detail : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Account_payment cAccountPayment = new Account_payment();
                int intProductId = int.Parse(this.qProductId);
                IList<object> iListPayment = cAccountPayment.getAccountPaymentByProductId(intProductId);

                gvPaymentList.DataSource = iListPayment;
                gvPaymentList.DataBind();

                if(iListPayment.Count > 0)
                    gvPaymentList.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }

        protected void lnnewinvoid_Click(object sender, EventArgs e)
        {
            panel_invoid_detail.Visible = true;
        }


        protected void btnMakeinvoid_Click(object sender, EventArgs e)
        {

            string strMonthsDetail = string.Empty;
            string strVatInclude = radioISVat.SelectedValue;
            int intProductId = int.Parse(this.qProductId);
            int intPaymentID = 0;
            ProductBookingEngineCommission cRevenue = new ProductBookingEngineCommission();
            cRevenue = cRevenue.GetCommission(intProductId);

            ProductBookingEngine cProductEngine = new ProductBookingEngine();
            cProductEngine = cProductEngine.GetProductbookingEngine(intProductId);


            strMonthsDetail = datePicker.GetDatetStart.ToString("dd MMM yyyy") + "-" + datePicker.GetDatetEnd.ToString("dd MMM yyyy");
            decimal decComTotal = cRevenue.Commission;

           
            if (cProductEngine.ComNum > 1)
                decComTotal = decComTotal * cProductEngine.ComNum;



            decimal decComVat =(decComTotal *7)/100;

            decimal decComCharge =   (decComTotal *3)/100; 
            decimal decBalance = (decComTotal + decComVat) - decComCharge;

            if (radioISVat.SelectedValue == "1")
            {
                Account_payment cAccountPayment = new Account_payment
                {
                    ProductId = intProductId,
                    ManageId = cProductEngine.ManageId,
                    ComVal = decComTotal,
                    ComCat = 2,
                    StaffId = this.CurrentStaffId,
                    PriceTotal = 0,
                    DateSubmit = DateTime.Now.Hotels2ThaiDateTime(),
                    Status = true,
                    MonthsDetail = strMonthsDetail,
                    Vat = 7,
                    VatTotal = decComVat,
                    WithholdingTax= 3,
                    WithholdingTaxTotal = decComCharge

                };
                intPaymentID = cAccountPayment.InsertPayment(cAccountPayment);
            }
            else
            {
                Account_payment cAccountPayment = new Account_payment
                {
                    ProductId = intProductId,
                    ManageId = cProductEngine.ManageId,
                    ComVal = decComTotal,
                    ComCat = 2,
                    StaffId = this.CurrentStaffId,
                    PriceTotal = 0,
                    DateSubmit = DateTime.Now.Hotels2ThaiDateTime(),
                    Status = true,
                    MonthsDetail = strMonthsDetail

                };
                intPaymentID = cAccountPayment.InsertPayment(cAccountPayment);
            }

            

            if (intPaymentID > 0)
                Response.Redirect(Request.Url.ToString());
                //Response.Redirect("slip_monthly_print.aspx?pid=" + this.qProductId + "&pay=" + intPaymentID);


        }


        protected void gvPaymentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //lblConfirmPayment

            if (e.Row.RowType == DataControlRowType.DataRow) 
            {
                Label lblIsConfirm = e.Row.Cells[5].FindControl("lblConfirmPayment") as Label;
                lblIsConfirm.Text = "";

                DateTime? dDatePayment = (DateTime?)DataBinder.Eval(e.Row.DataItem, "DatePayment");

                if (dDatePayment.HasValue)
                    lblIsConfirm.Text = ((DateTime)dDatePayment).ToString("dd MMM yyyy");
                else
                    lblIsConfirm.Text = "ยังไม่ได้ชำระ";
            }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            int intPaymentId = int.Parse(btn.CommandArgument);

            Account_payment cAcPayment = new Account_payment();
            if (cAcPayment.CancelPayment(intPaymentId))
                Response.Redirect(Request.Url.ToString());
        }
        protected void btnPaid_Click(object sender, EventArgs e)
        {
            bool bolCompleted = false;
            Button btn = (Button)sender;

            //decimal decFee = 0;
            int intPaymentId = int.Parse(btn.CommandArgument);

            //GridViewRow ParentRow = (GridViewRow)btn.Parent.Parent.Parent.Parent.Parent.Parent;

            //GridView GvCurrent = GvBhtAccount.Rows[ParentRow.RowIndex].Cells[0].FindControl("GvbhtBank") as GridView;

            //byte AccountId = (byte)GvBhtAccount.DataKeys[ParentRow.RowIndex].Value;

            Account_payment cAcPayment = new Account_payment();
            //Deposit_repay cDepRepay = new Deposit_repay();
            //Account_bht_bank_payment cbhtPayment = new Account_bht_bank_payment();

           // TextBox txtFee = GvCurrent.FooterRow.FindControl("txtFeeAmount") as TextBox;
            //decFee = decimal.Parse(txtFee.Text);




            bolCompleted = cAcPayment.MakePaymentCompleted(intPaymentId, DateTime.Now.Hotels2ThaiDateTime());
            if (bolCompleted)
                Response.Redirect(Request.Url.ToString());
            
            
        }
}
}