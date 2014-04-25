$(document).ready(function () {

    $("#period_Datestart").val("");
    DatepickerDual("rate_DateStart", "rate_DateEnd");
    DatepickerDual("period_Datestart", "period_DateEnd");

    $("#rate_amount").val("");

    $("#dropPolicyType").change(function () {

        if ($(this).val() == "100") {

            $("#policy_type_custom").animate({ width: "show",
                borderleft: "show",
                borderRight: "show",
                paddingLeft: "show",
                paddingRight: "show"
            }, "fast");

        } else {
            $("#txt_Type_custom").val("");
            $("#policy_type_custom").animate({ width: "hide",
                borderleft: "hide",
                borderRight: "hide",
                paddingLeft: "hide",
                paddingRight: "hide"
            });
        }
    });

    var optionId = $("#dropRoom").val();
    var connameid = $("#conditionTitle").val();
    var Dropadult = $("#drop_adult").val();
    var Abf = $("#drop_breakfast").val();


    
        if (connameid == "1") {
            $("#load_tariff_period_cancel").hide();
        }
        else {

            $("#load_tariff_period_cancel").show();
            
        }


    $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").appendTo("#progresscheck").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });

    $.get("../ajax/ajax_condition_name_check.aspx?connid=" + connameid + "&oid=" + optionId + "&adu=" + Dropadult + "&abf=" + Abf + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
        //alert(data);
        if (data != "0") {

            ValidateAlert("load_tariff_condition", "*This condition and adult has been used. Please use the different condition.", "");
            $("#hd_duplicate").val("no");

        }
        else {
            ValidateAlertClose("load_tariff_condition");
            $("#hd_duplicate").val("yes");
        }

    });


    $("#drop_adult").change(function () {
        var optionId = $("#dropRoom").val();
        var connameid = $("#conditionTitle").val()
        var Dropadult = $(this).val();
        var Abf = $("#drop_breakfast").val();

        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").appendTo("#progresscheck").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        $.get("../ajax/ajax_condition_name_check.aspx?connid=" + connameid + "&oid=" + optionId + "&adu=" + Dropadult + "&abf=" + Abf + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

            if (data != "0") {

                ValidateAlert("load_tariff_condition", "*This condition and adult has been used. Please use the different condition.", "");
                $("#hd_duplicate").val("no");
            }
            else {
                ValidateAlertClose("load_tariff_condition");
                $("#hd_duplicate").val("yes");
            }

        });


    });
    $("#conditionTitle").change(function () {
        var optionId = $("#dropRoom").val();
        var connameid = $(this).val();
        var Dropadult = $("#drop_adult").val();
        var Abf = $("#drop_breakfast").val();

        
        if (connameid == "1" || connameid == "5" ) {
            $("#load_tariff_period_cancel").hide();
        }
        else {

            $("#load_tariff_period_cancel").show();
        }

        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").appendTo("#progresscheck").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        $.get("../ajax/ajax_condition_name_check.aspx?connid=" + connameid + "&oid=" + optionId + "&adu=" + Dropadult + "&abf=" + Abf + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

            if (data != "0") {

                ValidateAlert("load_tariff_condition", "*This condition and adult has been used. Please use the different condition.", "");
                $("#hd_duplicate").val("no");
            }
            else {
                ValidateAlertClose("load_tariff_condition");
                $("#hd_duplicate").val("yes");
            }

        });


    });

    $("#drop_breakfast").change(function () {
        var connameid = $("#conditionTitle").val();
        var optionId = $("#dropRoom").val();
        var Dropadult = $("#drop_adult").val();
        var Abf = $(this).val();

        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").appendTo("#progresscheck").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        $.get("../ajax/ajax_condition_name_check.aspx?connid=" + connameid + "&oid=" + optionId + "&adu=" + Dropadult + "&abf=" + Abf + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

            if (data != "0") {

                ValidateAlert("load_tariff_condition", "*This condition and adult has been used. Please use the different condition.", "");
                $("#hd_duplicate").val("no");
            }
            else {
                ValidateAlertClose("load_tariff_condition");
                $("#hd_duplicate").val("yes");
            }

        });


    });


    $("#dropRoom").change(function () {
        var connameid = $("#conditionTitle").val();
        var optionId = $(this).val();
        var Dropadult = $("#drop_adult").val();
        var Abf = $("#drop_breakfast").val();

        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").appendTo("#progresscheck").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        $.get("../ajax/ajax_condition_name_check.aspx?connid=" + connameid + "&oid=" + optionId + "&adu=" + Dropadult + "&abf=" + Abf + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

            if (data != "0") {

                ValidateAlert("load_tariff_condition", "*This condition and adult has been used. Please use the different condition.", "");
                $("#hd_duplicate").val("no");
            }
            else {
                ValidateAlertClose("load_tariff_condition");
                $("#hd_duplicate").val("yes");
            }

        });


    });
});


