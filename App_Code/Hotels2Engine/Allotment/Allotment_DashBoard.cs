using System;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using System.Activities.Statements;
using Hotels2thailand.Staffs;


/// <summary>
/// Summary description for allotment
/// </summary>
/// 
namespace Hotels2thailand.ProductOption
{
    public class Allotment_DashBoard:Hotels2BaseClass
    {
        

        public DateTime DateAllot { get; set; }
        public int? AllotTotal { get; set; }
        


        public IList<object> getAllotmentCheckDashBoard(int intOptionId, short shrSupplierId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
               

                SqlCommand cmd = new SqlCommand("bk_extranet_get_allotment_check_dashboard", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
                
            }
        }



    }
}