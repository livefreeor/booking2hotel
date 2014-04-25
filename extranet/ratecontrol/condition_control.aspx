<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="condition_control.aspx.cs" Inherits="Hotels2thailand.UI.extranet_Condition_Control" %>

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

        $("#btn_search_condition").click(function () {
            GetConditionList("");
        });

        var qOptionId = GetValueQueryString("oid");
        if (qOptionId != "") {
            GetConditionList(qOptionId);
            $("#dropOptionList").val(qOptionId);
        }

    });

    function GetConditionList(qOptionId) {

        if ($("#img_progress").length) {
            $("#img_progress").remove();
        }
        $("<img id=\"img_progress\" class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertBefore("#condition_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });


        if (qOptionId == "") {
            qOptionId = $("#dropOptionList").val();
        }
        

        $.get("../ajax/ajax_condition_list.aspx?oid=" + qOptionId + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
            
            $("#condition_list").html(data);
            tooltip();
        });
        
    }

    function delCondition(conditionId) {


        if ($("#img_progress").length) {
            $("#img_progress").remove();
        }


        $("<img id=\"img_progress\" class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertBefore("#condition_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        $.get("../ajax/ajax_condition_del.aspx?conid=" + conditionId + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
           
            if (data == "True") {
                GetConditionList("");
            }

            if (data == "method_invalid") {
                DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
            }
        });
    }

    function EditCondition(encodeConditionId) {
        var OptionIdVal = $("#dropOptionList").val();
        if (GetValueQueryString("pid") == "" && GetValueQueryString("supid") == "") {
            window.location.href = "condition_manage.aspx?cdid=" + encodeConditionId + "&oid=" + OptionIdVal;
        }
        else {
            window.location.href = "condition_manage.aspx?pid=" + GetValueQueryString("pid") + "&supid=" + GetValueQueryString("supid") + "&cdid=" + encodeConditionId + "&oid=" + OptionIdVal;
        }
        
    }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div id="mainExtranet_form">
 <div id="condition_room_select" class="blogInsert">
 <h4><asp:Image ID="imgContent" runat="server" ImageUrl="~/images/content.png" /> Condition search box</h4>
    <table>
        <tr>
            <td><asp:DropDownList ID="dropOptionList"  Width="350px" runat="server"  ClientIDMode="Static" EnableTheming="false" CssClass="Extra_Drop" ></asp:DropDownList></td>
            <td><input type="button" value="Search" id="btn_search_condition" class="Extra_Button_small_blue" /></td>
        </tr>
    </table>
 </div>
    <div id="condition_list" style="margin:15px 0px 0px 0px"></div>

    

    

</div>



</asp:Content>

