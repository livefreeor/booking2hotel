<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="rate_control.aspx.cs" Inherits="Hotels2thailand.UI.extranet_rate_control" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
<link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
<script type="text/javascript" src="../../Scripts/darkman_datepicker.js"></script>

<script type="text/javascript"  language="javascript">
    $(document).ready(function () {

        GetRateSearchBox();

    });

    function GetRateSearchBox() {

        if ($("#img_progress").length) {
            $("#img_progress").remove();
        }

        $("<img id=\"img_progress\" class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertBefore("#rate_control_searchbox").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });
        $.get("../ajax/ajax_rate_control_searchbox.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), function (data) {
           
            $("#rate_control_searchbox").append(data);

            var OptionVal = $("#rate_control_room_type").val();

            Getcondition(OptionVal);
            DatepickerDual("rate_control_date_start", "rate_control_date_end");

            $("#rate_control_room_type").change(function () {
                Getcondition($(this).val());
                $("#rate_control_list").html("");
            });
        });
    }

    function GetRateSearch() {

        if ($("#img_progress").length) {
            $("#img_progress").remove();
        }

        $("<img id=\"img_progress\" class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertBefore("#rate_control_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_rate_control_list.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {
         
            $("#rate_control_list").html(data);
        });
    }

    function Getcondition(optionId) {

        $("<img class=\"img_progress\" src=\"../../images_extra/preloader_blue_gray.gif\" alt=\"Progress\" />").insertBefore("#rate_control_condition").ajaxStart(function ()       {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });
        $.get("../ajax/ajax_rate_control_condition.aspx?oid=" + optionId + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

            if (data != "False") {
                $("#rate_control_condition").html(data);
                $("#hd_condition_check").val("True");
                $("#rate_control_condition").change(function () {
                    $("#rate_control_list").html("");
                });

                ValidateAlertClose("rate_control_searchbox");
            }
            else {
                var result = "";
                result = result + "<p style=\" margin:0px; padding:0px;color:#5c75a9;font-weight:bold\">";
                result = result + "<span>Sorry!</span> No condition for this room type.";
                result = result + "</p>";

                $("#rate_control_condition").html(result);
                $("#hd_condition_check").val("False");
            }

        });
    }

    function ratecontrolSearch() {


        if ($("#hd_condition_check").val() == "True") {
            $("input[name^='rate_control_price_']").each(function () {
                var id = $(this).attr("id");



                if ($("#valid_alert_require" + id).length) {
                    $("#valid_alert_require" + id).fadeOut('fast', function () {

                        $(this).remove();
                    });
                }

            });

            if ($("#valid_alert_requirecondition_manage_save").length) {
                $("#valid_alert_requirecondition_manage_save").fadeOut('fast', function () {

                    $(this).remove();
                });
            }
            
            $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertBefore("#rate_control_list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            var post = $("#rate_control_form_search").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_rate_control_list.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {
               
                $("#rate_control_list").html(data);


            });

            ValidateAlertClose("rate_control_searchbox");
        } else {
            ValidateAlert("rate_control_searchbox", "Cannot search, please insert condition for this room type before.", "");
        }

       
    }

    function Rateautofill() {
        var roomallot = $("#rate_auto_fill").val();

        $("#rate_control_list input[name^='rate_control_price_']").val(roomallot);
    }


    function CheckRateZero() {
        var result = 0;
        var mainid = "condition_manage_save";

        $("input[name^='rate_control_price_']").each(function () {
            var id = $(this).attr("id");


            if (ValidateOptionMethod(id, "number0comma") == false) {
                result = result + 1;
            }
        });

        var Y_top = $("#" + mainid).offset().top + 45;
        var X_left = $("#" + mainid).offset().left + 10;
        var txtAlert = "*Requires and numeric (not add rate \"0\"(zero)) information only";
        if (result > 0) {
            if (!$("#valid_alert_require" + mainid).length) {
                $("#" + mainid).css("background-color", "#ffebe8");
                $("body").after("<label id=\"valid_alert_require" + mainid + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + txtAlert + "</label>");
                $("#valid_alert_require" + mainid).css({ "top": Y_top + "px", "left": X_left + "px" });
                $("#valid_alert_require" + mainid).fadeIn('fast');
            }
            
        } else {
            $("#" + mainid).css("background-color", "#f2f2f2");
            $("#valid_alert_require" + mainid).fadeOut('fast', function () {

                $(this).remove();
            });
        }


        $("input[name^='rate_control_price_']").keyup(function () {
            var id = $(this).attr("id");

            var Y_top = $("#" + mainid).offset().top + 45;
            var X_left = $("#" + mainid).offset().left + 10;
            var resultkeyup = 0;

            if (ValidateOptionMethod(id, "number0comma") == false) {
                resultkeyup = resultkeyup + 1;
            }

            if (resultkeyup > 0) {

                if (!$("#valid_alert_require" + mainid).length) {
                    $("#" + mainid).css("background-color", "#ffebe8");
                    $("body").after("<label id=\"valid_alert_require" + mainid + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + txtAlert + "</label>");
                    $("#valid_alert_require" + mainid).css({ "top": Y_top + "px", "left": X_left + "px" });
                    $("#valid_alert_require" + mainid).fadeIn('fast');
                }

            } else {

                $("#" + mainid).css("background-color", "#f2f2f2");
                $("#valid_alert_require" + mainid).fadeOut('fast', function () {

                    $(this).remove();
                });
            }

            resultkeyup = 0;
        });
        return result;
    }
    function SaveRateEdit() {
        //alert("HELLO");
        //alert(CheckRateZero())
        if (CheckRateZero() == 0) {

            if ($("#img_progress").length) {
                $("#img_progress").remove();
            }


            $("<img id=\"img_progress\" class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertBefore("#rate_control_list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });


            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_rate_control_save_edit.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {
                console.log(data);
                if (data == "True") {
                    
                    DarkmanPopUpAlert(450, "Edit new rate is completed");
                    ratecontrolSearch();
                }
                if (data == "method_invalid") {
                    DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
                }

            });
        }
    }

    function resetAutofill() {
        ratecontrolSearch();
    }

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="rate_control_searchbox"  class="blogInsert" >
    <h4><asp:Image ID="imgContent" runat="server" ImageUrl="~/images/content.png" /> Selling Rate Control Search Box</h4>
</div>
<input type="hidden" value="True" id="hd_condition_check" />
<div id="rate_control_list" style="margin:15px 0px 0px 0px;">

</div>
</asp:Content>

