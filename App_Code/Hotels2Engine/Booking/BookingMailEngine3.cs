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


/// <summary>
/// Summary description for Voucher
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public partial class BookingMailEngine : BookingPrintAndVoucher_Helper
    {
        //##1## Book now pay later mail resubmit
        public bool SendMailBookingRecevied_BookNow()
        {
            bool Success = false;
            string Maildisplay = "Hotels2Thailand.com Reservation Department";
            string subject = "Booking received for hotels2thailand.com  “ Book Now Pay Later” (ORDER ID:" + this.BookingId + ")";

            try
            {
                Hotels2MAilSender.SendmailBooking(Maildisplay, GetEmailBooking(), subject, this.Bcc, getMailBookingRecieved_Booknow());
                Success = true;
            }
            catch
            {
                Success = false;
            }
            return Success;
        }


        public string getMailBookingRecieved_Booknow()
        {
            string MainBody = getMailTheme();

            //Booking IitemList
            string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
            MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId());

            //Detail Top
            string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(5));


            //CustomerName 
            MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

            //disable grand Total
            string KeywordTotalGrand = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalGrandStart##-->", "<!--##@ContentTotalGrandEnd##-->");
            MainBody = MainBody.Replace(KeywordTotalGrand, " ");
            ////GrandTotal
            //MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal());

            //disable Total Paid & requset Payment
            string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalresubmitStart##-->", "<!--##@ContentTotalresubmitEnd##-->");
            MainBody = MainBody.Replace(KeywordTotalResubmit, " ");


            //getDetail Voucher
            //MainBody = MainBody.Replace("<!--##@ContentBodyDisplay##-->", TextDetailVoucher());
            return MainBody;
        }

        //##2## Book now pay later Voucher ## Charge 1$ completed
        public string getMailSendVoucher_Booknow()
        {
            string MainBody = getMailTheme();

            //Booking IitemList
            string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
            MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId());

            //Detail Top
            string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(6));


            //CustomerName 
            MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

            //GrandTotal
            MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal());

            //disable Total Paid & requset Payment
            string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalresubmitStart##-->", "<!--##@ContentTotalresubmitEnd##-->");
            MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

            //getDetail Voucher
            MainBody = MainBody.Replace("<!--##@ContentBodyDisplay##-->", TextDetailVoucher());
            return MainBody;
        }
        //##4##
        //public string getMailSendVoucher_Booknow_Charge_decline()
        //{
        //    string MainBody = getMailTheme();

        //    //Booking IitemList
        //    string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
        //    MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId());

        //    //Detail Top
        //    string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
        //    MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(3));


        //    //CustomerName 
        //    MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

        //    //GrandTotal
        //    MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal());

        //    //disable Total Paid & requset Payment
        //    string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalresubmitStart##-->", "<!--##@ContentTotalresubmitEnd##-->");
        //    MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

        //    //getDetail Voucher
        //    MainBody = MainBody.Replace("<!--##@ContentBodyDisplay##-->", TextDetailVoucher());
        //    return MainBody;
        //}

        //##3## Book now pay later mail resubmit ## Charge all Completed
        public string getMailSend_Booknow_fullCharge_Completed()
        {
            string MainBody = getMailTheme();

            //Booking IitemList
            string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
            MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId());

            //Detail Top
            string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(7));


            //CustomerName 
            MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

            //GrandTotal
            MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal());

            //disable Total Paid & requset Payment
            string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalresubmitStart##-->", "<!--##@ContentTotalresubmitEnd##-->");
            MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

            ////getDetail Voucher
            //MainBody = MainBody.Replace("<!--##@ContentBodyDisplay##-->", TextDetailVoucher());
            return MainBody;
        }
        //##6##
        //public string getMailSendVoucher_Booknow_fullCharge_Charge_decline()
        //{
        //    string MainBody = getMailTheme();

        //    //Booking IitemList
        //    string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
        //    MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId());

        //    //Detail Top
        //    string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
        //    MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(3));


        //    //CustomerName 
        //    MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

        //    //GrandTotal
        //    MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal());

        //    //disable Total Paid & requset Payment
        //    string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalresubmitStart##-->", "<!--##@ContentTotalresubmitEnd##-->");
        //    MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

        //    //getDetail Voucher
        //    MainBody = MainBody.Replace("<!--##@ContentBodyDisplay##-->", TextDetailVoucher());
        //    return MainBody;
        //}


        // Book now pay later mail resubmit 
        public string getMailResubmit_Booknow_online(int intPaymentId)
        {
            string MainBody = getMailTheme();


            //Booking IitemList
            string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
            MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId());

            //Detail Top
            string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(8));


            //CustomerName 
            MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

            ////disable grand Total
            //string KeywordTotalGrand = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalGrandStart##-->", "<!--##@ContentTotalGrandEnd##-->");
            //MainBody = MainBody.Replace(KeywordTotalGrand, " ");
            //GrandTotal
            MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal());


            //GrandPaid
            MainBody = MainBody.Replace("<!--##@mailItemTotalPaidContent##-->", GrandPaidTotal());

            //GrandRequestotal
            MainBody = MainBody.Replace("<!--##@mailItemTotalRequestContent##-->", GrandRequestTotal(intPaymentId));

            ////disable Total Paid & requset Payment
            //string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalresubmitStart##-->", "<!--##@ContentTotalresubmitEnd##-->");
            //MainBody = MainBody.Replace(KeywordTotalResubmit, " ");
           
            //get Button to MakePayment
            MainBody = MainBody.Replace("<!--##@ContentMakepayment##-->", getmakepaymentbutton_booknow(intPaymentId));
            //EmailTracking Is Open
            MainBody = MainBody.Replace("<!--##@ContentEmailtracking##-->", imgTrack(16));
            return MainBody;
        }

        public string getMailResubmit_Booknow_offline()
        {
            string MainBody = getMailTheme();


            //Booking IitemList
            string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
            MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId());

            //Detail Top
            string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(12));


            //CustomerName 
            MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

            //disable grand Total
            string KeywordTotalGrand = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalGrandStart##-->", "<!--##@ContentTotalGrandEnd##-->");
            MainBody = MainBody.Replace(KeywordTotalGrand, " ");
            ////GrandTotal
            //MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal());

            //disable Total Paid & requset Payment
            string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalresubmitStart##-->", "<!--##@ContentTotalresubmitEnd##-->");
            MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

            //get Button to MakePayment
            MainBody = MainBody.Replace("<!--##@ContentMakepayment##-->", getcardDetailInfo());


            //EmailTracking Is Open
            MainBody = MainBody.Replace("<!--##@ContentEmailtracking##-->", imgTrack(16));
            return MainBody;
        }

        public string getMailFullyBook()
        {
            string MainBody = getMailTheme();


            //Booking IitemList
            string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
            MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId());

            //Detail Top
            string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(9));


            //CustomerName 
            MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

            //disable grand Total
            string KeywordTotalGrand = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalGrandStart##-->", "<!--##@ContentTotalGrandEnd##-->");
            MainBody = MainBody.Replace(KeywordTotalGrand, " ");
            ////GrandTotal
            //MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal());

            //disable Total Paid & requset Payment
            string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalresubmitStart##-->", "<!--##@ContentTotalresubmitEnd##-->");
            MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

            //get Button to MakePayment
            MainBody = MainBody.Replace("<!--##@ContentMakepayment##-->", getDetailFoot(1));

            return MainBody;
        }


        public string getCancellationMail()
        {
            string MainBody = getMailTheme();


            //Booking IitemList
            string KeywordItem = Utility.GetKeywordReplace(MainBody, "<!--##@mailItemContentStart##-->", "<!--##@mailItemContentEnd##-->");
            MainBody = MainBody.Replace(KeywordItem, getProductListFromBookingId());

            //Detail Top
            string KeywordDetailTop = Utility.GetKeywordReplace(MainBody, "<!--##@WordingHeadContentStart##-->", "<!--##@WordingHeadContentEnd##-->");
            MainBody = MainBody.Replace(KeywordDetailTop, TextDetailTop(10));


            //CustomerName 
            MainBody = MainBody.Replace("<!--##@CusNameContent##-->", CusName());

            //disable grand Total
            string KeywordTotalGrand = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalGrandStart##-->", "<!--##@ContentTotalGrandEnd##-->");
            MainBody = MainBody.Replace(KeywordTotalGrand, " ");
            ////GrandTotal
            //MainBody = MainBody.Replace("<!--##@mailItemTotalContent##-->", GrandTotal());

            //disable Total Paid & requset Payment
            string KeywordTotalResubmit = Utility.GetKeywordReplace(MainBody, "<!--##@ContentTotalresubmitStart##-->", "<!--##@ContentTotalresubmitEnd##-->");
            MainBody = MainBody.Replace(KeywordTotalResubmit, " ");

            //get Button to MakePayment
            // MainBody = MainBody.Replace("<!--##@ContentMakepayment##-->", getcardDetailInfo());

            return MainBody;
        }

        public string getDetailFoot(byte TypeMail)
        {
            StringBuilder result = new StringBuilder();
            switch (TypeMail)
            {
                //fullybook and offer
                case 1:
                    result.Append("<tr><td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Please let us recommend;");
                    result.Append("</td></tr>");
                    result.Append("<tr><td style=\"height: 10px;\"></td></tr>");

                    result.Append("<tr><td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("<span style\"color:Red;\"><strong>Ramada Resort Karon Beach Phuket </strong> - New Hotel on Karon Beach near Woraburi Resort Phuket</span><br/>  http://www.hotels2thailand.com/phuket-hotels/ramada-resort-karon-beach-phuket.asp");
                    result.Append("</td></tr>");

                    result.Append("<tr><td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("<span style\"color:Red;\"><strong>Old Phuket Hotel</strong> - On the same beach, 50 Metres next to Woraburi Phuket</span> <br/> http://www.hotels2thailand.com/hotels_search.asp?page=1&keyword=old+phuket");
                    result.Append("</td></tr>");

                     result.Append("<tr><td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("<span style\"color:Red;\"><strong>Old Phuket Hotel</strong> - On the same beach, 50 Metres next to Woraburi Phuket</span> <br/> http://www.hotels2thailand.com/hotels_search.asp?page=1&keyword=old+phuket");
                    result.Append("</td></tr>");


                    result.Append("<tr><td style=\"height: 10px;\"></td></tr>");

                    result.Append("<tr><td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Your credit card will not be charged $1 until the booking is confirmed. Please kindly let us know how is your consideration of our alternative choices.");
                    result.Append("</td></tr>");
                    result.Append("<tr><td style=\"height: 10px;\"></td></tr>");

                    result.Append("<tr><td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("You can click the hotels link there will be much information, room rate, hotel class, and facilities for you and make a booking.");
                    result.Append("</td></tr>");
                    result.Append("<tr><td style=\"height: 10px;\"></td></tr>");

                    result.Append("<tr><td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
                    result.Append("Our price in the website is lower than that you book at the hotel on the day of arrival.\r\n  Website: http://www.hotels2thailand.com");
                    result.Append("</td></tr>");
                    result.Append("<tr><td style=\"height: 10px;\"></td></tr>");

                    result.Append("<tr><td style=\"height: 10px;\"></td></tr>");

                    break;
            }

            return result.ToString();
        }
        protected string getcardDetail()
        {
            StringBuilder result = new StringBuilder();
            int intBookingId = this.BookingId;
            string BookingIdEnCode = EncodeId(intBookingId);

            result.Append("<tr><td style=\"height:10px;text-align:center;\">");
            result.Append("<a href=\"http://www.hotels2thailand.com/cardRevise.aspx?bid=" + BookingIdEnCode + "\"><img src=\"http://www.hotels2thailand.com/theme_color/blue/images/button/make_payment.jpg\" style=\"border:0px;cursor:pointer;\" /></a>");
            //result.Append("");
            result.Append("</td></tr>");


            return result.ToString();
        }

        protected string getcardDetailInfo()
        {
            StringBuilder result = new StringBuilder();
            int intBookingId = this.BookingId;
            string BookingIdEnCode = EncodeId(intBookingId);

            result.Append("<tr><td style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; padding:10px 20px 5px 20px; color:#6d6e71;\">");
            result.Append("Other suggestion: Please use the different card to retry the payment yourself by click;");
            result.Append("</td></tr>");
            result.Append("<tr><td style=\"height: 10px;\"></td></tr>");
           

            result.Append("<tr><td style=\"height:10px;text-align:center;\">");
            result.Append("<a href=\"https://www.hotels2thailand.com/cardRevise.aspx?bid=" + BookingIdEnCode + "\"><img src=\"http://www.hotels2thailand.com/theme_color/blue/images/button/make_payment.jpg\" style=\"border:0px;cursor:pointer;\" /></a>");
            //result.Append("");
            result.Append("</td></tr>");
            result.Append("<tr><td style=\"height: 10px;\"></td></tr>");
           

            return result.ToString();
        }


        protected string getStarPicColor(byte bytStarClass)
        {
            string result = string.Empty;

            result = result + "<table cellpadding=\"0\" cellspacing=\"0\" >";
            result = result + "<tr>";
            for (int i = 0; i <= bytStarClass; i++)
            {
                result = result + "<td style=\"margin:0px;padding:0px;\" align=\"left\"><img src=\"http://www.booking2hotels.com/images_mail/star2.jpg\" alt=\"\" width=\"15\" height=\"19\" /></td>";
            }

            result = result + "</tr>";
            result = result + "</table>";

            return result;
        }

        protected string getStarPic(byte bytStarClass)
        {
            string result = string.Empty;

            result = result + "<table cellpadding=\"0\" cellspacing=\"0\" >";
            result = result + "<tr>";
            for (int i = 0; i < bytStarClass; i++)
            {
                result = result + "<td style=\"margin:0px;padding:0px;\" align=\"left\"><img src=\"http://www.booking2hotels.com/images_mail/star1.jpg\" alt=\"\" width=\"15\" height=\"19\" /></td>";
            }
               
            result = result + "</tr>";
            result = result + "</table>";
            
            return result;
        }
    }
}