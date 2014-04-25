using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for Annoucement
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductOptionSupplementQuantity : Hotels2BaseClass
    {
       

        public int SupplementID { get; set; }
        public int ConditionID { get; set; }
        public short SupplierID { get; set; }
        public int PeriodID { get; set; }
        public short QuanMax { get; set; }
        public short QuanMin { get; set; }
        public decimal SupAmount { get; set; }

        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();

        public List<object> GetSupQuantity(int intCondition, short shrSupplierId, int intPeriodId)
        {
            var Result = from sq in dcProduct.tbl_price_suplement_quantities
                         where sq.condition_id == intCondition && sq.supplier_id == shrSupplierId && sq.period_id == intPeriodId
                         select sq;

            return MappingObjectFromDataContextCollection(Result);
        }

        public static bool UpdateSupQuan(int intSupconId, short QuanMIN, short QuanMAX, decimal decAmount)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_price_suplement_quantity", "quantity_min,quantity_max,supplement", "supplement_id", intSupconId);
            //============================================================================================================================
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            int Update = dcProduct.ExecuteCommand("UPDATE tbl_price_suplement_quantity SET quantity_min={0},quantity_max={1},supplement={2} WHERE supplement_id={3}", QuanMIN, QuanMAX, decAmount, intSupconId);
           

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Supplement, StaffLogActionType.Update, StaffLogSection.Product,
                null, "tbl_price_suplement_quantity", "quantity_min,quantity_max,supplement", arroldValue, "supplement_id", intSupconId);
            //==================================================================================================================== COMPLETED ========
            return (Update == 1);
        }

        public static int InsertSupQuan(int intCondition, short shrSupplierId, int intPeriodId, short QuanMIN, short QuanMAX, decimal decAmount)
        {
            
            ProductOptionSupplementQuantity cSupQuan = new ProductOptionSupplementQuantity();
            //int inSert = dcProduct.ExecuteCommand("INSERT INTO tbl_price_suplement_quantity(condition_id,supplier_id,period_id,quantity_min,quantity_max,supplement)VALUES({0},{1},{2},{3},{4},{5})",
            //    intCondition, shrSupplierId, intPeriodId, QuanMIN, QuanMAX, decAmount);
            int inSert  = 0;
            using (SqlConnection cn = new SqlConnection(cSupQuan.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_price_suplement_quantity(condition_id,supplier_id,period_id,quantity_min,quantity_max,supplement)VALUES(@condition_id,@supplier_id,@period_id,@quantity_min,@quantity_max,@supplement); SET @supplement_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intCondition;
                cmd.Parameters.Add("@supplier_id", SqlDbType.Int).Value = shrSupplierId;
                cmd.Parameters.Add("@period_id", SqlDbType.Int).Value = intPeriodId;
                cmd.Parameters.Add("@quantity_min", SqlDbType.Int).Value = QuanMIN;
                cmd.Parameters.Add("@quantity_max", SqlDbType.Int).Value = QuanMAX;
                cmd.Parameters.Add("@supplement", SqlDbType.Int).Value = decAmount;
                
                cmd.Parameters.Add("@supplement_id", SqlDbType.Int).Direction =  ParameterDirection.Output;
                cn.Open();
                cSupQuan.ExecuteNonQuery(cmd);
                inSert = (int)cmd.Parameters["@supplement_id"].Value;
            }
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_Supplement, StaffLogActionType.Insert, StaffLogSection.Product,
                int.Parse(HttpContext.Current.Request.QueryString["pid"]), "tbl_price_suplement_quantity", "condition_id,supplier_id,period_id,quantity_min,quantity_max,supplement", "supplement_id", inSert);
            //========================================================================================================================================================
            return inSert;
        }
    }
}