<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="mainextra.aspx.cs" Inherits="Hotels2thailand.UI.mainextra"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript" src="../scripts/jquery-1.6.1.js"></script>
<script language="javascript" type="text/javascript" src="../scripts/darkman_cookies.js"></script>
<script language="javascript" type="text/javascript" src="../scripts/extranet/darkman_utility_extranet.js"></script>
<script language="javascript" type="text/javascript" src="../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../scripts/darkman_datepicker.js"></script>
<link type="text/css" href="/css/extranet/dashboard.css" rel="stylesheet" />
<script type="text/javascript" src="../scripts/Highcharts/highcharts.js"></script>

<script type="text/javascript" src="../scripts/Highcharts/modules/exporting.js"></script>

<!-- 1a) Optional: add a theme file -->
		<!--
			<script type="text/javascript" src="../js/themes/gray.js"></script>
		-->
<script type="text/javascript" language="javascript">

    $(document).ready(function () {
       
       

        var Staff_id = $("#staff_id").val();

       //getBookingCenterBlock();
        getRateControlBlock();
        //getAllotmentBlock();
        getPromotionBlock();


        var dDate = new Date();
        var Month = (dDate.getMonth() + 1);
        var Year = dDate.getFullYear();

        $("#Current_Chart_type").val('daily');
        $("#Current_date").val(Month + ";" + Year);

        $(".Month_select").show();

        GetBookingStat(dDate, 'daily');

    });

    function NextMonth() {
        $("#container").fadeOut('fast', function () {

            var CurrentDate = $("#Current_date").val();
            var CurrentMonth = CurrentDate.split(';')[0];
            var CurrentYear = CurrentDate.split(';')[1];
            var ChartType = $("#Current_Chart_type").val();

            if (ChartType == "daily") {
                if (CurrentMonth == 12) {
                    CurrentMonth = 1;
                    CurrentYear = parseInt(CurrentYear) + 1;
                }
                else {
                    CurrentMonth = parseInt(CurrentMonth) + 1
                }
            }

            if (ChartType == "monthly") {
                CurrentYear = parseInt(CurrentYear) + 1;
            }
            


            $("#Current_date").val(CurrentMonth + ";" + CurrentYear);
            var dDate = new Date(CurrentYear, (CurrentMonth - 1), 1);


            GetBookingStat(dDate, ChartType);

        });
        
    }

    function PreviousMonth() {
        $("#container").fadeOut('fast', function () {

            var CurrentDate = $("#Current_date").val();
            var CurrentMonth = CurrentDate.split(';')[0];
            var CurrentYear = CurrentDate.split(';')[1];
            var ChartType = $("#Current_Chart_type").val();

            if (ChartType == "daily") {
                if (CurrentMonth == 1) {
                    CurrentMonth = 12;
                    CurrentYear = parseInt(CurrentYear) - 1;
                } else {
                    CurrentMonth = parseInt(CurrentMonth) - 1
                }

            }

            if (ChartType == "monthly") {
                CurrentYear = parseInt(CurrentYear) - 1;
            }



            $("#Current_date").val(CurrentMonth + ";" + CurrentYear);
            var dDate = new Date(CurrentYear, (CurrentMonth - 1), 1);
            GetBookingStat(dDate, ChartType);

        });
        
    }

    function ChangChartType(chart_type) {

        $("#container").fadeOut('fast', function () {

            var CurrentDate = $("#Current_date").val();
            var CurrentMonth = CurrentDate.split(';')[0];
            var CurrentYear = CurrentDate.split(';')[1];
            var dDate = new Date(CurrentYear, (CurrentMonth - 1), 1);


            switch (chart_type) {
                case "daily":

                    GetBookingStat(dDate, 'daily');
                    $(".Month_select").show();
                    break;
                case "monthly":

                    GetBookingStat(dDate, 'monthly');
                    $(".Month_select").show();
                    break;
                case "yearly":
                    
                    GetBookingStat(dDate, 'yearly');
                    $(".Month_select").hide();
                    break;
            }

        });
        $("#Current_Chart_type").val(chart_type);
    }

    function GetBookingStat(dDate,chart_type){
   
        var Month = (dDate.getMonth() + 1);
        var Year = dDate.getFullYear();
        var TotalDayInMonth = LastDayOfMonth(Year,Month);

        $("<div class=\"progress_block_stat\"><img class=\"img_progress\" src=\"../images_extra/preloader.gif\" alt=\"Progress\" /></div>").prependTo("#main_dash_graph").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        var url = "";
        switch (chart_type) {
            case "daily":
                url = "ajax/ajax_dash_board_bookingStat.aspx?y=" + Year + "&m=" + Month + GetQuerystringProductAndSupplierForBluehouseManage("append");
                break;
            case "monthly":
                url = "ajax/ajax_dash_board_bookingStat_Month.aspx?y=" + Year + "&m=" + Month + GetQuerystringProductAndSupplierForBluehouseManage("append");
                break;
            case "yearly":
                url = "ajax/ajax_dash_board_bookingStat_Year.aspx?y=" + Year + "&m=" + Month + GetQuerystringProductAndSupplierForBluehouseManage("append");
                break;
        }


        $.get(url, function (data) {
           
            if (data != "") {

                $("#container").fadeIn('fast', function () {
                    RenderStatGraph(dDate, TotalDayInMonth, data, chart_type);
                });

            }
        });

    }

    function RenderStatGraph(dDate, TotalDayInMonth, result, chart_type) {

        
        var Mname = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        var  SeriesNameDate = "";
        var HeadName = "";

        switch (chart_type) {
            case "daily":

                var arrTotal_X = new Array();
                for (var i = 0; i <= TotalDayInMonth + 1; i++) {
                    arrTotal_X[i] = i + 1;
                }
                var SeriesNameDate = "";
                SeriesNameDate = Mname[dDate.getMonth()] + ' ' + dDate.getFullYear();
                HeadName = 'Daily Booking' + " <strong><label style=\"color:#fdbe14\">" + Mname[dDate.getMonth()] + " " + dDate.getFullYear() + "</label></strong>";
                break;
            case "monthly":
                SeriesNameDate = ' ' + dDate.getFullYear();
                var arrTotal_X = new Array();
                arrTotal_X = Mname;
                HeadName = 'Monthly Booking ' + " <strong><label style=\"color:#fdbe14\">" +" "+ dDate.getFullYear() + "</label></strong>";
                break;
            case "yearly":

                var Mydate = new Date();
                var yearMax = parseInt(Mydate.getFullYear()) + 4;
                var arrTotal_X = new Array();
                HeadName = 'Yearly Booking';
                count = 0;
                for (var y = 2011; y <= yearMax; y++) {

                    arrTotal_X[count] = y;
                    count = count + 1;
                }

                
                break;
        }


        var options = {
            chart: {
                renderTo: 'container',
                defaultSeriesType: 'line',
                marginRight: 60,
                marginBottom: 60
            },
            title: {
                text: HeadName,
                x: -20 //center 
            },
            subtitle: {
                text: 'Source: extranet.hotels2thailand.com',
                x: -20
            },
            xAxis: {
                categories: arrTotal_X
               

            },
            yAxis: {
                title: {
                    text: 'Booking'
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }],
               
                min:0,
                maxPadding:2
                
            },
            tooltip: {
                formatter: function () {
                    return '<b>' + this.x +' '+ SeriesNameDate +  '</b><br/>' +
								'<b>' + this.series.name + ': ' + this.y ;
                }
            },
            legend: {
                layout: 'horizontal',
                align: 'center',
                verticalAlign: 'bottom',
                x: 0,
                y: 15,
                borderWidth: 1
            },
            series: []
        };
 

