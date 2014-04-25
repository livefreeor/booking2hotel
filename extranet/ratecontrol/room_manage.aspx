<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="room_manage.aspx.cs" Inherits="Hotels2thailand.UI.extranet_room_manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>

<script type="text/javascript" language="javascript" src="../../Scripts/popup.js"></script>
<script type="text/javascript"  language="javascript">
    $(document).ready(function () {

        getRoomList(1);


        $("#extranet_tab li").click(function () {
            $("#extranet_tab li").removeClass("active");
            $(this).addClass("active");
           
            //GetPromotionList($(this).attr("id"));
            getRoomList($(this).attr("id"));

            return false;
        });

        $("#dropOptionCAt").change(function () {

            if ($(this).val() == 38) {
                $("#amen_main").slideDown('fast');
                $("#unit").html("*sq.m");
                $("#option_size").html("Size");
                $("#txt_size_pan").show();
            } else {
                $("#unit").html("");
                $("#option_size").html("");
                $("#txt_size_pan").hide();
                $("#amen_main").slideUp('fast');
                if ($(this).val() == 58) {
                    $("#guest_pan").show();
                   
                } else {
                    $("#guest_pan").hide();
                }
            }
        });
    });

    
    function LoadAmenToAdd_manaual() {

        $("<img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertAfter("#amen_main").ajaxStart(function () {
                    $(this).show();
                }).ajaxStop(function () {
                    $(this).remove();
                });

                $.get("../ajax/ajax_room_amen_add_manual.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), function (data) {
            DarkmanPopUp(600, data);
            // $("#amen_list").html(data);

        });
    }
    function LoadAmenToAdd_current_room() {

        $("<img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertAfter("#amen_main").ajaxStart(function () {
                    $(this).show();
                }).ajaxStop(function () {
                    $(this).remove();
                });

                $.get("../ajax/ajax_room_amen_room_current.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), function (data) {
            DarkmanPopUp(600, data);
            // $("#amen_list").html(data);

        });
    }
    function LoadAmenToAdd() {

        $("<img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertAfter("#amen_main").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        $.get("../ajax/ajax_room_amen_template_list.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), function (data) {
            DarkmanPopUp(600, data);
           // $("#amen_list").html(data);

        });
    }

    function GetFacSelectAddList_fromtemp() {
        //var CheckVal = $("input[name^='chek_fac_temp_']").filter(function (index) { return $(this).is(":checked") });


        
        $("input[name^='chek_fac_temp_']").filter(function (index) {

            if ($(this).is(":checked")) {
                var facVal = $(this).val();
                $("#amen_list").append("<p id=\"amen_" + index + "\"><input type=\"checkbox\" style=\"display:none;\" checked=\"checked\" value=\"" + index + "\" name=\"amenresult\" /><input type=\"text\" name=\"txt_amen_" + index + "\" style=\"display:none;\" class=\"Extra_textbox\" value=\"" + facVal + "\" />" + facVal + "&nbsp;<img src=\"/images_extra/del.png\" onclick=\"del('" + index + "');return false;\" />&nbsp;</p>");
            }
        });

        DarkmanPopUp_Close();
        return false;
    }

    function GetFacSelectAddList_frommanual() {
        
        var key = makeid();
        var result = $("#txt_amen_manual").val();

        $("#amen_list").append("<p id=\"amen_" + key + "\"><input type=\"checkbox\" style=\"display:none;\" checked=\"checked\" value=\"" + key + "\" name=\"amenresult\" /><input type=\"text\" name=\"txt_amen_" + key + "\" style=\"display:none;\" class=\"Extra_textbox\" value=\"" + result + "\" />" + result + "&nbsp;<img src=\"/images_extra/del.png\" onclick=\"del('" + key + "');return false;\" />&nbsp;</p>");

        //$("#txt_amen_manual").val("");

        DarkmanPopUp_Close();
        return false;
    }


    function GetFacSelectAddList_fromcurrent() {

        
        var OptionId = $("#select_option_current").val();

        $("<img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertBefore("#amen_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        $.get("../ajax/ajax_room_amen_room.aspx?oid=" + OptionId + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
            //alert(data);
            $("#amen_list").append(data);
            DarkmanPopUp_Close();
            // $("#amen_list").html(data);

        });
    }

    
    function del(key) {

        $("#amen_" + key).remove();
        
    }

    function rollback(num) {
        //$("#extranet_tab li").get(0).
        if (num == 1) {
            $("#extranet_tab li").filter(function (index) { return index == 1}).removeClass("active");
            $("#extranet_tab li").filter(function (index) { return index == 0 }).addClass("active");
        }

        if (num == 0) {
            $("#extranet_tab li").filter(function (index) { return index == 0}).removeClass("active");
            $("#extranet_tab li").filter(function (index) { return index == 1}).addClass("active");
        }
      
    }

    function getRoomList(status) {
        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#Room_List").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });
        $("#Room_List").fadeOut("fast");
        $.get("../ajax/ajax_room_manage_list.aspx?status=" + status + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
            
            $("#Room_List").html(data);
            $("#Room_List").fadeIn("fast");
        });
    }

    function savePriority() {
        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#Room_List").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });
        var Param = "";
        var Pri = $("input[name='option_list_priority']").each(function () {

            Param = Param + $(this).attr("id") + ";" + $(this).val() + ",";

        });

        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_room_manage_save_pri.aspx?param= " + Param + GetQuerystringProductAndSupplierForBluehouseManage("append"), post, function (data) {

            if (data == "True") {
                DarkmanPopUpAlert(450, "Priorities have changed completely.. Thank you.");
                getRoomList(1);
            }

            if (data == "method_invalid") {

                DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
            }
        });
    }

    function InsertOption() {
        if (ValidateOptionMethod("option_title", "required") == true) {
          

//            $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertAfter("#galaAdd").ajaxStart(function () {
//                $(this).show();
//            }).ajaxStop(function () {
//                $(this).remove();
//            });


            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_room_manage_save.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {

                if (data == "True") {
                    DarkmanPopUpAlert(450, "Product is added completely. Thank you.");
                    getRoomList(1);

                    $("#amen_list").val("");   
                    $("#room_detail").val("");
                    $("#option_title").val("");
                    $("#txt_size").val("");
                }

                if (data == "method_invalid") {

                    DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
                }
            });
        }
    }


