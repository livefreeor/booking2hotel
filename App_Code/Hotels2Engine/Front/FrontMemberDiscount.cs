using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand;
/// <summary>
/// Summary description for FrontMemberDiscount
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontMemberDiscount : Hotels2BaseClass
    {
        public int MemberDiscountID { get; set; }
        public int ProductID { get; set; }
        public int ConditionID { get; set; }
        public byte DiscountType { get; set; }
        public decimal Discount { get; set; }
        public string BenefitDetail { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public FrontMemberDiscount()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<FrontMemberDiscount> GetMemberDiscountList(int productID, DateTime dateStart,DateTime dateEnd)
        {
            List<FrontMemberDiscount> result = new List<FrontMemberDiscount>();
            string strCommand = string.Empty;
            
            strCommand = strCommand + "select md.member_discount_id,md.product_id,mdc.condition_id,md.discount,md.member_discount_type,md.date_start,md.date_end";
            strCommand = strCommand + " from tbl_member_discount md,tbl_member_discount_condition mdc";
            strCommand = strCommand + " where md.member_discount_id=mdc.member_discount_id";
            strCommand = strCommand + " and mdc.condition_id IN (";
            strCommand = strCommand + " select distinct(poc_ex.condition_id)";
            strCommand = strCommand + " from tbl_product_option op,tbl_product_option_condition_extra_net poc_ex,tbl_product_option_condition_price_extranet pocp_ex";
            strCommand = strCommand + " where op.option_id=poc_ex.option_id and poc_ex.condition_id=pocp_ex.condition_id";
            strCommand = strCommand + " and op.product_id="+productID;
            strCommand = strCommand + " and op.status=1";
            strCommand = strCommand + " and poc_ex.status=1";
            strCommand = strCommand + " and pocp_ex.date_price between "+dateStart.Hotels2DateToSQlString()+" and "+dateEnd.Hotels2DateToSQlString();
            strCommand = strCommand + " and pocp_ex.status=1";
            strCommand = strCommand + " )";
            strCommand = strCommand + " and md.member_discount_type =1";
            strCommand = strCommand + " and "+dateStart.Hotels2DateToSQlString()+" between md.date_start and md.date_end";
            strCommand = strCommand + " and md.status=1";
            strCommand = strCommand + " order by mdc.condition_id,md.member_discount_type";

            //HttpContext.Current.Response.Write(strCommand);

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(strCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    //HttpContext.Current.Response.Write(strCommand + "<br>");
                    //HttpContext.Current.Response.Flush();
                    result.Add(new FrontMemberDiscount
                    {
                        MemberDiscountID = (int)reader["member_discount_id"],
                        ProductID=(int)reader["product_id"],
                        ConditionID = (int)reader["condition_id"],
                        DiscountType = (byte)reader["member_discount_type"],
                        Discount = (decimal)reader["discount"],
                        DateStart=(DateTime)reader["date_start"],
                        DateEnd=(DateTime)reader["date_end"],
                        BenefitDetail=""
                    });
                }
            }
            return result;
        }

        public List<FrontMemberDiscount> GetMemberDiscountBenefitList(int productID, DateTime dateStart,DateTime dateEnd)
        {
            List<FrontMemberDiscount> result = new List<FrontMemberDiscount>();
            string strCommand = string.Empty;

            strCommand = strCommand + "select md.member_discount_id,md.product_id,mdc.condition_id,md.discount,md.member_discount_type,mdbc.detail,md.date_start,md.date_end";
            strCommand = strCommand + " from tbl_member_discount md,tbl_member_discount_condition mdc,tbl_member_discount_benefit mdb,tbl_member_discount_benefit_content mdbc";
            strCommand = strCommand + " where md.member_discount_id=mdc.member_discount_id and mdb.member_discount_id=md.member_discount_id and mdb.member_discount_benefit_id=mdbc.member_discount_benefit_id";
            strCommand = strCommand + " and md.member_discount_type =2";
            strCommand = strCommand + " and mdc.condition_id IN (";
            strCommand = strCommand + " select distinct(poc_ex.condition_id)";
            strCommand = strCommand + " from tbl_product_option op,tbl_product_option_condition_extra_net poc_ex,tbl_product_option_condition_price_extranet pocp_ex";
            strCommand = strCommand + " where op.option_id=poc_ex.option_id and poc_ex.condition_id=pocp_ex.condition_id";
            strCommand = strCommand + " and op.product_id="+productID;
            strCommand = strCommand + " and op.status=1";
            strCommand = strCommand + " and poc_ex.status=1";
            strCommand = strCommand + " and pocp_ex.date_price between "+dateStart.Hotels2DateToSQlString()+" and "+dateEnd.Hotels2DateToSQlString();
            strCommand = strCommand + " and pocp_ex.status=1";
            strCommand = strCommand + " )";
            strCommand = strCommand + " and "+dateStart.Hotels2DateToSQlString()+" between md.date_start and md.date_end";
            strCommand = strCommand + " and md.status=1";
            strCommand = strCommand + " and mdb.status=1";
            strCommand = strCommand + " and mdbc.lang_id=1";
            strCommand = strCommand + " order by mdc.condition_id,mdb.priority";

            //HttpContext.Current.Response.Write(strCommand+"<br>");
            //HttpContext.Current.Response.Flush();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(strCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new FrontMemberDiscount
                    {
                        MemberDiscountID = (int)reader["member_discount_id"],
                        ProductID=(int)reader["product_id"],
                        ConditionID = (int)reader["condition_id"],
                        DiscountType = (byte)reader["member_discount_type"],
                        Discount = (decimal)reader["discount"],
                        DateStart = (DateTime)reader["date_start"],
                        DateEnd = (DateTime)reader["date_end"],
                        BenefitDetail = reader["detail"].ToString()
                    });
                }
            }
            return result;
        }
    }
}