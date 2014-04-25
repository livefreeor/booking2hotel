$(document).ready(function(){
	
	
	if($("#cardForm").length)
	{
		$("#formBooking").validate(
		{
			rules: {
				email: "required",
				re_email: { equalTo: "#email" },
				cardnum: {creditcard2: function(){
				return $('#cardType').val(); 
				}
				
				
			}
		}
		});
	}else{
		$("#formBooking").validate({
                rules: {
                    email: "required", re_email: { equalTo: "#email" }
                }
        });
	}
	
	//$("#card_month").val(new Date().getMonth()+1);


	$(".digitCard").each(function(){
		$(this).keyup(function(){
		checkNumber($(this))
		});
	});
});
