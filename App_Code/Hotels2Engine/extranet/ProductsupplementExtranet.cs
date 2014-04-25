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
    public class ProductsupplementExtranet : Hotels2BaseClass
    {

        public int SupplementID { get; set; }
        public short shrSupplier { get; set; }
        public int ProductId { get; set; }
        public string DateTitle { get; set; }
        public DateTime DateSupplement { get; set; }
        public bool Status { get; set; }


        public int IsSupplement(int intProductId, short shrSupplierId, DateTime dDAteSupplement)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(supplement_date_id) FROM tbl_product_supplement_date_extra_net WHERE supplier_id = @supplier_id AND product_id = @product_id AND status=1 AND date_supplement = @date_supplement", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@date_supplement", SqlDbType.VarChar).Value = dDAteSupplement;
                cn.Open();
                return (int)ExecuteScalar(cmd);
                
            }
        }
        public IList<object> GetSupplementDateByDateRange(int intProductId, short shrSupplierId, DateTime dDateFrom, DateTime dDAteTo, bool bolstatus )
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_supplement_date_extra_net WHERE status=@status AND product_id=@product_id AND supplier_id=@supplier_id AND date_supplement BETWEEN '" + dDateFrom.ToString("yyyy-MM-dd") + "' AND '" + dDAteTo.ToString("yyyy-MM-dd") + "' ORDER BY date_supplement", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolstatus;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public ProductsupplementExtranet GetSupplementDateByDate(int intProductId, short shrSupplierId, DateTime dDate, bool bolstatus)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_supplement_date_extra_net WHERE status=@status AND product_id=@product_id AND supplier_id=@supplier_id AND date_supplement= @date_supplement ", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@date_supplement", SqlDbType.SmallDateTime).Value = dDate;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolstatus;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (ProductsupplementExtranet)MappingObjectFromDataReader(reader);
                else
                    return null;
                
            }
        }

        public IList<object> getOptionSuppleMentListCurrentYearBySupplierAndProductIdExtraNet(short shrSupplierId, int intProductId, DateTime DateYear, bool bolStatus)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_supplement_date_extra_net WHERE product_id=@product_id AND supplier_id=@supplier_id AND YEAR(date_supplement)=@date_supplement AND status=@status ORDER BY date_supplement", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@date_supplement", SqlDbType.VarChar).Value = DateYear.Year;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
            
        }


        public int InsertOptionSupplement(short shrSupId, int intProductId, string strTitle, DateTime dDateSuple)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_supplement_date_extra_net (supplier_id,product_id,date_title,date_supplement,status)VALUES(@supplier_id,@product_id,@date_title,@date_supplement,@status); SET @supplement_date_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@date_title", SqlDbType.NVarChar).Value = strTitle;
                cmd.Parameters.Add("@date_supplement", SqlDbType.SmallDateTime).Value = dDateSuple;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@supplement_date_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                cn.Open();
                ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@supplement_date_id"].Value;
            }



            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_Supplement, StaffLogActionType.Insert, StaffLogSection.Product,
               intProductId, "tbl_product_supplement_date_extra_net", "supplier_id,product_id,date_title,date_supplement,status", "supplement_date_id", ret);
            //========================================================================================================================================================

            return ret;
        }

        public bool UpdateOptionSupplement(int intProductId , int intSuppleId, string strTitle, DateTime dDateSup)
        {

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_supplement_date_extra_net", "date_title,date_supplement", "supplement_date_id", intSuppleId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_supplement_date_extra_net SET date_title=@date_title,date_supplement=@date_supplement WHERE supplement_date_id=@supplement_date_id", cn);
                cmd.Parameters.Add("@date_title", SqlDbType.NVarChar).Value = strTitle;
                cmd.Parameters.Add("@date_supplement", SqlDbType.SmallDateTime).Value = dDateSup;
               
                cmd.Parameters.Add("@supplement_date_id", SqlDbType.Int).Value = intSuppleId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);

            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Supplement, StaffLogActionType.Update, StaffLogSection.Product,
                intProductId, "tbl_product_supplement_date_extra_net", "date_title,date_supplement", arroldValue, "supplement_date_id", intSuppleId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }


        public bool UpdateOptionSupplementStatus(int intProductId, int intSuppleId, bool bolStatus)
        {

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_supplement_date_extra_net", "status", "supplement_date_id", intSuppleId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_supplement_date_extra_net SET status=@status WHERE supplement_date_id=@supplement_date_id", cn);
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cmd.Parameters.Add("@supplement_date_id", SqlDbType.Int).Value = intSuppleId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);

            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Supplement, StaffLogActionType.Update, StaffLogSection.Product,
                intProductId, "tbl_product_supplement_date_extra_net", "status", arroldValue, "supplement_date_id", intSuppleId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

    }

    
}