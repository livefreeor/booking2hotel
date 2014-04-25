<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="deposit_manage.aspx.cs" Inherits="Hotels2thailand.UI.extranet_deposit_manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js?ver=001"></script>

<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
<link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
<script type="text/javascript" src="../../Scripts/darkman_datepicker.js"></script>

<script type="text/javascript"  language="javascript">
    $(document).ready(function () {

        //getExtrbedInserBox();
        //getPeriodCurrentMinimumNight();
        $("#dep_date_start").val("");
        $("#dep_date_end").val("");


        DatepickerDual("dep_date_start", "dep_date_end");


        $("#btn_insert_deposit").click(function () {

            var valid = PeriodValidCheck("Deposit_insertBox", "dep_date_start", "dep_date_end", "", "deposit_checked_list", "hd_dep_date_From_", "hd_dep_date_To_");
            alert(valid);
            if (valid == 0 && ValidateOptionMethod("txtAmount", "number") == true) {

                $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#td_btnSaveRate").ajaxStart(function () {
                    $(this).show();
                }).ajaxStop(function () {
                    $(this).remove();
                });

                var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();


                $.post("../ajax/ajax_deposit_save.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {
                 
                    if (data == "True") {
                        DarkmanPopUpAlert(450, "Deposit is added completely. Thank you.");

                        getPeriodCurrentDeposit();
                    }

                    if (data == "method_invalid") {

                        DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
                    }
                });

            }

        });

        getPeriodCurrentDeposit();

    });


    function getPeriodCurrentDeposit() {

        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#deposit_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });
        $.get("../ajax/ajax_deposit_list.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), function (data) {
           
            $("#deposit_list").html(data);

            $("#deposit_list table tr").filter(function (index) {

                if (index > 0) {

                    var dDateStart = $(this).children().find(":text").attr("id");
                    var dDateEnd = $(this).children().next().find(":text").attr("id");

                    DatepickerDual(dDateStart, dDateEnd);
                    //alert(dDateEnd);
                }
            });

        });
    }


    function delDeposit(depId) {


        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#deposit_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        $.get("../ajax/ajax_deposit_del.aspx?depid=" + depId + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
            
            if (data == "True") {
                DarkmanPopUp_Close();

                DarkmanPopUpAlert(450, "Delete is completed.");
                getPeriodCurrentDeposit();
            }

            if (data == "method_invalid") {
                DarkmanPopUp_Close();
                DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
            }

        });
    }

    function DepositUpdate(DepId) {

        var dateStart = "hd_dep_date_From_" + DepId;
        var dateEnd = "hd_dep_date_To_" + DepId;

        var valid = PeriodValidCheck_overlap("deposit_list", DepId, dateStart, dateEnd, "", "deposit_checked_list", "hd_dep_date_From_", "hd_dep_date_To_");

        
        if (valid == 0) {

            $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#deposit_list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_deposit_update.aspx?depid=" + DepId + GetQuerystringProductAndSupplierForBluehouseManage("append"), post, function (data) {

                if (data == "True") {
                    DarkmanPopUpAlert(450, "Deposit is updated.");
                    getPeriodCurrentDeposit();
                }

                if (data == "method_invalid") {
                    DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
                }
            });
        }
    }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="Deposit_insertBox"  class="blogInsert" style="height:70px;" >

<h4><asp:Image ID="Image4" runat="server" ImageUrl="~/images/content.png" /> Deposit Insert Box</h4>

<table cellpadding="0" cellspacing="0" >
                    <tr>
                   
                    
                    <td><label>Date Range From</label>&nbsp;&nbsp;</td>
                    <td><input type="text" id="dep_date_start" readonly="readonly" style="width:100px;" name="dep_date_start" class="Extra_textbox" /></td>
                    <td>&nbsp;&nbsp;<label>To</label>&nbsp;&nbsp;</td>
                    <td><input type="text" id="dep_date_end" readonly="readonly" style="width:100px;" name="dep_date_end" class="Extra_textbox" /></td>
                    <td>&nbsp;&nbsp;<label>Type</label>&nbsp;&nbsp;</td>
                    
                    <td><asp:DropDownList ID="dropDepositCat" runat="server" ClientIDMode="Static" CssClass="Extra_Drop" EnableTheming="false" >
                    <asp:ListItem Value="1" >Paid Before Check in (%)</asp:ListItem>
                    <asp:ListItem Value="2" >Paid Before Check in (night)</asp:ListItem>
                    <asp:ListItem Value="3" >Paid Before Check in (fix)</asp:ListItem>
                    </asp:DropDownList></td>
                    
                    <td>&nbsp;&nbsp;<label>Amont</label>&nbsp;&nbsp;</td>
                    <td><input type="text" id="txtAmount" name="txtAmount"  style="width:50px;" class="Extra_textbox_yellow"  /></td>
                    <td id="td_btnSaveRate">&nbsp;&nbsp;&nbsp;<input type="button" class="Extra_Button_small_blue" value="Add" id="btn_insert_deposit"  /></td>
                    </tr>
                    </table>
</div>
        
    <div id="deposit_list"  style=" margin:15px 0px 0px 0px;">
    
</div>
 
</asp:Content>

