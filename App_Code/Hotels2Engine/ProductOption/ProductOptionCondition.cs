using System;
using System.Data;
using System.Data.SqlClient; 
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.Production;
using Hotels2thailand.Staffs;


/// <summary>
/// Summary description for ProductOptionCondition
/// </summary>
/// 
namespace Hotels2thailand.ProductOption
{
    public class ProductOptionCondition : Hotels2BaseClass
    {
        public int ConditionId { get; set; }
        private byte _market_id;
        public byte MarketId
        {
            get { return _market_id; }
            set { _market_id = value; }
        }
        public string Title { get; set; }
        public int OptionId { get; set; }
        public byte Breakfast { get; set; }

        private byte _num_adult;
        private byte _num_children;
        private byte _num_extra;

        public byte NumAdult 
        { 
            get{return _num_adult;}
            set { _num_adult = value; }
        }

        public byte NumChildren
        {
            get { return _num_children; }
            set { _num_children = value; }
        }
        public byte NumExtra
        {
            get { return _num_extra; }
            set { _num_extra = value; }
        }
        public bool Status { get; set; }
        public byte Priority { get; set; }
        public byte DayMin { get; set; }
        public bool HasTransfer { get; set; }

        private LinqProductionDataContext dcOption = new LinqProductionDataContext();

        public ProductOptionCondition()
        {
            //Default Value Market WorldWide
            _market_id = 1;

            _num_adult = 2;
            _num_children = 1;
            _num_extra = 2;
        }

        

