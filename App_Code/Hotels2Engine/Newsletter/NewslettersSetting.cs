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


/// <summary>
/// Summary description for NewslettersSetting
/// </summary>
/// 
namespace Hotels2thailand.Newsletters
{
    public class NewslettersSetting : Hotels2BaseClass
    {
        private int _id;
        private string _host;
        private string _mailUser;
        private string _mailPass;
        private string _mailAddressDisplay;
        private string _Displayfrom;
        private int _timedelay;
        private DateTime _date_modify;

        public NewslettersSetting()
        {
            _id = 0;
            _host = string.Empty;
            _mailUser = string.Empty;
            _mailPass = string.Empty;
            _mailAddressDisplay = string.Empty;
            _Displayfrom = string.Empty;
            _timedelay = 0;
            _date_modify = DateTime.Now; ; 
        }

        //public NewslettersSetting(int id, string host, string mailuser, string mailpass, string maildisplay, string displayfrom, int timedelay, DateTime datemodify)
        //{
        //    this.Id = id;
        //    this.Host = host;
        //    this.Mailuser = mailuser;
        //    this.Mailpass = mailpass;
        //    this.Maildisplay = maildisplay;
        //    this.Displayfrom = displayfrom;
        //    this.Timedelay = timedelay;
        //    this.DateModify = datemodify;
        //}

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int ProductID { get; set; }

        //public byte MailCat { get; set; }
        public string Host
        {
            get { return _host; }
            set { _host = value; }
        }

        public string Mailuser
        {
            get { return _mailUser; }
            set { _mailUser = value; }
        }

        public string Mailpass
        {
            get { return _mailPass; }
            set { _mailPass = value; }
        }

        public string Maildisplay
        {
            get { return _mailAddressDisplay; }
            set { _mailAddressDisplay = value; }
        }

        public string Displayfrom
        {
            get { return _Displayfrom; }
            set { _Displayfrom = value; }
        }

        public int Timedelay
        {
            get { return _timedelay; }
            set { _timedelay = value; }
        }

        public DateTime DateModify
        {
            get { return _date_modify; }
            set { _date_modify = value; }
        }

        public int Port { get; set; }
        public bool SSL { get; set; }
        public bool IsActive { get; set; }
        //public bool Update()
        //{
        //    return NewslettersSetting.UpdateMailsetting(this.Id, this.Host, this.Mailuser, this.Mailpass, this.Maildisplay, this.Displayfrom, this._timedelay);
        //}


        //================== Method =================

        //protected static NewslettersSetting GetSettingfromNewsletterSettingDetails(NewslettersSettingDetails record)
        //{
        //    if (record == null)
        //        return null;
        //    else
        //    {
        //        return new NewslettersSetting(record.Id, record.Host, record.Mailuser,record.Mailpass,record.Maildisplay,record.Displayfrom,record.Timedelay,record.DateModify);
        //    }
        //}

        //protected static List<NewslettersSetting> GetSettingCollectionfromNewsletterSettingDetails(List<NewslettersSettingDetails> recordset)
        //{
        //    List<NewslettersSetting> thesetting = new List<NewslettersSetting>();
        //    foreach (NewslettersSettingDetails record in recordset)
        //    {
        //        thesetting.Add(GetSettingfromNewsletterSettingDetails(record));
        //    }
        //    return thesetting;
        //}

        public  NewslettersSetting GetSettingbyId(int ProductId)
        {
            //NewslettersSetting mailSetting = null;
            //mailSetting = GetSettingfromNewsletterSettingDetails(SiteProvider.Newsletters.GetNewsSettingById(id));
            //return mailSetting;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("select TOP 1 * from tbl_Newsletters_Setting where product_id = @product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = ProductId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (NewslettersSetting)MappingObjectFromDataReader(reader);
                else
                    return null;

            }
        }

        public bool InsertNewsSetting(NewslettersSetting newssetting)
        {
            //NewslettersSettingDetails record = new NewslettersSettingDetails(id, host, user, pass,maildisplay,txtdisplay,timedelay,DateTime.Now);
            //bool ret = SiteProvider.Newsletters.UpdateNewsSetting(record);
            //return ret;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_Newsletters_Setting (host,product_id,mailUser,mailPass,mailAddressDisplay,Displayfrom,timedelay,date_modify,port,ssl,IsActive) VALUES (@host,@product_id,@mailUser,@mailPass,@mailAddressDisplay,@Displayfrom,@timedelay,@date_modify,@port,@ssl,@IsActive)", cn);
                //cmd.Parameters.Add("@id", SqlDbType.SmallInt).Value = newssetting.Id;
                cmd.Parameters.Add("@host", SqlDbType.VarChar).Value = newssetting.Host;
                cmd.Parameters.Add("@mailUser", SqlDbType.VarChar).Value = newssetting.Mailuser;
                cmd.Parameters.Add("@mailPass", SqlDbType.VarChar).Value = newssetting.Mailpass;
                cmd.Parameters.Add("@mailAddressDisplay", SqlDbType.VarChar).Value = newssetting.Maildisplay;
                cmd.Parameters.Add("@Displayfrom", SqlDbType.VarChar).Value = newssetting.Displayfrom;
                cmd.Parameters.Add("@timedelay", SqlDbType.Int).Value = newssetting.Timedelay;
                cmd.Parameters.Add("@date_modify", SqlDbType.DateTime).Value = newssetting.DateModify;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = newssetting.ProductID;
                cmd.Parameters.Add("@port", SqlDbType.Int).Value = newssetting.Port;
                cmd.Parameters.Add("@ssl", SqlDbType.Bit).Value = newssetting.SSL;
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = newssetting.IsActive;
                cn.Open();

                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }


        public bool UpdateMailsetting(NewslettersSetting newssetting)
        {
            //NewslettersSettingDetails record = new NewslettersSettingDetails(id, host, user, pass,maildisplay,txtdisplay,timedelay,DateTime.Now);
            //bool ret = SiteProvider.Newsletters.UpdateNewsSetting(record);
            //return ret;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Update tbl_Newsletters_Setting set host=@host,mailUser=@mailUser,mailPass=@mailPass,mailAddressDisplay=@mailAddressDisplay,Displayfrom=@Displayfrom,timedelay=@timedelay,date_modify=@date_modify,port=@port,ssl=@ssl,IsActive=@IsActive WHERE product_id=@product_id", cn);
                //cmd.Parameters.Add("@id", SqlDbType.SmallInt).Value = newssetting.Id;
                cmd.Parameters.Add("@host", SqlDbType.VarChar).Value = newssetting.Host;
                cmd.Parameters.Add("@mailUser", SqlDbType.VarChar).Value = newssetting.Mailuser;
                cmd.Parameters.Add("@mailPass", SqlDbType.VarChar).Value = newssetting.Mailpass;
                cmd.Parameters.Add("@mailAddressDisplay", SqlDbType.VarChar).Value = newssetting.Maildisplay;
                cmd.Parameters.Add("@Displayfrom", SqlDbType.VarChar).Value = newssetting.Displayfrom;
                cmd.Parameters.Add("@timedelay", SqlDbType.Int).Value = newssetting.Timedelay;
                cmd.Parameters.Add("@date_modify", SqlDbType.DateTime).Value = newssetting.DateModify;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = newssetting.ProductID;
                cmd.Parameters.Add("@port", SqlDbType.Int).Value = newssetting.Port;
                cmd.Parameters.Add("@ssl", SqlDbType.Bit).Value = newssetting.SSL;
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = newssetting.IsActive;
                cn.Open();

                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }
    }
}
 