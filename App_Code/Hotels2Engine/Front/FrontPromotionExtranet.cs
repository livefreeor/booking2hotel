using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;

/// <summary>
/// Summary description for FrontPromotionExtranet
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontPromotionExtranet
    {
        public int PromotionID { get; set; }
        public byte CategoryID { get; set; }
        public int ProductID { get; set; }
        public short SupplierID { get; set; }
        public string Title { get; set; }
        public string TitleLang { get; set; }
        public string DetailLang { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public Nullable<DateTime> TimeStart { get; set; }
        public Nullable<DateTime> TimeEnd { get; set; }
        public byte QuantityMin { get; set; }
        public byte DayMin { get; set; }
        public short DayAdvanceNum { get; set; }
        public DateTime DateSubmit { get; set; }
        public bool DayMon { get; set; }
        public bool DayTue { get; set; }
        public bool DayWed { get; set; }
        public bool DayThu { get; set; }
        public bool DayFri { get; set; }
        public bool DaySat { get; set; }
        public bool DaySun { get; set; }
        public byte IsWeekEndAll { get; set; }
        public byte IsHolidayCharge { get; set; }
        public byte MaxSet { get; set; }
        public byte IsBreakfast { get; set; }
        public decimal BreakfastCharge { get; set; }
        public bool Status { get; set; }
        public string Comment { get; set; }
        public bool IsCancellation { get; set; }
        public FrontPromotionExtranet()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public FrontPromotionExtranet GetPromotionExtraByID(int promotionID)
        {
            string sqlCommand = "select top 1  pm.promotion_id,pm.cat_id,pm.product_id,pm.supplier_id,pm.title,pmc.title as title_lang,pmc.detail,pm.date_start,";
            sqlCommand = sqlCommand + " pm.date_end,pm.time_start,pm.time_end,pm.quantity_min,pm.day_min,pm.day_advance_num,pm.date_submit,pm.day_mon,pm.day_tue,pm.day_wed,";
            sqlCommand = sqlCommand + " pm.day_thu,pm.day_fri,pm.day_sat,pm.day_sun,pm.is_weekend_all,pm.is_holiday_charge,pm.max_set,pm.is_breakfast,pm.breakfast_charge,pm.status,pm.comment,pm.iscancellation";
            sqlCommand = sqlCommand + " from tbl_promotion_extra_net pm,tbl_promotion_content_extra_net pmc where pm.promotion_id=pmc.promotion_id and pmc.lang_id=1 and pm.promotion_id=" + promotionID;

            DataConnect objConn = new DataConnect();
            SqlDataReader reader = objConn.GetDataReader(sqlCommand);

            if (reader.Read())
            {
                this.PromotionID = (int)reader["promotion_id"];
                this.CategoryID = (byte)reader["cat_id"];
                this.ProductID = (int)reader["product_id"];
                this.SupplierID = (short)reader["supplier_id"];
                this.Title = reader["title"].ToString();
                this.TitleLang = reader["title_lang"].ToString();
                this.DetailLang = reader["detail"].ToString();
                this.DateStart = (DateTime)reader["date_start"];
                this.DateEnd = (DateTime)reader["date_end"];
                this.TimeStart = (reader["time_start"] == DBNull.Value ? null : (DateTime?)reader["time_start"]);
                this.TimeEnd = (reader["time_end"] == DBNull.Value ? null : (DateTime?)reader["time_end"]);
                this.QuantityMin = (byte)reader["quantity_min"];
                this.DayMin = (byte)reader["day_min"];
                this.DayAdvanceNum = (short)reader["day_advance_num"];
                this.DateSubmit = (DateTime)reader["date_submit"];
                this.DayMon = (bool)reader["day_mon"];
                this.DayTue = (bool)reader["day_tue"];
                this.DayWed = (bool)reader["day_wed"];
                this.DayThu = (bool)reader["day_thu"];
                this.DayFri = (bool)reader["day_fri"];
                this.DaySat = (bool)reader["day_sat"];
                this.DaySun = (bool)reader["day_sun"];
                this.IsWeekEndAll = (byte)reader["is_weekend_all"];
                this.IsHolidayCharge = (byte)reader["is_holiday_charge"];
                this.MaxSet = (byte)reader["max_set"];
                this.IsBreakfast = (byte)reader["is_breakfast"];
                this.BreakfastCharge = (decimal)reader["breakfast_charge"];
                this.Status = (bool)reader["status"];
                this.Comment = reader["comment"].ToString();
                this.IsCancellation = (bool)reader["iscancellation"];
            }

            objConn.Close();
            return this;
        }
    }
}