using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.Front;
using Hotels2thailand;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;
using Hotels2thailand.Booking;
/// <summary>
/// Summary description for FrontPaymentMethod
/// </summary>
/// 
namespace Hotels2thailand.Front
{

    public class FrontPaymentMethod:Hotels2BaseClass
    {
        public int BookingPaymentID { get; set; }
        public byte BookingPaymentCate { get; set; }
        public byte Gateway { get; set; }
        public decimal Amount { get; set; }

        public FrontPaymentMethod()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string GetPaymentForm(PaymentInfo paymentInfo)
        {
            string result = string.Empty;

            result = GetBankForm(paymentInfo);
            return result;
        }

        public string GetPaymentFormByPaymentID(int BookingPaymentBankID)
        {
            string result = string.Empty;
            
            //using(SqlConnection cn=new SqlConnection(this.ConnectionString))
            //{
            //    string sqlCommand = "select gateway_id from tbl_booking_payment where booking_payment_id="+BookingPaymentID;
            //    SqlCommand cmd = new SqlCommand(sqlCommand, cn);
            //    cn.Open();
            //    byte gateway_id = (byte)cmd.ExecuteScalar();
            //    if(gateway_id==2)
            //    {
            //        //Krungsri
            //        BookingPaymentDisplay bookingPaymentHotel = new BookingPaymentDisplay();
            //        bookingPaymentHotel.InsertBookingPaymentBank(BookingPaymentID);
            //    }
            //}

            PaymentInfo paymentInfo = new PaymentInfo(BookingPaymentBankID);
            
            result = GetBankForm(paymentInfo.getPaymentInfo());
            return result;
        }

