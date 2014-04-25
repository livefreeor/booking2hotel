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
    public class monthly_hotelList:Hotels2BaseClass
    {
        public int ProductId { get; set; }
        public string  ProductCode { get; set; }
       
        public string ProductTitle { get; set; }
        public short StatusID { get; set; }
        public string StatusTitle { get; set; }
        public bool Status { get; set; }
        public DateTime CommissiontStart { get; set; }
        public byte MonthNum { get; set; }
        public DateTime? LatestInvoid { get; set; }
        public int NumPending { get; set; }


        public monthly_hotelList()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public IList<object> getProductListComMonthly(byte monthlycomType )
        {
            //string strSearch = "'%" + TxtSearch + "%'";

            StringBuilder Query = new StringBuilder();

            Query.Append("SELECT p.product_id, p.product_code, p.title as Producttitle,  p.status_id,st.title as StatusTitle, p.status, pg.commission_start,pg.monthly_commission_num ,(SELECT MAX(acp.date_submit) FROM tbl_account_hotel_payment acp WHERE acp.product_id = p.product_id AND acp.cat_id = 2) AS latest_invoid,(SELECT COUNT(acp.payment_id) FROM tbl_account_hotel_payment acp WHERE acp.product_id = p.product_id AND acp.cat_id = 2 AND acp.date_payment IS NULL AND acp.status = 1 ) AS num_pending FROM tbl_product p,  tbl_product_content pc, tbl_status st,  tbl_revenue rv, tbl_product_booking_engine pg WHERE p.Isextranet = 1 AND pc.product_id= p.product_id AND pc.lang_id = 1 AND st.cat_id = 1 AND st.status_id = p.status_id AND p.product_id = pg.product_id AND rv.product_id=p.product_id  AND rv.cat_id=2 AND pg.monthly_commission_type = @monthly_commission_type");
            Query.Append(" ORDER BY p.title");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@monthly_commission_type", SqlDbType.TinyInt).Value = monthlycomType;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

       
        
        
    }

}