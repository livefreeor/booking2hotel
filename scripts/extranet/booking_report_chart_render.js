


function GetChartName(chartName, metric) {
    var result = "";

    var IconChart = "<img src=\"http://www.hotels2thailand.com/images_extra/chart_icon_1.png\" />&nbsp;";

    switch (chartName) {
        case "1":
            result = IconChart + "All Booking";
            break;
        case "2":
            result = IconChart + "No. of booking";
            break;
        case "3":
            result = IconChart + "Room night (booking date)";
            break;
        case "4":
            result = IconChart + "Room night (check in date)";
            break;
    }


    if (metric != "0") {
        IconChart = "<img src=\"http://www.hotels2thailand.com/images_extra/chart_icon_11.png\" />&nbsp;";
        switch (metric) {
            case "1":
                result = result + "&nbsp;&nbsp;&nbsp;" + IconChart + "All Booking";
                break;
            case "2":
                result = result + "&nbsp;&nbsp;&nbsp;" + IconChart + "No. of booking";
                break;
            case "3":
                result = result + "&nbsp;&nbsp;&nbsp;" + IconChart + "Room night (booking date)";
                break;
            case "4":
                result = result + "&nbsp;&nbsp;&nbsp;" + IconChart + "Room night (check in date)";
                break;
        }
    }

   

    return result
}


function GetgraphChartname(chartName) {
    var result = "";
    switch (chartName) {
        case "1":
            result = "All Booking";
            break;
        case "2":
            result = "No. of booking";
            break;
        case "3":
            result = "Room night (booking date)";
            break;
        case "4":
            result = "Room night (check in date)";
            break;
    }

    return result;
}

function GetChartUnitName(chartName) {
    var result = "";
    switch (chartName) {
        case "1": case "2":
            result = "Booking";
            break;
        
        case "3": case "4":
            result = "Room night(s)";
            break;

    }

    return result;
}




function SelYearDataBind() {

    var Mydate = new Date();
    var yearMax = parseInt(Mydate.getFullYear()) + 4;

    var Option = "";
    count = 0;
    for (var y = 2011; y <= yearMax; y++) {

        Option = Option + "<option value=\"" + y + "\">" + y + "</option>";
        count = count + 1;
    }

    return Option;
}


function SelMothDataBind() {
    var Mname = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var Option = "";
    for (i = 0; i < Mname.length; i++) {
        Option = Option + "<option value=\"" + (i + 1) + "\">" + Mname[i] + "</option>";
    }
    return Option;
}




function GenDate_Display() {

    var ChartType = $("#hd_chart_type").val();

    var dDAtestart = parseDate($("#hd_date_start_daily").val());
    var dDateEnd = parseDate($("#hd_date_end_daily").val());


    var dYear = $("#sel_date_month_year").val();


    var Displayresult = "";
    switch (ChartType) {
        case "1":
            Displayresult = dDAtestart.getDate() + " " + getMonthName(dDAtestart.getMonth()) + " " + dDAtestart.getFullYear() + " - " +
                dDateEnd.getDate() + " " + getMonthName(dDateEnd.getMonth()) + " " + dDateEnd.getFullYear();

            break;
        case "2":
            Displayresult = dYear;
           
            break;
        case "3":
            var Mydate = new Date();
            var yearMax = parseInt(Mydate.getFullYear()) + 4;
            Displayresult = "2011" + " - "  + yearMax;
            break;
    }

    $("#p_date_display").html(Displayresult);
}








function GetBookingStatNormal_compare() {


    var dDAtestart = parseDate($("#hd_date_start_daily").val());
    var dDateEnd = parseDate($("#hd_date_end_daily").val());

    var TotalDayInMonth = daydiff(dDAtestart, dDateEnd) + 1;



    var chart_type = $("#hd_chart_type").val();


    $("<div class=\"progress_block_stat\"><img class=\"img_progress\" src=\"/images_extra/preloader.gif\" alt=\"Progress\" /></div>").prependTo("#main_chart").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });

    var url = "";

    switch (chart_type) {
        case "1":

            url = "../ajax/ajax_report_booking_compare_daily.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new");
            break;
        case "2":
            url = "../ajax/ajax_report_booking_compare_month.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new");
            break;
        case "3":
            url = "../ajax/ajax_report_booking_compare_year.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new");
            break;
    }


    var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
    $.post(url, post, function (data) {

        if (data != "") {

            $("#container").fadeIn('fast', function () {
                switch (chart_type) {
                    case "1":

                        RenderStatGraph_compare_daily(dDAtestart, TotalDayInMonth, data, chart_type);
                        break;
                    case "2":

                        RenderStatGraph_compare(dDAtestart, TotalDayInMonth, data, chart_type);
                        break;
                    case "3":
                        RenderStatGraph_compare(dDAtestart, TotalDayInMonth, data, chart_type);
                        break;
                }

            });

        }
    });



}

