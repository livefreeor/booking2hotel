<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="allotment_control.aspx.cs" Inherits="Hotels2thailand.UI.extranet_allotment_allotment_control" %>

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
        $("#insert_date_start").val("");
        DatepickerDual("insert_date_start", "insert_date_end");


        $("#custom_room :checked").removeAttr("checked");

        $("#customadd").click(function () {

            if ($("#valid_alert_allot_insert_box").length) {

                $("#allot_insert_box").css("background-color", "#f7f7f7");


                $("#valid_alert_allot_insert_box").fadeOut('fast', function () {

                    $(this).remove();

                });
            }


            if ($("#custom_room").css("display") == "none") {
                $("#select_room").slideUp('fast', function () {
                    $("#custom_room").slideDown();
                });

                $(this).html("Cancel");
            }
            else {
                $("#custom_room").slideUp('fast', function () {
                    $("#select_room").slideDown();
                });

                $("#custom_room :checked").removeAttr("checked");
                $(this).html("Custom select room");
            }


            return false;
        });
    });

    
    
    function insertnewallotment() {

        var result = DateCompareValid("allot_insert_box", "insert_date_start", "insert_date_end", "");
        if (result >= 0) {
            $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#allot_insert_box").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            var roomval = "";
            var RoomCustomeChecked = $("#custom_room :checked").each(function () {
                roomval = roomval + $(this).val() + ",";
            });

            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_allotment_save_insertbox.aspx?custom=" + roomval + GetQuerystringProductAndSupplierForBluehouseManage("append"), post, function (data) {

                if (data == "True") {
                    DarkmanPopUpAlert(450, "Allotment is added completely. Thank you.");
                }

                if (data == "method_invalid") {
                    DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
                }
            });
        }
    }

    

    function Autofill() {




        if (key == "room") {
            var roomallot = $("#room_allot_autofill").val();
            $("#allot_manage_list select[name^='room_allot_']").val(roomallot);
        }

        if (key == "cutoff") {
            var cutoffallot = $("#cutoff_allot_autofill").val();
            $("#allot_manage_list select[name^='cutoff_allot_']").val(cutoffallot);

        }

        if (key == "close") {

            var closeout = $("#close_out_autofill").val();

            $("#allot_manage_list :radio[name^='radio_status_']").filter(function (index) { return $(this).attr("value") == closeout }).attr("checked", "checked");
        }


       
       
    }

    
    function resetAutofill() {
        searchAllotManage();
    }

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="allot_insert_box" class="blogInsert" >
<h4><asp:Image ID="imgContent" runat="server" ImageUrl="~/images/content.png" /> Allotment Insert Box</h4>

    

    <div id="allot_room_select"  >   
    <fieldset style="border:0px;">
     <legend style="color:#000000; margin-bottom:5px;">Room Type  &nbsp;&nbsp;&nbsp; <a href="" id="customadd"  >Custom select room</a></legend>
       <div id="custom_room"  style="display:none; border-bottom:1px solid #ccccc1;  border-top:1px solid #ccccc1;  padding-bottom:10px; padding-top:5px;"><asp:CheckBoxList ID="chkRoom" runat="server" BackColor="#ebf0f3" Width="100%" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ClientIDMode="Static"></asp:CheckBoxList></div>
        <div id="select_room"><asp:DropDownList ID="dropOptionInsert" Width="450px" CssClass="Extra_Drop" EnableTheming="false" runat="server" ClientIDMode="Static"></asp:DropDownList> </div>
        <p style="margin:5px 0px 0px 0px; padding:0px 0px 0px 0px;"></p>
       </fieldset>
    </div>

    <table width="100%">
        
        <tr>
        <td><label>Date Range From</label></td>
        <td><input type="text" class="Extra_textbox" readonly="readonly" id="insert_date_start"  name="insert_date_start" /></td>
        <td><label>To</label></td>
        <td><input type="text" class="Extra_textbox" readonly="readonly" id="insert_date_end" name="insert_date_end" /></td>
        <td><label>Cut off </label><asp:DropDownList ID="dropCutoff"  CssClass="Extra_Drop" EnableTheming="false" runat="server" ClientIDMode="Static"></asp:DropDownList></td>
        <td><label>Allotment (Room) </label><asp:DropDownList ID="dropAllot"  CssClass="Extra_Drop" EnableTheming="false" runat="server" ClientIDMode="Static"></asp:DropDownList> </td>
        <td><input type="button" value="Add" onclick="insertnewallotment();return false;" class="Extra_Button_small_blue" /></td>
        <%--<td><input type="button" id="btnCancel" value="Cancel" onclick="InsertCancel();return false;"  class="Extra_Button_small_white"/></td>--%>
        </tr>   
    </table>
</div>


<div id="allot_manage_list"></div>

</asp:Content>

