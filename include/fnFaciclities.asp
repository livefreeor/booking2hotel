<%
Function fnFaciclities(intProductID, intType)

	Dim sqlGen
	Dim recGen
	Dim strTitle
	Dim intRecord
	Dim k, l
	
	SELECT CASE intType
		Case 1 'Option Feature
		
			sqlGen = "SELECT DISTINCT fp.feature_id, po.title_en"
			sqlGen = sqlGen & " FROM tbl_feature_product_option fp, tbl_product_option_feature po"
			sqlGen = sqlGen & " WHERE fp.feature_id=po.feature_id AND fp.option_id in (SELECT option_id FROM tbl_product_option WHERE product_id="& intProductID &")"
			sqlGen = sqlGen & " ORDER BY po.title_en ASC"
			
			strTitle = "Accommodation Facilities :"

		Case 2 'Product Feature
		
			sqlGen = "SELECT DISTINCT pf.feature_id, pf.title_en"
			sqlGen = sqlGen & " FROM tbl_product_feature pf, tbl_feature_product fp"
			sqlGen = sqlGen & " WHERE pf.feature_id=fp.feature_id AND fp.product_id=" & intProductID
			sqlGen = sqlGen & " ORDER BY pf.title_en ASC"
			
			strTitle = "Amenities and Services:"

		Case 3 'Nearby Option
		
			sqlGen = "SELECT DISTINCT n.nearby_id AS feature_id,n.title_en"
			sqlGen = sqlGen & " FROM tbl_nearby n, tbl_product_nearby pn"
			sqlGen = sqlGen & " WHERE pn.nearby_id=n.nearby_id AND pn.product_id=" & intProductID
			sqlGen = sqlGen & " ORDER BY n.title_en ASC"
		
			strTitle = "Nearby Facitlities:"
			
	END SELECT
	
	Set recGen = Server.CreateObject ("ADODB.Recordset")
	recGen.Open sqlGen, Conn,adOpenStatic, adLockReadOnly
	
		IF NOT recGen.EOF Then
		
			intRecord = recGen.RecordCount
			
			fnFaciclities = "<table width=""200"">"
			fnFaciclities = fnFaciclities & "<tr>"
			fnFaciclities = fnFaciclities & "<td bgcolor=""#CCE0FF"" class=""vs""><font color='#0066FF'><b>"& strTitle &"</b></font></td>"
			fnFaciclities = fnFaciclities & "</tr>"
			fnFaciclities = fnFaciclities & "</table>"

			fnFaciclities = fnFaciclities &"<table width=""550""  cellspacing=""2"" cellpadding=""1"">"
			
			For k=1 To (intRecord-(intRecord mod 3))/3 + (intRecord mod 3)
					fnFaciclities = fnFaciclities & "<tr> "
					For l=1 To 3
						IF NOT recGen.EOF Then
							fnFaciclities = fnFaciclities & "<td width=""150"" class=""vs1"">&nbsp;"
							fnFaciclities = fnFaciclities &"<b>"& fnFacilitiesIcon (recGen.Fields("feature_id"), intType)&"  "& recGen.Fields("title_en")&"</b>"
							fnFaciclities = fnFaciclities & "</td>"
							recGen.MoveNext
						Else
							fnFaciclities = fnFaciclities & "<td width=""150"" class=""vs1"">&nbsp;</td>"
						End IF
					Next
					fnFaciclities = fnFaciclities & "</tr> "
			Next
			
			fnFaciclities = fnFaciclities & "</table><br>"
		End IF
	
	recGen.Close
	Set recGen = Nothing
	
End Function
%>