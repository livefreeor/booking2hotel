 <%@ Page Title="Hotels2thailand - Send Newsletter" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="sendNewsletter.aspx.cs" Inherits="Hotels2thailand.UI.SendNewsletter"  %>
<%@ PreviousPageType VirtualPath="~/extranet/newsletter/ShowNewsletterList.aspx" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/Linkbox.ascx" TagName="Linkbox" TagPrefix="Ctrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script language="javascript" type="text/javascript" src="../../scripts/jquery-1.8.3.min.js"></script>
    <script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
     <link type="text/css" href="../../css/extranet/promotion_extra.css" rel="stylesheet" />
    <link href="/css/newsletter/newsletter.css"type="text/css" rel="Stylesheet" />
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            
           
            if ($("#txtSubject").val() == "") {
                $("#txtSubject").val("Subject").addClass("watermark");
            }
            
            $("#txtSubject").focus(function () {
                
                if ($(this).val() == "Subject") {
                    $("#txtSubject").val("").removeClass("watermark");;
                }
            })

            $("#txtSubject").blur(function () {
                if ($(this).val() == "") {
                    $("#txtSubject").val("Subject").addClass("watermark");
                }
            });
        });

        function ChecValid() {
            var ret = false;
            if ($("#txtSubject").val() != "" && $("#txtSubject").val() != "Subject") {

                ret = confirm('Are you sure you want to send the newsletter?')
                
            } else {
                DarkmanPopUpAlert("400", "The Subject field is required.");
            }
           
            return ret;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
<%--<div class="sectiontitle">--%>

    
  <asp:Literal ID="titles" runat="server" ></asp:Literal>
 <p class="pro_status_title" style=" border:0px;"> </p>
     
    <div id="right" style="margin:0 0 0 0 ; ">
 
   <asp:Panel runat="server" ID="panSend">
   
       <asp:Panel runat="server" ID="panelMailType" Visible="true">
   
   <%-- <asp:RadioButton ID="raioMailTypeGeneral" GroupName="mailtype" Checked="true"  Text="Send to all customer" runat="server" OnCheckedChanged="raioMailTypeGeneral_CheckedChanged" />
    <asp:RadioButton ID="radioMailTypeMember" GroupName="mailtype" runat="server"  Text="Send to member only!" />--%>
       </asp:Panel>

   <br />
   <asp:TextBox ID="txtSubject" runat="server" Width="845px" MaxLength="300" ClientIDMode="Static"  EnableTheming="false" CssClass="Extra_textbox_big"   ></asp:TextBox>
      
       <%--<asp:RegularExpressionValidator ControlToValidate="txtSubject" runat="server" SetFocusOnError="true" Text="The Subject field is required." ToolTip="The Subject field is required." Display="Dynamic" ForeColor="Red"  ValidationExpression=""></asp:RegularExpressionValidator>--%>
   <%--<asp:RequiredFieldValidator ID="valRequireSubject" runat="server"  ControlToValidate="txtSubject" SetFocusOnError="true" EnableClientScript="true"
      Text="The Subject field is required." ToolTip="The Subject field is required." Display="Dynamic" ForeColor="Red" ValidationGroup="Newsletter"></asp:RequiredFieldValidator>--%>
  
       <br /><br />
   
   
   <asp:Panel ID="fckeditor" runat="server">
       
 <%--  <small><b><asp:Literal runat="server" ID="lblHtmlBody" Text="HTML Body:" /></b></small><br />--%>
   
         <%-- <HTMLEditor:Editor  runat="server"  Id="txtHtmlBody" Height="600px" Width="860px" AutoFocus="true" />--%>

       <asp:TextBox ID="txtHtmlBody" runat="server" AutoCompleteType="Company" ClientIDMode="Static"
                BorderStyle="Solid" BorderColor="#3399FF" TabIndex="4" Height="600px" Width ="860px" 
               TextMode="MultiLine" AutoPostBack="True" ></asp:TextBox>

                
            <ajaxToolkit:HtmlEditorExtender ID="txtDescricao_HtmlEditorExtender" runat="server"  DisplaySourceTab="true" EnableSanitization="false"
                Enabled="True" TargetControlID="txtHtmlBody" OnImageUploadComplete=" txtDescricao_HtmlEditorExtender_ImageUploadComplete">
                  <Toolbar>
                    <ajaxToolkit:Undo />
                    <ajaxToolkit:Redo />
                    <ajaxToolkit:Bold />
                    <ajaxToolkit:Italic />
                    <ajaxToolkit:Underline />
                    <ajaxToolkit:StrikeThrough />
                    <ajaxToolkit:Subscript />
                    <ajaxToolkit:Superscript />
                    <ajaxToolkit:JustifyLeft />
                    <ajaxToolkit:JustifyCenter />
                    <ajaxToolkit:JustifyRight />
                    <ajaxToolkit:JustifyFull />
                    <ajaxToolkit:InsertOrderedList />
                    <ajaxToolkit:InsertUnorderedList />
                    <ajaxToolkit:CreateLink />
                    <ajaxToolkit:UnLink />
                    <ajaxToolkit:RemoveFormat />
                    <ajaxToolkit:SelectAll />
                    <ajaxToolkit:UnSelect />
                    <ajaxToolkit:Delete />
                    <ajaxToolkit:Cut />
                    <ajaxToolkit:Copy />
                    <ajaxToolkit:Paste />
                    <ajaxToolkit:BackgroundColorSelector />
                    <ajaxToolkit:ForeColorSelector />
                    <ajaxToolkit:FontNameSelector />
                    <ajaxToolkit:FontSizeSelector />
                    <ajaxToolkit:Indent />
                    <ajaxToolkit:Outdent />
                    <ajaxToolkit:InsertHorizontalRule />
                    <ajaxToolkit:HorizontalSeparator />
                    <ajaxToolkit:InsertImage />
                </Toolbar>
            </ajaxToolkit:HtmlEditorExtender>
       

   </asp:Panel>
    
    <%--<asp:CheckBox ID="chkIsfav" runat="server" Text="Keep this Newsletters to Favortie"  Height="20px"  CssClass="checkfav"  />
    <p></p>--%>
  <br /><br />
   <asp:Button ID="btnReSubmit" runat="server" Text="ReSend" ValidationGroup="Newsletter"  
      OnClientClick="if (confirm('Are you sure you want to send the newsletter?') == false) return false;" OnClick="btnReSubmit_Click" CssClass="Extra_Button_green"   />
   <asp:Button ID="btnSend" runat="server" Text="Send" ValidationGroup="Newsletter"
      OnClientClick="return ChecValid();" OnClick="btnSend_Click" CssClass="Extra_Button_green"  />
      
      <%--<asp:Button ID="btnreSend" runat="server" Text="Forward" ValidationGroup="Newsletter"
      OnClientClick="if (confirm('Are you sure you want to send from this newsletter?') == false) return false;" OnClick="btnreSend_Click" CssClass="Extra_Button_green"  />--%>
   
   <%--<asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" ValidationGroup="Newsletter"  />--%>
   <%--<asp:Button ID="btnUpdate" runat="server" Text="Update" onclick="btnUpdate_Click" ValidationGroup="Newsletter" />--%>
   
   
   
   </asp:Panel>

   <asp:Panel ID="panWait" runat="server" Visible="false">      
      <asp:Label runat="server" id="lblWait">
      <p>Another newsletter is currently being sent. Please wait until it completes
      before compiling and sending a new one.</p>
      <p>You can check the current newsletter's completion status from <a href="sendingNewsletter.aspx?mc=<%=this.qMailCat %>">this page</a>.</p>
      </asp:Label>
   </asp:Panel>

         </div>
   <div style="clear:both"></div>
</asp:Content>


