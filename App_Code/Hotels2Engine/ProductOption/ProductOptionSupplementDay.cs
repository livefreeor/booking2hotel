using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for ProductOptionSupplementDay
/// </summary>
/// 
namespace Hotels2thailand.ProductOption
{
    public class ProductOptionSupplementDay:Hotels2BaseClass
    {
        
        public int SupplementDayId { get; set; }
        public short SupplierId { get; set; }
        public int OptionId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public decimal DaySun{ get; set; }
        public decimal DayMon { get; set; }
        public decimal DayTue { get; set; }
        public decimal DayWed { get; set; }
        public decimal DayThu { get; set; }
        public decimal DayFri { get; set; }
        public decimal DaySat { get; set; }
        public bool Status { get; set; }


        public ProductOptionSupplementDay()
        {
            DaySun = 0;
            DayMon = 0;
            DayTue = 0;
            DayWed = 0;
            DayThu = 0;
            DayFri = 0;
            DaySat = 0;
            Status = true;
        }
        private LinqProductionDataContext dcOptionSuppleMent = new LinqProductionDataContext();

        public ProductOptionSupplementDay getWeekdayById(int intSupplementDayId)
        {
            var Result = dcOptionSuppleMent.tbl_product_option_supplement_days.SingleOrDefault(psd => psd.supplement_day_id == intSupplementDayId);
            if (Result == null)
            {
                return null;
            }
            else
            {
                return (ProductOptionSupplementDay)MappingObjectFromDataContext(Result);
            }
        }

        public List<object> getWeekdayListBySupplierIdAndOptionId(short shrSupplirId, int intOptionId)
        {
            //HttpContext.Current.Response.Write(shrSupplirId + "+++" + intOptionId);
            //HttpContext.Current.Response.End();
            var Result = from psd in dcOptionSuppleMent.tbl_product_option_supplement_days
                         where psd.supplier_id == shrSupplirId && psd.option_id == intOptionId 
                         orderby psd.status descending
                         select psd;

            return MappingObjectFromDataContextCollection(Result);

        }

        public static int insertNewWeekDay(short shrSupplier ,int intOptionId, DateTime dDateStart, DateTime dDateEnd, decimal decSun, decimal decMon, decimal
            decTue, decimal decWed, decimal decThu, decimal decFri, decimal decSat, bool bolStatus)
        {
            LinqProductionDataContext dcOptionSuppleMent = new LinqProductionDataContext();
            
            var Insert = new tbl_product_option_supplement_day
            {
                supplier_id = shrSupplier,
                option_id = intOptionId,
                date_start = dDateStart,
                date_end = dDateEnd,
                day_sun = decSun,
                day_mon = decMon,
                day_tue = decTue,
                day_wed = decWed,
                day_thu = decThu,
                day_fri = decFri,
                day_sat = decSat,
                status = bolStatus
            };

            dcOptionSuppleMent.tbl_product_option_supplement_days.InsertOnSubmit(Insert);
            dcOptionSuppleMent.SubmitChanges();
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_Supplement, StaffLogActionType.Insert, StaffLogSection.Product,
                int.Parse(HttpContext.Current.Request.QueryString["pid"]), "tbl_product_option_supplement_day", "supplier_id,option_id,date_start,date_end,day_sun,day_mon,day_tue,day_wed,day_thu,day_fri,day_sat,status", "supplement_day_id", Insert.supplement_day_id);
            //========================================================================================================================================================
            return Insert.supplement_day_id;
        }

        public bool Update()
        {
            return ProductOptionSupplementDay.UpdateWeekday(this.SupplementDayId, this.DateStart, this.DateEnd, this.DaySun, this.DayMon, this.DayTue, this.DayWed,
                this.DayThu, this.DayFri, this.DaySat,  this.Status);
        }

        public static bool UpdateWeekday(int intSupplementDayId,  DateTime dDateStart, DateTime dDateEnd, decimal decSun, decimal decMon, decimal
            decTue, decimal decWed, decimal decThu, decimal decFri, decimal decSat,  bool bolStatus)
        {
            LinqProductionDataContext dcOptionSuppleMent = new LinqProductionDataContext();
            ProductOptionSupplementDay cSupDay = new ProductOptionSupplementDay
            {
                SupplementDayId = intSupplementDayId,
                DateStart = dDateStart,
                DateEnd = dDateEnd,
                DaySun = decSun,
                DayMon = decMon,
                DayTue = decTue,
                DayWed = decWed,
                DayThu = decThu,
                DayFri = decFri,
                DaySat = decSat,
                Status = bolStatus

            };

            var Updates = dcOptionSuppleMent.tbl_product_option_supplement_days.SingleOrDefault(sd => sd.supplement_day_id == cSupDay.SupplementDayId);
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep(Updates.date_start, Updates.date_end, Updates.day_sun, Updates.day_mon, Updates.day_tue, Updates.day_wed,
                Updates.day_thu, Updates.day_fri, Updates.day_sat, Updates.status);
            //============================================================================================================================
            Updates.date_start = cSupDay.DateStart;
            Updates.date_end = cSupDay.DateEnd;
            Updates.day_sun = cSupDay.DaySun;
            Updates.day_mon = cSupDay.DayMon;
            Updates.day_tue = cSupDay.DayTue;
            Updates.day_wed = cSupDay.DayWed;
            Updates.day_thu = cSupDay.DayThu;
            Updates.day_fri = cSupDay.DayFri;
            Updates.day_sat = cSupDay.DaySat;
            Updates.status = cSupDay.Status;
            dcOptionSuppleMent.SubmitChanges();

            int ret = 1;
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Supplement, StaffLogActionType.Update, StaffLogSection.Product,
                int.Parse(HttpContext.Current.Request.QueryString["pid"]), "tbl_product_option_supplement_day", "date_start,date_end,day_sun,day_mon,day_tue,day_wed,day_thu,day_fri,day_sat,status", arroldValue, "supplement_day_id", cSupDay.SupplementDayId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }

        //public static bool DeleteSupplementDay(int SupplementDayId)
        //{
        //    LinqProductionDataContext dcOptionSuppleMent = new LinqProductionDataContext();
        //    var Delete = dcOptionSuppleMent.tbl_product_option_supplement_days.SingleOrDefault(sd => sd.supplement_day_id == SupplementDayId);

        //    dcOptionSuppleMent.tbl_product_option_supplement_days.DeleteOnSubmit(Delete);
        //    dcOptionSuppleMent.SubmitChanges();

        //    int ret = 1;
        //    return (ret == 1);

        //}

    }
}