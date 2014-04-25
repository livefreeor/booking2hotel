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



public partial class admin_account_invoice_monthly_print : System.Web.UI.Page
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

        string strMonthsDetail = string.Empty;
       
        int intProductId = int.Parse(this.qProductId);
        
        //ProductBookingEngineCommission cRevenue = new ProductBookingEngineCommission();
        //cRevenue = cRevenue.GetCommission(intProductId);
        ProductContent cProductContent = new ProductContent();
        Product cProduct = new Product();
        Account_payment cAccountPaymentSel = new Account_payment();
        //ProductBookingEngine cProductEngine = new ProductBookingEngine();

        Staff cStaff = new Staff();
        cAccountPaymentSel = cAccountPaymentSel.getAccountPayment(intPaymentID);

        cStaff = cStaff.getStaffById(cAccountPaymentSel.StaffId);
        
        int inProductid = int.Parse(this.qProductId);
        cProduct = cProduct.GetProductById(inProductid);
        cProductContent = cProductContent.GetProductContentById(inProductid, 1);
        
        //cProductEngine = cProductEngine.GetProductbookingEngine(inProductid);

       // strMonthsDetail = datePicker.GetDatetStart.ToString("dd MMM yyyy") + "-" + datePicker.GetDatetEnd.ToString("dd MMM yyyy");

        decimal decComTotal = (decimal)cAccountPaymentSel.ComVal;
        decimal decComVat = (decComTotal * 7) / 100;

        decimal decComCharge = (decComTotal * 3) / 100;
        decimal decBalance = (decComTotal + decComVat) - decComCharge;

        


        string ComTotal = decComTotal.ToString("#,##0.00");
        string ComVat = decComVat.ToString("#,##0.00");
        string ComCh = decComCharge.ToString("#,##0.00");
        string Balance = decBalance.ToString("#,##0.00");

        string Tmp = string.Empty;

        StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath("/admin/account/account_paper_temp/bht_invoice_monthly.html"));
        Tmp = objReader.ReadToEnd();
        objReader.Close();

      
        Tmp = Tmp.Replace("<!--##@PaymentID##-->", intPaymentID.ToString());
        Tmp = Tmp.Replace("<!--##@DatePayment##-->", cAccountPaymentSel.DateSubmit.ToString("MMM dd, yyyy"));
        Tmp = Tmp.Replace("<!--##@HotelName##-->", cProductContent.Title);
        Tmp = Tmp.Replace("<!--##@HotelAddress##-->", cProductContent.Address + "<br/>" + cProduct.ProductPhone + "<br/>email:" + cProduct.ProductEmail);

        Tmp = Tmp.Replace("<!--##@StaffName##-->", cStaff.Title);

        StringBuilder result = new StringBuilder();
        StringBuilder strPolicy = new StringBuilder();
        result.Append("<tr>");
        result.Append("<td align=\"center\">" + strMonthsDetail + "</td>");
        result.Append("<td align=\"right\">" + ComTotal + "</td>");
        result.Append("<td align=\"center\"></td>");
        result.Append("</tr>");

        result.Append("<tr>");
        result.Append("<td align=\"center\"></td>");
        result.Append("<td align=\"right\"></td>");
        result.Append("<td align=\"left\"></td>");

        result.Append("</tr>");
        result.Append("<tr>");
        result.Append("<td align=\"center\"></td>");
        result.Append(" <td align=\"right\"></td>");
        result.Append("<td align=\"left\"></td>");
        result.Append("</tr>");
        result.Append("<tr>");
        result.Append("<td align=\"center\"><strong>Total</strong></td>");
        result.Append(" <td align=\"right\">" + ComTotal + "</td>");
        result.Append("<td align=\"left\"></td>");
        result.Append("</tr>");
        if (cAccountPaymentSel.Vat.HasValue && cAccountPaymentSel.VatTotal.HasValue )
        {


            result.Append("<tr>");
            result.Append("<td align=\"left\">บวกภาษีมูลค่าเพิ่ม</td>");
            result.Append("<td align=\"right\">" + ComVat + "</td>");
            result.Append("<td align=\"left\"></td>");
            result.Append("</tr>");

            if (decComTotal > 1000)
            {
                result.Append("<tr>");
                result.Append("<td align=\"left\">หักภาษีหัก ณ ที่จ่าย</td>");
                result.Append("<td align=\"right\">" + ComCh + "</td>");
                result.Append("<td align=\"left\"></td>");
                result.Append("</tr>");
            }

            result.Append("<tr>");
            result.Append("<td align=\"left\"></td>");
            result.Append("<td align=\"right\"></td>");
            result.Append("<td align=\"left\"></td>");
            result.Append("</tr>");
            result.Append("<tr>");
            result.Append("<td align=\"center\">ยอดคงเหลือเรียกเก็บ</td>");
            result.Append("<td align=\"right\">" + Balance + "</td>");
            result.Append("<td align=\"left\"></td>");
            result.Append("</tr>");


            strPolicy.Append("<table>");
            strPolicy.Append("<tr>");
            strPolicy.Append("<td style=\"font-weight:bold;\">1.</td>");
            strPolicy.Append("<td>โอนเงินเข้าบัญชี บริษัท บูลเฮ้าส์ ทราเวล จำกัด ธนาคารกสิกรไทย สาขา เซ็นทรัลลาดพร้าว เลขที่ 730-2-37133-9 ประเภทออมทรัพย์</td>");
            strPolicy.Append("</tr>");
            strPolicy.Append("<tr>");
            strPolicy.Append("<td style=\"font-weight:bold;\">2.</td>");
            strPolicy.Append("<td>ชำระด้วยเช็คชีดคร่อมในนาม \"บริษัท บลูเฮ้าส์ ทราเวล จำกัด\" <br/> และจัดส่งมาที่ บริษัท บลูเฮ้าส์ทราเวล จำกัด เลขที่ 254/10 ซ.รัชดาภิเษก 42 แขวงลาดยาว เขตจตุจักร กทม. 10900</td>");
            strPolicy.Append("</tr>");



        }
        else
        {
            strPolicy.Append("<table>");
            strPolicy.Append("<tr>");
            strPolicy.Append("<td style=\"font-weight:bold;\">1.</td>");
            strPolicy.Append("<td>โอนเงินเข้าบัญชี นายพงษ์พัฒน์ โฆษิตวรกิจกุล ธ.ไทยพาณิชย์ สาขาเซ็นทรัลลาดพร้าว เลขที่ 206-2-08796-9 ประเภทออมทรัพย์ <br/>หรือ บัญชี นายพงษ์พัฒน์ โฆษิตวรกิจกุล ธนาคารกสิกรไทย สาขา เซ็นทรัลลาดพร้าว เลขที่ 730-2-51422-9 ประเภทออมทรัพย์</td>");
            strPolicy.Append("</tr>");


        }
        strPolicy.Append("<tr><td colspan=\"2\" style=\"height:10px;\"></td></tr>");
        strPolicy.Append("<tr>");

        strPolicy.Append("<td colspan=\"2\">***ในกรณีที่มีการชำระเงินด้วยการโอนเงินผ่านธนาคาร ทางโรงแรมจะเป็นผู้รับผิดชอบสำหรับค่าโอน ไม่ว่ากรณีใดๆทั้งสิ้น</td>");
        strPolicy.Append("</tr>");
        strPolicy.Append("</table>");

        result.Append("<tr>");
        result.Append("<td align=\"center\"></td>");
        result.Append(" <td align=\"right\"></td>");
        result.Append("<td align=\"left\"></td>");
        result.Append("</tr>");

        string KeywordPaymentBooking = Utility.GetKeywordReplace(Tmp, "<!--##@PaymentBookingListStart##-->", "<!--##@PaymentBookingListEnd##-->");
        Tmp = Tmp.Replace(KeywordPaymentBooking, result.ToString());


        string KeywordPolicy = Utility.GetKeywordReplace(Tmp, "<!--##@PolicyStart##-->", "<!--##@PolicyEnd##-->");
        Tmp = Tmp.Replace(KeywordPolicy, strPolicy.ToString());

        return Tmp;

    }
}