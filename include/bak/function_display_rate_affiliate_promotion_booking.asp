<%
FUNCTION function_display_rate_affiliate_promotion_booking(intProductID,intPsID,dateCheckIn,dateCheckOut,intType)

	Dim sqlPromo
	Dim recPromo
	Dim arrPromo
	Dim strPromo
	Dim bolPromo

	SELECT CASE intType
		Case 1 'No date Input
				sqlPromo = "SELECT TOP 1 pr.title,pr.detail,pr.date_start,pr.date_end"
				sqlPromo = sqlPromo & " FROM tbl_promotion pr, tbl_product_option o"
				sqlPromo = sqlPromo & " WHERE o.option_id=pr.option_id AND pr.status=1 AND date_end>="& function_date_sql(Day(Date),Month(Date),Year(Date),1) &" AND o.product_id=" & intProductID
				sqlPromo = sqlPromo & " ORDER BY day_min ASC ,pr.date_start ASC"
		Case 2 'With date Input
				'sqlPromo = "SELECT TOP 1 pr.title,pr.detail,pr.date_start,pr.date_end"
				'sqlPromo = sqlPromo & " FROM tbl_promotion pr, tbl_product_option o"
				'sqlPromo = sqlPromo & " WHERE o.option_id=pr.option_id AND pr.status=1 AND date_start<="& function_date_sql(intDayCheckIn,intMonthCheckIn,intYearCheckIn,1) &" AND date_end>="& function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1) &" AND day_min<="& intNight &" AND qty_min>="& intQty &" AND day_advance_num<="& intDayAdvance &" AND o.product_id=" & intProduct
				'sqlPromo = sqlPromo & " ORDER BY day_min DESC"
	END SELECT
	
	Set recPromo  = Server.CreateObject ("ADODB.Recordset")
	recPromo.Open SqlPromo , Conn,adOpenStatic,adLockreadOnly
		IF NOT recPromo.EOF Then
			arrPromo = recPromo.GetRows()
			bolPromo = True
		End IF
	recPromo.Close
	Set recPromo  = Nothing 

	IF bolPromo Then
		strPromo = strPromo & "<br />" & VbCrlf
		strPromo = strPromo & "<div align=""center"">" & VbCrlf
		strPromo = strPromo & "<div class=""promotion_border_color"" align=""left"">" & VbCrlf
		strPromo = strPromo & "<img src=""http://www.booking2hotels.com/images/ico_promotion.gif"" /> <span class=""promotion_text"">"&arrPromo(0,0)&"</span><br /><br />" & VbCrlf
		strPromo = strPromo & "&ensp; <span class=""valid_text""><strong>Valid from</strong> "&function_date(arrPromo(2,0),3)&" - "&function_date(arrPromo(3,0),3)&"</span>" & VbCrlf
		strPromo = strPromo & "<br /><div align = ""right""><a href=""javascript:popup('http://www.hotels2thailand.com/affiliate_include/hotelPromotion.asp?hotel_id="&intProductID&"&psid="&intPsID&"',600,700)"">See more promotion &gt;&gt;</a></div>" & VbCrlf
		strPromo = strPromo & "</div>" & VbCrlf
		strPromo = strPromo & "</div>" & VbCrlf
		strPromo = strPromo & "<br /><br />" & VbCrlf
	End IF

	function_display_rate_affiliate_promotion_booking = strPromo

END FUNCTION
%>