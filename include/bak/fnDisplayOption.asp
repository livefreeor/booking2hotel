<%
'แสดง drop down list ตามค่าที่ถูกกำหนด
Function DisplayOption (intLimit, intSelected, intStart, intStep, intType)

	Dim i
	
	SELECT CASE intType
	
		Case 1 'Normal int
	
			For	i = 0 to (intLimit - intStart) Step intStep
					If	Cstr((intStart + i)) = Cstr(intSelected) Then
						DisplayOption = DisplayOption & "<option value=" & chr (34) & intStart + i & chr (34) & " selected>" & intStart + i & "</option>" & vbCrLf & vbTab
					Else
						DisplayOption = DisplayOption & "<option value=" & chr (34) & intStart + i & chr (34) & ">" & intStart + i & "</option>" & vbCrLf & vbTab
					End If
			Next
			
		Case 2 'Month Name
			For	i = 0 to (intLimit - intStart) Step intStep
					If	Cstr((intStart + i)) = Cstr(intSelected) Then
						DisplayOption = DisplayOption & "<option value=" & chr (34) & intStart + i & chr (34) & " selected>" & MonthName(intStart + i) & "</option>" & vbCrLf & vbTab
					Else
						DisplayOption = DisplayOption & "<option value=" & chr (34) & intStart + i & chr (34) & ">" & MonthName(intStart + i) & "</option>" & vbCrLf & vbTab
					End If
			Next
			
	END SELECT
End Function
%>