        private string GetBankForm(PaymentInfo paymentInfo)
        { 
            string bankForm="";
            string bookingBank = "";
            
                //Kbank
                if(paymentInfo.ManageID==1)
                {
                    if (paymentInfo.GatewayID == 3)
                    {

                        bankForm = bankForm + "<form name=\"CreditForm\" method=\"post\" action=\"https://rt05.kasikornbank.com/pgpayment/payment.aspx\">\n";
                        bankForm = bankForm + "<INPUT type=\"hidden\" id=\"MERCHANT2\" name=\"MERCHANT2\" value=\"" + paymentInfo.MerchantID + "\">\n";
                        bankForm = bankForm + "<INPUT type=\"hidden\" id=\"TERM2\" name=\"TERM2\" value=\"" + paymentInfo.TerminalID + "\">\n";
                        bankForm = bankForm + "<INPUT type=\"hidden\" id=\"AMOUNT2\" name=\"AMOUNT2\" value=\"" + InsertZero(paymentInfo.Amount) + "\">\n";
                        bankForm = bankForm + "<INPUT type=\"hidden\" id=\"URL2\" name=\"URL2\" value=\"" + paymentInfo.ResponseUrl + "\">\n";
                        bankForm = bankForm + "<INPUT type=\"hidden\" id=\"RESPURL\" name=RESPURL value=\"https://www.hotels2thailand.com/bk2thengine/kbank_update.aspx\">\n";
                        bankForm = bankForm + "<INPUT type=\"hidden\" id=\"IPCUST2\" name=\"IPCUST2\" value=\"\">\n";
                        bankForm = bankForm + "<INPUT type=\"hidden\" id=\"DETAIL2\" name=\"DETAIL2\" value=\"" + paymentInfo.WebsiteName + " Order\">\n";
                        bankForm = bankForm + "<INPUT type=\"hidden\" id=\"INVMERCHANT\" name=\"INVMERCHANT\" value=\"" + paymentInfo.PaymentBankID + "\">\n";
                        //bankForm = bankForm + "<INPUT type=\"hidden\" id=\"SHOPID\" name=\"SHOPID\" value=\"01\">\n";
                        bankForm = bankForm + "</form>\n";
                    }
                    
                }else{
                    if (paymentInfo.GatewayID == 3)
                    {
                        bankForm = bankForm + "<form name=\"CreditForm\" method=\"post\" action=\"https://rt05.kasikornbank.com/pgpayment/payment.aspx\">\n";
                        bankForm = bankForm + "<INPUT type=\"hidden\" id=\"MERCHANT2\" name=\"MERCHANT2\" value=\"401001110379001\">\n";
                        bankForm = bankForm + "<INPUT type=\"hidden\" id=\"TERM2\" name=\"TERM2\" value=\"70342389\">\n";
                        bankForm = bankForm + "<INPUT type=\"hidden\" id=\"AMOUNT2\" name=\"AMOUNT2\" value=\"" + InsertZero(paymentInfo.Amount) + "\">\n";
                        bankForm = bankForm + "<INPUT type=\"hidden\" id=\"URL2\" name=\"URL2\" value=\"http://www.booking2hotels.com/order-complete.aspx\">\n";
                        bankForm = bankForm + "<INPUT type=\"hidden\" id=\"RESPURL\" name=RESPURL value=\"https://www.hotels2thailand.com/bk2thengine/kbank_update.aspx\">\n";
                        bankForm = bankForm + "<INPUT type=\"hidden\" id=\"IPCUST2\" name=\"IPCUST2\" value=\"\">\n";
                        bankForm = bankForm + "<INPUT type=\"hidden\" id=\"DETAIL2\" name=\"DETAIL2\" value=\"" + paymentInfo.WebsiteName + " Order\">\n";
                        bankForm = bankForm + "<INPUT type=\"hidden\" id=\"INVMERCHANT\" name=\"INVMERCHANT\" value=\"" + paymentInfo.PaymentBankID + "\">\n";
                        //bankForm = bankForm + "<INPUT type=\"hidden\" id=\"SHOPID\" name=\"SHOPID\" value=\"01\">\n";
                        bankForm = bankForm + "</form>\n";
                    }else{
                        //Mail order
                        bookingBank = "0000000000";
                        bookingBank = bookingBank + paymentInfo.PaymentBankID.ToString();
                        bookingBank = bookingBank.StringRight(12);

                        bankForm = bankForm + " <form name=\"CreditForm\" action=\"https://ipay.bangkokbank.com/b2c/eng/payment/payForm.jsp\" method=\"post\">\n";
                        bankForm = bankForm + "<input type=\"hidden\" name=\"merchantId\" value=\"2517\" />\n";
                        bankForm = bankForm + "<input type=\"hidden\" name=\"amount\" value=\"" + paymentInfo.Amount.ToString("#.00") + "\" />\n";
                        bankForm = bankForm + "<input type=\"hidden\" name=\"orderRef\" value=\"" + bookingBank + "\" />\n";
                        bankForm = bankForm + "<input type=\"hidden\" name=\"currCode\" value=\"764\" />\n";
                        bankForm = bankForm + "<input type=\"hidden\" name=\"successUrl\" value=\"http://www.booking2hotels.com/order-complete.aspx\" />\n";
                        bankForm = bankForm + "<input type=\"hidden\" name=\"failUrl\" value=\"http://www.booking2hotels.com/order-complete.aspx\" />\n";
                        bankForm = bankForm + "<input type=\"hidden\" name=\"cancelUrl\" value=\"http://www.booking2hotels.com/order-complete.aspx\" />\n";
                        bankForm = bankForm + "<input type=\"hidden\" name= \"remark\" value= \"-\">\n";
                        bankForm = bankForm + "<input type=\"hidden\" name=\"payType\" value=\"H\" />\n";
                        bankForm = bankForm + "<input type=\"hidden\" name=\"lang\" value=\"E\" />\n";
                        bankForm = bankForm + "</form>\n";
                       // HttpContext.Current.Response.Write(bankForm);
                        //HttpContext.Current.Response.End();
                    }
                    

            }

            if(paymentInfo.GatewayID ==9)
            {
                //Krungsri
                bookingBank = "000000000000";
                bookingBank = bookingBank + paymentInfo.PaymentBankID.ToString();
                bookingBank = bookingBank.StringRight(9);

			    bankForm=bankForm+"<form name=\"CreditForm\" method=\"post\" action=\"https://www.krungsriepay.com/webapp/PaymentManager/PaymentInput\">\n";
			    bankForm=bankForm+"<INPUT type=\"hidden\" id=\"MERCHANTNUMBER\" name=\"MERCHANTNUMBER\" value=\""+paymentInfo.MerchantID+"\">\n";
			    bankForm=bankForm+"<INPUT type=\"hidden\" id=\"ORDERNUMBER\" name=\"ORDERNUMBER\" value=\""+bookingBank+"\">\n";
			    bankForm=bankForm+"<INPUT type=\"hidden\" id=\"PAYMENTTYPE\" name=\"PAYMENTTYPE\" value=\"CreditCard\">\n";
			    bankForm=bankForm+"<INPUT type=\"hidden\" id=\"AMOUNT\" name=\"AMOUNT\" value=\""+(int)(paymentInfo.Amount*100)+"\">\n";
			    bankForm=bankForm+"<INPUT type=\"hidden\" id=\"CURRENCY\" name=\"CURRENCY\" value=\"764\">\n";
			    bankForm=bankForm+"<INPUT type=\"hidden\" id=\"AMOUNTEXP10\" name=\"AMOUNTEXP10\" value=\"-2\">\n";
			    bankForm=bankForm+"<INPUT type=\"hidden\" id=\"LANGUAGE\" name=\"LANGUAGE\" value=\"EN\">\n";
                bankForm = bankForm + "<INPUT type=\"hidden\" id=\"REF1\" name=\"REF1\" value=\"" + paymentInfo.PaymentBankID + "\">\n";
			    bankForm=bankForm+"<INPUT type=\"hidden\" id=\"REF2\" name=\"REF2\" value=\"\">\n";
			    bankForm=bankForm+"<INPUT type=\"hidden\" id=\"REF3\" name=\"REF3\" value=\"\">\n";
			    bankForm=bankForm+"<INPUT type=\"hidden\" id=\"REF4\" name=\"REF4\" value=\"\">\n";
			    bankForm=bankForm+"<INPUT type=\"hidden\" id=\"REF5\" name=\"REF5\" value=\"\">\n";
			    bankForm=bankForm+"</form>\n";
            }

            if(paymentInfo.GatewayID==10)
            {
                bankForm = bankForm + " <Form name=\"CreditForm\" action=\""+paymentInfo.UrlRedirect+"\" method=\"post\">\n";
              bankForm = bankForm + " <input name=\"mid\" type=\"hidden\" id=\"mid\" value=\""+paymentInfo.MerchantID+"\" />\n";
              bankForm = bankForm + " <input name=\"terminal\" type=\"hidden\" id=\"terminal\" value=\""+paymentInfo.TerminalID+"\" />\n";
              bankForm = bankForm + " <input name=\"version\" type=\"hidden\" id=\"version\" value=\"2_5_1\" />\n";
              bankForm = bankForm + " <input name=\"command\" type=\"hidden\" id=\"command\" value=\"CRAUTH\" />\n";
              bankForm = bankForm + " <input name=\"ref_no\" type=\"hidden\" id=\"ref_no\" value=\"" + paymentInfo.PaymentBankID + "\" />\n";
              bankForm = bankForm + " <input name=\"ref_date\" type=\"hidden\" id=\"ref_date\" value=\""+DateTime.Now.DateToStringYMD().Replace("-","")+"\" />\n";
              bankForm = bankForm + " <input name=\"service_id\" type=\"hidden\" id=\"service_id\" value=\"13\" />\n";
              if(paymentInfo.CurrencyID==25){
                bankForm = bankForm + " <input name=\"cur_abbr\" type=\"hidden\" id=\"cur_abbr\" value=\"THB\" />\n";
              }else{
                bankForm = bankForm + " <input name=\"cur_abbr\" type=\"hidden\" id=\"cur_abbr\" value=\"USD\" />\n";
              }
              bankForm = bankForm + " <input name=\"amount\" type=\"hidden\" id=\"amount\" value=\""+paymentInfo.Amount+"\" />\n";
              bankForm = bankForm + " <input name=\"backURL\" type=\"hidden\" id=\"backURL\" value=\""+paymentInfo.ResponseUrl+"\" />\n";
              bankForm = bankForm + " </Form>\n";
            }

            if (paymentInfo.GatewayID == 5)
            {
                bookingBank = "000000000000";
                bookingBank = bookingBank + paymentInfo.PaymentBankID.ToString();
                bookingBank = bookingBank.StringRight(12);

                bankForm = bankForm + " <form name=\"CreditForm\" action=\"https://ipay.bangkokbank.com/b2c/eng/payment/payForm.jsp\" method=\"post\">\n";
                bankForm = bankForm + "<input type=\"hidden\" name=\"merchantId\" value=\""+paymentInfo.MerchantID+"\" />\n";
                bankForm = bankForm + "<input type=\"hidden\" name=\"amount\" value=\"" + paymentInfo.Amount.ToString("0.00") + "\" />\n";
                bankForm = bankForm + "<input type=\"hidden\" name=\"orderRef\" value=\"" + bookingBank + "\" />\n";
                bankForm = bankForm + "<input type=\"hidden\" name=\"currCode\" value=\"764\" />\n";
                bankForm = bankForm + "<input type=\"hidden\" name=\"successUrl\" value=\""+paymentInfo.ResponseUrl+"\" />\n";
                bankForm = bankForm + "<input type=\"hidden\" name=\"failUrl\" value=\"" + paymentInfo.ResponseUrl + "\" />\n";
                bankForm = bankForm + "<input type=\"hidden\" name=\"cancelUrl\" value=\"" + paymentInfo.ResponseUrl + "\" />\n";
                bankForm = bankForm + "<input type=\"hidden\" name= \"remark\" value= \"-\">\n";
                bankForm = bankForm + "<input type=\"hidden\" name=\"payType\" value=\"A\" />\n";
                bankForm = bankForm + "<input type=\"hidden\" name=\"lang\" value=\"E\" />\n";
                bankForm = bankForm + "</form>\n";
                //HttpContext.Current.Response.Write(bankForm);
                //HttpContext.Current.Response.End();
            }

            


            if(paymentInfo.GatewayID==12)
            {
                //TMB

              

                bookingBank = "000000000000";
                bookingBank = bookingBank + paymentInfo.PaymentBankID.ToString();
                bookingBank = bookingBank.StringRight(12);

                bankForm = bankForm + " <form name=\"CreditForm\" action=\"https://tmbepgw.tmbbank.com/TMBPayment/Payment.aspx\" method=\"post\">\n";
			    bankForm = bankForm + " <input type=\"hidden\" id=\"MERID\" name=\"MERID\" value=\""+paymentInfo.MerchantID+"\" />\n";
			    bankForm = bankForm + " <input type=\"hidden\" id=\"TERMINALID\" name=\"TERMINALID\" value=\""+paymentInfo.TerminalID+"\" />\n";
			    bankForm = bankForm + " <input type=\"hidden\" id=\"AMOUNT\" name=\"AMOUNT\" value=\""+("000000000000"+(int)paymentInfo.Amount+"00").StringRight(12)+"\" />\n";
                bankForm = bankForm + " <input type=\"hidden\" id=\"BACKENDURL\" name=\"BACKENDURL\" value=\"" + paymentInfo.BackgroundUrl + "\" />\n";
			    bankForm = bankForm + " <input type=\"hidden\" id=\"RESPONSEURL\" name=\"RESPONSEURL\" value=\""+paymentInfo.ResponseUrl+"\" />\n";
			    bankForm = bankForm + " <input type=\"hidden\" id=\"MERCHANTDATA\" name=\"MERCHANTDATA\" value=\"Booking No."+paymentInfo.BookingHotelID+"\" />\n";
			    bankForm = bankForm + " <input type=\"hidden\" id=\"INVOICENO\" name=\"INVOICENO\" value=\""+bookingBank+"\" />\n";
			    bankForm = bankForm + " <input type=\"hidden\" id=\"CURRENCYCODE\" name=\"CURRENCYCODE\" value=\"764\" />\n";
			    bankForm = bankForm + " <input type=\"hidden\" id=\"VERSION\" name=\"VERSION\" value=\"1.0\" />\n";
			    bankForm = bankForm + " </form>\n";
            }

            

            

            string HeadForm = "<html>\n";
            HeadForm = HeadForm + "<body onload=\"CreditForm.submit()\">\n";
            //HeadForm = HeadForm + "<body>";
            HeadForm = HeadForm + bankForm;
            HeadForm = HeadForm+ "</body></html>\n";
            return HeadForm;
        }

