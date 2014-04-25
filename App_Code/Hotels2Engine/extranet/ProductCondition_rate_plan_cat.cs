using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Hotels2thailand;
using Hotels2thailand.Staffs;


/// <summary>
/// Summary description for ProductCategory
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductCondition_rate_plan_cat : Hotels2BaseClass
    {
        public byte RatePlancatId { get; set; }
        public string Title { get; set; }

        public List<object> GetRatePlanListAll()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_rate_plan_extra_net_category",cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));

            }
        }



    }
}