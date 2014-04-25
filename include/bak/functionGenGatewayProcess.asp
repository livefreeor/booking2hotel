<%
FUNCTION functionGenGatewayProcess (intOrderID,intRealID,intPriceTotal,intPricePayment)
 
	Dim sqlDetail
	Dim recDetail
	Dim arrDetail
	Dim strGateway
	Dim strCurrencyGateWay
	 
	 sqlDetail = "SELECT h.hotel_id,o.gateway_id,h.url_return,url_update,h.hotel_code,h.title,o.currency_id,h.merchant_id,h.merchant_terminal_id,h.url_site_redirect"
	 sqlDetail = sqlDetail & " FROM tbl_hotel h, tbl_order o"
	 sqlDetail = sqlDetail & " WHERE o.hotel_id=h.hotel_id AND o.order_id=" & intOrderID

	Set recDetail = Server.CreateObject ("ADODB.Recordset")
	recDetail.Open sqlDetail, Conn,AdOpenForwardOnly,AdLockReadOnly
		arrDetail = recDetail.GetRows()
	recDetail.Close
	Set recDetail = Nothing

	SELECT CASE arrDetail(1,0)
		Case 1,6'# Kbank #
			'#--Currency Gateway
			'SELECT CASE arrDetail(6,0)
'				Case 1 'USD
'					strCurrencyGateWay = "70340591"
'				Case 25 'Thai Baht
'					strCurrencyGateWay = "70340592"
'			END SELECT
			'#-------------------
			strCurrencyGateWay=arrDetail(8,0)
			strGateWay ="<html>"
			strGateWay = strGateWay & "<body onLoad=""CreditForm.submit()"">"
			'strGateWay = strGateWay & "<body>"
			strGateWay = strGateWay & "<form name=""CreditForm"" method=""post"" action=""https://rt05.kasikornbank.com/pgpayment/payment.aspx"">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""MERCHANT2"" name=""MERCHANT2"" value="""&arrDetail(7,0)&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""TERM2"" name=""TERM2"" value="""&strCurrencyGateWay&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""AMOUNT2"" name=""AMOUNT2"" value="""&functionKbankGenPrice(intPricePayment)&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""URL2"" name=""URL2"" value="""&arrDetail(2,0)&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""RESPURL"" name=RESPURL value="""&arrDetail(3,0)&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""IPCUST2"" name=""IPCUST2"" value=""208.106.234.126"">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""DETAIL2"" name=""DETAIL2"" value="""&arrDetail(5,0)&" Booking"">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""INVMERCHANT"" name=""INVMERCHANT"" value="""&left("0000000"&intRealID,12)&""">" & VbCrlf
			strGateWay = strGateWay & "</form>" & VbCrlf
			strGateWay = strGateWay & "</body>" & VbCrlf
			strGateWay = strGateWay & "</html>" & VbCrlf

		Case 2 '# Krungsri #
			strGateWay ="<html>"
			'strGateWay = strGateWay & "<body>"
			strGateWay = strGateWay & "<body onLoad=""CreditForm.submit()"">"&vbcrlf
			'strGateWay = strGateWay & "<form name=""CreditForm"" method=""post"" action=""https://www.krungsriepay.com/webapp/PaymentManager/PaymentInput"">"&vbcrlf
			'strGateWay = strGateWay & "<INPUT type=""hidden"" id=""MERCHANTNUMBER"" name=""MERCHANTNUMBER"" value=""950091173"">"&vbcrlf
			strGateWay = strGateWay & "<form name=""CreditForm"" method=""post"" action=""https://www.krungsriepay.com/webapp/PaymentManager/PaymentInput"">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""MERCHANTNUMBER"" name=""MERCHANTNUMBER"" value="""&arrDetail(7,0)&""">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""ORDERNUMBER"" name=""ORDERNUMBER"" value="""&right("00000"&int(intRealID),9)&""">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""PAYMENTTYPE"" name=""PAYMENTTYPE"" value=""CreditCard"">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""AMOUNT"" name=""AMOUNT"" value="""&function_krungsri_gen_price(intPricePayment)&""">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""CURRENCY"" name=""CURRENCY"" value=""764"">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""AMOUNTEXP10"" name=""AMOUNTEXP10"" value=""-2"">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""LANGUAGE"" name=""LANGUAGE"" value=""EN"">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""REF1"" name=""REF1"" value="""&int(intRealID)&""">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""REF2"" name=""REF2"" value="""">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""REF3"" name=""REF3"" value="""">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""REF4"" name=""REF4"" value="""">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""REF5"" name=""REF5"" value="""">"&vbcrlf
			strGateWay = strGateWay & "</form>"&vbcrlf
			strGateWay = strGateWay & "</body>" & VbCrlf
			strGateWay = strGateWay & "</html>" & VbCrlf
		Case 3 '# SCB #
			Dim rsCredit
			Dim sqlCredit
			Set rsCredit=server.CreateObject("adodb.recordset")
			sqlCredit="select * from tbl_order_credit where order_id="&intOrderID
			rsCredit.open sqlCredit,conn,1,3
			
			Dim date_ref
			date_ref=year(now)&month(now)&day(now)&hour(now)&minute(now)&second(now)
			SELECT CASE arrDetail(6,0)
				Case 1 'USD
					strCurrencyGateWay = "USD"
				Case 25 'Thai Baht
					strCurrencyGateWay = "THB"
			END SELECT
			strGateWay ="<html>"
			strGateWay = strGateWay & "<body onLoad=""CreditForm.submit()"">"
			'strGateWay = strGateWay & "<body>"
			'strGateWay = strGateWay & "<form name=""CreditForm"" method=""get"" action=""https://sips-test.scb.co.th"">" & VbCrlf
			strGateWay = strGateWay & "<form name=""CreditForm"" method=""post"" action="""&arrDetail(9,0)&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""mid"" name=""mid"" value="""&arrDetail(7,0)&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""terminal"" name=""terminal"" value="""&arrDetail(8,0)&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""version"" name=""version"" value=""2_5_1"">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""command"" name=""command"" value=""CRAUTH"">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""ref_no"" name=""ref_no"" value="""&intRealID&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""ref_date"" name=""ref_date"" value="""&date_ref&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""service_id"" name=""service_id"" value=""13"">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""cur_abbr"" name=""cur_abbr"" value="""&strCurrencyGateWay&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""amount"" name=""amount""  value="""&intPricePayment&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""cust_lname"" name=""cust_lname""  value="""&rsCredit("cus_lname")&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""cust_mname"" name=""cust_mname""  value="""&rsCredit("cus_mname")&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""cust_fname"" name=""cust_fname""  value="""&rsCredit("cus_fname")&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""cust_email"" name=""cust_email""  value="""&rsCredit("cus_email")&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""cust_country"" name=""cust_country""  value="""&trim(function_get_country_code(rsCredit("cus_country")))&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""cust_address1"" name=""cust_address1""  value="""&rsCredit("cus_address")&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""cust_city"" name=""cust_city""  value="""&rsCredit("cus_city")&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""cust_province"" name=""cust_province""  value="""&rsCredit("cus_province")&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""cust_zip"" name=""cust_zip""  value="""&rsCredit("cus_zip")&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""backURL"" name=""backURL""  value="""&arrDetail(2,0)&""">" & VbCrlf
			strGateWay = strGateWay & "</form>" & VbCrlf
			strGateWay = strGateWay & "</body>" & VbCrlf
			strGateWay = strGateWay & "</html>" & VbCrlf
		Case 4 'Bangkok Bank
			strGateWay ="<html>"
			strGateWay = strGateWay & "<body onLoad=""CreditForm.submit()"">"
			'strGateWay = strGateWay & "<body>"
			strGateWay = strGateWay & "<form name=""CreditForm"" action="" https://ipay.bangkokbank.com/b2c/eng/payment/payForm.jsp"" method=""post"">"
			strGateWay = strGateWay & "<input type=""hidden"" name=""merchantId"" value="""&arrDetail(7,0)&""" />"
			strGateWay = strGateWay & "<input type=""hidden"" name=""amount"" value="""&intPricePayment&""" />"
			strGateWay = strGateWay & "<input type=""hidden"" name=""orderRef"" value="""&right("00000000"&int(intRealID),12)&""" />"
			strGateWay = strGateWay & "<input type=""hidden"" name=""currCode"" value=""764"" />"
			strGateWay = strGateWay & "<input type=""hidden"" name=""successUrl"" value="""&arrDetail(2,0)&"?bkbresp=0"" />"
			strGateWay = strGateWay & "<input type=""hidden"" name=""failUrl"" value="""&arrDetail(2,0)&"?bkbresp=1"" />"
			strGateWay = strGateWay & "<input type=""hidden"" name=""cancelUrl"" value="""&arrDetail(2,0)&"?bkbresp=2"" />"
			strGateWay = strGateWay & "<input type=""hidden"" name= ""remark"" value= ""-"">"
			strGateWay = strGateWay & "<input type=""hidden"" name=""payType"" value=""A"" />"
			strGateWay = strGateWay & "<input type=""hidden"" name=""lang"" value=""E"" />"
			strGateWay = strGateWay & "</form>"
			strGateWay = strGateWay & "</body>" & VbCrlf
			strGateWay = strGateWay & "</html>" & VbCrlf
		Case 5 'Paypal
			Dim sqlUSD
			Dim intUSD
			Dim intPriceTotalPaypal
			
			sqlUSD = "SELECT prefix FROM tbl_currency WHERE currency_id=1"
			intUSD = conn.Execute(sqlUSD).GetString
			intUSD = Left(intUSD,Len(intUSD)-1)
			intPriceTotalPaypal = intPricePayment/intUSD
			
			strGateWay ="<html>"
			'strGateWay = strGateWay & "<body>"&vbcrlf
			strGateWay = strGateWay & "<body onLoad=""CreditForm.submit()"">"
			strGateWay = strGateWay & "<form name=""CreditForm"" action=""https://www.paypal.com/cgi-bin/webscr"" method=""post"">" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" name=""cmd"" value=""_xclick"">" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" name=""business"" value=""paypal@hotels2thailand.com"">" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" name=""item_name"" value="""&arrDetail(5,0)&" Booking"">" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" name=""item_number"" value="""&int(intRealID)&""">" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" name=""amount"" value="""&Cint(intPriceTotalPaypal)&""">" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" name=""page_style"" value=""Primary"">" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" name=""no_shipping"" value=""1"">" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" name=""return"" value="""&arrDetail(3,0)&""">" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" name=""no_note"" value=""1"">" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" name=""currency_code"" value=""USD"">" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" name=""bn"" value=""PP-BuyNowBF"">" & VbCrlf
			strGateWay = strGateWay & "</form>" & VbCrlf
			strGateWay = strGateWay & "</body>" & VbCrlf
			strGateWay = strGateWay & "</html>" & VbCrlf
			'response.write strGateWay
			
		Case 7 '# TMB
			intRealID = int(intRealID)
			strGateWay ="<html>" & VbCrlf
			'strGateWay = strGateWay & "<body>"&vbcrlf
			strGateWay = strGateWay & "<body onLoad=""CreditForm.submit()"">" & VbCrlf
			strGateWay = strGateWay & "<form name=""CreditForm"" action=""https://tmbepgw.tmbbank.com/TMBPayment/Payment.aspx"" method=""post"">" & VbCrlf
			'strGateWay = strGateWay & "<form name=""CreditForm"" action=""https://61.19.241.6/tmbpayment/payment.aspx "" method=""post"">" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" id=""MERID"" name=""MERID"" value="""&arrDetail(7,0)&""" />" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" id=""TERMINALID"" name=""TERMINALID"" value="""&arrDetail(8,0)&""" />" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" id=""AMOUNT"" name=""AMOUNT"" value="""&right("00000000"&intPricePayment&"00",12)&""" />" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" id=""BACKENDURL"" name=""BACKENDURL"" value="""&arrDetail(3,0)&""" />" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" id=""RESPONSEURL"" name=""RESPONSEURL"" value="""&arrDetail(2,0)&""" />" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" id=""MERCHANTDATA"" name=""MERCHANTDATA"" value=""Booking No."&intRealID&""" />" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" id=""INVOICENO"" name=""INVOICENO"" value="""&right("0000000000"&cstr(intRealID),12)&""" />" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" id=""CURRENCYCODE"" name=""CURRENCYCODE"" value=""764"" />" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" id=""VERSION"" name=""VERSION"" value=""1.0"" />" & VbCrlf
			strGateWay = strGateWay & "</form>" & VbCrlf
			strGateWay = strGateWay & "</body>" & VbCrlf
			strGateWay = strGateWay & "</html>" & VbCrlf
		Case 8 '# Paysbuy
			intRealID = int(intRealID)
			strGateWay ="<html>" & VbCrlf
			'strGateWay = strGateWay & "<body>"&vbcrlf
			strGateWay = strGateWay & "<body onLoad=""CreditForm.submit()"">" & VbCrlf
			strGateWay = strGateWay & "<form method=""post"" action=""https://www.paysbuy.com/paynow.aspx"" name=""CreditForm"">" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" name=""psb"" value=""psb"" />" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" name=""biz"" value=""visa@hotels2thailand.com"" />" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" name=""inv"" value="""&intRealID&""" />" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" name=""itm"" value="""" />" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" name=""amt"" value=""psb"" />" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" name=""reqURL"" value="""&arrDetail(3,0)&""" />" & VbCrlf
			strGateWay = strGateWay & "<input type=""hidden"" name=""postURL"" value="""&arrDetail(2,0)&""" />" & VbCrlf
			strGateWay = strGateWay & "</form>" & VbCrlf
			strGateWay = strGateWay & "</body>" & VbCrlf
			strGateWay = strGateWay & "</html>" & VbCrlf
		Case 49 '# Krungsri Test#
			strGateWay ="<html>" & VbCrlf
			'strGateWay = strGateWay & "<body>"
			strGateWay = strGateWay & "<body onLoad=""CreditForm.submit()"">"&vbcrlf
			strGateWay = strGateWay & "<form name=""CreditForm"" method=""post"" action=""https://www.krungsriepayment.net/EPayDefaultWeb/PaymentManager/PaymentInput.do"">"&vbcrlf
			'strGateWay = strGateWay & "<INPUT type=""hidden"" id=""MERCHANTNUMBER"" name=""MERCHANTNUMBER"" value=""950091173"">"&vbcrlf
			'strGateWay = strGateWay & "<form name=""CreditForm"" method=""post"" action=""https://www.krungsriepay.com/webapp/PaymentManager/PaymentInput"">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""MERCHANTNUMBER"" name=""MERCHANTNUMBER"" value="""&arrDetail(7,0)&""">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""ORDERNUMBER"" name=""ORDERNUMBER"" value="""&right("00000"&int(intRealID),9)&""">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""PAYMENTTYPE"" name=""PAYMENTTYPE"" value=""CreditCard"">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""AMOUNT"" name=""AMOUNT"" value="""&function_krungsri_gen_price(intPricePayment)&""">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""CURRENCY"" name=""CURRENCY"" value=""764"">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""AMOUNTEXP10"" name=""AMOUNTEXP10"" value=""-2"">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""LANGUAGE"" name=""LANGUAGE"" value=""EN"">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""REF1"" name=""REF1"" value="""&int(intRealID)&""">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""REF2"" name=""REF2"" value="""">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""REF3"" name=""REF3"" value="""">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""REF4"" name=""REF4"" value="""">"&vbcrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""REF5"" name=""REF5"" value="""">"&vbcrlf
			strGateWay = strGateWay & "</form>"&vbcrlf
			strGateWay = strGateWay & "</body>" & VbCrlf
			strGateWay = strGateWay & "</html>" & VbCrlf
		
		Case 50
		'### Gateway Test
			strGateWay ="<html>" & VbCrlf
			strGateWay = strGateWay & "<body onLoad=""CreditForm.submit()"">" & VbCrlf
			strGateWay = strGateWay & "<form name=""CreditForm"" method=""post"" action=""http://www.booking2hotel.com/payment_test.asp"">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""amount"" name=""amount""  value="""&intPricePayment&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""ref_no"" name=""ref_no"" value="""&intRealID&""">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""vtest"" name=""vtest""  value=""vtest"">" & VbCrlf
			strGateWay = strGateWay & "<INPUT type=""hidden"" id=""backURL"" name=""backURL""  value=""http://www.booking2hotel.com/hotels/hotel_a01/bluehouse_update.asp"">" & VbCrlf
			strGateWay = strGateWay & "</form>" & VbCrlf
			strGateWay = strGateWay & "</body>" & VbCrlf
			strGateWay = strGateWay & "</html>" & VbCrlf
		'### Gateway Test
	END SELECT
	
	functionGenGatewayProcess = strGateWay
	
END FUNCTION
%>