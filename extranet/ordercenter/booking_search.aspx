<%@ Page Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="booking_search.aspx.cs" Inherits="Hotels2thailand.UI.extranet_booking_booking_search" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
<link  href="../../css/boking_detail_style.css" type="text/css" rel="Stylesheet" />
<script language="javascript" type="text/javascript" src="../../scripts/jquery-1.7.1.min.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
<link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
<script type="text/javascript" src="../../Scripts/darkman_datepicker.js?ver=001"></script>
<script language="javascript" type="text/javascript">

    $(document).ready(function () {

        $("#date_start").val("");
     
        DatepickerDual_noMin("date_start", "date_end");
        DatepickerDual_noMin("check_in_date_start", "check_in_date_end");
        DatepickerDual_noMin("check_out_date_start", "check_out_date_end");
        $("#form1 :text").focus(function () {
            var id = $(this).attr("id");
            $("#form1 :text").filter(function (index) {
                return $(this).attr("id") != id && $(this).attr("id") != "date_start" && $(this).attr("id") != "date_end"
            && $(this).attr("id") != "check_in_date_start" && $(this).attr("id") != "check_in_date_end" && $(this).attr("id") != "check_out_date_start" && $(this).attr("id") != "check_out_date_end" }).val(" ");
        });

        //$("#date_start").unbind("focus");
        //$("#date_end").unbind("focus");
    });
 
  function clickButton(e, buttonid) {
      var evt = e ? e : window.event;
      var bt = document.getElementById(buttonid);
      if (bt) {
          if (evt.keyCode == 13) {
              bt.click();
              return false;
          }
      }
  }

    //txtSearch.Attributes.Add("onkeypress", "return clickButton(event,'" + btnSearch.ClientID + "')");
    function advanceSearch() {
        if ($("#search_booking_normal").css("display") == "block") {
            $("#search_booking_normal").hide();
            $("#search_booking_advance").slideDown("fast");
        } else {
            $("#search_booking_normal").slideDown("fast");
            $("#search_booking_advance").hide();
        }
    }

    function Search(type) {
       
        $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#result").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });
        
        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
     
        $.post("../ajax/ajax_booking_search.aspx?sty=" + type + GetQuerystringProductAndSupplierForBluehouseManage("append"), post, function (data) {
         

            $("#result").html(data);
            $('html, body').animate({ scrollTop: $("#result").offset().top }, 500);
            });

        
    }
