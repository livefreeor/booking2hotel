<%
FUNCTION function_gen_price_sightseeing(dateCheckIn,intOptionID,intAdult,intChild,intType)

	Dim sqlRateSight
	Dim recRateSight
	Dim arrRateSight
	Dim bolRateSight
	Dim intPriceAdult
	Dim intPriceChild
	Dim intPriceTotalSight
	
	sqlRateSight = "SELECT op.price_id,op.price,op.price_child,op.price_own,op.price_child_own,op.sup_weekend,op.sup_holiday,op.sup_long,"
	sqlRateSight = sqlRateSight & " ISNULL((SELECT TOP 1 sopq.supplement FROM tbl_option_price_quantity sopq WHERE sopq.price_id=op.price_id AND (quantity_min<"&intAdult&" AND quantity_max>="&intAdult&") ORDER BY quantity_max DESC),0) AS supplement"
	sqlRateSight = sqlRateSight & " FROM tbl_option_price op"
	sqlRateSight = sqlRateSight & " WHERE op.date_start<="&function_date_sql(Day(dateCheckIn),Month(dateCheckIn),Year(dateCheckIn),1)&" AND op.date_end>="&function_date_sql(Day(dateCheckIn),Month(dateCheckIn),Year(dateCheckIn),1)&" AND op.option_id=" & intOptionID
	Set recRateSight = Server.CreateObject ("ADODB.Recordset")
	recRateSight.Open SqlRateSight, Conn,adOpenStatic,adLockreadOnly
	IF NOT recRateSight.EOF Then
		arrRateSight = recRateSight.GetRows()
		bolRateSight = True
	Else
		bolRateSight = False
	End IF
	recRateSight.Close
	Set recRateSight = Nothing 

	IF bolRateSight Then
		SELECT CASE intType
			Case 1 '### Normal Price ###
				intPriceAdult = (arrRateSight(1,0)+arrRateSight(8,0))*Int(intAdult)
				intPriceChild = (arrRateSight(2,0))*Int(intChild)
				intPriceTotalSight = intPriceAdult + intPriceChild
			Case 2 '### Own Price ###
				intPriceAdult = (arrRateSight(3,0)+arrRateSight(8,0))*Int(intAdult)
				intPriceChild = (arrRateSight(4,0))*Int(intChild)
				intPriceTotalSight = intPriceAdult + intPriceChild
		END SELECT
	Else
		intPriceTotalSigh = 0
	End IF
	
	function_gen_price_sightseeing = intPriceTotalSight
	
END FUNCTION
%>