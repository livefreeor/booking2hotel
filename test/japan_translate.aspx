<%@ Page Language="C#" AutoEventWireup="true" CodeFile="japan_translate.aspx.cs" Inherits="test_japan_translate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

     <link href="../css/extranet/Master_style_extra.css" type="text/css" rel="stylesheet" />
    <link href="../css/extranet/extranet_style_core.css" type="text/css" rel="stylesheet" />

    <script language="javascript" type="text/javascript" src="/scripts/jquery-1.6.1.js"></script>
    <script language="javascript" type="text/javascript" src="/scripts/extranet/jquery.simplemodal.1.4.4.min.js"></script>


<script language="javascript" type="text/javascript" src="/scripts/extranet/darkman_utility_extranet.js?ver=001"></script>

<script language="javascript" type="text/javascript" src="/scripts/extranet/extranetmain.js"></script>

<script language="javascript" type="text/javascript" src="/scripts/darkman_datepicker.js"></script>

<link type="text/css" href="/css/extranet/dashboard.css" rel="stylesheet" />



<script type="text/javascript" language="javascript">



    $(document).ready(function () {

        //alert(getCookieASPNET('SessionKey'));
        $.getJSON('/extranet/ajax/translate.html', function (result) {


            var obj = result.word;


            $(".translate").each(function () {

                for (i = 0; i < obj.length; i++) {

                    var str = $(this).html();

                    var regex = new RegExp(obj[i][0]);

                    var result = regex.test(str);
                    if (result) {
                        $(this).html(obj[i][1]);
                        break;
                    }

                }

            });

        });

        $("#btnCheck").click(function () {
            $.getJSON('/extranet/ajax/translate.html', function (result) {


                var obj = result.word;


                $(".translate").each(function () {

                    for (i = 0; i < obj.length; i++) {

                        var str = $(this).html();


                        var regex = new RegExp(obj[i][0]);

                        var result = regex.test(str);
                        if (result) {
                            $(this).html(obj[i][1]);
                            break;
                        }

                    }


                });

            });

            return false;
        });

        //alert($(".translate").length);

    });

    function showcookies() {
        alert(getCookie('SessionKey'));
    }

    function changeCookie() {
        alert("chang to : LogKey=4955&LangActive=2&ProductActive=3449&SupplierActive=10");

        
        setCookie("SessionKey", "LogKey=4955&LangActive=2&ProductActive=3449&SupplierActive=10", 30, "/");
    }

    var spinnerVisible = false;
    function showProgress() {
        if (!spinnerVisible) {
            $("div#spinner").fadeIn("fast");
            spinnerVisible = true;

            //Fade in Background
            $('body').prepend('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.
            $('#fade').css({ 'opacity': '0.8', 'filter': 'alpha(opacity=80)' }).fadeIn(); //Fade in the fade layer - .css({'filter' : 'alpha(opacity=80)'}) is used to fix the IE Bug on fading transparencies
        }
    };
    function hideProgress() {
        if (spinnerVisible) {
            var spinner = $("div#spinner");
            spinner.stop();
            spinner.fadeOut("fast");
            spinnerVisible = false;

            $('#fade').fadeOut(function () {
                $('#fade').remove();  //fade them both out
            });
        }
    };


</script>

    <style type="text/css" >
        #simplemodal-container a.modalCloseImg {
	background:url(/images/x.png) no-repeat; /* adjust url as required */
	width:25px;
	height:29px;
	display:inline;
	z-index:3200;
	position:absolute;
	top:-15px;
	right:-18px;
	cursor:pointer;
}
        
        div#spinner
{
    display: none;
    width:100px;
    height: 100px;
    position: fixed;
    top: 50%;
    left: 50%;
    background:url(/images/progress_b.gif) no-repeat center #fff;
    text-align:center;
    padding:10px;
    font:normal 16px Tahoma, Geneva, sans-serif;
    border:1px solid #666;
    margin-left: -50px;
    margin-top: -50px;
    z-index:9999;
    overflow: auto;
}

        #fade { /*--Transparent background layer--*/
display: none; /*--hidden by default--*/
background: #000;
position: fixed; left: 0; top: 0;
width: 100%; height: 100%;
opacity: .80;
z-index: 9998;
}

img.btn_close {
float: right;
margin: 0px 0px 0 0;
}
/*--Making IE6 Understand Fixed Positioning--*/
*html #fade {
position: absolute;
}
*html .popup_block {
position: absolute;
}


    </style>
    <!--[if lt IE 7]>
<style type='text/css'>
	#simplemodal-container a.modalCloseImg {
		background:none;
		right:-14px;
		width:22px;
		height:26px;
		filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(
			src='img/x.png', sizingMethod='scale'
		);
	}
