using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

using System.Threading;
using System.Web.Profile;
using Hotels2thailand.Newsletters;
using Hotels2thailand.Staffs;
using Hotels2thailand;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Text;


namespace Hotels2thailand.Newsletters
{

    
   public class Newsletter : Hotels2BaseClass
   {
      private int _id;
      private DateTime _addedDate;
      private short _addedBy;
      
      
      
      private DateTime _sentDate;
      private int _status_id;
      private bool _favorite;
      
      private int _totalmailfail;
      private NewslettersSetting _mailsetup;


      public Newsletter() 
      {
          _id               = 0;
          _addedDate        = DateTime.Now;
         
          //_subject          = string.Empty;
          //_plainTextBody    = string.Empty;
          //_htmlBody         = string.Empty;
          _sentDate         = DateTime.Now;
          
          _favorite         = false;
          //_Ishtml           = true;
          _totalmailfail    = 0;
          _mailsetup        = null;
      
      }

     

      public int ID
      {
         get { return _id; }
         private set { _id = value; }
      }

      
      public DateTime AddedDate
      {
          get { return _addedDate; }
         set { _addedDate = value; }
      }

      
      public short AddedBy
      {
         get { return _addedBy; }
          set { _addedBy = value; }
      }

      
      //public string Subject
      //{
      //   get { return _subject; }
      //   set { _subject = value; }
      //}

     
      //public string PlainTextBody
      //{
      //   get
      //   {
      //      if (_plainTextBody == null)
      //         FillBody();
      //      return _plainTextBody;
      //   }
      //   set { _plainTextBody = value; }
      //}

      
      //public string HtmlBody
      //{
      //   get
      //   {
      //      if (_htmlBody == null)
      //         FillBody();
      //      return _htmlBody;
      //   }
      //   set { _htmlBody = value; }
      //}

       public DateTime SentDate
      {
          get { return _sentDate; }
          set { _sentDate = value; }
      }

       public int ProductId { get; set; }

       public byte MailCat { get; set; }
      public int Status_id
      {
          get { return _status_id; }
          set { _status_id = value; }
      }

      

      public bool Favorite
      {
          get { return _favorite; }
          set { _favorite = value; }
      }

      //public bool IshtmlFormat
      //{
      //    get { return _Ishtml; }
      //    set { _Ishtml = value; }
      //}

      public bool Status { get; set; }

      public string Subject_Body_EN { get; set; }
      public string Subject_Body_TH { get; set; }

      public int TotalSendfail
      {
          get
          {
              if (_totalmailfail == 0)
              {
                  NewsletterSending cNewSend = new NewsletterSending();
                  _totalmailfail = cNewSend.GetAllSendingList(this.ID).Count;
              }
              return _totalmailfail;
          }
      }
      public int TotalSend
      {
          get
          {
              
                  NewsletterSending cNewSend = new NewsletterSending();
                  return cNewSend.GetAllSendingListAll(this.ID).Count;
            
          }
      }

      public int TotalSendCompleted
      {
          get
          {
             
                  NewsletterSending cNewSend = new NewsletterSending();
                  return cNewSend.GetAllSendingListCompleted(this.ID).Count;
             
          }
      }
      public int TotalSendOut
      {
          get
          {

              NewsletterSending cNewSend = new NewsletterSending();
              return cNewSend.GetAllSendingListOut(this.ID).Count;
              

          }
      }
      public NewslettersSetting Mailsetup
      {
          get 
          {
              if (_mailsetup == null)
              {
                  NewslettersSetting cNewSet = new NewslettersSetting();
                  _mailsetup = cNewSet.GetSettingbyId(this.ProductId);
              }
                  return _mailsetup;
              
          }
      }

     
    

      

      private void FillBody()
      {
         //NewsletterDetails record = SiteProvider.Newsletters.GetNewsletterByID(this.ID);
         //this.PlainTextBody = record.PlainTextBody;
         //this.HtmlBody = record.HtmlBody;
      }

      /***********************************
      * Static properties
      ************************************/
      public static ReaderWriterLock Lock = new ReaderWriterLock();

      public static Thread ThreadNews = null;

      private static int _threadId = 0;
      public static int ThreadID
      {
          get { return _threadId; }
          private set { _threadId = value; }
      }

