<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="review_site_admin_detail.aspx.cs" Inherits="Hotels2thailand.UI.admin_review_site_admin_detail"  %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <link href="../../css/reviewstyle.css" rel="Stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../../css/jquery.fastconfirm.css"/>
    <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
  <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
	<script src="../../Scripts/jquery.fastconfirm.js" type="text/javascript"></script>
     <script type="text/javascript" src="../../Scripts/darkman_utility.js"></script>
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <script type="text/javascript">

       
       function StarValue(ulId, val) {
           $("#ratebox").after('<input id="value_hidden_' + ulId + '" type="hidden" name="' + ulId + '" value="' + val + '" />');
           if (val > 0) {
               $("#" + ulId).children("li").filter(function (index) {
                   return index <= val - 1;
               }).css({ "background-image": "Url(../../images/write_blank.png)", "background-position": "0 0", "cursor": "pointer" });

               $("#" + ulId).children("li").filter(function (index) {
                   return index > val - 1;
               }).css({ "background-image": "Url(../../images/write_blank.png)", "background-position": "0 -20px", "cursor": "pointer" });
           }
       }
      
   </script>
   <div class="reivew_site_main">
        
        <div class="content">
        
        
            <h1>General Information</h1>
            <div class="boxField">
                <table>
                    <tr>
                        <td class="title">Name</td>
                        <td><asp:TextBox ID="txtName" runat="server" ClientIDMode="Static" CssClass="txtsite_review" EnableTheming="false"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="title">Email</td>
                        <td><asp:TextBox ID="txtEmail" runat="server" ClientIDMode="Static" CssClass="txtsite_review" EnableTheming="false"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="title">Address</td>
                        <td><asp:TextBox ID="txtaddress" runat="server" ClientIDMode="Static" TextMode="MultiLine" Rows="4" CssClass="txtsite_review" EnableTheming="false"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="title">Occupation</td>
                        <td><asp:DropDownList ID="dropOcc" runat="server" CssClass="dropsite_review" EnableTheming="false"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="title">Date of Birth</td>
                        <td><div class="field_container">
                        <asp:DropDownList ID="birthday_month" CssClass="birthday_month" ClientIDMode="Static" runat="server" AppendDataBoundItems="true" EnableTheming="false">
                            <asp:ListItem Text="Month:" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                            <asp:ListItem Text="May" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                            <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                            <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                            <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                            <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                            <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                            <asp:ListItem Text="Dec" Value="13"></asp:ListItem>
                        </asp:DropDownList>
                         <asp:DropDownList ID="birthday_day"  CssClass="birthday_day" ClientIDMode="Static" runat="server" AppendDataBoundItems="true" EnableTheming="false">
                         <asp:ListItem Text="Day:" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="birthday_year" CssClass="birthday_year" ClientIDMode="Static" runat="server" AppendDataBoundItems="true" EnableTheming="false">
                         <asp:ListItem Text="year:" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                          </div>
                        </td>
                    </tr>
                     <tr>
                        <td class="title">Country</td>
                        <td><asp:DropDownList ID="dropCountry" runat="server" CssClass="dropsite_review" EnableTheming="false"></asp:DropDownList></td>
                    </tr>
                     <tr>
                        <td class="title">Telephone</td>
                        <td><asp:TextBox ID="txtTel" runat="server" ClientIDMode="Static" CssClass="txtsite_review" EnableTheming="false"></asp:TextBox></td>
                    </tr>
                     <tr>
                        <td class="title">Mobile</td>
                        <td><asp:TextBox ID="txtmobile" runat="server" ClientIDMode="Static" CssClass="txtsite_review" EnableTheming="false"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="title">Fax</td>
                        <td><asp:TextBox ID="txtFax" runat="server" ClientIDMode="Static" CssClass="txtsite_review" EnableTheming="false"></asp:TextBox></td>
                    </tr>
                </table>
            </div>

            <h1>Travel Information</h1>

             <div class="boxField">
                <p class="title">Purpose of Thailand visit</p>
                <asp:CheckBoxList ID="chkVisit" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="2" CssClass="chekListreview" ></asp:CheckBoxList>
                <p><asp:TextBox ID="TxtvisitOther" runat="server" ClientIDMode="Static" CssClass="txtsite_review" EnableTheming="false" style="width:200px"></asp:TextBox></p>
            <p class="title">Duration of Stay (Night)</p>
                <asp:DropDownList ID="dropNight" runat="server" CssClass="dropsite_review" EnableTheming="false"></asp:DropDownList>
                <br /><br />
                <p class="title">How often do you come to visit Thailand?</p>
                <asp:RadioButtonList ID="radioOften" runat="server"  ClientIDMode="Static" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="4"></asp:RadioButtonList>
                <%--<p><asp:TextBox ID="txtOftenOther" runat="server" ClientIDMode="Static" CssClass="txtsite_review" EnableTheming="false"></asp:TextBox></p>--%>
                <br /><br />
                <p class="title">Have you ever booked the hotel service via internet?</p>
                <asp:RadioButtonList ID="radioIsEverInter" runat="server"  ClientIDMode="Static" RepeatDirection="Horizontal" RepeatLayout="Table" CssClass="chekListreview">
                    <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                    <asp:ListItem Value="False" Text="No"></asp:ListItem>
                </asp:RadioButtonList>
             </div>

             <h1>Web site feedback</h1>
             <p>Our staff’s service: Please rate your opinion towards our service</p>
             <div class="boxField" >
                <div id="ratebox">
                    <p class="title">Promptly Mail Interaction (Speed)</p>
                    <ul id="speed"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>
                    <div style="clear:both; height:5px;"></div>
                    <asp:TextBox ID="txtSpeed" runat="server" ClientIDMode="Static" TextMode="MultiLine" Rows="4" CssClass="txtsite_review" EnableTheming="false"></asp:TextBox>
                    <br /><br />
                    <p class="title">Accuracy in enquiry’ s response</p>
                    <ul id="enquiry"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>
                    <div style="clear:both; height:5px;"></div>
                    <asp:TextBox ID="txtresponse" runat="server" ClientIDMode="Static" TextMode="MultiLine" Rows="4" CssClass="txtsite_review" EnableTheming="false"></asp:TextBox>
                    <br /><br />
                    <p class="title">Ability in problem solution</p>
                    <ul id="Ability"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>
                    <div style="clear:both; height:5px;"></div>
                    <asp:TextBox ID="txtability" runat="server" ClientIDMode="Static" TextMode="MultiLine" Rows="4" CssClass="txtsite_review" EnableTheming="false"></asp:TextBox>
                    <br /><br />
                    <p class="title">Knowledge of hotel information introduction</p>
                    <ul id="Knowledge"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>
                    <div style="clear:both; height:5px;"></div>
                    <asp:TextBox ID="txtknow" runat="server" ClientIDMode="Static" TextMode="MultiLine" Rows="4" CssClass="txtsite_review" EnableTheming="false"></asp:TextBox>
                </div>
                
                <br /> <br />
                <p class="title">How did you find us: Please select</p>
                
                    <asp:RadioButtonList ID="radiofindus" runat="server"  ClientIDMode="Static" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="4"></asp:RadioButtonList>
                    <p><asp:TextBox ID="txtfindusother" runat="server" ClientIDMode="Static" CssClass="txtsite_review" EnableTheming="false"></asp:TextBox></p>

                    <p class="title">What kind of enhancements to the website encouraged you booked with us?</p>
                    <asp:CheckBoxList ID="chkenhance" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="4"></asp:CheckBoxList>
                    <p><asp:TextBox ID="txtchkenhance" runat="server" ClientIDMode="Static" CssClass="txtsite_review" EnableTheming="false"></asp:TextBox></p>
                    
                    
                    
                    <p class="title">What kind of enhancement to the website that you would like us to include?</p>
                    <p><asp:CheckBoxList ID="chkenhanceProduct" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="4"></asp:CheckBoxList></p>
                    <p><asp:TextBox ID="txtchkenhanceProduct1" runat="server" ClientIDMode="Static" CssClass="txtsite_review" EnableTheming="false"></asp:TextBox></p>
                    <p><asp:TextBox ID="txtchkenhanceProduct2" runat="server" ClientIDMode="Static" CssClass="txtsite_review" EnableTheming="false"></asp:TextBox></p>

                    <p class="title">Was there anything not working that you experienced while booking with us?</p>
                    <p><asp:CheckBoxList ID="chkProblem" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="2"></asp:CheckBoxList></p>
                    <p><asp:TextBox ID="txtchkProblem1" runat="server" ClientIDMode="Static" CssClass="txtsite_review" EnableTheming="false"></asp:TextBox></p>
                    <p><asp:TextBox ID="txtchkProblem2" runat="server" ClientIDMode="Static" CssClass="txtsite_review" EnableTheming="false"></asp:TextBox></p>


                    <p class="title">Would you like to receive special information from us via email?</p>
                    <p><asp:RadioButtonList ID="radioMail" runat="server"  ClientIDMode="Static" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="4">
                    <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                    <asp:ListItem Value="False" Text="No"></asp:ListItem>
                </asp:RadioButtonList></p>
                 <br /> <br />
                <p class="title">Will you come back to book with us again?</p>
                    <p><asp:RadioButtonList ID="radiobackAgain" runat="server"  ClientIDMode="Static" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="4">
                    <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                    <asp:ListItem Value="False" Text="No (Please specify)"></asp:ListItem>
                    </asp:RadioButtonList></p>
                     <p><asp:TextBox ID="txtNo" runat="server" ClientIDMode="Static" CssClass="txtsite_review" EnableTheming="false"></asp:TextBox></p>
                  
                  <p class="title">Any further comments</p>  
                  <asp:TextBox ID="txtComment" runat="server" ClientIDMode="Static" TextMode="MultiLine" Rows="8" CssClass="txtsite_review" EnableTheming="false"></asp:TextBox>
             </div>
             <h1>Booking History</h1>
             <p></p>
             <div class="boxField" >
                <asp:GridView ID="GvBookingLIst" runat="server" AutoGenerateColumns="false" >
                    <Columns>
                        <asp:TemplateField HeaderText="BookingId">
                            <ItemTemplate>
                                <asp:Label ID="lblbookingId" runat="server" Text='<%# Bind("BookingID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("StatusTitle") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                    </Columns>
                </asp:GridView>
             </div>
             <%--<div class="review_btn">
                <asp:Button ID="btnCloseReview" runat="server"  SkinID="Green" CssClass="booknow"  EnableTheming="false" OnClick="btnCloseReview_Onclick" />
             </div>--%>
              
        </div>
    </div>

</asp:Content>

