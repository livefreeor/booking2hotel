using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Account_Commission_BHT_manage
/// </summary>
/// 

namespace Hotels2thailand.Account
{
    public class Flat_hotelList:Hotels2BaseClass
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
       
        public string ProductTitle { get; set; }
        public int NumBookingDue { get; set; }
        
        public Flat_hotelList()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public IList<object> getProductListComHotelManage()
        {
            //string strSearch = "'%" + TxtSearch + "%'";


            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_account_hotel_manage_hotel_list", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

       
        
        
    }

}