      private static bool _isSending = false;
      public static bool IsSending
      {
         get { return _isSending; }
         private set { _isSending = value; }
      }

      private static double _percentageCompleted = 0.0;
      public static double PercentageCompleted
      {
         get { return _percentageCompleted; }
         private set { _percentageCompleted = value; }
      }

      private static int _totalMails = -1;
      public static int TotalMails
      {
         get { return _totalMails; }
         private set { _totalMails = value; }
      }

      private static int _sentMails = 0;
      public static int SentMails
      {
         get { return _sentMails; }
         private set { _sentMails = value; }
      }


     
      /***********************************
      * Static methods
      ************************************/

      


      public List<object> GetNewsletters(byte bytCatId, byte bytStatus_id, int intProductId)
      {
          // define instant of QueryString
        
          using (SqlConnection cn = new SqlConnection(this.ConnectionString))
          {
              SqlCommand cmd = new SqlCommand("SELECT ns.NewsletterID, ns.AddedDate, ns.AddedBy, ns.SentDate, ns.product_id, ns.cat_id, ns.status_id, ns.favorite, ns.status ,ISNULL((SELECT Subject + '#!#' + HtmlBody FROM tbl_newsletter_body_lang nl WHERE lang_id = 1 AND ns.NewsletterID = nl.NewsletterID),''),ISNULL((SELECT Subject + '#!#' + HtmlBody FROM tbl_newsletter_body_lang nl WHERE lang_id = 2 AND ns.NewsletterID = nl.NewsletterID),'') FROM tbl_Newsletters ns WHERE ns.cat_id = @cat_id AND status_id=@status_id AND product_id = @product_id AND status = 1  ", cn);
              cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
              cmd.Parameters.Add("@status_id", SqlDbType.TinyInt).Value = bytStatus_id;
              cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
              cn.Open();
              return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
          }
      }


      public bool UpdateStatusIdNews(int intNewsID, byte bytStatusId)
      {
          using (SqlConnection cn = new SqlConnection(this.ConnectionString))
          {
              SqlCommand cmd = new SqlCommand("UPDATE tbl_Newsletters SET status_id = @status_id WHERE NewsletterID=@NewsletterID", cn);
              cmd.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = intNewsID;
              cmd.Parameters.Add("@status_id", SqlDbType.TinyInt).Value = bytStatusId;
              cn.Open();

              return (ExecuteNonQuery(cmd) == 1);

          }
      }
       public bool UpdateStatusNews(int intNewsID, bool bolStatus)
       {
           using(SqlConnection cn = new SqlConnection(this.ConnectionString))
           {
               SqlCommand cmd = new SqlCommand("UPDATE tbl_Newsletters SET status = @status WHERE NewsletterID=@NewsletterID",cn);
               cmd.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = intNewsID;
               cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
               cn.Open();

               return (ExecuteNonQuery(cmd) == 1);

           }
       }

      public int GetCountNewsletters(int QryType, byte bytNewCat, int intProductId)
      {
          // define instant of QueryString
          
          using (SqlConnection cn = new SqlConnection(this.ConnectionString))
          {
              SqlCommand cmd = new SqlCommand("SELECT COUNT(NewsletterID) FROM tbl_Newsletters Where status_id =@status_id AND cat_id =@cat_id AND product_id=@product_id", cn);
              cmd.Parameters.Add("@status_id", SqlDbType.TinyInt).Value = QryType;
              cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytNewCat;
              cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
              cn.Open();
              return (int)ExecuteScalar(cmd);
          }
      }
      

      /// <summary>
      /// Returns a Newsletter object with the specified ID
      /// </summary>
      public Newsletter GetNewsletterByID(int newsletterID)
      {
         
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
             SqlCommand cmd = new SqlCommand("SELECT ns.NewsletterID, ns.AddedDate, ns.AddedBy, ns.SentDate, ns.product_id, ns.cat_id, ns.status_id, ns.favorite, ns.status ,(SELECT Subject + '#!#' + HtmlBody FROM tbl_newsletter_body_lang nl WHERE lang_id = 1 AND ns.NewsletterID = nl.NewsletterID),(SELECT Subject + '#!#' + HtmlBody FROM tbl_newsletter_body_lang nl WHERE lang_id = 2 AND ns.NewsletterID = nl.NewsletterID) FROM tbl_Newsletters ns WHERE ns.NewsletterID = @NewsletterID", cn);
             cmd.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = newsletterID;
             cn.Open();
             IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
             if (reader.Read())
                 return (Newsletter)MappingObjectFromDataReader(reader);
             else
                 return null;
         }
      }

