<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="condition_manage.aspx.cs" Inherits="Hotels2thailand.UI.extranet_condition_manage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>

<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
<link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
<script type="text/javascript" src="../../Scripts/darkman_datepicker.js"></script>
<link type="text/css" href="../../css/extranet/load_tariff.css" rel="stylesheet" />
<script type="text/javascript"  language="javascript">
    $(document).ready(function () {

        //$("#period_Datestart").val("2013-10-20");

        $("#rate_DateStart").val("");

        DatepickerDual("period_Datestart", "period_DateEnd");

        DatepickerDual("rate_DateStart", "rate_DateEnd");

        $("#rate_amount").val("");

        ConditionDetailReset();
        var ConditionNameVal = $("#hd_ConditionName").val();
        if (ConditionNameVal == "1" || ConditionNameVal == "5") {
            $("#condition_period_cancel").hide();
        }
        else {

            $("#condition_period_cancel").show();
        }
        $("#dropPolicyType").change(function () {

            if ($(this).val() == "100") {

                $("#policy_type_custom").animate({ width: "show",
                    borderleft: "show",
                    borderRight: "show",
                    paddingLeft: "show",
                    paddingRight: "show"
                }, "slow");

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
        getPolicyList();
        getCancelList();
        SaveCondition();
        resetCondition();

        var optionId = GetValueQueryString("oid");
        var connameid = $("#hd_ConditionName").val();
        var Dropadult = $("#drop_adult").val();
        var Abf = $("#drop_breakfast").val();

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


        $("#drop_breakfast").change(function () {
            var optionId = GetValueQueryString("oid");
            var connameid = $("#hd_ConditionName").val();
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

        $("#drop_adult").change(function () {
            var optionId = GetValueQueryString("oid");
            var connameid = $("#hd_ConditionName").val();
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
    });

    function removeEle(targetid) {

        $("#" + targetid).fadeTo("fast", 0.00, function () { //fade
            $(this).slideUp("300", function () { //slide up
                $(this).remove(); //then remove from the DOM
            });
        });

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


    function getPolicyList() {
        var conditionId = $("#hd_ConditionID").val();
        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertBefore("#policy_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });


        $.get("../ajax/ajax_condition_policy_list.aspx?cdid=" + conditionId + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

            $("#policy_list").html(data);
            
        });
    }

    function getCancelList() {

        var conditionId = $("#hd_ConditionID").val();
        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertBefore("#period_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });


        $.get("../ajax/ajax_condition_cancel_list.aspx?cdid=" + conditionId + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

            $("#period_list").html(data);


            $(".period_list_item").each(function () {

                var DateStart = $(this).find("table").filter(function (index) { return index == 0 })
                .children()
                .children()
                .children().children().filter(function (index1) { return index1 == 0 }).attr("id");

                var DateEnd = $(this).find("table").filter(function (index) { return index == 0 })
                .children()
                .children()
                .children().children().filter(function (index1) { return index1 == 1 }).attr("id");

                DatePicker(DateStart);
                DatePicker(DateEnd);
                
            });
        });  
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
        ListItem = ListItem + "<input type=\"text\" id=\"day_charge\" value=\"0\" style=\"width:20px;\" class=\"Extra_textbox\" name=\"txt_day_charge" + mainid + "_" + random_cancel + "\"  />";
        ListItem = ListItem + "</td>";
        ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 0px 0px; padding:2px 0px 0px 0px;\" width=\"25%\">";
        ListItem = ListItem + "<input type=\"text\" id=\"percent_charge\" value=\"0\" style=\"width:22px;\" class=\"Extra_textbox\" name=\"txt_per_charge" + mainid + "_" + random_cancel + "\" />";
        ListItem = ListItem + "</td>";
        ListItem = ListItem + "<td width=\"5%\"><img src=\"../../images_extra/del.png\" style=\"cursor:pointer;\" onclick=\"remove('cancel_list_item_" + mainid + "_" + random_cancel + "');return false;\" /></td>";
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
        ListItem = ListItem + "<td align=\"center\" style=\"background-color:#edeff4; padding:2px;\"><input type=\"textbox\" id=\"date_from_" + random + "\" name=\"date_from_" + random + "\" value=\"" + hd_DateFrom + "\" class=\"Extra_textbox\" /></td>";
        ListItem = ListItem + "<td align=\"center\" style=\"background-color:#edeff4; padding:2px;\"><input type=\"textbox\" id=\"date_to_" + random + "\" name=\"date_to_" + random + "\" / value=\"" + hd_Dateto + "\" class=\"Extra_textbox\" /></td>";
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
        ListItem = ListItem + "<input type=\"text\" id=\"day_charge\" value=\"0\" style=\"width:20px;\" name=\"txt_day_charge" + random + "_" + random_cancel + "\" class=\"Extra_textbox\"  />";
        ListItem = ListItem + "</td>";
        ListItem = ListItem + "<td align=\"center\" style=\"margin:2px 0px 0px 0px; padding:2px 0px 0px 0px;\" width=\"25%\">";
        ListItem = ListItem + "<input type=\"text\" id=\"percent_charge\" value=\"0\" style=\"width:22px;\" name=\"txt_per_charge" + random + "_" + random_cancel + "\" class=\"Extra_textbox\" />";
        ListItem = ListItem + "</td>";
        ListItem = ListItem + "<td width=\"5%\"></td>";
        ListItem = ListItem + "</tr>";
        ListItem = ListItem + "</table>";
        ListItem = ListItem + "</div>";

        ListItem = ListItem + "</div>";

        ListItem = ListItem + "<a href=\"\" style=\" width:100%; text-align:center;text-decoration:underline;color:#608000;\" onclick=\"addRule('" + random + "','cancel_list_" + random + "');return false;\" ><img src=\"/images/plus_s.png\" />&nbsp;add rules</a>";
        ListItem = ListItem + "</td>";

        ListItem = ListItem + "<td align=\"center\" style=\"background-color:#edeff4; padding:2px;\"><img src=\"../../images_extra/bin.png\" style=\"cursor:pointer;\"  onclick=\"remove('period_list_item_" + random + "');return false;\" /></td>";
        ListItem = ListItem + "</tr>";
        ListItem = ListItem + "</table>";
        ListItem = ListItem + "</div>";

        var Valid = PeriodValidCheck("period_insert", "period_Datestart", "period_DateEnd", "", "period_list_Checked", "hd_date_from_", "hd_date_to_");

        if (PeriodValidCheck("period_insert", "period_Datestart", "period_DateEnd", "", "period_list_Checked", "hd_date_from_", "hd_date_to_") == 0) {

            
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


                if (PeriodId == "undefined" || PeriodId == null) {

                    PeriodId = $("#period_list .period_list_item").filter(function (index) { return index == 0 }).attr("id");
                    
                    $("#" + PeriodId).before(ListItem);
                } else {
                    $("#" + PeriodId).after(ListItem);
                }


//                $("#period_list_item_" + random).fadeIn('fast', function () {
//                    $('html, body').animate({ scrollTop: $(this).offset().top - 100 }, 500);
                //                });
                $("#period_list_item_" + random).fadeIn('slow');
                $('html, body').animate({ scrollTop: $("#period_list_item_" + random).offset().top - 100 }, 500);

                
                //$("#period_list_item_" + random).animate({ borderColor: "#0e7796", borderWidth: "10px" }, 600);
//                $("#period_list_item_" + random).css("border", "2px solid #67a54b")
//                .delay(1600)
//                .animate({ borderColor: "#3f5d9d", borderWidth: "2px" }, 2000);
                //alert($("#period_list_item_" + random).find("td").length);
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
        itemInsert = itemInsert + "<td width=\"10px\"><img src=\"../../images/greenbt.png\" /><input style=\"display:none;\" checked=\"checked\"  type=\"checkbox\" name=\"policylist\" value=\"" + random + "\" /></td>";
        itemInsert = itemInsert + "<td width=\"100px\"><input type=\"textbox\" readonly=\"readonly\" name=\"policy_type_" + random + "\" value=\"" + TypeTitle + "\" class=\"Extra_textbox\" /></td>";
        itemInsert = itemInsert + "<td width=\"87%\"><input type=\"textbox\" name=\"policy_" + random + "\" value=\"" + Policy + "\" style=\"width:500px\" class=\"Extra_textbox\" /></td>";
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
    function CancelCheckLoadTariff(id, position) {


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

            var DayCharge = $(this).find("input[name^='txt_day_charge']").val();
            var DayPer = $(this).find("input[name^='txt_per_charge']").val();


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

            $("#" + id).css("background-color", "#f7f7f7");
            //$("#" + id).next("div").stop().css("background-color", "#f7f7f7");

            $("#valid_alert_" + id).fadeOut('fast', function () {

                $(this).remove();
            });
        }

        $("input[name^='txt_day_charge']").keyup(function () {

            var result1 = 0;
            $(".cancel_list_item").each(function () {
                var DayCharge = $(this).find("input[name^='txt_day_charge']").val();
                var DayPer = $(this).find("input[name^='txt_per_charge']").val(); //$("#txt_per_charge_" + CheckVal).val();
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

                $("#" + id).css("background-color", "#f7f7f7");
                //$("#" + id).next("div").stop().css("background-color", "#f7f7f7");

                $("#valid_alert_" + id).fadeOut('fast', function () {

                    $(this).remove();
                });
            }

        });


        $("input[name^='txt_per_charge']").keyup(function () {

            var result2 = 0;
            $(".cancel_list_item").each(function () {
                var DayCharge = $(this).find("input[name^='txt_day_charge']").val(); //$("#txt_day_charge_" + CheckVal).val();
                var DayPer = $(this).find("input[name^='txt_per_charge']").val();
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

                $("#" + id).css("background-color", "#f7f7f7");
                //$("#" + id).next("div").stop().css("background-color", "#f7f7f7");

                $("#valid_alert_" + id).fadeOut('fast', function () {

                    $(this).remove();
                });
            }
        });







        return resultCharge;
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
            optionheight = $("#" + id).height();
            //alert(optionheight);
        }
        else {
            optionwidth = ($("#" + id).width() + 10) - $("#" + id).width();
        }


        optionheight = $("#" + id).height() - 11;


        $(".period_list_item").each(function () {
            var cancelListId = $(this).attr("id");

            var MainChecked = $(this).find("input[name='period_list_Checked']:checked").val();
            var resultDay = 0;
            var countindex = 0;


            $(this).find(".cancel_list_item").each(function () {

                var CheckVal = $(this).find("input[name^='cencel_list_Checked_']:checked").val();

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

            $("#" + id).css("background-color", "#f7f7f7");
            $("#" + id).next("div").stop().css("background-color", "#f7f7f7");

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

                    var MainChecked = $(this).find("input[name='period_list_Checked']:checked").val();

                    var countindex = 0;


                    $(this).find(".cancel_list_item").each(function () {

                        var CheckVal = $(this).find("input[name^='cencel_list_Checked_']:checked").val();

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

                    $("#" + id).css("background-color", "#f7f7f7");
                    $("#" + id).next("div").stop().css("background-color", "#f7f7f7");

                    $("#valid_alert_" + id).fadeOut('fast', function () {

                        $(this).remove();
                    });
                }

            });


        });

        return resultDayMain;
    }

    function SaveCondition() {
        var conditionId = $("#hd_ConditionID").val();
        $("<img id=\"img_progress\" class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertBefore("#condition_manage_save").ajaxStart(function () {
            $(this).show();

            $("#btnSave").unbind("click");
        }).ajaxStop(function () {
            $(this).remove();

            $("#btnSave").bind("click", function () {
                SaveCondition();
            });
        });
        $("#btnSave").click(function () {

            if ($("#img_progress").length) {
                $("#img_progress").remove();
            }


            //document.getElementById('btnSave').disabled = 'true';

            if ($("#hd_duplicate").val() == "yes" && DayancelCheckLoadTariff("condition_manage_save", "") == "0" && CancelCheckLoadTariff("condition_manage_save", "") == "0") {
               

                var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

                $.post("../ajax/ajax_condition_edit_save.aspx?cdid=" + conditionId + GetQuerystringProductAndSupplierForBluehouseManage("append"), post, function (data) {
                    console.log(data);
                    if (data == "True") {
                        DarkmanPopUpAlert(450, "Your data is updated to save.");

                    }

                    if (data == "method_invalid") {
                        DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
                    }


                });

            }
            return false;
        });
        
        
    }

    function AddRate() {

        //alert(GetholidaySurchargeHdList());

        var DateFrom = $("#rate_DateStart").val();
        var DateTo = $("#rate_DateEnd").val();

        var hd_DateFrom = $("#hd_rate_DateStart").val();
        var hd_Dateto = $("#hd_rate_DateEnd").val();

        //        var allot = $("#rate_allot").val();

        //        var cutoff = $("#rate_cutoff").val();

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
        //        ListItem = ListItem + "<td width=\"10%\">" + allot + "<input type=\"hidden\" name=\"hd_allot" + random + "\" value=\"" + allot + "\" /></td>";

        ListItem = ListItem + "<td width=\"10%\">" + surAmount + "<input type=\"hidden\" name=\"hd_surAmount" + random + "\" value=\"" + surAmount + "\" /></td>";
        ListItem = ListItem + "<td width=\"20%\"><div class=\"day_of_week_show\" id=\"day_of_week_show_" + random + "\"><p>S</p><p>M</p><p>T</p><p>W</p><p>T</p><p>F</p><p>S</p></div><div style=\"clear:both;\"></div><input type=\"hidden\" name=\"hd_day_checked_sur" + random + "\" value=\"" + DaySurVal + "\" /></td>";
        ListItem = ListItem + "<td width=\"10%\">" + holidaySur + "<input type=\"hidden\" name=\"hd_holiday_Sur" + random + "\" value=\"" + GetholidaySurchargeHdList() + "\" /></td>";

        ListItem = ListItem + "<td width=\"5%\"><img src=\"../../images_extra/bin.png\" onclick=\"removerate('rate_result_list_" + random + "');\" style=\"cursor:pointer;\"  /></td>";
        ListItem = ListItem + "</tr>";
        ListItem = ListItem + "</table>";
        ListItem = ListItem + "</div>";

        if (ValidateOptionMethod("rate_amount", "number0") == true && PeriodValidCheck("rate_insert", "rate_DateStart", "rate_DateEnd", "", "rate_result_checked", "hd_rate_date_form_", "hd_rate_date_To") == 0 && DateCompareValid("rate_insert", "rate_DateStart", "rate_DateEnd", "") >= 0) {
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


    function ConditionDetailReset() {

        var ConditionTitle = $("#txtConditionTitle").val();
        var ConAdult = $("#dropAdult").val();
        var Conchild = $("#dropchild").val();
        var ConAbf = $("#dropabf").val();
        var Conextra = $("#chkExtrabed").is(":checked");

        $("<input type=\"hidden\" id=\"hd_txtConditionTitle\" value=\"" + ConditionTitle + "\" />").insertAfter("body");
        $("<input type=\"hidden\" id=\"hd_dropAdult\" value=\"" + ConAdult + "\" />").insertAfter("body");
        $("<input type=\"hidden\" id=\"hd_dropchild\" value=\"" + Conchild + "\" />").insertAfter("body");
        $("<input type=\"hidden\" id=\"hd_dropabf\" value=\"" + ConAbf + "\" />").insertAfter("body");
        $("<input type=\"hidden\" id=\"hd_chkExtrabed\" value=\"" + Conextra + "\" />").insertAfter("body");
    }

    function resetCondition() {
        var conditionId = $("#hd_ConditionID").val();
        $("#btnreset").click(function () {
            //alert(conditionId);
            getPolicyList(conditionId);
            getCancelList(conditionId);

            
            var hd_contitle = $("#hd_txtConditionTitle").val();
            var hd_adult = $("#hd_dropAdult").val();
            var hd_child = $("#hd_dropchild").val();
            var hd_abf = $("#hd_dropabf").val();

            $("#txtConditionTitle").val(hd_contitle);
            $("#dropAdult").val(hd_adult);
            $("#dropchild").val(hd_child);
            $("#dropabf").val(hd_abf);

            if ($("#hd_chkExtrabed").val() == "true")
                $("#chkExtrabed").attr("checked", "checked");

            if ($("#hd_chkExtrabed").val() == "false")
                $("#chkExtrabed").removeAttr("checked");


        });
        
    
    }

</script>
<style  type="text/css">

.option_title
{
     margin:5px 0px 0px 2px;
     padding:2px 0px 5px 0px;
     font-size:14px;
     font-weight:bold;
     /*color:#68a64c;*/
      /*border-bottom:1px solid #e4e5e9;*/
     /*width:350px;*/
     
       color:#3b5998;
     
 }
 .ConditionTitle
 {
     margin:0px 0px 0px 0px;
     padding:0px 0px 0px 0px;
     font-size:14px;
     font-weight:bold;
      color:#3b5998;
    
      
 }
 .head_condition
 {
     margin:10px 0px 10px 0px;
     padding: 10px;
     background-color:#f2f2f2;
     
     border:1px solid #d6d6d6;
 }
  .head_condition label
 {
     font-weight:bold;
 }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<input type="hidden" id="hd_duplicate" />
<div class="head_condition"><label>Condition: </label><asp:Label ID="lblTitleCondition" runat="server" CssClass="ConditionTitle"></asp:Label><br /><br /><label>Room Type: </label> <asp:Label CssClass="option_title" ID="lblTitle" runat="server"></asp:Label></div>
    
<asp:HiddenField ID="hd_ConditionName" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hd_ConditionID" runat="server" ClientIDMode="Static" />
<div id="load_tariff_condition" style=" width:100%">
    <table width="100%">
        

        <tr>
        <td>

            <div><fieldset ><legend>Adult</legend>
            <asp:DropDownList ID="drop_adult"  EnableTheming="false"  runat="server" ClientIDMode="Static" Width="50px" CssClass="Extra_Drop"></asp:DropDownList>
            </fieldset></div>
            <div><fieldset ><legend>Child</legend>
            <asp:DropDownList ID="drop_child" EnableTheming="false"  runat="server" ClientIDMode="Static"  Width="50px" CssClass="Extra_Drop"></asp:DropDownList>
            </fieldset></div>
            <div style="width:100px; margin-right:20px;"><fieldset ><legend>ABF</legend>
            <asp:DropDownList ID="drop_breakfast" EnableTheming="false"  runat="server" ClientIDMode="Static"  Width="100px" CssClass="Extra_Drop"></asp:DropDownList>
            </fieldset></div>
            <div><fieldset ><legend>Extra Bed</legend>
            <asp:DropDownList ID="drop_extrabed" EnableTheming="false" runat="server" ClientIDMode="Static"  Width="50px" CssClass="Extra_Drop"></asp:DropDownList>
            </fieldset></div>
            
            
        </td>
        
        </tr>
    </table>
        
   <div id="progresscheck"></div>     
    </div>




<div id="condition_policy">

<div id="Div1" class="blogInsert">
<h4><asp:Image ID="Image4" runat="server" ImageUrl="~/images/content.png" /> Policy</h4>

<table>
            <tr>
                <td>
                    <div id="policy_type" style=" float:left;"><fieldset ><legend>Policy Type</legend>
                        <asp:DropDownList ID="dropPolicyType" EnableTheming="false"  runat="server" ClientIDMode="Static" CssClass="Extra_Drop">
                        <asp:ListItem Text="Check-in" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Check-out" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Pets" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Child" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Custom" Value="100"></asp:ListItem>
                        </asp:DropDownList>
                        </fieldset>
                    </div>
                    <div id="policy_type_custom" style="display:none; margin:0px 0px 0px 3px; float:left;"><fieldset ><legend>Custom Type</legend>
                        <asp:TextBox ID="txt_Type_custom" runat="server" EnableTheming="false" ClientIDMode="Static" CssClass="Extra_textbox_yellow" style="width:150px;" ></asp:TextBox>
                    </fieldset>
                    </div>
                </td>
                <td>
                <div><fieldset ><legend>Description</legend>
            <asp:TextBox ID="txt_policy" runat="server" EnableTheming="false" ClientIDMode="Static" CssClass="Extra_textbox" style="width:500px;" ></asp:TextBox>
            </fieldset></div>    
                </td>
                <td>
                <div>
                <input type="button" style="margin-top:17px;" value="Add"  class="Extra_Button_small_blue" onclick="appendPolicy();" />
                <%--<asp:Button ID="AddPolicy"  runat="server" style="margin-top:17px;"  Text="Add"   OnClientClick="appendPolicy();return false;"/>--%>
                </div>
                </td>
            </tr>
        </table>

        <div style="clear:both"></div>
        <br />
        <div class="policy_list" id="policy_list">
            <%--<p class="policy_list_head">Current policy</p>--%>
            
            <%--<asp:Label ID="lblpolicyList" runat="server"></asp:Label>--%>
            
        </div>
</div>
    

        
</div>

<div id="condition_period_cancel">
       
       
        <div id="period_insert" class="blogInsert">
            <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Cancellation</h4>
            <table>
            <tr><td><label>Date Range From</label> </td><td><input type="text" id="period_Datestart" class="Extra_textbox" style="width:120px; padding:2px;"  /></td><td><label> To </label>
            </td><td><input type="text" id="period_DateEnd" class="Extra_textbox" style="width:120px; padding:2px;"  /></td><td><input type="button" id="Button1" value="Add" onclick="AddPeriod();return false;"  class="Extra_Button_small_blue"  /></td></tr>
            
            </table>
        </div>
        <div id="period_list">
            
        </div>
     </div>

     <div id="load_rate">
     
        <%--<p class="extra_title">Add Rate</p>--%>
        <div id="rate_insert" class="blogInsert">
        <h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" /> Extend Rate (Optional)</h4>
            <table>
            <tr>
                <td><label>Date Range From </label></td>
                <td><input type="text" id="rate_DateStart" class="Extra_textbox" style="width:120px;"/></td>
                <td><label> To </label></td>
                <td><input type="text" id="rate_DateEnd" class="Extra_textbox" style="width:120px;"   /></td>
                <td><label>Amount</label></td>
                <td>
                <input type="text" id="rate_amount" class="Extra_textbox_yellow" style="width:80px;" /></td>
                <td>
                
                <asp:CheckBox ID="sur_checked" runat="server" ClientIDMode="Static" onclick="SurCharge_Checked();" />
                </td>
                <td><label>Surcharge includes</label></td>
                <td><input type="button" id="Button2" value="Add" onclick="AddRate();return false;" class="Extra_Button_small_blue" /></td>
            </tr>
      
            <tr>
                <td colspan="10">
                    <div id="surcharge_amount" style="display:none;" >
                        <div id="dayofweek_surcharge">
                        <table>
                            <tr>
                                <td><label> Nomal Day Surcharge</label></td>
                                <td >
                                <div class="day_list" id="day_list">
                                    <p><input type="checkbox" id="Sun" value="0" name="dayofWeek" />Sun</p>
                                    <p><input type="checkbox" id="Mon" value="1" name="dayofWeek"/>Mon</p>
                                    <p><input type="checkbox" id="Tue" value="2" name="dayofWeek"/>Tue</p>
                                    <p><input type="checkbox" id="Wed" value="3" name="dayofWeek" />Wed</p>
                                    <p><input type="checkbox" id="Thu" value="4" name="dayofWeek" />Thu</p>
                                    <p><input type="checkbox" id="Fri" value="5" name="dayofWeek" />Fri</p>
                                    <p><input type="checkbox" id="Sat" value="6" name="dayofWeek" />Sat</p>
                                    </div>
                                </td>

                                <td ><label>Amount</label></td>
                                <td>
                                    <input type="text" id="sur_amount" class="Extra_textbox_yellow" style="width:80px; padding:2px;" />
                                </td>
                            </tr>
                        </table>
                        </div>
                        <div id="holiday_surcharge">
                            <p class="holiday_surcharge_head"> <label> Holiday surcharge</label></p>
                            <div id="holiday_surcharge_charge">
                                
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
            
            </table>
            
        </div>
        <div id="rate_load_result">
            <div id="rate_load_head"  style="display:none;">
                <table width="100%" >
                    <tr bgcolor="#96b4f3" align="center"><td width="15%">Date From</td><td width="15%">Date To</td>
                    <td width="10%">Amount</td>
                    <td width="10%">Surcharge</td><td width="20%"><strong> Nomal Day Surcharge</strong></td><td width="10%">Holiday Surcharge</td><td width="5%">Delete</td>
                    </tr>
                </table>
            </div>
        </div>
     </div>  
     

     <div id="condition_manage_save" style="text-align:center; margin:15px 0px 0px ; padding:5px; border:1px solid #cccccc; background-color:#f2f2f2;">
    <p>*Please check information and rate above before click to save</p>

    <input type="button" id="btnSave" value="Save"  class="Extra_Button_green" />
     <input type="button" id="btnreset" value ="Reset" class="Extra_Button_small_white" />
    
    </div>


</asp:Content>

