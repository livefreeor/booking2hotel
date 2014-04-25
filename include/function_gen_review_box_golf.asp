<%
FUNCTION function_gen_review_box_golf(intProductID,strProductCode,intType)
	
	Dim sqlReview
	Dim recReview
	Dim arrReview
	Dim bolReview
	Dim intAvgPoint
	
	sqlReview = "SELECT COUNT(golf_comment_id) AS total_review,SUM(fairway+greens+caddies+clubhouse+food+value_of_money) AS total_point"
	sqlReview = sqlReview & " FROM tbl_golf_comment gm"
	sqlReview = sqlReview & " WHERE gm.status=1 AND gm.golf_course_id=(SELECT TOP 1 spg.golf_course_id FROM tbl_product_group spg WHERE spg.product_code='"&strProductCode&"')"

	Set recReview = Server.CreateObject ("ADODB.Recordset")
	recReview.Open SqlReview, ConnGolf,adOpenStatic,adLockreadOnly
		arrReview = recReview.GetRows()
	recReview.Close
	Set recReview = Nothing 
	
	IF arrReview(0,0)=0 Then
		bolReview = False
	Else
		bolReview = True
		intAvgPoint = arrReview(1,0)/(arrReview(0,0)*6)
	End IF

SELECT CASE intType
	Case 1 'Golf Detail
	IF bolReview Then
%>
	<table width="100%" cellpadding="2"  cellspacing="1" bgcolor="#F4F4F4">
		<tr>
			<td align="center" bgcolor="#FFFFF9"><font color="#990000">Average user rating:</font> <%=function_generate_star_rate(intAvgPoint,intProductID,1)%> <i>(<%=FormatNumber(intAvgPoint,1)%> from 5 )</i></td>
		</tr>
		<tr>
			<td align="center" bgcolor="#FFFFF9"><a href="/review_golf.asp?id=<%=intProductID%>">view <%=arrReview(0,0)%> review(s)</a> | <a href="/review_write_golf.asp?id=<%=intProductID%>">Write Review </a></td>
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
			<td align="center" bgcolor="#FFFFF9"><a href="/review_write_golf.asp?id=<%=intProductID%>">Write Review </a></td>
		</tr>
	</table>
<%
	End IF
	Case 2 'Golf List
	IF bolReview Then
%>
	<table width="100%" cellpadding="2"  cellspacing="1" bgcolor="#F4F4F4">
		<tr>
			<td align="left" bgcolor="#FFFFF9"><font color="#990000">Average user rating:</font> <%=function_generate_star_rate(intAvgPoint,intProductID,1)%> <i><font color="green">(<%=FormatNumber(intAvgPoint,1)%> from 5 )</font></i> <a href="/review_golf.asp?id=<%=intProductID%>">view <%=arrReview(0,0)%> review(s)</a> | <a href="/review_write_golf.asp?id=<%=intProductID%>">Write Review </a></td>
		</tr>
	</table>
<%
	Else
%>
	<table width="100%" cellpadding="2"  cellspacing="1" bgcolor="#F4F4F4">
		<tr>
			<td align="left" bgcolor="#FFFFF9"><font color="#990000">Average user rating: </font>Not yet rated | <a href="/review_write_golf.asp?id=<%=intProductID%>">Write Review </a>
</td>
		</tr>
	</table>
<%
	End IF
	Case 3
	
END SELECT
END FUNCTION
%>