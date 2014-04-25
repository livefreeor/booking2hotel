using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for MemberPrice
/// </summary>
/// 
namespace Hotels2thailand.Member
{
    public class MemberPrice : Hotels2BaseClass
    {
       
        public int DiscountId { get; set; }
        public int Product_id { get; set; }
        public byte DiscountType { get; set; }
        public decimal DiscountAmount  { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool Status { get; set; }

        public IList<int> ConditionSel
        {
            get
            {
                return this.GetMemberCOndition(this.DiscountId);
            }
        }

        public MemberPrice()
        {
            //
            // TODO: Add constructor logic here
            //
            this.DiscountType = 1;
        }


        public List<object> GetMemberPriceList(int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_member_discount WHERE product_id = @product_id AND status = 1 AND member_discount_type = 1 ORDER BY date_start, date_end", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }


        public IList<int> GetMemberCOndition(int intDiscountId)
        {

           // string[,] arr = new string[,] { };
            IList<int> iCon = new List<int>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT condition_id FROM tbl_member_discount_condition WHERE member_discount_id=@member_discount_id AND status = 1", cn);
                cmd.Parameters.Add("@member_discount_id", SqlDbType.Int).Value = intDiscountId;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd);
                
               
                while (reader.Read())
                {
                    iCon.Add((int)reader[0]);
                   
                }

                return iCon;
            }
        }

        public bool RemoveDiscount(int intDiscountId, int intProductId)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_member_discount SET status = 0 WHERE member_discount_id=@member_discount_id", cn);
                cmd.Parameters.Add("@member_discount_id", SqlDbType.Int).Value = intDiscountId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);

                return (ret == 1);
            }
        }

        public bool UpdateDisCount(int intDiscountId,DateTime dDateStart, DateTime dDateEnd,Decimal decDiscount,int intProductId)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_member_discount SET date_start=@date_start, date_end=@date_end,discount=@discount WHERE member_discount_id=@member_discount_id",cn);
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end",SqlDbType.SmallDateTime).Value = dDateEnd;
                cmd.Parameters.Add("@discount", SqlDbType.SmallMoney).Value = decDiscount;
                cmd.Parameters.Add("@member_discount_id", SqlDbType.Int).Value = intDiscountId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);

                SqlCommand cmdDelCon = new SqlCommand("DELETE FROM tbl_member_discount_condition WHERE member_discount_id=@member_discount_id", cn);
                cmdDelCon.Parameters.Add("@member_discount_id", SqlDbType.Int).Value = intDiscountId;
                ret = ExecuteNonQuery(cmdDelCon);

                foreach (string con in HttpContext.Current.Request.Form["checkbox_condition_check_" + intDiscountId].Split(','))
                {
                    SqlCommand cmdMapConDis = new SqlCommand("INSERT INTO tbl_member_discount_condition (member_discount_id,condition_id,status) VALUES (@member_discount_id,@condition_id,@status)",cn);
                    cmdMapConDis.Parameters.Add("@condition_id", SqlDbType.Int).Value = int.Parse(con);
                    cmdMapConDis.Parameters.Add("@member_discount_id", SqlDbType.Int).Value = intDiscountId;
                    cmdMapConDis.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                    ret = ExecuteNonQuery(cmdMapConDis);
                }

            }
            return (ret ==1);
        }

        public int InsertDiscount(int intProductId, decimal decDiscount, DateTime dDateStart, DateTime dDateEnd)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_member_discount (product_id,member_discount_type,discount,date_start,date_end,status) VALUES(@product_id,@member_discount_type,@discount,@date_start,@date_end,@status);SET @member_discount_id = SCOPE_IDENTITY();", cn);

                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId ;
                cmd.Parameters.Add("@member_discount_type", SqlDbType.TinyInt).Value = this.DiscountType;
                cmd.Parameters.Add("@discount", SqlDbType.SmallMoney).Value = decDiscount;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true;

                cmd.Parameters.Add("@member_discount_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
               int DiscountID=  (int)cmd.Parameters["@member_discount_id"].Value;

                string[] arrConditionId = HttpContext.Current.Request.Form["checkbox_condition_check"].Split(',');

                foreach (string con in arrConditionId)
                {
                    SqlCommand cmdcon = new SqlCommand("INSERT INTO tbl_member_discount_condition (member_discount_id,condition_id,status) VALUES (@member_discount_id,@condition_id,1)", cn);
                    cmdcon.Parameters.Add("@member_discount_id", SqlDbType.Int).Value = DiscountID;
                    cmdcon.Parameters.Add("@condition_id", SqlDbType.Int).Value = int.Parse(con);
                    cmdcon.Parameters.Add("@status", SqlDbType.Int).Value = true;
                    ret =  ExecuteNonQuery(cmdcon);
                }


                return ret;
            }
            
        }

    }
}