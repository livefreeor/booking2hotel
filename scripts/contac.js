$(document).ready(function () {

    
    $.get("/admin/front/ajax_hotels_contact.aspx", function (data) {
        $("#contact_form_send").html(data);

        $.validator.setDefaults({
            submitHandler: function () { SendContact(); }
        });
        $("#formcontact").validate({
            rules: {
                txtName: "required",
                txtEmail: {
                    required: true,
                    email: true
                },

                captcha_valid: "required"
            },
            messages: {
                txtName: "Please enter your firstname",
                txtEmail: "Please enter a valid email address",
                captcha_valid: "Please enter text from above image"
            }
        });

    });

});

function RefreshImage() {
    d = new Date();
    $("#img_captcha").attr("src", "/captcha.aspx?" + d.getTime());

}
function SendContact() {

    $("<img class=\"img_progress\" src=\"/images/progress.gif\" alt=\"Progress\" />").insertBefore(".contac_btn").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });

    var productId = $("#productDefault").val();
    var post = $("#formcontact").serialize();

    $.post("/admin/front/ajax_hotels_contact_send.aspx?pid=" + productId, post, function (data) {

        if (data == "sent") {
            var thankyou = "<div id=\"why_us_header\"><h4>Thank you very much</h4></div><br/></br><h4>Thank you for contact us. We will contact you as soon as possible.</h4>";
            $("#content_why").html(thankyou);
            $('html, body').animate({ scrollTop: $("#content_why").offset().top - 100 }, 500);
        }
        if (data == "failed") {
			
        }

        if (data == "captchar") {
            $("#captcha_valid .error").remove();
            $("#captcha_valid").parent("td").append("<label class=\"error\">The characters you entered didn't match the word verification. Please try again.<label>");
        }


    });
}