</style>
<![endif]-->
</head>
<body>
    <form id="form1" runat="server">
     <div id="spinner">
        Loading...
    </div>
    <input type="button" value="Go go"  onclick="showProgress();"/>

       <input type="button" value="Client Show Cookies" onclick="showcookies();" />
        <input type="button" value="Chang Cookies Cookies" onclick="changeCookie();" />
    <asp:Button ID="btnShowCookikie" OnClick="btnShowCookikie_Click"  runat="server" Text="Check ServerSide Cookies" />
<center>
    <span id="result"></span>
<div class="main">

    <div class="header">

    <img id="Image1" class="logo" src="../images_extra/logo_bookingengine.png" />

    

    <div id="userStaffLoginBox_panelStatusBox">

	

<div class="StatusBox_extra">

<table>

 <tr>

  <td>

  Hello <span id="userStaffLoginBox_lblStaffName">Mr.Darkman ^_^</span>

  &nbsp;&nbsp;|&nbsp;&nbsp;

  </td>

   

    <td><a id="userStaffLoginBox_linkProfile" href="../admin/staff/staffmanage.aspx?sid=5">Profile</a>  &nbsp;&nbsp;|&nbsp;&nbsp;</td>

     

      <td><a id="userStaffLoginBox_hlExtraNetlogout" href="../admin/staff/ajax_staff_logout.aspx">Log Out</a></td>

 </tr>

</table>



</div>



</div>

    

         <div class="menutop">

            

         </div>

         

         

     </div>

    <div style=" clear:both;"></div>

    

    <div id="paneltxt">

	

        <table  cellpadding="0" cellspacing="0" width="100%">

          <tr>

           <td style="text-align:right;" class="hotelschange">Current Product Active: &nbsp;</td>

           <td style="text-align:left;">

           <span id="lblProductTitle" class="txtProductTitle">BlueHouse Resort</span>

           </td>

          </tr>

        </table>

    

</div>

    

   

    

   

    <div style="clear:both"></div>

    <div class="content" style="text-align:left">

        <div class="contentLeft">

        <div class="menu_left">

        

        <ul>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnDash" href="mainextra.aspx?pid=3449&amp;supid=10">Dash Board</a></li>

        </ul>

        <ul class="menu_staff_admin">

          <panel> <label class="translate"> Manage Staff</label></panel>

           <li class="menu_staff_admin"><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnStaffList" href="staffmanage/stafflist.aspx?pid=3449&amp;supid=10"><label class="translate">Staff List</label></a></li>

           <li class="menu_staff_admin"><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnStaffAddStaff" href="staffmanage/addnewstaff.aspx?pid=3449&amp;supid=10"><label class="translate">Add New Staff</label></a></li>

        </ul>

        <ul>

        <panel>Rate Control</panel>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnHolidays" href="ratecontrol/holidays_control.aspx?pid=3449&amp;supid=10"><label class="translate">Public Holiday</label></a></li>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnRoom" href="ratecontrol/room_manage.aspx?pid=3449&amp;supid=10"><label class="translate">Product Manage</label></a></li>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lndeposit" href="ratecontrol/deposit_manage.aspx?pid=3449&amp;supid=10"><label class="translate">Deposit</label></a></li>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnloadtariff" href="ratecontrol/load_tariff.aspx?pid=3449&amp;supid=10"><label class="translate">Load Tariff</label></a></li>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnRoomControl" href="ratecontrol/condition_control.aspx?pid=3449&amp;supid=10"><label class="translate">Condition Control</label></a></li>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnRate" href="ratecontrol/rate_control.aspx?pid=3449&amp;supid=10"><label class="translate">Selling Rate Control</label></a></li>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnExtraBed" href="ratecontrol/extra_bed.aspx?pid=3449&amp;supid=10"><label class="translate">Extra Bed</label></a></li>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lntransfer" href="ratecontrol/transfer.aspx?pid=3449&amp;supid=10"><label class="translate">Transfer</label></a></li>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnGala" href="ratecontrol/gala_dinner.aspx?pid=3449&amp;supid=10"><label class="translate">Gala Dinner</label></a></li>

            <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnMeal" href="ratecontrol/meal.aspx?pid=3449&amp;supid=10"><label class="translate">Meal</label></a></li>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnmin" href="ratecontrol/minimum_night.aspx?pid=3449&amp;supid=10"><label class="translate">Minimum Night</label></a></li>

            

           

           </ul>

           <div id="panel_member">

	

           <ul>

           

            <panel>Member</panel>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnmemberlist" href="member/member_list.aspx?pid=3449&amp;supid=10"><label class="translate">Member list</label></a></li>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnmemberprice" href="member/member_price.aspx?pid=3449&amp;supid=10"><label class="translate">Member Price</label></a></li>

           

           </ul>

           

</div>



           <div id="panel_package">

	

           <ul>

           

            <panel>Package</panel>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnPackageManage" href="package/package_manage.aspx?pid=3449&amp;supid=10"><label class="translate">Create New Package</label></a></li>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnPackageList" href="package/package.aspx?pid=3449&amp;supid=10"><label class="translate">Package list</label></a></li>

           </ul>

           

