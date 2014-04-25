<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product-check-list.aspx.cs" Inherits="Hotels2thailand.UI.success_product_check_list" %>

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

            
            
        });

        function GetproductExtranetLsit(type) {
            
            $("<p class=\"progress_block\"><img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" /></p>").insertBefore("#product_list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });


            $("#product_list").fadeOut('fast');
            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            if (type == "normal") {

                
                $.post("../ajax/ajax_product_extranet_check_list.aspx", post, function (data) {
                    //alert(data);
                    $("#product_list").html(data);
                    $("#product_list").fadeIn('fast');

                    GetPageCheckList();
                });
            }

           
        }

        function GetPageCheckList() {
            
            var num = $(".product_row").length;
            if (num > 0) {
                $(".product_row").each(function () {
                    
                    var productid = $(this).attr("title");
                    
                    var type = $(this).children().each(function () {
                        
                        //book
                        if ($(this).attr("title") == "1") {
                            //alert($(this).attr("id"));
                            CheckUrl(productid, 1, $(this).attr("id"));
                        }

                        //map
                        if ($(this).attr("title") == "2") {
                            CheckUrl(productid, 2, $(this).attr("id"));
                        }

                        //review
                        if ($(this).attr("title") == "3") {
                            CheckUrl(productid, 3, $(this).attr("id"));
                        }

                        //review_write
                        if ($(this).attr("title") == "4") {
                            CheckUrl(productid, 4, $(this).attr("id"));
                        }

                        //thankyou
                        if ($(this).attr("title") == "5") {
                            CheckUrl(productid, 5, $(this).attr("id"));
                        }
                    });
                    
                    
                    
                });
            }
            
        }

        function CheckUrl(pid, pageType,obj) {
            
            $.get("../ajax/ajax_product_check_list.aspx?pid=" + pid + "&pt=" + pageType, function (data) {
               
                var ret = data.split(',');
                //var datass = JSON.parse(data);
               
                //$("#" + obj).html(datass.datareturn);
                
                if (ret[0] == "True") {
                    $("#" + obj).html("<img src=\"http://www.booking2hotels.com/images_extra/page_online.png\" title=\"" + ret[1] + "\" />");
                } else {
                    $("#" + obj).html("<img src=\"http://www.booking2hotels.com/images_extra/page_offline.png\" title=\"" + ret[1] + "\" />");
                }
               
            });
            
        }

        function jsonp(url, params, callback) {
            
            var script = document.createElement("script");
            script.setAttribute("src", url + '?' + params + '&callback=' + callback);
            script.setAttribute("type", "text/javascript");
            document.body.appendChild(script);

            alert(script);
        }

        function doit(data) {
            alert(data);
        }

        //jsonp('http://www.thezignhotel.com/thezignhotel_map.html', 'foo=bar', 'doit');
        //function GenProcess(desid) {
        //    var qProductCat = GetValueQueryString("pdcid");

        //    $.get("ajax_product_latest_code.aspx?desid=" + desid + "&pdcid=" + qProductCat + "&langId=1", function (data) {
        //        $("#latest_code").html(data);
        //    });
        //}

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
        
       
    
    
    <div class="product_list" id="product_list">
        
            
        

    </div>
    
    </asp:Content>
  
  