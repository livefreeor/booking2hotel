<%
Function function_char_encode(strData,intType)
	arrChar=array("<",">","'","""""")
	arrCharEn=array("&lt;","&gt;","&acute;","&quot;")
	For intChar=0 to Ubound(arrChar)
		strData=replace(strData,arrChar(intChar),arrCharEn(intChar))
	Next
	function_char_encode=strData
End Function
%>