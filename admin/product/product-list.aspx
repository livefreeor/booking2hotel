<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product-list.aspx.cs" Inherits="Hotels2thailand.UI.success_product_list" %>

 <asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
<link  href="../../css/extranet_list_style.css" rel="Stylesheet" type="text/css" />
<script language="javascript" type="text/javascript" src="../../scripts/jquery-1.7.1.min.js" ></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js" ></script>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            GetproductExtranetLsit("normal");

            $("#sp_click").click(function () {
                GetproductExtranetLsit("normal");
                //$("#dropDesExtranet").show();
                return false;
            });

            $("#ch_click").click(function () {
                GetproductExtranetLsit("chain");
                //$("#dropDesExtranet").hide();
                return false;
            });

            $("#bhtmanage").click(function () {
                GetproductExtranetLsit("bht");
                //$("#dropDesExtranet").hide();
                return false;
            });
            $("#bhtmanageB2b").click(function () {
                GetproductExtranetLsit("bhtb2b");
                //$("#dropDesExtranet").hide();
                return false;
            });
            $("#hotelmange").click(function () {
                GetproductExtranetLsit("hotel");
                //$("#dropDesExtranet").hide();
                return false;
            });
            $("#flatrate").click(function () {
                GetproductExtranetLsit("flat");
                //$("#dropDesExtranet").hide();
                return false;
            });
            $("#monthlyrate").click(function () {
                GetproductExtranetLsit("monthly");
                //$("#dropDesExtranet").hide();
                return false;
            });
            $("#steprate").click(function () {
                GetproductExtranetLsit("step");
                //$("#dropDesExtranet").hide();
                return false;
            });
            $("#offline").click(function () {
                GetproductExtranetLsit("hoff");
                //$("#dropDesExtranet").hide();
                return false;
            });
            $("#online").click(function () {
                GetproductExtranetLsit("hon");
                //$("#dropDesExtranet").hide();
                return false;
            });

            //$("#dropDesExtranet").change(function () {
            //    GetproductExtranetLsit("normal");
            //    return false;
            //});

            $("#btnSummit").click(function () {

                GetproductExtranetLsit("normal");
                return false;
            });
              
            $("#btnSearch").click(function () {
                
                GetproductExtranetLsit("advance");

                return false;
            });
            
        });

        function GetproductExtranetLsit(type) {
       
            $("<p class=\"progress_block\"><img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" /></p>").insertBefore("#product_list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            $("#product_list").fadeOut('fast', function () {
                $(this).data("");
            });
            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            if (type == "normal" || type != "chain") {
                $.post("../ajax/ajax_product_extranet_list.aspx?type=" + type, post, function (data) {
                    
                    $("#product_list").html(data);
                    $("#product_list").fadeIn('fast');

                   // GetPageCheckList();
                });
            }

            if (type == "chain") {

                $.post("../ajax/ajax_product_extranet_list_chain.aspx", post, function (data) {
                  
                    $("#product_list").html(data);
                    $("#product_list").fadeIn('fast');
                   // GetPageCheckList();
                });
            }


            if (type == "advance") {

                $.post("../ajax/ajax_product_extranet_list_advance.aspx", post, function (data) {

                    $("#product_list").html(data);
                    //$("#product_list").fadeIn('fast');
                   // GetPageCheckList();
                });
            }
        }



    </script>
     <style   type="text/css">

.block_extra_view
{
     margin:10px 0px 10px 0px;
}
.block_extra_view a
{
     float:left;
      padding:5px 10px 5px 10px;
       color:#ffffff;
       margin:0px 0px 0px 10px;
}
.drop_des
{
     margin:10px 0px 0px 0px;
}
.sp
{
     background-color:#3f5d9d;
}
.ch
{
     background-color:#72ac58;
    
    }
