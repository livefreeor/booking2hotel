$(document).ready(function () {
    // Set Default Value  :: Chart Type to Daily
    $("#date_rang_type").val(1);

    
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

    $("#chart_mode a").click(function () {

        if ($(this).attr("id") == "one_metric_a") {

            // swicth off compare mode
            $("#hd_compare_metric").val(0);

            $("#two_metric").fadeOut('fast', function () {
                $("#one_metric").fadeIn('fast');

                $("#two_metric :radio").removeAttr("checked");

                $("#two_metric input[name='radio_chart_tpye_compare']").parent().removeClass("bg_active");
                $("#two_metric input[name='radio_chart_tpye_compare']").parent().addClass("bg");

                $("#two_metric input[name='radio_chart_tpye_compare_to']").parent().removeClass("bg_compare_active");
                $("#two_metric input[name='radio_chart_tpye_compare_to']").parent().addClass("bg_compare");

                $("#two_metric input[name='radio_chart_tpye_compare']").filter(function (index) { return index == 0 }).attr("checked", "checked");
                $("#two_metric input[name='radio_chart_tpye_compare_to']").filter(function (index) { return index == 0 }).attr("checked", "checked");
                $("#two_metric input[name='radio_chart_tpye_compare']").filter(function (index) { return index == 0 }).parent().addClass("bg_active");
                $("#two_metric input[name='radio_chart_tpye_compare_to']").filter(function (index) { return index == 0 }).parent().addClass("bg_compare_active");
               
                

                $(this).parent().addClass("bg_active");
            });


            $("#chk_compare_to_part").removeAttr("disabled");
            $("#compareTitle").css("color", "#333333");

            GetBookingStatNormal();
        }

        if ($(this).attr("id") == "two_metric_a") {

            $("#hd_chart_name").val(1);
            // swicth on compare mode
            $("#hd_compare_metric").val(1);

            $("#one_metric").fadeOut('fast', function () {
                $("#two_metric").fadeIn('fast');

                $("#two_metric :radio").removeAttr("checked");

                $("#two_metric input[name='radio_chart_tpye_compare']").parent().removeClass("bg_active");
                $("#two_metric input[name='radio_chart_tpye_compare']").parent().addClass("bg");

                $("#two_metric input[name='radio_chart_tpye_compare_to']").parent().removeClass("bg_compare_active");
                $("#two_metric input[name='radio_chart_tpye_compare_to']").parent().addClass("bg_compare");

                $("#two_metric input[name='radio_chart_tpye_compare']").filter(function (index) { return index == 0 }).attr("checked", "checked");
                $("#two_metric input[name='radio_chart_tpye_compare_to']").filter(function (index) { return index == 0 }).attr("checked", "checked");
                $("#two_metric input[name='radio_chart_tpye_compare']").filter(function (index) { return index == 0 }).parent().addClass("bg_active");
                $("#two_metric input[name='radio_chart_tpye_compare_to']").filter(function (index) { return index == 0 }).parent().addClass("bg_compare_active");
               
            });
            $("#compareTitle").css("color", "#d3d2d2");

            $("#chk_compare_to_part").attr("disabled", "disabled");

            GetBookingStatNormal_compare();
        }

        var mainid = $(this).attr("id");
        $(this).removeClass("chart_mode_a");
        $(this).addClass("chart_mode_a_active");

        var OtherBoj = $("#chart_mode a").filter(function (index) { return $(this).attr("id") != mainid });
        OtherBoj.removeClass("chart_mode_a_active")
        OtherBoj.addClass("chart_mode_a");
        // $(this).toggleClass("chart_mode_a_active", "chart_mode_a");



        $("#date_chart_type label").html(GetChartName($("#hd_chart_name").val(), $("#hd_compare_metric").val()));


        return false;
    });


    $("#one_metric input[name='radio_chart_tpye']").click(function () {
        $("#hd_chart_name").val($(this).val());


        $("#date_chart_type label").html(GetChartName($(this).val(), $("#hd_compare_metric").val()));
        $("#container").fadeOut('fast', function () { GetBookingStatNormal(); });

    });
    $("#two_metric input[name='radio_chart_tpye_compare']").click(function () {

        $("#hd_chart_name").val($(this).val());


        $("#two_metric input[name='radio_chart_tpye_compare']").parent().removeClass("bg_active");
        $("#two_metric input[name='radio_chart_tpye_compare']").parent().addClass("bg");

        $(this).parent().addClass("bg_active");

        $("#date_chart_type label").html(GetChartName($("#hd_chart_name").val(), $("#hd_compare_metric").val()));
        $("#container").fadeOut('fast', function () { GetBookingStatNormal_compare(); });


    });

    $("#two_metric input[name='radio_chart_tpye_compare_to']").click(function () {

        $("#hd_compare_metric").val($(this).val());

        $("#two_metric input[name='radio_chart_tpye_compare_to']").parent().removeClass("bg_compare_active");
        $("#two_metric input[name='radio_chart_tpye_compare_to']").parent().addClass("bg_compare");

        $(this).parent().addClass("bg_compare_active");

        $("#date_chart_type label").html(GetChartName($("#hd_chart_name").val(), $("#hd_compare_metric").val()));

        $("#container").fadeOut('fast', function () { GetBookingStatNormal_compare(); });
    });

});




function GetBookingREportSummary() {
    
    $("<div class=\"progress_block_summary\"><img class=\"img_progress\" src=\"/images_extra/preloader.gif\" alt=\"Progress\" /></div>").appendTo("#summary").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });


    var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

    $.post("../ajax/ajax_report_booking_summary.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {

        $("#summary_result").fadeIn('fast', function () {
            $("#summary_result").html(data);
        });

    });
}

