<%
Function function_List_Box_Selected(strFields,strTable,strOrderBy,strWidth,objName,strEvent,con)
'FieldName , TableName , OrderBy , Width , Object Name ,Event ,Constant
Dim recListBox
Dim SqlListBox
Dim arrFields
Dim x
arrFields=split(strFields,"|")

Set recListBox = Server.CreateObject ("ADODB.Recordset")
SqlListBox="Select  "& arrFields(0) &","&arrFields(1)&" From "&strTable&" Order By "&strOrderBy
'response.write sqlListBox&"<br>"
recListBox.Open SqlListBox, Conn,1,3

function_List_Box_Selected=function_List_Box_Selected&"<select name="&chr(34)&objName&chr(34)&" "& strEvent &" "&">"
function_List_Box_Selected=function_List_Box_Selected&"<option value="&chr(34)&" "&chr(34)&">"&"Select"&"</option>"
Do While Not recListBox.Eof
if cstr(recListBox.Fields(trim(arrFields(0))))=cstr(con) then
	x="selected"
else
	x=""
end if
	function_List_Box_Selected=function_List_Box_Selected&  "<option value="&chr(34)&recListBox.Fields(trim(arrFields(0)))&chr(34)& x &">"&recListBox.Fields(trim(arrFields(1)))&"</option>"

recListBox.Movenext
Loop

function_List_Box_Selected=function_List_Box_Selected&"</select>"

End Function
%>

