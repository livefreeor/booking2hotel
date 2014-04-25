<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Linkbox.ascx.cs" Inherits="Hotels2thailand.UI.Controls.Controls_Linkbox" %>

<div class="Linkbox">


    <asp:HyperLink ID="hlCreatNew" runat="server" CssClass="menu1" Text="Create New"></asp:HyperLink>
    <asp:HyperLink ID="hlSent" runat="server" CssClass="menu4"></asp:HyperLink>
    <asp:HyperLink ID="lhOutbox" runat="server" CssClass="menu5"></asp:HyperLink>

<%--<a href="sendNewsletter.aspx?mc=<%= this.qMailCat %>" >
  <i class="menu1"></i>Create New<span class="spanlinkboxitem"><asp:Label ID="lblCreatNew" runat="server"></asp:Label></span>

</a>


<a href="showNewsletterList.aspx?temp=1&mc=<%= this.qMailCat %>">
<i class="menu4"></i>Sent box&nbsp;<span id="totalsentbox" class="spanlinkboxitem"><asp:Label ID="lblSent" runat="server"></asp:Label></span></a>

<a href="showNewsletterList.aspx?temp=2&mc=<%= this.qMailCat %>">
<i class="menu5"></i>Out box&nbsp;<span id="totaloutbox" class="spanlinkboxitem"><asp:Label ID="lbloutbox" runat="server"></asp:Label></span></a>--%>

</div>