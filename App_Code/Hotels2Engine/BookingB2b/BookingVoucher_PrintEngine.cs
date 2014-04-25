using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Linq;
using System.Collections;
using System.Xml;
using System.Text;
using Hotels2thailand.Front;

using Hotels2thailand;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Booking;
using Hotels2thailand.Production;

/// <summary>
/// Summary description for Voucher
/// </summary>
/// 
namespace Hotels2thailand.BookingB2b
{
    public partial class BookingVoucher_PrintEngineB2b : BookingPrintAndVoucher_Helper
    {
        public int BookingProductId { get; set; }

        public BookingVoucher_PrintEngineB2b(int intBookingProductId)
        {
            this.BookingProductId = intBookingProductId;
            
        }

        //private string _mainsite = "http://www.hotels2thailand.com/";
        protected string getTheme(byte bytLandId)
        {
            string Theme_file = "Template_voucherPrint_en.html";
            if (bytLandId == 2)
                Theme_file = "Template_voucherPrint_th.html";

            StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath("~/admin/booking/BookingPrintAndMail_Template_B2b/" + Theme_file + ""));
            string read = objReader.ReadToEnd();
            objReader.Close();
            objReader.Dispose();
            return read;
        }
       
        
        public string getVoucher()
        {
            
            BookingProductDisplay cBookingPLsit = new BookingProductDisplay();
            cBookingPLsit = cBookingPLsit.getBookingProductDisplayByBookingProductId(this.BookingProductId);

            byte bytBookingLang = cBookingPLsit.BookingLang;
            string ResultVoucher = getTheme(bytBookingLang);

            

            if (cBookingPLsit.BookingPaymentId != 2)
            {
                string KeywordItemListHOtel = Utility.GetKeywordReplace(ResultVoucher, "<!--##@voucherPrint_Booknow_Confirm100Start##-->", "<!--##@voucherPrint_Booknow_Confirm100End##-->");
                ResultVoucher = ResultVoucher.Replace(KeywordItemListHOtel, " ");
            }

            //Replace REmark Show Only
            if (cBookingPLsit.ProductCategory == 38)
            {
                

                switch (bytBookingLang)
                {
                    case 1:
                        ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_ShowRemark##-->", "<p class=\"note\"><span>*Note</span> Please print this voucher to get live ticket at ticket station.</p><p class=\"note\"><span>*Note</span> In any case of your booking isn’t matched with reality when you show upfront (such as child’s age, child’s height, the quantity of people and etc), you are responsible to pay the additional charge upfront the show/tour. Hotels2thailand.com reserves the right to non-refund if the booking is mistaken reserved by guest. </p>");
                        //ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_ShowRemark##-->", "");
                        //ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_ShowRemark##-->", "<p class=\"note\"><span>*Note</span> Voucher is requested to print out to redeem your real ticket at ticket box in front of the show.</p>");
                       
                        break;
                    case 2:
                        ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_ShowRemark##-->", "<p class=\"note\"><span>*Note</span> เพื่อความสะดวกในการเข้าชม กรุณาปรินท์เอกสารยืนยัน (Voucher) นี้นำไปแสดงต่อเจ้าหน้าที่ที่ห้องขายบัตรเพื่อแลกรับบัตรเข้าชมการแสดงตัวจริง</p><p class=\"note\"><span>*Note</span>หากเจ้าหน้าที่ ณ ห้องขายบัตรพบว่าบุกกิ้งที่ท่านจองกับเวปไซต์โฮเทลทูไทยแลนด์ไม่ตรงตามรายละเอียดในวันที่ท่านไปใช้บริการจริง อาทิ จำนวนผู้เข้าชม และ/หรืออายุของเด็ก และ/หรือความสูงของเด็ก ทางเวปไซต์โฮเทลทูไทยแลนด์ของสงวนสิทธิ์ให้เจ้าหน้าที่ ณ ห้องขายบัตรสามารถเรียกเก็บเงินส่วนต่างกับท่านได้ทันทีโดยท่านจะถูกเรียกเก็บส่วนต่างตามราคาจริงของผู้ประกอบการนั้นๆ ( Walk in rate)</p>");
                        //ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_ShowRemark##-->", "");

                        //ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_ShowRemark##-->", "<p class=\"note\"><span>*Note</span> กรุณาปริ้นท์เอกสารยืนยันการจองนี้ยื่นให้กับเจ้าหน้าที่บริเวณห้องขายบัตรเพื่อรับบัตรเข้าชมการแสดงจริง</p>");
                        break;
                }
            }

            //Replace REmark Show Only
            if (cBookingPLsit.ProductCategory == 38 || cBookingPLsit.ProductCategory == 34 || cBookingPLsit.ProductCategory == 36 )
            {

                switch (bytBookingLang)
                {
                    case 1:
                        ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_ShowRemark##-->", "<p class=\"note\"><span>*Note</span> In any case of your booking isn’t matched with reality when you show upfront (such as child’s age, child’s height, the quantity of people and etc), you are responsible to pay the additional charge upfront the show/tour. Hotels2thailand.com reserves the right to non-refund if the booking is mistaken reserved by guest. </p>");
                        break;
                    case 2:
                        ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_ShowRemark##-->", "<p class=\"note\"><span>*Note</span>หากเจ้าหน้าที่ ณ ห้องขายบัตรพบว่าบุกกิ้งที่ท่านจองกับเวปไซต์โฮเทลทูไทยแลนด์ไม่ตรงตามรายละเอียดในวันที่ท่านไปใช้บริการจริง อาทิ จำนวนผู้เข้าชม และ/หรืออายุของเด็ก และ/หรือความสูงของเด็ก ทางเวปไซต์โฮเทลทูไทยแลนด์ของสงวนสิทธิ์ให้เจ้าหน้าที่ ณ ห้องขายบัตรสามารถเรียกเก็บเงินส่วนต่างกับท่านได้ทันทีโดยท่านจะถูกเรียกเก็บส่วนต่างตามราคาจริงของผู้ประกอบการนั้นๆ ( Walk in rate)</p>");
                        break;
                }
            }


            string BookingDetail = string.Empty;
            string them_file_item = string.Empty;
            string strCustomerName = string.Empty;
            switch (cBookingPLsit.ProductCategory)
            {
                    //Hotel
                case 29:

                    switch (bytBookingLang)
                    {
                        case 1:
                            them_file_item = "tbookingVoucher_Bookingdetail_hotel_en.html";
                            break;
                        case 2:
                            them_file_item = "tbookingVoucher_Bookingdetail_hotel_th.html";
                            break;
                    }

                    StreamReader objHotelDetailReaderHotel = new StreamReader(HttpContext.Current.Server.MapPath("~/admin/booking/BookingPrintAndMail_Template_B2b/" + them_file_item + ""));


                    BookingDetail = objHotelDetailReaderHotel.ReadToEnd();
                    objHotelDetailReaderHotel.Close();
                    objHotelDetailReaderHotel.Dispose();

                  
                   
                    //Booking Name And Prefix
                    if (cBookingPLsit.PrefixId == 1)
                        strCustomerName = cBookingPLsit.BookingName ;
                    else
                        strCustomerName = cBookingPLsit.Prefixtitle + " " + cBookingPLsit.BookingName;


                    strCustomerName = strCustomerName + "/" + cBookingPLsit.CountryTitle;


                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingName##-->",strCustomerName);

                    //BookingCountry
                    //BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingCountry##-->", cBookingPLsit.CountryTitle);

                    //CheckIn
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingCheckIn##-->", DateTimeCheck(cBookingPLsit.DateTimeCheckIn, bytBookingLang));
                    //CheckOut
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingCheckOut##-->", DateTimeCheck(cBookingPLsit.DateTimeCheckOut, bytBookingLang));

                    //NumAult
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingNumAdult##-->", cBookingPLsit.NumAdult.ToString());
                    //Num Child
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingNumChild##-->", cBookingPLsit.NumChild.ToString());

                    //string KeywordNumGuest = Utility.GetKeywordReplace(BookingDetail, "<!--##@voucherPrint_BookingNumGuestStart##-->", "<!--##@voucherPrint_BookingNumGuestEnd##-->");
                    //BookingDetail = BookingDetail.Replace(KeywordNumGuest, GetNumGestString(cBookingPLsit.ProductCategory, cBookingPLsit.NumAdult, cBookingPLsit.NumChild, cBookingPLsit.NunGolf));
                    //ItemList

                    //if (!string.IsNullOrEmpty(cBookingPLsit.SupplierRefNumber))
                    //{
                    //    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingRef##-->", "<p class=\"ref\"><span>Bedsonline Reference Number</span> " + cBookingPLsit.SupplierRefNumber + "</p>");
                       

                    //    //voucherPrint_cssAdjust
                    //}
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_cssAdjust##-->", "style=\"margin:0px 30px 0 0;\"");
                    //

                    string KeywordItemListHOtel = Utility.GetKeywordReplace(BookingDetail, "<!--##@voucherPrint_Items_Start##-->", "<!--##@voucherPrint_Items_End##-->");
                    BookingDetail = BookingDetail.Replace(KeywordItemListHOtel, GetProductItemHotel(this.BookingProductId, DateTimeNightTotal(cBookingPLsit.DateTimeCheckIn, cBookingPLsit.DateTimeCheckOut), bytBookingLang));


                    //ItemExptra
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_Items_Extra##-->", GetProductItemExtra(this.BookingProductId, bytBookingLang));
                    //Note thai establishment
                    ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_Establishment##-->", "โรงแรม");


                    // 1828 Louis Tralvel
                    if (cBookingPLsit.ProductID == 1828)
                    {
                        //special remark punctual
                        ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_WordingSpecial##-->", GetWordingSpecial(bytBookingLang));

                    }

                    

                    break;

                    // Golf
                case 32:

                    switch (bytBookingLang)
                    {
                        case 1:
                            them_file_item = "tbookingVoucher_Bookingdetail_golf_en.html";
                            break;
                        case 2:
                            them_file_item = "tbookingVoucher_Bookingdetail_golf_th.html";
                            break;
                    }

                    //IssueDate
                    //ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_IssueDAte##-->", DateTime.Now.Hotels2ThaiDateTime().ToString("MMM dd, yyyy"));

                    StreamReader objHotelDetailReaderGolf = new StreamReader(HttpContext.Current.Server.MapPath("~/admin/booking/BookingPrintAndMail_Template_B2b/" + them_file_item + ""));
                    BookingDetail = objHotelDetailReaderGolf.ReadToEnd();
                    objHotelDetailReaderGolf.Close();
                    objHotelDetailReaderGolf.Dispose();

               
                  
                    //Booking Name And Prefix
                    if (cBookingPLsit.PrefixId == 1)
                        strCustomerName = cBookingPLsit.BookingName ;
                    else
                        strCustomerName = cBookingPLsit.Prefixtitle + " " + cBookingPLsit.BookingName;


                    strCustomerName = strCustomerName + "/" + cBookingPLsit.CountryTitle;


                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingName##-->",strCustomerName);


                    //BookingCountry
                    //BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingCountry##-->", cBookingPLsit.CountryTitle);

                    //Tee-Off Time
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingTimeDateConfirme##-->", DateTimeChekFullDateAndTime(cBookingPLsit.DateTimeCheckIn, cBookingPLsit.DateTimeConfirmCheckIn, bytBookingLang));
                 

                    //NumGolfer
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingNumGolfer##-->", cBookingPLsit.NumAdult.ToString());
                    
                    //ItemList
                    string KeywordItemListGolf = Utility.GetKeywordReplace(BookingDetail, "<!--##@voucherPrint_Items_Start##-->", "<!--##@voucherPrint_Items_End##-->");
                    BookingDetail = BookingDetail.Replace(KeywordItemListGolf, GetProductItem_Golf(this.BookingProductId, DateTimeNightTotal(cBookingPLsit.DateTimeCheckIn, cBookingPLsit.DateTimeCheckOut), bytBookingLang));

                    //Note thai establishment
                    ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_Establishment##-->", "สนามกอล์ฟ");
                    break;
                // Spa
                case 40:
                    ////IssueDate
                    //ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_IssueDAte##-->", DateTime.Now.Hotels2ThaiDateTime().ToString("MMM dd, yyyy"));
                    switch (bytBookingLang)
                    {
                        case 1:
                            them_file_item = "tbookingVoucher_Bookingdetail_spa_en.html";
                            break;
                        case 2:
                            them_file_item = "tbookingVoucher_Bookingdetail_spa_th.html";
                            break;
                    }

                    StreamReader objHotelDetailReader_Spa = new StreamReader(HttpContext.Current.Server.MapPath("~/admin/booking/BookingPrintAndMail_Template_B2b/" + them_file_item + ""));

                    BookingDetail = objHotelDetailReader_Spa.ReadToEnd();
                    objHotelDetailReader_Spa.Close();
                    objHotelDetailReader_Spa.Dispose();

           
                    
                    //Booking Name And Prefix
                    if (cBookingPLsit.PrefixId == 1)
                        strCustomerName = cBookingPLsit.BookingName ;
                    else
                        strCustomerName = cBookingPLsit.Prefixtitle + " " + cBookingPLsit.BookingName;


                    strCustomerName = strCustomerName + "/" + cBookingPLsit.CountryTitle;


                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingName##-->",strCustomerName);


                    //BookingCountry
                    //BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingCountry##-->", cBookingPLsit.CountryTitle);

                    //Service Date
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingspaDateService##-->", DateTimeCheck(cBookingPLsit.DateTimeCheckIn, cBookingPLsit.DateTimeConfirmCheckIn, bytBookingLang));
                    //Service Time
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingspaTimeService##-->", DateTimeCheck_TimeOnly(cBookingPLsit.DateTimeCheckIn, cBookingPLsit.DateTimeConfirmCheckIn, bytBookingLang));


                    //ItemList
                    string KeywordItemList_Spa = Utility.GetKeywordReplace(BookingDetail, "<!--##@voucherPrint_Items_Start##-->", "<!--##@voucherPrint_Items_End##-->");
                    BookingDetail = BookingDetail.Replace(KeywordItemList_Spa, GetProductItem_Spa(this.BookingProductId, DateTimeNightTotal(cBookingPLsit.DateTimeCheckIn, cBookingPLsit.DateTimeCheckOut), bytBookingLang));
                    //Note thai establishment
                    ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_Establishment##-->", "สถานประกอบการ");
                    break;
                // Transfer
                case 31:
                    //IssueDate
                    //ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_IssueDAte##-->", DateTime.Now.Hotels2ThaiDateTime().ToString("MMM dd, yyyy"));
                    switch (bytBookingLang)
                    {
                        case 1:
                            them_file_item = "tbookingVoucher_Bookingdetail_transfer_en.html";
                            break;
                        case 2:
                            them_file_item = "tbookingVoucher_Bookingdetail_transfer_th.html";
                            break;
                    }
                    StreamReader objHotelDetailReader_transfer = new StreamReader(HttpContext.Current.Server.MapPath("~/admin/booking/BookingPrintAndMail_Template_B2b/"+them_file_item+""));
                    BookingDetail = objHotelDetailReader_transfer.ReadToEnd();
                    objHotelDetailReader_transfer.Close();
                    objHotelDetailReader_transfer.Dispose();

                    
                  
                    //Booking Name And Prefix
                    if (cBookingPLsit.PrefixId == 1)
                        strCustomerName = cBookingPLsit.BookingName ;
                    else
                        strCustomerName = cBookingPLsit.Prefixtitle + " " + cBookingPLsit.BookingName;


                    strCustomerName = strCustomerName + "/" + cBookingPLsit.CountryTitle;


                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingName##-->",strCustomerName);


                    //BookingCountry
                    //BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingCountry##-->", cBookingPLsit.CountryTitle);

                    //NumAult
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingNumAdult##-->", cBookingPLsit.NumAdult.ToString());
                    //Num Child
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingNumChild##-->", cBookingPLsit.NumChild.ToString());

                    //FlightArr
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_FlightArr##-->", cBookingPLsit.FlightNumArr + "<br/>" + DateTimeChekFullDateAndTime(cBookingPLsit.FlightTimeArr, bytBookingLang));
                    //FlightDep
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_FlightDepart##-->", cBookingPLsit.FlightNumDep + "<br/>" + DateTimeChekFullDateAndTime(cBookingPLsit.FlightTimeDep, bytBookingLang));

                    //ItemList
                    string KeywordItemList_Transfer = Utility.GetKeywordReplace(BookingDetail, "<!--##@voucherPrint_Items_Start##-->", "<!--##@voucherPrint_Items_End##-->");
                    BookingDetail = BookingDetail.Replace(KeywordItemList_Transfer, GetProductItem_Transfer(this.BookingProductId, DateTimeNightTotal(cBookingPLsit.DateTimeCheckIn, cBookingPLsit.DateTimeCheckOut), bytBookingLang));

                    //Note thai establishment
                    ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_Establishment##-->", "บริษัทรถ");

                    break;

                // Show Water Days
                case 34:
                case 36:
                case 38:
                    switch (bytBookingLang)
                    {
                        case 1:
                            them_file_item = "tbookingVoucher_Bookingdetail_day_water_show_en.html";
                            break;
                        case 2:
                            them_file_item = "tbookingVoucher_Bookingdetail_day_water_show_th.html";
                            break;
                    }
                    StreamReader objHotelDetailReader_Show_water_Daytrip = new StreamReader(HttpContext.Current.Server.MapPath("~/admin/booking/BookingPrintAndMail_Template_B2b/" + them_file_item + ""));
                    BookingDetail = objHotelDetailReader_Show_water_Daytrip.ReadToEnd();
                    objHotelDetailReader_Show_water_Daytrip.Close();
                    objHotelDetailReader_Show_water_Daytrip.Dispose();

                    //Booking Name And Prefix
                    
                 
                    if (cBookingPLsit.PrefixId == 1)
                        strCustomerName = cBookingPLsit.BookingName ;
                    else
                        strCustomerName = cBookingPLsit.Prefixtitle + " " + cBookingPLsit.BookingName;


                    strCustomerName = strCustomerName + "/" + cBookingPLsit.CountryTitle;


                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingName##-->",strCustomerName);


                    //BookingCountry
                    //BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingCountry##-->", cBookingPLsit.CountryTitle);

                    //Tripservice Date
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingTripDate##-->", DateTimeCheck(cBookingPLsit.DateTimeCheckIn));
                    


                    //ItemList
                    string KeywordItemList_Show_water_Daytrip = Utility.GetKeywordReplace(BookingDetail, "<!--##@voucherPrint_Items_Start##-->", "<!--##@voucherPrint_Items_End##-->");
                    BookingDetail = BookingDetail.Replace(KeywordItemList_Show_water_Daytrip, GetProductItem_Show_water_Daytrip(this.BookingProductId, DateTimeNightTotal(cBookingPLsit.DateTimeCheckIn, cBookingPLsit.DateTimeCheckOut), bytBookingLang));

                    //special remark punctual
                    ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_PunctaulRemark##-->", GetRemarkPunctual(bytBookingLang));

                    // Disable Address 
                    string KeyWordAddressBartoAttn = Utility.GetKeywordReplace(BookingDetail, "<!--##@voucherPrint_ConvertHeadAddressStart##-->", "<!--##@voucherPrint_ConvertHeadAddressEnd##-->");
                    BookingDetail = BookingDetail.Replace(KeyWordAddressBartoAttn, " ");

                    //Note thai establishment
                    ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_Establishment##-->", "สถานประกอบการ");

                    break;

                // Health
                case 39:

                    StreamReader objHotelDetailReader_Health = new StreamReader(HttpContext.Current.Server.MapPath("~/admin/booking/BookingPrintAndMail_Template_B2b/tbookingVoucher_Bookingdetail_health.html"));
                    BookingDetail = objHotelDetailReader_Health.ReadToEnd();
                    objHotelDetailReader_Health.Close();
                    objHotelDetailReader_Health.Dispose();

                    //Booking Name And Prefix
                    if (cBookingPLsit.PrefixId == 1)
                        BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingName##-->", cBookingPLsit.BookingName);
                    else
                        BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingName##-->", cBookingPLsit.Prefixtitle + " " + cBookingPLsit.BookingName);


                    //BookingCountry
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingCountry##-->", cBookingPLsit.CountryTitle);

                    //Check Up Date
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingspaDateService##-->", DateTimeCheck(cBookingPLsit.DateTimeCheckIn, cBookingPLsit.DateTimeConfirmCheckIn));
                    //Chek Up Time
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingspaTimeService##-->", DateTimeCheck_TimeOnly(cBookingPLsit.DateTimeCheckIn, cBookingPLsit.DateTimeConfirmCheckIn));


                    //ItemList
                    string KeywordItemList_Health = Utility.GetKeywordReplace(BookingDetail, "<!--##@voucherPrint_Items_Start##-->", "<!--##@voucherPrint_Items_End##-->");
                    BookingDetail = BookingDetail.Replace(KeywordItemList_Health, GetProductItem_Health(this.BookingProductId, DateTimeNightTotal(cBookingPLsit.DateTimeCheckIn, cBookingPLsit.DateTimeCheckOut), cBookingPLsit.BookingLang));

                    //Replace Address bar To Attenttion 
                    string Newattenttion = "<tr><td align=\"left\" bgcolor=\"#FFFFFF\" class=\"mStrong\">Attn</td><td colspan=\"3\" align=\"left\" bgcolor=\"#FFFFFF\"></td></tr>";
                    string KeyWordAddressBar = Utility.GetKeywordReplace(BookingDetail, "<!--##@voucherPrint_ConvertHeadAddressStart##-->", "<!--##@voucherPrint_ConvertHeadAddressEnd##-->");
                    BookingDetail = BookingDetail.Replace(KeyWordAddressBar, Newattenttion);

                    break;
            }

            //BookingDetail
            string KeywordDetail = Utility.GetKeywordReplace(ResultVoucher, "<!--##@voucherPrint_BookingDetailStart##-->", "<!--##@voucherPrint_BookingDetailEnd##-->");
            ResultVoucher = ResultVoucher.Replace(KeywordDetail, BookingDetail);

            //Booking Head Wording 
            //string strVoucherHead = "<div class=\"hotel_booking\">Hotel <span>Voucher</span><br /><a href=\"http://www.hotels2thailand.com\">www.hotels2thailand.com</a></div>";
            //if (cBookingPLsit.ProductCategory != 29)
            //{
            //    strVoucherHead = "<div class=\"hotel_booking\">&nbsp;&nbsp;&nbsp;&nbsp;<span>Voucher</span><br /><a href=\"http://www.hotels2thailand.com\">www.hotels2thailand.com</a></div>";
            //}

            string strVoucherHead = "<div class=\"hotel_booking\"></div>";
            if (cBookingPLsit.BookingAgencyId.HasValue)
                strVoucherHead = "<div class=\"hotel_booking\">" + B2bAgency.GetAgencyImg((int)cBookingPLsit.BookingAgencyId) + "</div>";

            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_VoucherHead##-->", strVoucherHead);
            

            //Booking Id/ BookinfProductID

            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_BookingCode##-->", cBookingPLsit.BookingId + "/" + cBookingPLsit.BookingProductId);
            //BookingProductName
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_hotelName##-->", cBookingPLsit.ProductTitle);
            //Hotel Phone
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_hotelPhone##-->", cBookingPLsit.ProductPhone);
            //HOtel Address
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_hotelAddress##-->", cBookingPLsit.ProductAddress);


            //REquierMent
            string KeywordREquire = Utility.GetKeywordReplace(ResultVoucher, "<!--##@voucherPrint_RoomRequireMentStart##-->", "<!--##@voucherPrint_RoomRequireMentEnd##-->");
            ResultVoucher = ResultVoucher.Replace(KeywordREquire, GetRequirement(this.BookingProductId, cBookingPLsit.ProductCategory, bytBookingLang));

            //Policy
            string KeywordPolicy= Utility.GetKeywordReplace(ResultVoucher, "<!--##@voucherPrint_PolicyStart##-->", "<!--##@voucherPrint_PolicyEnd##-->");
            ResultVoucher = ResultVoucher.Replace(KeywordPolicy, GetProductItemPolicy(this.BookingProductId, cBookingPLsit.IsExtranet, bytBookingLang));

            //Remark
            //string KeywordREmark = Utility.GetKeywordReplace(ResultVoucher, "<!--##@voucherPrint_REmarkStartd##-->", "<!--##@voucherPrint_REmarkEnd##-->");
            //ResultVoucher = ResultVoucher.Replace(KeywordREmark, GetRemark(this.BookingProductId));


            return ResultVoucher;


        }
        private string GetWordingSpecial(byte bytBookingLang)
        {
            string strResult = string.Empty;
            switch (bytBookingLang)
            {
                case 1:
                    strResult = "<strong style=\"color:red;\">Please do not  pass Immigration / or collect your baggage otherwise you can not stay inside the airport </strong><br/><span style=\"font-size:11px;font-weight:bold\"> Emergency please call 66 8 63172211</span>";
                    break;
                case 2:
                    strResult = "<strong style=\"color:red;\">Please do not pass Immigration / or collect your baggage otherwise you can not stay inside the airport </strong><br/><span style=\"font-size:11px;font-weight:bold\"> Emergency please call 66 8 63172211</span>";
                    break;
            }
            StringBuilder result = new StringBuilder();

            result.Append("<p style=\"width:90%;margin:0 auto;color:#666666;padding:10px;border:1px solid #d1d2d4;\">");

            result.Append(strResult);
            result.Append("</p>");


            return result.ToString();
        }
        private string GetRemarkPunctual(byte bytBookingLang)
        {
            string strResult = string.Empty;
            switch (bytBookingLang)
            {
                case 1:
                    strResult = "<strong>Please be punctual</strong> for pick up time as Hotels2thailand.com holds <strong>no responsibility</strong> for any customer who miss the pick up. (No show & no refund)";
                    break;
                case 2:
                    strResult = "<strong>ในการรับส่งลูกค้าทุกท่าน</strong> ขอความกรุณามารอเจ้าหน้าที่ ณ จุดนัดหมายก่อนเวลาที่แจ้งไว้อย่างน้อยล่วงหน้า 10 นาที <br/><strong>เวปไซต์โฮเทลทูขอสงวนสิทธิไม่รับผิดชอบและไม่คืนเงินในทุกกรณีหากท่านมาไม่ตรงตามเวลาที่นัดหมายและพลาดรถรับส่งจากเรา</strong> ";
                    break;
            }
            StringBuilder result = new StringBuilder();

            result.Append("<br/><br/><p style=\"width:90%;margin:0 auto;color:#666666;padding:10px;border:1px solid #d1d2d4;\">");

            result.Append(strResult);
            result.Append("</p>");
            

            return result.ToString();
        }


