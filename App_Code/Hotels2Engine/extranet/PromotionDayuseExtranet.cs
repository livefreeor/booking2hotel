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
    public class PromotionDayuseExtranet : Hotels2BaseClass
    {
        public int DateUseId { get; set; }
        public int PromotionId { get; set; }
        public DateTime DateUseStart { get; set; }
        public DateTime  DateUseEnd { get; set; }



        public PromotionDayuseExtranet getUseDatePromotionTop1byPromotionID(int intPromotionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM tbl_promotion_date_use_extra_net WHERE promotion_id=@promotion_id", cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (PromotionDayuseExtranet)MappingObjectFromDataReader(reader);
                else
                    return null;

            }
        }


        public int intsertdateUse(int intProductId, int intPromotionId, DateTime dDateusestart, DateTime dDateuseEnd)
        {
            int ret = 0;
            int useDateId = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_promotion_date_use_extra_net (promotion_id,date_use_start,date_use_end) VALUES(@promotion_id,@date_use_start,@date_use_end);SET @promotion_date_use_id=SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cmd.Parameters.Add("@date_use_start", SqlDbType.SmallDateTime).Value = dDateusestart;
                cmd.Parameters.Add("@date_use_end", SqlDbType.SmallDateTime).Value = dDateuseEnd;
                cmd.Parameters.Add("@promotion_date_use_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();

                ret =  ExecuteNonQuery(cmd); 
                useDateId = (int)cmd.Parameters["@promotion_date_use_id"].Value;
            }

            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_promotion, StaffLogActionType.Insert, StaffLogSection.Product, intProductId,
                 "tbl_promotion_date_use_extra_net", "promotion_id,date_use_start,date_use_end", "promotion_date_use_id", useDateId);

            return ret;
        }

        public bool UpdatedateUse(int intProductId, int intPromotionId, DateTime dDateusestart, DateTime dDateuseEnd)
        {
            int ret = 0;
            ArrayList arrOld_Value = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_promotion_date_use_extra_net", "date_use_start,date_use_end", "promotion_id", intPromotionId);
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_promotion_date_use_extra_net SET date_use_start=@date_use_start,date_use_end=@date_use_end WHERE promotion_id=@promotion_id", cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cmd.Parameters.Add("@date_use_start", SqlDbType.SmallDateTime).Value = dDateusestart;
                cmd.Parameters.Add("@date_use_end", SqlDbType.SmallDateTime).Value = dDateuseEnd;
                cn.Open();

                ret = ExecuteNonQuery(cmd);

            }

            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_promotion, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_promotion_date_use_extra_net", "date_use_start,date_use_end", arrOld_Value, "promotion_id", intPromotionId);

            return (ret == 1);
        }

    }
}