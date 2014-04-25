<%
FUNCTION functionMailSendCDO_NoCC(strMailServer,intMailPort,strMailFrom,strMailTo,strMailUser,strMailPassword,strSubject,strBody,bolPassword,intMailType,attFile)

'response.Write(strMailServer)&"<br>"
'response.Write(intMailPort)&"<br>"
'response.Write(strMailFrom)&"<br>"
'response.Write(strMailTo)&"<br>"
'response.Write(strMailUser)&"<br>"
'response.Write(strMailPassword)&"<br>"
'response.Write(strSubject)&"<br>"
'response.Write(strBody)&"<br>"
'response.Write(bolPassword)&"<br>"
'response.Write(intMailType)&"<br>"
'response.Write(attFile)&"<br>"
'response.End()

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
		
	ObjSendMail.Subject = strSubject
	ObjSendMail.From = strMailFrom

	IF intMailType=1 Then 'Html
		ObjSendMail.HTMLBody = strBody
	ElseIF intMailTpye=0 Then 'Plain Text
		ObjSendMail.TextBody = strBody
	End IF
	
	ObjSendMail.Send
		 
	Set ObjSendMail = Nothing 
	
	functionMailSendCDO_NoCC = ""
	
END FUNCTION
%> 