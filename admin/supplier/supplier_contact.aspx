<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="supplier_contact.aspx.cs"
 Inherits="Hotels2thailand.UI.admin_supplier_supplier_contact"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" language="javascript" src="../../scripts/jquery-1.7.1.min.js"></script>
 <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
 <script type="text/javascript" language="javascript" src="../../scripts/darkman_utility.js"></script>
<script type="text/javascript" language="javascript">

  

    $(document).ready(function () {
        $("#btnQuickAdd").click(function () {
            
            SuppliercontactAdd();

            return false;
        });
        
    });

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
        result = result + "<input type=\"text\" id=\"txtEmail_"+key+"\" name=\"txtEmail_1\" OnKeyUp=\"GetEmailinsert('" + key + "');return false;\"";
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
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >

       <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>  
    </ContentTemplate>
           
    </asp:UpdatePanel>  --%>
    <h6><asp:Label ID="lblhead" runat="server"></asp:Label></h6>
    
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
                                            <asp:HyperLink ID="lstaff" runat="server" Text='<%# Bind("title") %>' NavigateUrl='<%# String.Format("~/admin/supplier/supplier_contact.aspx?contactId={0}", Eval("staff_id")) + this.AppendCurrentQueryString() %>'></asp:HyperLink>
                                            
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
                            <div id="phone<%# Eval("phone_id") %>" style=" display:none">
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
                            <td><asp:TextBox ID="txtPhone" runat="server" ></asp:TextBox></td>
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
                                
                                <div id="email<%# Eval("email_id") %>" style=" display:none; margin:5px 0px 0px 0px;">
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
                <td><asp:TextBox ID="txtcomment" runat="server" TextMode="MultiLine" Rows="5" Width="550px"></asp:TextBox></td>
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
   
</asp:Content>

