
<%
Dim BlackList,s
	
BlackList=array("insert","update","cast","declare") 

Function CheckStringForSQL(str)

  'On Error Resume Next 
  
  Dim lstr 
  
  ' If the string is empty, return true
  If ( IsEmpty(str) ) Then
    CheckStringForSQL = false
    Exit Function
  ElseIf ( StrComp(str, "") = 0 ) Then
    CheckStringForSQL = false
    Exit Function
  End If

  lstr = LCase(str)
  
  ' Check if the string contains any patterns in our
  ' black list
  For Each s in BlackList
    If ( InStr (lstr, s) <> 0 ) Then
		Select Case s
			Case "cast"
				If (( InStr (lstr, "(") <> 0 ) and ( InStr (lstr, ")") <> 0 )) or ((InStr(lstr,"%28")<>0) and (InStr(lstr,"%29")<>0)) Then
					 CheckStringForSQL = true
					 Exit Function
				end If
			Case "update"
				If ( InStr (lstr, "set") <> 0 ) Then
					 CheckStringForSQL = true
					 Exit Function
				end If				
			Case "insert"
				If ( InStr (lstr, "into") <> 0 ) and ( InStr (lstr, "values") <> 0 ) Then
					 CheckStringForSQL = true
					 Exit Function
				end If
			Case Else
					 CheckStringForSQL = true
					 Exit Function				
		End Select
    End If
  
  Next
  
  CheckStringForSQL = false
  
End Function


'response.write request.Form
Sub subFormInjection()

For Each s in Request.Form

  If ( CheckStringForSQL(Request.Form(s)) ) Then

    ' Redirect to an error page
    'Response.Redirect(ErrorPage)
	response.write "Error"
	response.end
  
  End If
Next

For Each s in Request.Form

  If ( CheckStringForSQL(replace(Request.Form(s),"%","" ) ))Then

    ' Redirect to an error page
    'Response.Redirect(ErrorPage)
	response.write "Error"
	response.end
  
  End If
Next

End Sub
%>