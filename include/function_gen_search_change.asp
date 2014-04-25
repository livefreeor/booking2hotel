<%
FUNCTION function_gen_search_change(intDestination,intlocation,intDayCheckIn,intMonthCheckIn,intYearCheckIn,intDayCheckOut,intMonthCheckOut,intYearCheckOut,intPriceMin,intPriceMax,strSort,intType)
	
	Dim strDateCheckIn
	Dim strDateCheckOut
	Dim intCount
	Dim strDestination
	Dim dateCheckIn
	Dim dateCheckOut
	
	IF intDaycheckIn="" Then
		
		DateCheckIn = DateAdd("d",17,Date)
		DateCheckOut = DateAdd("d",19,Date)
		
		intDayCheckIn = Day(DateCheckIn)
		intMonthCheckin = Month(DateCheckIn)
		intYearCheckin = Year(DateCheckIn)
		intDayCheckout = Day(DateCheckOut)
		intMonthCheckout = Month(DateCheckOut)
		intYearCheckout = Year(DateCheckOut)
		
	End IF
	
	strDateCheckIn = function_gen_dropdown_date(intDayCheckIn,intMonthCheckIn,intYearCheckIn,"ch_in_date","ch_in_month","ch_in_year",1)
	strDateCheckOut = function_gen_dropdown_date(intDayCheckOut,intMonthCheckOut,intYearCheckOut,"ch_out_date","ch_out_month","ch_out_year",1)
	strDestination = function_gen_dropdown_location(intDestination,intLocation,"destination","OnChange=""ChgCate(this.options[this.selectedIndex].value)""",1)
	strLocation = function_gen_dropdown_location(intDestination,intLocation,"location","",2)
%>
<table width="163" border="0" cellspacing="0" cellpadding="0" class="f11" background="/images/b_blue_155.gif">
              <tr> 
                <td height="24" align="center"><b><font color="346494">Change Your Search</font></b> </td>
              </tr>
            </table>
<table width="155" border="0" cellspacing="1" cellpadding="3" class="f11" bgcolor="97BFEC">
              <tr> 
                <td bgcolor="#FFFFFF"> 
                  <table width="155" cellpadding="3"  cellspacing="0" bgcolor="#FFFFFF" class="f11" border="0">
				<form action="thailand-hotels-search.asp" method="post" name="form_search">
					<input type="hidden" name="sort" value="<%=strSort%>">
					<input type="hidden" name="page" value="1">
                      <tr> 
                        <td bgcolor="F2F8FE" class="s" height="18"> 
                          <div align="left"><font color="346494">Destination :</font></div>
                        </td>
                      </tr>
                      <tr> 
                        <td bgcolor="#FFFFFF" class="s"> 
                          <div align="left"><%=function_list_box_selected("destination_id | title_en ","tbl_destination where destination_id NOT IN (56,64)" , "title_en" , "","destination","OnChange=""ChgCate(this.options[this.selectedIndex].value)""",intDestination)%></div>
                        </td>
                      </tr>
                      <tr> 
                        <td bgcolor="FFFAEF" class="s" height="18"> 
                          <div align="left"><font color="FE5400">Location :</font></div>
                        </td>
                      </tr>
                      <tr> 
                        <td bgcolor="#FFFFFF" class="s"> 
						<div align="left">
						<%if trim(intDestination)<>"" then%>
<%=function_List_Box_Selected("location_id | title_en ","tbl_location where destination_id="&intDestination , "title_en" , "","location","",intLocation)%>
<%else%>
<%=function_List_Box_Selected("location_id | title_en ","tbl_location where destination_id=0" , "title_en" , "","location","",intLocation)%>	
<%end if%>
						</div>
                        </td>
                      </tr>
                      <tr> 
                        <td bgcolor="F2F8FE" class="s" height="18"> 
                          <div align="left"><font color="346494">Check In :</font></div>
                        </td>
                      </tr>
                      <tr> 
                        <td bgcolor="#FFFFFF" class="s"> 
                          <div align="left"><%=strDateCheckIn%></div>
                        </td>
                      </tr>
                      <tr> 
                        <td bgcolor="F2F8FE" class="s" height="18"> 
                          <div align="left"><font color="346494">Check Out :</font></div>
                        </td>
                      </tr>
                      <tr> 
                        <td bgcolor="#FFFFFF" class="s">
						<div align="left"><%=strDateCheckOut%></div>
                        </td>
                      </tr>
                      <tr> 
                        <td bgcolor="F2F8FE" class="s" height="18"> 
                          <div align="left"><font color="346494">Minimum Price 
                            :</font></div>
                        </td>
                      </tr>
                      <tr> 
                        <td bgcolor="#FFFFFF" class="s"> 
                          <div align="left"><%=function_gen_drop_down_price(intPriceMin,"start_price",1)%></div>
                        </td>
                      </tr>
                      <tr> 
                        <td bgcolor="FFFAEF" class="s" height="18"> 
                          <div align="left"><font color="FE5400">Maximum Price 
                            :</font></div>
                        </td>
                      </tr>
                      <tr> 
                        <td bgcolor="#FFFFFF" class="s"> 
                          <div align="left"><%=function_gen_drop_down_price(intPriceMax,"end_price",2)%></div>
                        </td>
                      </tr>
                      <tr> 
                        <td height="30" class="s"> 
                          <div align="center"> 
                            <input type="image" src="/images/but_search_hotels.gif" name="image" width="70" height="22">
                          </div>
                        </td>
                      </tr>
                    </form>
                  </table>
                </td>
              </tr>
            </table>


  <%
END FUNCTION
%>
</p>
