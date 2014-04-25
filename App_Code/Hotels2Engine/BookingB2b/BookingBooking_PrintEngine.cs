using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Collections;
using System.Xml;
using System.Text;
using System.Linq;
using Hotels2thailand.Front;
using Hotels2thailand;
using Hotels2thailand.Suppliers;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Booking;


/// <summary>
/// Summary description for Voucher
/// </summary>
/// 
namespace Hotels2thailand.BookingB2b
{
    public partial class BookingBooking_PrintEngineB2b : BookingPrintAndVoucher_Helper
    {
        public int BookingProductId { get; set; }

        public BookingBooking_PrintEngineB2b(int intBookingProductId)
        {
            this.BookingProductId = intBookingProductId;
            
        }

        
        protected string getTheme()
        {
            StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template/Template_bookingPrint.html"));
            string read = objReader.ReadToEnd();
            objReader.Close();
            objReader.Dispose();
            return read;
        }
        
        public string getBookingPrint()
        {
            string ResultVoucher = getTheme();
            
            BookingProductDisplay cBookingPLsit = new BookingProductDisplay();
            cBookingPLsit = cBookingPLsit.getBookingProductDisplayByBookingProductId(this.BookingProductId);
            SupplierContactPhoneEmail SupContact = new SupplierContactPhoneEmail();

            byte bytBookingLang = cBookingPLsit.BookingLang;

            //-- Phone Mobile Res And Sales ssc.department_id In( 3,1)  sscp.cat_id IN ( 2,1)
            string PhoneMobile_ResAndSales = SupContact.GetstringContact(cBookingPLsit.SupplierId, "3,1", "2,1");

            //-- Fax Res And Sales ssc.department_id In( 3,1)  sscp.cat_id IN ( 3)
            string PhoneFax_ResAndSales = SupContact.GetstringContact(cBookingPLsit.SupplierId, "3,1", "3");


            //-- Email Res & sales  ssc.department_id In(3,1) 
            //string EmailResSales = SupContact.GetstringContactEmail(cBookingPLsit.SupplierId, "3,1");

            //-- Phone Mobile  Account ssc.department_id In( 2)  sscp.cat_id IN ( 2,1)
            //string PhoneMobile_Account = SupContact.GetstringContact(cBookingPLsit.SupplierId, "2", "2,1");

            //-- Fax Account ssc.department_id In( 2) sscp.cat_id IN (3)
            //string PhoneFaxAccount = SupContact.GetstringContact(cBookingPLsit.SupplierId, "2", "3");

            //-- Email Account ssc.department_id In( 2) 
            //string EmailAcc = SupContact.GetstringContactEmail(cBookingPLsit.SupplierId, "2");

            

            //Booking Id/ BookinfProductID
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_BookingCode##-->", cBookingPLsit.BookingId + "/" + cBookingPLsit.BookingProductId);
            //BookingProductName
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_hotelName##-->",  cBookingPLsit.ProductTitle);
            //Supplier Phone
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_hotelPhoneSup##-->", PhoneMobile_ResAndSales);

            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_IssueDate##-->", DateTime.Now.Hotels2ThaiDateTime().ToString("hh:mm MMM dd, yy"));
            //Supplier FAx
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_hotelPhoneFax##-->", PhoneFax_ResAndSales);

            string BookingDetail = string.Empty;
            switch (cBookingPLsit.ProductCategory)
            {
                //Hotel
                case 29:
                    StreamReader objHotelDetailReader = new StreamReader(HttpContext.Current.Server.MapPath("/admin/booking/BookingPrintAndMail_Template/tbookingPrint_Slip_Bookingdetail_hotel.html"));
                    BookingDetail = objHotelDetailReader.ReadToEnd();
                    objHotelDetailReader.Close();
                    objHotelDetailReader.Dispose();
                    //Booking Name And Prefix
                    if (cBookingPLsit.PrefixId == 1)
                        BookingDetail = BookingDetail.Replace("<!--##@voucherPrint_BookingName##-->",  cBookingPLsit.BookingName);
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

                    ////ItemExptra 
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


            //REquierMent
            string KeywordREquire = Utility.GetKeywordReplace(ResultVoucher, "<!--##@voucherPrint_RoomRequireMentStart##-->", "<!--##@voucherPrint_RoomRequireMentEnd##-->");
            ResultVoucher = ResultVoucher.Replace(KeywordREquire, GetRequirement(this.BookingProductId, cBookingPLsit.ProductCategory, bytBookingLang));

            //PicStaff_stamp
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_Staff_stamp##-->", StaffStampPic());
            //Staff_name
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_Staff_name##-->", StaffName());
            return ResultVoucher;


        }

        

        private string GetRequirement(int intBookingProductId, byte productCat,byte bytBookingLangId)
        {
            StringBuilder ReqItemResult = new StringBuilder();
            BookingRequireMent cRequire = new BookingRequireMent();


            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            //cBookingItem = (BookingItemDisplay)cBookingItem.getBookingItemListByBookingProductId(intBookingProductId)
            //.Where(cat => (int)cat.GetType().GetProperty("OptionCAtID").GetValue(cat, null) == 44)
            //.FirstOrDefault();
            var arrOptionCat = new[] { "44", "52", "53", "54" };
            cBookingItem = (BookingItemDisplay)cBookingItem.getBookingItemListByBookingProductId(intBookingProductId)
            .Where(cat => arrOptionCat.Contains((cat as BookingItemDisplay).OptionCAtID.ToString()))
            .FirstOrDefault();

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
                ReqItemResult.Append("<table width=\"100%\" border=\"0\" cellpadding=\"2\" cellspacing=\"1\" bgcolor=\"#444444\" class=\"m1\">");
                ReqItemResult.Append("<tr>");
                ReqItemResult.Append("<td bgcolor=\"#FFFFFF\" class=\"l2\">Requirement </td>");
                ReqItemResult.Append("</tr>");
                ReqItemResult.Append("<tr>");
                ReqItemResult.Append("<td bgcolor=\"#FFFFFF\">");


                if (cBookingItemRequireList.Count() > 0)
                {

                    foreach (ArrayList items in cBookingItemRequireList)
                    {
                        ReqItemResult.Append("<br />");
                        ReqItemResult.Append("<table width=\"100%\" border=\"0\" cellpadding=\"2\" cellspacing=\"1\" bgcolor=\"#CCCCCC\">");
                        ReqItemResult.Append("<tr>");
                        ReqItemResult.Append("<td colspan=\"2\" align=\"left\" bgcolor=\"#FFFFFF\" class=\"l1\"> " + Count + " # " + items[0].ToString() + " </td>");
                        ReqItemResult.Append("</tr>");


                        switch (productCat)
                        {
                            case 29:

                                ReqItemResult.Append("<tr>");
                                ReqItemResult.Append("<td width=\"20%\" align=\"left\" bgcolor=\"#FFFFFF\" class=\"mStrong\">Requirement</td>");
                                ReqItemResult.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + cRequire.requireTypeSmoke((byte)items[2]) + ", " + cRequire.requireTypeBed((byte)items[3]) + ", " + cRequire.requireTypeFloor((byte)items[4]) + "</td>");
                                ReqItemResult.Append("</tr>");
                                ReqItemResult.Append("<tr>");
                                ReqItemResult.Append("<td align=\"left\" bgcolor=\"#FFFFFF\" class=\"mStrong\">Special Requirement</td>");
                                ReqItemResult.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + items[1].ToString() + "</td>");
                                ReqItemResult.Append("</tr>");

                                break;
                            case 31:
                            case 34:
                            case 36:
                            case 38:
                                ReqItemResult.Append("<tr>");
                                ReqItemResult.Append("<td width=\"20%\" align=\"left\" bgcolor=\"#FFFFFF\" class=\"mStrong\">Special Requirement</td>");
                                ReqItemResult.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + items[1].ToString() + "</td>");
                                ReqItemResult.Append("</tr>");
                                 ReqItemResult.Append("<tr>");
                                ReqItemResult.Append("<td width=\"20%\" align=\"left\" bgcolor=\"#FFFFFF\" class=\"mStrong\">Comment</td>");
                                ReqItemResult.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + cBookingItem.BookingItemDetail + "</td>");
                                ReqItemResult.Append("</tr>");

                                break;
                            default:

                                ReqItemResult.Append("<tr>");
                                ReqItemResult.Append("<td width=\"20%\" align=\"left\" bgcolor=\"#FFFFFF\" class=\"mStrong\">Comment</td>");
                                ReqItemResult.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + items[1].ToString() + "</td>");
                                ReqItemResult.Append("</tr>");
                               
