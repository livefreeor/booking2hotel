<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="recipientsSelection.aspx.cs" Inherits="Hotels2thailand.UI.RecipientsSelection" %>
<%@ PreviousPageType VirtualPath="~/extranet/newsletter/SendNewsletter.aspx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="/css/newsletter/newsletter.css"type="text/css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>--%>

<asp:Panel runat="server" ID="panSend" CssClass="recipients">
 <%--<div class="contenthead">
    <h1><asp:Label ID="title" runat="server"></asp:Label></h1>
 </div>--%>

<p style="font-size:18px">Recipients.</p>
<asp:Panel ID="panCustomer" runat="server" Visible="false" CssClass="receive">
    <p><strong>Number of recipients:</strong> <asp:Label ID="lblallcus" runat="server"></asp:Label><%--&nbsp;&nbsp; <strong>subscribe : </strong><asp:Label ID="lblcusSub" runat="server"></asp:Label>--%></p>



</asp:Panel>
<asp:Panel ID="panelStaffExtranet" runat="server" Visible="false"></asp:Panel>
<asp:Panel ID="panelAffPartner" runat="server" Visible="false">

    <p><strong>Total Partner:</strong> <asp:Label ID="lblAllPartner" runat="server"></asp:Label></p>
</asp:Panel>
<asp:Panel ID="panSendSpecify" runat="server" Visible="false">
    <p>
        <strong>Email:</strong>&nbsp;<asp:TextBox ID="emailinput" runat="server" Width="250px"></asp:TextBox>
    </p>
</asp:Panel>
    <br />
     <br />
     <br />
<asp:Button ID="Send_Newsletter" runat="server" Text="Send Now"  OnClick="Send_Newsletter_Click" CssClass="Extra_Button_green"/>
</asp:Panel>

<asp:Panel ID="panWait" runat="server" Visible="false">      
      <asp:Label runat="server" id="lblWait" SkinID="FeedbackKO">
      <p>Another newsletter is currently being sent. Please wait until it completes
      before compiling and sending a new one.</p>
      <p>You can check the current newsletter's completion status from <a href="SendingNewsletter.aspx">this page</a>.</p>
      </asp:Label>
   </asp:Panel>

</asp:Content>

