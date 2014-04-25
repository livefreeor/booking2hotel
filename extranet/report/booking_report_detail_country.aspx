<%@ Page Title="Hotels2thailand:Booking Acknowledge" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="booking_report_detail_country.aspx.cs" Inherits="Hotels2thailand.UI.extranet_booking_report_detail_country" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
<script type="text/javascript" src="http://www.google.com/jsapi"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
<link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
<script type="text/javascript" src="../../Scripts/darkman_datepicker.js"></script>
<link type="text/css" href="/css/extranet/report.css" rel="stylesheet" />
<script type="text/javascript" src="../../scripts/Highcharts/highcharts.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/booking_report_chart_render.js"></script>

<script type="text/javascript" language="javascript">
    $(document).ready(function () {

        var dDate = new Date();
        var Month = dDate.getMonth();
        var Year = dDate.getFullYear();


        var lastDayOfMonth = LastDayOfMonth(Year, Month);

        var dDateStart = parseDate(GetValueQueryString("pdr").split('/')[0].trim());
        var dDateEnd = parseDate(GetValueQueryString("pdr").split('/')[1].trim());

        var ChartName = GetValueQueryString("cn");
        var ChartType = GetValueQueryString("ct");

        // Set Default Value  :: Chart Type to Daily
        $("#date_rang_type").val(ChartType);


        //default Value For Booking Report
        //All Booking == 1; No. of booking == 2; Room night (booking date) == 3 ; Room night (check in date) == 4

        $("#hd_chart_name").val(ChartName);

        // One Metric == 1; Two Metric == 2
        //$("#hd_chart_mode").val(1);

        // Daily == 1; Monthly == 2 ; yearly == 3
        $("#hd_chart_type").val(ChartType);


        //        $("#hd_compare_metric").val(0);


        //        $("#hd_compare_date_part").val(0);

        $("#hd_date_start").val(dDateStart.getFullYear() + "-" + (dDateStart.getMonth() + 1) + "-" + dDateStart.getDate());
        $("#hd_date_end").val(dDateEnd.getFullYear() + "-" + (dDateEnd.getMonth() + 1) + "-" + dDateEnd.getDate());
        $("#hd_date_month").val(Month);
        $("#hd_date_month_year").val(Year);

        $("#sel_date_month_year").append(SelYearDataBind());
        $("#date_start_daily").val(dDateStart.getFullYear() + "-" + (dDateStart.getMonth() + 1) + "-" + dDateStart.getDate());
        $("#date_end_daily").val(dDateEnd.getFullYear() + "-" + (dDateEnd.getMonth() + 1) + "-" + dDateEnd.getDate());
        DatePicker_smallPicture("date_start_daily");
        DatePicker_smallPicture("date_end_daily");
        $("#sel_date_month_year").val(Year);


        $("#date_chart_type label").html(GetChartName(ChartName, "0"));


        var ChartType = $("#date_rang_type").val();
        switch (ChartType) {
            case "1":
                $("#date_rang_select_daily").show();
                break;
            case "2":
                $("#date_rang_month").show();
                break;
            case "3":
                $("#date_rang_year").show();
                break;
        }

        $("#date_chart_type").click(function () {

            return false;
        });

        $("#date_rage_result").hover(function () {
            $(this).removeClass("date_rage_result");
            $(this).addClass("date_rage_result_active");
        }, function () {
            $(this).removeClass("date_rage_result_active");
            $(this).addClass("date_rage_result");

        });

        $("#btn_cancel").click(function () {

            SetDefaultDaterange();

            return false;

        });


        $("#btn_apply").click(function () {

            GetBookingREportCountry();

            $("#date_rang_selection_hiddend").slideUp('fast');
            var ChartType = $("#hd_chart_type").val();
            GenDate_Display();
            SetHiddenDate();

            $("#booking_stat_bar").fadeOut('fast', function () {
                GetBookingReportContryTable();
            });

            return false;
        });


        $("#click_date_range").click(function () {
            $("#date_rang_selection_hiddend").slideToggle('fast', function () {
                if ($(this).css("display") == "block") {
                    $("#date_rage_result").css("border-color", "#aaaaaa");
                } else {
                    SetDefaultDaterange()
                    $("#date_rage_result").removeAttr("style");
                    $("#date_rage_result").removeClass("date_rage_result_active");
                    $("#date_rage_result").addClass("date_rage_result");

                    //$("#date_rage_result").css("border-color", "#efefef");
                }
            });

            $(this).toggleClass("click_up", "click_down");


        });

        $("#date_rang_type").change(function () {

            var val = $(this).val();

            if ($(this).val() != $("#hd_chart_type").val()) {
                $("#btn_cancel").css("color", "#3b59aa");
                $("#btn_apply").removeAttr("disabled");
            }

            switch (val) {
                case "1":
                    $("#date_rang_select_daily").show();
                    $("#date_rang_month").hide();
                    $("#date_rang_year").hide();
                    $("#date_rang_selection_hiddend").css("width", "279px");

                    $("#chk_compare_to_part").removeAttr("disabled");
                    $("#compareTitle").css("color", "#333333");
                    break;
                case "2":
                    $("#date_rang_month").show();
                    $("#date_rang_select_daily").hide();
                    $("#date_rang_year").hide();
                    $("#date_rang_selection_hiddend").css("width", "200px");


                    $("#chk_compare_to_part").removeAttr("disabled");
                    $("#compareTitle").css("color", "#333333");
                    break;
                case "3":
                    $("#compareTitle").css("color", "#d3d2d2");

                    $("#chk_compare_to_part").attr("disabled", "disabled");


                    $("#date_rang_month").hide();
                    $("#date_rang_select_daily").hide();

                    $("#date_rang_selection_hiddend").css("width", "157px");

                    break;
            }

            $("#hd_chart_type").val(val);


            GenDate_Display();

        });



        // Append Method Special formap
        $("#date_chart_type").click(function () {

            if ($("#date_chart_type_detail_line").css("display") == "none") {
                $("#date_chart_type_detail").hide();
                $("#date_chart_type_detail_line").show();
            }
            else {
                $("#date_chart_type_detail").show();
                $("#date_chart_type_detail_line").hide();
            }


            $("#down_up").toggleClass("down_up_coll", "down_up");

            return false;
        });

//        $("#chart_mode a").click(function () {


//            var mainid = $(this).attr("id");
//            $(this).removeClass("chart_mode_a");
//            $(this).addClass("chart_mode_a_active");

//            var OtherBoj = $("#chart_mode a").filter(function (index) { return $(this).attr("id") != mainid });
//            OtherBoj.removeClass("chart_mode_a_active")
//            OtherBoj.addClass("chart_mode_a");
//            // $(this).toggleClass("chart_mode_a_active", "chart_mode_a");



//            $("#date_chart_type label").html(GetChartName($("#hd_chart_name").val(), $("#hd_compare_metric").val()));


//            return false;
//        });


        $("#one_metric input[name='radio_chart_tpye']").click(function () {
            $("#hd_chart_name").val($(this).val());


            $("#date_chart_type label").html(GetChartName($(this).val(), $("#hd_compare_metric").val()));

            GetBookingREportCountry();

            $("#booking_stat_bar").fadeOut('fast', function () {
                GetBookingReportContryTable();
            });
        });




        GenDate_Display();

        GetBookingREportCountry();

        

        $("#booking_stat_bar").fadeOut('fast', function () {
              GetBookingReportContryTable();
        });



    });



    function GetBookingReportContryTable() {

       
        $("<div class=\"progress_block_detail\"><img class=\"img_progress\" src=\"/images_extra/preloader.gif\" alt=\"Progress\" /></div>").appendTo("#main_booking_stat_bar").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
        
        $.post("../ajax/ajax_report_booking_summary_country_detail.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {

            $("#booking_stat_bar").fadeIn('fast', function () {
                $("#booking_stat_bar").html(data);
            });

        });
    }

    function GetBookingREportCountry() {

        $("<div class=\"progress_block_detail\"><img class=\"img_progress\" src=\"/images_extra/preloader.gif\" alt=\"Progress\" /></div>").appendTo("#container").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });


        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_report_booking_summary_geo_map.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (datas) {
            //alert(datas);

            var REsult = datas.split('/');
            var NumRow = REsult.length;

            var data = new google.visualization.DataTable();
            data.addRows(NumRow);
            data.addColumn('string', 'Country');
            data.addColumn('number', 'Booking');

            if (NumRow > 1) {
                for (i = 0; i < NumRow; i++) {
                    var Val = REsult[i].split(',');

                    data.setValue(i, 0, Val[0]);
                    data.setValue(i, 1, parseInt(Val[1]));
                }
            }


            var geomap = new google.visualization.GeoMap(
                      document.getElementById('container'));
            geomap.draw(data, { width: '915px', height: '350px', showLegend: true });


        });

    }

    google.load('visualization', '1', { packages: ['geomap'] });

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<input  type="hidden" id="hd_chart_name" name="hd_chart_name" />
<input  type="hidden" id="hd_chart_mode"  name="hd_chart_mode"/>
<input  type="hidden" id="hd_chart_type" name="hd_chart_type"/>

