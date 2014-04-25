using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.Production;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for ProductPaymentPlan
/// </summary>
/// 

namespace Hotels2thailand.Production
{
    public class ProductPaymentPlan : Hotels2BaseClass
    {
        public int PaymentPlanID { get; set; }
        public int ProductID { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public byte DayAdvance { get; set; }
        public byte DayPayment { get; set; }
        public byte DepositPercent { get; set; }
        public byte DepositRoomNight { get; set; }

        //private byte _day_advance;
        //private byte _day_payment;
        //private byte _deposit_room_night;

        public ProductPaymentPlan()
        {
            //_day_advance = 45;
            //_day_payment = 7;
            //_deposit_room_night = 0;
        }

        public List<object> GetProductPaymentPlanListByProductId(int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_payment_plan WHERE product_id=@product_id ORDER BY date_start, date_end", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public bool DeletePaymentPlan(int intPaymentPlanId, int intProduct_id)
        {
            IList<object[]> arroldValue = null;


            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            arroldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_payment_plan", "payment_plan_id", intPaymentPlanId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM  tbl_product_payment_plan WHERE payment_plan_id=@payment_plan_id", cn);
                cmd.Parameters.Add("@payment_plan_id", SqlDbType.Int).Value = intPaymentPlanId;
                //cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct_id;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            //int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ==
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_paymentPlan, StaffLogActionType.Delete, StaffLogSection.Product, intProduct_id,
                "tbl_product_payment_plan", arroldValue, "payment_plan_id", intPaymentPlanId);
            //============================================================================================================================
            return (ret == 1);
        }

        public bool UpdateProductPaymentPlan(int intPaymentPlanId, int intProduct_id, DateTime dDAteStart, DateTime dDateEnd, byte bytAdvance, byte bytDayPayment)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_payment_plan", "payment_plan_id,product_id,date_start,date_end,day_advance,day_payment", "payment_plan_id", intPaymentPlanId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_payment_plan SET date_start=@date_start,date_end=@date_end,day_advance=@day_advance,day_payment=@day_payment WHERE payment_plan_id=@payment_plan_id", cn);
                cmd.Parameters.Add("@payment_plan_id", SqlDbType.Int).Value = intPaymentPlanId;
                //cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct_id;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDAteStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cmd.Parameters.Add("@day_advance", SqlDbType.TinyInt).Value = bytAdvance;
                cmd.Parameters.Add("@day_payment", SqlDbType.TinyInt).Value = bytDayPayment;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            //int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_paymentPlan, StaffLogActionType.Update, StaffLogSection.Product, intProduct_id,
                "tbl_product_payment_plan", "payment_plan_id,product_id,date_start,date_end,day_advance,day_payment", arroldValue, "payment_plan_id", intPaymentPlanId);
            //==================================================================================================================== COMPLETED ========
            
            return (ret == 1);
        }

        public int InsertNewPaymentPlan(int intProduct_id , DateTime dDAteStart, DateTime dDateEnd, byte bytAdvance, byte bytDayPayment)
        {
            int ret = 0;
            int Payment = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_payment_plan (product_id,date_start,date_end,day_advance,day_payment) VALUES(@product_id,@date_start,@date_end,@day_advance,@day_payment); SET @payment_plan_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct_id;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDAteStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cmd.Parameters.Add("@day_advance", SqlDbType.TinyInt).Value = bytAdvance;
                cmd.Parameters.Add("@day_payment", SqlDbType.TinyInt).Value = bytDayPayment;
                cmd.Parameters.Add("@payment_plan_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                Payment = (int)cmd.Parameters["@payment_plan_id"].Value;
            }
            
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_rate, StaffLogActionType.Insert, StaffLogSection.Product,
                intProduct_id, "tbl_product_payment_plan", "payment_plan_id,product_id,date_start,date_end,day_advance,day_payment", "payment_plan_id", Payment);
            //========================================================================================================================================================

            return ret;
        }
        

        



    }
}