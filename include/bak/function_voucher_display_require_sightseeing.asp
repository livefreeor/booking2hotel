<%
FUNCTION function_voucher_display_require_sightseeing(recRequireSub)

	Dim strReturn
	Dim intCountAdult
	Dim intCountChild
	Dim strPickup
	
	
	'### Pickup Place ###
	recRequireSub.MoveFirst
	While NOT recRequireSub.EOF
		IF InStr(recRequireSub.Fields("guest"), "Pickup")>0 Then
			strPickup = recRequireSub.Fields("comment")
		End IF
		recRequireSub.MoveNext
	Wend
	'### Pickup Place ###
	
	recRequireSub.MoveFirst
	strReturn = "<br />" & VbCrlf
	strReturn = strReturn & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#000000"">" & VbCrlf
	strReturn = strReturn & "<tr>" & VbCrlf
	strReturn = strReturn & "<td colspan=""2"" bgcolor=""#FFFFFF"" class=""style12"" align=""left""><strong># "&recRequireSub.Fields("title_en")&"</strong></td>" & VbCrlf
	strReturn = strReturn & "</tr>" & VbCrlf
	strReturn = strReturn & "<tr>" & VbCrlf
	strReturn = strReturn & "<td bgcolor=""#FFFFFF"" align=""left"" class=""style12"" ><strong>Adult Name: </strong></td>" & VbCrlf
	strReturn = strReturn & "<td bgcolor=""#FFFFFF"" align=""left"" class=""style12"" >" & VbCrlf
	
	'### Adult Name ###
	recRequireSub.MoveFirst
	While NOT recRequireSub.EOF
		intCountAdult = intCountAdult + 1
		IF InStr(recRequireSub.Fields("comment"), "Adult")>0 Then
			strReturn = strReturn & intCountAdult & ". "&  recRequireSub.Fields("guest") &"<br />" & VbCrlf
		End IF
		recRequireSub.MoveNext
	Wend
	'### Adult Name ###
	
	strReturn = strReturn & "</td>" & VbCrlf
	strReturn = strReturn & "</tr>" & VbCrlf
	strReturn = strReturn & "<tr>" & VbCrlf
	strReturn = strReturn & "<td bgcolor=""#FFFFFF"" align=""left"" class=""style12"" ><strong>Children Name: </strong></td>" & VbCrlf
	strReturn = strReturn & "<td bgcolor=""#FFFFFF"" align=""left"" class=""style12"" >" & VbCrlf
	
	'### Children Name ###
	recRequireSub.MoveFirst
	While NOT recRequireSub.EOF
		intCountChild = intCountChild + 1
		IF InStr(recRequireSub.Fields("comment"), "Children")>0 Then
			strReturn = strReturn & intCountChild & ". "&  recRequireSub.Fields("guest") &"<br />" & VbCrlf
		End IF
		recRequireSub.MoveNext
	Wend
	'### Children Name ###

	strReturn = strReturn & "</td>" & VbCrlf
	strReturn = strReturn & "</tr>" & VbCrlf
	strReturn = strReturn & "<tr>" & VbCrlf
	strReturn = strReturn & "<td bgcolor=""#FFFFFF"" align=""left"" class=""style12"" ><strong>Pickup Place: </strong></td>" & VbCrlf
	
	strReturn = strReturn & "<td bgcolor=""#FFFFFF"" align=""left"" class=""style12"" >"&strPickup&"</td>" & VbCrlf
	strReturn = strReturn & "</tr>" & VbCrlf
	strReturn = strReturn & "</table>" & VbCrlf

	function_voucher_display_require_sightseeing = strReturn

END FUNCTION
%>

      
  
  
    
    
  

