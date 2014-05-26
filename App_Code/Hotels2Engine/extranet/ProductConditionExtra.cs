using System;
using System.Text;
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
    public class ProductConditionExtra : Hotels2BaseClass
    {
        public int ConditionId { get; set; }
        public int OptionID { get; set; }
        public string Title { get; set; }
        public byte Breakfast { get; set; }
        public byte NumAult { get; set; }
        public byte NumChild { get; set; }
        public byte NumExtra { get; set; }
        public bool Status { get; set; }
        public byte Priority { get; set; }
        public byte DayMin { get; set; }
        public byte ConditionNameId { get; set; }
        public bool IsAdult { get; set; }
        public DateTime? DateExpirePrice { get; set; }

        public string TitleCondition {
            get {
                return this.Title + HttpUtility.HtmlDecode( Hotels2String.AppendConditionDetailExtraNet(this.NumAult, this.Breakfast)
                    .Replace("<strong>","").Replace("</strong>",""));
            }

        }

        public string TitleConditionPackage
        {
            get
            {
                return this.Title + HttpUtility.HtmlDecode(Hotels2String.AppendConditionDetailExtraNet_Package(this.IsAdult)
                    .Replace("<strong>", "").Replace("</strong>", ""));
            }

        }

        public ProductConditionExtra getConditionByConditionId(int intConditionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT condition_id,option_id,title,breakfast,num_adult,num_children,num_extra,status,priority,day_min,condition_name_id,is_adult FROM tbl_product_option_condition_extra_net WHERE condition_id= @condition_id", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (ProductConditionExtra)MappingObjectFromDataReader(reader);
                else
                    return null;
              
            }
        }



        public int CountcheckConditionNameDuplicate(int intOptionId ,byte ConditionNameID, short shrSupplierId, byte bytNumAdult, byte bytNumABF, string conditionId= "")
        {
            StringBuilder query = new StringBuilder();

            string qAbf = "cone.breakfast = 0";
            if (bytNumABF > 0)
                qAbf = "cone.breakfast > 0";

           // int intConditionID = 0;
            

            query.Append("SELECT COUNT(cone.condition_id) FROM tbl_product_option_supplier ops , tbl_product_option_condition_extra_net cone");
            query.Append(" WHERE cone.option_id = ops.option_id AND ops.supplier_id = @supplier_id AND cone.status = 1 ");
            query.Append(" AND cone.condition_name_id = @condition_name_id  AND cone.option_id = @option_id AND cone.num_adult=@num_adult AND " + qAbf );

            if (!String.IsNullOrEmpty(conditionId))
                query.Append(" AND condition_id <> " + conditionId + "");


            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.Int).Value = shrSupplierId;
                cmd.Parameters.Add("@condition_name_id", SqlDbType.TinyInt).Value = ConditionNameID;
                cmd.Parameters.Add("@num_adult", SqlDbType.TinyInt).Value = bytNumAdult;
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }


        public int CountcheckConditionNameDuplicate_package_insert(int intOptionId, byte ConditionNameID, short shrSupplierId, byte bytNumGuest, bool bolIsAdult)
        {
            StringBuilder query = new StringBuilder();

            byte NumAdult = 0;
            byte NumChild = 0;

            if (bolIsAdult)
                NumAdult = bytNumGuest;
            else
                NumChild =bytNumGuest;



            query.Append("SELECT COUNT(cone.condition_id) FROM tbl_product_option_supplier ops , tbl_product_option_condition_extra_net cone");
            query.Append(" WHERE cone.option_id = ops.option_id AND ops.supplier_id = @supplier_id AND cone.status = 1 ");
            query.Append(" AND cone.condition_name_id = @condition_name_id  AND cone.option_id = @option_id AND cone.num_children=@num_children AND cone.num_adult=@num_adult AND is_adult =@is_adult");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.Int).Value = shrSupplierId;
                cmd.Parameters.Add("@condition_name_id", SqlDbType.TinyInt).Value = ConditionNameID;
                cmd.Parameters.Add("@num_adult", SqlDbType.TinyInt).Value = NumAdult;
                cmd.Parameters.Add("@num_children", SqlDbType.TinyInt).Value = NumChild;
                cmd.Parameters.Add("@is_adult", SqlDbType.Bit).Value = bolIsAdult;
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }


        public int CountcheckConditionNameDuplicate_package_update(int intOptionId, int ConditionId, short shrSupplierId, byte bytNumGuest)
        {
            StringBuilder query = new StringBuilder();

           

            query.Append("SELECT COUNT(cone.condition_id) FROM tbl_product_option_supplier ops , tbl_product_option_condition_extra_net cone");
            query.Append(" WHERE cone.option_id = ops.option_id AND ops.supplier_id = @supplier_id AND cone.status = 1 ");
            query.Append(" AND cone.condition_name_id = @condition_name_id  AND cone.option_id = @option_id AND cone.num_children=@num_children AND cone.num_adult=@num_adult AND is_adult =@is_adult");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {

                bool bolIsAdult = false;
                byte bytConNameId = 0;
                cn.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT is_adult,condition_name_id FROM tbl_product_option_condition_extra_net WHERE  condition_id = @condition_id", cn);
                cmd1.Parameters.Add("@condition_id", SqlDbType.Int).Value = ConditionId;
                IDataReader reader = ExecuteReader(cmd1);
                if(reader.Read())
                {
                    bolIsAdult = (bool)reader[0];
                    bytConNameId = (byte)reader[1];
                }

                reader.Close();
                byte NumAdult = 0;
                byte NumChild = 0;
                if (bolIsAdult)
                    NumAdult = bytNumGuest;
                else
                    NumChild = bytNumGuest;
                     
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.Int).Value = shrSupplierId;
                cmd.Parameters.Add("@condition_name_id", SqlDbType.TinyInt).Value = bytConNameId;
                cmd.Parameters.Add("@num_adult", SqlDbType.TinyInt).Value = NumAdult;
                cmd.Parameters.Add("@num_children", SqlDbType.TinyInt).Value = NumChild;
                cmd.Parameters.Add("@is_adult", SqlDbType.Bit).Value = bolIsAdult;
                
                return (int)ExecuteScalar(cmd);
            }
        }

        public ProductConditionExtra getTopConditionByOptionId(int intOptionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 condition_id,option_id,title,breakfast,num_adult,num_children,num_extra,status,priority,day_min,condition_name_id,is_adult FROM tbl_product_option_condition_extra_net WHERE option_id= @option_id AND status = 1", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (ProductConditionExtra)MappingObjectFromDataReader(reader);
                else
                    return null;
               
            }
        }


        public List<object> getConditionListByOptionId(int intOptionId, byte bytLangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_extranet_get_conditionByOptionAndLangId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public IList<object> getConditionListByOptionIdPrice(int intOptionId, byte bytLangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_extranet_get_conditionByOptionAndLangId_datePrice", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int insertNewCondition_Package(int intProductId, int intOptionId, string strtitle, byte bytBreakfast, byte bytNumAudult, byte bytNumChild, byte NumExtra, bool bolStatus, byte byteConditionNameId,bool Isadult)
        {
            int ret = 0;
            int ConditionId = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_condition_extra_net (option_id,title,breakfast,num_adult,num_children,num_extra,status,condition_name_id,is_adult) VALUES (@option_id,@title,@breakfast,@num_adult,@num_children,@num_extra,@status,@condition_name_id,@is_adult); SET @condition_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strtitle;
                cmd.Parameters.Add("@breakfast", SqlDbType.TinyInt).Value = bytBreakfast;
                cmd.Parameters.Add("@num_adult", SqlDbType.TinyInt).Value = bytNumAudult;
                cmd.Parameters.Add("@num_children", SqlDbType.TinyInt).Value = bytNumChild;
                cmd.Parameters.Add("@num_extra", SqlDbType.TinyInt).Value = NumExtra;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cmd.Parameters.Add("@condition_name_id", SqlDbType.TinyInt).Value = byteConditionNameId;
                cmd.Parameters.Add("@is_adult", SqlDbType.Bit).Value = Isadult;
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
                ConditionId = (int)cmd.Parameters["@condition_id"].Value;
                //#Staff_Activity_Log==========================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_Condition_Extra, StaffLogActionType.Insert, StaffLogSection.Product,
                    intProductId, "tbl_product_option_condition_extra_net", "option_id,title,breakfast,num_adult,num_children,num_extra,status,condition_name_id,is_adult",
                    "condition_id", ConditionId);
                //============================================================================================================================

                return ConditionId;
            }
        }

        public int insertNewCondition(int intProductId, int intOptionId, string strtitle, byte bytBreakfast, byte bytNumAudult, byte bytNumChild, byte NumExtra, bool bolStatus, byte byteConditionNameId)
        {
            int ret = 0;
            int ConditionId = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_condition_extra_net (option_id,title,breakfast,num_adult,num_children,num_extra,status,condition_name_id) VALUES (@option_id,@title,@breakfast,@num_adult,@num_children,@num_extra,@status,@condition_name_id); SET @condition_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strtitle;
                cmd.Parameters.Add("@breakfast", SqlDbType.TinyInt).Value = bytBreakfast;
                cmd.Parameters.Add("@num_adult", SqlDbType.TinyInt).Value = bytNumAudult;
                cmd.Parameters.Add("@num_children", SqlDbType.TinyInt).Value = bytNumChild;
                cmd.Parameters.Add("@num_extra", SqlDbType.TinyInt).Value = NumExtra;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cmd.Parameters.Add("@condition_name_id", SqlDbType.TinyInt).Value = byteConditionNameId;
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
                ConditionId = (int)cmd.Parameters["@condition_id"].Value;
                //#Staff_Activity_Log==========================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_Condition_Extra, StaffLogActionType.Insert, StaffLogSection.Product,
                    intProductId, "tbl_product_option_condition_extra_net", "option_id,title,breakfast,num_adult,num_children,num_extra,status,condition_name_id",
                    "condition_id", ConditionId);
                //============================================================================================================================

                return ConditionId;
            }
        }

        public bool UpdateConditionExtraABF(int intProductId,int ConditionId, byte bytNumABF)
        {
            int ret = 0;

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_condition_extra_net", "breakfast", "condition_id", ConditionId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition_extra_net SET breakfast=@breakfast WHERE condition_id=@condition_id", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = ConditionId;
                cmd.Parameters.Add("@breakfast", SqlDbType.TinyInt).Value = bytNumABF;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Condition_Extra, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_extra_net", "breakfast", arroldValue, "condition_id", ConditionId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }
        public bool UpdateConditionExtraStatus(int intProductId, int ConditionId, bool bolStatus)
        {
            int ret = 0;

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_condition_extra_net", "status", "condition_id", ConditionId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition_extra_net SET status=@status WHERE condition_id = @condition_id", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = ConditionId;
                
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
            }


            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Condition_Extra, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_extra_net", "status", arroldValue, "condition_id", ConditionId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }


        public bool UpdateConditionExtra_Package(int intProductId, int ConditionId,  byte bytNumAudult, byte bytNumChild)
        {
            int ret = 0;

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_condition_extra_net", "breakfast,num_adult,num_children,num_extra,status,is_adult", "condition_id", ConditionId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition_extra_net SET num_adult=@num_adult,num_children=@num_children WHERE condition_id = @condition_id", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = ConditionId;
                //cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strtitle;
                //cmd.Parameters.Add("@breakfast", SqlDbType.TinyInt).Value = bytBreakfast;
                cmd.Parameters.Add("@num_adult", SqlDbType.TinyInt).Value = bytNumAudult;
                cmd.Parameters.Add("@num_children", SqlDbType.TinyInt).Value = bytNumChild;
                cmd.Parameters.Add("@num_extra", SqlDbType.TinyInt).Value = NumExtra;
                //cmd.Parameters.Add("@is_adult", SqlDbType.Bit).Value = IsAdult;
                //cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
            }


            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Condition_Extra, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_extra_net", "num_adult,num_children", arroldValue, "condition_id", ConditionId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }
        public bool UpdateConditionExtra(int intProductId,int ConditionId, byte bytBreakfast, byte bytNumAudult, byte bytNumChild, byte NumExtra, bool bolStatus)
        {
            int ret = 0;
            
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_condition_extra_net", "breakfast,num_adult,num_children,num_extra,status", "condition_id", ConditionId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition_extra_net SET breakfast=@breakfast,num_adult=@num_adult,num_children=@num_children,num_extra=@num_extra,status=@status WHERE condition_id = @condition_id",cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = ConditionId;
                //cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strtitle;
                cmd.Parameters.Add("@breakfast", SqlDbType.TinyInt).Value = bytBreakfast;
                cmd.Parameters.Add("@num_adult", SqlDbType.TinyInt).Value = bytNumAudult;
                cmd.Parameters.Add("@num_children", SqlDbType.TinyInt).Value = bytNumChild;
                cmd.Parameters.Add("@num_extra", SqlDbType.TinyInt).Value = NumExtra;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
            }


            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Condition_Extra, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_extra_net", "breakfast,num_adult,num_children,num_extra,status", arroldValue, "condition_id", ConditionId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }

        //======================== Condition Extra Content 

        public int InsertConditionContentExtra(int intProductId, int ConditionId, byte bytLangId, string strTitle)
        {
            int ret = 0;
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_condition_title_lang_extra_net (condition_id,lang_id,title) VALUES (@condition_id,@lang_id,@title)", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = ConditionId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strTitle;

                cn.Open();

                ret = ExecuteNonQuery(cmd);
                
                //#Staff_Activity_Log==========================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_Condition_Extra, StaffLogActionType.Insert, StaffLogSection.Product,
                    intProductId, "tbl_product_option_condition_title_lang_extra_net", "condition_id,lang_id,title",
                    "condition_id,lang_id", ConditionId, bytLangId);
                //============================================================================================================================

                return ret;
            }
        }

        public bool UpdateConditionContentExtra(int intProductId, int ConditionId, byte bytLangId, string strtitle)
        {
            int ret = 0;

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_condition_title_lang_extra_net", "title", "condition_id,lang_id", ConditionId, bytLangId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition_title_lang_extra_net SET title=@title WHERE condition_id = @condition_id AND lang_id=@lang_id", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = ConditionId;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strtitle;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                
                cn.Open();

                ret = ExecuteNonQuery(cmd);
            }


            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Condition_Extra, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_title_lang_extra_net", "title", arroldValue, "condition_id,lang_id", ConditionId, bytLangId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }
    }
}