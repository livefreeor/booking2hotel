<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductTypeLangBox.ascx.cs" Inherits="Hotels2thailand.UI.Controls.Control_ProductTypeLang" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
<ContentTemplate>
 <h4><asp:Image ID="imgContentIcon" runat="server" ImageUrl="~/images/content.png" /> ProductType Language</h4>
 <p class="contentheadedetail">You can Switch Language To Input Data</p>
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
       <div class="productypeContent">
            <p style=" font-size:10px; margin:0px; padding:0px; text-align:left;">ProductType Name</p>
            <asp:TextBox ID="txtTitle" runat="server" Width="375px"></asp:TextBox>
      </div>
   <div style="clear:both;"></div> 
     <div  style=" text-align:center; margin-top:10px;"> 
            <asp:Button ID="btAdd" runat="server"  Width="100px"
            Text="Save"
                 onclick="btSave_Click" SkinID="Green" />
     </div>   
   </div>
</div>  
  
<asp:ObjectDataSource ID="Objlang" runat="server" SelectMethod="GetLanguageAll" 
    TypeName="Hotels2thailand.Production.Language"></asp:ObjectDataSource>


 </ContentTemplate>
</asp:UpdatePanel>   
