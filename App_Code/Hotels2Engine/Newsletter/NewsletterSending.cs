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
using Hotels2thailand.Front;
using System.Text;


namespace Hotels2thailand.Newsletters
{
    public class NewsletterSending:Hotels2BaseClass
    {
        private int             _news_send_id;
        private int?            _cus_id;
        
        private int?            _staff_id;
        private int             _news_id;
        private bool            _Is_sent;
        private Customer        _Customer;
        //private Newsletter      _IsHtml;
       

        private string _host;
        private string _mailUser;
        private string _mailPass;
        private string _mailAddressDisplay;
        private string _Displayfrom;
        private int _timedelay;
        private DateTime _date_modify;
        private int _status_id;

        public NewsletterSending()
        {
            //_news_send_id   = 0;
            //_cus_id         = 0;
            //_news_id        = 0;
            //_Is_sent        = false;
            //_email_sending  = null;
            ////_IsHtml         = null;
            //_News_id        = null;

        }

        //public NewsletterSending(int news_send_id, int? cus_id, int? partner_id, int? staff_id, int news_id, bool Is_sent)
        //{
        //    this.SendID = news_send_id;
        //    this.Cus_id = cus_id;
        //    this.Partnet_id = partner_id;
        //    this.Staff_id = staff_id;
        //    this.Newsletter_id = news_id;
        //    this.Is_Sent = Is_sent;
        //}

        public int SendID
        {
            get { return _news_send_id; }
            set { _news_send_id = value; }
        }

        public int? Cus_id
        {
            get { return _cus_id; }
            set { _cus_id = value; }
        }
        
        //public int? Partnet_id
        //{
        //    get { return _partner_id; }
        //    set { _partner_id = value; }
        //}

        public int? Staff_id
        {
            get { return _staff_id; }
            set { _staff_id = value; }
        }

        public int Newsletter_id
        {
            get { return _news_id; }
            set { _news_id = value; }
        }

        public bool Sending { get; set; }
        public bool Is_Sent
        {
            get { return _Is_sent; }
            set { _Is_sent = value; }
        }
        

        public string EmailTosend { get; set; }
        public int FacGroup { get; set; }

       public string Subject_Body_EN { get; set; }
        public string Subject_Body_TH { get; set; }

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
        public bool Ssl { get; set; }

        public byte MailCat { get; set; }
        public int Status_id
        {
            get { return _status_id; }
            set { _status_id = value; }
        }
        public string CusName { get; set; }
        public byte PrefixID { get; set; }
        public string PrefixTItle { get; set; }
        public Customer Customer
        {
            get
            {
                if (_Customer == null)
                {
                    Customer cCus = new Front.Customer();
                    _Customer = cCus.GetCustomerbyId((int)this.Cus_id);
                    
                }
                return _Customer;
            }
        }



