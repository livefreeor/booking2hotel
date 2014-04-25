<%
FUNCTION function_display_error_golf(strError)
	
	SELECT CASE strError
		Case "error01"
			function_display_error_golf = "Error!, Please Select Your Destination"
		Case "error02"
			function_display_error_golf = "Error!, Please Confirm Your Tee-Off Date"
		Case "error03"
			function_display_error_golf = "Error!, Please Confirm Your Tee-Off Date"
		Case "error04"
			function_display_error_golf = "Error!, Please Confirm Your Check Out Date"
		Case "error05"
			function_display_error_golf = "Error!, Please Confirm Your Check Out Date"
		Case "error06"
			function_display_error_golf = "Error!, Please Confirm Your Tee-Off Date"
		Case "error07"
			function_display_error_golf = "Error!, Please Confirm Your Check Out Date"
		Case "error08"
			function_display_error_golf = "Error!, Check out date must later than Tee-Off date"
		Case "error09"
			function_display_error_golf = "Error!, Tee-Off date must later than current date"
		Case "error10"
			function_display_error_golf = "Error!, Please select your course type and quantity"
		Case "error11"
			function_display_error_golf = "Exceed golfers, please select more golf course quantity."
		Case "error12"
			function_display_error_golf = "Exceed extrabed policy, please select more room"
		Case "error13"
			function_display_error_golf = "Email Error!, please recheck your email"
		Case "error14"
			function_display_error_golf = "Error!, we don't have rate for your period,<br>Please contactus at <a href=""mailto:support@hotels2thailand.com"">support@hotels2thailand.com</a>"
		Case "error15"
			function_display_error_golf = "Error! Please fill in all required fields"
		Case "error16"
			function_display_error_golf = "Error! Your email and repeat email does not math. Please recheck again."
		Case "error17"
			function_display_error_golf = "Exceed airport transfer policy, Please seelct more car."
		
		Case "error18"
			function_display_error_golf = "Compulsory of X'mas Eve Gala Dinner during your stay has not yet ordered, please kindly order the Gala Dinner in order to complete the reservation."
		Case "error19"
			function_display_error_golf = "Quantity of Compulsory X'mas Eve Gala Dinner during your stay was not ordered according to correct number of people, please kindly order the Gala Dinner equivalent to number of people in order to complete the reservation."
		Case "error20"
			function_display_error_golf =  "Compulsory of X'mas Eve Gala Dinner  for children during your stay has not yet ordered, please kindly order the Gala Dinner for children in order to complete the reservation."
		Case "error21"
			function_display_error_golf ="Quantity of Compulsory X'mas Eve Gala Dinner during your stay was not ordered according to correct number of children, please kindly order the Gala Dinner equivalent to number of children in order to complete the reservation."
			
			
		Case "error22"
			function_display_error_golf = "Compulsory of New Year Gala Dinner during your stay has not yet ordered, please kindly order the Gala Dinner in order to complete the reservation."
		Case "error23"
			function_display_error_golf = "Quantity of Compulsory New Year Gala Dinner during your stay was not ordered according to correct number of people, please kindly order the Gala Dinner equivalent to number of people in order to complete the reservation."
		Case "error24"
			function_display_error_golf =  "Compulsory of New Year Gala Dinner  for children during your stay has not yet ordered, please kindly order the Gala Dinner for children in order to complete the reservation."
		Case "error25"
			function_display_error_golf ="Quantity of CompulsoryNew Year Gala Dinner during your stay was not ordered according to correct number of children, please kindly order the Gala Dinner equivalent to number of children in order to complete the reservation."
		Case "error26"
			function_display_error_golf ="Your order ID is not matched to your email. Please recheck."	
	
	END SELECT

END FUNCTION

FUNCTION function_display_error_golf_box (strError,intType)

	Dim strAnchor
	Dim strAnchorLink

	SELECT CASE strError
		Case "error01"
			strAnchor = ""
			strAnchorLink = ""
		Case "error02","error03","error04","error05","error06","error07","error08","error09","error14"
			strAnchor = "#date"
			strAnchorLink = "<a name=""date""></a>"
		Case "error10"
			strAnchor = "#room"
			strAnchorLink = "<a name=""room""></a>"
		Case "error11"
			strAnchor = "#people"
			strAnchorLink = "<a name=""people""></a>"
		Case "error12","error17","error18","error19","error20","error21","error22","error23","error24","error25"
			strAnchor = "#option"
			strAnchorLink = "<a name=""option""></a>"
		Case "error15","error16"
			strAnchor = "#detail"
			strAnchorLink = "<a name=""detail""></a>"
	END SELECT
			
	SELECT CASE intType
		Case 1 'Main Error
			function_display_error_golf_box= "<table width=""65%"" border=""0"" cellspacing=""1"" cellpadding=""0"" bgcolor=""#CC0000"">" & VbCrlf
			function_display_error_golf_box = function_display_error_golf_box & "<tr><td bgcolor=""#FFFFF4"" class=""m"" align=""center"" valign=""middle"" height=""30""><font color=""#CC0000""><img src=""/images/ico_warning.gif"" width=""16"" height=""14"" align=""absmiddle""> &nbsp;There is a slight problem with your submission.(<a href="""&strAnchor&""">See below</a>)</font></td>" & VbCrlf
			function_display_error_golf_box = function_display_error_golf_box & "</table>" & VbCrlf
			
		Case 2 'Error Detail
			function_display_error_golf_box= strAnchorLink  & "<table width=""65%"" border=""0"" cellspacing=""1"" cellpadding=""0"" bgcolor=""#CC0000"">" & VbCrlf
			function_display_error_golf_box = function_display_error_golf_box & "<tr><td bgcolor=""#FFFFF4"" class=""m"" align=""center"" valign=""middle"" height=""30""><font color=""#CC0000""><img src=""/images/ico_warning.gif"" width=""16"" height=""14"" align=""absmiddle""> &nbsp;"&function_display_error_golf(strError)&"</font></td>" & VbCrlf
			function_display_error_golf_box = function_display_error_golf_box & "</table>" & VbCrlf
		
		
	END SELECT
	
END FUNCTION

%>