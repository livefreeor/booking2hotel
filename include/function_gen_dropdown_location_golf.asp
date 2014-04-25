<%
FUNCTION function_gen_dropdown_location_golf(intDestination,intLocation,strName,strJava,intType)

	Dim strBangkokSelect
	Dim strPhuketSelect
	Dim strChiangMaiSelect
	Dim strPattayaSelect
	Dim strKohSamuiSelect
	Dim strKrabiSelect
	Dim strKohChangSelect
	Dim strChiangRaiSelect
	Dim strMaeHongSonSelect
	Dim strHuaHinSelect
	Dim strChaAmSelect
	Dim strPhangNgaSelect
	Dim strKohSametSelect
	Dim strRayongSelect
	Dim strKanchanaburiSelect
	Dim strAyutthayaSelect
	Dim strKhaoYaiSelect
	Dim strNakornnayokSelect
	Dim strNakornpathomSelect
	Dim strRatchaburiSelect
	Dim strUthaiThaniSelect
	Dim strSamutprakarnSelect
	Dim strSongkhlaSelect
	Dim strPathumThaniSelect

	Dim strSelect1
	Dim strSelect2
	Dim strSelect3
	Dim strSelect4
	Dim strSelect5
	Dim strSelect6
	Dim strSelect7
	Dim strSelect8
	Dim strSelect9
	Dim strSelect10
	Dim strSelect11
	Dim strSelect12
	Dim strSelect13
	Dim strDestinationNone
	
	IF intDestination="" OR intDestination="none" Then
		intDestination = 0
		strDestinationNone = "<option value=""none"">Select Destination</option>" & VbCrlf
	Else
		intDestination = Cint(intDestination)
	End IF

	IF intLocation="" OR intLocation="none" Then
		intLocation = 0
	Else
		intLocation = Cint(intLocation)
	End IF

	SELECT CASE intType
		Case 1'Destination

			IF intDestination=ConstDesIDBangkok Then
				strBangkokSelect = "selected"
			ElseIF intDestination=ConstDesIDPhuket Then
				strPhuketSelect = "selected"
			ElseIF intDestination=ConstDesIDChiangMai Then
				strChiangMaiSelect = "selected"
			ElseIF intDestination=ConstDesIDPattaya Then
				strPattayaSelect = "selected"
			ElseIF intDestination=ConstDesIDKohSamui Then
				strKohSamuiSelect = "selected"
			ElseIF intDestination=ConstDesIDKrabi Then
				strKrabiSelect = "selected"
			ElseIF intDestination=ConstDesIDKohChang Then
				strKohChangSelect = "selected"
			ElseIF intDestination=ConstDesIDChiangRai Then
				strChiangRaiSelect = "selected"
			ElseIF intDestination=ConstDesIDMaeHongSon Then
				strMaeHongSonSelect = "selected"
			ElseIF intDestination=ConstDesIDHuaHin Then
				strHuahinSelect = "selected"
			ElseIF intDestination=ConstDesIDChaAm Then
				strChaAmSelect = "selected"
			ElseIF intDestination=ConstDesIDPhangNga Then
				strPhangNgaSelect = "selected"
			ElseIF intDestination=ConstDesIDRayong Then
				strRayongSelect = "selected"
			ElseIF intDestination=ConstDesIDKohSamet Then
				strKohSametSelect = "selected"
			ElseIF intDestination=ConstDesIDKanchanaburi Then
				strKanchanaburiSelect = "selected"
			ElseIF intDestination=ConstDesIDAyutthaya Then
				strAyutthayaSelect = "selected"
			ElseIF intDestination=ConstDesIDKhaoYai Then
				strKhaoYaiSelect = "selected"
			ElseIF intDestination=ConstDesIDNakornnayok Then
				strNakornnayokSelect = "selected"
			ElseIF intDestination=ConstDesIDNakornpathom Then
				strNakornpathomSelect = "selected"
			ElseIF intDestination=ConstDesIDRatchaburi Then
				strRatchaburiSelect = "selected"
			ElseIF intDestination=ConstDesIDUthaiThani Then
				strUthaiThaniSelect = "selected"
			ElseIF intDestination=ConstDesIDSamutprakarn Then
				strSamutprakarnSelect = "selected"
			ElseIF intDestination=ConstDesIDSongkhla Then
				strSongkhlaSelect = "selected"
			ElseIF intDestination=ConstDesIDPathumThani Then
				strPathumThaniSelect = "selected"	
			End IF
			
			function_gen_dropdown_location_golf = "<select name="""& strName &""" "& strJava &">" & VbCrlf
			function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & strDestinationNone & VbCrlf
			function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""30"" "& strBangkokSelect &">Bangkok</option>" & VbCrlf
			function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""31"" "& strPhuketSelect &">Phuket</option>" & VbCrlf
			function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""32"" "& strChiangMaiSelect &">Chiang Mai</option>" & VbCrlf
			function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""33"" "& strPattayaSelect &">Pattaya</option>" & VbCrlf
			function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""34"" "& strKohSamuiSelect &">Koh Samui</option>" & VbCrlf
			'function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""35"" "& strKrabiSelect &">Krabi</option>" & VbCrlf
			function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""36"" "& strChiangRaiSelect &">Chiang Rai</option>" & VbCrlf
			function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""37"" "& strChaAmSelect &">Cha Am</option>" & VbCrlf
			function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""38"" "& strHuaHinSelect &">Hua Hin</option>" & VbCrlf
			function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""42"" "& strRayongSelect &">Rayong</option>" & VbCrlf
			'function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""43"" "& strMaeHongSonSelect &">Mae Hong Son</option>" & VbCrlf
			function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""44"" "& strBangkokSelect &">Ayutthaya</option>" & VbCrlf
			function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""45"" "& strKanchanaburiSelect &">Kanchanaburi</option>" & VbCrlf
			'function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""46"" "& strKohChangSelect &">Kanchanaburi</option>" & VbCrlf
			'function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""50"" "& strKohSametSelect &">Koh Samet</option>" & VbCrlf
			'function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""51"" "& strPhangNgaSelect &">Phang Nga</option>" & VbCrlf
			function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""52"" "& strKhaoYaiSelect &">Khao Yai</option>" & VbCrlf
			function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""56"" "& strNakornpathomSelect &">Nakornpathom</option>" & VbCrlf
			function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""57"" "& strNakornnayokSelect &">Nakornnayok</option>" & VbCrlf
			function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""59"" "& strSamutprakarnSelect &">Samutprakarn</option>" & VbCrlf
			'function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""62"" "& strRatchaburiSelect &">Ratchaburi</option>" & VbCrlf
			'function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""67"" "& strUthaiThaniSelect &">Uthai Thani</option>" & VbCrlf
			
			function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""70"" "& strSongkhlaSelect &">Songkhla</option>" & VbCrlf
			function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "<option value=""95"" "& strPathumThaniSelect &">Pathum Thani</option>" & VbCrlf
			
			function_gen_dropdown_location_golf = function_gen_dropdown_location_golf & "</select>" & VbCrlf
	
		Case 2'Location
	
	END SELECT

END FUNCTION
%>