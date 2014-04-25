<%
Function function_check_date_outing(intType)
	Dim arrDate
	Dim dateCheck
	Dim dateCheckIn
	Dim intDate
	Dim arrDate2
	Dim bolCheck
	
	bolCheck=false
	
	'arrDate=array("2009-08-27","2009-08-28","2009-08-29")
	IF isArray(arrDate) Then
		dateCheckIn=dateserial(Request("ch_in_year"),Request("ch_in_month"),Request("ch_in_date"))
		For intDate=0 to Ubound(arrDate)
			arrDate2=split(arrDate(intDate),"-")
			dateCheck=dateserial(int(arrDate2(0)),int(arrDate2(1)),int(arrDate2(2)))
			IF (datediff("d",dateCheck,dateCheckIn)=0) Then
				bolCheck=true
				exit for
			End IF
		Next
	End IF
	function_check_date_outing=bolCheck
End Function

%>