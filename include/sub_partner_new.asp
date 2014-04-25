<%
Sub sub_partner()
	Dim strRefer
	Dim strIP
	Dim url
	strRefer = Request.ServerVariables("HTTP_REFERER")
	strRefer = Replace(strRefer,"'","''")
	strIP = Request.ServerVariables("REMOTE_ADDR")
	
	IF Request("psid")="" Then
		url=strRefer
		url=mid(url,instr(url,"://")+3)
		'### Domain Name Only
		check_url=instr(url,"www.")
		IF check_url<>0 Then
			url=replace(url,"www.","")
		End IF
							
		IF mid(url,len(url),len(url))="/" Then
			url=mid(url,1,len(url)-1)
		End IF
		'### 
		
	Else
	
	End IF
	
End Sub
%>