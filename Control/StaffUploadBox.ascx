<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StaffUploadBox.ascx.cs" Inherits="Hotels2thailand.UI.Controls.Control_StaffUploadBox" %>
<div class="FileUploadPicStaff_userContrl">
<asp:FileUpload ID="uploadPic" runat="server" />  
<asp:Button ID="btUpload" runat="server" Text="Upload Your Picture Now" onclick="btUploadd_Click"  SkinID="Blue_small" />
 <asp:Label ID="lblAlertUpload" runat="server"></asp:Label>
</div>