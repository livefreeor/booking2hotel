<%
Function function_birthday_input(intDate,intMonth,intYear,strDate,strMonth,strYear,intType)
Dim intCountDate
Dim intCountMonth
Dim intCountYear
Dim arrMonth
Dim dateSelect
Dim monthSelect
Dim yearSelect
Dim show_date
	Select Case intType
	Case 1
	arrMonth=array("","Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec")
	%>
	<select name="<%=strDate%>">
	<%
	For intCountDate=1 to 31
		IF intCountDate=int(intDate) Then
			dateSelect="selected"
		Else
			dateSelect=""
		End IF
		show_date=right("0"&intCountDate,2)
	%>
	<option value="<%=intCountDate%>" <%=dateSelect%> ><%=show_date%></option>
	<%
	Next
	%>
	</select>
	-<select name="<%=strMonth%>">
	<%
	For intCountMonth=1 to Ubound(arrMonth)
		IF intCountMonth=int(intMonth) Then
			monthSelect="Selected"
		Else
			monthSelect=""
		End IF
	%>
	<option value="<%=intCountMonth%>" <%=monthSelect%>><%=arrMonth(intCountMonth)%></option>
	<%
	Next
	%>
	</select>
	-<select name="<%=strYear%>">
	<%
	For intCountYear=year(date) to year(date)-70 step-1
		IF intCountYear=int(intYear) Then
			YearSelect="Selected"
		Else
			YearSelect=""
		End IF
	%>
	<option value="<%=intCountYear%>" <%=YearSelect%>><%=intCountYear%></option>
	<%
	Next
	%>
	</select>
	<%
	Case 2
	
	Dim Lastday
		
		Select case int(intmonth)
				case 1 
					Lastday=31
				case 2
					if (year(now))mod 4=0 then
					Lastday=29
					else
					Lastday=28
					end if
				case 3
					Lastday=31
				case 4
					Lastday=30
				case 5 
					Lastday=31
				case 6 
					Lastday=30
				case 7 
					Lastday=31
				case 8 
					Lastday=31
				case 9 
					Lastday=30
				case 10 
					Lastday=31
				case 11 
					Lastday=30
				case 12 
					Lastday=31
			End Select
			IF int(intDate)>Lastday Then
				function_birthday_input=False
			Else
				function_birthday_input=dateserial(intYear,intMonth,intDate)
			End IF
	End Select
End Function
%>