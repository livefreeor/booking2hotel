<%@ Page Language="C#" AutoEventWireup="true" CodeFile="contentAdd.aspx.cs" Inherits="temp_contentAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
    body
    {
	    font-family:Verdana, Geneva, sans-serif;
	    font-size:12px;
    }
    #listItem
    {
	    padding:5px;
	    width:960px;
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
	    width:420px;
	    height:30px;
	    border:2px solid #9cacbc;
    }
    .textArea400{
	    width:420px;height:100px;
	    border:1px solid #9cacbc;
    }
    </style>
</head>
<body>
    <asp:Label ID="lblMain" runat="server"></asp:Label>
</body>
</html>
