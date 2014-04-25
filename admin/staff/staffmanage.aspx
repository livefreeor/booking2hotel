<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="staffmanage.aspx.cs" Inherits="Hotels2thailand.UI.admin_staffmanage" %>
<%@ Register Src="~/Control/StaffListBox.ascx" TagName="ListStaffBox" TagPrefix="Staff" %>
<%@ Register Src="~/Control/StaffUploadBox.ascx" TagName="UploadPicStaffBox" TagPrefix="Staff" %>
<%@ Register Src="~/Control/StaffAddEditBox.ascx" TagName="AddEditBox" TagPrefix="Staff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Panel ID="panelStaffTopMenu" runat="server">
<div class="submenu">
 <a href="staffmanage.aspx?t=bluehouse"><img src="../../images/bluehousestaff.gif" border="0" alt="BlueHouseStaff"> BlueHouseStaff</a>
 <a href="staffmanage.aspx?t=partners"><img src="../../images/partnerstaff.gif" border="0" alt="Partner Staff"> Partner Staff</a>
 <a href="staffpageauthorize.aspx"><img src="../../images/partnerstaff.gif" border="0" alt="Partner Staff"> Page Authorize</a>
 </div>
 </asp:Panel>
 <%--<div class="headtitle">
<asp:Label ID="lblStaffHeadPage" runat="server"></asp:Label>
 </div>--%>
 <div class="btinsert">
    <asp:Button ID="btimgAddnewStaff" runat="server" OnClick="btAddnewStaff_Click" SkinID="Green"  Text="+ Create New Saff"  />
 </div>
<div class="LeftPan">
    <asp:Panel ID="panelInsert" runat="server">
        <Staff:AddEditBox ID="userInsertBox" runat="server" OnDataSaved="userInsertBox_OnDataSaved"  />
    </asp:Panel>
    <asp:Panel ID="panelListStaff"  runat="server">
        <Staff:ListStaffBox ID="userStaffList" runat="server" />
    </asp:Panel>
    <asp:Panel ID="panelUploadBox" runat="server">
   
      <Staff:UploadPicStaffBox ID="uploadbox"  runat="server" OnDataPictureUploaded="uploadbox_OnDataPictureUploaded"  />
     </asp:Panel>
 
 </div>

   
 <div class="RightPan">
 <%--<asp:Panel ID="panelStaffActivity" runat="server">
        <Staff:ActivityStaffBox ID="userStaffActivty" runat="server" />
    </asp:Panel>--%>

 </div>
 
</asp:Content>

