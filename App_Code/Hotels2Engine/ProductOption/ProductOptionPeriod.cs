using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for ProductOptionPeriod
/// </summary>
/// 
namespace Hotels2thailand.ProductOption
{
    public class ProductOptionPeriod : Hotels2BaseClass
    {
        public ProductOptionPeriod()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int PeriodId { get; set; }
        public int ProductId { get; set; }
        public short SupplierId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }


        private LinqProductionDataContext dcPeriod = new LinqProductionDataContext();

        //public List<object> getProductPeriodListEffective(int intProductId, short shrSupplierId)
        //{
        //    var Result = from pp in dcPeriod.tbl_product_periods
        //                 where pp.date_end >= DateTime.Today && pp.product_id == intProductId && pp.supplier_id == shrSupplierId
        //                 orderby pp.date_end 
        //                 select pp;

        //    return MappingObjectFromDataContextCollection(Result);
        //}

        public List<object> getProductPeriodListEffective(int intProductId, short shrSupplierId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT period_id, product_id, supplier_id, date_start, date_end FROM tbl_product_period WHERE product_id=@product_id AND supplier_id=@supplier_id AND date_end >= CONVERT(DateTime, convert(varchar(20),GETDATE(),101)) ORDER BY date_end ", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
       

        public ProductOptionPeriod getProductPeriodListEffectiveGala(int intProductId, short shrSupplierId, DateTime dDateStart, DateTime dDateEnd)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_period WHERE product_id=@product_id AND supplier_id=@supplier_id AND date_start<= CONVERT(DateTime, convert(varchar(20),@date_start,101)) AND date_end>=CONVERT(DateTime, convert(varchar(20),@date_end,101)) ORDER BY date_end", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return (ProductOptionPeriod)MappingObjectFromDataReader(reader);
                }
                else
                {
                    return null; 
                }

            }
        }

        

        public List<object> getProductPeriodListEffectiveCondition(int intProductId, short shrSupplierId, int intConditionId)
        {
            StringBuilder strQuery = new StringBuilder();
            strQuery.Append("SELECT pd.period_id, pd.product_id , pd.supplier_id , pd.date_start, pd.date_end");
            strQuery.Append(" FROM tbl_product_period pd");
            strQuery.Append(" WHERE pd.date_end >= CONVERT(DateTime, convert(varchar(20),GETDATE(),101)) AND pd.product_id = @ProductId  AND pd.supplier_id = @supplierId AND pd.period_id");
            strQuery.Append(" NOT IN (");
            strQuery.Append(" SELECT  spd.period_id");
            strQuery.Append(" FROM tbl_product_period spd , tbl_product_option_condition_price sopc");
            strQuery.Append(" WHERE spd.period_id = sopc.period_id AND spd.date_end >= CONVERT(DateTime, convert(varchar(20),GETDATE(),101)) AND sopc.condition_id = @ConditionId AND spd.supplier_id = @supplierId)");
            strQuery.Append(" ORDER BY pd.date_start, pd.date_end");
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(strQuery.ToString(), cn);
                cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplierId", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@ConditionId", SqlDbType.Int).Value = intConditionId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
                                        

            }
            
        }

        public ProductOptionPeriod getProductPeriodById(int PeriodId)
        {
            var Result = dcPeriod.tbl_product_periods.SingleOrDefault(pp=>pp.period_id == PeriodId);
            if(Result == null)
            {
                return null;
            }
            else
            {
                return (ProductOptionPeriod)MappingObjectFromDataContext(Result);
            }
        }

        public DateTime getProductPeriodListBySupplierAndMaxValue(short shrSupplierId, int intProductId)
        {
            var Result = dcPeriod.tbl_product_periods.Where(pr => pr.supplier_id == shrSupplierId && pr.product_id == intProductId);
            if (Result.Count() == 0)
                return DateTime.Now;
            else
            {
                return Result.Max(d => d.date_end);
            }

             
           
        }
        public int insertNewPeriod(ProductOptionPeriod cProductPeriod)
        {
            var insert = new tbl_product_period
            {
                product_id = cProductPeriod.ProductId,
                supplier_id = cProductPeriod.SupplierId,
                date_start = cProductPeriod.DateStart,
                date_end = cProductPeriod.DateEnd
            };

            dcPeriod.tbl_product_periods.InsertOnSubmit(insert);
            dcPeriod.SubmitChanges();
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_Period, StaffLogActionType.Insert, StaffLogSection.Product,
                int.Parse(HttpContext.Current.Request.QueryString["pid"]), "tbl_product_period", "product_id,supplier_id,date_start,date_end", "period_id", insert.period_id);
            //========================================================================================================================================================
            return insert.period_id;
        }

        public bool Update()
        {
            return ProductOptionPeriod.updatePeriodProduct(this.PeriodId, this.DateStart, this.DateEnd);
        }
        public bool updateProductPeriod(ProductOptionPeriod cProductPeriod)
        {
            var update = dcPeriod.tbl_product_periods.SingleOrDefault(pp => pp.period_id == cProductPeriod.PeriodId);

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep(update.date_start, update.date_end);
            //============================================================================================================================
            update.date_start = cProductPeriod.DateStart;
            update.date_end = cProductPeriod.DateEnd;

            dcPeriod.SubmitChanges();

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Period, StaffLogActionType.Update, StaffLogSection.Product,
                int.Parse(HttpContext.Current.Request.QueryString["pid"]), "tbl_product_period", "date_start,date_end", arroldValue, "period_id", cProductPeriod.PeriodId);
            //==================================================================================================================== COMPLETED ========
            return true;
        }

        public static bool updatePeriodProduct(int intPeriodId, DateTime dDateStart, DateTime dDateEnd)
        {
            ProductOptionPeriod cProductOptionPeriod = new ProductOptionPeriod
            {
                 PeriodId = intPeriodId,
                  DateStart = dDateStart,
                  DateEnd = dDateEnd
            };
            return cProductOptionPeriod.updateProductPeriod(cProductOptionPeriod);
        }




    }
}