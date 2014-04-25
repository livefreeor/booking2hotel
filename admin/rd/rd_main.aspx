<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="rd_main.aspx.cs" Inherits="Hotels2thailand.UI.admin_rd_main" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
<%--<script type="text/javascript" src="../../Scripts/jquery-1.4.2.js"></script>--%>
<script language="javascript" type="text/javascript">

    $(document).ready(function () {

        


    });
    
</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <a href="rd_location.aspx">Location</a>&nbsp;|&nbsp;
    <a href="rd_destination.aspx">Destination</a>     
</asp:Content>
