<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true"  CodeFile="product.aspx.cs"
 Inherits="Hotels2thailand.UI.admin_product_product" %>
<%@ Register  Src="~/Control/DatepickerCalendar-single.ascx" TagName="DatePicker_Add_Edit" TagPrefix="DateTime" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link  href="../../css/productstyle3.css" type="text/css" rel="Stylesheet" />
      
 <link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
 <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
  <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
 <script type="text/javascript" src="../../Scripts/jquery.corner.js"></script>
  	<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>

    <script type="text/javascript" src="../../Scripts/datepicker.js"></script>
 
  <script type="text/javascript" src="../../Scripts/darkman_utility.js"></script>
 <%--<script type="text/javascript" src="../../Scripts/darkman_product_script.js"></script>--%>
 <script type="text/javascript" >

     $(document).ready(function () {
         var qProductId = GetValueQueryString("pid");


         $("#btnQuickAdd").click(function () {

             SuppliercontactAdd();

             return false;
         });
         //DatePicker_nopic("txtdate_expired");

         //$("#dropCat").change(function () {
         //    if ($(this).val() == "3") {
         //        $("#revenue_com_step").show();
         //        $("#com_val").hide();
         //    }
         //    else {
         //        $("#revenue_com_step").hide();
         //        $("#com_val").show();
         //    }
         //        //
         //});

         CheckDuplicate();
         CheckFormat();
         $("#txtHotelCode").keyup(function () {

             CheckDuplicate();
             CheckFormat();
         });

         CalCommission();

     });


     function CalCommission() {
         if ($('#dropCat').val() != "1") {
             $('#divCalCom').hide();
            // $('#txtComVal').removeAttr('readonly');
         }
         else {

             //$('#txtComVal').attr('readonly', 'readonly');
         }


         $('#txtComVal').click(function () {
             if ($('#dropCat').val() == "1") {
                 if ($('#divCalCom').css('display') == 'none') {
                     $('#divCalCom').slideDown();
                 }
                 else {
                     $('#divCalCom').slideUp();
                 }
             }
         });


         $('#dropCat').change(function () {
             if ($('#dropCat').val() == "1") {
                // $('#txtComVal').attr('readonly', 'readonly');
                 $('#divCalCom').slideDown();
             }
             else {
                // $('#txtComVal').removeAttr('readonly');
                 $('#divCalCom').slideUp();
             }
         });

         $('#txtNetrate').keyup(function () {
             if ($('#txtNetrate').val() != '') {
                 if ($('#txtMarkup').val() != '') {
                     CalMarkup();
                 }

                 if ($('#txtSaleprice').val() != '') {
                     CalSaleprice();
                 }
             }
             else {
                 $('#txtComVal').val('');
             }
         });

         $('#txtMarkup').keyup(function () {

             if ($('#txtMarkup').val() == '') {
                 $('#txtSaleprice').removeAttr("disabled");
                 $('#txtComVal').val('');
             }
             else {
                 $('#txtSaleprice').attr("disabled", "disabled");
                 if ($('#txtNetrate').val() != '') {
                     CalMarkup();
                 }
             }
         });

         $('#txtSaleprice').keyup(function () {
             if ($('#txtSaleprice').val() == '') {
                 $('#txtMarkup').removeAttr("disabled");
                 $('#txtComVal').val('');
             }
             else {
                 $('#txtMarkup').attr("disabled", "disabled");
                 if ($('#txtNetrate').val() != '') {
                     CalSaleprice();
                 }
             }
         });
     }

     function CalMarkup() {
         var Net = $('#txtNetrate').val();
         var Mark = $('#txtMarkup').val();
         var a = (Net * Mark) / 100;
         var b = parseInt(Net) + parseInt(((Net * Mark) / 100));
         var Com = (a / b) * 100;
         Com = Math.round(Com * 1000) / 1000;
         $('#txtComVal').val(Com);
     }

     function CalSaleprice() {
         var Net = $('#txtNetrate').val();
         var Sale = $('#txtSaleprice').val();
         var Com = ((Sale - Net) / Sale) * 100;
         Com = Math.round(Com * 1000) / 1000;
         $('#txtComVal').val(Com);
     }

     function SupplierContacQuickSave() {

         $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").prependTo("#panelInsertBox").ajaxStart(function () {
             $(this).show();
         }).ajaxStop(function () {
             $(this).remove();
         });

         var qSupplierId = GetValueQueryString("supid");

         var post = $("#Contact_quick_add").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();


         $.post("../ajax/ajax_supplier_contact_quick_save.aspx?supid=" + qSupplierId, post, function (data) {

             if (data == "true") {
                 window.location.reload();
             } else {
                 alert(data);
             }


         });
     }

     function GetEmailinsert(id) {

         var result = "";

         var Count = $(".emailinsert").length;

         var key = makeid();

         result = result + "<div class=\"emailinsert\" style=\"display:none;\" >";
         result = result + "&nbsp;<input =\"checkbox\" style=\"display:none;\" name=\"chkEmailInSert\" checked=\"checked\" value=\"" + key + "\" />";
         result = result + "<input type=\"text\" id=\"txtEmail_" + key + "\" name=\"txtEmail_1\" OnKeyUp=\"GetEmailinsert('" + key + "');return false;\"";
         result = result + " class=\"TextBox_Extra_normal_small\" style=\"width:250px;\" OnBlur=\"CheckValueEmail('" + key + "');return false;\" />";
         result = result + "</div>";


         var lastId = $(".emailinsert").last().find("input[id^='txtEmail']").attr("id");

         var CurrentId = "txtEmail_" + id;

         if (CurrentId == lastId) {
             $("#emailinsertBlock").append(result);

             $(".emailinsert").last().fadeIn();
         }

     }

     function GetPhoneinsert(id) {
         var result = "";

         var Count = $(".phoneinsert").length;

         var key = makeid();
         result = result + "<div class=\"phoneinsert\" style=\"display:none;\" >";
         result = result + "<input =\"checkbox\" style=\"display:none;\" name=\"chkPhoneInSert\" checked=\"checked\" value=\"" + key + "\" />";
         result = result + "<table>";
         result = result + "<tr>";
         result = result + "<td>";

         result = result + "<select id=\"selPhoneCat_" + key + "\" OnChange =\"PhoneType('selPhoneCat_" + key + "');\" class=\"DropDownStyleCustom_small\" name=\"selPhoneCat_" + key + "\">";
         result = result + "<option value=\"1\" >Phone</option>";
         result = result + "<option value=\"2\" >Mobile</option>";
         result = result + "<option value=\"3\" >Fax</option>";
         result = result + "</select>";
         result = result + "</td>";
         result = result + "<td>";
         result = result + "<input type=\"text\" id=\"txtCountryCode_" + key + "\" value=\"66\" class=\"TextBox_Extra_normal_small\" name=\"txtCountryCode_" + key + "\"  maxlength=\"3\"   style=\" width:30px; background-color:#faffbd\"  />";
         result = result + "</td>";
         result = result + "<td>";
         result = result + "<input type=\"text\" id=\"txtLocal_" + key + "\" value=\"2\" class=\"TextBox_Extra_normal_small\" name=\"txtLocal_" + key + "\"  maxlength=\"2\"   style=\" width:20px; background-color:#faffbd\"  />";

         result = result + "</td>";
         result = result + "<td><input type=\"text\" id=\"txtPhone_" + key + "\" class=\"TextBox_Extra_normal_small\" OnBlur=\"CheckValue('" + key + "');return false;\" OnKeyUp=\"GetPhoneinsert('" + key + "');return false;\" name=\"txtPhone_" + key + "\"   style=\" width:200px;\" /></td>";

         result = result + "</tr>";
         result = result + "</table>";
         result = result + "</div>";

         var lastId = $(".phoneinsert").last().find("input[id^='txtPhone']").attr("id");
         var CurrentId = "txtPhone_" + id;
         if (CurrentId == lastId) {
             $("#phoneinsertBlock").append(result);

             $(".phoneinsert").last().fadeIn();
         }
         //FadeOptimize();


     }

     function CheckValueEmail(id) {
         //alert($(".emailinsert").length);
         if ($(".emailinsert").length > 1) {
             if ($("#txtEmail_" + id).val() == "") {
                 $("#txtEmail_" + id).parent().fadeOut(function () {
                     $(this).remove();
                 });
             }
         }

     }

     function CheckValue(id) {
         //        alert($("#txtPhone_" + id).parent().parent().parent().parent().parent().get(0).tagName);
         if ($(".phoneinsert").length > 1) {
             if ($("#txtPhone_" + id).val() == "") {
                 $("#txtPhone_" + id).parent().parent().parent().parent().parent().fadeOut(function () {
                     $(this).remove();
                 });
             }
         }

     }

     function PhoneType(id) {
         var PhoneCat = $("#" + id).val();
         var PhoneCode = "";


         if (PhoneCat == "2") {
             PhoneCode = "81";
         } else {
             PhoneCode = "2";
         }

         $("#" + id).parent().next().next().children(":text").val(PhoneCode);
     }
     function SuppliercontactAdd() {
         //var qBookingID = GetValueQueryString("bid");
         $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").prependTo("#panelInsertBox").ajaxStart(function () {
             $(this).show();
         }).ajaxStop(function () {
             $(this).remove();
         });
         $.get("../ajax/ajax_supplier_contact_quick.aspx", function (data) {
             //alert(data);
             DarkmanPopUp(450, data);
             //            
         });
     }

     function Notification(type) {

         var word = "";
         switch (type) {
             case 'duplicate':
                 word = "this code is not valid";
                 break;
             case 'format':
                 word = "wrong format";
                 break;
             case 'ok':
                 word = "";
                 break;
         }

         return word;
     }

     function CheckValid() {
         var ret = false;
         if ($("#hd_coderesult").val() == "true" && CheckFormat()) {
             ret = true;
         }
         return ret;
     }
     
     function CheckDuplicate() {

         var qProductId = GetValueQueryString("pid");
        
         $("<img class=\"img_progress\" src=\"/images/progress.gif\" alt=\"Progress\" />").prependTo("#AlertCode").ajaxStart(function () {
             $(this).show();
     
         }).ajaxStop(function () {
             $(this).remove();
             
         });

         var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
         //var ret = false;
         $.post("ajax_product_code_check.aspx", post, function (data) {
           
             var keycompare = 1;
             if (qProductId == "") {
                 keycompare = 0;
             }

             console.log(keycompare);

            // console.log("FALEE " + data);
             if (parseInt(data) > keycompare) {

                 console.log("FALSE " + data);

                 $("#hd_coderesult").val("false");
                // alert(Notification('duplicate'));
                 $("#AlertCode").html(Notification('duplicate'));
             }
             else {
                 console.log("TRUE" + data);
                 $("#hd_coderesult").val("true");
                 $("#AlertCode").html(Notification('ok'));
             }
             
         });

     

       
        
         
     }

     function CheckFormat() {

         var ret = false;
         var re = /^[A-Z]{3}[0-9]{3}$/;

         ret = re.test($("#txtHotelCode").val());

         if (!ret) {
             $("#AlertCode2").html(Notification('format'));
         } else {
             $("#AlertCode2").html(Notification('ok'));
         }
         return ret;
       
     }

 </script>
 
     <style type="text/css">
         .contentheadedetail {
             margin:0px;
             padding:0px;
         }
         #Commission {
         
             
         }
         #com_left {
             margin:10px 0 0 0 ;
             padding:0px;
             width:500px;
             float:left;
         }
         #com_right {
              width:600px;
             float:left;
         }

         #revenue_com_step {
             width:300px;
            
             margin:10px 0 0 0 ;
             padding:20px;
             background-color:#D5DEE5;
             border:1px solid #787984;
         }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:Panel ID="panelMenu" runat="server" Visible="false" >
 <div id="product_setup_menu" class="menu">
 <asp:HyperLink ID="lnkhotel" runat="server" Text="Hotel"   ></asp:HyperLink>
 <asp:HyperLink ID="lnkAdvance" runat="server" Text="Advance"></asp:HyperLink>
     <asp:HyperLink ID="lnkPI" runat="server" Text="Payment Information"></asp:HyperLink>
 <asp:HyperLink ID="lnkFTP" runat="server" Text="FTP"></asp:HyperLink>
 <asp:HyperLink ID="lnkContact" runat="server" Text="Contact"></asp:HyperLink>
 <asp:HyperLink ID="lnkCommission" runat="server" Text="Commission"></asp:HyperLink>
 <asp:HyperLink ID="lnkSales" runat="server" Text="Sales Ref"></asp:HyperLink>
   <asp:HyperLink ID="lnkPicture" runat="server"> Picture</asp:HyperLink> 
     <asp:HyperLink ID="lnkMail" runat="server"> Mail Setting</asp:HyperLink> 
  <div style="clear:both"></div>
 </div>    
 <div style="clear:both"></div>
 </asp:Panel>
 
 <asp:Panel ID="panelStatus" runat="server" Visible="false">
    <h4><img src="../../images/content.png"  alt="image_topic" /> Product</h4>
    <p class="contentheadedetail">Manage Product Detail And Status</p>
     <input type="hidden" id="hd_coderesult" />
  <div id="status">
    <table>
     <tr><td>HotelName: </td>
     <td><asp:TextBox ID="txtHotelName" Width="450" runat="server"></asp:TextBox></td>

     </tr>
     <tr><td>HotelCode: </td>
     <td><asp:TextBox ID="txtHotelCode" runat="server" Width="150" BackColor="#faffbd" ClientIDMode="Static" />
       <span style="color:red;" id="AlertCode"></span>
         <span style="color:red;" id="AlertCode2"></span>
     </td>
     </tr>
     <tr>
     <td>Destination: </td>
     <td><asp:DropDownList ID="dropDes" runat="server" Width="450" AutoPostBack="true" OnSelectedIndexChanged="dropDes_SelectedIndexChanged" ></asp:DropDownList></td>
     </tr>
     <tr><td>Address: </td>
     <td><asp:TextBox ID="txtAddress" runat="server" Width="450"></asp:TextBox><span style=" font-size:11px; color:Green"> *(Show in mail&Voucher)</span></td>
     </tr>
      <tr><td>Phone </td>
     <td><asp:TextBox ID="txtPhone" runat="server" Width="450"></asp:TextBox><span style=" font-size:11px; color:Green"> *(Show in mail&Voucher)</span></td>
     </tr>
         <tr><td>BHT Email contact </td>
     <td><asp:TextBox ID="txtEmailSup" runat="server" Width="450"></asp:TextBox></td>
     </tr>
    <%-- <tr><td>Latitude </td>
     <td><asp:TextBox ID="txtLat" runat="server" Width="150"></asp:TextBox><span style=" font-size:11px; color:Green"> *(Map page)</span></td>
     </tr>
      <tr><td>longtitude </td>
     <td><asp:TextBox ID="txtLong" runat="server" Width="150"></asp:TextBox><span style=" font-size:11px; color:Green"> *(Map page)</span></td>
     </tr>--%>
     <tr><td>Status:</td>
     <td><asp:RadioButtonList ID="radioStatus" RepeatDirection="Horizontal" runat="server">
      <asp:ListItem  Text="Enable" Value="True" ></asp:ListItem>
      <asp:ListItem Text="Disable" Value="False"></asp:ListItem>
     </asp:RadioButtonList></td>
     </tr>
     <tr>
     <td style=" font-weight:bold;">Active: </td>
     <td style="background:#faffbd;" >
     <asp:RadioButtonList ID="radioActive" RepeatDirection="Horizontal" runat="server">
      <asp:ListItem  Text="Active" Value="True" ></asp:ListItem>
      <asp:ListItem Text="Inactive" Value="False" ></asp:ListItem>
     </asp:RadioButtonList>
     </td>
     </tr>
     <tr><td>Hotel Activity status: </td>
     <td><asp:DropDownList ID="dropacStatus" runat="server" Width="450"></asp:DropDownList></td>
     </tr>
     <tr>
     <td>Comment: </td>
     <td><asp:TextBox ID="txtComment" TextMode="MultiLine" runat="server" Rows="8" Width="450" ></asp:TextBox></td>
     </tr>
     <tr>
     <td colspan="2" align="center"><asp:Button ID="btnSaveStatus" runat="server" Text="Save" SkinID="Green" OnClientClick="retrun CheckValid();" OnClick="btnSaveStatus_Onclick" /></td>
     </tr>
    </table>
 </div>
 </asp:Panel>

 <asp:Panel ID="panelAdvance" runat="server" Visible="false">
 
 <div id="Advance">
 <h4><img src="../../images/content.png"  alt="image_topic" /> Advance</h4>
 <p class="contentheadedetail">Manage product informantion</p>
 <table>
 <tr><td>Website Name: </td>
 <td><asp:TextBox ID="txtWebsiteName" Width="450" runat="server"></asp:TextBox>

     <asp:CheckBox ID="chkIsB2b" runat="server"  Text="Is B2b" /> 
 </td></tr>

     <tr><td>B2b Map: </td>
 <td><asp:TextBox ID="txtB2bMap" Width="450" runat="server"></asp:TextBox>
 
 </td></tr>
     <tr><td>Product Type: </td>
 <td>
     <asp:DropDownList ID="dropB2bCat" runat="server">
         <asp:ListItem Text="Hotel" Value="29"></asp:ListItem>
         <asp:ListItem Text="Show" Value="38"></asp:ListItem>
         <asp:ListItem Text="Day Trip" Value="34"></asp:ListItem>
         <asp:ListItem Text="Water Activity" Value="36"></asp:ListItem>
          <asp:ListItem Text="Golf Courses" Value="32"></asp:ListItem>
     </asp:DropDownList>
 
 </td></tr>

     <tr><td style="font-weight:bold">Manage BY: </td>
 <td>
     <asp:DropDownList ID="dropManage" runat="server" Width="450" AutoPostBack="true" OnSelectedIndexChanged="dropManage_SelectedIndexChanged">
 <asp:ListItem  Value="1" >Hotel manage</asp:ListItem>
 <asp:ListItem  Value="2" >BHT Manage</asp:ListItem>
 </asp:DropDownList>

     <asp:CheckBox ID="checkmailNotice" runat="server" Visible="false" Text="Is recieve e-mail ?" /> 
 </td></tr>

 <tr><td style=" font-weight:bold;">Booking payment type: </td>
 <td><asp:DropDownList ID="dropBookingType" runat="server" Width="450" BackColor="#faffbd" ></asp:DropDownList></td></tr>

     

     <tr><td style="font-weight:bold">Gateway ID: </td>
 <td ><asp:DropDownList ID="dropGateway" runat="server" Width="450"></asp:DropDownList></td></tr>

  <tr><td style=" font-weight:bold;">Show Price Inclue Vat: </td>
 <td><asp:DropDownList ID="dropVat" runat="server" Width="450" BackColor="#faffbd" >
 <asp:ListItem Text="No, Show price exclude Vat" Value="False"></asp:ListItem>
  <asp:ListItem Text="Yes, Show net price for this hotel" Value="True"></asp:ListItem>
 </asp:DropDownList></td></tr>
 <tr><td>Currency: </td>
 <td><asp:DropDownList ID="dropCurrency" runat="server" Width="450" ></asp:DropDownList></td></tr>
  <%--<tr><td>Email Engine: </td>
 <td><asp:TextBox ID="txtEmail" Width="450" runat="server"></asp:TextBox></td></tr>
  <tr><td>Email Engine Password: </td>
 <td><asp:TextBox ID="txtEmailPass" Width="450" runat="server"></asp:TextBox></td></tr>--%>
  <tr><td>Email Contact(show in customer email): </td>
 <td><asp:TextBox ID="txtcontactEmail" Width="450" runat="server"></asp:TextBox>
     <asp:RequiredFieldValidator ControlToValidate="txtcontactEmail" Display="Dynamic" Text="*Require" runat="server" SetFocusOnError="true" Width></asp:RequiredFieldValidator>

 </td></tr>
 <tr><td>Folder: </td>

 <td><asp:TextBox ID="txtfolder" Width="450" runat="server"></asp:TextBox></td></tr>
 
  
  <tr><td>Merchant_id: </td>
 <td><asp:TextBox ID="txtMerchant" Width="450" runat="server"></asp:TextBox></td></tr>
  <tr><td>Teminal ID: </td>

 <td><asp:TextBox ID="txtTeminal" Width="450" runat="server"></asp:TextBox></td></tr>

     <tr><td>MD5 (checksum) kbank ver2: </td>

 <td><asp:TextBox ID="txtmd5" Width="450" runat="server" MaxLength="32" placeholder="must 32 charactor only!"></asp:TextBox></td></tr>

      <tr><td>ProfileID (cyber sourceo only): </td>
 <td><asp:TextBox ID="txtProfileID" Width="450" runat="server"></asp:TextBox></td></tr>
     <tr><td>Access_Key (cyber sourceo only): </td>
 <td><asp:TextBox ID="txtAccessCode" Width="450" runat="server" MaxLength="32" placeholder="must 32 charactor only!"></asp:TextBox></td></tr>
     <tr><td>Secrect_Key (cyber sourceo only): </td>
 <td><asp:TextBox ID="txtSecretCode" Width="450" TextMode="MultiLine" Rows="4" MaxLength="256" placeholder="must 32 charactor only!" runat="server"></asp:TextBox></td></tr>
  <tr><td>Url Return: </td>
 <td><asp:TextBox ID="txtUrl" Width="450" runat="server"></asp:TextBox></td></tr>
  <tr><td>Url Update: </td>
 <td><asp:TextBox ID="txtUpdate" Width="450" runat="server"></asp:TextBox></td></tr>
   <tr><td>Url Site Redirect: </td>
 <td><asp:TextBox ID="txtRedirect" Width="450" runat="server"></asp:TextBox></td></tr>

   <tr><td style="font-weight:bold">Salse</td>
   <td><asp:DropDownList ID="dropSale" runat="server" Width="450" BackColor="#faffbd"></asp:DropDownList> </td>
   </tr>
    <tr>
     <td colspan="2" align="center"><asp:Button ID="btnSaveAdvance" runat="server" Text="Save" SkinID="Green" OnClick="btnSaveAdvance_Onclick" /></td>
     </tr>
 </table>
 </div>
 
 
 </asp:Panel>

 <asp:Panel ID="panelPI" runat="server" Visible="false">
     
     <div id="PaymentInformation">
         <h6><asp:Label ID="lblhead" runat="server"></asp:Label></h6><br /> <br />
          
     <div id="due_date">
         Due Payment to Hotel : <asp:TextBox ID="txtdue_date" runat="server" Width="150" BackColor="#faffbd" ></asp:TextBox>


     </div>

         <div style="width:100%;">
             <table>
                 <tr>
                     <td>Commisison Start Date:&nbsp;&nbsp; </td>
                     <td><DateTime:DatePicker_Add_Edit ID="datePicker_Com_date_start"  runat="server"  /></td>
                 </tr>
                 <tr>
                     <td>Number of Month commission due</td>
                     <td><asp:TextBox ID="txtnummonth" runat="server" Width="100px"></asp:TextBox></td>
                 </tr>
                 <tr>
                     <td>Type of Monthlty Commission</td>
                     <td>
                         <asp:DropDownList ID="dropComMonthType" runat="server">
                             <asp:ListItem Value="1" Text="Regular" Selected="True"></asp:ListItem>
                             <asp:ListItem Value="2" Text="Advance"></asp:ListItem>
                         </asp:DropDownList>
                     </td>
                 </tr>
             </table>
           
         </div>
         <br /> <br /> 
         
         &nbsp;&nbsp;<asp:Button ID="btnDuedateSave" runat="server" SkinID="Green" OnClick="btnDuedateSave_Click" Text="Save" />
         <br /><br />
 <asp:LinkButton ID="supplierCreate" runat="server"  OnClick="supplierCreate_Onclick"><asp:Image ID="Image2" ImageUrl="~/images/plus.png" runat="server" /> Add New Supplier Account</asp:LinkButton>
 <br /> <br /> 
 
        
  <%--<asp:HyperLink ID="supplierCreate" runat="server"> <asp:Image ID="Image1" ImageUrl="~/images/plus.png" runat="server" /> Add New Supplier Account</asp:HyperLink>--%>
    <asp:FormView ID="FormSupAccAdd" runat="server" DataSourceID="ObjectDataSource2"
        EnableModelValidation="True"  DataKeyNames="AccountId"  OnItemUpdating="FormSupAccAdd_ItemUpdating" OnItemUpdated="FormSupAccAdd_Updated"
        OnItemInserting="FormSupAccAdd_ItemInserting" OnItemInserted="FormSupAccAdd_ItemInserted" OnItemCommand="FormSupAccAdd_OnCommand">
    <EditItemTemplate>
        <h4><asp:Image ID="Image7" runat="server" ImageUrl="~/images/content.png" /> Account Insert</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <table>
            
            <tr>
                <td>Bank:</td>
                <td><asp:DropDownList DataSourceID="ObjectBank" ID="ddlBankId" runat="server" DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("BankId") %>'/></td>
            </tr>
            <tr>
                <td>Account Type:</td>
                <td><asp:DropDownList DataSourceID="ObjectAccType" ID="ddlAccountTypeId" runat="server" DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("AccountTypeId") %>'/></td>
            </tr>
            <tr>
                <td>Account Title:</td>
                <td><asp:TextBox ID="AccountTitleTextBox" runat="server" Text='<%# Bind("AccountTitle") %>' Width="700px" />
                <asp:RequiredFieldValidator ID="requireAccTitle" runat="server" Text="*required" 
                Display="Dynamic" ControlToValidate="AccountTitleTextBox"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>AccountName:</td>
                <td><asp:TextBox ID="AccountNameTextBox" runat="server" Text='<%# Bind("AccountName") %>'  Width="700px" />
                <asp:RequiredFieldValidator ID="requireAccName" runat="server" Text="*required" 
                Display="Dynamic" ControlToValidate="AccountNameTextBox"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Account Number:</td>
                <td><asp:TextBox ID="AccountNumberTextBox" runat="server" Text='<%# Bind("AccountNumber") %>' MaxLength="10"  Width="700px" />
                <asp:RequiredFieldValidator ID="requireAccNumber" runat="server" Text="*required" 
                Display="Dynamic" ControlToValidate="AccountNumberTextBox"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegAccNumber"  runat="server" Text="*Number Only & 10 charactor" ControlToValidate="AccountNumberTextBox" 
                 ValidationExpression="^[0-9]{10}"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>Account Branch:</td>
                <td><asp:TextBox ID="AccountBranchTextBox" runat="server" Text='<%# Bind("AccountBranch") %>'  Width="700px" />
                <asp:RequiredFieldValidator ID="requireAccBranch" runat="server" Text="*required" 
                Display="Dynamic" ControlToValidate="AccountBranchTextBox"></asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr>
                <td>Comment</td>
                <td><asp:TextBox ID="txtComment" runat="server" Text='<%# Bind("Comment") %>' TextMode="MultiLine" Rows="8" Width="700px" />
               
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" 
                    CommandName="Update" Text="Update"  SkinID="Green_small"/>
                    
                    &nbsp;
                    <asp:Button ID="UpdateCancelButton" runat="server" 
                    CausesValidation="False" CommandName="UpdateCancel" Text="Cancel" SkinID="White_small" />
                    
                </td>
            </tr>
        </table>
    </EditItemTemplate>
    <InsertItemTemplate>
        <h4><asp:Image ID="Image7" runat="server" ImageUrl="~/images/content.png" /> Account</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <table>
            
            <tr>
                <td>Bank:</td>
                <td><asp:DropDownList DataSourceID="ObjectBank" ID="ddlBankId" runat="server" DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("BankId") %>'/></td>
            </tr>
            <tr>
                <td>Account Type:</td>
                <td><asp:DropDownList DataSourceID="ObjectAccType" ID="ddlAccountTypeId" runat="server" DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("AccountTypeId") %>'/></td>
            </tr>
            <tr>
                <td>Account Title:</td>
                <td><asp:TextBox ID="AccountTitleTextBox" runat="server" Text='<%# Bind("AccountTitle") %>' Width="700px" />
                <asp:RequiredFieldValidator ID="requireAccTitle" runat="server" Text="*required" 
                Display="Dynamic" ControlToValidate="AccountTitleTextBox"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>AccountName:</td>
                <td><asp:TextBox ID="AccountNameTextBox" runat="server" Text='<%# Bind("AccountName") %>' Width="700px" />
                <asp:RequiredFieldValidator ID="requireAccName" runat="server" Text="*required" 
                Display="Dynamic" ControlToValidate="AccountNameTextBox"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>Account Number:</td>
                <td><asp:TextBox ID="AccountNumberTextBox" runat="server" Text='<%# Bind("AccountNumber") %>' MaxLength="10" Width="700px" />
                <asp:RequiredFieldValidator ID="requireAccNumber" runat="server" Text="*required" 
                Display="Dynamic" ControlToValidate="AccountNumberTextBox"  ></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegAccNumber"  runat="server" Text="*Number Only & 10 charactor" ControlToValidate="AccountNumberTextBox" 
                 ValidationExpression="^[0-9]{10}"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td>Account Branch:</td>
                <td><asp:TextBox ID="AccountBranchTextBox" runat="server" Text='<%# Bind("AccountBranch") %>'  Width="700px"/>
                <asp:RequiredFieldValidator ID="requireAccBranch" runat="server" Text="*required" 
                Display="Dynamic" ControlToValidate="AccountBranchTextBox"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>Comment :</td>
                <td><asp:TextBox ID="txtComment" runat="server" Text='<%# Bind("Comment") %>' TextMode="MultiLine" Rows="8" Width="700px" />
               
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="InsertButton" runat="server" CausesValidation="True" 
                    CommandName="Insert" Text="Insert" SkinID="Green_small" />
                    &nbsp;<asp:Button ID="InsertCancelButton" runat="server" 
                    CausesValidation="False" CommandName="Cancel" Text="Cancel" SkinID="White_small" />
                </td>
            </tr>
        </table>
    </InsertItemTemplate>
 
