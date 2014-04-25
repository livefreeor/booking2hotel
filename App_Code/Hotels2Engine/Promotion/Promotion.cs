using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for Promotion
/// </summary>
/// 
namespace Hotels2thailand.ProductOption
{
    public class Promotion:Hotels2BaseClass
    {
        public int PromotionId { get; set; }
        public byte ProCatId { get; set; }
        public int ProductId { get; set; }
        public short SupplierId { get; set; }
        public string Title { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DateuseStart { get; set; }
        public DateTime DateuseEnd { get; set; }
        public Nullable<DateTime> TimeStart { get; set; }
        public Nullable<DateTime> TimeEnd { get; set; }
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



        public Promotion()
        {
           
        }


        public List<object> GetPromotionListByProductIdAndSupplierId(int intProductId, short shrSupplierId, bool bolStatus)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder Query = new StringBuilder();
                Query.Append("SELECT promotion_id, cat_id, product_id, supplier_id, title, date_start, date_end, date_use_start, date_use_end, time_start, time_end,");
                Query.Append(" quantity_min, day_min, day_advance_num, date_submit, day_mon, day_tue, day_wed, day_thu, day_fri, day_sat,");
                Query.Append(" day_sun, is_weekend_all, Is_holiday_charge, max_set, Is_breakfast, breakfast_charge, status, comment");
                Query.Append(" FROM tbl_promotion");
                Query.Append(" WHERE product_id = @product_id AND supplier_id=@supplier_id AND status = @status ORDER BY status DESC, date_submit, title ");
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.Int).Value = shrSupplierId;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public Promotion GetPromotionById(int intPromotionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder Query = new StringBuilder();
                Query.Append("SELECT promotion_id, cat_id, product_id, supplier_id, title, date_start, date_end, date_use_start, date_use_end, time_start, time_end,");
                Query.Append(" quantity_min, day_min, day_advance_num, date_submit, day_mon, day_tue, day_wed, day_thu, day_fri, day_sat,");
                Query.Append(" day_sun, is_weekend_all, Is_holiday_charge, max_set, Is_breakfast, breakfast_charge, status, comment");
                Query.Append(" FROM tbl_promotion");
                Query.Append(" WHERE promotion_id = @PromotionId");

                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@PromotionId", SqlDbType.Int).Value = intPromotionId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return (Promotion)MappingObjectFromDataReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }

        public int InsertNewPromotionPanelDetail( byte bytCatId, int ProductId, short shrSupplierId,  string Title, DateTime dDateStart , DateTime dDateEnd , string Comment)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder Query = new StringBuilder();
                Query.Append("INSERT INTO tbl_promotion (cat_id, product_id, supplier_id, title, date_start, date_end, date_use_start, date_use_end,");
                Query.Append(" quantity_min, day_min, day_advance_num, date_submit, day_mon, day_tue, day_wed, day_thu, day_fri ,day_sat, day_sun, is_weekend_all,");
                Query.Append(" Is_holiday_charge, max_set, Is_breakfast, breakfast_charge, status, comment)");
                Query.Append(" VALUES(@cat_id, @product_id, @supplier_id, @title, @date_start, @date_end, @date_use_start, @date_use_end,");
                Query.Append(" @quantity_min, @day_min ,@day_advance_num ,@date_submit,'1','1','1','1','1','1','1', @is_weekend_all,");
                Query.Append(" @Is_holiday_charge,@max_set,@Is_breakfast,@breakfast_charge,@status,@comment);SET @promotion_id = SCOPE_IDENTITY();");
                
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.Int).Value = bytCatId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = ProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = Title;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cmd.Parameters.Add("@date_use_start", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmd.Parameters.Add("@date_use_end", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmd.Parameters.Add("@quantity_min", SqlDbType.TinyInt).Value = 1;
                cmd.Parameters.Add("@day_min", SqlDbType.TinyInt).Value = 1;
                cmd.Parameters.Add("@day_advance_num", SqlDbType.SmallInt).Value = 1;
                cmd.Parameters.Add("@date_submit", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmd.Parameters.Add("@is_weekend_all", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@Is_holiday_charge", SqlDbType.TinyInt).Value = 1;
                cmd.Parameters.Add("@max_set", SqlDbType.TinyInt).Value = 100;
                cmd.Parameters.Add("@Is_breakfast", SqlDbType.TinyInt).Value = true;
                cmd.Parameters.Add("@breakfast_charge", SqlDbType.Money).Value = 0;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = Comment;
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@promotion_id"].Value;
            }
            StringBuilder str = new StringBuilder();
            str.Append("cat_id,product_id,supplier_id,title,date_start,date_end,date_use_start,date_use_end,quantity_min,");
            str.Append("day_min,day_advance_num,date_submit,day_mon,day_tue,day_wed,day_thu,day_fri,day_sat,day_sun,is_weekend_all,");
            str.Append("Is_holiday_charge,max_set,Is_breakfast,breakfast_charge,status,comment");

            
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_promotion, StaffLogActionType.Insert, StaffLogSection.Product,
                int.Parse(HttpContext.Current.Request.QueryString["pid"]), "tbl_promotion", str.ToString(), "promotion_id", ret);
            //========================================================================================================================================================
             

            ////=== STAFF ACTIVITY =====================================================================================================================================
            //StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_detail, StaffLogActionType.Insert, StaffLogSection.Product,
            //    intProductId, "tbl_product_landmark", "product_id,landmark_id,transport_id,transport_time,transport_distance", "product_id,landmark_id", intProductId, LandMarkId);
            ////========================================================================================================================================================
            return ret;
        }

        public bool UpdatePromotionPanelDetail(int intPromotionId, byte bytCatId, string Title, DateTime dDateStart, DateTime dDateEnd, string Comment, bool Status)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_promotion", "cat_id,title,date_start,date_end,status,comment", "promotion_id", intPromotionId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder Query = new StringBuilder();
               Query.Append("UPDATE tbl_promotion SET cat_id=@cat_id, title=@title, date_start=@date_start, date_end=@date_end, comment=@comment, status=@status WHERE promotion_id=@PromotionId");

                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.Int).Value = bytCatId;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = Title;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = Status;
                cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = Comment;
                cmd.Parameters.Add("@PromotionId", SqlDbType.Int).Value = intPromotionId;
                cn.Open();
                 ret = ExecuteNonQuery(cmd);
                
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_promotion, StaffLogActionType.Update, StaffLogSection.Product, int.Parse(HttpContext.Current.Request.QueryString["pid"]),
                "tbl_promotion", "cat_id,title,date_start,date_end,status,comment", arroldValue, "promotion_id", intPromotionId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        public bool UpdatePromotionPanelInformation(int intPromotionId, byte IsWeekendAll, bool bolIsMon, bool bolIsTue, bool bolIsWed, bool bolIsThu, bool bolIsFri , bool bolIsSat, bool bolIsSun,
            byte bytDayMin, byte bytQuantityMin, short shrDayAdvance, byte Isholiday, byte bytMaxSet, byte IsBreakfast, decimal decBreakfastCharge, DateTime dUseStart, DateTime dUseEnd, Nullable<DateTime> tUseStart, Nullable<DateTime> tUseEnd)
        {
            StringBuilder str = new StringBuilder();
            str.Append("date_use_start, date_use_end,quantity_min,");
            str.Append("day_min, day_advance_num, date_submit, day_mon, day_tue, day_wed, day_thu, day_fri ,day_sat, day_sun, is_weekend_all,");
            str.Append("Is_holiday_charge, max_set, Is_breakfast, breakfast_charge, status, comment");
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_promotion", str.ToString(), "promotion_id", intPromotionId);
            //============================================================================================================================
            int ret  = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder Query = new StringBuilder();
                Query.Append("UPDATE tbl_promotion SET is_weekend_all=@is_weekend_all, day_mon=@day_mon, day_tue=@day_tue, day_wed=@day_wed, day_thu=@day_thu, day_fri=@day_fri, day_sat=@day_sat, day_sun=@day_sun,");
                Query.Append(" day_min=@day_min, quantity_min=@quantity_min, day_advance_num=@day_advance_num, Is_holiday_charge=@Is_holiday_charge, max_set=@max_set, Is_breakfast=@Is_breakfast, breakfast_charge=@breakfast_charge,");
                if (tUseStart != null && tUseEnd != null)
                {
                    Query.Append(" date_use_start=@date_use_start, date_use_end=@date_use_end, time_start=@time_start, time_end=@time_end WHERE promotion_id=@PromotionId");
                }
                else
                {
                    Query.Append(" date_use_start=@date_use_start, date_use_end=@date_use_end, time_start = NULL, time_end = NULL WHERE promotion_id=@PromotionId");
                }

                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@PromotionId", SqlDbType.Int).Value = intPromotionId;
                cmd.Parameters.Add("@is_weekend_all", SqlDbType.TinyInt).Value = IsWeekendAll;
                cmd.Parameters.Add("@day_mon", SqlDbType.Bit).Value = bolIsMon;
                cmd.Parameters.Add("@day_tue", SqlDbType.Bit).Value = bolIsTue;
                cmd.Parameters.Add("@day_wed", SqlDbType.Bit).Value = bolIsWed;
                cmd.Parameters.Add("@day_thu", SqlDbType.Bit).Value = bolIsThu;
                cmd.Parameters.Add("@day_fri", SqlDbType.Bit).Value = bolIsFri;
                cmd.Parameters.Add("@day_sat", SqlDbType.Bit).Value = bolIsSat;
                cmd.Parameters.Add("@day_sun", SqlDbType.Bit).Value = bolIsSun;
                cmd.Parameters.Add("@day_min", SqlDbType.TinyInt).Value = bytDayMin;
                cmd.Parameters.Add("@quantity_min", SqlDbType.TinyInt).Value = bytQuantityMin;
                cmd.Parameters.Add("@day_advance_num", SqlDbType.SmallInt).Value = shrDayAdvance;
                cmd.Parameters.Add("@Is_holiday_charge", SqlDbType.TinyInt).Value = Isholiday;
                cmd.Parameters.Add("@max_set", SqlDbType.TinyInt).Value = bytMaxSet;
                cmd.Parameters.Add("@Is_breakfast", SqlDbType.TinyInt).Value = IsBreakfast;
                cmd.Parameters.Add("@breakfast_charge", SqlDbType.Money).Value = decBreakfastCharge;
                cmd.Parameters.Add("@date_use_start", SqlDbType.SmallDateTime).Value = dUseStart;
                cmd.Parameters.Add("@date_use_end", SqlDbType.SmallDateTime).Value = dUseEnd;
                if (tUseStart != null && tUseEnd != null)
                {
                    cmd.Parameters.Add("@time_start", SqlDbType.SmallDateTime).Value = tUseStart;
                    cmd.Parameters.Add("@time_end", SqlDbType.SmallDateTime).Value = tUseEnd;
                }
                
                cn.Open();
               ret = ExecuteNonQuery(cmd);
                
            }
            
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_promotion, StaffLogActionType.Update, StaffLogSection.Product, int.Parse(HttpContext.Current.Request.QueryString["pid"]),
                "tbl_promotion", str.ToString(), arroldValue, "promotion_id", intPromotionId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        public bool UpdateStatus(int intPromotionId)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_promotion", "status", "promotion_id", intPromotionId);
            //============================================================================================================================
            int ret  = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                Promotion cPromotion = new Promotion();


                StringBuilder query = new StringBuilder();
                if (cPromotion.GetPromotionById(intPromotionId).Status)
                {
                    query.Append("UPDATE tbl_promotion SET status=0 WHERE promotion_id=@promotion_id");
                }
                else
                {
                    query.Append("UPDATE tbl_promotion SET status=1 WHERE promotion_id=@promotion_id");
                }
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
               
                
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_promotion, StaffLogActionType.Update, StaffLogSection.Product, int.Parse(HttpContext.Current.Request.QueryString["pid"]),
                "tbl_promotion", "status", arroldValue, "promotion_id", intPromotionId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        
    }
}