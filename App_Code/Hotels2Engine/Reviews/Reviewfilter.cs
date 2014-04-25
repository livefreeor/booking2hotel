using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Hotels2thailand.Staffs;
/// <summary>
/// Summary description for ReviewManage
/// </summary>
/// 
namespace Hotels2thailand.Reviews
{
    //public enum ListType : int
    //{
    //    Approve = 0,
    //    NotApprove = 1,
    //    Bin = 2
    //}

    public class Reviewfilter:Hotels2BaseClass
    {

        public Reviewfilter()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public ArrayList GetReviewSpam()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT review_id,title,full_name,review_from FROM tbl_review_all",cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                string temp = string.Empty;
                //string temp1 = string.Empty;
                //string temp2 = string.Empty;
                ArrayList review_id = new ArrayList();
                while (reader.Read())
                {
                    
                    //temp1 = reader["full_name"].ToString();
                    //temp2 = reader["review_from"].ToString();
                    if (temp == reader["title"].ToString())
                    {
                        HttpContext.Current.Response.Write(reader["title"].ToString() + "</br>");
                        review_id.Add(reader["review_id"].ToString());
                       // temp = string.Empty;
                    }

                    temp = reader["title"].ToString();
                    
                }


                return review_id;
            }
            
        }
        public void ClearSpamReview()
        {
            foreach (string item in this.GetReviewSpam())
            {
                this.DeletereviewDuplicate(int.Parse(item));
                HttpContext.Current.Response.Write(item + "</br>");
                
            }
            HttpContext.Current.Response.Flush();
        }

        public bool DeletereviewDuplicate(int intreviewId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_review_all WHERE review_id = @review_id",cn);
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intreviewId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

    }
}