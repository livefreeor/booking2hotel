using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Linq;
using System.Web;
using System.Reflection;
using Hotels2thailand.LinqProvider.Staff;
using Hotels2thailand.Suppliers;



/// <summary>
/// Summary description for Staff
/// </summary>
/// 
namespace Hotels2thailand.Staffs
{
    public partial class Staff_Activity_Price : Hotels2BaseClass
    {

        public Staff_Activity_Price() { } 

        public int PriceActivityID { get; set; }
        //public int PriceId { get; set; }
        public byte ActionTypeId { get; set; }
        public decimal PriceValue { get; set; }
        public DateTime DatePrice { get; set; }
        public DateTime DateActivity { get; set; }
        public short StaffId { get; set; }



        public  void InsertActivityRate(int intProductId, short shrSupplier , int intConditionId, decimal dePrice, DateTime dDatePrice, short shrStaffId, StaffLogActionType acType, SqlConnection cn)
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO tbl_staff_activity_price_extranet (product_id,supplier_id,condition_id,type_id,price,date_price,date_activity,staff_id)VALUES(@product_id,@supplier_id,@condition_id,@type_id,@price,@date_price,@date_activity,@staff_id)", cn);
            cmd1.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
            cmd1.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplier;
            cmd1.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
            cmd1.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = acType;
            //cmd1.Parameters.Add("@price_id", SqlDbType.VarChar).Value = strPriceId;
            cmd1.Parameters.Add("@price", SqlDbType.Money).Value = dePrice;
            cmd1.Parameters.Add("@date_price", SqlDbType.SmallDateTime).Value = dDatePrice;
            cmd1.Parameters.Add("@date_activity", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
            cmd1.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = shrStaffId;

            ExecuteNonQuery(cmd1);
        }

        public void InsertActivityRate_Period(int intProductId, short shrSupplier, int intPriceId_period, int intConditionId, decimal dePrice, DateTime dDAteStart, DateTime dDateEnd, bool bolIsSun, bool bolIsMon, bool bolIsTue, bool bolIsWed, bool bolIsthu, bool bolIsFri, bool bolIssat, decimal decSurcharge,short shrStaffId, StaffLogActionType acType, SqlConnection cn)
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO tbl_staff_activity_price_extranet_period (product_id,supplier_id,condition_id,price_period_id,type_id,price,date_start,date_end,sub_day_sun,sub_day_mon,sub_day_tue,sub_day_wed,sub_day_thu,sub_day_fri,sub_day_sat,supplement,date_activity,staff_id)VALUES(@product_id,@supplier_id,@condition_id,@price_period_id,@type_id,@price,@date_start,@date_end,@sub_day_sun,@sub_day_mon,@sub_day_tue,@sub_day_wed,@sub_day_thu,@sub_day_fri,@sub_day_sat,@supplement,@date_activity,@staff_id)", cn);
            cmd1.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
            cmd1.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplier;
            cmd1.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
            cmd1.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = acType;
            cmd1.Parameters.Add("@price_period_id", SqlDbType.Int).Value = intPriceId_period;
            cmd1.Parameters.Add("@price", SqlDbType.Money).Value = dePrice;
            //cmd1.Parameters.Add("@date_price", SqlDbType.SmallDateTime).Value = dDatePrice;

            cmd1.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDAteStart;
            cmd1.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;


            cmd1.Parameters.Add("@sub_day_sun", SqlDbType.Bit).Value = bolIsSun;
            cmd1.Parameters.Add("@sub_day_mon", SqlDbType.Bit).Value = bolIsMon;
            cmd1.Parameters.Add("@sub_day_tue", SqlDbType.Bit).Value = bolIsTue;
            cmd1.Parameters.Add("@sub_day_wed", SqlDbType.Bit).Value = bolIsWed;
            cmd1.Parameters.Add("@sub_day_thu", SqlDbType.Bit).Value = bolIsthu;
            cmd1.Parameters.Add("@sub_day_fri", SqlDbType.Bit).Value = bolIsFri;
            cmd1.Parameters.Add("@sub_day_sat", SqlDbType.Bit).Value = bolIssat;
            cmd1.Parameters.Add("@supplement", SqlDbType.Money).Value = decSurcharge;
            cmd1.Parameters.Add("@date_activity", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
            cmd1.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = shrStaffId;

            ExecuteNonQuery(cmd1);
        }
    }
}