</style>
 </asp:Content>
    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        
        <asp:HyperLink ID="lnkCreate" runat="server"  ><asp:Image ID="imgPlus" ImageUrl="~/images/plus.png" runat="server"  /> Add New Product</asp:HyperLink> &nbsp&nbsp;|&nbsp;&nbsp;
        <a href="../extranet/addnewextranet.aspx" target="_blank">add staff stand alone</a>&nbsp&nbsp;|&nbsp;&nbsp;
<a href="../extranet/addnewextranet_chain.aspx" target="_blank" style=" color:#72ac58;">add staff by Chain <label style="color:#dd3822;">new!!</label></a>&nbsp&nbsp;|&nbsp;&nbsp;
<a href="../extranet/addnewextranet_chain_extend.aspx" target="_blank" style=" color:#72ac58;">add staff by Extend Chain <label style="color:#dd3822;">new!!</label></a>&nbsp&nbsp;|&nbsp;&nbsp;
<a href="../extranet/extranet_public_holidays.aspx" target="_blank">Manage Public Holidays</a>
    <br /><br />
        <div  class="block_extra_view">
<a href="#" id="sp_click" class="sp" style=" margin-left:0px;" >Standard View</a>
<a href="#" id="ch_click"  class="ch">Chain(Group) View</a>
      <a href="product-check-list.aspx" id="checkList"  class="sp">Product Page Check List</a>
</div>
        <div style="clear:both"></div>
    <div class="product_list_sort_box">
        <table >
            <tr>
                <td>
                <p>Destination</p>
                    <asp:DropDownList ID="dropDestination" runat="server" ClientIDMode="Static" ></asp:DropDownList>
                </td>
                
                <td>
                <p>Process</p>
                    <asp:DropDownList ID="dropStatusProcess" runat="server" ClientIDMode="Static" ></asp:DropDownList>
                </td>
                <%--<td>
                <p>Expire Check</p>
                    <asp:DropDownList ID="dropExpired" runat="server" ></asp:DropDownList>
                </td>--%>
                
                <td>
                <p>Status</p>
                    <asp:DropDownList ID="dropStatus" runat="server" ClientIDMode="Static" >
                        <asp:ListItem Text="Enable" Value="True"></asp:ListItem>
                        <asp:ListItem Text="Disable" Value="False"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td valign="bottom">
                    
                    <asp:Button ID="btnSummit" runat="server"  SkinID="Blue" Text="Submit" ClientIDMode="Static" />
                </td>
            </tr>
            
        </table>
        <div style="text-align:center; padding:7px; margin:10px 0px 0px 0px; border:1px solid #627aad; background:#f2f2f2" >
            <p style="margin:0px;padding:2px;">Advance Search</p>
            <asp:TextBox ID="txtSearch" runat="server" Width="600px" Height="20px" ClientIDMode="Static"  ></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="Green" ClientIDMode="Static"  />
        </div>

        <p id="txt_latest_code">Latest code: <label id="latest_code"></label></p>
        <p>Qiuck Filter: 
            <a id="bhtmanage" href="#">BHT Manage</a>&nbsp;|&nbsp;
            <a id="bhtmanageB2b" href="#">BHT Manage(B2b)</a>&nbsp;|&nbsp;
            <a id="hotelmange" href="#">Hotel Manage</a>&nbsp;|&nbsp;
            <a id="flatrate" href="#">Commission Flat Rate(%)</a>&nbsp;|&nbsp;
            <a id="monthlyrate" href="#">Commission Monthly(รายเดือน)</a>&nbsp;|&nbsp;
            <a id="steprate" href="#">commission Step(ขั้นบันได)</a>&nbsp;|&nbsp;
            <a id="offline" href="#">Hotel Manage Offline Charge</a>&nbsp;|&nbsp;
            <a id="online" href="#">Hotel Manage GateWay</a>
        </p>
    </div>
    <br />
    
    
    <div class="product_list" id="product_list">
        
            
        

    </div>
    
    </asp:Content>
  
  