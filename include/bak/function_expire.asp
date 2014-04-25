<%
Function function_expire(arrCheck,intProduct,intCheckMonth)
Dim intCheck
IF Isarray(arrCheck) Then
		For intCheck=0 to Ubound(arrCheck,2)
			IF arrCheck(0,intCheck)=intProduct Then
				'IF (arrCheck(2,intCheck) <> "") Then
					IF int(datediff("M",date,arrCheck(2,intCheck)))	<= int(intCheckMonth) Then
						function_expire="<font color='red'>(Expire Date : "& function_date(arrCheck(2,intCheck),8)&")</font>"
						Exit For
					End IF
				'End IF
			End IF	
		Next
End IF
End Function
%>