var roomMaxAdult=0;
var transferMaxAdult=0;

$(function(){
	$(".ddPrice").each(function(){
		$(this).change(function(){
		calMaxAdult();
		});
	});
	
	$(".ddTransfer").each(function(){
		$(this).change(function(){
		calMaxTransfer();
		});
	});
	$("#btnTest").click(function(){
		checkBooking();
	});
	calMaxAdult();
	calMaxTransfer();
});
function calMaxTransfer()
{
	var max_adult_transfer=0;
	$(".ddTransfer").each(function(){
		optionQty=$(this).find("option:selected").stop().text().split(" ")[0];
		arrValue=$(this).find("option:selected").stop().val().split("_");
		optionValue=arrValue[0];
		max_adult_transfer=max_adult_transfer+($("#mtransfer_"+optionValue).text()*optionQty);
	});
	transferMaxAdult=max_adult_transfer;
}

function calMaxAdult()
{
	var max_adult_room=0;
	$(".ddPrice").each(function(){
		optionQty=$(this).find("option:selected").stop().text().split(" ")[0];
		arrValue=$(this).find("option:selected").stop().val().split("_");
		optionValue=arrValue[1];
		max_adult_room=max_adult_room+($("#madult_"+optionValue).text()*optionQty);
		
	});
	roomMaxAdult=max_adult_room;
}

function checkBooking()
{

	selAdult=parseInt($("#adult option:selected").val());
	selChild=parseInt($("#child option:selected").val());
	
	$("#errorRoom").removeClass("errorMessage");
	$("#errorTransfer").removeClass("errorMessage");

	if(selAdult>roomMaxAdult)
	{
		$("#errorRoom").text("Please check adult for room").addClass("errorMessage");
		return false;
	}
	
	if(transferMaxAdult>0)
	{
		if((selAdult+selChild)>transferMaxAdult)
		{
			$("#errorTransfer").text("Please check adult for transfer").addClass("errorMessage");
			return false;
		}
	}
	$("#bookingForm").submit();
	
}
