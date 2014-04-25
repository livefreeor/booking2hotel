<%
FUNCTION function_mail_send_cdo2(strMailServer,intMailPort,strMailFrom,strMailTo,strMailTocc,strMailToBcc,strMailUser,strMailPassword,strSubject,strBody,bolPassword,intMailType)

	Dim ObjSendMail
	Dim strBcc
	Set ObjSendMail = CreateObject("CDO.Message") 
		 
	'This section provides the configuration information for the remote SMTP server.
		 
	ObjSendMail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/sendusing") = 2 'Send the message using the network (SMTP over the network).
	ObjSendMail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/smtpserver") = strMailServer
	ObjSendMail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/smtpserverport") = intMailPort
	ObjSendMail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/smtpusessl") = False 'Use SSL for the connection (True or False)
	ObjSendMail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/smtpconnectiontimeout") = 120
		 
	IF bolPassword = 1 OR bolPassword = True Then
		ObjSendMail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate") = 1 'basic (clear-text) authentication
		ObjSendMail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/sendusername") = strMailUser
		ObjSendMail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/sendpassword") = strMailPassword
	End IF
	
	ObjSendMail.Configuration.Fields.Update
		 
	'End remote SMTP server configuration section==
	
	strMailTo = Replace(strMailTo,",",";")

	ObjSendMail.To = strMailTo
	
	IF strMailTocc<>"" Then
		ObjSendMail.CC = strMailTocc
	End IF
	
	IF strMailToBcc<>"" Then
		ObjSendMail.BCC = strMailToBcc
	End IF
	
	IF strMailToBcc<>"" Then
		strBcc=";"&strMailToBcc
	End IF
	
	ObjSendMail.Subject = strSubject
	ObjSendMail.From = strMailFrom

	IF intMailType=1 Then 'Html
		ObjSendMail.HTMLBody = strBody
	ElseIF intMailTpye=0 Then 'Plain Text
		ObjSendMail.TextBody = strBody
	End IF
	
	ObjSendMail.Send
		 
	Set ObjSendMail = Nothing 
	
	function_mail_send_cdo2 = ""
	
END FUNCTION
%>