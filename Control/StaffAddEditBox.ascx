<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StaffAddEditBox.ascx.cs" Inherits="Hotels2thailand.UI.Controls.Control_StaffAddEditBox" %>
<%@ Register Src="~/Control/SupplierSelectionControl.ascx"  TagName="SupplierSelection" TagPrefix="Supplier" %>

<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>--%>
<%--OnItemUpdating="FVStaffAddEdit_ItemUpdating"--%>
<div class="Staffinsert_UserContrl">
    
    
    <asp:FormView ID="FVStaffAddEdit" runat="server" OnDataBound="FVStaffAddEdit_DataBound" DataSourceID="ObjStaffAddEdit" 
        EnableModelValidation="True" DataKeyNames="Staff_Id" OnItemInserted="FVStaffAddEdit_ItemInserted" 
        OnItemInserting="FVStaffAddEdit_ItemInserting" OnItemUpdating="FVStaffAddEdit_ItemUpdating"  >
        <EditItemTemplate>
        <table>
            <tr>
            <td class="titleHead">Staff category</td>
                <td>
                    <asp:DropDownList ID="dropCatId" runat="server"  DataSourceID="ObjDropDownStaffCat" DataTextField="Value"
                     DataValueField="Key" SelectedValue='<%# Bind("Cat_Id") %>' AppendDataBoundItems="true"  >
                    <asp:ListItem  Selected="True" Value="0" Text="Select"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <%--<tr>
                <td>Supplier_Id</td>
                <td> 
                    <asp:TextBox ID="Supplier_IdTextBox" runat="server" Text='<%# Bind("Supplier_Id") %>' />
                </td>
            </tr>--%>

            <tr>
            <td>Full Name : </td>
                <td>
                     <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
                     <asp:RequiredFieldValidator ID="requireTitle" runat="server" ControlToValidate ="TitleTextBox" Display="Dynamic"  EnableClientScript="true" 
     ToolTip="Is Required" Text="* Require"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
            <td>UserName : </td>
                <td>
                      <asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>' />
     <asp:RequiredFieldValidator ID="requireUserName" runat="server" ControlToValidate ="UserNameTextBox" Display="Dynamic"  EnableClientScript="true" 
     ToolTip="Is Required" Text="* Require"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <%--<tr>
            <td>PassWord : </td>
                <td>
                    <asp:TextBox ID="PassWordTextBox" runat="server"  Text='<%# Bind("PassWord") %>'  />
     <asp:RequiredFieldValidator ID="requirePassword" runat="server" ControlToValidate ="PassWordTextBox" Display="Dynamic"  EnableClientScript="true" 
     ToolTip="Is Required" Text="* Require"></asp:RequiredFieldValidator>
                </td>
            </tr>--%>
        </table>
           <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                CommandName="Update" Text="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel"  />
        </EditItemTemplate>
        <InsertItemTemplate>
           
            <table>
            <tr>
            <td class="titleHead">Staff category</td>
                <td>
                    <asp:DropDownList ID="dropCatId" runat="server"  DataSourceID="ObjDropDownStaffCat" DataTextField="Value"
                     DataValueField="Key" SelectedValue='<%# Bind("Cat_Id") %>' AppendDataBoundItems="true" >
                     <asp:ListItem  Selected="True" Value="0" Text="Select"></asp:ListItem>
                     </asp:DropDownList>
                </td>
            </tr>
            <%--<tr>
                <td>Supplier_Id</td>
                <td>
                    <Supplier:SupplierSelection ID="SupplierSelection" runat="server" />
                    <asp:TextBox ID="Supplier_IdTextBox" runat="server" Text='<%# Bind("Supplier_Id") %>' />
                </td>
            </tr>--%>
            <tr>
            <td>Full Name : </td>
                <td>
                     <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
                     <asp:RequiredFieldValidator ID="requireTitle" runat="server" ControlToValidate ="TitleTextBox"  EnableClientScript="true" 
     ToolTip="Is Required" Text="* Require" ValidationGroup="insertForm"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
            <td>UserName : </td>
                <td>
                      <asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>' />
     <asp:RequiredFieldValidator ID="requireUserName" runat="server" ControlToValidate ="UserNameTextBox" Display="Dynamic"  EnableClientScript="true" 
     ToolTip="Is Required" Text="* Require" ValidationGroup="insertForm"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
            <td>PassWord : </td>
                <td>
                     <asp:TextBox ID="PassWordTextBox" runat="server"  Text='<%# Bind("PassWord") %>' />
     <asp:RequiredFieldValidator ID="requirePassword" runat="server" ControlToValidate ="PassWordTextBox" Display="Dynamic"  EnableClientScript="true" 
     ToolTip="Is Required" Text="* Require" ValidationGroup="insertForm"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
           
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                CommandName="Insert" Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" OnClick="UpdateCancelButton_Click" />
        </InsertItemTemplate>
        <ItemTemplate>
            <table>
            <tr>
                <td colspan="2"><h2><asp:Label ID="CatTitleLabel" runat="server" Text='<%# Bind("CatTitle") %>' /></h2></td>
            </tr>
            <tr>
                <td valign="top">
                <asp:Label ID="Staff_IdLabel" runat="server" Text='<%# Bind("Staff_Id") %>' />
                <asp:Panel ID="Linkbox" runat="server" CssClass="Linkbox">
                        <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" 
            CommandName="Edit" Text="Edit" />
                        <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" 
                CommandName="New" Text="Create New Staff" />
                    </asp:Panel>
                </td>
                <td valign="top">
                    <table>
                        <tr>
                            <td><h5>Full name : </h5></td>
                            <td>
                                <asp:Label ID="TitleLabel" runat="server" Text='<%# Bind("Title") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td><h5>User Name : </h5></td>
                            <td>
                                <asp:Label ID="UserNameLabel" runat="server" Text='<%# Bind("UserName") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top"><h5>Pass Word : </h5></td>
                            <td style="font-size:11px;color:#cb0606; position:relative">
                                <asp:Label ID="PassWordLabel" runat="server" Text='<%# Bind("PassWord") %>' />
                                <a href="javaScript:showDiv('resetPW')"><asp:Image ID="imgbtResetPW" imageUrl="~/images/icreset.gif" runat="server" BorderWidth="0px"/></a>
                                <div id="resetPW" style="display:none">
                                <table>
                                    <tr>
                                        <td>New PassWord : </td>
                                        <td>
                                            <asp:TextBox ID="txtNewPW" runat="server"></asp:TextBox><br />
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Confirm PassWord : </td>
                                        <td>
                                            <asp:TextBox ID="txtConfirmPW" runat="server"></asp:TextBox>
                                            
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                             <asp:Button ID="btReset" runat="server" Text="Save" SkinID="Green_small"  OnClick="btReset_Onclick" ValidationGroup="resetPW"/>
                                             
                                        </td>
                                   </tr>
                                </table>
                                    <asp:RequiredFieldValidator ID="requirnewPW" runat="server"  Text="*Required" SetFocusOnError="true" EnableClientScript="true"
                                    ControlToValidate="txtNewPW" ValidationGroup="resetPW" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="requirnewPW1" runat="server"  Text="*Required"  SetFocusOnError="true" EnableClientScript="true"
                                    ControlToValidate="txtConfirmPW" ValidationGroup="resetPW" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" ControlToCompare="txtConfirmPW" ControlToValidate="txtNewPW" EnableClientScript="true"
                                     Text="Please Correct Confirm PassWord" runat="server" ></asp:CompareValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td><h5>Last Access : </h5></td>
                            <td>
                                <asp:Label ID="LastAccessLabel" runat="server" Text='<%# Bind("LastAccess") %>' />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
            
        </ItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="ObjStaffAddEdit" runat="server" SelectMethod="getStaffById" 
        TypeName="Hotels2thailand.Staffs.Staff"  OnInserting="ObjStaffAddEdit_ItemInserting"  OnUpdating="ObjStaffAddEdit_ItemUpdating" 
         UpdateMethod="updateStaffs" InsertMethod="insertStaff" >
        <SelectParameters>
            <asp:QueryStringParameter Name="intStaff" QueryStringField="sid" Type="Int16" />
        </SelectParameters>
        
        </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjDropDownStaffCat" runat="server" 
    SelectMethod="getStaffCategoryAll" TypeName="Hotels2thailand.Staffs.Staff">
    </asp:ObjectDataSource>
</div>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>