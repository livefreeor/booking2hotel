using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.Production;
using Hotels2thailand.Staffs;
using System.Text;


/// <summary>
/// Summary description for ProductOptionCondition
/// </summary>
/// 
namespace Hotels2thailand.ProductOption
{
    public class ProductOptionConditionPrice : Hotels2BaseClass
    {
        public int ConditionId { get; set; }
        public int PeriodId { get; set; }
        public decimal RatePrice { get; set; }
        public decimal RateOwn { get; set; }
        public decimal RateRack {get; set; }
        public bool Status {get; set; }


        private DateTime _date_Start = DateTime.MinValue;

        public DateTime DateStart
        {
            get 
            {
                ProductOptionPeriod cPeriod = new ProductOptionPeriod();
                _date_Start = cPeriod.getProductPeriodById(this.PeriodId).DateStart;
                return _date_Start; 
            }
            
        }

        private DateTime _date_end;

        public DateTime DateEnd
        {
            get
            {
                ProductOptionPeriod cPeriod = new ProductOptionPeriod();
                _date_end = cPeriod.getProductPeriodById(this.PeriodId).DateEnd;
                return _date_end;
            }
            
        }
        
        

        private LinqProductionDataContext dcOptionConditionPrice = new LinqProductionDataContext();

        //public ProductOptionConditionPrice Get

        public  static int InsertRateConditionPrice(int intConditionId, int intPeriodId, decimal decRatePrice, decimal decRateOwn, decimal decRateRack)
        {
            LinqProductionDataContext dcOptionConditionPrice = new LinqProductionDataContext();
            int intInsert = dcOptionConditionPrice.ExecuteCommand("INSERT INTO tbl_product_option_condition_price (condition_id,period_id,rate,rate_own,rate_rack,status) VALUES({0},{1},{2},{3},{4},{5})",
                intConditionId, intPeriodId, decRatePrice, decRateOwn, decRateRack, true);
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_rate, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductId, "tbl_product_option_condition_price", "condition_id,period_id,rate,rate_own,rate_rack,status", "condition_id,period_id", intConditionId, intPeriodId);
            //========================================================================================================================================================
            return intInsert;
        }

        public static int UpdateConditionPrice(int intConditionId, int intPeriodId, decimal decRatePrice, decimal decRateOwn, decimal decRateRack)
        {
            LinqProductionDataContext dcOptionConditionPrice = new LinqProductionDataContext();
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_condition_price", "rate,rate_own,rate_rack", "condition_id,period_id", intConditionId, intPeriodId);
            //============================================================================================================================

            int intUpdate = dcOptionConditionPrice.ExecuteCommand("UPDATE tbl_product_option_condition_price SET rate={0},rate_own={1},rate_rack={2} WHERE condition_id={3} AND period_id={4}", 
                decRatePrice, decRateOwn, decRateRack, intConditionId, intPeriodId, true);

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_rate, StaffLogActionType.Update, StaffLogSection.Product, ProductId,
                "tbl_product_option_condition_price", "rate,rate_own,rate_rack", arroldValue, "condition_id,period_id", intConditionId, intPeriodId);
            //==================================================================================================================== COMPLETED ========
            return intUpdate;
        }


        public static int IsHaveRecord(int intConditionId, int intPeriodId)
        {
             LinqProductionDataContext dcOptionConditionPrice = new LinqProductionDataContext();
             var Result = dcOptionConditionPrice.tbl_product_option_condition_prices.SingleOrDefault(pc => pc.condition_id == intConditionId && pc.period_id == intPeriodId);
             if (Result == null)
                 return 0;
             else
             {
                 return 1;
             }
        }

        public ProductOptionConditionPrice GetProductConditionPriceGala(int intConditionId, int intPeriodId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option_condition_price WHERE condition_id=@condition_id AND period_id=@period_id", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cmd.Parameters.Add("@period_id", SqlDbType.Int).Value = intPeriodId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return (ProductOptionConditionPrice)MappingObjectFromDataReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }

        public ProductOptionConditionPrice GetConditionPriceByConditionIdAndInPeriodID(int intConditionId, int intPeriodId)
        {

            var Result = dcOptionConditionPrice.tbl_product_option_condition_prices.SingleOrDefault(pc => pc.condition_id == intConditionId && pc.period_id == intPeriodId);
            if (Result == null)
                return null;
            else
            {
                return (ProductOptionConditionPrice)MappingObjectFromDataContext(Result);
            }
        }

        public List<object> getRateCurrentByConditionId(int intConditionId, short shrSupplierId)
        {
            StringBuilder strQuery = new StringBuilder();
            strQuery.Append("SELECT  poc.condition_id, poc.period_id, poc.rate, poc.rate_own, poc.rate_rack ,poc.status");
            strQuery.Append(" FROM tbl_product_period pd, tbl_product_option_condition_price poc");
            strQuery.Append(" WHERE pd.period_id = poc.period_id AND pd.date_end >= CONVERT(DateTime, convert(varchar(20),GETDATE(),101)) AND poc.condition_id = @ConditionId AND pd.supplier_id = @SupplierId");
            strQuery.Append(" ORDER BY pd.date_start , pd.date_end");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(strQuery.ToString(), cn);
                cmd.Parameters.Add("@ConditionId", SqlDbType.Int).Value = intConditionId;
                cmd.Parameters.Add("@SupplierId", SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }


        }

        public bool UpdateRatePeriodStatus(int intConditionId, int intPeriodId, bool bolStatus)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_condition_price", "status", "condition_id,period_id", intConditionId, intPeriodId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition_price SET status=@status WHERE condition_id=@Condition_id AND period_id=@period_id", cn);
                cmd.Parameters.Add("@Condition_id", SqlDbType.Int).Value = intConditionId;
                cmd.Parameters.Add("@period_id", SqlDbType.Int).Value = intPeriodId;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                
            }

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_rate, StaffLogActionType.Update, StaffLogSection.Product, ProductId,
                "tbl_product_option_condition_price", "status", arroldValue, "condition_id,period_id", intConditionId, intPeriodId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }
    }
}