</script>
<style type="text/css">
    #amen_main
     {
         margin:20px 0px 0px 0px;
         padding:10px; background-color:#f2ebbd;
          border:1px solid #e2d6a6;
        
     }
 #amen_main a
 {
      font-size:11px;
      font-weight:normal;
      
 }
  #amen_list
  {
      width:800px;
      margin: 15px 0px 0px 0px;
      padding:5px;
      clear:both;
  }
 #amen_list > p
 {
     float:left;
     margin:5px 0px 0px 0px;
     padding:0px;
     width:200px;
     font-size:11px;
     color:#84785a;
 }
 #amen_list_head
 {
     padding:0px 0px 5px 0px;
     border-bottom:1px solid #e2d6a6;
 }
    #active_btn {
        position:relative;
         margin:0px;
         padding:0px;
    }
 #extranet_tab
{
    
    
    position:absolute;
    top:-20px;
    left:700px;
	margin-left:90px;
	margin-bottom:-1px;
	padding: 0;
	float: left;
	list-style: none;
	height: 32px;
	width: 200px;
}
#extranet_tab li
{
	float: left;
	margin-right:1px;
	padding: 0 10px;
	height: 31px;
	line-height:31px;
	border-left: none;
	margin-bottom: -1px;
	background:#f7f7f7;
	overflow: hidden;
	position: relative;
	font-size:12px;
	cursor:pointer;
}

#extranet_tab li.active {
	display: block;
	background:#328aa4;
	color:#ffffff;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="holiday_insert_box" class="blogInsert">
    <h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" /> Product Insert Box</h4>
        <table cellpadding="0" cellspacing="5" >
            <tr>
             <td><label> Product Type </label></td>
            <td>
            <select id="dropOptionCAt" name="dropOptionCAt" class="Extra_Drop">
             <option value="38">Room</option>
             <option value="39">Extra bed</option>
             <option value="58">Meal</option>
             <option value="44">Transfer</option>
            </select>
            <input type="text" class="Extra_textbox" style="width:410px;" id="option_title" name="option_title" /></td>
            <td style="width:35px"></td>
            <td>
             <table  cellpadding="0" cellspacing="0" >
              <tr><td><label id="option_size">Size</label></td><td style="width:5px"></td>
               <td>
                   <div id="txt_size_pan"> <input type="text" id="txt_size" class="Extra_textbox" style="width:30px;" name="txt_size" /></div>
                   <div id="guest_pan" style="display:none;">

                       <table>
                        <tr><td><input type="radio" name="chk_adult_child" value="0" checked="checked" /></td><td>For Adult</td></tr>
                        <tr><td><input type="radio" name="chk_adult_child" value="1" /></td><td>For Child</td></tr>
                        </table>
                   </div>
                    

               </td>
                  
                  <td></td><td style="width:5px"></td><td><label id="unit">*sq.m</label></td><td style="width:10px"></td>
              <td><label>Priority</label></td><td style="width:5px"></td><td><input type="text" id="txt_priority" class="Extra_textbox" style="width:30px;" name="txt_priority" value="1" /></td><td></td><td style="width:5px"></td>
              
              </tr>
             </table>
            </td>
            
            
            
           <%-- <td><input type="button" value="Save" onclick="insertHoliday();return false;" class="Extra_Button_small_blue"  /></td>--%>
            </tr>
            <tr><td colspan="4" style=" height:10px;"></td></tr>
            <tr>
            <td><label>Product Description</label></td><td><textarea id="room_detail" cols="70" name="room_detail" rows="3" class="Extra_textbox"></textarea></td>
            <td style="width:35px"></td>
            <td></td>
            
            </tr>
            <tr>
                <td colspan="4">
                <div id="amen_main">
                <p id="amen_list_head"><label>Amenities Box</label></p>
                    <a href="" onclick="LoadAmenToAdd_manaual();return false;" >Custom</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="" onclick="LoadAmenToAdd();return false;" >by Template</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="" onclick="LoadAmenToAdd_current_room();return false;" >by current room</a>
                    <div id="amen_list"> 

                         
                    </div>
                   <div style="clear:both"></div>
                </div>
                     <div style="clear:both"></div>
                </td>
                
            </tr>
            <tr><td style="height:10px;"></td></tr>
            <tr>
                <td><input type="button" value="Save" onclick="InsertOption(); return false;" class="Extra_Button_small_blue"  /></td>
            </tr>
        </table>
    </div>
   <div id="active_btn">

       <ul id="extranet_tab"><li class="active" id="1">Active</li><li id="0">Inactive</li></ul>
   </div> 

    <div id="Room_List" style="margin:15px 0px 0px 0px;" >
        
    </div>
 
</asp:Content>