//        var impression = result.split('%')[0].split(',');
//        var allbooking = result.split('%')[1].split(',');
//        var bookingCompleted = result.split('%')[2].split(',');
        //var impression = result.split('%')[0].split(',');
        var allbooking = result.split('%')[0].split(',');
        var bookingCompleted = result.split('%')[1].split(',');

//        var series = {
//            name: 'Impression',
//            data: []

//        };
        var series2 = {
            name: 'All Booking',
            data: []

        };
        var series3 = {
            name: 'Booking Completed',
            data: [],
            color: '#89a54e'

        };

//        for (var im = 0; im < impression.length; im++) {
//            series.data.push(parseFloat(impression[im]));
//        }
        for (var b = 0; b < allbooking.length; b++) {
            series2.data.push(parseFloat(allbooking[b]));
        }
        for (var c = 0; c < bookingCompleted.length; c++) {
            series3.data.push(parseFloat(bookingCompleted[c]));
         }


        //options.series.push(series);

        options.series.push(series2);
        options.series.push(series3);

        // Create the chart
        var chart = new Highcharts.Chart(options);
    }

    function hidenotice() {
        
        var Staff_id = $("#staff_id").val();

//        if (getCookie("allot_notice") == "" || getCookie("allot_notice") == "null" || getCookie("allot_notice") == null) {

//            
//        }
        $("#notice_allot").slideUp('600');
        setCookie("allot_notice", Staff_id, 30);
        
       // $("#notice_allot").animate({ height: 0, opacity: 0 ,padding:0}, '600');

    }

    function getBookingCenterBlock() {
        
        $("<div class=\"progress_block\"><img class=\"img_progress\" src=\"../images_extra/preloader.gif\" alt=\"Progress\" /></div>").appendTo("#booking").ajaxStart(function          () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        

        $.get("ajax/ajax_dash_board_booking.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), function (data) {
            $("#booking").html(data);
        });

    }

    function getPromotionBlock() {

        $("<div class=\"progress_block\"><img class=\"img_progress\" src=\"../images_extra/preloader.gif\" alt=\"Progress\" /></div>").appendTo("#promotion").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        $.get("ajax/ajax_dash_board_promotion.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), function (data) {
            $("#promotion").html(data);
        });

    }


    function getAllotmentBlock() {

        $("<div class=\"progress_block\"><img class=\"img_progress\" src=\"../images_extra/preloader.gif\" alt=\"Progress\" /></div>").appendTo("#allotment").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        $.get("ajax/ajax_dash_board_allotment.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), function (data) {
            $("#allotment").html(data);
            tooltip();
            $(".tooltip_content").css("font-size", "9px");
            var Staff_id = $("#staff_id").val();

            if (getCookie("allot_notice") == "" || getCookie("allot_notice") == "null" || getCookie("allot_notice") == null || getCookie("allot_notice") != Staff_id) {
               
                $("#notice_allot").slideDown();
            }

        });

    }
    

    function getRateControlBlock() {
        $("<div class=\"progress_block\"><img class=\"img_progress\" src=\"../images_extra/preloader.gif\" alt=\"Progress\" /></div>").appendTo("#rate_control").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        $.get("ajax/ajax_dash_board_rate_control.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), function (data) {
            $("#rate_control").html(data);

//            $(".topic_rate a").each(function () {
//                var ul = $(this).parent().next();
//                if (ul.children("li").length > 2) {
//                    $(this).click(function () {
//                        if (ul.children("li").length > 2) {
//                            ul.toggleClass("ul");
//                        } else {
//                            $(this).hide();
//                        }
//                    });
//                } else {
//                    $(this).hide();
//                }
//            });
        });
    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="staff_id" runat="server" ClientIDMode="Static" />
