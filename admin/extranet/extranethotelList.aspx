<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="extranethotelList.aspx.cs" Inherits="Hotels2thailand.UI.admin_extranet_extranethotelList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link  href="../../css/extranet_list_style.css" rel="Stylesheet" type="text/css" />
<script language="javascript" type="text/javascript" src="../../scripts/jquery-1.6.1.js" ></script>
<link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
   <script type="text/javascript" src="../../Scripts/jquery-1.4.2.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="../../Scripts/datepicker.js"></script>
    <script type="text/javascript" src="../../scripts/darkman_datepicker.js"></script>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        GetproductExtranetLsit("normal");
        $("#sp_click").click(function () {
            GetproductExtranetLsit("normal");
            $("#dropDesExtranet").show();
            return false;
        });

        $("#ch_click").click(function () {
            GetproductExtranetLsit("chain");
            $("#dropDesExtranet").hide();
            return false;
        });

        $("#dropDesExtranet").change(function () {
            GetproductExtranetLsit("normal");
        });

        //onmouseover=\"changein(this);\" onmouseout=\"changeout(this);\"


    });

    function GetproductExtranetLsit(type) {

        $("<p class=\"progress_block\"><img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" /></p>").insertBefore("#main_extra_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        if (type == "normal") {
            $.post("../ajax/ajax_product_extranet_list.aspx", post, function (data) {

                $("#product_extra_list").html(data);

            });
        }


        if (type == "chain") {

            $.post("../ajax/ajax_product_extranet_list_chain.aspx", post, function (data) {
                
                $("#product_extra_list").html(data);
            });
        }
    }

    function SaveACtive() {

        $("<p class=\"progress_block_manage\"><img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" /></p>").appendTo(".active_block").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_product_extranet_manage_saveActive.aspx", post, function (data) {
            if (data != "True") {
                alert(data);
            }
        });
    }

    function UpdateCom(comId) {
        $("<p class=\"progress_block_manage\"><img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" /></p>").appendTo(".active_block").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_product_extranet_manage_savecom.aspx?comid=" + comId, post, function (data) {
            
            if (data != "True") {
                alert(data);
            }
        });
    }

    function AddNewCom() {
        $("<p class=\"progress_block_manage\"><img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" /></p>").appendTo(".commission_insert_block").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_product_extranet_manage_saveNew.aspx", post, function (data) {
            alert(data);
            if (data != "True") {
                alert(data);
            }
        });
    }

    function Getproductmanage() {

        $("<p class=\"progress_block_manage\"><img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" /></p>").appendTo("#product_extra_manage_block").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_product_extranet_manage.aspx", post, function (data) {

            $("#product_extra_manage_block").html(data);

            $("#commission_block table tr").filter(function (index) {


                    var dDateStart = $(this).find("input[id^='date_start_']").attr("id");
                    var dDateEnd = $(this).find("input[id^='date_end_']").attr("id");

                    DatepickerDual(dDateStart, dDateEnd);
                    DatepickerDual("date_start_", "date_end_");
                    
            });
        });
    }
    
    

    function Manage(productId, supplierId) {


        if ($("#product_extra_manage").css("display") == "block") {

            if ($("#product_active").val() == productId) {
                $("#product_extra_list").css("width", "1020px");
                $("#product_extra_manage").css("width", "0px");
                $("#product_extra_manage").hide();

                $("#product_active").val("");
                $("#supplier_active").val("");
            } else {
                $("#product_active").val(productId);
                $("#supplier_active").val(supplierId);
                Getproductmanage();
            }
            

        } else {
            $("#product_extra_list").css("width", "550px");
            $("#product_extra_manage").css("width", "450px");
            $("#product_extra_manage").show();

            $("#product_active").val(productId);
            $("#supplier_active").val(supplierId);

            Getproductmanage();
        }

        $("[id^='product_row_']").css("background-color", "#ffffff");
//        $("#product_row_" + productId).removeAttr("onmouseover");
//        $("#product_row_" + productId).removeAttr("onmouseout");
        $("#product_row_" + productId).css("background-color", "#dde4ea");
        return false;
    }

    function CloseBlock() {
        $("#product_extra_list").css("width", "1020px");
        $("#product_extra_manage").css("width", "0px");
        $("#product_extra_manage").hide();
    }
</script>
<style   type="text/css">

.block_extra_view
{
     margin:10px 0px 10px 0px;
}
.block_extra_view a
{
     float:left;
      padding:5px 10px 5px 10px;
       color:#ffffff;
       margin:0px 0px 0px 10px;
}
.drop_des
{
     margin:10px 0px 0px 0px;
}
.sp
{
     background-color:#3f5d9d;
}
.ch
{
     background-color:#72ac58;
    
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style=" margin:0px 0px 15px 0px; padding:0 0 15px 0; border-bottom:1px solid #e0e0e0;">
<a href="addnewextranet.aspx">Add New Extranet Partner stand alone</a>&nbsp&nbsp;|&nbsp;&nbsp;
<a href="addnewextranet_chain.aspx" style=" color:#72ac58;">Add New Extranet Partner by Chain <label style="color:#dd3822;">new!!</label></a>&nbsp&nbsp;|&nbsp;&nbsp;
<a href="addnewextranet_chain_extend.aspx" style=" color:#72ac58;">Add New Extranet Partner by Extend Chain <label style="color:#dd3822;">new!!</label></a>&nbsp&nbsp;|&nbsp;&nbsp;
<a href="extranet_public_holidays.aspx">Manage Public Holidays</a>

</div>
<div  class="block_extra_view">
<a href="" id="sp_click" class="sp" style=" margin-left:0px;" >Supplier View</a>
<a href="" id="ch_click"  class="ch">Chain(Group) View</a>
</div>
<div style="clear:both;"> </div>
<div class="drop_des">
<asp:DropDownList ID="dropDesExtranet" runat="server" ClientIDMode="Static" SkinID="DropCustomstyle"></asp:DropDownList>
</div>
<div style="clear:both;"> </div>
<div id="main_extra_list">
<div id="product_extra_list"></div>
<div id="product_extra_manage">
<div id="product_extra_manage_head_block">
<img  src="/images/close.png" onclick="CloseBlock();return false;"  alt="" />
<p>Manage Block</p>

</div>
<input type="hidden"  id="product_active"  name="product_active" />
<input type="hidden"  id="supplier_active"  name="supplier_active" />
<div id="product_extra_manage_block">



</div>


</div>
<div style=" clear:both"></div>
</div>
<div style=" clear:both"></div>

</asp:Content>

