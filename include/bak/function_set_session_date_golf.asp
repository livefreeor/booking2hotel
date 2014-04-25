<%
FUNCTION function_set_session_date_golf(intDayCheckin,intMonthCheckin,intYearCheckin)

	Session("check_in_day_golf") = intDayCheckin
	Session("check_in_month_golf") = intMonthCheckin
	Session("check_in_year_golf") = intYearCheckin

END FUNCTION
%>