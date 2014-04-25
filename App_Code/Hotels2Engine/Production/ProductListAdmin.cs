using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.LinqProvider.Supplier;
using Hotels2thailand.Production;
using Hotels2thailand.Staffs;
using System.Text;

/// <summary>
/// Summary description for ProductList
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductListAdmin : Hotels2BaseClass
    {
        public ProductListAdmin()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public byte ProductCatId { get; set; }
        public string Producttitle { get; set; }
        public string ProductFileName { get; set; }
        public string DesTitle { get; set; }
        public string DesFolderName { get; set; }
        public short SupplierId { get; set; }
        public string suppliertitle { get; set; }
        public byte supplierCatId { get; set; }
        public string SupplierCatTitle { get; set; }
        public short StatusId { get; set; }
        public string StatusTitle { get; set; }
        public bool FlagFeture {get;set;}
        public bool Status { get; set; }
        public string WebsiteName { get; set; }
        public byte BookingType { get; set; }
        public byte GateWayId { get; set; }
        public byte ManageID { get; set; }
        //public Nullable<DateTime> DateExpired { get; set; }

        //p.product_id, p.product_code,p.cat_id as ProductCatId, p.title as Producttitle, d.title as Destiantiontitle,p.supplier_price, 
        //s.title as suppliertitle, s.cat_id as supplierCatId, sc.title as SupplierCatTitle, p.status_id,st.title as StatusTitle, p.flag_feature,p.status
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();


        public List<object> GetProductListShowExtranetByChainID(short shrChainID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT p.product_id, p.product_code,p.cat_id as ProductCatId, p.title as Producttitle, pc.file_name_main, d.title as Destiantiontitle, d.folder_destination,  p.supplier_price, s.title as suppliertitle, s.cat_id as supplierCatId, sc.title as SupplierCatTitle, p.status_id,st.title as StatusTitle, p.flag_feature,p.status, pg.web_site_name,pg.booking_type_id,pg.gateway_id,pg.manage_id");
                query.Append(" FROM tbl_product p, tbl_product_chain pca, tbl_product_content pc, tbl_status st, tbl_destination d, tbl_supplier s, tbl_supplier_category sc, tbl_product_booking_engine pg WHERE p.Isextranet = 1 AND p.product_id=pca.product_id AND pc.product_id= p.product_id AND pc.lang_id = 1 AND d.destination_id = p.destination_id AND p.supplier_price = s.supplier_id  AND s.cat_id = sc.cat_id AND st.cat_id = 1 AND st.status_id = p.status_id AND p.product_id = pg.product_id AND pca.status = 1 AND pca.chain_id=@chain_id ORDER BY p.title");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@chain_id", SqlDbType.SmallInt).Value = shrChainID;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

        }


        public IList<object> getProductListProcedure(int intProductCat, short intDesId,  byte intProcessStatus, bool bolstatus)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Productlist", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ProductCatId", SqlDbType.TinyInt).Value = intProductCat;
                cmd.Parameters.Add("@DestinationId", SqlDbType.SmallInt).Value = intDesId;
                //cmd.Parameters.Add("@SupplierId", SqlDbType.SmallInt).Value = intSupplierId;
                //cmd.Parameters.Add("@FlagFeature", SqlDbType.Bit).Value = bolRec;
                cmd.Parameters.Add("@StatusProcessId", SqlDbType.TinyInt).Value = intProcessStatus;
                cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = bolstatus;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));

            }
        }

        public IList<object> getProductListBhtManage()
        {
            StringBuilder Query = new StringBuilder();
            
             Query.Append("SELECT p.product_id, p.product_code,p.cat_id as ProductCatId, p.title as Producttitle, pc.file_name_main, d.title as Destiantiontitle, d.folder_destination,  p.supplier_price, s.title as suppliertitle, s.cat_id as supplierCatId, sc.title as SupplierCatTitle, p.status_id,st.title as StatusTitle, p.flag_feature,p.status, pg.web_site_name,pg.booking_type_id,pg.gateway_id,pg.manage_id");
            Query.Append(" FROM tbl_product p,  tbl_product_content pc, tbl_status st, tbl_destination d, tbl_supplier s, tbl_supplier_category sc, tbl_product_booking_engine pg WHERE p.Isextranet = 1 AND pc.product_id= p.product_id AND pc.lang_id = 1 AND d.destination_id = p.destination_id AND p.supplier_price = s.supplier_id  AND s.cat_id = sc.cat_id AND st.cat_id = 1 AND st.status_id = p.status_id AND p.product_id = pg.product_id AND p.cat_id = 29 AND pg.manage_id = 2");

            Query.Append(" ORDER BY p.status_id, p.title, p.product_code");
           
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        public IList<object> getProductListBhtManageB2b()
        {
            StringBuilder Query = new StringBuilder();

            Query.Append("SELECT p.product_id, p.product_code,p.cat_id as ProductCatId, p.title as Producttitle, pc.file_name_main, d.title as Destiantiontitle, d.folder_destination,  p.supplier_price, s.title as suppliertitle, s.cat_id as supplierCatId, sc.title as SupplierCatTitle, p.status_id,st.title as StatusTitle, p.flag_feature,p.status, pg.web_site_name,pg.booking_type_id,pg.gateway_id,pg.manage_id");
            Query.Append(" FROM tbl_product p,  tbl_product_content pc, tbl_status st, tbl_destination d, tbl_supplier s, tbl_supplier_category sc, tbl_product_booking_engine pg WHERE p.Isextranet = 1 AND pc.product_id= p.product_id AND pc.lang_id = 1 AND d.destination_id = p.destination_id AND p.supplier_price = s.supplier_id  AND s.cat_id = sc.cat_id AND st.cat_id = 1 AND st.status_id = p.status_id AND p.product_id = pg.product_id AND p.cat_id = 29 AND pg.manage_id = 2 AND pg.is_b2b =1 ");

            Query.Append(" ORDER BY p.status_id, p.title, p.product_code");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public IList<object> getProductListHotelManage()
        {
            //string strSearch = "'%" + TxtSearch + "%'";

            StringBuilder Query = new StringBuilder();

            Query.Append("SELECT p.product_id, p.product_code,p.cat_id as ProductCatId, p.title as Producttitle, pc.file_name_main, d.title as Destiantiontitle, d.folder_destination,  p.supplier_price, s.title as suppliertitle, s.cat_id as supplierCatId, sc.title as SupplierCatTitle, p.status_id,st.title as StatusTitle, p.flag_feature,p.status, pg.web_site_name,pg.booking_type_id,pg.gateway_id,pg.manage_id");
            Query.Append(" FROM tbl_product p,  tbl_product_content pc, tbl_status st, tbl_destination d, tbl_supplier s, tbl_supplier_category sc, tbl_product_booking_engine pg WHERE p.Isextranet = 1 AND pc.product_id= p.product_id AND pc.lang_id = 1 AND d.destination_id = p.destination_id AND p.supplier_price = s.supplier_id  AND s.cat_id = sc.cat_id AND st.cat_id = 1 AND st.status_id = p.status_id AND p.product_id = pg.product_id AND p.cat_id= 29 AND pg.manage_id = 1");

            Query.Append(" ORDER BY p.status_id, p.title, p.product_code");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        public IList<object> getProductListHotelManage_Offline()
        {
            //string strSearch = "'%" + TxtSearch + "%'";

            StringBuilder Query = new StringBuilder();

            Query.Append("SELECT p.product_id, p.product_code,p.cat_id as ProductCatId, p.title as Producttitle, pc.file_name_main, d.title as Destiantiontitle, d.folder_destination,  p.supplier_price, s.title as suppliertitle, s.cat_id as supplierCatId, sc.title as SupplierCatTitle, p.status_id,st.title as StatusTitle, p.flag_feature,p.status, pg.web_site_name,pg.booking_type_id,pg.gateway_id,pg.manage_id");
            Query.Append(" FROM tbl_product p,  tbl_product_content pc, tbl_status st, tbl_destination d, tbl_supplier s, tbl_supplier_category sc, tbl_product_booking_engine pg WHERE p.Isextranet = 1 AND pc.product_id= p.product_id AND pc.lang_id = 1 AND d.destination_id = p.destination_id AND p.supplier_price = s.supplier_id  AND s.cat_id = sc.cat_id AND st.cat_id = 1 AND st.status_id = p.status_id AND p.product_id = pg.product_id AND p.cat_id= 29 AND pg.manage_id = 1 AND pg.booking_type_id = 1");

            Query.Append(" ORDER BY p.status_id, p.title, p.product_code");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        public IList<object> getProductListHotelManage_Online()
        {
            //string strSearch = "'%" + TxtSearch + "%'";

            StringBuilder Query = new StringBuilder();

            Query.Append("SELECT p.product_id, p.product_code,p.cat_id as ProductCatId, p.title as Producttitle, pc.file_name_main, d.title as Destiantiontitle, d.folder_destination,  p.supplier_price, s.title as suppliertitle, s.cat_id as supplierCatId, sc.title as SupplierCatTitle, p.status_id,st.title as StatusTitle, p.flag_feature,p.status, pg.web_site_name,pg.booking_type_id,pg.gateway_id,pg.manage_id");
            Query.Append(" FROM tbl_product p,  tbl_product_content pc, tbl_status st, tbl_destination d, tbl_supplier s, tbl_supplier_category sc, tbl_product_booking_engine pg WHERE p.Isextranet = 1 AND pc.product_id= p.product_id AND pc.lang_id = 1 AND d.destination_id = p.destination_id AND p.supplier_price = s.supplier_id  AND s.cat_id = sc.cat_id AND st.cat_id = 1 AND st.status_id = p.status_id AND p.product_id = pg.product_id AND p.cat_id= 29 AND pg.manage_id = 1 AND pg.booking_type_id = 2");

            Query.Append(" ORDER BY p.status_id, p.title, p.product_code");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }


        public IList<object> getProductListComFlatrate()
        {
            //string strSearch = "'%" + TxtSearch + "%'";

            StringBuilder Query = new StringBuilder();

            Query.Append("SELECT p.product_id, p.product_code,p.cat_id as ProductCatId, p.title as Producttitle, pc.file_name_main, d.title as Destiantiontitle, d.folder_destination,  p.supplier_price, s.title as suppliertitle, s.cat_id as supplierCatId, sc.title as SupplierCatTitle, p.status_id,st.title as StatusTitle, p.flag_feature,p.status, pg.web_site_name,pg.booking_type_id,pg.gateway_id,pg.manage_id");
            Query.Append(" FROM tbl_product p,  tbl_product_content pc, tbl_status st, tbl_destination d, tbl_supplier s, tbl_supplier_category sc, tbl_revenue rv, tbl_product_booking_engine pg WHERE p.Isextranet = 1 AND pc.product_id= p.product_id AND pc.lang_id = 1 AND d.destination_id = p.destination_id AND p.supplier_price = s.supplier_id  AND s.cat_id = sc.cat_id AND st.cat_id = 1 AND st.status_id = p.status_id AND p.product_id = pg.product_id AND rv.product_id=p.product_id  AND rv.cat_id=1");

            Query.Append(" ORDER BY p.status_id, p.title, p.product_code");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public IList<object> getProductListComMonthly()
        {
            //string strSearch = "'%" + TxtSearch + "%'";

            StringBuilder Query = new StringBuilder();

            Query.Append("SELECT p.product_id, p.product_code,p.cat_id as ProductCatId, p.title as Producttitle, pc.file_name_main, d.title as Destiantiontitle, d.folder_destination,  p.supplier_price, s.title as suppliertitle, s.cat_id as supplierCatId, sc.title as SupplierCatTitle, p.status_id,st.title as StatusTitle, p.flag_feature,p.status, pg.web_site_name,pg.booking_type_id,pg.gateway_id,pg.manage_id");
            Query.Append(" FROM tbl_product p,  tbl_product_content pc, tbl_status st, tbl_destination d, tbl_supplier s, tbl_supplier_category sc, tbl_revenue rv, tbl_product_booking_engine pg WHERE p.Isextranet = 1 AND pc.product_id= p.product_id AND pc.lang_id = 1 AND d.destination_id = p.destination_id AND p.supplier_price = s.supplier_id  AND s.cat_id = sc.cat_id AND st.cat_id = 1 AND st.status_id = p.status_id AND p.product_id = pg.product_id AND rv.product_id=p.product_id  AND rv.cat_id=2");

            Query.Append(" ORDER BY p.status_id, p.title, p.product_code");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public IList<object> getProductListComStep()
        {
            //string strSearch = "'%" + TxtSearch + "%'";

            StringBuilder Query = new StringBuilder();

            Query.Append("SELECT p.product_id, p.product_code,p.cat_id as ProductCatId, p.title as Producttitle, pc.file_name_main, d.title as Destiantiontitle, d.folder_destination,  p.supplier_price, s.title as suppliertitle, s.cat_id as supplierCatId, sc.title as SupplierCatTitle, p.status_id,st.title as StatusTitle, p.flag_feature,p.status, pg.web_site_name,pg.booking_type_id,pg.gateway_id,pg.manage_id");
            Query.Append(" FROM tbl_product p,  tbl_product_content pc, tbl_status st, tbl_destination d, tbl_supplier s, tbl_supplier_category sc, tbl_revenue rv, tbl_product_booking_engine pg WHERE p.Isextranet = 1 AND pc.product_id= p.product_id AND pc.lang_id = 1 AND d.destination_id = p.destination_id AND p.supplier_price = s.supplier_id  AND s.cat_id = sc.cat_id AND st.cat_id = 1 AND st.status_id = p.status_id AND p.product_id = pg.product_id AND rv.product_id=p.product_id  AND rv.cat_id=3");

            Query.Append(" ORDER BY p.status_id, p.title, p.product_code");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        //bo_ProductList_Advance
        public IList<object> getProductListAdVance(byte intProductCat, string TxtSearch)
        {
            //string strSearch = "'%" + TxtSearch + "%'";

            StringBuilder Query = new StringBuilder();
            //Query.Append("SELECT p.product_id, p.product_code,p.cat_id as ProductCatId, p.title as Producttitle, pc.file_name_main, d.title as Destiantiontitle, d.folder_destination ,p.supplier_price,");
            //Query.Append(" s.title as suppliertitle, s.cat_id as supplierCatId, sc.title as SupplierCatTitle, p.status_id,st.title as StatusTitle, p.flag_feature,p.status");
            ////Query.Append(" (SELECT TOP 1 spp.date_end FROM tbl_product_period spp,tbl_supplier ss WHERE ss.supplier_id=spp.supplier_id AND ss.cat_id=2 AND spp.product_id=p.product_id ORDER BY spp.date_end DESC) as expire_direct");
            //Query.Append(" FROM tbl_product p ,tbl_product_content pc ,tbl_destination d, tbl_supplier s, tbl_supplier_category sc, tbl_status st");
            //Query.Append(" WHERE p.product_id = pc.product_id AND pc.lang_id = 1 AND p.destination_id = d.destination_id and p.supplier_price = s.supplier_id  and s.cat_id = sc.cat_id and p.status_id = st.status_id and st.cat_id = 1");


            Query.Append("SELECT p.product_id, p.product_code,p.cat_id as ProductCatId, p.title as Producttitle, pc.file_name_main, d.title as Destiantiontitle, d.folder_destination,  p.supplier_price, s.title as suppliertitle, s.cat_id as supplierCatId, sc.title as SupplierCatTitle, p.status_id,st.title as StatusTitle, p.flag_feature,p.status, pg.web_site_name,pg.booking_type_id,pg.gateway_id,pg.manage_id");
            Query.Append(" FROM tbl_product p,  tbl_product_content pc, tbl_status st, tbl_destination d, tbl_supplier s, tbl_supplier_category sc, tbl_product_booking_engine pg WHERE p.Isextranet = 1 AND pc.product_id= p.product_id AND pc.lang_id = 1 AND d.destination_id = p.destination_id AND p.supplier_price = s.supplier_id  AND s.cat_id = sc.cat_id AND st.cat_id = 1 AND st.status_id = p.status_id AND p.product_id = pg.product_id");

            char[] item = TxtSearch.Trim().ToCharArray();

            //HttpContext.Current.Response.Write(item[0]);
            //HttpContext.Current.Response.End();
            string TxtCodeSearch = TxtSearch.Hotels2LeftClr(1);
            if (item[0] == '#')
            {
                Query.Append(" and p.cat_id = @ProductCatId  and p.product_code LIKE '%" + TxtCodeSearch.Trim() + "%'");
            }
            else
            {
                Query.Append(" and p.cat_id = @ProductCatId and  p.title LIKE '%" + TxtSearch.Trim() + "%'");
            }

            Query.Append(" ORDER BY p.title");
            //HttpContext.Current.Response.Write(Query.ToString());
            //HttpContext.Current.Response.End();
            //OR p.product_code


            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ProductCatId", SqlDbType.TinyInt).Value = intProductCat;
                //cmd.Parameters.Add("@DestinationId", SqlDbType.SmallInt).Value = intDesId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        //public IList<ProductlistResult> getProductListProcedure(int intProductCat, int intDesId, int intSupplierId, bool bolRec, int intProcessStatus, bool bolstatus)
        //{
        //    var Result = from p in dcProduct.Productlist(intProductCat, intDesId, intSupplierId, bolRec, intProcessStatus, bolstatus)
        //                 select p;
        //    return Result.ToList();
        //}



        public static List<DateTime> getProductExpired(int intProductId)
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            var Result = from p in dcProduct.tbl_products
                         from ps in dcProduct.tbl_product_suppliers
                         
                         where p.product_id == ps.product_id && p.product_id == intProductId && ps.status == true 
                         select new { ps.supplier_id };

            List<DateTime> LDateTime = new List<DateTime>();
            foreach (var item in Result)
            {
                //Check Direct Supplier only
                Suppliers.Supplier cSupplier = new Suppliers.Supplier();
                if (cSupplier.getSupplierById(item.supplier_id).CategoryId != 1)
                {
                    var DateEndMax = from pd in dcProduct.tbl_product_periods
                                     where pd.supplier_id == item.supplier_id && pd.product_id == intProductId
                                     select pd;
                    DateTime dDateEnd = DateTime.Now;
                    if (DateEndMax.Count() > 0)
                    {
                        dDateEnd = DateEndMax.Max(dn => dn.date_end);
                    }

                    LDateTime.Add(dDateEnd);
                }
            }

            return LDateTime;
        }

        public static DateTime getDateTimeMAXfromProductPeriodBySupplierID(short shrSupplierId, int intProductId)
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            var ResultMax = (from pd in dcProduct.ProductPeriodMax(intProductId,shrSupplierId)
                            select pd.Column1).Max();

            if (ResultMax == null)
                return DateTime.Now;
            else
            {

                return (DateTime)ResultMax;
            }
        }

        public static IList<Product_SupplierQueryResult> getSupplierTitle(int intProductId)
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();

            var result = from ps in dcProduct.Product_SupplierQuery(intProductId)
                         select ps;
                
            return result.ToList();
        }



        public static bool UpdateRecommendProduct(int intProductId, int SqlBoolValue)
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product", "flag_feature", "product_id", intProductId);
            //============================================================================================================================

            var Updatestatus = dcProduct.ExecuteCommand("UPDATE tbl_product set flag_feature='" + SqlBoolValue + "' WHERE product_id =" + intProductId);

            
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_detail, StaffLogActionType.Update, StaffLogSection.Product, intProductId, "tbl_product", "flag_feature", arroldValue, "product_id", intProductId);
            //==================================================================================================================== COMPLETED ========
            int ret = 1;
            return (ret==1);
        }
    }
}