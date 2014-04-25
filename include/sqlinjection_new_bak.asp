<%
'config
Dim v_strSubject
Dim v_strTo
Dim v_strFrom
Dim v_strBody
Dim v_website
Dim v_strFileName
Dim v_BlackList
Dim strInputInjection

v_BlackList=array("insert","update","cast","declare")
v_strFileName=lcase(request.ServerVariables("SCRIPT_NAME"))
v_website="http://www.hotels2thailand.com"
v_strSubject="SQL Injection Report"
v_strTo="visa@hotels2thailand.com"
v_strFrom="visa@hotels2thailand.com"
v_strBody = "SQL Injection report." & VBcrlf & VBcrlf
v_strBody = v_strBody  & "IP = " & request.servervariables("REMOTE_ADDR")  & VBcrlf
v_strBody = v_strBody  & "Page Entered = " & request.servervariables("SCRIPT_NAME")  & VBcrlf
v_strBody = v_strBody  & "Method = " & request.servervariables("REQUEST_METHOD")  & VBcrlf
v_strBody = v_strBody  & "Content Length = " & request.servervariables("CONTENT_LENGTH")  & VBcrlf
v_strBody = v_strBody  & "Query String = " & request.servervariables("QUERY_STRING")  & VBcrlf



'-----------

Sub subinjection_new()
	Dim strQuery
	Dim strInputForm
	Dim s
	
	
	strQuery=lcase(request.servervariables("QUERY_STRING"))
	For Each s IN request.Form
		strInputForm=strInputForm& request.form(s)
	Next
	
	call QueryInjection(strQuery) 'Get
	call FormInjection(strInputForm) 'Post
	
End Sub

Sub QueryInjection(strQuery)
	Dim arrKeyword
	Dim intKeyword
	
	arrKeyword=array("select","update","delete","insert","from","cursor","object","system","declare",";","'","sp_","exec","cast","%3b")
	For intKeyword=0 to Ubound(arrKeyword)
		IF (inStr(strQuery,arrKeyword(intKeyword))<>0) Then
			IF Not instr(strQuery,"404;http")>0 Then
			call sendMailReport()
			response.end
			End IF
		End IF
	Next
End Sub

Sub FormInjection(strInputForm)
  IF (CheckStringForSQL(strInputForm)) Then
  	strInputInjection=strInputForm
	call sendMailReport()
	response.end
  End If
  IF ( CheckStringForSQL(replace(strInputForm,"%","" ) ))Then
  	strInputInjection=strInputForm
	call sendMailReport()
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

Sub sendMailReport()
	Dim myMail
	Set myMail=CreateObject("CDO.Message")
	myMail.Subject=v_strSubject
	myMail.From=v_strFrom
	myMail.To=v_strTo
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