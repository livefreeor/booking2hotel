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
using Hotels2thailand.Production;
using System.Configuration;
using System.Web.Configuration;


/// <summary>
/// Summary description for Voucher
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
     public enum MailCat : byte
     {
         BookingRecevied = 1,
         Resubmit = 2,
         ComfirmBooking = 3,
         Review = 4,
         Mail_hotel_notice = 5,
         Mail_hotel_offline = 6,
         BookingRecevied_allot = 7
        
     }
    public partial class BookingMailEngine : BookingPrintAndVoucher_Helper
    {
        public int BookingId { get; set; }
        public byte BookingLangId { get; set; }

        public DateTime DateSubmitBooking { get; set;  }
        
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

        private Hotels2thailand.Production.ProductBookingEngine _c_class_product_BookingEngine = null;
        public Hotels2thailand.Production.ProductBookingEngine cProductBookingEngine
        {
            get
            {
                if (_c_class_product_BookingEngine == null)
                {
                    Hotels2thailand.Production.ProductBookingEngine cProduct = new ProductBookingEngine();

                    _c_class_product_BookingEngine =   cProduct.GetProductbookingEngine(this.cClassBookingProduct.ProductID);
                }           
                return _c_class_product_BookingEngine;
            }

            set { _c_class_product_BookingEngine = value; }
        }

        public string MailNameDisplayDefault
        {
            get { return "Reservation:" + this.GetDomainName; }
        }

        private string _get_domain_name = string.Empty;
        public string GetDomainName
        {
            get {

                    return this.cProductBookingEngine.WebsiteName.Replace("http://www.","").Split('/')[0];
            
                }
        }
        public int BookingHotelId
        {
            get
            {
                BookingProductDisplay cBookingPLsit = new BookingProductDisplay();
                return cBookingPLsit.GetBookingHotelId(this.BookingId);
            }
        }



        private string _bcc = "sent@booking2hotels.com;sent2@booking2hotels.com;kpongphat@hotels2thailand.com;peerapong@hotels2thailand.com;visa@hotels2thailand.com;";
        public string Bcc
        {
            get { return _bcc; }
            set { _bcc = value; }
        }

        public BookingMailEngine() { }

        public BookingMailEngine(int intBookingId)
        {
            this.BookingId = intBookingId;
            BookingdetailDisplay cBooking = new BookingdetailDisplay();
           
            this.BookingLangId = cBooking.GetBookingLang(intBookingId);
            this.DateSubmitBooking = cBooking.GetBookingDetailListByBookingId(intBookingId).DateBookingREceive;
            

        }



        private string _mainsite = "http://www.booking2hotels.com/";
        protected string getMailTheme()
        {
            string Theme = string.Empty;

           
            string Theme_file = "Template_maillBooking_en.html";
            if (this.BookingLangId == 2)
                Theme_file = "Template_maillBooking_th.html";

            StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template/" + Theme_file + ""));
            string read = objReader.ReadToEnd();

            Theme = Utility.GetKeywordReplace(read, "<!--##@MailContentStart##-->", "<!--##@MailContentEnd##-->");
            return Theme;
        }

        protected string getMailThemeNew()
        {
            string Theme = string.Empty;


            string Theme_file = "mail_template_en.html";
            if (this.BookingLangId == 2)
                Theme_file = "mail_template_th.html";

            StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath("~/admin/booking/BookingPrintAndMail_Template/" + Theme_file + ""));
            string read = objReader.ReadToEnd();
            objReader.Close();
            Theme = Utility.GetKeywordReplace(read, "<!--##@MailContentStart##-->", "<!--##@MailContentEnd##-->");
            return Theme;
        }

        public bool SendMemberRegis(string mailto, int intProductId, int intCusId)
        {
            bool Success = false;
            ProductContent cProductContent = new ProductContent();
            cProductContent = cProductContent.GetProductContentById(intProductId, 1);

            ProductBookingEngine cProductBookingEngineNew = new ProductBookingEngine();
            cProductBookingEngineNew = cProductBookingEngineNew.GetProductbookingEngine(intProductId);

            //this.cProductBookingEngine.WebsiteName.Replace("http://www.","").Split('/')[0];
            //string Maildisplay = "Reservation:Hotels2thailand.com";
            string subject = "Thank you for your registration at: " + cProductContent.Title;
            string MailDisplay = "Member: " + cProductBookingEngineNew.WebsiteName.Replace("http://www.", "").Split('/')[0];


            try
            {
                string MailBody = Member_mail_registration_completed(intProductId, intCusId);

                Success = Hotels2MAilSender.SendmailBooking(cProductBookingEngineNew.Email, MailDisplay, mailto, subject, "", MailBody);

                //Success = Hotels2MAilSender.SendmailBooking(Maildisplay, this.Bcc, GetSubject(MailCat.BookingRecevied), "peerapong@hotels2thailand.com", removeEmailTrack(MailBody));
                Success = Hotels2MAilSender.SendmailBooking(cProductBookingEngineNew.Email, MailDisplay, this.Bcc, subject, "", MailBody);
            }
            catch
            {
                //HttpContext.Current.Response.Write(ex.Message + "<br/>" + ex.StackTrace);
                //HttpContext.Current.Response.End();
                Success = false;
            }
            return Success;
        }

        public bool SendForgotPassword(string mailto, int intProductId, int intCusId)
        {
            bool Success = false;
            ProductContent cProductContent = new ProductContent();
            cProductContent = cProductContent.GetProductContentById(intProductId, 1);

            ProductBookingEngine cProductBookingEngineNew = new ProductBookingEngine();
            cProductBookingEngineNew = cProductBookingEngineNew.GetProductbookingEngine(intProductId);

            //this.cProductBookingEngine.WebsiteName.Replace("http://www.","").Split('/')[0];
            //string Maildisplay = "Reservation:Hotels2thailand.com";
            string subject = "Member FORGOT PASSWORD " + cProductContent.Title;
            string MailDisplay = "Member: " + cProductBookingEngineNew.WebsiteName.Replace("http://www.", "").Split('/')[0];


            try
            {
                string MailBody = Member_mail_forgot_password(intProductId, intCusId);

                Success = Hotels2MAilSender.SendmailBooking(cProductBookingEngineNew.Email, MailDisplay, mailto, subject, "", MailBody);

                //Success = Hotels2MAilSender.SendmailBooking(Maildisplay, this.Bcc, GetSubject(MailCat.BookingRecevied), "peerapong@hotels2thailand.com", removeEmailTrack(MailBody));
                Success = Hotels2MAilSender.SendmailBooking(cProductBookingEngineNew.Email, MailDisplay, this.Bcc, subject, "", MailBody);
            }
            catch
            {
                Success = false;
            }
            return Success;
        }

        public bool SendMemberesetPass(string mailto, int intProductId, int intCusId)
        {
            bool Success = false;
            ProductContent cProductContent = new ProductContent();
            cProductContent = cProductContent.GetProductContentById(intProductId, 1);

            ProductBookingEngine cProductBookingEngineNew = new ProductBookingEngine();
            cProductBookingEngineNew = cProductBookingEngineNew.GetProductbookingEngine(intProductId);

            //this.cProductBookingEngine.WebsiteName.Replace("http://www.","").Split('/')[0];
            //string Maildisplay = "Reservation:Hotels2thailand.com";
            string subject = "Reset Password: " + cProductContent.Title;
            string MailDisplay = "Member: " + cProductBookingEngineNew.WebsiteName.Replace("http://www.", "").Split('/')[0];
            

            try
            {
                string MailBody = Member_mail_resetPass(intProductId, intCusId);

                Success = Hotels2MAilSender.SendmailBooking(cProductBookingEngineNew.Email, MailDisplay, mailto, subject, "", MailBody);

                //Success = Hotels2MAilSender.SendmailBooking(Maildisplay, this.Bcc, GetSubject(MailCat.BookingRecevied), "peerapong@hotels2thailand.com", removeEmailTrack(MailBody));
                Success = Hotels2MAilSender.SendmailBooking(cProductBookingEngineNew.Email, MailDisplay, this.Bcc, subject, "", MailBody);
            }
            catch
            {
                //HttpContext.Current.Response.Write(ex.Message + "<br/>" + ex.StackTrace);
                //HttpContext.Current.Response.End();
                Success = false;
            }
            return Success;
        }



        public bool SendMemberActivation(string mailto, int intProductId, int intCusId,string strNewGenEmail)
        {
            bool Success = false;
            ProductContent cProductContent = new ProductContent();
            cProductContent = cProductContent.GetProductContentById(intProductId,1);
            //string Maildisplay = "Reservation:Hotels2thailand.com";


            ProductBookingEngine cProductBookingEngineNew = new ProductBookingEngine();
            cProductBookingEngineNew = cProductBookingEngineNew.GetProductbookingEngine(intProductId);
            //Customer cCustomer = new Customer();
            //cCustomer = cCustomer.GetCustomerbyId(intCusId);
            

            string subject = "Welcome to " + cProductContent.Title + "member";
            string MailDisplay = "Member Activation: " + cProductBookingEngineNew.WebsiteName.Replace("http://www.", "").Split('/')[0];

            try
            {
                string MailBody = Member_mail_manaul_active(intProductId, intCusId, mailto, strNewGenEmail);

                Success = Hotels2MAilSender.SendmailBooking(cProductBookingEngineNew.Email, MailDisplay, mailto, subject, "", MailBody);

                //Success = Hotels2MAilSender.SendmailBooking(Maildisplay, this.Bcc, GetSubject(MailCat.BookingRecevied), "peerapong@hotels2thailand.com", removeEmailTrack(MailBody));
                Success = Hotels2MAilSender.SendmailBooking(cProductBookingEngineNew.Email, MailDisplay, this.Bcc, subject, "", MailBody);
            }
            catch
            {
                Success = false;
            }
            return Success;
        }

        public bool SendMailBookingRecevied()
        {
            bool Success = false;
            //string Maildisplay = "Reservation:Hotels2thailand.com";
            //string subject = "Booking received from hotels2thailand.com (ORDER ID:"+ this.BookingId +")";

            try
            {
                string MailBody = getMailBookingRecieved();

                Success = Hotels2MAilSender.SendmailBooking(this.cProductBookingEngine.Email, this.MailNameDisplayDefault, GetEmailBooking(), GetSubject(MailCat.BookingRecevied),"", MailBody);


                Success = Hotels2MAilSender.SendmailBooking(this.cProductBookingEngine.Email, this.MailNameDisplayDefault, this.Bcc, GetSubject(MailCat.BookingRecevied), "", MailBody);

            }
            catch
            {
                Success = false;
            }
            return Success;
        }
        public bool SendMailBookingRecevied_Booknow_offline()
        {
            bool Success = false;
            //string Maildisplay = "Reservation:Hotels2thailand.com";
            //string subject = "Booking received from hotels2thailand.com (ORDER ID:"+ this.BookingId +")";

            try
            {
                string MailBody = getMailBookingRecieved_Allot_Booknow_offline();

                Success = Hotels2MAilSender.SendmailBooking(this.cProductBookingEngine.Email, this.MailNameDisplayDefault, GetEmailBooking(), GetSubject(MailCat.BookingRecevied), "", MailBody);


                Success = Hotels2MAilSender.SendmailBooking(this.cProductBookingEngine.Email, this.MailNameDisplayDefault, this.Bcc, GetSubject(MailCat.BookingRecevied), "", MailBody);

            }
            catch
            {
                Success = false;
            }
            return Success;
        }

        public bool SendMailBookingRecevied_allot()
        {
            bool Success = false;
            //string Maildisplay = "Reservation:Hotels2thailand.com";
            //string subject = "Booking received from hotels2thailand.com (ORDER ID:"+ this.BookingId +")";

            try
            {
                string MailBody = getMailBookingRecieved_Allot();

                Success = Hotels2MAilSender.SendmailBooking(this.cProductBookingEngine.Email, this.MailNameDisplayDefault, GetEmailBooking(), GetSubject(MailCat.BookingRecevied_allot), "", MailBody);

                //Success = Hotels2MAilSender.SendmailBooking(Maildisplay, this.Bcc, GetSubject(MailCat.BookingRecevied), "peerapong@hotels2thailand.com", removeEmailTrack(MailBody));
                Success = Hotels2MAilSender.SendmailBooking(this.cProductBookingEngine.Email, this.MailNameDisplayDefault, this.Bcc, GetSubject(MailCat.BookingRecevied_allot), "", MailBody);
            }
            catch
            {
                Success = false;
            }
            return Success;
        }



        public bool SendMailBookingRecevied_Notification(string mailto)
        {
            bool Success = false;
            //string Maildisplay = "Reservation:Hotels2thailand.com";
            //string subject = "Booking received from hotels2thailand.com (ORDER ID:"+ this.BookingId +")";

            try
            {
                if (!this.cProductBookingEngine.Is_B2b)
                {
                    string MailBody = getMailBookingRecieved_hotel_notice();

                    Success = Hotels2MAilSender.SendmailBooking(this.cProductBookingEngine.Email, this.MailNameDisplayDefault, mailto, GetSubject(MailCat.Mail_hotel_notice), "", MailBody);

                    //Success = Hotels2MAilSender.SendmailBooking(Maildisplay, this.Bcc, GetSubject(MailCat.BookingRecevied), "peerapong@hotels2thailand.com", removeEmailTrack(MailBody));
                    Success = Hotels2MAilSender.SendmailBooking(this.cProductBookingEngine.Email, this.MailNameDisplayDefault, this.Bcc, GetSubject(MailCat.Mail_hotel_notice), "", MailBody);
                }
                else {
                     Success = true; 
                }
            }
            catch
            {
                Success = false;
            }
            return Success;
        }


        public bool SendMailBookingRecevied_Notification_offline(string mailto,string cardencode)
        {
            bool Success = false;
            //string Maildisplay = "Reservation:Hotels2thailand.com";
            //string subject = "Booking received from hotels2thailand.com (ORDER ID:"+ this.BookingId +")";

            try
            {
                if (!this.cProductBookingEngine.Is_B2b)
                {
                    string MailBody = getMailBookingRecieved_notice_offline_charge(cardencode);

                    Success = Hotels2MAilSender.SendmailBooking(this.cProductBookingEngine.Email, this.MailNameDisplayDefault, mailto, GetSubject(MailCat.Mail_hotel_offline), "", MailBody);

                    //Success = Hotels2MAilSender.SendmailBooking(Maildisplay, this.Bcc, GetSubject(MailCat.BookingRecevied), "peerapong@hotels2thailand.com", removeEmailTrack(MailBody));
                    Success = Hotels2MAilSender.SendmailBooking(this.cProductBookingEngine.Email, this.MailNameDisplayDefault, "sent@booking2hotels.com;sent2@booking2hotels.com;kpongphat@hotels2thailand.com;visa@hotels2thailand.com;peerapong@hotels2thailand.com", GetSubject(MailCat.Mail_hotel_offline), "", MailBody);
                }
                else
                {
                    Success = true; 
                }
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

            try
            {
                string MailBody = getMailBookingRecieved_BankTransfer();

                Success = Hotels2MAilSender.SendmailBooking(MailNameDisplayDefault, GetEmailBooking(), GetSubject(MailCat.BookingRecevied), "", MailBody);


                Success = Hotels2MAilSender.SendmailBooking(Maildisplay, this.Bcc, GetSubject(MailCat.BookingRecevied), "peerapong@hotels2thailand.com", removeEmailTrack(MailBody));

            }
            catch
            {
                Success = false;
            }
            return Success;
        }

        
        public string Member_mail_manaul_active(int intProductId, int CustomerId,string strEmail,string NewPass)
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


            string ContentFoot_start = Utility.GetKeywordReplace(MainBody, "<!--##@ContentFoot_start##-->", "<!--##@ContentFoot_end##-->");
            MainBody = MainBody.Replace(ContentFoot_start, FootReview);


            //Detail Top
            string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            MainBody = MainBody.Replace(KeywordDetailTop, TextActivateDetail(intProductId, CustomerId, strEmail, NewPass));

            //CustomerName 
            MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName_member(CustomerId));


            //getDetail Review 
            //MainBody = MainBody.Replace("<!--##@ContentReviewDetail##-->", TextActivateDetail());


            Production.ProductPic cProductPic = new Production.ProductPic();

            string Logo = "<img  src=\"" + cProductPic.getProductlogo(intProductId) + "\" style=\"width:150px; height:60px; border:solid 1px #eee5be\" />";
            //Logo Is Open
            MainBody = MainBody.Replace("<!--##@HotelLogo##-->", Logo);


            Product cProduct = new Product();
            cProduct = cProduct.GetProductById(intProductId);

            ProductContent cProductContent = new ProductContent();
            cProductContent = cProductContent.GetProductContentById(intProductId, 1);
            //BookingProductDisplay cBookingPLsit = new BookingProductDisplay();
            //cBookingPLsit = cBookingPLsit.getBookingProductDisplayByBookingProductId(intProductId);

            //BookingProductName
            MainBody = MainBody.Replace("<!--##@HotelName##-->", cProductContent.Title);
            //BookingProductName
            MainBody = MainBody.Replace("<!--##@HotelName_foot##-->", cProductContent.Title);
            //Hotel Phone
            MainBody = MainBody.Replace("<!--##@HotelAddress##-->", cProductContent.Address);
            //HOtel Address
            MainBody = MainBody.Replace("<!--##@HotelPhone##-->", cProduct.ProductPhone);




            return MainBody;




        }

        //Member_mail_forgot_password
        public string Member_mail_forgot_password(int intProductId, int CustomerId)
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

            

            string ContentFoot_start = Utility.GetKeywordReplace(MainBody, "<!--##@ContentFoot_start##-->", "<!--##@ContentFoot_end##-->");
            MainBody = MainBody.Replace(ContentFoot_start, FootReview);


            //Detail Top
            string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            MainBody = MainBody.Replace(KeywordDetailTop, TextforgotPassword(intProductId, CustomerId));

            //CustomerName 
            MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName_member(CustomerId));


            //getDetail Review 
            //MainBody = MainBody.Replace("<!--##@ContentReviewDetail##-->", TextResetPassDetail());


            Production.ProductPic cProductPic = new Production.ProductPic();

            string Logo = "<img  src=\"" + cProductPic.getProductlogo(intProductId) + "\" style=\"width:150px; height:60px; border:solid 1px #eee5be\" />";
            //Logo Is Open
            MainBody = MainBody.Replace("<!--##@HotelLogo##-->", Logo);


            Product cProduct = new Product();
            cProduct = cProduct.GetProductById(intProductId);

            ProductContent cProductContent = new ProductContent();
            cProductContent = cProductContent.GetProductContentById(intProductId, 1);
            //BookingProductDisplay cBookingPLsit = new BookingProductDisplay();
            //cBookingPLsit = cBookingPLsit.getBookingProductDisplayByBookingProductId(intProductId);

            //BookingProductName
            MainBody = MainBody.Replace("<!--##@HotelName##-->", cProductContent.Title);
            //BookingProductName
            MainBody = MainBody.Replace("<!--##@HotelName_foot##-->", cProductContent.Title);
            //Hotel Phone
            MainBody = MainBody.Replace("<!--##@HotelAddress##-->", cProductContent.Address);
            //HOtel Address
            MainBody = MainBody.Replace("<!--##@HotelPhone##-->", cProduct.ProductPhone);




            return MainBody;




        }

        public string Member_mail_resetPass(int intProductId, int CustomerId)
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


            string ContentFoot_start = Utility.GetKeywordReplace(MainBody, "<!--##@ContentFoot_start##-->", "<!--##@ContentFoot_end##-->");
            MainBody = MainBody.Replace(ContentFoot_start, FootReview);


            //Detail Top
            string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            MainBody = MainBody.Replace(KeywordDetailTop, TextResetPassDetail(intProductId, CustomerId));

            //CustomerName 
            MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName_member(CustomerId));


            //getDetail Review 
            //MainBody = MainBody.Replace("<!--##@ContentReviewDetail##-->", TextResetPassDetail());


            Production.ProductPic cProductPic = new Production.ProductPic();

            string Logo = "<img  src=\"" + cProductPic.getProductlogo(intProductId) + "\" style=\"width:150px; height:60px; border:solid 1px #eee5be\" />";
            //Logo Is Open
            MainBody = MainBody.Replace("<!--##@HotelLogo##-->", Logo);


            Product cProduct = new Product();
            cProduct = cProduct.GetProductById(intProductId);

            ProductContent cProductContent = new ProductContent();
            cProductContent = cProductContent.GetProductContentById(intProductId,1);
            //BookingProductDisplay cBookingPLsit = new BookingProductDisplay();
            //cBookingPLsit = cBookingPLsit.getBookingProductDisplayByBookingProductId(intProductId);

            //BookingProductName
            MainBody = MainBody.Replace("<!--##@HotelName##-->", cProductContent.Title);
            //BookingProductName
            MainBody = MainBody.Replace("<!--##@HotelName_foot##-->", cProductContent.Title);
            //Hotel Phone
            MainBody = MainBody.Replace("<!--##@HotelAddress##-->", cProductContent.Address);
            //HOtel Address
            MainBody = MainBody.Replace("<!--##@HotelPhone##-->", cProduct.ProductPhone);




            return MainBody;




        }

        public string Member_mail_registration_completed(int intProductId, int CustomerId)
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

            

            string ContentFoot_start = Utility.GetKeywordReplace(MainBody, "<!--##@ContentFoot_start##-->", "<!--##@ContentFoot_end##-->");
            MainBody = MainBody.Replace(ContentFoot_start, FootReview);


            //Detail Top
            string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            MainBody = MainBody.Replace(KeywordDetailTop, TextRegistrationCompleted(intProductId, CustomerId));

            //CustomerName 
            MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName_member(CustomerId));


            //getDetail Review 
            //MainBody = MainBody.Replace("<!--##@ContentReviewDetail##-->", TextResetPassDetail());


            Production.ProductPic cProductPic = new Production.ProductPic();

            string Logo = "<img  src=\"" + cProductPic.getProductlogo(intProductId) + "\" style=\"width:150px; height:60px; border:solid 1px #eee5be\" />";
            //Logo Is Open
            MainBody = MainBody.Replace("<!--##@HotelLogo##-->", Logo);


            Product cProduct = new Product();
            cProduct = cProduct.GetProductById(intProductId);

            ProductContent cProductContent = new ProductContent();
            cProductContent = cProductContent.GetProductContentById(intProductId, 1);
            //BookingProductDisplay cBookingPLsit = new BookingProductDisplay();
            //cBookingPLsit = cBookingPLsit.getBookingProductDisplayByBookingProductId(intProductId);

            //BookingProductName
            MainBody = MainBody.Replace("<!--##@HotelName##-->", cProductContent.Title);
            //BookingProductName
            MainBody = MainBody.Replace("<!--##@HotelName_foot##-->", cProductContent.Title);
            //Hotel Phone
            MainBody = MainBody.Replace("<!--##@HotelAddress##-->", cProductContent.Address);
            //HOtel Address
            MainBody = MainBody.Replace("<!--##@HotelPhone##-->", cProduct.ProductPhone);




            return MainBody;




        }


        public string getMailReview()
        {
            string MainBody = string.Empty;

            if (!this.cProductBookingEngine.Is_B2b)
            {
                 MainBody = getMailThemeNew();

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


                Production.ProductPic cProductPic = new Production.ProductPic();

                string Logo = "<img  src=\"" + cProductPic.getProductlogo(this.cClassBookingProduct.ProductID) + "\" style=\"width:150px; height:60px; border:solid 1px #eee5be\" />";
                //Logo Is Open
                MainBody = MainBody.Replace("<!--##@HotelLogo##-->", Logo);


                BookingProductDisplay cBookingPLsit = new BookingProductDisplay();
                cBookingPLsit = cBookingPLsit.getBookingProductDisplayByBookingProductId(this.cClassBookingProduct.BookingProductId);

                //BookingProductName
                MainBody = MainBody.Replace("<!--##@HotelName##-->", cBookingPLsit.ProductTitle);
                //BookingProductName
                MainBody = MainBody.Replace("<!--##@HotelName_foot##-->", cBookingPLsit.ProductTitle);
                //Hotel Phone
                MainBody = MainBody.Replace("<!--##@HotelAddress##-->", cBookingPLsit.ProductAddress);
                //HOtel Address
                MainBody = MainBody.Replace("<!--##@HotelPhone##-->", cBookingPLsit.ProductPhone);


            }
            else
            {
                Hotels2thailand.BookingB2b.BookingMailEngineB2b cMailBookingB2B = new BookingB2b.BookingMailEngineB2b(this.BookingId);
                MainBody = cMailBookingB2B.getMailReview();
            }

            return MainBody;
            
            
         

        }


        public string getMailResubmit(int intPaymentId)
        {
            string MainBody = string.Empty;
            if (!this.cProductBookingEngine.Is_B2b)
            {
                BookingProductDisplay cBookingPLsit = new BookingProductDisplay();
                //byte bytBookingLang = cBookingPLsit.BookingLang;
                 MainBody = getMailThemeNew();



                //Booking IitemList
                string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
                MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId().Replace("<!--##@BookingNameItem##-->", CusName()));

                //Detail Top
                string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
                MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(1));

                //Wording Balance For Cutomer
                //MainBody = MainBody.Replace("<!--##@Balance_Note##-->", "Please pay the balance at hotel upon your check in.");
                //CustomerName 
                MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

                //GrandTotal
                //MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal());

                //GrandBalance
                MainBody = MainBody.Replace("<!--##@mailItemTotalBalanceContent##-->", GrandBalance());

                //GrandPaid
                MainBody = MainBody.Replace("<!--##@mailItemTotalPaidContent##-->", GrandPaidTotal());

                //GrandRequestotal
                MainBody = MainBody.Replace("<!--##@mailItemTotalRequestContent##-->", GrandRequestTotal(intPaymentId));

                // //Disable Grandtotal Main
                string KeywordGrandTotal = Utility.GetKeywordReplace(MainBody, "<!--##@Grandtotal_Start##-->", "<!--##@Grandtotal_End##-->");
                MainBody = MainBody.Replace(KeywordGrandTotal, " ");

                //Disable Grand Balance Main
                string KeywordGrandBalance = Utility.GetKeywordReplace(MainBody, "<!--##@Balance_Start##-->", "<!--##@Balance_End##-->");
                MainBody = MainBody.Replace(KeywordGrandBalance, " ");

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
                        break;
                }


                Production.ProductPic cProductPic = new Production.ProductPic();

                string Logo = "<img  src=\"" + cProductPic.getProductlogo(this.cClassBookingProduct.ProductID) + "\" style=\"width:150px; height:60px; border:solid 1px #eee5be\" />";
                //Logo Is Open
                MainBody = MainBody.Replace("<!--##@HotelLogo##-->", Logo);

                //EmailTracking Is Open
                //MainBody = MainBody.Replace("<!--##@ContentEmailtracking##-->", imgTrack(16));

                cBookingPLsit = cBookingPLsit.getBookingProductDisplayByBookingProductId(this.cClassBookingProduct.BookingProductId);

                //BookingProductName
                MainBody = MainBody.Replace("<!--##@HotelName##-->", cBookingPLsit.ProductTitle);
                //BookingProductName
                MainBody = MainBody.Replace("<!--##@HotelName_foot##-->", cBookingPLsit.ProductTitle);
                //Hotel Phone
                MainBody = MainBody.Replace("<!--##@HotelAddress##-->", cBookingPLsit.ProductAddress);
                //HOtel Address
                MainBody = MainBody.Replace("<!--##@HotelPhone##-->", cBookingPLsit.ProductPhone);



                //ContactUsPhone <!--##@HotelPhoneContact##-->
                MainBody = MainBody.Replace("<!--##@HotelPhoneContact##-->", cBookingPLsit.ProductPhone);

                BookingStaff cBookingStaff = new BookingStaff();
                // Contact Us Email <!--##@EmailContactUs##-->
                MainBody = MainBody.Replace("<!--##@EmailContactUs##-->", cBookingStaff.GetStringEmail_mailtoForm_showMail(cProductBookingEngine.EmailContactMail));
            }
            else
            {
                Hotels2thailand.BookingB2b.BookingMailEngineB2b cMailBookingB2B = new BookingB2b.BookingMailEngineB2b(this.BookingId);
                MainBody = cMailBookingB2B.getMailResubmit(intPaymentId);
            }

           return MainBody;

        }

        

        public string getMailSendVoucher(string pricetype = "")
        {
            string MainBody = string.Empty;
            if (!this.cProductBookingEngine.Is_B2b)
            {
                MainBody = getMailThemeNew();

                //Booking IitemList
                string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
                MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId(pricetype).Replace("<!--##@BookingNameItem##-->", CusName()));

                //Detail Top
                string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
                MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(3));

                string strGrandBalance = GrandBalance(pricetype);

                if (strGrandBalance != "0.00")
                {
                    //Wording Balance For Cutomer
                    MainBody = MainBody.Replace("<!--##@Balance_Note##-->", "Please pay the balance at hotel upon your check in.");
                }
                //string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@GrandTotal_Resubmit_Start##-->", "<!--##@GrandTotal_Resubmit_End##-->");
                //MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

                //CustomerName 
                MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

                //GrandTotal
                MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal(pricetype));


                //GrandBalance
                MainBody = MainBody.Replace("<!--##@mailItemTotalBalanceContent##-->", strGrandBalance);

                //GrandPaid
                MainBody = MainBody.Replace("<!--##@mailItemTotalPaidContent##-->", GrandPaidTotal(pricetype));

                // //Disable Grandtotal Main
                string KeywordGrandTotal = Utility.GetKeywordReplace(MainBody, "<!--##@Grandtotal_Start##-->", "<!--##@Grandtotal_End##-->");
                MainBody = MainBody.Replace(KeywordGrandTotal, " ");

                //Disable BankTransfer 
                string KeywordBankTransfer = Utility.GetKeywordReplace(MainBody, "<!--##@Banktransfer_start##-->", "<!--##@Banktransfer_End##-->");
                MainBody = MainBody.Replace(KeywordBankTransfer, " ");
                //disable Total Paid & requset Payment
                //string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalresubmitStart##-->", "<!--##@ContentTotalresubmitEnd##-->");
                //MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

                string KeywordreqPay = Utility.GetKeywordReplace(MainBody, "<!--##@ReqPay_Start##-->", "<!--##@ReqPay_End##-->");
                MainBody = MainBody.Replace(KeywordreqPay, " ");

                //getDetail Voucher
                MainBody = MainBody.Replace("<!--##@ContentVoucherDetail##-->", TextDetailVoucher());

                Production.ProductPic cProductPic = new Production.ProductPic();

                string Logo = "<img  src=\"" + cProductPic.getProductlogo(this.cClassBookingProduct.ProductID) + "\" style=\"width:150px; height:60px; border:solid 1px #eee5be\" />";
                //Logo Is Open

                MainBody = MainBody.Replace("<!--##@HotelLogo##-->", Logo);
                //EmailTracking Is Open
                //MainBody = MainBody.Replace("<!--##@ContentEmailtracking##-->", imgTrack(14));
                BookingProductDisplay cBookingPLsit = new BookingProductDisplay();
                cBookingPLsit = cBookingPLsit.getBookingProductDisplayByBookingProductId(this.cClassBookingProduct.BookingProductId);

                //BookingProductName
                MainBody = MainBody.Replace("<!--##@HotelName##-->", cBookingPLsit.ProductTitle);
                //Hotel Phone
                MainBody = MainBody.Replace("<!--##@HotelAddress##-->", cBookingPLsit.ProductAddress);
                //HOtel Address
                MainBody = MainBody.Replace("<!--##@HotelPhone##-->", cBookingPLsit.ProductPhone);
                //BookingProductName
                MainBody = MainBody.Replace("<!--##@HotelName_foot##-->", cBookingPLsit.ProductTitle);
                //ContactUsPhone <!--##@HotelPhoneContact##-->
                MainBody = MainBody.Replace("<!--##@HotelPhoneContact##-->", cBookingPLsit.ProductPhone);


                //Sign Confirm
                MainBody = MainBody.Replace("<!--##@mailSignConfirm##-->", "<img src=\"http://order.booking2hotels.com/images_extra/confirmation2.gif\" />");

                BookingStaff cBookingStaff = new BookingStaff();

                // Contact Us Email <!--##@EmailContactUs##-->
                MainBody = MainBody.Replace("<!--##@EmailContactUs##-->", cBookingStaff.GetStringEmail_mailtoForm_showMail(cProductBookingEngine.EmailContactMail));
            }
            else
            {
                Hotels2thailand.BookingB2b.BookingMailEngineB2b cMailBookingB2B = new BookingB2b.BookingMailEngineB2b(this.BookingId);
                MainBody = cMailBookingB2B.getMailSendVoucher();
            }

            
            return MainBody;
        }
        public string getMailSendVoucher_Booknow_offline(string pricetype = "")
        {
            string MainBody = string.Empty;
            if (!this.cProductBookingEngine.Is_B2b)
            {
                MainBody = getMailThemeNew();

                //Booking IitemList
                string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
                MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId(pricetype).Replace("<!--##@BookingNameItem##-->", CusName()));

                //Detail Top
                string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
                MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(18));

                string strGrandBalance = GrandBalance(pricetype);

                if (strGrandBalance != "0.00")
                {
                    //Wording Balance For Cutomer
                    MainBody = MainBody.Replace("<!--##@Balance_Note##-->", "Please pay the balance at hotel upon your check in.");
                }
                //string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@GrandTotal_Resubmit_Start##-->", "<!--##@GrandTotal_Resubmit_End##-->");
                //MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

                //CustomerName 
                MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

                //GrandTotal
                MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal(pricetype));


                //GrandBalance
                MainBody = MainBody.Replace("<!--##@mailItemTotalBalanceContent##-->", strGrandBalance);

                //GrandPaid
                MainBody = MainBody.Replace("<!--##@mailItemTotalPaidContent##-->", GrandPaidTotal(pricetype));

                // //Disable Grandtotal Main
                string KeywordGrandTotal = Utility.GetKeywordReplace(MainBody, "<!--##@Grandtotal_Start##-->", "<!--##@Grandtotal_End##-->");
                MainBody = MainBody.Replace(KeywordGrandTotal, " ");

                //Disable BankTransfer 
                string KeywordBankTransfer = Utility.GetKeywordReplace(MainBody, "<!--##@Banktransfer_start##-->", "<!--##@Banktransfer_End##-->");
                MainBody = MainBody.Replace(KeywordBankTransfer, " ");
                //disable Total Paid & requset Payment
                //string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalresubmitStart##-->", "<!--##@ContentTotalresubmitEnd##-->");
                //MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

                string KeywordreqPay = Utility.GetKeywordReplace(MainBody, "<!--##@ReqPay_Start##-->", "<!--##@ReqPay_End##-->");
                MainBody = MainBody.Replace(KeywordreqPay, " ");

                //getDetail Voucher
                MainBody = MainBody.Replace("<!--##@ContentVoucherDetail##-->", TextDetailVoucher());

                Production.ProductPic cProductPic = new Production.ProductPic();

                string Logo = "<img  src=\"" + cProductPic.getProductlogo(this.cClassBookingProduct.ProductID) + "\" style=\"width:150px; height:60px; border:solid 1px #eee5be\" />";
                //Logo Is Open

                MainBody = MainBody.Replace("<!--##@HotelLogo##-->", Logo);
                //EmailTracking Is Open
                //MainBody = MainBody.Replace("<!--##@ContentEmailtracking##-->", imgTrack(14));
                BookingProductDisplay cBookingPLsit = new BookingProductDisplay();
                cBookingPLsit = cBookingPLsit.getBookingProductDisplayByBookingProductId(this.cClassBookingProduct.BookingProductId);

                //BookingProductName
                MainBody = MainBody.Replace("<!--##@HotelName##-->", cBookingPLsit.ProductTitle);
                //Hotel Phone
                MainBody = MainBody.Replace("<!--##@HotelAddress##-->", cBookingPLsit.ProductAddress);
                //HOtel Address
                MainBody = MainBody.Replace("<!--##@HotelPhone##-->", cBookingPLsit.ProductPhone);
                //BookingProductName
                MainBody = MainBody.Replace("<!--##@HotelName_foot##-->", cBookingPLsit.ProductTitle);
                //ContactUsPhone <!--##@HotelPhoneContact##-->
                MainBody = MainBody.Replace("<!--##@HotelPhoneContact##-->", cBookingPLsit.ProductPhone);


                //Sign Confirm
                MainBody = MainBody.Replace("<!--##@mailSignConfirm##-->", "<img src=\"http://order.booking2hotels.com/images_extra/confirmation2.gif\" />");

                BookingStaff cBookingStaff = new BookingStaff();

                // Contact Us Email <!--##@EmailContactUs##-->
                MainBody = MainBody.Replace("<!--##@EmailContactUs##-->", cBookingStaff.GetStringEmail_mailtoForm_showMail(cProductBookingEngine.EmailContactMail));
            }
            else
            {
                Hotels2thailand.BookingB2b.BookingMailEngineB2b cMailBookingB2B = new BookingB2b.BookingMailEngineB2b(this.BookingId);
                MainBody = cMailBookingB2B.getMailSendVoucher();
            }


            return MainBody;
        }

        public string getMailBookingRecieved_Booknow_offline()
        {
            bool bolIsB2b = this.cProductBookingEngine.Is_B2b;
            string MainBody = string.Empty;

            if (!bolIsB2b)
            {

                MainBody = getMailThemeNew();

                //CustomerName 
                MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

                // Detail Top
                string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
                MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(2));

                //Booking IitemList
                string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
                MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId().Replace("<!--##@BookingNameItem##-->", CusName()));

                string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@GrandTotal_Resubmit_Start##-->", "<!--##@GrandTotal_Resubmit_End##-->");
                MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

                string KeywordreqPay = Utility.GetKeywordReplace(MainBody, "<!--##@ReqPay_Start##-->", "<!--##@ReqPay_End##-->");
                MainBody = MainBody.Replace(KeywordreqPay, " ");

                string KeywordBankTransfer = Utility.GetKeywordReplace(MainBody, "<!--##@Banktransfer_start##-->", "<!--##@Banktransfer_End##-->");
                MainBody = MainBody.Replace(KeywordBankTransfer, " ");




                //Wording Balance For Cutomer
                //MainBody = MainBody.Replace("<!--##@Balance_Note##-->", "Please pay the balance at hotel upon your check in.");

                // //Disable Grandtotal Balance Main
                string KeywordGrandTotalBalance = Utility.GetKeywordReplace(MainBody, "<!--##@Balance_Start##-->", "<!--##@Balance_End##-->");
                MainBody = MainBody.Replace(KeywordGrandTotalBalance, " ");

                ////GrandTotal
                MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal());

                //EmailTracking Is Open
                //MainBody = MainBody.Replace("<!--##@ContentEmailtracking##-->", imgTrack(15));


                Production.ProductPic cProductPic = new Production.ProductPic();

                string Logo = "<img  src=\"" + cProductPic.getProductlogo(this.cClassBookingProduct.ProductID) + "\" style=\"width:150px; height:60px; border:solid 1px #eee5be\" />";
                //Logo Is Open

                MainBody = MainBody.Replace("<!--##@HotelLogo##-->", Logo);


                BookingProductDisplay cBookingPLsit = new BookingProductDisplay();
                cBookingPLsit = cBookingPLsit.getBookingProductDisplayByBookingProductId(this.cClassBookingProduct.BookingProductId);

                //BookingProductName
                MainBody = MainBody.Replace("<!--##@HotelName##-->", cBookingPLsit.ProductTitle);
                //BookingProductName
                MainBody = MainBody.Replace("<!--##@HotelName_foot##-->", cBookingPLsit.ProductTitle);
                //Hotel Phone
                MainBody = MainBody.Replace("<!--##@HotelAddress##-->", cBookingPLsit.ProductAddress);
                //HOtel Address
                MainBody = MainBody.Replace("<!--##@HotelPhone##-->", cBookingPLsit.ProductPhone);

                //ContactUsPhone <!--##@HotelPhoneContact##-->
                MainBody = MainBody.Replace("<!--##@HotelPhoneContact##-->", cBookingPLsit.ProductPhone);

                BookingStaff cBookingStaff = new BookingStaff();
                // Contact Us Email <!--##@EmailContactUs##-->
                MainBody = MainBody.Replace("<!--##@EmailContactUs##-->", cBookingStaff.GetStringEmail_mailtoForm_showMail(cProductBookingEngine.EmailContactMail));
            }
            else
            {
                Hotels2thailand.BookingB2b.BookingMailEngineB2b cBookingMailB2b = new BookingB2b.BookingMailEngineB2b(this.BookingId);
                MainBody = cBookingMailB2b.getMailBookingRecieved();
            }

            return MainBody;
            //string KeywordContentFoot = Utility.GetKeywordReplace(MainBody, "<!--##@ContentFoot_start##-->", "<!--##@ContentFoot_end##-->");
            //MainBody = MainBody.Replace(KeywordContentFoot, strGrandTotal.ToString());
            //getDetail Voucher
            //MainBody = MainBody.Replace("<!--##@ContentBodyDisplay##-->", TextDetailVoucher());

        }

        public string getMailBookingRecieved()
        {
            bool bolIsB2b = this.cProductBookingEngine.Is_B2b;
            string MainBody = string.Empty;

            if (!bolIsB2b)
            {

                MainBody = getMailThemeNew();

                //CustomerName 
                MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

                // Detail Top
                string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
                MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(2));

                //Booking IitemList
                string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
                MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId().Replace("<!--##@BookingNameItem##-->", CusName()));

                string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@GrandTotal_Resubmit_Start##-->", "<!--##@GrandTotal_Resubmit_End##-->");
                MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

                string KeywordreqPay = Utility.GetKeywordReplace(MainBody, "<!--##@ReqPay_Start##-->", "<!--##@ReqPay_End##-->");
                MainBody = MainBody.Replace(KeywordreqPay, " ");

                string KeywordBankTransfer = Utility.GetKeywordReplace(MainBody, "<!--##@Banktransfer_start##-->", "<!--##@Banktransfer_End##-->");
                MainBody = MainBody.Replace(KeywordBankTransfer, " ");




                //Wording Balance For Cutomer
                //MainBody = MainBody.Replace("<!--##@Balance_Note##-->", "Please pay the balance at hotel upon your check in.");

                // //Disable Grandtotal Balance Main
                string KeywordGrandTotalBalance = Utility.GetKeywordReplace(MainBody, "<!--##@Balance_Start##-->", "<!--##@Balance_End##-->");
                MainBody = MainBody.Replace(KeywordGrandTotalBalance, " ");

                ////GrandTotal
                MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal());

                //EmailTracking Is Open
                //MainBody = MainBody.Replace("<!--##@ContentEmailtracking##-->", imgTrack(15));


                Production.ProductPic cProductPic = new Production.ProductPic();

                string Logo = "<img  src=\"" + cProductPic.getProductlogo(this.cClassBookingProduct.ProductID) + "\" style=\"width:150px; height:60px; border:solid 1px #eee5be\" />";
                //Logo Is Open

                MainBody = MainBody.Replace("<!--##@HotelLogo##-->", Logo);


                BookingProductDisplay cBookingPLsit = new BookingProductDisplay();
                cBookingPLsit = cBookingPLsit.getBookingProductDisplayByBookingProductId(this.cClassBookingProduct.BookingProductId);

                //BookingProductName
                MainBody = MainBody.Replace("<!--##@HotelName##-->", cBookingPLsit.ProductTitle);
                //BookingProductName
                MainBody = MainBody.Replace("<!--##@HotelName_foot##-->", cBookingPLsit.ProductTitle);
                //Hotel Phone
                MainBody = MainBody.Replace("<!--##@HotelAddress##-->", cBookingPLsit.ProductAddress);
                //HOtel Address
                MainBody = MainBody.Replace("<!--##@HotelPhone##-->", cBookingPLsit.ProductPhone);

                //ContactUsPhone <!--##@HotelPhoneContact##-->
                MainBody = MainBody.Replace("<!--##@HotelPhoneContact##-->", cBookingPLsit.ProductPhone);

                BookingStaff cBookingStaff = new BookingStaff();
                // Contact Us Email <!--##@EmailContactUs##-->
                MainBody = MainBody.Replace("<!--##@EmailContactUs##-->", cBookingStaff.GetStringEmail_mailtoForm_showMail(cProductBookingEngine.EmailContactMail));
            }
            else
            {
                Hotels2thailand.BookingB2b.BookingMailEngineB2b cBookingMailB2b = new BookingB2b.BookingMailEngineB2b(this.BookingId);
                MainBody = cBookingMailB2b.getMailBookingRecieved();
            }

            return MainBody;
            //string KeywordContentFoot = Utility.GetKeywordReplace(MainBody, "<!--##@ContentFoot_start##-->", "<!--##@ContentFoot_end##-->");
            //MainBody = MainBody.Replace(KeywordContentFoot, strGrandTotal.ToString());
            //getDetail Voucher
            //MainBody = MainBody.Replace("<!--##@ContentBodyDisplay##-->", TextDetailVoucher());
            
        }


        public string getMailBookingRecieved_Allot_Booknow_offline()
        {

            string MainBody = string.Empty;
            if (!this.cProductBookingEngine.Is_B2b)
            {


                MainBody = getMailThemeNew();

                //CustomerName 
                MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

                // Detail Top
                string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
                MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(17));

                //Booking IitemList
                string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
                MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId().Replace("<!--##@BookingNameItem##-->", CusName()));

                string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@GrandTotal_Resubmit_Start##-->", "<!--##@GrandTotal_Resubmit_End##-->");
                MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

                string KeywordreqPay = Utility.GetKeywordReplace(MainBody, "<!--##@ReqPay_Start##-->", "<!--##@ReqPay_End##-->");
                MainBody = MainBody.Replace(KeywordreqPay, " ");

                string KeywordBankTransfer = Utility.GetKeywordReplace(MainBody, "<!--##@Banktransfer_start##-->", "<!--##@Banktransfer_End##-->");
                MainBody = MainBody.Replace(KeywordBankTransfer, " ");

                //Wording Balance For Cutomer
                //MainBody = MainBody.Replace("<!--##@Balance_Note##-->", "Please pay the balance at hotel upon your check in.");

                // //Disable Grandtotal Balance Main
                string KeywordGrandTotalBalance = Utility.GetKeywordReplace(MainBody, "<!--##@Balance_Start##-->", "<!--##@Balance_End##-->");
                MainBody = MainBody.Replace(KeywordGrandTotalBalance, " ");

                ////GrandTotal
                MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal());

                //EmailTracking Is Open
                //MainBody = MainBody.Replace("<!--##@ContentEmailtracking##-->", imgTrack(15));


                Production.ProductPic cProductPic = new Production.ProductPic();

                string Logo = "<img  src=\"" + cProductPic.getProductlogo(this.cClassBookingProduct.ProductID) + "\" style=\"width:150px; height:60px; border:solid 1px #eee5be\" />";
                //Logo Is Open

                MainBody = MainBody.Replace("<!--##@HotelLogo##-->", Logo);


                BookingProductDisplay cBookingPLsit = new BookingProductDisplay();
                cBookingPLsit = cBookingPLsit.getBookingProductDisplayByBookingProductId(this.cClassBookingProduct.BookingProductId);

                //BookingProductName
                MainBody = MainBody.Replace("<!--##@HotelName##-->", cBookingPLsit.ProductTitle);
                //BookingProductName
                MainBody = MainBody.Replace("<!--##@HotelName_foot##-->", cBookingPLsit.ProductTitle);
                //Hotel Phone
                MainBody = MainBody.Replace("<!--##@HotelAddress##-->", cBookingPLsit.ProductAddress);
                //HOtel Address
                MainBody = MainBody.Replace("<!--##@HotelPhone##-->", cBookingPLsit.ProductPhone);

                //ContactUsPhone <!--##@HotelPhoneContact##-->
                MainBody = MainBody.Replace("<!--##@HotelPhoneContact##-->", cBookingPLsit.ProductPhone);

                BookingStaff cBookingStaff = new BookingStaff();
                // Contact Us Email <!--##@EmailContactUs##-->
                MainBody = MainBody.Replace("<!--##@EmailContactUs##-->", cBookingStaff.GetStringEmail_mailtoForm_showMail(cProductBookingEngine.EmailContactMail));

            }
            else
            {
                Hotels2thailand.BookingB2b.BookingMailEngineB2b cBookingMailB2b = new BookingB2b.BookingMailEngineB2b(this.BookingId);
                MainBody = cBookingMailB2b.getMailBookingRecieved();
            }

            return MainBody;
            //string KeywordContentFoot = Utility.GetKeywordReplace(MainBody, "<!--##@ContentFoot_start##-->", "<!--##@ContentFoot_end##-->");
            //MainBody = MainBody.Replace(KeywordContentFoot, strGrandTotal.ToString());
            //getDetail Voucher
            //MainBody = MainBody.Replace("<!--##@ContentBodyDisplay##-->", TextDetailVoucher());

        }

        public string getMailBookingRecieved_Allot()
        {

            string MainBody = string.Empty;
            if (!this.cProductBookingEngine.Is_B2b)
            {


                 MainBody = getMailThemeNew();

                //CustomerName 
                MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

                // Detail Top
                string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
                MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(15));

                //Booking IitemList
                string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
                MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId().Replace("<!--##@BookingNameItem##-->", CusName()));

                string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@GrandTotal_Resubmit_Start##-->", "<!--##@GrandTotal_Resubmit_End##-->");
                MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

                string KeywordreqPay = Utility.GetKeywordReplace(MainBody, "<!--##@ReqPay_Start##-->", "<!--##@ReqPay_End##-->");
                MainBody = MainBody.Replace(KeywordreqPay, " ");

                string KeywordBankTransfer = Utility.GetKeywordReplace(MainBody, "<!--##@Banktransfer_start##-->", "<!--##@Banktransfer_End##-->");
                MainBody = MainBody.Replace(KeywordBankTransfer, " ");

                //Wording Balance For Cutomer
                //MainBody = MainBody.Replace("<!--##@Balance_Note##-->", "Please pay the balance at hotel upon your check in.");

                // //Disable Grandtotal Balance Main
                string KeywordGrandTotalBalance = Utility.GetKeywordReplace(MainBody, "<!--##@Balance_Start##-->", "<!--##@Balance_End##-->");
                MainBody = MainBody.Replace(KeywordGrandTotalBalance, " ");

                ////GrandTotal
                MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal());

                //EmailTracking Is Open
                //MainBody = MainBody.Replace("<!--##@ContentEmailtracking##-->", imgTrack(15));


                Production.ProductPic cProductPic = new Production.ProductPic();

                string Logo = "<img  src=\"" + cProductPic.getProductlogo(this.cClassBookingProduct.ProductID) + "\" style=\"width:150px; height:60px; border:solid 1px #eee5be\" />";
                //Logo Is Open

                MainBody = MainBody.Replace("<!--##@HotelLogo##-->", Logo);


                BookingProductDisplay cBookingPLsit = new BookingProductDisplay();
                cBookingPLsit = cBookingPLsit.getBookingProductDisplayByBookingProductId(this.cClassBookingProduct.BookingProductId);

                //BookingProductName
                MainBody = MainBody.Replace("<!--##@HotelName##-->", cBookingPLsit.ProductTitle);
                //BookingProductName
                MainBody = MainBody.Replace("<!--##@HotelName_foot##-->", cBookingPLsit.ProductTitle);
                //Hotel Phone
                MainBody = MainBody.Replace("<!--##@HotelAddress##-->", cBookingPLsit.ProductAddress);
                //HOtel Address
                MainBody = MainBody.Replace("<!--##@HotelPhone##-->", cBookingPLsit.ProductPhone);

                //ContactUsPhone <!--##@HotelPhoneContact##-->
                MainBody = MainBody.Replace("<!--##@HotelPhoneContact##-->", cBookingPLsit.ProductPhone);

                BookingStaff cBookingStaff = new BookingStaff();
                // Contact Us Email <!--##@EmailContactUs##-->
                MainBody = MainBody.Replace("<!--##@EmailContactUs##-->", cBookingStaff.GetStringEmail_mailtoForm_showMail(cProductBookingEngine.EmailContactMail));

            }
            else
            {
                Hotels2thailand.BookingB2b.BookingMailEngineB2b cBookingMailB2b = new BookingB2b.BookingMailEngineB2b(this.BookingId);
                MainBody = cBookingMailB2b.getMailBookingRecieved();
            }

            return MainBody;
            //string KeywordContentFoot = Utility.GetKeywordReplace(MainBody, "<!--##@ContentFoot_start##-->", "<!--##@ContentFoot_end##-->");
            //MainBody = MainBody.Replace(KeywordContentFoot, strGrandTotal.ToString());
            //getDetail Voucher
            //MainBody = MainBody.Replace("<!--##@ContentBodyDisplay##-->", TextDetailVoucher());

        }



        public string getMailBookingRecieved_hotel_notice()
        {
            string MainBody = getMailThemeNew();


            //CustomerName 
            MainBody = MainBody.Replace("<!--##@CusNameContent##-->", "Reservation");

            // Detail Top
            string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(14));

            //Booking IitemList
            string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
            MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId("supplier_price_show").Replace("<!--##@BookingNameItem##-->", CusName()));

            //string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@GrandTotal_Resubmit_Start##-->", "<!--##@GrandTotal_Resubmit_End##-->");
            //MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

            string KeywordreqPay = Utility.GetKeywordReplace(MainBody, "<!--##@ReqPay_Start##-->", "<!--##@ReqPay_End##-->");
            MainBody = MainBody.Replace(KeywordreqPay, " ");

            string KeywordBankTransfer = Utility.GetKeywordReplace(MainBody, "<!--##@Banktransfer_start##-->", "<!--##@Banktransfer_End##-->");
            MainBody = MainBody.Replace(KeywordBankTransfer, " ");

            //Wording Balance For Hotel
            //MainBody = MainBody.Replace("<!--##@Balance_Note##-->", "Customer will pay the balance at the hotel upon your check in time.");

            string pricetype = string.Empty;
            if (cProductBookingEngine.ProductID == 3605 || cProductBookingEngine.ProductID == 3692)
                pricetype = "supplier_price_show";


            //GrandBalance
                MainBody = MainBody.Replace("<!--##@mailItemTotalBalanceContent##-->", GrandBalance(pricetype));

            //GrandPaid
            MainBody = MainBody.Replace("<!--##@mailItemTotalPaidContent##-->", GrandPaidTotal(pricetype));

            ////GrandTotal
            MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal(pricetype));



            // //Disable Grandtotal Main
            string KeywordGrandTotal = Utility.GetKeywordReplace(MainBody, "<!--##@Grandtotal_Start##-->", "<!--##@Grandtotal_End##-->");
            MainBody = MainBody.Replace(KeywordGrandTotal, " ");
            //Grandtotal_Start
            //EmailTracking Is Open
            //MainBody = MainBody.Replace("<!--##@ContentEmailtracking##-->", imgTrack(15));


            Production.ProductPic cProductPic = new Production.ProductPic();

            string Logo = "<img  src=\"" + cProductPic.getProductlogo(this.cClassBookingProduct.ProductID) + "\" style=\"width:150px; height:60px; border:solid 1px #eee5be\" />";
            //Logo Is Open

            MainBody = MainBody.Replace("<!--##@HotelLogo##-->", Logo);


            BookingProductDisplay cBookingPLsit = new BookingProductDisplay();
            cBookingPLsit = cBookingPLsit.getBookingProductDisplayByBookingProductId(this.cClassBookingProduct.BookingProductId);

            //BookingProductName
            MainBody = MainBody.Replace("<!--##@HotelName##-->", cBookingPLsit.ProductTitle);
            //BookingProductName
            MainBody = MainBody.Replace("<!--##@HotelName_foot##-->","Booking2hotels.com");
            //Hotel Phone
            MainBody = MainBody.Replace("<!--##@HotelAddress##-->", cBookingPLsit.ProductAddress); 
            //HOtel Address
            MainBody = MainBody.Replace("<!--##@HotelPhone##-->", cBookingPLsit.ProductPhone);

            string KeywordContentFoot = Utility.GetKeywordReplace(MainBody, "<!--##@ContentFoot_start##-->", "<!--##@ContentFoot_end##-->");
            MainBody = MainBody.Replace(KeywordContentFoot, "");
            //getDetail Voucher
            //MainBody = MainBody.Replace("<!--##@ContentBodyDisplay##-->", TextDetailVoucher());


            //ContactUsPhone <!--##@HotelPhoneContact##-->
            MainBody = MainBody.Replace("<!--##@HotelPhoneContact##-->", cBookingPLsit.ProductPhone);

            BookingStaff cBookingStaff = new BookingStaff();
            // Contact Us Email <!--##@EmailContactUs##-->
            MainBody = MainBody.Replace("<!--##@EmailContactUs##-->",cBookingStaff.GetStringEmail_mailtoForm_showMail(cProductBookingEngine.EmailContactMail));

            return MainBody;
        }



        public string getMailBookingRecieved_notice_offline_charge(string stringEndcode)
        {
            string MainBody = getMailThemeNew();


            //CustomerName 
            MainBody = MainBody.Replace("<!--##@CusNameContent##-->", "Reservation");


            string strEncodeDisplay = "<div style=\"padding:15px;width:500px;border:1px solid #cccccc;\">" + stringEndcode + "</div>";
            // Detail Top
            string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");

            MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(16).Replace("<!--##@WordingCardEndcode##-->", strEncodeDisplay));

            //Booking IitemList
            string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
            MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId().Replace("<!--##@BookingNameItem##-->", CusName()));

            //string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@GrandTotal_Resubmit_Start##-->", "<!--##@GrandTotal_Resubmit_End##-->"); 
            //MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

            string KeywordreqPay = Utility.GetKeywordReplace(MainBody, "<!--##@ReqPay_Start##-->", "<!--##@ReqPay_End##-->");
            MainBody = MainBody.Replace(KeywordreqPay, " ");

            string KeywordBankTransfer = Utility.GetKeywordReplace(MainBody, "<!--##@Banktransfer_start##-->", "<!--##@Banktransfer_End##-->");
            MainBody = MainBody.Replace(KeywordBankTransfer, " ");

            //Wording Balance For Hotel
           // MainBody = MainBody.Replace("<!--##@Balance_Note##-->", "Customer will pay the balance at the hotel upon your check in time.");

            //GrandBalance
            MainBody = MainBody.Replace("<!--##@mailItemTotalBalanceContent##-->", GrandBalance());

            //GrandPaid
            MainBody = MainBody.Replace("<!--##@mailItemTotalPaidContent##-->", GrandPaidTotal());

            // //Disable Grandtotal Main
            string KeywordGrandTotal = Utility.GetKeywordReplace(MainBody, "<!--##@Grandtotal_Start##-->", "<!--##@Grandtotal_End##-->");
            MainBody = MainBody.Replace(KeywordGrandTotal, " ");

            ////GrandTotal
            MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal());

            //EmailTracking Is Open
            //MainBody = MainBody.Replace("<!--##@ContentEmailtracking##-->", imgTrack(15));


            Production.ProductPic cProductPic = new Production.ProductPic();

            string Logo = "<img  src=\"" + cProductPic.getProductlogo(this.cClassBookingProduct.ProductID) + "\" style=\"width:150px; height:60px; border:solid 1px #eee5be\" />";
            //Logo Is Open

            MainBody = MainBody.Replace("<!--##@HotelLogo##-->", Logo);


            BookingProductDisplay cBookingPLsit = new BookingProductDisplay();
            cBookingPLsit = cBookingPLsit.getBookingProductDisplayByBookingProductId(this.cClassBookingProduct.BookingProductId);

            //BookingProductName
            MainBody = MainBody.Replace("<!--##@HotelName##-->", cBookingPLsit.ProductTitle);
            //BookingProductName
            MainBody = MainBody.Replace("<!--##@HotelName_foot##-->", "Booking2hotels.com");
            //Hotel Phone
            MainBody = MainBody.Replace("<!--##@HotelAddress##-->", cBookingPLsit.ProductAddress);
            //HOtel Address
            MainBody = MainBody.Replace("<!--##@HotelPhone##-->", cBookingPLsit.ProductPhone);

            string KeywordContentFoot = Utility.GetKeywordReplace(MainBody, "<!--##@ContentFoot_start##-->", "<!--##@ContentFoot_end##-->");
            MainBody = MainBody.Replace(KeywordContentFoot, "");
            //getDetail Voucher
            //MainBody = MainBody.Replace("<!--##@ContentBodyDisplay##-->", TextDetailVoucher());


            //ContactUsPhone <!--##@HotelPhoneContact##-->
            MainBody = MainBody.Replace("<!--##@HotelPhoneContact##-->", cBookingPLsit.ProductPhone);

            BookingStaff cBookingStaff = new BookingStaff();
            // Contact Us Email <!--##@EmailContactUs##-->
            MainBody = MainBody.Replace("<!--##@EmailContactUs##-->",cBookingStaff.GetStringEmail_mailtoForm_showMail(cProductBookingEngine.EmailContactMail));

            return MainBody;
        }

        public string getMailBookingRecieved_BankTransfer()
        {
            string MainBody = getMailThemeNew();

            //CustomerName 
            MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

            //Detail Top
            string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(13));

            //Booking IitemList
            string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
            MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId());

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

            Production.ProductPic cProductPic = new Production.ProductPic();

            string Logo = "<img  src=\"" + cProductPic.getProductlogo(this.cClassBookingProduct.ProductID) + "\" style=\"width:150px; height:60px; border:solid 1px #eee5be\" />";
            //Logo Is Open

            MainBody = MainBody.Replace("<!--##@HotelLogo##-->", Logo);
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
            string[] arrFirt1 = { "Reservation Confirmation:", "เอกสารยืนยันการจอง:" };//
            string[] arrFirt2 = { "Please write review for ", "ความคิดเห็นของท่านมีค่ากับเราเสมอ:" };
            string[] arrFirt3 = { "Request Your Payment Information:", "ข้อมูลการเรียกชำระเงินสำหรับการจอง:" };
            string[] arrFirt4 = { "and", "และ" };
            string[] arrFirt5 = { "New Booking (Offline Payment):", "ข้อมูลการจอง (ออฟไลน์ชาร์ต):" };
            string[] arrFirt6 = { "New Booking :", "ข้อมูลการจอง:" };

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
                    result = txtSup + " #Booking ID : " + this.BookingHotelId;
                    break;
                case MailCat.Resubmit:
                    txtSup = arrFirt3[this.BookingLangId - 1] + " " + Product;
                    result = txtSup + " #Booking ID : " + this.BookingHotelId;
                    break;
                case MailCat.ComfirmBooking:
                    txtSup = arrFirt1[this.BookingLangId - 1] + " " + Product;
                    result = txtSup + " #Booking ID : " + this.BookingHotelId;
                    break;
                case MailCat.Review:

                    result = arrFirt2[this.BookingLangId - 1] + " " + Product;
                    break;
                case MailCat.Mail_hotel_notice:
                    txtSup = arrFirt6[this.BookingLangId - 1] + " " + Product;
                    result = txtSup + " #Booking ID : " + this.BookingHotelId;
                    break;
                case MailCat.Mail_hotel_offline:
                    txtSup = arrFirt5[this.BookingLangId - 1] + " " + Product;
                    result = txtSup + " #Booking ID : " + this.BookingHotelId;
                    break;
                case MailCat.BookingRecevied_allot:
                    txtSup = arrFirt[this.BookingLangId - 1] + " " + Product;
                    result = txtSup + " #Booking ID : " + this.BookingHotelId;
                    break;
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

        protected string imgTrack(byte bytMailConfirmCat)
        {
            string result = string.Empty;



            string query = "bid=" + Hotels2String.EncodeIdToURL(this.BookingId.ToString() + "#" + bytMailConfirmCat.ToString());


            result = "<!--##@ContentEmailtracking_remove_start##--><img id=\"img_captcha\"  src=\"http://www.booking2hotels.com/mt?" + query + "\" alt=\"\" /><!--##@ContentEmailtracking_remove_end##-->";

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
            switch (this.BookingLangId)
            {
                case 1:
                    strBtnPayment = "makpay_en.jpg";
                    makePayment = "Make Payment";
                    break;
                case 2:
                    strBtnPayment = "makpay_th.jpg";
                    makePayment = "ชำระเงิน";
                    break;
            }


            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td><tr>");
            result.Append("<tr><td align=\"center\" style=\"height:10px;text-align:center;\">");
            result.Append("<a href=\"http://www.booking2hotels.com/booking_resubmit.aspx?pcode=" + paymentEncode + "\"><img src=\"http://www.booking2hotels.com/images_mail/" + strBtnPayment + "\" style=\"border:0px;cursor:pointer;\" alt=\"" + makePayment + "\" width=\"206\" height=\"41\" /></a>");

            result.Append("</td></tr>");
            result.Append("<tr><td style=\"height:10px;text-align:center; font-size:11px;\">");
            result.Append("<span style=\"font-weight:bold;color:#dd4b39;\">**</span>If you can’t see <strong>\"Make Payment\"</strong> button, please click <a href=\"http://www.booking2hotels.com/booking_resubmit.aspx?pcode=" + paymentEncode + "\" >" + makePayment + "</a> here");
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
            result.Append("<a href=\"http://www.booking2hotels.com/booking_resubmit.aspx?pcode=" + paymentEncode + "\"><img src=\"http://www.booking2hotels.com/theme_color/blue/images/button/make_payment.jpg\" style=\"border:0px;cursor:pointer;\" /></a>");
            //result.Append("");
            result.Append("</td></tr>");
            result.Append("<tr><td style=\"height: 10px;\"></td></tr>");
            //result.Append("<tr><td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
            //result.Append("Kindly acknowledge us if you find any payment problem or any such obstacles. It will be very much appreciated to be advised of any inconvenience to help you rectify the problem. Thank you very much.");
            //result.Append("</td></tr>");


            return result.ToString();
        }

        protected string getProductListFromBookingId(string price_type = "")
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

                        StreamReader objHotelDetailReader = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template/" + them_file_item + ""));
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

                        if ((BookingProductITem.ProductID == 3605 || BookingProductITem.ProductID == 3692) && price_type == "supplier_price_show")
                        {
                            BookingDetail = BookingDetail.Replace(KeywordItemList, GetProductItemHOtel(BookingProductITem.BookingProductId, DateTimeNightTotal(BookingProductITem.DateTimeCheckIn, BookingProductITem.DateTimeCheckOut), "supplier_price_show"));
                        }
                        else
                        {
                            BookingDetail = BookingDetail.Replace(KeywordItemList, GetProductItemHOtel(BookingProductITem.BookingProductId, DateTimeNightTotal(BookingProductITem.DateTimeCheckIn, BookingProductITem.DateTimeCheckOut)));
                        }
               
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
                        StreamReader objHotelDetailReader_Spa = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template/" + them_file_item + ""));
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
                        BookingDetail = BookingDetail.Replace("<!--##@mailProduct_BookingspaDateService##-->", DateTimeCheck(BookingProductITem.DateTimeCheckIn, BookingProductITem.DateTimeConfirmCheckIn));
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
                        StreamReader objHotelDetailReader_transfer = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template/" + them_file_item + ""));
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
                        StreamReader objHotelDetailReader_Show_water_Daytrip = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template/" + them_file_item + ""));
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
                    BookingDetail = BookingDetail.Replace("<!--##@mailProduct_BookingTripDate##-->", DateTimeCheck(BookingProductITem.DateTimeCheckIn));


                    //ItemList
                    string KeywordItemList_Show_water_Daytrip = Utility.GetKeywordReplace(BookingDetail, "<!--##@mailProductItemStart##-->", "<!--##@mailProductItemEnd##-->");
                    BookingDetail = BookingDetail.Replace(KeywordItemList_Show_water_Daytrip, GetProductItem_show_day_water(BookingProductITem.BookingProductId, DateTimeNightTotal(BookingProductITem.DateTimeCheckIn, BookingProductITem.DateTimeCheckOut)));

                    break;
                // Health
                case 39:

                    StreamReader objHotelDetailReader_Health = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template/tbookingMail_health.html"));
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

        protected string EncodeId(int IdtoEncod)
        {
            string Random = Hotels2String.Hotels2RandomStringNuM(20);
            string strToEndCode = IdtoEncod + Random;
            string EncodeResult = strToEndCode.Hotel2EncrytedData_SecretKey();
            return HttpUtility.UrlEncode(EncodeResult);
        }



        public string MapProductCatLink(byte bytProductCat, int ProductId)
        {
            Hotels2thailand.Production.ProductBookingEngine cProduct = new ProductBookingEngine();

            cProduct = cProduct.GetProductbookingEngine(ProductId);


            //string KeyCat = Utility.GetProductType(bytProductCat)[0, 3];
            string resutlLink = cProduct.WebsiteName + "/" + cProduct.Folder + "_map.html" ;
            //string resutlLink = "thailand-" + KeyCat + "-" + "map.aspx?pid=" + ProductId;
            return resutlLink;
        }

        protected string REviewProductCatLink(byte bytProductCat, int ProductId)
        {
            Hotels2thailand.Production.ProductBookingEngine cProduct = new ProductBookingEngine();

            cProduct = cProduct.GetProductbookingEngine(ProductId);


            //string KeyCat = Utility.GetProductType(bytProductCat)[0, 3];
            string resutlLink = cProduct.WebsiteName + "/" + cProduct.Folder + "_review_write.html";
            //string resutlLink = "thailand-" + KeyCat + "-" + "map.aspx?pid=" + ProductId;
            return resutlLink;
        }

        protected string MemberREsetPassProductCatLink(int ProductId, int CusId)
        {
            Hotels2thailand.Production.ProductBookingEngine cProduct = new ProductBookingEngine();

            cProduct = cProduct.GetProductbookingEngine(ProductId);
            string query = "?rg=" + Hotels2String.EncodeIdToURL("cus_id=" + CusId + "&pid=" + ProductId);
            //string query = "bid=" + Hotels2String.EncodeIdToURL(this.BookingId.ToString() + "#" + bytMailConfirmCat.ToString());
            //string KeyCat = Utility.GetProductType(bytProductCat)[0, 3];
            string resutlLink = cProduct.WebsiteName + "/" + cProduct.Folder + "_reset_password.html" + query;
            //string resutlLink = "thailand-" + KeyCat + "-" + "map.aspx?pid=" + ProductId;
            return resutlLink;
        }


        protected string MemberREgisration(int ProductId, int CusId)
        {
            Hotels2thailand.Production.ProductBookingEngine cProduct = new ProductBookingEngine();

            cProduct = cProduct.GetProductbookingEngine(ProductId);

            string query = "?rg=" + Hotels2String.EncodeIdToURL("cus_id=" + CusId +"&pid=" + ProductId);

            //string KeyCat = Utility.GetProductType(bytProductCat)[0, 3];
            string resutlLink = cProduct.WebsiteName + "/" + cProduct.Folder + "_activate.html" + query;
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

        protected string TextRegistrationCompleted(int intProductId, int intCusId)
        {

            ProductContent cProductContent = new ProductContent();
            cProductContent = cProductContent.GetProductContentById(intProductId, 1);


            StringBuilder result = new StringBuilder();

            string Link = MemberREgisration(intProductId, intCusId);

            result.Append("<tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
            result.Append("Thank you for your registration at " + cProductContent.Title + "");
            result.Append("</td>");
            result.Append("</tr>");
            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
            result.Append("<tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
            result.Append("Please click link below to activate your account with us now.");
            result.Append("</td>");
            result.Append("</tr>");
            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");


            result.Append("<tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
            result.Append(Link);
            result.Append("</td>");
            result.Append("</tr>");

            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
            result.Append("<tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
            result.Append("Please note that you do not have to answer all the questions. We will use our effort to serve you the best service.");
            result.Append("</td>");
            result.Append("</tr>");




            return result.ToString();
        }

        protected string TextforgotPassword(int intProductId, int intCusId)
        {
            ProductContent cProductContent = new ProductContent();
            cProductContent = cProductContent.GetProductContentById(intProductId, 1);

            StringBuilder result = new StringBuilder();

            string Link = MemberREsetPassProductCatLink(intProductId, intCusId);

            result.Append("<tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
            result.Append("Thank you for choosing " + cProductContent.Title + "");
            result.Append("</td>");
            result.Append("</tr>");
            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
            result.Append("<tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
            result.Append("As valued customer, your opinions on all aspects of your stay are very important to us. We would be grateful if you would complete this questionnaire and send us back in order to improve our service to all customer's satisfaction.</a>");
            result.Append("</td>");
            result.Append("</tr>");
            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
            result.Append("<tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
            result.Append("Please Click Link Below To REset your Password");
            result.Append("</td>");
            result.Append("</tr>");

            result.Append("<tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
            result.Append(Link);
            result.Append("</td>");
            result.Append("</tr>");

            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
            result.Append("<tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
            result.Append("Please note that you do not have to answer all the questions. We will use our effort to serve you the best service.");
            result.Append("</td>");
            result.Append("</tr>");





            return result.ToString();
        }

        protected string TextResetPassDetail(int intProductId, int intCusId)
        {
            ProductContent cProductContent = new ProductContent();
            cProductContent = cProductContent.GetProductContentById(intProductId, 1);

            StringBuilder result = new StringBuilder();

            string Link = MemberREsetPassProductCatLink(intProductId, intCusId);

            result.Append("<tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");

            result.Append("Thank you for your interest in our member subscription.");
            result.Append("</td>");
            result.Append("</tr>");
            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
            result.Append("<tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
            result.Append("For your security, you can change your password thru this link below.</a>");
            result.Append("</td>");
            result.Append("</tr>");
            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
            

            result.Append("<tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
            result.Append(Link);
            result.Append("</td>");
            result.Append("</tr>");

            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
            result.Append("<tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
            result.Append("Keep up to date with our activities ");
            result.Append("</td>");
            result.Append("</tr>");
            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
            result.Append("<tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
            result.Append("Thank you for your participation.");
            result.Append("</td>");
            result.Append("</tr>");

                        





            return result.ToString();
        }

        protected string TextActivateDetail(int intProductId, int intCusId, string email, string Pass)
        {
            ProductContent cProductContent = new ProductContent();
            cProductContent = cProductContent.GetProductContentById(intProductId, 1);

            StringBuilder result = new StringBuilder();


            string Link = MemberREsetPassProductCatLink(intProductId, intCusId);

            result.Append("<tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
            result.Append("Welcome to " + cProductContent.Title + "");
            result.Append("</td>");
            result.Append("</tr>");

            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
          
            result.Append("<tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
            result.Append("Thank you for your interest in our member subscription.Your account is validated automatically by our system.");
            result.Append("</td>");
            result.Append("</tr>");

            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");

            result.Append("<tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
            result.Append("<p><span style=\"font-weight:bold\">Username: </span> " + email + "</p>");
            result.Append("<p><span style=\"font-weight:bold;margin-top:5px;\">Password: </span> " + Pass + "</p>");
            result.Append("</td>");
            result.Append("</tr>");
            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
            result.Append("However your password can be changed at anytime by editing in the link below.");
            result.Append("</td>");
            result.Append("</tr>");

            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
            

            result.Append("<tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
            result.Append(Link);
            result.Append("</td>");
            result.Append("</tr>");

            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
            result.Append("<tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
            result.Append("Keep up to date with our activities");
            result.Append("</td>");
            result.Append("</tr>");

            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");

            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
            result.Append("<tr>");
            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
            result.Append("Thank you for your participation.");
            result.Append("</td>");
            result.Append("</tr>");
            

            return result.ToString();
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



                    result.Append("<tr> ");
                    result.Append("<td  colspan=\"2\" height=\"20\"></td>");
                    result.Append("</tr>");
                    result.Append("<tr>");
                    result.Append("<td rowspan=\"5\" align=\"center\">");
                    result.Append("<table cellpadding=\"0\" cellspacing=\"0\" style=\"width:90px;height:80px;\">");
                    result.Append("<tr>");
                    result.Append("<td width=\"90\" height=\"80\" align=\"center\" style=\"padding:5px; margin:0px;background-color:#2d96e2;\"><img src=\"http://www.booking2hotels.com" + cFrontproductDetail.Thumbnail + "\" alt=\"\" width=\"80\" height=\"70\" /></td>");
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
                    result.Append("<a href=\"" + REviewProductCatLink(BookingProductITem.ProductCategory, BookingProductITem.ProductID) + "\">");
                    result.Append("<img src=\"http://www.booking2hotels.com/images_mail/" + Btn + "\" alt=\"\" style=\" border:0px; width:116px; height:23px;\" />");
                    result.Append("</a>");
                    result.Append("</td>");
                    result.Append("</tr>");
                    result.Append("<tr> <td  height=\"7\"> </td></tr>");
                    result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma; font-size:11px; font-style:italic; color:#898b90; \">**" + txt3 + "</td>");
                    result.Append("</tr>");

                    result.Append("<tr><td style=\"font-family:Tahoma; font-size:12px; color:#898b90;\">" + REviewProductCatLink(BookingProductITem.ProductCategory, BookingProductITem.ProductID) + "</td></tr>");
                    result.Append("<tr> <td  height=\"20\"> </td></tr>");
                    result.Append("<tr> <td   colspan=\"2\" align=\"center\" ><img src=\"http://www.booking2hotels.com/images_mail/line_review.jpg\" alt=\"\" width=\"575\" height=\"25\" /> </td></tr>");
                }
            }

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
                    txt4 = "Click Voucher";
                    txt5 = "Click Map";
                    break;
                case 2:
                    txt1 = "กรุณาคลิกลิงค์นี้เพื่อปรินท์เอกสารยืนยันการจองห้องพัก";
                    txt2 = "คลิกเพื่อดูแผนที่โรงแรม";
                    txt3 = "หากท่านไม่สามารถคลิกลิงค์ได้ กรุณาคัดลอก URL นี้แล้วนำไปเปิดในช่อง Browser ของท่านค่ะ";
                    txt4 = "เอกสารยืนยันการจอง คลิ๊กที่นี่";
                    txt5 = "แผนที่ คลิ๊กที่นี่";
                    break;
            }
            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
            result.Append("<tr>");
            result.Append("<td colspan=\"2\" style=\"margin:0px;padding:0px; font-size:11px;color:#63a614;\">Print hotel voucher and map</td>");
            result.Append("</tr>");
            result.Append("<tr>");
            result.Append("<td colspan=\"2\" tyle=\"margin:0px;padding:0px;\" align=\"center\"><img src=\"http://www.booking2hotels.com/images_mail/line-voucher.jpg\" width=\"639\" height=\"25\" alt=\"\" /></td>");
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
                result.Append("<td valign=\"middle\" rowspan=\"2\" align=\"center\" style=\" width:110px;\"><a href=\"" + _mainsite + "Voucher.aspx?id=" + EncodeId(BookingProductITem.BookingProductId) + "\" ><img src=\"http://www.booking2hotels.com/images_mail/hotels2voucher.jpg\" style=\"border:0px;\" alt=\"\"  width=\"73\" height=\"39\" /></a></td>");
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
                    result.Append("<td valign=\"middle\" rowspan=\"2\" align=\"center\" style=\" width:110px;color:#6d6e71;\"><a href=\"" + MapProductCatLink(BookingProductITem.ProductCategory, BookingProductITem.ProductID) + "\" ><img src=\"http://www.booking2hotels.com/images_mail/mail_map.jpg\" style=\"border:0px;\" alt=\"\"  width=\"40\" height=\"36\" /></a></td>");
                    result.Append("<td style=\"color:#6d6e71;font-family:Tahoma; font-size:11px; font-style:italic;\">");
                    result.Append("<span style=\"font-size:14px;color:#63a614;font-style:normal;\"><a href=\"" + MapProductCatLink(BookingProductITem.ProductCategory, BookingProductITem.ProductID) + "\" >" + txt5 + "</a></span> **" + txt3);
                    result.Append("</td>");
                    result.Append("</tr>");
                    result.Append("<tr>");
                    result.Append("<td style=\"color:#6d6e71;font-family:Tahoma; font-size:11px;\">" +MapProductCatLink(BookingProductITem.ProductCategory, BookingProductITem.ProductID) + "</td>");
                    result.Append("</tr>");
                }
                
                result.Append("<tr>");
                result.Append("<td colspan=\"2\" align=\"center\"><img src=\"http://www.booking2hotels.com/images_mail/line-voucher.jpg\" width=\"639\" height=\"25\" alt=\"\" /></td>");
                result.Append("</tr>");
                result.Append("</table>");
                result.Append("</td>");
                result.Append("</tr>");
                result.Append("<tr><td style=\"height:20px;\" height=\"20\"></td></tr>");

            }

        return result.ToString();
        }


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
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("Thank you for making the reservation at " + this.cClassBookingProduct.ProductTitle + ". This is to inform that your reservation request is AVAILABLE <span style=\"color:#000\"> (#Booking ID : " + this.BookingHotelId + ")</span> but your payment has not been paid. In order to process your booking completely, please click the “Make Payment” button below to complete your payment thru our high security online system.");
                            result.Append("</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("The availability bases on First Come First Serve. We are unable to secure your reservation unless the payment is paid in full successfully.");
                            result.Append("</td>");
                            result.Append("</tr>");
                           
                            
                            break;
                        case 2:
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("เว็บไซด์(<a href=\"" + this.cProductBookingEngine.WebsiteName + "\"> " + this.GetDomainName + " </a>)");
                            result.Append("ขอขอบพระคุณที่ใช้บริการจองกับเรา</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
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
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("Thank you very much for making the reservation with <a href=\""+this.cProductBookingEngine.WebsiteName+"\"> " +this.GetDomainName  +"</a>");
                            result.Append("</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">We have received your booking at " + this.cClassBookingProduct.ProductTitle + ". Your #Booking ID : " + this.BookingHotelId + ". Our staff is checking room availability and will contact you in 12 hours.");
                            result.Append(" </td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("<strong style=\"font-size:14px;\">Reminder:&nbsp;<span style=\"color:#b50c04;text-decoration:underline;\">This letter is NOT a confirmation.  It can not be used for checking in.</span></strong>");
                            result.Append(" </td>");
                            result.Append("</tr>");

                            break;
                        case 2:
                            result.Append("<tr>");
                            result.Append("<td  style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("เว็บไซด์(<a href=\"" + this.cProductBookingEngine.WebsiteName + "\"> " + this.GetDomainName + "</a>)");
                            result.Append(" ขอขอบพระคุณที่ใช้บริการจองกับเรา");
                            result.Append("</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");

                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            switch (this.cClassBookingProduct.ProductCategory)
                            {
                                case 29:
                                    result.Append("เราได้รับข้อมูลการจองของท่านเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการชำระเงินของท่านกับเราเท่านั้น<strong> ยังมิใช่เอกสารยืนยันการจองห้องพัก (Hotel Voucher)</strong>");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะห้องว่างที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 12</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    break;
                                case 32:
                                    result.Append("เราได้รับข้อมูลการจองของท่านพร้อมทั้งการชำระเงินผ่านระบบเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการชำระเงินของท่านกับเราเท่านั้น <strong> ยังมิใช่เอกสารยืนยันการจองสนามกอล์ฟ (Voucher)</strong> ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะการจองสนามกอล์ฟที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 12</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    break;
                                case 40:
                                    result.Append("เราได้รับข้อมูลการจองของท่านพร้อมทั้งการชำระเงินผ่านระบบเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการชำระเงินของท่านกับเราเท่านั้น <strong> ยังมิใช่เอกสารยืนยันการจองโปรแกรมสปา (Voucher)</strong> ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะโปรแกรมสปาที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 12</strong> ชั่วโมงหลังจากทำการจองค่ะ");
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
                                    result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะบริการที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 12</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    break;
                            }
                            
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("คุณสามารถใช้หมายเลขการจอง <strong> #Booking ID : " + this.BookingHotelId + "</strong> นี้ในการตรวจสอบสถานะบุคกิ้งของคุณกับเราค่ะ");
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
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("We would like to thank you for making the reservation. We are pleased to inform you that your booking at " + this.cClassBookingProduct.ProductTitle + " #Booking ID : " + this.BookingHotelId + " is <span style=\"color:#bf0000;font-weight:bold;\">CONFIRMED.</span> </a>");
                           
                            result.Append("</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                             result.Append("<tr>");
                             result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("Your payment is paid in full. Please see your booking detail below.");
                            result.Append("</td>");
                            result.Append("</tr>");
                            break;
                        case 2:

                            string txtLast = string.Empty;
                            
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("เวปไซต์ <a href=\""+this.cProductBookingEngine.WebsiteName+"\">"+this.GetDomainName+"</a> ขอขอบพระคุณที่ใช้บริการจองกับเรา โปรดปรินท์เอกสารยืนยันนี้และแสดงต่อเจ้าหน้าที่ในวันที่ใช้บริการค่ะ");
                            
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
                        result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                        result.Append("Thank you for choosing " + this.cClassBookingProduct.ProductTitle +"");
                        result.Append("</td>");
                        result.Append("</tr>");
                        result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                        result.Append("<tr>");
                        result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                        result.Append("As valued customer, your opinions on all aspects of your stay are very important to us. We would be grateful if you would complete this questionnaire and send us back in order to improve our service to all customer's satisfaction.</a>");
                      result.Append("</td>");
                        result.Append("</tr>");
                             result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("Please note that you do not have to answer all the questions. We will use our effort to serve you the best service.");
                            result.Append("</td>");
                        result.Append("</tr>");
                            break;
                        case 2:
                         result.Append("<tr>");
                         result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                         result.Append("เว็บไซด์(<a href=\"" + this.cProductBookingEngine.WebsiteName + "\"> " + this.GetDomainName + "</a> ขอขอบพระคุณที่ใช้บริการจองกับเรา");
                         
                         result.Append("</td>");
                         result.Append("</tr>");
                         result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                        result.Append("<tr>");
                        result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                        result.Append("ความคิดเห็นของท่านมีค่ากับเราเสมอ กรุณาแสดงความคิดเห็นเกี่ยวกับโรงแรมที่พักและการบริการให้กับเราเพื่อการปรับปรุงข้อบกพร่องและเพื่อเพิ่มความพึงพอใจของท่านในโอกาสต่อๆไปด้วยค่ะ");
                        result.Append("</td>");
                        result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                        result.Append("ท่านไม่จำเป็นต้องตอบทุกคำถามค่ะ เราจะพยายามปรับปรุงการบริการของเราให้ดีที่สุดค่ะ");
                            result.Append("</td>");
                        result.Append("</tr>");
                            
                            break;
                    }
                    

                    break;
                //Receive Mail Book Now
                case 5 :
                    

                    break;
                // Voucher book now
                case 6 :
                   

                    break;
                case 7 :
                    result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Greeting from www.hotels2thailand.com ");
                    result.Append("</td>");
                    result.Append("</tr>");

                     result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Thank you for choosing  <a href=\"http://www.booking2hotels.com\"> www.hotels2thailand.com </a>&nbsp; <strong> “Book Now Pay Later” </strong> ");
                    result.Append("</td>");
                    result.Append("</tr>");

                    result.Append("<tr>");
                    result.Append("<td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71; \">");
                    result.Append("Please be informed that your #Booking ID : " + this.BookingHotelId + "  is <strong> CONFIRMED.The total cost is charged successfully.</strong>");
                    //result.Append("<span style=\"color:#000\">ORDER ID: " + this.BookingId + " </span>");
                    result.Append("</td>");
                    result.Append("</tr>");
                    break;
                    //Online Charge
                case 8 :
                     

                    break;
                    //Cancellation
                case 10:
                     
                    break;
                    //Fully book And offer new product
                case 11:
                   
                    break;
                    // offine charge
                case 12:
                    

                    
                    break;
                //Receive Mail Bank Transfer
                case 13:

                    
                    break;

                //Receive Mail Notic To Hotel
                case 14:
                     result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("Thank you very much for choosing Booking2Hotels.com");
                            result.Append("</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">You receive a booking with following detail.(#Booking ID: " + this.BookingHotelId + ")");
                            result.Append(" </td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\"><a href=\"http://manage.booking2hotels.com/extranet/login.aspx\"/>Click here for login to Booking2hotels.com System </a> ");
                            result.Append(" </td>");
                            result.Append("</tr>");
                    break;
                    
                    // Recieve Mail Allotment
                case 15 :

                    switch (this.BookingLangId)
                    {
                        case 1:
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("Thank you very much for making the reservation with <a href=\""+this.cProductBookingEngine.WebsiteName+"\"> " +this.GetDomainName  +"</a>");
                            result.Append("</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">We have received your payment at " + this.cClassBookingProduct.ProductTitle + ". Your #Booking ID : " + this.BookingHotelId + " and <strong>the payment is paid in full. </strong>Our staff is checking room availability and will contact you in 12 hours.");
                            result.Append(" </td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("<strong style=\"font-size:14px;\">Reminder:&nbsp;<span style=\"color:#b50c04;text-decoration:underline;\">This letter is NOT a confirmation.  It can not be used for checking in.</span></strong>");
                            result.Append(" </td>");
                            result.Append("</tr>");

                            break;
                        case 2:
                            result.Append("<tr>");
                            result.Append("<td  style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("เว็บไซด์(<a href=\"" + this.cProductBookingEngine.WebsiteName + "\"> " + this.GetDomainName + "</a>)");
                            result.Append(" ขอขอบพระคุณที่ใช้บริการจองกับเรา");
                            result.Append("</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");

                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            switch (this.cClassBookingProduct.ProductCategory)
                            {
                                case 29:
                                    result.Append("เราได้รับข้อมูลการชำระเงินผ่านระบบเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการชำระเงินของท่านกับเราเท่านั้น<strong> ยังมิใช่เอกสารยืนยันการจองห้องพัก (Hotel Voucher)</strong>");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะห้องว่างที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 12</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    break;
                                case 32:
                                    result.Append("เราได้รับข้อมูลการจองของท่านพร้อมทั้งการชำระเงินผ่านระบบเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการชำระเงินของท่านกับเราเท่านั้น <strong> ยังมิใช่เอกสารยืนยันการจองสนามกอล์ฟ (Voucher)</strong> ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะการจองสนามกอล์ฟที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 12</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    break;
                                case 40:
                                    result.Append("เราได้รับข้อมูลการจองของท่านพร้อมทั้งการชำระเงินผ่านระบบเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการชำระเงินของท่านกับเราเท่านั้น <strong> ยังมิใช่เอกสารยืนยันการจองโปรแกรมสปา (Voucher)</strong> ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะโปรแกรมสปาที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 12</strong> ชั่วโมงหลังจากทำการจองค่ะ");
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
                                    result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะบริการที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 12</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    break;
                            }
                            
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("คุณสามารถใช้หมายเลขการจอง <strong> #Booking ID : " + this.BookingHotelId + "</strong> นี้ในการตรวจสอบสถานะบุคกิ้งของคุณกับเราค่ะ");
                            result.Append("</td>");
                            result.Append("</tr>");
                            break;
                    }
                     
                    break;
                //Receive Mail Notic To Hotel Offline Charge
                case 16:
                    result.Append("<tr>");
                    result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                    result.Append("Thank you very much for choosing Booking2Hotels.com");
                    result.Append("</td>");
                    result.Append("</tr>");
                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                    result.Append("<tr>");
                    result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">You receive offline charge booking with following detail.(#Booking ID: " + this.BookingHotelId + ")");
                    result.Append(" </td>");
                    result.Append("</tr>");
                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                    result.Append("<tr>");
                    result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\"><!--##@WordingCardEndcode##-->");
                    result.Append(" </td>");
                    result.Append("</tr>");
                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                    result.Append("<tr>");
                    result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\"><a href=\"http://manage.booking2hotels.com/extranet/login.aspx\"/>Click here for login to Booking2hotels.com System </a> ");
                    result.Append(" </td>");
                   result.Append("</tr>");
                    break;

                // Recieve Mail Allotment Booking Now offline
                case 17:

                    switch (this.BookingLangId)
                    {
                        case 1:
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("Thank you very much for making the reservation with <a href=\"" + this.cProductBookingEngine.WebsiteName + "\"> " + this.GetDomainName + "</a>");
                            result.Append("</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">We have received your payment at " + this.cClassBookingProduct.ProductTitle + ". Your #Booking ID : " + this.BookingHotelId + " Our staff is checking room availability and will contact you in 12 hours.");
                            result.Append(" </td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("<strong style=\"font-size:14px;\">Reminder:&nbsp;<span style=\"color:#b50c04;text-decoration:underline;\">This letter is NOT a confirmation.  It can not be used for checking in.</span></strong>");
                            result.Append(" </td>");
                            result.Append("</tr>");

                            break;
                        case 2:
                            result.Append("<tr>");
                            result.Append("<td  style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("เว็บไซด์(<a href=\"" + this.cProductBookingEngine.WebsiteName + "\"> " + this.GetDomainName + "</a>)");
                            result.Append(" ขอขอบพระคุณที่ใช้บริการจองกับเรา");
                            result.Append("</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");

                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            switch (this.cClassBookingProduct.ProductCategory)
                            {
                                case 29:
                                    result.Append("เราได้รับข้อมูลการชำระเงินผ่านระบบเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการชำระเงินของท่านกับเราเท่านั้น<strong> ยังมิใช่เอกสารยืนยันการจองห้องพัก (Hotel Voucher)</strong>");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะห้องว่างที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 12</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    break;
                                case 32:
                                    result.Append("เราได้รับข้อมูลการจองของท่านพร้อมทั้งการชำระเงินผ่านระบบเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการชำระเงินของท่านกับเราเท่านั้น <strong> ยังมิใช่เอกสารยืนยันการจองสนามกอล์ฟ (Voucher)</strong> ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะการจองสนามกอล์ฟที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 12</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    break;
                                case 40:
                                    result.Append("เราได้รับข้อมูลการจองของท่านพร้อมทั้งการชำระเงินผ่านระบบเป็นที่เรียบร้อยแล้วตามรายละเอียดด้านล่างนี้ และทางเวปไซต์โฮเทลทูฯขอเรียนให้ทราบว่าจดหมายฉบับนี้เป็นเพียงจดหมายยืนยันการชำระเงินของท่านกับเราเท่านั้น <strong> ยังมิใช่เอกสารยืนยันการจองโปรแกรมสปา (Voucher)</strong> ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                                    result.Append("<tr>");
                                    result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะโปรแกรมสปาที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 12</strong> ชั่วโมงหลังจากทำการจองค่ะ");
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
                                    result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                                    result.Append("เราจะรีบดำเนินการตรวจสอบสถานะบริการที่คุณจองไว้กับเราโดยเร็วที่สุดและท่านจะได้รับการติดต่อกลับจากเจ้าหน้าที่โฮเทลทูฯภายในเวลา<strong> 12</strong> ชั่วโมงหลังจากทำการจองค่ะ");
                                    result.Append("</td>");
                                    result.Append("</tr>");
                                    break;
                            }

                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("คุณสามารถใช้หมายเลขการจอง <strong> #Booking ID : " + this.BookingHotelId + "</strong> นี้ในการตรวจสอบสถานะบุคกิ้งของคุณกับเราค่ะ");
                            result.Append("</td>");
                            result.Append("</tr>");
                            break;
                    }

                    break;
                // Mail Send Voucher & Map & Recept Booknow Offline
                case 18:
                    switch (this.BookingLangId)
                    {
                        case 1:
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("We would like to thank you for making the reservation. We are pleased to inform you that your booking at " + this.cClassBookingProduct.ProductTitle + " #Booking ID : " + this.BookingHotelId + " is <span style=\"color:#bf0000;font-weight:bold;\">CONFIRMED.</span> </a>");

                            result.Append("</td>");
                            result.Append("</tr>");
                            result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("You can pay the booking at hotel. Please see your booking detail below.");
                            result.Append("</td>");
                            result.Append("</tr>");
                            break;
                        case 2:

                            string txtLast = string.Empty;

                            result.Append("<tr>");
                            result.Append("<td style=\"margin:0px; padding:0px;font-family:Tahoma;\">");
                            result.Append("เวปไซต์ <a href=\"" + this.cProductBookingEngine.WebsiteName + "\">" + this.GetDomainName + "</a> ขอขอบพระคุณที่ใช้บริการจองกับเรา โปรดปรินท์เอกสารยืนยันนี้และแสดงต่อเจ้าหน้าที่ในวันที่ใช้บริการค่ะ");

                            result.Append("</td>");
                            result.Append("</tr>");

                            break;
                    }

                    break;
            }

          
            return result.ToString();
        }

        protected string GrandTotal(string pricetype = "")
        {
            BookingTotalAndBalance cBookingtotalPrice = new BookingTotalAndBalance();
            
            string result = "0";
            decimal Total = cBookingtotalPrice.CalcullatePriceTotalByBookingId(this.BookingId).SumPrice;
            //int dd = 0;
            //dd =  ((this.DateSubmitBooking < new DateTime(2013,11,15,19,00,00) )? 0:9);
            if (!string.IsNullOrEmpty(pricetype) && pricetype == "supplier_price_show")
                Total = Total *  ( (this.DateSubmitBooking < new DateTime(2013,11,15,19,00,00) )? (decimal)(0.9):(decimal)(0.85));
            result = Total.Hotels2Currency();
            //result = cBookingDetailDisplay.GetPriceTotalByBookingId(this.BookingId).Hotels2Currency();

            return result.ToString();
        }


        protected string GrandPaidTotal(string pricetype="")
        {
            BookingTotalAndBalance cBookingtotalPrice = new BookingTotalAndBalance();
            
            string result = "0";
            decimal Total = cBookingtotalPrice.GetPriceTotalPaidByBookingId(this.BookingId);
            if (!string.IsNullOrEmpty(pricetype) && pricetype == "supplier_price_show")
            {

                ProductBookingEngine bEngine = this.cProductBookingEngine;

                if (bEngine.ProductID == 3605)
                {
                    Total = Total * ((this.DateSubmitBooking < new DateTime(2013, 11, 15, 19, 00, 00)) ? (decimal)(0.9) : (decimal)(0.85));
                    //break;
                }
                if (bEngine.ProductID == 3692)
                {
                    Total = Total * (decimal)(0.9);
                    //break;
                }

                //if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["hotel_supplier_showprice_mail"]))
                //{
                //    string ProductCal = ConfigurationManager.AppSettings["hotel_supplier_showprice_mail"].ToString();
                //    string[] arrProduct = ProductCal.Split(',');
                //    foreach (string pid in arrProduct)
                //    {
                        
                //    }
                    
                //}
              
            }
            
            result = Total.Hotels2Currency();
            //result = cBookingDetailDisplay.GetPriceTotalByBookingId(this.BookingId).Hotels2Currency();

            return result.ToString();
        }

        protected string GrandBalance(string pricetype = "")
        {
            BookingTotalAndBalance cBookingtotalPrice = new BookingTotalAndBalance();
            
            string result = "0";
            decimal Total = cBookingtotalPrice.getbalanceByBookingId(this.BookingId);
            decimal decPrice = Total;
            if (!string.IsNullOrEmpty(pricetype) && pricetype == "supplier_price_show")
                Total = Total * ((this.DateSubmitBooking < new DateTime(2013, 11, 15, 19, 00, 00)) ? (decimal)(0.9) : (decimal)(0.85));
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
                result = cBookingDetailDisplay.FullName;
            else
                result = cBookingDetailDisplay.PrefixTitle + " " + cBookingDetailDisplay.FullName;

            return result.ToString();
        }

        protected string CusName_member(int CusId)
        {
            Customer cCustomer = new Customer();
            cCustomer = cCustomer.GetCustomerbyId(CusId);

            string Prefix = "";
            if (cCustomer.PrefixID.HasValue)
            {
                PrefixName cPrefix = new PrefixName();
                Prefix = cPrefix.GetPrefixById((byte)cCustomer.PrefixID).Title;
            }
            
            string result = "";
            if (cCustomer.PrefixID == 1)
                result = cCustomer.FullName;
            else
                result = Prefix + " " + cCustomer.FullName;

            return result.ToString();
        }
    }
}