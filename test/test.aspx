<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test_test" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
      <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager1" />
    <div>
    <asp:TextBox runat="server" ID="txtBox2" TextMode="MultiLine" Columns="50" Rows="10"
                    Text="Hello <b>world!</b>" /><br />
     <ajaxToolkit:HtmlEditorExtender ID="htmlEditorExtender2" TargetControlID="txtBox2"
                    runat="server" DisplaySourceTab="True" OnImageUploadComplete="ajaxFileUpload_OnUploadComplete">
                    <Toolbar>                        
                        <ajaxToolkit:Bold />
                        <ajaxToolkit:Italic />
                        <ajaxToolkit:Underline />
                        <ajaxToolkit:HorizontalSeparator />
                        <ajaxToolkit:JustifyLeft />
                        <ajaxToolkit:JustifyCenter />
                        <ajaxToolkit:JustifyRight />
                        <ajaxToolkit:JustifyFull />
                        <ajaxToolkit:CreateLink />
                        <ajaxToolkit:UnLink />
                        <ajaxToolkit:InsertImage />                        
                    </Toolbar>
                </ajaxToolkit:HtmlEditorExtender>
    </div>
    </form>
</body>
</html>