      /// <summary>
      /// Updates an existing newsletter
      /// </summary>
      public bool UpdateNewsletter(Newsletter newsletter)
      {


          using (SqlConnection cn = new SqlConnection(this.ConnectionString))
          {
              SqlCommand cmd = new SqlCommand("UPDATE tbl_Newsletters SET AddedDate = @AddedDate, SentDate = @SentDate, status_id = @status_id, favorite = @favorite WHERE NewsletterID = @NewsletterID", cn);
              cmd.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = newsletter.ID;
              cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = newsletter.AddedDate;
              //cmd.Parameters.Add("@Subject", SqlDbType.NVarChar).Value = newsletter.Subject;
              //cmd.Parameters.Add("@PlainTextBody", SqlDbType.NText).Value = newsletter.PlainTextBody;
              //cmd.Parameters.Add("@HtmlBody", SqlDbType.NText).Value = newsletter.HtmlBody;
              cmd.Parameters.Add("@SentDate", SqlDbType.DateTime).Value = newsletter.SentDate;
              cmd.Parameters.Add("@status_id", SqlDbType.TinyInt).Value = newsletter.Status_id;
              //cmd.Parameters.Add("@IshtmlFormat", SqlDbType.Bit).Value = newsletter.IshtmlFormat;
              cmd.Parameters.Add("@favorite", SqlDbType.Bit).Value = newsletter.Favorite;
              cn.Open();
              int ret = ExecuteNonQuery(cmd);
              return (ret == 1);
          }
      }