        public List<object> getConditionList(int intOptionId)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT condition_id,market_id,title,option_id,breakfast,num_adult,num_children,num_extra,status,priority,day_min,has_transfer FROM tbl_product_option_condition WHERE option_id=@option_id ORDER BY status DESC",cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));

            }
               
        }

        public List<object> getConditionListOpenOnly(int intOptionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT condition_id,market_id,title,option_id,breakfast,num_adult,num_children,num_extra,status,priority,day_min,has_transfer FROM tbl_product_option_condition WHERE option_id=@option_id AND status = 1 ORDER BY status DESC", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
            

        }

        public List<object> getListConditionByOptionId(int intOptionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT condition_id, market_id, title, option_id, breakfast,num_adult,num_children,num_extra,status,priority,day_min,has_transfer FROM tbl_product_option_condition WHERE option_id = @option_id AND status = 1", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));

            }
        }

        public int getConditionByOptionIdGalaOnly(int intOptionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                int Result = 0;
                SqlCommand cmd = new SqlCommand("SELECT condition_id FROM tbl_product_option_condition WHERE option_id = @option_id AND status = 1", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    Result = (int)reader[0];
                }
                else
                {
                    Result = 0;
                }

                return Result;
             }
        }

        //public ProductOptionCondition getConditionByOptionId(int intOptionId)
        //{
        //    var Result = dcOption.tbl_product_option_conditions.SingleOrDefault(con => con.option_id == intOptionId);
        //    //var Result = (from opc in dcOption.tbl_product_option_conditions
        //    //             where opc.option_id == intOptionId
        //    //             select opc).Take(1);
        //    if (Result == null)
        //        return null;
        //    else
        //    {
        //        return (ProductOptionCondition)MappingObjectFromDataContext(Result);
        //    }
        //}

        public ProductOptionCondition getConditionById(int intConId)
        {
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT condition_id,market_id,title,option_id,breakfast,num_adult,num_children,num_extra,status,priority,day_min,has_transfer FROM tbl_product_option_condition WHERE condition_id=@condition_id ", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                
                if (reader.Read())
                    return (ProductOptionCondition)MappingObjectFromDataReader(reader);
                else
                    return null;

            }
            
        }


        public static void InsertQuickCondition(string strTitle, int intOptionId )
        {
            //LinqProductionDataContext dcOption = new LinqProductionDataContext();
            ProductOptionCondition cOptionC = new ProductOptionCondition();
            //int Insert = dcOption.ExecuteCommand("INSERT INTO tbl_product_option_condition (title,market_id,option_id,breakfast,num_adult,num_children,num_extra,status) VALUES({0},{1},{2},{3},{4},{5},{6},{7})",
            //    strTitle, 1, intOptionId, 0, 0, 0, 0, true);
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(cOptionC.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_condition (title,market_id,option_id,breakfast,num_adult,num_children,num_extra,status) VALUES(@title,@market_id,@option_id,@breakfast,@num_adult,@num_children,@num_extra,@status); SET @condition_id =SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@market_id", SqlDbType.TinyInt).Value = 1;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strTitle;
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@breakfast", SqlDbType.TinyInt).Value = 0;
                cmd.Parameters.Add("@num_adult", SqlDbType.TinyInt).Value = 0;
                cmd.Parameters.Add("@num_children", SqlDbType.TinyInt).Value = 0;
                cmd.Parameters.Add("@num_extra", SqlDbType.TinyInt).Value = 0;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                cOptionC.ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@condition_id"].Value;
            }

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_option_condition, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductId, "tbl_product_option_condition", "market_id,title,option_id,breakfast,num_adult,num_children,num_extra,status", "condition_id", ret);
            //========================================================================================================================================================
        }


        public int InsertNewCondition(byte MarketId, string strTitle, int intOptionId, byte bytABF, byte bytNumAdult, byte bytNumChild, byte bytNumExtra, bool bolStatus)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_condition (market_id,title,option_id,breakfast,num_adult,num_children,num_extra,status) VALUES(@market_id,@title,@option_id,@breakfast,@num_adult,@num_children,@num_extra,@status); SET @condition_id=SCOPE_IDENTITY()", cn);
                cmd.Parameters.Add("@market_id", SqlDbType.Int).Value = MarketId;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strTitle;
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@breakfast", SqlDbType.Int).Value = bytABF;
                cmd.Parameters.Add("@num_adult", SqlDbType.Int).Value = bytNumAdult;
                cmd.Parameters.Add("@num_children", SqlDbType.TinyInt).Value = bytNumChild;
                cmd.Parameters.Add("@num_extra", SqlDbType.TinyInt).Value = bytNumExtra;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                
                cn.Open();

                ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@condition_id"].Value;
                
            }

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_option_condition, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductId, "tbl_product_option_condition", "market_id,title,option_id,breakfast,num_adult,num_children,num_extra,status", "condition_id", ret);
            //========================================================================================================================================================

            return ret;
        }
        //public int InsertNewCondition(ProductOptionCondition cCondition)
        //{
        //    var insert = new tbl_product_option_condition
        //    {
        //        market_id = cCondition.MarketId,
        //        title = cCondition.Title,
        //        option_id = cCondition.OptionId,
        //        breakfast = cCondition.Breakfast,
        //        num_adult = cCondition.NumAdult,
        //        num_children = cCondition.NumChildren,
        //        num_extra = cCondition.NumExtra,
        //        status = cCondition.Status,
        //    };

        //    dcOption.tbl_product_option_conditions.InsertOnSubmit(insert);
        //    dcOption.SubmitChanges();
        //    return insert.condition_id;
        //}

        public bool UpdateMarketcondition(int intConditionId, byte bytMarketId)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_condition", "condition_id", intConditionId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition SET market_id=@market_id WHERE condition_id=@condition_id", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cmd.Parameters.Add("@market_id", SqlDbType.TinyInt).Value = bytMarketId;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
                
            }
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_option_condition, StaffLogActionType.Update, StaffLogSection.Product, ProductId,
                "tbl_product_option_condition", "market_id", arroldValue, "condition_id", intConditionId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        public bool Update()
        {
            return ProductOptionCondition.UpdateConditions(this.ConditionId, this.MarketId,this.Title, this.Breakfast,this.NumAdult, this.NumChildren, this.NumExtra, this.Status, this.HasTransfer);
        }

        public static bool UpdateConditions(int intConditionId, byte bytMarketId, string strtitle, byte bytBreakfast, byte bytNumAdult, byte bytNumchild, byte bytNumExtra, bool bolStatus, bool bolHastransfer)
        {
            ProductOptionCondition cUpdate = new ProductOptionCondition
            {
                ConditionId = intConditionId,
                MarketId = bytMarketId,
                Title = strtitle,
                Breakfast = bytBreakfast,
                NumAdult = bytNumAdult,
                NumChildren = bytNumchild,
                NumExtra = bytNumExtra,
                Status = bolStatus,
                HasTransfer = bolHastransfer
            };

            return cUpdate.UpdateCondition(cUpdate);
        }

        public bool UpdateCondition(ProductOptionCondition cCondition)
        {
            
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_condition", "market_id,title,breakfast,num_adult,num_children,num_extra,status,has_transfer", "condition_id", cCondition.ConditionId);
            //============================================================================================================================

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition SET market_id=@market_id,title=@title,breakfast=@breakfast,num_adult=@num_adult,num_children=@num_children,num_extra=@num_extra,status=@status,has_transfer=@has_transfer WHERE condition_id=@condition_id", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = cCondition.ConditionId;
                cmd.Parameters.Add("@market_id", SqlDbType.TinyInt).Value = cCondition.MarketId;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = cCondition.Title;
                cmd.Parameters.Add("@breakfast", SqlDbType.TinyInt).Value = cCondition.Breakfast;
                cmd.Parameters.Add("@num_adult", SqlDbType.TinyInt).Value = cCondition.NumAdult;
                cmd.Parameters.Add("@num_children", SqlDbType.TinyInt).Value = cCondition.NumChildren;
                cmd.Parameters.Add("@num_extra", SqlDbType.TinyInt).Value = cCondition.NumExtra;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = cCondition.Status;
                cmd.Parameters.Add("@has_transfer", SqlDbType.Bit).Value = cCondition.HasTransfer;
                cn.Open();

                ExecuteNonQuery(cmd);
            }
            

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_option_condition, StaffLogActionType.Update, StaffLogSection.Product, ProductId,
                "tbl_product_option_condition", "market_id,title,breakfast,num_adult,num_children,num_extra,status,has_transfer", arroldValue, "condition_id", cCondition.ConditionId);
            //==================================================================================================================== COMPLETED ========
            return true;
        }

        

       

    }
}