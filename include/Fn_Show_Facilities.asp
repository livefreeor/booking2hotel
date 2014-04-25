<%
Function Fn_Show_Facilities(strFields , strTable , condition , strOrderBy , strWidth)

Dim recGen
Dim sqlGen
Dim arrFields

if not trim(condition) = "" then

arrFields=split(strFields,"|")
Set recGen = Server.CreateObject ("ADODB.Recordset")
sqlGen=" Select "& arrFields(0) &","&arrFields(1)&" From "&strTable&" Where "& arrFields(0) &" in ("& condition &") Order By " & strOrderBy
recGen.Open sqlGen, Conn,1,3

	Fn_Show_Facilities=Fn_Show_Facilities&      "<table width="&chr(34)& strWidth &chr(34)&" border="&chr(34)&"0"&chr(34)&" cellspacing="&chr(34)&"1"&chr(34)&" cellpadding="&chr(34)&"1"&chr(34)&">"

Do While Not recGen.Eof

	If Not recGen.Eof then
		Fn_Show_Facilities=Fn_Show_Facilities&          "<tr bgcolor="&chr(34)&"#FFFFFF"&chr(34)&"> "
		Fn_Show_Facilities=Fn_Show_Facilities&            "<td width="&chr(34)&"150"&chr(34)&">&nbsp;"
		Fn_Show_Facilities=Fn_Show_Facilities&   	           "<strong><font color="&chr(34)&"#000000"&chr(34)&" size="&chr(34)&"1"&chr(34)&" face="&chr(34)&"MS Sans Serif"&chr(34)&">"&"<img src="&chr(34)&"image/bullet02.gif" &chr(34)&"width="&chr(34)&"7"&chr(34)&" height="&chr(34)&"7"&chr(34)&">"&"  "& recGen.Fields(trim(arrFields(1))) &"</font></strong></td>"
		recGen.Movenext
	end if

	If Not recGen.Eof then
		Fn_Show_Facilities=Fn_Show_Facilities&            "<td width="&chr(34)&"150"&chr(34)&">&nbsp;"
		Fn_Show_Facilities=Fn_Show_Facilities&   	           "<strong><font color="&chr(34)&"#000000"&chr(34)&" size="&chr(34)&"1"&chr(34)&" face="&chr(34)&"MS Sans Serif"&chr(34)&">"&"<img src="&chr(34)&"image/bullet02.gif" &chr(34)&"width="&chr(34)&"7"&chr(34)&" height="&chr(34)&"7"&chr(34)&">"&"  "& recGen.Fields(trim(arrFields(1))) &"</font></strong></td>"
		recGen.Movenext
	end if
			
	If Not recGen.Eof then
		Fn_Show_Facilities=Fn_Show_Facilities&            "<td width="&chr(34)&"150"&chr(34)&">&nbsp;"
		Fn_Show_Facilities=Fn_Show_Facilities&              "<strong><font color="&chr(34)&"#000000"&chr(34)&" size="&chr(34)&"1"&chr(34)&" face="&chr(34)&"MS Sans Serif"&chr(34)&">"&"<img src="&chr(34)&"image/bullet02.gif" &chr(34)&"width="&chr(34)&"7"&chr(34)&" height="&chr(34)&"7"&chr(34)&">"&"  "& recGen.Fields(trim(arrFields(1))) &"</font></strong></td>"
		Fn_Show_Facilities=Fn_Show_Facilities&          "</tr>"
		recGen.MoveNext
	end if
	
Loop

	Fn_Show_Facilities=Fn_Show_Facilities&        "</table>"

Else
	Fn_Show_Facilities= "-"

end if
recGen.Close
Set recGen = Nothing
End Function
%>

