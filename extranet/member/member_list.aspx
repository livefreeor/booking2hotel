<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="member_list.aspx.cs" Inherits="Hotels2thailand.UI.extranet_member_member_list" %>

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
     
        isActive = true;
        isBlock = true;
        
        var cookieData = readCookie("log");
        var history = ""
        if (cookieData != null) {
            if (location.hash != "") {
                history = location.hash.split('#')[1].split('?')[1];
            } else {
                history = cookieData;
            }
           
        }

        if (history != "") {
           
            window.location.hash = "?" + history;
            switch (history) {
                case "active":
                    isActive = true;
                    isBlock = true;
                    GetCustomerList(isActive, isBlock);
                    $("#extranet_tab li").removeClass("active");
                    $("#extranet_tab li").filter(function (index) { return $(this).attr("id") == "1" }).addClass("active");
                    break;
                case "inactive":
                    isActive = false;
                    isBlock = true;
                    GetCustomerList(isActive, isBlock);
                    $("#extranet_tab li").removeClass("active");
                    $("#extranet_tab li").filter(function (index) { return $(this).attr("id") == "0" }).addClass("active");
                    break;
                case "blocked":
                    isActive = false;
                    isBlock = false;
                    GetCustomerList(isActive, isBlock);
                    $("#extranet_tab li").removeClass("active");
                    $("#extranet_tab li").filter(function (index) { return $(this).attr("id") == "00" }).addClass("active");
                    break;
                default:
                    isActive = false;
                    isBlock = true;
                    searchKey = history.split('=')[1];
                    GetMemberQuickSearch(searchKey);
                   
                    $("#txt_search").val(searchKey);

                   
                    break;
            }
        } else {
            
            //$("#historyPage").val("active");
            GetCustomerList(true, true);
        }


       

        $("#extranet_tab li").click(function () {
            
            $("#extranet_tab li").removeClass("active");
            $(this).addClass("active");


            var id = $(this).attr("id");
            var isactive = true;
            var status = true;
            switch (id) {
                case "1":
                    $("#active_title").html("Active");
                    $("#historyPage").val("active");
                    isactive = true;
                    status = true;
                    CookieLog("active");
                    break;
                case "0":
                    $("#active_title").html("Inactive");
                    $("#historyPage").val("inactive");
                    isactive = false;
                    status = true;
                    CookieLog("inactive");
                    break;
                case "00":
                    $("#active_title").html("Blocked");
                    $("#historyPage").val("blocked");
                    isactive = false;
                    status = false;
                    CookieLog("blocked");
                    break;
            }

            GetCustomerList(isactive, status);

        });
        if (!$("#txt_search").val().length) {
            $("#txt_search").val("Quick Search").css({ "color": "#ccccc1" });
        }
        $("#txt_search").focus(function () {
            $(this).css({ "color": "#000000" });
            if ($(this).val() == "Quick Search") {
                $(this).val("");
            }
            
        });

        $("#txt_search").blur(function () {
            if ($(this).val() == "") {
                $(this).val("Quick Search").css({ "color": "#ccccc1" });
                
            }
        });
        

            $("#txt_search").keypress(function (e) {
                var evt = e ? e : window.event;
                var keypress = evt.which || evt.keyCode;
                if ($("#btn_search").length) {
                    if (keypress == 13) {
                        var key = $("#txt_search").val();
                        if (key != "") {
                            CookieLog("search=" + key);
                            GetMemberQuickSearch(key);
                        } else { RollbackDefault(); }
                        // false;
                    }
                }
            });

            $("#txt_search").keyup(function () {
                var key = $("#txt_search").val();
                if (key != "") {
                   
                    GetMemberQuickSearch(key);
                    CookieLog("search=" + key);
                } else { RollbackDefault(); }


                return false;
            });

            $("#btn_search").click(function () {
                var key = $("#txt_search").val();
                if (key != "") {
                  
                    GetMemberQuickSearch(key);
                    CookieLog("search=" + key);
                } else { RollbackDefault(); }
            });

           
    });


    function CookieLog(val) {
        //var hdVal = $("#hd_history_type").val();
        //alert(hdVal);
        
        createCookie("log", val, 7);


        var cookieData = readCookie("log");
        ////alert(cookieData);

        if (cookieData != null) {
            window.location.hash = "?" + cookieData;

        } 
       
    }
    function RollbackDefault() {
        $("#extranet_tab li").each(function () {
            if ($(this).attr("class") == "active") {
                $("#extranet_tab li").removeClass("active");
                $(this).addClass("active");
                var id = $(this).attr("id");
                var isactive = true;
                var status = true;
                switch (id) {
                    case "1":
                        $("#active_title").html("Active");
                        isactive = true;
                        status = true;
                        break;
                    case "0":
                        $("#active_title").html("Inactive");
                        isactive = false;
                        status = true;
                        break;
                    case "00":
                        $("#active_title").html("Blocked");
                        isactive = false;
                        status = false;
                        break;
                }

                GetCustomerList(isactive, status);
            }

        });
    }

    function GetMemberQuickSearch(keyword) {
        var StringUrl = "../ajax/ajax_member_list.aspx?qs=" + keyword + GetQuerystringProductAndSupplierForBluehouseManage("append");
        //var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
        
        $.get(StringUrl, function (data) {
            
            $("#cus_list").html(data);


            var Count;
            if ($("#hd_memberCount").length) {
                Count = $("#hd_memberCount").val();
            } else {
                Count = "0";
            }
            $("#member_total").html("&nbsp;Total:&nbsp;" + Count);
            CheckAll();

            MemberStatusManage();

           //CookieLog();
        });

        $("#active_title").html("Search Result");
    }

    function GetCustomerList(Isactive,Status) {

        $("<p class=\"progress_block_main\"><img class=\"img_progress\" src=\"../../images_extra/preloader_blue.gif\" alt=\"Progress\" /></p>").insertBefore("#cus_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        var StringUrl = "";

        var qcuurentPage = GetValueQueryString("pg");
        if (qcuurentPage == "") {
            qcuurentPage = 1;
        }
            
        StringUrl = "../ajax/ajax_member_list.aspx?pg=" + qcuurentPage + "&act=" + Isactive + "&sta=" + Status + GetQuerystringProductAndSupplierForBluehouseManage("append");
        
        $.get(StringUrl, function (data) {
            $("#cus_list").html(data);
          

            var Count;
            if ($("#hd_memberCount").length) {
                Count = $("#hd_memberCount").val();
            } else {
                Count = "0";
            }
            $("#member_total").html("&nbsp;Total:&nbsp;" + Count);
            CheckAll();

            MemberStatusManage();
            
            //disable this action ----- 
           // MemberActivateManage();
           // 
        });
    }

    function MemberActivateManage() {

        $("#active_list").click(function () {
            
            var checked = $("input[name='cus_checked']:checked");
           
            if (checked.length > 0) {
                DarkmanPopUpComfirmCallback(400, "Are you sure to Activate this member", function () {
                    
                });
                
            } else {
                DarkmanPopUpAlert(400, "Please check one before");
            }

            return false;
        });
    }


    function MemberStatusManage() {

        $("#block_list").click(function () {
            

            var checked = $("input[name='cus_checked']:checked");
            
            
            if (checked.length > 0) {
                DarkmanPopUpComfirmCallback(400, "Are you sure to Block this member?", function () {
                    var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
                    var StringUrl = "../ajax/ajax_member_list_update_status.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new");

                    $.post(StringUrl, post, function (data) {

                        if (data != "method_invalid") {

                            RollbackDefault();

                        } else {

                        }
                    });
                });
               
            } else {
                DarkmanPopUpAlert(400,"Please check one before");
            }

            return false;
        });
    }

    function CheckAll() {
        var item = $("input[name='cus_checked']");
        $("#check_main").click(function () {
            
            if ($(this).is(":checked")) {
                item.attr("checked","checked");
            }
            else {
                item.removeAttr("checked");
            }
            
        });
    }

</script>
    <style type="text/css">

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <input type="hidden" value="" id="historyPage" />
    <div id="member_search">

        <input type="text"  name="txt_search" id="txt_search" style="width:300px" class="Extra_textbox" />
        <input type="button" name="btn_search" id="btn_search"  class="Extra_Button_small_green" value="Search" />

    </div>

    <ul id="extranet_tab"><li class="active" id="1">Active</li><li id="0">Inactive</li><li id="00">Blocked</li></ul>
    <div id="main_member">
        <div id="head_status">
            <label class="active_title" id="active_title">Active</label>
            <label class="status_total" id="member_total">Total:<span>12</span></label>
        </div>
        <div id="cus_list">

        </div>
       
    </div>

  
</asp:Content>

