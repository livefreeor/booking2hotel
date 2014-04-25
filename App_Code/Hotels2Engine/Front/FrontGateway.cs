using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for FrontGateway
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontGateway:Hotels2BaseClass
    {
        public byte GatewayID { get; set; }
        public string Title { get; set; }

        public FrontGateway()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<FrontGateway> GetGateWayAuthenList()
        {
            List<FrontGateway> result = new List<FrontGateway>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string strCommand = "select gateway_id,title from tbl_gateway where gateway_id IN (3,5) and gateway_active=1";

                SqlCommand cmd = new SqlCommand(strCommand, cn);
                cn.Open();

                SqlDataReader reader = reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    result.Add(new FrontGateway { 
                    GatewayID=(byte)reader["gateway_id"],
                    Title=reader["title"].ToString()
                    });
                }
            }

            return result;
        }
    }
}