<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="gala_dinner.aspx.cs" Inherits="Hotels2thailand.UI.extranet_gala_dinner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
<link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
<script type="text/javascript" src="../../Scripts/darkman_datepicker.js"></script>

<script type="text/javascript"  language="javascript">
    $(document).ready(function () {
        $("#gala_date").val("");
        DatePicker("gala_date");
        getGalaList();
    });

    function postBack() {

    }
    
    function insertnewGala() {
        var valid = PeriodValidCheck_Single_Gala("gala_insertbox", "gala_date", "", "galaList_checked", "hd_gala_list_", "chk_adult_child", "hd_for_adult_");
       
        if (valid == 0 && ValidateOptionMethod("gala_rate", "number") == true && ValidateOptionMethod("gala_title", "required") == true) {
            

            $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#galaAdd").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_gala_save_insertbox.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {

                if (data == "True") {
                    DarkmanPopUpAlert(450, "Gala dinner is added completely. Thank you.");
                    getGalaList();
                }

                if (data == "method_invalid") {

                    DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
                }
            });
        }
    }

    function getGalaList() {
        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#gala_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });
        $.get("../ajax/ajax_gala_list.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), function (data) {

            $("#gala_list").html(data);

        });
    }

    function DelGala(optionId) {
        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#gala_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        $.get("../ajax/ajax_gala_update_remove.aspx?oid=" + optionId + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

            if (data == "True") {
                DarkmanPopUpAlert(450, "Delete is completed.");
                getGalaList();
            }
            if (data == "method_invalid") {

                DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
            }
        });
        
    }

    function EditGala(priceId, optionId) {
      
        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#gala_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        var url = "../ajax/ajax_gala_editForm.aspx?oid=" + optionId + "&pricId=" + priceId  + GetQuerystringProductAndSupplierForBluehouseManage("append");
        
        $.get(url, function (data) {
            
            DarkmanPopUp(500, data);
            DatePicker("date_gala_form");
        });
    }

    function UpdateGala() {
        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#formbox_buttom").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });
        var post = $("#gala_edit_form").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_gala_edit_save.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {
           

            if (data == "True") {
                DarkmanPopUp_Close();
                DarkmanPopUpAlert(450, "Gala dinner is updated.");
                getGalaList();
            }

            if (data == "method_invalid") {

                DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
            }
        });
    }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<div id="gala_insertbox" class="blogInsert">
<form id="insert_gala" action="">
<h4><asp:Image ID="Image4" runat="server" ImageUrl="~/images/content.png" /> Gala Dinner Insert Box</h4>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr  style=" height:35px; vertical-align: top;">
        <td><label> Gala Title </label></td><td ><input type="text" id="gala_title" name="gala_title" style="width:370px"  class="Extra_textbox" /></td>
        <td><label>Gala Date </label></td><td><input type="text" id="gala_date" name="gala_date" readonly="readonly" style="width:100px" class="Extra_textbox" /></td>
        <td><label>Amount</label></td><td><input type="text" id="gala_rate" name="gala_rate" style="width:100px" class="Extra_textbox_yellow" /></td>
        <td><input type="button" id="galaAdd" value="Add" onclick="insertnewGala();return false;"  class="Extra_Button_small_blue" /></td>
    </tr>
    <tr>
        <td><label>Gala Detail</label></td><td><textarea id="gala_detail" cols="50" name="gala_detail" rows="3" class="Extra_textbox"></textarea></td>
        <td><label>Rate For</label></td>
        <td>
            <table>
                <tr><td><input type="radio" name="chk_adult_child" value="0" checked="checked" /></td><td>For Adult</td></tr>
                <tr><td><input type="radio" name="chk_adult_child" value="1" /></td><td>For Child</td></tr>
            </table>
        </td>
        <td></td>
    </tr>
    
</table>
</form>
</div>

<div id="gala_list" style=" margin:15px 0px 0px 0px;" >
    
</div>

</asp:Content>