        private string GetRequirement(int intBookingProductId, byte productCat, byte bytBookingLangId)
        {
            StringBuilder ReqItemResult = new StringBuilder();
            BookingRequireMent cRequire = new BookingRequireMent();

            string strReqhead = string.Empty;
            string strReqfeild = string.Empty;
            string strDetail = string.Empty;

            switch (bytBookingLangId)
            {
                case 1:
                    strReqhead = "Requirement";
                    strReqfeild = "Special Requirement";
                    strDetail = "Comment";
                    break;
                case 2:
                    strReqhead = "ความต้องการพิเศษ";
                    strReqfeild = "คำขอพิเศษ";
                    strDetail = "รายละเอียด";
                    break;
            }

            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            var arrOptionCat = new[] { "44", "52", "53", "54" };

            //IList<object> ilit = cBookingItem.getBookingItemListByBookingProductId(intBookingProductId);

            cBookingItem = (BookingItemDisplay)cBookingItem.getBookingItemListByBookingProductId(intBookingProductId)
            .Where(cat => arrOptionCat.Contains((cat as BookingItemDisplay).OptionCAtID.ToString())) 
            .FirstOrDefault();

            //cat.GetType().GetProperty("OptionCAtID").GetValue(cat, null)

            IList<object> cBookingItemRequireListdefault = cRequire.GetRequireMentByBooinProductIDAndProductCat(intBookingProductId, productCat, 1);
            IList<object> cBookingItemRequireListReal = cRequire.GetRequireMentByBooinProductIDAndProductCat(intBookingProductId, productCat, bytBookingLangId);
            IList<object> cBookingItemRequireList = null;

            if (cBookingItemRequireListdefault.Count == cBookingItemRequireListReal.Count)
                cBookingItemRequireList = cBookingItemRequireListReal;
            else
                cBookingItemRequireList = cBookingItemRequireListdefault;



            int Count = 1;
            if (cBookingItemRequireList.Count > 0 || cBookingItem != null)
            {
                ReqItemResult.Append("<table cellpadding=\"0\" cellspacing=\"1\" class=\"option\">");
                ReqItemResult.Append("<tr>");
                ReqItemResult.Append("<th colspan=\"2\">" + strReqhead + "</th>");
                ReqItemResult.Append("</tr>");

                if (cBookingItemRequireList.Count > 0)
                {


                    foreach (ArrayList items in cBookingItemRequireList)
                    {

                        ReqItemResult.Append("<tr>");
                        ReqItemResult.Append("<td colspan=\"2\"><b>" + Count + " # " + items[0].ToString() + "</b></td>");
                        ReqItemResult.Append("</tr>");
                        ReqItemResult.Append("");



                        switch (productCat)
                        {
                            case 29:

                                ReqItemResult.Append("<tr>");
                                ReqItemResult.Append("<td width=\"150\">" + strReqhead + "</td>");
                                ReqItemResult.Append("<td>- " + cRequire.requireTypeSmoke((byte)items[2]) + ", " + cRequire.requireTypeBed((byte)items[3]) + ", " + cRequire.requireTypeFloor((byte)items[4]) + "</td>");
                                ReqItemResult.Append("</tr>");
                                ReqItemResult.Append("<tr>");
                                ReqItemResult.Append("<td>" + strReqfeild + "</td>");
                                ReqItemResult.Append("<td>" + items[1].ToString() + "</td>");
                                ReqItemResult.Append("</tr>");

                                break;
                            case 31:
                            case 36:
                            case 34:
                            case 38:
                                ReqItemResult.Append("<tr>");
                                ReqItemResult.Append("<td>" + strReqfeild + "</td>");
                                ReqItemResult.Append("<td>" + items[1].ToString() + "</td>");
                                ReqItemResult.Append("</tr>");


                                ReqItemResult.Append("<tr>");
                                ReqItemResult.Append("<td>" + strDetail + "</td>");
                                ReqItemResult.Append("<td>" + cBookingItem.BookingItemDetail + "</td>");
                                ReqItemResult.Append("</tr>");

                                break;
                            default:

                                ReqItemResult.Append("<tr>");
                                ReqItemResult.Append("<td>" + strReqfeild + "</td>");
                                ReqItemResult.Append("<td>" + items[1].ToString() + "</td>");
                                ReqItemResult.Append("</tr>");
                                break;

                        }

                        Count = Count + 1;
                    }
                }
                else
                {

                    ReqItemResult.Append("<tr>");
                    ReqItemResult.Append("<td colspan=\"2\"><b>" + Count + " # " + cBookingItem.OptionTitle + "</b></td>");
                    ReqItemResult.Append("</tr>");

                    ReqItemResult.Append("<tr>");
                    ReqItemResult.Append("<td>" + strDetail + "</td>");
                    ReqItemResult.Append("<td>" + cBookingItem.BookingItemDetail + "</td>");
                    ReqItemResult.Append("</tr>");

                    
                }


                

                ReqItemResult.Append("</table>");
            }
            else
            {
                ReqItemResult.Append(" ");
            }
            return ReqItemResult.ToString();
        }


