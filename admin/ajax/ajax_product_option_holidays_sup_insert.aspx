<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajax_product_option_holidays_sup_insert.aspx.cs" Inherits="Hotels2thailand.UI.ajax_product_option_holidays_sup_insert" 
EnableViewState="false" EnableViewStateMac="false" EnableEventValidation="false" EnableSessionState="False" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>ssss</title>

</head>
<body>
     
    <form id="form1" runat="server">
        <div id="option_supplement_add_dropdown">
        <asp:DropDownList ID="ProductOptionList" runat="server" Width="400px" CssClass="dropStyle" EnableTheming="false" ClientIDMode="Static"></asp:DropDownList>
        <a href="javaScript:showDivTwin('option_supplement_add_checkbox_list','option_supplement_add_dropdown')">Advance Selection</a>
        </div>
        
        <div id="option_supplement_add_checkbox_list" style="display:none" >
            <a href="javaScript:showDivTwin('option_supplement_add_checkbox_list','option_supplement_add_dropdown')" style=" float:right">Close</a>
            <asp:CheckBoxList ID="chkOptionList" runat="server" RepeatDirection="Vertical" ClientIDMode="Static" RepeatLayout="Table" Font-Size="12px" Font-Bold="true" >
                
            </asp:CheckBoxList>
        </div>
        <br /><br />
        <div class="option_supplement_add" id="holidays_insert_single">
            <table   >
                <tr>
                   <td width="40%"><p class="titleform">Title</p></td><td width="30%"><p class="titleform">Date</p></td><td width="20%" colspan="2"><p class="titleform">Amount</p></td>
                </tr>
                <tr>
                   <td>
                       <asp:TextBox ID="txttitle" runat="server" Width="400px" ClientIDMode="Static" ></asp:TextBox> 
                   </td>
                   <td>
                   <table>
                   <tr>
                   <td><input type="radio" id="radioDate" value="1"  checked="checked" name="issingleDateCheck" />Single Date</td>
                   <td><input type="radio" id="radioDate2" value="2" name="issingleDateCheck" />Multiple Date</td>
                   </tr>
                   <tr><td colspan="2" >
                   <div id="div_single">
                   <input type="text" name="txtDateStart" id="txtDateStart" class="TextBox_Extra_normal" readonly="readonly" style=" width:120px; padding:2px;" /></div>
                   <div style="display:none" id="div_twin">
                    <table>
                        <tr>
                        <td><input type="text" name="txtDateStart_01" id="txtDateStart_01" class="TextBox_Extra_normal" readonly="readonly" style=" width:120px; padding:2px;" /></td>
                        <td><input type="text" name="txtDateEnd_01" id="txtDateEnd_01" class="TextBox_Extra_normal" readonly="readonly" style=" width:120px; padding:2px;" /></td>
                        </tr>
                    </table>
                   
                   </div>
                   </td></tr>
                   </table>
                       
                   </td>
                   <td>
                       <asp:TextBox ID="txtAmount" runat="server" BackColor="#faffbd" Text="0" ClientIDMode="Static"></asp:TextBox> 
                   </td>
                   <td >
                    <asp:Button ID="btSave" runat="server"  Width="100px" Text="Save" SkinID="Green" OnClientClick="InsertOptionSingle();return false;" />
                   </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>
