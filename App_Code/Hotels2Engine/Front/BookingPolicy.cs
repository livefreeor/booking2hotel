using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;

/// <summary>
/// Summary description for BookingPolicy
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class BookingPolicy:Hotels2BaseClass
    {
        public int PolicyID { get; set; }
        public int ConditionID { get; set; }
        public byte PolicyCategory { get; set; }
        public byte PolicyType { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public BookingPolicy()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<BookingPolicy> GetPolicy(int conditionID,DateTime dateStart)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string strCommand = "select temp.policy_id,ppc.title,temp.cat_id,temp.type_id,temp.title as type_title,temp.date_start,temp.date_end,temp.condition_id,ppc.detail";
                strCommand = strCommand + " from";
                strCommand = strCommand + " (";
                strCommand = strCommand + " select pp.policy_id,pp.cat_id,pp.type_id,ppt.title,pp.date_start,pp.date_end,poc.condition_id";
                strCommand = strCommand + " from tbl_product_policy pp,tbl_product_option_condition_policy pocp,tbl_product_option po,tbl_product_option_condition poc,tbl_product_policy_type ppt";
                strCommand = strCommand + " where";
                strCommand = strCommand + " po.option_id=poc.option_id";
                strCommand = strCommand + " and pocp.policy_id=pp.policy_id";
                strCommand = strCommand + " and pocp.condition_id=poc.condition_id";
                strCommand = strCommand + " and pp.type_id=ppt.type_id";
                if (dateStart != null)
                {
                    strCommand = strCommand + " and " + dateStart.Hotels2DateToSQlString() + " between pp.date_start and pp.date_end";
                }
                strCommand = strCommand + " and poc.condition_id=" + conditionID;
                strCommand = strCommand + " ) as temp left outer join tbl_product_policy_content ppc on ppc.policy_id=temp.policy_id";
                strCommand = strCommand + " and ppc.lang_id=1";
                strCommand = strCommand + " order by temp.type_id desc";

                //HttpContext.Current.Response.Write(sqlCommand);

                SqlCommand cmd = new SqlCommand(strCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                List<BookingPolicy> policies = new List<BookingPolicy>();

                while (reader.Read())
                {
                    policies.Add(new BookingPolicy
                    {
                        PolicyID = (int)reader["policy_id"],
                        ConditionID = conditionID,
                        PolicyCategory = (byte)reader["cat_id"],
                        PolicyType = (byte)reader["type_id"],
                        Title = reader["title"].ToString(),
                        Detail = reader["detail"].ToString(),
                        DateStart = (DateTime)reader["date_start"],
                        DateEnd = (DateTime)reader["date_end"]
                    });
                }
                return policies;
            }

            
        }
    }
}