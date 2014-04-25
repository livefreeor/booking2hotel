<%
FUNCTION function_convert_location_id(intDestinationID,intLocationID)

	intDestinationID = Cint(intDestinationID)
	intLocationID = Cint(intLocationID)

	SELECT CASE intLocationID
	
		Case ConstDesIDBangkok 'Bangkok
			
			IF intLocationID=0 Then
				convert_location_id = "58,59,62,63,64,65,66,67,70,73"
			Else
				convert_location_id = intLocationID
			End IF
			
		Case ConstDesIDPhuket 'Phuket
		
		Case ConstDesIDChiangMai 'Chiang Mai
		
		Case ConstDesIDPattaya 'Pattaya
		
		Case ConstDesIDKohSamui 'Koh Samui
		
		Case ConstDesIDKrabi 'Krabi

	END SELECT

END FUNCTION
%>