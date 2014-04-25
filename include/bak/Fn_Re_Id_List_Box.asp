<%
Function Fn_Re_Id_List_Box(strShowValue,strTable,condition1)
'FieldName , TableName , condition
Dim recShowValue
Dim SqlShowValue
Dim arrShowValue

if not trim(condition1) = "" then

	arrShowValue=split(strShowValue,"|")

	Set recShowValue = Server.CreateObject ("ADODB.Recordset")
	SqlShowValue="Select  "& arrShowValue(0) &","&arrShowValue(1)&" From "&strTable&" Where "& arrShowValue(0)&"="&condition1
	recShowValue.Open SqlShowValue, Conn,1,3
		if not recShowValue.eof then
			Fn_Re_Id_List_Box=recShowValue(arrShowValue(1))
		else
			Fn_Re_Id_List_Box= "-"
		end if
	recShowValue.Close
	Set recShowValue = Nothing
	
Else
	Fn_Re_Id_List_Box= "-"

end if


End Function
%>

