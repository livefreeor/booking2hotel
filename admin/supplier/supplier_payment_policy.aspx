<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="supplier_payment_policy.aspx.cs" Inherits="Hotels2thailand.UI.admin_supplier_payment_policy" %>
<%@ Register Src="~/Control/DatepickerCalendar.ascx" TagName="datePicker" TagPrefix="Product" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
   <script type="text/javascript" src="../../Scripts/jquery-1.4.2.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
     <script type="text/javascript" src="../../Scripts/datepicker.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
  <h6><asp:Label ID="lblhead" runat="server"></asp:Label></h6>
    <div class="product_minimum_stay">
        <asp:Panel ID="panel5" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image7" runat="server" ImageUrl="~/images/content.png" /> Insert Box &nbsp;: For &nbsp;<asp:Label ID="lblPayment" runat="server"></asp:Label></h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
            <table>
                <tr>
                    <td><Product:datePicker ID="DatePicker" runat="server" /></td>
                    <td valign="top">
                    <p style="margin:0px 0px 0px 0px; padding:0px 0px 0px 0px">Day Advance</p>
                    <div style="margin:5px 0px 0px 0px; padding:0px 0px 0px 0px"><asp:DropDownList ID="dropDayAdvance" runat="server"></asp:DropDownList></div></td>
                </tr>
            </table>
            <br />
            <asp:Button ID="btnSave" runat="server" Text="Save" SkinID="Green"  OnClick="btnSave_Onclick"/>
        </asp:Panel>
        <asp:Panel ID="panel1" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Payment Policy List</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
            <asp:GridView ID="GVSupPolicy"  runat="server" DataKeyNames="PolicyId" ShowFooter="false" ShowHeader="true"  EnableModelValidation="true" SkinID="ProductList" OnRowDataBound="GVSupPolicy_OnRowDataBound" AutoGenerateColumns="false">
                <EmptyDataRowStyle   CssClass="alert_box" />
                            <EmptyDataTemplate>
                                      <div class="alert_inside_GridView">
                                       <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
                                       <p class="alert_box_head">No Payment Plan Record </p>
                                       <p  class="alert_box_detail">Please select at least one.</p>
                                     </div>
                                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="55%" HeaderText="From  - To" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <Product:datePicker ClientIDMode="AutoID" ID="dDatePicker" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="20%" HeaderText="Amount Day" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:DropDownList ID="drpAdvance" runat="server" ></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="20%" HeaderText="Save" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="btnminsave" runat="server" Text="Save" CommandArgument='<%# Eval("PolicyId") + "," + Container.DataItemIndex %>' CommandName="miniedit" OnClick="btnminsave_OnClick" SkinID="Green" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </div>
   
</asp:Content>
