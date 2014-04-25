<%
FUNCTION function_kbank_gen_price(intPrice)
	
	Dim intLenPrice
	Dim intPriceCount
	Dim strResult
	
	IF intPrice<>"" AND NOT ISNULL(intPrice) Then
		intPrice = Int(IntPrice)
	Else
		intPrice = 0
	End IF

	intPrice = intPrice * 100

	strResult = function_add_zero (intPrice, 12)
	
	function_kbank_gen_price = strResult
	
END FUNCTION

FUNCTION function_kbank_msg_read(strMsg,intType)

	Dim strResult

	SELECT CASE intType
		Case 1 '### Response Code ###
			strResult = Mid(strMsg,1,2)
		Case 2 '### Reference ###
			strResult = Mid(strMsg,3,12)
		Case 3 '### Authorize ###
			strResult = Mid(strMsg,15,6)
		Case 4 '### UAID ###
			strResult = Mid(strMsg,21,36)
		Case 5 '### Invoice (Order ID) ###
			strResult = Mid(strMsg,57,12)
			'strResult = Right(strResult,5)
		Case 6 '### Timestamp ###
			strResult = Mid(strMsg,69,14)
		Case 7 '### Amount ###
			strResult = Mid(strMsg,83,12)
		Case 8 '### Checksum ###
			strResult = Mid(strMsg,95,40)
		Case 9 '### Card Type ###
			strResult = Mid(strMsg,135,20)
		Case 10 '### ChecksumCard2 ###
			strResult = Mid(strMsg,155,40)
	END SELECT
	
	function_kbank_msg_read = strResult
	
END FUNCTION
%>
