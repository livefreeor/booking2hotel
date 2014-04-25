<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StaffListBox.ascx.cs" Inherits="Hotels2thailand.UI.Controls.Control_StaffListBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<script type='text/javascript'>
    function cancelClick() {
        var label = $get('ctl00_SampleContent_Label1');
        label.innerHTML = 'You hit cancel in the Confirm dialog on ' + (new Date()).localeFormat("T") + '.';
    }
    </script>
<div class="GvStaffList_userConTrl" >

<asp:GridView ID="GVStaffParent" runat="server"  OnRowDataBound="GVStaffParent_OndataBound" DataKeyNames="SupplierId" SkinID="GVParentSkin"  ShowHeader="false">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <h4><asp:Label ID="lblStaffParentHead" runat="server" ></asp:Label></h4>
                <asp:GridView ID="GvStaffList" runat="server"  OnRowDataBound="GvStaffList_RowDataBound" OnRowCommand="GvStaffList_RowCommand" >
                   <Columns>
                        <asp:TemplateField HeaderText ="No." ItemStyle-Width ="20px">         
                            <ItemTemplate>        
                                <%# Container.DataItemIndex + 1 %>    
                            </ItemTemplate>
                            </asp:TemplateField>
                           
                        <asp:TemplateField HeaderText="Name" ItemStyle-Width ="150px">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" ID="Linkdetail"  Text='<%# Eval("Title") %>'
                                             NavigateUrl='<%# String.Format("~/admin/staff/staffmanage.aspx?sid={0}", Eval("Staff_Id")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Username">
                            <ItemTemplate>
                            <asp:HyperLink runat="server" ID="Linkdetail2"  Text='<%# Eval("UserName") %>'
                                             NavigateUrl='<%# String.Format("~/admin/staff/staffmanage.aspx?sid={0}", Eval("Staff_Id")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
        
                        <asp:TemplateField HeaderText="Last Access">
                            <ItemTemplate>
                                <asp:Label Id="lblTitle" runat="server" Text='<%# Eval("LastAccess", "{0:D} , {0:T}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <%--<asp:HyperLink ID="linkstaffStat"  runat="server" ><asp:Image ID="imagestaffstat" runat="server" /></asp:HyperLink>--%>
                                <asp:ImageButton ID="imgButton" runat="server"  CommandName ="updatestatus" CommandArgument='<%# Eval("Staff_Id") %>' />
                               <%-- <asp:Button ID="Button" runat="server" Text="Click Me"  />--%><br />
                                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="imgButton" 
                                DisplayModalPopupID="ModalPopupExtender1" />
                    
                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="imgButton" PopupControlID="PNL" OkControlID="ButtonOk" CancelControlID="ButtonCancel" BackgroundCssClass="modalBackground" />
                                <asp:Panel ID="PNL" runat="server" style="display:none; width:200px; background-color:#f2f2f2; border-width:3px; border-color:#3b5998; border-style:solid; padding:20px;">
                                Are you sure you want to Update Status?
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
                </asp:GridView>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

 </div> 