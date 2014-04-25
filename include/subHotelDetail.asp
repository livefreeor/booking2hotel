<%OPTION EXPLICIT%>
<!--#include file="../include/fnroomprice.asp"-->
<!--#include file="../include/constant.asp"-->
<!--#include file="../include/fndate.asp"-->
<!--#include file="../include/currencyConstant.asp"-->
<!--#include file="../include/fnCurrencyList.asp"-->
<!--#include file="../include/SubCurrencyCheck.asp"-->
<!--#include file="../include/fnCurrencyDisplay.asp"-->
<!--#include file="../include/fnNavigator.asp"-->
<!--#include file="../include/fnFaciclities.asp"-->
<!--#include file="../include/fnFacilitiesIcon.asp"-->
<!--#include file="../include/fnGenHidden.asp"-->
<!--#include file="../include/fnconvertChr.asp"-->
<!--#include file="../include/fnStar.asp"-->
<!--#include file="../include/fnConvertSqlDate.asp"-->
<!--#include file="../include/fnRecentAdd.asp"-->
<!--#include file="../include/fnRecentShow.asp"-->
<!--#include file="../include/fnDestinationString.asp"-->

<%
Sub subHotelDetail(intProductID)
Call connOpen()
Call subCurrencyCheck()
Call fnRecentAdd(intProductID)

Dim sqlDetail
Dim recDetail
Dim strDetail
Dim strQuery
Dim strUrl

strQuery = Request.ServerVariables("QUERY_STRING")
strUrl = "http://www.hotels2thailand.com" & Request.ServerVariables("URL")

IF strQuery <> "" Then
	strQuery = "?" & strQuery
End IF

sqlDetail = "pr_hotel_detail " & intProductID

Set recDetail = Server.CreateObject ("ADODB.Recordset")
recDetail.Open SqlDetail, Conn,adOpenForwardOnly, adLockReadOnly

strDestination = recDetail.Fields("destination")
intDestinationID = recDetail.Fields("destination_id")

strDetail = Replace(recDetail.Fields("detail_en"), recDetail.Fields("title_en"), "<b><font color=""#0033FF"">" & recDetail.Fields("title_en") & "</font></b>")
strDetail = Replace(strDetail,VbCrlf,"<br>")
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html><!-- InstanceBegin template="/Templates/hotels2hotels-detail.dwt" codeOutsideHTMLIsLocked="false" -->
<head>
<!-- InstanceBeginEditable name="doctitle" -->
<title><%=recDetail.Fields("title_en")%> <%Response.Write " " & strDestination%></title>
<!-- InstanceEndEditable --> 
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link href="../style.css" rel="stylesheet" type="text/css">
<!-- InstanceBeginEditable name="head" -->
<meta name="description" content="<%=recDetail.Fields("title_en")%> : <%=recDetail.Fields("title_en")%> Reservation">
<meta name="keywords" content="<%=recDetail.Fields("title_en")%> reservations, <%=recDetail.Fields("title_en")%> booking, <%=recDetail.Fields("title_en")%> <%Response.Write " " & strDestination%>">
<!-- InstanceEndEditable -->
	<SCRIPT Language=Javascript>
	
	var bookmarklink = "<%=strUrl%>"
	var bookmarkheading = "Hotels2Thailand.com - <%=recDetail.Fields("title_en")%>"
	
	function SM()
	{
	var f=document.PTOL;
	f.eurl.value=bookmarklink;
	f.submit();
	}
	function SBK()
	{
	bookmarklink = bookmarklink;
	window.external.AddFavorite(bookmarklink,bookmarkheading);
	}
	</SCRIPT>
</head>

