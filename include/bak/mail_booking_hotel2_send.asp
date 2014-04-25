<!--#include virtual="/include/functionMailSendCDO_NoCC.asp"-->
<%
strVch = request.Form("strVch")
	On Error Resume Next
	'response.Write(strVch)
	call functionMailSendCDO_NoCC(constMailServer,25,"reservation@hotels2thailand.com","witchuda.s@hotels2thailand.com","reservation@hotels2thailand.com","bhtg0ibPq","Joop test 1/(2-09-2009)",strVch,true,1,attach)
			
'	IF Err.Number <> 0 then 'Resend
'		Response.Write("Send Error Please Try Again !")
'	End If

%>