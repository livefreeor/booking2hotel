<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product_thai_content.aspx.cs" Inherits="Hotels2thailand.UI.admin_product_thai_content" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
    
.tblListResult
{
	width:960px;
	background-color:#d8dfea;
}
.tblListResult td
{
	
	padding:3px;
	height:20px;
	line-height:25px;
}
.rowOdd 
{
    background-color:#ffffff;
		
}
.rowEven 
{
	background-color:#f2f2f2;
}

.tblListResult th
{
	font-family:verdana;
	font-size:15px;
	color:#004f4f;
	line-height:30px;
	background:#f4f4f4;
}
.tblListResult .productTitle
{
	color:#004f4f;
	background-color:#f2f2f2;
	font-size:14px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:Literal ID="lblresult" runat="server"></asp:Literal>
</asp:Content>

