<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="account_settle_report.aspx.cs" Inherits="Hotels2thailand.UI.admin_account_settle_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
  <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/darkman_utility.js" ></script>
<script language="javascript" type="text/javascript">
//    $(document).ready(function () {
//        //        $("#form1").removeAttr("action");
//        //        $("#form1").unbind("__doPostBack");

//        //        $("#form1").attr("action", "account_settle_report_log.aspx");

//        
//    });

//    $(document).ready(function () {


//       

//        
//    });

    function accountReport() {
        $("<p class=\"progress_block\"><img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" /></p>").insertBefore("#report_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });


        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
        $("#report_type").val("acc");
        $.post("../ajax/ajax_account_settle_list.aspx?type=acc", post, function (data) {

            $("#report_list").html(data);
            $("input[id^='txt_price_']").keyup(function () {
                var ALink = $(this).parent().find("a").stop();

                ALink.html($(this).val());
            });

            $("input[id^='txt_cost_']").keyup(function () {
                var ALink = $(this).parent().find("a").stop();

                ALink.html($(this).val());
            });
        });
    }

    function bhtreport() {

        $("<p class=\"progress_block\"><img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" /></p>").insertBefore("#report_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $("#report_type").val("bht");
        $.post("../ajax/ajax_account_settle_list.aspx?type=bht", post, function (data) {
           
            $("#report_list").html(data);
            $("input[id^='txt_price_']").keyup(function () {
                var ALink = $(this).parent().find("a").stop();
                
                ALink.html($(this).val());
            });

            $("input[id^='txt_cost_']").keyup(function () {
                var ALink = $(this).parent().find("a").stop();

                ALink.html($(this).val());
            });
        });
    }


    function changePrice(paymentId) {

        $("#price_" + paymentId).hide();
        $("#txt_price_" + paymentId).show();
       
    }

    function changeCost(paymentId) {
        
        $("#cost_" + paymentId).hide();
        $("#txt_cost_" + paymentId).show();
    }

    function Print() {

        var reportType = GetValueQueryString("t");
        if (reportType == "acc") {

            $("input[name='checkbox_checked']:checked").each(function () {
                var Id = $(this).val(); var PriceVal = $("#price_" + Id).html().replace(',', '');
                var arrVal = PriceVal.split('.');
                PriceVal = (parseFloat(PriceVal)).formatMoney(2, '.', ',');

                $("#price_" + Id).parent().html(PriceVal);

            });
        }


        if (reportType == "bht") {

            $("input[name='checkbox_checked']:checked").each(function () {
                var Id = $(this).val();
                var PriceVal = $("#price_" + Id).html().replace(',', '');
                var CostVal = $("#cost_" + Id).html().replace(',', '');
                var arrVal = PriceVal.split('.');
                var arrValCost = CostVal.split('.');
                PriceVal = (parseFloat(PriceVal)).formatMoney(2, '.', ',');
                CostVal = (parseFloat(CostVal)).formatMoney(2, '.', ',');
                $("#price_" + Id).parent().html(PriceVal);
                $("#cost_" + Id).parent().html(CostVal);
            });
        }
        
        


        var result = $("#report_list").html();
        var Style = $("style").html();
        var report_type = $("#report_type").val();
        post_to_url("account_settle_report_print.aspx?t=" + report_type, { 'p': result, 's': Style });


        return false;
    }

    function NewCal() {
        
        if ($("#report_type").val() == "acc") {

            var TotalPrice = 0.0;
            
            $("input[name='checkbox_checked']:checked").each(function () {

                var Id = $(this).val();

                var Price = parseFloat($("#price_" + Id).html().replace(',', ''));

                TotalPrice = TotalPrice + Price;
                
            });


            $("#total_price").html((parseFloat(TotalPrice)).formatMoney(2, '.', ','));
            
        }

        if ($("#report_type").val() == "bht") {
            var TotalPrice = 0.0;
            var TotalCost = 0.0;
            var TotalTrans = 0.0;
            var TotalProfit = 0.0;
            var TotalPercent = 0.0;
            $("input[name='checkbox_checked']:checked").each(function () {
                var Id = $(this).val();
                var Price = parseFloat($("#price_" + Id).html().replace(',', ''));
                var Cost = parseFloat($("#cost_" + Id).html().replace(',', ''));
                var Trans = Price * parseFloat("0.02675");
                var Profit = Price - Cost - Trans;
                var Percent = (Profit * 100) / Price;


                $("#trans_" + Id).html((parseFloat(Trans)).formatMoney(2, '.', ','));
                $("#profit_" + Id).html((parseFloat(Profit)).formatMoney(2, '.', ','));
                $("#percent_" + Id).html((parseFloat(Percent)).formatMoney(2, '.', ','));

                TotalPrice = TotalPrice + Price;
                TotalCost = TotalCost + Cost;
                TotalTrans = TotalTrans + Trans;
                TotalProfit = TotalProfit + Profit;

            });


            TotalPercent = (TotalProfit * 100) / TotalPrice;
            $("#total_price").html((parseFloat(TotalPrice)).formatMoney(2, '.', ','));
            $("#total_cost").html((parseFloat(TotalCost)).formatMoney(2, '.', ','));
            $("#total_trans").html((parseFloat(TotalTrans)).formatMoney(2, '.', ','));
            $("#total_profit").html((parseFloat(TotalProfit)).formatMoney(2, '.', ','));
            $("#total_percent").html((parseFloat(TotalPercent)).formatMoney(2, '.', ','));
            //            Val = (parseFloat(Val)).formatMoney(2, '.', ',');
        }



        return false;
    }
</script>
<style type="text/css">
    #report_list
    {
        margin:15px 0px 0px 0px;
        padding:0px;
        width:100%;
        font-size:12px;
    }
    #report_list a
    {
            font-size:12px;
    }
    #report_list table
    {
         background-color:#d8dfea;
         margin:0px;
         padding:0px;
         width:100%;
          font-size:12px;
    }
     #report_list table tr
    {
        margin:0px;
         padding:0px;
          height:30px;
          
    }
     #report_list table tr th
    {
        margin:0px;
         padding:0px;
         color:#ffffff;
         text-align:center;
         font-size:12px;
    }
     #report_list table tr td
    {
        margin:0px;
         padding:0px;
         text-align:center;
    }
    #block_print
    {
        margin:20px 0px 0px 0px;
        padding:0px;
        text-align:center;
    }
    .text_default
    {
        width:100%; text-align:center;
        font-size:14px;
        color:#3f5d9d;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style=" margin:0 auto; width:500px; border:1px solid #d6d6d6; background-color:#f2f2f2;" id="report_key_block">
    <h4>&nbsp; &nbsp;<img src="../../images/content.png"  alt="image_topic" /> Account Settle Report </h4>
         <p class="contentheadedetail">&nbsp; &nbsp;add payment id  one by one  and click to report which you want</p><br /><br />
     <table cellpadding="0" cellspacing="0" style=" margin:0 auto;" >
     <tr><td></td><td><label><p style="font:12px; font-weight:bold;">Payment Id Box</p></label><textarea name="txt_payment_id" rows="20" cols="9" id="txt_payment_id"></textarea></td><td></td></tr>
     <tr><td><input type="button" onclick="accountReport();return false;" value="Account Report" /></td><td></td><td><input type="submit" onclick="bhtreport();return false;" value="BlueHouse Report" /></td></tr>
     </table>
    </div>
    <input type="hidden" id="report_type" />
    <div id="report_list">
    <p class="text_default">*please insert Booking Payment Id in the box above and select one to report </p>
    </div>

    <div id="block_print">
    <input type="button"  value="Calculate" onclick="NewCal();return false;" />
    <input type="button"  value="Print Now" onclick="Print();return false;" />
    </div>
</asp:Content>

