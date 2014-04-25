<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="account_booking_list.aspx.cs" Inherits="Hotels2thailand.UI.admin_account_account_booking_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script language="javascript" type="text/javascript" src="../../scripts/jquery-1.7.1.min.js"></script>
<script language="javascript" type="text/javascript">


</script>
    <style type="text/css">
        #table_result {
               background-color:#ebeff5; 
               font-size:12px;
        }
        #table_result th {
            background-color:#3e5d9d;
            color:#ffffff;
            font-weight:bold;
            text-align:center;
        }
       #table_result tr {
           text-align:center;
             background-color:#ffffff;
        }
       .alt {
            background-color:#eceff5;
        }
        #productSelect {
            margin:10px 0px 0px 0px;
            padding:0px;
        }
        #SearchBar {
             margin:10px 0px 0px 0px;
            padding:0px;
        }
        #booking_report {
            margin:0px;
            padding:0px;
            width:100%;
        }
        #order_by {
            margin:10px 0px 0px 0px;
            padding:0px;
        }
        #result {
             margin:10px 0px 0px 0px;
            padding:0px;
        }
        #booking_status
     {
         margin:0px;
         padding:0px;
     }
        #BookingList {
           width:100%;
       }
          #booking_status a
     {
         float:left;
         margin:10px 10px 0px 10px;
         padding:0px;
         width:130px;
         background-color:#f2ebbd;
         height:25px;
         line-height:25px;
         font-weight:normal;
         text-align:center;
         font-size:11px;
         color:#6A785A;
     }
             .bookingList_Page
{
     width:100%;
     margin:0px;
     padding:0px;
     
}
        .bookingList_Page a
{
    display:block;
    float:left;
     margin:2px 0px 2px 10px;
     padding:0px 3px 0px 10px;
     width:15px;
     height:15px;
     
     
}
        #BookingList {
           width:100%;
       }
.page_list
{
    background-color:#F8F3DB;
   
}

.page_list_active
{
    
    background-color:#8C9051;

    color:#ffffff;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <div id="booking_report">
        <div id="productSelect"> 
            <asp:DropDownList ID="dropProduct" EnableTheming="false" CssClass="DropDownStyleCustom_big" runat="server"></asp:DropDownList>

        </div>
        <div id="SearchBar">
           <asp:DropDownList ID="dropMonthStart" EnableTheming="false" CssClass="DropDownStyleCustom_big"  runat="server"></asp:DropDownList>
            <asp:DropDownList ID="dropYeatStart" EnableTheming="false" CssClass="DropDownStyleCustom_big"  runat="server"></asp:DropDownList>&nbsp;&nbsp;-&nbsp;&nbsp;
            <asp:DropDownList ID="dropMonthEnd"  EnableTheming="false" CssClass="DropDownStyleCustom_big" runat="server"></asp:DropDownList>
            <asp:DropDownList ID="dropYearEnd" EnableTheming="false" CssClass="DropDownStyleCustom_big"  runat="server"></asp:DropDownList>
            <asp:Button ID="btnSearch" EnableTheming="false" CssClass="btStyleGreen" runat="server" Text="Search" OnClick="btnSearch_Click" />
        </div>
        <div>

            <div id="result">

                <asp:Literal ID="ListREsult" runat="server" ClientIDMode="Static"></asp:Literal>
            </div>

            <div id="order_by"> Sort By: 
                <asp:DropDownList ID="dropOrderby" EnableTheming="false" CssClass="DropDownStyleCustom_big"  runat="server" AutoPostBack="true"  OnSelectedIndexChanged="dropOrderby_SelectedIndexChanged">
                    <asp:ListItem Value="0" Text="Status >>"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Status <<"></asp:ListItem>
                    <asp:ListItem Value="2" Text="ORDER ID >>"></asp:ListItem>
                    <asp:ListItem Value="3" Text="ORDER ID <<" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="4" Text="Check-In <<"></asp:ListItem>
                    <asp:ListItem Value="5" Text="Check-In >>"></asp:ListItem>
                    <asp:ListItem Value="6" Text="Check-out <<"></asp:ListItem>
                    <asp:ListItem Value="7" Text="Check-out >>"></asp:ListItem>
                    <asp:ListItem Value="8" Text="Flag >>"></asp:ListItem>
                    <asp:ListItem Value="9" Text="Flag <<"></asp:ListItem>
                    <asp:ListItem Value="10" Text="Price >>"></asp:ListItem>
                    <asp:ListItem Value="11" Text="Price <<"></asp:ListItem>
                </asp:DropDownList>
            </div>

        </div>
        
    </div>
</asp:Content>

