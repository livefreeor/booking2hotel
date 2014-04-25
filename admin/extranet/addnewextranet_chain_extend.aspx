<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="addnewextranet_chain_extend.aspx.cs" Inherits="Hotels2thailand.UI.admin_extranet_addnewextranet_chain_extend" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register  Src="~/Control/DatepickerCalendar.ascx" TagName="DatePicker_Add_Edit" TagPrefix="DateTime" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
<script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
  <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="../../Scripts/darkman_datepicker.js"></script>
    <script type="text/javascript" src="../../scripts/darkman_utility.js"></script>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            DatepickerDual("txtDateStart", "txtDateEnd");
        });

        function removeCom(targetid) {
            
            $("#" + targetid).fadeTo("fast", 0.00, function () { //fade
                $(this).slideUp("300", function () { //slide up
                    $(this).remove(); //then remove from the DOM
                });
            });


            var countItem = $("#rate_load_result .commisstion_list_item").length;

            if (countItem == 0) {
                $("#commisstion_list_head").hide();
            }

        }

        function GetPeriodCommission() {

            var random = makeid();
            
            var dDatestart = $("#txtDateStart").val();
            var dDateEnds = $("#txtDateEnd").val();

            var hd_DateFrom = $("#hd_txtDateStart").val();
            var hd_Dateto = $("#hd_txtDateEnd").val();
            var ComAMount = $("#txtCom").val();
            var result = "";

            result = result + "<div class=\"commisstion_list_item\" id=\"commisstion_list_item_" + random + "\"  style=\"display:none;\" >";
            result = result + "<input type=\"checkbox\" id=\"checked_commission_" + random + "\" style=\"display:none;\" value=\"" + random + "\" name=\"checked_commission\" checked=\"checked\" />";

            result = result + "<table width=\"100%\" >";
            result = result + "<tr  align=\"center\"><td width=\"15%\">" + dDatestart + "<input type=\"hidden\" name=\"hd_rate_date_form_" + random + "\" value=\"" + hd_DateFrom + "\" /></td><td width=\"15%\">" + dDateEnds + "<input type=\"hidden\" name=\"hd_rate_date_To_" + random + "\" value=\"" + hd_Dateto + "\" /></td>";
            result = result + "<td width=\"10%\">" + ComAMount + "<input type=\"hidden\" name=\"hd_amount_" + random + "\" value=\"" + ComAMount + "\" /></td><td width=\"5%\"><a href=\"\" onclick=\"removeCom('commisstion_list_item_" + random + "');return false;\" >remove</a></td>";
            result = result + "</tr>";
            result = result + "</table>";
            result = result + "</div>";

            

            $("#commission_list").append(result);

            $("#commisstion_list_head").show();

            var countItem = $("#commission_list .commisstion_list_item").length;

            $("#commission_list .commisstion_list_item").filter(function (index) {
                return index == (countItem - 1)
            }).fadeIn();


        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Panel ID="panelListHotel" runat="server" >


<asp:TextBox ID="txthotelCode" runat="server"></asp:TextBox>
<asp:Button ID="btnhotelSearch" runat="server" OnClick="btnhotelSearch_Onclick" Text="Search" />

<br /><br />
<asp:GridView ID="GVHoteltoExtraResult" runat="server"  ShowFooter="false" ShowHeader="false" AutoGenerateColumns="false" EnableModelValidation ="false" DataKeyNames="ProductID"  OnRowDataBound="GVHoteltoExtraResult_OnRowDataBound">
 <Columns>
    <asp:TemplateField>
        <ItemTemplate>
         <table>
         <tr><td>
          <asp:Label ID="lbltitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
         </td>
         <td>
         <asp:Button ID="btnActiveExtraNet" runat="server" Text="UseThis" CommandArgument='<%# Eval("ProductID") + "," + Eval("SupplierPrice") %>' OnClick="btnActiveExtraNet_Onclick" />
         </td></tr>
         </table> 
        </ItemTemplate>
    </asp:TemplateField>
 </Columns>
</asp:GridView>
</asp:Panel>
<asp:Panel ID="panelUserAdd" runat="server" Visible="false">
<asp:HiddenField ID ="hdSupplierID" runat="server" />
<asp:HiddenField ID="hdProductId" runat="server" />
<asp:DropDownList ID="dropChain" runat="server" ></asp:DropDownList>
<h6 style="color:Green">Supplier Name : <asp:Label ID="lblSupplierTitle" runat="server"></asp:Label></h6>

<div>

<h3>Product :<asp:Label ID="lblProducttitle" runat="server" ClientIDMode="Static"></asp:Label></h3>
</div>

<asp:Panel ID="userForm" runat="server">
    <table>
    <tr><td>Name full</td><td><asp:TextBox ID="txtName" runat="server" Width="300px"></asp:TextBox></td></tr>
    <tr><td>Email</td><td><asp:TextBox ID="txtEmail" runat="server"  Width="300px"></asp:TextBox></td></tr>
    <tr><td>User name</td><td><span style=" font-size:14px; font-weight:bold;">Admin</span>@<asp:TextBox ID="txtsurffix"  Width="200px" runat="server"></asp:TextBox></td></tr>
    <tr><td>Pass word</td><td><asp:TextBox ID="txtPassword" runat="server"  Width="300px"></asp:TextBox></td></tr>
    </table>

    <div id="commissionInsert">
        <table>
            <tr>
            <td>Date From</td>
            <td><input  type="text" id="txtDateStart" class="textBoxStyle_s"   /></td>
            <td>To</td>
            <td><input  type="text" id="txtDateEnd"  class="textBoxStyle_s"  /></td>
            <td><asp:TextBox ID="txtCom" runat="server" ClientIDMode="Static"></asp:TextBox>(%)</td>
            <td><input type="button" value="Add" onclick="GetPeriodCommission();return false;" /></td>
            </tr>
        </table>
    </div>
    <div id="commission_list">
    <div id="commisstion_list_head"  style="display:none;">
                <table width="100%" >
                    <tr bgcolor="#96b4f3" align="center"><td width="15%">Date From</td><td width="15%">Date To</td>
                    <td width="10%">Amount</td><td width="5%">Delete</td>
                    </tr>
                </table>
    </div>
    
    </div>
</asp:Panel>


    
    <p>Please check Product And Supplier</p>
    <p><asp:Button ID="btnSave" runat="server" Text="Create user And active now" OnClick="btnSave_Onclick" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Onclick" />
    </p>
    
    
</asp:Panel>
<asp:Panel ID="panelAddSuccess" runat="server" Visible="false">
<p>Active Hotel to Extranet And Create Admin User Completed!!</p>
</asp:Panel>
<asp:Panel ID="panelCannetInsertChain" runat="server" Visible="false">
<p>Please Select Chain Before</p>
<asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Onclick" />
</asp:Panel>
</asp:Content>

