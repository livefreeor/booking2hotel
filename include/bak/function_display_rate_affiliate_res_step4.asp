<%
FUNCTION function_display_rate_affiliate_res_step4()

	Dim strStep4
	
	strStep4 = "<table width=""100%"" cellpadding=""2""  cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
	strStep4 = strStep4 & "<tr>" & VbCrlf
	strStep4 = strStep4 & "<td class=""step_bg_color""><span class=""step_num"">Step 4 :</span> <span class=""step_text"">Click &quot;Make Payment&quot; to finish your booking process.</span></td>" & VbCrlf
	strStep4 = strStep4 & "</tr>" & VbCrlf
	strStep4 = strStep4 & "<tr>" & VbCrlf
	strStep4 = strStep4 & "<td align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
	strStep4 = strStep4 & "<table width=""100%"" cellspacing=""1"" cellpadding=""6"">" & VbCrlf
	strStep4 = strStep4 & "<tr>" & VbCrlf
	strStep4 = strStep4 & "<td align=""left"" class=""m""><b><span class=""room_text"">Total Amount: </span></b><span class=""price_text"">"&function_display_price(Session("intPriceTotal"),3)&"&nbsp;Baht</span>" & VbCrlf
	strStep4 = strStep4 & "</td>" & VbCrlf
	strStep4 = strStep4 & "</tr>" & VbCrlf
	strStep4 = strStep4 & "<tr>" & VbCrlf
	strStep4 = strStep4 & "<td align=""left""><input type=""image"" src=""http://www.booking2hotels.com/images/b_make_payment.gif"" width=""112"" height=""26"" name=""cmdkbank"" value=""1"">" & VbCrlf
	strStep4 = strStep4 & "<img src=""http://www.booking2hotels.com/images/img_creditcard_visa.gif"" alt=""Visa Card""> <img src=""http://www.booking2hotels.com/images/img_creditcard_master.gif"" alt=""Master Card"">" & VbCrlf
	strStep4 = strStep4 & "</td>" & VbCrlf
	strStep4 = strStep4 & "</tr>" & VbCrlf
	strStep4 = strStep4 & "<tr>" & VbCrlf
	strStep4 = strStep4 & "<td>" & VbCrlf
	strStep4 = strStep4 & "<strong><span class=""valid_text"">Your fund will be held with us until your reservation is confirmed. In case your reservation is not available, we will release all funds to your credit card account immediately. </span> </strong> <br /> <br />" & VbCrlf
	strStep4 = strStep4 & "<strong> <font color=""#FF0000"">*</font> </strong> Your payment will be secured protection by a validated <font color=""#003366""><strong>Secure Sockets Layer (SSL)</strong></font>,   certificates capable of 128-bit encryption where you can safely enter your credit card details in an <font color=""#003366""><strong>encrypted</strong></font> environment. We value your privacy and will not share your personal information. <br /> " & VbCrlf
	strStep4 = strStep4 & "<br />" & VbCrlf
	strStep4 = strStep4 & "<strong> <font color=""#FF0000"">*</font></strong>  Hotels2Thailand.com is a travel agent registered under name of <font color=""#003366""><strong>Blue House Travel Co.,Ltd</strong></font> by Tourism Authority of Thailand.<font color=""#003366""><strong>TAT License No. 11/3240</strong> </font><br /><br />" & VbCrlf
	strStep4 = strStep4 & "<strong> <font color=""#FF0000"">*</font></strong>  Please review our <a href=""javascript:popup('/thailand-hotels-cancel-2.asp',700,750)""><u>Cancellation Policy</u></a>	" & VbCrlf
	strStep4 = strStep4 & "</td>" & VbCrlf
	strStep4 = strStep4 & "</tr>" & VbCrlf
	strStep4 = strStep4 & "</table></td>" & VbCrlf
	strStep4 = strStep4 & "</tr>" & VbCrlf
	strStep4 = strStep4 & "</table>" & VbCrlf

	function_display_rate_affiliate_res_step4 = strStep4
	
END FUNCTION
%>