</div>

           <ul>

            <panel>Manage Allotment</panel>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnAllotment" href="allotment/allotment_control.aspx?pid=3449&amp;supid=10"><label class="translate">Add New Allotment</label></a></li>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnAllotEdit" href="allotment/allotment_edit.aspx?pid=3449&amp;supid=10"><label class="translate">Edit Allotment</label></a></li>

           </ul>

           <ul>

            <panel>Manage Promotion</panel>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnPromotion" href="promotion/promotion_manage.aspx?pid=3449&amp;supid=10"><label class="translate">Create New Promotion</label></a></li>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnProList" href="promotion/promotion.aspx?pid=3449&amp;supid=10"><label class="translate">Promotion list</label></a></li>

           </ul>

           <ul>

           <panel>Manage Booking</panel>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnBookingList" href="ordercenter/booking_list.aspx?pid=3449&amp;supid=10"><label class="translate">Booking Center</label></a></li>

               <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnBookingSearch" href="ordercenter/booking_search.aspx?pid=3449&amp;supid=10" target="_blank"><label class="translate">Booking Search</label></a></li>

           </ul>

            <ul>

           <panel>Manage Review</panel>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnReview" href="review/review_list.aspx?pid=3449&amp;supid=10"><label class="translate">Review Center</label></a></li>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnReviewAdd" href="review/addreview.aspx?pid=3449&amp;supid=10"><label class="translate">Add New Review</label></a></li>

           </ul>

           <ul>

           <panel>Report</panel>

           <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" /><a id="lnBookingreport" href="report/booking_report.aspx?pid=3449&amp;supid=10"><label class="translate"> Booking Report</label></a></li>

           </ul>

             <div id="panel_Newsletter">

	

            <ul>

           <panel>Newsletter Manage</panel>

            <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" />

                <a id="hlNewsCreate" href="newsletter/sendNewsletter.aspx?pid=3449&amp;supid=10">Create New</a></li>

                <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" />

                <a id="hloutbox" href="newsletter/showNewsletterList.aspx?pid=3449&amp;supid=10&amp;temp=2">Out Box</a></li>

             <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" />

                <a id="hlSentbox" href="newsletter/showNewsletterList.aspx?pid=3449&amp;supid=10&amp;temp=1">Sent Box</a></li>

             



           

                <li><img src="http://www.booking2hotels.com/images_extra/ico-square-small.png" />

                <a id="nlDel" href="newsletter/showNewsletterList.aspx?pid=3449&amp;supid=10&amp;temp=6">Deleted</a></li>



          

           </ul>

             

</div>

         

        

        </div>

        

        </div>

        

        <div class="contentRight">

            <div class="contentRight_inner">
                
                


                <div class="headtitle" style="text-align:left">

                <p style=" float:left; margin-top:3px;"><span id="lblHeadPageTitle">Dash Board</span><br /></p>

                

                </div>

            

    <input type="hidden" name="ctl00$ContentPlaceHolder1$staff_id" id="staff_id" value="5" />

<input type="hidden" id="Current_date" />

<input type="hidden" id="Current_Chart_type" />



<div id="main_dash_graph">

<p class="btn_type"><input type="button" value="Daily" onclick="ChangChartType('daily'); return false;" class="button_Type" />

<input type="button" value="Monthly" onclick="ChangChartType('monthly'); return false;" class="button_Type" />

<input type="button" value="Yearly" onclick="ChangChartType('yearly'); return false;" class="button_Type"/></p>

    <div id="container" style="width: 910px; height: 250px; margin: 0 auto; display:none;">

    

    </div>

    <p class="Month_select"><input type="button" value="« Previous" class="button_Type" onclick="PreviousMonth(); return false;" /> 

    <input type="button" value="Next »" class="button_Type" onclick="NextMonth(); return false;" /></p>

</div>



 <div id="main_dash">

    

    <div class="dash_block">

     



    <div class="dash_block_item">

      <h2>Rate Control</h2>

      <div id="rate_control" class="dash_block_items">

       

       

      </div>

     </div>

    </div>



    <div class="dash_block"  style=" margin-left:8px;">

    



     <div class="dash_block_item" >

      <h2>Promotion</h2>

      <div id="promotion" class="dash_block_items" >

            

      </div>

     </div>

    

    </div>

    

    <div  style="clear:both"></div>

    

 </div>





            </div>

        </div>

        <div style=" clear:both;"></div>

    </div>

    <div style=" clear:both;"></div>

    <div class="footer">

        <p>Copyright © 1996-2012 Hotels 2 Thailand .com. All rights reserved.</p>

        <p>hotels2thailand.com is a registered travel agent with the Tourism Authority of Thailand. TAT License No. 11/3240</p>

        

    </div>

    <div style=" clear:both;"></div>

</div>

<div style=" clear:both;"></div>



</center>



    </form>
</body>
</html>
