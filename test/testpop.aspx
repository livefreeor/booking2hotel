<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testpop.aspx.cs" Inherits="test_testpop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/productstyle2.css" type="text/css"  rel="Stylesheet"/>
    <script language="javascript" type="text/javascript" src="/scripts/jquery-1.6.1.js"></script>
    <script language="javascript" type="text/javascript" src="/scripts/darkman_utility.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            DarkmanPopUp_front(600,"<p>HELLO</p>");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <%-- //<input type="button" id="tt" value="HHH" onclick="DarkmanPopUp_front(600,'<p>5555555</p>');" />--%>
    </div>
    </form>
</body>
</html>
