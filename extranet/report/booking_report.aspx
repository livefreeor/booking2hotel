<%@ Page Title="Hotels2thailand:Booking Acknowledge" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="booking_report.aspx.cs" Inherits="Hotels2thailand.UI.extranet_booking_report" %>

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

<script language="javascript" type="text/javascript" src="../../scripts/extranet/booking_report.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/booking_report_chart_render.js"></script>

<script type="text/javascript" language="javascript">
    $(document).ready(function () {

        var dDate = new Date();
        var Month = dDate.getMonth();
        var Year = dDate.getFullYear();


        var lastDayOfMonth = LastDayOfMonth(Year, Month);

        var dDateStart = new Date(Year, Month, 1);
        var dDateEnd = new Date(Year, Month, lastDayOfMonth);


        //default Value For Booking Report
        //All Booking == 1; No. of booking == 2; Room night (booking date) == 3 ; Room night (check in date) == 4

        $("#hd_chart_name").val(1);

        // One Metric == 1; Two Metric == 2
        $("#hd_chart_mode").val(1);

        // Daily == 1; Monthly == 2 ; yearly == 3
        $("#hd_chart_type").val(1);


        $("#hd_compare_metric").val(0);


        $("#hd_date_start").val(dDateStart.getFullYear() + "-" + (dDateStart.getMonth() + 1) + "-" + dDateStart.getDate());
        $("#hd_date_end").val(dDateEnd.getFullYear() + "-" + (dDateEnd.getMonth() + 1) + "-" + dDateEnd.getDate());

        $("#hd_date_month_year").val(Year);

        // End Default Hidden ------------------------------------


        $("#sel_date_month_year").append(SelYearDataBind());


        //set Default Date
        $("#date_start_daily").val(dDateStart.getFullYear() + "-" + (dDateStart.getMonth() + 1) + "-" + dDateStart.getDate());
        $("#date_end_daily").val(dDateEnd.getFullYear() + "-" + (dDateEnd.getMonth() + 1) + "-" + dDateEnd.getDate());
        //                $("#date_start_daily_compare").val(dDateStart.getFullYear() + "-" + (dDateStart.getMonth() + 1) + "-" + dDateStart.getDate());
        //                $("#date_end_daily_compare").val(dDateEnd.getFullYear() + "-" + (dDateEnd.getMonth() + 1) + "-" + dDateEnd.getDate());



        DatePicker_smallPicture("date_start_daily");
        DatePicker_smallPicture("date_end_daily");

        //        DatePicker_smallPicture("date_start_daily_compare");
        //        DatePicker_smallPicture("date_end_daily_compare");

        $("#sel_date_month_year").val(Year);


        $("#date_chart_type label").html(GetChartName("1", "0"));



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

            $("#container").fadeOut('fast', function () {

                if ($("#hd_compare_metric").val() == "0") {
                    GetBookingStatNormal();
                } else {
                    GetBookingStatNormal_compare();

                }



            });

            $("#date_rang_selection_hiddend").slideUp('fast');
            var ChartType = $("#hd_chart_type").val();
            GenDate_Display();
            SetHiddenDate();


            $("#summary_result").fadeOut('fast', function () {
                GetBookingREportSummary();
            });

            GetBookingReportCondition();

            GetBookingREportCountry();

            return false;
        });

        GenDate_Display();
        GetBookingStatNormal();
        $("#summary_result").fadeOut('fast', function () {
            GetBookingREportSummary();
        });
        GetBookingReportCondition();

        GetBookingREportCountry();

    });

    function GetBookingReportCondition() {

        $("<div class=\"progress_block_detail\"><img class=\"img_progress\" src=\"/images_extra/preloader.gif\" alt=\"Progress\" /></div>").appendTo("#booking_condition_stat_detail").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_report_booking_condition.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {


            $("#booking_condition_stat_detail").html(data);

            $("#view_report_condition").attr("href", "booking_report_detail_condition.aspx" + GetUrlLink());
        });
    }

    function GetBookingREportCountry() {
        

        $("<div class=\"progress_block_detail\"><img class=\"img_progress\" src=\"/images_extra/preloader.gif\" alt=\"Progress\" /></div>").appendTo("#visualization").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });


        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_report_booking_summary_geo_map.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (datas) {


            var REsult = datas.split('/');
            var NumRow = REsult.length;

            var data = new google.visualization.DataTable();
            data.addRows(NumRow);
            data.addColumn('string', 'Country');
            data.addColumn('number', 'Popularity');

            if (NumRow > 1) {
                for (i = 0; i < NumRow; i++) {
                    var Val = REsult[i].split(',');

                    data.setValue(i, 0, Val[0]);
                    data.setValue(i, 1, parseInt(Val[1]));
                }
            }


            var geomap = new google.visualization.GeoMap(
                      document.getElementById('visualization'));
            geomap.draw(data, { width: '453px', height: '222px', showLegend: false });


            $("#view_report_country").attr("href", "booking_report_detail_country.aspx" + GetUrlLink());
        });

    }


    function GetUrlLink() {
        var dDateStart = $("#hd_date_start").val();
        var dDateEnd = $("#hd_date_end").val();
        var dDateMonthYear = $("#hd_date_month_year").val();
        var ChartType = $("#hd_chart_type").val();
        var CahrtName = $("#hd_chart_name").val();

        var Reresult = "?pdr=" + dDateStart + "/" + dDateEnd + "&pdm=" + dDateMonthYear + "&ct=" + ChartType + "&cn=" + CahrtName;
        if (GetValueQueryString("pid") != " " && GetValueQueryString("supid") != " ") {
            Reresult =  Reresult + "&pid=" + GetValueQueryString("pid") + "&supid=" + GetValueQueryString("supid");
        }

        return Reresult;
    }

    google.load('visualization', '1', { packages: ['geomap'] });

    //google.setOnLoadCallback(GetBookingREportCountry);
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
            <tr><td><input type="text" class="date_range_period" id="date_start_daily" /></td><td><input class="date_range_period" type="text" id="date_end_daily" /></td></tr>
            </table>
           
            </div>

            <div id="date_rang_month" style=" display:none">
                <select id="sel_date_month_year" name="sel_date_month_year" class="date_range_period"></select>
            </div>

            <%--<div id="date_rang_select_daily_conpare">
            
             <table>
            <tr><td><input type="text" class="date_range_period" id="date_start_daily_compare" /></td><td><input type="text" class="date_range_period" id="date_end_daily_compare" /></td></tr>
            </table>
            
            </div>--%>

            
            <div class="date_range_btn"><input id="btn_apply" type="button" value="Apply"  /> <a href="" id="btn_cancel">cancel</a></div>
        </div>
