using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Collections;
using System.Xml;
using System.Text;
using Hotels2thailand.Front;
using Hotels2thailand;

using Hotels2thailand.Suppliers;
using Hotels2thailand.ProductOption;


/// <summary>
/// Summary description for Voucher
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public partial class BookingSlip_PrintEngine :  BookingPrintAndVoucher_Helper
    {
        public int BookingProductId { get; set; }

        public BookingSlip_PrintEngine(int intBookingProductId)
        {
            this.BookingProductId = intBookingProductId;
        }

        //private string _mainsite = "http://www.hotels2thailand.com";
        protected string getTheme()
        {
            StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template/Template_SlipPrint.html"));
            string read = objReader.ReadToEnd();
            return read;
        }
        
        public string getSlip()
        {
            string ResultVoucher = getTheme();
            BookingProductDisplay cBookingPLsit = new BookingProductDisplay();
            cBookingPLsit = cBookingPLsit.getBookingProductDisplayByBookingProductId(this.BookingProductId);
            SupplierContactPhoneEmail SupContact = new SupplierContactPhoneEmail();

            //-- Phone Mobile Res And Sales ssc.department_id In( 3,1)  sscp.cat_id IN ( 2,1)
            //string PhoneMobile_ResAndSales = SupContact.GetstringContact(cBookingPLsit.SupplierId, "3,1", "2,1");

            //-- Fax Res And Sales ssc.department_id In( 3,1)  sscp.cat_id IN ( 3)
           // string PhoneFax_ResAndSales = SupContact.GetstringContact(cBookingPLsit.SupplierId, "3,1", "3");

            //-- Phone Mobile  Account ssc.department_id In( 2)  sscp.cat_id IN ( 2,1)
            string PhoneMobile_Account = SupContact.GetstringContact(cBookingPLsit.SupplierId, "2", "2,1");

            //-- Fax Account ssc.department_id In( 2) sscp.cat_id IN (3)
            string PhoneFaxAccount = SupContact.GetstringContact(cBookingPLsit.SupplierId, "2","3");

            //-- Email Account ssc.department_id In( 2) 
            string EmailAcc = SupContact.GetstringContactEmail(cBookingPLsit.SupplierId, "2");

            //-- Email Res & sales  ssc.department_id In(3,1) 
            //string EmailResSales = SupContact.GetstringContactEmail(cBookingPLsit.SupplierId, "3,1");

            
            //Booking Id/ BookinfProductID
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_BookingCode##-->", cBookingPLsit.BookingId + "/" + cBookingPLsit.BookingProductId);
            //BookingProductName
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_hotelName##-->",  cBookingPLsit.ProductTitle);
            //Supplier Phone
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_hotelPhoneSup##-->", PhoneMobile_Account);

            //Supplier Email
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_hotelEmail##-->", EmailAcc);
            //Issue date
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_IssueDate##-->", DateTime.Now.Hotels2ThaiDateTime().ToString("dddd, MMM dd, yyyy"));
            //Supplier FAx
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_hotelPhoneFax##-->", PhoneFaxAccount);


            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_hotelAccount##-->", GetAccountlist(cBookingPLsit.SupplierId));
            


            string BookingDetail = string.Empty;
            switch (cBookingPLsit.ProductCategory)
            {
                case 29:
                    StreamReader objHotelDetailReader = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template/tbookingPrint_Slip_Bookingdetail_hotel.html"));
                    BookingDetail = objHotelDetailReader.ReadToEnd();
                    objHotelDetailReader.Close();
                    objHotelDetailReader.Dispose();
                    //Booking Name And Prefix
                    if (cBookingPLsit.PrefixId == 1)
                        BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingName##-->", cBookingPLsit.BookingName);
                    else
                        BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingName##-->", cBookingPLsit.Prefixtitle + " " + cBookingPLsit.BookingName);
                    //BookingCountry
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingCountry##-->", cBookingPLsit.CountryTitle);

                    //CheckIn
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingCheckIn##-->", DateTimeCheck(cBookingPLsit.DateTimeCheckIn));
                    //CheckOut
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingCheckOut##-->", DateTimeCheck(cBookingPLsit.DateTimeCheckOut));
                    
                    //NumAult
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingNumAdult##-->", cBookingPLsit.NumAdult.ToString());
                    //Num Child
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingNumChild##-->", cBookingPLsit.NumChild.ToString());

                    //ItemList
                    string KeywordItemList = Utility.GetKeywordReplace(BookingDetail, "<!--##@voucherPrint_Items_Start##-->", "<!--##@voucherPrint_Items_End##-->");
                    BookingDetail = BookingDetail.Replace(KeywordItemList, GetProductItem_hotel(this.BookingProductId, DateTimeNightTotal(cBookingPLsit.DateTimeCheckIn, cBookingPLsit.DateTimeCheckOut), cBookingPLsit.BookingLang));

                    //ItemExptra 
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_Items_Extra##-->", GetProductItemExtra(this.BookingProductId));

                    //MAinTotalPrice
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_MainTotal##-->", cBookingPLsit.TotalPriceSupplier.Hotels2Currency());

                    break;
                    
                // Golf
                case 32:

                    StreamReader objHotelDetailReaderGolf = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template/tbookingPrint_Slip_Bookingdetail_golf.html"));
                    BookingDetail = objHotelDetailReaderGolf.ReadToEnd();
                    objHotelDetailReaderGolf.Close();
                    objHotelDetailReaderGolf.Dispose();

                    //Booking Name And Prefix
                    if (cBookingPLsit.PrefixId == 1)
                        BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingName##-->", cBookingPLsit.BookingName);
                    else
                        BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingName##-->", cBookingPLsit.Prefixtitle + " " + cBookingPLsit.BookingName);


                    //BookingCountry
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingCountry##-->", cBookingPLsit.CountryTitle);

                    //Tee-Off Time
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingTimeDateConfirme##-->", DateTimeChekFullDateAndTime(cBookingPLsit.DateTimeCheckIn, cBookingPLsit.DateTimeConfirmCheckIn));


                    //NumGolfer
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingNumGolfer##-->", cBookingPLsit.NumAdult.ToString());

                    //ItemList
                    string KeywordItemListGolf = Utility.GetKeywordReplace(BookingDetail, "<!--##@voucherPrint_Items_Start##-->", "<!--##@voucherPrint_Items_End##-->");
                    BookingDetail = BookingDetail.Replace(KeywordItemListGolf, GetProductItem_Golf(this.BookingProductId, DateTimeNightTotal(cBookingPLsit.DateTimeCheckIn, cBookingPLsit.DateTimeCheckOut), cBookingPLsit.BookingLang));


                    //MAinTotalPrice
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_MainTotal##-->", cBookingPLsit.TotalPriceSupplier.Hotels2Currency());

                    break;
                // Spa
                case 40:

                    StreamReader objHotelDetailReader_Spa = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template/tbookingPrint_Slip_Bookingdetail_spa.html"));
                    BookingDetail = objHotelDetailReader_Spa.ReadToEnd();
                    objHotelDetailReader_Spa.Close();
                    objHotelDetailReader_Spa.Dispose();

                    //Booking Name And Prefix
                    if (cBookingPLsit.PrefixId == 1)
                        BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingName##-->", cBookingPLsit.BookingName);
                    else
                        BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingName##-->", cBookingPLsit.Prefixtitle + " " + cBookingPLsit.BookingName);


                    //BookingCountry
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingCountry##-->", cBookingPLsit.CountryTitle);

                    //Service Date
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingspaDateService##-->", DateTimeCheck(cBookingPLsit.DateTimeCheckIn, cBookingPLsit.DateTimeConfirmCheckIn));
                    //Service Time
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingspaTimeService##-->", DateTimeCheck_TimeOnly(cBookingPLsit.DateTimeCheckIn, cBookingPLsit.DateTimeConfirmCheckIn));


                    //ItemList
                    string KeywordItemList_Spa = Utility.GetKeywordReplace(BookingDetail, "<!--##@voucherPrint_Items_Start##-->", "<!--##@voucherPrint_Items_End##-->");
                    BookingDetail = BookingDetail.Replace(KeywordItemList_Spa, GetProductItem_Spa(this.BookingProductId, DateTimeNightTotal(cBookingPLsit.DateTimeCheckIn, cBookingPLsit.DateTimeCheckOut), cBookingPLsit.BookingLang));

                    //MAinTotalPrice
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_MainTotal##-->", cBookingPLsit.TotalPriceSupplier.Hotels2Currency());

                    break;

                // Transfer
                case 31:

                    StreamReader objHotelDetailReader_transfer = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template/tbookingPrint_Slip_Bookingdetail_transfer.html"));
                    BookingDetail = objHotelDetailReader_transfer.ReadToEnd();
                    objHotelDetailReader_transfer.Close();
                    objHotelDetailReader_transfer.Dispose();

                    //Booking Name And Prefix
                    if (cBookingPLsit.PrefixId == 1)
                        BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingName##-->", cBookingPLsit.BookingName);
                    else
                        BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingName##-->", cBookingPLsit.Prefixtitle + " " + cBookingPLsit.BookingName);


                    //BookingCountry
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingCountry##-->", cBookingPLsit.CountryTitle);

                    //NumAult
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingNumAdult##-->", cBookingPLsit.NumAdult.ToString());
                    //Num Child
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingNumChild##-->", cBookingPLsit.NumChild.ToString());

                    //FlightArr
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_FlightArr##-->", cBookingPLsit.FlightNumArr + "," + DateTimeChekFullDateAndTime(cBookingPLsit.FlightTimeArr));
                    //FlightDep
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_FlightDepart##-->", cBookingPLsit.FlightNumDep + "," + DateTimeChekFullDateAndTime(cBookingPLsit.FlightTimeDep));

                    //ItemList
                    string KeywordItemList_Transfer = Utility.GetKeywordReplace(BookingDetail, "<!--##@voucherPrint_Items_Start##-->", "<!--##@voucherPrint_Items_End##-->");
                    BookingDetail = BookingDetail.Replace(KeywordItemList_Transfer, GetProductItem_Transfer(this.BookingProductId, DateTimeNightTotal(cBookingPLsit.DateTimeCheckIn, cBookingPLsit.DateTimeCheckOut), cBookingPLsit.BookingLang));

                    //MAinTotalPrice
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_MainTotal##-->", cBookingPLsit.TotalPriceSupplier.Hotels2Currency());

                    break;
                // Show Water Days
                case 34:
                case 36:
                case 38:

                    StreamReader objHotelDetailReader_Show_water_Daytrip = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template/tbookingPrint_Slip_Bookingdetail_day_water_show.html"));
                    BookingDetail = objHotelDetailReader_Show_water_Daytrip.ReadToEnd();
                    objHotelDetailReader_Show_water_Daytrip.Close();
                    objHotelDetailReader_Show_water_Daytrip.Dispose();

                    //Booking Name And Prefix
                    if (cBookingPLsit.PrefixId == 1)
                        BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingName##-->", cBookingPLsit.BookingName);
                    else
                        BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingName##-->", cBookingPLsit.Prefixtitle + " " + cBookingPLsit.BookingName);


                    //BookingCountry
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingCountry##-->", cBookingPLsit.CountryTitle);

                    //Tripservice Date
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingTripDate##-->", DateTimeCheck(cBookingPLsit.DateTimeCheckIn));



                    //ItemList
                    string KeywordItemList_Show_water_Daytrip = Utility.GetKeywordReplace(BookingDetail, "<!--##@voucherPrint_Items_Start##-->", "<!--##@voucherPrint_Items_End##-->");
                    BookingDetail = BookingDetail.Replace(KeywordItemList_Show_water_Daytrip, GetProductItem_Show_water_Daytrip(this.BookingProductId, DateTimeNightTotal(cBookingPLsit.DateTimeCheckIn, cBookingPLsit.DateTimeCheckOut), cBookingPLsit.BookingLang));

                    //MAinTotalPrice
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_MainTotal##-->", cBookingPLsit.TotalPriceSupplier.Hotels2Currency());

                    break;
                // Health
                case 39:

                    StreamReader objHotelDetailReader_Health = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template/tbookingPrint_Slip_Bookingdetail_transfer.html"));
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

                    //MAinTotalPrice
                    BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_MainTotal##-->", cBookingPLsit.TotalPriceSupplier.Hotels2Currency());

                    break;
            }
            
            //BookingDetail
            string KeywordDetail = Utility.GetKeywordReplace(ResultVoucher, "<!--##@voucherPrint_BookingDetailStart##-->", "<!--##@voucherPrint_BookingDetailEnd##-->");
            ResultVoucher = ResultVoucher.Replace(KeywordDetail, BookingDetail);


            return ResultVoucher;


        }

        private string GetNumGestString(byte bytProductCat, byte bytNumAdult, byte bytNumChild, byte bytNumGolf)
        {
            StringBuilder TiemResult = new StringBuilder();
            
            

            TiemResult.Append("<tr bgcolor=\"#FFFFFF\">");
            switch (bytProductCat)
            {
                case 32:

                    TiemResult.Append("<td width=\"54\" height=\"25\" bgcolor=\"#FFFFFF\"><div align=\"center\">");
                    TiemResult.Append("<span class=\"style19\">Golfer : </span></div>");
                    TiemResult.Append(" </td>");
                    TiemResult.Append("<td width=\"21\" height=\"25\"><div align=\"left\"><span class=\"style12\">" + bytNumGolf + "</span></div></td>");
                    
                    break;
                default :
                    TiemResult.Append("<td width=\"54\" height=\"25\" bgcolor=\"#FFFFFF\"><div align=\"center\">");
                    TiemResult.Append("<span class=\"style19\">Adult : </span></div>");
                    TiemResult.Append(" </td>");
                    TiemResult.Append("<td width=\"21\" height=\"25\"><div align=\"left\"><span class=\"style12\">" + bytNumAdult + "</span></div></td>");
                    TiemResult.Append("<td width=\"52\" height=\"25\"><div align=\"center\"><span class=\"style19\">Child ");
                    TiemResult.Append(":</span></div></td>");
                    TiemResult.Append("<td width=\"21\" height=\"25\"><div align=\"left\"><span class=\"style12\">" + bytNumChild + "</span></div></td>");
                    
                    
                    break;
            }
            TiemResult.Append("</tr>");
            return TiemResult.ToString();
        }
       

        


      

    }
}