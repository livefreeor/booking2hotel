<%@ Page Language="C#" AutoEventWireup="true" CodeFile="order-complete.aspx.cs" Inherits="order_complete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
    <title></title>
    <link href="../css/style-b2b.css" type="text/css" rel="stylesheet" />
     <link href="../css/bookForm.css" type="text/css" rel="stylesheet" />
    <style>
        a {
            color: #3b59aa;
            font-weight: bold;
            text-decoration: none;
            font-size: 11px;
        }

            a:-webkit-any-link {
                cursor: auto;
            }

            a:hover {
                text-decoration: underline;
            }
    </style>
</head>
<body>
    <form  id="form1"  runat="server">
        <div>
            <div id="headerpage">
                <div class="header">
                    <div class="logo">
                        <asp:Label ID="lblLogo" runat="server"></asp:Label>
                    </div>
                    <%--<div class="text_top">
                        <span style="font-size: 15px"><b>Welcome to B2B </b></span>by Booking2Hotel
                        <br />
                        <asp:LinkButton ID="lbtAgentName" runat="server" OnClick="lbtAgentName_Click" CssClass="btnLink" target="_blank"></asp:LinkButton>
                        |  <a href="Logout.aspx" class="btnLink">Logout</a>
                    </div>--%>
                </div>
            </div>
            <div id="listbook">


                <div id="thankyouContent" style="margin-top:0px; padding:20px 0 0 0;">
        <p class="fnBig">Thank you for visiting our service </p>
        <br>
                    <!--###WordingDefaultStart###-->
                   <asp:Literal ID="wording" runat="server"></asp:Literal>
                     
                       
                        <div style="background-color:#f2f2f2; padding:10px;">
                        <div>
                    	<img src="/images/ico_mail_junk.png" style="float:left; margin-right:10px;"><p style="display:block;float:left; width:530px;"><span class="fnRed"><strong>Remark :</strong></span> Sometimes our voucher may be sent into your Junk mail, Spam mail or Bulk mail. Please kindly check in there.
                     	In case you do not find any revert mail from us after 24 hours passed, please contact us asap. Then our staff will contact to you in shortly. </p>
                        <br class="clearAll">
                        </div>

                        
                        <div style="margin-top:15px;">
                        <img src="/images/ico_deduce_card.png" style="float:left; margin-right:10px;">
                        <p style="display:block;float:left; width:530px;">Actual payment will be deducted from your credit card after your room reservation is 
                    vacancy available according to your requirement.</p>
                    <br class="clearAll"> 
                    </div> 
                    </div>
                    <br>
                    <p>We really are appreciated of your kind support. Our commitment ensures you that your reservation request is promptly attended to. </p> 
                    <%--<center><input type="button" value="Back to homepage" style="width:160px; height:30px;" onclick="location.href = 'http://www.hotels2thailand.com'"> </center>     --%>   
        </div>
                <%--<div id="bookingHeader" runat="server">
                </div>--%>
                <br class="clear" />
                <div style="padding:0 20px;">
                    <%--<div id="box_calendar">
                        <table width="90%" border="0" align="center">
                            <tr>
                                <td>Check in </td>
                                <td>
                                    <input name="dateci" id="dateci" size="20" autocomplete="off" value="CHECK IN" readonly="readonly" rel="datepicker" type="text"></td>
                                <td>Check Out</td>
                                <td>
                                    <input name="dateco" id="dateco" size="20" autocomplete="off" value="CHECK OUT" readonly="readonly" type="text"></td>
                                <td>&nbsp;</td>
                                <td>
                                    <input type="hidden" name="rateExchange" id="rateExchange" value="25" />
                                    <input type="hidden" name="pid" id="pid" runat="server" />
                                    <input type="hidden" name="hdAgencyID" id="hdAgencyID" runat="server" />
                                    <input type="hidden" name="hdTierID" id="hdTierID" runat="server" />
                                    <a href="javascript:void(0)" id="btnBook">
                                        <img src="../images/agency/Book.png" />
                                    </a></td>
                            </tr>
                        </table>
                    </div>--%>
                    <table id="roomRatePanel" width="100%">
                        <tr>
                            <td valign="top">
                                <div class="b2hRateResult"></div>
                            </td>
                        </tr>
                    </table>
                </div>
                <br class="clear" />
            </div>
            <br class="clear" />
        </div>
    </form>
</body>

</html>
