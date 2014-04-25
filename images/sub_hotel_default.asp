<%OPTION EXPLICIT%>
<!--#include file="include/constant.asp"-->
<!--#include file="include/function_gen_dropdown_date.asp"-->
<!--#include file="include/function_gen_drop_down_price.asp"-->
<!--#include file="include/function_gen_dropdown_location.asp"-->
<!--#include file="include/function_gen_hotel_popular_destination.asp"-->
<!--#include file="include/function_display_error.asp"-->
<!--#include file="include/function_gen_search_quick.asp"-->
<!--#include file="include/sub_partner.asp"-->
<!--#include file="include/function_cart_display_mini.asp"-->

<!--#include file="include/function_cart_get_id.asp"-->
<!--#include file="include/function_generate_hotel_link.asp"-->
<!--#include file="include/function_generate_golf_link.asp"-->
<!--#include file="include/function_display_char.asp"-->
<!--#include file="include/function_date.asp"-->
<!--#include file="include/function_cart_price_product.asp"-->
<!--#include virtual="/include/function_gen_room_price_average_promotion.asp"-->
<!--#include virtual="/include/function_gen_room_price_average.asp"-->
<!--#include virtual="/include/function_gen_room_price.asp"-->
<!--#include virtual="/include/function_get_price_promotion.asp"-->
<!--#include virtual="/include/function_array_check.asp"-->
<!--#include virtual="/include/functoin_date_check.asp"-->
<!--#include virtual="/include/function_date_sql.asp"-->
<!--#include virtual="/include/function_gen_price_sightseeing.asp"-->
<!--#include virtual="/include/function_generate_sightseeing_link.asp"-->
<!--#include virtual="/include/function_generate_water_activity_link.asp"--> 
<!--#include virtual="/include/function_box_why_choose.asp"-->
<!--#include virtual="/include/function_generate_show_event_link.asp"-->

<!--#include virtual="/include/function_List_Box_Selected.asp"-->

<!--#include virtual="/include/function_display_hotel_of_week.asp"-->

<!--#include virtual="/include/sub_display_logo.asp"-->

<!--#include virtual="/include/function_gen_review_last.asp"-->
<!--#include virtual="/include/function_display_text.asp"-->
<!--#include virtual="/include/function_generate_star_rate.asp"-->
<!--#include virtual="/include/function_gen_forum_last.asp"-->

<!--#include virtual="/include/function_display_service.asp"-->
<!--#include virtual="/include/function_display_action.asp"-->

<!--#include virtual="/include/function_display_hotel_hot.asp"-->

<!--#include virtual="/include/function_display_side_logo.asp"-->


<%
SUB sub_hotel_default(strKeyword,intPageType)
Dim strDateCheckIn
Dim strDateCheckOut
Dim intCount
Dim strDestination
Dim strLocation
Dim intDestination
Dim intLocation
Dim intDayCheckIn
Dim intMonthCheckIn
Dim intYearCheckIn
Dim intDayCheckOut
Dim intMonthCheckOut
Dim intYearCheckOut
Dim intPriceMin
Dim intPriceMax
Dim strSort
Dim DateCheckIn
Dim DateCheckOut
Dim strPriceMin
Dim strPriceMax
Dim strError
Dim strKeyMeta
Dim strKeyTitle
Dim strKeyH1
Dim strKeyAltLogo
Dim strKeyAltSearch
Dim intCountStart
Dim intCountEnd
Dim intRow
Dim intRowCount
Dim intColumNumber
Dim intColum
Dim intAllHotelCount

intColum = 6

Call connOpen()
Call sub_partner()

