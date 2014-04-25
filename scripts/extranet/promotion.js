
function GetProItem() {
    var ItemVal = "";
    if (GetValueQueryString("pro") == "") {
        ItemVal = $("input[name='pro_item_select']:checked").val();
    } else {
        ItemVal = $("#hd_promotion_group_item").val();
    }
    return ItemVal;
}

function GoTowizard() {
    $("#main_step").show();
    //var ProTitle = $("input[name='pro_item_select']:selected").parent("td").next("td").html();$("#")
    var ProTitle = $("input[name='pro_item_select']:checked").parent().next().next().stop().html();

    $(".DefaultProtitle").html(ProTitle);
    
    $("#promotion_menu").slideUp('slow');
    $("#promotion_group_item_selected").slideUp('slow', function () {
        $("#wiz_main").slideDown();
    });


    SetCurrentStep(1);
    SetsequenceCurrentStep(1);
    SetStepActivePanel(1, 1);

}


function SetCurrentStep(step) {
    $("#current_step").val(step);
}

function SetsequenceCurrentStep(step) {
    $("#sequence_step").val(step);
}

function getCurrentStep() {
    return parseInt($("#current_step").val());
}

function getsequenceCurrentStep() {
    return parseInt($("#sequence_step").val());
}

function SetStepActivePanel(step, stepShow) {
    
    if (step == 3) {
        $("#step" + step).slideDown('fast', function () {
            ProsettingSttep2_Filtter();
            $(".step_show").html("Step " + stepShow);
        });

    } else if (step == 4) {
        if (GetProItem() == "5" || GetProItem() == "19" ||GetProItem() == "20") {
            $("#benefit_option_choice").hide();
            $("#benefit_box").show();
        }

        $("#step" + step).slideDown(function () {
            $(".step_show").html("Step " + stepShow);
        });
    }else {

        $("#step" + step).slideDown(function () {
            $(".step_show").html("Step " + stepShow);
        });
    }

}

function getRealStep(item) {

    var Stepset;

    switch (parseInt(item)) {
        case 1: case 2: case 3: case 4: case 6: case 7: case 11: case 12: case 13: case 14: case 15: case 16: case 21: case 22: case 23: case 24:
            Stepset = new Array(1, 2, 3, 4, 5, 6, 7,8);

            break;
        case 5: case 8: case 9: case 17: case 18: case 19: case 20:
            Stepset = new Array(1, 2, 3, 4, 5, 6, 7,8);
            break;
    }


    return Stepset;
}

function PromotionSettingType(proItem) {
    //alert(proItem);
    var set = "";
    switch (proItem) {
        case 1:
            set = "1,2,3";
            break;
        case 2:
            set = "1,2,4";
            break;
        case 3:
            set = "1,3";
            break;
        case 4:
            set = "1,4";
            break;
        case 5:
            set = "1";
            break;
        case 6:
            set = "6,5";
            break;
        case 7: case 8:
            set = "6,7,5";
            break;
        case 9:
            set = "6";
            break;
        case 11:
            set = "2,3";
            break;
        case 12:
            set = "2,4";
            break;
        case 13:
            set = "8,3,5";
            break;
        case 14:
            set = "8,4,5";
            break;
        case 15:
            set = "3";
            break;
        case 16:
            set = "4";
            break;
        case 17:
            set = "3";
            break;
        case 18:
            set = "4";
            break;
        case 19:
            set = "2";
            break;
        case 20:
            set = "8,5";
            break;
        case 21:
            set = "9,3";
            break;
        case 22:
            set = "9,4";
            break;
        case 23:
            set = "2,9,3";
            break;
        case 24:
            set = "2,9,4";
            break;
    }

    return set;
}

function remove(targetid) {


    $("#" + targetid).fadeTo("fast", 0.00, function () { //fade
        $(this).slideUp("300", function () { //slide up
            $(this).remove(); //then remove from the DOM
        });
    });

}

function opendayofweek() {
    $("#dayofweek").slideDown('fast', function () {
        $(this).fadeIn('fast');
    });

}

function closedayofweek() {

    $("#dayofweek").slideUp('fast', function () {
        $(this).fadeOut('fast');
    });

}

function CancelStep() {
    
    var qPid = GetValueQueryString("pid");
    var qSuid = GetValueQueryString("supid");

    var qPromotionId = GetValueQueryString("pro");
    var qPromotionGroup = GetValueQueryString("pg");

    if (qPromotionId == "" && qPromotionGroup == "") {
        if (qPid != "" && qSuid != "") {
            location.href = "promotion_manage.aspx?pid=" + qPid + "&supid=" + qSuid;
        } else {
            location.href = "promotion_manage.aspx";
        }
    } else {

        if (qPid != "" && qSuid != "") {

            location.href = "promotion_manage.aspx?pg=" + qPromotionGroup + "&pro=" + qPromotionId + "&pid=" + qPid + "&supid=" + qSuid;
        } else {
            location.href = "promotion_manage.aspx?pg=" + qPromotionGroup + "&pro=" + qPromotionId;
        }

    }

}

