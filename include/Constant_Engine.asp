<%
Dim strConn
Dim conn
Session.Timeout = 1200
strConn="Provider=SQLOLEDB;Data source=bluehouseserver;database=refer_hotels;uid=sa;pwd=;"

Sub connOpen()
	Set Conn= server.CreateObject("ADODB.Connection")
	Conn.Open strConn
End Sub

Sub connClose()
	Conn.Close
	Set Conn = Nothing
End Sub
%>