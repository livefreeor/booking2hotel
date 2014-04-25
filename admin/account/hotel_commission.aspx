<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="hotel_commission.aspx.cs" Inherits="Hotels2thailand.UI.admin_account_hotel_commission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .div_menu_block {
             margin:0px;
             padding:20px;
             width:900px;
        }
            .div_menu_block a {
                display:block;
                margin:10px 0 0 10px;
             float:left;
              padding:40px;
              background-color:#f8f4f4;
              border:1px solid #cccccc;
              font-size:18px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="div_menu_block">
             <a href="/admin/account/com_hotel_manage.aspx" target="_blank"><img src="/images_extra/commission.png" alt="booking search" /><br />Commission <br />Hotel Manage</a>
             
            <a href="/admin/account/com_monthly_rate.aspx" target="_blank"><img src="/images_extra/commission.png" alt="booking search" /><br />Commission<br /> Monthly Rate </a>
         
            <a href="/admin/account/com_bht_manage.aspx" target="_blank"><img src="/images_extra/commission.png" alt="booking search" /><br />Commission <br />BHT Manage</a>
         
         <a href="/admin/account/com_sales_commission.aspx" target="_blank"><img src="/images_extra/commission.png" alt="booking search" /><br />Commission <br />Sales Com Manage [The Berkeley Hotel Only]</a>
         
      </div>    

</asp:Content>

