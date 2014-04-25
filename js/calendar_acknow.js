
var date_advance_bht=1;

function CreateCalendar()
{
	
	$(".vPicker").each(function(index){
		cInput=this.id;	
		cInput=cInput.replace("Input","");
		if(!$("#"+cInput).length)
		{
			dateInputDefault=$(this).attr("rel");
			$(this).after('<input type="hidden" name="'+cInput+'" id="'+cInput+'">');
			if($("#"+cInput).val()!=""){
				$("#"+cInput).val(dateInputDefault);
				$(this).val(getFormatDate(dateInputDefault,1))
				//alert($(this).val());
			}else{
				if(dateInputDefault!="")
				{
					$("#"+cInput).val(dateInputDefault);
					$(this).val(getFormatDate(dateInputDefault,1))
				}else{
					$(this).val("");
				}
			
			}
			
		}
		
		
			setDatePickerInput2(this.id);
		
		
	});
}

function getDateDiff(dateStart,dateEnd)
{
	return (Math.ceil((dateEnd.getTime()-dateStart.getTime())/(24*60*60*1000)));
}
function getFormatDate(dateInput,typeFormat)
{

	var arrMonth = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
	
	var dateSplit =dateInput.split("-");
    var dateYear = dateSplit[0];
    var dateMonth = dateSplit[1] - 1;
    var dateDate = dateSplit[2];
	
	dDate = new Date(dateYear, dateMonth, dateDate);	
	var stringDateFormat = dDate.getDate() + "-" + arrMonth[dDate.getMonth()] + "-" + dDate.getFullYear();
	return stringDateFormat;
}

function setDatePickerInput(elemInput,dateDefault,dateMin,typeDisplay){

	cInput='#'+elemInput.replace("Input","");
	$("#"+elemInput).datepicker({
	minDate:dateMin,
	changeMonth:true,
	changeYear:true,
	dateFormat:'dd-M-yy',
	altField:cInput,
	altFormat:'mm/dd/yy',
	showOn: 'both',
    buttonImage: 'vcalendar/ico_calendar_search.jpg',
    buttonImageOnly: true,
    showButtonPanel: true
		});
}

function setDatePickerInput2(elemInput){

	cInput='#'+elemInput.replace("Input","");
	$("#"+elemInput).datepicker({
	changeMonth:true,
	changeYear:true,
	dateFormat:'dd-M-yy',
	altField:cInput,
	altFormat:'mm/dd/yy',
	showOn: 'both',
    buttonImage: '/images/calendaricon.png',
    buttonImageOnly: true,
    showButtonPanel: true
		});
}

function getDateformat(dateInput)
{
	arrDate=dateInput.split('-');
	arrMonth=new Array("Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec");
	
	for(i=0;i<arrMonth.length;i++)
	{
		if(arrMonth[i]==arrDate[1])
		{
			result=arrDate[2]+"-"+(i+1)+"-"+arrDate[0];
		}
	}
	return result;
	
}
//End DatePicker