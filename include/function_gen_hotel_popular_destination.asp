<%
FUNCTION function_gen_hotel_popular_destination(intDestination,intLocation,intProduct,intType)

	SELECT CASE intType
		Case 1 '### Normal Type ###
%>
<table width="163" border="0" cellspacing="0" cellpadding="0" class="f11" background="/images/b_blue_155.gif">
              <tr> 
                <td height="24" align="center"><b><font color="346494"><span class="f11">Popular Destinations</span></font></b> </td>
              </tr>
            </table>
<table width="163" border="0" cellspacing="1" cellpadding="3" bgcolor="97BFEC" id="hotels_list">
              <tr> 
                <td bgcolor="#FFFFFF">
				<p>
				<a href="/bangkok-hotels.asp"><font color="#fe5f10"><b>Bangkok Hotels</b></font></a><br>
                  <li><a  href="/bangkok-airport-hotels.asp">Airport Hotels</a></li>
				  <li><a  href="/bangkok-sukhumvit-hotels.asp">Sukhumvit Hotels</a></li>
				  <li><a  href="/bangkok-silom-hotels.asp">Silom Hotels</a></li>
				  <li><a  href="/bangkok-siam-hotels.asp">Siam Hotels</a></li>
				  </p>
				  <p>
				<a href="/phuket-hotels.asp"><font color="#fe5f10"><b>Phuket Hotels</b></font></a><br>
                  <li><a  href="/phuket-patong-beach-hotels.asp">Patong Beach Hotels</a></li>
				  <li><a  href="/phuket-kata-beach-hotels.asp">Kata Beach Hotels</a></li>
				  <li><a  href="/phuket-karon-beach-hotels.asp">Karon Beach Hotels</a></li>
				  </p>
				  <p>
				<a href="/pattaya-hotels.asp"><font color="#fe5f10"><b>Pattaya Hotels</b></font></a><br>
                  <li><a  href="/pattaya-city-hotels.asp">Pattaya City Hotels</a></li>
				  <li><a  href="/pattaya-north-pattaya-hotels.asp">North Pattaya Hotels</a></li>
				  <li><a  href="/pattaya-sout-pattaya-hotels.asp">South Pattaya Hotels</a></li>
				  <li><a  href="/pattaya-jomtien-pattaya-hotels.asp">Jomtien Pattaya Hotels</a></li>
				 </p>
				 <p>
				<a href="/chiang-mai-hotels.asp"><font color="#fe5f10"><b>Chiang Mai Hotels</b></font></a><br>
                  <li><a  href="/chiang-mai-town-hotels.asp">Chiang Mai City Hotels</a></li>
				  </p>
				  <p>
				<a href="/koh-samui-hotels.asp"><font color="#fe5f10"><b>Koh Samui Hotels</b></font></a><br>
                  <li><a  href="/koh-samui-chaweng-hotels.asp">Chaweng Beach Hotels</a></li>
				  <li><a  href="/koh-samui-bo-phut-hotels.asp">Bao Phut Hotels</a></li>
				  </p>
				  <p>
				<a href="/krabi-hotels.asp"><font color="#fe5f10"><b>Krabi Hotels</b></font></a><br>
                  <li><a  href="/krabi-ao-nang-hotels.asp">Ao Nang Hotels</a></li>
				  <li><a  href="/krabi-city-hotels.asp">Krabi Town Hotels</a></li>
				  </p>
				  <p>
				<a href="/koh-chang-hotels.asp"><font color="#fe5f10"><b>Koh Chang Hotels</b></font></a><br>
                  <li><a  href="/koh-chang-white-sand-beach.asp">White Sand Beach Hotels</a></li>
				  </p>
				  <p>
				<a href="/chiang-rai-hotels.asp"><font color="#fe5f10"><b>Chiang Rai Hotels</b></font></a><br>
                  <li><a  href="/chiang-rai-city-hotels.asp">Chiang Rai City</a></li>
				  </p>
				  <p>
				<a href="/mae-hong-son-hotels.asp"><font color="#fe5f10"><b>Mae Hong Son Hotels</b></font></a><br>
                  <li><a  href="/mae-hong-son-pai-hotels.asp">Pai</a></li>
				  </p>
				  <p>
				<a href="/hua-hin-hotels.asp"><font color="#fe5f10"><b>Hua Hin Hotels</b></font></a><br>
                  <li><a  href="/hua-hin-city-hotels.asp">Hua Hin</a></li>
				  </p>
				  <p>
				<a href="/cha-am-hotels.asp"><font color="#fe5f10"><b>Cha Am Hotels</b></font></a><br>
                  <li><a  href="/cha-am-city-hotels.asp">Cha Am</a></li>
				  </p>
				  <p>
				<a href="/phang-nga-hotels.asp"><font color="#fe5f10"><b>Phang Nga Hotels</b></font></a><br>
                  <li><a  href="/phang-nga-khao-lak-hotels.asp">Khao Lak</a></li>
				  </p>
				  <p>
				<a href="/koh-samet-hotels.asp"><font color="#fe5f10"><b>Koh Samet Hotels</b></font></a><br>
                  <li><a  href="/koh-samet-ao-prao-beach-hotels.asp">Ao Prao Beach</a></li>
				  <li><a  href="/koh-samet-vongduean-beach-hotels.asp">Vongduean Beach</a></li>
				  </p>
				  <p>
				<a href="/rayong-hotels.asp"><font color="#fe5f10"><b>Rayong Hotels</b></font></a><br>
                  <li><a  href="/rayong-city-hotels.asp">Rayong City</a></li>
				  </p>
				  <p>
				<a href="/kanchanaburi-hotels.asp"><font color="#fe5f10"><b>Kanchanaburi Hotels</b></font></a><br>
                  <li><a  href="/kanchanaburi-kwai-yai-hotels.asp">Kwai Yai</a></li>
				  </p>
				  <p>
				<a href="/khao-yai-hotels.asp"><font color="#fe5f10"><b>Khao Yai Hotels</b></font></a><br>
                  <li><a  href="/khao-yai-khao-yai-hotels.asp">Khao Yai</a></li>
				  </p>
				  <p>
				<a href="/prachuap-khiri-khan-hotels.asp"><font color="#fe5f10"><b>Prachuap Khiri khan Hotels</b></font></a><br>
                  <li><a  href="/prachuap-khiri-khan-bankrut-beach-hotels.asp">BanKrut Beach</a></li>
				  </p>
				  <p>
				<a href="/trang-hotels.asp"><font color="#fe5f10"><b>Trang Hotels</b></font></a><br>
                  <li><a  href="/trang-chang-lang-beach-hotels.asp">Chang Lang Beach</a></li>
				  <li><a  href="/trang-koh-mook-hotels.asp">Koh Mook</a></li>
				  </p>
                </td>
              </tr>
            </table>
<%
		Case 2 '### HomePage ###
		Case 3
	END SELECT
END FUNCTION
%>