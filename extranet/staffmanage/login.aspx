<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanelFirst.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="admin_login_rd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/extranet/loginPage.css" type="text/css" rel="stylesheet" />
    <script src="../scripts/jquery-1.6.1.js" type="text/javascript" language="javascript"></script>

    <script  type="text/javascript" language="javascript">

        $(document).ready(function () {
            $("#contentLogin").css("height", $(window).height() - 190 + "px");
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p style=" color:#899070; font-size:22px; font-weight:bold;">Welcome to Booking2Hotels.com</p>
    <table class="LoginBox">
    <tr>
    <td style="color:#0e385f" align="right">User name</td><td><asp:TextBox ID="txtUserName" runat="server" CssClass="Extra_textbox" EnableTheming="false" Width="200px" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtUserName" ForeColor="Red" Text="*require"  runat="server" ></asp:RequiredFieldValidator>
    </td>
    </tr>
    
    <tr>
    <td style="color:#0e385f" align="right">Password</td><td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" EnableTheming="false" CssClass="Extra_textbox"  Width="200px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txtPassword"  ForeColor="Red"  Text="*require"  runat="server" ></asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <td></td>
    <td>
    <div  style="width:100px">
    <asp:Button ID="BtLogin" runat="server" Text="Login" OnClick="BtLogin_Click"  EnableTheming="false"  CssClass="Extra_Button" />
    </div>
    <p style="margin:5px 0px 0px 0px; padding:0px;"><a href="forgotpassword.aspx" style="font-size:11px; font-weight:normal;">Forgot your Password? Click Here</a></p>
    </td>
    </tr>
  </table>
  

    
 </asp:Content>

