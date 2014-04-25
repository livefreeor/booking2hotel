<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanelFirst.master" AutoEventWireup="true" CodeFile="login_rd.aspx.cs" Inherits="admin_login_rd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .txtlogin {
                 font-size:14px;
                 padding:5px;
                 width:250px;
                 border:1px solid #a9a9a9;
        }
        .Extra_Button_green
{
    padding:7px;
    font-size:1.5em;
    width:150px;
     cursor:pointer;
    color:#ffffff;
    
    font-weight:bold;
    font-family:Segoe UI;
    background-color:#c6b257;
    border:0px;
   
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="LoginBox">
    <tr>
    <td></td><td><asp:TextBox ID="txtUserName" runat="server" EnableTheming="false" CssClass="txtlogin" ></asp:TextBox></td>
    </tr>
        <tr><td style="height:5px;"></td></tr>
    <tr>
    <td></td><td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" EnableTheming="false"  CssClass="txtlogin"></asp:TextBox></td>
    </tr>
        <tr><td style="height:5px;"></td></tr>
    <tr>
    <td colspan="2">
    <asp:Button ID="BtLogin" runat="server" Text="SignIn" OnClick="BtLogin_Click"  EnableTheming="false" CssClass="Extra_Button_green" />
    </td>
    </tr>
  </table>
 </asp:Content>

