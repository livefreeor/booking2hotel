using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using Hotels2thailand;
using Hotels2thailand.Staffs;
using Hotels2thailand.LinqProvider.Production;

/// <summary>
/// Summary description for ProductSupplier
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductSupplier : Hotels2BaseClass
    {
        
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();
        
        public short SupplierID { get; set; }
        public int ProductID { get; set; }
        private bool _status;
        public bool Status
        {
            get{return _status;}
            set{_status = value;}
        }

        private string _sup_title = string.Empty;

        public string SupplierTitle
        {
            get
            {
                Suppliers.Supplier cSupplier = new Suppliers.Supplier();
                _sup_title = cSupplier.getSupplierById(this.SupplierID).SupplierTitle;
                return _sup_title;
            }
            
        }

        public ProductSupplier()
        {
            _status = true;
        }

       
        public List<object> getSupplierListByProductID(int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT supplier_id,product_id,status FROM tbl_product_supplier WHERE product_id=@product_id ORDER BY status DESC", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> getSupplierListByProductIDActive(int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT supplier_id,product_id,status FROM tbl_product_supplier WHERE product_id=@product_id AND status = 1 ORDER BY status DESC", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> getSupplierListByProductIdAndActiveOnly(int intProductId)
        {
            var result = from ps in dcProduct.tbl_product_suppliers
                         where ps.product_id == intProductId && ps.status == true
                         orderby ps.status descending
                         select ps;
            return MappingObjectFromDataContextCollection(result);
        }

        

        public ProductSupplier getSupplierProductByProductIdAndSupId(short shrSupId, int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT supplier_id,product_id,status  FROM tbl_product_supplier WHERE supplier_id=@supplier_id AND  product_id=@product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, System.Data.CommandBehavior.SingleRow);
                if (reader.Read())
                    return (ProductSupplier)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
           
        }

        public static int InsertProductSupplier(short shrSupId, int intProductId)
        {
            ProductSupplier cProductSupplier = new ProductSupplier();
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(cProductSupplier.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_supplier (supplier_id,product_id,status) VALUES(@supplier_id,@product_id,1)", cn);
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                ret = cProductSupplier.ExecuteNonQuery(cmd);

            }

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_supplier, StaffLogActionType.Insert, StaffLogSection.Product,
                cProductSupplier.ProductID, "tbl_product_supplier", "supplier_id,product_id,status", "supplier_id,product_id", shrSupId, intProductId);
            //========================================================================================================================================================
            return ret;
        }
        public int InsertNewProductSupplier(int intProductId, short shrSupplierId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_supplier (supplier_id,product_id,status) VALUES (@supplier_id,@product_id,1)", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);

                //=== STAFF ACTIVITY =====================================================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_supplier, StaffLogActionType.Insert, StaffLogSection.Product,
                    intProductId, "tbl_product_supplier", "supplier_id,product_id,status", "supplier_id,product_id", shrSupplierId, intProductId);
                //========================================================================================================================================================
                return ret;
            }
        }

        public int InsertNewProductSupplier(ProductSupplier cProductSupplier)
        {
            tbl_product_supplier insert = new tbl_product_supplier{
                 supplier_id = cProductSupplier.SupplierID,
                  product_id = cProductSupplier.ProductID,
                  status = cProductSupplier.Status
                  
            };
            dcProduct.tbl_product_suppliers.InsertOnSubmit(insert);
            dcProduct.SubmitChanges();

            int ret = insert.product_id;
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_supplier, StaffLogActionType.Insert, StaffLogSection.Product,
                cProductSupplier.ProductID, "tbl_product_supplier", "supplier_id,product_id", "supplier_id,product_id", cProductSupplier.SupplierID, cProductSupplier.ProductID);
            //========================================================================================================================================================
            return ret;
        }

        public bool UpdateStatusProductSupplier(short shrSupId, int intProductId)
        {
            bool Status = this.getSupplierProductByProductIdAndSupId(shrSupId, intProductId).Status;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_supplier", "status", "supplier_id,product_id", shrSupId, intProductId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_supplier SET status=@status WHERE supplier_id=@supplier_id AND  product_id=@product_id", cn);
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                if (Status)
                {
                    
                    cmd.Parameters.Add("@status", SqlDbType.Bit).Value = false;
                }
                else
                {
                    
                    cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                }
                
                cn.Open();
                ret = ExecuteNonQuery(cmd);
               
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_supplier, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_product_supplier", "status", arroldValue, "supplier_id,product_id", shrSupId, intProductId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        
        


        public static bool DeleteProductSupplier(short shrSupplierId, int intProductId)
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            IList<object[]> arroldValue = null;

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            arroldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_supplier", "supplier_id,product_id", shrSupplierId, intProductId);
            //============================================================================================================================
            var Delete = dcProduct.tbl_product_suppliers.SingleOrDefault(s => s.supplier_id == shrSupplierId && s.product_id == intProductId);
            if (Delete == null)
            {
                return false;
            }
            else
            {
                dcProduct.tbl_product_suppliers.DeleteOnSubmit(Delete);
                dcProduct.SubmitChanges();

                int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
                //#Staff_Activity_Log================================================================================================ STEP 2 ==
                StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_supplier, StaffLogActionType.Delete, StaffLogSection.Product, ProductId,
                    "tbl_product_supplier", arroldValue, "supplier_id,product_id", shrSupplierId, intProductId);
                //============================================================================================================================
                return true;
            }

            
        }


    }
}