<body>
<table width="780" cellspacing="1" cellpadding="2">
  <tr> 
    <td colspan="2"><h1><%=recDetail.Fields("title_en")%></h1></td>
  </tr>
  <tr> 
    <td colspan="2"><table width="100%" cellpadding="2" cellspacing="1" bgcolor="#F3F3F3">
        <tr bgcolor="#99CC00"> 
          <td bgcolor="#99CC00"><div align="center"><a href="http://www.hotels2thailand.com" class="MainNav" title="Thailand Hotels Home"><b>Thailand 
              Hotels Home</b></a></div></td>
          <td><div align="center"><a href="../thailand-hotels-faq.asp" class="MainNav"><b>FAQ</b></a></div></td>
          <td bgcolor="#99CC00"><div align="center"><a href="../thailand-hotels-contact.asp" class="MainNav"><b>Contact 
              US</b></a></div></td>
          <td><div align="center"><a href="../thailand-hotels-travel.asp" class="MainNav"><b>Thailand 
              Travel Guide</b></a></div></td>
          <td><div align="center"><a href="../thailand-hotels-testimonial.asp" class="MainNav"><b>User 
              Testimonial</b></a></div></td>
        </tr>
      </table></td>
  </tr>
  <tr> 
    <td width="160">&nbsp;</td>
    <td width="620" bgcolor="#A9E100">
	<%=fnNavigator(1, strDestination, intDestinationID, recDetail.Fields("title_en"))%>
	</td>
  </tr>
  <tr> 
    <td valign="top"><table width="160" bgcolor="#F3F3F3">
        <tr> 
          <td valign="top" bgcolor="#FFFFFF">

                
		<div align="center">
              <table width="90%" cellpadding="2" cellspacing="1" bgcolor="#FF9933">
                <tr>
                  <td><div align="center"><font color="#FFFFFF"><b>Personal Tools</b></font></div></td>
                </tr>
                <tr>
                  <td bgcolor="#FFFFFF">
              <table width="160" cellspacing="1" cellpadding="2">
                <tr> 
                  <td><img src="../image/favorite.gif" width="16" height="13"></td>
                  <td><a href="javascript:SBK();" class="hotel">Bookmark This 
                    Hotel</a></td>
                </tr>
                <tr> 
                  <td valign="top"><img src="../image/sndemail.gif"></td>
                  <td valign="top"><a href="../thailand-hotels-tell.asp?pd=<%=intProductID%>" class="Hotel">Send 
                    this hotel to a friend</a></td>
                </tr>
              </table>
              </td>
                </tr>
              </table>
              </div>
			  <p><%Call fnRecentShow(1,intDestinationID)%></p>
<%
Dim sqlRelate
Dim recRelate

sqlRelate = "pr_hotel_relate " & intProductID
Set recRelate = Server.CreateObject ("ADODB.Recordset")
recRelate.Open SqlRelate, Conn,adOpenForwardOnly, adLockReadOnly
	IF NOT recRelate.EOF Then
%>
		<table cellpadding="2" cellspacing="1" bgcolor="#C4DBFF">
		  <tr><td align="center"><font color="#0066FF"><b>Related Hotels</b></font></td></tr>
		  <tr><td bgcolor="#FFFFFF">
		    <%While NOT recRelate.EOF%>
            <li><a href="<%=recRelate.Fields("files_name")%>" class="Hotel"><font color="#3366CC"><%=recRelate.Fields("product")%> </font></a> 
              <%
			recRelate.MoveNext
		Wend
		%>
		</td></tr>
		</table>
              <%
	End IF
