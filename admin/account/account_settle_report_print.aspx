<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="account_settle_report_print.aspx.cs" Inherits="Hotels2thailand.UI.admin_account_settle_report_print" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
  <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/darkman_utility.js" ></script>

<style type="text/css">
    #report_list
    {
        margin:15px 0px 0px 0px;
        padding:0px;
        width:100%;
        font-size:18px;
    }
    #report_list a
    {
            font-size:12px;
    }
    #report_list table
    {
         background-color:#d8dfea;
         margin:0px;
         padding:0px;
         width:100%;
    }
     #report_list table tr
    {
        margin:0px;
         padding:0px;
          height:30px;
          
    }
     #report_list table tr th
    {
        margin:0px;
         padding:0px;
         color:#ffffff;
         text-align:center;
         font-size:12px;
    }
     #report_list table tr td
    {
        margin:0px;
         padding:0px;
         text-align:center;
    }
    #block_print
    {
        margin:20px 0px 0px 0px;
        padding:0px;
        text-align:center;
    }
    .text_default
    {
        width:100%; text-align:center;
        font-size:14px;
        color:#3f5d9d;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<asp:HiddenField ID="report_type" runat="server"  ClientIDMode="Static" />--%>
</asp:Content>

