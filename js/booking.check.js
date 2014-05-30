$(document).ready(function(){
	

    $("#formBooking").on('submit', function () {
        var isvalidate = $("#formBooking").valid();
        if (isvalidate) {
            if (!$("#chkPolicy").is(':checked')) {
                alert("Please accept the cancellation policy and user agreement before proceeding to check out.");
                return false;
            }
            LoadingPanelShow();
        } else {
            return false;
        }
    });

    if ($("#current_gateway").length) {

        var gateway = $("#current_gateway").val();

        var country = $("select[name='country'] :selected").val();


        // canada = 37
        //United States = 222
        $("select[name='country']").change(function () {
            var countryVal = $(this).val().split(',')[0];
            if (countryVal == 37 || countryVal == 222) {

                $("#drop_state").show();
                $("#drop_state").addClass("required valid");
                //$(this).addClass("valid");
                console.log($(this).val());


                if (countryVal == 37) {
                    //stat_ca

                    $("#drop_state select").html($("#stat_ca").html());
                    //$(".ca").show();
                    //$(".us").hide();
                }
                if (countryVal == 222) {
                    //stat_us
                    $("#drop_state select").html($("#stat_us").html());
                    //$(".ca").hide();
                    //$(".us").show();
                }

            } else {
                $("#drop_state").hide();
                $("#drop_state").removeClass("required");
                $("#drop_state").removeClass("valid");
            }
        });
    }
	
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
