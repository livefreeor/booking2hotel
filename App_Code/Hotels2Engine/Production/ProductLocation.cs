using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for ProductLocation
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductLocation : Hotels2BaseClass
    {
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();
        public int ProductID { get; set; }
        public short LocationID { get; set; }
        private string _location_title = string.Empty;
        public string LocationTitle
        {
            get
            {
                Location cLocation = new Location();
                _location_foldertitle = cLocation.getLocationTitleById(this.LocationID);
                return _location_foldertitle;
            }
        }

        private string _location_foldertitle = string.Empty;
        public string LocationFolderName
        {
            get
            {
                Location cLocation = new Location();
                _location_foldertitle = cLocation.getLocationFolderById(this.LocationID);
                return _location_foldertitle;
            }
        }



        public int Insert(int ProductID, short shrLocationID)
        {
            ProductLocation cProductLocation = new ProductLocation();
            int ret = 0;
            if (cProductLocation.geteL0ctionByProductAndLocation(ProductID, LocationID) == null)
            {
                tbl_product_location product_location = new tbl_product_location { product_id = ProductID, location_id = shrLocationID };
                dcProduct.tbl_product_locations.InsertOnSubmit(product_location);
                dcProduct.SubmitChanges();
                ret = product_location.location_id;
                //=== STAFF ACTIVITY =====================================================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_detail, StaffLogActionType.Insert, StaffLogSection.Product,
                    ProductID, "tbl_product_location", "product_id,location_id", "product_id,location_id", ProductID, shrLocationID);
                //========================================================================================================================================================
                return ret;
            }
            else
            {
                return ret;
            }
            
            
        }


        public List<object> getProductLocationCheckedListByProdutId(int intProductId)
        {
            var result = from tp in dcProduct.tbl_product_locations
                         where tp.product_id == intProductId
                         select tp;
            return MappingObjectFromDataContextCollection(result);
        }

        public ProductLocation geteL0ctionByProductAndLocation(int intProductId, short shrLocationId)
        {
            var result = dcProduct.tbl_product_locations.SingleOrDefault(pl => pl.product_id == intProductId && pl.location_id == shrLocationId);
            if (result == null)
            {
                return null;
            }
            else
            {
                return (ProductLocation)MappingObjectFromDataContext(result);
            }
                
        }

        

        public List<object> getLocationListByProductId(int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT product_id,location_id FROM tbl_product_location WHERE product_id=@product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
            
        }

        public string getDefaultLocationPath(int intProductId)
        {
            
            string FolderPath = string.Empty;
            bool IsRecord = false;
            short locationId = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT location_id FROM tbl_product_pic_location_default_path WHERE product_id=@product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();

                IDataReader readerfirst = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (readerfirst.Read())
                {
                    IsRecord = true;
                    locationId = (short)readerfirst[0];
                }
                
            }

            if (IsRecord)
            {
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT folder_location FROM tbl_location WHERE location_id=@location_id", cn);
                    cmd2.Parameters.Add("@location_id", SqlDbType.SmallInt).Value = locationId;
                    cn.Open();
                    IDataReader reader = ExecuteReader(cmd2, CommandBehavior.SingleRow);
                    
                    if (reader.Read())
                    {
                        FolderPath = reader[0].ToString();
                    }
                }
            }
            
            return FolderPath;
        }

        public int InsertDefaultLocationPath(int intProductId, short shrlocationId)
        {
            int ret = 0 ;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_pic_location_default_path (product_id,location_id) VALUES (@product_id,@location_id)", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@location_id", SqlDbType.SmallInt).Value = shrlocationId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
             }
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_detail, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductID, "tbl_product_pic_location_default_path", "product_id,location_id", "product_id,location_id", intProductId, shrlocationId);
            //========================================================================================================================================================
            return ret;
        }

        public ProductLocation getTopOneLactionInProduct(int intProductId)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROm tbl_product_location WHERE product_id=@product_id ORDER BY location_id DESC", cn);
                cmd.Parameters.Add("@product_id",SqlDbType.SmallInt).Value = intProductId;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return (ProductLocation)MappingObjectFromDataReader(reader);
                }
                else
                {
                    return null;
                }
            }
            
        }

        public bool DeleteProductLocation(short shrLocationId, int intProductId)
        {
            IList<object[]> arroldValue = null;


            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            arroldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_location", "product_id,location_id", shrLocationId, intProductId);
            //============================================================================================================================
            var Result = dcProduct.tbl_product_locations.SingleOrDefault(pl => pl.product_id == intProductId && pl.location_id == shrLocationId);

            if (Result == null)
            {
                return false;
            }
            else
            {
                dcProduct.tbl_product_locations.DeleteOnSubmit(Result);
                dcProduct.SubmitChanges();
                
                //#Staff_Activity_Log================================================================================================ STEP 2 ==
                StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_detail, StaffLogActionType.Delete, StaffLogSection.Product, intProductId,
                    "tbl_product_location", arroldValue, "product_id,location_id", shrLocationId, intProductId);
                //============================================================================================================================
                return true;
            }
        }


        public bool DeleteProductLocationNotInCustomCheck(int ProductId, string Loc_NOtIn)
        {
            //IList<object[]> arroldValue = null;
            ////#Staff_Activity_Log================================================================================================ STEP 1 ==
            //arroldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_location", "product_id,location_id", ProductId, intProductId);
            ////============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_location WHERE product_id = @product_id AND location_id NOT IN(" + Loc_NOtIn + ")", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = ProductId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                
            }
            ////#Staff_Activity_Log================================================================================================ STEP 2 ==
            //StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_detail, StaffLogActionType.Delete, StaffLogSection.Product, intProductId,
            //    "tbl_product_location", arroldValue, "product_id,location_id", shrLocationId, intProductId);
            ////============================================================================================================================
            return (ret == 1);
        }

        public bool DeleteProductLocationAllByProductId(int ProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_location WHERE product_id = @product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = ProductId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public int IsHaveLocation(short shrLocationId, int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tbl_product_location WHERE product_id=@product_id AND location_id=@location_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@location_id", SqlDbType.SmallInt).Value = shrLocationId;
                cn.Open();
                int ret = (int)ExecuteScalar(cmd);
                return ret;
            }
        }
    }
}