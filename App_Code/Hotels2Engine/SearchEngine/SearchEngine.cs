using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Hotels2thailand;
using Hotels2thailand.DataAccess;

/// <summary>
/// Summary description for SearchEngine
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class SearchEngine : Hotels2BaseClass
    {
        public SearchEngine()
        {
           
        }

        //public int ImportKeyWordSuggestion()
        //{
        //    int ret = 0;
        //    ArrayList arCatId = new ArrayList();
        //    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        //    {

        //        SqlCommand cmd = new SqlCommand("SELECT title FROM tbl_product_content", cn);
        //        IDataReader reader = ExecuteReader(cmd);
        //        while (reader.Read())
        //        {
        //            using (SqlConnection cn1 = new SqlConnection(this.ConnectionString))
        //            {

        //                SqlCommand cmd2 = new SqlCommand("INSERT INTO tbl_keyword_suggest (title)VALUES('"+reader[0]+"')", cn1);
        //                ret = ExecuteNonQuery(cmd2);

        //            }
        //        }
                
        //    }

        //    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        //    {

        //        SqlCommand cmd = new SqlCommand("SELECT keyword FROM tbl_product_keyword", cn);
        //        IDataReader reader = ExecuteReader(cmd);
        //        while (reader.Read())
        //        {
        //            using (SqlConnection cn1 = new SqlConnection(this.ConnectionString))
        //            {

        //                SqlCommand cmd2 = new SqlCommand("INSERT INTO tbl_keyword_suggest (title)VALUES('" + reader[0] + "')", cn1);
        //                ret = ExecuteNonQuery(cmd2);

        //            }
        //        }

        //    }
            

            
        //    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        //    {
                
        //        SqlCommand cmd = new SqlCommand("SELECT cat_id FROM tbl_product_category WHERE cat_id NOT IN (31,37)", cn);
        //        IDataReader reader = ExecuteReader(cmd);
        //        while (reader.Read())
        //        {
        //            arCatId.Add(reader[0]);
        //        }
            
        //    }

        //    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("SELECT cat_id FROM tbl_product_category WHERE cat_id NOT IN (31,37)", cn);
        //        IDataReader reader = ExecuteReader(cmd);
        //        while (reader.Read())
        //        {
        //            arCatId.Add(reader[0]);
        //        }
        //    }
        //}

        public IDictionary<int,string> GetListSearchList(string strKeyinput)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT TOP 10  pc.product_id , pc.title");
                query.Append(" FROM tbl_product_content pc WHERE pc.title LIKE @Keyword ");
                query.Append(" OR pc.product_id IN (SELECT product_id FROM tbl_product_keyword spk WHERE spk.keyword LIKE @Keyword)");
                query.Append(" OR pc.product_id IN (SELECT pl.product_id");
                query.Append(" FROM tbl_location l, tbl_product_location pl, tbl_location_name ln");
                query.Append(" WHERE l.location_id = pl.location_id AND pl.location_id = ln.location_id AND ln.title LIKE @Keyword)");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                string Key = "'"+ strKeyinput +"'";
                cmd.Parameters.AddWithValue("@Keyword", Key);
                IDictionary<int,string> dicList = new Dictionary<int,string>();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    dicList.Add((int)reader[0],reader[1].ToString());
                }
                return dicList;
            }
        }

        public ArrayList GetListSearchListSuggest()
        {

            ArrayList arrList = new ArrayList();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                
                SqlCommand cmdProduct = new SqlCommand("SELECT pc.title FROM tbl_product_content pc, tbl_product p WHERE p.product_id=pc.product_id AND p.status = 1", cn);
                //HttpContext.Current.Response.Write("SELECT title FROM tbl_product_content WHERE title LIKE '%" + strKeyinput + "%'");
                //HttpContext.Current.Response.End();
                //cmdProduct.Parameters.AddWithValue("@Keyword", strKeyinput);
                cn.Open();
                IDataReader readerProduct = ExecuteReader(cmdProduct);
                while (readerProduct.Read())
                {
                    arrList.Add(readerProduct[0]);
                }
                
            }

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                //SqlCommand cmdKeyword = new SqlCommand("SELECT keyword FROM tbl_product_keyword WHERE keyword LIKE '%" + strKeyinput + "%'", cn);
                SqlCommand cmdKeyword = new SqlCommand("SELECT pk.keyword FROM tbl_product_keyword pk, tbl_product p WHERE p.product_id=pk.product_id AND p.status = 1", cn);
                //cmdKeyword.Parameters.AddWithValue("@Keyword", strKeyinput);
                cn.Open();
                IDataReader readerKey = ExecuteReader(cmdKeyword);
                while (readerKey.Read())
                {
                    arrList.Add(readerKey[0]);
                }
            }

            return arrList;
        }
    }
}