</asp:FormView>
   <br /> 
<%--OnInserted="ObjectDataSource2_Inserted"--%>
<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
    DataObjectTypeName="Hotels2thailand.Suppliers.SupplierAccount" 
    InsertMethod="insertNewSupplierAccount" SelectMethod="getSupplierAccountById" 
    TypeName="Hotels2thailand.Suppliers.SupplierAccount" 
    UpdateMethod="updateSupplierAccount">
    <SelectParameters>
        <asp:QueryStringParameter Name="shrSupplierAc" QueryStringField="acid" 
            Type="Int16" />
    </SelectParameters>
</asp:ObjectDataSource>

<asp:ObjectDataSource ID="ObjectBank" runat="server" 
        SelectMethod="getListBankTitleALL" 
        TypeName="Hotels2thailand.Suppliers.SupplierAccount">
</asp:ObjectDataSource>
    
<asp:ObjectDataSource ID="ObjectAccType" runat="server" 
        SelectMethod="getListAccountTitle" 
        TypeName="Hotels2thailand.Suppliers.SupplierAccount">
</asp:ObjectDataSource>
   
    <asp:gridview ID="gridSupplierAccount" AutoGenerateColumns="False" 
        runat="server"  EnableModelValidation="True"  DataKeyNames="AccountId" OnRowCommand="gridSupplierAccount_RowCommand"
         OnRowDataBound="gridSupplierAccount_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText ="No." ItemStyle-Width ="20px">         
                <ItemTemplate>        
                    <%# Container.DataItemIndex + 1 %>    
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:HyperLinkField DataNavigateUrlFields="AccountId" DataNavigateUrlFormatString="supplier_account_add.aspx?supid={0}&amp;acid={0}" DataTextField="Value" HeaderText="Account Title" />--%>
            <%--<asp:HyperLinkField DataNavigateUrlFields="SupplierId,AccountId" DataNavigateUrlFormatString= "supplier_account_list.aspx?supid={0}&amp;acid={1}"
             DataTextField="AccountTitle" Text="Account Title" HeaderText="Account Name" />--%>