                                break;

                        }
                        ReqItemResult.Append("</table><br />");
                        Count = Count + 1;
                    }
                }
                else
                {
                  
                        ReqItemResult.Append("<br />");
                        ReqItemResult.Append("<table width=\"100%\" border=\"0\" cellpadding=\"2\" cellspacing=\"1\" bgcolor=\"#CCCCCC\">");
                        ReqItemResult.Append("<tr>");
                        ReqItemResult.Append("<td colspan=\"2\" align=\"left\" bgcolor=\"#FFFFFF\" class=\"l1\"> " + Count + " # " + cBookingItem.OptionTitle + " </td>");
                        ReqItemResult.Append("</tr>");
                        ReqItemResult.Append("<tr>");
                        ReqItemResult.Append("<td width=\"20%\" align=\"left\" bgcolor=\"#FFFFFF\" class=\"mStrong\">Comment</td>");
                        ReqItemResult.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + cBookingItem.BookingItemDetail + "</td>");
                        ReqItemResult.Append("</tr>");
                        ReqItemResult.Append("</table><br />");
                    
                }



                
                


                ReqItemResult.Append("</td>");
                ReqItemResult.Append("</tr>");
                ReqItemResult.Append("</table>");
            }
            else
            {
                ReqItemResult.Append(" ");
            }
            return ReqItemResult.ToString();
        }

    }
}