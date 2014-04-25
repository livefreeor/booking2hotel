<%@ Control Language="C#" AutoEventWireup="true" CodeFile="langswitch.ascx.cs" Inherits="Hotels2thailand.UI.Controls.Control_langswitch" %>
<style type="text/css">

TD.bgstyle {   COLOR: #215dc6;  BACKGROUND-COLOR: #dde4ea}

TD.bgstyleleft {  COLOR: #215dc6; BACKGROUND-COLOR: #dde4ea}

TD.bgstyleright {   COLOR: #215dc6; BACKGROUND-COLOR: #dde4ea}

</style>


<div id="langswitch" class="langswitch">
        <p><asp:Label ID="lblCountry" runat="server" ClientIDMode="Static"></asp:Label>&nbsp;&nbsp;
        <a href="javaScript:showDiv('langS')"><asp:Image ID="imgbt" runat="server" ImageUrl="~/images/Bttriangle.png" ClientIDMode="Static" /></a></p>
            
        <p><asp:Image ID="imgLang" CssClass="lang_flag" EnableTheming="false" runat="server" ClientIDMode="Static" /></p>
</div>
<div id="langS" style="display:none">
    <asp:GridView ID="GvLangList" runat="server" DataSourceID="ObjlangList"  GridLines="None"
        EnableModelValidation="True"  DataKeyNames="LanguageID" ShowHeader="false" OnRowDataBound="GvLangList_RowdataBound"
         OnRowCommand="GvLangList_Rowcommand" BackColor="#a9a9a9"  AutoGenerateColumns="false" 
         >
        <Columns>
            <asp:TemplateField ControlStyle-BorderWidth="0px">
                <ItemTemplate>
                    <p><asp:Image ID="imgflagS" runat="server" ClientIDMode="Static" />&nbsp;
                    <asp:LinkButton ID="linkBt" runat="server" CommandName="switch" CommandArgument='<%# Bind("LanguageID") %>' ClientIDMode="Static"></asp:LinkButton></p>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjlangList" runat="server" 
        SelectMethod="GetLanguageAll" TypeName="Hotels2thailand.Production.Language">
    </asp:ObjectDataSource>
</div>