SELECT CASE intPageType
	Case 1 'Main Default Page
		strKeyMeta = "<meta name=""Description"" CONTENT=""Thailand Hotels discount up to 75% off from rack rates including hotels in Bangkok,Pattaya,Phuket,Koh Samui,Samet,Chiang Mai"">" & VbCrlf
		strKeyMeta = strKeyMeta & "<meta name=""Keywords"" CONTENT=""thailand hotels bangkok pattaya krabi phuket chiang mai koh samui hotel resort"">" & VbCrlf
		strKeyMeta = strKeyMeta & "<meta name=""classification"" content=""Thailand Hotels: Travel and Tourism"">" & VbCrlf
		strKeyTitle = "Thailand Hotels Bangkok,Phuket Hotels Discount Hotel in Thailand"
		strKeyH1 = "Thailand Hotels Reservations Hotels Thailand"
		strKeyAltLogo = "Thailand Hotels"
		strKeyAltSearch = "Thailand Hotels Search"
	Case 2 'Keyword Page
		strKeyMeta = "<meta name=""Description"" CONTENT="""&strKeyword&" discount up to 75% off from rack rates including hotels in Bangkok,Pattaya,Phuket,Koh Samui,Samet,Chiang Mai"">" & VbCrlf
		strKeyMeta = strKeyMeta & "<meta name=""Keywords"" CONTENT="""&strKeyword&"bangkok pattaya krabi phuket chiang mai koh samui hotel resort"">" & VbCrlf
		strKeyMeta = strKeyMeta & "<meta name=""classification"" content="""&strKeyword&": Travel and Tourism"">" & VbCrlf
		strKeyTitle = strKeyword & " Bangkok,Phuket Hotels Discount Hotel in Thailand"
		strKeyH1 = strKeyword
		strKeyAltLogo = strKeyword
		strKeyAltSearch = strKeyword & " Search"
END SELECT

intDestination = Request("destination")
intLocation = Request("location")
strSort = Request("sort")
intPriceMin = Request("start_price")
intPriceMax = Request("end_price")
intDayCheckin = Request("ch_in_date")
intMonthCheckin = Request("ch_in_month")
intYearCheckin = Request("ch_in_year")
intDayCheckout = Request("ch_out_date")
intMonthCheckout = Request("ch_out_month")
intYearCheckout = Request("ch_out_year")

'### Set Date Check In and Check Out ###
IF intDayCheckIn="" Then
	DateCheckIn = DateAdd("d",17,Date)
	DateCheckOut = DateAdd("d",19,Date)
	
	intDayCheckIn = Day(DateCheckIn)
	intMonthCheckin = Month(DateCheckIn)
	intYearCheckin = Year(DateCheckIn)
	intDayCheckout = Day(DateCheckOut)
	intMonthCheckout = Month(DateCheckOut)
	intYearCheckout = Year(DateCheckOut)
End IF
'### Set Date Check In and Check Out ###

'strDestination = function_gen_dropdown_location(intDestination,intLocation,"destination","OnChange=""ChgCate(this.options[this.selectedIndex].value)""",1)
'strLocation = function_gen_dropdown_location(intDestination,intLocation,"location","",2)
strDateCheckIn = function_gen_dropdown_date(intDayCheckIn,intMonthCheckIn,intYearCheckIn,"ch_in_date","ch_in_month","ch_in_year",1)
strDateCheckOut = function_gen_dropdown_date(intDayCheckOut,intMonthCheckOut,intYearCheckOut,"ch_out_date","ch_out_month","ch_out_year",1)
strPriceMin = function_gen_drop_down_price(intPriceMin,"start_price",1)
strPriceMax = function_gen_drop_down_price(intPriceMax,"end_price",2)

IF Request("error") <> "" Then
	strError = "<br><font color= ""#FF0000"">" & function_display_error(Request("error")) & "</font>"
End IF
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
<title><%=strKeyTitle %></title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<%=strKeyMeta%>
<meta name="verify-v1" content="xj406QbI7IuqfX1rHuIF/f8njlBZKS5URYeZa3SK8U0=" > 
<link rel="stylesheet" href="css/css.css" type="text/css">
<link rel="stylesheet" href="css/optimize.css" type="text/css">
<link rel="stylesheet" href="css/autotab.css" type="text/css">
<script language="JavaScript" src="/java/destination.js" type="text/javascript"></script>
<script language="JavaScript" src="/java/livechat.js" type="text/javascript"></script>
<script language="JavaScript" src="/java/popup.js" type="text/javascript"></script>
<script language="javascript" src="/java/prototype-1.6.0.2.js"></script>
<script language="JavaScript" src="/java/autotab.js" type="text/javascript"></script>