function benefitadd() {
    var random = makeid();


    var Benefit = $("#benefit_list").val();

    var itemInsert = "<div class=\"benefit_list_item\" id=\"benefit_list_item_" + random + "\" style=\"display:none;\" >";
    itemInsert = itemInsert + "<table width=\"100%\"><tr>";
    itemInsert = itemInsert + "<td width=\"10px\"><img src=\"../../images/greenbt.png\" /><input style=\"display:none;\" checked=\"checked\"  type=\"checkbox\" name=\"benefitList\" value=\"" + random + "\" /></td>";
    itemInsert = itemInsert + "<td>" + Benefit + "<input type=\"hidden\" name=\"hd_benefit_" + random + "\" value=\"" + Benefit + "\" /></td>";


    itemInsert = itemInsert + "<td width=\"5%\"><a href=\"\" style=\"font-size:11px;font-weight:normal;\" onclick=\"remove('benefit_list_item_" + random + "');return false;\">remove</a></td>";
    itemInsert = itemInsert + "</tr>";
    itemInsert = itemInsert + "</table></div>";

    if (ValidateOptionMethod("benefit_list", "required")) {
        $("#benefit_list_result").append(itemInsert);

        var countItem = $("#benefit_list_result .benefit_list_item").length;

        $("#benefit_list_result .benefit_list_item").filter(function (index) {
            return index == (countItem - 1)
        }).fadeIn();

        $("#benefit_list").val("");
    }

}

