<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product_option_holidays_supplement.aspx.cs" Inherits="Hotels2thailand.UI.admin_productOption_product_option_holidays_supplement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
   <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
  <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="../../Scripts/darkman_datepicker.js"></script>
     <script type="text/javascript" src="../../Scripts/darkman_utility.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var qProductId = GetValueQueryString("pid");

            SupplierSelection();

            SupplementInsertbox();

            SuppleMentListOptionANDmenuBar();


           
        });

        function SupplierSelection() {
            var qProductId = GetValueQueryString("pid");
            var qProductCat = GetValueQueryString("pdcid");
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#panelSupplierSelection").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });
            $.get("../ajax/ajax_product_option_holidays_sup_select.aspx?pid=" + qProductId, function (data) {

                $("#panelSupplierSelection").append(data);
                //DarkmanPopUp(550, data);
            });

        }

        function SupplementInsertbox() {
            var qProductId = GetValueQueryString("pid");
            var qProductCat = GetValueQueryString("pdcid");
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#panelsupplementadd").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            $.get("../ajax/ajax_product_option_holidays_sup_insert.aspx?pid=" + qProductId, function (data) {
                $("#panelsupplementadd").html(data);
                DatePicker("txtDateStart");
                DatepickerDual("txtDateStart_01", "txtDateEnd_01");
                $("input[name='issingleDateCheck']").click(function () {
                    if ($(this).val() == "1") {
                        $("#div_single").show();
                        $("#div_twin").hide();
                    }

                    if ($(this).val() == "2") {
                        
                        $("#div_single").hide();
                        $("#div_twin").show();
                    }
                });
            });
        }

        function HolidayTemplate() {
            var qProductId = GetValueQueryString("pid");
            var qProductCat = GetValueQueryString("pdcid");
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#holidays_template_list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            $.get("../ajax/ajax_product_option_holidays_template.aspx?pid=" + qProductId, function (data) {
                $("#holidays_template_list").html(data);
                $("#holidays_template_list table tr:odd").css("background-color", "#eceff5");
                
            });
        }

        function SUpChang() {
            var SupVal = $("#dropSup :selected").val();

            var qProductId = GetValueQueryString("pid");
            var qProductCat = GetValueQueryString("pdcid");
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#panelsupplementadd").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            $.get("../ajax/ajax_product_option_holidays_sup_insert.aspx?pid=" + qProductId + "&supid=" + SupVal, function (data) {
                $("#panelsupplementadd").html(data);
                DatePicker("txtDateStart");
                SuppleMentListOptionANDmenuBar_SupplierChanged();
            });

            
        }

        function InsertOptionCHeck() {
            var $CheckOption = $("#option_supplement_add_checkbox_list");
            var $DropOption = $("#option_supplement_add_dropdown");
            var OptionVal = "";

            if ($CheckOption.css("display") == "block") {

                $("#option_supplement_add_checkbox_list :checked").each(function () {

                    OptionVal = OptionVal + $(this).attr("value") + ";"

                });
            }

            if ($CheckOption.css("display") == "none") {
                OptionVal = $DropOption.find("#ProductOptionList :selected").val() + ";";

                if (OptionVal == "0;") {
                    OptionVal = "";
                    $("#option_supplement_add_checkbox_list :checkbox").each(function () {
                        OptionVal = OptionVal + $(this).attr("value") + ";"
                    });
                }
            }

            return OptionVal;
        }

        function InsertTemplateCheck() {
            var $CheckOption = $("#PublicHolidaysTemplate");
            var Holiday = "";
            $("#PublicHolidaysTemplate :checked").filter(function (index) {

                if ($(this).attr("id") != "headcheck") {
                    Holiday = Holiday + $(this).attr("value") + ";"
                }
                

            });

            return Holiday;
        }

        function InsertOptionSingle() {

            //optionList Select
            var OptionList = InsertOptionCHeck();
            
            if (OptionList == "none;") {
                DarkmanPopUpAlert(400, "Please Select Option To Supplement Before!!");
            }
            else {
                //Supplier Current Selected
                var SupVal = $("#dropSup :selected").val();

                //title single form
                var Title = $("#txttitle").val();
                // dateSuppliement
                var dDate = $("#hd_txtDateStart").val();
                // Amount
                var Amount = $("#txtAmount").val();

                var qProductId = GetValueQueryString("pid");
                var qProductCat = GetValueQueryString("pdcid");
                $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#panelsupplementList").ajaxStart(function () {
                    $(this).show();
                }).ajaxStop(function () {
                    $(this).remove();
                });
                var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
                
                var Url = "../ajax/ajax_product_option_holidays_insert.aspx?pid=" + qProductId + "&type=single&oid=" + OptionList + "&supid=" + SupVal + "&t=" + Title + "&d=" + dDate + "&total=" + Amount;

                $.post(Url, post, function (data) {
                    alert(data);
                    if (data == "true") {
                        SuppleMentList_SupplierChecnged();

                        SupplementInsertbox();
                    }
                });
            }


        }

        function InsertOptionTemplate() {
            //optionList Select
            var OptionList = InsertOptionCHeck();
            var TemplateCheck = InsertTemplateCheck();

            if (OptionList == "none;") {
                DarkmanPopUpAlert(400, "Please Select Option To Supplement Before!!");
            }
            else {
                //Supplier Current Selected
                var SupVal = $("#dropSup :selected").val();

                //title single form
                var Title = $("#txttitle").val();
                // dateSuppliement
                var dDate = $("#hd_txtDateStart").val();
                // Amount
                var Amount = $("#txtAmount").val();

                var qProductId = GetValueQueryString("pid");
                var qProductCat = GetValueQueryString("pdcid");
                $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#panelsupplementList").ajaxStart(function () {
                    $(this).show();
                }).ajaxStop(function () {
                    $(this).remove();
                });
                var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
                var Url = "../ajax/ajax_product_option_holidays_insert.aspx?pid=" + qProductId + "&type=template&oid=" + OptionList + "&supid=" + SupVal + "&t=" + Title + "&d=" + dDate + "&hol=" + TemplateCheck;


                $.post(Url, post, function (data) {
                    if (data == "true") {
                        SuppleMentList_SupplierChecnged();
                        $("#holidays_template_list").html("");
                        $("#holidays_template_list").slideUp();
                    }
                    //$("#panelsupplementadd").html(data);
                });
            }
        }






        function SuppleMentListOptionANDmenuBar_SupplierChanged() {
            // Supplier Selected
            var SupVal = $("#dropSup :selected").val();
            //alert(SupVal);
            //get queryString for ProductId
            var qProductId = GetValueQueryString("pid");

            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#SupplementMenuoption").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            $.get("../ajax/ajax_product_option_holidays_list.aspx?pid=" + qProductId + "&supid=" + SupVal, function (data) {

                $("#SupplementMenuoption").html(data);
                SuppleMentList_SupplierChecnged();

            });
        }
        function SuppleMentListOptionANDmenuBar() {
            // Supplier Selected
            //var SupVal = $("#dropSup :selected").val();

            //get queryString for ProductId
            var qProductId = GetValueQueryString("pid");

            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#SupplementMenuoption").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            $.get("../ajax/ajax_product_option_holidays_list.aspx?pid=" + qProductId, function (data) {

                $("#SupplementMenuoption").html(data);
                SuppleMentList();

            });
        }

        function SupplementUpdate() {
            // Supplier Selected
            var SupVal = $("#dropSup :selected").val();

            //get queryString for ProductId
            var qProductId = GetValueQueryString("pid");

            // OptionList Selected
            var OptionVal = $("#dropOptionList :selected").val();
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#SupplementList").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });
            // YearSelected
            var yearVal = $("#supList_dropyear :selected").val();
            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
            
            $.post("../ajax/ajax_product_option_holidays_sup_update.aspx?pid=" + qProductId + "&oid=" + OptionVal + "&y=" + yearVal + "&supid=" + SupVal + "&status=" + CheckStatusVal(), post, function (data) {
               // 
                if (data == "True") {
                    $.get("../ajax/ajax_product_option_holidays_list.aspx?pid=" + qProductId + "&oid=" + OptionVal + "&y=" + yearVal + "&supid=" + SupVal + "&status=" + CheckStatusVal(), function (data) {
                        $("#SupplementList").html(data);
                        $("#SupplementList table tr:odd").css("background-color", "#eceff5");
                        $("#SupplementList table tr").each(function () {

                            var dateInput = $(this).children("td").children(":text").filter(function (index) { return index == 1 });

                            DatePicker(dateInput.attr("id"));

                        });
                    });
                }

                if (data == "dont") {
                    DarkmanPopUpAlert(400, "No change at least one.");
                }


            });
        }

        function SupplementUpdatestatus() {
            // Supplier Selected
            var SupVal = $("#dropSup :selected").val();

            //get queryString for ProductId
            var qProductId = GetValueQueryString("pid");

            // OptionList Selected
            var OptionVal = $("#dropOptionList :selected").val();
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#SupplementList").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });
            // YearSelected
            var yearVal = $("#supList_dropyear :selected").val();
            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
            $.post("../ajax/ajax_product_option_holidays_sup_update_status.aspx?pid=" + qProductId, post, function (data) {

                if (data == "True") {
                    $.get("../ajax/ajax_product_option_holidays_list.aspx?pid=" + qProductId + "&oid=" + OptionVal + "&y=" + yearVal + "&supid=" + SupVal + "&status=" + CheckStatusVal(), function (data) {
                        $("#SupplementList").html(data);
                        $("#SupplementList table tr:odd").css("background-color", "#eceff5");
                        $("#SupplementList table tr").each(function () {

                            var dateInput = $(this).children("td").children(":text").filter(function (index) { return index == 1 });

                            DatePicker(dateInput.attr("id"));

                        });
                    });
                }
                if (data == "Empty") {
                    DarkmanPopUpAlert(400, "Please select at least one item.");
                }

            });

        }

        function CheckStatusVal() {
            var result = "False";
            var classEnable = $("#holiday_Enable").attr("class");
            var classDisable = $("#holiday_Disable").attr("class");

            if (classEnable == "holiday_Enable") {
                result = "True"
            }
            if (classDisable == "holiday_Enable") {
                result = "False"
            }

            return result;
        }

        function SupListStatus(Statusval, id) {
            // Supplier Selected
            var SupVal = $("#dropSup :selected").val();

            //get queryString for ProductId
            var qProductId = GetValueQueryString("pid");

            // OptionList Selected
            var OptionVal = $("#dropOptionList :selected").val();

            // YearSelected
            var yearVal = $("#supList_dropyear :selected").val();

            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#SupplementList").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            $.get("../ajax/ajax_product_option_holidays_list.aspx?pid=" + qProductId + "&oid=" + OptionVal + "&y=" + yearVal + "&supid=" + SupVal + "&status=" + Statusval, function (data) {
                $("#SupplementList").html(data);
                $("#SupplementList table tr:odd").css("background-color", "#eceff5");
                $("#SupplementList table tr").each(function () {

                    var dateInput = $(this).children("td").children(":text").filter(function (index) { return index == 1 });

                    DatePicker(dateInput.attr("id"));


                    if ($("#" + id).attr("id") == "holiday_Enable") {
                        $("#holiday_Enable").removeClass("holiday_Disable").addClass("holiday_Enable");
                        $("#holiday_Disable").addClass("holiday_Disable");
                    }
                    if ($("#" + id).attr("id") == "holiday_Disable") {
                        $("#holiday_Disable").removeClass("holiday_Disable").addClass("holiday_Enable");
                        $("#holiday_Enable").addClass("holiday_Disable");
                    }
                });
            });
        }

        function SuppleMentList() {
            // Supplier Selected
            var SupVal = $("#dropSup :selected").val();
            //alert(SupVal);
            //get queryString for ProductId
            var qProductId = GetValueQueryString("pid");

            // OptionList Selected
            var OptionVal = $("#dropOptionList :selected").val();

            // YearSelected
            var yearVal = $("#supList_dropyear :selected").val();

            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#SupplementList").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            $.get("../ajax/ajax_product_option_holidays_list.aspx?pid=" + qProductId + "&oid=" + OptionVal + "&y=" + yearVal + "&supid=" + SupVal + "&status=" + CheckStatusVal(), function (data) {
                $("#SupplementList").html(data);
                $("#SupplementList table tr:odd").css("background-color", "#eceff5");
                $("#SupplementList table tr").each(function () {

                    var dateInput = $(this).children("td").children(":text").filter(function (index) { return index == 1 });

                    DatePicker(dateInput.attr("id"));

                });
            });
        }

        function SuppleMentList_SupplierUpdate() {
            // Supplier Selected
            var SupVal = $("#dropSup :selected").val();

            //get queryString for ProductId
            var qProductId = GetValueQueryString("pid");

            // OptionList Selected
            var OptionVal = $("#dropOptionList :selected").val();

            // YearSelected
            var yearVal = $("#supList_dropyear :selected").val();

            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#SupplementList").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            $.get("../ajax/ajax_product_option_holidays_list.aspx?pid=" + qProductId + "&oid=" + OptionVal + "&y=" + yearVal + "&supid=" + SupVal, function (data) {
                $("#SupplementList").html(data);
                $("#SupplementList table tr:odd").css("background-color", "#eceff5");
                $("#SupplementList table tr").each(function () {

                    var dateInput = $(this).children("td").children(":text").filter(function (index) { return index == 1 });

                    DatePicker(dateInput.attr("id"));

                });
            });
        }

        function SuppleMentList_SupplierChecnged() {
            // Supplier Selected
            var SupVal = $("#dropSup :selected").val();

            //get queryString for ProductId
            var qProductId = GetValueQueryString("pid");

            // OptionList Selected
            var OptionVal = $("#dropOptionList :selected").val();

            // YearSelected
            var yearVal = $("#supList_dropyear :selected").val();

            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#SupplementList").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            $.get("../ajax/ajax_product_option_holidays_list.aspx?pid=" + qProductId + "&oid=" + OptionVal + "&y=" + yearVal + "&supid=" + SupVal, function (data) {
                $("#SupplementList").html(data);
                $("#SupplementList table tr:odd").css("background-color", "#eceff5");
                $("#SupplementList table tr").each(function () {

                    var dateInput = $(this).children("td").children(":text").filter(function (index) { return index == 1 });

                    DatePicker(dateInput.attr("id"));

                });
            });
        }

        function SuppleMentList_YearChang() {
            // Supplier Selected
            var SupVal = $("#dropSup :selected").val();

            //get queryString for ProductId
            var qProductId = GetValueQueryString("pid");

            // OptionList Selected
            var OptionVal = $("#dropOptionList :selected").val();

            // YearSelected
            var yearVal = $("#supList_dropyear :selected").val();

            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#SupplementList").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            $.get("../ajax/ajax_product_option_holidays_list.aspx?pid=" + qProductId + "&oid=" + OptionVal + "&y=" + yearVal + "&supid=" + SupVal, function (data) {
                $("#SupplementList").html(data);
                $("#SupplementList table tr:odd").css("background-color", "#eceff5");
                $("#SupplementList table tr").each(function () {

                    var dateInput = $(this).children("td").children(":text").filter(function (index) { return index == 1 });

                    DatePicker(dateInput.attr("id"));

                });
            });
        }

        function YearChang() {
            var yearVal = $("#dropyear :selected").val();
            var qProductId = GetValueQueryString("pid");
            var qProductCat = GetValueQueryString("pdcid");
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#PublicHolidaysTemplate").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            $.get("../ajax/ajax_product_option_holidays_template.aspx?pid=" + qProductId + "&y=" + yearVal, function (data) {
                $("#PublicHolidaysTemplate").html(data);
                $("#holidays_template_list table tr:odd").css("background-color", "#eceff5");
                //                $("#holidays_template_list table tr").each(function () {

                //                    var dateInput = $(this).find(":text").stop();
                //                    DatePicker(dateInput.attr("id"));
                //                    //dateInput.change(DatePicker(dateInput.attr("id")));
                //                });
            });
        }


        function CheckboxChecked(parent, element) {
            
            if ($("#" + parent).is(':checked')) {
                $("#" + element).find(":checkbox").filter(function (index) {

                    if (index != 0) {
                        $(this).attr('checked', 'checked');
                        $(this).click(function () {

                            $("#" + parent).removeAttr('checked');
                        });
                    }
                });
            } else {
                $("#" + element).find(":checkbox").filter(function (index) {

                    if (index != 0) {
                        $(this).removeAttr('checked');
                        $(this).click(function () {

                            $("#" + parent).removeAttr('checked');
                        });
                    }
                });

            }


            //            var main = $("#" + element).filter(function () { 
            //                return 
            //            });
        }
        
	</script>
    
     <h6><asp:Label ID="Destitle" runat="server" ></asp:Label> : <asp:Label ID="txthead" runat="server"></asp:Label></h6>
   
   <div id="panelSupplierSelection" class="productPanel">
        <h4><asp:Image ID="Image3" runat="server" ImageUrl="~/images/content.png" /> Active supplier </h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        
   </div>
    
    <div id="panelsupplementadd_panel" class="productPanel">
        <h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" /> SuppleMent Insert Box</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from <a href="javaScript:showDivTwin('holidays_insert_single','holidays_template_list');HolidayTemplate();">Template Click</a></p><br />
        <%--<input type="button" value="CHECK"  onclick="check('txtDateStart');" />--%>
        <div id="panelsupplementadd"></div>
        <div id="holidays_template_list" style="display:none"></div>
    </div>
    
    
    <div id="panelsupplementList" class="productPanel">
        <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> SupplementDate List</h4>
        <p class="contentheadedetail">List Supplier of This Product, you can Change or Add Supplier to List </p><br />
        <div id="SupplementMenuoption"></div>
        <div id="SupplementList"></div>
    </div>
    
     
</asp:Content>

