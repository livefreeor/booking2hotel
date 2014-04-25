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
    public class Productdeposit : Hotels2BaseClass
    {
        public int DepositId { get; set; }
        public int ProductId { get; set; }
        public byte DepositCat { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public short Deposit { get; set; }
        public bool Status { get; set; }


        public IList<object> GetDepositListByProductId(int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_deposit WHERE product_id = @product_id AND status = 1 AND date_end >= DATEADD(HH,14,GETDATE()) ORDER BY date_end ASC", cn);
                cmd.Parameters.AddWithValue("@product_id", intProductId);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public bool UpdateStatusDeposit(int intProductId, int intDepId)
        {
            int ret = 0;
            ArrayList ObjOldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_deposit", "status", "deposit_id", intDepId);
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_deposit SET status = 0 WHERE deposit_id =@deposit_id", cn);
                cmd.Parameters.AddWithValue("@deposit_id", intDepId);
                cn.Open();
                ret = ExecuteNonQuery(cmd);

            }

            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Deposit, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_product_deposit", "status", ObjOldValue, "deposit_id", intDepId);

            return (ret == 1);
        }

        public bool UpdateDeposit(int intProductId, int intDepositId, short DepoistAmount, byte bytDepositCat, DateTime dDateStart, DateTime dDateEnd)
        {
            int ret = 0;
            ArrayList ObjOldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_deposit", "deposit_cat_id,deposit,date_start,date_end", "deposit_id", intDepositId);
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_deposit SET deposit_cat_id=@deposit_cat_id,date_start=@date_start,date_end=@date_end,deposit=@deposit WHERE deposit_id = @deposit_id", cn);
                cmd.Parameters.AddWithValue("@deposit_cat_id", bytDepositCat);
                cmd.Parameters.AddWithValue("@deposit", DepoistAmount);
                cmd.Parameters.AddWithValue("@date_start", dDateStart);
                cmd.Parameters.AddWithValue("@date_end", dDateEnd);
                cmd.Parameters.AddWithValue("@deposit_id", intDepositId);
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Deposit, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
               "tbl_product_deposit", "deposit_cat_id,deposit,date_start,date_end", ObjOldValue, "deposit_id", intDepositId);

            return (ret == 1);
        }

        public int InsertDeposit(int intProductId, byte bytDepositCat, short DepoistAmount, DateTime dDateStart, DateTime dDateEnd)
        {
            int DepositId = 0;
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_deposit (product_id,deposit_cat_id,date_start,date_end,deposit,status) VALUES(@product_id,@deposit_cat_id,@date_start,@date_end,@deposit,@status); SET @deposit_id = SCOPE_IDENTITY()", cn);
                cmd.Parameters.AddWithValue("@product_id", intProductId);
                cmd.Parameters.AddWithValue("@deposit_cat_id", bytDepositCat);
                cmd.Parameters.AddWithValue("@deposit", DepoistAmount);
                cmd.Parameters.AddWithValue("@date_start", dDateStart);
                cmd.Parameters.AddWithValue("@date_end", dDateEnd);
                cmd.Parameters.AddWithValue("@status", true);
                cmd.Parameters.Add("@deposit_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);

                DepositId = (int)cmd.Parameters["@deposit_id"].Value;
            }


            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_Deposit, StaffLogActionType.Insert, StaffLogSection.Product, intProductId,
               "tbl_product_deposit", "product_id,deposit_cat_id,date_start,date_end,deposit,status", "deposit_id", DepositId);

            return DepositId;
        }

    }
}