<%
FUNCTION function_gen_review_box_spa(intProductID,intType)
	
	Dim sqlReview
	Dim recReview
	Dim arrReview
	Dim bolReview
	Dim intAvgPoint
	
	sqlReview = "SELECT COUNT(*) AS num_review,SUM(rate_overall) AS num_rate"
	sqlReview = sqlReview & " FROM tbl_product_review_spa "
	sqlReview = sqlReview & " WHERE product_id=" & intProductID & " AND status=1"
	
	Set recReview = Server.CreateObject ("ADODB.Recordset")
	recReview.Open SqlReview, Conn,adOpenStatic,adLockreadOnly
		arrReview = recReview.GetRows()
	recReview.Close
	Set recReview = Nothing 
	
	IF arrReview(0,0)=0 Then
		bolReview = False
	Else
		bolReview = True
		intAvgPoint = arrReview(1,0)/arrReview(0,0)
	End IF

SELECT CASE intType
	Case 1 'Detail
	IF bolReview Then
%>
	<table width="100%" cellpadding="2"  cellspacing="1" bgcolor="#F4F4F4">
		<tr>
			<td align="center" bgcolor="#FFFFF9"><font color="#990000">Average user rating:</font> <%=function_generate_star_rate(intAvgPoint,intProductID,1)%> <i>(<%=FormatNumber(intAvgPoint,1)%> from 5 )</i></td>
		</tr>
		<tr>
			<td align="center" bgcolor="#FFFFF9"><a href="/review_spa.asp?id=<%=intProductID%>">view <%=arrReview(0,0)%> review(s)</a> | <a href="/review_write_spa.asp?id=<%=intProductID%>">Write Review </a></td>
		</tr>
	</table>
<%
	Else
%>
	<table width="100%" cellpadding="2"  cellspacing="1" bgcolor="#F4F4F4">
		<tr>
			<td align="center" bgcolor="#FFFFF9"><font color="#990000">Average user rating: </font>Not yet rated
</td>
		</tr>
		<tr>
			<td align="center" bgcolor="#FFFFF9"><a href="/review_write_spa.asp?id=<%=intProductID%>">Write Review </a></td>
		</tr>
	</table>
<%
	End IF
	Case 2 'List
	IF bolReview Then
%>
	<table width="100%" cellpadding="2"  cellspacing="1" bgcolor="#F4F4F4">
		<tr>
			<td align="left" bgcolor="#FFFFF9"><font color="#990000">Average user rating:</font> <%=function_generate_star_rate(intAvgPoint,intProductID,1)%> <i><font color="green">(<%=FormatNumber(intAvgPoint,1)%> from 5 )</font></i> <a href="/review_spa.asp?id=<%=intProductID%>">view <%=arrReview(0,0)%> review(s)</a> | <a href="/review_write_spa.asp?id=<%=intProductID%>">Write Review </a></td>
		</tr>
	</table>
<%
	Else
%>
	<table width="100%" cellpadding="2"  cellspacing="1" bgcolor="#F4F4F4">
		<tr>
			<td align="left" bgcolor="#FFFFF9"><font color="#990000">Average user rating: </font>Not yet rated | <a href="/review_write_spa.asp?id=<%=intProductID%>">Write Review </a>
</td>
		</tr>
	</table>
<%
	End IF
	Case 3
	
END SELECT
END FUNCTION
%>