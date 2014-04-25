<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="booking_allot_report.aspx.cs" Inherits="Hotels2thailand.UI.admin_booking_allot_report" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
<link rel="stylesheet" type="text/css" href="/css/jquery.autocomplete.css">
<link href="/css/vstyle.css" type="text/css" rel="stylesheet" />
<link rel="stylesheet" type="text/css" href="/css/redmond2/jquery-ui-1.7.3.custom.css" media="screen" />
<script type="text/javascript" language="javascript" src="/js/jquery-ui-1.8.11.custom.min.js"></script>   
<script type="text/javascript" language="javascript"  src="/js/ht2th.ui.v1.js"></script>
<script language="javascript"   src="/js/jquery-1.4.2.min.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript"  src="/js/jquery-ui-1.7.2.custom.min.js"></script> 
<script type="text/javascript" src="/js/jquery.autocomplete.js"></script>  

<script type="text/javascript" language="javascript"  src="/js/calendar_acknow.js"></script> 
<script language="javascript"   type="text/javascript">
    var tab_index = 0;

    $(document).ready(function () {
        $("#extranet_tab li").each(function (index) {
            $(this).click(function () {
                tab_index = index;

                if (index == 0) {
                    $("#ddLocation").show();
                } else {
                    $("#ddLocation").hide();
                }
                $("#extranet_tab li").removeClass("active");

                $(this).addClass("active");

                return false;
            });
        });

        var result = $.get("/admin/front/ajax_front_search_suggest.aspx", function (data) {
            arrData = data.split(";!;");
            $("#txtSearch").autocomplete(arrData);
        });

        $("#btnAckSearch").click(function () {
            dateCheckIn = $("#date_startInput").val();
            dateCheckOut = $("#date_endInput").val();
            destination = $("#dest").val();
            quantityRoom = $("#roomQuantity").val();
            titleKeyword = $("#txtSearch").val();
            isAvailable = "";
            if ($("#chkAvai").is(':checked')) {
                isAvailable = "&hasRoom=1";
            }
            if (titleKeyword != "") {
                titleKeyword = "&k=" + titleKeyword;
            }
            qsFilter = "?cat=" + getProductType(tab_index) + "&dest=" + destination + "&qty=" + quantityRoom + isAvailable + titleKeyword;
            if (tab_index == 0) {

                loctate = $("#loc").val();
                qsFilter = qsFilter + "&loc=" + loctate;
            }


            if (dateCheckIn != "" && dateCheckOut != "") {
                qsFilter = qsFilter + "&date_in=" + getDateformat(dateCheckIn) + "&date_out=" + getDateformat(dateCheckOut);
            } else {
                alert("Please enter date");
                return false;
            }

            adminKey = GetValueQueryString("l6fpvf");
            if (adminKey != "") {

                qsFilter = qsFilter + "&l6fpvf=" + adminKey;
            }

            qsFilter = qsFilter + "&sortBY=" + $("#sortBy").val();

            $("#displayResult").html("<img class=\"img_progress\" src=\"/images/preloader.gif\" alt=\"Progress\" /> Please wait...").show();
            getStat(qsFilter);
        });

        $("#acknow_advance_search").click(function () {
            $("#acknow_advance_form").slideToggle();
        });
        getXMLLocationToDropDown(29);
        CreateCalendar();
    });

    function getStat(qsFilter) {

        $.ajax({
            url: "/admin/ajax/ajax_booking_recommendReport.aspx" + qsFilter,
            cache: false,
            dataType: "html",
            timeout: 180000,
            success: function (data, textStatus, XMLHttpRequest) {
                if (data != "") {

                    $("#displayResult").html(data).fadeIn(1000);
                    tooltip();
                }
            }, error: function (x, t, m) {
                if (t === "timeout") {
                    alert("got timeout");
                } else {
                    alert(t);
                }
            }
        });
    }
    function getXMLLocationToDropDown(productCate) {

        var category = null;
        $.ajax({
            type: "GET",
            url: "/location.xml",
            dataType: "xml",
            success: function (xml) {
                categoryRoot = $(xml).find('ProductCategory').filter(function () {
                    return $(this).attr('id') == productCate
                });

                getDestinationDropDown(categoryRoot)

            }

        });
    }

    function getDestinationDropDown(categoryRoot) {

        //var destinationFirst=categoryRoot.find('Destination').stop().attr('id');
        var destinationFirst = 0;
        var destinationList = '<select id="dest" class="dd_dest">';
        destinationList = destinationList + '<option value="0">Select Destination</option>';
        categoryRoot.find('Destination').each(function () {
            destinationList = destinationList + '<option value="' + $(this).attr('id') + '">' + $(this).attr('title') + '</option>';
        });
        destinationList = destinationList + '</select>';
        $("#ddDestination").html(destinationList);

        //set event onchange

        if (categoryRoot.attr('id') == 29) {

            $("#ddLocation").show();
            $(".dateCheckoutBox").show();
            $("#dest").change(function () {
                getLocationDropDown(categoryRoot, $(this).val())
            });
            getLocationDropDown(categoryRoot, destinationFirst);
        } else {

            $("#ddLocation").hide();
        }

    }

    function getProductType(index) {
        result = 29;
        switch (index) {
            case 0:
                result = 29;
                break;
            case 1:
                result = 32;
                break;
            case 2:
                result = 34;
                break;
            case 3:
                result = 36;
                break;
            case 4:
                result = 38;
                break;
            case 5:
                result = 39;
                break;
            case 6:
                result = 40;
                break;
        }

        return result;
    }
    function GetValueQueryString(key, default_) {
        if (default_ == null) default_ = "";
        key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
        var qs = regex.exec(window.location.href);
        if (qs == null)
            return default_;
        else
            return qs[1];
    }
    function getLocationDropDown(categoryRoot, destination_id) {


        locationList = categoryRoot.find('Destination').filter(function () {
            return $(this).attr('id') == destination_id
        });

        var ddLocation = '<select id="loc" class="dd_loc">';
        if (destination_id == 0) {
            ddLocation = ddLocation + '<option value="-1">Select Location</option>';
        } else {
            ddLocation = ddLocation + '<option value="0">All</option>';
        }

        locationList.find('Location').each(function () {
            ddLocation = ddLocation + '<option value="' + $(this).attr('id') + '">' + $(this).text() + '</option>';
        });
        ddLocation = ddLocation + '</select>';
        $("#ddLocation").html(ddLocation);


    }
   
