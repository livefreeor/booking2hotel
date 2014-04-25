<!--#include virtual="/include/function_generate_health_check_up_link.asp"-->
<!--#include virtual="/include/function_generate_spa_link.asp"-->
<%
Function function_cart_display_mini()

	Dim sqlCart
	Dim arrCart
	Dim recCart
	Dim intCartID
	Dim bolCart
	Dim intCountCart
	Dim strRoom
	Dim strNight
	Dim strCourse
	Dim intPriceProduct
	Dim intPriceTotal
	Dim strGolfer
	Dim strProduct
	
	intCartID = function_cart_get_id()

	IF intCartID<>"" AND NOT ISNULL(intCartID) Then
		bolCart = True
	Else
		bolCart = False
	End IF

	IF bolCart Then
		
		sqlCart = "st_cart_display_mini " & intCartID 
		Set recCart = Server.CreateObject ("ADODB.Recordset")
		recCart.Open SqlCart, Conn,adOpenStatic, adLockReadOnly
			IF NOT recCart.EOF Then
				arrCart = recCart.GetRows()
				bolCart = True
			Else
				bolCart = False
			End IF
		recCart.Close
		Set recCart = Nothing		
	End IF
%>
<table width="163" border="0" cellspacing="0" cellpadding="0" class="f11" background="/images/bar_green02.gif">
              <tr> 
                <td height="24" align="center"><b><font color="#CC3300"><img src="/images/ico_cart_orange.gif"> &nbsp;<span class="f11">Your Booking List</span></font></b> </td>
              </tr>
</table>
<%
IF bolCart Then
	IF Ubound(arrCart,2)+1 = 1  OR Ubound(arrCart,2)+1 = 0 Then
		strProduct = "Total " & Ubound(arrCart,2)+1 & " Product"
	Else
		strProduct = "Total " & Ubound(arrCart,2)+1 & " Products"
	End IF
%>
<table width="163" border="0" cellspacing="1" cellpadding="3" bgcolor="#CAFD9F">
              <tr>
                <td align="center" bgcolor="#FBFBFF"><font color="#A5896A"><%=strProduct%></font> </td>
              </tr>
              <tr>
                <td bgcolor="#FBFBFF">
<%
For intCountCart=0 To Ubound(arrCart,2)

	IF arrCart(8,intCountCart)>1 Then
		strRoom = arrCart(8,intCountCart) & " Rooms"
	Else
		strRoom = arrCart(8,intCountCart) & " Room"
	End IF
	
	IF dateDiff("d",arrCart(2,intCountCart),arrCart(3,intCountCart))>1 Then
		strNight = dateDiff("d",arrCart(2,intCountCart),arrCart(3,intCountCart)) & " Nights"
	Else
		strNight = dateDiff("d",arrCart(2,intCountCart),arrCart(3,intCountCart)) & " Night"
	End IF
	
	IF arrCart(8,intCountCart)>1 Then
		strCourse = arrCart(8,intCountCart) & " Rounds"
	Else
		strCourse = arrCart(8,intCountCart) & " Round"
	End IF
	
	IF arrCart(10,intCountCart)>1 Then
		strGolfer = arrCart(10,intCountCart) & " Golfers"
	Else
		strGolfer = arrCart(10,intCountCart) & " Golfer"
	End IF

	intPriceProduct = function_cart_price_product(arrCart(0,intCountCart),arrCart(9,intCountCart),arrCart(1,intCountCart),arrCart(2,intCountCart),arrCart(3,intCountCart),arrCart(11,intCountCart),arrCart(12,intCountCart),arrCart(10,intCountCart),1)
	intPriceTotal = intPriceTotal + intPriceProduct
	SELECT CASE arrCart(1,intCountCart)
		Case 29 'Hotel
%>
				<p>
				 <img src="/images/bullet_orange.gif" width="5" height="5" align="absmiddle" hspace="3"> <a href="/<%=function_generate_hotel_link(arrCart(6,intCountCart),"",1)%>/<%=arrCart(7,intCountCart)%>"><%=function_display_char(arrCart(4,intCountCart),20)%></a>
				 <span class="s">
				 <br>
				 &nbsp;&nbsp;&nbsp;&nbsp;<%=strNight%>/<%=strRoom%>
				 <br>
				 &nbsp;&nbsp;&nbsp;&nbsp;<%=function_date(arrCart(2,intCountCart),1)%>-<%=function_date(arrCart(3,intCountCart),1)%>
				 </span>
				 </p>
<%
	Case 31 'Airport Trnsfer
%>
				<p>
				 <img src="/images/bullet_orange.gif" width="5" height="5" align="absmiddle" hspace="3"> <font color="#0066CC"><%=function_display_char(arrCart(4,intCountCart),20)%></font>
				 <span class="s">
				 <br>
				 &nbsp;&nbsp;&nbsp;&nbsp;<%=arrCart(8,intCountCart)%> Car
				 <br>
				 &nbsp;&nbsp;&nbsp;&nbsp;<%=function_date(arrCart(2,intCountCart),5)%>
				 </span>
				 </p>


<%
	Case 32 'Golf Course
