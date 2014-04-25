<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="supplier_account_list.aspx.cs" Inherits="Hotels2thailand.UI.admin_supplier_supplier_account_list" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <h6><asp:Label ID="lblhead" runat="server"></asp:Label></h6><br /> <br />
 <asp:LinkButton ID="supplierCreate" runat="server"  OnClick="supplierCreate_Onclick"><asp:Image ID="Image2" ImageUrl="~/images/plus.png" runat="server" /> Add New Supplier Account</asp:LinkButton>
 <br /> 
 
  <%--<asp:HyperLink ID="supplierCreate" runat="server"> <asp:Image ID="Image1" ImageUrl="~/images/plus.png" runat="server" /> Add New Supplier Account</asp:HyperLink>--%>
    <asp:FormView ID="FormSupAccAdd" runat="server" DataSourceID="ObjectDataSource2"
        EnableModelValidation="True"  DataKeyNames="AccountId"  OnItemUpdating="FormSupAccAdd_ItemUpdating" OnItemUpdated="FormSupAccAdd_Updated"
        OnItemInserting="FormSupAccAdd_ItemInserting" OnItemInserted="FormSupAccAdd_ItemInserted" OnItemCommand="FormSupAccAdd_OnCommand">
    <EditItemTemplate>
        <h4><asp:Image ID="Image7" runat="server" ImageUrl="~/images/content.png" /> Account Insert</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <table>
            
            <tr>
                <td>Bank:</td>
                <td><asp:DropDownList DataSourceID="ObjectBank" ID="ddlBankId" runat="server" DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("BankId") %>'/></td>
            </tr>
            <tr>
                <td>Account Type:</td>
                <td><asp:DropDownList DataSourceID="ObjectAccType" ID="ddlAccountTypeId" runat="server" DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("AccountTypeId") %>'/></td>
            </tr>
            <tr>
                <td>Account Title:</td>
                <td><asp:TextBox ID="AccountTitleTextBox" runat="server" Text='<%# Bind("AccountTitle") %>' Width="700px" />
                <asp:RequiredFieldValidator ID="requireAccTitle" runat="server" Text="*required" 
                Display="Dynamic" ControlToValidate="AccountTitleTextBox"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>AccountName:</td>
                <td><asp:TextBox ID="AccountNameTextBox" runat="server" Text='<%# Bind("AccountName") %>'  Width="700px" />
                <asp:RequiredFieldValidator ID="requireAccName" runat="server" Text="*required" 
                Display="Dynamic" ControlToValidate="AccountNameTextBox"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Account Number:</td>
                <td><asp:TextBox ID="AccountNumberTextBox" runat="server" Text='<%# Bind("AccountNumber") %>' MaxLength="10"  Width="700px" />
                <asp:RequiredFieldValidator ID="requireAccNumber" runat="server" Text="*required" 
                Display="Dynamic" ControlToValidate="AccountNumberTextBox"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegAccNumber"  runat="server" Text="*Number Only & 10 charactor" ControlToValidate="AccountNumberTextBox" 
                 ValidationExpression="^[0-9]{10}"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>Account Branch:</td>
                <td><asp:TextBox ID="AccountBranchTextBox" runat="server" Text='<%# Bind("AccountBranch") %>'  Width="700px" />
                <asp:RequiredFieldValidator ID="requireAccBranch" runat="server" Text="*required" 
                Display="Dynamic" ControlToValidate="AccountBranchTextBox"></asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr>
                <td>Comment</td>
                <td><asp:TextBox ID="txtComment" runat="server" Text='<%# Bind("Comment") %>' TextMode="MultiLine" Rows="8" Width="700px" />
               
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" 
                    CommandName="Update" Text="Update"  SkinID="Green_small"/>
                    
                    &nbsp;
                    <asp:Button ID="UpdateCancelButton" runat="server" 
                    CausesValidation="False" CommandName="UpdateCancel" Text="Cancel" SkinID="White_small" />
                    
                </td>
            </tr>
        </table>
    </EditItemTemplate>
    <InsertItemTemplate>
        <h4><asp:Image ID="Image7" runat="server" ImageUrl="~/images/content.png" /> Account</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <table>
            
            <tr>
                <td>Bank:</td>
                <td><asp:DropDownList DataSourceID="ObjectBank" ID="ddlBankId" runat="server" DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("BankId") %>'/></td>
            </tr>
            <tr>
                <td>Account Type:</td>
                <td><asp:DropDownList DataSourceID="ObjectAccType" ID="ddlAccountTypeId" runat="server" DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("AccountTypeId") %>'/></td>
            </tr>
            <tr>
                <td>Account Title:</td>
                <td><asp:TextBox ID="AccountTitleTextBox" runat="server" Text='<%# Bind("AccountTitle") %>' Width="700px" />
                <asp:RequiredFieldValidator ID="requireAccTitle" runat="server" Text="*required" 
                Display="Dynamic" ControlToValidate="AccountTitleTextBox"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>AccountName:</td>
                <td><asp:TextBox ID="AccountNameTextBox" runat="server" Text='<%# Bind("AccountName") %>' Width="700px" />
                <asp:RequiredFieldValidator ID="requireAccName" runat="server" Text="*required" 
                Display="Dynamic" ControlToValidate="AccountNameTextBox"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>Account Number:</td>
                <td><asp:TextBox ID="AccountNumberTextBox" runat="server" Text='<%# Bind("AccountNumber") %>' MaxLength="10" Width="700px" />
                <asp:RequiredFieldValidator ID="requireAccNumber" runat="server" Text="*required" 
                Display="Dynamic" ControlToValidate="AccountNumberTextBox"  ></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegAccNumber"  runat="server" Text="*Number Only & 10 charactor" ControlToValidate="AccountNumberTextBox" 
                 ValidationExpression="^[0-9]{10}"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td>Account Branch:</td>
                <td><asp:TextBox ID="AccountBranchTextBox" runat="server" Text='<%# Bind("AccountBranch") %>'  Width="700px"/>
                <asp:RequiredFieldValidator ID="requireAccBranch" runat="server" Text="*required" 
                Display="Dynamic" ControlToValidate="AccountBranchTextBox"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>Comment :</td>
                <td><asp:TextBox ID="txtComment" runat="server" Text='<%# Bind("Comment") %>' TextMode="MultiLine" Rows="8" Width="700px" />
               
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="InsertButton" runat="server" CausesValidation="True" 
                    CommandName="Insert" Text="Insert" SkinID="Green_small" />
                    &nbsp;<asp:Button ID="InsertCancelButton" runat="server" 
                    CausesValidation="False" CommandName="Cancel" Text="Cancel" SkinID="White_small" />
                </td>
            </tr>
        </table>
    </InsertItemTemplate>
 
