

function RenderRichBox() {

    $('textarea.mceEditor').tinymce({
        // Location of TinyMCE script
        script_url: '/js/tinymce/tiny_mce.js',

        // General options
        theme: "advanced",
        force_br_newlines: true,
        force_p_newlines: false,
        // Theme options
        plugins: "safari,emotions,preview",
        theme_advanced_buttons1: "undo,redo,|,bold,italic,underline,strikethrough,forecolor,backcolor,|,justifyleft,justifycenter,justifyright,|,bullist,numlist,outdent,indent,|,link,unlink,|,removeformat,custom_button_test,preview,code",
        theme_advanced_buttons2: "",
        theme_advanced_buttons3: "",
        theme_advanced_buttons4: "",
        theme_advanced_toolbar_location: "top",
        theme_advanced_toolbar_align: "left",
        theme_advanced_statusbar_location: "bottom",
        theme_advanced_resizing: true,

        // Example content CSS (should be your site CSS)
        content_css: "css/content.css",

        // Drop lists for link/image/media/template dialogs
        template_external_list_url: "lists/template_list.js",
        external_link_list_url: "lists/link_list.js",
        external_image_list_url: "lists/image_list.js",
        media_external_list_url: "lists/media_list.js",


        // Replace values for the template plugin
        template_replace_values: {
            username: "Some User",
            staffid: "991234"
        }
    });
}


function showhide(id) {
    $("#" + id).slideToggle('fast');

    
}
function DisablePackageDetail() {

    $("#package_detail_insert").slideUp('600', function () {
        $("#package_detail_list").slideDown('600', function () {
            var optionid = $("#dropPackage").val();
            var ConnameId = $("#conditionTitle").val();
            var NumGuest = $("#drop_adult_child").val();
            var Isadult = $("input[name='chk_adult_child']").val();
            //ConditionDuplicate_check(optionid, ConnameId, NumGuest, Isadult);
        });

        $("#dropNight").attr("disabled", "disabled");
        $("#dropNight").css("background-color", "#fbfbf9");

        $("#Booking_DateStart").attr("disabled", "disabled");
        $("#Booking_DateEnd").attr("disabled", "disabled");
        $("#Stay_DateStart").attr("disabled", "disabled");
        $("#Stay_DateEnd").attr("disabled", "disabled");

        $("#Booking_DateStart").css("background-color", "#fbfbf9");
        $("#Booking_DateEnd").css("background-color", "#fbfbf9");
        $("#Stay_DateStart").css("background-color", "#fbfbf9");
        $("#Stay_DateEnd").css("background-color", "#fbfbf9");
    });
    
    
   


}

function RollbackPackageDetail() {
    $("#package_detail_insert").slideDown('600');
    $("#package_detail_list").hide();
    $("#dropNight").removeAttr("disabled");
    $("#dropNight").css("background-color", "#faffbd");


    $("#Booking_DateStart").removeAttr("disabled");
    $("#Booking_DateEnd").removeAttr("disabled");
    $("#Stay_DateStart").removeAttr("disabled");
    $("#Stay_DateEnd").removeAttr("disabled");

    $("#Booking_DateStart").css("background-color", "#ffffff");
    $("#Booking_DateEnd").css("background-color", "#ffffff");
    $("#Stay_DateStart").css("background-color", "#ffffff");
    $("#Stay_DateEnd").css("background-color", "#ffffff");


}


