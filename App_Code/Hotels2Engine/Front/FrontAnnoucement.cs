using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;
/// <summary>
/// Summary description for FrontAnnoucemen
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontAnnoucement:Hotels2BaseClass
    {
        public string Title { get; set; }
        public string Detail { get; set; }

        private int _productID = 0;
        private byte _langID = 1;

        public byte LangID
        {
            set { _langID = value; }
        }

        public FrontAnnoucement()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public FrontAnnoucement(int ProductID)
        {
            _productID = ProductID;
        }

        public List<FrontAnnoucement> LoadAnnoucement()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select anc.title,anc.detail,";
                sqlCommand = sqlCommand + " (select sanc.title  from tbl_announcement_content sanc where sanc.annoucement_id=an.annoucement_id and sanc.lang_id="+_langID+") as second_lang,";
                sqlCommand = sqlCommand + " (select sanc.detail  from tbl_announcement_content sanc where sanc.annoucement_id=an.annoucement_id and sanc.lang_id=" + _langID + ") as second_lang_detail";
                sqlCommand = sqlCommand + " from tbl_annoucement an,tbl_announcement_content anc";
                sqlCommand = sqlCommand + " where an.annoucement_id=anc.annoucement_id and an.product_id=" + _productID + " and an.status=1 and anc.lang_id=1";
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                List<FrontAnnoucement> itemList = new List<FrontAnnoucement>();
                string titleDefault = string.Empty;
                string detailDefault = string.Empty;

                while (reader.Read())
                {
                    if(_langID==1)
                    {
                        titleDefault = reader["title"].ToString();
                        detailDefault = reader["detail"].ToString();
                    }else{
                        titleDefault = reader["second_lang"].ToString();
                        detailDefault = reader["second_lang_detail"].ToString();
                        if (string.IsNullOrEmpty(titleDefault))
                        {
                            titleDefault = reader["title"].ToString();
                            detailDefault = reader["detail"].ToString();
                        }
                    }
                    itemList.Add(new FrontAnnoucement
                    {
                        Title = titleDefault,
                        Detail = detailDefault
                    });

                }
                return itemList;
            }
            
            
        }

        public List<FrontAnnoucement> LoadAnnoucementByDate(string DateStart,string DateEnd)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select anc.title,anc.detail,";
                sqlCommand = sqlCommand + " (select sanc.title  from tbl_announcement_content sanc where sanc.annoucement_id=an.annoucement_id and sanc.lang_id=" + _langID + ") as second_lang,";
                sqlCommand = sqlCommand + " (select sanc.detail  from tbl_announcement_content sanc where sanc.annoucement_id=an.annoucement_id and sanc.lang_id=" + _langID + ") as second_lang_detail";
                sqlCommand = sqlCommand + " from tbl_annoucement an,tbl_announcement_content anc";
                sqlCommand = sqlCommand + " where an.annoucement_id=anc.annoucement_id and an.product_id=" + _productID + " and an.status=1 and anc.lang_id=1";
                sqlCommand = sqlCommand + " and ((" + DateStart + " between an.date_start and an.date_end )and (" + DateEnd + " between an.date_start and an.date_end ))";
                //sqlCommand = sqlCommand + " and ((an.date_start<=" + DateStart + " and an.date_end>=" + DateStart + ") or (an.date_start<=" + DateEnd + " and an.date_end>=" + DateEnd + ") or (an.date_start>=" + DateStart + " and an.date_end<=" + DateEnd + ")) ";
                //HttpContext.Current.Response.Write(sqlCommand);
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<FrontAnnoucement> itemList = new List<FrontAnnoucement>();
                string titleDefault = string.Empty;
                string detailDefault = string.Empty;
                while (reader.Read())
                {
                    if (_langID == 1)
                    {
                        titleDefault = reader["title"].ToString();
                        detailDefault = reader["detail"].ToString();
                    }
                    else
                    {
                        titleDefault = reader["second_lang"].ToString();
                        detailDefault = reader["second_lang_detail"].ToString();
                        if (string.IsNullOrEmpty(titleDefault))
                        {
                            titleDefault = reader["title"].ToString();
                            detailDefault = reader["detail"].ToString();
                        }
                    }
                    itemList.Add(new FrontAnnoucement
                    {
                        Title = titleDefault,
                        Detail = detailDefault
                    });

                }
                return itemList;
            }
            
        }

    }
}