function CheckConditionName(connameid, optionid, Dropadult, Abf) {
    var result = false;
    $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").appendTo("#progresscheck").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });

    $.get("../ajax/ajax_condition_name_check.aspx?connid=" + connameid + "&oid=" + optionid + "&adu=" + Dropadult + "&abf=" + Abf + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

        if (data == "0") {
            result = true;

        }

    });

    return result;
}

function removeEle(targetid) {

    $("#" + targetid).fadeTo("fast", 0.00, function () { //fade
        $(this).slideUp("300", function () { //slide up
            $(this).remove(); //then remove from the DOM
        });
    });

    return false;

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
        var holidaySur = "<label style=\"font-weight:bold;color:#ba0c0c;\">Yes</lable>";
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

//    alert(ValidateOptionMethod("rate_amount", "number0"));
    if (ValidateOptionMethod("rate_amount", "number0") == true && PeriodValidCheck("rate_insert", "rate_DateStart", "rate_DateEnd", "", "rate_result_checked", "hd_rate_date_form_", "hd_rate_date_To") == 0) {
        $("#rate_load_head").show();
        $("#rate_load_result").append(ListItem);


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

        $("#sur_checked").removeAttr("checked")
        $("#day_list :checkbox").removeAttr("checked");

        $("#surcharge_amount").hide();

        $("#rate_allot").val("");
        $("#sur_amount").val("");
        $("#rate_amount").val("");

        $("#holiday_surcharge_charge").html("");
    }

}


function addRule(mainid, targetid) {


    var random_cancel = makeid();

    var ListItem = "<div class=\"cancel_list_item\" id=\"cancel_list_item_" + mainid + "_" + random_cancel + "\" style=\"display:none;\" >";


    ListItem = ListItem + "<input type=\"checkbox\" checked=\"checked\" value=\"" + random_cancel + "\" name=\"cencel_list_Checked_" + mainid + "\" style=\"display:none;\" />"

    ListItem = ListItem + "<table cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center; margin:0px 0px 0px 0px;\">";
    ListItem = ListItem + "<tr style=\"background-color:#ffffff;\" >";
    ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 0px 0px; padding:2px 0px 0px 0px;\" width=\"40%\">";
    ListItem = ListItem + "<select id=\"drop_daycancel\" class=\"Extra_Drop\" name=\"drop_daycancel_" + mainid + "_" + random_cancel + "\" >";
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
    ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 0px 0px; padding:2px 0px 0px 0px;\" width=\"30%\">";
    ListItem = ListItem + "<input type=\"text\" id=\"day_charge\" maxlength=\"2\" value=\"0\" style=\"width:20px;\" class=\"Extra_textbox\" name=\"txt_day_charge" + mainid + "_" + random_cancel + "\"  />";
    ListItem = ListItem + "</td>";
    ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 0px 0px; padding:2px 0px 0px 0px;\" width=\"25%\">";
    ListItem = ListItem + "<input type=\"text\" id=\"percent_charge\" maxlength=\"3\" value=\"0\" style=\"width:22px;\" class=\"Extra_textbox\" name=\"txt_per_charge" + mainid + "_" + random_cancel + "\" />";
    ListItem = ListItem + "</td>";
    ListItem = ListItem + "<td width=\"5%\"><img src=\"../../images_extra/del.png\" style=\"cursor:pointer;\" onclick=\"removeEle('cancel_list_item_" + mainid + "_" + random_cancel + "');return false;\" /></td>";
    ListItem = ListItem + "</tr>";
    ListItem = ListItem + "</table>";
    ListItem = ListItem + "</div>";

    $("#" + targetid).append(ListItem);

    var countItem = $("#" + targetid).find(".cancel_list_item").length;


    $("#" + targetid).find(".cancel_list_item").filter(function (index) {
        return index == (countItem - 1)
    }).fadeIn();
}

