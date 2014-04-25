<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popup_edit_option_detail.aspx.cs" Inherits="Hotels2thailand.UI.extranet_ordercenter_popup_edit_option_detail" %>
<%@ Register Src="~/Control/DatepickerCalendar-single.ascx" TagName="datePicker" TagPrefix="Product" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
<link href="../../css/extranet/extranet_style_core.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" language="JavaScript" src="../../scripts/popup.js"></script>
    <script type="text/javascript" language="javascript">
        function ParentFucntion() {
            
            window.opener.getRoomList(1);
            window.opener.rollback(1);
            window.close();
        }

       

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
            if ($(".box_empty").length) {

                $(".box_empty").remove();
            }
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
            if ($(".box_empty").length) {

                $(".box_empty").remove();
            }
            var key = makeid();
            var result = $("#txt_amen_manual").val();

            $("#amen_list").append("<p id=\"amen_" + key + "\"><input type=\"checkbox\" style=\"display:none;\" checked=\"checked\" value=\"" + key + "\" name=\"amenresult\" /><input type=\"text\" name=\"txt_amen_" + key + "\" style=\"display:none;\" class=\"Extra_textbox\" value=\"" + result + "\" />" + result + "&nbsp;<img src=\"/images_extra/del.png\" onclick=\"del('" + key + "');return false;\" />&nbsp;</p>");

            //$("#txt_amen_manual").val("");

            DarkmanPopUp_Close();
            return false;
        }


        function GetFacSelectAddList_fromcurrent() {
            if ($(".box_empty").length) {

                $(".box_empty").remove();
            }
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
  
 #amen_list > p
 {
     float:left;
     margin:10px 0px 0px 0px;
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
</style>
</head>
<body style=" background:#faf7dd;" >
    <form id="form1" runat="server">
    <div style=" width:600px; font-family:Tahoma; margin:0 auto; border:1px solid #e2d6a6; background-color:#ffffff ; font-size:12px; color:#6a785a; padding:20px;">
       <table width="100%">
        <tr>
         <td><asp:Literal ID="ltProductType" runat="server" Text="Amenities:"></asp:Literal></td>
         <td><asp:TextBox ID="txtTitle" EnableTheming="false" Width="450px"   CssClass="Extra_textbox" runat="server" ></asp:TextBox></td>
        </tr>
        <tr>
         <td><asp:Literal ID="ltProductDetail" runat="server" Text="Amenities:"></asp:Literal></td>
         <td><asp:TextBox ID="txtDetail" EnableTheming="false"  CssClass="Extra_textbox" TextMode="MultiLine" Rows="10" Width="450px" runat="server" ></asp:TextBox></td>
        </tr>
        <tr>
         <td><asp:Literal ID="ltSize" runat="server"  Text="Size:"></asp:Literal></td>
         <td><asp:TextBox ID="txtSize" EnableTheming="false" Width="50px"  CssClass="Extra_textbox" runat="server" ></asp:TextBox></td>
        </tr>
        <tr>
         <td>Prioriry:</td>
         <td><asp:TextBox ID="txtPri" EnableTheming="false"  Width="50px"  CssClass="Extra_textbox" runat="server" ></asp:TextBox></td>
        </tr>
           <tr>
         <td>Status:</td>
         <td> 
            
             <asp:RadioButtonList ID="radioStatus" runat="server"  RepeatDirection="Vertical"  >
                 <asp:ListItem Value="True">Active</asp:ListItem>
                  <asp:ListItem Value="False">Inactive</asp:ListItem>
             </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
         <td>
          <asp:Literal ID="ltAmen" runat="server" Text="Amenities:"></asp:Literal>
         </td>
         <td>
            <asp:Panel ID="panel_amen" runat="server" Visible="false">
          <div id="amen_main">
                
                    <a href="" onclick="LoadAmenToAdd_manaual();return false;" >Custom</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="" onclick="LoadAmenToAdd();return false;" >by Template</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="" onclick="LoadAmenToAdd_current_room();return false;" >by current room</a>
                    <p id="amen_list_head"><label>Amenities Box</label></p>
                    <div id="amen_list"> 
                    <asp:Literal ID="ltAmenlist" runat="server"></asp:Literal>
                         
                    </div>
                   <div style="clear:both"></div>
                </div>
                     <div style="clear:both"></div>
         </asp:Panel>
         </td>
        </tr>
           <tr><td style="height:10px;"></td></tr>
        <tr>
        
         <td colspan="2px">
          <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSaveOption_Onclick" EnableTheming="false" CssClass="Extra_Button_small_blue" />
          <input type="button" value="Close" class="Extra_Button_small_white" onclick="window.close();" />
         </td>
        </tr>
       </table>
    </div>
    </form>
</body>
</html>
