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

        public string GetPaymentForm(PaymentInfo paymentInfo, IDictionary<string,string> FormVal = null)
        {
            string result = string.Empty;

            result = GetBankForm(paymentInfo, FormVal);
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
        public String getUTCDateTime()
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            return time.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
        }
        public String getUUID()
        {
            return System.Guid.NewGuid().ToString();
        }
        private string GetBankForm(PaymentInfo paymentInfo,IDictionary<string,string> FormVal = null)
        { 
            string bankForm="";
            string bookingBank = "";
            
                //Kbank
                if(paymentInfo.ManageID==1)
                {
                    //Krung Sri Cyber Source
                    if (paymentInfo.GatewayID == 15)
                    {
                        string param = "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,amount,currency";


                        string param2 = "bill_to_address_country,bill_to_address_city,bill_to_address_line1,bill_to_address_line2,device_fingerprint_id,bill_to_address_postal_code,bill_to_address_state,bill_to_company_name,bill_to_email,bill_to_forename,bill_to_phone,bill_to_surname,customer_ip_address,merchant_defined_data1,merchant_defined_data2,merchant_defined_data3,merchant_defined_data5,merchant_defined_data7,consumer_id,merchant_defined_data11,payment_method";
                        IDictionary<string, string> parameters = new Dictionary<string, string>();
                        parameters.Add("access_key", paymentInfo.Access_Key);
                        parameters.Add("profile_id", paymentInfo.ProfileID);
                        parameters.Add("transaction_uuid", getUUID());
                        parameters.Add("signed_field_names", param);
                        parameters.Add("locale", "en");
                        parameters.Add("signed_date_time", getUTCDateTime());
                        parameters.Add("transaction_type", "sale");
                        parameters.Add("reference_number", paymentInfo.PaymentBankID.ToString());
                        parameters.Add("amount", paymentInfo.Amount.ToString("#.00"));
                        parameters.Add("currency", "THB");
                        //parameters.Add("transaction_type", "sale");
                        parameters.Add("unsigned_field_names", param2);

                        parameters.Add("bill_to_address_country", FormVal["country"].Split(',')[1]);
                        parameters.Add("bill_to_address_city", FormVal["req_city"]);
                        parameters.Add("bill_to_address_line1", FormVal["req_address_1"]);
                        parameters.Add("bill_to_address_line2", FormVal["req_address_2"]);
                        parameters.Add("device_fingerprint_id", FormVal["Session_current_id"]);
                        parameters.Add("bill_to_address_postal_code", FormVal["req_postal_code"]);
                        parameters.Add("bill_to_address_state", FormVal["sel_drop_state"]);
                        parameters.Add("bill_to_company_name", "Booking2Hotel");
                        parameters.Add("bill_to_email", FormVal["email"]);
                        parameters.Add("bill_to_forename", FormVal["first_name"]);
                        parameters.Add("bill_to_phone", FormVal["phone"]);
                        parameters.Add("bill_to_surname", FormVal["last_name"]);
                        parameters.Add("customer_ip_address", FormVal["user_ip_address"]);
                        parameters.Add("merchant_defined_data1", FormVal["first_name"]);
                        parameters.Add("merchant_defined_data2", FormVal["last_name"]);

                        //send as Default to "Web" (Web/Mobile)
                       // parameters.Add("override_custom_receipt_page", "Web");

                        //country of passport
                        parameters.Add("merchant_defined_data3", paymentInfo.CountryTitle);
                        //Country of residence
                       // parameters.Add("merchant_defined_data4", paymentInfo.CountryTitle);

                        parameters.Add("consumer_id", "0");
                        //no: of days between booking and the check in days
                        int intDayDiffBooking = paymentInfo.Date_check_in.Subtract(paymentInfo.BookingSubmit).Days;
                        parameters.Add("merchant_defined_data5", intDayDiffBooking.ToString());
                        ////Destination Country
                        //parameters.Add("merchant_defined_data6", "Thailand");
                       // no: Duration of stay in days
                        int intDurationStay = paymentInfo.Date_check_out.Subtract(paymentInfo.Date_check_in).Days;    
                        parameters.Add("merchant_defined_data7", intDurationStay.ToString());

                        //parameters.Add("merchant_defined_data20", FormVal["email"]);
                        //parameters.Add("ship_to_forename", FormVal["first_name"]);
                        //parameters.Add("ship_to_surname", FormVal["last_name"]);


                        //Hotel category 3 star/5 star/budget
                        //parameters.Add("merchant_defined_data8", "3");
                        ////Hotel Destination
                        //parameters.Add("merchant_defined_data9", paymentInfo.DestinationTitle);
                        ////Hotel Name
                        //parameters.Add("merchant_defined_data10", paymentInfo.Producttitle);

                        //Language
                        parameters.Add("merchant_defined_data11", "ENG");

                        ////Login Category /Channel (by call/website)
                        //parameters.Add("merchant_defined_data12", "website");
                        ////no: of hotel rooms
                        //parameters.Add("merchant_defined_data13", "100");
                        ////No: of previous visit
                        //parameters.Add("merchant_defined_data14", "0");
                        //Saved Payment Method (Credit card, Debit Card, Electronic check debit, Paypal)
                        parameters.Add("payment_method", "card");
                        //Profile email address as registered during registeration sign up 
                        //parameters.Add("merchant_defined_data15", FormVal["email"]);
                        ////Profile Password hashed (if available this is the encrypted password that a user might provide)
                        //parameters.Add("merchant_defined_data16", "no");
                        ////Promotion Code
                        //parameters.Add("merchant_defined_data17", "no");
                        ////Service Provider name (Booked hotel name) If available
                        //parameters.Add("merchant_defined_data18", paymentInfo.Producttitle);
                        ////Third-Party booking/Agent Booking (if the credit card holder is not the party in the guest list then "Y" or/else "N"
                        //parameters.Add("merchant_defined_data19", "Y");

                        bankForm = bankForm + "<form name=\"CreditForm\" method=\"post\" action=\"https://testsecureacceptance.cybersource.com/pay\">\n";



                        foreach (KeyValuePair<string, string> keys in parameters)
                        {
                            bankForm = bankForm + "<input type=\"hidden\" id=\"" + keys.Key + "\" name=\"" + keys.Key + "\" value=\"" + keys.Value + "\"/>\n";
                           
                        }

                        bankForm = bankForm + "<input type=\"hidden\" id=\"signature\" name=\"signature\" value=\"" + secureacceptance.Security.sign(parameters, paymentInfo.Secret_Key.Trim()) + "\"/>\n";
                        


                        //bankForm = bankForm + "<input type=\"hidden\" id=\"signature\" name=\"signature\" value=\"" + secureacceptance.Security.sign(parameters, paymentInfo.Secret_Key.Trim()) + "\"/>";
    //<input type="hidden" name="unsigned_field_names" value="bill_to_address_country">
    //<input type="hidden" name="signed_date_time" value="<% Response.Write(getUTCDateTime()); %>">
    //<input type="hidden" name="locale" value="en">
    //<fieldset>
    //    <legend>Payment Details</legend>
    //    <div id="paymentDetailsSection" class="section">
    //        <span>transaction_type:</span><input type="text" name="transaction_type" size="25"><br/>
    //        <span>reference_number:</span><input type="text" name="reference_number" size="25"><br/>
    //        <span>amount:</span><input type="text" name="amount" size="25"><br/>
    //        <span>currency:</span><input type="text" name="currency" size="25"><br/>
    //    </div> 
    //</fieldset>

    //  <input type="hidden" name="bill_to_address_country" value="TH">


                        bankForm = bankForm + "</form>\n";
                    }
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

        public void BookingProcess(int BookingID, string bookingnow = "")
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


                                if (string.IsNullOrEmpty(bookingnow))
                                {
                                    //chang to mail voucher booknow
                                    mailBody = mailBookingRecieve.getMailSendVoucher();
                                }else
                                {
                                    mailBody = mailBookingRecieve.getMailSendVoucher_Booknow_offline();
                                }
                                //#Send Mail Voucher (Normal)
                                
                                
                                
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