function AddPeriod() {

    var DateFrom = $("#period_Datestart").val();
    var DateTo = $("#period_DateEnd").val();

    var hd_DateFrom = $("#hd_period_Datestart").val();
    var hd_Dateto = $("#hd_period_DateEnd").val();
    
    var random = makeid();
    var random_cancel = makeid();

    var ListItem = "<div class=\"period_list_item\" id=\"period_list_item_" + random + "\" style=\"display:none;\">";
    ListItem = ListItem + "<input type=\"checkbox\" checked=\"checked\" value=\"" + random + "\" name=\"period_list_Checked\" style=\"display:none;\" />"
    ListItem = ListItem + "<table style=\"width:100%\" align=\"center\" cellpadding=\"0\" cellspacing=\"3\">";
    ListItem = ListItem + "<tr style=\"background-color:#3b5998;color:#ffffff;font-weight:bold;height:15px;line-height:15px;\">";
    ListItem = ListItem + "<td width=\"20%\"  align=\"center\">Date From</td><td  width=\"20%\" align=\"center\">Date To</td>";
    ListItem = ListItem + "<td width=\"40%\" align=\"center\">Cancellation Rule(s)</td><td width=\"10%\" align=\"center\">Delete</td></tr>";
    ListItem = ListItem + "<tr>";
    ListItem = ListItem + "<td align=\"center\" style=\"background-color:#edeff4; padding:2px;\">" + DateFrom + "<input type=\"hidden\" name=\"hd_date_from_" + random + "\" value=\"" + hd_DateFrom + "\" /></td>";
    ListItem = ListItem + "<td align=\"center\" style=\"background-color:#edeff4; padding:2px;\">" + DateTo + "<input type=\"hidden\" name=\"hd_date_to_" + random + "\" / value=\"" + hd_Dateto + "\" /></td>";
    ListItem = ListItem + "<td align=\"center\">";


    ListItem = ListItem + "<table cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center; margin:0px 0px 0px 0px;\">";
    ListItem = ListItem + "<tr style=\"background-color:#edeff4;color:#333333;font-weight:bold;height:15px;line-height:15px;font-size:11px;\"><td align=\"center\" width=\"40%\" >No. of Day Cancel</td>";
    ListItem = ListItem + "<td align=\"center\" width=\"30%\">Night(s) charge</td><td align=\"center\" width=\"30%\" colspan=\"2\" >Percentage Charge</td></tr>";
    ListItem = ListItem + "</table>";

    ListItem = ListItem + "<div id=\"cancel_list_" + random + "\" >";
    ListItem = ListItem + "<div class=\"cancel_list_item\" id=\"cancel_list_item_" + random + "_" + random_cancel + "\" >";


    ListItem = ListItem + "<input type=\"checkbox\" checked=\"checked\" value=\"" + random_cancel + "\" name=\"cencel_list_Checked_" + random + "\" style=\"display:none;\" />"

    ListItem = ListItem + "<table cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center; margin:0px 0px 0px 0px;\">";
    ListItem = ListItem + "<tr style=\"background-color:#ffffff;\" >";
    ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 0px 0px; padding:2px 0px 0px 0px;\" width=\"40%\">";
    ListItem = ListItem + "<select id=\"drop_daycancel\" class=\"Extra_Drop\" name=\"drop_daycancel_" + random + "_" + random_cancel + "\" >";

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
    ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 0px 0px; padding:2px 0px 0px 0px;\" width=\"30%\">";
    ListItem = ListItem + "<input type=\"text\" id=\"day_charge\" maxlength=\"2\" value=\"0\" style=\"width:20px;\" name=\"txt_day_charge" + random + "_" + random_cancel + "\" class=\"Extra_textbox\"  />";
    ListItem = ListItem + "</td>";
    ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 0px 0px; padding:2px 0px 0px 0px;\" width=\"25%\">";
    ListItem = ListItem + "<input type=\"text\" id=\"percent_charge\" maxlength=\"3\" value=\"0\" style=\"width:22px;\" name=\"txt_per_charge" + random + "_" + random_cancel + "\" class=\"Extra_textbox\" />";
    ListItem = ListItem + "</td>";
    ListItem = ListItem + "<td width=\"5%\"></td>";
    ListItem = ListItem + "</tr>";
    ListItem = ListItem + "</table>";
    ListItem = ListItem + "</div>";

    ListItem = ListItem + "</div>";

    ListItem = ListItem + "<a href=\"\" style=\" width:100%; text-align:center;text-decoration:underline;color:#608000;\" onclick=\"addRule('" + random + "','cancel_list_" + random + "');return false;\" ><img src=\"/images/plus_s.png\" />&nbsp;add rules</a>";
    ListItem = ListItem + "</td>";

    ListItem = ListItem + "<td align=\"center\" style=\"background-color:#edeff4; padding:2px;\"><img src=\"../../images_extra/bin.png\" style=\"cursor:pointer;\"  onclick=\"removeEle('period_list_item_" + random + "');return false;\" /></td>";
    ListItem = ListItem + "</tr>";
    ListItem = ListItem + "</table>";
    ListItem = ListItem + "</div>";

    if (PeriodValidCheck("period_insert", "period_Datestart", "period_DateEnd", "", "period_list_Checked", "hd_date_from_", "hd_date_to_") == 0 ) {

        if ($("#period_list .period_list_item").length == 0) {
            $("#period_list").append(ListItem);
            var countItem = $("#period_list .period_list_item").length;
            $("#period_list .period_list_item").filter(function (index) {
                return index == (countItem - 1)
            }).fadeIn();

            $('html, body').animate({ scrollTop: $("#period_list_item_" + random).offset().top - 100 }, 500);
        } else {

            var hd_DateFrom = $("#hd_period_Datestart").val();
            var hd_Dateto = $("#hd_period_DateEnd").val();

            var PeriodId = $("#period_list .period_list_item").filter(function (index) {
                var Checked = $(this).find(":checked").stop().val();

                var DateStart = $(this).find("input[name='hd_date_from_" + Checked + "']").val();
                var DateEnd = $(this).find("input[name='hd_date_to_" + Checked + "']").val();

                return parseDate(DateEnd) < parseDate(hd_Dateto);

            }).last().attr("id");

            //$("#" + PeriodId).after(ListItem);

            if (PeriodId == "undefined" || PeriodId == null) {

                PeriodId = $("#period_list .period_list_item").filter(function (index) { return index == 0 }).attr("id");

                $("#" + PeriodId).before(ListItem);
            } else {
                $("#" + PeriodId).after(ListItem);
            }

            //$("#period_list_item_" + random).fadeIn();
            $("#period_list_item_" + random).fadeIn('slow');
            $('html, body').animate({ scrollTop: $("#period_list_item_" + random).offset().top - 100 }, 500);
            
        }

        DatePicker("date_from_" + random);
        DatePicker("date_to_" + random);
    }
    

}


