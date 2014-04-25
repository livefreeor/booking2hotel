<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="promotion_manage.aspx.cs" Inherits="Hotels2thailand.UI.extranet_promotion_manage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
<link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
<script type="text/javascript" src="../../Scripts/darkman_datepicker.js"></script>
<script type="text/javascript" src="../../scripts/extranet/promotion.js?ver=30_05_2012_11:00"></script>
<link type="text/css" href="../../css/extranet/promotion_extra.css" rel="stylesheet" />


<script type="text/javascript" language="javascript" charset="utf-8">

    $(document).ready(function () {

        var qPromotionId = GetValueQueryString("pro");
       
        DatepickerDual("booking_start", "booking_End");

        DatepickerDual("Stay_start", "Stay_End");


        if (qPromotionId != "") {
            $(".btn_summary_edit_mode").show();

            $("#promotion_menu").hide();
            $("#lblHeadPageTitle").html("Manage Promotion");

            $("#step_body_step3").remove();

            $("#step_body_step5").remove();

            $("#step_body_step5_edit").find("input[name='radioweekDay']").each(function () {

                if ($(this).attr("class") == "checked") {

                    $(this).attr("checked", "checked");
                }
            });
            $("#step_body_step5_edit").find("input[name='radioholiday']").each(function () {

                if ($(this).attr("class") == "checked") {

                    $(this).attr("checked", "checked");
                }
            });

            $("#step_body_step7").remove();
            $("#step_body_step7_edit").find("input[name='radiocancel']").each(function () {

                if ($(this).attr("class") == "checked") {

                    $(this).attr("checked", "checked");
                }
            });


            $("input[name='checkbox_room_check']").each(function () {

                var childCheck = $(this).parent().parent().children("div").find(":checked").length;
                if (childCheck > 0) {
                    $(this).attr("checked", "checked");
                }

            });



            if ($(".benefit_list_item").length > 0) {
                $("#benefit_box").show();
                $("input[name='radiobenefit']").filter(function (index) { return $(this).val() == "0" }).attr("checked", "checked");
            }

            SummaryEdiMode();

        }
        else {


            $(".sitemapcurrent").html("Create New Promotion");

            $("#lblHeadPageTitle").html("Create New Promotion");


            $("#sel_within_day").html(SelDataBind(30));
            $("#sel_advance_day").html(SelDataBind(120));
            $("#sel_min_day").html(SelDataBind(60));
            $("#limit_book").append(SelDataBind(10));
            $("#sel_consec_night").html(SelDataBind(10));


            $("#sel_advance_day").change(function () {
                ConditionCheck();
            });

            $("#sel_min_day").change(function () {
                ConditionCheck();
            });

            $("#free_night_stay").change(function () {
                ConditionCheck();
            });
            $("#sel_consec_night").change(function () {
                ConditionCheck();
            });
        }


        $("input[name='checkbox_room_check']").click(function () {

            if ($(this).is(":checked")) {
                $(this).parent().parent().children("div").find(":checkbox").each(function () {

                    var attrs = $(this).attr("disabled");

                    if (attrs == null) {
                        $(this).attr("checked", "checked");
                    }

                });


            }
            else {

                $(this).parent().parent().children("div").find(":checkbox").removeAttr("checked");
            }

        });

        $("input[name='checkbox_condition_check']").click(function () {

            var count = $(this).parent().parent().parent().find(":checked").length;

            if (count == 0) {
                $(this).parent().parent().parent().parent().parent().parent().children().find(":checkbox").stop().removeAttr("checked")

            }

        });


        if ($("#country_selected option").length == 0) {
            $("#country_selected").append("<option value=\"0\">All country</option>");
        }
    });


    function DefaultTitleBenefit(title) {
        

        if ($(".benefit_list_item").length > 0) {
            var Defulttitle = title + "<label class=\"default_protitle_extend_benefit\" style=\"color:#3b5998\"> And Get Special Benefit</label>";
            $(".DefaultProtitle").html(Defulttitle);
        } else {
            $(".DefaultProtitle").html(title);
        }
    }


    function ConditionCheck() {
//        var qPromotionId = GetValueQueryString("pro");
//        var qProGroupId = GetValueQueryString("pg");

//        var ajaxUrl = "../ajax/ajax_promotion_check.aspx?pg=" + qProGroupId + GetQuerystringProductAndSupplierForBluehouseManage("append");
//        if (qPromotionId != "") {
//            ajaxUrl = "../ajax/ajax_promotion_check.aspx?pg=" + qProGroupId + "&pro=" + qPromotionId + GetQuerystringProductAndSupplierForBluehouseManage("append");
//        }

//            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
//            $.post(ajaxUrl, post, function (data) {
//                //alert(data);
//                if (data != "") {


//                    //checkbox_condition_check
//                    var arrCondition = data.split(',');


//                    var count = 0;
//                    $("input[name='checkbox_room_check']").each(function () {

//                        var RoomCheckId = $(this).val();
//                        var RoomcountCheck = 0;
//                        $("#condition_list" + RoomCheckId).find(":checkbox").each(function () {
//                            for (i = 0; i < arrCondition.length; i++) {

//                                if ($(this).val() == arrCondition[i]) {

//                                    $(this).attr("disabled", "disabled");

//                                    $("#checkCon_" + arrCondition[i]).css("color", "#ccccc1");
//                                    count = count + 1;
//                                    RoomcountCheck = RoomcountCheck + 1;
//                                    $("#checkCon_Alert_" + arrCondition[i]).show();

//                                }

//                            }

//                        });
//                        //alert(RoomcountCheck + "---" + $("#condition_list" + RoomCheckId).find(":checkbox").length);
//                        if (RoomcountCheck == $("#condition_list" + RoomCheckId).find(":checkbox").length) {
//                            $(this).attr("disabled", "disabled");

//                        }


//                    });

//                    if (count > 0) {

//                        $("#condition_alert").show();


//                    }

//                } else {
//                    $("input[name='checkbox_condition_check']").each(function () {

//                        $(this).removeAttr("disabled");


//                        $("#checkCon_" + $(this).val()).css("color", "#333333");
//                        $("#checkCon_Alert_" + $(this).val()).hide();


//                        $("#condition_alert").hide();
//                    });

//                    $("input[name='checkbox_room_check']").each(function () {
//                        $(this).removeAttr("disabled");

//                    });
//                }

//            });
       
    }

    function FinishedStep() {

        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertBefore("#btn_summary").ajaxStart(function () {
            $(this).show();
            $("#btn_Previous_sum").unbind("click");
            $("#btn_Finish_sum").unbind("click");
            $("#btn_Cancel_sum").unbind("click");

        }).ajaxStop(function () {
            $(this).remove();
            
        });
        
        var Progroup = GetValueQueryString("pg");
        var qPromotionId = GetValueQueryString("pro");

        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        if (qPromotionId == "") {

        
            $.post("../ajax/ajax_promotion_save.aspx?pg=" + Progroup + GetQuerystringProductAndSupplierForBluehouseManage("append"), post, function (data) {
                //alert(data);
                if (data == "True") {
                    $("#btn_Previous_sum").attr("disabled", "disabled");
                    $("#btn_Finish_sum").attr("disabled", "disabled");
                    $("#btn_Cancel_sum").attr("disabled", "disabled");
                    DarkmanPopUpAlertFn(450, "This promotion is added completely. Thank you.", "GotoPromotionList();");

                }
                else {
                    DarkmanPopUpAlertFn(450, data);
                }
                if (data == "method_invalid") {
                    DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
                }
            });
        } else {



//            var ajaxUrl = "../ajax/ajax_promotion_check.aspx?pg=" + Progroup + "&pro=" + qPromotionId + GetQuerystringProductAndSupplierForBluehouseManage("append");

//            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
//            $.post(ajaxUrl, post, function (data) {
                //alert(data);
            //                if (data == "") {

        $.post("../ajax/ajax_promotion_save_edit.aspx?pro=" + qPromotionId + "&pg=" + Progroup + GetQuerystringProductAndSupplierForBluehouseManage("append"), post, function (data) {

            
            if (data == "True") {
                DarkmanPopUpAlertFn(450, "This promotion is updated completely. Thank you.", "GotoPromotionList();");

            }
            else {
                DarkmanPopUpAlertFn(450, data);
            }
            if (data == "method_invalid") {
                DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
            }
        });
//                }
//                else {

//                    $("#wiz_main").css("background-color", "#ffebe8");
//                    $("#step_report").after("<p id=\"valid_alert_\" style=\"color:red; display:none; padding:0px 0px 0px 10px; font-size:11px; font-family:tahoma; \">*Minimum night and period of stay has been added in this condition. Please recheck.</p>");
//                    $("#valid_alert_").fadeIn('fast');

//                }
            //});
        
    }

    }



    function SummaryEdiMode() {
        var proGroupId = $("#hd_promotion_group_item").val();

        //alert(proGroupId);

        $("#promotion_group_item_selected").hide();

        var arrStep = getRealStep(proGroupId);
        var arrCount = arrStep.length;
        var lastIndex = arrCount - 1;

        var currentStep = arrStep[lastIndex];
        var currentSequenceStep = arrCount;
        var count = 1;

        GetStepReport(arrStep[0], 1);
        GetStepReport(arrStep[1], 2);
        GetStepReport(arrStep[2], 3);
        GetStepReport(arrStep[3], 4);
        GetStepReport(arrStep[4], 5);
        GetStepReport(arrStep[5], 6);
        GetStepReport(arrStep[6], 7);
        GetStepReport(arrStep[7], 8);
        if (arrCount == 7) {
            SetsequenceCurrentStep(currentSequenceStep + 1);
        } else {
            SetsequenceCurrentStep(currentSequenceStep);
        }
        SetCurrentStep(8);

        $(".foot_step").hide();
        $("#wiz_main").slideDown('fast', function () {
            $('#wiz_step').show();

            $("#pro_title").html(PromotiontitleGenerate());
            //alert(PromotiontitleGenerate());

            $("#hd_promotion_title").val(PromotiontitleGenerate());
            $("#summary_head").fadeIn();
            $("#btn_summary").fadeIn();
        });


    }



    function DayancelCheck(id, position) {

        
        var resultDay = 0;
        var Y_top = $("#" + id).offset().top + 23;
        var X_left = $("#" + id).offset().left;
        //        
        var text = "*You can not add the same no.of days cancel. Please recheck";
        var optionwidth = 0;
        var optionheight = 0;

        if (position == "left") {
            optionwidth = $("#" + id).width() + 10;
            optionheight = $("#" + id).height();
            
        }
        else {
            optionwidth = ($("#" + id).width() + 10) - $("#" + id).width();
        }


        optionheight = $("#" + id).height() - 15;

        var countindex = 0;
        $(".cancel_list_item").each(function () {

            var CheckVal = $(this).find(":checked").val();

            var DayCancel = $("#drop_daycancel_" + CheckVal).val();

            var CountDetect = $(".cancel_list_item").filter(function (index) {
                return $("#drop_daycancel_" + $(this).find(":checked").val()).val() == DayCancel && index != countindex;
            }).length;

            if (CountDetect > 0) {
                resultDay = resultDay + CountDetect;
                return false;
            }
            countindex = countindex + 1;
        });


        
        if (resultDay > 0) {
            if (!$("#valid_alert_" + id).length) {
                $("#" + id).css("background-color", "#ffebe8");
                $("#" + id).next("div").stop().css("background-color", "#ffebe8");
                $("body").after("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                $("#valid_alert_" + id).fadeIn('fast');
            }
        } else {

            $("#" + id).css("background-color", "#f7f7f7");
            $("#" + id).next("div").stop().css("background-color", "#f7f7f7");

            $("#valid_alert_" + id).fadeOut('fast', function () {

                $(this).remove();
            });
        }




        $("select[name^='drop_daycancel_']").change(function () {
            var result1 = 0;
            var result2 = 0;
            var countindex = 0;

            $(".cancel_list_item").each(function () {
                
                var CheckVal = $(this).find(":checked").val();

                var DayCancel = $("#drop_daycancel_" + CheckVal).val();
                

                result2 = $(".cancel_list_item").filter(function (index) {
                    return $("#drop_daycancel_" + $(this).find(":checked").val()).val() == DayCancel && index != countindex;
                }).length;

               
                if (result2 > 0) {
                    //alert("lll");
                    result1 = result1 + result2;
                    return false;
                }
                countindex = countindex + 1;
            });

           
            if (result1 > 0) {
                if (!$("#valid_alert_" + id).length) {
                    $("#" + id).css("background-color", "#ffebe8");
                    $("#" + id).next("div").stop().css("background-color", "#ffebe8");
                    $("body").after("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                    $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                    $("#valid_alert_" + id).fadeIn('fast');
                }
            } else {

                $("#" + id).css("background-color", "#f7f7f7");
                $("#" + id).next("div").stop().css("background-color", "#f7f7f7");

                $("#valid_alert_" + id).fadeOut('fast', function () {

                    $(this).remove();
                });
            }

        });


        return resultDay;
    }

    function CancelCheck(id, position) {
        var resultCharge = 0;
        
        var Y_top = $("#" + id).offset().top + 23;
        var X_left = $("#" + id).offset().left;
  
        var text = "*Please add the number of in No.of Night(s) charge or Percentage Charge.";
        var optionwidth = 0;
        var optionheight = 0;

        if (position == "left") {
            optionwidth = $("#" + id).width() + 10;
            optionheight = $("#" + id).height();
            
        }
        else {
            optionwidth = ($("#" + id).width() + 10) - $("#" + id).width();
        }


        optionheight = $("#" + id).height()-15;


        
        var regExpr = new RegExp("^[0-9][0-9]*$");

        $(".cancel_list_item").each(function () {

            
            var CheckVal = $(this).find(":checked").val();

            var DayCharge = $("#txt_day_charge_" + CheckVal).val();
            var DayPer = $("#txt_per_charge_" + CheckVal).val();


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
                $("#" + id).next("div").stop().css("background-color", "#ffebe8");
                $("body").after("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                $("#valid_alert_" + id).fadeIn('fast');
            }
        } else {

            $("#" + id).css("background-color", "#f7f7f7");
            $("#" + id).next("div").stop().css("background-color", "#f7f7f7");

            $("#valid_alert_" + id).fadeOut('fast', function () {

                $(this).remove();
            });
        }


            $("input[name^='txt_day_charge']").keyup(function () {

                var result1 = 0;
                
                $(".cancel_list_item").each(function () {
                    var DayCharge = $(this).find("input[name^='txt_day_charge']").val();
                    var DayPer = $(this).find("input[name^='txt_per_charge']").val();

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
                        $("#" + id).next("div").stop().css("background-color", "#ffebe8");
                        $("body").after("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                        $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                        $("#valid_alert_" + id).fadeIn('fast');
                    }
                } else {

                    $("#" + id).css("background-color", "#f7f7f7");
                    $("#" + id).next("div").stop().css("background-color", "#f7f7f7");

                    $("#valid_alert_" + id).fadeOut('fast', function () {

                        $(this).remove();
                    });
                }

            });



               $("input[name^='txt_per_charge']").keyup(function () {
                   var result2 = 0;
                $(".cancel_list_item").each(function () {
                    var DayCharge = $(this).find("input[name^='txt_day_charge']").val();
                    var DayPer = $(this).find("input[name^='txt_per_charge']").val();


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
                        $("#" + id).next("div").stop().css("background-color", "#ffebe8");
                        $("body").after("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                        $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                        $("#valid_alert_" + id).fadeIn('fast');
                    }
                } else {

                    $("#" + id).css("background-color", "#f7f7f7");
                    $("#" + id).next("div").stop().css("background-color", "#f7f7f7");

                    $("#valid_alert_" + id).fadeOut('fast', function () {

                        $(this).remove();
                    });
                }
            });


        return resultCharge;
    }

    function BenefitOpen() {

        var ItemVal = GetProItem();
        if (ItemVal != "5") {

            var Defulttitle = $(".DefaultProtitle").html();
            Defulttitle = Defulttitle + "<label class=\"default_protitle_extend_benefit\" style=\"color:#3b5998\"> And Get Special Benefit</label>";
            $(".DefaultProtitle").html(Defulttitle);
        }

        $("#benefit_box").slideDown('fast', function () {
            $(this).fadeIn('fast');

        });
    }

    function BenefitClose() {
        var ItemVal = GetProItem();
        
        if (ItemVal != "5") {
            $(".default_protitle_extend_benefit").fadeOut('fast', function () {
                $(this).remove();
            });
        }

        $("#benefit_list").css("background-color", "#ffffff");
        //$("#" + id).next("div").stop().css("background-color", "#f7f7f7");

        $("#valid_alert_requirebenefit_list").fadeOut('fast', function () {
            $(this).remove();

        });

       
        $("#benefit_box").slideUp('fast', function () {
            $(this).fadeOut('fast');
            $(".benefit_list_item").remove();
        });

       
    }


    function selectoneClick() {
        //
        $("#country_selected option").filter(function (index) { return $(this).val() == "0"; }).remove();
        var OptionSelected = $("#listCountry option").filter(function (index) { return $(this).attr("selected") == "selected"; });
        $("#country_selected").append(OptionSelected);
        $("#country_selected option").each(function () { $(this).removeAttr("selected"); });

        $("#country_selected option").sortElements(function (a, b) {
            return $(a).text() > $(b).text() ? 1 : -1;
        });

        CountryHiddenCheck();
    }

    function selectallClick() {
        $("#country_selected option").filter(function (index) { return $(this).val() == "0"; }).remove();
        var OptionSelected = $("#listCountry option");
        if (OptionSelected.length > 0) {
            $("#listCountry option").each(function () {
                $("#country_selected").append($(this));
            });
            $("#country_selected option").sortElements(function (a, b) {
                return $(a).text() > $(b).text() ? 1 : -1;
            });


        }

        CountryHiddenCheck();
    }

    function removeoneClick() {
        //alert("KK");
        var OptionSelected = $("#country_selected option").filter(function (index) { return $(this).attr("selected") == "selected"; });
        $("#listCountry").append(OptionSelected);
        $("#listCountry option").each(function () { $(this).removeAttr("selected"); });
        $("#listCountry option").sortElements(function (a, b) {
            return $(a).text() > $(b).text() ? 1 : -1;
        });
        if ($("#country_selected option").length == 0) {
            $("#country_selected").append("<option value=\"0\">All country</option>");
        }
        CountryHiddenCheck();
    }
    function removeallClick() {
        //alert("HELLO");

        var OptionSelected = $("#country_selected option");
        if (OptionSelected.length > 0) {
            $("#country_selected option").each(function () {
                $("#listCountry").append($(this));
            });

            $("#listCountry option").sortElements(function (a, b) {
                return $(a).text() > $(b).text() ? 1 : -1;
            });


        }
        
        if ($("#country_selected option").length == 0) {
            $("#country_selected").append("<option value=\"0\">All country</option>");
        }

        CountryHiddenCheck();
    }

    function CountryHiddenCheck() {

        var val = "";
        var total = $("#country_selected option").length;
        var count = 1;
        $("#country_selected option").each(function () {
            if ($(this).val() != "0") {
                if (count == total) {
                    val = val + $(this).val();
                } else {
                    val = val + $(this).val() + ",";
                }
                count = count + 1;
            }
        });

        $("#hd_country_selected").val(val);
        
    }
</script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="promotion_menu">
<asp:HyperLink ID="lnLast" runat="server" Text="Last Minute" NavigateUrl="~/extranet/promotion/promotion_manage.aspx?pg=5"></asp:HyperLink>
<asp:HyperLink ID="lnEarly" runat="server" Text="Advance Booking" NavigateUrl="~/extranet/promotion/promotion_manage.aspx?pg=1"></asp:HyperLink>
<asp:HyperLink ID="lnFree" runat="server" Text="Free Night" NavigateUrl="~/extranet/promotion/promotion_manage.aspx?pg=2"></asp:HyperLink>
<asp:HyperLink ID="lnMini" runat="server" Text="Minimum Night" NavigateUrl="~/extranet/promotion/promotion_manage.aspx?pg=3"></asp:HyperLink>
<asp:HyperLink ID="lnDis" runat="server" Text="Discount" NavigateUrl="~/extranet/promotion/promotion_manage.aspx?pg=4"></asp:HyperLink>
</div>
<div style="clear:both"></div>

<div id="promotion_group_item_selected" >
<asp:Literal ID="lrtProSelect" runat="server"></asp:Literal>
</div>
<div id="wiz_main" style="display:none">

<div id="main_step" style="display:none">
    
    <%--Step 1 -------------Date Booking--------------%>
    <div id="step1" style="display:none">
    <p class="head_step"><span class="step_show">Step 1</span> Period of Booking</p>
    <div class="step_body" id="date_booking_blog">
        <p class="DefaultProtitle"></p>
        <table>
            <tr>
            <td><label>Date Range From</label></td>
            <td><input type="button" runat="server" enableviewstate="false" id="booking_start" style="width:150px; text-align:left;" clientidmode="Static" class="Extra_textbox" /></td>
            <td><label>To</label></td>
            <td><input type="button" runat="server" enableviewstate="false" id="booking_End" style="width:150px; text-align:left;" clientidmode="Static" class="Extra_textbox" />
            </td>
            </tr>
        </table>
        
        </div>
        <div class="btn_step" id="btn_save_booking">
        <table width="100%">
            <tr>
                
                 <td ><input type="button" value="Cancel" style="width:100px" class="Extra_Button_small_white"  onclick="DarkmanPopUpComfirm(350, 'Are you sure you want to cancel?', 'CancelStep();');" /></td>
                 <td  class="btn_summary_edit_mode" align="right" style="display:none;width:150px"><input type="button" class="Extra_Button_small_green" value="Go to summary" onclick="Goto_Summary();" /></td>
                <td style="width:15%" align="right"><input type="button" style="width:100px"  value="Next" class="Extra_Button_small_blue" onclick="NextStepCheckstep1();" /></td>
                
               
            </tr>
        </table>
     </div>
    
    </div>

    <%--End Step 1 ----------------------%>

    <%--Step 2 --------------Date Stay-------------%>
    <div id="step2" class="step_style" style="display:none">
    <p class="head_step"><span class="step_show">Step 2</span> Period of Stay</p>
    <div class="step_body" id="date_stay_blog">
        <p class="DefaultProtitle"></p>
        <table>
            <tr>
            <td><label>Date Range From</label></td>
            <td><input type="button" runat="server" enableviewstate="false" id="Stay_start" style="width:150px; text-align:left;" clientidmode="Static" class="Extra_textbox" /></td>
            <td><label>To</label></td>
            <td><input type="button" runat="server" enableviewstate="false" id="Stay_End" style="width:150px; text-align:left;" clientidmode="Static" class="Extra_textbox" /></td>
            </tr>
        </table>
        
     </div>
        <div class="btn_step" id="btn_save_stay">

            
            <table width="100%">
                <tr>
                <td ><input type="button" class="Extra_Button_small_white" value="Cancel" style="width:100px"  onclick="DarkmanPopUpComfirm(350, 'Are you sure you want to cancel?', 'CancelStep();');" /></td>
                <td align="right" class="btn_summary_edit_mode"  style="display:none;width:150px"><input type="button" class="Extra_Button_small_green" value="Go to summary" onclick="Goto_Summary();" /></td>
                <td style="width:15%" align="right" ><input type="button" style="width:100px" class="Extra_Button_small_blue" value="Previous" onclick="PreviousStepCheckstep2();ConditionCheck();" /></td>
                <td align="right" style="width:15%"><input type="button" style="width:100px"  class="Extra_Button_small_blue" value="Next" onclick="NextStepCheckstep2();ConditionCheck();" /></td>
                
                
                </tr>
            </table>
      </div>
      <%--<p class="foot_step"></p>--%>
    </div>

    <%--End Step 2 ----------------------%>

    <%--Step 3 --------------Promotion Setting-------------%>
    <div id="step3" class="step_style" style="display:none">
    <p class="head_step"><span class="step_show">Step 3</span> Set Promotion </p>
    <asp:Literal ID="step3Edit" runat="server"></asp:Literal>
    <div class="step_body" id="step_body_step3">
    <p class="DefaultProtitle"></p>
     <div id="pro_set_1" class="proset_step3">
        <table>
            <tr><td> <label>Advance &nbsp;</label> </td><td><select id="sel_advance_day" style=" width:50px;" class="Extra_Drop" name="sel_advance_day" ></select></td><td><label>Day(s)</label></td></tr>
         </table>
     </div>
     <div id="pro_set_9" class="proset_step3">
        <table>
            <tr><td> <label>Within&nbsp;&nbsp;&nbsp;&nbsp;</label> </td><td><select id="sel_within_day" style=" width:50px;" class="Extra_Drop" name="sel_within_day" ></select></td><td><label>Day(s)</label></td></tr>
         </table>
     </div>
     <div id="pro_set_2" class="proset_step3">
        <table>
            <tr><td><label>Minimum</label></td><td><select id="sel_min_day" style=" width:50px;" class="Extra_Drop" name="sel_min_day"></select></td><td><label>Night(s)</label></td></tr>
         </table>
     </div>

     <div id="pro_set_3" class="proset_step3">
        <table>
            <tr><td><label>Discount</label></td><td><input type="text"  id="dis_percent" class="Extra_textbox_yellow" name="dis_percent" /></td><td><label>(%)</label></td></tr>
         </table>
     </div>

     <div id="pro_set_4" class="proset_step3">
        <table>
            <tr><td><label>Discount</label></td><td><input type="text"  id="dis_baht" class="Extra_textbox_yellow" name="dis_baht" /></td><td><label>Baht</label></td></tr>
         </table>
     </div>
     <div id="pro_set_6" class="proset_step3">
        <table>
            <tr>
                <td><label> Stay</label></td><td><input type="text" id="free_night_stay" class="Extra_textbox_yellow" style="width:50px" name="free_night_stay" /></td><td><label>Night(s)</label></td>
                <td><label>Pay</label></td><td><input type="text" id="free_night_pay" class="Extra_textbox_yellow" style="width:50px" name="free_night_pay" /></td><td><label>Night(s)</label></td>
            </tr>
         </table>
     </div>

     <div id="pro_set_7" class="proset_step3">
        <table>
            <tr><td><label>Plus </label></td><td><input type="text" id="com_abf" class="Extra_textbox_yellow" name="com_abf" /></td><td><label>Baht on Free Nights</label></td></tr>
         </table>
     </div>

     <div id="pro_set_8" class="proset_step3">
        <table>
            <tr><td><label>Consecutive</label></td><td><select id="sel_consec_night" class="Extra_Drop" name="sel_consec_night"></select></td><td><label>Night(s)</label></td></tr>
         </table>
     </div>
     <div id="pro_set_5" class="proset_step3">
        <table>
            <tr><td>In case of long stay, booking is limited at </td><td><select id="limit_book" class="Extra_Drop" name="limit_book">
            <option value="100">Infinity</option>
            </select></td><td>time(s) in one booking.</td></tr>
         </table>
     </div>
     
    </div>

   
    <div class="btn_step" >
            <table width="100%">
                <tr>
                <td><input type="button" class="Extra_Button_small_white"  value="Cancel" style="width:100px"  onclick="DarkmanPopUpComfirm(350, ' Are you sure you want to cancel?', 'CancelStep();');" /></td>
                <td align="right" class="btn_summary_edit_mode"  style="display:none;width:150px"><input type="button" class="Extra_Button_small_green" value="Go to summary" onclick="Goto_Summary();" /></td>
                <td style="width:15%" align="right"><input type="button" style="width:100px" class="Extra_Button_small_blue" value="Previous" onclick="PreviousStep();ConditionCheck();" /></td>
                <td align="right" style="width:15%"><input type="button" style="width:100px" class="Extra_Button_small_blue" value="Next" onclick="NextStep();ConditionCheck();" /></td>
                
                
                </tr>
            </table>
      </div>
      <%--<p class="foot_step"></p>--%>
    </div>
    <%--End Step 3 ----------------------%>

    <%--Step 4 --------------Special Benefit------------%>
    <div id="step4" class="step_style" style="display:none">
    <p class="head_step"><span class="step_show">Step 4</span> Special Benefit</p>
    <div class="step_body">
    <p class="DefaultProtitle"></p>
    <div id="benefit_option_choice">
    <p><label>Do you want to add any benefits in this promotion?</label></p>
    <table>
    <tr><td><input type="radio" name="radiobenefit" value="0"   onclick="BenefitOpen();" /></td><td>Yes</td></tr>
    <tr><td><input type="radio" name="radiobenefit" value="1" onclick="BenefitClose();" checked="checked"  /></td><td>No</td></tr>
    </table>
    </div>
    <div id="benefit_box" style="display:none;margin:15px 0px 0px 0px;" >
        <table>
            <tr><td><label> Benefit</label></td><td><input type="text" id="benefit_list" class="Extra_textbox" style="width:350px;" /></td><td><input type="button" value="Add" class="Extra_Button_small_green"  onclick="benefitadd();" /></td></tr>
        </table>
        <div id="benefit_list_result" style="margin:10px 0px 0px 0px; ">
        <asp:Literal ID="ltrBenefit" runat="server"></asp:Literal>
        </div>

        
    </div>
    </div>
    <div class="btn_step">
     <table width="100%">
                <tr>
                <td><input type="button" class="Extra_Button_small_white" value="Cancel" style="width:100px"  onclick="DarkmanPopUpComfirm(350, 'Are you sure you want to cancel?', 'CancelStep();');" /></td>
                <td align="right" class="btn_summary_edit_mode"  style="display:none;width:150px"><input type="button" class="Extra_Button_small_green" value="Go to summary" onclick="Goto_Summary();" /></td>
                <td style="width:15%"><input type="button" style="width:100px" class="Extra_Button_small_blue" value="Previous" onclick="PreviousStep();" /></td>
                <td align="right" style="width:15%"><input type="button" style="width:100px" class="Extra_Button_small_blue" value="Next" onclick="NextStep();" /></td>
                
                
                </tr>
            </table>
    </div>
    <%--<p class="foot_step"></p>--%>
    </div>
    <%--End Step 4 ----------------------%>


     <%--Step 5 --------------Time Sensitive Promotion-------------%>
    <div id="step5" class="step_style" style="display:none">
    <p class="head_step"><span class="step_show">Step 5</span> Time Sensitive Promotion</p>
    
    <asp:Literal ID="step5Edit" runat="server"></asp:Literal>
    
    <div class="step_body" id="step_body_step5">
    <p class="DefaultProtitle"></p>
    <p><label> Is this promotion valid to everyday of the week? </label></p>
    <table>
    <tr><td><input type="radio" name="radioweekDay" value="0"   onclick="closedayofweek();" checked="checked" /></td><td>Yes, this promotion is valid to everyday of the week.</td></tr>
    <tr><td><input type="radio" name="radioweekDay" value="1" onclick="opendayofweek();" /></td><td>No, please select valid day </td></tr>
    </table>
    <div id="dayofweek" style="display:none">
        <table><tr>
        <td> <label> What day? </label></td>
        <td><input type="checkbox" name="check_dayofWeek" value="0" checked="checked" title="Sun" />Sun</td>
        <td><input type="checkbox" name="check_dayofWeek" value="1" checked="checked" title="Mon" />Mon</td>
        <td><input type="checkbox" name="check_dayofWeek" value="2" checked="checked" title="Tue" />Tue</td>
        <td><input type="checkbox" name="check_dayofWeek" value="3" checked="checked" title="Wed" />Wed</td>
        <td><input type="checkbox" name="check_dayofWeek" value="4" checked="checked" title="Thu" />Thu</td>
        <td><input type="checkbox" name="check_dayofWeek" value="5" checked="checked" title="Fri" />Fri</td>
        <td><input type="checkbox" name="check_dayofWeek" value="6" checked="checked" title="Sat" />Sat</td>
        </tr></table>
    </div>
    <div id="holiday_applicable">
         <table>
         <tr>
         <td><label> Is public holiday applicable? </label></td>
         <td><input type="radio" name="radioholiday" value="0"  title="Yes." checked="checked" /> Yes</td>
         <td><input type="radio" name="radioholiday" value="1" title="No." /> No</td>
         </tr>
         </table>
    </div>
    
    </div>
    <div class="btn_step">
     <table width="100%">
                <tr>
                <td><input type="button" class="Extra_Button_small_white" value="Cancel" style="width:100px"  onclick="DarkmanPopUpComfirm(350, 'Are you sure you want to cancel?', 'CancelStep();');" /></td>
                <td align="right" class="btn_summary_edit_mode"  style="display:none;width:150px"><input type="button" class="Extra_Button_small_green" value="Go to summary" onclick="Goto_Summary();" /></td>
                <td style="width:15%" align="right"><input type="button"  style="width:100px" class="Extra_Button_small_blue" value="Previous" onclick="PreviousStep();ConditionCheck();" /></td>
                <td align="right" style="width:15%"><input type="button"  style="width:100px" class="Extra_Button_small_blue" value="Next" onclick="NextStep();ConditionCheck();" /></td>
                
                
                </tr>
            </table>
    </div>
    <%--<p class="foot_step"></p>--%>
    </div>
    <%--End Step 5 ----------------------%>

    <%--Step 6 --------------Room Select-------------%>
    <div id="step6" class="step_style" style="display:none">
    <p class="head_step"><span class="step_show">Step 6</span> Select Room & Condition</p>
    <div class="step_body">
        
     <asp:Literal ID="roomSelect" runat="server"></asp:Literal>
    </div>
    <div class="btn_step">
     <table width="100%">
                <tr>
                <td><input type="button" class="Extra_Button_small_white" value="Cancel" style="width:100px"  onclick="DarkmanPopUpComfirm(350, 'Are you sure you want to cancel?', 'CancelStep();');" /></td>
                <td align="right" class="btn_summary_edit_mode"  style="display:none;width:150px"><input type="button" class="Extra_Button_small_green" value="Go to summary" onclick="Goto_Summary();" /></td>
                <td style="width:15%" align="right"><input type="button" style="width:100px" class="Extra_Button_small_blue" value="Previous" onclick="PreviousStep();" /></td>
                <td align="right" style="width:15%"><input type="button" style="width:100px" class="Extra_Button_small_blue"  value="Next" onclick="NextStep();" /></td>
                
                
                </tr>
            </table>
    </div>
    <%--<p class="foot_step"></p>--%>
    </div>
    <%--End Step 6 ----------------------%>

    <%--Step 7 --------------cancelltion Policy-------------%>
    <div id="step7" class="step_style" style="display:none">
    <p class="head_step"><span class="step_show">Step 7</span> Cancellation Policy</p>
    <asp:Literal ID="ltrStep7" runat="server"></asp:Literal>
    <div class="step_body" id="step_body_step7">
        <p class="DefaultProtitle"></p>
        <p><label> Do you want to use standard cancellation policy in this promotion ?</label></p>

        <table width="100%">
        <tr><td><input type="radio" name="radiocancel" value="0"  title="Yes." onclick="closecancel();" checked="checked" /></td><td>Yes.</td></tr>
        <tr><td><input type="radio" name="radiocancel" value="1"  title="No." onclick="opencancel();" /></td><td>No, I want to use new cancellation policy.</td></tr>
        </table>

        <div id="cancelltionAdd_main" style="display:none">

        </div>
    </div>
    <div class="btn_step">
     <table width="100%">
                <tr>
                <td><input type="button" class="Extra_Button_small_white" value="Cancel" style="width:100px"  onclick="DarkmanPopUpComfirm(350, 'Are you sure you want to cancel?', 'CancelStep();');" /></td>
                <td align="right" class="btn_summary_edit_mode"  style="display:none;width:150px"><input type="button" class="Extra_Button_small_green" value="Go to summary" onclick="Goto_Summary();" /></td>
                <td style="width:15%" align="right"><input type="button" style="width:100px"  class="Extra_Button_small_blue" value="Previous" onclick="PreviousStepChekCancel();" /></td>
                <td align="right" style="width:15%"><input type="button" style="width:100px" class="Extra_Button_small_blue" value="Next" onclick="NextStepCheckCancel();" /></td>
               
                
                </tr>
      </table>
    </div>
    <%--<p class="foot_step"></p>--%>
    </div>
    <%--End Step 7 ----------------------%>
    
     <%--Step 8 --------------Promotion Country-------------%>
     <div id="step8" class="step_style" style="display:none" >
     <p class="head_step"><span class="step_show">Step 8</span> Country Target</p>
        <div class="step_body" id="step_body_step8">
        <p><label> Which country does this promotion apply to? </label></p>
        <table  cellpadding="0" cellspacing="0">
        <tr><td valign="bottom"><label style="font-size:11px">Store Front</label></td><td></td><td valign="bottom"><label style="font-size:11px">Selected</label></td></tr>
        <tr>
        <td>
            <asp:ListBox ID="listCountry"  ClientIDMode="Static" runat="server" CssClass="Extra_drop_list">
            
            </asp:ListBox>
           
        </td>
        <td>
            <table width="60px">
   
            <tr><td align="center"><input type="button" value=">>" id="selectall"  title="select all" style=" width:40px;" name="selectall" onclick="selectallClick();return false;" /></td></tr>
            <tr><td align="center"><input type="button" value=">" id="selectone" title="select" style=" width:40px;" name="selectone" onclick="selectoneClick();return false;" /></td></tr>
            <tr><td align="center"><input type="button" value="<" id="removeone" title="remove" style=" width:40px;" name="removeone" onclick="removeoneClick();return false;" /></td></tr>
            <tr><td align="center"><input type="button" value="<<" id="removeall" title="remove all" style=" width:40px;" name="" onclick="removeallClick();return false;" /></td></tr>
            </table>
    
        </td>

        <td>
            <asp:ListBox ID="country_selected"  ClientIDMode="Static" runat="server" CssClass="Extra_drop_list">
            
            </asp:ListBox>
            <asp:HiddenField ID="hd_country_selected" runat="server" ClientIDMode="Static" />
            <%--<select  size="8" id="country_selected" class="Extra_drop_list" >
            
            </select> --%>
        </td>
        </tr>
        </table>
        <p><label> *If no country are selected, this promotion will be applied to all country. </label></p>
        </div>
        <div class="btn_step">
     <table width="100%">
                <tr>
                <td><input type="button" class="Extra_Button_small_white" value="Cancel" style="width:100px"  onclick="DarkmanPopUpComfirm(350, 'Are you sure you want to cancel?', 'CancelStep();');" /></td>
                <td align="right" class="btn_summary_edit_mode"  style="display:none;width:150px"><input type="button" class="Extra_Button_small_green" value="Go to summary" onclick="Goto_Summary();" /></td>
                <td style="width:15%" align="right"><input type="button" style="width:100px"  class="Extra_Button_small_blue" value="Previous" onclick="PreviousStepChekCancel();" /></td>
                <td align="right" style="width:15%"><input type="button" style="width:100px" class="Extra_Button_small_blue" value="Next" onclick="NextStepCheckCancel();" /></td>
               
                
                </tr>
      </table>
    </div>
     </div>
     <%--End Step 8 ----------------------%>
     <p class="foot_step"></p>
</div>

<div id="wiz_step" class="wiz_step">
    <div id="summary_head" style="display:none">
    <p class="summary_head_title" >Promotion Summary</p>
    <p class="summary_head_des" >Please review the details below and click on <span>"Finish"</span> to save your promotion.</p>
    </div>
    <p id="pro_title"></p>
    <div id="step_report">
        
    </div>
    <div style="display:none" id="btn_summary">
         <table width="100%">
                <tr>
                <td align="left"><input type="button" class="Extra_Button_small_white"  style="width:100px" id="btn_Cancel_sum" value="Cancel"  onclick="DarkmanPopUpComfirm(350, 'Are you sure you want to cancel?', 'CancelStep();');" /></td>
                <td style="width:15%" ><input type="button" class="Extra_Button_small_blue"  style="width:100px" id="btn_Previous_sum" value="Previous" onclick="PreviousStepSummary();" /></td>
                <td ><input type="button" class="Extra_Button_small_blue" id="btn_Finish_sum"  style="width:100px" value="Finish" onclick="FinishedStep();" /></td>
                
                </tr>
          </table>
    </div>
</div>

<div style="clear:both"></div>
</div>



<div id="hd_value" style="display:none">
<input type="hidden" name="current_step" id="current_step" />
<input type="hidden" name="sequence_step" id="sequence_step"  />
<input type="hidden"  name="hd_promotion_title" id="hd_promotion_title" />
<asp:HiddenField ID="hd_promotion_group_item" runat="server" ClientIDMode="Static" />
</div>

</asp:Content>

