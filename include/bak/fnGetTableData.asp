<%
Function fnGetTableData(strField,strTable,strCondition)

	Dim recGetData
	Dim SqlGetData
	Dim arrGetData
	
	sqlGetData = "SELECT " & strField & " FROM " & strTable & " WHERE " & strCondition
	
	Set recGetData = Server.CreateObject ("ADODB.Recordset")
	recGetData.Open SqlGetData, Conn,adOpenForwardOnly,adLockReadOnly
		IF NOT recGetData.eof Then
			fnGetTableData = recGetData.Fields(strField)
		Else
			fnGetTableData = "-"
		End IF
	recGetData.Close
	Set recGetData = Nothing


End Function
%>