        public string InsertZero(decimal price)
        {
            price = price * 100;
            return ("00000000000" +Convert.ToInt32(price).ToString()).StringRight(12);
        }


        public string ReadTMBUrlReturn(string urlReturn, byte typeReturn)
        {
            string strResult = "";
            switch (typeReturn)
            {
                case 1://### Check Result ###
                    strResult = urlReturn.Substring(1, 2);
                    break;
                case 2://### Invoice ID ###
                    strResult = urlReturn.Substring(56, 12);
                    break;
                case 3://### Timestamp ###
                    strResult = urlReturn.Substring(68, 14);
                    break;
                case 4://### Amount ###
                    strResult = urlReturn.Substring(82, 12);
                    break;
                case 5://### Card Type ###
                    strResult = urlReturn.Substring(94, 20);
                    break;
                case 6://### Credit Card ###
                    strResult = urlReturn.Substring(114, 16);
                    break;
            }
            return strResult;
        }

        public string ReadPaysbuyUrlReturn(string urlReturn, byte typeReturn)
        {
            string strResult = "";
            switch (typeReturn)
            {
                case 1://### Check Result ###
                    strResult = urlReturn.Substring(1, 2);
                    break;
                case 2://### Invoice ID ###
                    strResult = urlReturn.Substring(3, urlReturn.Length);
                    break;
            }
            return strResult;
        }