</div>

<div style="clear:both"></div>
<input  type="hidden" id="hd_chart_name" name="hd_chart_name" />
<input  type="hidden" id="hd_chart_mode"  name="hd_chart_mode"/>
<input  type="hidden" id="hd_chart_type" name="hd_chart_type"/>

<input  type="hidden" id="hd_date_start" name="hd_date_start" />
<input  type="hidden" id="hd_date_end" name="hd_date_end" />


<input  type="hidden" id="hd_date_month_year" name="hd_date_month_year" />


<input  type="hidden" id="hd_compare_metric" name="hd_compare_metric" />
<%--<input  type="hidden" id="hd_compare_date_part" name="hd_compare_date_part" />

<input  type="hidden" id="hd_compare_date_start" name="hd_compare_date_start" />
<input  type="hidden" id="hd_compare_date_end" name="hd_compare_date_end" />--%>

<%--<input  type="hidden" id="hd_compare_date_month" name="hd_compare_date_month" />--%>
<%--<input  type="hidden" id="hd_compare_date_month_year" name="hd_compare_date_month_year" />--%>

<div id="date_chart_type_block">
    
    <a href="" id="date_chart_type"><label></label><span class="down_up" id="down_up"></span></a>
     <div id="date_chart_type_detail_line"></div>
     <div id="date_chart_type_detail">
        <div id="chart_mode">
        <table cellpadding="0" cellspacing="0"><tr><td>Graph mode : &nbsp;</td>
        <td style=" width:100px;"><table cellpadding="0" cellspacing="0"><tr><td style="width:20px;"><img src="http://www.hotels2thailand.com/images_extra/chart_icon_2.png" /></td><td><a id="one_metric_a" class="chart_mode_a_active" href="">One Metrics</a></td></tr></table></td>
        <td><table cellpadding="0" cellspacing="0"><tr><td style="width:20px;"><img src="http://www.hotels2thailand.com/images_extra/chart_icon_3.png" /></td><td><a class="chart_mode_a" id="two_metric_a" href=""> Two Metrics</a></td></tr></table></td></tr></table>
        </div>
        <div id="date_chart_type_detail_content">
            <div id="one_metric">
            <table cellpadding="3" cellspacing="1">
            <tr><td><input type="radio" value="1" name="radio_chart_tpye" checked="checked" /></td><td>All Booking</td></tr>
            <tr><td><input type="radio" value="2" name="radio_chart_tpye" /></td><td>No. of booking</td></tr>
            <tr><td><input type="radio" value="3" name="radio_chart_tpye" /></td><td>Room night (booking date)</td></tr>
            <tr><td><input type="radio" value="4" name="radio_chart_tpye" /></td><td>Room night (check in date)</td></tr>
            </table>
              
            </div>
            <div id="two_metric" >
            <table  cellspacing="1" >
            <tr><td class="bg_active" ><input type="radio" value="1" name="radio_chart_tpye_compare" checked="checked"  /></td><td class="bg_compare_active"><input type="radio" checked="checked" value="1" name="radio_chart_tpye_compare_to" /></td><td>&nbsp;&nbsp;All Booking</td></tr>
            <tr><td class="bg"><input type="radio" value="2" name="radio_chart_tpye_compare" /></td><td class="bg_compare"><input type="radio" value="2" name="radio_chart_tpye_compare_to" /></td><td>&nbsp;&nbsp;No. of booking</td></tr>
            <tr><td class="bg"><input type="radio" value="3" disabled="disabled" name="radio_chart_tpye_compare" /></td><td class="bg_compare"><input type="radio" value="3" disabled="disabled" name="radio_chart_tpye_compare_to" /></td><td>&nbsp;&nbsp;<label style="text-decoration:line-through; color:Gray">Room night (booking date)</label></td></tr>
            <tr><td class="bg"><input type="radio" value="4" disabled="disabled" name="radio_chart_tpye_compare" /></td><td class="bg_compare"><input type="radio" value="4" disabled="disabled"  name="radio_chart_tpye_compare_to" /></td><td>&nbsp;&nbsp;<label style="text-decoration:line-through;color:Gray">Room night (check in date)</label></td></tr>
            </table>
              
            </div>
        </div>
     </div>
 </div>
 <div style="clear:both"></div>
<div id="main_chart">
    
    <div id="container" style="width: 920px; height: 250px; margin: 0 auto; display:none;">
    
    </div>
    
</div>
<p class="booking_summary">Booking Summary</p>
<div id="summary">
    <div id="summary_result"></div>
    
</div>
<div style="clear:both"></div>
<div id="main_booking_stat_block">

    <div id="booking_condition_stat">
    <p class="block_head"> Condition report</p>
    <div class="stat_block_detail" id="booking_condition_stat_detail" ></div>
    <p class="view_report"><a id="view_report_condition">view report</a></p>
    </div>


    <div id="booking_country_stat">
    <p class="block_head"> Country Report</p>
    <div class="stat_block_detail_map" id="visualization" ></div>
    <p class="view_report"><a id="view_report_country">view report</a></p>
    </div>
</div>
<div style="clear:both"></div>
</asp:Content>

