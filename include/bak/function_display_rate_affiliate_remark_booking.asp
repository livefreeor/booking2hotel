<%
FUNCTION function_display_rate_affiliate_remark_booking(intProductID,intPsID,intType)

	Dim sqlRemark
	Dim recRemark
	Dim bolRemark
	Dim arrRemark
	Dim strRemark
	Dim intCountRemark
	
	sqlRemark = "st_hotel_detail_special " & intProductID
	
	Set recRemark = Server.CreateObject ("ADODB.Recordset")
	recRemark.Open SqlRemark, Conn,adOpenStatic,adLockreadOnly
		IF NOT recRemark.EOF Then
			arrRemark = recRemark.GetRows()
			bolRemark = True
		Else
			bolRemark = False
		End IF
	recRemark.Close
	Set recRemark = Nothing 
	
	strRemark = "<table cellspacing=1 cellpadding=2 width=""100%"" bgcolor=#e4e4e4 class=""f11"">" & VbCrlf
	strRemark = strRemark & "<tbody>" & VbCrlf
	strRemark = strRemark & "<tr>" & VbCrlf
	strRemark = strRemark & "<td align=left bgcolor=#ffffff>" & VbCrlf
	strRemark = strRemark & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""3"" class=""f11"">" & VbCrlf
	strRemark = strRemark & "<tr>" & VbCrlf
	strRemark = strRemark & "<td height=""25"" class=""step_bg_color""><span class=""step_text"">Remarks</span></td>" & VbCrlf
	strRemark = strRemark & "</tr>" & VbCrlf
	strRemark = strRemark & "</table>" & VbCrlf
	strRemark = strRemark & "<ul>" & VbCrlf
	'strRemark = strRemark & "<li><font color=CC3300>Room rates are <b>inclusive</b> of 10% service charge and applicable government tax.</font></li>" & VbCrlf
	strRemark = strRemark & "<li><font color=CC3300>Room rates are EXCLUDED of Tax and Service Charge.</font></li>" & VbCrlf
	
	IF bolRemark Then
		For intCountRemark=0 To Ubound(arrRemark,2)
			strRemark = strRemark & "<li>"&arrRemark(0,intCountRemark)&"</li>" & VbCrlf
		Next
	End IF
	
	strRemark = strRemark & "</ul>" & VbCrlf
	strRemark = strRemark & "</td>" & VbCrlf
	strRemark = strRemark & "</tr>" & VbCrlf
	strRemark = strRemark & "</tbody>" & VbCrlf
	strRemark = strRemark & "</table>" & VbCrlf

	function_display_rate_affiliate_remark_booking = strRemark
END FUNCTION
%>
              
            
          
        