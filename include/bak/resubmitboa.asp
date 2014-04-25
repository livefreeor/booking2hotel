<%OPTION EXPLICIT%>
<!--#include file="admin/include/Constant.asp"--> 
<!--#include file="include/inc_constant_boa.asp"-->
<!--#include file="include/inc_boamsg.asp"-->
<%
Dim sqlBoa, strBoa
Dim strBoaEncrypt
Dim strOrderID
Dim sqlPrice
Dim recPrice
Dim intPrice

strOrderID = MID(Request("order_id"),5,5)
'sqlBoa = "SELECT boa_msg FROM tbl_boa WHERE order_id=" & strOrderID & " AND direction=0"
'strBoa = conn.Execute(sqlBoa).GetString

'conn.Close
'Set conn = Nothing
'strBoa = encrypt_boa(strBoa, 0)

sqlPrice = "SELECT SUM(price) AS price FROM tbl_order_item WHERE order_id=" & strOrderID
Set recPrice = Server.CreateObject ("ADODB.Recordset")
	recPrice.Open SqlPrice, Conn,adOpenForwardOnly,adLockReadOnly
		intPrice = recPrice.Fields("price")
	recPrice.Close
	Set recPrice = Nothing

strBoa = CreateRequestBoaMsg (BOA_TRANS_AUTHORIZE_CODE, BOA_CURRENCY_BAHT, BOA_MERCHANT_ID, strOrderID, intPrice, "", "", BOA_PAYMENT_CREDIT, BOA_SETTLEMENT_ESETTLEMENT)
'strBoaEncrypt = encrypt_boa(strBoa, 0)
strBoaEncrypt = strBoa 
sqlBoa = "INSERT INTO tbl_boa (order_id,boa_msg,direction) VALUES ("& strOrderID &","& Chr(39) & strBoa & Chr(39) &",0)"
conn.Execute sqlBoa
%>

<html>
<!--<body onLoad="payment.submit()">
<form name="payment" action="https://boaepay.boa.co.th/boapayment/boapayment.php" method="post">
<input type="hidden" name="boamsg" value="<%'=strBoa%>">
</form>
</body>-->

<body onLoad="CreditForm.submit()">
<form name="CreditForm" action="https://boaepay.boa.co.th/boapayment/boapayment.php" method="post">
<input type="hidden" name="boamsg" value="<%=strBoaEncrypt%>">
</form>
</body>
</html>