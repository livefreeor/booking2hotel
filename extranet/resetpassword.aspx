<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanelFirst.master" AutoEventWireup="true" CodeFile="resetpassword.aspx.cs" Inherits="admin_resetpassword" %>

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
    <p style=" color:#0e385f; font-size:22px; font-weight:bold;">Reset Your Password</p>
    <asp:Panel ID="panelForm" runat="server">
    <table class="LoginBox">
    <tr>
    <td style="color:#0e385f" align="right">New Password :</td><td><asp:TextBox ID="txtNewPass" runat="server"  TextMode="Password" CssClass="Extra_textbox" EnableTheming="false" Width="200px" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtNewPass" ForeColor="Red" Text="*require"  runat="server" ></asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <td style="color:#0e385f" align="right">Confirm New Password :</td><td><asp:TextBox ID="txtReNewPass" runat="server" TextMode="Password" EnableTheming="false" CssClass="Extra_textbox"  Width="200px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txtReNewPass"  ForeColor="Red"  Text="*require"  runat="server" ></asp:RequiredFieldValidator>
    <asp:CompareValidator ControlToValidate="txtReNewPass" ControlToCompare="txtNewPass" ForeColor="Red" Text="*your password do not match" runat="server"></asp:CompareValidator>
    </td>
    </tr>
    <tr>
    <td></td>
    <td>
    <div style="width:150px">
    <asp:Button ID="BtLogin" runat="server" Text="Change Password" OnClick="BtLogin_Click"  EnableTheming="false"  CssClass="Extra_Button" />
    </div>
    <%--<p style="margin:5px 0px 0px 0px; padding:0px;"><a href="forgotpassword.aspx" style="font-size:11px; font-weight:normal;">Got Locked out or Forgot your Password? Click Here</a></p>--%>
    </td>
    </tr>
  </table>
    </asp:Panel>

    <asp:Panel ID="panelCompleted" runat="server" Visible="false">
        <p>Your Password has been changed.</p>
        <p>Go to Login Page <a href="http://manage.booking2hotels.com/extranet/login.aspx">click</a></p>
    </asp:Panel>

    <asp:Panel ID="panelincompleted" runat="server" Visible="false">
        
    </asp:Panel>
    
 </asp:Content>

