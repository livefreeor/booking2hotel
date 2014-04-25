<%
FUNCTION function_test(intTest)
	Dim sqlTest
	Dim recTest
	Dim arrTest
		sqlTest = "SELECT TOP 1 * FROM tbl_product"
					Set recTest = Server.CreateObject ("ADODB.Recordset")
					recTest.Open SqlTest, Conn,adOpenStatic,adLockreadOnly
						'arrTest = recTest.GetRows
					recTest.Close
					Set recTest = Nothing 
					function_test = 1
END FUNCTION
%>