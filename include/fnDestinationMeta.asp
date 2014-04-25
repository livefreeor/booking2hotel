<%
Function fnDestinationMeta(intDestinationID)

	Dim strKey
	Dim strDescription

	SELECT CASE intDestinationID
		Case 30 'Bangkok
			strKey = "Bangkok hotels, Bangkok hotel, asia hotels Bangkok, asia hotels Bangkok, Thailand Bangkok hotels, Thailand Bangkok hotel"
			strDescription = "Bangkok Hotels List of Bangkok Hotels discount up to 75% off rack rates including Ambassdor Bangkok,Oriental Bangkok,Bangkok Airport hotel and all hotels in Bangkok"
		Case 31 'Phuket
			strKey = "Phuket hotels, Phuket hotel,hotels in Phuket,hotel in Phuket, Thailand Phuket hotels, Thailand Phuket hotel"
			strDescription = "Phuket Hotels List of Phuket Hotels discount up to 75% off rack rates including  Patong beach hotels,arcadia  Phuket, Evason Phuket, Marriott Phuket and all hotels in phuket  "
		Case 32 'Chaing Mai
			strKey = "Chaing Mai hotels, Chaing Mai hotel, asia hotels Chaing Mai, asia hotels Chaing Mai, Thailand Chaing Mai hotels, Thailand Chaing Mai hotel"
			strDescription = "Chaing Mai Hotels List of Chaing Mai Hotels discount up to 75% off rack rates including all resorts in Chaing Mai"
		Case 33 'Pattaya
			strKey = "Pattaya hotels, Pattaya hotel,hotels in Pattaya,hotel inPattaya, Thailand Pattayahotels, Thailand Pattaya hotel"
			strDescription = "Pattaya Hotels List of Pattaya Hotels discount up to 75% off rack rates including  Pattaya center hotels,Jomtien Pattaya hotels, Pattaya North hotels, Pattaya South hotels and all hotels in phuket  "
		Case 34 'Samui
			strKey = "Koh Samui hotels, Koh Samui hotel, Koh Samui resorts, Koh Samui resort,hotels in Koh Samui,hotel in Koh Samui, Ko Samui hotels, Ko Samui hotel, Ko Samui bungalows, Ko Samui bungalow"
			strDescription = "Koh Samui Hotels List of Koh Samui Hotels discount up to 75% off rack rates including all resorts and bungalows in Koh Samui"
		Case 35 'Krabi
			strKey = "Krabi hotels, Krabi hotel, hotels in Krabi, hotel in Krabi, Thailand Krabi hotels, Thailand Krabi hotel"
			strDescription = "Krabi Hotels List of Krabi Hotels discount up to 75% off rack rates including all resorts in Krabi"
		Case Else
			strKey = "Thailand hotels,Thailand hotel,Bangkok hotels, Bangkok hotel, Thailand accommodations, Thailand accommodation"
			strDescription = "Thailand Hotels List of Thailand Hotels discount up to 75% off rack rates including hotels in Bangkok,Pattaya,Phuket,Koh Samui,Samet,Chiang Mai"
	END SELECT
	
	fnDestinationMeta = "<META NAME=""Keywords"" CONTENT="""& strKey&""">" & VbCrlf
	fnDestinationMeta = fnDestinationMeta & "<META NAME=""Description"" CONTENT="""& strDescription &""">" & VbCrlf
	fnDestinationMeta = fnDestinationMeta & "<META NAME=""Author"" CONTENT=""Hotels 2 Thailand"">" & VbCrlf
	fnDestinationMeta = fnDestinationMeta & "<META NAME=""Copyright"" CONTENT=""Hotels 2 Thailand"">" & VbCrlf

End Function
%>



