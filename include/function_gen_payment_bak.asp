<%
FUNCTION function_gen_payment(intCartID,intType)

	Dim bolKbank
	Dim bolKrungSri
	Dim sqlCat
	Dim recCat
	Dim arrCat
	Dim intCountCat
	
	bolKbank = False
	bolKrungSri = False
	
	SELECT CASE intType
	
		Case 1 'Kbank
%>
<!--			<tr>
			<td align="left"><font color="#000000">Please click here for VISA or Master Card holder</font></td>
			</tr>-->
			<tr>
			<td align="left"><input type="image" src="images/b_make_payment.gif" width="112" height="26" name="cmdkbank" value="1">
			<img src="/images/img_creditcard_visa.gif" alt="Visa Card"> <img src="/images/img_creditcard_master.gif" alt="Master Card"></td>
			</tr>
<!--			<tr>
			<td align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong><font color="#FF0000">OR</font></strong></td>
			</tr>-->
<%
		Case 2 'Krungsri
%>
<!--			<tr>
			<td align="left"><font color="#000000">Please click here for VISA or Master Card holder</font></td>
			</tr>-->
			<tr>
			<td align="left"><input type="image" src="images/b_make_payment.gif" width="112" height="26" name="cmdkrungsri" value="1">
			<img src="/images/img_creditcard_visa.gif" alt="Visa Card"> <img src="/images/img_creditcard_master.gif" alt="Master Card"></td>
			</tr>
<!--			<tr>
			<td align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong><font color="#FF0000">OR</font></strong></td>
			</tr>-->

<%
		Case 3 'By Condition
		
			'### Check Product Category ###
			sqlCat = "SELECT product_cat_id FROM tbl_cart_product WHERE cart_id= " & intCartID
			Set recCat = Server.CreateObject ("ADODB.Recordset")
			recCat.Open sqlCat, Conn,adOpenStatic,adLockreadOnly
					arrCat = recCat.GetRows()
			recCat.Close
			Set recCat = Nothing 
			
			For intCountCat=0 To Ubound(arrCat,2)
				IF arrCat(0,intCountCat)=34 OR arrCat(0,intCountCat)=35 OR arrCat(0,intCountCat)=36 Then
					bolKrungSri = True
					bolKbank = False
					Exit For
				End IF
			Next
			'### Check Product Category ###

			'### Check Time ###
			IF NOT (bolKrungSri OR bolKbank) Then
				IF Hour(dateCurrentConstant)>8 AND Hour(dateCurrentConstant)<=20 Then
					bolKrungSri = False
					bolKbank = True
				Else
					bolKrungSri = True
					bolKbank = False
				End IF
			End IF
			'### Check Time ###
			
			IF bolKrungSri Then
%>
			<tr>
			<td align="left"><input type="image" src="images/b_make_payment.gif" width="112" height="26" name="cmdkrungsri" value="1">
			<img src="/images/img_creditcard_visa.gif" alt="Visa Card"> <img src="/images/img_creditcard_master.gif" alt="Master Card"></td>
			</tr>
<%
			End IF
			
			IF bolKbank Then
%>
			<tr>
			<td align="left"><input type="image" src="images/b_make_payment.gif" width="112" height="26" name="cmdkbank" value="1">
			<img src="/images/img_creditcard_visa.gif" alt="Visa Card"> <img src="/images/img_creditcard_master.gif" alt="Master Card"></td>
			</tr>
<%
			End IF
%>

<%
		Case 999 'Temp
		
	END SELECT

END FUNCTION
%>