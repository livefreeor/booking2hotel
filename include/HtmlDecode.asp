<%
Function HTMLDecode(byVal encodedstring)
    Dim tmp, i
    tmp = encodedstring
	IF Not isnull(tmp) Then
    tmp = Replace( tmp, "&quot;", chr(34) )
    tmp = Replace( tmp, "&lt;"  , chr(60) )
    tmp = Replace( tmp, "&gt;"  , chr(62) )
    tmp = Replace( tmp, "&amp;" , chr(38) )
    tmp = Replace( tmp, "&nbsp;", chr(32) )
    For i = 1 to 255
        tmp = Replace( tmp, "(*)#" & i & ";", chr( i ) )
        
    Next
	Else
		tmp=""
	End IF
    HTMLDecode = tmp
End Function

%>