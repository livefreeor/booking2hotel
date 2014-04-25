<%
FUNCTION function_set_session_date(intDayCheckin,intMonthCheckin,intYearCheckin,intDayCheckout,intMonthCheckout,intYearCheckout)

	Session("check_in_day") = intDayCheckin
	Session("check_in_month") = intMonthCheckin
	Session("check_in_year") = intYearCheckin
	Session("check_out_day") = intDayCheckout
	Session("check_out_month") = intMonthCheckout
	Session("check_out_year") = intYearCheckout

END FUNCTION
%>