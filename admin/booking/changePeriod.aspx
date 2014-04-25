<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changePeriod.aspx.cs" Inherits="vtest_changePeriod" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Period</title>
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
</script>
</head>
<body>
   <form action="BookingProductEdit.aspx" method="post">
    <table>
    <tr><td>Date Check In</td><td><input type="text" id="dateStart" name="dateStart" /><img src="/images/ico_calendar_new.jpg" onclick="pickCalendar('dateStart')" /></td></tr>
    <tr><td>Date Check Out</td><td><input type="text" id="dateEnd" name="dateEnd" /><img src="/images/ico_calendar_new.jpg" onclick="pickCalendar('dateEnd')" /></td></tr>
    <tr><td></td><td>
        <input type="hidden" id="pid" name="pid" value="<%=Request.QueryString["proid"] %>" />
        <input type="hidden" id="bpid" name="bpid" value="<%=Request.QueryString["bpid"] %>" />
        <input type="hidden" id="cid" name="cid" value="<%=Request.QueryString["cid"] %>" />
        <input type="submit" id="submit" name="submit" value="Submit" />
    </td></tr>
    </table>
    </form>
</body>
</html>
