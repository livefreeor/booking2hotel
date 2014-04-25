<%
FUNCTION function_cart_gala_check_period(dateCheckIn,dateCheckOut,strGala,intAdult,intChildren)

	Dim bolGala
	
	bolGala = False
	strGala = LCase(strGala)
	
	IF (datecheckIn<=DateSerial(Year(Date),12,24) AND datecheckOut>DateSerial(Year(Date),12,24)) Then
		IF InStr(strGala, "christ")>0 OR InStr(strGala, "mas")>0 Then
			IF InStr(strGala, "child")>0 Then 'Christmas for childdren
				IF intChildren>0 Then
					bolGala = True
				End IF
			Else 'Christmas for adult
				bolGala = True
			End IF
		End IF
	End IF
	
	IF (datecheckIn<=DateSerial(Year(Date),12,31) AND datecheckOut>DateSerial(Year(Date),12,31)) Then
		IF InStr(strGala, "new")>0 AND InStr(strGala, "chinese")<=0 Then
			IF InStr(strGala, "child")>0 Then 'Newyear for childdren
				IF intChildren>0 Then
					bolGala = True
				End IF
			Else 'Newyear for adult
				bolGala = True
			End IF
		End IF
	End IF

	function_cart_gala_check_period = bolGala
	
END FUNCTION
%>