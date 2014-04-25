<%@ Page Title="Hotels2thailand:Booking Acknowledge" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="acknowledge_control.aspx.cs" Inherits="Hotels2thailand.UI.extranet_acknowledge_control" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
<script type="text/javascript" src="/js/jquery-ui-1.7.2.custom.min.js"></script> 
<link href="/css/acknowledge.css" type="text/css" rel="stylesheet" />
<link rel="stylesheet" type="text/css" href="/css/redmond2/jquery-ui-1.7.3.custom.css" media="screen" />
<script type="text/javascript" src="/js/calendar_acknow.js"></script> 
<script language="javascript" type="text/javascript">
    $(document).ready(function () {

        $("#extranet_tab li").each(function (index) {
            $(this).click(function () {
                $("#extranet_tab li").removeClass("active");
                $("#displayResult").fadeOut();
                $(this).addClass("active");
                qsFilter = "?act=" + index;
                qsFilter = qsFilter + "&sort=1";
                GetAckList(qsFilter);
                return false;
            });
        });

        

        $("#btnQSearch").click(function () {
            typeSearch = $("#type_search").val();
            textKeyword = $("#txtKeyword").val();


            if (typeSearch != 0) {
                if (textKeyword != "") {
                    qsFilter = "?ct=" + typeSearch + "&k=" + textKeyword + "&act=0";
                    qsFilter = qsFilter + "&sort=1";
                    GetAckList(qsFilter);
                } else {
                    alert("Please enter keyword");
                }
            } else {
                alert("Please select type of search");
            }
        });

        $("#btnAckSearch").click(function () {
            dateCheckIn = $("#date_startInput").val();
            dateCheckOut = $("#date_endInput").val();
            dateRecieveIn = $("#date_start2Input").val();
            dateRecieveOut = $("#date_end2Input").val();
            extranet_status = $("#status_extranet").val();
            sort_by = $("#sortBy").val();
            qsFilter = "?act=" + extranet_status;

            if (dateCheckIn != "" && dateCheckOut != "") {
                qsFilter = qsFilter + "&date_in=" + getDateformat(dateCheckIn) + "&date_out=" + getDateformat(dateCheckOut);
            }
            if (dateRecieveIn != "" && dateRecieveOut != "") {
                qsFilter = qsFilter + "&rdate_in=" + getDateformat(dateRecieveIn) + "&rdate_out=" + getDateformat(dateRecieveOut);
            }
            qsFilter = qsFilter + "&sort=" + sort_by;

            GetAckList(qsFilter);

        });

        $("#acknow_advance_search").click(function () {
            $("#acknow_advance_form").slideToggle();
        });

        qsFilter = "?act=0";
        qsFilter = qsFilter + "&sort=1";

        var qFromdash = GetValueQueryString("ack_tpye");
        if (qFromdash != "") {
            $("#extranet_tab li").removeClass("active");
            $("#extranet_tab li").filter(function (index) { return index == parseInt(qFromdash) }).addClass("active");

            qsFilter = "?act=" + qFromdash;
            qsFilter = qsFilter + "&sort=1";
        }
        


        CreateCalendar();
        GetAckList(qsFilter);
    });

    function GetAckList(qsFilter) {
        $("<img class=\"img_progress\" src=\"/images_extra/preloader.gif\" alt=\"Progress\" />").appendTo("#ackBody").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });
        //alert("/extranet/ajax/bookingAckReport.aspx" + qsFilter);
        $.ajax({
            url: "/extranet/ajax/bookingAckReport.aspx" + qsFilter + GetQuerystringProductAndSupplierForBluehouseManage("append"),
            cache: false,
            dataType: "html",
            success: function (data, textStatus, XMLHttpRequest) {
                if (data != "") {
                    $("#displayResult").html(data).fadeIn();
                    $(".ackButton").click(function () {
                        $("#extranet_tab li").filter(function (index) { return index == 1 }).trigger("click");
                    });
                    $(".cancelButton").click(function () {
                        $("#extranet_tab li").filter(function (index) { return index == 3 }).trigger("click");
                    });
                }
            }
        });
    }

    function saveBooking() {
        var post = $("#frmSave").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
        $.post("/extranet/ajax/acknowledge_pcs.aspx", post, function (data) {
            $("#btnAckSearch").trigger("click");
        });
    }
    function AcknowledgeSave() {
        $("<img class=\"img_progress\" src=\"/images_extra/preloader.gif\" alt=\"Progress\" />").appendTo("#ackBody").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        var post = $("#Ack_update").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
        $.post("/extranet/ajax/ajax_booking_ack_save.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {
            
            if (data == "True") {

                DarkmanPopUp_Close();
            }
            if (data == "not") {

                DarkmanPopUpAlert(450, "can not update!");
            }
        });
    }

    function GetBookingDetail(id) {
        $("<img class=\"img_progress\" src=\"/images_extra/preloader.gif\" alt=\"Progress\" />").appendTo("#ackBody").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        //alert(id);
        $.get("/extranet/ajax/ajax_booking_detail.aspx?bpid=" + id + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
            DarkmanPopUp(800, data);
            // alert(data);
        });
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="acknow_search">