<input type="hidden" id="Current_date" />
<input type="hidden" id="Current_Chart_type" />
<%--<div id="main_dash_annouc">
 <div id="announc">
    <h1><label>News!:</label> We have 2 new features are uploaded.</h1>
    <p class="body">1.<label>Last minute</label> in promotion management</p>
    <p class="body">2.<label>Rate Plan</label> in rate control</p>
    <p class="bottom">**You can download how to use it from dash board.</p>
 </div>
</div>--%>
<div id="main_dash_graph">
<p class="btn_type"><input type="button" value="Daily" onclick="ChangChartType('daily');return false;" class="button_Type" />
<input type="button" value="Monthly" onclick="ChangChartType('monthly');return false;" class="button_Type" />
<input type="button" value="Yearly" onclick="ChangChartType('yearly');return false;" class="button_Type"/></p>
    <div id="container" style="width: 910px; height: 250px; margin: 0 auto; display:none;">
    
    </div>
    <p class="Month_select"><input type="button" value="« Previous" class="button_Type" onclick="PreviousMonth();return false;" /> 
    <input type="button" value="Next »" class="button_Type" onclick="NextMonth();return false;" /></p>
</div>

 <div id="main_dash">
    
    <div class="dash_block">
     <%--<div class="dash_block_item">
      <h2>Booking Center</h2>
       <div id="booking" class="dash_block_items">
         
       </div>
     </div>--%>

    <div class="dash_block_item">
      <h2>Rate Control</h2>
      <div id="rate_control" class="dash_block_items">
       
       
      </div>
     </div>
    </div>

    <div class="dash_block"  style=" margin-left:8px;">
    <%--<div class="dash_block_item">
      <h2>Allotment </h2>
      
      <div id="notice_allot" style="display:none;" class="notice"><img src="http://hotels2thailand.com/images_extra/notice.png" /><strong> What we are trying to tell you in this section????</strong><br />
        <span style=" font-size:11px;">This section helps you recheck the current allotment quantity in the system in order to avoid missing bookings; ie:
        
         <p style=" background-color:#fdda72;"><img src="http://hotels2thailand.com/images/dot.png" /> How many days you haven’t added the allotment</p> 
         <p style="background-color:#ffce3a;"><img src="http://hotels2thailand.com/images/dot.png" /> How many days has 1 room left</p>
         <p style="background-color:#fdbe14;"><img src="http://hotels2thailand.com/images/dot.png" /> How many days has 0 room left.</p></span>
        <a class="hide_notice" href="" onclick="hidenotice();return false;">Do not show this message again!</a>
        </div>
     
      <div id="allotment" class="dash_block_items" >
        
      </div>

     </div>--%>

     <div class="dash_block_item" >
      <h2>Promotion</h2>
      <div id="promotion" class="dash_block_items" >
            
      </div>
     </div>
    
    </div>
    
    <div  style="clear:both"></div>
    
 </div>

</asp:Content>