      public bool UpdateNewsletterStatus(int intNewsId, byte bytStatus_id)
      {


          using (SqlConnection cn = new SqlConnection(this.ConnectionString))
          {
              SqlCommand cmd = new SqlCommand("UPDATE tbl_Newsletters SET  status_id = @status_id ,SentDate = @SentDate WHERE NewsletterID = @NewsletterID", cn);
              cmd.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = intNewsId;
              //cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = newsletter.AddedDate;
              //cmd.Parameters.Add("@Subject", SqlDbType.NVarChar).Value = newsletter.Subject;
              //cmd.Parameters.Add("@PlainTextBody", SqlDbType.NText).Value = newsletter.PlainTextBody;
              //cmd.Parameters.Add("@HtmlBody", SqlDbType.NText).Value = newsletter.HtmlBody;
              cmd.Parameters.Add("@SentDate", SqlDbType.DateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
              cmd.Parameters.Add("@status_id", SqlDbType.TinyInt).Value = bytStatus_id;
              //cmd.Parameters.Add("@IshtmlFormat", SqlDbType.Bit).Value = newsletter.IshtmlFormat;
              //cmd.Parameters.Add("@favorite", SqlDbType.Bit).Value = newsletter.Favorite;
              cn.Open();
              int ret = ExecuteNonQuery(cmd);
              return (ret == 1);
          }
      }
      

      /// <summary>
      /// Deletes an existing newsletter
      /// </summary>
      //public static bool DeleteNewsletter(int id)
      //{
      //   bool ret = SiteProvider.Newsletters.DeleteNewsletter(id);
      //   return ret;
      //}

      public static int SendEmailUnsubscribe(int cus_id, string body)
      {
          Newsletter mailsetup = new Newsletter();

          string maildisplay = mailsetup.Mailsetup.Maildisplay;
          string mailNamedisplay = mailsetup.Mailsetup.Displayfrom;
          string mailuser = mailsetup.Mailsetup.Mailuser;
          string mailpass = mailsetup.Mailsetup.Mailpass;
          string host = mailsetup.Mailsetup.Host;

          MailMessage mail = new MailMessage();

          mail.From = new MailAddress(maildisplay, mailNamedisplay);
          mail.To.Add("bht-rd@hotels2thailand.com");
          mail.Subject = "UnsubScribe By Customer ID : "+Convert.ToString(cus_id);

          mail.Body = body;
          mail.IsBodyHtml = true;

          SmtpClient smtpClient = new SmtpClient();
          smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
          smtpClient.Host = host;
          smtpClient.Credentials = new System.Net.NetworkCredential(mailuser, mailpass);
          smtpClient.Send(mail);

          return cus_id;
      }

      /// <summary>
      /// Sends a newsletter
      /// </summary>
      /// 
      public static void SendServiceNewsletter()
      {
          Newsletter.UpdateNewsletterSent();
          if (Newsletter.ThreadNews != null)
          {

              if (Newsletter.ThreadNews.IsAlive)
              {
                  //Newsletter.ThreadNews.Interrupt();
                  Newsletter.ThreadNews.Abort();
                  Newsletter.SendNewsletter();
              }
              else
              {
                  Newsletter.SendNewsletter();
              }

          }
          else
          {
              Newsletter.SendNewsletter();
          }

      }

      public void ServiceUpdateNewsletterSent()
      {
          try
          {
              using (SqlConnection cn = new SqlConnection(this.ConnectionString))
              {
                  StringBuilder query = new StringBuilder();


                  query.Append("UPDATE tbl_Newsletters  SET status_id = 1 WHERE NewsletterID IN ( "); 
                  query.Append(" SELECT n.NewsletterID FROM tbl_Newsletters n  ");
                  query.Append(" WHERE (SELECT COUNT(ns.News_sending_id) FROM tbl_Newsletters_Sending ns WHERE  ns.NewsletterID = n.NewsletterID ) = ");  
                  query.Append(" (SELECT COUNT(ns.News_sending_id) FROM tbl_Newsletters_Sending ns WHERE  ns.NewsletterID = n.NewsletterID AND ns.Sending = 1) ");
                  query.Append(" AND status_id = 2 )");



                  SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                  cn.Open();
                  ExecuteNonQuery(cmd);

              }
          }
          catch(Exception ex) { Hotels2LogWriter.WriteFile("extranet/newsletter/log.html", ex.Message + "<br/>" + ex.StackTrace); }
      }

      public static void NewsUpdate()
      {
          Newsletter cNew = new Newsletter();
          cNew.ServiceUpdateNewsletterSent();
      }

      public static int UpdateNewsletterSent()
      {

        
          //ParameterizedThreadStart pts = new ParameterizedThreadStart(SendEmails);


          Thread Threadupdate = new Thread(new ThreadStart(NewsUpdate));
          //Threadupdate.Name = "SendEmails";
          Threadupdate.Priority = ThreadPriority.BelowNormal;
          Threadupdate.Start();

         // Newsletter.ThreadID = ThreadNews.ManagedThreadId;




          //thread.ManagedThreadId
          return 1;
      }

      

      public static int SendNewsletter()
      {
         

         int news_id = 0;
         Lock.AcquireWriterLock(Timeout.Infinite);
         Newsletter.TotalMails = -1;
         Newsletter.SentMails = 0;
         Newsletter.PercentageCompleted = 0.0;
         Newsletter.IsSending = true;
         Newsletter.ThreadID = 0;
         Lock.ReleaseWriterLock();

         
         object[] parameters = new object[] {news_id};
         ParameterizedThreadStart pts = new ParameterizedThreadStart(SendEmails);


         ThreadNews = new Thread(pts);
         ThreadNews.Name = "SendEmails";
         ThreadNews.Priority = ThreadPriority.BelowNormal;
         ThreadNews.Start(parameters);

         Newsletter.ThreadID = ThreadNews.ManagedThreadId;


         

         //thread.ManagedThreadId
         return news_id;
      }

      /// <summary>
      /// Sends the newsletter e-mails to all subscribers
      /// </summary>
      private static void SendEmails(object data)
      {
         object[] parameters = (object[])data;
         int news_id = (int)parameters[0];
         
            Lock.AcquireWriterLock(Timeout.Infinite);
            Newsletter.TotalMails = 0;
          
            Lock.ReleaseWriterLock();


            string htmlbody = string.Empty;
            string Pliantextbody = string.Empty;
            string Subject = string.Empty;
           

           
            bool htmlIsPersonalized = HasPersonalizationPlaceholders(htmlbody, true);


            IList<object> SendingList = new List<object>();
            NewsletterSending cNewsletterSending = new NewsletterSending();
            SendingList = cNewsletterSending.GetAllSendingList_service();

            foreach (NewsletterSending SendingLists in SendingList)
            {
                  Lock.AcquireWriterLock(Timeout.Infinite);
                  Newsletter.TotalMails += 1;
                  Lock.ReleaseWriterLock();
            }

            Newsletter cNew = new Newsletter();
            //string emailformat = "^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$";
            string emailformat = "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            Regex regura = new Regex(emailformat);

            SmtpClient smtpClient = new SmtpClient();
            string maildisplay = string.Empty;
            string mailNamedisplay = string.Empty;
            string mailuser = string.Empty;
            string mailpass = string.Empty;
            string host = string.Empty;
            int timedelay = 0;
         
            
            foreach (NewsletterSending SendingLists in SendingList)
            {

                IList<object> SendingListCheck = new List<object>();

                
                if (Hotels2String.IsMatchEmail(SendingLists.EmailTosend))
                {
                    try
                    {

                        MailMessage mail = new MailMessage();

                         maildisplay = SendingLists.Maildisplay;
                         mailNamedisplay = SendingLists.Displayfrom;
                         mailuser = SendingLists.Mailuser;
                         mailpass = SendingLists.Mailpass;
                         host = SendingLists.Host;
                         timedelay = SendingLists.Timedelay;

                        //string customerName = "<p style='margin:0px;padding0px;font-size:14px;font-family:tahoma;'>Dear " + SendingLists.EmailTosend.Full_name + "</p>";
                        string bodyhtml = string.Empty;
                        string strMail = string.Empty;
                        string[] arrMail = { };

                        if (!String.IsNullOrEmpty(SendingLists.Subject_Body_EN))
                            strMail = SendingLists.Subject_Body_EN;
                        else
                            strMail = SendingLists.Subject_Body_TH;

                        arrMail = Regex.Split(strMail, "#!#");
                        Subject = arrMail[0];
                        bodyhtml = arrMail[1].Replace("<!--##@CusNameContent##-->", GenCusName(SendingLists.PrefixID, SendingLists.CusName, SendingLists.PrefixTItle));


                        //CustomerName 
                        //MainBody = MainBody

                        mail.From = new MailAddress(maildisplay, mailNamedisplay);
                        mail.To.Add(SendingLists.EmailTosend.Trim());
                        mail.Subject = Subject;
                        mail.Body = bodyhtml;
                        mail.IsBodyHtml = true;

                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.Host = host;

                        if (SendingLists.Port > 0 )
                            smtpClient.Port = SendingLists.Port;

                        if (SendingLists.Ssl)
                            smtpClient.EnableSsl = SendingLists.Ssl;
                        // System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(mailuser, mailpass);
                        smtpClient.Credentials = new System.Net.NetworkCredential(mailuser, mailpass);
                        smtpClient.Send(mail);

                       cNewsletterSending.UpdateIssentWhenEmailsentOnebyone(SendingLists.SendID);

                    }
                    
                    catch (Exception ex)
                    {

                        string strException = ex.Message + "#!#" + ex.StackTrace;
                        Hotels2LogWriter.WriteFile("extranet/newsletter/log.html", DateTime.Now.Hotels2ThaiDateTime().ToString("dd-MMM-yyy HH:mm tt") + ex.Message + "<br/>" + ex.StackTrace);

                        try
                        {
                            cNew.insertSendingEmailRecordfromCustomer(SendingLists.Customer.CustomerID, null, SendingLists.EmailTosend,
        SendingLists.Newsletter_id, strException);
                        }
                        catch (Exception exw)
                        {
                            Hotels2LogWriter.WriteFile("extranet/newsletter/log.html", DateTime.Now.Hotels2ThaiDateTime().ToString("dd-MMM-yyy HH:mm tt") + exw.Message + "<br/>" + exw.StackTrace);
                        }

                       

                        //Lock.AcquireWriterLock(Timeout.Infinite);
                        //Newsletter.IsSending = false;
                        //Lock.ReleaseWriterLock();

                        //throw new Exception();
                    }
                    

               }
               else
               {

                   

                   Hotels2LogWriter.WriteFile("extranet/newsletter/log.html", DateTime.Now.Hotels2ThaiDateTime().ToString("dd-MMM-yyy HH:mm tt") + "<br/>format invalid");
                   try
                   {
                       int ret = cNew.insertSendingEmailRecordfromCustomer(SendingLists.Customer.CustomerID, null, SendingLists.EmailTosend,
     SendingLists.Newsletter_id, "Email format Invalid#!#Email format Invalid");
                   }
                   catch (Exception ex)
                   {
                       Hotels2LogWriter.WriteFile("extranet/newsletter/log.html", DateTime.Now.Hotels2ThaiDateTime().ToString("dd-MMM-yyy HH:mm tt") + "<br/>" + ex.Message + "<br/>" + ex.StackTrace);
                       //"dd-MMM-yyy HH:mm tt"
                   }
               }

               cNewsletterSending.UpdateIssentWhenEmailsendingOnebyone(SendingLists.SendID);
               
               Lock.AcquireWriterLock(Timeout.Infinite);
               Newsletter.SentMails += 1;
               Newsletter.PercentageCompleted = 
                  (double)Newsletter.SentMails * 100 / (double)Newsletter.TotalMails;
               Lock.ReleaseWriterLock();

               DateTime dt1 = DateTime.Now;
               int diff = 0;

               while (diff < timedelay)
               {

                   DateTime dt2 = DateTime.Now;
                   TimeSpan ts = dt2.Subtract(dt1);
                   diff = (int)ts.Milliseconds;

               }
               
            }
            

         Lock.AcquireWriterLock(Timeout.Infinite);
         Newsletter.IsSending = false;
         Lock.ReleaseWriterLock();


         //Newsletter NewsUpdate = Newsletter.GetNewsletterByID(news_id);
         //NewsUpdate.IsSent = 1;
         //NewsUpdate.SentDate = DateTime.Now;
         //NewsUpdate.Update();

          //Update to SENT Completed sent box
       
         //cNew.UpdateNewsletterStatus(news_id, 1);
         Newsletter.UpdateNewsletterSent();

      }

       

      /// <summary>
      /// Save the newsletter e-mails to DB
      /// </summary>
      public int SaveNewsletter(Newsletter cNewsletter)
      {
          

          using (SqlConnection cn = new SqlConnection(this.ConnectionString))
          {
              SqlCommand cmd = new SqlCommand("INSERT INTO tbl_Newsletters (AddedDate,AddedBy,SentDate,product_id,cat_id,status_id,favorite) VALUES(@AddedDate,@AddedBy,@SentDate,@product_id,@cat_id,@status_id,@favorite); SET @NewsletterID=SCOPE_IDENTITY();", cn);
        
              cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = cNewsletter.AddedDate;
              cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = cNewsletter.AddedBy;
              cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = cNewsletter.MailCat;
              cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = cNewsletter.ProductId;
              //cmd.Parameters.Add("@Subject", SqlDbType.NVarChar).Value = newsletter.Subject;
              //cmd.Parameters.Add("@PlainTextBody", SqlDbType.NText).Value = newsletter.PlainTextBody;
              //cmd.Parameters.Add("@HtmlBody", SqlDbType.NText).Value = newsletter.HtmlBody;
              cmd.Parameters.Add("@SentDate", SqlDbType.DateTime).Value = cNewsletter.SentDate;
              cmd.Parameters.Add("@status_id", SqlDbType.TinyInt).Value = cNewsletter.Status_id;
              cmd.Parameters.Add("@favorite", SqlDbType.Bit).Value = cNewsletter.Favorite;
              //cmd.Parameters.Add("@IshtmlFormat", SqlDbType.Bit).Value = newsletter.IshtmlFormat;
              cmd.Parameters.Add("@NewsletterID", SqlDbType.Int).Direction = ParameterDirection.Output;
              cn.Open();
              int ret = ExecuteNonQuery(cmd);
              return (int)cmd.Parameters["@NewsletterID"].Value;
          }

      }
      /// <summary>
      /// Returns whether the input text contains personalization placeholders
      /// </summary>
      private static bool HasPersonalizationPlaceholders(string text, bool isHtml)
      {
         if (isHtml)
         {
          if (Regex.IsMatch(text, @"&lt;%\s*email\s*%&gt;", RegexOptions.IgnoreCase | RegexOptions.Compiled))
               return true; 
         }
         else
         {
            if (Regex.IsMatch(text, @"<%\s*email\s*%>", RegexOptions.IgnoreCase | RegexOptions.Compiled))
                return true;
         }
         return false;
      }

      /// <summary>
      /// Replaces the input text's personalization placeholders
      /// </summary>
      private static string ReplacePersonalizationPlaceholders(string text, NewsletterSending SendingLists, bool isHtml)
      {
         if (isHtml)
         {
             text = Regex.Replace(text, @"&lt;%\s*email\s*%&gt;",
                SendingLists.EmailTosend, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            
         }
         else
         {
             text = Regex.Replace(text, @"<%\s*email\s*%>",
                SendingLists.EmailTosend, RegexOptions.IgnoreCase | RegexOptions.Compiled);  
         }
         return text;
      }

      public static string GenCusName(byte bytPrefix, string Cusname, string PrefixName)
      {
          
          string result = "";
          if (bytPrefix == 1)
              result = Cusname;
          else
              result = PrefixName + " " + Cusname;

          return result;
      }

      public DataTable GetsendingfailedDatatable()
      {
          DataSet Mydataset = new DataSet();
          using (SqlConnection cn = new SqlConnection(this.ConnectionString))
          {
              SqlCommand cmd = new SqlCommand("select id, cus_id, email, NewsletterID from tbl_Newsletters_Sendingfailed", cn);
              cn.Open();
              SqlDataAdapter myadapter = new SqlDataAdapter();
              myadapter.SelectCommand = cmd;
              myadapter.Fill(Mydataset, "tblDatatableSendingfailed");
              cn.Close();
              return Mydataset.Tables["tblDatatableSendingfailed"];
          }
      }

       //============== NewsletterSending failed ============= 
      public int insertSendingEmailRecordfromCustomer(int? cusid, int? staff_id, string email, int news_id, string strException)
      {
          using (SqlConnection cn = new SqlConnection(this.ConnectionString))
          {
                int ret = 0;
                SqlCommand cmd1 = new SqlCommand("UPDATE tbl_Newsletters_Sendingfailed SET cus_id=@cus_id ,staff_id=@staff_id,email=@email,NewsletterID=@NewsletterID,Exception=@Exception WHERE cus_id=@cus_id AND NewsletterID=@NewsletterID", cn);
              if (cusid.HasValue)
                  cmd1.Parameters.Add("@cus_id", SqlDbType.Int).Value = cusid;
              else
                  cmd1.Parameters.AddWithValue("@cus_id", DBNull.Value);


              if (staff_id.HasValue)
                  cmd1.Parameters.Add("@staff_id", SqlDbType.Int).Value = staff_id;
              else
                  cmd1.Parameters.AddWithValue("@staff_id", DBNull.Value);

              //cmd.Parameters.Add("@cus_id", SqlDbType.VarChar).Value = cusid;
              cmd1.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
              cmd1.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = news_id;
              cmd1.Parameters.Add("@Exception", SqlDbType.VarChar).Value = strException;
              cn.Open();
                ret = ExecuteNonQuery(cmd1);
              if (ret == 0)
              {
                  SqlCommand cmd = new SqlCommand("insert into tbl_Newsletters_Sendingfailed(cus_id,staff_id, email, NewsletterID,Exception)values(@cus_id,@staff_id, @email, @NewsletterID,@Exception)", cn);

                  if (cusid.HasValue)
                      cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = cusid;
                  else
                      cmd.Parameters.AddWithValue("@cus_id", DBNull.Value);


                  if (staff_id.HasValue)
                      cmd.Parameters.Add("@staff_id", SqlDbType.Int).Value = staff_id;
                  else
                      cmd.Parameters.AddWithValue("@staff_id", DBNull.Value);

                  //cmd.Parameters.Add("@cus_id", SqlDbType.VarChar).Value = cusid;
                  cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                  cmd.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = news_id;
                  cmd.Parameters.Add("@Exception", SqlDbType.VarChar).Value = strException;
                  
                  ret = ExecuteNonQuery(cmd);
              }
              return ret;
          }
      }


   }
}