<%--            <asp:HyperLinkField 
             DataTextField="AccountTitle" Text="Account Title" HeaderText="Account Name" />--%>
            <asp:TemplateField HeaderText="Account Name">
                <ItemTemplate>
                    <asp:HyperLink ID="hlAccountName" runat="server"  Text='<%# Eval("AccountName") %>' ></asp:HyperLink>
                    
                </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Account Type">
                <ItemTemplate>
                    <asp:Label ID="hlAccount" runat="server" Text='<%# Eval("AccountTypeTitle") %>'></asp:Label>
                    
                    
                </ItemTemplate>
             </asp:TemplateField>
            <asp:TemplateField HeaderText="Account Number">
                <ItemTemplate>
                    <asp:Label ID="lblAccountNum" runat="server" Text='<%# Eval("AccountNumber") %>'></asp:Label>
                    
                </ItemTemplate>
             </asp:TemplateField>
        </Columns>
    </asp:gridview>

    
     </div>
 </asp:Panel>


 <asp:Panel ID="panelFTP" runat="server" Visible="false"><div id="FTP">
    <h4><img src="../../images/content.png"  alt="image_topic" /> FTP</h4>
 <p class="contentheadedetail">Manage ftp multiple and Comment</p>
 <div id="ftp_insert">
    <table>
    <tr>
     <td>server name:</td>
     <td><asp:TextBox ID="txtServer" runat="server"  Width="350"></asp:TextBox></td>
    </tr>
    <tr>
     <td>usernmee:</td>
     <td><asp:TextBox ID="txtuser" runat="server" Width="350"></asp:TextBox></td>
    </tr>
    <tr>
     <td>password:</td>
     <td><asp:TextBox ID="txtPass" runat="server" Width="350"></asp:TextBox></td>
    </tr>
    <tr>
     <td>port:</td>
     <td><asp:TextBox ID="port" runat="server" Width="350"></asp:TextBox></td>
    </tr>
    <tr>
     <td>Comment:</td>
     <td><asp:TextBox ID="com" TextMode="MultiLine" Rows="5" Width="350" runat="server"></asp:TextBox></td>
    </tr>
    <tr><td colspan="2">
     <asp:Button ID="btnSaveFtp" runat="server" Text="Insert new FTP"  onclick="btnSaveFtp_Click" />
    </td></tr>
    </table>
 </div>
    
 
   <div id="FtpList">
     
     <asp:GridView ID="GVFTP" runat="server" EnableModelValidation="false"  GridLines="None" OnRowDataBound="GVFTP_OnrowDataBound" ShowHeader="false" AutoGenerateColumns="false"  DataKeyNames="FtpId" >
      <Columns>
       <asp:TemplateField>
            <ItemTemplate>
               <%# Container.DataItemIndex + 1 %> 
            </ItemTemplate>
       </asp:TemplateField>
       <asp:TemplateField>
          <ItemTemplate>
             <table><tr><td>Server</td><td colspan="6"><asp:TextBox ID="txtServer" Text='<%# Bind("Server") %>' Width="450" runat="server"></asp:TextBox></td>
             </tr><tr>
             <td>User Name</td><td><asp:TextBox ID="txtUser" runat="server" Text='<%# Bind("UserName") %>' /></td>
             <td>Password</td><td><asp:TextBox ID="txtPass" runat="server" Text='<%# Bind("Password") %>'></asp:TextBox></td>
             <td>Port</td><td><asp:TextBox ID="txtPort" runat="server" Text='<%# Bind("Prot") %>'></asp:TextBox></td>
             </tr><tr>
             <td>Comment</td><td colspan="6"><asp:TextBox TextMode="MultiLine" Rows="8" Width="450" ID="txtCommentFTP" runat="server" Text='<%# Bind("Comment") %>'></asp:TextBox></td>
             </tr></table>
          </ItemTemplate>
       </asp:TemplateField>
      </Columns>
     </asp:GridView>
   </div>
 
 </div></asp:Panel>

 <asp:Panel ID="panelContact" runat="server" Visible="false">
     

     <div class="option_add_left" style=" width:300px">
          
        <asp:Panel ID="panelInsertBox" runat="server" CssClass="productPanel" ClientIDMode="Static">
        <h4><asp:Image ID="Image4" runat="server" ImageUrl="~/images/content.png" /> Insert Box</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
            
            <p><asp:DropDownList ID="dropcatInsert" runat="server" Width="200px"></asp:DropDownList></p>
            <p><asp:TextBox ID="txtTitleName" runat="server" Width="295px"></asp:TextBox></p>
            <p style=" margin:5px 0px 0px 0px; padding:0px;"></p>
            <asp:Button ID="Button1" runat="server" SkinID="Green_small" Text="Add new partner" OnClick="btnSave_Onclick" />
            <asp:Button ID="btnQuickAdd" runat="server" SkinID="Blue_small" ClientIDMode="Static" Text="Quick Add" />
        </asp:Panel>

       <asp:Panel ID="panelcontactList" runat="server" CssClass="productPanel">
       <h4><asp:Image ID="Image7" runat="server" ImageUrl="~/images/content.png" /> Contact List</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p>

     
         <asp:GridView ID="GvDepartment" runat="server"  AutoGenerateColumns="false" ShowHeader="false" ShowFooter="false" DataKeyNames="Key" OnRowDataBound="GvDepartment_OnRowdataBound" SkinID="Nostyle">
            <EmptyDataRowStyle   CssClass="alert_box" />
                            <EmptyDataTemplate>
                                      <div class="alert_inside_GridView">
                                       <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
                                       <p class="alert_box_head">No Payment Plan Record </p>
                                       <p  class="alert_box_detail">Please select at least one.</p>
                                     </div>
                                </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                    <h4><asp:Label ID="Cattitle"  runat="server"  Text='<%#Bind("Value") %>'></asp:Label></h4>
                        <asp:GridView ID="GVContactList" runat="server" AutoGenerateColumns="false" ShowHeader="false" ShowFooter="false" DataKeyNames="staff_id" SkinID="Nostyle"  OnRowDataBound="GVContactList_OnRowDataBound"> 
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div style=" width:100%; text-align:left; margin:0px; padding:2px 2px 10px 2px; display:block;border-bottom:1px solid #eeeee1;">
                                            <asp:Image ID="imgBtGreen"  runat="server" ImageUrl="~/images/greenbt.png" />&nbsp;
                                            <asp:HyperLink ID="lstaff" runat="server" Text='<%# Bind("title") %>' NavigateUrl='<%# String.Format("product.aspx?contactId={0}", Eval("staff_id")) + this.AppendCurrentQueryString() %>'></asp:HyperLink>
                                            
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
         </asp:GridView>
        
       </asp:Panel>     
           
            
           
        
            
                
            
            
     <div style=" clear:both"></div>        
    </div>
    
    
    <div class="option_add_right" style=" width:620px" >
    <asp:Panel ID="screenBlock" runat="server" CssClass="Product_rate_screen_block" Visible="false">
            <div  style="margin:150px 0px 0px 50px; width:80% ">
                       <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
                       <p class="alert_box_head">Condition Selection</p>
                       <p  class="alert_box_detail">Please Select Condition </p>
            </div>
            
        </asp:Panel>
    <h6><asp:Label ID="lblHeadtitle" runat="server"></asp:Label></h6>
       <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
       <ContentTemplate>--%>
       
        <asp:Panel ID="panelContactInsert" runat="server" CssClass="productPanel">
          <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Contact Detail</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
            <table width="100%">
             <tr>
                <td width="15%">Department</td>
                <td><asp:DropDownList ID="DropDep" runat="server" Width="400px"></asp:DropDownList></td>
             </tr>
            <tr>
                <td width="15%">Contact Name</td>
                <td><asp:TextBox ID="txtTitle" runat="server" Width="550px"></asp:TextBox> </td>
            </tr>
            <tr>
                <td width="15%">Phone</td>
                <td>
                <asp:GridView  ID="GVPhoneList" runat="server" ShowFooter="false" ShowHeader="false" AutoGenerateColumns="false" DataKeyNames="phone_id" OnRowDataBound="GVPhoneList_OnRowDataBound" SkinID="ProductList" >
                <Columns>
                    <asp:TemplateField ItemStyle-Width="90%">
                        <ItemTemplate>
                            <p class="headPhoneCat"><asp:Label ID="lablePhone" runat="server"></asp:Label></p>
                            <div id='phone<%# Eval("phone_id") %>' style=" display:none">
                                <table width="100%">
                                <tr>
                                    <td><asp:DropDownList ID="dropPhontCatEdit" runat="server"></asp:DropDownList></td>
                                    <td><asp:TextBox ID="txteditCountryCodeEdit" runat="server" Width="30px" MaxLength="3"  BackColor="#faffbd" Text="66"></asp:TextBox></td>
                                    <td>
                                     <asp:TextBox ID="txteditLocalEdit" runat="server" Width="20px" MaxLength="2" BackColor="#faffbd"></asp:TextBox>
                                    </td>
                                    <td><asp:TextBox ID="txtPhoneEdit" runat="server" ></asp:TextBox></td>
                                    <td><asp:Button ID="btnSavephoneEdit" runat="server" Text="Save" SkinID="Green_small" OnClick="btnSavephoneEdit_OnClick" CommandArgument='<%# Eval("phone_id")+ "," + DataBinder.Eval(Container, "DataItemIndex") %>' CommandName="phoneEdit" />
                                    <asp:Button ID="btnDisable" runat="server" Text="Disable"  SkinID="Green_small" OnClick="btnSavephoneEdit_OnClick" CommandArgument='<%# Eval("phone_id")+ "," + DataBinder.Eval(Container, "DataItemIndex") %>' CommandName="phoneDis" />
                                    <asp:Button ID="btnDel" runat="server" Text="Del" SkinID="White_small" OnClick="btnSavephoneEdit_OnClick" CommandArgument='<%# Eval("phone_id")+ "," + DataBinder.Eval(Container, "DataItemIndex") %>' CommandName="PhoneDel"  />
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
                                                                TargetControlID="btnDel"  DisplayModalPopupID="ModalPopupExtender2" />
                                                                <br />
                                                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
                                                                TargetControlID="btnDel" PopupControlID="Panel3" 
                                                                OkControlID="ButtonOks" 
                                                                CancelControlID="ButtonCancels" 
                                                                BackgroundCssClass="modalBackground"  />
                                                                <asp:Panel ID="Panel3" runat="server"  style="display:none; width:200px; background-color:#f2f2f2; border-width:3px; border-color:#3b5998; border-style:solid; padding:20px;">
                                                                    <p style="margin:0px; padding:0px 0px 5px 0px; text-align:left; width:100%;  font-weight:bold; color:Black">Are you sure to Delete</p>
                                                                    <div style="text-align:right;">
                                                                        <asp:Button ID="ButtonOks" runat="server" Text="OK"  SkinID="Green_small" />
                                                                        <asp:Button ID="ButtonCancels" runat="server" Text="Cancel" SkinID="White_small" />
                                                                    </div>
                                                                </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <a href="javaScript:showDiv('phone<%# Eval("phone_id") %>')">Edit</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                </asp:GridView>
                <div id="linkphone" style=" margin:5px 0px 0px 0px;"><a href="javaScript:showDivTwin('phoneinsert','linkphone')"><asp:Image ID="Image3" runat="server" ImageUrl="~/images/plus_s.png" /> Add Phone</a></div>
               <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                <div id="phoneinsert"  style=" display:none">
                    <table>
                        <tr>
                            <td><asp:DropDownList ID="dropPhontCat" runat="server" OnSelectedIndexChanged="dropPhontCat_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                            <td><asp:TextBox ID="txteditCountryCode" runat="server" Width="30px" MaxLength="3"  BackColor="#faffbd" Text="66"></asp:TextBox></td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txteditLocal" runat="server" Width="20px" MaxLength="2" BackColor="#faffbd"></asp:TextBox>
                                     </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger  ControlID="dropPhontCat"/>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td><asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox></td>
                            <td><asp:Button ID="btnSavephone" runat="server" Text="Save" SkinID="Green_small" OnClick="btnSavephone_OnClick" />
                           <a href="javaScript:showDivTwin('phoneinsert','linkphone')">cancel</a>
                    </td>
                        </tr>
                    </table>
                    
                </div>
                    <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
                </td>
            </tr>
            <tr>
                <td width="15%">Email</td>
                <td>
                    <asp:GridView ID="GvEmail" runat="server" ShowFooter="false" ShowHeader="false" AutoGenerateColumns="false"  DataKeyNames="email_id" OnRowDataBound="GvEmail_OnRowDataBound" SkinID="ProductList">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="90%">
                            <ItemTemplate>
                                <p class="headPhoneCat"><asp:Label ID="LabelEmail" runat="server"></asp:Label></p>
                                
                                <div id='email<%# Eval("email_id") %>' style=" display:none; margin:5px 0px 0px 0px;">
                                 <asp:TextBox ID="txteditemailedit" runat="server" Text='<%# Bind("Email") %>' Width="230px"></asp:TextBox>
                                <asp:Button ID="btnEmailsaveedit" runat="server" Text="Save" SkinID="Green_small" OnClick="btnEmailsaveedit_OnClick" CommandArgument='<%# Eval("email_id")+ "," + DataBinder.Eval(Container, "DataItemIndex") %>' CommandName="EmailEdit" />
                                <asp:Button ID="btnEmailsaveeditDis" runat="server" Text="Disable" SkinID="Green_small" OnClick="btnEmailsaveedit_OnClick" CommandArgument='<%# Eval("email_id")+ "," + DataBinder.Eval(Container, "DataItemIndex") %>' CommandName="EmailDis" />
                                <asp:Button ID="btnEmailsaveeditDel" runat="server" Text="Del" SkinID="White_small" OnClick="btnEmailsaveedit_OnClick" CommandArgument='<%# Eval("email_id")+ "," + DataBinder.Eval(Container, "DataItemIndex") %>' CommandName="Emaildel" />   
                                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
                                                                TargetControlID="btnEmailsaveeditDel"  DisplayModalPopupID="ModalPopupExtender2" />
                                                                <br />
                                                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
                                                                TargetControlID="btnEmailsaveeditDel" PopupControlID="Panel3" 
                                                                OkControlID="ButtonOks" 
                                                                CancelControlID="ButtonCancels" 
                                                                BackgroundCssClass="modalBackground"  />
                                                                <asp:Panel ID="Panel3" runat="server"  style="display:none; width:200px; background-color:#f2f2f2; border-width:3px; border-color:#3b5998; border-style:solid; padding:20px;">
                                                                    <p style="margin:0px; padding:0px 0px 5px 0px; text-align:left; width:100%;  font-weight:bold; color:Black">Are you sure to Delete</p>
                                                                    <div style="text-align:right;">
                                                                        <asp:Button ID="ButtonOks" runat="server" Text="OK"  SkinID="Green_small" />
                                                                        <asp:Button ID="ButtonCancels" runat="server" Text="Cancel" SkinID="White_small" />
                                                                    </div>
                                                                </asp:Panel>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <a href="javaScript:showDiv('email<%# Eval("email_id") %>')">Edit</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div id="linkemail" style="margin:5px 0px 0px 0px;"><a href="javaScript:showDivTwin('emailinsert','linkemail')"><asp:Image ID="imgplus" runat="server" ImageUrl="~/images/plus_s.png" /> Add Email</a></div>
                <div id="emailinsert" style=" display:none">
                    
                    <asp:TextBox ID="txteditemail" runat="server" Text='<%# Bind("Email") %>' Width="265px"></asp:TextBox>
                    <asp:Button ID="btnEmailsave" runat="server" Text="Save" SkinID="Green_small" OnClick="btnEmailsave_OnClick" />
                    <a href="javaScript:showDivTwin('emailinsert','linkemail')">cancel</a>
                    
                </div>
                </td>
            </tr>
            <tr>
                <td width="15%">Comment</td>
                <td><asp:TextBox ID="txtCommentContact" runat="server" TextMode="MultiLine" Rows="5" Width="550px"></asp:TextBox></td>
            </tr>
            <tr>
                <td width="15%">Status</td>
                <td><asp:RadioButton ID="radiocontactenable" Text="Enable" runat="server" GroupName="contactstatus" Checked="true" /><asp:RadioButton ID="radiocontactDisable" Text="Diable" runat="server" GroupName="contactstatus" /></td>
            </tr>
         </table>

         <br /><br />

         <asp:Button ID="btnContactsave" runat="server" Text="Save"  SkinID="Green" OnClick="btnContactsave_Onclick"/>
         
        </asp:Panel>
      
       <%--</ContentTemplate>
        </asp:UpdatePanel>--%>  
    </div>    

 </asp:Panel>


 <asp:Panel ID="panelCom" runat="server" Visible="false">
     <div id="Commission">

          <h4><img src="../../images/content.png"  alt="image_topic" /> Commission</h4>
            <p class="contentheadedetail">Manage commission and activity</p>

         <div id="com_left">
             <asp:Label ID="date_Created" runat="server"></asp:Label>
           <table width="100%">
            <tr>
             <td style="width:100px">Commission Expired: </td>
             <td>

                 <DateTime:DatePicker_Add_Edit ID="dateExpired"  runat="server"  />

             </td>
            </tr>
            <tr>
             <td style="width:100px">Comission Cat: </td>
             <td><asp:DropDownList ID="dropCat" runat="server" Width="100" ClientIDMode="Static" AutoPostBack="true" BackColor="#faffbd" OnSelectedIndexChanged="dropCat_SelectedIndexChanged"></asp:DropDownList>

                 

             </td>
            </tr>
               <tr>
           <td style="width:100px">Commission</td>
               <td>
                   
                   <asp:Panel runat="server" ClientIDMode="Static" ID="revenue_com_step">
                       
                     <table runat="server" ID="tbl_Com_coll">
                         <tr>
                             <th>Max</th>
                             <th>Com Amount</th>
                         </tr>
                         <tr>
                             <td><asp:TextBox ID="txtPricerange1" runat="server" ClientIDMode="Static"  CssClass="Extra_textbox_yellow" EnableTheming="false"></asp:TextBox></td>
                             <td><asp:TextBox ID="Comreal1" runat="server" ClientIDMode="Static" CssClass="Extra_textbox_yellow" EnableTheming="false"></asp:TextBox></td>
                         </tr>
                          <tr>
                             <td><asp:TextBox ID="txtPricerange2" runat="server" ClientIDMode="Static" CssClass="Extra_textbox_yellow" EnableTheming="false"></asp:TextBox></td>
                             <td><asp:TextBox ID="Comreal2" runat="server" ClientIDMode="Static" CssClass="Extra_textbox_yellow" EnableTheming="false"></asp:TextBox></td>
                         </tr>
                          <tr>
                             <td><asp:TextBox ID="txtPricerange3" runat="server" ClientIDMode="Static" CssClass="Extra_textbox_yellow" EnableTheming="false"></asp:TextBox></td>
                             <td><asp:TextBox ID="Comreal3" runat="server" ClientIDMode="Static" CssClass="Extra_textbox_yellow" EnableTheming="false"></asp:TextBox></td>
                         </tr>
                          <tr>
                             <td><asp:TextBox ID="txtPricerange4" runat="server" ClientIDMode="Static" CssClass="Extra_textbox_yellow" EnableTheming="false"></asp:TextBox></td>
                             <td><asp:TextBox ID="Comreal4" runat="server" ClientIDMode="Static" CssClass="Extra_textbox_yellow" EnableTheming="false"></asp:TextBox></td>
                         </tr>
                     </table>
                 </asp:Panel>

                   <asp:Panel ClientIDMode="Static" runat="server" ID="com_val">
                       <asp:TextBox ID="txtComVal" runat="server" ClientIDMode="Static"  CssClass="Extra_textbox_yellow" EnableTheming="false"></asp:TextBox>
                       <br />
                                <br />
                                <div id="divCalCom" style="display: none">
                                    <table>
                                        <tr>
                                            <td>Net rate : 
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNetrate" ClientIDMode="Static" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Mark up (%) : 
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMarkup" ClientIDMode="Static" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Sale price : 
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSaleprice" ClientIDMode="Static" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                   </asp:Panel>
               </td>
                   </tr>
               <tr><td colspan="2">
     <asp:Button ID="btnSave" runat="server" Text="Save"  SkinID="Green"  OnClick="btnSave_Click" />
    </td></tr>
            </table>
         </div>
         <div id="com_right">
             <asp:Literal ID="activity" runat="server"></asp:Literal>
             <asp:TextBox ID="txt_activity" TextMode="MultiLine" Rows="5" Width="300px" runat="server"></asp:TextBox>
             <br /><br />
             <asp:Button ID="btnSaveAc" runat="server" OnClick="btnSaveAc_Click" Text="Save" SkinID="Green" />
         </div>
         

     </div>
     <div style="clear:both;"> </div>
 </asp:Panel>
    <div style="clear:both;"> </div>
  <asp:Panel ID="panelSales" runat="server" Visible="false">
      
      <asp:Panel ID="Sales_ref" runat="server">
          <table>
         <tr><td>Name: </td>
         <td><asp:TextBox ID="txtSalesName" Width="450" runat="server"></asp:TextBox></td>

         </tr>
         
         <tr>
         <td>Commission Type: </td>
         <td><asp:DropDownList ID="dropComType" runat="server" Width="450" ></asp:DropDownList></td>
         </tr>
            <tr>
         <td><strong>Commission Value </strong></td>
         <td><asp:TextBox ID="txtComvalues" Width="200" runat="server" BackColor="#faffbd"></asp:TextBox></td>
         </tr>
         
          <tr><td>Phone </td>
         <td><asp:TextBox ID="txtSalePhone" runat="server" Width="450"></asp:TextBox><span style=" font-size:11px; color:Green"> * Require!</span></td>
         </tr>
             <tr><td>Fax </td>
         <td><asp:TextBox ID="txtFax" runat="server" Width="450"></asp:TextBox><span style=" font-size:11px; color:Green"> * Require!</span></td>
         </tr>
            <tr><td>Email </td>
         <td><asp:TextBox ID="txtmail" runat="server" Width="450"></asp:TextBox><span style=" font-size:11px; color:Green"> * Require!</span></td>
         </tr>
        
         <tr>
         <td>Comment: </td>
         <td><asp:TextBox ID="TextBox3" TextMode="MultiLine" runat="server" Rows="8" Width="450" ></asp:TextBox></td>
         </tr>
         
        </table>

      </asp:Panel>
      <asp:Panel ID="panelBHT" runat="server" Visible="false">
          <h4>BLUESHOUE DIRECT CONTRACT</h4>
      </asp:Panel>

  </asp:Panel>
 
 <asp:Panel ID="panelMailSetting" runat="server" Visible="false">

     Latest Modify :<asp:Label ID="Datemodify" runat="server" ></asp:Label>
     <table border="0" class="formsetting">
    <tr>
    <td class="formtitle">Host</td>
    <td class="textboxform"><asp:TextBox ID="host" runat="server" TextMode="SingleLine" ToolTip="Host to send" Width="300px" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="valRequireDESPACK" runat="server" ControlToValidate="host"
                        Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
                        ToolTip="The Title field is required.">The Title field is required.</asp:RequiredFieldValidator>
    </td>
    </tr>
         <tr>
             <td class="formtitle">Port</td>
             <td class="textboxform"><asp:TextBox ID="txtPort" runat="server" TextMode="SingleLine" ToolTip="port to send" Width="50px" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPort"
                        Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
                        ToolTip="The Title field is required.">The Title field is required.</asp:RequiredFieldValidator></td>
         </tr>
         <tr>
             <td class="formtitle">SSL</td>
             <td class="textboxform">
                 <asp:RadioButtonList ID="radioSSL" runat="server">
                     <asp:ListItem Text="Yes" Value="True" Selected="True"></asp:ListItem>
                     <asp:ListItem Text="No" Value="False"></asp:ListItem>
                 </asp:RadioButtonList>

             </td>
         </tr>
    <tr>
    <td class="formtitle">Mail User</td>
    <td class="textboxform"><asp:TextBox ID="mailUser" runat="server" TextMode="SingleLine" ToolTip="mailuser login" Width="300px"></asp:TextBox>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="mailUser"
                        Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
                        ToolTip="The Title field is required.">The Title field is required.</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
    <td class="formtitle">Mail Password</td>
    <td class="textboxform"><asp:TextBox ID="mailPass" runat="server" TextMode="SingleLine" ToolTip="mailuser Password" Width="300px"></asp:TextBox>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="mailPass"
                        Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
                        ToolTip="The Title field is required.">The Title field is required.</asp:RequiredFieldValidator></td>
    </tr>
   <tr>
    <td class="formtitle">MailDisplay</td>
    <td class="textboxform"><asp:TextBox ID="MailDisplay" runat="server" TextMode="SingleLine" ToolTip="MailDisplaytoCustomer" Width="300px"></asp:TextBox>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="MailDisplay"
                        Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
                        ToolTip="The Title field is required.">The Title field is required.</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
    <td class="formtitle">Name Display</td>
    <td class="textboxform"><asp:TextBox ID="namedisplay" runat="server" TextMode="SingleLine" ToolTip="NameDisplaytoCustomer" Width="300px"></asp:TextBox>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="namedisplay"
                        Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
                        ToolTip="The Title field is required.">The Title field is required.</asp:RequiredFieldValidator></td>
    </tr>
  </table>
  <p>Sending Config Time delay per one Newsletter ** milisecond 0- 999 ms. only!!!</p>
  <table border="0" class="formsetting">
    <tr>
    <td class="formtitle">Time delay</td>
    <td class="textboxform"><asp:TextBox ID="timedelay" runat="server" TextMode="SingleLine" ToolTip="Host to send" MaxLength="3"></asp:TextBox>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="timedelay"
                        Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
                        ToolTip="The Title field is required.">The Title field is required.</asp:RequiredFieldValidator>
    </td>
    </tr>
      <tr>
             <td class="formtitle">Active Customer Or Test ?</td>
             <td class="textboxform">
                 <asp:RadioButtonList ID="RadioIsActive" runat="server">
                     <asp:ListItem Text="Active" Value="True" Selected="True"></asp:ListItem>
                     <asp:ListItem Text="Inactive" Value="False"></asp:ListItem>
                 </asp:RadioButtonList>

             </td>
         </tr>
  </table>
  <asp:Button ID="settingUpdate" runat="server" Text="Update" 
        onclick="settingUpdate_Click" />
 </asp:Panel>
 
 
 
</asp:Content>
    
   