recRelate.Close
Set recRelate = Nothing
%>

              <br>
              <br>
              <div align="center"><img src="../image/tat.gif" width="90" height="45"><br>
                <br>
                <img src="../image/img_creditcard.gif" width="80" height="37"><br>
                <br>
                <img src="../image/Verified-by-Visa.gif" width="84" height="60"> 
                <br>
                <br>
                <img src="../image/verisign.gif" width="80" height="33"> <br>
                <br>
              </div>
          </td>
        </tr>
      </table></td>
    <td valign="top"><table width="620" cellspacing="1" cellpadding="2">
        <tr> 
          <td><!-- InstanceBeginEditable name="Hotel Body" --> <font color="#0066FF"> 
            <h4><%=recDetail.Fields("title_en")%></h4>
            </font> <img src="../thailand-hotels-pic/<%=recDetail.Fields("product_code")%>_logo.jpg" alt="<%=recDetail.Fields("title_en")%> logo"> 
            <br>
            <br>
            <!-- Thailand Hotels Detail Picture-->
            <table width="588" border="0" cellspacing="0" cellpadding="0">
              <tr align="left" valign="top"> 
                <td><div align="center"><img src="../thailand-hotels-pic/<%=recDetail.Fields("product_code")%>_a.jpg"></div></td>
                <td><div align="center"><img src="../thailand-hotels-pic/<%=recDetail.Fields("product_code")%>_b.jpg"></div></td>
                <td><div align="center"><img src="../thailand-hotels-pic/<%=recDetail.Fields("product_code")%>_c.jpg"></div></td>
                <td><div align="center"><img src="../thailand-hotels-pic/<%=recDetail.Fields("product_code")%>_d.jpg"></div></td>
              </tr>
			  <%IF recDetail.Fields("num_pic")>0 Then%>
              <tr align="left" valign="top"> 
                <td colspan="4"><br><div align="right"><a href="../thailand-hotels-pic.asp?pic=1&id=<%=intProductID%>" target="_blank">See More <%=recDetail.Fields("title_en")%> Pictures&gt;&gt;</a></div></td>
              </tr>
			  <%End IF%>
            </table>
            <!-- Thailand Hotels Detail Picture-->
            <br>
            <br>
			<%=strDetail%>
            <br>
            <br>
            <font color="#0066FF"><b>Address</b></font> <%=recDetail.Fields("address_en")%><br><br>
			<a href="../thailand-hotels-map.asp?id=<%=intProductID%>" target="_blank" title="<%=recDetail.Fields("title_en")%> map"><b>Click here for <%=recDetail.Fields("title_en")%> map</b></a><br>
            <br>
            <div align="center"><br><p><a name="currency"><%response.Write fnCurrencyList(Session("currency_id"),1)%></a></p></div>
            <br>
            <b><font color="#0066FF"><%=recDetail.Fields("title_en")%> Class</font></b> 
            <%=FnStar(recDetail.Fields("star"))%><br>
            <br>
            <%=fnroomprice(intProductID)%> <br>
			<table width="100%">
			<form action="../thailand-hotels-booking.asp" method="get">
              <tr> 
                <td> <div align="center"> 
					  <%=fnGenHidden("ch_in_date,ch_in_month,ch_in_year,ch_out_date,ch_out_month,ch_out_year")%>
					  <input type="hidden" name="id" value="<%=intProductID%>">
                    <input name="Submit" type="submit" class="greenBtn" value="Booking">
                  </div></td>
              </tr>
			  </form>
            </table>
            <br>
						<%=fnFaciclities(intProductID, 1)%>
			<%=fnFaciclities(intProductID, 2)%>
			<%=fnFaciclities(intProductID, 3)%>
			<br>
			<table width="100%">
			<form action="../thailand-hotels-booking.asp" method="get">
              <tr> 
                <td> <div align="center"> 
					  <%=fnGenHidden("id,ch_in_date,ch_in_month,ch_in_year,ch_out_date,ch_out_month,ch_out_year")%>
					  <input type="hidden" name="id" value="<%=intProductID%>">
                    <input name="Submit" type="submit" class="greenBtn" value="Booking">
                  </div></td>
              </tr>
			  </form>
            </table>
			</p>
            <!-- InstanceEndEditable --></td>
        </tr>
      </table></td>
  </tr>
  <tr>
    <td valign="top">&nbsp;</td>
    <td valign="top" bgcolor="#A9E100"><%=fnNavigator(1, strDestination, intDestinationID, recDetail.Fields("title_en"))%></td>
  </tr>
  <tr> 
    <td colspan="2" valign="top"><table width="100%" cellpadding="2" cellspacing="1" bgcolor="#99CC00">
        <tr> 
          <td><div align="center"><font color="#FFFFFF">Copyright &copy; 1996-2004 
              <b>Hotels 2 Thailand .com</b>. All rights reserved.</font></div></td>
        </tr>
      </table></td>
  </tr>
</table>
</body>
<!-- InstanceEnd --></html>
<%
recDetail.Close
Set recDetail = Nothing

Call connClose()
End Sub
%>
