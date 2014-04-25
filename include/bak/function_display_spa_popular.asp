<%
FUNCTION function_display_spa_popular(intDestinationID,intType)

	Dim sqlPop
	Dim recPop
	Dim arrPop
	Dim intWidth
	Dim bolPop
	Dim strReturn
	Dim intCountPop
	
	SELECT CASE intType
		Case 1 'Thailand day Trips Page
			sqlPop = "SELECT TOP 4 product_id,destination_id,files_name,title_en,product_code"
			sqlPop = sqlPop & " FROM tbl_product "
			sqlPop = sqlPop & " WHERE destination_id="&intDestinationID&" AND product_cat_id=40 AND status=1"
			sqlPop = sqlPop & " ORDER BY point DESC"
			Set recPop = Server.CreateObject ("ADODB.Recordset")
			recPop.Open SqlPop, Conn,adOpenStatic,adLockreadOnly
				IF NOT recPop.EOF Then
					bolPop = True
					arrPop = recPop.GetRows()
				Else
					bolPop = False
				End IF
			recPop.Close
			Set recPop = Nothing
			
			IF bolPop Then
				intWidth = 100/(Ubound(arrPop,2)+1)
				strReturn = "<table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#F3F3F3"">" & VbCrlf
				strReturn = strReturn & "<tr>" & VbCrlf
				
				For intCountPop=0 To Ubound(arrPop,2)
					strReturn = strReturn & "<td valign=""top"" width="""&intWidth&"%"" align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
					strReturn = strReturn & "<a href=""/"&function_generate_spa_link(intDestinationID,"",1)&"/"&arrPop(2,intCountPop)&"""> <img src=""thailand-spa-pic/"&arrPop(4,intCountPop)&"_b_1.jpg"" width=""50"" height=""55"" border=""0""><br />" & VbCrlf
					strReturn = strReturn & arrPop(3,intCountPop) & "</a>" & VbCrlf
					strReturn = strReturn & "</td>" & VbCrlf							  
				Next
				
				strReturn = strReturn & "</tr>" & VbCrlf
				strReturn = strReturn & "</table>	" & VbCrlf	
			End IF

		Case 2
		Case 999 'Temp
	END SELECT

	function_display_spa_popular = strReturn

END FUNCTION
%>