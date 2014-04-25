using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for Promotion
/// </summary>
/// 
namespace Hotels2thailand.ProductOption
{
    public class PromotionConditionExtranet:Hotels2BaseClass
    {
        public PromotionConditionExtranet()
        {
           
        }

        public int PromotionId { get; set; }
        public int ConditionId { get; set; }
        public bool Status { get; set; }

        public byte bytProGroupId { get; set; }

        public List<object> getConditionByPromotionId(int intPromotionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT promotion_id, condition_id, status FROM tbl_promotion_condition_extra_net WHERE promotion_id=@promotion_id AND status = 1", cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }


        public List<object> PromotionConditionDuplicatCheck(int intProductId, short shrSupplierId, byte bytDayMin, DateTime dDateStart, DateTime dDateEnd)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT pro.promotion_id, procon.condition_id,  procon.status");
            query.Append(" FROM tbl_promotion_extra_net pro , tbl_promotion_date_use_extra_net prou, tbl_promotion_condition_extra_net procon");
            query.Append(" WHERE pro.product_id = @product_id AND pro.supplier_id = @supplier_id AND pro.status = 1 AND procon.promotion_id = pro.promotion_id");
            query.Append(" AND prou.promotion_id = pro.promotion_id AND pro.day_min = @day_min AND procon.status = 1");
            query.Append(" AND ((prou.date_use_start <= @datestart and prou.date_use_end >= @datestart) or (prou.");
            query.Append(" date_use_start <= @dateend and prou.date_use_end >= @dateend) or (prou.date_use_start >= @datestart and prou.date_use_end <= @dateend)) ");
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@day_min", SqlDbType.TinyInt).Value = bytDayMin;
                cmd.Parameters.Add("@datestart", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@dateend", SqlDbType.SmallDateTime).Value = dDateEnd;
                cn.Open();
                
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> PromotionConditionDuplicatCheck_Hotdeal(int intProductId, short shrSupplierId, DateTime dDateStart, DateTime dDateEnd)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT pro.promotion_id, procon.condition_id,  procon.status");
            query.Append(" FROM tbl_promotion_extra_net pro , tbl_promotion_date_use_extra_net prou, tbl_promotion_condition_extra_net procon");
            query.Append(" ,tbl_promotion_group_item_extra_net proi");
            query.Append(" WHERE pro.product_id = @product_id AND pro.supplier_id = @supplier_id AND pro.status = 1 AND procon.promotion_id = pro.promotion_id");
            query.Append(" AND proi.pro_group_item_id = pro.pro_group_item_id AND pro.pro_group_item_id = 4");
            query.Append(" AND prou.promotion_id = pro.promotion_id AND procon.status = 1");
            query.Append(" AND ((prou.date_use_start <= @datestart and prou.date_use_end >= @datestart) or (prou.");
            query.Append(" date_use_start <= @dateend and prou.date_use_end >= @dateend) or (prou.date_use_start >= @datestart and prou.date_use_end <= @dateend)) ");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                //cmd.Parameters.Add("@day_min", SqlDbType.TinyInt).Value = bytDayMin;
                cmd.Parameters.Add("@datestart", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@dateend", SqlDbType.SmallDateTime).Value = dDateEnd;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> PromotionConditionDuplicatCheck_Advance(int intProductId, short shrSupplierId, byte bytDayMin, DateTime dDateStart, DateTime dDateEnd, short byteAdvanceDay)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT pro.promotion_id, procon.condition_id,  procon.status, pgi.pro_group_id");
            query.Append(" FROM tbl_promotion_extra_net pro , tbl_promotion_date_use_extra_net prou, tbl_promotion_condition_extra_net procon");
            query.Append(" ,tbl_promotion_group_item_extra_net pgi");
            query.Append(" WHERE pro.product_id = @product_id AND pro.supplier_id = @supplier_id AND pro.status = 1 AND procon.promotion_id = pro.promotion_id AND pro.day_advance_num = @day_advance_num AND procon.status = 1");
            query.Append(" AND prou.promotion_id = pro.promotion_id AND pro.day_min = @day_min AND pgi.pro_group_item_id = pro.pro_group_item_id AND pgi.status = 1");
            query.Append(" AND ((prou.date_use_start <= @datestart and prou.date_use_end >= @datestart) or (prou.");
            query.Append(" date_use_start <= @dateend and prou.date_use_end >= @dateend) or (prou.date_use_start >= @datestart and prou.date_use_end <= @dateend))");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@day_min", SqlDbType.TinyInt).Value = bytDayMin;
                cmd.Parameters.Add("@datestart", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@dateend", SqlDbType.SmallDateTime).Value = dDateEnd;
                cmd.Parameters.Add("@day_advance_num", SqlDbType.TinyInt).Value = byteAdvanceDay;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }



        public List<object> PromotionConditionDuplicatCheck_editMode(int intProductId, short shrSupplierId, byte bytDayMin, DateTime dDateStart, DateTime dDateEnd, int intPromotionId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT pro.promotion_id, procon.condition_id,  procon.status");
            query.Append(" FROM tbl_promotion_extra_net pro , tbl_promotion_date_use_extra_net prou, tbl_promotion_condition_extra_net procon");
            query.Append(" WHERE pro.product_id = @product_id AND pro.supplier_id = @supplier_id AND pro.status = 1 AND procon.promotion_id = pro.promotion_id");
            query.Append(" AND prou.promotion_id = pro.promotion_id AND pro.day_min = @day_min AND procon.status = 1 AND pro.promotion_id <> @promotion_id");
            query.Append(" AND ((prou.date_use_start <= @datestart and prou.date_use_end >= @datestart) or (prou.");
            query.Append(" date_use_start <= @dateend and prou.date_use_end >= @dateend) or (prou.date_use_start >= @datestart and prou.date_use_end <= @dateend)) ");
            query.Append(" AND procon.condition_id IN (SELECT condition_id FROM tbl_promotion_condition_extra_net  WHERE promotion_id = @promotion_id)");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@day_min", SqlDbType.TinyInt).Value = bytDayMin;
                cmd.Parameters.Add("@datestart", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@dateend", SqlDbType.SmallDateTime).Value = dDateEnd;
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> PromotionConditionDuplicatCheck_Hotdeal_editMode(int intProductId, short shrSupplierId, DateTime dDateStart, DateTime dDateEnd, int intPromotionId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT pro.promotion_id, procon.condition_id,  procon.status");
            query.Append(" FROM tbl_promotion_extra_net pro , tbl_promotion_date_use_extra_net prou, tbl_promotion_condition_extra_net procon");
            query.Append(" ,tbl_promotion_group_item_extra_net proi");
            query.Append(" WHERE pro.product_id = @product_id AND pro.supplier_id = @supplier_id AND pro.status = 1 AND procon.promotion_id = pro.promotion_id");
            query.Append(" AND proi.pro_group_item_id = pro.pro_group_item_id AND pro.pro_group_item_id = 4");
            query.Append(" AND prou.promotion_id = pro.promotion_id AND procon.status = 1 AND pro.promotion_id <> @promotion_id");
            query.Append(" AND ((prou.date_use_start <= @datestart and prou.date_use_end >= @datestart) or (prou.");
            query.Append(" date_use_start <= @dateend and prou.date_use_end >= @dateend) or (prou.date_use_start >= @datestart and prou.date_use_end <= @dateend)) ");
            query.Append(" AND procon.condition_id IN (SELECT condition_id FROM tbl_promotion_condition_extra_net  WHERE promotion_id = @promotion_id)");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                //cmd.Parameters.Add("@day_min", SqlDbType.TinyInt).Value = bytDayMin;
                cmd.Parameters.Add("@datestart", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@dateend", SqlDbType.SmallDateTime).Value = dDateEnd;
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }


        public List<object> PromotionConditionDuplicatCheck_Advance_editMode(int intProductId, short shrSupplierId, byte bytDayMin, DateTime dDateStart, DateTime dDateEnd, short byteAdvanceDay, int intPromotionId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT pro.promotion_id, procon.condition_id,  procon.status, pgi.pro_group_id");
            query.Append(" FROM tbl_promotion_extra_net pro , tbl_promotion_date_use_extra_net prou, tbl_promotion_condition_extra_net procon");
            query.Append(" ,tbl_promotion_group_item_extra_net pgi");
            query.Append(" WHERE pro.product_id = @product_id AND pro.supplier_id = @supplier_id AND pro.status = 1 AND procon.promotion_id = pro.promotion_id AND pro.day_advance_num = @day_advance_num AND procon.status = 1 AND pro.promotion_id <> @promotion_id");
            query.Append(" AND prou.promotion_id = pro.promotion_id AND pro.day_min = @day_min AND pgi.pro_group_item_id = pro.pro_group_item_id AND pgi.status = 1");
            query.Append(" AND ((prou.date_use_start <= @datestart and prou.date_use_end >= @datestart) or (prou.");
            query.Append(" date_use_start <= @dateend and prou.date_use_end >= @dateend) or (prou.date_use_start >= @datestart and prou.date_use_end <= @dateend))");

            query.Append(" AND procon.condition_id IN (SELECT condition_id FROM tbl_promotion_condition_extra_net  WHERE promotion_id = @promotion_id)");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@day_min", SqlDbType.TinyInt).Value = bytDayMin;
                cmd.Parameters.Add("@datestart", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@dateend", SqlDbType.SmallDateTime).Value = dDateEnd;
                cmd.Parameters.Add("@day_advance_num", SqlDbType.TinyInt).Value = byteAdvanceDay;
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }




        public PromotionConditionExtranet getConditionByPromotionAndConditionId(int intPromotionId, int intCondition)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT promotion_id, condition_id, status FROM tbl_promotion_condition_extra_net WHERE promotion_id=@promotion_id AND condition_id=@condition_id", cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intCondition;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return (PromotionConditionExtranet)MappingObjectFromDataReader(reader);
                }
                else
                {
                    return null;
                }
                
            }
        }

        public int InsertMappingConditionPromotionId(int intPromotionId, int intCondition, int intProductId)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder Query = new StringBuilder();
                
                Query.Append("INSERT INTO tbl_promotion_condition_extra_net(promotion_id,condition_id,status)VALUES(@promotion_id,@condition_id,'1')");
                
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intCondition;
                
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                
            }

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_promotion, StaffLogActionType.Insert, StaffLogSection.Product,
                intProductId, "tbl_promotion_condition_extra_net", "promotion_id,condition_id,status", "promotion_id,condition_id", intPromotionId, intCondition);
            //========================================================================================================================================================
            return ret;
        }
        public int UPdateMappingConditionPromotionId(int intPromotionId, int intCondition, bool bolStatus, int intProductId)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_promotion_condition_extra_net", "status", "promotion_id,condition_id", intPromotionId, intCondition);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder Query = new StringBuilder();
                Query.Append("UPDATE tbl_promotion_condition_extra_net SET status=@status WHERE promotion_id=@promotion_id AND condition_id=@condition_id");
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intCondition;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cn.Open();
                ExecuteNonQuery(cmd);
                
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_promotion, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_promotion_condition_extra_net", "status", arroldValue, "promotion_id,condition_id", intPromotionId, intCondition);
            //==================================================================================================================== COMPLETED ========
            return ret;
        }

