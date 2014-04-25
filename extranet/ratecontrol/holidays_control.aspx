<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="holidays_control.aspx.cs" Inherits="Hotels2thailand.UI.extranet_holidays_control" %>

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
        var today = new Date();

        //alert(today.getFullYear());
        $("#yeardrop option").each(function () {
            if (today.getFullYear() == $(this).val()) {
                $(this).attr("selected", "selected");
            }
            //var todays = new Date();
            //alert(today.getFullYear());
            //alert(todays.getFullYear);
            //alert($(this).val());
        });

        $("#single").attr("checked","checked");
        $("#date_insert_holiday").val("");
        $("#date_insert_holiday_end").val("");

        DatepickerDual("date_insert_holiday", "date_insert_holiday_end");
       

        GetHolidayList(today.getFullYear());

        var DateTypeVal = $("input[name='date_type_insert']:checked").val();

        $("input[name='date_type_insert']").click(function () {

            if ($(this).val() == "0") {
                $("#date_from").html("Date From");
                $("#date_to").fadeIn('slow');

            } else {


                $("#date_to").fadeOut('fast', function () {
                    $("#date_from").html("Holiday Date");

                });
            }

        });



    });


    

    function SerchHolidays() {

        var yearVal = $("#yeardrop").val();

        GetHolidayList(yearVal);
    }

    function GetHolidayList(year) {

        if (!$("#ajax_progress").length) {

            $("<img id=\"ajax_progress\" class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertBefore("#holiday_list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });
        }


        $.get("../ajax/ajax_holiday_list.aspx?y=" + year + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

            $("#holiday_list").html(data);

            $("#holidayCurrentList table tr:odd").css("background-color", "#eceff5");

            $("#holidayCurrentList input[name^='txtDateStart_']").each(function () {

                DatePicker($(this).attr("id"));

            });
        
            
        });
    }

    function insertHoliday() {

        var Holidays = $("#hd_date_insert_holiday").val();
        

        var valid = 0;
        if ($("input[name='date_type_insert']:checked").val() == "1") {
            valid = PeriodValidCheck_Single("holiday_insert_box", "date_insert_holiday", "", "checked_holiday_list", "hd_txtDateStart_");
        }
        else {
            valid = PeriodValidCheck_Single("holiday_insert_box", "date_insert_holiday", "", "checked_holiday_list", "hd_txtDateStart_") +
            PeriodValidCheck_Single("holiday_insert_box", "date_insert_holiday_end", "", "checked_holiday_list", "hd_txtDateStart_"); 
        }



        if (valid == 0 && ValidateOptionMethod("holiday_title", "required")) {
            if (!$("#ajax_progress").length) {

                $("<img id=\"ajax_progress\" class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertBefore("#holiday_list").ajaxStart(function () {
                    $(this).show();
                }).ajaxStop(function () {
                    $(this).remove();
                });
            }
        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_holiday_save.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {
            //alert(data);
            if (data == "True") {
                DarkmanPopUpAlert(450, "Insert holiday completed");

               // var yearVal = 

                $("#holiday_title").val("");


                var YearInsert = $("#hd_date_insert_holiday").val().split('-')[0];

                //alert(YearInsert);
                //alert(YearInsert);
                $("#yeardrop").val(YearInsert);

                //alert(YearInsert);


               // DatepickerDual("date_insert_holiday", "date_insert_holiday_end");

                GetHolidayList(YearInsert);
            }

            if (data == "method_invalid") {
                DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
            }

        });

                $("#holiday_title").val("");
        }
        


    }

    function SupplementUpdate() {

        if (!$("#ajax_progress").length) {
            $("<img id=\"ajax_progress\" class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertBefore("#holiday_list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });
        }
       
        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_holidays_surcharge_update.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {

            if (data == "True") {
                DarkmanPopUpAlert(450, "Update Completed");
                SerchHolidays();
            }

            if (data == "method_invalid") {
                DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
            }


        });
    }



    function SupplementUpdatestatus() {

        if (!$("#ajax_progress").length) {
            $("<img id=\"ajax_progress\" class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertBefore("#holiday_list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });
        }


        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();


        $.post("../ajax/ajax_holidays_surcharge_update_status.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {

            if (data == "True") {
                DarkmanPopUpAlert(450, "Delete Completed");
                SerchHolidays();
            }

            if (data == "method_invalid") {
                DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
            }


        });

    }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style=" position:absolute; top:170px; left:930px;">
        <table>
            <tr>
 
                <td><label> Select Year</label></td>
                <td>
                   <select id="yeardrop" class="Extra_Drop" >
                    <option value="2011">2011</option>
                    <option value="2012">2012</option>
                    <option value="2013">2013</option>
                       <option value="2014">2014</option>
                       <option value="2015">2015</option>
                       <option value="2016">2016</option>
                   </select>
                </td>
                <td><input type="button" onclick="SerchHolidays();return false;" value="Search" class="Extra_Button_small_green" /></td>
            </tr>
    </table>
</div>
    <div id="holiday_insert_box" class="blogInsert">
    <h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" /> Holiday Insert Box</h4>
        <table>
            <tr>
                

                <td><label> Holiday Title </label></td>
            <td><input type="text" class="Extra_textbox" style="width:200px;" id="holiday_title" name="holiday_title" /></td>
            <td>
            <table><tr><td><input type="radio" checked="checked" id="single" name="date_type_insert" value="1" /><label style="font-size:11px;">Date</label></td></tr>
            <tr><td><input type="radio" id="dual" name="date_type_insert" value="0" /><label style="font-size:11px;">Date Range</label></td></tr></table></td>
            <td>

            <span><label id="date_from">Holiday Date</label>&nbsp;<input type="text" class="Extra_textbox" id="date_insert_holiday" style="width:100px;" name="date_insert_holiday" /></span>

            <span id="date_to" style="display:none;"><label>Date End</label>&nbsp;<input type="text" class="Extra_textbox" id="date_insert_holiday_end" style="width:100px;" name="date_insert_holiday_end" /></span>
            </td>
            
            <td><input type="button" value="Save" onclick="insertHoliday();return false;" class="Extra_Button_small_blue"  /></td>
            </tr>
        </table>
    </div>
    <div id="holiday_list" style="margin:15px 0px 0px 0px;" >
        
    </div>
 
</asp:Content>

