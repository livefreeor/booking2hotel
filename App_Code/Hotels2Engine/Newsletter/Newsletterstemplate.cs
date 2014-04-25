using System;
using System.Data;
using System.Data.SqlClient;
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
using Hotels2thailand;
using System.Text.RegularExpressions;
using System.Net.Mail;
/// <summary>
/// Summary description for Newsletterstemplate
/// </summary>
/// 
namespace Hotels2thailand.Newsletters
{
    public class Newsletterstemplate :Hotels2BaseClass
    {
        public short TemplateId { get; set; }
        public string Title { get; set; }
        public string Template_Path { get; set; }
        public bool Status { get; set; }
        public byte TemplateCat { get; set; }
        public DateTime DateSubmit { get; set; }
        public string BodyEng { get; set; }
        public string BodyThai { get; set; }


        public Newsletterstemplate()
        {
            this.DateSubmit = DateTime.Now.Hotels2ThaiDateTime();
        }

        //public DataTable GetTemplate()
        //{
        //    DataSet Mydataset = new DataSet();
        //    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand(Qrystring.QrySelectNewsletterTemplate(0), cn);
        //        cn.Open();
        //        SqlDataAdapter myadapter = new SqlDataAdapter();
        //        myadapter.SelectCommand = cmd;
        //        myadapter.Fill(Mydataset, "tblTemplate");
        //        cn.Close();
        //        return Mydataset.Tables["tblTemplate"];
        //    }
        //}


        public Newsletterstemplate GetTemplateByTempId(short shrTempID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT tmp.temp_id,tmp.temp_title,tmp.temp_path,tmp.status,tmp.cat_id,tmp.date_submit,(SELECT temp_body FROM tbl_newsletter_template_lang tml WHERE tml.lang_id= 1 AND temp_id=tmp.temp_id ),(SELECT temp_body FROM tbl_newsletter_template_lang tml WHERE tml.lang_id= 2 AND temp_id=tmp.temp_id ) FROM tbl_Newsletters_template tmp WHERE tmp.temp_id=@temp_id", cn);
                cn.Open();
                cmd.Parameters.Add("@temp_id", SqlDbType.SmallInt).Value = shrTempID;
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (Newsletterstemplate)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }

        public IList<object> GetTemplateByCat(byte bytCat_id)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT tmp.temp_id,tmp.temp_title,tmp.temp_path,tmp.status,tmp.cat_id,tmp.date_submit,(SELECT temp_body FROM tbl_newsletter_template_lang tml WHERE tml.lang_id= 1 AND temp_id=tmp.temp_id ),(SELECT temp_body FROM tbl_newsletter_template_lang tml WHERE tml.lang_id= 2 AND temp_id=tmp.temp_id ) FROM tbl_Newsletters_template tmp WHERE tmp.cat_id=@cat_id", cn);
                cn.Open();
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCat_id;

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int InsertNewslettersTemplate(Newsletterstemplate cNewsTmp)
        { 
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_Newsletters_template (temp_title,temp_path,date_submit,cat_id)VALUES(@temp_title,@temp_path,@date_submit,@cat_id);SET @temp_id=SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@temp_title", SqlDbType.VarChar).Value = cNewsTmp.Title;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = cNewsTmp.TemplateCat;
                cmd.Parameters.Add("@temp_path", SqlDbType.VarChar).Value = cNewsTmp.Template_Path;
                cmd.Parameters.Add("@date_submit", SqlDbType.SmallDateTime).Value = cNewsTmp.DateSubmit;
                cmd.Parameters.Add("@temp_id", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                 ret = (short)cmd.Parameters["@temp_id"].Value;
                 if (ret > 0)
                 {
                     SqlCommand cmd2 = new SqlCommand("INSERT INTO tbl_newsletter_template_lang (temp_id,lang_id,temp_body)VALUES(@temp_id,@lang_id,@temp_body)", cn);
                     cmd2.Parameters.Add("@temp_id", SqlDbType.SmallInt).Value = ret;
                     cmd2.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = 1;
                     cmd2.Parameters.Add("@temp_body", SqlDbType.NVarChar).Value = cNewsTmp.BodyEng;
                     ExecuteNonQuery(cmd2);

                     SqlCommand cmd3 = new SqlCommand("INSERT INTO tbl_newsletter_template_lang (temp_id,lang_id,temp_body)VALUES(@temp_id,@lang_id,@temp_body)", cn);
                     cmd3.Parameters.Add("@temp_id", SqlDbType.SmallInt).Value = ret;
                     cmd3.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = 2;
                     cmd3.Parameters.Add("@temp_body", SqlDbType.NVarChar).Value = cNewsTmp.BodyThai;
                     ExecuteNonQuery(cmd3);
                 }
                return ret;
            }
        }

