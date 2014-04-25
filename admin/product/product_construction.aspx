<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product_construction.aspx.cs" Inherits="Hotels2thailand.UI.admin_product_construction" %>

<%@ Register Src="~/Control/Lang_Construction_Content_Box.ascx" TagName="Construction_Content_Lang" TagPrefix="Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   	
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
   <h6><asp:Label ID="Destitle" runat="server" ></asp:Label> : <asp:Label ID="txthead" runat="server"></asp:Label></h6>
    
    <div class="option_add_left">
        
        <asp:Panel ID="panel5" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image7" runat="server" ImageUrl="~/images/content.png" /> Insert Box</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
            <asp:DropDownList ID="dropcatInsert" runat="server" Width="200px"></asp:DropDownList>
            <asp:TextBox ID="txtTitle" runat="server" Width="345px"></asp:TextBox>
            <p style=" margin:5px 0px 0px 0px; padding:0px;"></p>
            <asp:Button ID="btnSave" runat="server" SkinID="Green_small" Text="Save" OnClick="btnSave_Onclick" />
        </asp:Panel>

        <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Option List</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <%--<asp:HyperLink ID="lnkOptionCreate" runat="server" ><asp:Image ID="imgPlus" runat="server" ImageUrl="~/images/plus.png" /> Add New Option</asp:HyperLink>--%>
        <asp:GridView ID="GvConScat" runat="server" EnableModelValidation="true" AutoGenerateColumns="false" DataKeyNames="Key" SkinID="Nostyle"
         ShowHeader="false" OnRowDataBound="GvConScat_OnRowDataBound">
            <EmptyDataRowStyle   CssClass="alert_box" />
                            <EmptyDataTemplate>
                                      <div class="alert_inside_GridView">
                                       <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
                                       <p class="alert_box_head">No Payment Plan Record </p>
                                       <p  class="alert_box_detail">Please select at least one.</p>
                                     </div>
                                </EmptyDataTemplate>
             <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                    <h4><asp:Label ID="lblCattitle" runat="server" Text='<%#Bind("Value") %>'></asp:Label></h4>
                   <%-- <asp:HiddenField ID="hiddenParentIndex" runat="server" Value='<%# Container.DataItemIndex %>' />--%>
                    
                        <asp:GridView ID="gvChildConstruction" runat="server" AutoGenerateColumns="false" ShowHeader="false"  SkinID="Nostyle" OnRowDataBound="gvChildConstruction_OnRowDataBound" DataKeyNames="ConstructionID" >
                            <EmptyDataRowStyle   CssClass="alert_box" />
                            <EmptyDataTemplate>
                                      <div class="alert_inside_GridView">
                                       <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
                                       <p class="alert_box_head">No Payment Plan Record </p>
                                       <p  class="alert_box_detail">Please select at least one.</p>
                                     </div>
                                </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    <div style=" width:100%; text-align:left; margin:0px; padding:2px 2px 10px 2px; display:block;border-bottom:1px solid #eeeee1;">
                                        <div style="float:left;margin:0px; padding:0px; width:220px">
                                        <asp:Image ID="imgBtGreen"  runat="server" ImageUrl="~/images/greenbt.png" />&nbsp;
                                        <asp:HyperLink ID="lOption" runat="server" Text='<%# Bind("Title") %>' CssClass="sss" NavigateUrl='<%# String.Format("~/admin/product/product_construction.aspx?cons={0}", Eval("ConstructionID")) + this.AppendCurrentQueryString() %>'></asp:HyperLink>
                                        </div>
                                        <a href="javaScript:showDiv('gala<%# Eval("ConstructionID") %>')">
                                                <asp:Image ID="imgEdit" runat="server"  ImageUrl="~/images/edit.png" ImageAlign="Right" />
                                        </a>
                                            <div id="gala<%#Eval("ConstructionID") %>" style="display:none; margin:5px 0px 0px 0px; padding:0px;">
                                            <asp:TextBox ID="txtTitle" runat="server" Text='<%# Bind("Title") %>' Width="330px"></asp:TextBox>
                                            <p style=" margin:2px 0px 0px 0px; padding:0px;"></p>
                                            <asp:Button ID="tbnSave" runat="server" Text="Save" SkinID="Green_small" CommandArgument='<%# Eval("ConstructionID") + "," + Container.DataItemIndex  %>' CommandName="ancSave" OnClick="galaBtn_Cilck" />
                                            <asp:Button ID="tbnDis" runat="server" Text="Disable" SkinID="White_small" CommandArgument='<%# Eval("ConstructionID") + "," + Container.DataItemIndex %>' CommandName="ancDis" OnClick="galaBtn_Cilck" />
                                            </div>
                                       <div style="clear:both"></div>     
                                     </div>
                                     <div style="clear:both"></div>
                                    </div>
                                    <table  style=" width:100%; text-align:left; margin:0px; padding:1px;">
                                    <tr>
                                    <td style=" width:100%;margin:0px; padding:1px;"">
                                        
                                    </td>
                                    <td style=" width:100%; text-align:right;margin:0px; padding:1px;"">
                                        
                                    </td>
                                    </tr>
                                    </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>               
    </div>
    <%--Right *******************************************--%>
    <div class="option_add_right">
        <h6><asp:Label ID="lblHeadtitle" runat="server"></asp:Label></h6>
        <asp:Panel ID="screenBlock" runat="server" CssClass="screen_block" Visible="false">
            <div class="alert_box"   style="margin:150px 0px 0px 50px; width:80%" >
                       <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
                       <p class="alert_box_head">No Payment Plan Record </p>
                       <p  class="alert_box_detail">Please select at least one.</p>
            </div>
            
        </asp:Panel>
        <asp:Panel ID="panel1" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image3" runat="server" ImageUrl="~/images/content.png" /> Information</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />

            <asp:DropDownList ID="dropConCat" runat="server" Width="300px"></asp:DropDownList>
            <p style="color:#3b59aa;font-size:12px; font-weight:bold; margin:5px 0px 5px 0px;padding:0px">Service Time</p>

            <asp:RadioButton ID="radioIsTimeCheckNo" runat="server" GroupName="TimeCheck" Text="Not Show Time Service" TextAlign="Right" Checked="true" />
            <br /><p style=" margin:5px 0px 0px 0px; padding:0px"></p>
            <asp:RadioButton ID="radioIsTimeCheckYes" runat="server" GroupName="TimeCheck"  Text="Show Time Service"  TextAlign="Right" />
            
            <div id="DivTimeService" style=" display:none">
            <table class="time_table_form">
                               
                                <tr>
                                    <td valign="middle"   style=" font-size:11px; font-weight:bold; width:35px;">Open</td>
                                    <td>hrs:
                                      <asp:DropDownList ID="drpHrsStart" runat="server"></asp:DropDownList>
                                    </td>
                                    <td>mins:
                                      <asp:DropDownList ID="drpMinsStart" runat="server">
                                      <asp:ListItem Value="0" Text="00"></asp:ListItem>
                                       <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                       <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                       <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                      </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle"   style=" font-size:11px; font-weight:bold; width:35px;">Close</td>
                                    <td>hrs:
                                      <asp:DropDownList ID="drpHrsEnd" runat="server"></asp:DropDownList>
                                    </td>
                                    <td>mins:
                                      <asp:DropDownList ID="drpMinsEnd" runat="server">
                                       <asp:ListItem Value="0" Text="00"></asp:ListItem>
                                       <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                       <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                       <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                      </asp:DropDownList>
                                    </td>
                                </tr>
            </table>
            </div>
            <br /><br />
            <asp:Button ID="btnConSave" runat="server" Text="Save" SkinID="Green" OnClick="btnConSave_OnClick" />
        </asp:Panel>

        

        <asp:Panel ID="panel3" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image5" runat="server" ImageUrl="~/images/content.png" /> Construction Content Language</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
            <Product:Construction_Content_Lang ID="Content_Lang_box" runat="server" />
        </asp:Panel>

    </div>





    
    
        
    
        
    
</asp:Content>
