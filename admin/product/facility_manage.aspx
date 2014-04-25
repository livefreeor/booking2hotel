<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="facility_manage.aspx.cs" Inherits="Hotels2thailand.UI.admin_facility_manage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    
    <script type="text/javascript" language="javascript" src="../../scripts/jquery-1.6.1.js"></script>
    <script type="text/javascript" language="javascript" >
        $(document).ready(function () {

            GetFacList();
            $("#dropFacCat").change(function () {

                GetFacList();

            });
        });

        function GetFacList() {
//            var Val = $(this).val();
            $("<p class=\"progress_block\"><img class=\"img_progress\" src=\"/images/progress.gif\" alt=\"Progress\" /></p>").insertBefore("#Fac_temp_list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_product_facility_template_manage.aspx", post, function (data) {
              
                $("#Fac_temp_list").html(data);
            });
        }

        function FacUpdate() {
            $("<p class=\"progress_block\"><img class=\"img_progress\" src=\"/images/progress.gif\" alt=\"Progress\" /></p>").insertBefore("#Fac_temp_list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_product_facility_template_manage_update.aspx", post, function (data) {
               
                if (data == "true") {
                    GetFacList();
                }
            });
        }
        function Gen_Input() {
            var result = "";
            var InputVal = "<p><input type=\"text\" style=\"width:350px\" class=\"TextBox_Extra_normal\" /></p>";
            var RowNum = $("#selNum").val();
            result = "<table>"
            for (i = 1; i <= RowNum; i++) {
                result = result + "<tr>";
                result = result + "<td><input type=\"checkbox\" style=\"display:none;\" name=\"ChkInsert\" value=\""+i+"\" checked=\"checked\" /></td>";
                result = result + "<td>ENG: <input type=\"text\" style=\"width:300px\" name=\"fac_eng_"+i+"\" class=\"TextBox_Extra_normal\" />";
                result = result + "<td>&nbsp;&nbsp;&nbsp; THAI: <input type=\"text\" name=\"fac_thai_"+i+"\" style=\"width:300px\" class=\"TextBox_Extra_normal\" />";
                result = result + "</tr>";
            }
            result = result + "</table>"
            result = result  + " <input type=\"button\" value=\"Save\"  onclick=\"SaveFac();return false;\" />";
            $("#input_List_insert").html(result);
        }

        function SaveFac() {
            $("<p class=\"progress_block\"><img class=\"img_progress\" src=\"/images/progress.gif\" alt=\"Progress\" /></p>").insertBefore("#Fac_temp_list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_product_facility_template_manage_save.aspx", post, function (data) {

                if (data == "true") {
                    GetFacList();
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="insert_box" class="productPanel">
        <h4><img src="../../images/content.png"  alt="image_topic" /> Insert Box</h4>
        <p class="contentheadedetail">you can insert facility and amenity</p><br />
        Select number of row to quick insert &nbsp;&nbsp;<select id="selNum"  class="DropDownStyleCustom"  >
        <option value="1" >1</option>
        <option value="5" >5</option>
        <option value="10" >10</option>
        <option value="20" >20</option>
        </select>
        <input type="button" value="Submit" onclick="Gen_Input();return false;" />
        <div id="input_List_insert" ></div>

       
    </div>
    <div class="productPanel">
    <h4><img src="../../images/content.png"  alt="image_topic" /> Template List</h4>
        <p class="contentheadedetail">you can insert facility and amenity</p><br />
    <div>
     <asp:DropDownList ID="dropFacCat"  SkinID="DropCustomstyle" runat="server" ClientIDMode="Static">
     <asp:ListItem Value="1">Hotel Facility</asp:ListItem>
     <asp:ListItem Value="2">Room ameniity</asp:ListItem>
     </asp:DropDownList>
    </div>
           
     <div id="Fac_temp_list">
        
     </div>  
     </div>
</asp:Content>
