using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;
using System.Collections;
using System.Xml;
using System.Text;
using Hotels2thailand.Front;
using Hotels2thailand;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Booking;

/// <summary>
/// Summary description for Voucher
/// </summary>
/// 
namespace Hotels2thailand.BookingB2b
{
    
    public partial class BookingMailEngineB2b : BookingPrintAndVoucher_Helper
    {
        public int BookingId { get; set; }
        public byte BookingLangId { get; set; }
        
        private BookingProductList _c_class_booking_product = null;
        public BookingProductList cClassBookingProduct
        {
            get
            {
                if (_c_class_booking_product == null)
                {
                    BookingProductList cBookingProduct = new BookingProductList();
                    _c_class_booking_product = cBookingProduct.getTop1ProductListShowFirstByBookingId_customerDisplay(this.BookingId);
                }
                return _c_class_booking_product;
            }
            set
            {
                
                _c_class_booking_product = value;
            }
        }

        public static string _mail_display_default = "Reservation:Hotels2thailand.com";
        public static string MailNameDisplayDefault
        {
            get
            {
                return _mail_display_default;
            }
            set
            {
                _mail_display_default = value;
            }
        }

        private string _bcc = "sent@hotels2thailand.com;sent2@hotels2thailand.com;peerapong@hotels2thailand.com";
        public string Bcc
        {
            get { return _bcc; }
            set { _bcc = value; }
        }

        private byte _flag_deal = 0;

        public BookingMailEngineB2b(int intBookingId)
        {
            this.BookingId = intBookingId;
            BookingdetailDisplay cBooking = new BookingdetailDisplay();
            this.BookingLangId = cBooking.GetBookingLang(intBookingId);
            
        }


        public BookingMailEngineB2b(byte  bytLangId)
        {

            this.BookingLangId = bytLangId;

        }
        private Hotels2thailand.Production.ProductBookingEngine _c_class_product_BookingEngine = null;
        public Hotels2thailand.Production.ProductBookingEngine cProductBookingEngine
        {
            get
            {
                if (_c_class_product_BookingEngine == null)
                {
                    Hotels2thailand.Production.ProductBookingEngine cProduct = new Hotels2thailand.Production.ProductBookingEngine();

                    _c_class_product_BookingEngine = cProduct.GetProductbookingEngine(this.cClassBookingProduct.ProductID);
                }
                return _c_class_product_BookingEngine;
            }

            set { _c_class_product_BookingEngine = value; }
        }
        private string _mainsite = "http://www.booking2hotels.com/";
        protected string getMailTheme()
        {
            string Theme = string.Empty;

           
            string Theme_file = "Template_maillBooking_en.html";
            if (this.BookingLangId == 2)
                Theme_file = "Template_maillBooking_th.html";

            StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template_B2b/" + Theme_file + ""));
            string read = objReader.ReadToEnd();
            objReader.Close();
            Theme = Utility.GetKeywordReplace(read, "<!--##@MailContentStart##-->", "<!--##@MailContentEnd##-->");
            return Theme;
        }

        protected string getMailThemeNew()
        {
            string Theme = string.Empty;


            string Theme_file = "mail_template_en.html";
            if (this.BookingLangId == 2)
                Theme_file = "mail_template_th.html";

            StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template_B2b/" + Theme_file + ""));
            string read = objReader.ReadToEnd();
            objReader.Close();
            Theme = Utility.GetKeywordReplace(read, "<!--##@MailContentStart##-->", "<!--##@MailContentEnd##-->");

            //replace queryCache

            Theme = Theme.Replace("<!--##@bannerQueryCache##-->","?ver=" + DateTime.Now);

            return Theme;
        }


        public bool SendMailBookingRecevied()
        {
            bool Success = false;
            string Maildisplay = "Reservation:Hotels2thailand.com";
            //string subject = "Booking received from hotels2thailand.com (ORDER ID:"+ this.BookingId +")";
            string strSubject = string.Empty;

            strSubject = this.GetSubject(MailCat.BookingRecevied);

            //if (cClassBookingProduct.FlagDeal == 1)
            //    strSubject = this.GetSubject(MailCat.BookingRecevied_deal);
            //else
            //    strSubject = this.GetSubject(MailCat.BookingRecevied);


            try
            {
                string MailBody = getMailBookingRecieved();

                Success = Hotels2MAilSender.SendmailBooking(BookingMailEngineB2b.MailNameDisplayDefault, GetEmailBooking(), strSubject, "", MailBody);

                Success = Hotels2MAilSender.SendmailBooking(Maildisplay, this.Bcc, strSubject, "", removeEmailTrack(MailBody));

            }
            catch
            {
                Success = false;
            }
            return Success;
        }


        public bool SendMailBookingRecevied_OfflineCharge()
        {
            bool Success = false;
            string Maildisplay = "Reservation:Hotels2thailand.com";
            //string subject = "Booking received from hotels2thailand.com (ORDER ID:"+ this.BookingId +")";

            try
            {
                string MailBody = getMailBookingRecieved_offline();

                Success = Hotels2MAilSender.SendmailBooking(BookingMailEngineB2b.MailNameDisplayDefault, GetEmailBooking(), GetSubject(MailCat.BookingRecevied), "", MailBody);


                Success = Hotels2MAilSender.SendmailBooking(Maildisplay, this.Bcc, GetSubject(MailCat.BookingRecevied), "", removeEmailTrack(MailBody));

            }
            catch
            {
                Success = false;
            }
            return Success;
        }   
        

        public bool SendMailBookingRecevied_BankTranfer()
        {
            bool Success = false;
            string Maildisplay = "Reservation:Hotels2thailand.com";
            //string subject = "Booking received from hotels2thailand.com (ORDER ID:"+ this.BookingId +")";
            string strSubject = string.Empty;
            strSubject = this.GetSubject(MailCat.BookingRecevied);
            //if (cClassBookingProduct.FlagDeal == 1)
            //    strSubject = this.GetSubject(MailCat.BookingRecevied_deal);
            //else
            //    strSubject = this.GetSubject(MailCat.BookingRecevied);

            try
            {
                string MailBody = getMailBookingRecieved_BankTransfer();

                Success = Hotels2MAilSender.SendmailBooking(BookingMailEngineB2b.MailNameDisplayDefault, GetEmailBooking(), strSubject, "", MailBody);

                Success = Hotels2MAilSender.SendmailBooking(Maildisplay, this.Bcc, strSubject, "", removeEmailTrack(MailBody));

            }
            catch
            {
                Success = false;
            }
            return Success;
        }

        /// <summary>
        /// Forgot Password!!!
        /// sendmail to customer for resetPassword
        /// </summary>
        /// <param name="strBody"></param>
        /// <returns> true false </returns>
        public bool SendMailCustomerForgotPassword(string strName, string strBody, string strSubject, string strMailTo)
        {
            bool Success = false;
            string Maildisplay = "Support:Hotels2thailand.com";
            //string subject = "Booking received from hotels2thailand.com (ORDER ID:"+ this.BookingId +")";

            try
            {
                //string MailBody = getMailBookingRecieved();

                Success = Hotels2MAilSender.SendmailBooking(Maildisplay, strMailTo, strSubject, "", strBody);


               // Success = Hotels2MAilSender.SendmailBooking(Maildisplay, this.Bcc, GetSubject(MailCat.BookingRecevied), "", removeEmailTrack(MailBody));

            }
            catch
            {
                Success = false;
            }
            return Success;
        }


        public string getMailForgotpassword(string strName, string strBodyparam)
        {
            string MainBody = getMailThemeNew();

            //ContentReviewDetail

            //Disable Item
            string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
            MainBody = MainBody.Replace(KeywordItem, " ");
            string ContentTotalresubmitStart = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalresubmitStart##-->", "<!--##@ContentTotalresubmitEnd##-->");
            MainBody = MainBody.Replace(ContentTotalresubmitStart, " ");

            string linecontentStart = Utility.GetKeywordReplace(MainBody, "<!--##@linecontentStart##-->", "<!--##@linecontentEnd##-->");
            MainBody = MainBody.Replace(linecontentStart, " ");

            StringBuilder strBody = new StringBuilder();
           
            strBody.Append("<tr>");
            strBody.Append("<td  style=\"margin:0px; padding:0px;\">");
            strBody.Append(strBodyparam);
            strBody.Append("</td>");
            strBody.Append("</tr>");
        
           //Wordingforgotpassword

          

            string ContentFoot_start = Utility.GetKeywordReplace(MainBody, "<!--##@ContentFoot_start##-->", "<!--##@ContentFoot_end##-->");
            MainBody = MainBody.Replace(ContentFoot_start, " ");


            //Detail Top
            string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            MainBody = MainBody.Replace(KeywordDetailTop, " ");

            //CustomerName 
            MainBody = MainBody.Replace("<!--##@CusNameContent##-->", strName);


            //getDetail Review 
            MainBody = MainBody.Replace("<!--##@ContentReviewDetail##-->", TextDetailREview());


            //Mail body 
            MainBody = MainBody.Replace("<!--##@Wordingforgotpassword##-->", strBody.ToString());

            //MainBody = MainBody.Replace("<!--##@ContentEmailtracking##-->", imgTrack(15));

            return MainBody;
        }


        public string getMailReview()
        {
            string MainBody = getMailThemeNew();

            //ContentReviewDetail

            //Disable Item
            string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
            MainBody = MainBody.Replace(KeywordItem, " ");
            string ContentTotalresubmitStart = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalresubmitStart##-->", "<!--##@ContentTotalresubmitEnd##-->");
            MainBody = MainBody.Replace(ContentTotalresubmitStart, " ");

            string linecontentStart = Utility.GetKeywordReplace(MainBody, "<!--##@linecontentStart##-->", "<!--##@linecontentEnd##-->");
            MainBody = MainBody.Replace(linecontentStart, " ");



            string FootReview = string.Empty;

            if (this.BookingLangId != 1)
            {
                FootReview = FootReview + "<tr><td style=\"height:10px;\" height=\"10\"></td></tr>";
                FootReview = FootReview + "<tr>";
                FootReview = FootReview + "<td style=\" font-family:Tahoma;\">";
                FootReview = FootReview + "ขอบพระคุณอีกครั้งค่ะที่เลือกใช้บริการเวปไซต์โฮเทลทูฯ";
                FootReview = FootReview + "</td>";
                FootReview = FootReview + "</tr>";
            }

            string ContentFoot_start = Utility.GetKeywordReplace(MainBody, "<!--##@ContentFoot_start##-->", "<!--##@ContentFoot_end##-->");
            MainBody = MainBody.Replace(ContentFoot_start, FootReview);

            
            //Detail Top
            string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(4));

            //CustomerName 
            MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());


            //getDetail Review 
            MainBody = MainBody.Replace("<!--##@ContentReviewDetail##-->", TextDetailREview());


            //disable grand Total
            //string KeywordTotalGrand = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalGrandStart##-->", "<!--##@ContentTotalGrandEnd##-->");
            //MainBody = MainBody.Replace(KeywordTotalGrand, " ");

            //disable foot Posotion1
            //string KeywordFootWord_01 = Utility.GetKeywordReplace(MainBody, "<!--##@ContentFoot_start_01##-->", "<!--##@ContentFoot_End_01##-->");
            //MainBody = MainBody.Replace(KeywordTotalGrand, " ");

            //Replace FootWord
            //string KeywordFootWord = Utility.GetKeywordReplace(MainBody, "<!--##@ContentFoot_start##-->", "<!--##@ContentFoot_End##-->");
            //MainBody = MainBody.Replace(KeywordFootWord, "<tr ><td colspan=\"2\" style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:15px 20px 5px 20px; color:#6d6e71;\">" + getFooterReview()+ "</td></tr>");

            //disable ItemCOntent
            //string KeywordItemContent = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
            //MainBody = MainBody.Replace(KeywordItemContent, " ");

            //EmailTracking Is Open
            MainBody = MainBody.Replace("<!--##@ContentEmailtracking##-->", imgTrack(15));
            
