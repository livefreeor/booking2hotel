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
using Hotels2thailand.Front;
using System.Threading;
using Hotels2thailand;
using Hotels2thailand.Production;
using Hotels2thailand.Affiliate;
using Hotels2thailand.Newsletters;

namespace Hotels2thailand.UI
{
    public partial class RecipientsSelection : Hotels2BasePageExtra
    {
        public string qMailCat
        {
            get { return Request.QueryString["mc"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //bool isSending = false;
            //Newsletter.Lock.AcquireReaderLock(Timeout.Infinite);
            //isSending = Newsletter.IsSending;
            //Newsletter.Lock.ReleaseReaderLock();

            ////title.Text = "Selecting recipients.";
            //if (!this.IsPostBack && isSending)
            //{
            //    panWait.Visible = true;
            //    panSend.Visible = false;
            //}
            
            if (!this.IsPostBack)
            {
                Customer cCustomer = new Customer();
                switch (this.qMailCat)
                {
                    case "5": 
                        panCustomer.Visible = true;
                        lblallcus.Text = cCustomer.getCustomerListAll(this.CurrentProductActiveExtra).Count.ToString();
                        //lblcusSub.Text = cCustomer.getCustomerListSubscribe().Count.ToString();
                        break;

                    case "7":
                        panCustomer.Visible = true;
                        lblallcus.Text = cCustomer.getCustomerMember(this.CurrentProductActiveExtra).Count.ToString();
                        //lblallcus.Text = cCustomer.getCustomerListAll().Count.ToString();
                        //lblcusSub.Text = cCustomer.getCustomerMember(this.CurrentProductActiveExtra).Count.ToString();
                        break;
                    
                }
                
               
            }

        }

        

        
        


        //protected SendNewsletter prev;
        protected void Send_Newsletter_Click(object sender, EventArgs e)
        {
            int News_id = int.Parse(this.Request.QueryString["ID"]);

            bool isSending = false;
            Newsletter.Lock.AcquireReaderLock(Timeout.Infinite);
            isSending = Newsletter.IsSending;
            Newsletter.Lock.ReleaseReaderLock();
            Newsletter cNew = new Newsletter();
            NewsletterSending cNewSending = new NewsletterSending();
            Customer cCustomer = new Customer();
            int count = 0;
            int intFacGroup = 1;

            IList<object> iListCus = null;

            NewslettersSetting cMailSetting = new NewslettersSetting();
            cMailSetting = cMailSetting.GetSettingbyId(this.CurrentProductActiveExtra);

            

            switch (this.qMailCat)
            {
                case "5":
                    //Check Active User Customer ; if not active defult by BlueHouse ' s Staff ;3449
                    if (cMailSetting.IsActive)
                        iListCus = cCustomer.getCustomerListAll(this.CurrentProductActiveExtra);
                    else
                        iListCus = cCustomer.getCustomerListAll(3449);
                    //panCustomer.Visible = true;

                    foreach (Customer mailItem in iListCus)
                        {
                            //if (Hotels2String.IsMatchEmail(mailItem.Email.Trim()))
                            //{
                                if (intFacGroup == 50)
                                    intFacGroup = 1;

                                NewsletterSending cNewInsert = new NewsletterSending
                                {
                                    Cus_id = mailItem.CustomerID,
                                    Newsletter_id = News_id,
                                    EmailTosend = mailItem.Email.Trim(),
                                    Sending = false,
                                    Is_Sent = false,
                                    FacGroup = intFacGroup
                                };

                                // for this condition cus_id = 0 ,Coz there are no people from DataBase
                                int insertNewsletter = cNewSending.InsertNewsletterSending(cNewInsert);
                                count = count + 1;
                                intFacGroup = intFacGroup + 1;
                            //}
                            //else
                            //{

                            //    cNew.insertSendingEmailRecordfromCustomer(mailItem.CustomerID, null, mailItem.Email, News_id);
                            //}
                        }

                    break;

                case "7":

                    //panCustomer.Visible = true;
                    //Check Active User Customer ; if not active defult by BlueHouse ' s Staff ;3449
                    if (cMailSetting.IsActive)
                        iListCus = cCustomer.getCustomerMember(this.CurrentProductActiveExtra);
                    else
                        iListCus = cCustomer.getCustomerMember(3449);

                    foreach (Customer mailItem in iListCus)
                    {
                        //if (Hotels2String.IsMatchEmail(mailItem.Email.Trim()))
                        //{
                            if (intFacGroup == 50)
                                intFacGroup = 1;
                            NewsletterSending cNewInsert = new NewsletterSending
                            {
                                Cus_id = mailItem.CustomerID,
                                Newsletter_id = News_id,
                                EmailTosend = mailItem.Email.Trim(),
                                Sending = false,
                                Is_Sent = false,
                                FacGroup = intFacGroup
                            };

                            // for this condition cus_id = 0 ,Coz there are no people from DataBase
                            int insertNewsletter = cNewSending.InsertNewsletterSending(cNewInsert);
                            count = count + 1;
                            intFacGroup = intFacGroup + 1;
                        //}
                        //else
                        //{

                        //    cNew.insertSendingEmailRecordfromCustomer(mailItem.CustomerID, null, mailItem.Email, News_id);
                        //}
                    }

                    break;
                
            }

            string AppendQueryString = "";
            if (!string.IsNullOrEmpty(Request.QueryString["pid"]) && !string.IsNullOrEmpty(Request.QueryString["supid"]))
            {
                AppendQueryString = "&pid=" + Request.QueryString["pid"] + "&supid=" + Request.QueryString["supid"];
            }



            Newsletter.SendServiceNewsletter();

            
            this.Response.Redirect("sendingNewsletter.aspx?" + AppendQueryString.Hotels2LeftClr(1) + "&mc=" + this.qMailCat + "&ID=" + News_id);
            
        }

       
}        
}

