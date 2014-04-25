<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="staffmanage.aspx.cs" Inherits="Hotels2thailand.UI.extranet_staffmanage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>


<script language="javascript" type="text/javascript">
    //noticeShow



    $(document).ready(function () {

        var dropval = $("#dropMethodOrdercenter").val();
        //getNotice(dropval);

        //$("#dropMethodOrdercenter").change(function () {

        //    var dropVal = $(this).val();
        //    //alert(dropVal);
        //    getNotice(dropVal);
        //});
    });

    function getNotice(methodid) {
        var notice = "";

        switch (methodid) {
            case "5":
                notice = "*Can view only."; 
                break;
            case "4":
                notice = "*Get notified, but e-mail to show in my email client.";
                break;
            case "6":
                notice = "*Get notified, but e-mail will not show in email client.";
                break;
            case "7":
                notice = "*Get notified, but e-mail to show in my email client.";
                break;
        }

      
        $("#noticeShow").html("&nbsp;&nbsp;"+notice);
    }
    
</script>
<style type="text/css">
    #div_notice
    {
        margin:0px;
        padding:0px;
        position:relative;
        width:100%;
    }
 #noticeShow
 {
    position:absolute;
     top:-5px;
     left:-245px;
     font-size:11px;
     text-indent:10px;
     color:#065cdd;
 }
 #drop_order
 {
     
     margin:0px;
     position:relative;
 }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
    <h4><asp:Image ID="Image4" runat="server" ImageUrl="~/images/content.png" /> Hotel Selection</h4>
    <asp:CheckBoxList ID="chkListProduct"  Font-Bold="true" Font-Size="14px" DataTextField ="ProductTitle" DataValueField="ProductID" runat="server">
     
    </asp:CheckBoxList>
</div>
<div>
<h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" /> Staff Information</h4>
 <table>
     <tr><td>Full name :</td><td>
     <asp:TextBox ID="txtFullName" runat="server" CssClass="Extra_textbox"  EnableTheming="false" Width="300px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="ReqFullName" runat="server" ControlToValidate="txtFullName" Text="*require" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
     
     </td></tr>
     <tr><td>Email :</td><td>
     <asp:TextBox ID="txtEmails" runat="server" CssClass="Extra_textbox" EnableTheming="false" Width="300px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="ReqEmails" runat="server" ControlToValidate="txtEmails" Text="*require" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
     <asp:RegularExpressionValidator ID="RegFilterEmail" ControlToValidate="txtEmails" ForeColor="Red"  Text="*Email format"  runat="server" Display="Dynamic"  ValidationExpression="^[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z_+])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9}$" ></asp:RegularExpressionValidator>
     </td></tr>
     <tr><td>User Name :</td><td>
     <asp:TextBox ID="txtUsername" runat="server" CssClass="Extra_textbox" EnableTheming="false" Width="300px"></asp:TextBox>
     <label style="font-size:14px; font-weight:bold">@<asp:Label ID="txtSurfixSup" runat="server" ></asp:Label></label>
     <asp:RequiredFieldValidator ID="ReqUsername" runat="server" ControlToValidate="txtUsername" Text="*require" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
     </td>
     
     <td style="font-size:14px; font-weight:bold"></td></tr>
     <tr><td  style=" font-weight:bold">New password :</td><td><asp:TextBox ID="textnewPass" runat="server" TextMode="Password" CssClass="Extra_textbox_yellow" EnableTheming="false" Width="300px"></asp:TextBox></td></tr>
     <tr><td style=" font-weight:bold">Comfirm new password :</td><td><asp:TextBox ID="textnewPassCon" TextMode="Password" runat="server" CssClass="Extra_textbox_yellow" EnableTheming="false" Width="300px"></asp:TextBox>
     <asp:CompareValidator ID="ComparetextnewPassCon" ControlToValidate="textnewPass" ControlToCompare="textnewPassCon" ForeColor="Red" Text="*password do not match" runat="server"></asp:CompareValidator>
     </td></tr>
     <tr><td>Active :</td><td><asp:CheckBox ID="activestaff" runat="server" /></td></tr>
 </table>
