<%
FUNCTION function_display_error_cart(strError)
	
	SELECT CASE strError
		Case "error01"
			function_display_error_cart = "No.of adult is exceed no.of room. We have updated no.of adult from "&Request.QueryString("adult_old")&" to "&Request.QueryString("adult_current")&". <br />If there are "&Request.QueryString("adult_old")&" adults, please select more item quantity."
		Case "error02" '"Your check in date must more than today."
			function_display_error_cart = "Error!, Please Confirm Your Check In Date"
		Case "error03" '"Your check in date must more than check out."
			function_display_error_cart = "Error!, Please Confirm Your Check In Date"
		Case "error04" '"Check in error on Feb"
			function_display_error_cart = "Error!, Please Confirm Your Check In Date"
		Case "error05" '"Check out error on Feb"
			function_display_error_cart = "Error!, Please Confirm Your Check Out Date"
		Case "error06" '"Check in error on 30 month"
			function_display_error_cart = "Error!, Please Confirm Your Check In Date"
		Case "error07" '"Check out error on 30 month"
			function_display_error_cart = "Error!, Please Confirm Your Check Out Date"
		Case "error08"
			function_display_error_cart = "No.of adult is exceed the limit, please select more item quantity"
		Case "error09"
			function_display_error_cart = "Exceed extrabed policy, please select more room"
		Case "error10"
			function_display_error_cart = "Quantity of Compulsory X'mas Eve Gala Dinner during your stay was not ordered according to correct number of people, please kindly order the Gala Dinner equivalent to number of people in order to complete the reservation."
		Case "error11"
			function_display_error_cart = "Compulsory of X'mas Eve Gala Dinner  for children during your stay has not yet ordered, please kindly order the Gala Dinner for children in order to complete the reservation."
		Case "error12"
			function_display_error_cart =  "Quantity of Compulsory New Year Gala Dinner during your stay was not ordered according to correct number of people, please kindly order the Gala Dinner equivalent to number of people in order to complete the reservation."
		Case "error13"
			function_display_error_cart =  "Compulsory of New Year Gala Dinner  for children during your stay has not yet ordered, please kindly order the Gala Dinner for children in order to complete the reservation."
		Case "error14"
			function_display_error_cart = "Error!, we don't have rate for your period,<br>Please contactus at <a href=""mailto:support@hotels2thailand.com"">support@hotels2thailand.com</a>"
		Case "error15"
			function_display_error_cart = "Error!, we don't have trip for your period."
	END SELECT

END FUNCTION

FUNCTION function_display_error_box (strError,intType)

	Dim strAnchor
	Dim strAnchorLink

	SELECT CASE strError
		Case "error01","error02","error03","error04","error05","error06","error07","error08","error09","error10","error11","error12","error13","error14","error15"
			strAnchor = "#error_cart_product_" & Request.QueryString("error_cart_product_id")
			strAnchorLink = "<a name=""error_cart_product_"& Request.QueryString("error_cart_product_id")&"""></a>"
	END SELECT
			
	SELECT CASE intType
		Case 1 'Main Error
			function_display_error_box= "<table width=""65%"" border=""0"" cellspacing=""1"" cellpadding=""0"" bgcolor=""#CC0000"">" & VbCrlf
			function_display_error_box = function_display_error_box & "<tr><td bgcolor=""#FFFFF4"" class=""m"" align=""center"" valign=""middle"" height=""30""><font color=""#CC0000""><img src=""/images/ico_warning.gif"" width=""16"" height=""14"" align=""absmiddle""> &nbsp;There is a slight problem with your update.(<a href="""&strAnchor&""">See below</a>)</font></td>" & VbCrlf
			function_display_error_box = function_display_error_box & "</table>" & VbCrlf
			
		Case 2 'Error Detail
			function_display_error_box= strAnchorLink  & "<table width=""80%"" border=""0"" cellspacing=""1"" cellpadding=""0"" bgcolor=""#CC0000"">" & VbCrlf
			function_display_error_box = function_display_error_box & "<tr><td bgcolor=""#FFFFF4"" class=""m"" align=""center"" valign=""middle"" height=""30""><font color=""#CC0000""><img src=""/images/ico_warning.gif"" width=""16"" height=""14"" align=""absmiddle""> &nbsp;"&function_display_error_cart(strError)&"</font></td>" & VbCrlf
			function_display_error_box = function_display_error_box & "</table>" & VbCrlf
		
		
	END SELECT
	
END FUNCTION
%>