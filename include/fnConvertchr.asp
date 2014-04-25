<%
Function fnConvertChr(strComment,intType)

	Select Case intType
		Case 0 
			strComment=Cstr(Replace(strComment,chr(34),"\/\/"))
			strComment=Cstr(Replace(strComment,chr(39),"\/"))
			strComment=Cstr(Replace(strComment,VbCrlf,"<BR>"))
			strComment=Cstr(Replace(strComment,chr(60),"<"))
			strComment=Cstr(Replace(strComment,chr(62),">"))
			'strComment=Cstr(Replace(strComment," ","&nbsp;"))
'			strComment=Cstr(Replace(strComment,chr(160),""))
		Case 1
			strComment=Cstr(Replace(strComment,"\/\/",chr(34)))
			strComment=Cstr(Replace(strComment,"\/",chr(39)))
			strComment=Cstr(Replace(strComment,"<BR>",VbCrlf))
			strComment=Cstr(Replace(strComment,"<",chr(60)))
			strComment=Cstr(Replace(strComment,">",chr(62)))
			strComment=Cstr(Replace(strComment,"&nbsp;"," "))
	End Select
	
	fnConvertChr=strComment

End Function
%>