Search By: 
<select name="type_search" id="type_search">
<option value="0">Please Select</option>
<option value="1">Booking ID</option>
<option value="2">Acknowledge ID</option>
<option value="3">Guest Name</option>
<option value="4">Email</option>
</select> 
: <input type="text" id="txtKeyword" name="txtKeyword" class="Extra_textbox" /> <input type="button" id="btnQSearch" value="Search" /><br />

<span id="acknow_advance_search">Advance Search <img src="/images/ico_blue_more.jpg" /></span>
    
    
  <div id="acknow_advance_form">
    <table border="0" cellpadding="3" id="ack_search_box" >
      <tr>
        <td>Recieve Date</td>
        <td>From</td>
        <td><input type="text" name="date_startInput" id="date_startInput" class="vPicker" rel="" /></td>
        <td>To</td>
        <td><input type="text" name="date_endInput" id="date_endInput" class="vPicker" rel="" /></td>
      </tr>
      <tr>
        <td>Check in Date</td>
        <td>From</td>
        <td><input type="text" name="date_start2Input" id="date_start2Input" class="vPicker" rel="" /></td>
        <td>To</td>
        <td><input type="text" name="date_end2Input" id="date_end2Input" class="vPicker" rel="" /></td>
      </tr>
      <tr>
        <td>Action</td>
        <td colspan="4">
        <select id="status_extranet">
         <option value="0">All Booking</option>
        <option value="1">Waiting for Confirm Acknowledge</option>
        <option value="2">Acknowledge Confirm Completed</option>
        <option value="3">Waiting for Confirm Cancel</option>
        <option value="4">Cancel Completed</option>
        </select></td>
      </tr>
    </table>
        <div id="acknow_submit">
            <p><span>Order by</span>
            <select id="sortBy" class="sortBy">
            <option value="1">Booking ID</option>
            <option value="2">Guest Name</option>
            <option value="3">Request Date</option>
            <option value="4">Check In Date</option>
            </select>
            </p>
            <input type="button" id="btnAckSearch" value="Search now" style="font-weight:bold;color:#FFF;width:160px; height:30px; border:2px solid #093; background-color:#5d9119; margin-top:10px; cursor:pointer;" />
        </div>
    </div>
    
    <ul id="extranet_tab">
    	<li class="active">All Booking</li>
    	<li>Waiting for Confirm Acknowledge</li>
    	<li>Acknowledge Confirm Completed</li>
        <li>Waiting for Confirm Cancel</li>
        <li>Cancel Completed</li>
    </ul>
    <br style="clear:both;" />
  <div style="background-color:#f2f2f2" id="ackBody"><div id="displayResult"></div></div>
</div>

</asp:Content>

