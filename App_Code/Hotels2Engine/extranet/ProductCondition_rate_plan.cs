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
    public class ProductCondition_rate_plan : Hotels2BaseClass
    {
        public int RatePlanId { get; set; }
        public int OptionId { get; set; }
        public int ConditionId { get; set; }
        public string Conditiontitle { get; set; }
        public byte CountryId { get; set; }
        public string CountryTitle { get; set; }
        public byte bytNumAdult { get; set; }
        public byte RateplanCatId { get; set; }
        public string RateplanCatTitle { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public decimal Ratevalue { get; set; }
        public bool Status { get; set; }


        public int intSertRatePlan(int intProductId, int intConditionId, byte bytCountryId, byte bytRatePlanCatId, DateTime? dDateStart, DateTime? dDateEnd, decimal bytValue, bool Status)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_condition_rate_plan_extra_net (condition_id,country_id,rate_plan_cat_id,date_start,date_end,rate_value,status) VALUES (@condition_id,@country_id,@rate_plan_cat_id,@date_start,@date_end,@rate_value,@status); SET @rate_plan_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = bytCountryId;
                cmd.Parameters.Add("@rate_plan_cat_id", SqlDbType.TinyInt).Value = bytRatePlanCatId;
                if (dDateStart.HasValue)
                    cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = (DateTime)dDateStart;
                else
                    cmd.Parameters.AddWithValue("@date_start", DBNull.Value);

                if (dDateEnd.HasValue)
                    cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = (DateTime)dDateEnd;
                else
                    cmd.Parameters.AddWithValue("@date_end", DBNull.Value);

                cmd.Parameters.Add("@rate_value", SqlDbType.Money).Value = bytValue;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = Status;
                cmd.Parameters.Add("@rate_plan_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@rate_plan_id"].Value;
            }

            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_Condition_Extra, StaffLogActionType.Insert, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_rate_plan_extra_net", "condition_id,country_id,rate_plan_cat_id,date_start,date_end,rate_value,status", "rate_plan_cat_id", ret);
            
            return ret;
        }

        public bool UpdateRatePlanStatus(string intRatePlanId, int intProductId, bool bolStatus)
        {
            int ret = 0;
            string Status = "0";
            if (bolStatus)
                Status = "1";


            string Query = "UPDATE tbl_product_option_condition_rate_plan_extra_net SET status = " + Status + " WHERE rate_plan_id IN (" + intRatePlanId + ")";
            IList<ArrayList> arrObj_oldValue = StaffActivity.ActionUpdateMethodStaff_log_MultipleRecord_FirstStep(Query);

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition_rate_plan_extra_net SET status = @status WHERE rate_plan_id IN (" + intRatePlanId + ")", cn);
                
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cn.Open();
                ret = ExecuteNonQuery(cmd);

            }

            StaffActivity.ActionUpdateMethodStaff_log_MultipleRecord_Laststep(StaffLogModule.Product_Condition_Extra, StaffLogActionType.Update, StaffLogSection.Product, intProductId, "tbl_product_option_condition_rate_plan_extra_net", Query, arrObj_oldValue, "rate_plan_id", intRatePlanId);

            return (ret == 1);

        }

        public bool UpdateRatePlan(int intProductId, int intRatePlanId, byte bytRatePlanCatId, DateTime? dDateStart, DateTime? dDateEnd, decimal bytValue)
        {
            int ret = 0;
            ArrayList arrObj_oldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_condition_rate_plan_extra_net", "rate_plan_cat_id,date_start,date_end,rate_value", "rate_plan_id", intRatePlanId);

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition_rate_plan_extra_net SET rate_plan_cat_id=@rate_plan_cat_id,date_start=@date_start,date_end=@date_end,rate_value=@rate_value WHERE rate_plan_id=@rate_plan_id", cn);
                cmd.Parameters.Add("@rate_plan_cat_id", SqlDbType.TinyInt).Value = bytRatePlanCatId;
                if (dDateStart.HasValue)
                    cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = (DateTime)dDateStart;
                else
                    cmd.Parameters.AddWithValue("@date_start", DBNull.Value);

                if (dDateEnd.HasValue)
                    cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = (DateTime)dDateEnd;
                else
                    cmd.Parameters.AddWithValue("@date_end", DBNull.Value);
                cmd.Parameters.Add("@rate_value", SqlDbType.Money).Value = bytValue;
                cmd.Parameters.Add("@rate_plan_id", SqlDbType.Int).Value = intRatePlanId;

                ret = ExecuteNonQuery(cmd);

            }

            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Condition_Extra, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_rate_plan_extra_net", "rate_plan_cat_id,date_start,date_end,rate_value", arrObj_oldValue, "rate_plan_id", intRatePlanId);

            return (ret == 1);

        }

        public IList<object> getRatePlanList(int intProductId, short shrSupplierId, byte bytCountryId, byte bytlangId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT opcr.rate_plan_id, op.option_id, opcr.condition_id, cont.title, opcr.country_id, cty.title, cone.num_adult,");
            query.Append(" opcr.rate_plan_cat_id,  rc.title, opcr.date_start, opcr.date_end, opcr.rate_value, opcr.status");
            query.Append(" FROM tbl_product_option_condition_rate_plan_extra_net opcr, tbl_product_option_condition_extra_net cone");
            query.Append(" , tbl_product_option_supplier os, tbl_product_option op, tbl_country cty ,tbl_product_option_condition_title_lang_extra_net cont");
            query.Append(" , tbl_rate_plan_extra_net_category rc");
            query.Append(" WHERE opcr.country_id = @country_id AND opcr.condition_id = cone.condition_id AND os.option_id = cone.option_id AND opcr.status = 1");
            query.Append(" AND os.supplier_id = @supplier_id AND os.option_id = op.option_id AND op.status = 1 AND op.cat_id = 38");
            query.Append(" AND cone.status = 1 AND cty.country_id = opcr.country_id AND op.product_id = @product_id");
            query.Append(" AND cont.condition_id = cone.condition_id AND cont.lang_id = @lang_id AND rc.rate_plan_cat_id = opcr.rate_plan_cat_id");
            query.Append(" ORDER BY cty.title");
            query.Append("");

            IDictionary<byte, string> iDicCountry = new Dictionary<byte, string>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = bytCountryId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytlangId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));


            }
            
        }
        public IDictionary<byte,string> getCountryRatePlan(int intProductId, short shrSupplierId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT DISTINCT(opcr.country_id), cty.title");
            query.Append(" FROM tbl_product_option_condition_rate_plan_extra_net opcr, tbl_product_option_condition_extra_net cone");
            query.Append(" , tbl_product_option_supplier os, tbl_product_option op, tbl_country cty");
            query.Append(" WHERE  opcr.condition_id = cone.condition_id AND os.option_id = cone.option_id AND opcr.status  = 1 ");
            query.Append(" AND os.supplier_id = @supplier_id AND os.option_id = op.option_id AND op.status = 1 AND op.cat_id = 38");
            query.Append(" AND cone.status = 1 AND cty.country_id = opcr.country_id AND op.product_id = @product_id ORDER BY cty.title");
            query.Append("");

            IDictionary<byte, string> iDicCountry = new Dictionary<byte, string>();
            using(SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd  = new SqlCommand(query.ToString(),cn);
                cmd.Parameters.Add("@product_id",SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd);
                while(reader.Read())
                {
                    iDicCountry.Add((byte)reader[0], reader[1].ToString());
                }

                
            }
            return iDicCountry;
        }

    }
}