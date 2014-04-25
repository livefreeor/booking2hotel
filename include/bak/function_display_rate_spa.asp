<%
Function function_display_rate_spa(intProduct,intType)
Dim sqlRate
Dim rsRate
Dim arrRate
Dim intRate
Dim strDetail
Dim intRowspan
Dim option_temp
Dim option_title

sqlRate="select po.option_id,po.title_en,op.date_start,op.date_end,op.price,po.show_option,"
sqlRate=sqlRate&" (select top 1 sp.files_name from tbl_product sp where sp.product_id=po.product_id)as filesname,time_use,p.destination_id"
sqlRate=sqlRate&" from tbl_product_option po,tbl_option_price op,tbl_product p"
sqlRate=sqlRate&" where p.product_id=po.product_id AND po.option_id=op.option_id and date_end>getdate() and po.product_id="&intProduct &" and po.status = 1 "
sqlRate=sqlRate&" Order By po.option_id asc"

Set rsRate=server.CreateObject("adodb.recordset")
rsRate.open sqlRate,conn,1,3
IF Not rsRate.Eof Then
	arrRate=rsRate.getRows()
End IF
rsRate.close()
Set rsRate=Nothing

IF Isarray(arrRate) Then
	Select Case intType
	Case 1
		strDetail="<table width=""98%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4""><tr align='center'align=""center"" bgcolor=""#EDF5FE""><td>Title</td><td>Mins</td><td>Period</td><td>Price Currency<br><font color=""#990066"">"&Session("currency_title") & "&nbsp; <img src=""/images/flag_"&Session("currency_code")&".gif"">"&"</font></td></tr>"&vbcrlf
		For intRate=0 to Ubound(arrRate,2)
			IF arrRate(5,intRate) Then
				option_title="<a href=""javascript:popup('"&function_generate_spa_link(arrRate(8,intRate),"",1)&"/"&replace(arrRate(6,intRate),".asp","_spa_"&arrRate(0,intRate))&".asp',600,600)"">"&arrRate(1,intRate)&"</a>"
			Else
				option_title=arrRate(1,intRate)
			End IF
			IF arrRate(0,intRate)<>option_temp Then
				strDetail=replace(strDetail,"intRowspan",intRowspan)
				strDetail=strDetail&"<tr align=""center"" bgcolor=""#FFFFFF""><td rowspan='intRowspan' align='left'>"&option_title&"</td><td>"&arrRate(7,intRate)&"</td><td>"&function_date(arrRate(2,intRate),3)&"-"&function_date(arrRate(3,intRate),3)&"</td><td>"&function_display_price(arrRate(4,intRate),1)&"</td></tr>"&vbcrlf
				intRowspan=1
			Else
				strDetail=strDetail&"<tr align=""center"" bgcolor=""#FFFFFF""><td>"&function_date(arrRate(2,intRate),3)&"-"&function_date(arrRate(3,intRate),3)&"</td><td>"&function_display_price(arrRate(4,intRate),1)&"</td></tr>"&vbcrlf
				intRowspan=intRowspan+1
			End IF
			option_temp=arrRate(0,intRate)
		Next
		strDetail=replace(strDetail,"intRowspan",intRowspan)
		strDetail=strDetail&"</table>"&vbcrlf
	Case 2
		strDetail="<table width=""98%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4""><tr align='center'align=""center"" bgcolor=""#EDF5FE""><td>Sel</td><td>Quantity</td><td>Title</td><td>Mins</td><td>Period</td><td>Price Currency<br><font color=""#990066"">"&Session("currency_title") & "&nbsp; <img src=""/images/flag_"&Session("currency_code")&".gif"">"&"</font></td></tr>"&vbcrlf
		For intRate=0 to Ubound(arrRate,2)
			IF arrRate(5,intRate)=true Then
				option_title="<a href=""javascript:popup('"&replace(arrRate(6,intRate),".asp","_spa_"&arrRate(0,intRate))&".asp',600,600)"">"&arrRate(1,intRate)&"</a>"
			Else
				option_title=arrRate(1,intRate)
			End IF
			IF arrRate(0,intRate)<>option_temp Then
				strDetail=replace(strDetail,"intRowspan",intRowspan)
				strDetail=strDetail&"<tr align=""center"" bgcolor=""#FFFFFF""><td rowspan='intRowspan'><input type='checkbox' name='option_id' value='"&arrRate(0,intRate)&"'></td><td rowspan='intRowspan'>"&function_gen_dropdown_number(1,9,"","qty"&arrRate(0,intRate),1)&"</td><td rowspan='intRowspan' align='left'>"&option_title&"</td><td>"&arrRate(7,intRate)&"</td><td>"&function_date(arrRate(2,intRate),3)&"-"&function_date(arrRate(3,intRate),3)&"</td><td>"&function_display_price(arrRate(4,intRate),1)&"</td></tr>"&vbcrlf
				intRowspan=1
			Else
				strDetail=strDetail&"<tr align=""center"" bgcolor=""#FFFFFF""><td>"&function_date(arrRate(2,intRate),3)&"-"&function_date(arrRate(3,intRate),3)&"</td><td>"&function_display_price(arrRate(4,intRate),1)&"</td></tr>"&vbcrlf
				intRowspan=intRowspan+1
			End IF
			option_temp=arrRate(0,intRate)
		Next
		strDetail=replace(strDetail,"intRowspan",intRowspan)
		strDetail=strDetail&"</table>"&vbcrlf
	End Select
Else
    strDetail=strDetail&"<p><font color=""red""><b>Sorry, We don't have rate for this product.</b></font><br><br> Please contact us at <a href=""mailto:support@hotels2thailand.com"">support@hotels2thailand.com</a></p>"
End IF
function_display_rate_spa=strDetail
End Function
%>