        public bool UpdateNewsletterTemplate(Newsletterstemplate cNewsTmp)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_Newsletters_template SET temp_title=@temp_title WHERE temp_id=@temp_id", cn);
                cmd.Parameters.Add("@temp_id", SqlDbType.VarChar).Value = cNewsTmp.TemplateId;
                cmd.Parameters.Add("@temp_title", SqlDbType.VarChar).Value = cNewsTmp.Title;
                
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                if (ret == 1)
                {
                    SqlCommand cmd1 = new SqlCommand("UPDATE tbl_newsletter_template_lang SET temp_body=@temp_body WHERE temp_id=@temp_id AND lang_id=1", cn);
                    cmd1.Parameters.Add("@temp_id", SqlDbType.SmallInt).Value = cNewsTmp.TemplateId;
                    cmd1.Parameters.Add("@temp_body", SqlDbType.NVarChar).Value = cNewsTmp.BodyEng;
                    ret = ExecuteNonQuery(cmd1);

                    SqlCommand cmd2 = new SqlCommand("UPDATE tbl_newsletter_template_lang SET temp_body=@temp_body WHERE temp_id=@temp_id AND lang_id=2", cn);
                    cmd2.Parameters.Add("@temp_id", SqlDbType.SmallInt).Value = cNewsTmp.TemplateId;
                    cmd2.Parameters.Add("@temp_body", SqlDbType.NVarChar).Value = cNewsTmp.BodyThai;
                    ret = ExecuteNonQuery(cmd2);
                }
                return (ret == 1);
            }
        }


        //============== newOptionSelected ============= 

        public void InsertOptionSelected(byte bytCat_id, int intNewsletterId, string stroptionList)
        {
            string [] arrOptionId = stroptionList.Split(',');
            if (arrOptionId.Length > 0)
            {
                foreach(string strOption in arrOptionId)
                {
                    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO tbl_newsletter_option_selected (option_id,cat_id,NewsletterID) VALUES(@option_id,@cat_id,@NewsletterID)", cn);
                        cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = int.Parse(strOption);
                        cmd.Parameters.Add("@cat_id", SqlDbType.Int).Value = bytCat_id;
                        cmd.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = intNewsletterId;
                        cn.Open();
                        ExecuteNonQuery(cmd);
                    }
                }
            }
        }
         //public static DataTable GetNewsletterTemplate()
         //{
         //   return SiteProvider.Newsletters.GetTemplate();
         //}

         //public static DataTable GetNewsletterTemplateById(int id)
         //{
         //    return SiteProvider.Newsletters.GetTemplatebyId(id);
         //}

         //public static int Insertnewtemplate(string title, string body, string path)
         //{
         //    return SiteProvider.Newsletters.InsertNewslettersTemplate(title, body, path);
         //}

         //public static bool UpdateTemplate(int id,string title, string body, string path)
         //{
         //    return SiteProvider.Newsletters.UpdateNewsletterTemplate(id,title, body, path);
         //}
             
    }


    
}
