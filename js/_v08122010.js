function bhtDateDiff(dateStart,dateEnd)
{
	return (Math.ceil((dateEnd.getTime()-dateStart.getTime())/(24*60*60*1000)));
}
function bookingSubmit()
{
	$('#bookingForm').submit();
}
function fnSubmit() {

	//$('#bookingForm').submit();
	//dest="+$('#destinationID')+"&loc="+$('#locationID')"+&month_day_in="+arrDateIn[1]+"&month_year_in="+arrDateIn[0]+"-"+arrDateIn[2]+"&month_day_out="+arrDateOut[1]+"&month_year_out="+arrDateOut[0]+"-"+arrDateOut[2];
	strUrl='/search_process.aspx?';
	
	if($('#dateStart').val()=='' || $('#dateEnd')==''){
		alert('Please input date');
		return false;
	}
	
	arrDateIn=$('#dateStart').val().split('/')
	arrDateOut=$('#dateEnd').val().split('/');
	
	dest_id="dest="+$('#destinationID').val();
	loc_id="&loc="+$('#locationID').val();
	
	
	
	if($("input[@name='category']:checked").val()!=null){
		cate_id="&cate="+$("input[@name='category']:checked").val();
	}else{
		cate_id="&cate="+querySt("cate");
	}
	if(querySt("type_id")==null)
	{
		type_id="&sort_by=1";
	}
	
	
	
	dayIn="&month_day_in="+arrDateIn[1];
	MonthYearIn = "&month_year_in=" + arrDateIn[0] + "-" + arrDateIn[2];
	if (loc_id == 29) {
	    dayOut = "&month_day_out=" + arrDateOut[1];
	    MonthYearOut = "&month_year_out=" + arrDateOut[0] + "-" + arrDateOut[2];
	    //alert(strUrl+dest_id+loc_id+dayIn+MonthYearIn+dayOut+MonthYearOut+cate_id);
	    location.href = strUrl + dest_id + loc_id + dayIn + MonthYearIn + dayOut + MonthYearOut + cate_id + type_id;
	} else {
	    dayOut = "&month_day_out=" + arrDateIn[1] + 1;
	    MonthYearOut = "&month_year_out=" + arrDateOut[0] + "-" + arrDateOut[2];
	    //alert(strUrl+dest_id+loc_id+dayIn+MonthYearIn+dayOut+MonthYearOut+cate_id);
	    location.href = strUrl + dest_id + "&loc=0" + dayIn + MonthYearIn + dayOut + MonthYearOut + cate_id + type_id;
	}
}


function fnInit()
{
	arrlist=$("a");
	for(var i=0;i<arrlist.length;i++)
	{
		attribute=$.attr($("a").get(i),'href')+ window.location.search;
		arrlist[i].setAttribute("href",attribute);
	}
}

function genDateFormatForCalendar(intDay,strMonthYear)
{
	arrData=strMonthYear.split('-');
	return arrData[0]+"/"+intDay+"/"+arrData[1]
}

$(function(){
	var dateStart=new Date().addDays(1);
	var dateEnd=new Date().addDays(3);
	
	$("#dateStart").datepicker({minDate:1})
	$("#dateEnd").datepicker({minDate:2})
	
	if(querySt("month_day_in")!=null){
		$("#dateStart").val(genDateFormatForCalendar(querySt("month_day_in"),querySt("month_year_in")));
		$("#dateEnd").val(genDateFormatForCalendar(querySt("month_day_out"),querySt("month_year_out")));
	}
	
	
	$("#dateStart").change(function(){
		$("#dateEnd").datepicker( "destroy");
		$("#dateEnd").datepicker({minDate:bhtDateDiff(new Date(),$("#dateStart").datepicker("getDate"))+1})
	});
	
	$("#dateEnd").change(function(){
		$("#dateStart").datepicker( "destroy");
		$("#dateStart").datepicker({maxDate:bhtDateDiff(new Date(),$("#dateEnd").datepicker("getDate"))-1})
	});
	

	if($('#orderby').length>0)
	{
		$radios = $('input:radio[name=orderby]');
		
    	if($radios.is(':checked') === false) { 
        	$radios.filter("[value="+querySt("sort_by")+"]").attr('checked', true); 
    	} 
	}
		
	genDropDownDestination("ddDestination")
	fnInit()
});