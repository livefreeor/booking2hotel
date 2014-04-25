<%
FUNCTION function_display_fac_ico(intProductID,strProductTitle,intType)

	Dim sqlFac
	Dim recFac
	Dim arrFac
	Dim bolFac
	Dim sqlRe
	Dim recRe
	Dim arrRe
	Dim bolRe
	Dim intCount
	
	CONST constReFitness = 15
	CONST constRePoolOutDoor = 28
	CONST constRePool = 1
	CONST constReSpa = 4
	CONST constFacInternet = 19
	CONST constFacRestaurant = 4
	
	sqlFac = "SELECT f.facility_id,f.title_en"
	sqlFac = sqlFac & " FROM tbl_facility f, tbl_product_facility fp"
	sqlFac = sqlFac & " WHERE f.facility_id=fp.facility_id AND f.facility_id IN (19,4) AND fp.product_id=" & intProductID
	
'	response.Write(sqlFac)
'	response.End()
	
	Set recFac = Server.CreateObject ("ADODB.Recordset")
	recFac.Open SqlFac, Conn,adOpenStatic,adLockreadOnly
		IF NOT recFac.EOF Then
			arrFac = recFac.GetRows()
			bolFac = True
		Else
			bolFac = False
		End IF
	recFac.Close
	Set recFac = Nothing 
	
	sqlRe = "SELECT r.recreation_id,r.title_en"
	sqlRe = sqlRe & " FROM tbl_recreation r, tbl_product_recreation rp"
	sqlRe = sqlRe & " WHERE r.recreation_id=rp.recreation_id AND r.recreation_id IN (1,4,15,28) AND rp.product_id=" & intProductID
	
	Set recRe = Server.CreateObject ("ADODB.Recordset")
	recRe.Open SqlRe, Conn,adOpenStatic,adLockreadOnly
		IF NOT recRe.EOF Then
			arrRe = recRe.GetRows()
			bolRe = True
		Else
			bolRe = False
		End IF
	recRe.Close
	Set recRe = Nothing 
	
	IF bolFac Then
		For intCount=0 To Ubound(arrFac,2)
			SELECT CASE Cint(arrFac(0,intCount))
				Case constFacInternet
					function_display_fac_ico = function_display_fac_ico & "<img src=""/images/i_internet.gif"" alt="""& strProductTitle &" High Speed Internet"">"
				Case constFacRestaurant
					function_display_fac_ico = function_display_fac_ico & "<img src=""/images/i_service.gif"" alt="""& strProductTitle &" Restaurant"">"
			END SELECT
		Next
	End IF
	
	IF bolRe Then
		For intCount=0 To Ubound(arrRe,2)
			SELECT CASE Cint(arrRe(0,intCount))
				Case constReFitness
					function_display_fac_ico = function_display_fac_ico & "<img src=""/images/i_room.gif"" alt="""& strProductTitle &" Fitness"">"
				Case constRePoolOutDoor, constRePool
					function_display_fac_ico = function_display_fac_ico & "<img src=""/images/i_pool.gif"" alt="""& strProductTitle &" "&arrRe(1,intCount)&""">"
				Case constReSpa
					function_display_fac_ico = function_display_fac_ico & "<img src=""/images/i_spa.gif"" alt="""& strProductTitle &" Spa Service"">"
			END SELECT
		Next
	End IF
	
END FUNCTION
%>