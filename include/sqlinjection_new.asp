<%
'config
Dim v_strSubject
Dim v_strTo
Dim v_strTo2
Dim v_strFrom
Dim v_strBody
Dim v_website
Dim v_strFileName
Dim v_BlackList
Dim strInputInjection
Dim strCookies
Dim strSession
Dim c
Dim sc
	
v_BlackList=array("insert","update","cast","declare","drop")
v_strFileName=lcase(request.ServerVariables("SCRIPT_NAME"))
v_website="http://www.hotels2thailand.com"
v_strSubject="SQL Injection Report"
v_strTo="visa@hotels2thailand.com"
v_strTo2 = "tanit@hotels2thailand.com"
v_strFrom="visa@hotels2thailand.com"
v_strBody = "SQL Injection report." & VBcrlf & VBcrlf
v_strBody = v_strBody  & "Web Site = " & v_website  & VBcrlf
v_strBody = v_strBody  & "IP = " & request.servervariables("REMOTE_ADDR")  & VBcrlf
v_strBody = v_strBody  & "Page Entered = " & request.servervariables("SCRIPT_NAME")  & VBcrlf
v_strBody = v_strBody  & "Method = " & request.servervariables("REQUEST_METHOD")  & VBcrlf
v_strBody = v_strBody  & "Content Length = " & request.servervariables("CONTENT_LENGTH")  & VBcrlf
v_strBody = v_strBody  & "Query String = " & request.servervariables("QUERY_STRING")  & VBcrlf
v_strBody = v_strBody  & "Form Data = " & request.form  & VBcrlf

	For Each c In response.Cookies
		strCookies=strCookies& request.Cookies(c)&"++"
	Next
	
	For Each sc In session.Contents
		strSession=strSession&session.Contents(sc)&"++"
	Next
v_strBody = v_strBody  & "Cookies Data = " & strCookies & VBcrlf
v_strBody = v_strBody  & "Session Data = " & strSession & VBcrlf

'-----------

Sub subinjection_new()
	Dim strQuery
	Dim strInputForm
	Dim s
	
	strQuery=lcase(request.servervariables("QUERY_STRING"))
	
	For Each s IN request.Form
		strInputForm=strInputForm& request.form(s)
	Next
	
	
	
'	IF Len(strCookies)>100 Then
'		call sendMailReport(3)
'		Response.Write "Error"
'		Response.end
'	End IF
	
	' IF NOT (InStr(Request.ServerVariables("URL"),"dual")>0 OR InStr(Request.ServerVariables("URL"),"admin")>0 OR InStr(Request.ServerVariables("URL"),"vahoo")>0 OR lcase(request.servervariables("REQUEST_METHOD")<>"post"))Then
'		v_strSubject="SQL Injection First Check"
'		call sendMailReport()
'		v_strSubject="SQL Injection Report"
'	 End IF

	IF CheckURL() Then 
		call QueryInjection(strQuery) 'Get
		'call FormInjection(strInputForm) 'Post
		call QueryInjection(strCookies)
		call QueryInjection(strSession)
	End IF
	'### Check Len For POST ###
'	For Each s IN request.Form
'		IF s<>"address" AND s<>"review" AND s<>"message" Then
'			IF NOT (InStr(s, "comment_")>0 OR inStr(s,"detail_")>0) Then
'				IF NOT (InStr(Request.ServerVariables("URL"),"dual")>0 OR InStr(Request.ServerVariables("URL"),"admin")>0 OR InStr(Request.ServerVariables("URL"),"vahoo")>0)Then
'					IF Len(Request.Form(s))>100 Then
'						call sendMailReport(1)
'						Response.Write "Error"
'						Response.end
'					End IF
'				End IF
'			End IF
'		End IF
'	NEXT
	'### Check Len For POST ###
	'For Each sc In session.Contents
'		IF NOT (InStr(Request.ServerVariables("URL"),"dual")>0 OR InStr(Request.ServerVariables("URL"),"admin")>0 OR InStr(Request.ServerVariables("URL"),"vahoo")>0)Then
'			IF Len(session.Contents(sc))>100 Then
'				call sendMailReport(4)
'				Response.Write "Error"
'				Response.end
'			End IF
'		End IF
'	Next
End Sub