</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 
 <div id="search_booking_normal">
     
    
        <fieldset>
    <legend>Booking ID: </legend>
   
     <input type="text" onkeypress="return clickButton(event,'btn_booking_hotel_id_search');" id="txtBookingHotelId_search" name="txtBookingHotelId_search" class="Extra_textbox_big_yellow"  style="width:600px" />
    <asp:Button ID="btn_booking_hotel_id_search" ClientIDMode="Static" EnableTheming="false" CssClass="Extra_Button_small_blue" runat="server"  OnClientClick="Search('8');return false;" Text="Search" />
    </fieldset>

     <fieldset>
    <legend>Hotel Booking ID: </legend>
   
     <input type="text" onkeypress="return clickButton(event,'btn_booking_hotel_input_search');" id="txtBookingHotelId_intput_search" name="txtBookingHotelId_intput_search" class="Extra_textbox_big_yellow"  style="width:600px" />
    <asp:Button ID="btn_booking_hotel_input_search" ClientIDMode="Static" EnableTheming="false" CssClass="Extra_Button_small_blue" runat="server"  OnClientClick="Search('12');return false;" Text="Search" />
    </fieldset>

     <fieldset>
    <legend>Payment ID :</legend>
   
     <input type="text" onkeypress="return clickButton(event,'btn_payment_search');" id="txtpayment_search" name="txtpayment_search" class="Extra_textbox_big_yellow"  style="width:600px" />
    <asp:Button ID="btn_payment_search" ClientIDMode="Static" runat="server" CssClass="Extra_Button_small_blue" EnableTheming="false" OnClientClick="Search('6');return false;" Text="Search" />
    </fieldset>
    <fieldset>
    <legend>Customer Name: </legend>
    
    <input type="text" onkeypress="return clickButton(event,'btn_name_search');" id="txtname_search" name="txtname_search" class="Extra_textbox_big_yellow"  style="width:600px" />
    <asp:Button ID="btn_name_search" ClientIDMode="Static" runat="server" EnableTheming="false" CssClass="Extra_Button_small_blue"  OnClientClick="Search('1');return false;" Text="Search" />
    </fieldset>
    
    <fieldset>
    <legend>Email: </legend>
    <input type="text" onkeypress="return clickButton(event,'btn_email_search');" id="txtemail_search"  name="txtemail_search" class="Extra_textbox_big_yellow"  style="width:600px" />
    
    <asp:Button ID="btn_email_search" ClientIDMode="Static" runat="server" EnableTheming="false" CssClass="Extra_Button_small_blue" OnClientClick="Search('3');return false;" Text="Search" />
    </fieldset>
     <fieldset>
    <legend>Booking Date: </legend>
    Date From&nbsp;&nbsp;<input type="text" readonly="readonly" id="date_start" class="Extra_textbox_big_yellow" /> &nbsp;&nbsp;&nbsp;&nbsp;To &nbsp;&nbsp;<input type="text" class="Extra_textbox_big_yellow" readonly="readonly" id="date_end" />
   
   &nbsp;&nbsp;&nbsp;<input type="button" value="Search" id="btn_Address_search" class="Extra_Button_small_blue" onclick="Search('9'); return false;" /> 
    </fieldset>

     <fieldset>
    <legend>Booking Check-in Period: </legend>
    Date From&nbsp;&nbsp;<input type="text" readonly="readonly" id="check_in_date_start" class="Extra_textbox_big_yellow" /> &nbsp;&nbsp;&nbsp;&nbsp;To &nbsp;&nbsp;<input type="text" class="Extra_textbox_big_yellow" readonly="readonly" id="check_in_date_end" />
   
   &nbsp;&nbsp;&nbsp;<input type="button" value="Search" id="Button1" class="Extra_Button_small_blue" onclick="Search('10'); return false;" /> 
    </fieldset>


     <fieldset>
    <legend>Booking Check-out Period: </legend>
    Date From&nbsp;&nbsp;<input type="text" readonly="readonly" id="check_out_date_start" class="Extra_textbox_big_yellow" /> &nbsp;&nbsp;&nbsp;&nbsp;To &nbsp;&nbsp;<input type="text" class="Extra_textbox_big_yellow" readonly="readonly" id="check_out_date_end" />
    
   &nbsp;&nbsp;&nbsp;<input type="button" value="Search" id="Button2" class="Extra_Button_small_blue" onclick="Search('11'); return false;" /> 
    </fieldset>
    <%--<fieldset>
    <legend>Address: </legend>
    
    <input type="text" onkeypress="return clickButton(event,'btn_Address_search');" id="txtAddress_search" name="txtAddress_search" class="Extra_textbox_big_yellow"  style="width:600px" />
    <asp:Button ID="btn_Address_search" ClientIDMode="Static" runat="server" EnableTheming="false" CssClass="Extra_Button_small_blue" OnClientClick="Search('4');return false;" Text="Search" />
    </fieldset>--%>
    <%--<fieldset>
    <legend>Hotel Name: </legend>
   
     <input type="text" onkeypress="return clickButton(event,'btn_product_search');" id="txtproduct_search" name="txtproduct_search" class="Extra_textbox_big_yellow"  style="width:600px" />
    <asp:Button ID="btn_product_search" ClientIDMode="Static" runat="server" EnableTheming="false" CssClass="Extra_Button_small_blue" OnClientClick="Search('5');return false;" Text="Search" />
    </fieldset>--%>

    
 </div>
    <div id="search_booking_advance" style="display:none;">
   
    
    </div>
    <br /><br />
    <div id="result" >
    
    </div>
</asp:Content>