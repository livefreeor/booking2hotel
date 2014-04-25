
<!--<script language="javascript" src="/java/tab_panel.js" type="text/javascript"></script>-->
<!--<script language="javascript" src="/java/prototype-1.6.0.2.js"></script>-->
<%
Dim sqlGroup
Dim rsGroup


FUNCTION function_display_hotel_hot2(intDestinationID,intLocationID,intType)
	SELECT CASE intType
		Case 1 '### Home Page ###
	
%>
	<fieldset id="flash" style="height:528px;">
	<legend><strong>Hot Thailand Hotels</strong></legend>	
    		
    <span>
		<a href="javascript:void(0)" onclick="stopTab(this)" id="1" class="atab"><img src="/images/bkk_y.png" id="img1" border="0" /></a>
	</span>
	<span>
		<a href="javascript:void(0)" onclick="stopTab(this)" id="2" class="itab"><img src="/images/cm_b.png" id="img2" border="0" /></a>
	</span>
	<span>
		<a href="javascript:void(0)" onclick="stopTab(this)" id="3" class="itab"><img src="/images/huahin_b.png" id="img3" border="0" /></a>
	</span>
	<span>
		<a href="javascript:void(0)" onclick="stopTab(this)" id="4" class="itab"><img src="/images/kohchang_b.png" id="img4" border="0" /></a>
	</span>
    <span>
		<a href="javascript:void(0)" onclick="stopTab(this)" id="5" class="itab"><img src="/images/samui_b.png" id="img5" border="0" /></a>
	</span>

    <div id="display" style="height:433px;">
    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0" width="424" height="420">
      <param name="movie" value="http://www.hotels2thailand.com/Flash/hot flash.swf" />
      <param name="quality" value="high" />
      <embed src="http://www.hotels2thailand.com/Flash/hot flash.swf" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" width="424" height="420"></embed>
    </object>
    </div>
	
	<span>
		<a href="javascript:void(0)" onclick="stopTab(this)" id="6" class="itab"><img src="/images/krabi_b.png" id="img6" border="0" /></a>
	</span>
    <span>
		<a href="javascript:void(0)" onclick="stopTab(this)" id="7" class="itab"><img src="/images/pattaya_b.png" id="img7" border="0" /></a>
	</span>
	<span>
		<a href="javascript:void(0)" onclick="stopTab(this)" id="8" class="itab"><img src="/images/phangnga_b.png" id="img8" border="0" /></a>
	</span>
	<span>
		<a href="javascript:void(0)" onclick="stopTab(this)" id="9" class="itab"><img src="/images/phuket_b.png" id="img9" border="0" /></a>
	</span>
	<span>
		<a href="javascript:void(0)" onclick="stopTab(this)" id="10" class="itab"><img src="/images/other_b.png" id="img10" border="0" /></a>
	</span>
    </fieldset>
<%
		Case 2
		Case 3
	END SELECT

END FUNCTION
%>