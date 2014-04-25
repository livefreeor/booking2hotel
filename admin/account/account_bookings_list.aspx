<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" Theme="hotels2theme" AutoEventWireup="true" CodeFile="account_bookings_list.aspx.cs" Inherits="Hotels2thailand.UI.admin_booking_bookings_list" EnableEventValidation="false" EnableTheming="false" EnableViewState="false" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
  <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
 <script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>

   <script language="javascript" type="text/javascript">

       $(document).ready(function () {


           var hash = location.hash;
           //alert($(window).scrollTop());
           if (hash) {
               hash = hash.replace('#', '');
               var Url = $("window").context.URL.toString();
               var Url_split1 = Url.split("#")[0];
               var Url_split2 = Url_split1.split("?")[0];

               location.href = Url_split2 + hash;

           }
           //           $(window).load(function () {
           //               alert("HELLO");
           //           });

           GetPageDefault(false);
           //--------------
           setInterval(function () {
               // 1 Sec. = 1000
               // Loop Do.. Every  1 Mins
               GetPageDefault(true);
           }, 300000);


       });

       function GetPageDefault(Isrefresh) {

           var qBookingStatus;
           var qBookingProductStatus;

           var hash = window.location.hash;

           if (!hash) {
               qBookingStatus = GetValueQueryString("bs");
               qBookingProductStatus = GetValueQueryString("bps");

               if (qBookingStatus == "" && qBookingProductStatus == "") {
                   qBookingStatus = 68;
                   qBookingProductStatus = 10;
               }

               if (qBookingStatus != "" && qBookingProductStatus == "") {
                   qBookingProductStatus = "";
               }
           } else {
               qBookingStatus = getHashVars()["bs"];
               qBookingProductStatus = getHashVars()["bps"];
               if (getHashVars()["bs"] == null && getHashVars()["bps"] == null) {
                   qBookingStatus = 68;
                   qBookingProductStatus = 10;
               }
               if (getHashVars()["bs"] != null && qBookingProductStatus == null) {
                   qBookingProductStatus = "";
               }
           }

           //getStatusProductBar(qBookingStatus, qBookingProductStatus);



           $.get("../ajax/ajax_acc_booking_list.aspx?bs=" + qBookingStatus + "&bps=" + qBookingProductStatus , function (data) {

               $("#BookingList").html(data);


               

               $(".btn_move_status").click(function () {

                   var ParamVal = $(this).attr("rel").split(',');

                   //alert(ParamVal);
                   var bookingStatus = ParamVal[0];
                   var bookingProductStatus = ParamVal[1];


                   
                  DarkmanProgress("Booking_list_block_" + bookingProductStatus);

                   var ObjStatus = { "objBookingStatus": bookingStatus };


                  $.get("../ajax/ajax_acc_booking_quick_move_status.aspx", function (data) {
                       
                       DarkmanPopUpComfirmCallback(450, data, function (obj) {

                           var StatusTo = $("#Status_Change").val();
                          // var bookingStatus = $(this).attr("href");
                          
                           var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
                           //alert(obj.objBookingStatus);
                           $.post("../ajax/ajax_acc_booking_quick_move_status.aspx?bs=" + obj.objBookingStatus + "&bst=" + StatusTo, post, function (data) {

                               //alert(data);
                               console.log(data);
                               if (data == "nosel") {
                                   DarkmanPopUpAlert(450, "Please select least one!");
                               }
                               if (data == "True") {

                                   DarkmanPopUpAlert(450, "Move Completed!");

                                   GetPageDefault();
                               }
                               
                           });

                       }, ObjStatus);
                       
                 });

                   return false;
               });

               $(".btn_close_booking").click(function () {

                   var ParamVal = $(this).attr("rel").split(',');
                   var bookingStatus = ParamVal[0];
                   var bookingProductStatus = ParamVal[1];
                  
                   var ObjStatus = { "objBookingStatus": bookingStatus };

                   DarkmanPopUpComfirmCallback(450, "Are You Aure to remove these booking now?", function (obj) {

                       DarkmanProgress("Booking_list_block_" + bookingProductStatus);

                      

                       var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
                       //alert(obj.objBookingStatus);
                       $.post("../ajax/ajax_acc_booking_quick_close.aspx?bs=" + obj.objBookingStatus, post, function (data) {

                           //alert(data);

                           if (data == "nosel") {
                               DarkmanPopUpAlert(450, "Please select least one!");
                           }
                           if (data == "True") {

                               DarkmanPopUpAlert(450, "Remove Completed!");

                               //GetPageDefault();
                           }


                       });
                   }, ObjStatus);

                   
                   //$("input[name='booking_list_checked_" + keyval + "']").removeAttr("checked");


                   return false;
               });



               var OldstatusProduct = "";
               $(".Booking_list_block").filter(function (index) {

                   var hidden = $(this).children("input[id^='hd_BodyVal_']").stop().val();
                   var hiddenOrder = $(this).children("input[id^='orderby_']").stop().val();
                   //var hidden = $(this).children(":hidden").stop().val();
                   //var hidden = $(this).children("#hd_BodyVal_" + qBookingProductStatus).val();

                   var hiddenVal = hidden.split(';');

                   var listtype = hiddenVal[0];
                   var bookingStatus = hiddenVal[1];
                   var bookingProductStatus = hiddenVal[2];

                   if (index == 0) {
                       OldstatusProduct = bookingProductStatus;
                   }

                   var qProductId = GetValueQueryString("pid");
                   var qSupplierId = GetValueQueryString("supid");

                   GetBookingListbody(bookingStatus, bookingProductStatus, listtype, hiddenOrder);

                   GetBookingListSum(bookingStatus, bookingProductStatus, listtype, hiddenOrder);

                   OldstatusProduct = bookingProductStatus;


                  
               });

           });
       }


       function GetBookingListbody(bookingStatus, bookingProductStatus, listtype, order) {

           $("<p class=\"progress_block\"><img class=\"img_progress\" src=\"http://www.booking2hotels.com/images/progress.gif\" alt=\"Progress\" /></p>").appendTo("#Booking_list_" + bookingProductStatus).ajaxStart(function () {
               $(this).show();
           }).ajaxStop(function () {
               $(this).remove();
           });


           $.get("../ajax/ajax_acc_booking_list_body.aspx?bs=" + bookingStatus + "&bps=" + bookingProductStatus + "&lTpye=" + listtype + "&order=" + order , function (data) {
               $("#Booking_list_" + bookingProductStatus).html(data);



               $("input[name^='main_checked_']").click(function () {
                   var keyval = $(this).val();
                   if ($(this).is(":checked")) {
                       $("input[name='booking_list_checked_" + keyval + "']").attr("checked", "checked");
                   } else {
                       $("input[name='booking_list_checked_" + keyval + "']").removeAttr("checked");
                   }
               });


           });

       }



       function GetBookingListSum(bookingStatus, bookingProductStatus, listtype, order) {
           $.get("../ajax/ajax_acc_booking_list_sum.aspx?bs=" + bookingStatus + "&bps=" + bookingProductStatus + "&lTpye=" + listtype + "&order=" + order, function (data) {

               $("#Booking_sum_" + bookingProductStatus).html(data);

               if ($("#hd_page_" + bookingProductStatus).length) {
                   var pageVal = $("#hd_page_" + bookingProductStatus).val();


                   $("#Booking_sum_" + bookingProductStatus).find("a").each(function (index) {
                       $(this).removeClass("page_list_active");
                       $(this).addClass("page_list");
                       var pageA = $(this).attr("title");
                       if (pageA == pageVal) {
                           $(this).removeClass("page_list");
                           $(this).addClass("page_list_active");
                       }
                   });

               }
           });
       }

       function getBookingPage(pagestart, SBookingProduct, SBooking, listtype, pageactive, order) {

           $("<p class=\"progress_block\"><img class=\"img_progress\" src=\"http://www.booking2hotels.com/images/progress.gif\" alt=\"Progress\" /></p>").appendTo("#Booking_list_" + SBookingProduct).ajaxStart(function () {
               $(this).show();
           }).ajaxStop(function () {
               $(this).remove();
           });
           $("#hd_page_" + SBookingProduct).val(pageactive);


           $.get("../ajax/ajax_acc_booking_list_body.aspx?bs=" + SBooking + "&bps=" + SBookingProduct + "&lTpye=" + listtype + "&page=" + pagestart + "&order=" + order , function (data) {

               $("#Booking_list_" + SBookingProduct).fadeOut('fast', function () {
                   $("#Booking_list_" + SBookingProduct).html(data);
                   $("#Booking_list_" + SBookingProduct).fadeIn('fast', function () {

                       var height = $("#Booking_list_" + SBookingProduct).height();

                       $('html, body').animate({ scrollTop: $("#Booking_list_" + SBookingProduct).offset().top + height }, 500);

                       var pageVal = $("#hd_page_" + SBookingProduct).val();

                       $("#Booking_sum_" + SBookingProduct).find("a").each(function (index) {
                           $(this).removeClass("page_list_active");
                           $(this).addClass("page_list");
                           var pageA = $(this).attr("title");
                           if (pageA == pageVal) {
                               $(this).removeClass("page_list");
                               $(this).addClass("page_list_active");
                           }
                       });
                   });
               });
           });

       }

       function Getpage(Sbooking, SBookingProduct) {

           if (SBookingProduct == "") {
               window.location.hash = "?bs=" + Sbooking;
           } else {
               window.location.hash = "?bs=" + Sbooking + "&bps=" + SBookingProduct;
           }

           var qBookingStatus;
           var qBookingProductStatus;

           qBookingStatus = Sbooking;
           qBookingProductStatus = SBookingProduct;



           $.get("../ajax/ajax_acc_booking_list.aspx?bs=" + qBookingStatus + "&bps=" + qBookingProductStatus , function (data) {

               $("#BookingList").html(data);

               $(".Booking_list_block").filter(function (index) {

                   var hidden = $(this).children("input[id^='hd_BodyVal_']").stop().val();
                   var hiddenOrder = $(this).children("input[id^='orderby_']").stop().val();

                   var hiddenVal = hidden.split(';');

                   var listtype = hiddenVal[0];
                   var bookingStatus = hiddenVal[1];
                   var bookingProductStatus = hiddenVal[2];
                   var qProductId = GetValueQueryString("pid");
                   var qSupplierId = GetValueQueryString("supid");


                   GetBookingListbody(bookingStatus, bookingProductStatus, listtype, hiddenOrder);
                   GetBookingListSum(bookingStatus, bookingProductStatus, listtype, hiddenOrder);

               });

           });


       }

       function GetpageBookingStatus(Sbooking, SBookingProduct) {

          // getStatusProductBar(Sbooking, SBookingProduct);
           if (SBookingProduct == "") {
               window.location.hash = "?bs=" + Sbooking;
           } else {
               window.location.hash = "?bs=" + Sbooking + "&bps=" + SBookingProduct;
           }

           var qBookingStatus;
           var qBookingProductStatus;

           qBookingStatus = Sbooking;
           qBookingProductStatus = SBookingProduct;



           $.get("../ajax/ajax_acc_booking_list.aspx?bs=" + qBookingStatus + "&bps=" + qBookingProductStatus , function (data) {

               $("#BookingList").html(data);

               $(".Booking_list_block").filter(function (index) {

                   //var hidden = $(this).children(":hidden").stop().val();
                   var hidden = $(this).children("input[id^='hd_BodyVal_']").stop().val();
                   var hiddenOrder = $(this).children("input[id^='orderby_']").stop().val();
                   var hiddenVal = hidden.split(';');

                   var listtype = hiddenVal[0];
                   var bookingStatus = hiddenVal[1];
                   var bookingProductStatus = hiddenVal[2];
                   var qProductId = GetValueQueryString("pid");
                   var qSupplierId = GetValueQueryString("supid");

                   GetBookingListbody(bookingStatus, bookingProductStatus, listtype, hiddenOrder);
                   GetBookingListSum(bookingStatus, bookingProductStatus, listtype, hiddenOrder);

               });

           });


       }

       //function getStatusProductBar(bookingStatus, bookingProductStatus) {

       //    $.get("../ajax/ajax_booking_list_statusBar.aspx?bs=" + bookingStatus + "&bps=" + bookingProductStatus , function (data) {

       //        $("#BookingProductStatusBar_block").html(data);
       //    });
       //}

       function GetByOrder(bookingProductStatus) {
           $("<img class=\"img_progress\" src=\"http://www.booking2hotels.com/images/progress.gif\" alt=\"Progress\" />").insertBefore("#drop_orderBy_" + bookingProductStatus).ajaxStart(function () {
               $(this).show();
           }).ajaxStop(function () {
               $(this).remove();
           });

           var orderVal = $("#drop_orderBy_" + bookingProductStatus).val();
           $("orderby_" + bookingProductStatus).val(orderVal);

           var block = $("#Booking_list_block_" + bookingProductStatus);

           var hidden = block.children("input[id^='hd_BodyVal_']").stop().val();
           var hiddenOrder = block.children("input[id^='orderby_']").stop().val();

           var hiddenVal = hidden.split(';');

           var listtype = hiddenVal[0];
           var bookingStatus = hiddenVal[1];
           var bookingProductStatus = hiddenVal[2];

           var qProductId = GetValueQueryString("pid");
           var qSupplierId = GetValueQueryString("supid");

           //alert(listtype + "--- " + bookingStatus + "---" + bookingProductStatus);
           GetBookingListbody(bookingStatus, bookingProductStatus, listtype, orderVal);
           GetBookingListSum(bookingStatus, bookingProductStatus, listtype, orderVal);
       }
   </script>
   <style  type="text/css">
       #BookingProductStatusBar_block
       {
           margin:0px 0px 15px 0px;
           padding:0px 0px 0px 0px;
     
       }
     
       .chk_out
{
    margin:3px 0px 0px 0px;
    padding:0px;
     height:10px;
     color:#868b94;
     font-size:11px;
     
}
.bookingList_priceTotal
{
    margin:0px;
    padding:0px;
     font-weight:bold;
}
.BookingStatusProductHead
{
     font-size:12px;
     color:#6A785A;
}
.Booking_list_block
{
    margin:0px 0px 20px 0px;
}
.chk_in
{
    margin:3px 0px 0px 0px;
    padding:0px;
    height:10px;
    color:#4f4e4e;
    
    
    font-size:11px;
}
    .ProcessCheckTitel
    {
         
     font-weight:bold;
     text-align:center;
     padding:5px 0px 10px 600px;
     margin:0px 0px 0px 0px;
     border-top:1px solid #e2d6a2;
     border-bottom:1px solid #e2d6a2;
    }
     .ProcessCheckTitel p
    {
         float:left;
         
         color:#6A785A;
         font-size:11px;
     margin:5px 0px 0px 10px;
     padding:0px 0px 0px 0px;   
    }
     .ProcessCheckTitel p span
     {
          font-weight:bold;
           color:#c6b257;
           margin:0px 5px 0px 0px;
     padding:0px 0px 0px 0px;
     }
     #booking_status
     {
         margin:0px 0px 0px 20px;
         padding:0px;
     }
     #booking_status a
     {
         float:left;
         margin:40px 5px 0px 5px;
         padding:0px;
         width:105px;
         background-color:#f2ebbd;
         height:25px;
         line-height:25px;
         font-weight:normal;
         text-align:center;
         font-size:11px;
         color:#6A785A;
     }
       .gateway_item li {
           list-style:none;
           font-size:10px;
           font-weight:bold;
       }
       .booking_id_display {
           text-align:center;
           font-size:10px;
       }   .bookingList_Page
{
     width:100%;
     margin:0px;
     padding:0px;
     
}
.bookingList_Page a
{
    display:block;
    float:left;
     margin:2px 0px 2px 10px;
     padding:0px 3px 0px 10px;
     width:15px;
     height:15px;
     
     
}
#BookingList {
           width:100%;
       }
.page_list
{
    background-color:#F8F3DB;
   
}

.page_list_active
{
    
    background-color:#8C9051;

    color:#ffffff;
}
   </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="main_booking_list"  style="width:950px; margin: 0 auto;">
 <div id="booking_status">
<asp:Literal ID="bookingStatus" runat="server"></asp:Literal>
</div>
<div style="clear:both"></div>

<div id="BookingProductStatusBar_block"  >
    <div class="ProcessCheckTitel">
        <p><span>1</span>Payment </p><p><span>2</span>Input</p><p><span>3</span>Confirm</p>
        <p><span>4</span>Settle</p>
        <p><span>5</span>Com.. Payment</p>
        
        <div style="clear:both;"></div>
        
    </div>
</div>     

<div id="BookingList" >
</div>  
<div class="scroll"></div>
</div>

</asp:Content>

