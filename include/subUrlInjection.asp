<%
Sub subUrlInjection()
Dim arrKeyword
Dim intKeyword
Dim strQuery
Dim bolCheck
Dim strSqlMessage
Dim strResult
strSqlMessage=""
strQuery=LCase(Request.ServerVariables("QUERY_STRING"))


IF len(strQuery)>150 Then
	'response.write "A"
	IF  (instr(request.ServerVariables("PATH_INFO"),"show_link_gen.asp")<0) or (instr(request.ServerVariables("PATH_INFO"),"thailand-hotels-search.asp")<0) or (instr(lcase(request.ServerVariables("PATH_INFO")),"affiliate.asp")<0) Then
		strSqlMessage="IP:"&Request.ServerVariables("REMOTE_ADDR")&"<br>"
		strSqlMessage=strSqlMessage&"Referer:"&Request.ServerVariables("HTTP_REFERER")&"<br>"
		strSqlMessage=strSqlMessage&"Url:http://www.hotels2thailand.com"&request.ServerVariables("PATH_INFO")&"?"&strQuery&"<br>"
		strSqlMessage=strSqlMessage&"Date :"&dateadd("h",14,now)&"<br>"
		strSqlMessage=strSqlMessage&"Type :Injection!!"	
		'response.write instr(lcase(request.ServerVariables("PATH_INFO")),"affiliate.asp")
		strResult = function_mail_send ("mail.hotels2thailand.com","visa@hotels2thailand.com","Sql Injection","visa@hotels2thailand.com","visa@hotels2thailand.com", "","","Sql Injection",strSqlMessage, 2, 1, "")
		'strResult = function_mail_send_cdo(constMailServer,25,"System Mail","visa@hotels2thailand.com","","","catch@hotels2thailand.com","welcome","Url Injection",strSqlMessage,1,1)
		'response.write "hello"
		response.end
		response.Redirect("http://www.hotels2thailand.com")
	End IF
End IF

arrKeyword=array("select","update","delete","insert","from","cursor","object","system","declare",";","'","sp_","exec","cast","%3b")
	For intKeyword=0 to Ubound(arrKeyword)
		IF (inStr(strQuery,arrKeyword(intKeyword))<>0) Then
			IF Not instr(strQuery,"404;http")>0 Then
			strSqlMessage="IP:"&Request.ServerVariables("REMOTE_ADDR")&"<br>"
			strSqlMessage=strSqlMessage&"Referer:"&Request.ServerVariables("HTTP_REFERER")&"<br>"
			strSqlMessage=strSqlMessage&"Url:http://www.hotels2thailand.com"&request.ServerVariables("PATH_INFO")&"?"&strQuery&"<br>"
			strSqlMessage=strSqlMessage&"Date :"&dateadd("h",14,now)
			strResult = function_mail_send ("mail.hotels2thailand.com","visa@hotels2thailand.com","Sql Injection","visa@hotels2thailand.com","", "","","Sql Injection",strSqlMessage, 2, 1, "")
			'response.write  strQuery
			response.end
			response.Redirect("http://www.hotels2thailand.com")
			End IF
		End IF
	Next
End Sub
%>