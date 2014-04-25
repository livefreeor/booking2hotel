<%@ Page Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master"  AutoEventWireup="true" CodeFile="booking_detail.aspx.cs" Inherits="Hotels2thailand.UI.admin_booking_booking_detail" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link  href="../../css/extranet/boking_detail_style.css" type="text/css" rel="Stylesheet" />
    <link  href="../../css/extranet/boking_style.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" language="javascript" src="../../Scripts/popup.js"></script>
    <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../scripts/popup.js"></script>
    <link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
   <script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="../../Scripts/darkman_datepicker.js"></script>
   <script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
     <script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
    <script type="text/javascript" language="javascript">

        //Newversion Start here
        //----------------------------------------------------------------------------
        $(document).ready(function () {
            PageLoad();


            $("#lnCloseBooking").click(function () {

                var StatusVal = $("#hdStatus").val();
                var bookingStatus = $("#hdStatusBooking").val();
               
                var qBookingID = GetValueQueryString("bid");

                if (bookingStatus == "False") {
                    if (StatusVal != "85") {

                        addnewactivity_AndCloseBooking();

                        return false;
                    } else {

                        var productAndSup = "";
                        if (GetValueQueryString("pid") != "" && GetValueQueryString("supid") != "") {

                            productAndSup = "&pid=" + GetValueQueryString("pid") + "&supid=" + GetValueQueryString("supid");
                        }
                       
                        window.open('review_send.aspx?bid=' + qBookingID + productAndSup, '_blank');
                        return false;
                    }
                } else {
                    DarkmanPopUpComfirm(400, "Would you like to retrieve booking now?", "Openbooking();")
                }
                
            });
           
        });

        function PageLoad() {
            //[[1]]

            getBookingHead();
            bookingHistory(1);
        }

        function Openbooking() {
            var qBookingID = GetValueQueryString("bid");
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#booking_detail").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            $.get("../ajax/ajax_booking_open_save.aspx?bid=" + qBookingID, function (data) {

                if (data == "True") {
                    window.location.reload(true);
                }

            });

        }

        //#1 GetBookingHead
        function getBookingHead() {
            var qBookingID = GetValueQueryString("bid");
             
            $.get("../ajax/ajax_booking_product_head.aspx?bid=" + qBookingID + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                
                $("#booking_head").html(data);

            });
        }


        function UpdateStatus(id, type) {

            if (type == "booking") {
                var StatusVal = $("#booking_status_drop").val();


                $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#booking_detail").ajaxStart(function () {
                    $(this).show();
                }).ajaxStop(function () {
                    $(this).remove();
                });
                $.get("../ajax/ajax_booking_status_edit.aspx?bid=" + id + "&type=booking&sid=" + StatusVal + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                    if (data == "True") {


                    }

                });

            }



        }

        function Cannotresubmit() {
            DarkmanPopUpAlert(400, "Sorry!,you can not resubmit!");
        }
        function CannotEdit() {
            DarkmanPopUpAlert(400, "Sorry!,you can not Edit!");
        }
        function CannotSendVoucher() {
            DarkmanPopUpAlert(400, "Can not Send Voucher to this Customer!, Please check for the payment balance, Payment must be completed!!!");
        }
        function BookingPaymentConfirm(PayMentId, payType) {

            var qBookingID = GetValueQueryString("bid");
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#tbl_payment_list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            $.get("../ajax/ajax_booking_confirm_payment.aspx?bid=" + qBookingID + "&payid=" + PayMentId + "&pType=" + payType, function (data) {


                if (data == "True") {
                    window.location.reload(true);
                }

            });

        }
        function confirmswitchbackPayment(paymentId, payType) {
            var qBookingID = GetValueQueryString("bid");

            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#tbl_payment_list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });


            $.get("../ajax/ajax_booking_confirm_payment_rollback.aspx?bid=" + qBookingID + "&payid=" + paymentId + "&pType=" + payType, function (data) {

                if (data == "True") {
                    window.location.reload(true);
                }

            });
        }

        function Editpayment(paymentId, bookingLang) {

            var qBookingID = GetValueQueryString("bid");
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").prependTo("#booking_payment_list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });
            $.get("../ajax/ajax_booking_payment_EditForm.aspx?pmid=" + paymentId + "&lang=" + bookingLang, function (data) {
                
                DarkmanPopUp(450, data);
            });
        }


        function EditSavePayment(paymentId) {

            var qBookingID = GetValueQueryString("bid");

            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").prependTo("#booking_payment_list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });
            var post = $("#paymeny_insert_form").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_booking_payment_editsave.aspx?bid=" + qBookingID + "&pmid=" + paymentId, post, function (data) {
                console.log(data + "sdasdas");
                if (data == "True") {
                    window.location.reload(true);
                }

            });
            DarkmanPopUp_Close();
        }

        function newPaymentForm(langId) {
            var qBookingID = GetValueQueryString("bid");
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").prependTo("#booking_payment_list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });
            $.get("../ajax/ajax_booking_payment_insertForm.aspx?bid=" + qBookingID + "&lang=" + langId, function (data) {

                DarkmanPopUp(450, data);
            });
        }

        function InsertNewPayment() {
            var qBookingID = GetValueQueryString("bid");

            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").prependTo("#booking_payment_list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });
            var post = $("#paymeny_insert_form").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_booking_payment_newsave.aspx?bid=" + qBookingID, post, function (data) {

                if (data == "true") {
                    window.location.reload(true);
                }

            });
            DarkmanPopUp_Close();
        }

        function getActivityBooking() {
            var qBookingID = GetValueQueryString("bid");
            

            $.get("../ajax/ajax_booking_activity.aspx?bid=" + qBookingID + "&bt=booking", function (data) {

                $("#booking_activity").html(data);
            });
        }

        function addnewactivity() {
            var qBookingID = GetValueQueryString("bid");
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").prependTo("#booking_activity").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            $.get("../ajax/ajax_booking_activity_insertForm.aspx?bid=" + qBookingID + "&bt=booking", function (data) {

                DarkmanPopUp(450, data);
            });
            
        }
        function addnewactivity_AndCloseBooking() {
            var qBookingID = GetValueQueryString("bid");
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").prependTo("#booking_activity").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

           
            $.get("../ajax/ajax_booking_activity_insertForm.aspx?bid=" + qBookingID + "&bt=booking_close", function (data) {
               
                DarkmanPopUp(450, data);
            });

        }
        function InsertnewActivity(id, bookingType) {

            var qBookingID = GetValueQueryString("bid");

            var post = $("#activity_insert_form").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_booking_activity_insertsave.aspx?bid=" + qBookingID, post, function (data) {

                if (data == "1") {
                    if (bookingType == "product") {
                        getActivityProduct(id);
                    }

                    if (bookingType == "booking") {
                        window.location.reload(true);
                    }

                }

                if (data == "3") {
                    window.location.reload(true);

                }

            });
            DarkmanPopUp_Close();

        }

        function BookingConfirmBookingInput(confirmCatId) {

            var qBookingID = GetValueQueryString("bid");

           
            var result = "<form id=\"booking_input_insertform\" action=\"\" >";

            result = result + "<div class=\"formbox_head\">Hotel Booking Input</div>";
            result = result + "<div class=\"formbox_body\">";
            result = result + "<table id=\"tbl_payment_list\" cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:left;\">";
            result = result + "<tr><td colspan=\"2\" style=\"background-color:#ffffff;\"><div style=\"padding:5px;color:#505050;font-size:11px;font-style:italic;\">You can put your hotel rsvn no. here (This field is not mandator). Your hotel rsvn no. and Booking2hotel rsvn no. will be appeared in customer voucher.</div></td></tr>";
            result = result + "<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Hotel Booking No.</td><td>&nbsp;<input type=\"text\" id=\"input_txt\"  class=\"Extra_textbox_form\"  style=\"width:150px;\" name=\"input_txt\" /></td></tr>";

            result = result + "</table>";
            result = result + "</div>";
            result = result + "<div class=\"formbox_buttom\"><input type=\"button\" value=\"Save\" onclick=\"BookingConfirmAndBookingInPut(" + confirmCatId + ");return false;\"  class=\"btStyleGreen\" />&nbsp;<input type=\"button\" value=\"Cancel\" onclick=\"DarkmanPopUp_Close();\" class=\"btStyleWhite\" style=\" width:80px\" /></div>";

            result = result + "</form>";


            //$("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#booking_detail_confirm").ajaxStart(function () {
            //    $(this).show();
            //}).ajaxStop(function () {
            //    $(this).remove();
            //});

          
            DarkmanPopUp(450, result);
            
            //$.get("../ajax/ajax_booking_confirm.aspx?bid=" + qBookingID + "&conc=" + confirmCatId, function (data) {

            //    if (data == "1") {

            //        window.location.reload(true);
            //    }

            //});

        }
        function BookingConfirmAndBookingInPut(confirmCatId) {

            var qBookingID = GetValueQueryString("bid");

            var txtInput = $("#input_txt").val();
            
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#booking_detail_confirm").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });
            $.get("../ajax/ajax_booking_confirm_input.aspx?bid=" + qBookingID + "&conc=" + confirmCatId + "&txt=" + txtInput, function (data) {
              
                if (data == "1") {
                    DarkmanPopUp_Close();
                    window.location.reload(true);
                }

            });

        }
        function BookingConfirm(confirmCatId) {

            var qBookingID = GetValueQueryString("bid");


            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#booking_detail_confirm").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });
            $.get("../ajax/ajax_booking_confirm.aspx?bid=" + qBookingID + "&conc=" + confirmCatId, function (data) {

                if (data == "1") {

                    window.location.reload(true);
                }

            });

        }

        
        function BookingConfirmSendVoucher(confirmCatId) {
            var qBookingID = GetValueQueryString("bid");

            var qProductId = GetValueQueryString("pid");
            var qSuplierId = GetValueQueryString("supid");

            var qVal = "";
            if (qProductId != "" && qSuplierId != "") {
                qVal = "&pid=" + qProductId + "&supid=" + qSuplierId;
            }

            window.open("voucher_send.aspx?bid=" + qBookingID + qVal, '_blank');
            
            
//           


//            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#booking_detail_confirm").ajaxStart(function () {
//                $(this).show();
//            }).ajaxStop(function () {
//                $(this).remove();
//            });
//            $.get("../ajax/ajax_booking_confirm.aspx?bid=" + qBookingID + "&conc=" + confirmCatId, function (data) {

//                if (data == "1") {

//                    window.open("voucher_send.aspx?bid=" + qBookingID, '_blank');

//                   
//                    window.location.reload(true);
//                }

//            });

        }

        function ConfirmSwitchBackBooking(confirmCatId) {
            var qBookingID = GetValueQueryString("bid");

            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#booking_detail_confirm").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });
            $.get("../ajax/ajax_booking_confirm_switchBack.aspx?bid=" + qBookingID + "&conc=" + confirmCatId, function (data) {

                if (data == "True") {

                    window.location.reload(true);
                }

            });
        }

        function GetBookingButtomManageDetail() {
            BookingConfirm(4);
            
        }

        function bookingHistory(page) {
            var qBookingID = GetValueQueryString("bid");

         
            $("<img class=\"img_progress\" src=\"../../images/progress_b.gif\" alt=\"Progress\" />").prependTo("#booking_history").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });


            $.get("../ajax/ajax_booking_history.aspx?bid=" + qBookingID + "&page=" + page + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
              
                $("#booking_history").html(data);
            });
        }
        //Newversion End here
        //--------------------------------------------------------------------------
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="mainOrder">

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3">
             <div id="booking_head" >
                
             </div>
            
            </td>
        </tr>
        <tr>
            <td valign="top"   style="width:48%; " >
                 
                <div id="booking_detail"  style="background-color:#fbfbf9; padding:10px; border:1px solid #f7f3da;">
                <h4><img   src="../../images/content.png" /> Booking Detail <label style=" float:right; font-size:11px; font-weight:normal;">
                <asp:HyperLink ID="lkvoucherPrint" runat="server" Font-Bold="false" Target="_blank" >Print Voucher</asp:HyperLink>
                | <asp:HyperLink ID="lkEditBookingDetail" runat="server" Font-Bold="false"  >Edit</asp:HyperLink>
                  </label></h4>
                 <table width="100%" cellpadding="0" cellspacing="0" >
                  <tr>
                  <td class="detail_head">Name:</td>
                  <td><asp:Literal ID="ltBookingName" runat="server"></asp:Literal></td>
                  </tr>
                  <tr>
                  <td class="detail_head">Email:</td>
                  <td><asp:Literal ID="ltEmail" runat="server"></asp:Literal></td>
                  </tr>
                  
                  <tr>
                  <td class="detail_head">Phone:</td>
                  <td><asp:Literal ID="tPhone" runat="server"></asp:Literal></td>
                  </tr>
                  <tr>
                  <td class="detail_head">Mobile:</td>
                  <td><asp:Literal ID="ltMobile" runat="server"></asp:Literal></td>
                  </tr>
                  <tr>
                  <td class="detail_head">Country:</td>
                  <td><asp:Literal ID="ltCountry" runat="server"></asp:Literal></td>
                  </tr>
                  
                  <tr>
                  <td class="detail_head">Booking Receive:</td>
                  <td><asp:Literal ID="ltBookingRecieved" runat="server"></asp:Literal></td>
                  </tr>
                  <tr>
                  <td class="detail_head">Booking Status:</td>
                   <td><asp:DropDownList ID="dropStatusBooking" runat="server"  EnableTheming="false" CssClass="Extra_Drop" ClientIDMode="Static" ></asp:DropDownList>
                   <asp:Button ID="btnSaveStatus" runat="server" CssClass="Extra_Button_small_blue"  Text="Update" OnClick="btnSaveStatus_Onclick" />
                 </td>
                  </tr>
                  <tr>
                  <td class="detail_head">Status:</td>
                  <td>
                    <asp:Literal ID="ltStatus" runat="server"></asp:Literal>
                  </td>
                  </tr>
                  <tr>
                  <td class="detail_head"> <asp:Literal ID="ltrtxtremove" runat="server"></asp:Literal></td>
                  <td>
                      <asp:HyperLink ID="lnCloseBooking" NavigateUrl="#"  Font-Size="11px" runat="server" Font-Underline="true" ClientIDMode="Static" ></asp:HyperLink>
                  <%--<a id="lnCloseBooking" href="#" style="text-decoration:underline;font-size:11px;">Remove Now</a>--%>
                  <%--<asp:LinkButton ID="lkUpdateStatus" runat="server"  ForeColor="Red" 
                          Text="Close Now" ></asp:LinkButton>--%></td>
                  </tr>
                 </table>
                 <asp:HiddenField ID="hdStatus" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hdStatusBooking" ClientIDMode="Static" runat="server" />
                </div>
                <asp:Panel ID="panel_transfer" runat="server" Visible="true">
                    <div id="booking_transfer_req" style="background-color:#fbfbf9; padding:10px; border:1px solid #f7f3da; margin:10px 0px 0px 0px">
                     <h4><img   src="../../images/content.png" /> Booking Transfer & Flight Detail <label style=" float:right; font-size:11px; font-weight:normal;">
               
                </label></h4>
                 <table width="100%" cellpadding="0" cellspacing="0">
                             <tr>
                              <td class="detail_head">Arrival Flight:</td>
                              <td><asp:Literal ID="ltarrF" runat="server"></asp:Literal></td>
                              </tr>
                              <tr>
                              <td class="detail_head">Daparture Flight:</td>
                              <td><asp:Literal ID="ltDepF" runat="server"></asp:Literal></td>
                              </tr>
                              <tr>
                              <td class="detail_head">Transfer Request:</td>
                              <td><asp:Literal ID="lItemDetail" runat="server"></asp:Literal></td>
                            </tr>
                        </table>
                </div>
                </asp:Panel>
                <div id="booking_req">

                    <h4><img   src="../../images/content.png" /> Booking Requirement <label style=" float:right; font-size:11px; font-weight:normal;">
               
                </label></h4>

                    <asp:GridView ID="GVBookingItem" DataKeyNames="RequirID" AutoGenerateColumns="false" runat="server" EnableModelValidation="false"  ShowFooter="false" ShowHeader="false" OnRowDataBound="GVBookingItem_ONrowDataBound" GridLines="None" EnableTheming="false">
                    <EmptyDataTemplate>
                     <p>No requirement</p>
                    </EmptyDataTemplate>
        <Columns>
            
            <asp:TemplateField>
                <ItemTemplate>
                <div style="width:100%; margin:5px 0px 0px 0px; padding:5px 5px 5px 10px;">
                <asp:Literal ID="hdCat" runat="server" Text='<%# Eval("ProductCat") %>' Visible="false"></asp:Literal>
                <p style="width:100%;  font-size:14px; font-weight:bold; margin:0px 0px 0px 0px; padding:0px;"><asp:Label ID="optionTitle" runat="server"></asp:Label></p>
                <p style=" width:100%; margin:10px 0px 0px 0px; padding:0px;">
                <asp:DropDownList ID="dropSmoke" runat="server" SkinID="DropCustomstyle" Visible="false"></asp:DropDownList>
                <asp:DropDownList ID="dropRoom" runat="server" SkinID="DropCustomstyle" Visible="false"></asp:DropDownList>
                <asp:DropDownList ID="sropFloor" runat="server" SkinID="DropCustomstyle" Visible="false"></asp:DropDownList>
                </p>
                <div style=" width:100%; margin:10px 0px 0px 0px; padding:0px;">
                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="80%" Rows="5"></asp:TextBox>
                </div>
                </div>
                <div> <asp:Button ID="btnUpdateReq" runat="server" Text="Save"  EnableTheming="false" CssClass="Extra_Button_small_green" OnClick="Updaterequirement"  /> </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
                </div>
                <div id="booking_activity">
                 <asp:Literal ID="ltBookingActivity" runat="server"></asp:Literal>
                </div>
            </td>
            <td style="width:1%"></td>
            <td valign="top" style="width:51%" >
            <div id="booking_product_list">
                <h4><img   src="../../images/content.png" /> Product Detail <label style=" float:right; font-size:11px; font-weight:normal;">
                <asp:HyperLink ID="lkEditProductDetail" runat="server" Font-Bold="false"  >Edit</asp:HyperLink>
                </label></h4>

                    <table cellpadding="0" cellspacing="0" width="100%" style="margin:0px; padding:0px">
                                            
                                            
                        <tr><td style="height:5px;" height="5"></td></tr>            
                        <tr>
                            <td style="font-size:11px; color:#6d6e71;font-weight:bold; font-family:Tahoma">Check In:</td>
                            <td style="font-size:11px;font-family:Tahoma">
                            <asp:Literal ID="ltCheckIn" runat="server"></asp:Literal>
                            </td>
                            <td style="font-size:11px;color:#6d6e71;font-weight:bold;font-family:Tahoma">Adult:</td>
                            <td style="font-size:11px;font-family:Tahoma">
                            <asp:Literal ID="ltNumAdult" runat="server"></asp:Literal>
                            
                            </td>
                        </tr>
                        <tr><td style="height:5px;" height="5"></td></tr>
                        <tr>
                            <td style="font-size:11px;color:#6d6e71;font-weight:bold;font-family:Tahoma">Check Out:</td>
                            <td style="font-size:11px;"><asp:Literal ID="ltCheckOut" runat="server"></asp:Literal></td>
                            <td style="font-size:11px;color:#6d6e71;font-weight:bold;font-family:Tahoma">Child:</td>
                            <td style="font-size:11px;"><asp:Literal ID="ltNumChild" runat="server"></asp:Literal></td>
                        </tr>
                    </table>
                <div style=" height:5px;" ></div>
                                        
                <asp:Literal ID="ltBookingItem" runat="server"></asp:Literal>
                <p class="payment grand">Grand Total: <asp:Literal ID="ltGrandTotal" runat="server"></asp:Literal> </p>
                <p class="payment paid">Paid: <asp:Literal ID="ltPaid" runat="server"></asp:Literal> </p>
                <p class="payment balance">Balance: <asp:Literal ID="ltBalance" runat="server"></asp:Literal> </p>
            </div>
            <div id="booking_confirm">
             <asp:Literal ID="ltConfirmBlock" runat="server"></asp:Literal>
            </div>
            <div id="booking_payment">
             <asp:Literal ID="ltPaymentList" runat="server"></asp:Literal>
            
            </div>
            
            </td>

        </tr>
        <tr>
         <td colspan="3">
             <div id="booking_history">


             </div>
             <div id="booking_history_sum">


             </div>
         </td>
        </tr>
    </table>
    

    
    
    <div style="clear:both"></div>
   <div id="booking_manage_menu">
   
    </div>
    <div style="clear:both"></div>


    </div>
</asp:Content>
