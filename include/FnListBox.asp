<%
Function fnListBox(strName,strValue,strTable,strOrderBy,strcon,objName,strEvent)

	Dim recListBox
	Dim SqlListBox
	
	SqlListBox="SELECT "& strName &","& strValue &" FROM "&strTable&" ORDER BY "&strOrderBy
	Set recListBox = Server.CreateObject ("ADODB.Recordset")
	recListBox.Open SqlListBox, Conn,1,3
		
	
	fnListBox=fnListBox&"<select name="&chr(34)&objName&chr(34)&" "& strEvent &" "&">"
	
	While Not recListBox.Eof
	
		IF NOT (Cstr(recListBox.Fields(strValue))=strcon) then
			fnListBox=fnListBox&  "<option value="&chr(34)&recListBox.Fields(strValue)&chr(34)&">"&recListBox.Fields(strName)&"</option>"
		Else
			fnListBox=fnListBox&  "<option value="&chr(34)&recListBox.Fields(strValue)&chr(34)&" selected>"&recListBox.Fields(strName)&"</option>"
		End IF
	
	recListBox.Movenext
	Wend
	
	fnListBox=fnListBox&"</select>"

End Function
%>

