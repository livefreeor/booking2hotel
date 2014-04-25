<%
FUNCTION function_display_promotion_title(intProduct,intQty,intDayCheckIn,intMonthCheckIn,intYearCheckIn,intDayCheckOut,intMonthCheckOut,intYearCheckOut,intType,intDisplayType)

	Dim sqlPromo
	Dim recPromo
	Dim strPromo
	Dim bolPromo
	Dim intNight
	Dim dateCheckIn
	Dim dateCheckOut
	Dim intDayAdvance
	Dim arrPromotion
	
	IF intDayCheckIn <> "" Then
		dateCheckIn = DateSerial(intYearCheckIn,intMonthCheckIn,intDayCheckIn)
		dateCheckOut = DateSerial(intYearCheckOut,intMonthCheckOut,intDayCheckOut)
		
		intNight = DateDiff("d",dateCheckIn,dateCheckOut)
		intDayAdvance = DateDiff("d",Date,dateCheckIn)
	End IF
	
	SELECT CASE intType
		Case 1 'No date Input
				sqlPromo = "SELECT TOP 1 pr.title,pr.detail,pr.date_start,pr.date_end,pr.promotion_id "
				sqlPromo = sqlPromo & " FROM tbl_promotion pr, tbl_product_option o"
				sqlPromo = sqlPromo & " WHERE o.option_id=pr.option_id AND pr.status=1 AND date_end>="& function_date_sql(Day(Date),Month(Date),Year(Date),1) &" AND o.product_id=" & intProduct
				sqlPromo = sqlPromo & " ORDER BY day_min ASC ,pr.date_start ASC"
				
		Case 2 'With date Input
				sqlPromo = "SELECT TOP 1 pr.title,pr.detail,pr.date_start,pr.date_end,pr.promotion_id "
				sqlPromo = sqlPromo & " FROM tbl_promotion pr, tbl_product_option o"
				sqlPromo = sqlPromo & " WHERE o.option_id=pr.option_id AND pr.status=1 AND date_start<="& function_date_sql(intDayCheckIn,intMonthCheckIn,intYearCheckIn,1) &" AND date_end>="& function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1) &" AND day_min<="& intNight &" AND qty_min>="& intQty &" AND day_advance_num<="& intDayAdvance &" AND o.product_id=" & intProduct
				sqlPromo = sqlPromo & " ORDER BY day_min DESC"

	END SELECT
	
	Set recPromo  = Server.CreateObject ("ADODB.Recordset")
	recPromo.Open SqlPromo , Conn,adOpenStatic,adLockreadOnly
		IF NOT recPromo.EOF Then
			arrPromotion = recPromo.GetRows()
			'strPromo  = recPromo.GetString
			bolPromo = True
		End IF
	recPromo.Close
	Set recPromo  = Nothing 
				
	IF bolPromo Then
		SELECT CASE intDisplayType
			Case 1 ' Hotel Detail
				function_display_promotion_title = "<table width=""450""  border=""0"" align=""center"" cellpadding=""4"" cellspacing=""1"" bgcolor=""#FFCC99"">" & VbCrlf
				function_display_promotion_title = function_display_promotion_title & "<tr><td align=""center"" bgcolor=""#FFFFF9"" class=""m"">" & VbCrlf
				function_display_promotion_title = function_display_promotion_title & "<table width=""100%"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				function_display_promotion_title = function_display_promotion_title & "<tr>" & VbCrlf
				function_display_promotion_title = function_display_promotion_title & "<td align=""center""><h2><em><font color=""#FF0000"">Special Promotion !</font></em></h2></td>" & VbCrlf
				function_display_promotion_title = function_display_promotion_title & "</tr>" & VbCrlf
				function_display_promotion_title = function_display_promotion_title & "<tr>" & VbCrlf
				function_display_promotion_title = function_display_promotion_title & "<td align=""left"">" & VbCrlf
                function_display_promotion_title = function_display_promotion_title & "<h3><font color=""#FF7900""><img src=""../images/ico_promotion.gif"" /> <b>"&arrPromotion(0,0)&"</b></font></h3>" & VbCrlf	
                function_display_promotion_title = function_display_promotion_title & "<font color=""#B30000"">&ensp; "&arrPromotion(1,0)&"</font> <br /><br />" & VbCrlf
				
                'function_display_promotion_title = function_display_promotion_title & "&ensp; <font color=""#00B300""><strong>Valid from</strong> "&function_date(arrPromotion(2,0),3)&" - "&function_date(arrPromotion(3,0),3)&"	</font></td>" & VbCrlf
                function_display_promotion_title = function_display_promotion_title & "&ensp; <font color=""#00B300""><strong>Valid from</strong> "&function_date(arrPromotion(2,0),3)&" - "&function_date(arrPromotion(3,0),3)&"</font><br>" & VbCrlf
                function_display_promotion_title = function_display_promotion_title & "&ensp; <a href=""javascript:popup('/room_type_promotion.asp?hotel_id="&intProduct&"&pro_id="&arrPromotion(4,0)&"',600,700)""><img src=""/images/valid_type4.jpg"" border=""0""></a></td>" & VbCrlf

				function_display_promotion_title = function_display_promotion_title & "</table>" & VbCrlf
				function_display_promotion_title = function_display_promotion_title & "<br /><div align = ""right""><a href=""javascript:popup('http://www.hotels2thailand.com/hotel_promotion.asp?hotel_id="& intProduct &"',600,700)"">See more promotion &gt;&gt;</a></div>" & VbCrlf
				function_display_promotion_title = function_display_promotion_title & "<br />" & VbCrlf
				function_display_promotion_title = function_display_promotion_title & "You can get this promotion only at <font color=""#00B300"">www.hotels2thailand.com</font></td>" & VbCrlf
				function_display_promotion_title = function_display_promotion_title & "</tr></table>" & VbCrlf
		
			Case 2 'Hotel List
				function_display_promotion_title = "<img src=""/images/ico_promotion.gif""> <font color=""#008000""><b>"& arrPromotion(0,0) &"</b></font><br><a href=""javascript:popup('/room_type_promotion_list.asp?hotel_id="&intProduct&"&pro_id="&arrPromotion(4,0)&"',600,700)""><font color=""red""><img src=""/images/ico_ok.gif"" border=""0""> Check Valid Type >> </font></a><br><div align = ""right""><a href=""javascript:popup('http://www.hotels2thailand.com/hotel_promotion.asp?hotel_id="& intProduct &"',600,700)"">See more promotion</a></div>"
		END SELECT
	End IF

END FUNCTION
%> 
