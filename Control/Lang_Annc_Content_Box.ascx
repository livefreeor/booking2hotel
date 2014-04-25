<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Lang_Annc_Content_Box.ascx.cs" Inherits="Hotels2thailand.UI.Controls.Control_Lang_Annc_Content_Box" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
    <ContentTemplate>--%>
 
 <div class="productcontentLangBox">
    <div class="productmenuBox">
        <asp:DataList ID="DlLang" runat="server" DataSourceID="Objlang" DataKeyField="LanguageID" RepeatDirection="Horizontal"
  RepeatLayout="Table" SeparatorStyle-BorderWidth="0px" GridLines="None"  ItemStyle-BorderWidth="0px" ItemStyle-BorderStyle="None"
   OnItemDataBound="DlLang_OnitemdataBound"  OnItemCommand="DlLang_OnitemCommand" >
    <ItemTemplate>
        
        <asp:LinkButton ID="LinkBtLang" runat="server" CommandArgument='<%# Bind("LanguageID") %>' CommandName="selectLang"></asp:LinkButton>
    </ItemTemplate>
            
 </asp:DataList>
    </div>
   <div class="productcontentLangBox_item">
   <div class="productcontentLangBox_item_left">
        <table style="width:100%">
            <tr>
                <td><p>Title</p></td>
                <td><asp:TextBox ID="txtTitle" runat="server" Width="500px"></asp:TextBox></td>
            </tr>
            
            <tr>
               <td><p>Detail</p></td>
               <td><asp:TextBox ID="txtdetail" runat="server" TextMode="MultiLine" Width="500px" Rows="8"></asp:TextBox></td>
            </tr>
        </table>
        
     </div>
     <div class="productcontentLangBox_item_right">
        
     </div>   
     <div style="clear:both;"></div> 
     <div  style=" text-align:center; margin-top:10px;">
            <asp:Button ID="btAdd" runat="server"  Text="Save"  Width="100px" onclick="btSave_Click" SkinID="Green" />
     </div>   
    </div>
</div>  
  
<asp:ObjectDataSource ID="Objlang" runat="server" SelectMethod="GetLanguageAll" 
    TypeName="Hotels2thailand.Production.Language"></asp:ObjectDataSource>


 <%--</ContentTemplate>
</asp:UpdatePanel>   --%>