<script language="javascript" src="/java/ajax_utility.js"></script>
<style type="text/css">
#logo_home{
background:#FFFFFF;
}
#logo_home a:hover{
background:#FFFFFF;
}
</style>
</head>
<body background="images/bg_main.jpg" bgcolor="#FFFFFF" marginheight="0" leftmargin="0" topmargin="0" marginwidth="0">
<table width="780" border="0" cellspacing="0" cellpadding="0" class="f11" align="center">
  <tr>
    <td>
      <table width="780" border="0" cellspacing="0" cellpadding="0" height="82" bgcolor="#FFFFFF">
	  <tr><td colspan="3" height="10"> </td></tr>
        <tr> 
          <td width="12">&nbsp;</td>
          <td  bgcolor="#FFFFFF"> 
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="f11">
              <tr> 
                <td height="57" valign="middle"> 
                  <table width="100%" border="0" cellspacing="0" cellpadding="0" class="f11">
                    <tr> 
                      <td>
						<%Call sub_display_logo(Request.Cookies("site_id"),strKeyH1,strKeyAltLogo,1)%>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr> 
                <td height="25" valign="bottom" align="center"> 
                  <table border="0" cellspacing="0" cellpadding="0">
                    <tr valign="bottom" class="f13">
                      <td height="24" width="9"><img src="./images/b_blue_L.gif" width="9" height="24" align="absmiddle"></td>
                      <td width="50" background="./images/bg_b_blue.gif" valign="middle" align="center"><a href="/" title="Thailand Hotels Home"><font color="346494"><b>Home</b></font></a></td>
                      <td width="9"><img src="./images/spacer_b_o.gif" width="9" height="24" align="absmiddle"></td>
                      <td width="50" background="./images/bg_b_orange.gif" valign="middle" align="center"><a href="/thailand-hotels.asp" title="Hotels in Thailand"><font color="FE5400"><b>Hotels</b></font></a></td>
                      <td width="9"><img src="./images/spacer_o_o.gif" width="9" height="24" align="absmiddle"></td>
                      <td width="60" background="./images/bg_b_orange.gif" valign="middle" align="center"><a href="/thailand-day-trips.asp" title="Thailand Travel, Package Tours, Day Trips, Sightseeing"><font color="FE5400"><b>Day Trips</b></font></a></td>
					  <td width="9"><img src="./images/spacer_o_o.gif" width="9" height="24" align="absmiddle"></td>
                      <td width="100" background="./images/bg_b_orange.gif" valign="middle" align="center"><a href="/thailand-water-activity.asp" title="Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing"><font color="FE5400"><b>Water Activities</b></font></a></td>
                      <td width="9"><img src="./images/spacer_o_o.gif" width="9" height="24" align="absmiddle"></td>
                      <td width="80" background="./images/bg_b_orange.gif" valign="middle" align="center"><a href="/thailand-golf-courses.asp" title="Thailand Golf Courses"><font color="FE5400"><b>Golf Courses</b></font></a></td>
                      <td width="9"><img src="./images/spacer_o_o.gif" width="9" height="24" align="absmiddle"></td>
                      <td background="./images/bg_b_orange.gif" valign="middle" align="center"><a href="/thailand-show-event.asp" title="Thailand Shows and Events"><font color="FE5400"><b>Show & Event</b></font></a></td>
                      <td width="9"><img src="./images/spacer_o_o.gif" width="9" height="24" align="absmiddle"></td>
                      <td background="./images/bg_b_orange.gif" valign="middle" align="center"><a href="/thailand-health-check-up.asp" title="Hospitals Medical and Health Check Up in Thailand "><font color="FE5400"><b>Health</b></font></a></td>
                      <td width="9"><img src="./images/spacer_o_o.gif" width="9" height="24" align="absmiddle"></td>
                      <td width="70"  background="./images/bg_b_orange.gif" valign="middle" align="center"><a href="/discount_thaialand_hotels.asp" title="Hot Promotion Hotels in Thailand"><font color="FE5400"><b>Promotions</b></font></a> </td>
					  <td width="9"><img src="./images/spacer_o_g.gif" width="9" height="23" align="absmiddle"></td>
                      <td width="16" background="./images/bg_b_green01.gif" valign="middle" align="center"><a href="/cart_view.asp" title="View Your Booking List"> <img src="./images/ico_cart_orange.gif" height="14" border="0"></a></td>
					  <td align="center" valign="middle" background="./images/bg_b_green01.gif"><a href="/cart_view.asp" title="View Your Booking List"><font color="#fe5400"><b>View Booking List</b></font></a></td>
                      <td width="9" height="23"><img src="./images/bg_green_r.gif" width="6" height="23"></td>
                    </tr>
                </table>
                </td>
              </tr>
            </table>
          </td>
          <td width="12">&nbsp;</td>
        </tr>
      </table>
      <table width="780" border="0" cellspacing="0" cellpadding="0" class="f11">
        <tr> 
          <td height="24" background="images/bg_bar.gif" align="center">
	<div id="hotelmenu"></div>
		<script language="javascript">
			ajax_utility('/include/ajax/ajaxDisplayMenu.asp?intType=1','hotelmenu','<img src=/images/vista.gif>');
		</script>
		  </td>
        </tr>
      </table>
      <table width="780" border="0" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF">
        <tr> 
          <td width="48%" valign="bottom"> 
            <table border="0" cellspacing="0" cellpadding="0" class="f11">
              <tr> 
                <td width="24"><img src="images/b_search.gif" width="24" height="217" align="absmiddle" alt="<%=strKeyAltSearch%>"></td>
                <td background="images/bg_box_search.gif" width="348" align="center" valign="middle"> 
				<%IF strError<>"" Then%>
                  <table width="100%" border="0" cellspacing="0" cellpadding="5" class="f11">
                    <tr>
                      <td align="center"><b><font color="#FF0000"><%=Replace(strError,"<br>","")%></font></b></td>
                    </tr>
                  </table>
				  <%End IF%>
                  <table cellspacing=1 cellpadding=2 class="f11">
				    <form action="thailand-hotels-search.asp" method="post" name="form_search">
					  <input type="hidden" name="sort" value="featureDESC">
					  <input type="hidden" name="page" value="1">
                      <tr>
                        <td width="10" align="center"><img src="images/i_arrow2.gif" width="15" height="11"></td>
                        <td width="100"> 
                          <div align=left><font color="003663"><b>Destination</b></font></div>                        </td>
                        <td><div align=left><%=function_list_box_selected("destination_id | title_en ","tbl_destination where destination_id NOT IN (56,64,59)" , "title_en" , "","destination","OnChange=""ChgCate(this.options[this.selectedIndex].value)""",intDestination)%></div></td>
                      </tr>
                      <tr>
                        <td align="center"><img src="images/i_arrow2.gif" width="15" height="11"></td>
                        <td> 
                          <div align=left><font color="003663">Location</font></div>                        </td>
                        <td><div align=left>
						<%if trim(intDestination)<>"" then%>