<input  type="hidden" id="hd_date_start" name="hd_date_start" />
<input  type="hidden" id="hd_date_end" name="hd_date_end" />

<input  type="hidden" id="hd_date_month_year" name="hd_date_month_year" />

<input  type="hidden" id="hd_compare_metric" name="hd_compare_metric" />

<div id="date_range">
            
        <div id="date_rage_result"  class="date_rage_result"><p class="p_date_display" id="p_date_display"></p><p id="click_date_range" class="click_down"></p>
        <div style=" clear:both"></div>
        </div>
        
        
        <div id="date_rang_selection_hiddend">
            <p class="date_range_title"><span>Chart Type : </span><select id="date_rang_type" class="drop">
            <option value="1">Daily</option>
            <option value="2">Monthly</option>
            <option value="3">Yearly</option>
            </select>
            </p>


            <div id="date_rang_select_daily"  style=" display:none">
            
            <table>
            <tr><td><input type="text" class="date_range_period" id="date_start_daily" /></td><td></td><td><input class="date_range_period" type="text" id="date_end_daily" /></td></tr>
            </table>
           
            </div>

            <div id="date_rang_month" style=" display:none">
                <%--<select id="sel_date_month" name="sel_date_month" class="date_range_period"></select>--%>&nbsp;<select id="sel_date_month_year" name="sel_date_month_year" class="date_range_period"></select>
            </div>

            <%--<div id="date_rang_year" style=" display:none">
                <select id="sel_date_year" name="sel_date_year"  class="date_range_period"></select>
            </div>--%>
            
            <%--<p class="date_range_title" id="compareTitle">Compare to Past <input type="checkbox" id="chk_compare_to_part" /></p>--%>
            <div id="date_rang_select_daily_conpare" style="display:none">
            
             <table>
            <tr><td><input type="text" class="date_range_period" id="date_start_daily_compare" /></td><td></td><td><input type="text" class="date_range_period" id="date_end_daily_compare" /></td></tr>
            </table>
            </div>
            <%--<div id="date_rang_month_compare" style=" display:none">
                <select id="sel_date_month_conpare" name="sel_date_month_conpare" class="date_range_period"></select>&nbsp;<select id="sel_date_month_conpare_year"  name="sel_date_month_conpare_year" class="date_range_period"></select>
            </div>--%>
