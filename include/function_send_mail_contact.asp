<%
Function function_send_mail_contact()
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
End Function
%>