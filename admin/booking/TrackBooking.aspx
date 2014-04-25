<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TrackBooking.aspx.cs" Inherits="admin_booking_TrackBooking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Track Booking</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<style>
body,td,div
{
	font-family:Verdana, Geneva, sans-serif;
	font-size:12px;
	color:#555;
}
.tbl_report
{
	background-color:#D9EFFB;
	border:2px solid #0C5087;
}
.tbl_report th
{
	padding:5px;

	background-color:#C4E8FB;
	text-align:center;
	font-family:Verdana, Geneva, sans-serif;
	font-size:12px;
	color:#444;
}

.tbl_report td
{
	background-color:#fff;
	padding:5px;
	font-family:Verdana, Geneva, sans-serif;
	font-size:12px;
	color:#555;
	text-align:left;
}
.cell_title td{
	background-color:#E2F9FA;
	text-align:center;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <table width="800" border="0" class="tbl_report" cellspacing="1">
  <tr>
    <th colspan="8">Entry</th>
  </tr>
  <tr class="cell_title">
    <td>Type</td>
    <td>Page</td>
    <td>Keyword</td>
    <td>Engine</td>
    <td>Website</td>
    <td>Affiliate</td>
    <td>Campaign</td>
    <td>Time</td>
  </tr>
  <asp:Literal ID="liEntry" runat="server"></asp:Literal>
</table>
<br /><br />
<table width="800"  class="tbl_report" cellspacing="1">
  <tr>
    <th colspan="4">Foot Print</th>
  </tr>
  <tr class="cell_title">
    <td>Page</td>
    <td>Time Use</td>
    <td>search</td>
    <td>Keyword</td>
  </tr>
    <asp:Literal ID="liFootPrint" runat="server"></asp:Literal>
</table>
<br /><br />
    <table width="800"  class="tbl_report" cellspacing="1">
  <tr>
    <th colspan="10">Other visit</th>
  </tr>
  <tr class="cell_title">
  	<td>No.</td>
    <td>Type</td>
    <td>Page</td>
    <td>Keyword</td>
    <td>Engine</td>
    <td>Website</td>
    <td>Affiliate</td>
    <td>Campaign</td>
    <td>Time</td>
    <td>Booking</td>
  </tr>
  <asp:Literal ID="liOtherVisit" runat="server"></asp:Literal>
</table>
    </div>
    </form>
</body>
</html>
