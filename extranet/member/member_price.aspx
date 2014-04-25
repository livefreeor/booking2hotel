<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="member_price.aspx.cs"
     Inherits="Hotels2thailand.UI.extranet_member_member_price" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" language="javascript" src="../../scripts/jquery-1.7.1.min.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js?ver=003"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
<link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
<script type="text/javascript" src="../../Scripts/darkman_datepicker.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ba-bbq.min.js"></script>
  
<script type="text/javascript" src="../../Scripts/extranet/member.js?ver=2.5"></script>
<link type="text/css" href="../../css/extranet/member.css" rel="stylesheet" />
    
    <script type="text/javascript" language="javascript">
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <div id="main_member">
        <div id="member_insert_link">
            <img src="../../images/plus.png" /><a href="" id="addnewmember">Add New Plan</a>
        </div>
        <div id="member_price_head">
            <h1>Member Price Plan</h1>
            <div id="member_price_insert_box">
               
            </div>
            <div id="member_price_box_real_border">
                <div id="member_price_box_real">
                    
                </div>
            </div>
            
        </div>

       <div id="member_price_list">

       </div>
    </div>
</asp:Content>

