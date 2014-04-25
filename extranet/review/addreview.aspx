<%@ Page Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" Theme="hotels2theme" AutoEventWireup="true" CodeFile="addreview.aspx.cs" Inherits="Hotels2thailand.UI.extranet_review_addreview" EnableEventValidation="false" EnableTheming="false" EnableViewState="false" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">

  <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
  <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
 <script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
   <script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
  <script language="javascript" type="text/javascript" src="../../js/jquery.rating.js"></script>
<link href="../../css/jquery.rating.css" type="text/css" rel="stylesheet" />
   <script language="javascript" type="text/javascript">
       
   </script>

   <style type="text/css">
        
ul#ratingPan{
	
	padding:0px;
	margin:0px;
	width:600px;
}
ul#ratingPan li{
font-family:Verdana, Geneva, sans-serif;
font-size:12px;
color:#333;
display:block;
float:left;
width:200px;
padding:5px 0px;
list-style:none;
background-image:none;
}
.review_rate_select_box{
	margin-left:5px;
	padding-left:10px;
	width:665px;
	color:#000;
}
.review_rate_select_box span{
	padding-right:8px;
	font-size:15px;
	color:#77787b;
	font-weight:bold;
	float:left;
}
#review_box .review_rate_select_box ul li{ width:240px; margin-right:50px;}
#review_box{
	position:relative;
	margin:10px 0 0px 32px;
	padding:6px 0 0 0;
	width:100%;
	
}
#review_box span
{
     font-size:11px;
     margin:0px 0px 5px 0px;
}
.review_box_header{
	margin:0 0 6px 5px;
	padding-left:10px;
	background:#f4f7fc;
	width:665px;
	height:34px;
	font-size:22px;
	color:#F93;
	font-weight:bold;
	line-height:28px;	
}
.rating_header_orage{
	margin-left:5px;
	padding:35px 0 0 10px;
	width:665px;
	font-size:15px;
	color:#f57f20;
	font-weight:bold;	
}
.rating_header_orage span{ font-weight:normal; color:#000; font-size:13px;}

.rating_header_tell{
	margin-left:5px;
	padding:0 0 20px 10px;
	width:665px;
	font-size:15px;
	color:#f57f20;
	font-weight:bold;	
}
.rating_header_tell span{
	padding-left:10px;
	font-size:12px;
	color:#000;
	font-weight:normal;
}
#review_box .rating_header_tell .captcha{ margin:0px; padding:0px;}
#review_box .rating_header_tell a{ padding-left:10px; text-decoration:underline; line-height:30px;}
#review_box .rating_header_tell .captcha img{ margin:0px; padding:0 0 10px 10px; width:103px; height:25px; float:left;}
   </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

            <div id="review_box">
           	         
                
              <div class="review_rate_select_box">
              <ul id="ratingPan"><li><strong>Overall</strong><br/><input name="rate_overall" type="radio" class="star" value="1" />
<input name="rate_overall" type="radio" class="star" value="2" />
<input name="rate_overall" type="radio" class="star" value="3" />
<input name="rate_overall" type="radio" class="star" value="4" />
<input name="rate_overall" type="radio" class="star" value="5" />
<input name="rate_overall" type="radio" class="star" value="6" />
<input name="rate_overall" type="radio" class="star" value="7" />
<input name="rate_overall" type="radio" class="star" value="8" />
<input name="rate_overall" type="radio" class="star" value="9" />
<input name="rate_overall" type="radio" class="star" value="10" />
</li>
<li><strong>Service</strong><br/><input name="rate_service" type="radio" class="star" value="1" />
<input name="rate_service" type="radio" class="star" value="2" />
<input name="rate_service" type="radio" class="star" value="3" />
<input name="rate_service" type="radio" class="star" value="4" />
<input name="rate_service" type="radio" class="star" value="5" />
<input name="rate_service" type="radio" class="star" value="6" />
<input name="rate_service" type="radio" class="star" value="7" />
<input name="rate_service" type="radio" class="star" value="8" />
<input name="rate_service" type="radio" class="star" value="9" />
<input name="rate_service" type="radio" class="star" value="10" />
</li>
<li><strong>Location</strong><br/><input name="rate_location" type="radio" class="star" value="1" />
<input name="rate_location" type="radio" class="star" value="2" />
<input name="rate_location" type="radio" class="star" value="3" />
<input name="rate_location" type="radio" class="star" value="4" />
<input name="rate_location" type="radio" class="star" value="5" />
<input name="rate_location" type="radio" class="star" value="6" />
<input name="rate_location" type="radio" class="star" value="7" />
<input name="rate_location" type="radio" class="star" value="8" />
<input name="rate_location" type="radio" class="star" value="9" />
<input name="rate_location" type="radio" class="star" value="10" />
</li>
<li><strong>Cleanliness</strong><br/><input name="rate_cleanliness" type="radio" class="star" value="1" />
<input name="rate_cleanliness" type="radio" class="star" value="2" />
<input name="rate_cleanliness" type="radio" class="star" value="3" />
<input name="rate_cleanliness" type="radio" class="star" value="4" />
<input name="rate_cleanliness" type="radio" class="star" value="5" />
<input name="rate_cleanliness" type="radio" class="star" value="6" />
<input name="rate_cleanliness" type="radio" class="star" value="7" />
<input name="rate_cleanliness" type="radio" class="star" value="8" />
<input name="rate_cleanliness" type="radio" class="star" value="9" />
<input name="rate_cleanliness" type="radio" class="star" value="10" />
</li>
<li><strong>Rooms</strong><br/><input name="rate_rooms" type="radio" class="star" value="1" />
<input name="rate_rooms" type="radio" class="star" value="2" />
<input name="rate_rooms" type="radio" class="star" value="3" />
<input name="rate_rooms" type="radio" class="star" value="4" />
<input name="rate_rooms" type="radio" class="star" value="5" />
<input name="rate_rooms" type="radio" class="star" value="6" />
<input name="rate_rooms" type="radio" class="star" value="7" />
<input name="rate_rooms" type="radio" class="star" value="8" />
<input name="rate_rooms" type="radio" class="star" value="9" />
<input name="rate_rooms" type="radio" class="star" value="10" />
</li>
<li><strong>Value for money</strong><br/><input name="rate_value_for_money" type="radio" class="star" value="1" />
<input name="rate_value_for_money" type="radio" class="star" value="2" />
<input name="rate_value_for_money" type="radio" class="star" value="3" />
<input name="rate_value_for_money" type="radio" class="star" value="4" />
<input name="rate_value_for_money" type="radio" class="star" value="5" />
<input name="rate_value_for_money" type="radio" class="star" value="6" />
<input name="rate_value_for_money" type="radio" class="star" value="7" />
<input name="rate_value_for_money" type="radio" class="star" value="8" />
<input name="rate_value_for_money" type="radio" class="star" value="9" />
<input name="rate_value_for_money" type="radio" class="star" value="10" />
</li>
</ul>
<div style="clear:both"></div>

              </div>               

            <div class="rating_header_orage">* Review Title<br /><span>(Example: Excellent location with supreb food.)</span>
 <br />
            <input type="text" name="review_title" id="review_title" class="Extra_textbox" />
            </div>
            
            <div class="rating_header_orage">* Your Review <br /><span>(What did you like or dislike about this place and why?)</span><br />
            <textarea name="review_detail" rows="5" cols="45" class="Extra_textbox"></textarea>
            </div>
            <div class="rating_header_orage">&nbsp;&nbsp;Your Name<br />  
              <input type="text" name="cus_name" value="" class="Extra_textbox"/>
              <br /></div>
            
            <div class="rating_header_orage">&nbsp;&nbsp;Where are you from?  <br /><span>&nbsp;&nbsp;(Example: London, UK)</span><br />
            <input type="text" name="cus_from" value="" class=" Extra_textbox"/>
            </div>
            
            <div id="rating_buttom"> 
            <input type="hidden" name="product" value="624"/><input type="hidden" name="category" value="29"/>
            <br /><asp:Button ID="btnREviewSave" runat="server" OnClick="btnREviewSave_OnClick" Text="Add Review Now" Width="200px" EnableTheming="false" CssClass="Extra_Button" /> </div>
            
            </div>
            
            <!--review_box-->
            
</asp:Content>

