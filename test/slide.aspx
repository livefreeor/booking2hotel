<%@ Page Language="C#" AutoEventWireup="true" CodeFile="slide.aspx.cs" Inherits="slide" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript" src="../scripts/jquery-1.6.1.js"></script>

  <script type="text/javascript" language="javascript">
      $(document).ready(function () {

          $("#click_slide").click(function () {
              $("#slid").animate({ width: "show",
                  borderleft: "show",
                  borderRight: "show",
                  paddingLeft: "show",
                  paddingRight: "show"
              }, "slow");

              return false;
          });
      });
</script>
<style  type="text/css">

 #slid
 {
     margin:0px;
     padding:0px;
     display:none;
     width:500px;
     height:300px;
     background-color:Blue;
     position:absolute;
 }
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="main">
     <a href="" id="click_slide" >Click</a>
    </div>
    <div id="slid"></div>
    </form>
</body>
</html>