function GetBookingStatNormal() {


    var dDAtestart = parseDate($("#hd_date_start_daily").val());
    var dDateEnd = parseDate($("#hd_date_end_daily").val());

    var TotalDayInMonth = daydiff(dDAtestart, dDateEnd) + 1;


    var chart_type = $("#hd_chart_type").val();

    $("<div class=\"progress_block_stat\"><img class=\"img_progress\" src=\"/images_extra/preloader.gif\" alt=\"Progress\" /></div>").prependTo("#main_chart").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });

    var url = "";

    switch (chart_type) {
        case "1":

            url = "../ajax/ajax_report_booking_normal_daily.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new");
            break;
        case "2":
            url = "../ajax/ajax_report_booking_normal_month.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new");
            break;
        case "3":
            url = "../ajax/ajax_report_booking_normal_year.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new");
            break;
    }

    var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
    $.post(url, post, function (data) {
       
        if (data != "") {

            $("#container").fadeIn('fast', function () {
                switch (chart_type) {
                    case "1":
                        RenderStatGraph_daily(dDAtestart, TotalDayInMonth, data, chart_type);
                        break;
                    case "2":
                        RenderStatGraph(dDAtestart, TotalDayInMonth, data, chart_type);
                        break;
                    case "3":
                        RenderStatGraph(dDAtestart, TotalDayInMonth, data, chart_type);
                        break;
                }

            });

        }
    });


}
function SetHiddenDate() {

    $("#hd_date_start").val($("#hd_date_start_daily").val());
    $("#hd_date_end").val($("#hd_date_end_daily").val());
    //        $("#hd_date_month").val($("#sel_date_month").val());
    $("#hd_date_month_year").val($("#sel_date_month_year").val());

    //        $("#hd_compare_date_start").val($("#hd_date_start_daily_compare").val());
    //        $("#hd_compare_date_end").val($("#hd_date_end_daily_compare").val());
    //        $("#hd_compare_date_month").val($("#sel_date_month_conpare").val());
    //        $("#hd_compare_date_month_year").val($("#sel_date_month_conpare_year").val());
}

function SetDefaultDaterange() {
    var dDateStart = $("#hd_date_start").val();
    var dDateEnd = $("#hd_date_end").val();

    var dDatecancel = new Date()

    dDatecancel = parseDate(dDateStart);
    //        var MonthCancel = $("#hd_date_month").val();
    var MonthYear = $("#hd_date_month_year").val();


    $("#date_start_daily").val(dDateStart);
    $("#date_end_daily").val(dDateEnd);
    //        $("#date_start_daily_compare").val(dDateStart);
    //        $("#date_end_daily_compare").val(dDateEnd);

    DatePicker_smallPicture("date_start_daily");
    DatePicker_smallPicture("date_end_daily");
    //        DatePicker_smallPicture("date_start_daily_compare");
    //        DatePicker_smallPicture("date_end_daily_compare");


    //        $("#sel_date_month").val(MonthCancel);
    $("#sel_date_month_year").val(MonthYear);
    //        

    //        $("#sel_date_month_conpare").val(MonthCancel);
    //        $("#sel_date_month_conpare_year").val(MonthYear);

}

function RenderStatGraph(dDate, TotalDayInMonth, result, chart_type) {
   
    var Mname = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var SeriesNameDate = "";
    var HeadName = "";
    
    switch (chart_type) {
        case "1":

            break;
        case "2":
            SeriesNameDate = ' ' + dDate.getFullYear();
            var arrTotal_X = new Array();
            arrTotal_X = Mname;

            break;
        case "3":

            var Mydate = new Date();
            var yearMax = parseInt(Mydate.getFullYear()) + 4;
            var arrTotal_X = new Array();

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
            defaultSeriesType: 'area',
            marginRight: 20,
            marginBottom: 60
        },
        title: null,

        xAxis: {
            categories: arrTotal_X

        },
        yAxis: {
            title: null,
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
            }],

            min: 0,
            maxPadding: 2

        },
        tooltip: {
            crosshairs: {
                width: 1,
                color: '#e7e7e7'
            },
            formatter: function () {
                var s = '<label style=\"font-size:10px\">' + this.x + ', ' + SeriesNameDate + '</label>';

                $.each(this.points, function (i, point) {
                    if (i == 0) {
                        s += '<br/><label style=\"color:#0077cc\">' + point.series.name + ': </label><strong>' +
                    point.y + '</strong>';
                    } else {
                        s += '<br/><label style=\"color:' + point.series.color + '\">' + point.series.name + ': </label><strong>' + point.y + '</strong>';
                    }
                });

                return s;
            },
            shared: true
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

    var allbooking = result.split(',');

    var ChartName = GetgraphChartname($("#hd_chart_name").val());

    var series = {
        name: ChartName,
        data: [],
        color: '#e6f2fa',
        marker: {

            fillColor: '#0077cc',
            symbol: 'circle',
            radius: 3,
            lineColor: '#0077cc',
            lineWidth: 3

        },
        shadow: false,
        lineColor: '#0077cc',
        lineWidth: 3


    };


    for (var im = 0; im < allbooking.length; im++) {
        series.data.push(parseFloat(allbooking[im]));
    }


    options.series.push(series);

    // Create the chart
    var chart = new Highcharts.Chart(options);
}

