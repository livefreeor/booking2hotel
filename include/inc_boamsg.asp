
<%
'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'+	Programmed By :	Danuchpong Savetsila
'+	Email :			danuch@yahoo.com
'+	Objective :		Generated "boamsg"
'+	Output:			Bank of asia message format without encrypt
'+	Date Created:		13 March 2002
'+	Date Modified:		23 March 2002
'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Function CreateRequestBoaMsg (strTransactionCode, curCurrencyCode, intMerchantID, intOrderID, curAmount, intApprovalCode, intCardType, intPaymentType, intSettlementType)

	Dim blnError
	
	'sequence of boa message
	'transaction code = 			4 char
	'currency code = 			6 char
	'merchant id = 				9 char
	'bank reserved 1 = 			2 char
	'order number = 			6 numeric
	'transaction date = 			8 char
	'transaction time = 			6 char
	'card number = 			19 numeric
	'expiration date = 			4 char
	'transaction amount 			12 numeric
	'response code = 			2 char
	'approval code = 			6 numeric
	'customer reference 1 = 		19 numeric
	'customer reference 2 = 		19 numeric
	'reserved 2 = 				25 char
	'cvv/cvc = 				4 char
	'card type = 				1 char
	'reserved 3 = 				45 char
	'payment type = 			1 char
	'settlement type = 			1 char
	'authentication code = 		16 char

	Dim strBoaMsg, strTransactionDate, strTransactionTime
	Dim dteCurrentDate, dteCurrentTime, intAmountLength

	'prepare transaction date/time to a correct format
	dteCurrentDate = DateAdd("H", 14, Now())
	dteCurrentTime = TimeValue(FormatDateTime(DateAdd("H", 14, Now()),4))
	strTransactionDate = Year(dteCurrentDate) & AddZero(Month(dteCurrentDate), 2) & AddZero(Day(dteCurrentDate), 2)
	strTransactionTime = AddZero(Hour(dteCurrentTime), 2) & AddZero(Minute(dteCurrentTime), 2) & AddZero(Second(dteCurrentTime), 2)

	'prepare transaction amount
	curAmount = FormatNumber(curAmount, 2, -1, 0, 0)
	intAmountLength = Len(curAmount)
	curAmount = Mid(curAmount, 1, intAmountLength - 3) & Mid(curAmount, intAmountLength - 1, 2)

	'add transaction code
	strBoaMsg = strTransactionCode 'transaction code
	strBoaMsg = strBoaMsg & curCurrencyCode 'currency code
	strBoaMsg = strBoaMsg & intMerchantID 'merchant id
	strBoaMsg = strBoaMsg & "00" 'boa reserved field 1
	strBoaMsg = strBoaMsg & AddZero(intOrderId, 6) 'order identity
	strBoaMsg = strBoaMsg & strTransactionDate 'transaction date
	strBoaMsg = strBoaMsg & strTransactionTime ' transaction time
	strBoaMsg = strBoaMsg & AddZero("", 19) 'credit card number
	strBoaMsg = strBoaMsg & AddZero("", 4) 'credit card expiration date
	strBoaMsg = strBoaMsg & AddZero(curAmount, 12) 'order amount
	strBoaMsg = strBoaMsg & AddZero("", 2) 'response code

	'appoval code is depend on transaction code
	Select Case strTransactionCode
		Case BOA_TRANS_REVERSAL_CODE , BOA_TRANS_SETTLEMENT_CODE
			strBoaMsg = strBoaMsg & AddZero(intApprovalCode, 6)
		Case Else
			strBoaMsg = strBoaMsg & AddZero("", 6)
	End Select

	strBoaMsg = strBoaMsg & AddZero("", 19) 'our reference field 1
	strBoaMsg = strBoaMsg & AddZero("", 19) 'our reference field 2
	strBoaMsg = strBoaMsg & AddZero("", 25) 'boa reserved field 2
	strBoaMsg = strBoaMsg & AddZero("", 4) 'cvv, cvc

	'card type code is depend on transaction code
	Select Case strTransactionCode
		Case BOA_TRANS_REVERSAL_CODE, BOA_TRANS_SETTLEMENT_CODE
			strBoaMsg = strBoaMsg & AddZero(intCardType, 1)
		Case Else
			strBoaMsg = strBoaMsg & AddZero("", 1)
	End Select
	
	strBoaMsg = strBoaMsg & AddZero("", 45) 'boa reserved field 3
	strBoaMsg = strBoaMsg & intPaymentType 'payment type
	strBoaMsg = strBoaMsg & intSettlementType 'settlement type
	strBoaMsg = strBoaMsg & AddZero("", 16) 'authentication code (for internal use)

	'return result to called function
	CreateRequestBoaMsg = strBoaMsg

