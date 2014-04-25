<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true"  CodeFile="product_report.aspx.cs"
 Inherits="Hotels2thailand.UI.product_report" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Producttion:Booking Stat</title>
<script language="javascript" src="../../scripts/jquery-1.6.1.js" type="text/javascript"></script>

<script language="javascript" type="text/javascript">

    $(document).ready(function () {

        $("#dropReport_type").change(function () {
            var val = $(this).val();

            if (val == "6" || val == "7") {

                $("#month").animate({ width: "show",
                    borderleft: "show",
                    borderRight: "show",
                    paddingLeft: "show",
                    paddingRight: "show"
                }, "fast");

            } else {

                $("#month").animate({ width: "hide",
                    borderleft: "hide",
                    borderRight: "hide",
                    paddingLeft: "hide",
                    paddingRight: "hide"
                });
            }

        });


        $("#btnSearch").click(function () {

            $("<p class=\"progress_block\"><img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" /></p>").insertBefore("#product_report_result").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_report_product_list.aspx", post, function (data) {

                $("#product_report_result").html(data);
            });
        });

    });
</script>
 <style type="text/css">
  #product_report_result
  {
      margin:0px;
      padding:0px;
      width:100%;
  }
  .total
  {
       margin:15px 0px 0px 0px;
      padding:0px 0px 5px 0px;
      width:100%;
      font-size:14px;
      font-weight:bold;
      border-bottom:1px solid #cccccc;
  }
  #product_result
  {
      margin:0 auto;
      padding:0px;
       font-size:12px;
  }
  #product_result table
  {
      margin:15px 0px 0px 0px ;
      padding:0px;
      background-color:#d6d6d6;
      width:100%;
  }
  #product_result table tr 
  {
      margin:0px;
      padding:0px;
      height:25px;
      line-height:25px;
  }
  #product_result table tr th
  {
      margin:0px;
      padding:0px;
      text-align:center;
       color:#ffffff;
      
  }
  #product_result table tr td
  {
      margin:0px;
      padding:0px;
      text-align:center;
  }
 </style>
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 

 <div id="main_product_report">
 <div id="product_report_box_search">
     <table>
         <tr>
            <td><asp:DropDownList ID="dropCat" runat="server" SkinID="DropCustomstyle" ClientIDMode="Static"></asp:DropDownList></td>
            <td><asp:DropDownList ID="dropDes" runat="server" SkinID="DropCustomstyle" ClientIDMode="Static"></asp:DropDownList></td>
            <td>
            <asp:DropDownList ID="dropReport_type" runat="server" SkinID="DropCustomstyle" ClientIDMode="Static">
             <asp:ListItem Value="1" >Product Online-Contract</asp:ListItem>
             <asp:ListItem Value="2" >Product Online-Extranet</asp:ListItem>
             <asp:ListItem Value="3" >Product Whole Sales</asp:ListItem>
             <asp:ListItem Value="4" >Product Rate Expired-Contract</asp:ListItem>
             <asp:ListItem Value="5" >Product Rate Expired-Extranet</asp:ListItem>
             <asp:ListItem Value="6" >New Product of Month</asp:ListItem>
             <asp:ListItem Value="7" >Product New Promotion of Month</asp:ListItem>
             
            </asp:DropDownList>

            
            </td>
            <td><div id="month" style="display:none" >
             <select id="selMonth" name="selMonth" class="DropDownStyleCustom_big">
              <option  value="1">Jan</option>
              <option  value="2">Feb</option>
              <option  value="3">Mar</option>
              <option  value="4">Apr</option>
              <option  value="5">May</option>
              <option  value="6">Jun</option>
              <option  value="7">Jul</option>
              <option  value="8">Aug</option>
              <option  value="9">Sep</option>
              <option  value="10">Oct</option>
              <option  value="11">Nov</option>
              <option  value="12">Dec</option>
             </select>
            </div></td>
            <td>
             <input type="button" id="btnSearch" value="Search" class="btStyleGreen" />
            </td>
         </tr>
     </table>
 </div>
  <div id="product_report_result">
        
  </div>
 </div>

</asp:Content>
    
   
