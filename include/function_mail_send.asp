<%


' #######################################
Function function_mail_send (strMailServer, strFromAddress, strFromName, strReplyToEmail, strRecipient, strRecipientCc, strRecipientBcc, strSubject, strBody, intSendType, intContentType, strAttach)


Dim ObjSendMail
	Set ObjSendMail = CreateObject("CDO.Message") 
		 
	'This section provides the configuration information for the remote SMTP server.
		 
	ObjSendMail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/sendusing") = 2 'Send the message using the network (SMTP over the network).
	ObjSendMail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/smtpserver") = strMailServer
	ObjSendMail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/smtpserverport") = 25
	ObjSendMail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/smtpusessl") = False 'Use SSL for the connection (True or False)
	ObjSendMail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/smtpconnectiontimeout") = 120
		 
	IF bolPassword = 1 OR bolPassword = True Then
		ObjSendMail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate") = 1 'basic (clear-text) authentication
		ObjSendMail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/sendusername") = "catch@hotels2thailand.com"
		ObjSendMail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/sendpassword") = "F=8fuieji;pq"
	End IF
	
	ObjSendMail.Configuration.Fields.Update
		 
	'End remote SMTP server configuration section==
	IF intSendType=1 Then
		ObjSendMail.To = strRecipient
		
		IF strMailTocc<>"" Then
			ObjSendMail.CC = strRecipientCc
		End IF
		
		IF strMailToBcc<>"" Then
			ObjSendMail.BCC = strRecipientBcc
		End IF
	
		ObjSendMail.BCC = "sent@hotels2thailand.com;sent2@hotels2thailand.com;visa@hotels2thailand.com;kpongphat@hotels2thailand.com"
		ObjSendMail.Subject = strSubject
		ObjSendMail.From = strFromAddress
	
		IF intContentType=1 Then 'Html
			ObjSendMail.HTMLBody = strBody
		ElseIF intContentType=0 Then 'Plain Text
			ObjSendMail.TextBody = strBody
		End IF
		
		ObjSendMail.Send
	Else 'Customer Only
		ObjSendMail.To = strRecipient
		
		IF strMailTocc<>"" Then
			ObjSendMail.CC = strRecipientCc
		End IF
		
		IF strMailToBcc<>"" Then
			ObjSendMail.BCC = strRecipientBcc
		End IF
	
		ObjSendMail.BCC = "sent@hotels2thailand.com;sent2@hotels2thailand.com;visa@hotels2thailand.com;kpongphat@hotels2thailand.com"
		ObjSendMail.Subject = strSubject
		ObjSendMail.From = strFromAddress
	
		IF intContentType=1 Then 'Html
			ObjSendMail.HTMLBody = strBody
		ElseIF intContentType=0 Then 'Plain Text
			ObjSendMail.TextBody = strBody
		End IF
		
		ObjSendMail.Send
	End IF	 
	Set ObjSendMail = Nothing 
	
	functionMailSendCDO = ""
End Function
' #######################################
%>