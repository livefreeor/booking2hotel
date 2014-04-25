<%
Function fnDestinationKeyword (intDestinationID)
	SELECT CASE intDestinationID
		Case 30 'Bangkok
			fnDestinationKeyword = "Bangkok Hotels"
		Case 31 'Phuket
			fnDestinationKeyword = "Phuket Hotels"
		Case 32 'Chaing Mai
			fnDestinationKeyword = "Chaing Mai Hotels"
		Case 33 'Pattaya
			fnDestinationKeyword = "Pattaya Hotels"
		Case 34 'Samui
			fnDestinationKeyword = "Koh Samui Hotels"
		Case 35 'Krabi
			fnDestinationKeyword = "Krabi Hotels"
	END SELECT
End Function
%>