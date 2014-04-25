<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DatepickerCalendar-single.ascx.cs" Inherits="Hotels2thailand.UI.Controls.Control_DatepickerCalendar_single" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
     <link href="../css/extranet/extranet_style_core.css" type="text/css" rel="Stylesheet" />
        <style type="text/css">
            .datepickerBlock
            {
                  margin:5px 0px 5px 0px;
                  padding:0px;
                  position:relative;
            }
            .datepicker_start{ margin:0px;padding:0px;}
            .datepicker_start_display{margin:0px;padding:0px; position:absolute; top:1px; left:0px;}
        </style>       
        
            
         <div class="datepickerBlock">
                    
          <div class="datepicker_start"><asp:TextBox runat="server" ID="txtDateStart"  CssClass="dateTxtBox"  SkinID="Datehidden"  /></div>
          <div class="datepicker_start_display"><asp:TextBox runat="server" CssClass="Extra_Drop" Width="130" EnableTheming="false" ID="txtDateStartReal"  ReadOnly="true"  ></asp:TextBox></div>
                    
         </div>
                  
                    
                  
        