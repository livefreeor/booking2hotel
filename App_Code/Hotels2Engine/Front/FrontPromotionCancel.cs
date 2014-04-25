using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for FrontPromotionCancel
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontPromotionCancel:Hotels2BaseClass
    {
        public int PromotionID { get; set; }
        public byte DayCancel { get; set; }
        public byte ChangePercent { get; set; }
        public byte ChangeNight { get; set; }

        public FrontPromotionCancel()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public IList<object> GetPromotionCancelByPromotionID(int PromotionID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string strCommand = "select promotion_id,day_cancel,charge_percent,charge_night";
                strCommand=strCommand+" from tbl_product_option_condition_cancel_content_promotion_extra_net where promotion_id="+PromotionID+" and status=1";
                strCommand = strCommand + " order by day_cancel asc";
                SqlCommand cmd = new SqlCommand(strCommand,cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

    }
}