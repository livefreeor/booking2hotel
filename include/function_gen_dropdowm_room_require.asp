<%
FUNCTION function_gen_dropdowm_room_require(intRequireID,intDefault,intType)

	Dim strSelect1
	Dim strSelect2
	Dim strSelect3

	SELECT CASE intDefault
		Case 1
			strSelect1= "selected"
		Case 2
			strSelect2= "selected"
		Case 3
			strSelect3= "selected"
	END SELECT

	SELECT CASE intType
		Case 1 'Smoke
%>
<select name="smoke_<%=intRequireID%>">
  <option value="1" <%=strSelect1%>>Non Smoking</option>
  <option value="2" <%=strSelect2%>>Smoking</option>
  <option value="3" <%=strSelect3%>>-</option>
</select>
<%
		Case 2 'Bed
%>
<select name="bed_<%=intRequireID%>">
  <option value="1" <%=strSelect1%>>King Bed</option>
  <option value="2" <%=strSelect2%>>Twin Bed</option>
  <option value="3" <%=strSelect3%>>-</option>
</select>
<%
		Case 3 'Floor
%>
<select name="floor_<%=intRequireID%>">>
  <option value="1" <%=strSelect1%>>High Floor</option>
  <option value="2" <%=strSelect2%>>Low Floor</option>
  <option value="3" <%=strSelect3%>>-</option>
</select>
<%
	END SELECT

END FUNCTION
%>