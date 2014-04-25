<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="com_monthly_rate.aspx.cs" Inherits="Hotels2thailand.UI.admin_account_com_monthly_rate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript" src="../../Scripts/popup.js"></script>
    <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
    <link href="../../css/account/bhtmanage.css" type="text/css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h6>Monthly Rate Manage Detail : Hotel List</h6>
    <h1>Regular (ชำระตอบรอบปกติ)</h1>
    <asp:GridView ID="gvhotelListregular" runat="server" DataKeyNames="ProductId" ClientIDMode="Static"  CssClass="tbl_account" GridLines="None" EnableModelValidation="false" Width="100%" CellPadding="0" CellSpacing="1" AutoGenerateColumns="false"  OnRowDataBound="gvhotelListregular_RowDataBound" EnableTheming="false" ShowFooter="false">
        <RowStyle HorizontalAlign="Center" />
        <HeaderStyle HorizontalAlign="Center" />
        <Columns>
            <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hotel Code">
                <ItemTemplate>
                     <%# Eval("ProductCode") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hotel Name">
                <ItemTemplate>
                     <%# Eval("ProductTitle") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status Process">
                <ItemTemplate>
                     <%# Eval("StatusTitle") %>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Commission Start">
                <ItemTemplate>
                     <%# String.Format("{0:dd MMM yyyy}", Eval("CommissiontStart"))  %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="จำนวนรอบบิล">
                <ItemTemplate>
                     <%# Eval("MonthNum") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รอบบิล ล่าสุด">
                <ItemTemplate>
                <asp:Label ID="lblDAte" runat="server" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="จำนวน Payment ที่ค้างชำระ">
                <ItemTemplate>
                <%# Eval("NumPending") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Detail">
                <ItemTemplate>
                <asp:HyperLink ID="lnView" runat="server"  Target="_blank" NavigateUrl='<%# String.Format("/admin/account/com_monthly_rate_detail.aspx?pid={0}", Eval("ProductId")) %>'>Detail</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>

     <h1>Advance (ชำระล่วงหน้า)</h1>
    <asp:GridView ID="GvAdvance" runat="server" DataKeyNames="ProductId" ClientIDMode="Static"  CssClass="tbl_account" GridLines="None" EnableModelValidation="false" Width="100%" CellPadding="0" CellSpacing="1" AutoGenerateColumns="false"  EnableTheming="false" ShowFooter="false" OnRowDataBound="GvAdvance_RowDataBound">
        <RowStyle HorizontalAlign="Center" />
        <HeaderStyle HorizontalAlign="Center" />
        <Columns>
           <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hotel Code">
                <ItemTemplate>
                     <%# Eval("ProductCode") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hotel Name">
                <ItemTemplate>
                     <%# Eval("ProductTitle") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status Process">
                <ItemTemplate>
                     <%# Eval("StatusTitle") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Commission Start">
                <ItemTemplate>
                     <%# String.Format("{0:dd MMM yyyy}", Eval("CommissiontStart"))  %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="จำนวนรอบบิล">
                <ItemTemplate>
                     <%# Eval("MonthNum") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รอบบิล ล่าสุด">
                <ItemTemplate>
                <asp:Label ID="lblDAte" runat="server" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="จำนวน Payment ที่ค้างชำระ">
                <ItemTemplate>
                <%# Eval("NumPending") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Detail">
                <ItemTemplate>
                <asp:HyperLink ID="lnView" runat="server"  Target="_blank" NavigateUrl='<%# String.Format("/admin/account/com_monthly_rate_detail.aspx?pid={0}", Eval("ProductId")) %>'>Detail</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>
</asp:Content>

