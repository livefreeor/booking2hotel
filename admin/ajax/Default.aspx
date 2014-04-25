<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="admin_ajax_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../../Scripts/jquery-1.4.2.js"></script>
   <script type="text/javascript" language="javascript">
      
       $(document).ready(function () {

           //$("#option_supplement_add_checkbox_list").after('<input id="hdDateStart" type="hidden" name="hdDateStart" />');
           // $("#txtDateStart").change(DuplicatDatFormat("txtDateStart", "hdDateStart"));

           $("#option_supplement_add_checkbox_list").after('<div style="border:1px solid #000000; width:500px; height:100px;">ssssssssaaaaa</div>');
           

       });

       function checkhd() {
           var hdVal = $("#hdDateStart").val();
           if ($("#hdDateStart").length) {
               alert("HELL0");
           }

       }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="option_supplement_add_checkbox_list">
    
    </div>
    </form>
</body>
</html>
