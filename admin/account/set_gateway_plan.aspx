<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" Theme="hotels2theme" AutoEventWireup="true" CodeFile="set_gateway_plan.aspx.cs" Inherits="Hotels2thailand.UI.admin_account_set_gateway_plan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        
        <asp:GridView ID="GVGateWay" runat="server" AutoGenerateColumns="false"  DataKeyNames="GateWayId" ShowFooter="false" OnRowDataBound="GVGateWay_OnRowDataBound">
        
         <Columns>
           <asp:TemplateField>
           <ItemTemplate>
           <asp:Image ID="imgActive"  runat="server" />
             
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <ItemTemplate>
              <asp:ImageButton ID="imgbtnActive" runat="server" CommandArgument='<%# Eval("GateWayId") + "," + Eval("GatWayActive") %>'   OnClick="imgbtnActive_Onclick" />
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <ItemTemplate>
               <asp:Label ID="lbltitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <ItemTemplate>
                <asp:DropDownList ID="dropTimestart" runat="server"></asp:DropDownList>
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <ItemTemplate>
                <asp:DropDownList ID="dropTimeEnd" runat="server"></asp:DropDownList>
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <ItemTemplate>
                 <asp:CheckBox ID="chkDayMon" runat="server" />
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <ItemTemplate>
                <asp:CheckBox ID="chkDayTue" runat="server" />
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <ItemTemplate>
                <asp:CheckBox ID="chkDayWed" runat="server" />
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <ItemTemplate>
                <asp:CheckBox ID="chkDayThu" runat="server" />
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <ItemTemplate>
                <asp:CheckBox ID="chkDayFri" runat="server" />
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <ItemTemplate>
                <asp:CheckBox ID="chkDaySat" runat="server" />
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <ItemTemplate>
                <asp:CheckBox ID="chkDaySun" runat="server" />
           </ItemTemplate>
           </asp:TemplateField>
           
         </Columns>
        </asp:GridView>
        
        <asp:Button ID="btnSaveAll" runat="server"  Text="Save" OnClick="btnSaveAll_Onclick"/>
</asp:Content>
