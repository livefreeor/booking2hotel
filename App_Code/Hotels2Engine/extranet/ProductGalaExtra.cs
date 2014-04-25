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
    public class ProductGalaExtra : Hotels2BaseClass
    {
        public int OptionId { get; set; }
        public short Optioncat { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public int ContidionId { get; set; }
        public int ProductId { get; set; }
        public string PriceId { get; set; }
        public decimal Price { get; set; }
        public DateTime DatePrice { get; set; }
        public bool ForAdult { get; set; }
        public bool ForChild { get; set; }



        public List<object> GetGalaExtraNetList(int intProductID, short shrSupplierId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT op.option_id, op.cat_id, opc.title, opc.detail, opcon.condition_id, op.product_id, opconP.price_id , opconP.price, opconP.date_price");
            query.Append(" ,opg.require_adult,opg.require_child");
            query.Append(" FROM tbl_product_option op, tbl_product_option_content opc, tbl_product_option_supplier ops");
            query.Append(" ,tbl_product_option_condition_extra_net opcon, tbl_product_option_condition_price_extranet opconP");
            query.Append(" ,tbl_product_option_gala opg");
            query.Append(" WHERE op.option_id = opc.option_id AND opc.lang_id = 1 AND ops.option_id = op.option_id AND ops.supplier_id = @suplier_id");
            query.Append(" AND op.option_id=opg.option_id");
            query.Append(" AND op.product_id = @product_id AND op.cat_id  = 47 ANd opcon.option_id = op.option_id AND opconP.condition_id = opcon.condition_id");
            query.Append(" AND op.status = 1 AND opconP.date_price >= CONVERT(DateTime, convert(varchar(20),GETDATE(),101))");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductID;
                cmd.Parameters.Add("@suplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public ProductGalaExtra GetGalaExtraNetListByOptionId(int intProductID, short shrSupplierId, int intOptionId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT op.option_id, op.cat_id, opc.title, opc.detail, opcon.condition_id, op.product_id, opconP.price_id , opconP.price, opconP.date_price");
            query.Append(" ,opg.require_adult,opg.require_child");
            query.Append(" FROM tbl_product_option op, tbl_product_option_content opc, tbl_product_option_supplier ops");
            query.Append(" ,tbl_product_option_condition_extra_net opcon, tbl_product_option_condition_price_extranet opconP");
            query.Append(" ,tbl_product_option_gala opg");
            query.Append(" WHERE op.option_id = opc.option_id AND opc.lang_id = 1 AND ops.option_id = op.option_id AND ops.supplier_id = @suplier_id");
            query.Append(" AND op.option_id=opg.option_id");
            query.Append(" AND op.product_id = @product_id AND op.cat_id  = 47 ANd opcon.option_id = op.option_id AND opconP.condition_id = opcon.condition_id");
            query.Append(" AND op.status = 1 AND op.option_id=@option_id AND opconP.date_price >= CONVERT(DateTime, convert(varchar(20),GETDATE(),101))");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductID;
                cmd.Parameters.Add("@suplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (ProductGalaExtra)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }

        public bool RemoveOptionGala(int intProductId, int intOptionId, bool bolStatus)
        {
            int ret = 0;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option", "status", "option_id", intOptionId);
            //============================================================================================================================
            using(SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option SET status =@status WHERE option_id=@option_id",cn);
                cmd.Parameters.Add("@status",SqlDbType.Bit).Value = bolStatus;
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_option_detail, StaffLogActionType.Update, StaffLogSection.Product, intProductId, "tbl_product_option", "status", arroldValue, "option_id", intOptionId);
               
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }


        public bool DelGala(int intProductId, int intOptionId,  short shrSupplierId, int intConditionId, string PriceId, byte bytLangId)
        {
            int ret = 0;
            // step 1 Delete PriceId

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            IList<object[]> IlistoldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_option_condition_price_extranet", "price_id", PriceId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_option_condition_price_extranet WHERE price_id=@price_id", cn);
                cmd.Parameters.Add("@price_id", SqlDbType.VarChar).Value = PriceId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_Price_Extra, StaffLogActionType.Delete, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_price_extranet", IlistoldValue, "price_id", PriceId);
            //==================================================================================================================== COMPLETED ========


            //---------------------------------------------------------------------------------------------------------------------------------


            //Step 2 Delete condition content

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            IList<object[]> IlistoldValue1 = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_option_condition_title_lang_extra_net", "condition_id,lang_id", intConditionId, bytLangId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_option_condition_title_lang_extra_net WHERE condition_id =@condition_id AND lang_id=@lang_id", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_Condition_Extra, StaffLogActionType.Delete, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_title_lang_extra_net", IlistoldValue1, "condition_id,lang_id", intConditionId, bytLangId);
            //==================================================================================================================== COMPLETED ========

            //---------------------------------------------------------------------------------------------------------------------------------

            // step 3 Delete Condition

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            IList<object[]> IlistoldValue2 = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_option_condition_extra_net", "condition_id", intConditionId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_option_condition_extra_net WHERE condition_id=@condition_id", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_Condition_Extra, StaffLogActionType.Delete, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_extra_net", IlistoldValue2, "condition_id", intConditionId);
            //==================================================================================================================== COMPLETED ========

            //---------------------------------------------------------------------------------------------------------------------------------

            // step 4 Delete Option Supplier

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            IList<object[]> IlistoldValue3 = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_option_supplier", "option_id,supplier_id", intOptionId, shrSupplierId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_option_supplier WHERE option_id=@option_id AND supplier_id=@supplier_id", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_option_supplier, StaffLogActionType.Delete, StaffLogSection.Product, intProductId,
                "tbl_product_option_supplier", IlistoldValue3, "option_id,supplier_id", intOptionId, shrSupplierId);
            //==================================================================================================================== COMPLETED ========

            //---------------------------------------------------------------------------------------------------------------------------------

            // step 5 Delete Option Content
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            IList<object[]> IlistoldValue4 = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_option_content", "option_id,lang_id", intOptionId, bytLangId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_option_content WHERE option_id=@option_id AND lang_id=@lang_id", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_option_detail, StaffLogActionType.Delete, StaffLogSection.Product, intProductId,
                "tbl_product_option_content", IlistoldValue4, "option_id,lang_id", intOptionId, bytLangId);
            //==================================================================================================================== COMPLETED ========

            //---------------------------------------------------------------------------------------------------------------------------------

            // step 6 Delete Option Gala
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            IList<object[]> IlistoldValue5 = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_option_gala", "option_id", intOptionId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_option_gala WHERE option_id=@option_id", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_gala, StaffLogActionType.Delete, StaffLogSection.Product, intProductId,
                "tbl_product_option_gala", IlistoldValue5, "option_id", intOptionId);
            //==================================================================================================================== COMPLETED ========

            //---------------------------------------------------------------------------------------------------------------------------------
            // step 7 Delete Option Main
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            IList<object[]> IlistoldValue6 = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_option", "option_id", intOptionId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_option WHERE option_id=@option_id", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;

                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_option_detail, StaffLogActionType.Delete, StaffLogSection.Product, intProductId,
                "tbl_product_option", IlistoldValue6, "option_id", intOptionId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }
    }
}