function RenderStatGraph_compare_daily(dDate, TotalDayInMonth, result, chart_type) {
    var Mname = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var SeriesNameDate = "";
    var HeadName = "";

    var month = dDate.getMonth();
    var year = dDate.getFullYear();
    var date = dDate.getDate()

    var timeinterval = 7;

    //        if(TotalDayInMonth > 7 && TotalDayInMonth <= 90){
    //            timeinterval = 7;
    //        }
    //        


    var options = {
        chart: {
            renderTo: 'container',
            defaultSeriesType: 'area',
            marginRight: 20,
            marginBottom: 60
        },
        title: null,

        xAxis: {

            type: 'datetime',
            dateTimeLabelFormats: { //custom date formats for different scales
                second: '%H:%M:%S',
                minute: '%H:%M',
                hour: '%H:%M',
                day: '%e. %b',
                week: '%e. %b',
                month: '%b', //month formatted as month only
                year: '%Y'

            },
            maxZoom: 7 * 24 * 3600 * 1000


        },
        yAxis: {
            title: null,
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
            }],

            min: 0,
            maxPadding: 2

        },
        tooltip: {
            crosshairs: {
                width: 1,
                color: '#e7e7e7'
            },
            formatter: function () {
                var s = '<label style=\"font-size:10px\">' + Highcharts.dateFormat('%a, %d %b, %Y', this.x) + '</label>';

                $.each(this.points, function (i, point) {
                    if (i == 0) {
                        s += '<br/><label style=\"color:#0077cc\">' + point.series.name + ': </label><strong>' +
                    point.y + '</strong>';
                    } else {
                        s += '<br/><label style=\"color:' + point.series.color + '\">' + point.series.name + ': </label><strong>' + point.y + '</strong>';
                    }
                });

                return s;
            },
            shared: true
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

    var allbooking = result.split('%')[0].split(',');
    var allbooking_compare = result.split('%')[1].split(',');

    var ChartName = GetgraphChartname($("#hd_chart_name").val());
    var ChartName_metric_compare = GetgraphChartname($("#hd_compare_metric").val());



    var series = {
        name: ChartName,
        data: [],
        color: '#e6f2fa',
        marker: {

            fillColor: '#0077cc',
            symbol: 'circle',
            radius: 3,
            lineColor: '#0077cc',
            lineWidth: 3

        },
        shadow: false,
        lineColor: '#0077cc',
        lineWidth: 3,
        pointStart: Date.UTC(year, month, date),
        pointInterval: 24 * 3600 * 1000

    };


    var series2 = {
        type: 'line',
        name: ChartName_metric_compare,
        data: [],
        color: '#ff9900',
        marker: {

            fillColor: '#ff9900',
            symbol: 'circle',
            radius: 2,
            lineColor: '#ff9900',
            lineWidth: 2

        },
        shadow: false,
        lineColor: '#ff9900',
        lineWidth: 2,
        pointStart: Date.UTC(year, month, date),
        pointInterval: 24 * 3600 * 1000

    };

    for (var im = 0; im < allbooking.length; im++) {
        series.data.push(parseFloat(allbooking[im]));
    }

    for (var b = 0; b < allbooking_compare.length; b++) {
        series2.data.push(parseFloat(allbooking_compare[b]));
    }

    options.series.push(series);
    options.series.push(series2);
    // Create the chart
    var chart = new Highcharts.Chart(options);
}

