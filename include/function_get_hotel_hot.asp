
<%
Function function_get_hotel_hot(intDest,intType)

	Dim sqlHotel
	Dim rsHotel
	Dim arrHotel
	Dim strHotel
	Dim intHotel
	Dim sqlGroup
	Dim rsGroup
	Dim arrList
	Dim hotel_list
	Dim intList
	Dim strList
	
	sqlGroup="select hotel_list from tbl_hotel_hot where destination_id="&intDest
	
	Set rsGroup=server.CreateObject("adodb.recordset")
	rsGroup.Open sqlGroup,conn,1,3
	hotel_list=rsGroup("hotel_list")
	arrList=split(hotel_list,",")
	For intList=0 to Ubound(arrList)
		strList=strList&"'"&arrList(intList)&"',"
	Next
	strList=trim(mid(strList,1,len(strList)-1))
	
	sqlHotel="select product_id from tbl_product where product_code IN ("&strList&")"
	Set rsHotel=server.CreateObject("adodb.recordset")
	rsHotel.Open sqlHotel,conn,1,3
	arrHotel=rsHotel.getRows()
	strHotel=""
	For intHotel=0 to Ubound(arrHotel,2)
		strHotel=strHotel&arrHotel(0,intHotel)&","
	Next
	strHotel=trim(mid(strHotel,1,len(strHotel)-1))
	function_get_hotel_hot=strHotel
End Function
%>