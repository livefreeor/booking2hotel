using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Security;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Threading;
using Hotels2thailand;
using Hotels2thailand.Front;
using Hotels2thailand.Newsletters;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class SendNewsletter : Hotels2BasePageExtra
    {

        //public TextBox PublicTextBody
        //{
        //    get{return this.txtPlainTextBody;}
        //}
        public string qMailCat
        {
            get { return Request.QueryString["mc"]; }
        }
        public string qNewId
        {
            get { return Request.QueryString["ID"]; }
        }

        //public AjaxControlToolkit.HTMLEditor.Editor PublicHtmlBody
        //{
        //    get { return this.txtHtmlBody; }
        //}

        public TextBox Subjecttxt
        {
            get{return this.txtSubject;}
        }

        protected void txtDescricao_HtmlEditorExtender_ImageUploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {
            //htmlEditor.AjaxFileUpload.SaveAs(@"~\\Container\\temp\\" + e.FileName);
            //e.PostedUrl = Page.ResolveUrl(@"~\\Container\\temp\\" + e.FileName);

           
          
            
            // Generate file path
            string foldertemp = "~/Upload/" + this.CurrentProductActiveExtra + "/";
            DirectoryInfo Folder = new DirectoryInfo(Server.MapPath(foldertemp));
            if (!Folder.Exists)
                Folder.Create();

            
            //get the file name of the posted image
            string imgName = DateTime.Now.ToString("yyyy-MM-d-hh-mm-ss")+ "-" + e.FileName;
            
            
            // Save uploaded file to the file system

            var ajaxFileUpload = (AjaxControlToolkit.AjaxFileUpload)sender;
            
            ajaxFileUpload.SaveAs(Server.MapPath(foldertemp + imgName));

            //e.PostedUrl = this.Master.Page.ResolveUrl(filePath);
            // Update client with saved image path
            e.PostedUrl = Page.ResolveUrl(foldertemp + imgName);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            //var ajaxFileUpload = txtDescricao_HtmlEditorExtender.AjaxFileUpload;
            //ajaxFileUpload.AllowedFileTypes = "jpg,jpeg";
            //ajaxFileUpload.cl = "3333";
            
            bool isSending = false;
            int intThredId = 0;
            Newsletter.Lock.AcquireReaderLock(Timeout.Infinite);
            isSending = Newsletter.IsSending;
            intThredId = Newsletter.ThreadID;
            Newsletter.Lock.ReleaseReaderLock();

          

            //char dd = Thread.CurrentThread.Name[intThredId];
            //Response.Write(intThredId + "<br/>");
            //Response.Write(Thread.CurrentThread.ManagedThreadId + "<br/>");

            //if (Newsletter.ThreadNews != null)
            //{
            //    Response.Write(Newsletter.ThreadNews.IsAlive);
            //    Response.Write(Newsletter.ThreadNews.ManagedThreadId);
            //    Response.Write(Newsletter.ThreadNews.IsThreadPoolThread);
            //}

            
           
            //Response.Write(Thread.CurrentThread.);
            //Response.Write(Newsletter.thread.IsAlive);
            //hlMailSetting.NavigateUrl = "newslettersSetting.aspx?mc=" + this.qMailCat;
            //hlTemp.NavigateUrl = "newslettertemplate.aspx?mc=" + this.qMailCat;


            switch (this.qMailCat)
            {
                
                case "5":
                    titles.Text = "All Customer";
                    break;
                case "7":
                    titles.Text = "Member Only";
                    break;
                
            }
            

           
            if (!this.IsPostBack)
            {
               
                if (string.IsNullOrEmpty(this.qNewId))
                {

                    string Theme = string.Empty;


                    string Theme_file = "mail_template_en.html";


                    StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath("~/admin/booking/BookingPrintAndMail_Template/" + Theme_file + ""));
                    string read = objReader.ReadToEnd();
                    objReader.Close();
                    Theme = Utility.GetKeywordReplace(read, "<!--##@MailContentNewsletterStart##-->", "<!--##@MailContentNewsletterEnd##-->");
                    

                    fckeditor.Visible = true;


                    string MainBody = Theme;

                    Production.ProductPic cProductPic = new Production.ProductPic();
                    int intProductId = this.CurrentProductActiveExtra;
                    string Logo = "<img  src=\"" + cProductPic.getProductlogo(this.CurrentProductActiveExtra) + "\" style=\"width:150px; height:60px; border:solid 1px #eee5be\" />";

                    //Logo Is Open

                    MainBody = MainBody.Replace("<!--##@HotelLogo##-->", Logo);

                    Production.Product cProduct = new Production.Product();
                    cProduct = cProduct.GetProductById(intProductId);
                    Production.ProductContent cProductContent = new Production.ProductContent();
                    cProductContent = cProductContent.GetProductContentById(intProductId,1);

                    //BookingProductDisplay cBookingPLsit = new BookingProductDisplay();
                    //cBookingPLsit = cBookingPLsit.getBookingProductDisplayByBookingProductId(this.cClassBookingProduct.BookingProductId);

                    //BookingProductName
                    MainBody = MainBody.Replace("<!--##@HotelName##-->", cProductContent.Title);
                    //BookingProductName
                    MainBody = MainBody.Replace("<!--##@HotelName_foot##-->", "Booking2hotels.com");
                    //Hotel Phone
                    MainBody = MainBody.Replace("<!--##@HotelAddress##-->", cProductContent.Address);
                    //HOtel Address
                    MainBody = MainBody.Replace("<!--##@HotelPhone##-->", cProduct.ProductPhone);

                    StringBuilder result = new StringBuilder();

                    result.Append("<tr><td  style=\"min-height:300px\"><br/><br/><br/>Please insert content this here<br/><br/><br/></td></tr>");
                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                    result.Append("<tr><td style=\"height:10px;\" height=\"10\"></td></tr>");
                    result.Append("<tr>");
                    result.Append("<td style=\" font-family:Tahoma;\">");
                    result.Append("In case you have any other enquiries, call to " + cProduct.ProductPhone + "");

                    result.Append("</td>");
                    result.Append("</tr>");
                    result.Append(" <tr><td style=\"height:10px;\" height=\"10\"></td></tr>");

                    result.Append("<tr>");
                    result.Append("<td style=\" font-family:Tahoma; font-size:22px;color:#bd9663 \">");
                    result.Append("Best Regards,");
                    result.Append("</td>");
                    result.Append("</tr>");

                    result.Append("</td>");
                    result.Append("</tr>");
                    result.Append("</table>");


                    MainBody = MainBody + result.ToString();


                    txtHtmlBody.Text = MainBody;

                    //ContactUsPhone <!--##@HotelPhoneContact##-->
                   // MainBody = MainBody.Replace("<!--##@HotelPhoneContact##-->", cProduct.);

                    //txtHtmlBody.ActiveMode = AjaxControlToolkit.HTMLEditor.ActiveModeType.Preview;
                    //textbox.Visible = false;
                    //txtHtmlBody.Visible = true;
                    //txtPlainTextBody.Visible = false;

                    

                    //btnUpdate.Visible = false;
                    //btnreSend.Visible = false;
                    btnReSubmit.Visible = false;
                    //HtmlType.Checked = true;

                    panelMailType.Visible = true;

                }
                else
                {
                    //txtDescricao_HtmlEditorExtender.ac
                    //txtHtmlBody.ActiveMode = AjaxControlToolkit.HTMLEditor.ActiveModeType.Preview;
                    panelMailType.Visible = false;


                     Newsletter cNewsletter = new Newsletter();
                     int newsid = int.Parse(this.qNewId);
                    cNewsletter = cNewsletter.GetNewsletterByID(newsid);
                    int IseverSendcount = cNewsletter.Status_id;

                    if (IseverSendcount == 2 || IseverSendcount == 4)
                    {
                        //btnreSend.Visible = false; 
                    }
                    //else
                    //{
                    //    
                    //}


                    if (cNewsletter.Status_id == 1)
                    {
                         btnReSubmit.Visible = false;
                    }
                    btnSend.Visible = false;
                    //btnSave.Visible = false;


                    string BodyEN = cNewsletter.Subject_Body_EN;
                    string BodyTH = cNewsletter.Subject_Body_TH;
                    string Body =string.Empty;
                    if (!string.IsNullOrEmpty(BodyEN))
                    {
                        Body = BodyEN;
                    }
                    else
                        Body = BodyTH;
                    string[] arrBody = Regex.Split(Body, "#!#");
                    txtSubject.Text = arrBody[0];
                    txtHtmlBody.Text = arrBody[1];
                }
                
            }
           
           
            
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            //default mailcat = 5 ; is (all customer) manual
            byte bytMailCat = 5;
            //if (radioMailTypeMember.Checked)
            //    bytMailCat = 7;

            Newsletter cNewsletter = new Newsletter { 
                 AddedBy = this.CurrentStaffId,
                  Favorite = false,
                  Status_id  = 2,
                  MailCat = bytMailCat,
                  ProductId = this.CurrentProductActiveExtra   
            };

           int id =  cNewsletter.SaveNewsletter(cNewsletter);
           
           if (id > 0)
           {
               NewsletterLang cNewsLang = new NewsletterLang();
               Regex reg = new Regex(@"[ก-ฮ]+");

              if( reg.IsMatch(txtHtmlBody.Text))
                    cNewsLang.LangId = 2;
              else
                    cNewsLang.LangId = 1;

              cNewsLang.NewsletterID = id;
              cNewsLang.Subject = txtSubject.Text;

              cNewsLang.HtmlBody = HttpUtility.HtmlDecode(txtHtmlBody.Text).Replace("src=\"/Upload/", "src=\"http://order.booking2hotels.com/Upload/"); 
          
              cNewsLang.InsertMailBody(cNewsLang);
                
           }

           string AppendQueryString = "";
            if (!string.IsNullOrEmpty(Request.QueryString["pid"]) && !string.IsNullOrEmpty(Request.QueryString["supid"]))
            {
                AppendQueryString  = "&pid=" + Request.QueryString["pid"] + "&supid=" + Request.QueryString["supid"];
            }

            //int id = Newsletter.SaveNewsletter(txtSubject.Text,
            //    txtHtmlBody.Content, 2, chkIsfav.Checked, htmlCheck);
            Server.Transfer("recipientsSelection.aspx?ID=" + id + AppendQueryString + "&mc=" + bytMailCat);
            
        }

        protected void btnReSubmit_Click(object sender, EventArgs e)
        {
            string strid = this.qNewId;

            byte bytMailCat = 5;
            //if (radioMailTypeMember.Checked)
            //    bytMailCat = 7;

            int newsid = int.Parse(strid);
            NewsletterSending cNewSending = new NewsletterSending();
            int IseverSendcount = cNewSending.GetAllSendingList(newsid).Count;

            string AppendQueryString = "";
            if (!string.IsNullOrEmpty(Request.QueryString["pid"]) && !string.IsNullOrEmpty(Request.QueryString["supid"]))
            {
                AppendQueryString = "&pid=" + Request.QueryString["pid"] + "&supid=" + Request.QueryString["supid"];
            }


            if (IseverSendcount > 0)
            {
                bool isSending = false;
                Newsletter.Lock.AcquireReaderLock(Timeout.Infinite);
                isSending = Newsletter.IsSending;
                Newsletter.Lock.ReleaseReaderLock();


                if (isSending)
                {
                    panWait.Visible = true;
                }
                else
                {
                    int id = Newsletter.SendNewsletter();
                    this.Response.Redirect("sendingNewsletter.aspx?" + AppendQueryString.Hotels2LeftClr(1) + "&mc=" + this.qMailCat + "&ID=" + newsid);
                    //Server.Transfer("recipientsSelection.aspx?ID=" + id + AppendQueryString + "&mc=" + bytMailCat);
                    //this.Response.Redirect("sendingNewsletter.aspx?mc=" + bytMailCat + "&" + AppendQueryString.Hotels2LeftClr(1));
                }

            }
            else
                Server.Transfer("recipientsSelection.aspx?ID=" + strid + AppendQueryString + "&mc=" + bytMailCat);



        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
           


        }
        
        protected void TextType_Changed(object sender, EventArgs e)
        {
           
           
        }

        protected void btnreSend_Click(object sender, EventArgs e)
        {
        }

        

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            
        }

        
        protected void btnPreview_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void raioMailTypeGeneral_CheckedChanged(object sender, EventArgs e)
        {

        }
}
}