function RenderStatGraph_compare(dDate, TotalDayInMonth, result, chart_type) {

    var Mname = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var SeriesNameDate = "";
    var HeadName = "";

    switch (chart_type) {
        case "1":

            break;
        case "2":
            SeriesNameDate = ' ' + dDate.getFullYear();
            var arrTotal_X = new Array();
            arrTotal_X = Mname;

            break;
        case "3":

            var Mydate = new Date();
            var yearMax = parseInt(Mydate.getFullYear()) + 4;
            var arrTotal_X = new Array();

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
            defaultSeriesType: 'area',
            marginRight: 20,
            marginBottom: 60
        },
        title: null,

        xAxis: {
            categories: arrTotal_X



        },
        yAxis: {
            title: null,
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
            }],

            min: 0,
            maxPadding: 2

        },

        tooltip: {
            crosshairs: {
                width: 1,
                color: '#e7e7e7'
            },
            formatter: function () {

                var s = '<label style=\"font-size:10px\">' + this.x + ', ' + SeriesNameDate + '</label>';

                $.each(this.points, function (i, point) {
                    if (i == 0) {
                        s += '<br/><label style=\"color:#0077cc\">' + point.series.name + ': </label><strong>' +
                    point.y + '</strong>';
                    } else {
                        s += '<br/><label style=\"color:' + point.series.color + '\">' + point.series.name + ': </label><strong>' + point.y + '</strong>';
                    }
                });

                return s;
            },
            shared: true
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

    var allbooking = result.split('%')[0].split(',');
    var allbooking_compare = result.split('%')[1].split(',');

    var ChartName = GetgraphChartname($("#hd_chart_name").val());
    var ChartName_metric_compare = GetgraphChartname($("#hd_compare_metric").val());

    var series = {

        name: ChartName,
        data: [],
        color: '#e6f2fa',
        marker: {

            fillColor: '#0077cc',
            symbol: 'circle',
            radius: 3,
            lineColor: '#0077cc',
            lineWidth: 3

        },
        shadow: false,
        lineColor: '#0077cc',
        lineWidth: 3


    };

    var series2 = {
        type: 'line',
        name: ChartName_metric_compare,
        data: [],
        color: '#ff9900',
        marker: {

            fillColor: '#ff9900',
            symbol: 'circle',
            radius: 2,
            lineColor: '#ff9900',
            lineWidth: 2

        },
        shadow: false,
        lineColor: '#ff9900',
        lineWidth: 2

    };

    for (var im = 0; im < allbooking.length; im++) {
        series.data.push(parseFloat(allbooking[im]));
    }
    for (var b = 0; b < allbooking_compare.length; b++) {
        series2.data.push(parseFloat(allbooking_compare[b]));
    }

    options.series.push(series);
    options.series.push(series2);

    // Create the chart
    var chart = new Highcharts.Chart(options);
}


function RenderStatGraph_daily(dDate, TotalDayInMonth, result, chart_type) {
    var Mname = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var SeriesNameDate = "";
    var HeadName = "";

    var month = dDate.getMonth();
    var year = dDate.getFullYear();
    var date = dDate.getDate()

    var timeinterval = 7;

    //        if(TotalDayInMonth > 7 && TotalDayInMonth <= 90){
    //            timeinterval = 7;
    //        }
    //


    var options = {
        chart: {
            renderTo: 'container',
            defaultSeriesType: 'area',
            marginRight: 20,
            marginBottom: 60
        },
        title: null,

        xAxis: {

            type: 'datetime',
            dateTimeLabelFormats: { //custom date formats for different scales
                second: '%H:%M:%S',
                minute: '%H:%M',
                hour: '%H:%M',
                day: '%e. %b',
                week: '%e. %b',
                month: '%b', //month formatted as month only
                year: '%Y'

            },
            maxZoom: 7 * 24 * 3600 * 1000


        },
        yAxis: {
            title: null,
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
            }],

            min: 0,
            maxPadding: 2

        },
        tooltip: {
            crosshairs: {
                width: 1,
                color: '#e7e7e7'
            },
            formatter: function () {
                var s = '<label style=\"font-size:10px\">' + Highcharts.dateFormat('%a, %d %b, %Y', this.x) + '</label>';

                $.each(this.points, function (i, point) {
                    if (i == 0) {
                        s += '<br/><label style=\"color:#0077cc\">' + point.series.name + ': </label><strong>' +
                    point.y + '</strong>';
                    } else {
                        s += '<br/><label style=\"color:' + point.series.color + '\">' + point.series.name + ': </label><strong>' + point.y + '</strong>';
                    }


                });

                return s;
            },
            shared: true
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

    var allbooking = result.split(',');

    var ChartName = GetgraphChartname($("#hd_chart_name").val());


    var series = {
        name: ChartName,
        data: [],
        color: '#e6f2fa',
        marker: {

            fillColor: '#0077cc',
            symbol: 'circle',
            radius: 3,
            lineColor: '#0077cc',
            lineWidth: 3

        },
        shadow: false,
        lineColor: '#0077cc',
        lineWidth: 3,
        pointStart: Date.UTC(year, month, date),
        pointInterval: 24 * 3600 * 1000

    };


    for (var im = 0; im < allbooking.length; im++) {
        series.data.push(parseFloat(allbooking[im]));
    }


    options.series.push(series);

    // Create the chart
    var chart = new Highcharts.Chart(options);
}

