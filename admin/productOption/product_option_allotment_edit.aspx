<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product_option_allotment_edit.aspx.cs" Inherits="Hotels2thailand.UI.admin_product_option_allotment_edit" %>
<%@ PreviousPageType VirtualPath="~/admin/productOption/product_option_allotment.aspx" %>
<%@ Register Src="~/Control/DatepickerCalendar.ascx" TagName="WeekDAy_datePicker" TagPrefix="Product" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <h6><asp:Image ID="imghead" runat="server" ImageUrl="~/images/imgheadtitle.png" />&nbsp;<asp:Label ID="Destitle" runat="server" ></asp:Label> : <asp:Label ID="txthead" runat="server"></asp:Label></h6>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
        
       
    <asp:Panel ID="panelSupplierSelection" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image3" runat="server" ImageUrl="~/images/content.png" /> Allotment Normal Edit </h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
         <asp:GridView ID="GvOptionList" runat="server" ShowFooter="false" DataKeyNames="Key" AutoGenerateColumns="false" ShowHeader="false" SkinID="Nostyle" OnRowDataBound="GvOptionList_OnRowDataBound" >
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                    <h4><asp:Label ID="lblOptionTitle"  runat="server" Text='<%# Eval("Value") %>'></asp:Label></h4>
                    <asp:GridView ID="GVAllotList" runat="server" ShowFooter="false" DataKeyNames="AllotmentId" AutoGenerateColumns="false"   OnRowDataBound="GVAllotList_OnRowdataBound" >
                    <Columns>
                        <asp:TemplateField HeaderText="No."  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblDAte" runat="server"  Text='<%#Bind("DateAllotment","{0:d-MMM-yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Room Allotment"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:DropDownList ID="dropNumRoom" runat="server" ></asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Day Cutoff"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:DropDownList ID="dropcutoff" runat="server" ></asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:RadioButtonList ID="radioStatus" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Table">
                                    <asp:ListItem Text="Enable" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Disable" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Save"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Button ID="btnSave"  runat="server" Text="Save" SkinID="Green_small" OnClick="btnSave_OnClick"  CommandArgument='<%#Eval("AllotmentId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
                        
                
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
             

        <asp:Button ID="btnQuickSave" runat="server" Text="Qucik Save" SkinID="Blue" OnClick="btnQuickSave_OnClick"/>
    </asp:Panel>

     <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    
   
    

    

</asp:Content>
