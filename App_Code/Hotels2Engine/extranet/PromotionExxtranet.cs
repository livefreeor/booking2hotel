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
    public class PromotionExxtranet : Hotels2BaseClass
    {

        public int PromotionId { get; set; }
        public byte ProCatId { get; set; }
        public int ProductId { get; set; }
        public short SupplierId { get; set; }
        public string ProTitle { get; set; }
        public DateTime Datebookingstart { get; set; }
        public DateTime DatebookingEnd { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public byte QuantityMin { get; set; }
        public byte DayMin { get; set; }
        public short DayAdVanceMin { get; set; }
        public DateTime DateSubmit { get; set; }
        public bool IsMon { get; set; }
        public bool IsTue { get; set; }
        public bool IsWed { get; set; }
        public bool IsThu { get; set; }
        public bool IsFri { get; set; }
        public bool IsSat { get; set; }
        public bool IsSun { get; set; }
        public byte IsWeekEndAll { get; set; }
        public byte IsHolidayCharge { get; set; }
        public byte MaxRepeatSet { get; set; }
        public byte IsBreakfast { get; set; }
        public decimal BreakfastCharge { get; set; }
        public bool Status { get; set; }
        public string Comment { get; set; }
        public short  ProgroupItemId { get; set; }
        public bool Iscancelltion { get; set; }
        public string ProCode { get; set; }
        public bool IsLastMinute { get; set; }
        public byte PromotionScore { get; set; }
        public byte PromotionScoreBenefit { get; set; }
        public bool StatusBin { get; set; }
        public short DayLastminute { get; set; }
        public bool IsWorldWide { get; set; }

        public PromotionExxtranet getPromotionExtranetByPromotionId(int intPromotionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_promotion_extra_net WHERE promotion_id=@promotion_id", cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (PromotionExxtranet)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }



        public IList<object> getPromotionListExtranetExprired(int intProductId, short shrSupplierId, bool boolstatus, byte bytMonthExpired)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_promotion_extra_net WHERE supplier_id = @supplier_id AND product_id = @product_id AND DATEDIFF(month,DATEADD(hh,14,getdate()),  date_end) <= @expired AND status = @status", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = boolstatus;
                cmd.Parameters.Add("@expired", SqlDbType.TinyInt).Value = bytMonthExpired;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

        }

        public IList<object> getPromotionListExtranet(int intProductId, short shrSupplierId, bool boolstatus)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_promotion_extra_net WHERE product_id=@product_id AND supplier_id=@supplier_id AND status = @status", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId ;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = boolstatus;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
            
        }
        public ArrayList GetPromotionCheckById(int intPromotionId)
        {
            ArrayList arrResult = new ArrayList();
            StringBuilder result = new StringBuilder();
            result.Append("SELECT pro.promotion_id, pro.product_id, pro.supplier_id, prodate.date_use_start, prodate.date_use_end");
            result.Append(" ,prog.pro_group_id ,pro.pro_group_item_id ,pro.day_min, pro.day_advance_num");
            result.Append(" FROM tbl_promotion_extra_net pro,");
            result.Append(" tbl_promotion_date_use_extra_net prodate, tbl_promotion_group_item_extra_net prog");
            result.Append(" WHERE pro.promotion_id = prodate.promotion_id AND pro.pro_group_item_id= prog.pro_group_item_id");
            result.Append(" AND pro.promotion_id = @promotion_id");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {

                SqlCommand cmd = new SqlCommand(result.ToString(), cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    arrResult.Add((int)reader[0]);
                    arrResult.Add((int)reader[1]);
                    arrResult.Add((short)reader[2]);
                    arrResult.Add((DateTime)reader[3]);
                    arrResult.Add((DateTime)reader[4]);
                    arrResult.Add((byte)reader[5]);
                    arrResult.Add((short)reader[6]);
                    arrResult.Add((byte)reader[7]);
                    arrResult.Add((short)reader[8]);
                }
            }

            return arrResult;
        }

        public int InsertPromotion(byte bytCatId, int ProductId, short shrSupplierId, string Title, DateTime dDateStart, DateTime dDateEnd, bool bolIsMon, bool bolIsTue, bool bolIsWed, bool bolIsThu, bool bolIsFri, bool bolIsSat, bool bolIsSun, byte bytDaymin, short byteAdvanceDay, byte bytHolidayAll, byte bytMaxSet, byte IsABF, decimal ABFCharge, short shrGropItemId, bool IsCancelltion, bool IsLastMinute, byte bytScore, byte bytScoreBenefit, short shrLastminutenum, bool bolIsWorldwide)
        {
            int ret = 0;
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder Query = new StringBuilder();
                Query.Append("INSERT INTO tbl_promotion_extra_net (cat_id, product_id, supplier_id, title, date_start, date_end,");
                Query.Append(" quantity_min, day_min, day_advance_num, date_submit, day_mon, day_tue, day_wed, day_thu, day_fri ,day_sat, day_sun, is_weekend_all,");
                Query.Append(" Is_holiday_charge, max_set, Is_breakfast, breakfast_charge, status, comment, pro_group_item_id,iscancellation,is_last_minute,promotion_score,promotion_score_benefit,status_bin,day_last_minute_num,Is_use_worldwide)");
                Query.Append(" VALUES(@cat_id, @product_id, @supplier_id, @title, @date_start, @date_end, ");
                Query.Append(" @quantity_min, @day_min ,@day_advance_num ,@date_submit,@day_mon,@day_tue,@day_wed,@day_thu,@day_fri,@day_sat,@day_sun, @is_weekend_all,");
                Query.Append(" @Is_holiday_charge,@max_set,@Is_breakfast,@breakfast_charge,@status,@comment,@pro_group_item_id,@iscancellation,@is_last_minute,@promotion_score,@promotion_score_benefit,@status_bin,@day_last_minute_num,@Is_use_worldwide);SET @promotion_id = SCOPE_IDENTITY();");
                
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.Int).Value = bytCatId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = ProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = Title;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cmd.Parameters.Add("@quantity_min", SqlDbType.TinyInt).Value = 1;
                cmd.Parameters.Add("@day_min", SqlDbType.TinyInt).Value = bytDaymin;
                cmd.Parameters.Add("@day_advance_num", SqlDbType.SmallInt).Value = byteAdvanceDay;
                cmd.Parameters.Add("@date_submit", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();

                cmd.Parameters.Add("@day_mon", SqlDbType.Bit).Value = bolIsMon;
                cmd.Parameters.Add("@day_tue", SqlDbType.Bit).Value = bolIsTue;
                cmd.Parameters.Add("@day_wed", SqlDbType.Bit).Value = bolIsWed;
                cmd.Parameters.Add("@day_thu", SqlDbType.Bit).Value = bolIsThu;
                cmd.Parameters.Add("@day_fri", SqlDbType.Bit).Value = bolIsFri;
                cmd.Parameters.Add("@day_sat", SqlDbType.Bit).Value = bolIsSat;
                cmd.Parameters.Add("@day_sun", SqlDbType.Bit).Value = bolIsSun;

                cmd.Parameters.Add("@is_weekend_all", SqlDbType.TinyInt).Value = 1;
                cmd.Parameters.Add("@Is_holiday_charge", SqlDbType.TinyInt).Value = bytHolidayAll;
                cmd.Parameters.Add("@max_set", SqlDbType.TinyInt).Value = bytMaxSet;
                cmd.Parameters.Add("@Is_breakfast", SqlDbType.TinyInt).Value = IsABF;
                cmd.Parameters.Add("@breakfast_charge", SqlDbType.Money).Value = ABFCharge;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = string.Empty;
                cmd.Parameters.Add("@pro_group_item_id", SqlDbType.SmallInt).Value = shrGropItemId;
                cmd.Parameters.Add("@iscancellation", SqlDbType.Bit).Value = IsCancelltion;
                cmd.Parameters.Add("@is_last_minute", SqlDbType.Bit).Value = IsLastMinute;
                cmd.Parameters.Add("@promotion_score", SqlDbType.TinyInt).Value = bytScore;
                cmd.Parameters.Add("@promotion_score_benefit", SqlDbType.TinyInt).Value = bytScoreBenefit;
                cmd.Parameters.Add("@status_bin", SqlDbType.Bit).Value = true;

                cmd.Parameters.Add("@day_last_minute_num", SqlDbType.SmallInt).Value = shrLastminutenum;
                cmd.Parameters.Add("@Is_use_worldwide", SqlDbType.Bit).Value = bolIsWorldwide;
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@promotion_id"].Value;
            }
            StringBuilder str = new StringBuilder();
            str.Append("cat_id, product_id, supplier_id, title, date_start, date_end,");
            str.Append("quantity_min, day_min, day_advance_num, date_submit, day_mon, day_tue, day_wed, day_thu, day_fri ,day_sat, day_sun, is_weekend_all,");
            str.Append("Is_holiday_charge, max_set, Is_breakfast, breakfast_charge, status, comment,pro_group_item_id,iscancellation,Is_last_minute,promotion_score,promotion_score_benefit,status_bin,day_last_minute_num,Is_use_worldwide");
            

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_promotion, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductId, "tbl_promotion_extra_net", str.ToString(), "promotion_id", ret);
            //========================================================================================================================================================

            return ret;
        }
        public bool UpdatePromotionProcode(int intPromotionId, string ProCode, int intProductId)
        {
            int ret = 0;
            
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_promotion_extra_net", "pro_code", "promotion_id", intPromotionId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
               
                SqlCommand cmd = new SqlCommand("UPDATE tbl_promotion_extra_net SET pro_code=@pro_code WHERE promotion_id=@promotion_id", cn);
                cmd.Parameters.Add("@pro_code", SqlDbType.VarChar).Value = ProCode;
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_promotion, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_promotion_extra_net", "pro_code", arroldValue, "promotion_id", intPromotionId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }
        public bool UpdatePromotionStatus(int intPromotionId, int intProductId)
        {
            int ret = 0;
            bool Status = true;

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_promotion_extra_net", "status", "promotion_id", intPromotionId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                cn.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT status FROM tbl_promotion_extra_net WHERE promotion_id = @promotion_id", cn);
                cmd1.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                bool bolStatus = (bool)ExecuteScalar(cmd1);

                if (bolStatus)
                    Status = false;

                StringBuilder Query = new StringBuilder();
                Query.Append("UPDATE tbl_promotion_extra_net SET status=@status WHERE promotion_id=@promotion_id");
                
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);


                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = Status;
                
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                
                ret = ExecuteNonQuery(cmd);
            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_promotion, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_promotion_extra_net","status", arroldValue, "promotion_id", intPromotionId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }


        public bool UpdatePromotion(int intPromotionId, int ProductId, string Title, DateTime dDateStart, DateTime dDateEnd, bool bolIsMon, bool bolIsTue, bool bolIsWed, bool bolIsThu, bool bolIsFri, bool bolIsSat, bool bolIsSun, byte bytDaymin, short byteAdvanceDay, byte bytHolidayAll, byte bytMaxSet,
 decimal ABFCharge, bool IsCancelltion, bool IsLastMinute, byte bytScore, byte bytScoreBenefit, short shLastMinuteNum, bool bolIsWorldwide)
        {
            int ret = 0;
            StringBuilder str = new StringBuilder();
            str.Append("title, date_start, date_end,");
            str.Append("day_min, day_advance_num,  day_mon, day_tue, day_wed, day_thu, day_fri ,day_sat, day_sun,");
            str.Append("Is_holiday_charge, max_set,  breakfast_charge, status, comment,iscancellation,Is_last_minute,promotion_score,promotion_score_benefit,status_bin,day_last_minute_num,Is_use_worldwide");

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_promotion_extra_net", str.ToString(), "promotion_id", intPromotionId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder Query = new StringBuilder();
                Query.Append("UPDATE tbl_promotion_extra_net SET title=@title, date_start=@date_start, date_end=@date_end,");
                Query.Append(" day_min=@day_min, day_advance_num=@day_advance_num, day_mon=@day_mon,");
                Query.Append(" day_tue=@day_tue, day_wed=@day_wed, day_thu=@day_thu, day_fri=@day_fri ,day_sat=@day_sat, day_sun=@day_sun,");
                Query.Append(" Is_holiday_charge=@Is_holiday_charge, max_set=@max_set, breakfast_charge=@breakfast_charge, ");
                Query.Append(" iscancellation=@iscancellation, Is_last_minute=@Is_last_minute , promotion_score=@promotion_score,promotion_score_benefit=@promotion_score_benefit,day_last_minute_num=@day_last_minute_num, Is_use_worldwide=@Is_use_worldwide WHERE promotion_id=@promotion_id");
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = Title;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                
                cmd.Parameters.Add("@day_min", SqlDbType.TinyInt).Value = bytDaymin;
                cmd.Parameters.Add("@day_advance_num", SqlDbType.SmallInt).Value = byteAdvanceDay;
               

                cmd.Parameters.Add("@day_mon", SqlDbType.Bit).Value = bolIsMon;
                cmd.Parameters.Add("@day_tue", SqlDbType.Bit).Value = bolIsTue;
                cmd.Parameters.Add("@day_wed", SqlDbType.Bit).Value = bolIsWed;
                cmd.Parameters.Add("@day_thu", SqlDbType.Bit).Value = bolIsThu;
                cmd.Parameters.Add("@day_fri", SqlDbType.Bit).Value = bolIsFri;
                cmd.Parameters.Add("@day_sat", SqlDbType.Bit).Value = bolIsSat;
                cmd.Parameters.Add("@day_sun", SqlDbType.Bit).Value = bolIsSun;

                
                cmd.Parameters.Add("@Is_holiday_charge", SqlDbType.TinyInt).Value = bytHolidayAll;
                cmd.Parameters.Add("@max_set", SqlDbType.TinyInt).Value = bytMaxSet;
                //cmd.Parameters.Add("@Is_breakfast", SqlDbType.TinyInt).Value = IsABF;
                cmd.Parameters.Add("@breakfast_charge", SqlDbType.Money).Value = ABFCharge;
              
                //cmd.Parameters.Add("@pro_group_item_id", SqlDbType.SmallInt).Value = shrGropId;
                cmd.Parameters.Add("@iscancellation", SqlDbType.Bit).Value = IsCancelltion;
                cmd.Parameters.Add("@is_last_minute", SqlDbType.Bit).Value = IsLastMinute;
                cmd.Parameters.Add("@promotion_score", SqlDbType.TinyInt).Value = bytScore;
                cmd.Parameters.Add("@promotion_score_benefit", SqlDbType.TinyInt).Value = bytScoreBenefit;
                cmd.Parameters.Add("@day_last_minute_num", SqlDbType.SmallInt).Value = shLastMinuteNum;
                cmd.Parameters.Add("@Is_use_worldwide", SqlDbType.Bit).Value = bolIsWorldwide;
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;

                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

                //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_promotion, StaffLogActionType.Update, StaffLogSection.Product, ProductId,
                "tbl_promotion_extra_net", str.ToString(), arroldValue, "promotion_id", intPromotionId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }
    }
}