        /// <summary>
        /// AJAX PAGE : get Option active Promotion
        /// </summary>
        /// <param name="intPromotionId"></param>
        /// <returns></returns>
        public IDictionary<int, string> getOptionByActivePromotion(int intPromotionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
               IDictionary<int, string> dicOutput = new Dictionary<int, string>();
                StringBuilder Query = new StringBuilder();
                Query.Append(" SELECT DISTINCT(opc.option_id), op.title");
                Query.Append(" FROM tbl_promotion_condition_extra_net pcon, tbl_product_option_condition opc, tbl_product_option op");
                Query.Append(" WHERE pcon.condition_id = opc.condition_id AND op.option_id = opc.option_id AND pcon.promotion_id = @promotion_id AND pcon.status = 1 AND opc.status = 1");
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                
                    while (reader.Read())
                    {
                        dicOutput.Add((int)reader[0], reader[1].ToString());
                    }
                
                return dicOutput;
            }
        }
        

        /// <summary>
        /// AJAX PAGE : get ConditionList active Promotion 
        /// </summary>
        /// <returns></returns>
        public IDictionary<int, string> getPromotionCondition(int intPromotionId, int intOptionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                IDictionary<int, string> dicOutput = new Dictionary<int, string>();
                StringBuilder Query = new StringBuilder();
                Query.Append("SELECT condition_id, title FROM tbl_product_option_condition");
                Query.Append(" WHERE option_id = @option_id AND condition_id IN (");
                Query.Append(" SELECT condition_id FROM tbl_promotion_condition_extra_net WHERE promotion_id = @promotion_id AND status = 1) AND status = 1");
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                
                    while (reader.Read())
                    {
                        dicOutput.Add((int)reader[0], reader[1].ToString());
                    }
                
                return dicOutput;
            }
        }
        
         
        
    }
}