<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="booking_search.aspx.cs" Inherits="Hotels2thailand.UI.admin_booking_booking_search" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
<link  href="../../css/boking_detail_style.css" type="text/css" rel="Stylesheet" />
<script language="javascript" type="text/javascript" src="../../scripts/jquery-1.4.2.min.js"></script>
<script language="javascript" type="text/javascript">

    $(document).ready(function () {

        $("#form1 :text").focus(function () {
            var id = $(this).attr("id");
            $("#form1 :text").filter(function (index) { return $(this).attr("id") != id }).val(" ");
        });
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
      
        $.post("../ajax/ajax_booking_search.aspx?sty=" + type, post, function (data) {
            
            $("#result").html(data);
            $('html, body').animate({ scrollTop: $("#result").offset().top }, 500);
            });

        return false;
    }
</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h4><img   src="../../images/content.png" /> Booking search</h4>
<p class="contentheadedetail">You can search by type which you want </p><br />
 
 <div id="search_booking_normal">
     <fieldset>
    <legend>BookingId</legend>
   
     <input type="text" onkeypress="return clickButton(event,'btn_booking_id_search');" id="txtbooking_id_search" name="txtbooking_id_search" class="TextBox_Extra"  style="width:750px" />
    <asp:Button ID="btn_booking_id_search" ClientIDMode="Static" runat="server" SkinID="Blue" OnClientClick="Search('7');return false;" Text="Search" />
    </fieldset>
    
        <fieldset>
    <legend>Booking HotelId</legend>
   
     <input type="text" onkeypress="return clickButton(event,'btn_booking_hotel_id_search');" id="txtBookingHotelId_search" name="txtBookingHotelId_search" class="TextBox_Extra"  style="width:750px" />
    <asp:Button ID="btn_booking_hotel_id_search" ClientIDMode="Static" runat="server" SkinID="Blue" OnClientClick="Search('8');return false;" Text="Search" />
    </fieldset>
    <fieldset>
    <legend>Full Name :</legend>
    
    <input type="text" onkeypress="return clickButton(event,'btn_name_search');" id="txtname_search" name="txtname_search" class="TextBox_Extra"  style="width:750px" />
    <asp:Button ID="btn_name_search" ClientIDMode="Static" runat="server" SkinID="Blue" OnClientClick="Search('1');return false;" Text="Search" />
    </fieldset>
    <%--<fieldset>
    <legend>Guest Name :</legend>
    <input type="text" id="txtname_search" name="txtname_search" class="TextBox_Extra"  style="width:750px" />
    <asp:TextBox ID="txtguest_search" runat="server" ClientIDMode="Static" EnableTheming="false" CssClass="TextBox_Extra" Width="750px"></asp:TextBox>
    <asp:Button ID="btn_guest_search" runat="server" SkinID="Blue" OnClientClick="Search('2');" Text="Search" />
    </fieldset>onkeypress="return clickButton(event,'btn_name_search');"--%>
    <fieldset>
    <legend>Email :</legend>
    <input type="text" onkeypress="return clickButton(event,'btn_email_search');" id="txtemail_search"  name="txtemail_search" class="TextBox_Extra"  style="width:750px" />
    
    <asp:Button ID="btn_email_search" ClientIDMode="Static" runat="server" SkinID="Blue" OnClientClick="Search('3');return false;" Text="Search" />
    </fieldset>
    <fieldset>
    <legend>Address :</legend>
    
    <input type="text" onkeypress="return clickButton(event,'btn_Address_search');" id="txtAddress_search" name="txtAddress_search" class="TextBox_Extra"  style="width:750px" />
    <asp:Button ID="btn_Address_search" ClientIDMode="Static" runat="server" SkinID="Blue" OnClientClick="Search('4');return false;" Text="Search" />
    </fieldset>
    <fieldset>
    <legend>Product Name :</legend>
   
     <input type="text" onkeypress="return clickButton(event,'btn_product_search');" id="txtproduct_search" name="txtproduct_search" class="TextBox_Extra"  style="width:750px" />
    <asp:Button ID="btn_product_search" ClientIDMode="Static" runat="server" SkinID="Blue" OnClientClick="Search('5');return false;" Text="Search" />
    </fieldset>


      <fieldset>
    <legend>Payment ID :</legend>
   
     <input type="text" onkeypress="return clickButton(event,'btn_payment_search');" id="txtpayment_search" name="txtpayment_search" class="TextBox_Extra"  style="width:750px" />
    <asp:Button ID="btn_payment_search" ClientIDMode="Static" runat="server" SkinID="Blue" OnClientClick="Search('6');return false;" Text="Search" />
    </fieldset>
    
 </div>
    <div id="search_booking_advance" style="display:none;">
   
    
    </div>
    <br /><br />
    <div id="result" >
    
    </div>
</asp:Content>