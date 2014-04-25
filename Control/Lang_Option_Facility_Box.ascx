<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Lang_Option_Facility_Box.ascx.cs" Inherits="Hotels2thailand.UI.Controls.Control_Lang_Option_Facility_Box" %>
<script type="text/javascript">
    


    function showDivUnCheckBox(id_name) {
            var target = document.getElementById(id_name);
            
            target.style.display = (target.style.display == "none") ? "block" : "none";


            var grd = document.getElementById('productcontentLangBox_item_left');
            var rdoArray = grd.getElementsByTagName("input");

            for (i = 0; i <= rdoArray.length - 1; i++) {
                if (rdoArray[i].type == 'checkbox') {
                    rdoArray[i].checked = false;
                }

            }
        }
</script>

    
<style type="text/css">
    .productcontentLangBox_item a{font-size:11px;}
   </style>
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
   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
    <ContentTemplate>
   <div class="productcontentLangBox_item">
   <div id="productcontentLangBox_item_left" style=" width:100%; text-align:center">
   <p><a href="javaScript:showDivUnCheckBox('facility_template_box')">Template </a>&nbsp;:&nbsp;
   <a href="javaScript:showDivUnCheckBox('facility_used_box')">Amenity Option Used</a></p>
   
    <div id="facility_template_box" style="display:none">
        <fieldset>
        <legend>Template Box <a href="javaScript:showDivUnCheckBox('facility_template_box')">:: Close ::</a></legend>
            <asp:CheckBoxList ID="chkFac_tamplate" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table" TextAlign="Right" CssClass="chkFac_tamplateStyle">
        
            </asp:CheckBoxList>
            <div  style="text-align:left; margin:10px 0px 10px 10px;">
            <asp:Button ID="btchekbox" runat="server"  Text="Save"  Width="100px" SkinID="White" OnClick="btchekbox_Onclick" />
            </div>
        </fieldset>
    </div>
    <div id="facility_used_box" style="display:none;text-align:left;">
        <fieldset>
                            <legend>Template Box <a href="javaScript:showDivUnCheckBox('facility_used_box')">:: Close ::</a></legend>
       
         <asp:RadioButtonList ID="RadioOption" runat="server"  
         RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table" TextAlign="Right" CssClass="chkFac_tamplateStyle">
         </asp:RadioButtonList>
                            
                    
        <div  style=" text-align:left;margin:10px 0px 0px 5px;">
        <asp:Button ID="RadioBt" runat="server"  Text="Save"  Width="100px" SkinID="White" OnClick="RadioBt_OnClick" />
        </div>
        </fieldset>
         
    </div>
    <%--<div id="facility_textinput" style=" text-align:left;">
        Title
         <asp:TextBox ID="txtTitle" runat="server" Width="300px"></asp:TextBox> <asp:Button ID="btAdd" runat="server"  Text="Save" onclick="btSave_Click" SkinID="Green_small" />
     </div>  --%> 
     </div>
    
     <div style="clear:both;"></div> 
     <a name="Fac"></a>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1" DynamicLayout="true">
            <ProgressTemplate>
                <asp:Image ID="imgProgress" runat="server" ImageUrl="~/images/progress.gif" />
            </ProgressTemplate>
            </asp:UpdateProgress>
     <div id="CurrentFac" style=" text-align:center; margin-top:20px;">
        
            
           <asp:DataList ID="dlCurrentFac" runat="server" DataKeyField="FacilityId" RepeatColumns="4" RepeatDirection="Horizontal" 
           RepeatLayout="Table"  CssClass="dlCurrentFac_style"  >
           <ItemStyle CssClass="dlCurrentFac_Item_Style"/>
            <ItemTemplate>
            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
                
                
            <div id="itemlist<%# Eval("FacilityId") %>" style=" display:block">
                <asp:Image ID="imgGreenBt" ImageUrl="~/images/greenbt.png" runat="server"  />
                <a href="javaScript:showDivTwin('itemedit<%# Eval("FacilityId") %>','itemlist<%# Eval("FacilityId") %>')"><asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label></a>
            </div>
            <div id="itemedit<%# Eval("FacilityId") %>" style="display:none">
                    
                    <a href="javaScript:showDivTwin('itemedit<%# Eval("FacilityId") %>','itemlist<%# Eval("FacilityId") %>')">Cancel</a>&nbsp;:&nbsp;
                    <asp:LinkButton ID="lBtDelete"  runat="server" Text="Delete" CommandName="itemdelete" CommandArgument='<%# Eval("FacilityId") %>' OnClick="FacilityDel_Click"></asp:LinkButton> 
             </div>
            <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>
            
            
            </ItemTemplate>
           </asp:DataList>
        
     </div>   
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>  
</div>  
  
<asp:ObjectDataSource ID="Objlang" runat="server" SelectMethod="GetLanguageAll" 
    TypeName="Hotels2thailand.Production.Language"></asp:ObjectDataSource>


  
