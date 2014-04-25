using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;

/// <summary>
/// Summary description for GatewayMethod
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class GatewayMethod:Hotels2BaseClass
    {

        private int productID;

        public GatewayMethod(int ProductID)
        {
            productID = ProductID;
        }

        

        public byte GetGateway()
        {
            byte gatewayActiveID = 0;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select booking_type_id,gateway_id,manage_id from  tbl_product_booking_engine where product_id=" + productID;
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                
                if(reader.Read())
                {
                    if((byte)reader["booking_type_id"]==2)
                    {
                        if((byte)reader["manage_id"]==2)
                        {
                            gatewayActiveID = 3;
                        }else{
                            gatewayActiveID = (byte)reader["gateway_id"];
                        }
                        
                    }
                }

            }

            return gatewayActiveID;
        }

        public bool IsHighRiskCountryBYCountryID(byte countryID)
        {
            byte[] arrCountryRisk = { 32, 36, 61, 77, 80, 97, 98, 99, 102, 108, 121, 127, 142, 154, 160, 173, 174, 214, 219, 229, 235, 208 };
            bool result = false;
            for (byte countCountry = 0; countCountry < arrCountryRisk.Length; countCountry++)
            {
                if (arrCountryRisk[countCountry] == countryID)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        

    }
}