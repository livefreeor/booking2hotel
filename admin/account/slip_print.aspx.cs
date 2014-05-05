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
using System.Text;


public partial class admin_account_slip_print : System.Web.UI.Page
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

        StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath("/admin/account/account_paper_temp/bht_invoice.html"));
        Tmp = objReader.ReadToEnd();
        objReader.Close();

        Account_payment cAccountPayment = new Account_payment();
        //ProductBookingEngine cProductEngine = new ProductBookingEngine();
        cAccountPayment = cAccountPayment.getAccountPayment(intPaymentID);
        Product cProduct = new Product();
        ProductContent cProductContent = new ProductContent();
        int inProductid = int.Parse(this.qProductId);
        cProduct = cProduct.GetProductById(inProductid);
        cProductContent = cProductContent.GetProductContentById(inProductid, 1);
       // cProductEngine = cProductEngine.GetProductbookingEngine(inProductid);
        string BankTitle = SupplierAccount.getBanktitlebyId((byte)cAccountPayment.BankId);


        Tmp = Tmp.Replace("<!--##@PaymentID##-->", intPaymentID.ToString());
        Tmp = Tmp.Replace("<!--##@DatePayment##-->", cAccountPayment.DateSubmit.ToString("MMM dd, yyyy"));
        Tmp = Tmp.Replace("<!--##@HotelName##-->", cProductContent.Title);
        Tmp = Tmp.Replace("<!--##@HotelAddress##-->", cProductContent.Address + "<br/>" + cProduct.ProductPhone + "<br/>email:" + cProduct.ProductEmail);

        Tmp = Tmp.Replace("<!--##@BankName##-->", BankTitle);
        Tmp = Tmp.Replace("<!--##@AccountName##-->", cAccountPayment.AccountName);
        Tmp = Tmp.Replace("<!--##@BankNumber##-->", cAccountPayment.AccountNum);
        Tmp = Tmp.Replace("<!--##@BankBranch##-->", cAccountPayment.AccountBranch);


        Account_payment_Booking cAccountPaymentBooking = new Account_payment_Booking();


        StringBuilder strPayment = new StringBuilder();
        strPayment.Append("");
        decimal NetPriceTotal = 0;
        int Count = 1;
        BookingProductDisplay cBookingProduct = new BookingProductDisplay();

        foreach (Account_payment_Booking payBook in cAccountPaymentBooking.getPaymentBookingList(intPaymentID))
        {

            BookingdetailDisplay cBook = payBook.ClassBookingDetail;
            cBookingProduct = cBookingProduct.getBookingProductDisplayByBookingProductId(cBook.BookingProductId);

            strPayment.Append("<tr>");
            strPayment.Append("<td align=\"center\">" + Count + ".</td>");
            strPayment.Append("<td align=\"center\">" + cBook.BookingHotelId + "/"+cBook.BookingId+"</td>");
            strPayment.Append("<td align=\"left\">" + cBook.FullName + "</td>");
            strPayment.Append("<td align=\"center\">" + ((DateTime)cBookingProduct.DateTimeCheckIn).ToString("dd MMM yyyy") + "</td>");
            strPayment.Append("<td align=\"center\">" + ((DateTime)cBookingProduct.DateTimeCheckOut).ToString("dd MMM yyyy") + "</td>");
            strPayment.Append("<td align=\"right\">" + ((decimal)payBook.PriceAmount).ToString("#,##0.00") + "</td>");
            strPayment.Append("<td align=\"right\">" + ((decimal)payBook.ComAmount).ToString("#,##0.00") + "</td>");
            strPayment.Append("<td align=\"right\">" + ((decimal)payBook.CostAmount).ToString("#,##0.00") + "</td>");
            strPayment.Append(" </tr>");

            NetPriceTotal = NetPriceTotal + (decimal)payBook.CostAmount;
            Count = Count + 1;
        }



        string KeywordPaymentBooking = Utility.GetKeywordReplace(Tmp, "<!--##@PaymentBookingListStart##-->", "<!--##@PaymentBookingListEnd##-->");
        Tmp = Tmp.Replace(KeywordPaymentBooking, strPayment.ToString());

        Tmp = Tmp.Replace("<!--##@CostTotal##-->", NetPriceTotal.ToString("#,##0.00"));


        StringBuilder strDep = new System.Text.StringBuilder();
        Deposit_repay cDeprepay = new Deposit_repay();
        IList<object> iList = cDeprepay.getDepRepayListByPaymentID(cAccountPayment.PaymentID);

        //Response.Write(cAccountPayment.PaymentID);
        ////Response.Write(repay.ClassDeposit.ClassBookingDetail.BookingProductId);
        //Response.End();

        decimal decDepTotal = 0;

        if (iList.Count() > 0)
        {
            foreach (Deposit_repay repay in iList)
            {

               
                //cBookingProduct = cBookingProduct.getBookingProductDisplayByBookingProductId(repay.ClassDeposit.ClassBookingDetail.BookingProductId);

                strDep.Append("<tr>");
                strDep.Append("<td colspan=\"3\" align=\"left\">เครดิตคงค้างที่ยังรอตัด Booking No. " + repay.ClassDeposit.ClassBookingDetail.BookingHotelId + "</td>");
                strDep.Append("<td align=\"center\">" + ((DateTime)repay.ClassDeposit.DateCheckIn).ToString("dd MMM yyyy") + "</td>");
                strDep.Append("<td align=\"center\">" + ((DateTime)repay.ClassDeposit.DateCheckout).ToString("dd MMM yyyy") + "</td>");
                strDep.Append("<td align=\"right\" colspan=\"2\">เป็นเงิน Deposit คงค้าง&nbsp;</td>");
                strDep.Append("<td align=\"right\">" + repay.Amount.ToString("#,##0.00") + "</td>");
                strDep.Append("</tr>");
                decDepTotal = decDepTotal + repay.Amount;
            }

            strDep.Append("<tr class=\"total\"><td colspan=\"7\" align=\"right\"> GRAND TOTAL&nbsp;</td><td align=\"right\">" + (NetPriceTotal - decDepTotal).ToString("#,##0.0") + "</td></tr>");
            string KeywordDepBooking = Utility.GetKeywordReplace(Tmp, "<!--##@PaymentDepositStart##-->", "<!--##@PaymentDepositEnd##-->");
            Tmp = Tmp.Replace(KeywordDepBooking, strDep.ToString());

            Tmp = Tmp.Replace("<!--##@TotalWord##-->", "TOTAL");
        }
        else
        {
            string KeywordDepBooking = Utility.GetKeywordReplace(Tmp, "<!--##@PaymentDepositStart##-->", "<!--##@PaymentDepositEnd##-->");
            Tmp = Tmp.Replace(KeywordDepBooking, strDep.ToString());
            Tmp = Tmp.Replace("<!--##@TotalWord##-->", "GRAND TOTAL");
        }


        return Tmp;

    }
}