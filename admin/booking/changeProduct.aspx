<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changeProduct.aspx.cs" Inherits="admin_booking_changeProduct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add New Product</title>
    <link rel="stylesheet" type="text/css" href="/css/smoothness/jquery-ui-1.7.2.custom.css"/>
    
<style type="text/css">
.ui-datepicker{width:200px;font-size:11px;}
</style>
<script language="javascript" src="/js/jquery-1.4.2.min.js" type="text/javascript"></script>
<script type="text/javascript" src="/js/jquery-ui-1.7.2.custom.min.js"  language="javascript"></script>   
<script type="text/javascript" src="/js/date.js" language="javascript"></script>
<script language="javascript" type="text/javascript">
    function bhtDateDiff(dateStart, dateEnd) {
        return (Math.ceil((dateEnd.getTime() - dateStart.getTime()) / (24 * 60 * 60 * 1000)));
    }
    $(function () {
        var dateStart = new Date().addDays(1);
        var dateEnd = new Date().addDays(3);

        $("#dateStart").datepicker({ minDate: 1, buttonImage: '../images/ico_calendar.gif' })


        $("#dateEnd").datepicker({ minDate: 2 })

        $("#dateStart").change(function () {
            $("#dateEnd").datepicker("destroy");
            $("#dateEnd").datepicker({ minDate: bhtDateDiff(new Date(), $("#dateStart").datepicker("getDate")) + 1 })


        });


    });
    function pickCalendar(element) {
        $("#" + element + "").focus();
    }
    function clickButton(e, buttonid) {
        var evt = e ? e : window.event;
        var bt = document.getElementById(buttonid);
        if (bt) {
            if (evt.keyCode == 13) {
                bt.click();
                return false;
            }
        }
    }
    function SearchHotel() {
        
        var KeyWord = $("#txtSearch").val();
        var ProductCat = $("#dropProductCat").val();
        var DesId = $("#dropDestination").val();
        $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").prependTo("#hotelresult").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });
        $.get("../ajax/ajax_booking_product_search_addProduct.aspx?Key=" + KeyWord + "&cid=" + ProductCat + "&desId=" + DesId, function (data) {
           
            $("#hotelresult").html(data);

        });
    }

    function ChangProductCat(catId) {
        $("#cid").val(catId);
    }
</script>
</head>
<body>

     <form action="BookingProductNew.aspx" method="post" >
    <table>
    <tr><td>Date Check In</td><td><input type="text" id="dateStart" name="dateStart" /><img src="/images/ico_calendar_new.jpg" onclick="pickCalendar('dateStart')" /></td></tr>
    <tr><td>Date Check Out</td><td><input type="text" id="dateEnd" name="dateEnd" /><img src="/images/ico_calendar_new.jpg" onclick="pickCalendar('dateEnd')" /></td></tr>
    <tr><td colspan="2">Select New Product</td></tr>
    <tr><td colspan="2"><asp:Literal ID="lrlProduct" runat="server"></asp:Literal></td></tr>
    <tr><td></td><td>
        <input type="hidden" id="bid" name="bid" value="<%=Request.QueryString["bid"] %>" />
        <input type="hidden" id="cid" name="cid" value="<%=Request.QueryString["cid"] %>" />
        
    </td></tr>
    </table>

    <div style="text-align:center; padding:7px; margin:10px 0px 0px 0px; border:1px solid #627aad; background:#f2f2f2" >
            <p style="margin:0px;padding:2px;">Advance Search</p>
            <asp:Literal ID="lrlProductCat" runat="server"></asp:Literal>
            <asp:Literal ID="lrlDes" runat="server"></asp:Literal>
            <input type="text" id="txtSearch" style="width:600px;height:20px;" onkeypress="return clickButton(event,'btnSearch');"  />
            &nbsp;&nbsp;&nbsp;<input type="button" id="btnSearch" value="Search" onclick="SearchHotel();return false;" />
    </div>
    <div  id="hotelresult">
        
    </div>

    <div>
        <input type="submit" id="submit" name="submit" value="Submit" />
    </div>
    </form>

</body>
</html>
