<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product_picture.aspx.cs" Inherits="Hotels2thailand.UI.admin_product_product_picture" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/controlpicture/control_picture_insert.ascx" TagName="Picture_insert" TagPrefix="Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
    <script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
    <link  href="../../css/option_section/option_picture_style.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            PictureListDataBind();
            $("#drpOptionList").change(function () {
                PictureListDataBind();
            });

            $("#drpConstrucTionList").change(function () {
                PictureListDataBind();
            });
            
        });

        function PictureListDataBind() {
            var qProductId = GetValueQueryString("pid");
            var qImagCat;
            if (GetValueQueryString("imgcat_id") == "") {
                qImagCat = 1;
            }
            else {
                qImagCat = GetValueQueryString("imgcat_id")
            }
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#pictureList").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_product_picture_list.aspx?pid=" + qProductId + "&imgcat_id=" + qImagCat, post, function (data) {
                $("#pictureList").html(data);
                $("#pictureList #GvPicTypeList table tr:odd").addClass("RowAlten");
                $("#pictureList #GvPicTypeList table tr:even").addClass("RowAlten2");

                
            });
        }
        function btnEdit_pri_save(){
            var qProductId = GetValueQueryString("pid");
            var qImagCat;
            if (GetValueQueryString("imgcat_id") == "") {
                qImagCat = 1;
            }
            else {
                qImagCat = GetValueQueryString("imgcat_id")
            }
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#pictureList").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_product_picture_list_priority.aspx?pid=" + qProductId + "&imgcat_id=" + qImagCat, post, function (data) {

                if (data == "True") {
                    PictureListDataBind();
                }
                
            });
        }
        function btnEdit_Delete_save() {
            DarkmanPopUpComfirm(400," Are you sure to delete??","btnEdit_Delete();")
        }
        function btnEdit_Delete() {
            var qProductId = GetValueQueryString("pid");
            var qImagCat;
            if (GetValueQueryString("imgcat_id") == "") {
                qImagCat = 1;
            }
            else {
                qImagCat = GetValueQueryString("imgcat_id")
            }
            

            var CheckboxChecked = $("#pictureList #GvPicTypeList table tr").find("td :checked").toArray();
            if (CheckboxChecked.length > 0) {
                $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#pictureList").ajaxStart(function () {
                    $(this).show();
                }).ajaxStop(function () {
                    $(this).remove();
                });
                $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertAfter("#pictureList").ajaxStart(function () {
                    $(this).show();
                }).ajaxStop(function () {
                    $(this).remove();
                });
                var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

                $.post("../ajax/ajax_product_picture_list_del.aspx?pid=" + qProductId, post, function (data) {
                  
                    if (data == "True") {
                        PictureListDataBind();
                    }

                });
            } else
            { DarkmanPopUpAlert(400, "Please Select Picture Less one"); }

            return false;
        }

        function SavePictitle(id) {
            
            var qProductId = GetValueQueryString("pid");
            var qImagCat;
            if (GetValueQueryString("imgcat_id") == "") {
                qImagCat = 1;
            }
            else {
                qImagCat = GetValueQueryString("imgcat_id")
            }
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertAfter("#insertPictureName_Lang_" + id).ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });
            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_product_picture_save_title.aspx?pid=" + qProductId + "&picId=" + id, post, function (data) {
                
                if (data == "True") {
                    //PictureListDataBind();
                }

            });
        }
        function showEditMode_cancel() {
            $("#btnEdit_Delete").hide();

            $("#btnEdit_Delete_cancel").hide();
            $("#btnEdit_Delete_foot").hide();
            $("#btnEdit_Delete_cancel_foot").hide();


            $(".checkEdit").hide();

            $("#btnEdit").show();
            $("#btnEdit_foot").show();

            $("#pictureList #GvPicTypeList table tr").each(function () {
                $(this).unbind("click");
                $(this).css("background-color", "");
                var CheckboxID = $(this).find("td :checkbox").stop().attr("id");
                var ChkImg = $(this).find("div").stop();
                if ($("#" + CheckboxID).is(':checked')) {
                   
                    $("#" + CheckboxID).removeAttr("checked");
                    $("#chkImg_" + CheckboxID).removeClass("checkboxCheckedStyle").addClass("checkboxCheckedDefaultStyle");
                } 
            });
            $("#pictureList #GvPicTypeList table tr:odd").addClass("RowAlten");
            $("#pictureList #GvPicTypeList table tr:even").addClass("RowAlten2");
        }



        function showEditMode_pti_cancel() {
            $("#btnEdit_save").hide();
            $("#btnEdit_save_cancel").hide();
            $("#btnEdit_save_foot").hide();
            $("#btnEdit_save_cancel_foot").hide();


            $(".pic_item_drop").hide();


            $("#btnEdit_pri").show();
            $("#btnEdit_pri_foot").show();
        }
        function showEditMode_pti() {

            $("#btnEdit_save").show();
            $("#btnEdit_save_cancel").show();
            $("#btnEdit_save_foot").show();
            $("#btnEdit_save_cancel_foot").show();
            

            $(".pic_item_drop").show();


            $("#btnEdit_pri").hide();
            $("#btnEdit_pri_foot").hide();
            //alert($("#btnEdit_Delete").css("display"));
            if ($("#btnEdit_Delete").css("display") == "inline-block") {
                
                showEditMode_cancel();
            }
        }
        function showEditMode() {
            
            $("#btnEdit_Delete").show();

            $("#btnEdit_Delete_cancel").show();
            $("#btnEdit_Delete_foot").show();
            $("#btnEdit_Delete_cancel_foot").show();
            
            
            $(".checkEdit").show();

            $("#btnEdit").hide();
            $("#btnEdit_foot").hide();

            $("#pictureList #GvPicTypeList table tr").click(function () {
                var CheckboxID = $(this).find("td :checkbox").stop().attr("id");
                var ChkImg = $(this).find("div").stop();

                if ($("#" + CheckboxID).is(':checked')) {
                    $(this).css("background-color", "");
                    $("#" + CheckboxID).removeAttr("checked");
                    $("#chkImg_" + CheckboxID).removeClass("checkboxCheckedStyle").addClass("checkboxCheckedDefaultStyle");
                } else {
                    $(this).css("background-color", "#f9dada");
                    $("#" + CheckboxID).attr("checked", "checked");
                    $("#chkImg_" + CheckboxID).removeClass("checkboxCheckedDefaultStyle").addClass("checkboxCheckedStyle");

                }

            });

            if ($("#btnEdit_save").css("display") == "inline-block") {
                showEditMode_pti_cancel();
            }
            
        }

        function HideEditMode() {
            $("#btnEdit_save").hide();
            $("#btnEdit").show();
        }

        function GenFormInsert() {
            
            var qProductId = GetValueQueryString("pid");
            var qRow = $("#numpicinsrt :selected").val();
            var qImgType = $("#drppicType :selected").val(); 
            var qImagCat ;
            if  (GetValueQueryString("imgcat_id") == ""){
                qImagCat = 1;
            }
            else{
                qImagCat = GetValueQueryString("imgcat_id")
            }
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertAfter("#picture_insert_form").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });
            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_product_picture_insertForm.aspx?pid=" + qProductId + "&imgcat_id=" + qImagCat + "&row=" + qRow + "&imgtype_id=" + qImgType, post, function (data) {
                if ($("#picture_insert_form").css("display") == "none") {
                    $("#picture_insert_form").slideDown();
                }

                $("#picture_insert_form").html(data);
            });
        }

        function SavePicForm() {
            var qProductId = GetValueQueryString("pid");
            var qImagCat;
            if (GetValueQueryString("imgcat_id") == "") {
                qImagCat = 1;
            }
            else {
                qImagCat = GetValueQueryString("imgcat_id")
            }

            var MaxRow = $("#picture_insert_form .pic_insert_box").toArray();
            
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertAfter("#picture_insert_form").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });
            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_product_picture_save.aspx?pid=" + qProductId + "&imgcat_id=" + qImagCat + "&maxrow=" + MaxRow.length, post, function (data) {
                $("#picture_insert_form").slideUp("600");
                $("#picture_insert_form").html("");
                if (data == "True") {
                    PictureListDataBind();
                }
            });
        }

        function dropPictypeChang(id) {
            var tableForm = $("#pic_form_" + id);
            var typeValue = $("#dropPictype_" + id).children("option:selected").html();
            var NumValue = $("#dropNumPic_" + id).children(":selected").val(); 
            var InputFilename = $("#txtfilename_" + id);
            var currentType = InputFilename.attr("alt");
            
            var fileName = InputFilename.attr("value");
            var newfileName = fileName.replace(currentType, typeValue);

            InputFilename.attr("value", newfileName);
            InputFilename.attr("alt", typeValue);
        }

        function dropNumPicChang(id) {
            
            var tableForm = $("#pic_form_" + id);
            var typeValue = $("#dropPictype_" + id).children(":selected").val();
            var NumValue = $("#dropNumPic_" + id).children(":selected").val(); 
            var InputFilename = $("#txtfilename_" + id);
            //var currentType = InputFilename.attr("alt");

            //var FilenameNum = InputFilename.attr("value").split('_')[InputFilename.attr("value").split('_').length - 1].split('.')[0];
            var filenameNew = "";
            for (i = 0; i < InputFilename.attr("value").split('_').length; i++) {
                if (i != InputFilename.attr("value").split('_').length - 1) {
                    filenameNew = filenameNew  + InputFilename.attr("value").split('_')[i] + "_" 
                }
            }

            filenameNew = filenameNew +  NumValue + ".jpg";
            InputFilename.attr("value", filenameNew);
            
        }
    </script>
    <style type="text/css">
 .pic_insert_box
 {
      width:950px;
      margin:10px 0px 0px 0px;
      padding:0px;
      
 }
 .pic_insert_box td
 {
      margin:10px 0px 0px 0px;
      padding:5px;
      border:1px solid #eeeee1;
      font-size:11px;
      color:#1c2a47;
      font-weight:bold;
 }
 .pic_insert_box p 
 {
      margin:0px; padding:0px;
 }
 .Alternatecolors
 {
     /*background:#f8f8f8;*/
 }
  .Alternatecolors_2
 {
    /*background:#dedede;*/
 }
 .p_insert_box
 {
     font-size:12px;
 }
 .span_insert_box
 {
      color:#3b59aa;
 }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="picture_style">
        
        <div class="picture_cat_menu">
            <asp:DataList ID="dLPictureCat" runat="server"  DataKeyField="Key" 
                HorizontalAlign="Center"  RepeatColumns="10" RepeatDirection="Horizontal" 
                DataSourceID="Objpicture_cat" >
                <ItemTemplate>
                    <asp:HyperLink ID="LbtProduct_cat_menu" Text='<%# Eval("Value") %>' NavigateUrl='<%# String.Format("~/admin/product/product_picture.aspx?imgcat_id={0}", Eval("Key")) + this.AppendCurrentQueryString() %>'  runat="server"></asp:HyperLink> |
                </ItemTemplate>
            </asp:DataList>
            <asp:ObjectDataSource ID="Objpicture_cat" runat="server" 
                SelectMethod="getPictureCategoryAll" 
                TypeName="Hotels2thailand.Production.ProductPicCategory">
            </asp:ObjectDataSource>
        </div>
        <div id="productheadTitle">
        <h6><asp:Image ID="imghead" runat="server" ImageUrl="~/images/imgheadtitle.png" />&nbsp;<asp:Label ID="Destitle" runat="server" ></asp:Label> : <asp:Label ID="txthead" runat="server"></asp:Label>
        
        </h6>
            <div style=" position:absolute; top:170px; left:800px">
            <p style="margin:0px 0px 0px 0px; padding:0px 0px 0px 0px; font-size:12px; color:#3f5d9d; font-weight:bold;" >Active To New Picture</p>
            <asp:RadioButtonList ID="raioNewPic" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="raioNewPic_OnChange">
                <asp:ListItem Value="True" Text="Enable"></asp:ListItem>
                <asp:ListItem Value="False" Text="Disable"></asp:ListItem>
            </asp:RadioButtonList>
            
            </div>
        </div>
        <div class="picture_add_edit">
                <asp:Panel ID="paneloptionList" runat="server"  CssClass="productPanel">
                <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Option List</h4>
                <p class="contentheadedetail">List Supplier of This Product, you can Change or Add Supplier to List </p><br />
                 <asp:DropDownList ID="drpOptionList" runat="server"  Width="400px"  ClientIDMode="Static"></asp:DropDownList>
                 <asp:Panel ID="paneloptionEmpty" runat="server" CssClass ="pic_item_empty" Visible="false">
                   <p class="pic_item_empty_head">List Empty</p>
                   <p  class="pic_item_empty_detail">There are no any option in this product Please insert on before </p>
                 </asp:Panel>
               </asp:Panel>
               <asp:Panel ID="panelConstructionList" runat="server"  CssClass="productPanel">
               <h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" /> Construction List</h4>
                <p class="contentheadedetail">List Supplier of This Product, you can Change or Add Supplier to List </p><br />
                 <asp:DropDownList ID="drpConstrucTionList" runat="server"  Width="400px"  ClientIDMode="Static" ></asp:DropDownList>
                 <asp:Panel ID="panelconEmpty" runat="server" CssClass ="pic_item_empty" Visible="false">
                   <p class="pic_item_empty_head">List Empty</p>
                   <p  class="pic_item_empty_detail">There are no any Construction in this product Please insert on before </p>
                 </asp:Panel>
               </asp:Panel>
               <asp:Panel ID="panelItineraryList" runat="server"  CssClass="productPanel">
               <h4><asp:Image ID="Image3" runat="server" ImageUrl="~/images/content.png" /> Itinerary List</h4>
                <p class="contentheadedetail">List Supplier of This Product, you can Change or Add Supplier to List </p><br />
                 <asp:DropDownList ID="dropItinerary" runat="server"  Width="400px"  ClientIDMode="Static"></asp:DropDownList>
                 <asp:Panel ID="panelItineraryEmpty" runat="server" CssClass ="pic_item_empty" Visible="false">
                   <p class="pic_item_empty_head">List Empty</p>
                   <p  class="pic_item_empty_detail">There are no any Itinerary in this product Please insert on before </p>
                 </asp:Panel>
               </asp:Panel>
            <asp:Panel ID="panelpicture_insert" runat="server"  CssClass="productPanel">
            <h4><asp:Image ID="Image4" runat="server" ImageUrl="~/images/content.png" /> Insert Box</h4>
            <p class="contentheadedetail">List Supplier of This Product, you can Change or Add Supplier to List </p><br />
            Select Type &  Row To Add <asp:DropDownList ID="drppicType" runat="server" DataTextField="Value" DataValueField="Key" ClientIDMode="Static"  EnableTheming="false"  CssClass="DropDownStyleCustom"  ></asp:DropDownList>
                
                <asp:DropDownList ID="numpicinsrt" runat="server" ClientIDMode="Static" EnableTheming="false"  CssClass="DropDownStyleCustom" ></asp:DropDownList>
                <asp:Button ID="btn_genform" runat="server"  Text="Submit" SkinID="Green_small"   OnClientClick="GenFormInsert();return false;" />
                <div id="picture_insert_form">
                    
                </div>
                <%--<product:Picture_insert Id="controlPictureInsertBox" runat="server"></product:Picture_insert>--%>
                
            </asp:Panel>
            <div style=" margin:10px 0px 0px 0px;padding:0px;" >
              <p style=" margin:0px;padding:0px; font-size:14px; font-weight:bold;"><span style=" color:Green">Please Upload To </span> <asp:label ID="lblPathUpLoad" runat="server"></asp:label></p>
            </div>
            <asp:Panel ID="panelpictureList" runat="server"  CssClass="productPanel_nofootline" ClientIDMode="Static">
            <h4><asp:Image ID="Image5" runat="server" ImageUrl="~/images/content.png" /> Picture List Box</h4>
            <div id="pictureList"></div>
            </asp:Panel>
            
        </div>
    </div>
    
    
</asp:Content>

