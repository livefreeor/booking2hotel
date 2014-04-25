<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StaffStatusBox.ascx.cs" Inherits="Hotels2thailand.UI.Controls.Control_StaffStatusBox" %>
<asp:Panel ID="panelStatusBox" runat="server">
<div class="StatusBox">
<asp:Image ID="imgpisStaff_s" runat="server" />
<p><asp:Label ID="lblStaffName" runat="server"></asp:Label></p>
<p><asp:Label ID="lblStaffCat" runat="server"></asp:Label></p>
<p><asp:HyperLink ID="linkProfile" runat="server" Text="Profile" ></asp:HyperLink>
&nbsp;<%--<asp:LinkButton ID="linkSignout" runat="server" Text="Sign Out" onclick="linkSignout_Click"></asp:LinkButton>--%>
<asp:HyperLink ID="HyperLink1" runat="server" Text="Sign Out" NavigateUrl="~/admin/staff/ajax_staff_logout.aspx" ></asp:HyperLink>

</p>

</div>
</asp:Panel>