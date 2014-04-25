<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StaffStatusBox_Extra.ascx.cs" Inherits="Hotels2thailand.UI.Controls.Control_StaffStatusBox_Extra" %>
<asp:Panel ID="panelStatusBox" runat="server">
<div class="StatusBox_extra">
<table>
 <tr>
  <td>
  Hello <asp:Label ID="lblStaffName" runat="server"></asp:Label>
  &nbsp;&nbsp;|&nbsp;&nbsp;
  </td>
   <%--<td><img src="/images_extra/separator.jpg" alt="" /></td>--%>
    <td><asp:HyperLink ID="linkProfile" runat="server" Text="Profile" ></asp:HyperLink>  &nbsp;&nbsp;|&nbsp;&nbsp;</td>
     <%--<td><img src="/images_extra/separator.jpg" alt="" /></td>--%>
      <td><asp:HyperLink ID="hlExtraNetlogout" runat="server" Text="Log Out"  ></asp:HyperLink></td>
 </tr>
</table>

</div>
</asp:Panel>