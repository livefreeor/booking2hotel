<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="allotment_edit.aspx.cs" Inherits="Hotels2thailand.UI.extranet_allotment_allotment_edit" %>

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

    var qOptionId = GetValueQueryString("oid");
    var qMonth = GetValueQueryString("month");
    var qYear = GetValueQueryString("y");

    $(document).ready(function () {
        $("#edit_date_start").val("");
        DatepickerDual("edit_date_start", "edit_date_end");

        $("#custom_room :checked").removeAttr("checked");


        $("#customadd").click(function () {

            if ($("#valid_alert_allot_edit_search_box").length) {
                $("#allot_edit_search_box").css("background-color", "#f7f7f7");


                $("#valid_alert_allot_edit_search_box").fadeOut('fast', function () {

                    $(this).remove();

                });
            }

            if ($("#custom_room").css("display") == "none") {

                //valid_alert_



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



        if (qOptionId != "" && qMonth != "" && qYear != "") {


            //var dDateFirstDateOfMonth = new Date(qYear, qMonth,1);
            var dDateLastDateofMount = new Date(qYear, qMonth - 1, LastDayOfMonth(qYear, qMonth - 1));

            var firstDate = qYear + "-" + qMonth + "-1";
            var LastDate = dDateLastDateofMount.getFullYear() + "-" + (dDateLastDateofMount.getMonth() + 1) + "-" + dDateLastDateofMount.getDate();


            $("#edit_date_start").val(firstDate);
            $("#edit_date_end").val(LastDate);
            $("#dropOption").val(qOptionId);

            $("#customadd").hide();

            DatePicker_manual("edit_date_start");
            DatePicker_manual("edit_date_end");

            searchAllotManage();
        }


    });

   
    function searchAllotManage() {

        //allot_edit_search_box
        var result = DateCompareValid("allot_edit_search_box", "edit_date_start", "edit_date_end", "");
        if (result >= 0) {
            $("<img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertBefore("#allot_manage_list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });
            var roomval = "";
            if (qOptionId != "" && qMonth != "" && qYear != "") {
                
                roomval = qOptionId + ",";
            } else {
                var RoomCustomeChecked = $("#custom_room :checked").each(function () {
                    roomval = roomval + $(this).val() + ",";
                });
            }
            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_allotment_list.aspx?custom=" + roomval + GetQuerystringProductAndSupplierForBluehouseManage("append"), post, function (data) {
               
                $("#allot_manage_list").html(data);
            });
        }
        
    }

    function Autofill(key) {
        console.log(key);
       //var roomallot = $("#room_allot_autofill").val();
       //var cutoffallot = $("#cutoff_allot_autofill").val();
       //var closeout = $("#close_out_autofill").val();

       ////alert(roomallot + "--" + cutoffallot + "--" + closeout);

       //$("#allot_manage_list select[name^='room_allot_']").val(roomallot);
       //$("#allot_manage_list select[name^='cutoff_allot_']").val(cutoffallot);

       //$("#allot_manage_list input[value^='" + closeout + "']").attr("checked", "checked");
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
            console.log(closeout);
            $("#allot_manage_list :radio[name^='radio_status_']").filter(function (index) { return $(this).attr("value") == closeout }).attr("checked", "checked");
        }


       
    }

    function SaveeditAllotment() {
        $("<img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertAfter("#allot_manage_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        
        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_allotment_save_edit.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {
            //alert(data);
            if (data == "True") {
                DarkmanPopUpAlert(450, "Allotment is updated");
                searchAllotManage();
            }
            if (data == "method_invalid") {
                DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
            }

        });
    }


    function resetAutofill() {
        searchAllotManage();
    }

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<div id="allot_edit_search_box" class="blogInsert">
 <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Allotment Search Box</h4>

 <div id="allot_room_select"  >   
    <fieldset style="border:0px;">
     <legend style="color:#000000; margin-bottom:5px;">Room Type  &nbsp;&nbsp;&nbsp; <a href="" id="customadd"  >Custom select room</a></legend>
       <div id="custom_room"  style="display:none; border-bottom:1px solid #ccccc1;  border-top:1px solid #ccccc1;  padding-bottom:10px; padding-top:5px;">
       <asp:CheckBoxList ID="chkRoom" runat="server" ClientIDMode="Static" BackColor="#ebf0f3" Width="100%" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px"></asp:CheckBoxList></div>
        <div id="select_room"><asp:DropDownList ID="dropOption" Width="450px" CssClass="Extra_Drop" EnableTheming="false" runat="server" ClientIDMode="Static"></asp:DropDownList> </div>
        <p style="margin:5px 0px 0px 0px; padding:0px 0px 0px 0px;"></p>
       </fieldset>
    </div>

    <table width="70%">
        <tr>
        <td><label>Date Range From</label></td>
        <td><input type="text" class="Extra_textbox" readonly="readonly" id="edit_date_start"  name="edit_date_start" /></td>
        <td><label>To</label></td>
        <td><input type="text" class="Extra_textbox"  readonly="readonly" id="edit_date_end" name="edit_date_end" /></td>
        <td><input type="button" value="Search" onclick="searchAllotManage();return false;" class="Extra_Button_small_blue" /></td>
        
        </tr>   
    </table>
</div>

<div id="allot_manage_list" style="margin:15px 0px 0px 0px;">


</div>

</asp:Content>

