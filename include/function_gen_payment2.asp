<%
FUNCTION function_gen_payment(intCartID,intType)
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
	Set rsGateway=server.CreateObject("adodb.recordset")
	sqlGateway="select plan_id from tbl_gateway_plan where status=1"
	rsGateway.Open sqlGateway,conn,1,3
		arrGateway=rsGateway.getRows()
	rsGateway.close
	Set rsGateway=Nothing
	
	'Select Case arrGateway(0,0)
	Select Case 3
	Case 1 'kbank only
	%>
	<tr>
	<td align="left" valign="top"><input type="radio" name="payBank" value="3" checked="checked"/></td>
	<td><img src="/images/logo_visa.gif" alt="Visa Card">&nbsp;&nbsp;<img src="/images/logo_mastercard.gif" alt="Master Card"></td>
	</tr>
    <tr>
	<td align="left" valign="top"><input type="radio" name="payBank" value="5" /></td>
	<td><img src="/images/logo_jcb.gif" alt="JCB"> </td>
    </tr>
    <tr>
    <td colspan="2"><input type="image" src="/images/b_make_payment.gif" width="112" height="26" name="cmdkbank" value="1"></td>
	</tr>
	<%
	Case 2 'krungsri only
	%>
	<tr>
	<td align="left"><input type="radio" name="payBank" value="4" checked="checked"/></td>
    <td>
	<img src="/images/img_creditcard_visa.gif" alt="Visa Card"> <img src="/images/img_creditcard_master.gif" alt="Master Card"></td>
    </tr>
    <tr>
    <td colspan="2"><input type="image" src="/images/b_make_payment.gif" width="112" height="26" name="cmdkbank" value="1"></td>
	</tr>
    <%
	Case 3 'BBL
	%>
	<tr>
	<td align="left" valign="top"><input type="radio" name="payBank" value="5" checked="checked"/></td>
    <td>
	<img src="/images/logo_visa.gif" alt="Visa Card">&nbsp;&nbsp;<img src="/images/logo_mastercard.gif" alt="Master Card">&nbsp;&nbsp;<img src="/images/logo_jcb.gif" alt="JCB">
    </td>
    </tr>
    <tr>
    <td colspan="2"><input type="image" src="/images/b_make_payment.gif" width="112" height="26" name="cmdkbank" value="1"></td>
	</tr>
	<%
	Case 999
			
	End Select
END FUNCTION
%>