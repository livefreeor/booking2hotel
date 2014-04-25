<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookingProductEdit.aspx.cs" Inherits="vtest_BookingProductEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
   <style type="text/css">
    .lmTransfer
    {
	    display:none;
    }
    </style>
    <script language="javascript" src="/js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("#btnBooking").click(function () {
                $("#bookingForm").submit();
            });
        });
    </script>
    <link href="/theme_color/blue/style_rate.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>