function appendPolicy() {

    var TypeTitle = "";

    var countItem = $("#policy_list .policy_list_item").length - 1;

    var random = countItem + 1;

    if ($("#txt_Type_custom").val() == "") {
        TypeTitle = $("#dropPolicyType :selected").text();
    } else {
        TypeTitle = $("#txt_Type_custom").val();
    }

    var Policy = $("#txt_policy").val();

    var itemInsert = "<div class=\"policy_list_item\" id=\"policy_list_item_" + random + "\" style=\"display:none;\" >";
    itemInsert = itemInsert + "<table width=\"100%\"><tr>";
    itemInsert = itemInsert + "<td width=\"5%\" align=\"right\" ><img src=\"../../images/greenbt.png\" /><input style=\"display:none;\" checked=\"checked\"  type=\"checkbox\" name=\"policylist\" value=\"" + random + "\" /></td>";
    itemInsert = itemInsert + "<td width=\"10%\" style=\"font-weight:bold;\" >" + TypeTitle + "&nbsp;:&nbsp;<input type=\"hidden\" name=\"policy_type_" + random + "\" value=\"" + TypeTitle + "\" /></td>";
    itemInsert = itemInsert + "<td width=\"70%\" align=\"left\" >" + Policy + "<input type=\"hidden\" name=\"policy_" + random + "\" value=\"" + Policy + "\" /></td>";
    itemInsert = itemInsert + "<td width=\"5%\"><img src=\"/images_extra/bin.png\" style=\"cursor:pointer;\" onclick=\"removeEle('policy_list_item_" + random + "');\"  /></td>";
    itemInsert = itemInsert + "</tr>";
    itemInsert = itemInsert + "</table></div>";

    var PolicyTypeVal = $("#dropPolicyType").val();
    var countItem = $("#policy_list .policy_list_item").length;

    var countdupli = 0;
    $("#policy_list .policy_list_item").each(function () {
        if ($("#dropPolicyType :selected").text() == $(this).find("input[name^='policy_type_']").val()) {
            countdupli = countdupli + 1
        }
    });

    $("#dropPolicyType").change(function () {
        var countdupli = 0;
        $("#policy_list .policy_list_item").each(function () {
            if ($("#dropPolicyType :selected").text() == $(this).find("input[name^='policy_type_']").val()) {
                countdupli = countdupli + 1
            }
        });

        if (countdupli > 0) {
            ValidateAlert("dropPolicyType", "Already use", "");
        } else {
            ValidateAlertClose("dropPolicyType");
        }
    });
    if (countdupli > 0) {
        ValidateAlert("dropPolicyType", "Already use", "");
    } else {
        ValidateAlertClose("dropPolicyType");
        if (ValidateOptionMethod("txt_policy", "required") == true) {
            if (countItem == 0) {
                $(itemInsert).appendTo("#policy_list");

                var Item = $("#policy_list .policy_list_item").length;

                $("#policy_list .policy_list_item").filter(function (index) {
                    return index == (Item - 1);
                }).fadeIn();
            }
            else {
                if (parseInt(PolicyTypeVal) == 0) {
                    var id = $("#policy_list .policy_list_item").filter(function (index) {
                        return index == 0
                    }).attr("id");
                    $("#" + id).before(itemInsert);
                    $("#policy_list_item_" + random).fadeIn();
                } else {

                    var id = $("#policy_list .policy_list_item").filter(function (index) {
                        return index >= parseInt(PolicyTypeVal) - 1;
                    }).attr("id");


                    if (parseInt(PolicyTypeVal) == 100 || id == null) {
                        $(itemInsert).appendTo("#policy_list");

                        var Item = $("#policy_list .policy_list_item").length;

                        $("#policy_list .policy_list_item").filter(function (index) {
                            return index == (Item - 1);
                        }).fadeIn();
                    } else {
                        var id = $("#policy_list .policy_list_item").filter(function (index) {
                            return index >= parseInt(PolicyTypeVal) - 1;
                        }).attr("id");

                        $("#" + id).after(itemInsert);
                        $("#policy_list_item_" + random).fadeIn();
                    }

                }

            }

            $("#txt_Type_custom").val("");
            $("#txt_policy").val("");
        }
    }
    
}