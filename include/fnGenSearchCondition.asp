<%
FUNCTION fnGenSearchCondition(strLocation,strRoomType,intPriceMin,intPriceMax,intSource)
	
	SELECT CASE strLocation
		Case "0"
			strLocation = "All"
		Case "1"
			strLocation = "Sukhumvit Rd: Phloen Chit &#8211; Asoke"
		Case "2"
			strLocation = "Sukhumvit Rd: Phrom Phong &#8211; Ekkamai"
		Case "3"
			strLocation = "Chit Lom / Lang Suan / Wireless Rd"
		Case "4"
			strLocation = "Silom / Sathorn"
		Case "5"
			strLocation = "Other"
	END SELECT
	
	SELECT CASE strRoomType
		Case "0"
			strRoomType = "All"
		Case "1"
			strRoomType = "Studio"
		Case "2"
			strRoomType = "1 Bedroom"
		Case "3"
			strRoomType = "2 Bedrooms"
		Case "4"
			strRoomType = "3 Bedrooms"
		Case "5"
			strRoomType = "More than 3 Bedrooms"
	END SELECT
	
	IF int(intPriceMax)>50000 Then
		intPriceMax = "More than 50,000 Baht/Month"
	Else
		intPriceMax = FormatNumber(intPriceMax,0) & "Baht/Month"
	End IF
		intPriceMin = FormatNumber(intPriceMin,0)
		
	fnGenSearchCondition = "<b>Location:</b>"& strLocation &"&nbsp;&nbsp;&nbsp;<b>Room Type:</b>"& strRoomType &"&nbsp;&nbsp;&nbsp;<b>Price:</b>" & intPriceMin & "-" & intPriceMax
	
END FUNCTION
%>