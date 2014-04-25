<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="review_admin.aspx.cs" Inherits="Hotels2thailand.UI.admin_review_review_admin"  %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../css/extranet/reviewstyle.css" rel="Stylesheet" type="text/css" />
 <link rel="stylesheet" type="text/css" href="../../css/jquery.fastconfirm.css"/>
  <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
  <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>

 <script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
 <script src="../../Scripts/jquery.fastconfirm.js" type="text/javascript"></script>
 <script type="text/javascript" src="../../Scripts/darkman_review_script.js?ver=02"></script>
 <script language="javascript" type="text/javascript" src="../../js/jquery.rating.js"></script>
<link href="../../css/jquery.rating.css" type="text/css" rel="stylesheet" />
   <script language="javascript" type="text/javascript">

       
   </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <script type="text/javascript">

      
   </script>
<div class="review_main" >
    <div id="Review_page_title" class="headtitle">
            
    </div>
 <div class="review_left" id="review_left">
        <%--<div class="review_left_Product" id="review_left_Product">
            <asp:HyperLink ID="h_hotels"  runat="server" ClientIDMode="Static" onclick="getReviewListAjax('hotels','h_hotels');return false;"><img class="img_icon" src="../../images/hotelicon_s.png" alt="hotels" />Hotel</asp:HyperLink>
            <asp:HyperLink ID="h_spa" runat="server" ClientIDMode="Static" onclick="getReviewListAjax('spa','h_spa');return false;"><img class="img_icon" src="../../images/spaicon_s.png" alt="spa" />Spa</asp:HyperLink>
            <asp:HyperLink ID="h_golfs" runat="server" ClientIDMode="Static" onclick="getReviewListAjax('golfs','h_golfs');return false;"><img class="img_icon" src="../../images/golficon_s.png" alt="golfs" />Golf Course</asp:HyperLink>
            <asp:HyperLink ID="h_daytrips" runat="server" ClientIDMode="Static" onclick="getReviewListAjax('daytrips','h_daytrips');return false;"><img class="img_icon" src="../../images/dayicon_s.png" alt="daytrips" />Day Trips</asp:HyperLink>
            <asp:HyperLink ID="h_waters" runat="server" ClientIDMode="Static" onclick="getReviewListAjax('waters','h_waters');return false;"><img class="img_icon" src="../../images/watericon_s.png" alt="waters" />Water Activity</asp:HyperLink>
            <asp:HyperLink ID="h_show" runat="server" ClientIDMode="Static" onclick="getReviewListAjax('show','h_show');return false;"><img class="img_icon" src="../../images/showicon_s.png" alt="show" />Show & Event</asp:HyperLink>
            <asp:HyperLink ID="h_health" runat="server" ClientIDMode="Static" onclick="getReviewListAjax('health','h_health');return false;"><img class="img_icon" src="../../images/healthicon_s.png" alt="health" />Health CheckUp</asp:HyperLink>
        </div>--%>
        <div class="review_left_type" id="review_left_type">
            <asp:HyperLink ID="h_approve" runat="server" ClientIDMode="Static" onclick="getReviewListAjaxType('approve','h_approve');return false;">Approved</asp:HyperLink>
            <asp:HyperLink ID="h_waiting" runat="server" ClientIDMode="Static" onclick="getReviewListAjaxType('waiting','h_waiting');return false;">Wait to Approve</asp:HyperLink>
            <asp:HyperLink ID="h_bin" runat="server" ClientIDMode="Static" onclick="getReviewListAjaxType('bin','h_bin');return false;">Bin</asp:HyperLink>
         </div>
         <div  style=" clear:both"></div>
         <%--<div class="review_left_type" >
            <asp:HyperLink ID="h_site_review" NavigateUrl="~/admin/review/review_site_admin_list.aspx" runat="server" ClientIDMode="Static" ><img class="img_icon" src="../../images/comment_b.png"  style=" width:20px; height:20px" alt="hotels" />Site Review</asp:HyperLink>
         </div>--%>
            <%--<div class="review_left_type" >
            <asp:LinkButton ID="clearSpam" runat="server" Text="Clear Spam" OnClick="clearSpam_Onclick"></asp:LinkButton>
         </div>--%>
    </div>
     <div  style=" clear:both"></div>
    <div class="review_right">
    <div class="review_temp" style=" display:none;"></div>
    
        
        <div id="navigator_top"  class="review_navigator_page"  >
        
        </div> 
        
        <div style="clear:both"></div>
       
        <div id="review_result">
            
        </div>
        
        <div id="navigator_bottom"  class="review_navigator_page" >
        
        
        </div> 
        
    </div>
    <div style="clear:both"></div>
</div>


<%--<div><asp:DropDownList ID="dropPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropPageSize_OnSelectIndexChange"></asp:DropDownList></div>--%>

    


</asp:Content>