        public string ReadKbankUrlReturn(string urlReturn, byte typeReturn)
        {
            string strResult = "";
            switch (typeReturn)
            {
                case 1://### Response Code ###
                    strResult = urlReturn.Substring(0, 2);
                    break;
                case 2://### Reference ###
                    strResult = urlReturn.Substring(2, 12);
                    break;
                case 3://### Authorize ###
                    strResult = urlReturn.Substring(14, 6);
                    break;
                case 4://### UAID ###
                    strResult = urlReturn.Substring(20, 36);
                    break;
                case 5://### Invoice (Order ID) ###
                    strResult = urlReturn.Substring(56, 12);
                    break;
                case 6://### Timestamp ###
                    strResult = urlReturn.Substring(68, 14);
                    break;
                case 7://### Amount ###
                    strResult = urlReturn.Substring(82, 12);
                    break;
                case 8://### Checksum ###
                    strResult = urlReturn.Substring(94, 40);
                    break;
                case 9://### Card Type ###
                    strResult = urlReturn.Substring(134, 20);
                    break;
                case 10://### ChecksumCard2 ###
                    strResult = urlReturn.Substring(154, 40);
                    break;
            }
            return strResult;
        }

        public void BookingProcess(int BookingID)
        {
            string sqlCommand = "";
            

            //Check Extranet
            DataConnect objConn = new DataConnect();

            sqlCommand = "select b.payment_type_id,b.email,p.cat_id,bp.product_id,p.extranet_active";
            sqlCommand = sqlCommand + " from tbl_booking_product bp,tbl_product p,tbl_booking b";
            sqlCommand = sqlCommand + " where b.booking_id=bp.booking_id and bp.product_id=p.product_id and p.cat_id<>31 and bp.booking_id=" + BookingID;

            SqlDataReader reader = objConn.GetDataReader(sqlCommand);
            BookingMailEngine mailBookingRecieve = new BookingMailEngine(BookingID);
            string mailBody = string.Empty;
            string enParam = string.Empty;
            byte paymentType = 1;
            string bccMail = string.Empty;
            string subjectMail = string.Empty;
            string mailStaffList = string.Empty;

            if (reader.Read())
            {
                
                if ((bool)reader["extranet_active"])
                {
                    //Is Extranet
                    //Check Allotment 
                    if ((byte)reader["cat_id"] == 29)
                    {
                        FrontAllotmentUpdate objAllotUpdate = new FrontAllotmentUpdate();
                        if (objAllotUpdate.UpdateAllotment(BookingID))
                        {
                            
                            //Has Allotment
                            sqlCommand = "update tbl_booking set status_extranet_id=1 where booking_id=" + BookingID;
                            objConn.ExecuteScalar(sqlCommand);
                            if (paymentType == 2)
                            {
                                //book now paylater
                                //#Send Mail Voucher (Book now)
                                
                                mailBody = mailBookingRecieve.getMailSendVoucher_Booknow();
                                Hotels2MAilSender.SendmailBooking("reservation@hotels2thailand.com", reader["email"].ToString(), "Reservation confirm from booking2hotels.com (ORDER ID:" + BookingID + ")", "sent@hotels2thailand.com;sent2@hotels2thailand.com", mailBody);
                            }
                            else
                            {
                                
                                //#Send Mail Voucher (Normal)
                                mailBody = mailBookingRecieve.getMailSendVoucher();
                                
                                
                                //HttpContext.Current.Response.End();
                                subjectMail = mailBookingRecieve.GetSubject(MailCat.ComfirmBooking);
                                bccMail = mailBookingRecieve.Bcc;
                                BookingStaff objStaff = new BookingStaff();
                                foreach (string staffHotel in objStaff.GetStringEmailSendMail(BookingID))
                                {
                                    //Response.Write(staffHotel+"<br/>");

                                    mailStaffList = mailStaffList + staffHotel + ";";
                                }
                                mailStaffList = mailStaffList.StringLeft(mailStaffList.Count() - 1);
                                //HttpContext.Current.Response.Write(bccMail + ";" + mailStaffList);
                                //HttpContext.Current.Response.Flush();
                                Hotels2MAilSender.SendmailBooking(mailBookingRecieve.cProductBookingEngine.Email,mailBookingRecieve.MailNameDisplayDefault, reader["email"].ToString(), subjectMail,"", mailBody);
                                if ((int)reader["product_id"] == 3605)
                                {
                                    string mailBodyStaff = mailBookingRecieve.getMailSendVoucher("supplier_price_show");
                                    Hotels2MAilSender.SendmailBooking(mailBookingRecieve.cProductBookingEngine.Email, mailBookingRecieve.MailNameDisplayDefault, bccMail + ";" + mailStaffList, subjectMail, "", mailBodyStaff);
                                }
                                else {
                                    Hotels2MAilSender.SendmailBooking(mailBookingRecieve.cProductBookingEngine.Email, mailBookingRecieve.MailNameDisplayDefault, bccMail + ";" + mailStaffList, subjectMail, "", mailBody);
                                }

                            }

                            sqlCommand = "update tbl_booking set status_extranet_id=1 where booking_id=" + BookingID;
                            objConn.ExecuteScalar(sqlCommand);

                            sqlCommand = "update tbl_booking_item set status_use_allotment=1 where booking_id=" + BookingID;
                            objConn.ExecuteScalar(sqlCommand);

                            sqlCommand = "insert into tbl_booking_confirm (booking_id,confirm_cat_id,date_submit,status) values(" + BookingID + ",4," + DateTime.Now.Hotels2ThaiDateTime().Hotels2DateToSQlString() + ",1)";

                            objConn.ExecuteScalar(sqlCommand);

                            //BookingAcknowledge objBooking = new BookingAcknowledge();
                            //objBooking.BookingID = BookingID;
                            //objBooking.StatusExtranetID = 1;
                            //objBooking.Insert(objBooking);

                            //#Send Mail to Hotel (Acknowledge)
                            //MailAcknowledge mailStaff = new MailAcknowledge();
                            //mailStaff.SendMailToHotelStaff((int)reader["product_id"], BookingID, 1);
                        }
                        else
                        {
                            if ((byte)reader["payment_type_id"] == 1)
                            {
                                mailBookingRecieve.SendMailBookingRecevied_allot();
                            }
                        }

                    }
                }
                else {
                    mailBookingRecieve.SendMailBookingRecevied_allot();
                }
            }
            reader.Close();
            objConn.Close();
        }
    }
}