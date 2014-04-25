<%
FUNCTION function_gen_payment_new(intCartID,intType)

	Dim rsGateway
	Dim sqlGateway
	Dim arrGateway
	Dim bolCheck
	Dim bolKrungsri
	Dim bolKbank
	Dim sqlCat
	Dim recCat
	Dim arrCat
	Dim intCountCat
	Dim intCheck
	Dim date_check
	Dim dateCurrentConstant1
	Set rsGateway=server.CreateObject("adodb.recordset")
	sqlGateway="select plan_id,time_start,time_end,day_mon,day_tue,day_wed,day_thu,day_fri,day_sat,day_sun from tbl_gateway_plan where plan_id=4"
	rsGateway.Open sqlGateway,conn,1,3
		arrGateway=rsGateway.getRows()
	rsGateway.close
	Set rsGateway=Nothing
	arrGateway(0,0)=4 'type
	arrGateway(1,0)=8 'Start
	arrGateway(2,0)=16 'End
	'dateCurrentConstant1=dateadd("h",7,now())
	dateCurrentConstant1=now
	Select Case arrGateway(0,0)
	Case 1 'kbank only
	%>
	<tr>
	<td align="left">kbank1<input type="image" src="images/b_make_payment.gif" width="112" height="26" name="cmdkbank" value="1">
	<img src="/images/img_creditcard_visa.gif" alt="Visa Card"> <img src="/images/img_creditcard_master.gif" alt="Master Card"></td>
	</tr>
	<%
	Case 2 'krungsri only
	%>
	<tr>
	<td align="left">krungsri1<input type="image" src="images/b_make_payment.gif" width="112" height="26" name="cmdkrungsri" value="1">
	<img src="/images/img_creditcard_visa.gif" alt="Visa Card"> <img src="/images/img_creditcard_master.gif" alt="Master Card"></td>
	</tr>
	<%
	Case 3,4 'Set Peroid
	bolCheck=true
	'### Check Product Category ###
			sqlCat = "SELECT product_cat_id FROM tbl_cart_product WHERE cart_id= " & intCartID
			Set recCat = Server.CreateObject ("ADODB.Recordset")
			recCat.Open sqlCat, Conn,adOpenStatic,adLockreadOnly
					arrCat = recCat.GetRows()
			recCat.Close
			Set recCat = Nothing 
			arrCat(0,0)=30
			For intCountCat=0 To Ubound(arrCat,2)
				IF arrCat(0,intCountCat)=34 OR arrCat(0,intCountCat)=35 OR arrCat(0,intCountCat)=36 Then
					bolKrungSri = True
					bolKbank = False
					Exit For
				End IF
			Next
			'### Check Product Category ###
			'### Check Time Period
			
			
			IF Hour(dateCurrentConstant1)>arrGateway(1,0) AND Hour(dateCurrentConstant1)<=arrGateway(2,0) Then
				bolCheck=True
			Else
				bolCheck=False
			End IF
			response.write arrGateway(2,0)
			response.write bolCheck&"<br>"
			'### Check Time Period
			
			select case weekday(dateCurrentConstant1)
			case 1 'sun
				date_check=arrGateway(9,0)
			case 2
			date_check=arrGateway(3,0)
			case 3
			date_check=arrGateway(4,0)
			case 4
			date_check=arrGateway(5,0)
			case 5
			date_check=arrGateway(6,0)
			case 6
			date_check=arrGateway(7,0)
			case 7 'sat
			date_check=arrGateway(8,0)
			end select
			'### Check Date
			
				IF date_check=0 Then
					bolCheck=false
				End IF
			
			'response.write weekday(dateCurrentConstant)
			'### Check Date
			
			IF bolKrungsri or ((arrGateway(0,0)=4) and (bolCheck=true)) Then
			response.write "krungsri"
			%>
			<tr>
			<td align="left">4<input type="image" src="images/b_make_payment.gif" width="112" height="26" name="cmdkrungsri" value="1">
			<img src="/images/img_creditcard_visa.gif" alt="Visa Card"> <img src="/images/img_creditcard_master.gif" alt="Master Card"></td>
			</tr>
			<%
			Else
			response.write "kbank"
			%>
			<tr>
			<td align="left">3<input type="image" src="images/b_make_payment.gif" width="112" height="26" name="cmdkbank" value="1">
			<img src="/images/img_creditcard_visa.gif" alt="Visa Card"> <img src="/images/img_creditcard_master.gif" alt="Master Card"></td>
			</tr>
			<%
			End IF
	Case 999
			
	End Select
END FUNCTION
%>