<%=function_List_Box_Selected("location_id | title_en ","tbl_location where destination_id="&intDestination , "title_en" , "","location","",intLocation)%>
<%else%>
<%=function_List_Box_Selected("location_id | title_en ","tbl_location where destination_id=0" , "title_en" , "","location","",intLocation)%>	
<%end if%>
						</div></td>
                      </tr>
                      <tr>
                        <td align="center"><img src="images/i_arrow2.gif" width="15" height="11"></td>
                        <td> 
                          <div align=left><font color="FE5F10"><b>Check in</b></font>                          </div>                        </td>
                        <td><div align=left><%=strDateCheckIn%></div></td>
                      </tr>
                      <tr>
                        <td align="center"><img src="images/i_arrow2.gif" width="15" height="11"></td>
                        <td> 
                          <div align=left><font color="FE5F10"><b>Check out</b></font>                          </div>                        </td>
                        <td><div align=left><%=strDateCheckOut%></div></td>
                      </tr>
                      <tr>
                        <td align="center"><img src="images/i_arrow2.gif" width="15" height="11"></td>
                        <td> 
                          <div align=left><font color="003663">Minimum Price</font>                          </div>                        </td>
                        <td><div align=left> <%=strPriceMin%></div></td>
                      </tr>
                      <tr>
                        <td align="center"><img src="images/i_arrow2.gif" width="15" height="11"></td>
                        <td> 
                          <div align=left><font color="003663">Maximum Price</font></div>                        </td>
                        <td><div align=left><%=strPriceMax%> </div></td>
                      </tr>
                      <tr> 
                        <td colspan=3><div align=center><input type="image" src="/images/but_search_hotels.gif"></div></td>
                      </tr>
                    </form>
                  </table>                </td>
              </tr>
          </table>          </td>
          <td width="52%" valign="bottom">


