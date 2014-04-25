<%
FUNCTION functionPaymentBangkokBank()
	IF Request("successcode")<>"" AND NOT ISNULL(Request("successcode")) Then
		IF Request("successcode") = "0" Then
			functionPaymentBangkokBank = int(Request("Ref"))
		Else
			functionPaymentBangkokBank = 0
		End IF
	Else
		functionPaymentBangkokBank = 0
	End IF
END FUNCTION
%>
