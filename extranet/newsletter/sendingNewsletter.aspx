<%@ Page Title="Hotels2thailand.com - Sending Newsletter" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="SendingNewsletter.aspx.cs" Inherits="Hotels2thailand.UI.SendingNewsletter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript" src="../../scripts/jquery-1.7.1.min.js"></script>
    <link href="/css/newsletter/newsletter.css"type="text/css" rel="Stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div id="left">
        <%--<Ctrl:Linkbox ID="CtrlLinkBox" runat="server" />--%>
    </div>

   <div id="right">
   <asp:Panel ID="panProgress" runat="server">
   <div class="sectiontitle">
      <asp:Literal runat="server" ID="lblSendNewsletter" Text="Sending Newsletter..." />
   </div>
   <p></p>
   <div class="progressbarcontainer">
      <div class="progressbar" id="progressbar"></div>
   </div>
   <br /><br />
   <div id="progressdescription"></div>
   <br /><br />
   <div style="text-align: center; display: none;" id="panelcomplete"></div>
   </asp:Panel>
   
   <asp:Panel ID="panNoNewsletter" runat="server" Visible="false" CssClass="panelminheight" >
      <b>No newsletter is currently being sent.</b>
   </asp:Panel>
   
   <script type="text/javascript">      
      function CallUpdateProgress()
      {
         <asp:Literal runat="server" ID="lblScriptName" />;
      }
      
      function UpdateProgress(result, context) 
      {
          
         // result is a semicolon-separated list of values, so split it
         var params = result.split(";");
         var percentage = params[2];
         var sentMails = params[1];
         var totalMails = params[0];
         var sendcom = params[3];
         var failed = params[4];
        
         
         //console.log(sentMails);
         if (totalMails < 0)
            totalMails = '???';
         
         // update progressbar's width and description text
         var progBar = window.document.getElementById('progressbar');
         progBar.style.width = percentage + '%';


         var descr = window.document.getElementById('progressdescription');
         descr.innerHTML = '<b>' + percentage + '% completed</b> - ' +
            sentMails + ' out of ' + totalMails + ' e-mails have been sent.';
            

          //sentMails + '['+sendcom+']['+failed+']'+ ' out of ' + totalMails + ' e-mails have been sent.';


         //if($("#totaloutbox").length ){
         //    //var outboxtotal = window.document.getElementById('totaloutbox');
         //    //outboxtotal.innerHTML = 

         //    $("#totaloutbox").html("(" + totalOutbox + ")");
         //}

         //if($("#totalsentbox").length)
         //{
         //    //var sentboxtotal = window.document.getElementById('totalsentbox');
         //    //sentboxtotal.innerHTML = "(" + totalSentBox + ")"
         //    $("#totalsentbox").html("(" + totalSentBox + ")");
         //}
         // if the current percentage is less than 100%, 
         // recall the server callback method in 2 seconds
         if (percentage == '100')
            window.document.getElementById('panelcomplete').style.display = '';
         else
            setTimeout('CallUpdateProgress()', 2000);
      }
   </script>
   </div> 

    <div style="clear:both"></div>
</asp:Content>