            return MainBody;

        }


        public string getMailResubmit(int intPaymentId)
        {
            //byte bytBookingLang = cBookingPLsit.BookingLang;
            string MainBody = getMailThemeNew();


           //Booking IitemList
           string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
           //if (cClassBookingProduct.FlagDeal == 1)
           //{
           //    MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId_forDeal());

           //    //Define Value for FlagDeal private property
           //    _flag_deal = 1;

           //    //Detail Top
           //    string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
           //    MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(16));


           //    //getDetail Voucher
           //    //MainBody = MainBody.Replace("<!--##@ContentVoucherDetail##-->", TextDetailVoucherForDeal());

           //    //Detail Foot Custo for deal
           //    string KeywordDetailFoot = Utility.GetKeywordReplace(MainBody, "<!--##@ContentFoot_start##-->", "<!--##@ContentFoot_end##-->");
           //    MainBody = MainBody.Replace(KeywordDetailFoot, TextDetailFoot(1));


           //    //Detail Item Line
           //    string KeywordDetailLine = Utility.GetKeywordReplace(MainBody, "<!--##@linecontentStart_TOP##-->", "<!--##@linecontentEnd_TOP##-->");
           //    MainBody = MainBody.Replace(KeywordDetailLine, TextDetailLineItem(1));
           //}
           //else
           //{
           //    MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId());

           //    //Detail Top
           //    string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
           //    MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(1));
           //}

           MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId());

           //Detail Top
           string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
           MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(1));


           //CustomerName 
           MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

           //GrandTotal
           MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal());

           //GrandPaid
           MainBody = MainBody.Replace("<!--##@mailItemTotalPaidContent##-->", GrandPaidTotal());

           //GrandRequestotal
           MainBody = MainBody.Replace("<!--##@mailItemTotalRequestContent##-->", GrandRequestTotal(intPaymentId));

            //Disable Grandtotal Main
            string KeywordGrandTotal = Utility.GetKeywordReplace(MainBody, "<!--##@Grandtotal_Start##-->", "<!--##@Grandtotal_End##-->");
           MainBody = MainBody.Replace(KeywordGrandTotal, " ");

           BookingPaymentDisplay cBookingPayment = new BookingPaymentDisplay();
           cBookingPayment = cBookingPayment.GEtPaymentByPaymentId(intPaymentId);
           switch (cBookingPayment.CatId)
           {
               case 1:
               case 3:
                   //get Button to MakePayment
                   MainBody = MainBody.Replace("<!--##@ContentMakepayment##-->", getmakepaymentbutton(intPaymentId));
                   //Disable BankTransfer 
                   string KeywordBankTransfer = Utility.GetKeywordReplace(MainBody, "<!--##@Banktransfer_start##-->", "<!--##@Banktransfer_End##-->");
                   MainBody = MainBody.Replace(KeywordBankTransfer, " ");
                   break;
               case 2:
                   //if (cClassBookingProduct.FlagDeal == 1)
                   //{
                   //    string BTF = "<tr><td  align=\"left\" style=\"font-family:Tahoma; font-size:14px;color:#6d6e71;  font-weight:bold;\">หมายเหตุ</td></tr>";
                   //    BTF = BTF + "<tr><td  align=\"left\" style=\"font-family:Tahoma; font-size:11px;color:#ef3021;\">";
                   //    BTF = BTF + " - ในกรณีที่ท่านไม่สามารถโอนเงินได้ภายใน 48 ชั่วโมง ขอความกรุณาตรวจสอบสถานะของดีลที่ท่านสั่งซื้อไว้อีกครั้งกับเจ้าหน้าที่ของเราก่อนทำการโอนเงินค่ะ ติดต่อ Call Center 02-9300973";
                   //    BTF = BTF + "</td></tr>";
                   //    BTF = BTF + "<tr><td  align=\"left\" style=\"font-family:Tahoma; font-size:11px;color:#6d6e71;\">";
                   //    BTF = BTF + " - หลังจากท่านชำระเงินเรียบร้อยแล้ว กรุณาแฟกซ์หลักฐานการชำระเงินมาที่เบอร์ 02-9306825, 02-9306514 หรืออีเมล์ reservation@hotels2thailand.com หรือโทรแจ้งทางเจ้าหน้าที่โฮเทลทูไทยแลนด์เพื่อส่งคูปองยืนยันการสั่งซื้อให้ท่านทันทีค่ะ";
                   //    BTF = BTF + "</td></tr>";

                   //    string KeywordBankTransferNote = Utility.GetKeywordReplace(MainBody, "<!--##@Banktransfer_NoteStart##-->", "<!--##Banktransfer_NoteEnd##-->");
                   //    MainBody = MainBody.Replace(KeywordBankTransferNote, BTF);
                   //}
                   break;
           }
           

           //EmailTracking Is Open
           MainBody = MainBody.Replace("<!--##@ContentEmailtracking##-->", imgTrack(16));

           return MainBody;

        }

        
        public string getMailSendVoucher()
        {
            string MainBody = getMailThemeNew();

            //Booking IitemList
            string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
            //MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId());
            //if (cClassBookingProduct.FlagDeal == 1)
            //{
            //    MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId_forDeal());


            //    //Define Value for FlagDeal private property
            //    _flag_deal = 1;

            //    //Detail Top
            //    string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            //    MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(15));

                
            //    //getDetail Voucher
            //    MainBody = MainBody.Replace("<!--##@ContentVoucherDetail##-->", TextDetailVoucherForDeal());

            //    //Detail Foot Custo for deal
            //    string KeywordDetailFoot = Utility.GetKeywordReplace(MainBody, "<!--##@ContentFoot_start##-->", "<!--##@ContentFoot_end##-->");
            //    MainBody = MainBody.Replace(KeywordDetailFoot, TextDetailFoot(1));


            //    //Detail Item Line
            //    string KeywordDetailLine = Utility.GetKeywordReplace(MainBody, "<!--##@linecontentStart_TOP##-->", "<!--##@linecontentEnd_TOP##-->");
            //    MainBody = MainBody.Replace(KeywordDetailLine, TextDetailLineItem(1));

            //}
            //else
            //{
            //    MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId());

            //    //Detail Top
            //    string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            //    MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(3));

            //    //getDetail Voucher
            //    MainBody = MainBody.Replace("<!--##@ContentVoucherDetail##-->", TextDetailVoucher());
            //}

            MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId());

            //Detail Top
            string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(3));

            //getDetail Voucher
            MainBody = MainBody.Replace("<!--##@ContentVoucherDetail##-->", TextDetailVoucher());
            


            string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@GrandTotal_Resubmit_Start##-->", "<!--##@GrandTotal_Resubmit_End##-->");
            MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

            //CustomerName 
            MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());


            //GrandTotal
            MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal());


            //Disable BankTransfer 
            string KeywordBankTransfer = Utility.GetKeywordReplace(MainBody, "<!--##@Banktransfer_start##-->", "<!--##@Banktransfer_End##-->");
            MainBody = MainBody.Replace(KeywordBankTransfer, " ");
            //disable Total Paid & requset Payment
            //string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalresubmitStart##-->", "<!--##@ContentTotalresubmitEnd##-->");
            //MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

            string KeywordreqPay = Utility.GetKeywordReplace(MainBody, "<!--##@ReqPay_Start##-->", "<!--##@ReqPay_End##-->");
            MainBody = MainBody.Replace(KeywordreqPay, " ");

            

            //EmailTracking Is Open
            MainBody = MainBody.Replace("<!--##@ContentEmailtracking##-->", imgTrack(14));

            return MainBody;
        }


        public string TextDetailLineItem(byte TypeMail)
        {
            StringBuilder result = new StringBuilder();
            switch (TypeMail)
            {
                case 1:
                    if (this.BookingLangId == 1)
                    {
                        result.Append("<tr><td height=\"20\"></td></tr>");
                        result.Append("<tr><td style=\"margin:0px; padding:0px;font-size:11px;font-family:Tahoma; color:#6d6e71;\">Your Deal Summary</td></tr>");
                        result.Append("<tr><td style=\"margin:0px; padding:0px;\"><img src=\"http://www.hotels2thailand.com/images_mail/line-body.jpg\" alt=\"\" width=\"659\" height=\"5\"  /></td></tr>");

                       
                    }
                    else
                    {
                        result.Append("<tr><td height=\"20\"></td></tr>");
                        result.Append("<tr><td style=\"margin:0px; padding:0px;font-size:11px;font-family:Tahoma; color:#6d6e71;\">รายละเอียดดีล</td></tr>");
                        result.Append("<tr><td style=\"margin:0px; padding:0px;\"><img src=\"http://www.hotels2thailand.com/images_mail/line-body.jpg\" alt=\"\" width=\"659\" height=\"5\"  /></td></tr>");
                    }
                    break;
            }

            return result.ToString();
        }


        public string TextDetailFoot(byte TypeMail)
        {
            StringBuilder result = new StringBuilder();
            switch (TypeMail)
            {
                case 1:
                    if (this.BookingLangId == 1)
                    {
                        result.Append("<tr>");
                        result.Append("<td  style=\" font-family:Tahoma;\">");
                        result.Append("Kindly acknowledge us if you find any payment problem or any such obstacles. It will be very much appreciated to be advised of any inconvenience to help you rectify the problem.");
                        result.Append(" </td>");
                        result.Append(" </tr>");
                        result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                        result.Append("<tr>");
                        result.Append("<td style=\" font-family:Tahoma;\">");
                        result.Append("In case you have any enquiry, please click <a href=\"http://www.hotels2thailand.com/thailand-hotels-contact.aspx\">contact us</a> and your booking id is important to put for reference. ");
                        result.Append(" </td>");
                        result.Append("</tr>");
                    }
                    else
                    {
                        result.Append("<tr>");
                        result.Append("<td  style=\" font-family:Tahoma;\">");
                        result.Append("หากท่านมีข้อสงสัยหรือพบปัญหาในการให้บริการของเรา ท่านสามารถติดต่อเจ้าหน้าที่โฮเทลทูไทยแลนด์ได้ตลอด ตั้งแต่เวลา 7:00 – 20:00 ทุกวันไม่เว้นวันหยุด ค่ะ ทางเรายินดีให้บริการและน้อมรับฟังคำติชมจากท่านทุกประการค่ะ");
                        result.Append(" </td>");
                        result.Append(" </tr>");
                        result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                        result.Append("<tr>");
                        result.Append("<td style=\" font-family:Tahoma;\">");
                        result.Append(" ขอบพระคุณอีกครั้งค่ะที่เลือกใช้บริการเวปไซต์โฮเทลทูไทยแลนด์");
                        result.Append(" </td>");
                        result.Append("</tr>");
                    }
                    break;
            }

            return result.ToString();
        }


        public string getMailBookingRecieved()
        {
            string MainBody = getMailThemeNew();

            //CustomerName 
            MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

           // Detail Top
            string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(2));

            //Booking IitemList
            string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");

            MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId());

            //if (cClassBookingProduct.FlagDeal == 1)
            //    MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId_forDeal());
            //else
            //    MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId());

            string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@GrandTotal_Resubmit_Start##-->", "<!--##@GrandTotal_Resubmit_End##-->");
            MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

            string KeywordreqPay = Utility.GetKeywordReplace(MainBody, "<!--##@ReqPay_Start##-->", "<!--##@ReqPay_End##-->");
            MainBody = MainBody.Replace(KeywordreqPay, " ");

            string KeywordBankTransfer = Utility.GetKeywordReplace(MainBody, "<!--##@Banktransfer_start##-->", "<!--##@Banktransfer_End##-->");
            MainBody = MainBody.Replace(KeywordBankTransfer, " ");



            ////GrandTotal
             MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal());

            //EmailTracking Is Open
            MainBody = MainBody.Replace("<!--##@ContentEmailtracking##-->", imgTrack(15));



            //string KeywordContentFoot = Utility.GetKeywordReplace(MainBody, "<!--##@ContentFoot_start##-->", "<!--##@ContentFoot_end##-->");
            //MainBody = MainBody.Replace(KeywordContentFoot, strGrandTotal.ToString());
            //getDetail Voucher
            //MainBody = MainBody.Replace("<!--##@ContentBodyDisplay##-->", TextDetailVoucher());
            return MainBody;
        }


        public string getMailBookingRecieved_offline()
        {
            string MainBody = getMailThemeNew();

            //CustomerName 
            MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

            // Detail Top
            string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(14));

            //Booking IitemList
            string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
            MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId());

            string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@GrandTotal_Resubmit_Start##-->", "<!--##@GrandTotal_Resubmit_End##-->");
            MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

            string KeywordreqPay = Utility.GetKeywordReplace(MainBody, "<!--##@ReqPay_Start##-->", "<!--##@ReqPay_End##-->");
            MainBody = MainBody.Replace(KeywordreqPay, " ");

            string KeywordBankTransfer = Utility.GetKeywordReplace(MainBody, "<!--##@Banktransfer_start##-->", "<!--##@Banktransfer_End##-->");
            MainBody = MainBody.Replace(KeywordBankTransfer, " ");



            ////GrandTotal
            MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal());


            //Offline Charge Detail  OfflineCharge_Detail
           MainBody = MainBody.Replace("<!--##@OfflineCharge_Detail##-->", detailMailOffline());

            //EmailTracking Is Open
            MainBody = MainBody.Replace("<!--##@ContentEmailtracking##-->", imgTrack(15));



            //string KeywordContentFoot = Utility.GetKeywordReplace(MainBody, "<!--##@ContentFoot_start##-->", "<!--##@ContentFoot_end##-->");
            //MainBody = MainBody.Replace(KeywordContentFoot, strGrandTotal.ToString());
            //getDetail Voucher
            //MainBody = MainBody.Replace("<!--##@ContentBodyDisplay##-->", TextDetailVoucher());
            return MainBody;
        }


        public string getMailBookingRecieved_BankTransfer()
        {
            string MainBody = getMailThemeNew();

            //CustomerName 
            MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

            //if (cClassBookingProduct.FlagDeal == 1)
            //{
            //    //Define Value for FlagDeal private property
            //    _flag_deal = 1;

            //    //Booking IitemList
            //    string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
            //    MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId_forDeal());

            //    //Detail Top
            //    string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            //    MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(17));

            //    //Detail Item Line
            //    string KeywordDetailLine = Utility.GetKeywordReplace(MainBody, "<!--##@linecontentStart_TOP##-->", "<!--##@linecontentEnd_TOP##-->");
            //    MainBody = MainBody.Replace(KeywordDetailLine, TextDetailLineItem(1));
            //}
            //else
            //{

            //    //Booking IitemList
            //    string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
            //    MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId());

            //    //Detail Top
            //    string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            //    MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(13));

            //}

            //Booking IitemList
            string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
            MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId());

            //Detail Top
            string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(13));

            string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@GrandTotal_Resubmit_Start##-->", "<!--##@GrandTotal_Resubmit_End##-->");
            MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

            string KeywordreqPay = Utility.GetKeywordReplace(MainBody, "<!--##@ReqPay_Start##-->", "<!--##@ReqPay_End##-->");
            MainBody = MainBody.Replace(KeywordreqPay, " ");

            string KeywordBankTransfer = Utility.GetKeywordReplace(MainBody, "<!--##@Banktransfer_start##-->", "<!--##@Banktransfer_End##-->");
            MainBody = MainBody.Replace(KeywordBankTransfer, " ");

            //Foot BankTransfer
            MainBody = MainBody.Replace("<!--##@ContentDetailBookingRecieved##-->", TextDetailBookingREcievedBankTranfer());


            ////GrandTotal
            MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal());

            ////disable Total Paid & requset Payment
            //string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalresubmitStart##-->", "<!--##@ContentTotalresubmitEnd##-->");
            //MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

            //EmailTracking Is Open
            MainBody = MainBody.Replace("<!--##@ContentEmailtracking##-->", imgTrack(15));
            

            //getDetail Voucher
            //MainBody = MainBody.Replace("<!--##@ContentBodyDisplay##-->", TextDetailVoucher());
            return MainBody;
        }


        public string GetSubject(MailCat mailcat)
        {
            string result = string.Empty;

            string txtSup = string.Empty;
            string Product = string.Empty;

            string[] arrFirt = { "Thank You Mail:", "ข้อมูลการจอง:"};

            string[] arrFirt1 = { "Reservation Confirmation:", "เอกสารยืนยันการจอง:" };

            string[] arrFirt2 = { "Please write review for www.hotels2thailand.com", "ความคิดเห็นของท่านมีค่ากับเราเสมอ:" };

            string[] arrFirt3 = { "Request Your Payment Information:", "ข้อมูลการเรียกชำระเงินสำหรับการจอง:" };

            string[] arrFirt4 = { "and", "และ" };

            string[] arrFirt5 = { "Request Your Payment Information:", "ข้อมูลการเรียกชำระเงินสำหรับการสั่งซื้อ:" };

            string[] arrFirt6 = { "Deal Order Mail:", "ข้อมูลการสั่งซื้อ:" };

            string[] arrFirt7 = { "Order Confirmation:", "คูปองยืนยันการสั่งซื้อ:" };

            BookingProductList BookingProductITem = new BookingProductList();
            IList<object> iListProduct = BookingProductITem.getProductListShowFirstByBookingId_customerDisplay(this.BookingId);

            int count = 1;
            foreach(BookingProductList productitem in  iListProduct)
            {
                if (count != iListProduct.Count)
                    Product = Product + productitem.ProductTitle + " " + arrFirt4[this.BookingLangId - 1] + " ";
                else
                    Product = Product + productitem.ProductTitle;
                count = count + 1;
            }
            switch (mailcat)
            {
                case MailCat.BookingRecevied :
                    txtSup = arrFirt[this.BookingLangId - 1] + " " + Product;
                    result = txtSup + " #" + this.BookingId;
                    break;
                case MailCat.Resubmit:
                    txtSup = arrFirt3[this.BookingLangId - 1] + " " + Product;
                    result = txtSup + " #" + this.BookingId;
                    break;
                case MailCat.ComfirmBooking:
                    txtSup = arrFirt1[this.BookingLangId - 1] + " " + Product;
                    result = txtSup + " #" + this.BookingId;
                    break;
                case MailCat.Review:

                    result = arrFirt2[this.BookingLangId - 1] + " " + Product;
                    break;
                //case MailCat.BookingRecevied_deal:
                //    txtSup = arrFirt6[this.BookingLangId - 1] + " " + Product;
                //    result = txtSup + " #" + this.BookingId;
                //    break;
                //case MailCat.ComfirmBooking_deal:
                //    txtSup = arrFirt7[this.BookingLangId - 1] + " " + Product;
                //    result = txtSup + " #" + this.BookingId;
                //    break;
                //case MailCat.Resubmit_deal:

                //    txtSup = arrFirt5[this.BookingLangId - 1] + " " + Product;
                //    result = txtSup + " #" + this.BookingId;
                //    break;

            }


            
            return result;
        }

        public string getBookingCheckStatus()
        {
            return getProductListFromBookingId();
        }


        private string getFooterReview()
        {
            string result = string.Empty;
            switch (this.BookingLangId)
            {
                case 1:
                    result = "Please note that you do not have to answer all the questions. We will use our effort to serve you the best service.";
                    break;
                case 2:
                    result = "ท่านไม่จำเป็นต้องตอบทุกคำถามค่ะ เราจะพยายามปรับปรุงการบริการของเราให้ดีที่สุดค่ะ";
                    break;
            }

            return result;
        }

        protected string detailMailOffline()
        {
            StringBuilder result = new StringBuilder();
          
            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
            result.Append("<tr>");
            result.Append("<td>");
            if (this.BookingLangId == 1)
            {
                result.Append("<strong>NOTE:</strong> Your credit card will not be charged until you receive the voucher confirmation completely. In case your reservation is unavailable, we will send the notification mail to inform you for other alternative selection within 24 hours.");
            }
            else
            {
                result.Append("ทางเวปไซต์โฮเทลทูไทยแลนด์จะไม่หักยอดเงินจากบัตรเครดิตของท่านจนกว่าท่านจะได้รับเอกสารยืนยันการจอง (voucher) เป็นที่เรียบร้อยแล้วในกรณีที่รายการที่จองนั้นไม่สามารถยืนยันการจองให้ได้ ท่านจะได้รับอีเมล์จากเราอีกครั้งภายใน 24 ชั่วโมงค่ะ");
            }
            result.Append("</td>");
            result.Append("</tr>");
            return result.ToString();
        }

        protected string imgTrack(byte bytMailConfirmCat)
        {
            string result = string.Empty;

            string query = "bid=" + Hotels2String.EncodeIdToURL(this.BookingId.ToString() + "#" + bytMailConfirmCat.ToString());
            result = "<!--##@ContentEmailtracking_remove_start##--><img id=\"img_captcha\"  src=\"http://www.hotels2thailand.com/mt?" + query + "\" alt=\"\" /><!--##@ContentEmailtracking_remove_end##-->";

            return result;
        }

        public static string removeEmailTrack(string MailBody)
        {
            string KeywordmailTrack = Utility.GetKeywordReplace(MailBody, "<!--##@ContentEmailtracking_remove_start##-->", "<!--##@ContentEmailtracking_remove_end##-->");
                MailBody = MailBody.Replace(KeywordmailTrack, " ");
                return MailBody;
        }

        protected string getmakepaymentbutton(int intPaymentId)
        {
            StringBuilder result = new StringBuilder();
            
            string paymentEncode = EncodeId(intPaymentId);

            string strBtnPayment = string.Empty;
            string makePayment = string.Empty;
            string[] strCantClick = {"","",""};
            switch (this.BookingLangId)
            {
                case 1:
                    strBtnPayment = "makpay_en.jpg";
                    makePayment = "Make Payment";
                    strCantClick[0] = "If you can’t see";
                    strCantClick[1] = "button, please click";
                    strCantClick[2] = "here";
                    break;
                case 2:
                    strBtnPayment = "makpay_th.jpg";
                    makePayment = "ชำระเงิน";
                    strCantClick[0] = "หากท่านไม่พบปุ่ม";
                    strCantClick[1] = "กรุณาคลิ๊ก";
                    strCantClick[2] = "ที่นี่";
                    break;
            }


            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td><tr>");
            result.Append("<tr><td align=\"center\" style=\"height:10px;text-align:center;\">");
            result.Append("<a href=\"http://www.booking2hotels.com/booking_resubmit.aspx?pcode=" + paymentEncode + "\"><img src=\"http://www.hotels2thailand.com/images_mail/" + strBtnPayment + "\" style=\"border:0px;cursor:pointer;\" alt=\"" + makePayment + "\" width=\"206\" height=\"41\" /></a>");

            result.Append("</td></tr>");
            result.Append("<tr><td style=\"height:10px;text-align:center; font-size:11px;\">");
            result.Append("<span style=\"font-weight:bold;color:#dd4b39;\">**</span>" + strCantClick[0] + "<strong>\"Make Payment\"</strong>" + strCantClick[1] + " <a href=\"http://www.booking2hotels.com/booking_resubmit.aspx?pcode=" + paymentEncode + "\" >" + makePayment + "</a> " + strCantClick[2]);
            result.Append("</td></tr>");
            
            

            return result.ToString();
        }

        protected string getmakepaymentbutton_booknow(int intPaymentId)
        {
            StringBuilder result = new StringBuilder();
            
            string paymentEncode = EncodeId(intPaymentId);

            result.Append("<tr><td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
            result.Append("Other suggestion: Please use the different card to retry the payment yourself by click;");
            result.Append("</td></tr>");
            result.Append("<tr><td style=\"height: 10px;\"></td></tr>");
            result.Append("<tr><td style=\"height:10px;text-align:center;\">");
            result.Append("<a href=\"http://www.hotels2thailand.com/booking_resubmit.aspx?pcode=" + paymentEncode + "\"><img src=\"http://www.hotels2thailand.com/theme_color/blue/images/button/make_payment.jpg\" style=\"border:0px;cursor:pointer;\" /></a>");
            //result.Append("");
            result.Append("</td></tr>");
            result.Append("<tr><td style=\"height: 10px;\"></td></tr>");
            //result.Append("<tr><td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
            //result.Append("Kindly acknowledge us if you find any payment problem or any such obstacles. It will be very much appreciated to be advised of any inconvenience to help you rectify the problem. Thank you very much.");
            //result.Append("</td></tr>");


            return result.ToString();
        }

        protected string getProductListFromBookingId()
        {
            BookingProductList cBookingProduct = new BookingProductList();
            
            PromotionContent cProContent = new PromotionContent();
            StringBuilder result = new StringBuilder();
            string BookingDetail = string.Empty;
            foreach (BookingProductList BookingProductITem in cBookingProduct.getProductListShowFirstByBookingId_customerDisplay(this.BookingId))
            {
                Front.FrontProductDetail cFrontproductDetail = new FrontProductDetail();
                if (BookingProductITem.ProductCategory != 31)
                {
                    cFrontproductDetail.GetProductDetailByID(BookingProductITem.ProductID, BookingProductITem.ProductCategory, this.BookingLangId);
                }
                
                
                string them_file_item = string.Empty;
                switch(BookingProductITem.ProductCategory)
                {
                    //Hotel
                    case 29 :

                        switch (this.BookingLangId)
                        {
                            case 1:
                                them_file_item = "tbookingMail_hotel_item_en.html";
                                break;
                            case 2:
                                them_file_item = "tbookingMail_hotel_item_th.html";
                                break;
                        }
                        StreamReader objHotelDetailReader = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template_B2b/" + them_file_item + ""));
                        BookingDetail = objHotelDetailReader.ReadToEnd();
                        objHotelDetailReader.Close();
                        objHotelDetailReader.Dispose();
                        
                        //ProductName 
                        BookingDetail = BookingDetail.Replace("<!--##@mailProductName##-->", BookingProductITem.ProductTitle);

                        
                        //ProductAddress
                        BookingDetail = BookingDetail.Replace("<!--##@mailProductaddress##-->", cFrontproductDetail.Address);


                        //ProductStar
                        BookingDetail = BookingDetail.Replace("<!--##@mailProductStar##-->", getStarPic(Convert.ToByte(Math.Round(cFrontproductDetail.Star))));

                        //HttpContext.Current.Response.Write(getStarPic(Convert.ToByte(Math.Round(cFrontproductDetail.Star))));
                        //HttpContext.Current.Response.End();
                        //Front.FrontProductPicture cFrontPicture = new FrontProductPicture();
                        //cFrontPicture.PicturePath
                        //ProductPicture
                        BookingDetail = BookingDetail.Replace("<!--##@mailProductPicture##-->", cFrontproductDetail.Thumbnail);

                        //DateCheckIn 
                        BookingDetail = BookingDetail.Replace("<!--##@mailProductCheckIn##-->", DateTimeCheck(BookingProductITem.DateTimeCheckIn, this.BookingLangId));
                        //DateCheckOut
                        BookingDetail = BookingDetail.Replace("<!--##@mailProductCheckOut##-->", DateTimeCheck(BookingProductITem.DateTimeCheckOut, this.BookingLangId));

                        //NumAdult
                        BookingDetail = BookingDetail.Replace("<!--##@mailProductNumAdult##-->", BookingProductITem.NumAdult.ToString());

                        //NumChild
                        BookingDetail = BookingDetail.Replace("<!--##@mailProductNumChild##-->", BookingProductITem.NumChild.ToString());



                        //ItemList
                        string KeywordItemList = Utility.GetKeywordReplace(BookingDetail, "<!--##@mailProductItemStart##-->", "<!--##@mailProductItemEnd##-->");
                        BookingDetail = BookingDetail.Replace(KeywordItemList, GetProductItemHOtel(BookingProductITem.BookingProductId, DateTimeNightTotal(BookingProductITem.DateTimeCheckIn, BookingProductITem.DateTimeCheckOut)));
               
                        break;
                    // Golf
                    case 32:
                        switch (this.BookingLangId)
                        {
                            case 1:
                                them_file_item = "tbookingMail_golf_item_en.html";
                                break;
                            case 2:
                                them_file_item = "tbookingMail_golf_item_th.html";
                                break;
                        }
                        StreamReader objHotelDetailReaderGolf = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template/" + them_file_item + ""));
                        BookingDetail = objHotelDetailReaderGolf.ReadToEnd();
                        objHotelDetailReaderGolf.Close();
                        objHotelDetailReaderGolf.Dispose();

                        //ProductName 
                        BookingDetail = BookingDetail.Replace("<!--##@mailProductName##-->", BookingProductITem.ProductTitle);
                        //ProductAddress
                        BookingDetail = BookingDetail.Replace("<!--##@mailProductaddress##-->", cFrontproductDetail.Address);

                        //Front.FrontProductPicture cFrontPicture = new FrontProductPicture();
                        //cFrontPicture.PicturePath
                        //ProductPicture
                        BookingDetail = BookingDetail.Replace("<!--##@mailProductPicture##-->", cFrontproductDetail.Thumbnail);
                        //Time
                        BookingDetail = BookingDetail.Replace("<!--##@mailProduct_BookingDateConfirme##-->", DateTimeCheck(BookingProductITem.DateTimeCheckIn));
                        //Tee-Off Time
                        BookingDetail = BookingDetail.Replace("<!--##@mailProduct_BookingTimeConfirme##-->", DateTimeChekFullTime(BookingProductITem.DateTimeCheckIn, BookingProductITem.DateTimeConfirmCheckIn));
                        
                    
                        ////Golfer
                        //BookingDetail = BookingDetail.Replace("<!--##@mailProduct_Golfer##-->", BookingProductITem.NunGolf.ToString());


                        //ItemList
                        string KeywordItemList_golf = Utility.GetKeywordReplace(BookingDetail, "<!--##@mailProductItemStart##-->", "<!--##@mailProductItemEnd##-->");
                        BookingDetail = BookingDetail.Replace(KeywordItemList_golf, GetProductItem_Golf(BookingProductITem.BookingProductId, DateTimeNightTotal(BookingProductITem.DateTimeCheckIn, BookingProductITem.DateTimeCheckOut)));

                        break;
                    // Spa
                    case 40:
                        switch (this.BookingLangId)
                        {
                            case 1:
                                them_file_item = "tbookingMail_spa_item_en.html";
                                break;
                            case 2:
                                them_file_item = "tbookingMail_spa_item_th.html";
                                break;
                        }
                        StreamReader objHotelDetailReader_Spa = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template_B2b/" + them_file_item + ""));
                        BookingDetail = objHotelDetailReader_Spa.ReadToEnd();
                        objHotelDetailReader_Spa.Close();
                        objHotelDetailReader_Spa.Dispose();

                        //ProductName 
                        BookingDetail = BookingDetail.Replace("<!--##@mailProductName##-->", BookingProductITem.ProductTitle);


                       
                        //ProductAddress
                        BookingDetail = BookingDetail.Replace("<!--##@mailProductaddress##-->", cFrontproductDetail.Address);

                        //Front.FrontProductPicture cFrontPicture = new FrontProductPicture();
                        //cFrontPicture.PicturePath
                        //ProductPicture
                        BookingDetail = BookingDetail.Replace("<!--##@mailProductPicture##-->", cFrontproductDetail.Thumbnail);

                        //Service Date
                        BookingDetail = BookingDetail.Replace("<!--##@mailProduct_BookingspaDateService##-->", DateTimeCheck(BookingProductITem.DateTimeCheckIn, BookingProductITem.DateTimeConfirmCheckIn, this.BookingLangId));
                        //Service Time
                        BookingDetail = BookingDetail.Replace("<!--##@mailProduct_BookingspaTimeService##-->", DateTimeCheck_TimeOnly(BookingProductITem.DateTimeCheckIn, BookingProductITem.DateTimeConfirmCheckIn));


                        //ItemList
                        string KeywordItemList_spa = Utility.GetKeywordReplace(BookingDetail, "<!--##@mailProductItemStart##-->", "<!--##@mailProductItemEnd##-->");
                        BookingDetail = BookingDetail.Replace(KeywordItemList_spa, GetProductItem_Spa(BookingProductITem.BookingProductId, DateTimeNightTotal(BookingProductITem.DateTimeCheckIn, BookingProductITem.DateTimeCheckOut)));

                        break;

                    // Transfer
                    case 31:
                        switch (this.BookingLangId)
                        {
                            case 1:
                                them_file_item = "tbookingMail_transfer_item_en.html";
                                break;
                            case 2:
                                them_file_item = "tbookingMail_transfer_item_th.html";
                                break;
                        }
                        StreamReader objHotelDetailReader_transfer = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template_B2b/" + them_file_item + ""));
                        BookingDetail = objHotelDetailReader_transfer.ReadToEnd();
                        objHotelDetailReader_transfer.Close();
                        objHotelDetailReader_transfer.Dispose();

                        //ProductName 
                        BookingDetail = BookingDetail.Replace("<!--##@mailProductName##-->", BookingProductITem.ProductTitle);

                        ////ProductAddress
                        //BookingDetail = BookingDetail.Replace("<!--##@mailProductaddress##-->", cFrontproductDetail.Address);

                        //NumAult
                        BookingDetail = BookingDetail.Replace("<!--##@mailProductNumAdult##-->", BookingProductITem.NumAdult.ToString());
                        //Num Child
                        BookingDetail = BookingDetail.Replace("<!--##@mailProductNumChild##-->", BookingProductITem.NumChild.ToString());

                        //FlightArr
                        BookingDetail = BookingDetail.Replace("<!--##@mailProduct_FlightArr##-->", BookingProductITem.FlightNumArr + "," + DateTimeChekFullDateAndTime(BookingProductITem.FlightTimeArr));
                        //FlightDep
                        BookingDetail = BookingDetail.Replace("<!--##@mailProduct_FlightDepart##-->", BookingProductITem.FlightNumDep + "," + DateTimeChekFullDateAndTime(BookingProductITem.FlightTimeDep));


                        //ItemList
                        string KeywordItemList_transfer = Utility.GetKeywordReplace(BookingDetail, "<!--##@mailProductItemStart##-->", "<!--##@mailProductItemEnd##-->");
                        BookingDetail = BookingDetail.Replace(KeywordItemList_transfer, GetProductItem_transfer(BookingProductITem.BookingProductId, DateTimeNightTotal(BookingProductITem.DateTimeCheckIn, BookingProductITem.DateTimeCheckOut)));

                        break;
                        // Show Water Days
                case 34:
                case 36:
                case 38:
                        switch (this.BookingLangId)
                        {
                            case 1:
                                them_file_item = "tbookingMail_day_water_show_item_en.html";
                                break;
                            case 2:
                                them_file_item = "tbookingMail_day_water_show_item_th.html";
                                break;
                        }
                        StreamReader objHotelDetailReader_Show_water_Daytrip = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template_B2b/" + them_file_item + ""));
                    BookingDetail = objHotelDetailReader_Show_water_Daytrip.ReadToEnd();
                    objHotelDetailReader_Show_water_Daytrip.Close();
                    objHotelDetailReader_Show_water_Daytrip.Dispose();

                    //ProductName 
                    BookingDetail = BookingDetail.Replace("<!--##@mailProductName##-->", BookingProductITem.ProductTitle);

                    if (BookingProductITem.ProductCategory == 38)
                    {
                        //ProductAddress
                        BookingDetail = BookingDetail.Replace("<!--##@mailProductaddress##-->", cFrontproductDetail.Address);
                    }

                    //Front.FrontProductPicture cFrontPicture = new FrontProductPicture();
                    //cFrontPicture.PicturePath
                    //ProductPicture
                    BookingDetail = BookingDetail.Replace("<!--##@mailProductPicture##-->", cFrontproductDetail.Thumbnail);

                    //Tripservice Date
                    BookingDetail = BookingDetail.Replace("<!--##@mailProduct_BookingTripDate##-->", DateTimeCheck(BookingProductITem.DateTimeCheckIn, this.BookingLangId));


                    //ItemList
                    string KeywordItemList_Show_water_Daytrip = Utility.GetKeywordReplace(BookingDetail, "<!--##@mailProductItemStart##-->", "<!--##@mailProductItemEnd##-->");
                    BookingDetail = BookingDetail.Replace(KeywordItemList_Show_water_Daytrip, GetProductItem_show_day_water(BookingProductITem.BookingProductId, DateTimeNightTotal(BookingProductITem.DateTimeCheckIn, BookingProductITem.DateTimeCheckOut)));

                    break;
                // Health
                case 39:

                    StreamReader objHotelDetailReader_Health = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template_B2b/tbookingMail_health.html"));
                    BookingDetail = objHotelDetailReader_Health.ReadToEnd();
                    objHotelDetailReader_Health.Close();
                    objHotelDetailReader_Health.Dispose();

                    //ProductName 
                    BookingDetail = BookingDetail.Replace("<!--##@mailProductName##-->", BookingProductITem.ProductTitle);

                    //Check Up Date
                    BookingDetail = BookingDetail.Replace("<!--##@mailProduct_BookingspaDateService##-->", DateTimeCheck(BookingProductITem.DateTimeCheckIn, BookingProductITem.DateTimeConfirmCheckIn));
                    //Chek Up Time
                    BookingDetail = BookingDetail.Replace("<!--##@mailProduct_BookingspaTimeService##-->", DateTimeCheck_TimeOnly(BookingProductITem.DateTimeCheckIn, BookingProductITem.DateTimeConfirmCheckIn));


                    //ItemList
                    string KeywordItemList_health = Utility.GetKeywordReplace(BookingDetail, "<!--##@mailProductItemStart##-->", "<!--##@mailProductItemEnd##-->");
                    BookingDetail = BookingDetail.Replace(KeywordItemList_health, GetProductItem_health(BookingProductITem.BookingProductId, DateTimeNightTotal(BookingProductITem.DateTimeCheckIn, BookingProductITem.DateTimeCheckOut)));

                    break;
                }

                result.Append(BookingDetail);
            }

            return result.ToString();
        }

        //protected string getProductListFromBookingId_forDeal()
        //{
        //    BookingProductList cBookingProduct = new BookingProductList();

        //    PromotionContent cProContent = new PromotionContent();
        //    StringBuilder result = new StringBuilder();
        //    string BookingDetail = string.Empty;
        //    foreach (BookingProductList BookingProductITem in cBookingProduct.getProductListShowFirstByBookingId_customerDisplay(this.BookingId))
        //    {
        //        Front.FrontProductDetail cFrontproductDetail = new FrontProductDetail();
        //        if (BookingProductITem.ProductCategory != 31)
        //        {
        //            cFrontproductDetail.GetProductDetailByID(BookingProductITem.ProductID, BookingProductITem.ProductCategory, this.BookingLangId);
        //        }
        //        string them_file_item = string.Empty;

        //        if (BookingProductITem.FlagDeal == 1)
        //        {
        //            switch (this.BookingLangId)
        //            {
        //                case 1:
        //                    them_file_item = "tbookingMail_deal_item_en.html";
        //                    break;
        //                case 2:
        //                    them_file_item = "tbookingMail_deal_item_th.html";
        //                    break;
        //            }
        //            StreamReader objHotelDetailReader = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template_B2b/" + them_file_item + ""));
        //            BookingDetail = objHotelDetailReader.ReadToEnd();
        //            objHotelDetailReader.Close();
        //            objHotelDetailReader.Dispose();

        //            //ProductName 
        //            BookingDetail = BookingDetail.Replace("<!--##@mailProductName##-->", BookingProductITem.ProductTitle);


        //            //ProductAddress
        //            BookingDetail = BookingDetail.Replace("<!--##@mailProductaddress##-->", cFrontproductDetail.Address);


        //            Hotels2thailand.Production.ProductPic cPic = new Production.ProductPic();
        //            IList<object> iLisTPic = cPic.getProductPicList(5, 9, BookingProductITem.ProductID);
        //            if (iLisTPic.Count > 0)
        //            {
        //                //ProductPicture
        //                cPic = (Production.ProductPic)iLisTPic.First();
        //                string UrlPic =  cPic.PicCode;
        //                BookingDetail = BookingDetail.Replace("<!--##@mailProductPicture##-->", UrlPic);
        //            }

                   
        //            //ItemList
        //            string KeywordItemList = Utility.GetKeywordReplace(BookingDetail, "<!--##@mailProductItemStart##-->", "<!--##@mailProductItemEnd##-->");
        //            BookingDetail = BookingDetail.Replace(KeywordItemList, GetProductItemDeal(BookingProductITem.BookingProductId, DateTimeNightTotal(BookingProductITem.DateTimeCheckIn, BookingProductITem.DateTimeCheckOut)));


        //        }
               

        //        result.Append(BookingDetail);
        //    }

        //    return result.ToString();
        //}

        protected string EncodeId(int IdtoEncod)
        {
            string Random = Hotels2String.Hotels2RandomStringNuM(20);
            string strToEndCode = IdtoEncod + Random;
            string EncodeResult = strToEndCode.Hotel2EncrytedData_SecretKey();
            return HttpUtility.UrlEncode(EncodeResult);
        }

        protected string EncodeId(int BookingId, int BookingProductId, string Email,byte bytLangId)
        {
            string Random = Hotels2String.Hotels2RandomStringNuM(20);
            string strToEndCode = BookingId + "," + BookingProductId + "," + Email + "," + bytLangId + Random;
            string EncodeResult = strToEndCode.Hotel2EncrytedData_SecretKey();
            return HttpUtility.UrlEncode(EncodeResult);
        }

        public string MapProductCatLink(byte bytProductCat, int ProductId)
        {
            Hotels2thailand.Production.ProductBookingEngine cProduct = new Hotels2thailand.Production.ProductBookingEngine();

            cProduct = cProduct.GetProductbookingEngine(ProductId);


            //string KeyCat = Utility.GetProductType(bytProductCat)[0, 3];
            string resutlLink = cProduct.B2bMap;
            //string resutlLink = "thailand-" + KeyCat + "-" + "map.aspx?pid=" + ProductId;
            return resutlLink;
        }


        protected string TextDetailBookingREcievedBankTranfer()
        {
            string result = string.Empty;
            //result = "<tr><td colspan=\"2\" style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:15px 20px 5px 20px; color:#6d6e71;\"><p style=\"font-weight:bold\">ในกรณีที่เราสามารถยืนยันการจองห้องพักของท่านได้ พร้อมทั้งการชำระเงินของท่านเสร็จสมบูรณ์แล้ว ท่านจะได้รับเอกสารยืนยันการจอง (Hotel Voucher) เป็นเอกสารยืนยันการเข้าพักจากทางเวปไซต์โฮเทลทูฯค่ะ</p></td></tr>";

                 
                 result = result + "<tr>";
                 result = result + "<td  style=\" font-family:Tahoma; font-size:12px;color:#6d6e71; font-weight:bold;\">";
                 result = result + "ในกรณีที่เราสามารถยืนยันการจองห้องพักของท่านได้ พร้อมทั้งการชำระเงินของท่านเสร็จสมบูรณ์แล้ว ท่านจะได้รับเอกสารยืนยันการจอง (Hotel Voucher) เป็นเอกสารยืนยันการเข้าพักจากทางเวปไซต์โฮเทลทูฯค่ะ";
                 result = result + "</td>";
                 result = result + "</tr>";
                 result = result + "<tr><td style=\"height:10px;\" height=\"10\"></td></tr>";
            return result;
        }

        protected string TextDetailREview()
        {
            BookingProductList cBookingProduct = new BookingProductList();
            BookingdetailDisplay cBookingDetail = new BookingdetailDisplay();
            cBookingDetail = cBookingDetail.GetBookingDetailListByBookingId(this.BookingId);
            IList<object> iListBookningProduct = cBookingProduct.getProductListShowFirstByBookingId(this.BookingId);
            StringBuilder result = new StringBuilder();

            string Btn = string.Empty;
            string txt3 = string.Empty;
            if (this.BookingLangId == 1)
            {
                Btn = "btn_review_en.jpg";
                txt3 = "If you can not click the links, please copy this URL and paste on your browser";
            }
            else
            {
                Btn = "btn_review_th.jpg";
                txt3 = "หากท่านไม่สามารถคลิกลิงค์ได้ กรุณาคัดลอก URL นี้แล้วนำไปเปิดในช่อง Browser ของท่านค่ะ";
            }
                
            result.Append("<tr>");
            result.Append("<td  style=\" margin:0px; padding:20px;\">");
            result.Append("<table cellpadding=\"0\" cellspacing=\"0\" width=\"620\"  style=\"background:#f1f9fb;\">");
            foreach (BookingProductList BookingProductITem in iListBookningProduct)
            {
                if (BookingProductITem.ProductCategory != 31)
                {
                    Front.FrontProductDetail cFrontproductDetail = new FrontProductDetail();
                    cFrontproductDetail.GetProductDetailByID(BookingProductITem.ProductID, BookingProductITem.ProductCategory, this.BookingLangId);

                    string strB2bMap = this.cProductBookingEngine.B2bMap;
                    string[] arrMAp = strB2bMap.Split('&');

                    string ProductCat = arrMAp[1].Replace("cat=", "").Trim();
                    string Product = arrMAp[0].Split('?')[1].Replace("pid=", " ").Trim();



                    Hotel2Service.ServiceB2b cService = new Hotel2Service.ServiceB2b();
                    Hotel2Service.FrontProductDetail ServiceFrontDetail = cService.GetPicThumbnail(int.Parse(Product), byte.Parse(ProductCat));
                    
                    //string Thumb = cService.GetPicThumbnail(int.Parse(Product), byte.Parse(ProductCat));

                    result.Append("<tr> ");
                    result.Append("<td  colspan=\"2\" height=\"20\"></td>");
                    result.Append("</tr>");
                    result.Append("<tr>");
                    result.Append("<td rowspan=\"5\" align=\"center\">");
                    result.Append("<table cellpadding=\"0\" cellspacing=\"0\" style=\"width:90px;height:80px;\">");
                    result.Append("<tr>");
                    result.Append("<td width=\"90\" height=\"80\" align=\"center\" style=\"padding:5px; margin:0px;background-color:#2d96e2;\"><img src=\"http://www.hotels2thailand.com" + cFrontproductDetail.Thumbnail + "\" alt=\"\" width=\"80\" height=\"70\" /></td>");
                    result.Append("</tr>");
                    result.Append("</table>");
                    result.Append("</td>");

                    result.Append("<td style=\"font-family:Tahoma; font-size:18px; color:#31b105; height:40px;\">");

                    result.Append("<table cellpadding=\"0\" cellspacing=\"0\">");
                    result.Append("<tr>");
                    result.Append("<td style=\" margin:0px; padding:0px;font-family:Tahoma; font-size:18px; color:#31b105; height:40px;\">");
                    result.Append(cFrontproductDetail.Title);
                    result.Append("</td>");
                    result.Append("<td align=\"left\" style=\"margin:0px;padding:0px;\">");
                    result.Append(getStarPicColor(Convert.ToByte(Math.Round(cFrontproductDetail.Star))));
                    result.Append("</td>");
                    result.Append("</tr>");
                    result.Append("</table>");

                    result.Append("</td>");
                    result.Append("</tr>");
                    result.Append("<tr>");
                    result.Append("<td>");

                    string UrlQury = Hotels2String.EncodeIdToURL(BookingProductITem.ProductID + "&cus_id=" + cBookingDetail.CusId);

                    result.Append("<a href=\"http://www.hotels2thailand.com/review_write.aspx?pid=" + UrlQury + "\">");
                    result.Append("<img src=\"http://www.hotels2thailand.com/images_mail/" + Btn + "\" alt=\"\" style=\" border:0px; width:116px; height:23px;\" />");
                    result.Append("</a>");
                    result.Append("</td>");
                    result.Append("</tr>");
                    result.Append("<tr> <td  height=\"7\"> </td></tr>");
                    result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma; font-size:11px; font-style:italic; color:#898b90; \">**" + txt3 + "</td>");
                    result.Append("</tr>");

                    result.Append("<tr><td style=\"font-family:Tahoma; font-size:12px; color:#898b90;\">http://www.hotels2thailand.com/review_write.aspx?pid=" + UrlQury + "</td></tr>");
                    result.Append("<tr> <td  height=\"20\"> </td></tr>");
                    result.Append("<tr> <td   colspan=\"2\" align=\"center\" ><img src=\"http://www.hotels2thailand.com/images_mail/line_review.jpg\" alt=\"\" width=\"575\" height=\"25\" /> </td></tr>");
                }
            }


            result.Append("<tr>");
            result.Append("<td rowspan=\"5\" width=\"138\" height=\"58\" style=\"padding:10px; margin:0px;\"><img src=\"http://www.hotels2thailand.com/images_mail/hotels2productService.jpg\" alt=\"\" width=\"138\" height=\"58\" /></td>");
            result.Append("<td style=\" font-family:Tahoma; font-size:18px; color:#31b105; height:40px;\">");
            result.Append("Hotels2Thailand.com Product and Service");
            result.Append("</td>");
            result.Append("</tr>");
            result.Append(" <tr>");
            result.Append("<td>");
            result.Append("<a  href=\"http://www.hotels2thailand.com/review_site_write.aspx?cus_id=" + cBookingDetail.CusId + "&langid=" + cBookingDetail.LangId + "\">");
            result.Append("<img src=\"http://www.hotels2thailand.com/images_mail/" + Btn + "\" alt=\"\" style=\" border:0px; width:116px; height:23px;\" />");
            result.Append("</a>");
            result.Append("</td>");
            result.Append("</tr>");
            result.Append("<tr> <td  height=\"7\"> </td></tr>");
            result.Append("<tr>");
            result.Append("<td style=\"font-family:Tahoma; font-size:11px; font-style:italic; color:#898b90;\">**" + txt3 + "</td>");
            result.Append("</tr>");

            result.Append("<tr><td style=\"font-family:Tahoma; font-size:12px; color:#898b90;\">http://www.hotels2thailand.com/review_site_write.aspx?cus_id=" + cBookingDetail.CusId + "&langid=" + cBookingDetail.LangId + "</td></tr>");
            result.Append("<tr> <td  height=\"20\"> </td></tr>");
            result.Append("<tr> <td  height=\"20\"> </td></tr>");
            result.Append("</table>");
            result.Append("</td>");
            result.Append("</tr>");
            result.Append("<tr><td style=\"height:20px;\" height=\"20\"></td></tr>");



            return result.ToString();
        }

        protected string TextDetailVoucher()
        {
            //Hotels2thailand.Booking.BookingMailEngine cmailEngine = new BookingMailEngine();
            BookingProductList cBookingProduct = new BookingProductList();
            IList<object> iListBookningProduct = cBookingProduct.getProductListShowFirstByBookingId_customerDisplay(this.BookingId);
            StringBuilder result = new StringBuilder();

            string txt1 = string.Empty;
            string txt2 = string.Empty;
            string txt3 = string.Empty;
            string txt4 = string.Empty;
            string txt5 = string.Empty;
            string txt6 = string.Empty;

            switch (this.BookingLangId)
            {
                case 1:
                    txt1 = "Print hotel voucher and map";
                    txt2 = "Print Hotel Map.";
                    txt3 = "If you can not click this link, please copy this URL and paste on your browser";
                    txt4 = "Print Voucher";
                    txt5 = "View Map";
                    break;
                case 2:
                    txt1 = "กรุณาคลิกลิงค์นี้เพื่อปรินท์เอกสารยืนยันการจองห้องพัก";
                    txt2 = "คลิกเพื่อดูแผนที่โรงแรม";
                    txt3 = "หากท่านไม่สามารถคลิกลิงค์ได้ กรุณาคัดลอก URL นี้แล้วนำไปเปิดในช่อง Browser ของท่านค่ะ";
                    txt4 = "พิมพ์ เอกสารยืนยันการจอง";
                    txt5 = "พิมพ์ แผนที่";
                    break;
            }
            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
            result.Append("<tr>");
            result.Append("<td colspan=\"2\" style=\"margin:0px;padding:0px; font-size:11px;color:#63a614;\">" + txt1 + "</td>");
            result.Append("</tr>");
            result.Append("<tr>");
            result.Append("<td colspan=\"2\" tyle=\"margin:0px;padding:0px;\" align=\"center\"><img src=\"http://www.hotels2thailand.com/images_mail/line-voucher.jpg\" width=\"639\" height=\"25\" alt=\"\" /></td>");
            result.Append("</tr>");

            foreach (BookingProductList BookingProductITem in iListBookningProduct)
            {

                result.Append("<tr>");
                result.Append("<td style=\"margin:0px; padding:0px;\">");
                result.Append("<table cellpadding=\"0\" cellspacing=\"0\" width=\"650\">");


                result.Append("<tr>");
                result.Append("<td colspan=\"2\"  style=\" margin:0px; padding:0px 0px 0px 20px; color:#4a81a8; font-family:Tahoma;font-size:16px; height:25px;\">");
                result.Append(BookingProductITem.ProductTitle);
                result.Append("</td>");
                result.Append("</tr>");
                result.Append("<tr><td colspan=\"2\"  height=\"10\" ></td></tr>");
                result.Append("<tr>");
                result.Append("<td valign=\"middle\" rowspan=\"2\" align=\"center\" style=\" width:110px;\"><a href=\"" + _mainsite + "Voucher.aspx?id=" + EncodeId(BookingProductITem.BookingProductId) + "\" ><img src=\"http://www.hotels2thailand.com/images_mail/hotels2voucher.jpg\" style=\"border:0px;\" alt=\"\"  width=\"73\" height=\"39\" /></a></td>");
                result.Append("<td style=\"color:#6d6e71;font-family:Tahoma; font-size:11px; font-style:italic;\">");

                result.Append("<span style=\"font-size:14px;color:#63a614; font-style:normal;\"> <a href=\"" + _mainsite + "Voucher.aspx?id=" + EncodeId(BookingProductITem.BookingProductId) + "\" >" + txt4 + "</a></span> **" + txt3);
                result.Append("</td>");
                result.Append("</tr>");
                result.Append("<tr>");
                result.Append("<td style=\"color:#6d6e71;font-family:Tahoma; font-size:11px;\">" + _mainsite + "Voucher.aspx?id=" + EncodeId(BookingProductITem.BookingProductId) + "</td>");
                result.Append("</tr>");
                result.Append("<tr><td colspan=\"2\"  height=\"20\"></td></tr>");
                if (BookingProductITem.ProductCategory != 31 && BookingProductITem.ProductCategory != 34)
                {
                    result.Append("<tr>");
                    result.Append("<td valign=\"middle\" rowspan=\"2\" align=\"center\" style=\" width:110px;color:#6d6e71;\"><a href=\"" + MapProductCatLink(BookingProductITem.ProductCategory, BookingProductITem.ProductID) + "\" ><img src=\"http://www.hotels2thailand.com/images_mail/mail_map.jpg\" style=\"border:0px;\" alt=\"\"  width=\"40\" height=\"36\" /></a></td>");
                    result.Append("<td style=\"color:#6d6e71;font-family:Tahoma; font-size:11px; font-style:italic;\">");
                    result.Append("<span style=\"font-size:14px;color:#63a614;font-style:normal;\"><a href=\""  + MapProductCatLink(BookingProductITem.ProductCategory, BookingProductITem.ProductID) + "\" >" + txt5 + "</a></span> **" + txt3);
                    result.Append("</td>");
                    result.Append("</tr>");
                    result.Append("<tr>");
                    result.Append("<td style=\"color:#6d6e71;font-family:Tahoma; font-size:11px;\">"  + MapProductCatLink(BookingProductITem.ProductCategory, BookingProductITem.ProductID) + "</td>");
                    result.Append("</tr>");
                }
                
                result.Append("<tr>");
                result.Append("<td colspan=\"2\" align=\"center\"><img src=\"http://www.hotels2thailand.com/images_mail/line-voucher.jpg\" width=\"639\" height=\"25\" alt=\"\" /></td>");
                result.Append("</tr>");
                result.Append("</table>");
                result.Append("</td>");
                result.Append("</tr>");
                result.Append("<tr><td style=\"height:20px;\" height=\"20\"></td></tr>");

            }

        return result.ToString();
        }

        //protected string TextDetailVoucherForDeal()
        //{
        //    BookingProductList cBookingProduct = new BookingProductList();
        //    IList<object> iListBookningProduct = cBookingProduct.getProductListShowFirstByBookingId_customerDisplay(this.BookingId);
        //    StringBuilder result = new StringBuilder();

        //    string txt1 = string.Empty;
        //    string txt2 = string.Empty;
        //    string txt3 = string.Empty;
        //    string txt4 = string.Empty;
        //    string txt5 = string.Empty;
        //    string txt6 = string.Empty;

        //    switch (this.BookingLangId)
        //    {
        //        case 1:
        //            txt1 = "Print hotel voucher and map";
        //            txt2 = "Print Hotel Map.";
        //            txt3 = "If you can not click this link, please copy this URL and paste on your browser";
        //            txt4 = "Print Voucher";
        //            txt5 = "View Map";
        //            break;
        //        case 2:
        //            txt1 = "พิมพ์คูปองและแผนที่";
        //            txt2 = "คลิกเพื่อดูแผนที่โรงแรม";
        //            txt3 = "หากท่านไม่สามารถคลิกลิงค์ได้ กรุณาคัดลอก URL นี้แล้วนำไปเปิดในช่อง Browser ของท่านค่ะ";
        //            txt4 = "พิมพ์คูปอง";
        //            txt5 = "พิมพ์แผนที่";
        //            break;
        //    }
        //    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
        //    result.Append("<tr>");
        //    result.Append("<td colspan=\"2\" style=\"margin:0px;padding:0px; font-size:11px;color:#63a614;\">" + txt1 + "</td>");
        //    result.Append("</tr>");
        //    result.Append("<tr>");
        //    result.Append("<td colspan=\"2\" tyle=\"margin:0px;padding:0px;\" align=\"center\"><img src=\"http://www.hotels2thailand.com/images_mail/line-voucher.jpg\" width=\"639\" height=\"25\" alt=\"\" /></td>");
        //    result.Append("</tr>");

        //    foreach (BookingProductList BookingProductITem in iListBookningProduct)
        //    {

        //        result.Append("<tr>");
        //        result.Append("<td style=\"margin:0px; padding:0px;\">");
        //        result.Append("<table cellpadding=\"0\" cellspacing=\"0\" width=\"650\">");


        //        result.Append("<tr>");
        //        result.Append("<td colspan=\"2\"  style=\" margin:0px; padding:0px 0px 0px 20px; color:#4a81a8; font-family:Tahoma;font-size:16px; height:25px;\">");
        //        result.Append(BookingProductITem.ProductTitle);
        //        result.Append("</td>");
        //        result.Append("</tr>");
        //        result.Append("<tr><td colspan=\"2\"  height=\"10\" ></td></tr>");
        //        result.Append("<tr>");
        //        result.Append("<td valign=\"middle\" rowspan=\"2\" align=\"center\" style=\" width:110px;\"><a href=\"" + _mainsite + "members/guestVoucher.aspx?id=" + EncodeId(this.BookingId, BookingProductITem.BookingProductId, this.GetEmailBooking(), this.BookingLangId) + "\" ><img src=\"http://www.hotels2thailand.com/images_mail/hotels2voucher.jpg\" style=\"border:0px;\" alt=\"\"  width=\"73\" height=\"39\" /></a></td>");
        //        result.Append("<td style=\"color:#6d6e71;font-family:Tahoma; font-size:11px; font-style:italic;\">");

        //        result.Append("<span style=\"font-size:14px;color:#63a614; font-style:normal;\"> <a href=\"" + _mainsite + "members/guestVoucher.aspx?id=" + EncodeId(this.BookingId, BookingProductITem.BookingProductId,this.GetEmailBooking(),this.BookingLangId) + "\" >" + txt4 + "</a></span> **" + txt3);
        //        result.Append("</td>");
        //        result.Append("</tr>");
        //        result.Append("<tr>");
        //        result.Append("<td style=\"color:#6d6e71;font-family:Tahoma; font-size:11px;\">" + _mainsite + "members/guestVoucher.aspx?id=" + EncodeId(this.BookingId, BookingProductITem.BookingProductId, this.GetEmailBooking(), this.BookingLangId) + "</td>");
        //        result.Append("</tr>");
        //        result.Append("<tr><td colspan=\"2\"  height=\"20\"></td></tr>");
        //        if (BookingProductITem.ProductCategory != 31 && BookingProductITem.ProductCategory != 34)
        //        {
        //            result.Append("<tr>");
        //            result.Append("<td valign=\"middle\" rowspan=\"2\" align=\"center\" style=\" width:110px;color:#6d6e71;\"><a href=\"" + _mainsite + MapProductCatLink(BookingProductITem.ProductCategory, BookingProductITem.ProductID) + "\" ><img src=\"http://www.hotels2thailand.com/images_mail/mail_map.jpg\" style=\"border:0px;\" alt=\"\"  width=\"40\" height=\"36\" /></a></td>");
        //            result.Append("<td style=\"color:#6d6e71;font-family:Tahoma; font-size:11px; font-style:italic;\">");
        //            result.Append("<span style=\"font-size:14px;color:#63a614;font-style:normal;\"><a href=\"" + _mainsite + MapProductCatLink(BookingProductITem.ProductCategory, BookingProductITem.ProductID) + "\" >" + txt5 + "</a></span> **" + txt3);
        //            result.Append("</td>");
        //            result.Append("</tr>");
        //            result.Append("<tr>");
        //            result.Append("<td style=\"color:#6d6e71;font-family:Tahoma; font-size:11px;\">" + _mainsite + MapProductCatLink(BookingProductITem.ProductCategory, BookingProductITem.ProductID) + "</td>");
        //            result.Append("</tr>");
        //        }

        //        result.Append("<tr>");
        //        result.Append("<td colspan=\"2\" align=\"center\"><img src=\"http://www.hotels2thailand.com/images_mail/line-voucher.jpg\" width=\"639\" height=\"25\" alt=\"\" /></td>");
        //        result.Append("</tr>");
        //        result.Append("</table>");
        //        result.Append("</td>");
        //        result.Append("</tr>");
        //        result.Append("<tr><td style=\"height:20px;\" height=\"20\"></td></tr>");

        //    }

        //    return result.ToString();
        //}

        protected string TextDetailTop(byte TypeMail)
        {
            StringBuilder result = new StringBuilder();
            //result.Append("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
            //result.Append("<tr><td style=\"height:20px;\"></td></tr>");
            
            switch (TypeMail)
            {
                    //Resubmit
                case 1 :
                    switch (this.BookingLangId)
                    {
                        case 1:
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            result.Append("Thank you very much for using our service. This is to inform that your reservation request is AVAILABLE <span style=\"color:#000\"> (#" + this.BookingId + ")</span> but your payment has not been paid. In order to process your booking completely, please click the “Make Payment” button below to complete your payment thru our high security online system.");
                            result.Append("</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            result.Append("The availability bases on First Come First Serve. We are unable to secure your reservation unless the payment is paid in full successfully.");
                            result.Append("</td>");
                            result.Append("</tr>");

                            
                            break;
                        case 2:
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            result.Append("เว็บไซด์โฮเทลทูไทยแลนด์ (<a href=\"http://www.hotels2thailand.com\"> Hotels2thailand.com </a>)");
                            result.Append("ขอขอบพระคุณที่ใช้บริการจองกับเรา</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                    switch (this.cClassBookingProduct.ProductCategory)
                    {
                        case 29:
                            result.Append("ทางเวปไซต์โฮเทลทูฯยินดีที่จะแจ้งให้ท่านทราบว่า<strong>ห้องพักที่ท่านต้องการนั้นยังว่างอยู่ค่ะ</strong>แต่ทางเรายังไม่สามารถสำรองห้องพักให้กับท่านหากการชำระเงินยังไม่สมบูรณ์ค่ะ ห้องพักที่ท่านต้องการอาจจะเต็มได้ทุกเมื่อ ดังนั้นทางเวปไซต์โฮเทลทูขออนุญาตแนะนำให้ท่านทำการชำระเงินโดยเร็วที่สุดเพื่อทางเราจะได้ดำเนินการสำรองห้องพักให้กับท่านทันทีค่ะ");
                            break;
                        case 32:
                            result.Append("ทางเวปไซต์โฮเทลทูฯยินดีที่จะแจ้งให้ท่านทราบว่า <strong>วันเวลาและสนามที่ท่านต้องการออกรอบยังว่างอยู่ค่ะ </strong>แต่ทางเรายังไม่สามารถดำเนินการจองให้กับท่านหากการชำระเงินยังไม่สมบูรณ์ค่ะ สนามในวันและเวลาที่ท่านต้องการอาจจะเต็มได้ทุกเมื่อ ดังนั้นทางเวปไซต์โฮเทลทูขออนุญาตแนะนำให้ท่านทำการชำระเงินโดยเร็วที่สุดเพื่อทางเราจะได้ดำเนินการจองให้กับท่านทันทีค่ะ");
                            break;
                        case 40:
                            result.Append("ทางเวปไซต์โฮเทลทูฯยินดีที่จะแจ้งให้ท่านทราบว่า <strong>" + this.cClassBookingProduct.ProductTitle + "ที่ท่านต้องการยังว่างอยู่ค่ะ</strong> แต่ทางเรายังไม่สามารถดำเนินการจองให้กับท่านหากการชำระเงินยังไม่สมบูรณ์ค่ะ โปรแกรมในวันและเวลาที่ท่านต้องการอาจจะเต็มได้ทุกเมื่อ ดังนั้นทางเวปไซต์โฮเทลทูขออนุญาตแนะนำให้ท่านทำการชำระเงินโดยเร็วที่สุดเพื่อทางเราจะได้ดำเนินการจองให้กับท่านทันทีค่ะ");
                            break;
                        case 38:
                        case 36:
                        case 34:
                            result.Append("ทางเวปไซต์โฮเทลทูฯยินดีที่จะแจ้งให้ท่านทราบว่า<strong>" + this.cClassBookingProduct.ProductTitle + "ที่ท่านต้องการยังว่างอยู่ค่ะ แต่ทางเรายังไม่สามารถดำเนินการจองให้กับท่านหากการชำระเงินยังไม่สมบูรณ์ค่ะ โปรแกรมในวันและเวลาที่ท่านต้องการอาจจะเต็มได้ทุกเมื่อ ดังนั้นทางเวปไซต์โฮเทลทูขออนุญาตแนะนำให้ท่านทำการชำระเงินโดยเร็วที่สุดเพื่อทางเราจะได้ดำเนินการจองให้กับท่านทันทีค่ะ");
                            break;
                       

                    }
                    
                    result.Append("</td>");
                    result.Append("</tr>");
                            break;
                    }

                    break;
                    //Receive Mail
                case 2 :

                    switch (this.BookingLangId)
                    {
                        case 1:
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            result.Append("Thank you very much for making the reservation with <a href=\"http://www.hotels2thailand.com\"> www.hotels2thailand.com </a>");
                            result.Append("</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;\">We have received your booking at " + this.cClassBookingProduct.ProductTitle + ". Your Booking ID# " + this.BookingId + " and your payment is paid in full. However, this email is not a confirmation. Our staff is checking room availability and will contact you in 24 hours.");
                            result.Append(" </td>");
                            result.Append("</tr>");

                            break;
                        case 2:
                            result.Append("<tr>");
                            result.Append("<td  style=\"margin:0px; padding:0px;\">");
                            result.Append("เว็บไซด์โฮเทลทูไทยแลนด์ (<a href=\"http://www.hotels2thailand.com\"> Hotels2thailand.com</a>)");
                            result.Append(" ขอขอบพระคุณที่ใช้บริการจองกับเรา");
                            result.Append("</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");

                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            switch (this.cClassBookingProduct.ProductCategory)
                            {
                                case 29:
                                    result.Append("เราได้รับข้อมูลการจองของท่านพร้อมทั้งการชำระเงินผ่านระบบเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการชำระเงินของท่านกับเราเท่านั้น<strong> ยังมิใช่เอกสารยืนยันการจองห้องพัก (Hotel Voucher)</strong>");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะห้องว่างที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 24</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    break;
                                case 32:
                                    result.Append("เราได้รับข้อมูลการจองของท่านพร้อมทั้งการชำระเงินผ่านระบบเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการชำระเงินของท่านกับเราเท่านั้น <strong> ยังมิใช่เอกสารยืนยันการจองสนามกอล์ฟ (Voucher)</strong> ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะการจองสนามกอล์ฟที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 24</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    break;
                                case 40:
                                    result.Append("เราได้รับข้อมูลการจองของท่านพร้อมทั้งการชำระเงินผ่านระบบเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการชำระเงินของท่านกับเราเท่านั้น <strong> ยังมิใช่เอกสารยืนยันการจองโปรแกรมสปา (Voucher)</strong> ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะโปรแกรมสปาที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 24</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    break;
                                case 38:
                                case 36:
                                case 34:
                                    result.Append("เราได้รับข้อมูลการจองของท่านพร้อมทั้งการชำระเงินผ่านระบบเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการชำระเงินของท่านกับเราเท่านั้น <strong> ยังมิใช่เอกสารยืนยันการจองบริการ (Voucher)</strong> ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะบริการที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 24</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    break;
                            }
                            
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            result.Append("คุณสามารถใช้หมายเลขการจอง <strong> #" + this.BookingId + "</strong> นี้ในการตรวจสอบสถานะบุคกิ้งของคุณกับเราค่ะ");
                            result.Append("</td>");
                            result.Append("</tr>");
                            break;
                    }
                     
                    break;
                    // Mail Send Voucher & Map & Recept
                case 3 :
                    switch (this.BookingLangId)
                    {
                        case 1:
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            result.Append("We would like to thank you for making the reservation with www.hotels2thailand.com. We are pleased to inform you that your booking at "+this.cClassBookingProduct.ProductTitle+" #" + this.BookingId + " is CONFIRMED. </a>");
                           
                            result.Append("</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                             result.Append("<tr>");
                             result.Append("<td style=\"margin:0px; padding:0px;\">");
                            result.Append("Your payment is paid in full. Please see your booking detail below.");
                            result.Append("</td>");
                            result.Append("</tr>");
                            break;
                        case 2:

                            string txtLast = string.Empty;
                            
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            result.Append("เวปไซต์โฮเทลทูไทยแลนด์ <a href=\"http://www.hotels2thailand.com\">(Hotels2thailand.com)</a> ขอขอบพระคุณที่ใช้บริการจองกับเรา โปรดปรินท์เอกสารยืนยันนี้และแสดงต่อเจ้าหน้าที่ในวันที่ใช้บริการค่ะ");
                            
                            result.Append("</td>");
                            result.Append("</tr>");

                            break;
                    }
                    
                    break;
                // Mail Review 
                case 4:
                    switch (this.BookingLangId)
                    { 
                        case 1:
                        result.Append("<tr>");
                        result.Append("<td style=\"margin:0px; padding:0px;\">");
                        result.Append("Thank you for choosing <a href=\"http://www.hotels2thailand.com\">www.hotels2thailand.com</a></a>");
                        result.Append("</td>");
                        result.Append("</tr>");
                        result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                        result.Append("<tr>");
                        result.Append("<td style=\"margin:0px; padding:0px;\">");
                        result.Append("As valued customer, your opinions on all aspects of your stay are very important to us. We would be grateful if you would complete this questionnaire and send us back in order to improve our service to all customer's satisfaction.</a>");
                      result.Append("</td>");
                        result.Append("</tr>");
                             result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                        result.Append("<td style=\"margin:0px; padding:0px;\">");
                            result.Append("Please note that you do not have to answer all the questions. We will use our effort to serve you the best service.");
                            result.Append("</td>");
                        result.Append("</tr>");
                            break;
                        case 2:
                         result.Append("<tr>");
                         result.Append("<td style=\"margin:0px; padding:0px;\">");
                         result.Append("เวปไซต์โฮเทลทูไทยแลนด์ <a href=\"http://www.hotels2thailand.com\">(Hotels2thailand.com)</a> ขอขอบพระคุณที่ใช้บริการจองกับเรา");
                         result.Append("</td>");
                         result.Append("</tr>");
                         result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                        result.Append("<tr>");
                        result.Append("<td style=\"margin:0px; padding:0px;\">");
                        result.Append("ความคิดเห็นของท่านมีค่ากับเราเสมอ กรุณาแสดงความคิดเห็นเกี่ยวกับโรงแรมที่พักและการบริการให้กับเราเพื่อการปรับปรุงข้อบกพร่องและเพื่อเพิ่มความพึงพอใจของท่านในโอกาสต่อๆไปด้วยค่ะ");
                        result.Append("</td>");
                        result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                        result.Append("<td style=\"margin:0px; padding:0px;\">");
                        result.Append("ท่านไม่จำเป็นต้องตอบทุกคำถามค่ะ เราจะพยายามปรับปรุงการบริการของเราให้ดีที่สุดค่ะ");
                            result.Append("</td>");
                        result.Append("</tr>");
                            
                            break;
                    }
                    

                    break;
                //Receive Mail Book Now
                case 5 :
                     result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Thank you for choosing  <a href=\"http://www.hotels2thailand.com\"> www.hotels2thailand.com </a>  “Book Now Pay Later”  ");
                    result.Append("</td>");
                    result.Append("</tr>");


                     result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71; font-weight:bold;\">");
                    result.Append("This booking is NOT instant confirmation. Our reservation officer will check room availability and contact you back in 24 hours. </a>");
                    //result.Append("<span style=\"color:#000\">ORDER ID: " + this.BookingId + " </span>");
                    result.Append("</td>");
                    result.Append("</tr>");


                    break;
                // Voucher book now
                case 6 :
                    result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Greeting from www.hotels2thailand.com ");
                    result.Append("</td>");
                    result.Append("</tr>");

                     result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Thank you for choosing  <a href=\"http://www.hotels2thailand.com\"> www.hotels2thailand.com </a>&nbsp; <strong> “Book Now Pay Later” </strong> ");
                    result.Append("</td>");
                    result.Append("</tr>");


                     result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Your ORDER ID : " + this.BookingId + " &nbsp;is <strong>CONFIRMED.</strong>");
                    //result.Append("<span style=\"color:#000\">ORDER ID: " + this.BookingId + " </span>");
                    result.Append("</td>");
                    result.Append("</tr>");

                     result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71; \">");
                    result.Append("We have charged you $1 for holding guarantee of your booking. We do not charge the total cost on your credit card when you book. Your credit card will be automatic charged for remaining total cost 1-3 days (Or as hotel policy stated) before check in date. Please check the sufficiency of your credit limit before due date.");
                    //result.Append("<span style=\"color:#000\">ORDER ID: " + this.BookingId + " </span>");
                    result.Append("</td>");
                    result.Append("</tr>");

                    result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("If the booking is canceled or insufficient fund on the date of charge your credit card, your holding guarantee 1$ will not be refunded.");
                    //result.Append("<span style=\"color:#000\">ORDER ID: " + this.BookingId + " </span>");
                    result.Append("</td>");
                    result.Append("</tr>");
                    break;
                case 7 :
                    result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Greeting from www.hotels2thailand.com ");
                    result.Append("</td>");
                    result.Append("</tr>");

                     result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Thank you for choosing  <a href=\"http://www.hotels2thailand.com\"> www.hotels2thailand.com </a>&nbsp; <strong> “Book Now Pay Later” </strong> ");
                    result.Append("</td>");
                    result.Append("</tr>");

                    result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71; \">");
                    result.Append("Please be informed that your ORDER ID: " + this.BookingId + "  is <strong> CONFIRMED.The total cost is charged successfully.</strong>");
                    //result.Append("<span style=\"color:#000\">ORDER ID: " + this.BookingId + " </span>");
                    result.Append("</td>");
                    result.Append("</tr>");
                    break;
                    //Online Charge
                case 8 :
                     result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Greeting from www.hotels2thailand.com ");
                    result.Append("</td>");
                    result.Append("</tr>");

                     result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Thank you for choosing  <a href=\"http://www.hotels2thailand.com\"> www.hotels2thailand.com </a>&nbsp; <strong> “Book Now Pay Later” </strong> ");
                    result.Append("</td>");
                    result.Append("</tr>");

                    result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Please kindly be informed that the remaining total cost of your booking is not be able to charge successfully. This is due to your credit card has some limitations to <strong>DECLINE.</strong> Please recheck the following criteria.  ");
                    result.Append("</td>");
                    result.Append("</tr>");

                    result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("<p>Possible Reasons for Declined Cards;</p>");
                    result.Append("<ul>");
                    result.Append("<li>There were insufficient funds on your credit card.</li>");
                    result.Append("<li>The credit card number or the expiry date was incorrect.</li>");
                    result.Append("<li>Your name and address are not matched</li>");
                    result.Append("<li>Make sure it's credit card not debit card.</li>");
                    result.Append("<li>Some credit card companies will reject international charges. Please contact to your bank.</li>");
                    result.Append("<li>Your bank or credit card company was having technical issues. Please contact to your bank.</li>");
                    result.Append("");
                    result.Append("</ul>");
                    result.Append("</td>");
                    result.Append("</tr>");

                     result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("<strong>If the problem persists after trying several times, we are pleased to offer you special discount, if you are convenient to pay online full payment to our payment system. </strong>");
                    result.Append("</td>");
                    result.Append("</tr>");

                    result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("<strong>If you wish to continue the booking, please click “Make Payment” </strong>");
                    result.Append("</td>");
                    result.Append("</tr>");
                    break;
                    //fully book and Offer
                case 9:
                     result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Greeting from www.hotels2thailand.com ");
                    result.Append("</td>");
                    result.Append("</tr>");

                     result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Thank you for choosing  <a href=\"http://www.hotels2thailand.com\"> www.hotels2thailand.com </a>&nbsp; <strong> “Book Now Pay Later” </strong> ");
                    result.Append("</td>");
                    result.Append("</tr>");

                    result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("<strong>We're very sorry to inform you that all of room types below currently is fully booked.</strong>");
                    result.Append("</td>");
                    result.Append("</tr>");

                   

                    break;
                    //Cancellation
                case 10:
                      result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Greeting from www.hotels2thailand.com ");
                    result.Append("</td>");
                    result.Append("</tr>");

                     result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Thank you for choosing  <a href=\"http://www.hotels2thailand.com\"> www.hotels2thailand.com </a>&nbsp; <strong> “Book Now Pay Later” </strong> ");
                    result.Append("</td>");
                    result.Append("</tr>");

                    result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Due to your credit card is DECLINED. Please be informed that your booking  below (ORDER ID : "+this.BookingId+")has been <strong>CANCELLED.</strong>");
                    result.Append("</td>");
                    result.Append("</tr>");

                    result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Once again we do apologize for any inconvenience caused. We do hope that you will allow us the opportunity to be of service to you in the future.");
                    result.Append("</td>");
                    result.Append("</tr>");
                    break;
                    //Fully book And offer new product
                case 11:
                    result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Greeting from www.hotels2thailand.com ");
                    result.Append("</td>");
                    result.Append("</tr>");

                     result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Thank you for choosing  <a href=\"http://www.hotels2thailand.com\"> www.hotels2thailand.com </a>&nbsp; <strong> “Book Now Pay Later” </strong> ");
                    result.Append("</td>");
                    result.Append("</tr>");

                    result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Please let us recommend;");
                    result.Append("</td>");
                    result.Append("</tr>");
                    break;
                    // offine charge
                case 12:
                    result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Greeting from www.hotels2thailand.com ");
                    result.Append("</td>");
                    result.Append("</tr>");

                    result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Thank you for choosing  <a href=\"http://www.hotels2thailand.com\"> www.hotels2thailand.com </a>&nbsp; <strong> “Book Now Pay Later” </strong> ");
                    result.Append("</td>");
                    result.Append("</tr>");

                    result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Please kindly be informed that the remaining total cost of your booking is not be able to charge successfully. This is due to your credit card has some limitations to <strong>DECLINE.</strong> Please recheck the following criteria.  ");
                    result.Append("</td>");
                    result.Append("</tr>");

                    result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("<p>Possible Reasons for Declined Cards;</p>");
                    result.Append("<ul>");
                    result.Append("<li>There were insufficient funds on your credit card.</li>");
                    result.Append("<li>The credit card number or the expiry date was incorrect.</li>");
                    result.Append("<li>Your name and address are not matched</li>");
                    result.Append("<li>Make sure it's credit card not debit card.</li>");
                    result.Append("<li>Some credit card companies will reject international charges. Please contact to your bank.</li>");
                    result.Append("<li>Your bank or credit card company was having technical issues. Please contact to your bank.</li>");
                    result.Append("");
                    result.Append("</ul>");
                    result.Append("</td>");
                    result.Append("</tr>");

                    
                    break;
                //Receive Mail Bank Transfer
                case 13:

                    switch (this.BookingLangId)
                    {
                        case 1:
                            result.Append("<tr>");
                            result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                            result.Append("Thank you very much for using our service,<a href=\"http://www.hotels2thailand.com\"> www.hotels2thailand.com </a>");
                            result.Append("</td>");
                            result.Append("</tr>");


                            result.Append("<tr>");
                            result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                            result.Append("We received your enquiry for making the reservation with Hotels2thailand.com with many thanks.Your </a>");
                            result.Append("<span style=\"color:#000\">ORDER ID: " + this.BookingId + " </span>");
                            result.Append("</td>");
                            result.Append("</tr>");


                            result.Append("<tr>");
                            result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                            result.Append("Thank you for your payment to http://www.hotels2thailand.com.  Your payment for <span style=\"color:#000\">ORDER ID : " + this.BookingId + " </span> detail see below</a>");
                            result.Append("</td>");
                            result.Append("</tr>");
                            break;
                        case 2:
                            result.Append("<tr>");
                            result.Append("<td  style=\"margin:0px; padding:0px;\">");
                            result.Append("เว็บไซด์โฮเทลทูไทยแลนด์ (<a href=\"http://www.hotels2thailand.com\"> Hotels2thailand.com</a>)");
                            result.Append(" ขอขอบพระคุณที่ใช้บริการจองกับเรา");
                            result.Append("</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");


                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            switch (this.cClassBookingProduct.ProductCategory)
                            {
                                case 29:
                                    result.Append("เราได้รับข้อมูลการจองของท่านเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯ ขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการรับข้อมูลการจองจากท่านเท่านั้น  <strong> ยังมิใช่เอกสารยืนยันการจองห้องพัก (Hotel Voucher)</strong>");

                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะห้องว่างที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 24</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                    result.Append("</td>");
                                    result.Append("</tr>");

                                    break;
                                case 32:
                                    result.Append("เราได้รับข้อมูลการจองของท่านเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯ ขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการรับข้อมูลการจองจากท่านเท่านั้น <strong> ยังมิใช่เอกสารยืนยันการจองสนามกอล์ฟ (Hotel Voucher)</strong> ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะการจองสนามกอล์ฟที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 24</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    break;
                                case 40:
                                    result.Append("เราได้รับข้อมูลการจองของท่านเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯ ขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการรับข้อมูลการจองจากท่านเท่านั้น  <strong> ยังมิใช่เอกสารยืนยันการจองโปรแกรมสปา (Voucher)</strong>");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะโปรแกรมสปาที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 24</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                     result.Append("</td>");
                                    result.Append("</tr>");
                                    break;
                                case 38:
                                case 36:
                                case 34:
                                    result.Append("เราได้รับข้อมูลการจองของท่านเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯ ขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการรับข้อมูลการจองจากท่านเท่านั้น  <strong> ยังมิใช่เอกสารยืนยันการจองบริการ (Voucher)</strong> ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะบริการที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 24</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    break;
                            }

                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");

                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            result.Append("คุณสามารถใช้หมายเลขการจอง <strong> #" + this.BookingId + "</strong> นี้ในการตรวจสอบสถานะบุคกิ้งของคุณกับเราค่ะ");
                            result.Append("</td>");
                            result.Append("</tr>");
                            break;
                    }

                    break;
                //Receive Mail offline charge
                case 14:

                    switch (this.BookingLangId)
                    {
                        case 1:
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            result.Append("Thank you very much for making the reservation with <a href=\"http://www.hotels2thailand.com\"> www.hotels2thailand.com </a>");
                            result.Append("</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;\">We have received your booking at " + this.cClassBookingProduct.ProductTitle + ". Your Booking ID# " + this.BookingId + " and your payment will be not charged until your booking is confirmed. However, this email is not a confirmation. Our staff is checking room availability and will contact you in 24 hours.");
                            result.Append(" </td>");
                            result.Append("</tr>");

                            break;
                        case 2:
                            result.Append("<tr>");
                            result.Append("<td  style=\"margin:0px; padding:0px;\">");
                            result.Append("เว็บไซด์โฮเทลทูไทยแลนด์ (<a href=\"http://www.hotels2thailand.com\"> Hotels2thailand.com</a>)");
                            result.Append(" ขอขอบพระคุณที่ใช้บริการจองกับเรา");
                            result.Append("</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");

                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            switch (this.cClassBookingProduct.ProductCategory)
                            {
                                case 29:
                                    result.Append("เราได้รับข้อมูลการจองของท่านพร้อมทั้งการชำระเงินผ่านระบบเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการชำระเงินของท่านกับเราเท่านั้น<strong> ยังมิใช่เอกสารยืนยันการจองห้องพัก (Hotel Voucher)</strong>");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะห้องว่างที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 24</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    break;
                                case 32:
                                    result.Append("เราได้รับข้อมูลการจองของท่านพร้อมทั้งการชำระเงินผ่านระบบเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการชำระเงินของท่านกับเราเท่านั้น <strong> ยังมิใช่เอกสารยืนยันการจองสนามกอล์ฟ (Voucher)</strong> ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะการจองสนามกอล์ฟที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 24</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    break;
                                case 40:
                                    result.Append("เราได้รับข้อมูลการจองของท่านพร้อมทั้งการชำระเงินผ่านระบบเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการชำระเงินของท่านกับเราเท่านั้น <strong> ยังมิใช่เอกสารยืนยันการจองโปรแกรมสปา (Voucher)</strong> ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะโปรแกรมสปาที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 24</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    break;
                                case 38:
                                case 36:
                                case 34:
                                    result.Append("เราได้รับข้อมูลการจองของท่านพร้อมทั้งการชำระเงินผ่านระบบเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการชำระเงินของท่านกับเราเท่านั้น <strong> ยังมิใช่เอกสารยืนยันการจองบริการ (Voucher)</strong> ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะบริการที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 24</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    break;
                            }

                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            result.Append("คุณสามารถใช้หมายเลขการจอง <strong> #" + this.BookingId + "</strong> นี้ในการตรวจสอบสถานะบุคกิ้งของคุณกับเราค่ะ");
                            result.Append("</td>");
                            result.Append("</tr>");
                            break;
                    }

                    break;
                // Mail Send Voucher & Map & Recept For Deal 
                case 15:
                    switch (this.BookingLangId)
                    {
                        case 1:
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            result.Append("We would like to thank you for purchasing deal with www.hotels2thailand.com. We are pleased to inform you that your purchase at " + this.cClassBookingProduct.ProductTitle + " #" + this.BookingId + " is CONFIRMED. </a>");

                            result.Append("</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            result.Append("Your purchase is paid in full. Please see your voucher detail below.");
                            result.Append("</td>");
                            result.Append("</tr>");
                            break;
                        case 2:

                            string txtLast = string.Empty;

                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            result.Append("เวปไซต์โฮเทลทูไทยแลนด์ <a href=\"http://www.hotels2thailand.com\">(Hotels2thailand.com)</a> ขอขอบพระคุณที่ใช้บริการของเรา โปรดพิมพ์เอกสารยืนยันการสั่งซื้อนี้และแสดงต่อเจ้าหน้าที่ในวันที่ใช้บริการค่ะ");

                            result.Append("</td>");
                            result.Append("</tr>");

                            break;
                    }

                    break;
                    //REsubmit Deal
                case 16:
                    switch (this.BookingLangId)
                    {
                        case 1:
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            result.Append("Thank you very much for using our service. This is to inform that your purchase request <span style=\"color:#000\"> (#" + this.BookingId + ")</span> has not been paid successfully. In order to process your purchase completely, please click the “Make Payment” button below to complete your payment thru our high security online system.");
                            result.Append("</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            result.Append("The availability bases on First Come First Serve. We are unable to secure your purchase unless the payment is paid in full successfully.");
                            result.Append("</td>");
                            result.Append("</tr>");


                            break;
                        case 2:
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            result.Append("เว็บไซด์โฮเทลทูไทยแลนด์ (<a href=\"http://www.hotels2thailand.com\"> Hotels2thailand.com </a>)");
                            result.Append("ขอขอบพระคุณที่ท่านสนใจดีลของเรา</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;\">ทางเวปไซต์โฮเทลทูไทยแลนด์ ยังไม่ได้รับการชำระเงินจากดีลที่ท่านสั่งซื้อ ดังนั้นทางเวบไซด์โฮเทลทูไทยแลนด์ขออนุญาติแนะนำให้ท่านทำการชำระเงินโดยเร็วที่สุดเพื่อทางเราจะได้ดำเนินการส่งคูปองให้กับท่านทันทีค่ะ </td>");
                            
                            result.Append("</tr>");
                            break;
                    }

                    break;
                    // Bank Transfer for deal
                case 17:
                    switch (this.BookingLangId)
                    {
                        case 1:
                            result.Append("<tr>");
                            result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                            result.Append("Thank you very much for using our service,<a href=\"http://www.hotels2thailand.com\"> www.hotels2thailand.com </a>");
                            result.Append("</td>");
                            result.Append("</tr>");


                            result.Append("<tr>");
                            result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                            result.Append("This is to inform that your purchase request </a>");
                            result.Append("<span style=\"color:#000\"># " + this.BookingId + " </span> has not been paid successfully. This letter is not a voucher confirmation. You will receive another email requesting you please make the payment thru our system to protect your purchase order. Then the voucher confirmation of your purchase will be sent to your email immediately once your payment is done.");
                            result.Append("</td>");
                            result.Append("</tr>");


                            //result.Append("<tr>");
                            //result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                            //result.Append("Thank you for your payment to http://www.hotels2thailand.com.  Your payment for <span style=\"color:#000\">ORDER ID : " + this.BookingId + " </span> detail see below</a>");
                            //result.Append("</td>");
                            //result.Append("</tr>");
                            break;
                        case 2:
                            result.Append("<tr>");
                            result.Append("<td  style=\"margin:0px; padding:0px;\">");
                            result.Append("เว็บไซด์โฮเทลทูไทยแลนด์ (<a href=\"http://www.hotels2thailand.com\"> Hotels2thailand.com</a>)");
                            result.Append(" ขอขอบพระคุณที่ท่านเลือกสั่งซื้อดีลกับเรา");
                            result.Append("</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");


                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            result.Append("เราได้รับข้อมูลการสั่งซื้อของท่านเป็นที่เรียบร้อยแล้วและทางเวปไซต์โฮเทลทูฯขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการรับข้อมูลการสั่งซื้อจากท่านเท่านั้น <strong>  ยังมิใช่คูปองยืนยันการสั่งซื้อดีลจากเรา </strong>");

                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;\">");
                                    result.Append("กรุณาชำระเงินภายในเวลา 48 ชม เพื่อป้องกันการยกเลิกคำสั่งซื้อของท่านโดยอัตโนมัติด้วยค่ะ และท่านจะได้รับอีเมล์คูปองยืนยันการสั่งซื้อจากเราอีกครั้งเมื่อทำชำระเงินเป็นที่เรียบร้อยแล้ว");
                                    result.Append("</td>");
                                    result.Append("</tr>");

                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");

                            result.Append("<td style=\"margin:0px; padding:0px;\">");
                            result.Append("คุณสามารถใช้หมายเลขการสั่งซื้อ <strong> #" + this.BookingId + "</strong> นี้ในการตรวจสอบสถานะบุคกิ้งของคุณกับเราค่ะ");
                            result.Append("</td>");
                            result.Append("</tr>");
                            break;
                    }

                    break;
            }

            //result.Append(" </table>");
            //result.Append("");
            //result.Append("");
            return result.ToString();
        }
        
        protected string GrandTotal()
        {
            BookingTotalAndBalance cBookingtotalPrice = new BookingTotalAndBalance();
            BookingdetailDisplay cBookingDetailDisplay = new BookingdetailDisplay();
            string result = "0";
            decimal Total = cBookingtotalPrice.CalcullatePriceTotalByBookingId(this.BookingId).SumPrice;
            result = Total.Hotels2Currency();
            //result = cBookingDetailDisplay.GetPriceTotalByBookingId(this.BookingId).Hotels2Currency();

            return result.ToString();
        }


        protected string GrandPaidTotal()
        {
            BookingTotalAndBalance cBookingtotalPrice = new BookingTotalAndBalance();
            BookingdetailDisplay cBookingDetailDisplay = new BookingdetailDisplay();
            string result = "0";
            decimal Total = cBookingtotalPrice.GetPriceTotalPaidByBookingId(this.BookingId);
            result = Total.Hotels2Currency();
            //result = cBookingDetailDisplay.GetPriceTotalByBookingId(this.BookingId).Hotels2Currency();

            return result.ToString();
        }


        protected string GrandRequestTotal(int intPaymentId)
        {
            //BookingTotalAndBalance cBookingtotalPrice = new BookingTotalAndBalance();
            BookingPaymentDisplay cBookingPayment = new BookingPaymentDisplay();
            cBookingPayment = cBookingPayment.GEtPaymentByPaymentId(intPaymentId);
            //BookingdetailDisplay cBookingDetailDisplay = new BookingdetailDisplay();
            string result = "0";
            decimal Total = cBookingPayment.Amount;
            //decimal Total = cBookingtotalPrice.getbalanceByBookingId(this.BookingId) * -1;
            result = Total.Hotels2Currency();
            //result = cBookingDetailDisplay.GetPriceTotalByBookingId(this.BookingId).Hotels2Currency();

            return result.ToString();
        }

        public string GetEmailBooking()
        {
            BookingdetailDisplay cBookingDetailDisplay = new BookingdetailDisplay();
            string result = "";
            result = cBookingDetailDisplay.GetBookingDetailListByBookingId(this.BookingId).Email ;

            return result.ToString();
        }
        

        protected string CusName()
        {
            BookingdetailDisplay cBookingDetailDisplay = new BookingdetailDisplay();
            cBookingDetailDisplay = cBookingDetailDisplay.GetBookingDetailListByBookingId(this.BookingId);
            string result = "";
            if (cBookingDetailDisplay.PrefixId == 1 || this.BookingLangId == 2)
            {
                result = cBookingDetailDisplay.FullName;

                if(this.BookingLangId  == 2)
                    result = "คุณ " + cBookingDetailDisplay.FullName;
            }
            else
                result = cBookingDetailDisplay.PrefixTitle + " " + cBookingDetailDisplay.FullName;

            return result.ToString();
        }
    }
}