End Function


'use this function to put zero in front of input string to make sure
'it is in correct length
Function AddZero (strInput, intTotalLength)

	Dim strOutput, intCurrentLength, intLengthDifference, i

	strOutput = strInput
	intCurrentLength = CInt(Len(strInput))
	intTotalLength = CInt(intTotalLength)
	intLengthDifference = intTotalLength - intCurrentLength

	If intLengthDifference > 0 Then
		For i=1 To intLengthDifference
			strOutput = "0" & strOutput
		Next
	End If

	'return result to function
	AddZero = strOutput

End Function

'write bao message to database
Function WriteBoaMsg (strFullBoaMessage, intType)

	'database related variables
	Dim conSqlServer, cmdSqlServer, sqlString
	Dim strQuote, strSQuote, intStatus

	'input related variables
	Dim strTransactionCode, curCurrencyCode, intMerchantID, strReserved1, intOrderID, dteTransactionDate, dteTransactionTime
	Dim intCreditCardNumber, dteCreditCardExpireDate, curAmount, strResponseCode, intApprovalCode, strCustomerReference1
	Dim strCustomerReference2, strReserved2, intCvv, intCardType, strReserved3, intPaymentType, intSettlementType
	Dim strAuthenticationCode, strRefererURL, strUserAgent, strRefererIP

	strTransactionCode = Mid(strFullBoaMessage, 1, 4)
	curCurrencyCode = Mid(strFullBoaMessage, 5, 6)
	intMerchantID = Mid(strFullBoaMessage, 11, 9)
	strReserved1 = Mid(strFullBoaMessage, 20, 2)
	intOrderID = Mid(strFullBoaMessage, 22, 6)
	dteTransactionDate = Mid(strFullBoaMessage, 28, 8)
	dteTransactionTime = Mid(strFullBoaMessage, 36, 6)
	intCreditCardNumber = Mid(strFullBoaMessage, 42, 19)
	dteCreditCardExpireDate = Mid(strFullBoaMessage, 61, 4)
	curAmount = Mid(strFullBoaMessage, 65, 12)
	strResponseCode = Mid(strFullBoaMessage, 77, 2)
	intApprovalCode = Mid(strFullBoaMessage, 79, 6)
	strCustomerReference1 = Mid(strFullBoaMessage, 85, 19)
	strCustomerReference2 = Mid(strFullBoaMessage, 104, 19)
	strReserved2 = Mid(strFullBoaMessage, 123, 25)
	intCvv = Mid(strFullBoaMessage, 148, 4)
	intCardType = Mid(strFullBoaMessage, 152, 1)
	strReserved3 = Mid(strFullBoaMessage, 153, 45)
	intPaymentType = Mid(strFullBoaMessage, 198, 1)
	intSettlementType = Mid(strFullBoaMessage, 199, 1)
	strAuthenticationCode = Mid(strFullBoaMessage, 200, 16)
	
	SELECT CASE intType
		Case 1
			WriteBoaMsg = strTransactionCode
		Case 2
			WriteBoaMsg = curCurrencyCode
		Case 3
			WriteBoaMsg = intMerchantID
		Case 4
			WriteBoaMsg = strReserved1
		Case 5
			WriteBoaMsg = intOrderID
		Case 6
			WriteBoaMsg = dteTransactionDate
		Case 7
			WriteBoaMsg = dteTransactionTime
		Case 8
			WriteBoaMsg = intCreditCardNumber
		Case 9
			WriteBoaMsg = dteCreditCardExpireDate
		Case 10
			WriteBoaMsg = curAmount
		Case 11
			WriteBoaMsg = strResponseCode
		Case 12
			WriteBoaMsg = intApprovalCode
		Case 13
			WriteBoaMsg = strCustomerReference1
		Case 14
			WriteBoaMsg = strCustomerReference2
		Case 15
			WriteBoaMsg = strReserved2
		Case 16
			WriteBoaMsg = intCvv
		Case 17
			WriteBoaMsg = intCardType
		Case 18
			WriteBoaMsg = strReserved3
		Case 19
			WriteBoaMsg = intPaymentType
		Case 20
			WriteBoaMsg = intSettlementType
		Case 21
			WriteBoaMsg = strAuthenticationCode
	END SELECT
	
	strQuote = Chr(34)
	strSQuote = Chr(39)

	strRefererURL = Request.ServerVariables("HTTP_REFERER")
	strUserAgent = Request.ServerVariables("HTTP_USER_AGENT")
	strRefererIP = Request.ServerVariables("REMOTE_ADDR")

	If strRefererURL = "" Or IsNull(strRefererURL) Then
		strRefererURL = "No referer url"
	End If
	
	If strUserAgent = "" Or IsNull(strUserAgent) Then
		strUserAgent = "No agent"
	End If
	
	If strRefererIP = "" Or IsNull(strRefererIP) Then
		strRefererIP = "No referer ip"
	End If

