<%
FUNCTION fnGenSearch(strLocation,strRoomType,intPriceMin,intPriceMax,intSource)
	
	IF strLocation = "0" Then
		strLocation = "1,2,3,4,5"
	End IF
	
	IF strRoomType="0" Then
		strRoomType = "1,2,3,4,5"
	End IF
	
	fnGenSearch = "pr_search '"& strLocation &"','"& strRoomType &"',"& intPriceMin &","& intPriceMax &"," & intSource
	
END FUNCTION
%>