%>
				<p>
				 <img src="/images/bullet_orange.gif" width="5" height="5" align="absmiddle" hspace="3"> <a href="/<%=function_generate_golf_link(arrCart(6,intCountCart),"",1)%>/<%=arrCart(7,intCountCart)%>"><%=function_display_char(arrCart(4,intCountCart),20)%></a>
				 <span class="s">
				 <br>
				 &nbsp;&nbsp;&nbsp;&nbsp;<%=strCourse%>/<%=strGolfer%>
				 <br>
				 &nbsp;&nbsp;&nbsp;&nbsp;<%=function_date(arrCart(2,intCountCart),4)%>
				 </span>
				 </p>
<%
	Case 34 'Sight Seeing
%>
				<p>
				 <img src="/images/bullet_orange.gif" width="5" height="5" align="absmiddle" hspace="3"> <a href="/<%=function_generate_sightseeing_link(arrCart(6,intCountCart),"",1)%>/<%=arrCart(7,intCountCart)%>"><%=function_display_char(arrCart(4,intCountCart),20)%></a>
				 <span class="s">
				 <br>
				 &nbsp;&nbsp;&nbsp;&nbsp;Adult:<%=arrCart(11,intCountCart)%> Children:<%=arrCart(12,intCountCart)%>
				 <br>
				 &nbsp;&nbsp;&nbsp;&nbsp;<%=function_date(arrCart(2,intCountCart),5)%>
				 </span>
				 </p>
<%
Case 36 'Water Activity
%>
				<p>
				 <img src="/images/bullet_orange.gif" width="5" height="5" align="absmiddle" hspace="3"> <a href="/<%=function_generate_water_activity_link(arrCart(6,intCountCart),"",1)%>/<%=arrCart(7,intCountCart)%>"><%=function_display_char(arrCart(4,intCountCart),20)%></a>
				 <span class="s">
				 <br>
				 &nbsp;&nbsp;&nbsp;&nbsp;Adult:<%=arrCart(11,intCountCart)%> Children:<%=arrCart(12,intCountCart)%>
				 <br>
				 &nbsp;&nbsp;&nbsp;&nbsp;<%=function_date(arrCart(2,intCountCart),5)%>
				 </span>
				 </p>
<%
Case 38 'Shows & Events
%>
				<p>
				 <img src="/images/bullet_orange.gif" width="5" height="5" align="absmiddle" hspace="3"> <a href="/<%=function_generate_show_event_link(arrCart(6,intCountCart),"",1)%>/<%=arrCart(7,intCountCart)%>"><%=function_display_char(arrCart(4,intCountCart),20)%></a>
				 <span class="s">
				 <br>
				 &nbsp;&nbsp;&nbsp;&nbsp;Adult:<%=arrCart(11,intCountCart)%> Children:<%=arrCart(12,intCountCart)%>
				 <br>
				 &nbsp;&nbsp;&nbsp;&nbsp;<%=function_date(arrCart(2,intCountCart),5)%>
				 </span>

			 </p>
<%	
Case 39 'Health Check Up
%>
				<p>
				 <img src="/images/bullet_orange.gif" width="5" height="5" align="absmiddle" hspace="3"> <a href="/<%=function_generate_health_check_up_link(arrCart(6,intCountCart),"",1)%>/<%=arrCart(7,intCountCart)%>"><%=function_display_char(arrCart(4,intCountCart),20)%></a>
				 <span class="s">
				 <br>
				 &nbsp;&nbsp;&nbsp;&nbsp;<%=arrCart(4,intCountCart)%>/<%=arrCart(8,intCountCart)%>
				 <br>
				 &nbsp;&nbsp;&nbsp;&nbsp;<%=function_date(arrCart(2,intCountCart),4)%>
				 </span>
				 </p>
<%
	Case 40 'Sight Seeing
%>
				<p>
				 <img src="/images/bullet_orange.gif" width="5" height="5" align="absmiddle" hspace="3"> <a href="/<%=function_generate_spa_link(arrCart(6,intCountCart),"",1)%>/<%=arrCart(7,intCountCart)%>"><%=function_display_char(arrCart(4,intCountCart),20)%></a>
				 <span class="s">
				 <br>
				 &nbsp;&nbsp;&nbsp;&nbsp;Adult:<%=arrCart(11,intCountCart)%> Children:<%=arrCart(12,intCountCart)%>
				 <br>
				 &nbsp;&nbsp;&nbsp;&nbsp;<%=function_date(arrCart(2,intCountCart),5)%>
				 </span>
				 </p>
<%
Case 999 'Tmp
%>
<%
	END SELECT
Next
%>
				</td>
              </tr>
              <tr>
                <td align="right" bgcolor="#FFFFF9" class="l1" style="border:1px #ffffff dashed"><font color="#49607A">Total:</font> <font color="#38AC11"><%=FormatNumber(intPriceTotal,0)%>&nbsp;<%=ConstCurrencyDisplay%></font> </td>
              </tr>
              <tr>
                <td align="center" bgcolor="#FFFFFF" class="s2">
				<br />
				<a href="/cart_check_out.asp"><img src="../images/book_now.gif" border="0"></a><br />
				or<br />
                  <a href="/cart_view.asp">View Full Detail </a>
				  <br />
			    </td>
              </tr>
</table>
<%
Else
%>
<table width="163" border="0" cellpadding="2" cellspacing="1" bgcolor="#CAFD9F">
  <tr>
    <td bgcolor="#FBFBFB">List is empty.</td>
  </tr>
</table>

<%
End IF
%>
<%
End Function
%>