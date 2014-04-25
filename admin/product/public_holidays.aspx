<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="public_holidays.aspx.cs" Inherits="Hotels2thailand.UI.admin_public_holiday" %>
<%@ Register Src="~/Control/DatepickerCalendar-single.ascx" TagName="datePicker" TagPrefix="Product" %>
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
    
           
        
    <asp:Panel ID="panelinsert" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image7" runat="server" ImageUrl="~/images/content.png" /> Public Holidays Insert Box</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />

        
        <p><Product:datePicker ID="dDatePicker" runat="server" /></p>
        <p><asp:TextBox ID="txttitle" runat="server" Width="600px"></asp:TextBox></p>
        <p><asp:Button ID="btnSave" runat="server" Text="Save" SkinID="Green" OnClick="btnSave_Onclick" /></p>
        
    </asp:Panel>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>--%>
    <asp:Panel ID="panelPublicList" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Public Holidays List</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <p><span>Select Year</span><asp:DropDownList ID="dropyear" runat="server" OnSelectedIndexChanged="dropyear_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></p>
        <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <asp:Image ID="imgProgress" runat="server" ImageUrl="~/images/progress_b.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
        
        <asp:GridView ID="GVList" runat="server" EnableModelValidation="false" ShowFooter="false"  AutoGenerateColumns="false" DataKeyNames="HolidayId"  OnRowDataBound="GVList_OnRowDataBound">
        <EmptyDataRowStyle   CssClass="alert_box" />
                    <EmptyDataTemplate>
                         <div class="alert_inside_GridView" >
                               <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
                               <p class="alert_box_head">There are no Holiday in Year </p>
                               <p class="alert_box_detail">Please select other year</p>
                        </div>
                 </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="No." ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#  Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title" ItemStyle-Width="60%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        &nbsp;&nbsp;<asp:TextBox ID="txtTitle" runat="server" Width="550px" Text='<%# Eval("Title") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date" ItemStyle-Width="15%"  HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                       <Product:datePicker ID="dDatePicker" runat="server"  ClientIDMode="AutoID"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Save" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Button ID="btnSaveEdit" runat="server" Text="Save"  SkinID="Green_small" OnClick="btnSaveEdit_OnClick"  CommandArgument='<%# Eval("HolidayId") %>' CommandName="save" />
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
    </asp:Panel>
   <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
