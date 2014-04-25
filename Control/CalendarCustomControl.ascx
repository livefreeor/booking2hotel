<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CalendarCustomControl.ascx.cs" Inherits="Hotels2thailand.UI.Controls.Control_CalendarCustomControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

   <div class="main_calendar_div">
   
   <table class="main_calendar">
        <tr>
            <td colspan="3">
                <table width="100%">
                    <tr>
                    <td class="calendar_drop_mouth">
                        <asp:DropDownList ID="dropmonth" runat="server"></asp:DropDownList>
                    </td>
                    <td class="calendar_drop_year">
                        <asp:DropDownList ID="dropyear" runat="server"></asp:DropDownList>
                    </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="calendar_month_previous"  width="10%">
                <asp:Button ID="btnprevious" runat="server" Text="<<" SkinID="Green_small" OnClick="btnprevious_OnClick" />
            </td>
            <td class="calendar_month_current" width="80%">
                <asp:Label ID="lblActiveMonth" runat="server"></asp:Label>
            </td>
            <td class="calendar_month_next" width="10%">
                <asp:Button ID="btnnext" runat="server" Text=">>" SkinID="Green_small" OnClick="btnnext_OnClick" />
            </td>
        </tr>
        <tr>
            <td class="calendar_date_area" colspan="3">
            <asp:Label ID="lbldateshow" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="calendar_today" colspan="3">
             <asp:Button ID="btnToday" runat="server" OnClick="btnToday_OnClick" Text="To Day" SkinID="Blue" />
            </td>
        </tr>
   </table>     
    </div>               
                    
                  
        