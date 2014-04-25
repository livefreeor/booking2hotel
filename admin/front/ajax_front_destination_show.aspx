<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajax_front_destination_show.aspx.cs" Inherits="Hotels2thailand.UI.admin_ajax_front_destination_show" EnableViewState="false" 
EnableViewStateMac="false" EnableEventValidation="false"  %>

        
     
      <table cellspacing="0">
         <tr> 
         	<td class="top_left"></td> 
            <td class="bg_border_top"></td> 
            <td class="top_right"></td> 
         </tr>
           
         <tr>
         	<td class="bg_border_left"></td>
            
            <td class="bg_write"> 
            	<div id="sub_menu_content">                    <asp:DataList ID="dl_des" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"  RepeatLayout="Table"  DataKeyField="DesId" OnItemDataBound="dl_des_OnItemDataBound" >
                        <ItemTemplate>
                            <asp:HyperLink ID="hyTitle" Text='<%# Bind("Title") %>' runat="server"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:DataList>                </div> 
            </td>
            
            <td class="bg_border_right"></td>
         </tr>
         
         <tr>
         	<td class="bottom_left"></td>
         	<td class="bg_border_bottom"></td> 
            <td class="bottom_right"></td>
         </tr>      
      </table>