</asp:FormView>
   <br /> 
<%--OnInserted="ObjectDataSource2_Inserted"--%>
<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
    DataObjectTypeName="Hotels2thailand.Suppliers.SupplierAccount" 
    InsertMethod="insertNewSupplierAccount" SelectMethod="getSupplierAccountById" 
    TypeName="Hotels2thailand.Suppliers.SupplierAccount" 
    UpdateMethod="updateSupplierAccount">
    <SelectParameters>
        <asp:QueryStringParameter Name="shrSupplierAc" QueryStringField="acid" 
            Type="Int16" />
    </SelectParameters>
</asp:ObjectDataSource>

<asp:ObjectDataSource ID="ObjectBank" runat="server" 
        SelectMethod="getListBankTitleALL" 
        TypeName="Hotels2thailand.Suppliers.SupplierAccount">
</asp:ObjectDataSource>
    
<asp:ObjectDataSource ID="ObjectAccType" runat="server" 
        SelectMethod="getListAccountTitle" 
        TypeName="Hotels2thailand.Suppliers.SupplierAccount">
</asp:ObjectDataSource>
   
    <asp:gridview ID="gridSupplierAccount" AutoGenerateColumns="False" 
        runat="server" DataSourceID="ObjSupplierAccount" EnableModelValidation="True"  DataKeyNames="AccountId" OnRowCommand="gridSupplierAccount_RowCommand"
         OnRowDataBound="gridSupplierAccount_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText ="No." ItemStyle-Width ="20px">         
                <ItemTemplate>        
                    <%# Container.DataItemIndex + 1 %>    
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:HyperLinkField DataNavigateUrlFields="AccountId" DataNavigateUrlFormatString="supplier_account_add.aspx?supid={0}&amp;acid={0}" DataTextField="Value" HeaderText="Account Title" />--%>
            <asp:HyperLinkField DataNavigateUrlFields="SupplierId,AccountId" DataNavigateUrlFormatString= "supplier_account_list.aspx?supid={0}&amp;acid={1}"
             DataTextField="AccountTitle" Text="Account Title" HeaderText="Account Name" />
             <asp:TemplateField HeaderText="Account Number">
                <ItemTemplate>
                    <%# Eval("AccountNumber")%> (<%# Eval("AccountTypeTitle")%>)
                </ItemTemplate>
             </asp:TemplateField>
            <asp:TemplateField HeaderText="Active Account">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgButton" runat="server"  CommandName ="updateflagDefault" CommandArgument='<%# Eval("AccountId") %>' />
                               <br />
                                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="imgButton" 
                                OnClientCancel="cancelClick" DisplayModalPopupID="ModalPopupExtender1" />
                    
                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="imgButton" PopupControlID="PNL" OkControlID="ButtonOk" CancelControlID="ButtonCancel" BackgroundCssClass="modalBackground" />
                                <asp:Panel ID="PNL" runat="server" style="display:none; width:200px; background-color:#f2f2f2; border-width:3px; border-color:#3b5998; border-style:solid; padding:20px;">
                                Are you sure you want to use account to default?
                                <br /><br />
                                <div style="text-align:right;">
                                <asp:Button ID="ButtonOk" runat="server" Text="OK"  SkinID="Green_small" />
                                <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" SkinID="White_small" />
                                </div>
                                </asp:Panel>
                                <%--<asp:Label Id="lblTitle" runat="server" Text='<%# Eval("Status") %>'></asp:Label>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
        </Columns>
    </asp:gridview>

    <asp:ObjectDataSource ID="ObjSupplierAccount" runat="server" 
        SelectMethod="getSupplierAccountAllBySupplierID" 
        TypeName="Hotels2thailand.Suppliers.SupplierAccount">
        <SelectParameters>
            <asp:QueryStringParameter Name="shrSupplierId" QueryStringField="supid" 
                Type="Int16" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>


