<%
function function_gen_link(strDes,strPsid,strLink,strPC,hotCon)
	Dim strCon
	if (strPsid <> " ") then
		strCon = "?psid="&strPsid&""
	end if		
	function_gen_link = "thailand-"&strPC&".asp"&strCon	
			Select Case strLink
				Case "1"
						function_gen_link= Replace(strDes&"-"&strPC&".asp"," ","-")&strCon
				Case "2	"		
						function_gen_link= Replace(strDes&"-"&strPC," ","-")&"/"&Replace(hotCon," ","-")&strCon
			End Select
		Case"32"	'	Golf Course
			Select Case strLink
				Case "1"
						function_gen_link= Replace(strDes&"-"&strPC&".asp"," ","-")&strCon
				Case "2"			
						function_gen_link= Replace(strDes&"-"&strPC," ","-")&"/"&Replace(hotCon," ","-")&strCon
			End Select
		Case"34"	'	Day Trip
			Select Case strLink
				Case "1"
						function_gen_link= Replace(strDes&"-"&strPC&".asp"," ","-")&strCon
				Case "2"			
						function_gen_link= Replace(strDes&"-"&strPC," ","-")&"/"&Replace(hotCon," ","-")&strCon
			End Select
		Case "36"	'	Water Activity
			Select Case strLink
				Case "1"
						function_gen_link= Replace(strDes&"-"&strPC&".asp"," ","-")&strCon
				Case "2"			
						function_gen_link= Replace(strDes&"-"&strPC," ","-")&"/"&Replace(hotCon," ","-")&strCon
			End Select
		Case "38"	'	Show And Event
			Select Case strLink
				Case "1"
						function_gen_link= Replace(strDes&"-"&strPC&".asp"," ","-")&strCon
				Case "2"			
						function_gen_link= Replace(strDes&"-"&strPC," ","-")&"/"&Replace(hotCon," ","-")&strCon
			End Select
		Case "39"	'	Health Check Up
			Select Case strLink
				Case "1"
						function_gen_link= Replace(strDes&"-"&strPC&".asp"," ","-")&strCon
				Case "2"			
						function_gen_link= Replace(strDes&"-"&strPC," ","-")&"/"&Replace(hotCon," ","-")&strCon
			End Select
end function
%>