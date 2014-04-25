<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="minimum_night_control.aspx.cs" Inherits="Hotels2thailand.UI.extranet_minimum_night_control" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js?ver=015"></script>

<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
<link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
<script type="text/javascript" src="../../Scripts/darkman_datepicker.js"></script>

<script type="text/javascript" language="javascript">
    $(document).ready(function () {

        //getExtrbedInserBox();
        getPeriodCurrentMinimumNight();
        $("#min_date_start").val("");
        $("#min_date_end").val("");

        
        DatepickerDual("min_date_start", "min_date_end");


        $("#min_day_amount").html(SelDataBind(30));

        $("#btn_insert_new_minimum").click(function () {

            var valid = PeriodValidCheck("Minimum_insertBox", "min_date_start", "min_date_end", "", "minimum_checked_list", "hd_min_date_From_", "hd_min_date_To_");
            
            if (valid == 0) {

                $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#td_btnSaveRate").ajaxStart(function () {
                    $(this).show();
                }).ajaxStop(function () {
                    $(this).remove();
                });

                var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();


                $.post("../ajax/ajax_condition_minimum_save.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {

                    if (data == "True") {
                        DarkmanPopUpAlert(450, "Minimum Night is added completely. Thank you.");

                        getPeriodCurrentMinimumNight();
                    }

                    if (data == "method_invalid") {

                        DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
                    }
                });

            }


        });
    });



        function delMinimum(minDayId) {
           

            $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#minimum_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        $.get("../ajax/ajax_condition_minimum_del.aspx?minid=" + minDayId + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
          
            if (data == "True") {
                DarkmanPopUp_Close();

                DarkmanPopUpAlert(450, "Delete is completed.");
                getPeriodCurrentMinimumNight();
            }

            if (data == "method_invalid") {
                DarkmanPopUp_Close();
                DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
            }

        });
    }


    function getPeriodCurrentMinimumNight() {

        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#minimum_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        
        var ConditionId = $("#hdConditionID").val();

        $.get("../ajax/ajax_condition_minimum_night_list.aspx?conid=" + ConditionId + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

            $("#minimum_list").html(data);

            //            alert($("#minimum_list table tr").length);

            $("#minimum_list table tr").filter(function (index) {

                if (index > 0) {

                    var dDateStart = $(this).children().find(":text").attr("id");
                    var dDateEnd = $(this).children().next().find(":text").attr("id");
                    
                    DatepickerDual(dDateStart, dDateEnd);
                    //alert(dDateEnd);
                }
            });

        });
    }

   

    function MinDayUpdate(minDayId, conditionId) {
        var dateStart = "hd_min_date_From_" + minDayId;
        var dateEnd = "hd_min_date_To_" + minDayId;


        //alert(dd + "----" + de);

        var valid = PeriodValidCheck_overlap("minimum_list", minDayId, dateStart, dateEnd, "", "minimum_checked_list", "hd_min_date_From_", "hd_min_date_To_");
        if (valid == 0) {

            $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#minimum_list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();



            //alert(valid);

            $.post("../ajax/ajax_condition_minimum_update.aspx?minid=" + minDayId + "&conid=" + conditionId + GetQuerystringProductAndSupplierForBluehouseManage("append"), post, function (data) {

                if (data == "True") {
                    DarkmanPopUpAlert(450, "Minimum day is updated.");
                    getPeriodCurrentMinimumNight();
                }

                if (data == "method_invalid") {
                    DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
                }
            });
        }
        
    }
   

</script>
<style  type="text/css">

.option_title
{
     margin:5px 0px 0px 2px;
     padding:2px 0px 5px 0px;
     font-size:14px;
     font-weight:bold;
     /*color:#68a64c;*/
      /*border-bottom:1px solid #e4e5e9;*/
     /*width:350px;*/
     
       color:#3b5998;
     
 }
 .ConditionTitle
 {
     margin:0px 0px 0px 0px;
     padding:0px 0px 0px 0px;
     font-size:14px;
     font-weight:bold;
      color:#3b5998;
    
      
 }
 .head_condition
 {
     margin:10px 0px 10px 0px;
     padding: 10px;
     background-color:#f2f2f2;
     
     border:1px solid #d6d6d6;
 }
  .head_condition label
 {
     font-weight:bold;
 }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="head_condition"><label>Condition: </label><asp:Label ID="lblTitleCondition" runat="server" CssClass="ConditionTitle"></asp:Label><br /><br /><label>Room Type: </label> <asp:Label CssClass="option_title" ID="lblTitle" runat="server"></asp:Label></div>
<asp:HiddenField ID="hdConditionID" ClientIDMode="Static"  runat="server" />
<div id="Minimum_insertBox"  class="blogInsert">

<h4><asp:Image ID="Image4" runat="server" ImageUrl="~/images/content.png" /> Minimum Night Insert Box</h4>

<table>
                    <tr>
                   
                    
                    <td><label>Date Range From</label></td>
                    <td><input type="text" id="min_date_start" readonly="readonly" name="min_date_start" class="Extra_textbox" /></td>
                    <td>&nbsp;&nbsp;<label>To</label></td>
                    <td><input type="text" id="min_date_end" readonly="readonly" name="min_date_end" class="Extra_textbox" /></td>

                    <td>&nbsp;&nbsp;<label>Minimum</label></td>
                    <td>
                    <select id="min_day_amount" name="min_day_amount" class="Extra_Drop"></select>
                    </td>
                    <td><label>Night(s)</label></td>
                    <td id="td_btnSaveRate">&nbsp;&nbsp;&nbsp;<input type="button" class="Extra_Button_small_blue" value="Add" id="btn_insert_new_minimum"  /></td>
                    </tr>
                    </table>
</div>

<div id="minimum_list"  style=" margin:15px 0px 0px 0px;">
    
</div>
</asp:Content>