        //Get remark If old DB
        private string GetRemark(int intBookingProductID)
        {
            StringBuilder remark = new StringBuilder();
            BookingRemark_FromODB cREmark = new BookingRemark_FromODB();
            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            List<object> cBookingItemList = cBookingItem.getBookingItemListByBookingProductId(intBookingProductID);
            
            int Count = 0;
            XmlDocument doc = new XmlDocument();
            foreach (BookingItemDisplay items in cBookingItemList)
            {
                doc.LoadXml(items.ConditionDetail);
                XmlNode root = doc.DocumentElement;
                if (!root.HasChildNodes)
                {
                    Count = Count + 1;
                }
            }
            if (Count > 0)
            {
                remark.Append("</br><table width=\"650\" border=\"0\" cellpadding=\"5\" cellspacing=\"1\" bgcolor=\"#444444\" class=\"m1\">");
                remark.Append("<tr>");
                remark.Append("<td align=\"left\" bgcolor=\"#FFFFFF\" class=\"l2\">Remark :</td>");
                remark.Append("</tr>");
                remark.Append("<tr>");
                remark.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">");
                remark.Append("<ul>");
                foreach (BookingRemark_FromODB remarkItem in cREmark.getRemarkByProductId(intBookingProductID))
                {
                    remark.Append("<li>" + remarkItem.TitleEn + "</li>");
                }
                remark.Append("</ul>");
                remark.Append("</td>");
                remark.Append("</tr>");
                remark.Append("</table>");
            }
            else
            {
                remark.Append(" ");
            }
            return remark.ToString();
            
        }


        private string GetProductItemPolicy(int intBookingProductId, bool Isextranet, byte bytookingLang)
        {
            StringBuilder TiemResult = new StringBuilder();
            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            

            List<object> cBookingItemListreal = cBookingItem.getBookingItemListByBookingProductId(intBookingProductId);

            if (cBookingItemListreal.Count > 0)
                TiemResult.Append(Hotels2XMLContent.Hotels2XMlReader_PolicyAndCancellation(cBookingItemListreal, intBookingProductId, Isextranet, bytookingLang));

            
            
            return TiemResult.ToString();
        }

        //Get Cancellation Policy Old Db
        public string GetOldCancelltion(bool IsAll)
        {
            StringBuilder can = new StringBuilder();
            can.Append("</br><table width=\"650\" border=\"0\" cellpadding=\"5\" cellspacing=\"1\" bgcolor=\"#444444\" class=\"m1\">");
            if (IsAll)
            {
                can.Append("<tr>");
                can.Append("<td align=\"left\" bgcolor=\"#FFFFFF\" class=\"l2\">Cancellation Policy :</td>");
                can.Append("</tr>");
            }
            can.Append("<tr>");
            can.Append(" <td align=\"left\" bgcolor=\"#FFFFFF\"><br />");
            can.Append("No charge is made to your credit card until your booking is final, that is, when you submitted the payment either online");
            can.Append("or by fax and we e-mailed you the hotel voucher. In order to  secure your room reservation you must complete and submit");
            can.Append("your  credit card details via our Secure Online Credit Card Form or  alternatively you may want to print it out and fax");
            can.Append("it to us  duly completed and signed (Our fax no.: +66 (0)2930 6514). <br />");
            can.Append("<div id=\"tblCancel\">");
            can.Append("<table width=\"100%\" id=\"cancel_detail\" cellpadding=\"5\">");
            can.Append("<tr>");
            can.Append("<td valign=\"top\"><span class=\"hd_cancel\"><strong>Normal Season</strong></span><br /><span class=\"title_cancel\">May 1 to Oct 31</span></td>");
            can.Append("</tr>");
            can.Append("<tr>");
            can.Append("<td>");
            can.Append("<ul class=\"condition_cancel\">");
            can.Append("<li>+14 Days prior to arrival<br />There is a 7% administration charge<br /><br /></li>");
            can.Append("<li>13-7 Days prior to arrival<br />There is a 15% administration charge<br /><br /></li>");
            can.Append("<li>6-3 Days prior to arrival<br />There is a cancellation fee of one night<br /><br /></li>");
            can.Append("<li>2-0 Days prior to arrival<br />There is a cancellation fee of your full booking amount<br /><br /></li>");
            can.Append("</ul>");
            can.Append("</td>");
            can.Append("</tr>");
            can.Append(" <tr>");
            can.Append("<td valign=\"top\"><span class=\"hd_cancel\"><strong>High Season:</strong></span><br />");
            can.Append("<span class=\"title_cancel\">Jan 16 - Apr 30 and Nov 1 - Dec 14 for all destinations and Jan 16 - Apr 30 and Jul 16 - Sep 15 for Koh Samui only.</span>");
            can.Append("</td>");
            can.Append("</tr>");
            can.Append("<tr>");
            can.Append("<td>");
            can.Append("<ul class=\"condition_cancel\">");
            can.Append("<li>+14 Days prior to arrival<br />There is a 7% administration charge<br /><br /></li>");
            can.Append("<li>7-13 Days prior to arrival<br />There is a cancellation fee of 1 night plus a 7% administration charge<br /><br /></li>");
            can.Append("<li>7-0 Days prior to arrival<br />There is a cancellation fee of your full booking amount<br /><br /></li>");
            can.Append("</ul>");
            can.Append("</td>");
            can.Append("</tr>");
            can.Append("<tr>");
            can.Append("<td valign=\"top\"><span class=\"hd_cancel\"><strong>Peak Season:</strong></span><br />");
            can.Append("<span class=\"title_cancel\">December 15 - January 15 / Festive and Holiday period for all destinations</span>");
            can.Append("</td>");
            can.Append("</tr>");
            can.Append("<tr>");
            can.Append("<td>");
            can.Append("<ul class=\"condition_cancel\">");
            can.Append("<li>+45 Days prior to arrival<br />There is a 7% administration charge<br /><br /></li>");
            can.Append("<li>45-30 Days prior to arrival<br />There is a cancellation fee of 1 night plus a 7% administration charge<br /><br /></li>");
            can.Append("<li>30-0 Days prior to arrival<br />There is a cancellation fee of your full booking amount<br /><br /></li>");
            can.Append("</ul><br /><br /></td></tr></table></div>");
            can.Append("<p>In case of No Show we will debit your credit card for the full amount of your booked stay, e.g.: No Refund Applies.</p>");
            can.Append("<p>In case of cancellation or a booking amend request, please do contact us immediately via our online cancellation form ");
            can.Append("below. Please use the same email address as in your original booking request, plus enter your ORDER ID for reference!</p>");
            can.Append("<p>Please note that all cancellations or booking amendment requests  MUST be submitted in writing (either by email, via our online"); 
            can.Append("form or by fax). Please be advised that we will not accept any cancellation or booking amendment requests by phone or ");
            can.Append("without above described details.</p>");
            can.Append("<p>Cancellations or booking amendment requests must show your ORDER ID and email address you originally used to make your ");
            can.Append("booking. Cancellations are ONLY valid if you received an email from us confirming your cancellation. If you have submitted ");
            can.Append("your cancellation and do not receive our confirmation of receipt by email do contact us IMMEDIATELY.</p><br>");
            can.Append("<div><strong>Amendments</strong><br />");
            can.Append("<ul>");
            can.Append("<li>All requests for additional nights or rooms are subject to room   availability. </li>");
            can.Append("<li>If the amendment results in a refund of payment (reduction of stay, number   of rooms, or number of people booked), a handling fee will be charged equivalent   to 7% of the amount to be refunded. </li>");
            can.Append("<li>Amendments are only effective when we have confirmed by email.</li>");
            can.Append("</ul>");
            can.Append("</div></td></tr>");
            can.Append("</table>");

            return can.ToString();
        }
    }
}