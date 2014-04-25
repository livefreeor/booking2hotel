<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="resubmit_send.aspx.cs" Inherits="Hotels2thailand.UI.admin_resubmit_send" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
 <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Panel ID="panelMailOption" runat="server" Visible="false">
<p style="  font-size:14px;  font-weight:bold; color:#333333;">Please select type of resubmit for BOOK NOW PAY LATER</p>
<div>
<p style="  font-size:12px; color:#3f5d9d;">1: กรณีที่ต้องการให้ลูกค้า กรอกบัตรเครดิตใหม่ , <span  style=" color:Red">*กรณีที่ลุกค้า กรอกบัตรมาเรียบร้อยแล้ว จะมีอีเมลล์ ส่งมา แจ้ง Reservation </span></p>
<asp:Button runat="server" Text="Request offline charge" ID="submit_go" OnClick="submit_booknow_offline_click" SkinID="Green" Width="200px" />
</div>
<br /><br />
<div>
<p style="  font-size:12px;  color:#3f5d9d;">2: กรณีที่ต้องการให้ลูกค้า ทำจ่ายเองผ่าน Gateway ของเราแบบ ด้วยตัวเองแบบ booking ปกติ ตามยอดจำนวนเงินของ Payment นั้นๆ </p>
<asp:Button runat="server" Text="Request online charge" ID="Button1" OnClick="submit_booknow_online_click" SkinID="Green" Width="200px" />
</div>
    
</asp:Panel>
<asp:Panel ID="panelMail" runat="server">
   <HTMLEditor:Editor runat="server"  Id="editor" Height="600px" AutoFocus="true" Width="100%" />
   
 <div class="mailform" style=" text-align:center; ">
    <table>
        <tr><td align="right">MailTo</td><td align="left"><asp:TextBox ID="txtMailTO" runat="server"  Width="300px" ></asp:TextBox></td></tr>
        <tr><td align="right">Bcc</td><td align="left"><asp:TextBox ID="txtBcc" runat="server" Width="300px" ></asp:TextBox></td></tr>
        <tr><td align="right">Subject</td><td align="left"><asp:TextBox ID="txtSubject" runat="server" Width="600px" ></asp:TextBox></td></tr>
    </table>
    <asp:Button runat="server" Text="Send Now" ID="submit" OnClick="submit_click" SkinID="Green" />
   
 </div>
 </asp:Panel>
   
</asp:Content>

