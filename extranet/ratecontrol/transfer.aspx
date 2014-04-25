<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="transfer.aspx.cs" Inherits="Hotels2thailand.UI.extranet_transfer" %>
<%@ Register  Src="~/Control/DatepickerCalendar_Extra.ascx" TagName="DatePicker_Add_Edit" TagPrefix="DateTime" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
<link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
<script type="text/javascript" src="../../Scripts/darkman_datepicker.js"></script>

<%--<link type="text/css" href="../../css/extranet/extrabeds.css" rel="stylesheet" />
--%>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {

        getExtrbedInserBox();
       
    });

    function getExtrbedInserBox() {

        if ($("#empty_extrabed").length) {
            $("#empty_extrabed").remove();
        }

        $.get("../ajax/ajax_extrabed_insertbox.aspx?opcat=44" + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

            $("#extrabed_insertBox").append(data);
            getPeriodCurrentExtrabed();

            $("#drop_option").change(function () {

                getPeriodCurrentExtrabed();
            });

            if ($("#extrabed_date_start").length) {

                DatepickerDual("extrabed_date_start", "extrabed_date_end");
            }

            $("#btnInsertnewExtra").click(function () {

                var valid = PeriodValidCheck("extrabed_insertBox", "extrabed_date_start", "extrabed_date_end", "", "extrabed_checked_list", "hd_extrabed_date_From_", "hd_extrabed_date_To_");

                if (valid == 0 && ValidateOptionMethod("extrabed_amount_rate", "number") == true) {

                    $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#td_btnSaveRate").ajaxStart(function () {
                        $(this).show();
                    }).ajaxStop(function () {
                        $(this).remove();
                    });

                    var post = $("#extra_bed_insertform").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();


                    $.post("../ajax/ajax_extrabed_save_insertbox.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {

                        if (data == "True") {
                            DarkmanPopUpAlert(450, "Extra bed is added completely. Thank you.");
                            DatepickerDual("extrabed_date_start", "extrabed_date_end");
                            getPeriodCurrentExtrabed();
                        }

                        if (data == "method_invalid") {

                            DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
                        }
                    });
                }





            });
        });
    }

    function delExtra(index ,conditionId) {

        var DateStart = $("input[name='hd_extrabed_date_From_" + index + "']").val();
        var Dateend = $("input[name='hd_extrabed_date_To_" + index + "']").val();
       
               $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#extrabed_list").ajaxStart(function () {
                   $(this).show();
               }).ajaxStop(function () {
                   $(this).remove();
               });

               $.get("../ajax/ajax_extrabed_del.aspx?ds=" + DateStart + "&dn=" + Dateend + "&conid=" + conditionId + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                   
                   if (data == "True") {
                       DarkmanPopUp_Close();

                       DarkmanPopUpAlert(450, "Delete is completed.");
                       getPeriodCurrentExtrabed();
                   }

                   if (data == "method_invalid") {
                       DarkmanPopUp_Close();
                       DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
                   }

               });
    }

    
    function getPeriodCurrentExtrabed() {
        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#extrabed_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });
        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
        $.post("../ajax/ajax_extrabed_list.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {

            $("#extrabed_list").html(data);

        });
    }

    function updateAbf(conditionId) {
        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#extrabed_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_extrabed_update_abf.aspx?conid=" + conditionId + GetQuerystringProductAndSupplierForBluehouseManage("append"), post, function (data) {
           
            if (data == "True") {
                DarkmanPopUpAlert(450, "Extra bed is updated.");
                getPeriodCurrentExtrabed();
            }

            if (data == "method_invalid") {

                DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
            }
        });
    }

    function RateUpdate(index,datestart,dateend,conditionId) {

        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#extrabed_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();


        $.post("../ajax/ajax_extrabed_update_rate.aspx?pri=" + index + "&ds=" + datestart + "&dn=" + dateend + "&conid=" + conditionId + GetQuerystringProductAndSupplierForBluehouseManage("append"), post, function (data) {
           
            if (data == "True") {
                DarkmanPopUpAlert(450, "Extra bed rate is updated.");
                getPeriodCurrentExtrabed();
            }

            if (data == "method_invalid") {
                DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
            }
        });
    }


</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<div id="extrabed_insertBox"  class="blogInsert">

<h4><asp:Image ID="Image4" runat="server" ImageUrl="~/images/content.png" /> Transfer Insert Box</h4>


</div>

<div id="extrabed_list"  style=" margin:15px 0px 0px 0px;">
    
</div>


</asp:Content>

