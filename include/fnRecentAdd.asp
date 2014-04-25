<%
Sub fnRecentAdd(intProductID)
	
	Dim bolDuplicate
	Dim arrProductID
	Dim k
	
	Session.Timeout = 1200
	
	IF Session("recent") = "" Then
		Session("recent") = intProductID
	Else
		
		bolDuplicate = True
		
		arrProductID = Split(Session("recent"),",")
		
		For k=0 To Ubound(arrProductID)
			IF Cstr(arrProductID(k)) = Cstr(intProductID) Then
				bolDuplicate = False
			End IF
		Next
		
		IF bolDuplicate Then
			Session("recent") = Session("recent") & "," & intProductID
		End IF
		
	End IF
	
END Sub
%>