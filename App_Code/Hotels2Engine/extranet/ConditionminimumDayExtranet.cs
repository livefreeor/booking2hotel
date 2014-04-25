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
    public class ConditionminimumDayExtranet : Hotels2BaseClass
    {
        public int MinStayId { get; set; }
        public int ConditionId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public byte NumMin { get; set; }
        public bool Status { get; set; }


        public List<object> GetConditionMinimumstayListByConditionId(int intConditionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option_condition_minimum_stay_extra_net WHERE condition_id = @condition_id AND status=1",cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));

            }
        }

        public ConditionminimumDayExtranet GetConditionMinimumstayById(int intMinDayId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option_condition_minimum_stay_extra_net WHERE mini_stay_id=@mini_stay_id AND status = 1", cn);
                cmd.Parameters.Add("@mini_stay_id", SqlDbType.Int).Value = intMinDayId;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (ConditionminimumDayExtranet)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }

        public int InsertMinimumstay(int intProductId, int intConditionId, DateTime dDateStart, DateTime dDateEnd, byte bytNumMin)
        {
            int ret = 0;
            int MinStay = 0;

           
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_condition_minimum_stay_extra_net (condition_id,date_start,date_end,num_minimum,status) VALUES(@condition_id,@date_start,@date_end,@num_minimum,1);SET @mini_stay_id=SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cmd.Parameters.Add("@num_minimum", SqlDbType.TinyInt).Value = bytNumMin;

                cmd.Parameters.Add("@mini_stay_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                MinStay = (int)cmd.Parameters["@mini_stay_id"].Value;
                
                
            }

            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_option_condition, StaffLogActionType.Insert, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_minimum_stay_extra_net", "condition_id,date_start,date_end,num_minimum,status", "mini_stay_id", MinStay);

            return MinStay;
        }


        public bool UpdateMinimumStay(int intProductId, int intMinDayId, DateTime dDateStart, DateTime dDateEnd, byte bytNumMin)
        {
            int ret = 0;
            ArrayList ObjOldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_condition_minimum_stay_extra_net", "date_start,date_end,num_minimum", "mini_stay_id", intMinDayId);
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition_minimum_stay_extra_net SET date_start=@date_start,date_end=@date_end,num_minimum=@num_minimum WHERE mini_stay_id=@mini_stay_id", cn);
                cmd.Parameters.Add("@mini_stay_id", SqlDbType.Int).Value = intMinDayId;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cmd.Parameters.Add("@num_minimum", SqlDbType.TinyInt).Value = bytNumMin;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_option_condition, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_minimum_stay_extra_net", "date_start,date_end,num_minimum", ObjOldValue, "mini_stay_id", intMinDayId);

            return (ret == 1);
        }


        public bool UpdateMinimumStayStatus(int intProductId, int intMinDayId, bool bolStatus)
        {
            int ret = 0;
            ArrayList ObjOldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_condition_minimum_stay_extra_net", "status", "mini_stay_id", intMinDayId);
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition_minimum_stay_extra_net SET status=@status WHERE mini_stay_id=@mini_stay_id", cn);
                cmd.Parameters.Add("@mini_stay_id", SqlDbType.Int).Value = intMinDayId;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
               
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_option_condition, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_minimum_stay_extra_net", "status", ObjOldValue, "mini_stay_id", intMinDayId);

            return (ret == 1);
        }
        //public int OptionId { get; set; }
        //public short Optioncat { get; set; }
        //public string Title { get; set; }
        //public string Detail { get; set; }
        //public int ContidionId { get; set; }
        //public int ProductId { get; set; }
        //public int PriceId { get; set; }
        //public decimal Price { get; set; }
        //public DateTime DatePrice { get; set; }
        //public bool ForAdult { get; set; }
        //public bool ForChild { get; set; }



        //public List<object> GetGalaExtraNetList(int intProductID, short shrSupplierId)
        //{
        //    StringBuilder query = new StringBuilder();
        //    query.Append("SELECT op.option_id, op.cat_id, opc.title, opc.detail, opcon.condition_id, op.product_id, opconP.price_id , opconP.price, opconP.date_price");
        //    query.Append(" ,opg.require_adult,opg.require_child");
        //    query.Append(" FROM tbl_product_option op, tbl_product_option_content opc, tbl_product_option_supplier ops");
        //    query.Append(" ,tbl_product_option_condition_extra_net opcon, tbl_product_option_condition_price_extranet opconP");
        //    query.Append(" ,tbl_product_option_gala opg");
        //    query.Append(" WHERE op.option_id = opc.option_id AND opc.lang_id = 1 AND ops.option_id = op.option_id AND ops.supplier_id = @suplier_id");
        //    query.Append(" AND op.option_id=opg.option_id");
        //    query.Append(" AND op.product_id = @product_id AND op.cat_id  = 47 ANd opcon.option_id = op.option_id AND opconP.condition_id = opcon.condition_id");
        //    query.Append(" AND op.status = 1 AND opconP.date_price >= CONVERT(DateTime, convert(varchar(20),GETDATE(),101))");
        //    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand(query.ToString(), cn);
        //        cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductID;
        //        cmd.Parameters.Add("@suplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
        //        cn.Open();
        //        return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
        //    }
        //}

        //public ProductGalaExtra GetGalaExtraNetListByOptionId(int intProductID, short shrSupplierId, int intOptionId)
        //{
        //    StringBuilder query = new StringBuilder();
        //    query.Append("SELECT op.option_id, op.cat_id, opc.title, opc.detail, opcon.condition_id, op.product_id, opconP.price_id , opconP.price, opconP.date_price");
        //    query.Append(" ,opg.require_adult,opg.require_child");
        //    query.Append(" FROM tbl_product_option op, tbl_product_option_content opc, tbl_product_option_supplier ops");
        //    query.Append(" ,tbl_product_option_condition_extra_net opcon, tbl_product_option_condition_price_extranet opconP");
        //    query.Append(" ,tbl_product_option_gala opg");
        //    query.Append(" WHERE op.option_id = opc.option_id AND opc.lang_id = 1 AND ops.option_id = op.option_id AND ops.supplier_id = @suplier_id");
        //    query.Append(" AND op.option_id=opg.option_id");
        //    query.Append(" AND op.product_id = @product_id AND op.cat_id  = 47 ANd opcon.option_id = op.option_id AND opconP.condition_id = opcon.condition_id");
        //    query.Append(" AND op.status = 1 AND op.option_id=@option_id AND opconP.date_price >= CONVERT(DateTime, convert(varchar(20),GETDATE(),101))");
        //    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand(query.ToString(), cn);
        //        cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductID;
        //        cmd.Parameters.Add("@suplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
        //        cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
        //        cn.Open();
        //        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        //        if (reader.Read())
        //            return (ProductGalaExtra)MappingObjectFromDataReader(reader);
        //        else
        //            return null;
        //    }
        //}

        //public bool RemoveOptionGala(int intProductId, int intOptionId, bool bolStatus)
        //{
        //    int ret = 0;
        //    //#Staff_Activity_Log================================================================================================ STEP 1 ==
        //    ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option", "status", "option_id", intOptionId);
        //    //============================================================================================================================
        //    using(SqlConnection cn = new SqlConnection(this.ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option SET status =@status WHERE option_id=@option_id",cn);
        //        cmd.Parameters.Add("@status",SqlDbType.Bit).Value = bolStatus;
        //        cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
        //        cn.Open();
        //        ret = ExecuteNonQuery(cmd);
                
        //    }
        //    //#Staff_Activity_Log================================================================================================ STEP 2 ============
        //    StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_option_detail, StaffLogActionType.Update, StaffLogSection.Product, intProductId, "tbl_product_option", "status", arroldValue, "option_id", intOptionId);
               
        //    //==================================================================================================================== COMPLETED ========
        //    return (ret == 1);
        //}


        //public bool DelGala(int intProductId, int intOptionId,  short shrSupplierId, int intConditionId, int PriceId, byte bytLangId)
        //{
        //    int ret = 0;
        //    // step 1 Delete PriceId

        //    //#Staff_Activity_Log================================================================================================ STEP 1 ==
        //    IList<object[]> IlistoldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_option_condition_price_extranet", "price_id", PriceId);
        //    //============================================================================================================================
        //    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_option_condition_price_extranet WHERE price_id=@price_id", cn);
        //        cmd.Parameters.Add("@price_id", SqlDbType.Int).Value = PriceId;
        //        cn.Open();
        //        ret = ExecuteNonQuery(cmd);
        //    }
        //    //#Staff_Activity_Log================================================================================================ STEP 2 ============
        //    StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_Price_Extra, StaffLogActionType.Delete, StaffLogSection.Product, intProductId,
        //        "tbl_product_option_condition_price_extranet", IlistoldValue, "price_id", PriceId);
        //    //==================================================================================================================== COMPLETED ========


        //    //---------------------------------------------------------------------------------------------------------------------------------


        //    //Step 2 Delete condition content

        //    //#Staff_Activity_Log================================================================================================ STEP 1 ==
        //    IList<object[]> IlistoldValue1 = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_option_condition_title_lang_extra_net", "condition_id,lang_id", intConditionId, bytLangId);
        //    //============================================================================================================================
        //    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_option_condition_title_lang_extra_net WHERE condition_id =@condition_id AND lang_id=@lang_id", cn);
        //        cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
        //        cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
        //        cn.Open();
        //        ret = ExecuteNonQuery(cmd);
        //    }

        //    //#Staff_Activity_Log================================================================================================ STEP 2 ============
        //    StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_Condition_Extra, StaffLogActionType.Delete, StaffLogSection.Product, intProductId,
        //        "tbl_product_option_condition_title_lang_extra_net", IlistoldValue1, "condition_id,lang_id", intConditionId, bytLangId);
        //    //==================================================================================================================== COMPLETED ========

        //    //---------------------------------------------------------------------------------------------------------------------------------

        //    // step 3 Delete Condition

        //    //#Staff_Activity_Log================================================================================================ STEP 1 ==
        //    IList<object[]> IlistoldValue2 = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_option_condition_extra_net", "condition_id", intConditionId);
        //    //============================================================================================================================
        //    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_option_condition_extra_net WHERE condition_id=@condition_id", cn);
        //        cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
        //        cn.Open();
        //        ret = ExecuteNonQuery(cmd);
        //    }
        //    //#Staff_Activity_Log================================================================================================ STEP 2 ============
        //    StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_Condition_Extra, StaffLogActionType.Delete, StaffLogSection.Product, intProductId,
        //        "tbl_product_option_condition_extra_net", IlistoldValue2, "condition_id", intConditionId);
        //    //==================================================================================================================== COMPLETED ========

        //    //---------------------------------------------------------------------------------------------------------------------------------

        //    // step 4 Delete Option Supplier

        //    //#Staff_Activity_Log================================================================================================ STEP 1 ==
        //    IList<object[]> IlistoldValue3 = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_option_supplier", "option_id,supplier_id", intOptionId, shrSupplierId);
        //    //============================================================================================================================
        //    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_option_supplier WHERE option_id=@option_id AND supplier_id=@supplier_id", cn);
        //        cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
        //        cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
        //        cn.Open();
        //        ret = ExecuteNonQuery(cmd);
        //    }
        //    //#Staff_Activity_Log================================================================================================ STEP 2 ============
        //    StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_option_supplier, StaffLogActionType.Delete, StaffLogSection.Product, intProductId,
        //        "tbl_product_option_supplier", IlistoldValue3, "option_id,supplier_id", intOptionId, shrSupplierId);
        //    //==================================================================================================================== COMPLETED ========

        //    //---------------------------------------------------------------------------------------------------------------------------------

        //    // step 5 Delete Option Content
        //    //#Staff_Activity_Log================================================================================================ STEP 1 ==
        //    IList<object[]> IlistoldValue4 = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_option_content", "option_id,lang_id", intOptionId, bytLangId);
        //    //============================================================================================================================
        //    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_option_content WHERE option_id=@option_id AND lang_id=@lang_id", cn);
        //        cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
        //        cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
        //        cn.Open();
        //        ret = ExecuteNonQuery(cmd);
        //    }
        //    //#Staff_Activity_Log================================================================================================ STEP 2 ============
        //    StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_option_detail, StaffLogActionType.Delete, StaffLogSection.Product, intProductId,
        //        "tbl_product_option_content", IlistoldValue4, "option_id,lang_id", intOptionId, bytLangId);
        //    //==================================================================================================================== COMPLETED ========

        //    //---------------------------------------------------------------------------------------------------------------------------------

        //    // step 6 Delete Option Gala
        //    //#Staff_Activity_Log================================================================================================ STEP 1 ==
        //    IList<object[]> IlistoldValue5 = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_option_gala", "option_id", intOptionId);
        //    //============================================================================================================================
        //    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_option_gala WHERE option_id=@option_id", cn);
        //        cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                
        //        cn.Open();
        //        ret = ExecuteNonQuery(cmd);
        //    }
        //    //#Staff_Activity_Log================================================================================================ STEP 2 ============
        //    StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_gala, StaffLogActionType.Delete, StaffLogSection.Product, intProductId,
        //        "tbl_product_option_gala", IlistoldValue5, "option_id", intOptionId);
        //    //==================================================================================================================== COMPLETED ========

        //    //---------------------------------------------------------------------------------------------------------------------------------
        //    // step 7 Delete Option Main
        //    //#Staff_Activity_Log================================================================================================ STEP 1 ==
        //    IList<object[]> IlistoldValue6 = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_option", "option_id", intOptionId);
        //    //============================================================================================================================
        //    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_option WHERE option_id=@option_id", cn);
        //        cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;

        //        cn.Open();
        //        ret = ExecuteNonQuery(cmd);
        //    }
        //    //#Staff_Activity_Log================================================================================================ STEP 2 ============
        //    StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_option_detail, StaffLogActionType.Delete, StaffLogSection.Product, intProductId,
        //        "tbl_product_option", IlistoldValue6, "option_id", intOptionId);
        //    //==================================================================================================================== COMPLETED ========

        //    return (ret == 1);
        //}
    }
}