function addRule() {

    var random_cancel = makeid();

    var ListItem = "<div class=\"cancel_list_item\" id=\"cancel_list_item_" + random_cancel + "\" style=\"display:none;\" >";


    ListItem = ListItem + "<input type=\"checkbox\" checked=\"checked\" value=\"" + random_cancel + "\" name=\"cencel_list_Checked\" style=\"display:none;\" />"

    ListItem = ListItem + "<table cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center; margin:0px 0px 0px 0px;\">";
    ListItem = ListItem + "<tr style=\"background-color:#ffffff;\" >";
    ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 0px 0px; padding:2px 0px 0px 0px;\" width=\"40%\">";
    ListItem = ListItem + "<select id=\"drop_daycancel_" + random_cancel + "\" class=\"Extra_Drop\" name=\"drop_daycancel_" + random_cancel + "\" >";
    for (i = 0; i <= 30; i++) {
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
    ListItem = ListItem + "<input type=\"text\" maxlength=\"2\" id=\"txt_day_charge_" + random_cancel + "\" value=\"0\" style=\"width:20px;\" class=\"Extra_textbox\" name=\"txt_day_charge_" + random_cancel + "\"  />";
    ListItem = ListItem + "</td>";
    ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 0px 0px; padding:2px 0px 0px 0px;\" width=\"25%\">";
    ListItem = ListItem + "<input type=\"text\" maxlength=\"3\" id=\"txt_per_charge_" + random_cancel + "\" value=\"0\" style=\"width:22px;\" class=\"Extra_textbox\" name=\"txt_per_charge_" + random_cancel + "\" />";
    ListItem = ListItem + "</td>";
    ListItem = ListItem + "<td width=\"5%\"><img src=\"../../images_extra/del.png\" style=\"cursor:pointer;\" onclick=\"remove('cancel_list_item_" + random_cancel + "');return false;\" /></td>";
    ListItem = ListItem + "</tr>";
    ListItem = ListItem + "</table>";
    ListItem = ListItem + "</div>";

    $("#cancelltionAdd").append(ListItem);

    var countItem = $("#cancelltionAdd").find(".cancel_list_item").length;


    $("#cancelltionAdd").find(".cancel_list_item").filter(function (index) {
        return index == (countItem - 1)
    }).fadeIn();
}

function RuleDefault() {

    var random_cancel = makeid();

    ListItem = "<div id=\"cancelltionAdd\">";

    ListItem = ListItem + "<table cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center; margin:0px 0px 0px 0px;\">";
    ListItem = ListItem + "<tr style=\"background-color:#edeff4;color:#333333;font-weight:bold;height:15px;line-height:15px;\"><td align=\"center\" width=\"40%\" >No.of Day (s) Cancel</td>";
    ListItem = ListItem + "<td align=\"center\" width=\"30%\">No. of Night (s) Charge</td><td align=\"center\" width=\"30%\" colspan=\"2\" >Percentage Charge </td></tr>";
    ListItem = ListItem + "</table>";



    ListItem = ListItem + "<div class=\"cancel_list_item\" id=\"cancel_list_item_" + random_cancel + "\" >";


    ListItem = ListItem + "<input type=\"checkbox\" checked=\"checked\" value=\"" + random_cancel + "\" name=\"cencel_list_Checked\" style=\"display:none;\" />"

    ListItem = ListItem + "<table cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center; margin:0px 0px 0px 0px;\">";
    ListItem = ListItem + "<tr style=\"background-color:#ffffff;\" >";
    ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 0px 0px; padding:2px 0px 0px 0px;\" width=\"40%\">";
    ListItem = ListItem + "<select id=\"drop_daycancel_" + random_cancel + "\" class=\"Extra_Drop\" name=\"drop_daycancel_" + random_cancel + "\" >";

    for (i = 0; i <= 60; i++) {
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
    ListItem = ListItem + "<input type=\"text\"  maxlength=\"2\" id=\"txt_day_charge_" + random_cancel + "\" value=\"0\" style=\"width:20px;\" name=\"txt_day_charge_" + random_cancel + "\" class=\"Extra_textbox\"  />";
    ListItem = ListItem + "</td>";
    ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 0px 0px; padding:2px 0px 0px 0px;\" width=\"25%\">";
    ListItem = ListItem + "<input type=\"text\" maxlength=\"3\" id=\"txt_per_charge_" + random_cancel + "\" value=\"0\" style=\"width:22px;\" name=\"txt_per_charge_" + random_cancel + "\" class=\"Extra_textbox\" />";
    ListItem = ListItem + "</td>";
    ListItem = ListItem + "<td width=\"5%\"></td>";
    ListItem = ListItem + "</tr>";
    ListItem = ListItem + "</table>";
    ListItem = ListItem + "</div>";
    ListItem = ListItem + "</div>";
    ListItem = ListItem + "<div class=\"add_rule\">";
    ListItem = ListItem + "<a href=\"\" style=\" width:100%; text-align:center;text-decoration:underline;color:#608000;\" onclick=\"addRule();return false;\" ><img src=\"/images/plus_s.png\" />&nbsp;add rules</a>";
    ListItem = ListItem + "</div>";

    return ListItem;
}


function closecancel() {

    var id = "step_body_step7";
    if (GetValueQueryString("pro") != "") {
        id = "step_body_step7_edit";
    }
    $("#" + id).css("background-color", "#f7f7f7");
    $("#" + id).next("div").stop().css("background-color", "#f7f7f7");

    $("#valid_alert_" + id).fadeOut('fast', function () {

        $(this).remove();

    });

    $("#cancelltionAdd_main").html(RuleDefault());
    $("#cancelltionAdd_main").slideUp('fast', function () {
        $(this).fadeOut('fast');
    });
   
}


function opencancel() {

    var id = "step_body_step7";
    if (GetValueQueryString("pro") != "") {
        id = "step_body_step7_edit";
    }
    $("#" + id).css("background-color", "#f7f7f7");
    $("#" + id).next("div").stop().css("background-color", "#f7f7f7");

    $("#valid_alert_" + id).fadeOut('fast', function () {

        $(this).remove();

    });

    $("#cancelltionAdd_main").html(RuleDefault());
    $("#cancelltionAdd_main").slideDown('fast', function () {
        $(this).fadeIn('fast');

    });

    

}

function SetActiveSummary(step) {

    $(".foot_step").hide();
    
    $("#main_step").hide();
    //$('#wiz_step').fadeOut();


    $("#step" + step).fadeOut(function () {
        $('#wiz_step').fadeIn('fast', function () {

            $("#pro_title").html(PromotiontitleGenerate()).show();
            $("#hd_promotion_title").val(PromotiontitleGenerate());
            $("#summary_head").fadeIn();
            $("#btn_summary").fadeIn();
            
        });

    });

}

function SetDisActiveSummary(step) {
    $(".foot_step").show();
    $("#step" + step).animate({ width: "show",
        borderleft: "show",
        borderRight: "show",
        paddingLeft: "show",
        paddingRight: "show"
    });

    $("#summary_head").hide();
    $("#btn_summary").hide();



}



function NextStepCheckstep1() {
    var result = DateCompareValid("date_booking_blog", "booking_start", "booking_End", "");
    if (result >= 0) {
        NextStep();
    }

}

function NextStepCheckstep2() {
    var result = DateCompareValid("date_stay_blog", "Stay_start", "Stay_End", "");
    if (result >= 0) {
        NextStep();
    }

}

function PreviousStepCheckstep2() {
    var result = DateCompareValid("date_stay_blog", "Stay_start", "Stay_End", "");
    if (result >= 0) {
        PreviousStep();
    }

}

function NextStepCheckCancel() {

    var id = "step_body_step7";
    if (GetValueQueryString("pro") != "") {
        id = "step_body_step7_edit";
    }

    var IsProCancel = $("input[name='radiocancel']:checked").val();
    
    if (IsProCancel != "0") {
        if (CancelCheck(id, "") == "0" && DayancelCheck(id, "") == "0") {
            NextStep();
        }
    } else {
        NextStep();
    }
    
}

function PreviousStepChekCancel() {
    var id = "step_body_step7";
    if (GetValueQueryString("pro") != "") {
        id = "step_body_step7_edit";
    }
    var IsProCancel = $("input[name='radiocancel']:checked").val();
    if (IsProCancel != "0") {
        if (CancelCheck(id, "") == "0" && DayancelCheck(id, "") == "0") {
            PreviousStep();
        }
    } else {
        PreviousStep();
    }



}

function Goto_Summary() {

    var ItemVal = "";
    if (GetValueQueryString("pro") == "") {
        ItemVal = $("input[name='pro_item_select']:checked").val();
    } else {
        ItemVal = $("#hd_promotion_group_item").val();
    }

    if (getCurrentStep() > 0) {

        var currentStep = getCurrentStep();
        var SequenceStep = getsequenceCurrentStep();

        

        var nextStep = SequenceStep + 1;


        var realnextStep = getRealStep(ItemVal)[SequenceStep];


       
        SetsequenceCurrentStep(nextStep);


        SetActiveSummary(currentStep);

        SetCurrentStep(currentStep);

        GetStepReport(currentStep, SequenceStep);
    }

    CheckBalanceDiv();
}
function NextStep() {
    
    var ItemVal = "";
    if (GetValueQueryString("pro") == "") {
        ItemVal = $("input[name='pro_item_select']:checked").val();
    } else {
        ItemVal = $("#hd_promotion_group_item").val();
    }

    if (getCurrentStep() > 0) {

        var currentStep = getCurrentStep();
        var SequenceStep = getsequenceCurrentStep();

        var nextStep = SequenceStep + 1;


        var realnextStep = getRealStep(ItemVal)[SequenceStep];



        SetsequenceCurrentStep(nextStep);
        
        if (realnextStep <= 8) {

            SetCloseActiveStep(currentStep);

            SetCurrentStep(realnextStep);

            SetStepActivePanel(realnextStep, nextStep);
        }
        else {
            SetActiveSummary(currentStep);

            SetCurrentStep(currentStep);
        }

        GetStepReport(currentStep, SequenceStep);
    }

    CheckBalanceDiv();
}


function PreviousStep() {

    //var ItemVal = $("input[name='pro_item_select']:checked").val();

    var ItemVal = "";
    if (GetValueQueryString("pro") == "") {
        ItemVal = $("input[name='pro_item_select']:checked").val();
    } else {
        ItemVal = $("#hd_promotion_group_item").val();
    }
    if (getCurrentStep() > 0) {
        var currentStep = getCurrentStep();

        var SequenceStep = getsequenceCurrentStep();

       

        var nextStep = SequenceStep - 1;


        var realnextStep = getRealStep(ItemVal)[nextStep - 1];


        SetCurrentStep(realnextStep);
        
        SetsequenceCurrentStep(nextStep);
        SetCloseActiveStep(currentStep);

        SetStepActivePanel(realnextStep, nextStep);

        CheckBalanceDiv();
     
    }
}

function gotoStep(step, stepShow) {
    ConditionCheck();
    $(".foot_step").show();
    $("#main_step").show();
    var currentStep = getCurrentStep();

    var SequenceStep = getsequenceCurrentStep();

    SetCloseActiveStep(currentStep);

    SetStepActivePanel(step, stepShow);

    SetCurrentStep(step);
    SetsequenceCurrentStep(stepShow);
    $("#pro_title").hide();
    $("#summary_head").hide();
    $("#btn_summary").hide();

    CheckBalanceDiv();
}

function CheckBalanceDiv() {

    var MainStep = $("#main_step").height();
    var wizStep = $("#wiz_step").height();

}
function PreviousStepSummary() {
    var SequenceStep = getsequenceCurrentStep();
    var currentStep = getCurrentStep();


    SetCurrentStep(currentStep);
    SetsequenceCurrentStep(SequenceStep - 1);
    SetStepActivePanel(currentStep, SequenceStep - 1);

    SetDisActiveSummary(currentStep);

    $("#pro_title").hide();
}

function ProsettingSttep2_Filtter() {
    
    var ItemVal = "";
    if (GetValueQueryString("pro") == "") {
        ItemVal = $("input[name='pro_item_select']:checked").val();
    } else {
        ItemVal = $("#hd_promotion_group_item").val();
    }

    var ProsettingSet = PromotionSettingType(parseInt(ItemVal));



    var arrstep = ProsettingSet.split(',');
    for (i = 0; i < arrstep.length; i++) {
        $("#pro_set_" + arrstep[i]).show();
    }
}

function ProsettingReportSttep2_Filtter() {
    
    var ItemVal = "";
    if (GetValueQueryString("pro") == "") {
        ItemVal = $("input[name='pro_item_select']:checked").val();
    } else {
        ItemVal = $("#hd_promotion_group_item").val();
    }

    //alert(ItemVal);
    var ProsettingSet = PromotionSettingType(parseInt(ItemVal));


    var arrstep = ProsettingSet.split(',');
    for (i = 0; i < arrstep.length; i++) {
        $("#step_report_" + arrstep[i]).show();
    }
}

function GetStepReport(currentstep, sequencestep) {
    
    var result = "";
    if (!$("#report_list_" + sequencestep).length) {
        result = result + "<div class=\"report_list\" id=\"report_list_" + sequencestep + "\">";
    }

    var bullet = "<img src=\"../../images/greenbt.png\" />&nbsp;";
    var qPromotionId = GetValueQueryString("pro");

    switch (currentstep) {
        case 1:
            var booking_start = $("#booking_start").val();
            var booking_end = $("#booking_End").val();
            result = result + "<p class=\"step_title\">";
            result = result + "<a href=\"\" onclick=\"gotoStep('" + currentstep + "','" + sequencestep + "');return false;\" >Step " + sequencestep + " Period of Booking </a></p>";

            result = result + "<p class=\"step_content\">Valid From <strong><lable class=\"date_display\">" + booking_start + "</lable></strong> to <strong><lable class=\"date_display\">" + booking_end + "</lable></strong></p>";
            break;
        case 2:
            var Stay_start = $("#Stay_start").val();
            var Stay_end = $("#Stay_End").val();
            result = result + "<p class=\"step_title\">";
            result = result + "<a href=\"\" onclick=\"gotoStep('" + currentstep + "','" + sequencestep + "');return false;\" >Step " + sequencestep + " Period of Stay </a></p>";
            result = result + "<p class=\"step_content\">Valid From <strong><lable class=\"date_display\">" + Stay_start + "</lable></strong> to <strong><lable class=\"date_display\">" + Stay_end + "</lable></strong></p>";
            break;
        case 3:


            var sel_advance_day = $("#sel_advance_day").val();
            var sel_min_day = $("#sel_min_day").val();
            var dis_percent = $("#dis_percent").val();
            var dis_baht = $("#dis_baht").val();

            var free_night_stay = $("#free_night_stay").val();
            var free_night_pay = $("#free_night_pay").val();


            var com_abf = $("#com_abf").val();
            var sel_consec_night = $("#sel_consec_night").val();

            var limit_book = ""
            if ($("#limit_book").val() == "0") {
                limit_book = $("#limit_book :selected").text();
            } else {
                limit_book = $("#limit_book").val();
            }


            result = result + "<p class=\"step_title\">";
            result = result + "<a href=\"\" onclick=\"gotoStep('" + currentstep + "','" + sequencestep + "');return false;\" >Step " + sequencestep + " Set Promotion </a></p>";
            result = result + "<div class=\"step_content\">";
            result = result + "" + bullet + "&nbsp;"+PromotiontitleGenerate();
            

            if (limit_book < 100) {
                result = result + "<br/><span id=\"step_report_5\" style=\"display:none;\"><img src=\"../../images/greenbt.png\" style=\"margin:0px 0px 0px 5px;\" />&nbsp; In case of long stay, booking is limited <strong>" + limit_book + "</strong> time(s) in one booking.</span>";
            } else {
                result = result + " <br/><span id=\"step_report_5\" style=\"display:none;\"><img src=\"../../images/greenbt.png\" style=\"margin:0px 0px 0px 5px;\" />&nbsp; In case of long stay, booking is <strong>no</strong> limited time(s) in one booking.</span>";
            }
            result = result + "";
            result = result + "</div>";

            break;
        case 4:

            var ItemVal = "";
            if (GetValueQueryString("pro") == "") {
                ItemVal = $("input[name='pro_item_select']:checked").val();
            } else {
                ItemVal = $("#hd_promotion_group_item").val();
            }
            

            result = result + "<p class=\"step_title\">";
            result = result + "<a href=\"\" onclick=\"gotoStep('" + currentstep + "','" + sequencestep + "');return false;\" >Step " + sequencestep + " Special Benefit </a></p>";
            result = result + "<div class=\"step_content\">";

            if (parseInt(ItemVal) == 5) {
                $(".benefit_list_item").each(function () {
                    result = result + "<p class=\"benefit_report_item\"><img src=\"../../images/greenbt.png\" /> " + $(this).find("input[name^='hd_benefit_']").val() + "</p>";

                });
            } else {
                var Isbenefit = $("input[name='radiobenefit']:checked").val();
                var BenefitREsult = "Yes.";
                if (Isbenefit == "1") {
                    BenefitREsult = "No.";
                }
                result = result + "<span style=\"font-size:11px;\">Do you want to add any benefits in this promotion?</span>&nbsp;<strong> " + BenefitREsult + "</strong>";
                $(".benefit_list_item").each(function () {
                    result = result + "<p class=\"benefit_report_item\"><img src=\"../../images/greenbt.png\" /> " + $(this).find("input[name^='hd_benefit_']").val() + "</p>";

                });

            }

            result = result + "</div>";
            break;
        case 5:

            result = result + "<p class=\"step_title\">";
            result = result + "<a href=\"\" onclick=\"gotoStep('" + currentstep + "','" + sequencestep + "');return false;\" >Step " + sequencestep + " Time Sensitive Promotion</a></p>";
            result = result + "<div class=\"step_content\">";


            if ($("input[name='radioweekDay']:checked").val() == "0") {
                result = result + "" + bullet + " <span style=\"font-size:11px;\">Is this promotion valid to everyday of the week?</span>&nbsp;<strong> Yes.</strong>";
                //result = result + "<p class=\"time_sen_report\"> it is valid to everyday of the week.</p>";
                result = result + "";
            }
            else {
                var day = "";
                $("input[name='check_dayofWeek']:checked").each(function () {
                    day = day + $(this).attr("title") + ",";
                });
                result = result + "" + bullet + " <span style=\"font-size:11px;\">Is this promotion valid to everyday of the week?</span>&nbsp;<strong> No.</strong>";
                result = result + "<p class=\"time_sen_report\"> <span>What day? </span> " + day + "</p>";

            }
            var holidayapplicable = $("input[name='radioholiday']:checked").attr("title");
            result = result + "<p class=\"time_sen_report\">" + bullet + " Is public holiday applicable? <span>" + holidayapplicable + "</span></p>"
            result = result + "</div>";
            break;
        case 6:
            result = result + "<p class=\"step_title\">";
            result = result + "<a href=\"\" onclick=\"gotoStep('" + currentstep + "','" + sequencestep + "');return false;\" >Step " + sequencestep + " Select Room & Condition</a></p>";
            result = result + "<div class=\"step_content\">";

            $("input[name='checkbox_room_check']:checked").each(function () {
                var conditionChecked = $(this).parent().parent().children("div").find(":checked");
                if (conditionChecked.length > 0) {
                    result = result + "<div class=\"room_list_report\">";
                    result = result + "<p class=\"room_title_report\"> " + $(this).attr("title") + "</p>";
                    result = result + "<div class=\"condition_list_report\">";
                    conditionChecked.each(function () {
                        result = result + "<p class=\"condition_title_report\"><img src=\"../../images/greenbt.png\" />  &nbsp;" + $(this).attr("title") + "</p>";
                        result = result + "";
                        result = result + "";
                    });

                    result = result + "</div>";
                    result = result + "</div>";
                }

            });
            result = result + "</div>";
            break;
        case 7:
            result = result + "<p class=\"step_title\">";
            result = result + "<a href=\"\" onclick=\"gotoStep('" + currentstep + "','" + sequencestep + "');return false;\" >Step " + sequencestep + " Cancellation Policy</a></p>";
            result = result + "<div class=\"step_content\">";
            //result = result + "<span style=\"text-decoration:underline;font-size:12px;\">Do you want to use standard cancellation policy in this promotion ?</span>";
            if ($("input[name='radiocancel']:checked").val() == "0") {
                result = result + "<p class=\"time_sen_report\">" + bullet + " Use hotel standard cancellation policy</p>";
                result = result + "";
            }
            else {
                result = result + "<p class=\"time_sen_report\">" + bullet + " Use new cancellation policy </p>";
                result = result + "<div class=\"new_cancel_report\">";
                var counts = 1;
                $(".cancel_list_item").each(function () {
                    var dayCancel = $(this).find(":selected").stop().val();

                    var daycharge = $(this).find("input[name^='txt_day_charge_']").stop().val();
                    var percentCharge = $(this).find("input[name^='txt_per_charge_']").stop().val();

                    result = result + "<p class=\"cancel_list_report\">- &nbsp;" + ConvertRuletoStringWording(dayCancel, percentCharge, daycharge) + "</p>";
                   
                    counts = counts + 1;
                });

                result = result + "</div>";
            }

            result = result + "";
            result = result + "";
            result = result + "";
            result = result + "</div>";
            break;
        case 8:
            result = result + "<p class=\"step_title\">";
            result = result + "<a href=\"\" onclick=\"gotoStep('" + currentstep + "','" + sequencestep + "');return false;\" >Step " + sequencestep + " Country Target</a></p>";
            result = result + "<div class=\"step_content\">";
            //country_selected

            if ($("#country_selected option").filter(function (index) { return $(this).val() != "0" }).length == 0) {
                result = result + "<p class=\"time_sen_report\">" + bullet + " This promotion applied to all country</p>";
                result = result + "";
            } else {
                result = result + "<p class=\"time_sen_report\"> <strong>This promotion applied for: </strong></p>";
                result = result + "<div style=\"max-height:150px;overflow:auto; width:100%;padding:5px 0px 0px 10px;\">";
                $("#country_selected option").each(function () {
                    result = result + "<p class=\"time_sen_report\">" + bullet + " " + $(this).html() + "</p>";
                });

                result = result + "</div>";
            }
            result = result + "</div>";
            break;
    }
    if (!$("#report_list_" + sequencestep).length) {
        result = result + "</div>";
    }

    if ($("#report_list_" + sequencestep).length) {

        $("#report_list_" + sequencestep).html(" ");
        $("#report_list_" + sequencestep).html(result);
    } else {

        $("#step_report").append(result);
    }

    ProsettingReportSttep2_Filtter();

    var countItem = $("#step_report .report_list").length;

    $("#step_report .report_list").filter(function (index) {
        return index == (countItem - 1)
    }).fadeIn();


}



function ConvertRuletoStringWording(DayCancel, PercentCh, nightcharge) {


    var result = "";

    var strDaycancel = "";
    var strperch = "";
    var strpernig = "";
    var bytDayCancel = parseInt(DayCancel);
    var bytPercentCh = parseInt(PercentCh);
    var nightCh = parseInt(nightcharge);

    strDaycancel = bytDayCancel;

    if (bytDayCancel == 0) {
        strDaycancel = "No-Show";
    }
    else {

        strDaycancel = "Less than&nbsp;" + bytDayCancel + "&nbsp;Days";
    }


    if (bytPercentCh == 0 && nightCh > 0) {
        result = strDaycancel + ",&nbsp;" + nightCh + "&nbsp;Night is Charged";
    }
    else if (bytPercentCh > 0 && nightCh == 0) {
        result = strDaycancel + ",&nbsp;" + bytPercentCh + "% is Charged";
    }
    else {
        result = "incorrect format";
    }

    return result;
}


function getSequence(bytNum) {
    var strSeq = "st";
    if (bytNum == 2) {
        strSeq = "nd";
    }
    else if (bytNum == 3) {
        strSeq = "rd";
    }
    else if (bytNum > 3) {
        strSeq = "th";
    }

    return strSeq;
}

function dDetectPostback() {

}

function SetCloseActiveStep(step) {
    $("#step" + step).slideUp();
}

function PromotiontitleGenerate() {
    var protitle = "";
    var proItem = "";
    if (GetValueQueryString("pro") == "") {
        proItem = $("input[name='pro_item_select']:checked").val();
    }else {
        proItem = $("#hd_promotion_group_item").val();
    }
    

    switch (parseInt(proItem)) {
        case 1:
            protitle = "Advance Booking  <span class=\"setting_val\">" + $("#sel_advance_day").val() + "</span> Days with Stay Minimum  <span class=\"setting_val\">" + $("#sel_min_day").val() + "</span> Nights, Get  <span class=\"setting_val\">" + $("#dis_percent").val() + "</span>% Discount";
            break;
        case 2:
            protitle = "Advance Booking  <span class=\"setting_val\">" + $("#sel_advance_day").val() + "</span> Days with Stay Minimum  <span class=\"setting_val\">" + $("#sel_min_day").val() + "</span> Nights, Get Discount  <span class=\"setting_val\">" + $("#dis_baht").val() + "</span> Baht";
            break;
        case 3:
            protitle = "Advance Booking  <span class=\"setting_val\">" + $("#sel_advance_day").val() + "</span> Days, Get  <span class=\"setting_val\">" + $("#dis_percent").val() + "</span>% Discount";
            break;
        case 4:
            protitle = "Advance Booking  <span class=\"setting_val\">" + $("#sel_advance_day").val() + "</span> Days, Get Discount  <span class=\"setting_val\">" + $("#dis_baht").val() + "</span> Baht";
            break;
        case 5:
            protitle = "Advance Booking  <span class=\"setting_val\">" + $("#sel_advance_day").val() + "</span> Days, Get Special Benefit";
            break;
        case 6:
            protitle = "Stay  <span class=\"setting_val\">" + $("#free_night_stay").val() + "</span> Nights, Pay  <span class=\"setting_val\">" + $("#free_night_pay").val() + "</span> Nights";
            break;
        case 7:
            protitle = "Stay  <span class=\"setting_val\">" + $("#free_night_stay").val() + "</span> Nights, Pay  <span class=\"setting_val\">" + $("#free_night_pay").val() + "</span> Nights Plus  <span class=\"setting_val\">" + $("#com_abf").val() + "</span> Baht on Free Nights";
            break;

        case 8:
            protitle = "Stay  <span class=\"setting_val\">" + $("#free_night_stay").val() + "</span> Nights, Pay  <span class=\"setting_val\">" + $("#free_night_pay").val() + "</span> Nights Plus  <span class=\"setting_val\">" + $("#com_abf").val() + "</span> Baht on Free Nights Or/And Get Special Benefit";
            break;
        case 9:
            protitle = "Stay  <span class=\"setting_val\">" + $("#free_night_stay").val() + "</span> Nights, Pay  <span class=\"setting_val\">" + $("#free_night_pay").val() + "</span> Nights Or/And Get Special Benefit";

            break;
        case 11:
            protitle = "Stay Minimum  <span class=\"setting_val\">" + $("#sel_min_day").val() + "</span> Nights, Get  <span class=\"setting_val\">" + $("#dis_percent").val() + "</span>% Discount";
            break;
        case 12:
            protitle = "Stay Minimum  <span class=\"setting_val\">" + $("#sel_min_day").val() + "</span> Nights, Get  <span class=\"setting_val\">" + $("#dis_baht").val() + "</span> Baht Discount";
            break;
        case 13:
            protitle = "Stay Every  <span class=\"setting_val\">" + $("#sel_consec_night").val() + "</span> Nights, Get  <span class=\"setting_val\">" + $("#dis_percent").val() + "</span>% Discount";
            break;
        case 14:
            protitle = "Stay Every  <span class=\"setting_val\">" + $("#sel_consec_night").val() + "</span> Nights, Get   <span class=\"setting_val\">" + $("#dis_baht").val() + "</span> Baht Discount";
            break;
        case 15:
            protitle = "Hot Deal, Get Discount  <span class=\"setting_val\">" + $("#dis_percent").val() + "</span>%";
            break;
        case 16:
            protitle = "Hot Deal, Get Discount  <span class=\"setting_val\">" + $("#dis_baht").val() + "</span> Baht";
            break;
        case 17:
            protitle = "Hot Deal, Get Discount  <span class=\"setting_val\">" + $("#dis_percent").val() + "</span>%  with Special Benefit";
            break;
        case 18:
            protitle = "Hot Deal, Get Discount  <span class=\"setting_val\">" + $("#dis_baht").val() + "</span> Baht  with Special Benefit";
            break;

        case 19:
            protitle = "Stay Minimum  <span class=\"setting_val\">" + $("#sel_min_day").val() + "</span> Nights, Get Special Benefit";
            break;
        case 20:
            protitle = "Stay Every  <span class=\"setting_val\">" + $("#sel_consec_night").val() + "</span> Nights, Get Special Benefit";
            break;
        case 21:
            protitle = "Book within  <span class=\"setting_val\">" + $("#sel_within_day").val() + "</span> Days before check in date, Get  <span class=\"setting_val\">" + $("#dis_percent").val() + "</span>% Discount";
            break;
        case 22:
            protitle = "Book within  <span class=\"setting_val\">" + $("#sel_within_day").val() + "</span> Days before check in date, Get Discount  <span class=\"setting_val\">" + $("#dis_baht").val() + "</span> Baht";
            break;
        case 23:
            protitle = "Book within  <span class=\"setting_val\">" + $("#sel_within_day").val() + "</span> Days before check in date with Stay Minimum <span class=\"setting_val\">" + $("#sel_min_day").val() + "</span> Nights, Get  <span class=\"setting_val\">" + $("#dis_percent").val() + "</span>% Discount";
            break;
        case 24:
            protitle = "Book within  <span class=\"setting_val\">" + $("#sel_within_day").val() + "</span> Days before check in date with Stay Minimum <span class=\"setting_val\">" + $("#sel_min_day").val() + "</span> Nights, Get Discount  <span class=\"setting_val\">" + $("#dis_baht").val() + "</span> Baht";
            break;
    }

    var benefitCount = $(".benefit_list_item").length;
    if (parseInt(proItem) != 19 && parseInt(proItem) != 20 && parseInt(proItem) != 5 && benefitCount > 0) {
        protitle = protitle + " And Get Special Benefit";
    }
    //alert(benefitCount + "--" + proItem);
    return protitle;
}

function GotoPromotionList() {
    var qProductId = GetValueQueryString("pid");
    var qSupplierId = GetValueQueryString("supid");

    if (qProductId != "" && qSupplierId != "") {
        window.location.href = "promotion.aspx?pid=" + qProductId + "&supid=" + qSupplierId;

    } else {
        window.location.href = "promotion.aspx";
    }
}



