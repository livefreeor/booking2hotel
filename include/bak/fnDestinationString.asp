<%
FUNCTION fnDestinationString(intDestinationID,intType)

	SELECT CASE intType
		Case 1 'File Path
			SELECT CASE Cstr(intDestinationID)
				Case "30"
					fnDestinationString = "bangkok-hotels"
				Case "31"
					fnDestinationString = "phuket-hotels"
				Case "32"
					fnDestinationString = "chiang-mai-hotels"
				Case "33"
					fnDestinationString = "pattaya-hotels"
				Case "34"
					fnDestinationString = "koh-samui-hotels"
				Case "35"
					fnDestinationString = "krabi-hotels"
			END SELECT 
	END SELECT
	
END FUNCTION
%>