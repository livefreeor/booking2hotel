<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="landmark.aspx.cs" Inherits="Hotels2thailand.UI.admin_product_landmark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="landmarkinsert">
        <table>
            <tr>
            <td>title:<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox></td>
            <td><asp:DropDownList ID="dropDestinationInsert" runat="server"></asp:DropDownList></td>
            <td><asp:DropDownList ID="dropCat" runat="server"></asp:DropDownList></td>
            <td>latitude:<asp:TextBox ID="txtLatitude" runat="server"></asp:TextBox></td>
            <td>long:<asp:TextBox ID="txtLong" runat="server"></asp:TextBox></td>
             <td><asp:Button ID="btnSaveInsert" runat="server" OnClick="btnSaveInsert_OnClick" Text="Save" /></td>
            </tr>
        </table>
    </div>

    <br />
    <br />
    <asp:DropDownList ID="dropDestination" runat="server" OnSelectedIndexChanged="dropDestination_OnSelectedIndexChanged" ></asp:DropDownList>
    <asp:DropDownList ID="dropLandmarkCat" runat="server" OnSelectedIndexChanged="dropLandmarkCat_OnSelectedIndexChanged"></asp:DropDownList>

    <br />
    <br />
    
    <asp:GridView ID="GVLandmark" runat="server" AutoGenerateColumns="false" ShowFooter="false" DataKeyNames="LandmarkID" OnRowDataBound="GVLandmark_OnRowDataBound">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lbllandmark" runat="server" Text='<%#Bind("Title") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lbllatitude" runat="server" Text='<%#Bind("Latitude") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lbllongtitude" runat="server" Text='<%#Bind("Longitude") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:TextBox ID="txtENG" runat="server"  ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:TextBox ID="txtTHai" runat="server" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnSave" runat="server" Text="Save" CommandArgument='<%# Eval("LandmarkID")+","+ DataBinder.Eval(Container, "DataItemIndex") %>' OnClick="btnSave_OnClick"  CommandName="landmarkUpdate" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
