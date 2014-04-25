<%
FUNCTION function_recent_view(intProductID,intType)

Dim bolDuplicate
Dim arrProductID
Dim intCount
Dim k
Dim sqlProduct
Dim recProduct
Dim arrProduct
Dim bolProduct

	SELECT CASE intType
	
		Case 1 'Add Product ID To Session
			Session.Timeout = 1200
			
			IF Session("recent") = "" Then
				Session("recent") = intProductID
			Else
				
				bolDuplicate = True
				
				arrProductID = Split(Session("recent"),",")
				
				For k=0 To Ubound(arrProductID)
					IF Cstr(arrProductID(k)) = Cstr(intProductID) Then
						bolDuplicate = False
					End IF
				Next
				
				IF bolDuplicate Then
					Session("recent") = Session("recent") & "," & intProductID
				End IF
				
			End IF
		Case 2 'Display Recent View
			IF Session("recent")<>"" Then
			
				'sqlProduct = "SELECT product_id,title_en,files_name,destination_id,product_cat_id FROM tbl_product WHERE product_cat_id=29 AND product_id IN ("&Session("recent")&")"
				sqlProduct = "st_hotel_detail_recent '" & Session("recent") & "'"
				Set recProduct = Server.CreateObject ("ADODB.Recordset")
				recProduct.Open SqlProduct, Conn,adOpenStatic,adLockreadOnly
					IF NOT recProduct.EOF Then
						arrProduct = recProduct.GetRows()
						bolProduct = True
					Else
						bolProduct = False
					End IF
				recProduct.Close
				Set recProduct = Nothing 
		
%>
<%
IF bolProduct Then 
%>
<table width="163" border="0" cellspacing="0" cellpadding="0" class="f11" background="/images/b_blue_155.gif">
	<tr> 
		<td height="24" align="center"><b><font color="346494">Recently Viewed Items</font></b> </td>
	</tr>
</table>
<table width="163" border="0" cellspacing="1" cellpadding="3" bgcolor="97BFEC" id="hotels_list">
<form action="/recent.asp">
              <tr> 
                <td bgcolor="#FFFFFF">
				<%For intCount=0 to Ubound(arrProduct,2)%>
					<li><a href="/<%=function_generate_hotel_link(arrProduct(3,intCount),"",1)%>/<%=arrProduct(2,intCount)%>"><%=arrProduct(1,intCount)%></a></li>
				<%Next%>
                </td>
              </tr>
              <tr>
                <td bgcolor="#FFFFFF" align="center"><input type="image" src="/images/b_view_all.gif" width="70" height="22"></td>
              </tr>
</form>
</table>
<%
End IF
%>
<%
		End IF
	
	Case 3'For temp
	END SELECT

END FUNCTION
%>