<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="meal.aspx.cs" Inherits="Hotels2thailand.UI.extranet_meal" %>
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
       <link type="text/css" href="../../css/extranet/extranet_style_core.css" rel="stylesheet" />
    <link type="text/css" href="../../css/extranet/package.css" rel="stylesheet" />
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

        $.get("../ajax/ajax_meal_insertbox.aspx?opcat=58" + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

            $("#extrabed_insertBox").append(data);

            getPeriodCurrentMeal();

            $("#drop_option").change(function () {

                getPeriodCurrentMeal();
            });

            //if ($("#extrabed_date_start").length) {

            //    DatepickerDual("extrabed_date_start", "extrabed_date_end");
            //}


            DatepickerDual("rate_DateStart", "rate_DateEnd");

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

    function AddRate() {

        //document.getElementById('btnload_tariff_save').disabled = 'true';

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
        

        //alert(ValidateOptionMethod("rate_amount", "number0"));

        //alert(PeriodValidCheck("extrabed_insertBox", "rate_DateStart", "rate_DateEnd", "", "rate_result_checked", "hd_rate_date_form_", "hd_rate_date_To"));


        if (ValidateOptionMethod("rate_amount", "number0") == true && PeriodValidCheck("extrabed_insertBox", "rate_DateStart", "rate_DateEnd", "", "rate_result_checked", "hd_rate_date_form_", "hd_rate_date_To") == 0) {
           
           // $("#rate_load_head").show();
            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_meal_insert_new.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {
               
                if (data == "True") {

                    DarkmanPopUpAlert(450, "This meal rate is added completely. Thank you.");
                    getPeriodCurrentMeal();
                }
                else {
                    DarkmanPopUpAlert(450, data);
                }
                if (data == "method_invalid") {
                    DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
                }
            });
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
    function delMeal(PeriodId) {

       
       
               $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#extrabed_list").ajaxStart(function () {
                   $(this).show();
               }).ajaxStop(function () {
                   $(this).remove();
               });

               $.get("../ajax/ajax_meal_del.aspx?prid=" + PeriodId  + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                   
                   if (data == "True") {
                       DarkmanPopUp_Close();

                       DarkmanPopUpAlert(450, "Delete is completed.");
                       getPeriodCurrentMeal();
                   }

                   if (data == "method_invalid") {
                       DarkmanPopUp_Close();
                       DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
                   }

               });
    }

    
    function getPeriodCurrentMeal() {
        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#extrabed_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        var optionId = $("#drop_option").val();
        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

       

        $.post("../ajax/ajax_meal_period_detail.aspx?op=" + optionId + GetQuerystringProductAndSupplierForBluehouseManage("append"), post, function (data) {
           
            $("#rate_load_result").html(data);

        });
    }

    


</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<div id="extrabed_insertBox"  class="blogInsert">

<h4><asp:Image ID="Image4" runat="server" ImageUrl="~/images/content.png" /> Meal Rate Insert Box</h4>


</div>
<div id="rate_load_result">
           
 </div>

<%--<div id="extrabed_list"  style=" margin:15px 0px 0px 0px;">
    
</div>--%>


</asp:Content>

