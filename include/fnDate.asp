<%
'Function ������Ѻ����¹����������������Ẻ�� ������ö���͡�ٻẺ�ʴ��Ţͧ���Ѿ���� 7 Ẻ
'�����: 2 ���
'1 date_input (date,Time) ����Ѻ��������ҷ���ͧ��è��ŧ �������������� Default ��ͤ�����һѨ�غѹ
'2 output_format (integer) ��ͤ�ҵ���Ţ��褣��ͧ������ Function �ʴ��� ���������ҧ��ҧ��ҧ

' 1 -> �ѹ������� 15 �á�Ҥ� 2545
' 2 -> �ѹ��� 15 �á�Ҥ� 2545
' 3 -> 15 �á�Ҥ� 2545
' 4 -> ������� 15 �.�. 45
' 5 -> 15:00 �. 15 �.�. 45
' 6 -> 15:00 �.
' 7 -> 15:00
' 8 -> ������ҵ�� Server
'9 -> 15/7/45
'10 -> 15:00 15/7/45
'11 -> 15 July 2002
'12 -> July 02
'13 -> 15 �.�. 45 15:00 �. 
'14-> 15 �.�. 45
'15 -> Jul 02
'16 -> Jul. 15, 03
'17 -> July 15, 2003 15:00
'18 -> 15 July, 2003

Function convert_date(date_input,output_format)

	Dim arr_full_month_convert_thai_date
	Dim arr_semi_month_convert_thai_date
	Dim arr_day_convert_thai_date
	Dim arr_eng_month_convert_thai_date
	Dim arr_semi_eng_month_convert_thai_date
	Dim day_num_convert_thai_date
	
	arr_full_month_convert_thai_date = array("���Ҥ�","����Ҿѹ��","�չҤ�","����¹","����Ҥ�","�Զع�¹","�á�Ҥ�","�ԧ�Ҥ�","�ѹ��¹","���Ҥ�","��Ȩԡ�¹","�ѹ�Ҥ�")
	arr_semi_month_convert_thai_date = array("�.�.","�.�.","��.�.","��.�.","�.�.","��.�.","�.�.","�.�.","�.�.","�.�.","�.�.","�.�.")
	arr_eng_month_convert_thai_date = array("January","February","March","April","May","June","July","August","September","October","November","December")
	arr_semi_eng_month_convert_thai_date = array("Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec")
	arr_day_convert_thai_date = array("�ҷԵ��","�ѹ���","�ѧ���","�ظ","�����","�ء��","�����")
	
	IF date_input="" Then
		date_input = Now()
	End IF

	day_num_convert_thai_date = WeekDay(date_input)

	SELECT CASE output_format
	
		Case 1
			convert_date = "�ѹ" & arr_day_convert_thai_date(day_num_convert_thai_date-1) & "��� " & Day(date_input)  & " " & arr_full_month_convert_thai_date(Month(date_input)-1) & " " & Year(date_input) + 543
			
		Case 2
			convert_date = "�ѹ��� " & Day(date_input)  & " " & arr_full_month_convert_thai_date(Month(date_input)-1) & " " & Year(date_input) + 543
			
		Case 3
			convert_date = Day(date_input)  & " " & arr_full_month_convert_thai_date(Month(date_input)-1) & " " & Year(date_input) + 543
			
		Case 4
			convert_date = arr_day_convert_thai_date(day_num_convert_thai_date-1) & " " & Day(date_input)  & " " & arr_semi_month_convert_thai_date(Month(date_input)-1) & " " & Year(date_input) + 543 - 2500
			
		Case 5
			convert_date =  FormatDateTime(date_input, 4) & " �. " & Day(date_input)  & " " & arr_semi_month_convert_thai_date(Month(date_input)-1) & " " & Year(date_input) + 543 - 2500
		
		Case 6
			convert_date =  FormatDateTime(date_input, 4) & " �. "
		
		Case 7
			convert_date =  FormatDateTime(date_input, 4)
			
		Case 8
			convert_date =  date_input
			
		Case 9
			convert_date = Day(date_input)  & "/"  & Month(date_input) & "/" & Year(date_input) + 543 - 2500
			
		Case 10
			convert_date = FormatDateTime(input, 4) & " " & Day(date_input)  & "/"  & Month(date_input) & "/" & Year(date_input) + 543 - 2500
			
		Case 11
			convert_date = Day(date_input)  & " " & arr_eng_month_convert_thai_date(Month(date_input)-1) & " " & Year(date_input)
			
		Case 12
			convert_date = arr_eng_month_convert_thai_date(Month(date_input)-1) & " " & Right(Year(date_input),2)
			
		Case 13
			convert_date =  Day(date_input)  & " " & arr_semi_month_convert_thai_date(Month(date_input)-1) & " " & Year(date_input) + 543 - 2500 & " " & FormatDateTime(date_input, 4) '& " �. "
			
		Case 14
			convert_date =  Day(date_input)  & " " & arr_semi_month_convert_thai_date(Month(date_input)-1) & " " & Year(date_input) + 543 - 2500

		Case 15
			convert_date = arr_semi_eng_month_convert_thai_date(Month(date_input)-1) & " " & Right(Year(date_input),2)
			
		Case 16
			convert_date = arr_semi_eng_month_convert_thai_date(Month(date_input)-1) & ". " & Day(date_input) & ", " & Right(Year(date_input),2)
			
		Case 17
			convert_date = arr_eng_month_convert_thai_date(Month(date_input)-1)  & " " & Day(date_input) & ", " & Year(date_input) & " " & FormatDateTime(date_input, 4)
		
		Case 18
			convert_date = arr_eng_month_convert_thai_date(Month(date_input)-1) & " " & Day(date_input) & ", " & Year(date_input)
			
	END SELECT

End Function
%>