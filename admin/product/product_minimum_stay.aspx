<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product_minimum_stay.aspx.cs" Inherits="Hotels2thailand.UI.admin_product_minimum_stay" %>
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
    <div class="product_minimum_stay">
        <asp:Panel ID="panel5" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image7" runat="server" ImageUrl="~/images/content.png" /> Insert Box</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
            <table>
                <tr>
                    <td><Product:datePicker ID="DatePicker" runat="server" /></td>
                    <td valign="top">
                    <p style="margin:0px 0px 0px 0px; padding:0px 0px 0px 0px">Minimum Stay</p>
                    <div style="margin:5px 0px 0px 0px; padding:0px 0px 0px 0px"><asp:DropDownList ID="dropMiniMum" runat="server"></asp:DropDownList></div></td>
                </tr>
            </table>
            <br />
            <asp:Button ID="btnSave" runat="server" Text="Save" SkinID="Green"  OnClick="btnSave_Onclick"/>
        </asp:Panel>
        <asp:Panel ID="panel1" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Minimum Stay List</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
            <asp:GridView ID="GVMiniList"  runat="server" DataKeyNames="MinimumStayId" ShowFooter="false" ShowHeader="true"  EnableModelValidation="true"   OnRowDataBound="GVMiniList_OnRowDataBound" AutoGenerateColumns="false">
                <EmptyDataRowStyle   CssClass="alert_box" Width="100%" />
                            <EmptyDataTemplate >
                                      <div class="alert_inside_GridView">
                                       <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
                                       <p class="alert_box_head">No Payment Plan Record </p>
                                       <p  class="alert_box_detail">Please select at least one.</p>
                                     </div>
                                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="40%" HeaderText="From  - To" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <Product:datePicker ID="dDatePicker" runat="server"  ClientIDMode="AutoID"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="25%" HeaderText="Amount Day" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:DropDownList ID="drpMini" runat="server" ></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="25%" HeaderText="Save" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="btnminsave" runat="server" Text="Save" CommandArgument='<%# Eval("MinimumStayId") + "," + Container.DataItemIndex %>' CommandName="miniedit" OnClick="btnminsave_OnClick" SkinID="Blue" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                <ItemTemplate>
                    <asp:ImageButton ID="imgbtnStatus" runat="server" OnClick="imgbtnStatus_StatusUPdate" CommandArgument='<%# Eval("MinimumStayId") + "," + Container.DataItemIndex + "," + Eval("Status") %>' />
                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
                                                                TargetControlID="imgbtnStatus"  DisplayModalPopupID="ModalPopupExtender2" />
                                                                <br />
                                                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
                                                                TargetControlID="imgbtnStatus" PopupControlID="Panel3" 
                                                                OkControlID="ButtonOks" 
                                                                CancelControlID="ButtonCancels" 
                                                                BackgroundCssClass="modalBackground"  />
                                                                <asp:Panel ID="Panel3" runat="server"  style="display:none; width:200px; background-color:#f2f2f2; border-width:3px; border-color:#3b5998; border-style:solid; padding:20px;">
                                                                <p style="margin:0px; padding:0px; text-align:left; width:100%;  font-weight:bold; color:Black">Are you sure to Update Status</p>
                                                                <div style="text-align:right;margin:10px 0px 0px 0px; padding:0px;">
                                                                        <asp:Button ID="ButtonOks" runat="server" Text="OK"  SkinID="Green_small" />
                                                                        <asp:Button ID="ButtonCancels" runat="server" Text="Cancel" SkinID="White_small" />
                                                                </div>
                                                                </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </div>
   
</asp:Content>
