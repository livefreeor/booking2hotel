using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using Hotels2thailand;
using Hotels2thailand.Newsletters;

namespace Hotels2thailand.UI
{
    public partial class SendingNewsletter : Hotels2BasePageExtra, ICallbackEventHandler
    {
        string callbackResult = "";
        public string qMailCat
        {
            get { return Request.QueryString["mc"]; }
        }
        public string qNewId
        {
            get { return Request.QueryString["ID"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            

            bool isSending = false;
            Newsletter.Lock.AcquireReaderLock(Timeout.Infinite);
            isSending = Newsletter.IsSending;
            Newsletter.Lock.ReleaseReaderLock();


           

            //hlMailSetting.NavigateUrl = "newslettersSetting.aspx?mc=" + this.qMailCat;
            //hlTemp.NavigateUrl = "newslettertemplate.aspx?mc=" + this.qMailCat;

            // If no newsletter is currently being sent, show a panel with a message saying so.
            // Otherwise register the server-side callback procedure to update the progressbar

            if (!this.IsPostBack && !isSending)
            {
                panNoNewsletter.Visible = true;
                panProgress.Visible = false;
            }
            else
            {
                string callbackRef = this.ClientScript.GetCallbackEventReference(
                   this, "", "UpdateProgress", "null");

                lblScriptName.Text = callbackRef;
                this.ClientScript.RegisterStartupScript(this.GetType(), "StartUpdateProgress",
                   @"<script type=""text/javascript"">CallUpdateProgress();</script>");

                
            }
        }

        public string GetCallbackResult()
        {
            return callbackResult;
        }

        public void RaiseCallbackEvent(string eventArgument)
        {

            NewsletterSending cNewSending = new NewsletterSending();
            //Response.Write(cNewSending.GetCountSendingListCompleted(this.CurrentProductActiveExtra));
            //Response.End();

             byte bytMailCat = byte.Parse(this.qMailCat);
             int intProductId = this.CurrentProductActiveExtra;
           // Newsletter cNewsletter = new Newsletter();
            //List<Newsletter> OutboxRecord = Newsletter.GetNewsletters(4);
            //List<Newsletter> sentRecord = Newsletter.GetNewsletters(3);
            //int intOutbox = cNewsletter.GetCountNewsletters(2, bytMailCat, this.CurrentProductActiveExtra);
            //int intSent = cNewsletter.GetCountNewsletters(1, bytMailCat, this.CurrentProductActiveExtra);
            Newsletter.Lock.AcquireReaderLock(Timeout.Infinite);
            //callbackResult = Newsletter.PercentageCompleted.ToString("N0") + ";" +
            //   Newsletter.SentMails.ToString() + ";" + Newsletter.TotalMails.ToString() + ";" + intOutbox
            //   + ";" + intSent;

            callbackResult = cNewSending.GetCountSendingListCompleted(int.Parse(qNewId));
            Newsletter.Lock.ReleaseReaderLock();

            
        }
    }
}