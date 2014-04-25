<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="member_detail.aspx.cs"
    
     Inherits="Hotels2thailand.UI.extranet_member_member_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript" src="../../scripts/jquery-1.7.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
<link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
<script type="text/javascript" src="../../Scripts/darkman_datepicker.js"></script>

<link type="text/css" href="../../css/extranet/member.css?ver=003" rel="stylesheet" />
<script type="text/javascript" language="javascript">

    $(document).ready(function () {

        GetMemberDetail();
        
    });


    function GetMemberDetail() {
        var MembetID = GetValueQueryString("mid");

        DarkmanProgress("cus_detail");
        var StringUrl = "";


        StringUrl = "../ajax/ajax_member_detail.aspx?mid=" + MembetID + GetQuerystringProductAndSupplierForBluehouseManage("append");

        $.get(StringUrl, function (data) {

            $("#cus_detail").html(data);

            GetMemberPendding();

            GetMemberHistory();

            $("#edit_mode_click,#cancelEdit").click(function () {
                
                $("td.edit_mode :input,td.edit_mode >span,#btnedit,#div_date_birth").toggle('300');
                //$("td.edit_mode span, td.edit_mode:input").each(function () {
                //    alert($(this).html());
                //});
                return false;
            });


            //REsetPass
            $("#link_reset_pass").click(function () {

                var hdActive = $("#hd_isactive").val();
                var hdBlockrd = $("#hd_isblocked").val();


                var CusId = $(this).attr("href");

                if (hdActive == "False" || hdBlockrd == "False") {
                    DarkmanPopUpAlert(450,"Sorry! The member must be active or unblocked.");
                } else {

                    var ObjParam = { "CustomerId": CusId };

                    var con = "Are you sure to reset pass for this customer?";
                    con = con + "<label style=\"font-size:10px;color:#a58905; display:block; margin:5px 0 0 0;\">***The system will send an email to the customer in order to allow customers to change their own password.</label>";
                    DarkmanPopUpComfirmCallback(500, con, function (cusid) {
                        DarkmanProgress("cus_detail");
                        StringUrl = "../ajax/ajax_member_reset_password.aspx?mid=" + ObjParam.CustomerId + GetQuerystringProductAndSupplierForBluehouseManage("append");
                        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
                        $.post(StringUrl, post, function (data) {

                            if (data != "method_invalid") {

                                if (data == "True") {
                                    DarkmanPopUpAlert(450, "Mail reset password have been sent!");
                                }
                                else {
                                    DarkmanPopUpAlert(450, "Sorry,Email cannot send. Please contact Bluehouse tralvel Team.");
                                }

                            } else {

                            }
                        });
                    }, ObjParam)
                }
                
                return false;
            });
          
            //DatePicker_nopic("txt_date_birth");

            $("#SaveCus_edit").click(function () {

                DarkmanProgress("cus_detail");


                var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

                StringUrl = "../ajax/ajax_member_detail_update.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new");

               

                $.post(StringUrl, post, function (data) {
                   
                    if (data != "method_invalid") {
                       
                        DarkmanPopUpAlert(450, "This Member detail is updated!");
                        GetMemberDetail();

                    } else {

                    }
                });
                // false;
            });

            $("#btn_block").click(function () {
                var txt = $(this).html();
                DarkmanPopUpComfirmCallback(400, "Are you sure to " + txt + "", function () {
                    UpdateStatus();
                });
                return false;
            });
            $("#btn_active_cus").click(function () {
                
                DarkmanPopUpComfirmCallback(400, "Are you sure to activate this customer ", function () {



                    UpdateActive();
                });
                return false;
            });
            
        });

    }

    function GetMemberHistory() {
       
        DarkmanProgress("hs");
        StringUrl = "../ajax/ajax_member_history.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new");

        
        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
        $.post(StringUrl, post, function (data) {

            $("#hs").html(data);

        });
    }

    function GetMemberPendding() {
        
        DarkmanProgress("pd");
        StringUrl = "../ajax/ajax_member_pendding.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new");
        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
        $.post(StringUrl, post, function (data) {
            
            $("#pd").html(data);
        });
    }

    function UpdateStatus() {

        DarkmanProgress("cus_detail");
        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
        var StringUrl = "../ajax/ajax_member_detail_update_status.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new");
        $.post(StringUrl, post, function (data) {
            if (data != "method_invalid") {
                GetMemberDetail();
            } else {
            }
        });
    }

    function UpdateActive() {
        
        DarkmanProgress("cus_detail");
        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
        var StringUrl = "../ajax/ajax_member_detail_update_active.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new");
        $.post(StringUrl, post, function (data) {

            console.log(data);
            if (data != "method_invalid") {
                
                if (data == "True") {
                    DarkmanPopUpAlert(450, "Activation & Mail sent completed!");
                    GetMemberDetail();
                }
                else {
                    DarkmanPopUpAlert(450, "Sorry,Email cannot send. Please contact Bluehouse tralvel Team.");
                }
            } else {
            }
        });

    }

    function getBookingPage(pagestart) {

        

        DarkmanProgress("hs");
        StringUrl = "../ajax/ajax_member_history.aspx?page=" + pagestart + GetQuerystringProductAndSupplierForBluehouseManage("append");


        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
        $.post(StringUrl, post, function (data) {

            $("#hs").html(data);
           
        });

    }

</script>
    <style type="text/css">

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
    <div id="main_member">

        <div id="cus_detail">
            
        </div>
         <div id="cus_pedding_booking">
             <div class="head_title"><img src="../../images_extra/bullet.png" /> Pedding Booking</div>
             <div id="pd" ></div>
        </div>
         <div id="cus_booking_history">
            <div class="head_title"><img src="../../images_extra/bullet.png" />Booking History</div>
             <div id="hs"></div>
        </div>
        
    </div>
</asp:Content>