function appendpackage() {

    var TypeTitle = "";

    //var countItem = $("#package_detail_list .package_list_item").length - 1;

    var random = makeid();

    //        if ($("#txt_Type_custom").val() == "") {
    //            TypeTitle = $("#droppackageType :selected").text();
    //        } else {
    //            TypeTitle = $("#txt_Type_custom").val();
    //        }

    var package = $("#txt_package_detail").val();

    var itemInsert = "<div class=\"package_list_item\" id=\"package_list_item_" + random + "\" style=\"display:none;\" >";
    itemInsert = itemInsert + "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\"><tr>";
    itemInsert = itemInsert + "<td width=\"2%\" align=\"right\" ><img src=\"../../images/greenbt.png\" /><input style=\"display:none;\" checked=\"checked\"  type=\"checkbox\" name=\"packagelist\" value=\"" + random + "\" />&nbsp;&nbsp;</td>";
    //        itemInsert = itemInsert + "<td width=\"10%\" style=\"font-weight:bold;\" >" + TypeTitle + "&nbsp;:&nbsp;<input type=\"hidden\" name=\"package_type_" + random + "\" value=\"" + TypeTitle + "\" /></td>";
    itemInsert = itemInsert + "<td width=\"94%\" align=\"left\" >" + package + "<input type=\"hidden\" name=\"package_" + random + "\" value=\"" + package + "\" /></td>";
    itemInsert = itemInsert + "<td width=\"5%\"><img src=\"/images_extra/bin.png\" style=\"cursor:pointer;\" onclick=\"remove('package_list_item_" + random + "');\"  /></td>";
    itemInsert = itemInsert + "</tr>";
    itemInsert = itemInsert + "</table></div>";


    $("#package_detail_list").append(itemInsert);
    $("#txt_package_detail").val("");
    $("#package_list_item_" + random).fadeIn();

}


function remove(targetid) {

    $("#" + targetid).fadeTo("fast", 0.00, function () { //fade
        $(this).slideUp("300", function () { //slide up
            $(this).remove(); //then remove from the DOM
        });
    });

    return false;

}

function addRule() {


    var random_cancel = makeid();


    var ListItem = "<div class=\"cancel_list_item\" id=\"cancel_list_item_" + random_cancel + "\" style=\"display:none;\" >";


    ListItem = ListItem + "<input type=\"checkbox\" checked=\"checked\" value=\"" + random_cancel + "\" name=\"cencel_list_Checked\" style=\"display:none;\" />"

    ListItem = ListItem + "<table cellpadding=\"5\" cellspacing=\"1\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center; margin:0px 0px 0px 0px;\">";
    ListItem = ListItem + "<tr style=\"background-color:#ffffff;\" >";
    ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 2px 0px; padding:2px 0px 2px 0px;\" width=\"30%\">";
    ListItem = ListItem + "<select id=\"drop_daycancel_" + random_cancel + "\" class=\"Extra_Drop\" name=\"drop_daycancel_" + random_cancel + "\" >";
    for (i = 0; i <= 120; i++) {
        if (i == 0) {
            ListItem = ListItem + "<option value=\"" + i + "\">no-show</option>";
        }
        else {
            ListItem = ListItem + "<option value=\"" + i + "\">" + i + "</option>";
        }
    }
    ListItem = ListItem + "</select>";
    ListItem = ListItem + "</td>";
    ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 2px 0px; padding:2px 0px 2px 0px;\" width=\"30%\">";
    ListItem = ListItem + "<input type=\"text\" id=\"txt_day_charge_" + random_cancel + "\" maxlength=\"2\" value=\"0\" style=\"width:20px;\" class=\"Extra_textbox\" name=\"txt_day_charge_" + random_cancel + "\"  />";
    ListItem = ListItem + "</td>";
    ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 2px 0px; padding:2px 0px 2px 0px;\" width=\"30%\">";
    ListItem = ListItem + "<input type=\"text\" id=\"txt_per_charge_" + random_cancel + "\" maxlength=\"3\" value=\"0\" style=\"width:22px;\" class=\"Extra_textbox\" name=\"txt_per_charge_" + random_cancel + "\" />";
    ListItem = ListItem + "</td>";
    ListItem = ListItem + "<td width=\"10%\"><img src=\"../../images_extra/del.png\" style=\"cursor:pointer;\" onclick=\"remove('cancel_list_item_" + random_cancel + "');return false;\" /></td>";
    ListItem = ListItem + "</tr>";
    ListItem = ListItem + "</table>";
    ListItem = ListItem + "</div>";

    $("#cancel_list").append(ListItem);


    var countItem = $("#cancel_list").find(".cancel_list_item").length;


    $("#cancel_list").find(".cancel_list_item").filter(function (index) {
        return index == (countItem - 1)
    }).fadeIn();

}

