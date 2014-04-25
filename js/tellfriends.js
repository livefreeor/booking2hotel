$.validator.setDefaults({
        submitHandler: function () { Submit_SendMail(); }
    });
    $(document).ready(function () {
       
        //$("#formTellFriends").validate();
        // validate signup form on keyup and submit
        $("#formTellFriends").validate({
            rules: {
                fName: "required",
                email: {
                    required: true,
                    email: true
                },
                friendName: "required",

                friendEmail: {
                    required: true,
                    email: true
                },
                textarea: "required",
                captcha_valid: "required"
            },
            messages: {
                fName: "Please enter your firstname",
                email: "Please enter a valid email address",
                friendName: "Please enter your firstname",
                friendEmail: "Please enter a valid email address",
                textarea: "Please enter your firstname",
                captcha_valid: "Please enter text from above image"
            }
        });

    });
        
    function RefreshImage() {
        d = new Date();
        $("#img_captcha").attr("src", "../captcha.aspx?" + d.getTime());

    }

    function Submit_SendMail() {
        $("<img class=\"img_progress\" src=\"/images/progress.gif\" alt=\"Progress\" />").insertAfter("#product_detail").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });
        var post = $('#formTellFriends').serialize();
        var qProductId = GetValueQueryString("pd");
        $.post("../admin/ajax/ajax_thailand_hotels_tell.aspx?pd=" + qProductId, post, function (data) {

            if (data == "NO") {
                var Palert = $("#captcha_valid").parent("td").find("p").stop();
                if (Palert.length > 0) {
                    Palert.remove();
                }
                $("#captcha_valid").parent("td").append("<p class=\"error\">The characters you entered didn't match the word verification. Please try again.<p>");
            } else {
                post_to_url('http://www.hotels2thailand.com/thailand-hotels-tell-thankyou.aspx', { 'pd': qProductId ,'m': data});

                //window.location.href = "/thailand-hotels-tell-thankyou.aspx?pd=" + qProductId + "&m=" + data;
            }

        });

    }
    function post_to_url(path, params, method) {
        method = method || "post"; // Set method to post by default, if not specified.

        // The rest of this code assumes you are not using a library.
        // It can be made less wordy if you use one.
        var form = document.createElement("form");
        form.setAttribute("method", method);
        form.setAttribute("action", path);

        for (var key in params) {
            var hiddenField = document.createElement("input");
            hiddenField.setAttribute("type", "hidden");
            hiddenField.setAttribute("name", key);
            hiddenField.setAttribute("value", params[key]);

            form.appendChild(hiddenField);
        }

        document.body.appendChild(form);    // Not entirely sure if this is necessary
        form.submit();
    }
