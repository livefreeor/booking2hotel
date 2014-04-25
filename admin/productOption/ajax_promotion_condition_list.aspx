<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajax_promotion_condition_list.aspx.cs" Inherits="Hotels2thailand.UI.admin_ajax_promotion_condition_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GVOptionList" runat="server" ShowFooter="false" ShowHeader="false" DataKeyNames="Key"  
        AutoGenerateColumns="false" EnableModelValidation="false" OnRowDataBound="GVOptionList_OnRowDataBound" SkinID="Nostyle">
            <EmptyDataRowStyle    Width="100%" />
                   <EmptyDataTemplate >
                                      <div class="alert_inside_GridView">
                                       <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
                                       <p class="alert_box_head">No Option & Condition Active</p>
                                       <p  class="alert_box_detail">this promotion are not comprementary for any Room Type</p>
                                     </div>
                   </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                     <ItemTemplate>
                        <p style=" margin:0px; padding:0px; font-size:12px; color:Black; font-weight:bold"> <%# Container.DataItemIndex + 1 %>&nbsp<asp:Label ID="lblOptionTItle" runat="server" Text='<%# Bind("Value") %>'></asp:Label></p>
                        <asp:GridView ID="GVConditionList" runat="server" ShowFooter="false" ShowHeader="false" DataKeyNames="Key"  AutoGenerateColumns="false" 
                         EnableModelValidation="false" OnRowDataBound="GVConditionList_OnRowDataBound" SkinID="Nostyle">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        &nbsp;&nbsp;&nbsp;<asp:Image ID="imgreen" runat="server" ImageUrl="~/images/greenbt.png" />&nbsp;<asp:Label ID="lblConditionTitle" runat="server" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                     </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
