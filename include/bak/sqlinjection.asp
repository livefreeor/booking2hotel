<%

' SQL Injection Killer 
' ==========================================================================
' Version : 1.1
' Latest Update: 30-06-2008
' Script was written by Saran Nantasuk

' Note : In current version this script can protect only http "GET" and http "POST" method only
' if your script used other request object such as upload.file please try  another way to protect  the page. :-P

' ## Main Feature ##
' Check all  GET & POST variables
' Easy to add new filter keywords  ( Bad Keywords Farm )
' Quick check wrong GET parameter over than 255 characters
' Easy to config Stop & Redirect Mode ( Var_Action )
' Email Alert function supported  ASP Qemail , CDONT Email
' ==========================================================================




' Set Variables & Config
' ==========================================================================
Dim againstSQLinjection
Dim refscriptname
Dim yourwebsitename
Dim injectredirectpage


againstSQLinjection=request.servervariables("QUERY_STRING")
refscriptname = request.servervariables("SCRIPT_NAME")
yourwebsitename = "hotels2thailand.com"

' Set redirect page
injectredirectpage = "http://www.hotels2thailand.com" 
' ==========================================================================




' Email Configuration
' ==========================================================================
Dim inj_mailhost
Dim inj_mailfrom
Dim inj_mailfromname
Dim inj_mailtoname 
Dim inj_mailtoaddess
Dim inj_Subject
Dim inj_Body

inj_mailhost = "mail.hotels2thailand.com"
inj_mailfrom ="visa@hotels2thailand.com"
inj_mailfromname = "@aRobot Engine" 
inj_mailtoname = "visa@hotels2thailand.com" 
inj_mailtoaddess = "visa@hotels2thailand.com" 
inj_Subject = yourwebsitename & " SQL Injection report."

inj_Body = "SQL Injection report." & VBcrlf & VBcrlf
inj_Body = inj_Body  & "IP = " & request.servervariables("REMOTE_ADDR")  & VBcrlf
inj_Body = inj_Body  & "Page Entered = " & request.servervariables("SCRIPT_NAME")  & VBcrlf
inj_Body = inj_Body  & "Method = " & request.servervariables("REQUEST_METHOD")  & VBcrlf
inj_Body = inj_Body  & "Content Length = " & request.servervariables("CONTENT_LENGTH")  & VBcrlf
inj_Body = inj_Body  & "Query String = " & request.servervariables("QUERY_STRING")  & VBcrlf

Dim FormInp
Dim FormInputValue
DIm POSTItem

For Each FormInp in request.form
	FormInputValue =  FormInp &  POSTItem & "=" & request.form(FormInp)  & VBcrlf
Next

If IsNull(FormInputValue) = False Then 
	inj_Body = inj_Body  & "Form Input  "  & VBcrlf & FormInputValue
End if
' ==========================================================================


' Script Configuration
' ==========================================================================
Dim Var_BlockGetOverLimit
Dim Var_ShowServerVar
Dim Var_ShowInjectionKeywords
Dim Var_ShowGetMethod
Dim Var_ShowPostMethod 
Dim Var_ShowTestForm
Dim Var_BlockDotJS
Dim Var_Action
Dim Var_SendInjectionAlertEmail
Dim Var_EmailComponent 
Dim BadWordArrays
Dim tfbody
Dim InjectionKeywords
Var_BlockGetOverLimit = True '  Redirect page when GET variables over than 255 characters 
Var_ShowInjectionKeywords  = False  ' Check current bad words was protection by this script
Var_ShowServerVar  = False  '  Check Server Variable Values 
Var_ShowGetMethod  = False  ' Check GET Values 
Var_ShowPostMethod  = False  '  Check POST Values 
Var_ShowTestForm = False ' Show Test Form 
Var_BlockDotJS = True ' Block user to use word ".JS"

Var_Action = 2 '[1] Stop and show error msg.  [2] REDIRECT TO injectredirectpage

Var_SendInjectionAlertEmail = True '  Send alert email to someone 
Var_EmailComponent = 1 ' [1] CDONT [2] Qemail 
' ==========================================================================

%>


<style type="text/css">
.InjectFont {
	font-family: Tahoma;
	font-size: 12px;
}
</style>


<%


' Quick Check Bad QueryString
' ==========================================================================
IF Var_BlockGetOverLimit = True Then
	If len(againstSQLinjection) > 255 Then
		Call AlertAction(Var_Action)
		response.end
	End if
End if
' ==========================================================================



' Bad Keywords Farm
' ==========================================================================
InjectionKeywords = array("%3cscript","%3c%20script","<%20script","%3c+script","< script","declare",";set","%3bset",";exec","%3bexec","cast(","<script",";","update","%3b")
' ==========================================================================




' Check Bad keywords
' ==========================================================================

' [Block .JS]
If Var_BlockDotJS = True Then
if Instr(lcase(againstSQLinjection),".js")  > 0 and Instr(lcase(againstSQLinjection),".jsp") <=0 then
	Call AlertAction(Var_Action)
	response.end
end if 
End If


