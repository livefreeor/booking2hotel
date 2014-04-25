<%@ Control Language="C#" AutoEventWireup="true" CodeFile="control_picture_insert_box.ascx.cs" Inherits="Hotels2thailand.UI.Controls.Control_controlpicture_control_picture_insert_box" %>
<style type="text/css">
 .pic_insert_box
 {
      width:950px;
      margin:10px 0px 0px 0px;
      padding:0px;
      
 }
 .pic_insert_box td
 {
      margin:10px 0px 0px 0px;
      padding:5px;
      border:1px solid #eeeee1;
      font-size:11px;
      color:#1c2a47;
      font-weight:bold;
 }
 .pic_insert_box p 
 {
      margin:0px; padding:0px;
 }
 .Alternatecolors
 {
     /*background:#f8f8f8;*/
 }
  .Alternatecolors_2
 {
    /*background:#dedede;*/
 }
 .p_insert_box
 {
     font-size:12px;
 }
 .span_insert_box
 {
      color:#3b59aa;
 }
</style>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<table class="pic_insert_box" >
    <tr>
        <td colspan="3">
        <p class="p_insert_box">Picture Title : <span  class="span_insert_box"><asp:Label ID="txtpicName" runat="server" Width="600px"></asp:Label></span></p>
       </td>
    </tr>
    <tr>
        <td width="20%">
        <p>Picture Type</p>
        <asp:DropDownList ID="drppicType" runat="server" 
                DataTextField="Value" DataValueField="Key" AutoPostBack="true"  OnSelectedIndexChanged="drppicType_OnSeletedIndexChange"></asp:DropDownList>
            <%--<asp:ObjectDataSource ID="ObjpictureType" runat="server" 
                SelectMethod="getPictureTypeAll" 
                TypeName="Hotels2thailand.Production.ProductPicType"></asp:ObjectDataSource>--%>
        </td>
        <td width="10%">
        <p>Pic Number</p>
        <asp:DropDownList ID="drpDownNumBerOfPic" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="drpDownNumBerOfPic_OnseletedIndexChange" ></asp:DropDownList>
        </td>
        <td width="70%">
        <p>file Name</p>
        <asp:TextBox ID="txtfilename" runat="server" Width="450px" BackColor="#faffbd" ForeColor="Red" ></asp:TextBox></td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>