function CheckURL()
	Dim currentURL
	Dim arrKeyword
	Dim bolCheck
	Dim intCheck
	
	bolCheck=true
	
	arrKeyword=array("dual","admin","vahoo","newadmin","bcalendar","order-complete")
	currentURL=Request.ServerVariables("URL")
	For intCheck=0 to UBound(arrKeyword)
		IF (Instr(currentURL,arrKeyword(intCheck))) >0 Then
			bolCheck=false
		End IF
	Next
	CheckURL=bolCheck
End function

Sub QueryInjection(strQuery)
	Dim arrKeyword
	Dim intKeyword
	
	arrKeyword=array("select","update","delete","insert","from","cursor","object","system","declare","'","sp_","exec","cast","%3b","drop")
	For intKeyword=0 to Ubound(arrKeyword)
		IF (inStr(strQuery,arrKeyword(intKeyword))<>0) Then
			IF Not instr(strQuery,"404;http")>0 Then
			call sendMailReport(2)
			response.end
			End IF
		End IF
	Next
End Sub

Sub FormInjection(strInputForm)
 
  
  IF (CheckStringForSQL(strInputForm)) Then
  	strInputInjection=strInputForm
	call sendMailReport(1)
	response.end
  End If
  
  IF (CheckStringForSQL(replace(strInputForm,"%","" ) ))Then
  	strInputInjection=strInputForm
	call sendMailReport(1)
	response.end
  End If
End Sub

Function CheckStringForSQL(str)

  'On Error Resume Next 
  
  Dim lstr,s
  
  ' If the string is empty, return true
  If ( IsEmpty(str) ) Then
    CheckStringForSQL = false
    Exit Function
  ElseIf ( StrComp(str, "") = 0 ) Then
    CheckStringForSQL = false
    Exit Function
  End If

  lstr = LCase(str)
  
  ' Check if the string contains any patterns in our
  ' black list
  For Each s in v_BlackList
    If ( InStr (lstr, s) <> 0 ) Then
		Select Case s
			Case "cast"
				If (( InStr (lstr, "(") <> 0 ) and ( InStr (lstr, ")") <> 0 )) or ((InStr(lstr,"%28")<>0) and (InStr(lstr,"%29")<>0)) Then
					 CheckStringForSQL = true
					 Exit Function
				end If
			Case "drop"
				If (( InStr (lstr, "(") <> 0 ) and ( InStr (lstr, ")") <> 0 )) or ((InStr(lstr,"%28")<>0) and (InStr(lstr,"%29")<>0)) Then
					 CheckStringForSQL = true
					 Exit Function
				end If
			Case "update"
				If ( InStr (lstr, "set") <> 0 ) Then
					 CheckStringForSQL = true
					 Exit Function
				end If				
			Case "insert"
				If ( InStr (lstr, "into") <> 0 ) and ( InStr (lstr, "values") <> 0 ) Then
					 CheckStringForSQL = true
					 Exit Function
				end If
			Case Else
					 CheckStringForSQL = true
					 Exit Function				
		End Select
    End If
  
  Next
  
  CheckStringForSQL = false
  
End Function

Sub sendMailReport(intType)
	Select Case intType
	Case 1
		v_strSubject="SQL Injection Report (Form)"
	Case 2
		v_strSubject="SQL Injection Report (Get)"
	Case 3
		v_strSubject="SQL Injection Report (Cookies)"
	Case 4
		v_strSubject="SQL Injection Report (Session)"
	End Select
	
	Dim myMail
	Set myMail=CreateObject("CDO.Message")
	myMail.Subject=v_strSubject
	myMail.From=v_strFrom
	myMail.To=v_strTo
	myMail.CC=v_strTo2
	myMail.TextBody=v_strBody & "Form Input = " & strInputInjection & Vbcrlf  & Vbcrlf  &  "Sent by QEmail Component " 
	myMail.Configuration.Fields.Item _
	("http://schemas.microsoft.com/cdo/configuration/sendusing")=2
	'Name or IP of remote SMTP server
	myMail.Configuration.Fields.Item _
	("http://schemas.microsoft.com/cdo/configuration/smtpserver") _
	="mail.hotels2thailand.com"
	'Server port
	myMail.Configuration.Fields.Item _
	("http://schemas.microsoft.com/cdo/configuration/smtpserverport") _
	=25 
	myMail.Configuration.Fields.Update
	myMail.Send
	set myMail=nothing
	'response.Redirect(v_website)
End Sub

call subinjection_new()
%>