<%--            <div id="date_rang_year_conpare" style=" display:none">
                <select id="sel_date_year_compare" name="sel_date_year_compare" class="date_range_period"></select>
            </div>--%>
            
            <div class="date_range_btn"><input id="btn_apply" type="button" value="Apply"  /> <a href="" id="btn_cancel">cancel</a></div>
        </div>
             
            
</div>
<div style="clear:both"></div>

<div id="date_chart_type_block">
    
    <a  id="date_chart_type"><label></label><span class="down_up" id="down_up"></span></a>
     <div id="date_chart_type_detail_line"></div>
     <div id="date_chart_type_detail">
        
        <div id="date_chart_type_detail_content" style="margin:15px 0px 0px 0px;">
            <div id="one_metric" >
            <table cellpadding="3" cellspacing="1">
            <tr><td><input type="radio" value="1" name="radio_chart_tpye" checked="checked" /></td><td>All Booking</td></tr>
            <tr><td><input type="radio" value="2" name="radio_chart_tpye" /></td><td>No. of booking</td></tr>
            <tr><td><input type="radio" value="3" name="radio_chart_tpye" /></td><td>Room night (booking date)</td></tr>
            <tr><td><input type="radio" value="4" name="radio_chart_tpye" /></td><td>Room night (check in date)</td></tr>
            </table>
              
            </div>
            
        </div>
     </div>
 </div>

<div style="clear:both"></div>
<div id="main_chart" style=" height:350px;">
    
    <div id="container" style="width: 915px; height: 350px; margin: 0 auto; ">
        
    </div>
    
</div>
<div style="clear:both"></div>

<div id="main_booking_stat_bar">
    
    <div id="booking_stat_bar" style=" display:none;" >
    
    
    </div>
</div>
<div style="clear:both"></div>


</asp:Content>