End Function


'this function use to encrypt and decrypt boa message
'which comunicate between our server and bank server
Function encrypt_boa(str_data, int_encrypt_type)

	dim str_boamsg, encrypt_object
	int_encrypt_type = cint(int_encrypt_type)

	'create encrypt/encrypt component from boa
	set encrypt_object = server.createobject("encrypt.boaencrypt")

	'encrypt data
	if int_encrypt_type = 0 then
		encrypt_boa = encrypt_object.boa_encrypt(cstr(str_data))
	'decrypt data
	elseif int_encrypt_type = 1 then
		encrypt_boa = encrypt_object.boa_decrypt(cstr(str_data))
	end if

	'destroy object
	set encrypt_object = nothing


End Function

'change column flag base on field name and flag value
function fn_change_order_flag (int_order_id, str_field_name, bln_flag )

	'database related variables
	dim conn_sqlserver, cmd_sqlserver, sql
	dim bln_return_status

	set	conn_sqlserver = server.createobject("adodb.connection")
	set	cmd_sqlserver = server.createobject("adodb.command")


		conn_sqlserver.connectionstring = Application("str_conn_flowers")
		conn_sqlserver.open

		with cmd_sqlserver

			.activeconnection = conn_sqlserver
			.commandtext = "pr_change_order_flag"
			.commandtype = adCmdStoredProc

			.parameters.append .createparameter ("RETURN_VALUE",adtinyint,adparamreturnvalue)
			.parameters.append .createparameter ("@int_order_id", adinteger, adparaminput, ,int_order_id)
			.parameters.append .createparameter ("@chv_field_name",advarchar, adparaminput, 50, str_field_name)
			.parameters.append .createparameter ("@bit_flag",adboolean, adparaminput,, bln_flag)

			.execute ,,adexecutenorecords

			bln_return_status = cint(.parameters ("RETURN_VALUE"))

		end with

		conn_sqlserver.close

	set conn_sqlserver = nothing
	set cmd_sqlserver = nothing

	fn_change_order_flag = bln_return_status

end function
%>