<!--***********************************************************************************************************-->	 
 <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td valign="top">
	<div id="hotelwhy"></div>
		<script language="javascript">
			ajax_utility('/include/ajax/ajaxDisplayWhyChoose.asp?intPercent=405&intType=2','hotelwhy','<img src=/images/vista.gif>');
		</script>
	<%'=function_box_why_choose("405",2)%>
    </td>
  </tr>
</table>
<!--***********************************************************************************************************-->		  
</td>
        </tr>
      </table>
      <table width="780" border="0" cellspacing="0" cellpadding="0" class="f11" bgcolor="#FFFFFF">
        <tr> 
          <td>&nbsp;</td>
        </tr>
      </table>

	  <table width="780" border="0" cellspacing="0" cellpadding="0" class="f11" bgcolor="#FFFFFF">
        <tr> 
          <td>&nbsp;</td>
        </tr>
      </table>
      <table width="780" border="0" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF" height="274" class="f11">
        <tr> 
          <td valign="top" width="150">
		    <!--<p align="center">
			<a href="thailand-airport-news.asp"><img src="images/airport.gif" border="0" style="padding-left:13px;"/></a>	</p>-->
		  	<p align="center">
			<a href="thailand-spa.asp"><img src="images/home_spa.gif" width="141" height="93" border="0" style="padding-left:13px;"/></a>			</p>
		  	<p align="center">
			<a href="javascript:popup('charity.asp',550,480)">
			<img src="images/charity.gif" alt ="Hotels2thailand Charity Campaigns"  border="0" style="padding-left:13px;"/>
			</a>
			</p> 
			<p><%=function_livechat(1)%></p>
			<p><%=function_gen_search_quick("",1)%></p>
			<p><%'=function_cart_display_mini()%></p>
			<!--p><%'=function_gen_hotel_popular_destination("","","",1)%></p-->		  

<%=function_display_side_logo(1)%>
<a href="javascript:popup('http://www.dbd.go.th/edirectory/paper/?id=0105546113072&st=1',600,500)"><img src="images/dbd_ban.gif" width="130" height="68" border="0"></a>


</td>
          <td width="5px">&nbsp;          </td>
          <td valign="top"  width="100%">
                          	<div id="hotelhot"></div>
                    <script language="javascript">
                    ajax_utility('/include/ajax/ajaxDisplayHotelHot.asp?desId=0&LocID=0&intType=1','hotelhot','<img src=/images/vista.gif>');
					</script>
<%'=function_display_hotel_hot(0,0,1)%>
<br />
<%=function_gen_hotel_popular_destination(0,0,0,2)%><br />

<%=function_display_service(1)%>
<br />
<%=function_display_action(2)%>
<br />          </td>
        </tr>

        <tr>
          <td colspan="3" align="left" valign="top">&nbsp;</td>
        </tr>
      </table>
      <table width="780" border="0" cellspacing="0" cellpadding="0" class="f11" bgcolor="97BFEC">
        <tr> 
          <td height="1"><img src="images/spacer.gif" width="1" height="1"></td>
        </tr>
      </table>
      <table width="780" border="0" cellspacing="0" cellpadding="0" class="f11">
        <tr> 
          <td height="24" background="./images/bg_bar.gif" align="center"><font color="346494">Copyright 
            &copy; 1996-<%=Year(Date)%> Hotels 2 Thailand .com. All rights reserved.</font></td>
        </tr>
      </table>
      <table width="780" border="0" cellspacing="0" cellpadding="0" class="f11">
        <tr> 
          <td align="center">
          <a href="/thailand-hotels.asp">Hotels</a> | 
          <a href="/thailand-day-trips.asp">Day Trips</a> | 
          <a href="/thailand-water-activity.asp">Water Activities</a> | 
          <a href="/thailand-golf-courses.asp">Golf Courses</a> | 
          <a href="/thailand-show-event.asp">Show & Event</a> |  
          <a href="/thailand-health-check-up.asp">Medical Service</a> | 
          <a href="/discount_thaialand_hotels.asp">Promotions</a> | 
          <a href="/thailand-spa.asp">Spa</a> | 
          <a href="http://www.24confirm.com" target="_blank">Hotels Price Compare</a>
 		  </td>
        </tr>
      </table>
    </td>
  </tr>
</table>

</html>
<%
Call connClose()
End Sub
%>