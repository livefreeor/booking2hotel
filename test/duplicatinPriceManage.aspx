<%@ Page Language="C#" AutoEventWireup="true" CodeFile="duplicatinPriceManage.aspx.cs" Inherits="test_duplicatinPriceManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>TOTAL DUPLICATE:<asp:Label ID="lblTotal" runat="server"></asp:Label></div>

        ProductID : <asp:TextBox ID="txtProductId" runat="server"></asp:TextBox>
        ConditionID : <asp:TextBox ID="txtCOnditionID" runat="server"></asp:TextBox>
        <asp:Button ID="btnListByProduct" runat="server" Text="runByProductIdOnly" OnClick="btnListByProduct_Click" />
        <asp:Button ID="btnListByCondition" runat="server" Text="runByConditionId" OnClick="btnListByCondition_Click" />
        <asp:Button ID="btnPrepare" runat="server" Text="Prepare data" OnClick="btnPrepare_Click" />
        <asp:Button  Text="Clean Duplicat Now!" ID="btnClean" runat="server" OnClientClick="return confirm('are you sure?');" OnClick="btnClean_onclick" />
    <div>
    <asp:GridView ID="GvPrice" runat="server" AutoGenerateColumns="false" > 
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblproductId" runat="server" Text='<%# Eval("ProductId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblproductTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblOptionId" runat="server" Text='<%# Eval("OptionId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblOptionTitle" runat="server" Text='<%# Eval("OptionTitle") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblConditionId" runat="server" Text='<%# Eval("ConditionId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblCOnditionTitle" runat="server" Text='<%# Eval("ConditionTitle") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblPriceID" runat="server" Text='<%# Eval("PriceId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblDatePrice" runat="server" Text='<%# Eval("DAtePrice") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblCount" runat="server" Text='<%# Eval("CountDuplicate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>


    </form>
</body>
</html>
