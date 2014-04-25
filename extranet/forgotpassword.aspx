<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanelFirst.master" AutoEventWireup="true" CodeFile="forgotpassword.aspx.cs" Inherits="admin_forgotpassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/extranet/loginPage.css" type="text/css" rel="stylesheet" />
    <script src="../scripts/jquery-1.6.1.js" type="text/javascript" language="javascript"></script>

    <script  type="text/javascript" language="javascript">

        $(document).ready(function () {
            $("#contentLogin").css("height", $(window).height() - 190 + "px");
        });

    </script>
    <style type="text/css">
     .login_forgot_incom
      {
         
      }
      
     .login_forgot_com
     {
         
     }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p style=" color:#0e385f; font-size:22px; font-weight:bold;">Forgot Your Password ?</p>
    <asp:Panel ID="panelForm" runat="server">
    <p>Please enter your member ID, User Name and e-mail address. We will e-mail the detail and instructions right away.</p>
    <table class="LoginBox">
    <tr>
    <td style="color:#0e385f" align="right">User name :</td><td><asp:TextBox ID="txtUserName" runat="server" CssClass="Extra_textbox" EnableTheming="false" Width="200px" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtUserName" ForeColor="Red" Text="*require"  runat="server" ></asp:RequiredFieldValidator>
    
    </td>
    </tr>
    <tr>
    <td style="color:#0e385f" align="right">Email :</td><td><asp:TextBox ID="txtEmail" runat="server"  EnableTheming="false" CssClass="Extra_textbox"  Width="200px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txtEmail"  ForeColor="Red"  Text="*require"  runat="server" ></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegFilterEmail" ControlToValidate="txtEmail" ForeColor="Red"  Text="*Email format"  runat="server" Display="Dynamic"  ValidationExpression="^[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z_+])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9}$" ></asp:RegularExpressionValidator>
    </td>
    </tr>
    <tr>
    <td></td>
    <td>
    <div style="width:100px">
    <asp:Button ID="BtLogin" runat="server" Text="Submit"  OnClick="BtLogin_Click" EnableTheming="false"  CssClass="Extra_Button" />
    </div>
    <%--<p style="margin:5px 0px 0px 0px; padding:0px;"><a href="forgotpassword.aspx" style="font-size:11px; font-weight:normal;">Got Locked out or Forgot your Password? Click Here</a></p>--%>
    </td>
    </tr>
  </table>
    </asp:Panel>

    <asp:Panel ID="panelincompleted" runat="server" Visible="false" CssClass="login_forgot_incom">
        <p><span>Sorry</span> This login name can not be found.</p>
    </asp:Panel>

    <asp:Panel ID="panelcompleted" runat="server" Visible="false" CssClass="login_forgot_com">
        <p>We are sent email ro recovery your password already. Please check your email and back to login page here <a href="login.aspx">click</a></p>
    </asp:Panel>

 </asp:Content>

