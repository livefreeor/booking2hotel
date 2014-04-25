using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Linq;
using System.Web;
using System.Reflection;
using Hotels2thailand.LinqProvider.Staff;
using Hotels2thailand.Suppliers;



/// <summary>
/// Summary description for Staff
/// </summary>
/// 
namespace Hotels2thailand.Staffs
{
    public partial class StaffChain : Hotels2BaseClass
    {

        public StaffChain() { }

        public short insertChain(string strTitle)
        {
            short ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_chain (title) VALUES(@title); SET @chain_id=SCOPE_IDENTITY();",cn);
                cmd.Parameters.Add("title", SqlDbType.VarChar).Value = strTitle;
                cmd.Parameters.Add("@chain_id", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cn.Open();

                ExecuteNonQuery(cmd);
                ret = (short)cmd.Parameters["@chain_id"].Value;
            }

            return ret;
        }


        public int intSertProductChain(short shrChainId, int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_chain (chain_id,product_id,status)VALUES(@chain_id,@product_id,1)", cn);
                cmd.Parameters.Add("@chain_id", SqlDbType.SmallInt).Value = shrChainId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return ret;
            }
        }

        public IDictionary<short, string> GetStaffChain()
        {
            IDictionary<short, string> idicChain = new Dictionary<short, string>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT chain_id, title FROM tbl_chain WHERE status = 1 ORDER BY title", cn);
                cn.Open();
                
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    idicChain.Add((short)reader[0], reader[1].ToString());
                }
            }

            return idicChain;
        }
       
    }
}