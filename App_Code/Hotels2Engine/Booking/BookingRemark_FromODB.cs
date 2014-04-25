using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;
using System.Data;
using Hotels2thailand.Production;

using Hotels2thailand.LinqProvider.Production;

/// <summary>
/// Summary description for BookingProductShow
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public class BookingRemark_FromODB : Hotels2BaseClass
    {
        public int PolicyID { get; set; }
        public int ProductID { get; set; }
        public string TitleEn { get; set; }
        public bool Status { get; set; }


        //public decimal ConfirmPayment { get; set; }
        //public Nullable<DateTime> DatePayment { get; set; }
        //public string ProductNote { get; set; }

        //public byte NumAdult { get; set; }
        //public byte NumChild { get; set; }
        //public byte NumGolfer { get; set; }
        
        //public Nullable<DateTime> DateTimeCheckInConfirm { get; set; }
        //public string Detail { get; set; }
        
        //public Nullable<DateTime> PrepaidDate { get; set; }
        //public short SupplierID { get; set; }

        public List<object> getRemarkByProductId(int intProductID)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT policy_id, product_id, title_en,status FROM tbl_policy_speacial WHERE product_id=@product_id AND status = 1",cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductID;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        
    }
}