'[Check Query String Method ]
For BadWordArrays = 0 to Ubound(InjectionKeywords)	
	If Instr(lcase(Server.HTMLENCODE(againstSQLinjection)), Cstr(Server.HTMLENCODE(InjectionKeywords(BadWordArrays))))  > 0	 Then
		Call AlertAction(Var_Action)
		response.end
	End if
Next
Dim SplitPost


'[Check Form Method ]
For BadWordArrays = 0 to Ubound(InjectionKeywords)
	For Each PostItem in request.form
			SplitPost = 	PostItem & request.form(PostItem)
			If Instr(lcase(Server.HTMLENCODE(SplitPost)), Cstr(Server.HTMLENCODE(InjectionKeywords(BadWordArrays))))  > 0	 Then
				Call AlertAction(Var_Action)
				response.end
			End if
     Next
Next

' ==========================================================================



'  Config Condition
' ==========================================================================
If Var_ShowTestForm  = True Then
	Call ShowTestForm(refscriptname)
End if

If Var_ShowInjectionKeywords  = True Then
	Call ShowInjectionKeywords
End if

If Var_ShowGetMethod  = True Then
	Call ShowGetMethod
End if

If Var_ShowPostMethod  = True Then
	Call ShowPostMethod
End if

If Var_ShowServerVar  = True Then
	Call ShowServerVar
End if
' ==========================================================================



' Subs & Functions
' ==========================================================================
Sub ShowInjectionKeywords
	response.write "<h2>Injection Keywords</h2>"
	response.write "<h4>All words was protected by script.</h4>" 
	Call DrawLine
	for BadWordArrays = 0 to Ubound(InjectionKeywords)
		response.write "<font class=""InjectFont"">" & BadWordArrays+1 & ". " & Cstr(Server.HTMLENCODE(InjectionKeywords(BadWordArrays))) & "</font><br>"
	Next
	Call DrawLine
	response.write "<br><br>"
End Sub


Sub ShowServerVar
		response.write "<h2>Server Variables</h2>"
	    response.write "<h4></h4>" 
		Call DrawLine
		For Each QItem in request.ServerVariables
			response.write "<font class=""InjectFont"">" &  QItem & "=" & request.ServerVariables(Qitem)  & "</font><br>"
		Next
		Call DrawLine
End Sub


Sub ShowGetMethod
		response.write "<h2>HTTP GET Variables</h2>"
	    response.write "<h4></h4>" 
		Call DrawLine
		For Each GETItem in request.querystring
			response.write GETItem & "=" & request.querystring(GETItem)  & "<br>"
		Next
		Call DrawLine
End Sub


Sub ShowPostMethod
		response.write "<h2>HTTP POST Variables</h2>"
	    response.write "<h4></h4>" 
		Call DrawLine
		For Each POSTItem in request.form
			response.write "<font class=""InjectFont"">" & POSTItem & "=" & request.form(POSTItem)  & "</font><br>"
		Next
		Call DrawLine
End Sub


Sub ShowTestForm(refscriptname)
	tfbody = "<form action='" & refscriptname  & "' method=post>"
	tfbody = tfbody & "<textarea name='testtextbox' cols=""50"" rows=""5"" ></textarea><br>"
	tfbody = tfbody & "<input type=submit value='Test Post Method'>"
	tfbody = tfbody & "</form>"
	response.write tfbody
End sub



Sub DrawLine
	response.write "<hr>"
End Sub


Sub AlertAction(nums)
		If Var_SendInjectionAlertEmail = True Then
		Call sendinjectmail(inj_mailfrom,inj_mailfromname,inj_mailtoname,inj_mailtoaddess,inj_Subject,inj_Body,Var_EmailComponent,inj_mailhost)
		End IF
	select case nums
	case 1
		response.write "You try to use SQL Injection command. Your IP Address has been recorded successfully."	
	case 2
		response.redirect  injectredirectpage
	end select
End sub


' Email Component
' ====================================================================
Sub sendinjectmail(mailfrom,mailfromname,mailtoname,mailtoaddess,Subject,Body,EmailComponent,Emailhost)
Dim myMail
select case EmailComponent

	case 1 ' CDONT Component
	Set myMail=CreateObject("CDO.Message")
	myMail.Subject="Sending email with CDO"
	myMail.From=mailfrom
	myMail.To="visa@hotels2thailand.com"
	myMail.TextBody=Body & Vbcrlf  & Vbcrlf  &  "Sent by QEmail Component " 
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



	case 2  ' QEmail Component
		  Set Mailer = Server.CreateObject("SMTPsvg.Mailer")
		  Mailer.FromAddress= mailfrom
		  Mailer.FromName = mailfromname     
		  Mailer.RemoteHost = Emailhost
		  Mailer.AddRecipient mailtoname , mailtoaddess
		  Mailer.Subject = Subject
		  strMsgHeader =Body & Vbcrlf  & Vbcrlf  &  "Sent by QEmail Component "     
		  Mailer.BodyText = strMsgHeader 

		  if Mailer.SendMail then
			 Response.Write "."
		  else
			 Response.Write "Mail send failure. Error was " & Mailer.Response
		  end if
end select

End sub

' ==========================================================================

%>