</div>
<div>
    <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Module Selection</h4>
    <table  width="700px">
        <tr>
        <td><asp:CheckBox ID="chkRateControl" ClientIDMode="Static" runat="server" /></td>
        <td>Rate Control</td>
        <td><asp:DropDownList ID="dropMethodRateControl" EnableTheming="false" CssClass="Extra_Drop"  Width="200px" runat="server">
        <asp:ListItem Text="Full Control" Value="4" ></asp:ListItem>
        <asp:ListItem Text="Read Only" Value="5" Selected="True"></asp:ListItem>
        <asp:ListItem Text="Add/Edit" Value="6" ></asp:ListItem>
        <asp:ListItem Text="Add/Edit/Delete" Value="7" ></asp:ListItem>
        </asp:DropDownList></td>
        </tr>
        <tr>
        <td><asp:CheckBox ID="chkPackage" ClientIDMode="Static" runat="server" /></td>
        <td>Package Control</td>
        <td><asp:DropDownList ID="dropMethodPackageControl" EnableTheming="false" CssClass="Extra_Drop"  Width="200px" runat="server">
        <asp:ListItem Text="Full Control" Value="4" ></asp:ListItem>
        <asp:ListItem Text="Read Only" Value="5" Selected="True"></asp:ListItem>
        <asp:ListItem Text="Add/Edit" Value="6" ></asp:ListItem>
        <asp:ListItem Text="Add/Edit/Delete" Value="7" ></asp:ListItem>
        </asp:DropDownList></td>
        </tr>

        <tr>
        <td><asp:CheckBox ID="chkMember" ClientIDMode="Static" runat="server" /></td>
        <td>Member Manage</td>
        <td><asp:DropDownList ID="dropMethodMember" EnableTheming="false" CssClass="Extra_Drop"  Width="200px" runat="server">
        <asp:ListItem Text="Full Control" Value="4" ></asp:ListItem>
        <asp:ListItem Text="Read Only" Value="5" Selected="True"></asp:ListItem>
        <asp:ListItem Text="Add/Edit" Value="6" ></asp:ListItem>
        <asp:ListItem Text="Add/Edit/Delete" Value="7" ></asp:ListItem>
        </asp:DropDownList></td>
        </tr>

        <tr>
        <td><asp:CheckBox ID="chkPromotion" ClientIDMode="Static" runat="server" /></td>
        <td>Promotion</td>
        <td><asp:DropDownList ID="dropMethodPromotion" EnableTheming="false" CssClass="Extra_Drop"  Width="200px" runat="server">
        <asp:ListItem Text="Full Control" Value="4" ></asp:ListItem>
        <asp:ListItem Text="Read Only" Value="5" Selected="True"></asp:ListItem>
        <asp:ListItem Text="Add/Edit" Value="6" ></asp:ListItem>
        <asp:ListItem Text="Add/Edit/Delete" Value="7" ></asp:ListItem>
        </asp:DropDownList></td>
        </tr>
        <tr>
        <td><asp:CheckBox ID="chkAllotment" ClientIDMode="Static" runat="server" /></td>
        <td>Allotment</td>
        <td><asp:DropDownList ID="dropMethodAllotment" EnableTheming="false" CssClass="Extra_Drop"  Width="200px" runat="server" runat="server">
        <asp:ListItem Text="Full Control" Value="4"></asp:ListItem>
        <asp:ListItem Text="Read Only" Value="5" Selected="True"></asp:ListItem>
        <asp:ListItem Text="Add/Edit" Value="6" ></asp:ListItem>
        <asp:ListItem Text="Add/Edit/Delete" Value="7"></asp:ListItem>
        </asp:DropDownList></td>
        </tr>
        <tr>
        <td><asp:CheckBox ID="chkOrdercenter" ClientIDMode="Static" runat="server" /></td>
        <td>Booking Center</td>
        <td id="drop_order"><asp:DropDownList ID="dropMethodOrdercenter" EnableTheming="false" ClientIDMode="Static" CssClass="Extra_Drop"  Width="200px" runat="server">
        <asp:ListItem Text="Full Control" Value="4"></asp:ListItem>
        <asp:ListItem Text="Read Only" Value="5" Selected="True"></asp:ListItem>
        <asp:ListItem Text="Add/Edit" Value="6"></asp:ListItem>
        <asp:ListItem Text="Add/Edit/Delete" Value="7" ></asp:ListItem>
        </asp:DropDownList></td>
  
        
        </tr>
        <tr>
        <td><asp:CheckBox ID="chkReview" ClientIDMode="Static" runat="server" /></td>
        <td>Review</td>
        <td><asp:DropDownList ID="dropMethodreview" EnableTheming="false"  CssClass="Extra_Drop"  Width="200px" runat="server">
        <asp:ListItem Text="Full Control" Value="4"></asp:ListItem>
        <asp:ListItem Text="Read Only" Value="5" Selected="True"></asp:ListItem>
        <asp:ListItem Text="Add/Edit" Value="6"></asp:ListItem>
        <asp:ListItem Text="Add/Edit/Delete" Value="7" ></asp:ListItem>
        </asp:DropDownList>
        
        </td>
        </tr>
        <tr>
        <td><asp:CheckBox ID="checkReport" ClientIDMode="Static" runat="server" /></td>
        <td>Report</td>
        <td><asp:DropDownList ID="dropMethodReport" EnableTheming="false" CssClass="Extra_Drop"  Width="200px" runat="server">
        <asp:ListItem Text="Full Control" Value="4"></asp:ListItem>
        <asp:ListItem Text="Read Only" Value="5" Selected="True"></asp:ListItem>
        <asp:ListItem Text="Add/Edit" Value="6"></asp:ListItem>
        <asp:ListItem Text="Add/Edit/Delete" Value="7" ></asp:ListItem>
        </asp:DropDownList></td>
        </tr>
         <tr>
        <td><asp:CheckBox ID="checkNews" ClientIDMode="Static" runat="server" /></td>
        <td>Newsletter</td>
        <td><asp:DropDownList ID="dropMethodNews" EnableTheming="false" CssClass="Extra_Drop"  Width="200px" runat="server">
        <asp:ListItem Text="Full Control" Value="4"></asp:ListItem>
        <asp:ListItem Text="Read Only" Value="5" Selected="True"></asp:ListItem>
        <asp:ListItem Text="Add/Edit" Value="6"></asp:ListItem>
        <asp:ListItem Text="Add/Edit/Delete" Value="7" ></asp:ListItem>
        </asp:DropDownList></td>
        </tr>
      </table>
</div>



<div style="text-align:center; margin:15px 0px 0px ; padding:5px; border:1px solid #cccccc; background-color:#f2f2f2;">
<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Onclick" CssClass="Extra_Button" />
</div>
 

</asp:Content>

