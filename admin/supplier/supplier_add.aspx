<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="supplier_add.aspx.cs" Inherits="Hotels2thailand.UI.admin_supplier_supplier_add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <asp:Panel ID="panelSupmenuSupplier" runat="server">
 <div class="SupmenuSupplier">
    <asp:HyperLink ID="linksuppolicy" runat="server">
    <div class="SupmenuSupplier_link_item">
        <p><asp:Image ID="Image2" runat="server"  ImageUrl="~/images/mail_b.png" /></p>
        <p>Payment Policy</p>
    </div>
    </asp:HyperLink>
    <asp:HyperLink ID="linkContact" runat="server"  >
    <div class="SupmenuSupplier_link_item">
        <p><asp:Image ID="imgIconContac" runat="server"  ImageUrl="~/images/contact_b.png" /></p>
        <p>Contact</p>
    </div>
    </asp:HyperLink>
     <asp:HyperLink ID="LinkAccount" runat="server">
    <div class="SupmenuSupplier_link_item">
        <p><asp:Image ID="Image1" runat="server"  ImageUrl="~/images/mail_b.png" /></p>
        <p>Account</p>
    </div>
    </asp:HyperLink>

 </div>
 </asp:Panel>
    <asp:FormView ID="FvSupplierAdd" runat="server" DataSourceID="ObjectDataSource1" 
        EnableModelValidation="True"  DataKeyNames="SupplierId"   OnDataBound="FvSupplierAdd_OndataBound" OnItemInserting="FvSupplierAdd_OnItemInserting" OnItemUpdating="FvSupplierAdd_OnItemUpdating"
         OnItemInserted="FvSupplierAdd_ItemInserted" Width="100%" OnItemCommand="FvSupplierAdd_ItemCommand">
        <InsertItemTemplate>
        <table width="100%" >
            <tr>
                <td valign="top" width="50%"> 
                    <table >
                        <tr>
                            <td>Supplier Title Common:</td>
                            <td>
                            <asp:TextBox ID="SupplierTitleCommonTextBox" runat="server" 
                                Text='<%# Bind("SupplierTitleCommon") %>' Width="300px" />
                                <asp:RequiredFieldValidator ID="RequirTitleCommon" runat="server" Display="Dynamic" Text="*required"
                                 ControlToValidate="SupplierTitleCommonTextBox" ValidationGroup="form" ></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Supplier Title:</td>
                            <td>
                            <asp:TextBox ID="SupplierTitleTextBox" runat="server" 
                                Text='<%# Bind("SupplierTitle") %>' Width="300px" />
                                <asp:RequiredFieldValidator ID="RequiredTitle" runat="server" Display="Dynamic" Text="*required"
                                 ControlToValidate="SupplierTitleTextBox" ValidationGroup="form"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Suppler Title Company:</td>
                            <td>
                            <asp:TextBox ID="SupplerTitleCompanyTextBox" runat="server" 
                                Text='<%# Bind("SupplerTitleCompany") %>' Width="300px" />
                                <asp:RequiredFieldValidator ID="RequiredTitleCompany" runat="server" Display="Dynamic" Text="*required"
                                 ControlToValidate="SupplerTitleCompanyTextBox" ValidationGroup="form"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Supplier Category:</td>
                            <td>
                            <asp:DropDownList DataSourceID="ObjectSupCate" ID="ddlCategoryId" runat="server" DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("CategoryId") %>'/>
                            </td>
                        </tr>
                        <tr>
                            <td>PaymentType:</td>
                            <td>
                            <asp:DropDownList DataSourceID="ObjectSupPayType" ID="DropDownList1" runat="server" DataTextField="Title" DataValueField="PaymentType_Id" SelectedValue='<%# Bind("PaymentTypeId") %>'/>
                            </td>
                        </tr>
                        <tr>
                            <td> Address:</td>
                            <td>
                            <asp:TextBox Wrap="True" TextMode="MultiLine" ID="AddressTextBox" runat="server" Text='<%# Bind("Address") %>'  Width="300px" Rows="5" />
                            <asp:RequiredFieldValidator ID="RequiredAddress" runat="server" Display="Dynamic" Text="*required"
                                 ControlToValidate="AddressTextBox" ValidationGroup="form"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td> Address Office</td>
                            <td>
                            <asp:TextBox Wrap="True" TextMode="MultiLine" ID="TextBox1" runat="server" Text='<%# Bind("AddressOffice") %>'  Width="300px" Rows="5" />
                            
                            </td>
                        </tr>
                    </table>
                <td valign="top" width="50%">
                    <table>
                        <tr>
                            <td>TaxVat:</td>
                            <td>
                            <asp:TextBox ID="TaxVatTextBox" runat="server" Text='<%# Bind("TaxVat") %>' />%
                            <asp:RequiredFieldValidator ID="RequiredTaxVat" runat="server" Display="Dynamic" Text="*required"
                                 ControlToValidate="TaxVatTextBox" ValidationGroup="form"></asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ID="Regtaxvat" runat="server" 
                                 ControlToValidate="TaxVatTextBox" Text="*Number Only" Display="Dynamic" ValidationExpression="^[0-9]*.$">
                                 </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>TaxService:</td>
                            <td>
                            <asp:TextBox ID="TaxServiceTextBox" runat="server" 
                                Text='<%# Bind("TaxService") %>' />%
                                <asp:RequiredFieldValidator ID="RequiredTaxService" runat="server" Display="Dynamic" Text="*required"
                                 ControlToValidate="TaxServiceTextBox" ValidationGroup="form"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>TaxLocal:</td>
                            <td>
                            <asp:TextBox ID="TaxLocalTextBox" runat="server" 
                                Text='<%# Bind("TaxLocal") %>' />%
                                <asp:RequiredFieldValidator ID="RequiredTaxLocal" runat="server" Display="Dynamic" Text="*required"
                                 ControlToValidate="TaxLocalTextBox" ValidationGroup="form"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Comment:</td>
                            <td>
                            <asp:TextBox Wrap="True" TextMode="MultiLine" ID="CommentTextBox" runat="server" Text='<%# Bind("Comment") %>' Width="300px" Rows="5" />
                            </td>
                        </tr>
                        <tr>
                            <td>Status</td>
                            <td>
                               <asp:RadioButton ID="radioEnable" runat="server" Text="Enable" GroupName="Status" Checked="true" />
                               <asp:RadioButton ID="radioDisable" runat="server" Text="Disable" GroupName="Status" />
                            </td>
                            
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                            <asp:Button ID="InsertButton" runat="server" CausesValidation="True" 
                                CommandName="Insert" Text="Insert"  SkinID="Green_small" ValidationGroup="form"/>&nbsp;
                            <asp:Button ID="InsertCancelButton" runat="server" 
                                CausesValidation="False" CommandName="Cancel" Text="Cancel" SkinID="White_small" /> 
                            
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
        </table>
        </InsertItemTemplate>
        
        
        <EditItemTemplate>
        <table width="100%" >
            <tr>
                <td align="left" colspan="2">
                 <h6><asp:Label ID="supTitle" runat="server" Text='<%# Bind("SupplierTitleCommon") %>'> </asp:Label></h6>
                </td>
            </tr>
            <tr>
                <td valign="top" width="50%"> 
                    <table>
                        <tr>
                            <td>Supplier Title Common:</td>
                            <td>
                            <asp:TextBox ID="SupplierTitleCommonTextBox" runat="server" 
                                Text='<%# Bind("SupplierTitleCommon") %>' Width="300px" />
                                <asp:RequiredFieldValidator ID="RequirTitleCommon" runat="server" Display="Dynamic" Text="*required"
                                 ControlToValidate="SupplierTitleCommonTextBox" ValidationGroup="form"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Supplier Title:</td>
                            <td>
                            <asp:TextBox ID="SupplierTitleTextBox" runat="server" 
                                Text='<%# Bind("SupplierTitle") %>'  Width="300px"/>
                                <asp:RequiredFieldValidator ID="RequiredTitle" runat="server" Display="Dynamic" Text="*required"
                                 ControlToValidate="SupplierTitleTextBox" ValidationGroup="form"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Suppler Title Company:</td>
                            <td>
                            <asp:TextBox ID="SupplerTitleCompanyTextBox" runat="server" 
                                Text='<%# Bind("SupplerTitleCompany") %>' Width="300px" />
                                <asp:RequiredFieldValidator ID="RequiredTitleCompany" runat="server" Display="Dynamic" Text="*required"
                                 ControlToValidate="SupplerTitleCompanyTextBox" ValidationGroup="form"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Supplier Category:</td>
                            <td>
                            <asp:DropDownList DataSourceID="ObjectSupCate" ID="ddlCategoryId" runat="server" DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("CategoryId") %>'/>
                            </td>
                        </tr>
                        <tr>
                            <td>PaymentType:</td>
                            <td>
                            <asp:DropDownList DataSourceID="ObjectSupPayType" ID="DropDownList1" runat="server" DataTextField="Title" DataValueField="PaymentType_Id" SelectedValue='<%# Bind("PaymentTypeId") %>'/>
                            </td>
                        </tr>
                        <tr>
                            <td> Address:</td>
                            <td>
                            <asp:TextBox Wrap="True" TextMode="MultiLine" ID="AddressTextBox" runat="server" Text='<%# Bind("Address") %>' Width="300px" Rows="5"  />
                            <asp:RequiredFieldValidator ID="RequiredAddress" runat="server" Display="Dynamic" Text="*required"
                                 ControlToValidate="AddressTextBox" ValidationGroup="form"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                         <tr>
                            <td> Address Office</td>
                            <td>
                            <asp:TextBox Wrap="True" TextMode="MultiLine" ID="TextBox1" runat="server" Text='<%# Bind("AddressOffice") %>'  Width="300px" Rows="5" />
                            
                            </td>
                        </tr>
                    </table>
                <td valign="top">
                    <table>
                        <tr>
                            <td>TaxVat:</td>
                            <td>
                            <asp:TextBox ID="TaxVatTextBox" runat="server" Text='<%# Bind("TaxVat") %>' />%
                             <asp:RequiredFieldValidator ID="RequiredTaxVat" runat="server" Display="Dynamic" Text="*required"
                                 ControlToValidate="TaxVatTextBox" ValidationGroup="form"></asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ID="Regtaxvat" runat="server" 
                                 ControlToValidate="TaxVatTextBox" Text="*Number Only" Display="Dynamic" ValidationExpression="^[0-9]*.$">
                                 </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>TaxService:</td>
                            <td>
                            <asp:TextBox ID="TaxServiceTextBox" runat="server" 
                                Text='<%# Bind("TaxService") %>' />%
                                 <asp:RequiredFieldValidator ID="RequiredTaxService" runat="server" Display="Dynamic" Text="*required"
                                 ControlToValidate="TaxServiceTextBox" ValidationGroup="form"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>TaxLocal:</td>
                            <td>
                            <asp:TextBox ID="TaxLocalTextBox" runat="server" 
                                Text='<%# Bind("TaxLocal") %>' />%
                                 <asp:RequiredFieldValidator ID="RequiredTaxLocal" runat="server" Display="Dynamic" Text="*required"
                                 ControlToValidate="TaxLocalTextBox" ValidationGroup="form"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Comment:</td>
                            <td>
                            <asp:TextBox Wrap="True" TextMode="MultiLine" ID="CommentTextBox" runat="server" Text='<%# Bind("Comment") %>' Width="300px" Rows="5"  />
                            </td>
                        </tr>
                        <tr>
                            <td>Status</td>
                            <td>
                               <asp:RadioButton ID="radioEnable" runat="server" Text="Enable" GroupName="Status" Checked="true" />
                               <asp:RadioButton ID="radioDisable" runat="server" Text="Disable" GroupName="Status" />
                            </td>
                            
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" 
                                CommandName="Update" Text="Update"  SkinID="Green_small" ValidationGroup="form" />&nbsp;
                            <asp:Button ID="updateCancelButton" runat="server" 
                                CausesValidation="True" CommandName="Cancel" Text="Cancel" SkinID="White_small" /> 
                        </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </EditItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        DataObjectTypeName="Hotels2thailand.Suppliers.Supplier" 
        InsertMethod="insertNewSupplier" SelectMethod="getSupplierById" 
        UpdateMethod="updateSupplier" TypeName="Hotels2thailand.Suppliers.Supplier"  
        OnInserted="ObjectDataSource1_Inserted" OnInserting="ObjectDataSource1_Inserting" >
        <SelectParameters>
            <asp:QueryStringParameter Name="intSuppleierId" QueryStringField="supid" 
                Type="Int32"  />
        </SelectParameters>
    </asp:ObjectDataSource>

     <asp:ObjectDataSource ID="ObjectSupCate" runat="server" 
            SelectMethod="getDictionarySupplierCat" 
            TypeName="Hotels2thailand.Suppliers.Supplier"></asp:ObjectDataSource>

    <asp:ObjectDataSource ID="ObjectSupPayType" runat="server" 
            SelectMethod="getSupplierPaymetnTypeListALL" 
            TypeName="Hotels2thailand.Suppliers.SupplierPaymentType"></asp:ObjectDataSource>
            
</asp:Content>
