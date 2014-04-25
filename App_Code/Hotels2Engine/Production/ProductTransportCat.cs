using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for ProductTransportCat
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductTransportCat : Hotels2BaseClass
    {
        public ProductTransportCat()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static Dictionary<byte, string> getDictransportAll()
        {
            ProductTransportCat cTransCat = new ProductTransportCat();
            Dictionary<byte, string> dicTranspot = new Dictionary<byte, string>();
            using (SqlConnection cn = new SqlConnection(cTransCat.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT transport_id,title FROM tbl_transport_cat ORDER BY title DESC", cn);
                cn.Open();
                IDataReader reader = cTransCat.ExecuteReader(cmd);
                while (reader.Read())
                {
                    dicTranspot.Add((byte)reader[0], reader[1].ToString());
                }
                return dicTranspot;
            }
        }
    }
}