        public List<object> GetAllSendingList_service()
        {
            StringBuilder result = new StringBuilder();
            result.Append("");

            result.Append("SELECT nd.News_sending_id  , nd.cus_id, nd.staff_id, nd.NewsletterID, nd.Sending, nd.IsSent, nd.email , nd.facGroup");
            result.Append(" ,(SELECT Subject + '#!#' + HtmlBody FROM tbl_newsletter_body_lang nl ");
            result.Append(" WHERE lang_id = 1 AND ns.NewsletterID = nl.NewsletterID),(SELECT Subject + '#!#' + HtmlBody FROM tbl_newsletter_body_lang nl WHERE lang_id = 2 ");
            result.Append(" AND ns.NewsletterID = nl.NewsletterID) ,nt.product_id, nt.host, nt.mailUser, nt.mailPass, nt.mailAddressDisplay,nt.Displayfrom,nt.timedelay, nt.date_modify,nt.port,nt.ssl,ns.cat_id,ns.status_id, cs.full_name,pf.prefix_id,pf.title");
            result.Append(" FROM tbl_Newsletters ns, tbl_Newsletters_Sending nd, tbl_Newsletters_setting nt , tbl_customer cs,tbl_prefix_name pf");
            result.Append(" WHERE  ns.NewsletterID = nd.NewsletterID AND ns.product_id = nt.product_id AND cs.cus_id = nd.cus_id AND cs.[prefix_id]=pf.prefix_id AND nd.IsSent = 0 ORDER BY nd.facGroup , nd.News_sending_id ");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(result.ToString(), cn);
                cn.Open();
                //cmd.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = News_id;
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public  List<object> GetAllSendingList(int News_id)
        {
           

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_Newsletters_Sending WHERE NewsletterID=@NewsletterID and IsSent = 0", cn);
                cn.Open();
                cmd.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = News_id;
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }


        public List<object> GetAllSendingListAll(int News_id)
        {
            

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_Newsletters_Sending WHERE NewsletterID=@NewsletterID", cn);
                cn.Open();
                cmd.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = News_id;
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> GetAllSendingListCompleted(int News_id)
        {
           
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_Newsletters_Sending WHERE NewsletterID=@NewsletterID and IsSent = 1", cn);
                cn.Open();
                cmd.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = News_id;
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        //not sure to completed
        public List<object> GetAllSendingListOut(int News_id)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_Newsletters_Sending WHERE NewsletterID=@NewsletterID and Sending = 1", cn);
                cn.Open();
                cmd.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = News_id;
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public string GetCountSendingListCompleted(int NewID)
        {
            int retall = 0;
            int retsent = 0;
            int retSencom = 0;
            int retSendfailed = 0;
            double resultPer = 0.0;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd1 = new SqlCommand("SELECT COUNT(ns.NewsletterID) FROM tbl_Newsletters_Sending ns WHERE ns.NewsletterID=@NewsletterID ", cn);
                cn.Open();
                //cmd1.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd1.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = NewID;
                retall = (int)ExecuteScalar(cmd1);


                SqlCommand cmd = new SqlCommand("SELECT COUNT(ns.NewsletterID) FROM tbl_Newsletters_Sending ns WHERE  ns.NewsletterID=@NewsletterID AND ns.Sending = 1", cn);

                cmd.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = NewID;

                //cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;

                retsent = (int)ExecuteScalar(cmd);


                SqlCommand cmd2 = new SqlCommand("SELECT COUNT(ns.NewsletterID) FROM tbl_Newsletters_Sending ns WHERE ns.NewsletterID=@NewsletterID AND ns.IsSent = 1", cn);

                cmd2.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = NewID;

                //cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;

                retSencom = (int)ExecuteScalar(cmd2);

                SqlCommand cmd3 = new SqlCommand("SELECT COUNT(ns.id) FROM tbl_Newsletters_Sendingfailed ns  WHERE ns.NewsletterID=@NewsletterID", cn);

                cmd3.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = NewID;

                //cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;

                retSendfailed = (int)ExecuteScalar(cmd3);


                //retSendfailed
            }
            resultPer = retsent * 100 / retall;
            //Newsletter.PercentageCompleted =
            //     (double)Newsletter.SentMails * 100 / (double)Newsletter.TotalMails;
            return retall + ";" + retsent + ";" + resultPer + ";" + retSencom + ";" + retSendfailed;
        }
     

        public  int InsertNewsletterSending(NewsletterSending cNews)
        {
          
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("insert into tbl_Newsletters_Sending(cus_id,staff_id, NewsletterID, IsSent, email,facGroup)values(@cus_id,@staff_id , @NewsletterID, @IsSent,@email,@facGroup)", cn);
                if (cNews.Cus_id.HasValue)
                    cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = cNews.Cus_id;
                else
                    cmd.Parameters.AddWithValue("@cus_id", DBNull.Value);

                //if (cNews.Partnet_id.HasValue)
                //    cmd.Parameters.Add("@partner_id", SqlDbType.Int).Value = cNews.Partnet_id;
                //else
                //    cmd.Parameters.AddWithValue("@partner_id", DBNull.Value);


                if (cNews.Staff_id.HasValue)
                    cmd.Parameters.Add("@staff_id", SqlDbType.Int).Value = cNews.Staff_id;
                else
                    cmd.Parameters.AddWithValue("@staff_id", DBNull.Value);

                cmd.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = cNews.Newsletter_id;
                cmd.Parameters.Add("@IsSent", SqlDbType.Bit).Value = cNews.Is_Sent;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = cNews.EmailTosend;
                cmd.Parameters.Add("@facGroup", SqlDbType.Int).Value = cNews.FacGroup;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret = 1);
            }
        }

        public bool UpdateIssentWhenEmailsentOnebyone( int send_id) 
        {
            //NewsletterSendingDetails record = new NewsletterSendingDetails { ID= send_id, Is_Sent= true};
            //bool ret = SiteProvider.Newsletters.UpdateIssentWhenSent(record);
            //return ret;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Update tbl_Newsletters_Sending set IsSent=@IsSent where News_sending_id=@News_sending_id", cn);
                cmd.Parameters.Add("@News_sending_id", SqlDbType.Int).Value = send_id;
                cmd.Parameters.Add("@IsSent", SqlDbType.Bit).Value = true;

                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public bool UpdateIssentWhenEmailsendingOnebyone(int send_id)
        {
            //NewsletterSendingDetails record = new NewsletterSendingDetails { ID= send_id, Is_Sent= true};
            //bool ret = SiteProvider.Newsletters.UpdateIssentWhenSent(record);
            //return ret;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Update tbl_Newsletters_Sending set Sending=@Sending where News_sending_id=@News_sending_id", cn);
                cmd.Parameters.Add("@News_sending_id", SqlDbType.Int).Value = send_id;
                cmd.Parameters.Add("@Sending", SqlDbType.Bit).Value = true;

                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        
    
    }
}
