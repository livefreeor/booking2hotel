<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="showNewsletterList.aspx.cs" Inherits="Hotels2thailand.UI.ShowNewsletterList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript" src="../../scripts/jquery-1.7.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
    <link type="text/css" href="../../css/extranet/promotion_extra.css" rel="stylesheet" />
    <link href="/css/newsletter/newsletter.css"type="text/css" rel="Stylesheet" />
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var qstatus = GetValueQueryString("temp");

           // $("#hd_newscat").val(5);
            //default list for Manual(all customer)
            GetNewsletterLsit(5,qstatus);

            //$("#extranet_tab li").click(function () {
            //    $("#extranet_tab li").removeClass("active");
            //    $(this).addClass("active");
            //    $("#hd_newscat").val($(this).attr("id"));
            //    GetNewsletterLsit($(this).attr("id"), qstatus);
            //});


            

        });

        function DeleteMail() {
            DarkmanPopUpComfirmCallback(400, "Are you sure to Delete ?", function () {


                if ($("input[name='chk_news']:checked").length > 0) {


                    var qstatus = GetValueQueryString("temp");

                    $("<p class=\"progress_block_main\"><img class=\"img_progress\" src=\"../../images_extra/preloader_blue.gif\" alt=\"Progress\" /></p>").insertBefore("#newsletter_result").ajaxStart(function () {
                        $(this).show();
                    }).ajaxStop(function () {
                        $(this).remove();
                    });

                    var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
                    var StringUrl = "../ajax/ajax_newsletter_del_sel.aspx?temp=" + qstatus + GetQuerystringProductAndSupplierForBluehouseManage("append");

                    $.post(StringUrl, post, function (data) {

                        if (data == "True") {

                            GetNewsletterLsit(5, qstatus);
                        } else {

                        }
                        console.log(data);

                    });

                }
                else {
                    DarkmanPopUpAlert(400, "Please least one to Empty!")
                }
                // console.log($("input[name^='chk_news_']:checked").length);

            });

        }


        function Emptymail() {
            DarkmanPopUpComfirmCallback(400, "Are you sure to Empty ?", function () {

                
                if ($("input[name='chk_news']:checked").length > 0) {
                    

                    var qstatus = GetValueQueryString("temp");

                    $("<p class=\"progress_block_main\"><img class=\"img_progress\" src=\"../../images_extra/preloader_blue.gif\" alt=\"Progress\" /></p>").insertBefore("#newsletter_result").ajaxStart(function () {
                        $(this).show();
                    }).ajaxStop(function () {
                        $(this).remove();
                    });

                    var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
                    var StringUrl = "../ajax/ajax_newsletter_empty.aspx?temp=" + qstatus + GetQuerystringProductAndSupplierForBluehouseManage("append");

                    $.post(StringUrl, post,function (data) {

                        if (data == "True") {

                            GetNewsletterLsit($("#hd_newscat").val(), qstatus);
                        } else {

                        }
                        console.log(data);

                    });

                }
                else {
                    DarkmanPopUpAlert(400,"Please least one to Empty!")
                }
               // console.log($("input[name^='chk_news_']:checked").length);

            });

        }

        function GetNewsletterLsit(cat,status) {

            $("<p class=\"progress_block_main\"><img class=\"img_progress\" src=\"../../images_extra/preloader_blue.gif\" alt=\"Progress\" /></p>").insertBefore("#newsletter_result").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            
             var StringUrl  = "../ajax/ajax_newsletter_list.aspx?mc=" + cat + "&temp=" + status + GetQuerystringProductAndSupplierForBluehouseManage("append");
       
            $.get(StringUrl, function (data) {
               
                $("#newsletter_result").fadeOut("fast", function () {
                    $("#newsletter_result").html(data);
                    $("#newsletter_result").fadeIn("fast", function () {
                        $("#tbl_news tbody tr").not(":first").hover(
                           function () {
                               //$(this).css("background", "#f6f3e0");
                               $(this).addClass("simplehighlight");
                           },
                           function () {
                               // $(this).css("background", "");
                               $(this).removeClass("simplehighlight");
                           });
                    });

                    $("#mainCheck").click(function () {

                        if ($("#mainCheck").is(":checked")) {
                            $("input[name='chk_news']").attr("checked", "checked");
                            $("input[name='chk_news']").parent().parent().addClass("simplehighlight_checked");
                        } else {
                            $("input[name='chk_news']").removeAttr("checked");
                            $("input[name='chk_news']").parent().parent().removeClass("simplehighlight_checked");
                        }
                    });

                    $("input[name='chk_news']").click(function () {
                        if ($(this).is(":checked")) {
                            $(this).parent().parent().addClass("simplehighlight_checked");
                        } else {
                            $(this).parent().parent().removeClass("simplehighlight_checked");
                        }
                    });

                });
                
                

            });
        }

        function delNews(id, cat) {

            var qstatus = GetValueQueryString("temp");

            $("<p class=\"progress_block_main\"><img class=\"img_progress\" src=\"../../images_extra/preloader_blue.gif\" alt=\"Progress\" /></p>").insertBefore("#newsletter_result").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });


            var StringUrl = "../ajax/ajax_newsletter_del.aspx?ID=" + id + "&mc=" + cat + "&temp=" + qstatus + GetQuerystringProductAndSupplierForBluehouseManage("append");



            $.get(StringUrl, function (data) {

                if (data == "True") {
                    GetNewsletterLsit(cat, qstatus);
                } else {

                }
                console.log(data);



            });

        }
   

        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<ul id="extranet_tab" style="left:880px"><li class="active" id="5">All Customer</li><li id="7">Member</li></ul>--%>
   <%-- <div class="contenthead">
 <h1><asp:Label ID="title" runat="server"></asp:Label></h1>
 </div>--%>
    <input type="hidden" id="hd_newscat" />
    <div id="newsletter_result">

    </div>
    
    
</asp:Content>


