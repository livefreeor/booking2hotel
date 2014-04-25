using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using Hotels2thailand.Staffs;


/// <summary>
/// Summary description for ProductCategory
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductCommission : Hotels2BaseClass
    {
        public int Commission_id { get; set; }
        public int ProductId { get; set; }
        public short SupplierId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public byte Commission { get; set; }

        public List<object> GetCommissionBySuppierIdAndProductID(short shrSupplierId, int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT commission_id, product_id,supplier_id,date_start,date_end,commission  FROM tbl_product_commission WHERE product_id=@product_id AND supplier_id=@supplier_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int SelectCountCommissionPeriod(int intProductId, short shrSupplierId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tbl_product_commission WHERE product_id=@product_id AND supplier_id=@supplier_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();
                int Count = (int)ExecuteScalar(cmd);
                return Count;
            }
        }

        public int Insertnewcommission(int intProductId, short shrSupplirId, DateTime dDateStart, DateTime dDateEnd, byte bytcommission)
        {
            int ret = 0;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_commission (product_id,supplier_id,date_start,date_end,commission) VALUES(@product_id,@supplier_id,@date_start,@date_end,@commission);SET @commission_id = SCOPE_IDENTITY();",cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplirId;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cmd.Parameters.Add("@commission", SqlDbType.TinyInt).Value = bytcommission;
                cmd.Parameters.Add("@commission_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@commission_id"].Value;
            }

            //#Staff_Activity_Log==========================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_commission, StaffLogActionType.Insert, StaffLogSection.Product,
                intProductId, "tbl_product_commission", "product_id,supplier_id,date_start,date_end,commission",
                "commission_id", ret);
            //============================================================================================================================

            return ret; 
        }

        public bool UpdateProductcommissionbyCommissionId(int intCommissionId, int intProductId, DateTime dDateStart, DateTime dDateEnd, byte bytcommission)
        {
            int ret = 0;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_commission", "date_start,date_end,commission", "commission_id", intCommissionId);
            //============================================================================================================================

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_commission SET date_start=@date_start,date_end=@date_end,commission=@commission WHERE commission_id =@commission_id", cn);
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cmd.Parameters.Add("@commission", SqlDbType.TinyInt).Value = bytcommission;
                cmd.Parameters.Add("@commission_id", SqlDbType.Int).Value = intCommissionId;
                cn.Open();
                ret  = ExecuteNonQuery(cmd);

            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_commission, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_product_commission", "date_start,date_end,commission", arroldValue, "commission_id", intProductId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }
    }
}