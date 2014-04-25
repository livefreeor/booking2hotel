<%
Function Lastday(intmonth)
	Select case intmonth
		case 1 
			Lastday=31
		case 2
			if (year(now)-543)mod 4=0 then
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
End Function
%>