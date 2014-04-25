using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.Staffs;
using Hotels2thailand.ProductOption;

/// <summary>
/// Summary description for ProductOptionGala
/// </summary>
/// 
namespace Hotels2thailand.ProductOption
{
    public class ProductOptionGala : Hotels2BaseClass
    {
        private bool _require_adult;
        private bool _require_child;

        public short GalaID { get; set; }
        public int OptionID { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        
        public bool RequireAdult
        {
            get { return _require_adult; }
            set { _require_adult = value; }
        }

        public bool RequireChild
        {
            get { return _require_child; }
            set { _require_child = value; }
        }
        public byte DefaultGala { get; set; }
        public bool IsCompulsory { get; set; }

        public ProductOptionGala()
        {
            _require_adult = false;
            _require_child = false;
        }

        
        private LinqProductionDataContext dcOptionGala = new LinqProductionDataContext();

        //========================== GALA DINNER ===========================================
        //INSERT INTO tbl_option FIRST!
        public static int insertOptionGala(int intProductId, string strTitle,short shrSupplierId)
        {
            LinqProductionDataContext dcOptionGala = new LinqProductionDataContext();
            var Insert = new tbl_product_option
            {
                cat_id = 47,
                product_id = intProductId,
                title = strTitle,
                priority = 0,
                IsShow = false,
                status = true
            };

            dcOptionGala.tbl_product_options.InsertOnSubmit(Insert);
            dcOptionGala.SubmitChanges();

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_gala, StaffLogActionType.Insert, StaffLogSection.Product,
                intProductId, "tbl_product_option", "cat_id, product_id, title, priority, IsShow, status", "option_id", Insert.option_id);
            //========================================================================================================================================================

            //INSERT INTO tbl_product_option_supplier
            Option.insertOptionMappingSupplier(Insert.option_id, shrSupplierId);


            //INSERT INTO tbl_product_option_gala
            //int insertGala = dcOptionGala.ExecuteCommand("INSERT INTO tbl_product_option_gala (option_id,date_start,date_end,require_adult,require_child,default_gala,Is_compulsory) VALUES ({0},{1},{2},{3},{4},{5},{6})",
            //    Insert.option_id, DateTime.Now, DateTime.Now.AddDays(1), false, false, 1,true);
            ProductOptionGala cOptionGala = new ProductOptionGala();
            int insertGala = 0;
            using (SqlConnection cn = new SqlConnection(cOptionGala.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_gala (option_id,date_start,date_end,require_adult,require_child,default_gala,Is_compulsory) VALUES (@option_id,@date_start,@date_end,@require_adult,@require_child,@default_gala,@Is_compulsory); SET @gala_id=SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = Insert.option_id;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = DateTime.Now.AddDays(1).Hotels2ThaiDateTime();
                cmd.Parameters.Add("@require_adult", SqlDbType.Bit).Value = false;
                cmd.Parameters.Add("@require_child", SqlDbType.Bit).Value = false;
                cmd.Parameters.Add("@default_gala", SqlDbType.TinyInt).Value = 1;
                cmd.Parameters.Add("@Is_compulsory", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@gala_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                cOptionGala.ExecuteNonQuery(cmd);
                insertGala = (int)cmd.Parameters["@gala_id"].Value;

            }
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_gala, StaffLogActionType.Insert, StaffLogSection.Product,
                intProductId, "tbl_product_option_gala", "option_id,date_start,date_end,require_adult,require_child,default_gala,Is_compulsory", "gala_id", insertGala);
            //========================================================================================================================================================

            return insertGala;
            
        }

        public static int InsertOptionGalaOnlyExtraNet(int intProductId, int intOptionId, DateTime dDAteGala, bool bolRequireAdult, bool olrequireChild)
        {
           
            ProductOptionGala cOptionGala = new ProductOptionGala();
            int insertGala = 0;
            using (SqlConnection cn = new SqlConnection(cOptionGala.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_gala (option_id,date_start,date_end,require_adult,require_child,default_gala,Is_compulsory) VALUES (@option_id,@date_start,@date_end,@require_adult,@require_child,@default_gala,@Is_compulsory); SET @gala_id=SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDAteGala;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDAteGala;
                cmd.Parameters.Add("@require_adult", SqlDbType.Bit).Value = bolRequireAdult;
                cmd.Parameters.Add("@require_child", SqlDbType.Bit).Value = olrequireChild;
                cmd.Parameters.Add("@default_gala", SqlDbType.TinyInt).Value = 1;
                cmd.Parameters.Add("@Is_compulsory", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@gala_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                cOptionGala.ExecuteNonQuery(cmd);
                insertGala = (int)cmd.Parameters["@gala_id"].Value;

            }
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_gala, StaffLogActionType.Insert, StaffLogSection.Product,
                intProductId, "tbl_product_option_gala", "option_id,date_start,date_end,require_adult,require_child,default_gala,Is_compulsory", "gala_id", insertGala);
            //========================================================================================================================================================
            return insertGala;
        }
        public static int InsertOptionGalaOnly(int intOptionId)
        {
            LinqProductionDataContext dcOptionGala = new LinqProductionDataContext();
            //int insertGala = dcOptionGala.ExecuteCommand("INSERT INTO tbl_product_option_gala (option_id,date_start,date_end,require_adult,require_child,default_gala,Is_compulsory) VALUES ({0},{1},{2},{3},{4},{5},{6})",
            //intOptionId, DateTime.Now, DateTime.Now.AddDays(1), false, false, 1, true);
            ProductOptionGala cOptionGala = new ProductOptionGala();
            int insertGala = 0;
            using (SqlConnection cn = new SqlConnection(cOptionGala.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_gala (option_id,date_start,date_end,require_adult,require_child,default_gala,Is_compulsory) VALUES (@option_id,@date_start,@date_end,@require_adult,@require_child,@default_gala,@Is_compulsory); SET @gala_id=SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = DateTime.Now.AddDays(1).Hotels2ThaiDateTime();
                cmd.Parameters.Add("@require_adult", SqlDbType.Bit).Value = false;
                cmd.Parameters.Add("@require_child", SqlDbType.Bit).Value = false;
                cmd.Parameters.Add("@default_gala", SqlDbType.TinyInt).Value = 1;
                cmd.Parameters.Add("@Is_compulsory", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@gala_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                cOptionGala.ExecuteNonQuery(cmd);
                insertGala = (int)cmd.Parameters["@gala_id"].Value;

            }
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_gala, StaffLogActionType.Insert, StaffLogSection.Product,
                int.Parse(HttpContext.Current.Request.QueryString["pid"]), "tbl_product_option_gala", "option_id,date_start,date_end,require_adult,require_child,default_gala,Is_compulsory", "gala_id", insertGala);
            //========================================================================================================================================================
            return insertGala;
        }

       


        public int InsertOptionGala(ProductOptionGala data)
        {
            tbl_product_option_gala optionGala = new tbl_product_option_gala
            {
                option_id = data.OptionID,
                date_start = data.DateStart,
                date_end = data.DateEnd,
                require_adult = data.RequireAdult,
                require_child = data.RequireChild,
                default_gala = data.DefaultGala,
                Is_compulsory = data.IsCompulsory
            };
            dcOptionGala.tbl_product_option_galas.InsertOnSubmit(optionGala);
            dcOptionGala.SubmitChanges();

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_gala, StaffLogActionType.Insert, StaffLogSection.Product,
                data.OptionID, "tbl_product_option_gala", "option_id,date_start,date_end,require_adult,require_child,default_gala,Is_compulsory", "gala_id", optionGala.gala_id);
            //========================================================================================================================================================
            return optionGala.gala_id;
        }


        public bool Update()
        {
            return ProductOptionGala.UpdateGala(this.GalaID, this.OptionID, this.DateStart, this.DateEnd, this.RequireAdult, this.RequireChild, this.DefaultGala, this.IsCompulsory);
        }

        public static bool UpdateGala(short shrGalaId, int intOptionId, DateTime dDateStart, DateTime dDateEnd, bool ReqAdult, bool ReqChild, byte bytDefaultGala,bool bolIsCompulsory)
        {
            ProductOptionGala cProductGala = new ProductOptionGala
            {
                GalaID = shrGalaId,
                OptionID = intOptionId,
                DateStart = dDateStart,
                DateEnd = dDateEnd,
                RequireAdult = ReqAdult,
                RequireChild = ReqChild,
                DefaultGala = bytDefaultGala,
                IsCompulsory = bolIsCompulsory
            };

            return cProductGala.UpdateOptionGala(cProductGala);
        }

        public bool UpdateOptionGala(ProductOptionGala data)
        {
            tbl_product_option_gala RsProductOption = dcOptionGala.tbl_product_option_galas.SingleOrDefault(og => og.option_id == data.OptionID);
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep(data.DateStart, data.DateEnd, data.RequireAdult, data.RequireChild, data.DefaultGala, data.IsCompulsory);
            //============================================================================================================================
            RsProductOption.date_start = data.DateStart;
            RsProductOption.date_end = data.DateEnd;
            RsProductOption.require_adult = data.RequireAdult;
            RsProductOption.require_child = data.RequireChild;
            RsProductOption.default_gala = data.DefaultGala;
            RsProductOption.Is_compulsory = data.IsCompulsory;
            dcOptionGala.SubmitChanges();

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_gala, StaffLogActionType.Update, StaffLogSection.Product,
                int.Parse(HttpContext.Current.Request.QueryString["pid"]), "tbl_product_option_gala", "date_start,date_end,require_adult,require_child,default_gala,Is_compulsory", arroldValue, "gala_id", data.GalaID);
            //==================================================================================================================== COMPLETED ========
            return true;
        }

        public bool UpdateOptionGalaExtranet(int intProductId, int intOptionId, DateTime dDateGala, bool bolForAult, bool bolForchild)
        {
            int ret = 0;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_gala", "date_start,date_end,require_adult,require_child", "option_id", intOptionId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_gala SET date_start=@date_start , date_end=@date_end, require_adult=@require_adult, require_child=@require_child WHERE option_id=@option_id", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateGala;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateGala;
                cmd.Parameters.Add("@require_adult", SqlDbType.Bit).Value = bolForAult;
                cmd.Parameters.Add("@require_child", SqlDbType.Bit).Value = bolForchild;
                cn.Open();
                ExecuteNonQuery(cmd);
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_gala, StaffLogActionType.Update, StaffLogSection.Product,
                intProductId, "tbl_product_option_gala", "date_start,date_end,require_adult,require_child", arroldValue, "option_id", intOptionId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }

        public List<object> GetProducOptiontGalaAll()
        {
            var result = from item in dcOptionGala.tbl_product_option_galas
                         select item;

            return MappingObjectFromDataContextCollection(result);
        }

        public ProductOptionGala GetProductOptionGalaById(int intOptionGalaId)
        {
            var result = dcOptionGala.tbl_product_option_galas.SingleOrDefault(og => og.gala_id == intOptionGalaId);
            return (ProductOptionGala)MappingObjectFromDataContext(result);
        }

        public ProductOptionGala GetProductOptionGalabyOptionId(int intOptionId)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT gala_id,option_id,date_start,date_end,require_adult,require_child,default_gala,Is_compulsory FROM tbl_product_option_gala WHERE option_id=@option_id", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return (ProductOptionGala)MappingObjectFromDataReader(reader);
                }
                else
                {
                    return null;
                }
            }
          
  
        }

        
        

    }
}