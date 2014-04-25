<%
Dim strOuput
Dim intType

intType=Cint(Request.QueryString("id"))

	SELECT Case intType
		Case 1 '### Check Booking Status ###
			Response.Write "<a href=""http://www.hotels2thailand.com/members/order_check.asp"" target=""_blank"" title=""Check Your Booking Status""> " & VbCrlf
			Response.Write "<img src=""images/check_booking01.gif"" alt=""Check Your Booking Status"" border=""0""></a>" & VbCrlf
			
		Case 999
	END SELECT

%>

              
                
                  
              