<%
FUNCTION function_gen_room_require(intOptionID,intOptionCount,intSmooking,intBed,intFloor,intType)

	Dim strSmookingYes
	Dim strSmokingNo
	Dim strSmokingNone
	Dim strBedKing
	Dim strBedTwin
	Dim strBedNone
	Dim strFloorHigh
	Dim strFloorLow
	Dim strFloorNone
	Dim strResult
	
	SELECT CASE Cstr(intSmooking)
		Case "1"
			strSmokingNo = "checked"
		Case "2"
			strSmookingYes = "checked"
		Case "3"
			strSmokingNone = "checked"
		Case Else
			strSmokingNone = "checked"
	END SELECT
	
	SELECT CASE Cstr(intBed)
		Case "1"
			strBedKing = "checked"
		Case "2"
			strBedTwin = "checked"
		Case "3"
			strBedNone = "checked"
		Case Else
			strBedNone = "checked"
	END SELECT
	
	SELECT CASE Cstr(intFloor)
		Case "1"
			strFloorHigh = "checked"
		Case "2"
			strFloorLow = "checked"
		Case "3"
			strFloorNone = "checked"
		Case Else
			strFloorNone = "checked"
	END SELECT

	SELECT CASE intType
		Case 1
%>
<table width="100%" cellpadding="2" cellspacing="1" bgcolor="#F5F5F5">
                <tr bgcolor="#FFFFFF">
                  <td><input type="radio" name="smoking_<%=intOptionID%>_<%=intOptionCount%>" value="1" <%=strSmokingNo%>>
                    Non-Smoking</td>
                  <td><input type="radio" name="smoking_<%=intOptionID%>_<%=intOptionCount%>" value="2" <%=strSmookingYes%>>
                    Smoking</td>
                  <td><input name="smoking_<%=intOptionID%>_<%=intOptionCount%>" type="radio" value="3" <%=strSmokingNone%>>
                    No Preference</td>
                </tr>
                <tr bgcolor="#FFFFFF">
                  <td><input type="radio" name="bed_type_<%=intOptionID%>_<%=intOptionCount%>" value="1" <%=strBedKing%>>
                    1 King size bed</td>
                  <td><input type="radio" name="bed_type_<%=intOptionID%>_<%=intOptionCount%>" value="2" <%=strBedTwin%>>
                    Twin beds</td>
                  <td><input name="bed_type_<%=intOptionID%>_<%=intOptionCount%>" type="radio" value="3" <%=strBedNone%>>
                    No Preference</td>
                </tr>
                <tr bgcolor="#FFFFFF">
                  <td><input type="radio" name="floor_<%=intOptionID%>_<%=intOptionCount%>" value="1" <%=strFloorHigh%>>
                    High Floor</td>
                  <td><input type="radio" name="floor_<%=intOptionID%>_<%=intOptionCount%>" value="2" <%=strFloorLow%>>
                    Low Floor</td>
                  <td><input name="floor_<%=intOptionID%>_<%=intOptionCount%>" type="radio" value="3" <%=strFloorNone%>>
                    No Preference</td>
                </tr>
              </table>

<%
		Case 2
%>
<table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                  <td><input type="radio" name="smoking_<%=intOptionID%>_<%=intOptionCount%>" value="1" <%=strSmokingNo%>>
                    Non-Smoking</td>
                  <td><input type="radio" name="smoking_<%=intOptionID%>_<%=intOptionCount%>" value="2" <%=strSmookingYes%>>
                    Smoking</td>
                  <td><input name="smoking_<%=intOptionID%>_<%=intOptionCount%>" type="radio" value="3" <%=strSmokingNone%>>
                    No Preference</td>
                </tr>
                <tr>
                  <td><input type="radio" name="bed_type_<%=intOptionID%>_<%=intOptionCount%>" value="1" <%=strBedKing%>>
                    1 King size bed</td>
                  <td><input type="radio" name="bed_type_<%=intOptionID%>_<%=intOptionCount%>" value="2" <%=strBedTwin%>>
                    Twin beds</td>
                  <td><input name="bed_type_<%=intOptionID%>_<%=intOptionCount%>" type="radio" value="3" <%=strBedNone%>>
                    No Preference</td>
                </tr>
                <tr>
                  <td><input type="radio" name="floor_<%=intOptionID%>_<%=intOptionCount%>" value="1" <%=strFloorHigh%>>
                    High Floor</td>
                  <td><input type="radio" name="floor_<%=intOptionID%>_<%=intOptionCount%>" value="2" <%=strFloorLow%>>
                    Low Floor</td>
                  <td><input name="floor_<%=intOptionID%>_<%=intOptionCount%>" type="radio" value="3" <%=strFloorNone%>>
                    No Preference</td>
                </tr>
              </table>
<%
		Case 3 '### For Express Checkout ###
			strResult = "<table width=""100%"" cellpadding=""2"" cellspacing=""1"">" & VbCrlf
			strResult = strResult & "<tr>" & VbCrlf
			strResult = strResult & "<td><input type=""radio"" name=""smoking_"& intOptionID & "_" & intOptionCount & """ value=""1"" " & strSmokingNo & ">Non-Smoking</td>" & VbCrlf
			strResult = strResult & "<td><input type=""radio"" name=""smoking_" & intOptionID & "_" & intOptionCount & """ value=""2"" " & strSmookingYes & ">Smoking</td>" & VbCrlf
			strResult = strResult & "<td><input name=""smoking_" & intOptionID & "_" & intOptionCount & """ type=""radio"" value=""3"" " & strSmokingNone & ">No Preference</td>" & VbCrlf
			strResult = strResult & "</tr>" & VbCrlf
			strResult = strResult & "<tr>" & VbCrlf
			strResult = strResult & "<td><input type=""radio"" name=""bed_type_" & intOptionID & "_" & intOptionCount & """ value=""1"" " & strBedKing & ">1 King size bed</td>" & VbCrlf
			strResult = strResult & "<td><input type=""radio"" name=""bed_type_" & intOptionID & "_" & intOptionCount & """ value=""2"" " & strBedTwin & ">Twin beds</td>" & VbCrlf
			strResult = strResult & "<td><input name=""bed_type_" & intOptionID & "_" & intOptionCount & """ type=""radio"" value=""3"" " & strBedNone & ">No Preference</td>" & VbCrlf
			strResult = strResult & "</tr>" & VbCrlf
			strResult = strResult & "<tr>" & VbCrlf
			strResult = strResult & "<td><input type=""radio"" name=""floor_" & intOptionID & "_" & intOptionCount & """ value=""1"" " & strFloorHigh & ">High Floor</td>" & VbCrlf
			strResult = strResult & "<td><input type=""radio"" name=""floor_" & intOptionID & "_" & intOptionCount & """ value=""2"" " & strFloorLow & ">Low Floor</td>" & VbCrlf
			strResult = strResult & "<td><input name=""floor_" & intOptionID & "_" & intOptionCount & """ type=""radio"" value=""3"" " & strFloorNone & ">No Preference</td>" & VbCrlf
			strResult = strResult & "</tr>" & VbCrlf
			strResult = strResult & "</table>" & VbCrlf

			function_gen_room_require = strResult
			
		Case 4
	END SELECT
END FUNCTION
%>