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



public partial class admin_account_invoice_hotel_manage_print : System.Web.UI.Page
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

        StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath("/admin/account/account_paper_temp/bht_invoice_comission.html"));
        Tmp = objReader.ReadToEnd();
        objReader.Close();



        //Account_payment cAccountPayment = new Account_payment();
        //cAccountPayment = cAccountPayment.getAccountPayment(intPaymentID);
        Product cProduct = new Product();
        ProductContent cProductContent = new ProductContent();
       // ProductBookingEngine cProductEngine = new ProductBookingEngine();
        int inProductid = int.Parse(this.qProductId);
        cProduct = cProduct.GetProductById(inProductid);
        cProductContent = cProductContent.GetProductContentById(inProductid, 1);
        //cProductEngine = cProductEngine.GetProductbookingEngine(inProductid);
        // string BankTitle = SupplierAccount.getBanktitlebyId((byte)cAccountPayment.BankId);
        Account_payment cPayment = new Account_payment();
        cPayment = cPayment.getAccountPayment(intPaymentID);

        Staff cStaff = new Staff();
        cStaff = cStaff.getStaffById(cPayment.StaffId);

        Tmp = Tmp.Replace("<!--##@PaymentID##-->", intPaymentID.ToString());
        Tmp = Tmp.Replace("<!--##@DatePayment##-->", cPayment.DateSubmit.ToString("MMM dd, yyyy"));
        Tmp = Tmp.Replace("<!--##@HotelName##-->", cProductContent.Title);
        Tmp = Tmp.Replace("<!--##@HotelAddress##-->", cProductContent.Address + "<br/>" + cProduct.ProductPhone + "<br/>email:" + cProduct.ProductEmail);
        Tmp = Tmp.Replace("<!--##@StaffName##-->", cStaff.Title);
        //Tmp = Tmp.Replace("<!--##@BankName##-->", BankTitle);
        //Tmp = Tmp.Replace("<!--##@AccountName##-->", cAccountPayment.AccountName);
        //Tmp = Tmp.Replace("<!--##@BankNumber##-->", cAccountPayment.AccountNum);
        //Tmp = Tmp.Replace("<!--##@BankBranch##-->", cAccountPayment.AccountBranch);


        Account_payment_Booking cAccountPaymentBooking = new Account_payment_Booking();


        StringBuilder strPayment = new StringBuilder();
        strPayment.Append("");
        //decimal NetPriceTotal = 0;
        //int Count = 1;
        BookingProductDisplay cBookingProduct = new BookingProductDisplay();

       // byte bytIsVat = byte.Parse(raidoVat.SelectedValue);


        decimal NetPriceTotal = (cPayment.ComTotal.HasValue? (decimal)cPayment.ComTotal : 0);
        decimal decVatIncludeComTotal = NetPriceTotal +( cPayment.VatTotal.HasValue? (decimal)cPayment.VatTotal:0);

        
        decimal decTaxTotal = (cPayment.WithholdingTaxTotal.HasValue ? (decimal)cPayment.WithholdingTaxTotal : 0);

        strPayment.Append("<tr>");
        strPayment.Append("<td align=\"left\"><strong>Booking Amount</strong></td>");
        strPayment.Append("<td align=\"right\">" + cPayment.PriceTotal.ToString("#,##0.00") + "</td>");


        strPayment.Append("</tr>");
        strPayment.Append("<tr>");
        strPayment.Append("<td align=\"center\"></td>");
        strPayment.Append("<td align=\"right\"></td>");


        strPayment.Append("</tr>");

        strPayment.Append("<tr>");
        strPayment.Append("<td align=\"left\"><strong>Comission Fee</strong></td>");
        strPayment.Append("<td align=\"right\">" + (NetPriceTotal).ToString("#,##0.00") + "</td>");

        strPayment.Append(" </tr>");
        if (cPayment.Vat.HasValue)
        {
            strPayment.Append(" <tr>");
            strPayment.Append(" <td align=\"left\">รวมเงิน/ Sub total</td>");
            strPayment.Append(" <td align=\"right\">" + (NetPriceTotal).ToString("#,##0.00") + "</td>");

            strPayment.Append(" </tr>");

            strPayment.Append("<tr>");
            strPayment.Append("<td align=\"left\">ภาษีมูลค่าเพิ่ม /Vat</td>");
            strPayment.Append(" <td align=\"right\">" + ((decimal)cPayment.VatTotal).ToString("#,##0.00") + "</td>");

            strPayment.Append("</tr>");
            strPayment.Append("<tr>");
            strPayment.Append("<td align=\"left\">ยอดรวมทั้งหมดรวมภาษีมูลค่าเพิ่ม " + cPayment.Vat + "%/Grand Total</td>");
            strPayment.Append("<td align=\"right\">" + decVatIncludeComTotal.ToString("#,##0.00") + "</td>");

            strPayment.Append("</tr>");

            NetPriceTotal =  decVatIncludeComTotal;

            StringBuilder str = new System.Text.StringBuilder();
            if (cPayment.WithholdingTax.HasValue)
            {
                str.Append(" <tr><td style=\"height:10px\"></td></tr>");
                str.Append(" <tr><td align=\"center\">");
                str.Append(" <table width=\"90%\" cellspacing=\"1\" cellpadding=\"0\">");
                str.Append(" <tr>");
                str.Append("  <td>ทั้งนี้ทางโรงแรมจะต้องทำการหักภาษี ณ ที่จ่าย บริษัทฯ " + cPayment.WithholdingTax + "% โดยออกเอกสารการหักภาษี ณ ที่จ่ายให้บริษัทฯ");
                str.Append(" <br />");
                str.Append("<strong>" + decTaxTotal.ToString("#,##0.00") + " บาท</strong></td>");
                str.Append("  </tr>");
                str.Append("  </table>");
                str.Append(" </td></tr>");

                string KeywordDepBooking = Utility.GetKeywordReplace(Tmp, "<!--##@PaymenttaxStart##-->", "<!--##@PaymenttaxEnd##-->");
                Tmp = Tmp.Replace(KeywordDepBooking, str.ToString());

                NetPriceTotal = NetPriceTotal - decTaxTotal;

            }
            else
            {
                string KeywordDepBooking = Utility.GetKeywordReplace(Tmp, "<!--##@PaymenttaxStart##-->", "<!--##@PaymenttaxEnd##-->");
                Tmp = Tmp.Replace(KeywordDepBooking, str.ToString());
            }


        }
        else
        {
            strPayment.Append(" <tr>");
            strPayment.Append(" <td align=\"left\">รวมเงิน/ Sub total</td>");
            strPayment.Append(" <td align=\"right\">" + (NetPriceTotal).ToString("#,##0.00") + "</td>");

            strPayment.Append(" </tr>");
        }
        strPayment.Append("<tr>");
        strPayment.Append(" <td align=\"left\"></td>");
        strPayment.Append(" <td align=\"right\"></td>");

        strPayment.Append("</tr>");



        string KeywordPaymentBooking = Utility.GetKeywordReplace(Tmp, "<!--##@PaymentBookingListStart##-->", "<!--##@PaymentBookingListEnd##-->");
        Tmp = Tmp.Replace(KeywordPaymentBooking, strPayment.ToString());

        Tmp = Tmp.Replace("<!--##@NetTotal##-->", NetPriceTotal.ToString("#,##0.00"));



        StringBuilder strPolicy = new System.Text.StringBuilder();

        if (cPayment.Vat.HasValue)
        {
            strPolicy.Append("<table>");
            strPolicy.Append("<tr>");
            strPolicy.Append("<td style=\"font-weight:bold;\">1.</td>");
            strPolicy.Append("<td>โอนเงินบัญชี บริษัท บูลเฮ้าส์ ทราเวล จำกัด ธนาคารกสิกรไทย สาขา เซ็นทรัลลาดพร้าว เลขที่ 730-2-37133-9 ประเภทออมทรัพย์</td>");
            strPolicy.Append("</tr>");
            strPolicy.Append(" <tr>");
            strPolicy.Append("<td style=\"font-weight:bold;\">2.</td>");
            strPolicy.Append("<td>ชำระด้วยเช็คขีดคร่อมในนาม \"บริษัท บลูเฮาส์ ทราเวล จำกัด\" <br/>และจัดส่งมาที่ บริษัท บลูเฮ้าส์ ทราเวล จำกัด เลขที่ 254/10 ซ.รัชดาภิเษก 42 แขวงลาดยาว เขตจตุจักร กทม. 10900</td>");
            strPolicy.Append("</tr>");

            strPolicy.Append("</table>");
        }
        else
        {
            strPolicy.Append("<table>");
            strPolicy.Append("<tr>");
            //strPolicy.Append("<td style=\"font-weight:bold;\">1.</td>");

            if (cPayment.BankId == 5)
                strPolicy.Append("<td>โอนเงินบัญชี บริษัท บูลเฮ้าส์ ทราเวล จำกัด ธนาคารกสิกรไทย สาขา เซ็นทรัลลาดพร้าว เลขที่ 730-2-37133-9 ประเภทออมทรัพย์</td>");
            else
                strPolicy.Append("<td>โอนเงินบัญชี บริษัท บูลเฮ้าส์ ทราเวล จำกัด ธนาคารไทยพาณิชย์ สาขา เซ็นทรัลลาดพร้าว 2 เลขที่ 206-2-08796-9 ประเภทออทรัพย์</td>");

            strPolicy.Append("</tr>");

            strPolicy.Append("</table>");
        }

        strPolicy.Append("<table style=\"margine:20px 0 0 0 ;\">");
        strPolicy.Append("<tr>");
        strPolicy.Append("<td>***ในกรณีที่มีการชำระเงินด้วยการโอนเงินผ่านธนาคาร ทางโรงแรมจะเป็นผู้รับผิดชอบสำหรับค่าโอน ไม่ว่ากรณีใดๆทั้งสิ้น</td>");

        strPolicy.Append("</tr>");

        strPolicy.Append("</table>");

        string KeywordPaymentBookingpolicy = Utility.GetKeywordReplace(Tmp, "<!--##@PaymentPolicyStart##-->", "<!--##@PaymentPolicyEnd##-->");
        Tmp = Tmp.Replace(KeywordPaymentBookingpolicy, strPolicy.ToString());

        return Tmp;

    }
}