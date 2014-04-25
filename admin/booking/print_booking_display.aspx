<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="print_booking_display.aspx.cs" Inherits="Hotels2thailand.UI.admin_print_booking_display" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
 <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
<script type="text/javascript" language="javascript" src="../../scripts/darkman_utility.js"></script>
<link rel="stylesheet" href="../../css/voucherPrint.css" type="text/css" />
<style type="text/css">
 .divtransfer{width:100%; margin:20px 0px 0px 0px; padding:5px 5px 5px 10px; }
 .divtransfer_bg{width:100%; margin:5px 0px 0px 0px; padding:5px 5px 5px 10px;   }
    .reqEmpty {
        width:100%; margin:5px 0px 0px 0px; padding:5px 5px 5px 10px;  border:1px solid #333333; background-color:#eceff5;
    }
</style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:TextBox ID="txtAttendName" runat="server" Width="50%"></asp:TextBox>
       <asp:Button ID="submit" runat="server" SkinID="Green" Width="150px" OnClick="submit_Onclick" Text="Next To Print Page" />

 <%--  <asp:Panel ID="panelRequireMent" runat="server">
   <div style=" margin:0px 0px 10px 0px; padding:10px 0px 10px 0px;  border-bottom:1px solid #cccccc; ">
   <p style=" margin:0px; padding:0px; color:#3f5d9d; font-size:18px; font-weight:bold;">Attn:</p>
       
    

   </div>
       <h1>Special Requirement</h1>
    <asp:GridView ID="GVBookingItem" DataKeyNames="RequirID" AutoGenerateColumns="false" runat="server" EnableModelValidation="false"  ShowFooter="false" ShowHeader="false" OnRowDataBound="GVBookingItem_ONrowDataBound">
        <EmptyDataTemplate>
            <div class="reqEmpty">
                <p>No Requirement</p>
            </div>
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                <div style="width:100%; margin:5px 0px 0px 0px; padding:5px 5px 5px 10px;">
                <asp:Literal ID="hdCat" runat="server" Text='<%# Eval("ProductCat") %>' Visible="false"></asp:Literal>
                <p style="width:100%;  font-size:14px; font-weight:bold; margin:0px 0px 0px 0px; padding:0px;"><asp:Label ID="optionTitle" runat="server"></asp:Label></p>
                <p style=" width:100%; margin:10px 0px 0px 0px; padding:0px;">
                <asp:DropDownList ID="dropSmoke" runat="server" SkinID="DropCustomstyle" Visible="false"></asp:DropDownList>
                <asp:DropDownList ID="dropRoom" runat="server" SkinID="DropCustomstyle" Visible="false"></asp:DropDownList>
                <asp:DropDownList ID="sropFloor" runat="server" SkinID="DropCustomstyle" Visible="false"></asp:DropDownList>
                </p>
                <div style=" width:100%; margin:10px 0px 0px 0px; padding:0px;">
                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="80%" Rows="5"></asp:TextBox>
                </div>
                </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Panel ID="paneltranfer" runat="server" Visible="false" CssClass="divtransfer" >
        <h1>Pick up information</h1>
        <div >
                
                <p style="width:100%;  font-size:14px; font-weight:bold; margin:0px 0px 0px 0px; padding:0px;"><asp:Label ID="optionTitle" runat="server"></asp:Label></p>
                <p style=" width:100%; margin:10px 0px 0px 0px; padding:0px;">
                </p>
                <div style=" width:100%; margin:10px 0px 0px 0px; padding:0px;">
                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="80%" Rows="5"></asp:TextBox>
                </div>
         </div>
    </asp:Panel>
    <br />
 
    <asp:Button ID="submitMail" runat="server" SkinID="Green"   Width="150px" OnClick="submitMail_Onclick" Text="Next To Mailling Page" />
   </asp:Panel>
   <asp:Panel ID="panelPrintDisplay" Visible="false"  runat="server">
        
   </asp:Panel>
   <asp:Panel ID="panelMAilDisplay" Visible="false" runat="server">
        <HTMLEditor:Editor runat="server"  Id="editor" Height="600px" AutoFocus="true" Width="100%" />
        <table>
        <tr><td align="right">MailTo</td><td align="left"><asp:TextBox ID="txtMailTO" runat="server"  Width="300px" ></asp:TextBox></td></tr>
        <tr><td align="right">Bcc</td><td align="left"><asp:TextBox ID="txtBcc" runat="server" Width="300px" ></asp:TextBox></td></tr>
        <tr><td align="right">Subject</td><td align="left"><asp:TextBox ID="txtSubject" runat="server" Width="600px" ></asp:TextBox></td></tr>
        <tr><td align="right">Message</td><td align="left"><asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="5" Width="600px" ></asp:TextBox></td></tr>
    </table>
    
    <asp:Button runat="server" Text="Send Now" ID="Button1" OnClick="submit_click" SkinID="Green" />
    
   </asp:Panel>--%>
</asp:Content>