function DayancelCheckLoadTariff(id, position) {
    var resultDayMain = 0;
    var Y_top = $("#" + id).offset().top + 23;
    var X_left = $("#" + id).offset().left;
    //

    var text = "*You can not add the same no.of days cancel. Please recheck";
    var optionwidth = 0;
    var optionheight = 0;

    if (position == "left") {
        optionwidth = $("#" + id).width() + 10;
        optionheight = $("#" + id).outerHeight();
        //alert(optionheight);
    }
    else {
        optionwidth = ($("#" + id).width() + 10) - $("#" + id).width();
    }


    optionheight = $("#" + id).outerHeight() - 11;


    $(".period_list_item").each(function () {
        var cancelListId = $(this).attr("id");

        // var MainChecked = $(this).find("input[name='period_list_Checked']:checked").val();
        var resultDay = 0;
        var countindex = 0;


        $(this).find(".cancel_list_item").each(function () {

            var CheckVal = $(this).find("input[name^='cencel_list_Checked']:checked").val();

            var DayCancel = $(this).find(":selected").val();


            var CountDetect = $(this).parent().find(".cancel_list_item").filter(function (index) {
                return $(this).find(":selected").val() == DayCancel && index != countindex;
            }).length;



            if (CountDetect > 0) {
                resultDay = resultDay + CountDetect;
                return false;
            }

            countindex = countindex + 1;



        });

        if (resultDay > 0) {
            resultDayMain = resultDayMain + 1;
            return false;
        }

    });



    if (resultDayMain > 0) {
        if (!$("#valid_alert_" + id).length) {
            $("#" + id).css("background-color", "#ffebe8");
            $("#" + id).next("div").stop().css("background-color", "#ffebe8");
            $("body").after("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
            $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
            $("#valid_alert_" + id).fadeIn('fast');
        }
    } else {

        $("#" + id).css("background-color", "#fbfbf9");
        $("#" + id).next("div").stop().css("background-color", "#fbfbf9");

        $("#valid_alert_" + id).fadeOut('fast', function () {

            $(this).remove();
        });
    }



    $(".cancel_list_item").each(function () {

        var CheckVal = $(this).find(":checked").val();



        $(this).find("select").stop().change(function () {

            //var DayCharge = $(this).val();
            var countindex = 0;
            var DayCancel = $(this).val();

            var CancelId = $(this).attr("id");
            var result1 = 0;
            var result2 = 0;
            $(".period_list_item").each(function () {

                var cancelListId = $(this).attr("id");

                //var MainChecked = $(this).find("input[name='period_list_Checked']:checked").val();

                var countindex = 0;


                $(this).find(".cancel_list_item").each(function () {

                    var CheckVal = $(this).find("input[name^='cencel_list_Checked']:checked").val();

                    var DayCancel = $(this).find(":selected").val();


                    var CountDetect = $(this).parent().find(".cancel_list_item").filter(function (index) {
                        return $(this).find(":selected").val() == DayCancel && index != countindex;
                    }).length;



                    if (CountDetect > 0) {
                        result1 = result1 + CountDetect;
                        return false;
                    }

                    countindex = countindex + 1;



                });



                if (result1 > 0) {
                    result2 = result2 + 1;
                    return false;
                }

            });



            if (result2 > 0) {
                if (!$("#valid_alert_" + id).length) {
                    $("#" + id).css("background-color", "#ffebe8");
                    $("#" + id).next("div").stop().css("background-color", "#ffebe8");
                    $("body").after("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                    $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                    $("#valid_alert_" + id).fadeIn('fast');
                }
            } else {

                $("#" + id).css("background-color", "#fbfbf9");
                $("#" + id).next("div").stop().css("background-color", "#fbfbf9");

                $("#valid_alert_" + id).fadeOut('fast', function () {

                    $(this).remove();
                });
            }

        });


    });

    return resultDayMain;
}

function CancelCheckLoadTariff(id, position) {



    //        var DateBookingstart = $("#hd_" + datestart).val();
    //        var DateBookingend = $("#hd_" + dateend).val();

    var Y_top = $("#" + id).offset().top + 23;
    var X_left = $("#" + id).offset().left;
    //        
    var text = "*Please add the number of in No.of Night(s) charge or Percentage Charge.";
    var optionwidth = 0;
    var optionheight = 0;

    if (position == "left") {
        optionwidth = $("#" + id).width() + 10;
        optionheight = $("#" + id).height();
        //alert(optionheight);
    }
    else {
        optionwidth = ($("#" + id).width() + 10) - $("#" + id).width();
    }


    optionheight = $("#" + id).height() - 11;


    //result = daydiff(parseDate(DateBookingstart), parseDate(DateBookingend));
    var regExpr = new RegExp("^[0-9][0-9]*$");
    var resultCharge = 0;
    $(".cancel_list_item").each(function () {


        var CheckVal = $(this).find(":checked").val();

        var DayCharge = $(this).find("input[name^='txt_day_charge_']").val();
        var DayPer = $(this).find("input[name^='txt_per_charge_']").val();


        if (DayCharge == "0" && DayPer == "0") {
            resultCharge = resultCharge + 1;
        }

        if (DayCharge > 0 && DayPer > 0) {
            resultCharge = resultCharge + 1;
        }

        if (DayCharge == "" && DayPer == "") {
            resultCharge = resultCharge + 1;
        }

        if (!regExpr.test(DayCharge) || !regExpr.test(DayPer)) {
            resultCharge = resultCharge + 1;
        }


    });


    if (resultCharge > 0) {
        if (!$("#valid_alert_" + id).length) {
            $("#" + id).css("background-color", "#ffebe8");
            //$("#" + id).next("div").stop().css("background-color", "#ffebe8");
            $("body").after("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
            $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
            $("#valid_alert_" + id).fadeIn('fast');
        }
    } else {

        $("#" + id).css("background-color", "#fbfbf9");
        //$("#" + id).next("div").stop().css("background-color", "#fbfbf9");

        $("#valid_alert_" + id).fadeOut('fast', function () {

            $(this).remove();
        });
    }

    $("input[name^='txt_day_charge_']").keyup(function () {

        var result1 = 0;
        $(".cancel_list_item").each(function () {
            var DayCharge = $(this).find("input[name^='txt_day_charge_']").val();
            var DayPer = $(this).find("input[name^='txt_per_charge_']").val(); //$("#txt_per_charge_" + CheckVal).val();
            //alert(DayCharge + "--" + DayPer);
            if (DayCharge == "0" && DayPer == "0") {
                result1 = result1 + 1;
            }

            if (DayCharge > 0 && DayPer > 0) {
                result1 = result1 + 1;
            }

            if (DayCharge == "" && DayPer == "") {
                result1 = result1 + 1;
            }

            if (!regExpr.test(DayCharge) || !regExpr.test(DayPer)) {
                result1 = result1 + 1;
            }
        });


        if (result1 > 0) {
            if (!$("#valid_alert_" + id).length) {
                $("#" + id).css("background-color", "#ffebe8");
                //$("#" + id).next("div").stop().css("background-color", "#ffebe8");
                $("body").after("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                $("#valid_alert_" + id).fadeIn('fast');
            }
        } else {

            $("#" + id).css("background-color", "#fbfbf9");
            //$("#" + id).next("div").stop().css("background-color", "#fbfbf9");

            $("#valid_alert_" + id).fadeOut('fast', function () {

                $(this).remove();
            });
        }

    });


    $("input[name^='txt_per_charge_']").keyup(function () {

        var result2 = 0;
        $(".cancel_list_item").each(function () {
            var DayCharge = $(this).find("input[name^='txt_day_charge_']").val(); //$("#txt_day_charge_" + CheckVal).val();
            var DayPer = $(this).find("input[name^='txt_per_charge_']").val();
            //alert(DayCharge + "--" + DayPer);

            if (DayCharge == "0" && DayPer == "0") {
                result2 = result2 + 1;
            }

            if (DayCharge > 0 && DayPer > 0) {
                result2 = result2 + 1;
            }

            if (DayCharge == "" && DayPer == "") {
                result2 = result2 + 1;
            }

            if (!regExpr.test(DayCharge) || !regExpr.test(DayPer)) {
                result2 = result2 + 1;
            }

        });

        if (result2 > 0) {
            if (!$("#valid_alert_" + id).length) {
                $("#" + id).css("background-color", "#ffebe8");
                //$("#" + id).next("div").stop().css("background-color", "#ffebe8");
                $("body").after("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                $("#valid_alert_" + id).fadeIn('fast');
            }
        } else {

            $("#" + id).css("background-color", "#fbfbf9");
            //$("#" + id).next("div").stop().css("background-color", "#fbfbf9");

            $("#valid_alert_" + id).fadeOut('fast', function () {

                $(this).remove();
            });
        }
    });

    return resultCharge;
}

function SurCharge_Checked() {

    $("#surcharge_amount").slideToggle();


    var hd_DateFrom = $("#hd_rate_DateStart").val();
    var hd_Dateto = $("#hd_rate_DateEnd").val();

    GetHolidaySurchargeList(hd_DateFrom, hd_Dateto);


    $("#rate_DateStart").change(function () {
        var hd_DateFrom_fr = $("#hd_rate_DateStart").val();
        var hd_Dateto_fr = $("#hd_rate_DateEnd").val();
        GetHolidaySurchargeList(hd_DateFrom_fr, hd_Dateto_fr);
    });

    $("#rate_DateEnd").change(function () {
        var hd_DateFrom_to = $("#hd_rate_DateStart").val();
        var hd_Dateto_to = $("#hd_rate_DateEnd").val();

        GetHolidaySurchargeList(hd_DateFrom_to, hd_Dateto_to);
    });

    //-------------------------------------------------------------------


}

function AddRate() {
    

    var DateFrom = $("#rate_DateStart").val();
    var DateTo = $("#rate_DateEnd").val();

    var hd_DateFrom = $("#hd_rate_DateStart").val();
    var hd_Dateto = $("#hd_rate_DateEnd").val();


    var amount = "0";
    if ($("#rate_amount").val() != "") {
        amount = $("#rate_amount").val();
    }


    var surAmount = "0";
    if ($("#sur_amount").val() != "") {
        surAmount = $("#sur_amount").val();
    }


    var random = makeid();

    var DaySurVal = "";
    if ($("#sur_checked").is(":checked")) {
        $("#day_list :checked").each(function (index) {
            DaySurVal = DaySurVal + $(this).val() + ",";
        });
    }

    var holidaySur = "No";
    if (GetholidaySurchargeHdList() != "") {
        //var holidaySur = "<label style=\"font-weight:bold;color:#ba0c0c;\">Yes</lable>";

        holidaySur = "<a href=\"\" onclick=\"return false;\" style=\"font-weight:bold;color:#ba0c0c;\"  class=\"tooltip\" >Yes";
        holidaySur = holidaySur + "<span class=\"tooltip_content\">";
        holidaySur = holidaySur + GetholidaySurchargeHdList_detail();
        holidaySur = holidaySur + "</span>";
        holidaySur = holidaySur + "</a>";
    }
    
    var ListItem = "<div class=\"rate_result_list\" id=\"rate_result_list_" + random + "\" style=\"display:none;\">";
    ListItem = ListItem + "<input type=\"checkbox\" id=\"checked_rate_result_" + random + "\" style=\"display:none;\" value=\"" + random + "\" name=\"rate_result_checked\" checked=\"checked\" />";
    ListItem = ListItem + "<table width=\"100%\">";
    ListItem = ListItem + "<tr align=\"center\">";
    ListItem = ListItem + "<td width=\"15%\">" + DateFrom + "<input type=\"hidden\" name=\"hd_rate_date_form_" + random + "\" value=\"" + hd_DateFrom + "\" /></td>";
    ListItem = ListItem + "<td width=\"15%\">" + DateTo + "<input type=\"hidden\" name=\"hd_rate_date_To" + random + "\" value=\"" + hd_Dateto + "\" /></td>";
    ListItem = ListItem + "<td width=\"10%\">" + amount + "<input type=\"hidden\" name=\"hd_amount" + random + "\" value=\"" + amount + "\" /></td>";

    ListItem = ListItem + "<td width=\"10%\">" + surAmount + "<input type=\"hidden\" name=\"hd_surAmount" + random + "\" value=\"" + surAmount + "\" /></td>";
    ListItem = ListItem + "<td width=\"20%\"><div class=\"day_of_week_show\" id=\"day_of_week_show_" + random + "\"><p>S</p><p>M</p><p>T</p><p>W</p><p>T</p><p>F</p><p>S</p></div><div style=\"clear:both;\"></div><input type=\"hidden\" name=\"hd_day_checked_sur" + random + "\" value=\"" + DaySurVal + "\" /></td>";
    ListItem = ListItem + "<td width=\"10%\">" + holidaySur + "<input type=\"hidden\" name=\"hd_holiday_Sur" + random + "\" value=\"" + GetholidaySurchargeHdList() + "\" /></td>";

    ListItem = ListItem + "<td width=\"5%\"><img src=\"../../images_extra/bin.png\" onclick=\"removerate('rate_result_list_" + random + "');\" style=\"cursor:pointer;\"  /></td>";
    ListItem = ListItem + "</tr>";
    ListItem = ListItem + "</table>";
    ListItem = ListItem + "</div>";

    //alert(ValidateOptionMethod("rate_amount", "number0"));

    //alert(PeriodValidCheck("rate_insert", "rate_DateStart", "rate_DateEnd", "", "rate_result_checked", "hd_rate_date_form_", "hd_rate_date_To"));


    if (ValidateOptionMethod("rate_amount", "number0") == true && PeriodValidCheck("rate_insert", "rate_DateStart", "rate_DateEnd", "", "rate_result_checked", "hd_rate_date_form_", "hd_rate_date_To") == 0) {
        $("#rate_load_head").show();
        $("#rate_load_result").append(ListItem);

        //alert(ListItem);

        var countItem = $("#rate_load_result .rate_result_list").length;

        if ($("#sur_checked").is(":checked")) {
            $("#day_list :checked").each(function () {
                var checkedVal = $(this).val();
                $("#day_of_week_show_" + random).children("p").filter(function (index) { return index == checkedVal }).css({ "color": "#a20c0c", "font-weight": "bold" });

            });
        }

        $("#rate_load_result .rate_result_list").filter(function (index) {
            return index == (countItem - 1)
        }).fadeIn();

        tooltip();

        $("#sur_checked").removeAttr("checked")
        $("#day_list :checkbox").removeAttr("checked");

        $("#surcharge_amount").hide();

        $("#rate_allot").val("");
        $("#sur_amount").val("");
        $("#rate_amount").val("");

        $("#holiday_surcharge_charge").html("");
    }

    ChkDatePackageValid();

}
function GetholidaySurchargeHdList_detail() {
    var strholidayDetail = "";

    if ($("input[name='chk_supplement']:checked").length > 0) {

        strholidayDetail = "<table style=\"width:100%;font-size:11px;\">";

        $("input[name='chk_supplement']:checked").each(function () {

            var checkVal = $(this).val();
            if ($("#supplement_amount_" + checkVal).val() != "") {
                
                strholidayDetail = strholidayDetail + "<tr>";
                strholidayDetail = strholidayDetail + "<td>" + $("#hd_date_supplement_title_" + checkVal).val() + "</td>";
                strholidayDetail = strholidayDetail + "<td><strong>" + $("#supplement_amount_" + checkVal).val() + "</strong> Baht of Surcharge</td>";

                strholidayDetail = strholidayDetail + "</tr>";
            }


        });

        strholidayDetail = strholidayDetail + "</table>";
        
    
    }
    return strholidayDetail;
}
function GetholidaySurchargeHdList() {
    var ListItem = "";

    if ($("input[name='chk_supplement']:checked").length > 0) {
        $("input[name='chk_supplement']:checked").each(function () {

            var checkVal = $(this).val();
            if ($("#supplement_amount_" + checkVal).val() != "") {

                ListItem = ListItem + $("#hd_date_supplement_" + checkVal).val() + ";"
            + $("#supplement_amount_" + checkVal).val() + "#";
            }


        });
    }
    return ListItem;
}

function GetHolidaySurchargeList(dFrom, dTo) {

    $("<img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertBefore("#holiday_surcharge_charge").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });

    $.get("../ajax/ajax_holiday_surcharge_list.aspx?dFrom=" + dFrom + "&dTo=" + dTo + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

        $("#holiday_surcharge_charge").html(data);

    });
}

function removerate(targetid) {


    $("#" + targetid).fadeTo("fast", 0.00, function () { //fade
        $(this).slideUp("300", function () { //slide up
            $(this).remove(); //then remove from the DOM
        });
    });


    var countItem = $("#rate_load_result .rate_result_list").length;

    if (countItem == 1) {
        $("#rate_load_head").hide();
    }

}