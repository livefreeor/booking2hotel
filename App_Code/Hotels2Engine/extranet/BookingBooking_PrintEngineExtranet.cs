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


/// <summary>
/// Summary description for Voucher
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public partial class BookingBooking_PrintEngineExtranet : BookingPrintAndVoucher_Helper
    {
        public int BookingProductId { get; set; }

        public BookingBooking_PrintEngineExtranet(int intBookingProductId)
        {
            this.BookingProductId = intBookingProductId;
            
        }

        
        protected string getTheme()
        {
            StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath("/extranet/ordercenter/BookingPrintAndMail_Template/Template_booking_print.html"));
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


            //Booking Name And Prefix
            if (cBookingPLsit.PrefixId == 1)
                ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_BookingName##-->", cBookingPLsit.BookingName);
            else
                ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_BookingName##-->", cBookingPLsit.Prefixtitle + " " + cBookingPLsit.BookingName);
            //BookingCountry
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_BookingCountry##-->", cBookingPLsit.CountryTitle);

            //CheckIn
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_BookingCheckIn##-->", DateTimeCheck(cBookingPLsit.DateTimeCheckIn));
            //CheckOut
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_BookingCheckOut##-->", DateTimeCheck(cBookingPLsit.DateTimeCheckOut));

            //NumAult
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_BookingNumAdult##-->", cBookingPLsit.NumAdult.ToString());
            //Num Child
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_BookingNumChild##-->", cBookingPLsit.NumChild.ToString());


            //ItemList
            string KeywordItemList = Utility.GetKeywordReplace(ResultVoucher, "<!--##@voucherPrint_Items_Start##-->", "<!--##@voucherPrint_Items_End##-->");
            ResultVoucher = ResultVoucher.Replace(KeywordItemList, GetProductItem_hotel(this.BookingProductId, DateTimeNightTotal(cBookingPLsit.DateTimeCheckIn, cBookingPLsit.DateTimeCheckOut), cBookingPLsit.BookingLang));

            ////ItemExptra 
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_Items_Extra##-->", GetProductItemExtra(this.BookingProductId));

            //MAinTotalPrice
            ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_MainTotal##-->", cBookingPLsit.TotalPriceSupplier.Hotels2Currency());



            ////BookingDetail
            //string KeywordDetail = Utility.GetKeywordReplace(ResultVoucher, "<!--##@voucherPrint_BookingDetailStart##-->", "<!--##@voucherPrint_BookingDetailEnd##-->");
            //ResultVoucher = ResultVoucher.Replace(KeywordDetail, BookingDetail);


            //REquierMent
            string KeywordREquire = Utility.GetKeywordReplace(ResultVoucher, "<!--##@voucherPrint_RoomRequireMentStart##-->", "<!--##@voucherPrint_RoomRequireMentEnd##-->");
            ResultVoucher = ResultVoucher.Replace(KeywordREquire, GetRequirement(this.BookingProductId, cBookingPLsit.ProductCategory));

            ////PicStaff_stamp
            //ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_Staff_stamp##-->", StaffStampPic());
            ////Staff_name
            //ResultVoucher = ResultVoucher.Replace("<!--##@voucherPrint_Staff_name##-->", StaffName());
            return ResultVoucher;


        }

        

        private string GetRequirement(int intBookingProductId, byte productCat)
        {
            StringBuilder ReqItemResult = new StringBuilder();
            BookingRequireMent cRequire = new BookingRequireMent();


            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            cBookingItem = (BookingItemDisplay)cBookingItem.getBookingItemListByBookingProductId(intBookingProductId)
            .Where(cat => (short)cat.GetType().GetProperty("OptionCAtID").GetValue(cat, null) == 44)
            .FirstOrDefault();

            IList<object> cBookingItemRequireList = cRequire.GetRequireMentByBooinProductIDAndProductCat(intBookingProductId, productCat);
            int Count = 1;
            if (cBookingItemRequireList.Count > 0 || cBookingItem != null)
            {
                ReqItemResult.Append("<table cellpadding=\"0\" cellspacing=\"2\">");
                ReqItemResult.Append("<tr>");
                ReqItemResult.Append("<th colspan=\"2\">Requirement</th>");
                ReqItemResult.Append("</tr>");
                ReqItemResult.Append("<tr>");
                ReqItemResult.Append("<td bgcolor=\"#FFFFFF\">");
                foreach (ArrayList items in cBookingItemRequireList)
                {
                    ReqItemResult.Append("<br />");
                    ReqItemResult.Append("<table cellpadding=\"0\" cellspacing=\"2\">");
                    ReqItemResult.Append("<tr>");
                    ReqItemResult.Append("<td> &nbsp; " + Count + " # " + items[0].ToString() + " </td>");
                    ReqItemResult.Append("</tr>");


                    switch (productCat)
                    {
                        case 29:

                            ReqItemResult.Append("<tr>");
                            ReqItemResult.Append("<td><span class=\"typeTex4\">Requirement : </span>");
                            ReqItemResult.Append( cRequire.requireTypeSmoke((byte)items[2]) + ", " + cRequire.requireTypeBed((byte)items[3]) + ", " + cRequire.requireTypeFloor((byte)items[4]) + "</td>");
                            ReqItemResult.Append("</tr>");
                            ReqItemResult.Append("<tr>");
                            ReqItemResult.Append("<td><span class=\"typeTex4\">Special Request :</span> ");
                            ReqItemResult.Append(items[1].ToString() + "</td>");
                            ReqItemResult.Append("</tr>");
                            
                            break;
                       default:

                            ReqItemResult.Append("<tr>");
                            ReqItemResult.Append("<td><span class=\"typeTex4\">Comment : </span>");
                            ReqItemResult.Append(items[1].ToString()+"</td>");
                            ReqItemResult.Append("</tr>");
                            break;

                    }
                    ReqItemResult.Append("</table><br />");
                    Count = Count + 1;
                }



                if (cBookingItem != null)
                {
                    ReqItemResult.Append("<br />");
                    ReqItemResult.Append("<table cellpadding=\"0\" cellspacing=\"2\">");
                    ReqItemResult.Append("<tr>");
                    ReqItemResult.Append("<td>&nbsp;  " + Count + " # " + cBookingItem.OptionTitle + " </td>");
                    ReqItemResult.Append("</tr>");
                    ReqItemResult.Append("<tr>");
                    ReqItemResult.Append("<td><span class=\"typeTex4\">Comment : ");
                    ReqItemResult.Append(cBookingItem.BookingItemDetail + "</td>");
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