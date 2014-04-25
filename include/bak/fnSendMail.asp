<%
CONST constMailServer = "mail.hotels2thailand.com"
CONST constReservationMail = "reservation@hotels2thailand.com"
Const constReservationName = "hotels2thailand.com Reservation Department"

' #######################################
Function fnSendMail (strMailServer, strFromAddress, strFromName, strReplyToEmail, strRecipient, strRecipientCc, strRecipientBcc, strSubject, strBody, intSendType, intContentType, strAttach)

'send type = 1 then send all at onece
'send type = 2 then send each recipient

'content type = 0 then send plain text
'content type = 1 then send html text

	Dim arrRecipient, intToUBound, i, strTO
	Dim arrAttach, intAttact, k
	Dim arrCcRecipient, intCcUBound, l, strCc
	Dim arrBccRecipient, intBccUBound, j, strBcc
	Dim objMail
	Dim intAttachUBound

	arrRecipient = Split(strRecipient, ",", -1, vbTextCompare)
	arrCcRecipient = Split(strRecipientCc, ",", -1, vbTextCompare)
	arrBccRecipient = Split(strRecipientBcc, ",", -1, vbTextCompare)
	arrAttach = Split(strAttach, ",", -1, vbTextCompare)
	intToUBound = UBound(arrRecipient)
	intCcUBound = UBound(arrCcRecipient)
	intBccUBound = UBound(arrBccRecipient)
	intAttachUBound = UBound(arrAttach)

	'send all at onece
	If intSendType = 1 Then

		Set	objMail = Server.CreateObject("SMTPsvg.Mailer")

			objMail.RemoteHost = strMailServer
			objMail.FromAddress = strFromAddress
			objMail.FromName = strFromName
			objMail.ReplyTo	= strReplyToEmail
			
			if intContentType =  1 Then
               	objMail.ContentType = "text/html;"
			end if
			
			'add recipient
			For i = 0 To intToUBound
				strTo = Trim(arrRecipient(i))
				If strTo <> "" Then objMail.AddRecipient strTo, strTo end if
			Next
			'add recipient cc
			For l = 0 To intCcUBound
				strCc = Trim(arrCcRecipient(l))
				If strCc <> "" Then objMail.AddCc strCc, strCc end if
			Next
			'add recipient bcc
			For j = 0 To intBccUBound
				strBcc = Trim(arrBccRecipient(j))
				If strBcc <> "" Then objMail.AddBcc strBcc, strBcc end if
			Next
			'add attachment
			For k=0 To intAttachUBound
				objMail.AddAttachment arrAttach(k)
			Next
			
			objMail.Subject	= strSubject
			objMail.BodyText= strBody

			If objMail.SendMail Then
				fnSendMail = ""
			Else
				fnSendMail = objMail.Response
			End IF

		Set objMail = Nothing

	'send for each recipient and assume that there is no bcc email
	ElseIf intSendType = 2 Then

		For i = 0 To intToUBound

			strTo = Trim(arrRecipient(i))

			If strTo <> "" Then

				Set objMail = Server.CreateObject("SMTPsvg.Mailer")

					objMail.RemoteHost		= strMailServer
					objMail.FromAddress		= strFromAddress
					objMail.FromName		= strFromName
					objMail.ReplyTo			= strReplyToEmail
					objMail.AddRecipient strTo, strTo
					objMail.Subject			= strSubject
					objMail.BodyText		= strBody
					'objMail.AddBcc "sent@hotels2thailand.com", "sent@hotels2thailand.com"
					if strRecipientCc <> "" Then
						objMail.AddCc strRecipientCc, strRecipientCc
					end if
					
					if strRecipientBcc <> "" Then
						objMail.AddBcc strRecipientBcc, strRecipientBcc
					end if
					
					if intContentType =  1 Then
						objMail.ContentType = "text/html"
					end if

					If objMail.SendMail Then
						fnSendMail = ""
					Else
						fnSendMail = objMail.Response
						i = intToUBound + 1
					End IF
				Set objMail = Nothing
			End If
		Next
		
	End If

End Function
' #######################################
%>