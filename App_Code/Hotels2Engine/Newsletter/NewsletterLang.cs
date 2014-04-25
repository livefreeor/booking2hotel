using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for NewsletterLang
/// </summary>
/// 
namespace Hotels2thailand.Newsletters
{
    public class NewsletterLang:Hotels2BaseClass
    {
        public int NewsletterID { get; set; }
        public byte LangId { get; set; }
        public string Subject { get; set; }
        public string HtmlBody { get; set; }

        public NewsletterLang()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public int InsertMailBody(NewsletterLang cNewsletterLang)
        {
        
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_newsletter_body_lang (NewsletterID,lang_id,Subject,HtmlBody) VALUES(@NewsletterID,@lang_id,@Subject,@HtmlBody)", cn);
                cmd.Parameters.AddWithValue("@NewsletterID", cNewsletterLang.NewsletterID);
                cmd.Parameters.AddWithValue("@lang_id", cNewsletterLang.LangId);
                cmd.Parameters.AddWithValue("@Subject", cNewsletterLang.Subject);

                cmd.Parameters.AddWithValue("@HtmlBody", cNewsletterLang.HtmlBody);
                cn.Open();
                 return ExecuteNonQuery(cmd);
            }
        }

        public bool UpdateMailBody(NewsletterLang cNewsletterLang)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_newsltter_body_lang SET Subject=@Subject,HtmlBody=@HtmlBody WHERE NewsletterID=@NewsletterID AND lang_id=@lang_id", cn);
                cmd.Parameters.AddWithValue("@NewsletterID", cNewsletterLang.NewsletterID);
                cmd.Parameters.AddWithValue("@lang_id", cNewsletterLang.LangId);
                cmd.Parameters.AddWithValue("@Subject", cNewsletterLang.Subject);

                cmd.Parameters.AddWithValue("@HtmlBody", cNewsletterLang.HtmlBody);
                cn.Open();
                 return (ExecuteNonQuery(cmd) == 1);
            }
        }


    }
}