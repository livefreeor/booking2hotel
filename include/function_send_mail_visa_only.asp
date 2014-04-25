<%
Function function_send_mail_visa_only(strSubject,strTo,strForm,strBody)
	Dim myMail
	Set myMail=CreateObject("CDO.Message")
	myMail.Subject=strSubject
	myMail.From=strForm
	myMail.To=strTo
	myMail.TextBody=strBody & Vbcrlf  & Vbcrlf  &  "Sent by QEmail Component " 
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