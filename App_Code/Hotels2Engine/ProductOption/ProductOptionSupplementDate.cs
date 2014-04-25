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
/// Summary description for ProductOptionSupplementDate
/// </summary>
/// 
namespace Hotels2thailand.ProductOption
{
    public class ProductOptionSupplementDate : Hotels2BaseClass
    {
        public ProductOptionSupplementDate()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int SuppleMentId { get; set; }
        public short SupplierId { get; set; }
        public int OptionId { get; set; }
        public string DateTitle { get; set; }
        public DateTime DateSupplement { get; set; }
        public decimal SupplementAmount { get; set; }
        public bool  Status { get; set; }

        private LinqProductionDataContext dcOptionSuppleMent = new LinqProductionDataContext();

        public ProductOptionSupplementDate getOptionSuppleMentById(int intSupplementId)
        {
            var Result = dcOptionSuppleMent.tbl_product_supplement_dates.SingleOrDefault(ps => ps.supplement_date_id == intSupplementId);
            return (ProductOptionSupplementDate)MappingObjectFromDataContext(Result);
        }

        public List<object> getOptionSuppleMentList()
        {
            var Result = from ps in dcOptionSuppleMent.tbl_product_supplement_dates
                         select ps;

            return MappingObjectFromDataContextCollection(Result);
        }

        public List<object> getOptionSuppleMentListCurrentYearBySupplierAndOptionId(short shrSupplierId , int intOptonId, DateTime DateYear, bool bolStatus)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_supplement_date WHERE option_id=@option_id AND supplier_id=@supplier_id AND YEAR(date_supplement)=@date_supplement AND status=@status ORDER BY date_supplement", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptonId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@date_supplement", SqlDbType.VarChar).Value = DateYear.Year;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
            //var Result = from ps in dcOptionSuppleMent.tbl_product_supplement_dates
            //             where ps.supplier_id == shrSupplierId && ps.option_id == intOptonId && ps.date_supplement.Year >= DateYear.Year
            //             orderby ps.status descending
            //             select ps;

            //return MappingObjectFromDataContextCollection(Result);
        }

        public int InsertOptionSupplement(short shrSupId, int intOptionId, string strTitle, DateTime dDateSuple, decimal Amount)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_supplement_date (supplier_id,option_id,date_title,date_supplement,supplement,status)VALUES(@supplier_id,@option_id,@date_title,@date_supplement,@supplement,@status); SET @supplement_date_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupId;
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@date_title", SqlDbType.NVarChar).Value = strTitle;
                cmd.Parameters.Add("@date_supplement", SqlDbType.SmallDateTime).Value = dDateSuple;
                cmd.Parameters.Add("@supplement", SqlDbType.SmallMoney).Value = Amount;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@supplement_date_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                cn.Open();
                ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@supplement_date_id"].Value;
            }

            

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_Supplement, StaffLogActionType.Insert, StaffLogSection.Product,
                int.Parse(HttpContext.Current.Request.QueryString["pid"]), "tbl_product_supplement_date", "supplier_id,option_id,date_title,date_supplement,supplement,status", "supplement_date_id", ret);
            //========================================================================================================================================================

            return ret;
        }

        public bool UpdateOptionSupplement(int intSuppleId, string strTitle, DateTime dDateSup, decimal SuppleMent)
        {

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_supplement_date", "date_title,date_supplement,supplement", "supplement_date_id", intSuppleId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_supplement_date SET date_title=@date_title,date_supplement=@date_supplement,supplement=@supplement WHERE supplement_date_id=@supplement_date_id", cn);
                cmd.Parameters.Add("@date_title",  SqlDbType.NVarChar).Value = strTitle;
                cmd.Parameters.Add("@date_supplement", SqlDbType.SmallDateTime).Value = dDateSup;
                cmd.Parameters.Add("@supplement", SqlDbType.SmallMoney).Value = SuppleMent;
                cmd.Parameters.Add("@supplement_date_id", SqlDbType.Int).Value = intSuppleId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);

            }
            
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Supplement, StaffLogActionType.Update, StaffLogSection.Product,
                int.Parse(HttpContext.Current.Request.QueryString["pid"]), "tbl_product_supplement_date", "date_title,date_supplement,supplement", arroldValue, "supplement_date_id", intSuppleId);
            //==================================================================================================================== COMPLETED ========
            return (ret==1);
        }
        public bool UpdateOptionSupplementStatus(int intSuppleId, bool bolStatus)
        {

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_supplement_date", "status", "supplement_date_id", intSuppleId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_supplement_date SET status=@status WHERE supplement_date_id=@supplement_date_id", cn);
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cmd.Parameters.Add("@supplement_date_id", SqlDbType.Int).Value = intSuppleId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);

            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Supplement, StaffLogActionType.Update, StaffLogSection.Product,
                int.Parse(HttpContext.Current.Request.QueryString["pid"]), "tbl_product_supplement_date", "status", arroldValue, "supplement_date_id", intSuppleId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }
        //public bool Update()
        //{
        //    return ProductOptionSupplementDate.UpdateSupplement(this.SuppleMentId, this.SupplierId, this.OptionId, this.DateTitle, this.DateSupplement, this.SupplementAmount,this.Status);
        //}

        //public static bool UpdateSupplement(int intSupplementId, short shrSupplierId, int intOptionId, string strTitle, DateTime datSupDate, decimal decSupAmount, bool bolStatus)
        //{
        //    ProductOptionSupplementDate cProductSupplement = new ProductOptionSupplementDate
        //    {
        //        SuppleMentId = intSupplementId,
        //        SupplierId = shrSupplierId,
        //        OptionId = intOptionId,
        //        DateTitle = strTitle,
        //        DateSupplement = datSupDate,
        //        SupplementAmount = decSupAmount,
        //        Status = bolStatus
        //    };

        //    return cProductSupplement.UpdateOptionSupplement(cProductSupplement);
        //}

        //public static bool  DeleteSupplement(int SupplementID)
        //{
        //    LinqProductionDataContext dcOptionSuppleMent = new LinqProductionDataContext();
        //    var Delete = dcOptionSuppleMent.tbl_product_supplement_dates.SingleOrDefault(ps=>ps.supplement_date_id == SupplementID);

        //    dcOptionSuppleMent.tbl_product_supplement_dates.DeleteOnSubmit(Delete);
        //    dcOptionSuppleMent.SubmitChanges();

        //    int ret = 1;
        //    return (ret == 1);

        //}
    }
}