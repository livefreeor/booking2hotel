﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DatepickerCalendar_Extra.ascx.cs" Inherits="Hotels2thailand.UI.Controls.Control_DatepickerCalendar_Extra" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    
       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
             <ContentTemplate>--%>
         <style type="text/css">
            .datepickerBlock
            {
                  margin:0px;
                  padding:0px;
                  position:relative;
            }
            .datepicker_start{ margin:0px;padding:0px;}
            .datepicker_start_display{margin:0px;padding:0px; position:absolute; top:1px; left:0px;}
        </style>       
        <div class="DatePickerBlock">
            <table  width="450px" style="text-align:left"  >
                 <tr>
                  <td  class="hdate">Date Range From</td>
                    <td   valign="bottom" style="position:relative">
                     <div class="datepickerBlock">
                    <div class="datepicker_start"><asp:TextBox runat="server" ID="txtDateStart"  CssClass="dateTxtBox" SkinID="Datehidden"   /></div>
                    <div class="datepicker_start_display"><asp:TextBox runat="server" ID="txtDateStartReal" ReadOnly="true" ></asp:TextBox></div>
                    </div>
                    </td>
                     <td   class="hdate">To</td>
                    <td valign="bottom" style="position:relative">
                     <div class="datepickerBlock">
                    <div class="datepicker_start"><asp:TextBox runat="server" ID="txtDateEnd"  CssClass="dateTxtBox" SkinID="Datehidden"  /></div>
                    <div class="datepicker_start_display"><asp:TextBox runat="server" ID="txtDateEndReal" ReadOnly="true" ></asp:TextBox></div>
                    </div>
                    </td>
                 </tr>
            </table>
        </div>
            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>