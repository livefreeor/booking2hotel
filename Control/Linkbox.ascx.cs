using System;
using System.Data;
using System.Configuration;
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
using Hotels2thailand.Staffs;

namespace Hotels2thailand.UI.Controls
{

    public partial class Controls_Linkbox : System.Web.UI.UserControl
    {
        public string qMailCat
        {
            get { return Request.QueryString["mc"]; }
        }
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        public Staff CurrentStaffobj
        {
            get
            {
                StaffSessionAuthorize cStaffAuthorize = new StaffSessionAuthorize();
                return cStaffAuthorize.CurrentClassStaff;
            }
        }
        public int CurrentProductActiveExtra
        {
            get
            {
                Staff cStaff = this.CurrentStaffobj;
                if (cStaff.Cat_Id == 6)
                {
                    return int.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["ProductActive"]);
                }
                else
                    return short.Parse(this.qProductId);

            }
        }

        //string callbackResult = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isSending = false;
            Newsletter.Lock.AcquireReaderLock(Timeout.Infinite);
            isSending = Newsletter.IsSending;
            Newsletter.Lock.ReleaseReaderLock();
            Newsletter cNews = new Newsletter();
            byte bytMailCat = byte.Parse(this.qMailCat);
            int intProductId = int.Parse(Request.QueryString["pid"]);

            

            if (!this.IsPostBack && !isSending)
            {
             
                if (!IsPostBack)
                {
                    string QryString = Request.QueryString["temp"];
                   

                    //List<Newsletter> StoreRecord = Newsletter.GetNewsletters(1);
                    //List<Newsletter> FavoriteRecord = Newsletter.GetNewsletters(2);
                    //List<Newsletter> sentRecord = Newsletter.GetNewsletters(3);
                    //List<Newsletter> OutboxRecord = Newsletter.GetNewsletters(4);
                    //List<Newsletter> newsTemplate = Newsletter.GetNewsletters(5);

                    hlSent.Text = "Sent box&nbsp;(" + cNews.GetCountNewsletters(1, bytMailCat, intProductId) + ")";
                    //lbltemplate.Text = "(" + cNews.GetCountNewsletters(4, bytMailCat) + ")";
                    //lblFavorite.Text = "(" + cNews.GetCountNewsletters(2, bytMailCat) + ")";
                   // lblstroe.Text = "(" + cNews.GetCountNewsletters(0, bytMailCat) + ")";
                    lhOutbox.Text = "Out box&nbsp;(" + cNews.GetCountNewsletters(2, bytMailCat, intProductId) + ")";
                }
             }
            else
            {
                hlSent.Text = "Sent box&nbsp;(" + cNews.GetCountNewsletters(1, bytMailCat, intProductId) + ")";
               // lbltemplate.Text = "(" + cNews.GetCountNewsletters(4, bytMailCat) + ")";
                //lblFavorite.Text = "(" + cNews.GetCountNewsletters(2, bytMailCat) + ")";
                //lblstroe.Text = "(" + cNews.GetCountNewsletters(0, bytMailCat) + ")";
                lhOutbox.Text = "Out box&nbsp;(" + cNews.GetCountNewsletters(2, bytMailCat, intProductId) + ")";
                //List<Newsletter> StoreRecord = Newsletter.GetNewsletters(1);
                //List<Newsletter> FavoriteRecord = Newsletter.GetNewsletters(2);
                //lblFavorite.Text = "(" + cNews.GetCountNewsletters(2,bytMailCat + ")";
                //lblstroe.Text = "(" + cNews.GetCountNewsletters(0,bytMailCat) + ")";
                //lblSent.Visible = false;
               // lblFavorite.Visible = true;
                //lblstroe.Visible = true;
                //lbloutbox.Visible = false;
                
            }

            hlSent.NavigateUrl = "../extranet/newsletter/showNewsletterList.aspx";
            lhOutbox.NavigateUrl = "../extranet/newsletter/showNewsletterList.aspx";
            hlCreatNew.NavigateUrl = "../extranet/newsletter/sendNewsletter.aspx";

            if (!string.IsNullOrEmpty(Request.QueryString["pid"]) && !string.IsNullOrEmpty(Request.QueryString["supid"]))
            {
                string AppendQueryString = "?pid=" + Request.QueryString["pid"] + "&supid=" + Request.QueryString["supid"]
                    + "&mc=" + Request.QueryString["mc"];
                hlSent.NavigateUrl = hlSent.NavigateUrl + AppendQueryString + "&temp=" + 1;
                lhOutbox.NavigateUrl = lhOutbox.NavigateUrl + AppendQueryString + "&temp=" + 2;
                hlCreatNew.NavigateUrl = hlCreatNew.NavigateUrl + AppendQueryString + "&temp=" + 1;
            }
            
        }
         
    }
}


        