</script>
<style type="text/css">
body
{
	font-family:Verdana, Geneva, sans-serif;
	font-size:12px;
}
#divMain{
	width:960px;
	margin:0 auto;
}
.tblListResult
{
	width:960px;
	background-color:#d8dfea;
}
.tblListResult td
{
	background-color:#ffffff;
	padding:3px;
	height:20px;
	line-height:25px;
}
.tblListResult .rowOdd td
{
		background-color:#ffffff;
		
}
.tblListResult .rowEven td
{
		background-color:#f2f2f2;
}

.tblListResult th
{
	font-family:verdana;
	font-size:15px;
	
	color:#004f4f;
	line-height:30px;
	background:#f4f4f4;
}
.tblListResult .productTitle
{
	color:#004f4f;
	background-color:#f2f2f2;
	font-size:14px;
}

#tooltip {
	color:#333;
	background:#fff9e7;
	/*opacity: .8;*/
	border:1px solid #f6b402;
	display:none; /*--Hides by default--*/
	padding:10px;
	position:absolute;	z-index:1000;
	-webkit-border-radius: 3px;
	-moz-border-radius: 3px;
	border-radius: 3px;
}
.tooltip{color:#397c96; text-decoration:none}
.tooltip_content{
display:none;	
}
ul.condition_list{padding:0px;margin:0px;}
ul.condition_list li{padding:0px;margin:0px;font-size:12px; font-family:Verdana, Geneva, sans-serif; font-weight:normal; list-style:none;}
.priceGrandSeiling{color:#e84b00}
.priceGrandNet{color:#397c96}
</style>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="divMain">
<div id="acknow_search">
    <ul id="extranet_tab">
    	<li class="active">Hotels</li>
    	<li>Golf Couses</li>
    	<li>Day Trips</li>
        <li>Water Activity</li>
        <li>Shows & Event</li>
        <li>Health Checkup</li>
        <li>Spa</li>
    </ul>
    <br style="clear:both; line-height:1px;" />
    <div id="acknow_advance_form">
    <table border="0" cellpadding="3" id="ack_search_box" >
     <tr>
        <td colspan="2">Destination</td>
        <td><div id="ddDestination"></div></td>
        <td>Location</td>
        <td><div id="ddLocation"></div></td>
      </tr>
      <tr>
        <td>Check in date</td>
        <td>from</td>
        <td><input type="text" name="date_startInput" id="date_startInput" class="vPicker" rel="" /></td>
        <td align="right">to</td>
        <td><input type="text" name="date_endInput" id="date_endInput" class="vPicker" rel="" /></td>
      </tr>
     <tr>
        <td colspan="2">Quantity</td>
        <td>
        <select id="roomQuantity" name="roomQuantity">
        <option value="1">1</option>
        <option value="2">2</option>
        <option value="3">3</option>
        <option value="4">4</option>
        <option value="5">5</option>
        <option value="6">6</option>
        <option value="7">7</option>
        <option value="8">8</option>
        <option value="9">9</option>
        <option value="10">10</option>
        </select>
        </td>
        <td align="right"><input type="checkbox" id="chkAvai" name="chkAvai"/></td><td>Room available</td>
      </tr>
      <tr><td colspan="2">Product Title: </td><td colspan="3"><input type="text" id="txtSearch" name="txtSearch" style="width:400px" /></td></tr>
    </table>
        <div id="acknow_submit">
           <!--<p class="sortBy"><span>Order by</span>
            <select id="sortBy">
            <option value="1">Title A-Z</option>
            <option value="2">Title Z-A</option>
            <option value="3">Booking All A-Z</option>
            <option value="4">Booking All Z-A</option>
            <option value="5">Revenue All A-Z</option>
            <option value="6">Revenue All Z-A</option>
            <option value="7">Booing Paid A-Z</option>
            <option value="8">Booking Paid Z-A</option>
            <option value="9">Revenue Paid A-Z</option>
            <option value="10">Revenue Paid Z-A</option>
            <option value="11">Booking Complete A-Z</option>
            <option value="12">Booking Complete Z-A</option>
            <option value="13">Revenue Complete A-Z</option>
            <option value="14">Revenue Complete Z-A</option>
            </select>
            </p>-->
            <input type="button" value="Search Now" id="btnAckSearch" style="font-family:Verdana, Geneva, sans-serif;font-size:20px; margin-left:80px;color:#FFF;width:160px; height:50px; border:2px solid #093; background-color:#5d9119; cursor:pointer;" />
    	</div>
  </div>
  <div style="background-color:#f2f2f2" id="ackBody">
      <div id="displayResult">

      </div>
  </div>
</div>
</div>

</asp:Content>