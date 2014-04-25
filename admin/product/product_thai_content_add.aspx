<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product_thai_content_add.aspx.cs" Inherits="Hotels2thailand.UI.admin_product_thai_content_add" %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/ProductTypeLangBox.ascx" TagName="ProductTypeLangBox" TagPrefix="Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

 <script language="javascript" type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            $("#btnSave").attr("disabled", "disabled");
            $("#btnPreview").click(function () {

                $("<div class=\"progress_block\"><img class=\"img_progress\" src=\"/images/progress.gif\" alt=\"Progress\" /></div>").insertAfter("#preview").ajaxStart(function () {
                    $(this).show();
                }).ajaxStop(function () {
                    $(this).remove();
                });

                var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

                $.post("../ajax/ajax_product_thaicontent_preview.aspx", post, function (data) {

                    if (data == "error") {
                        $("#btnSave").attr("disabled","disabled");
                        $("#preview").slideDown('fast', function () {
                            $("#preview").html("Incorrect Format Please Check Thai Detail Block!!!");
                        });
                    } else {
                        $("#btnSave").removeAttr("disabled");
                        $("#preview").slideDown('fast', function () {
                            $("#preview").html(data);
                        });
                    }

                });
            });

        });
         


    </script>
      <style type="text/css">
    
    #listItem
    {
	    padding:5px;
	    width:1020px;
	    background-color:#d8dfea;
    }
    #listItem td
    {
	    background-color:#ffffff;
	    padding:10px;
	    height:20px;
	    line-height:25px;
	
    }
    #listItem th
    {
	    font-family:verdana;
	    font-size:15px;
	
	    color:#004f4f;
	    line-height:30px;
	    background:#f4f4f4;
	
    }
    .text400{
	    width:460px;
	    height:30px;
	    border:2px solid #9cacbc;
    }
    .textArea400{
	    width:460px;height:300px;
	    border:1px solid #9cacbc;
    }
    #preview
    {
        border:2px solid #d8dfea;
        padding:10px;
        margin:0px;
    }
     .style
     {
         margin:0px;
         padding:0px;
     }
     .style .header
     {
         margin:0px;
         padding:0px;
         color:Blue;
         font-size:16px;
         
         height:20px;
     }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <h6><asp:Image ID="imghead" runat="server" ImageUrl="~/images/imgheadtitle.png" />&nbsp;<asp:Label ID="Destitle" runat="server" ></asp:Label> : <asp:Label ID="txthead" runat="server"></asp:Label></h6>
 <br /><br />
    <asp:Label ID="lblMain" runat="server"></asp:Label>
    <br /><br />
    <div id="preview" style="display:none">
    
    </div>
    <div style=" margin:0 auto; width:100%; text-align:center;">
    <input type="button" value="Detail Preview" id="btnPreview" />
    <asp:Button ID="btnSave"  runat="server" ClientIDMode="Static" Text="Save" OnClick="btnSave_Onclick" />
<%--    result = result + "<tr><td colspan=\"2\" align=\"center\"><input type=\"submit\" name=\"submit\" value=\"Save\" /></td>\n";
                            result = result + "</tr>\n";--%>
    </div>
</asp:Content>

