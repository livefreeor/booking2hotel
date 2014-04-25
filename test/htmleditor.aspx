<%@ Page Language="C#" AutoEventWireup="true" CodeFile="htmleditor.aspx.cs" Inherits="test_htmleditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>--%>
 <script language="javascript" type="text/javascript" src="../scripts/jquery-1.8.3.min.js"></script>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

        $(document).ready(function () {
           // document.getElementById('txtDescricao').innerHTML
            //alert($("#txtDescricao").text());
            //alert($("#txtDescricao_HtmlEditorExtender_ExtenderContentEditable").html());
            //document.getElementById('txtDescricao_HtmlEditorExtender_ExtenderContentEditable').innerHTML
        });

        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1"  />
    <div>
         <asp:TextBox ID="txtSubject" runat="server" Width="845px" MaxLength="300" ClientIDMode="Static"  EnableTheming="false" CssClass="Extra_textbox_big"   ></asp:TextBox>
        <br />
        <asp:TextBox ID="txtDescricao" runat="server" ClientIDMode="Static" AutoCompleteType="Company" 
                BorderStyle="Solid" BorderColor="#3399FF" TabIndex="4" Width ="800" 
                Height="700" TextMode="MultiLine" AutoPostBack="True" MaxLength="5000"></asp:TextBox>

                
            <ajaxToolkit:HtmlEditorExtender ID="txtDescricao_HtmlEditorExtender" runat="server"  DisplaySourceTab="true"   EnableSanitization="false"
                Enabled="True" TargetControlID="txtDescricao" OnImageUploadComplete=" txtDescricao_HtmlEditorExtender_ImageUploadComplete">
                  <Toolbar>
                    <ajaxToolkit:Bold />
                    <ajaxToolkit:Italic />
                    <ajaxToolkit:InsertImage />
                </Toolbar>
               
            </ajaxToolkit:HtmlEditorExtender>


      
     <%--  <HTMLEditor:Editor  runat="server"  Id="txtHtmlBody" Height="600px" Width="860px" AutoFocus="true" />--%>
    </div>
        <br /><br />
          <asp:Button ID="btnTest" runat="server" Text="Run" OnClick="btnTest_Click" />
    </form>
</body>
</html>
