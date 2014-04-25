using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for ProductLandmark
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductLandmark : Hotels2BaseClass
    {
        
        public int ProductID { get; set; }
        public int LandmarkID { get; set; }
        public byte TransportId { get; set; }
        //return Minute unit 
        public byte TransportTime { get; set; }
        //retuen Meter unit
        public short TransportDistance { get;set; }

        
        public string LandmarkTitle{ get; set; }
        public string LandmarkTitleCat { get; set; }
        //private string _landmark

        public List<object> geProductLandmarkListbyProduct(int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT pl.product_id,pl.landmark_id,pl.transport_id,pl.transport_time,pl.transport_distance , lm.title, lmc.title ");
                query.Append("FROM tbl_product_landmark pl, tbl_landmark lm, tbl_landmark_category lmc ");
                query.Append("WHERE pl.landmark_id = lm.landmark_id AND lm.landmark_cat_id = lmc.landmark_cat_id AND pl.product_id = @product_id ");
                query.Append("ORDER BY lm.title");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
           
        }



        public int InsertProductLandMarkFirst(int intProductId, int LandMarkId)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_landmark (product_id,landmark_id,transport_id,transport_time,transport_distance) VALUES(@product_id,@landmark_id,'1','0','0')", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@landmark_id", SqlDbType.Int).Value = LandMarkId;
                cn.Open();
                ret =  ExecuteNonQuery(cmd);
                
                
            }
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_detail, StaffLogActionType.Insert, StaffLogSection.Product,
                intProductId, "tbl_product_landmark", "product_id,landmark_id,transport_id,transport_time,transport_distance", "product_id,landmark_id", intProductId, LandMarkId);
            //========================================================================================================================================================
            return ret;
        }


        public int IsHaveRecord(int intProductId, int LandMarkId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(product_id) FROM tbl_product_landmark WHERE product_id=@product_id AND landmark_id=@landmark_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@landmark_id", SqlDbType.Int).Value = LandMarkId;
                cn.Open();
                int ret = (int)ExecuteScalar(cmd);
                return ret;
            }
        }

        public bool DeleteLandMark(int intProductId, int LandMarkId)
        {
            IList<object[]> arroldValue = null;


            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            arroldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_landmark", "product_id,landmark_id", intProductId, LandMarkId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_landmark WHERE product_id=@product_id AND landmark_id=@landmark_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@landmark_id", SqlDbType.Int).Value = LandMarkId;
                cn.Open();
                ret = (int)ExecuteNonQuery(cmd);
                
            }

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ==
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_detail, StaffLogActionType.Delete, StaffLogSection.Product, ProductId,
                "tbl_product_landmark", arroldValue, "product_id,landmark_id", intProductId, LandMarkId);
            //============================================================================================================================

            return (ret == 1);
        }

        public bool UpdateProductLandmark(int intProductId, int LandMarkId, byte byttransId, byte bytTime, short shrDistance)
        {

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_landmark", "transport_id,transport_time,transport_distance", "product_id,landmark_id", intProductId, LandMarkId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_landmark SET transport_id=@transport_id,transport_time=@transport_time,transport_distance=@transport_distance WHERE product_id=@product_id AND landmark_id=@landmark_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@landmark_id", SqlDbType.Int).Value = LandMarkId;
                cmd.Parameters.Add("@transport_id", SqlDbType.TinyInt).Value = byttransId;
                cmd.Parameters.Add("@transport_time", SqlDbType.TinyInt).Value = bytTime;
                cmd.Parameters.Add("@transport_distance", SqlDbType.SmallInt).Value = shrDistance;       
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_detail, StaffLogActionType.Update, StaffLogSection.Product, ProductId,
                "tbl_product_landmark", "transport_id,transport_time,transport_distance", arroldValue, "product_id,landmark_id", intProductId, LandMarkId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }
        //public int Insert(ProductLandmark landmark)
        //{
        //    ProductLandmark cProductLandmark = new ProductLandmark();
        //    int ret = 0;
        //    if (cProductLandmark.getProductLandmarkByLandmarkIdandproductId(landmark.LandmarkID, landmark.ProductID) == null)
        //    {
        //        tbl_product_landmark productLandmark = new tbl_product_landmark
        //        {
        //            product_id = landmark.ProductID,
        //            landmark_id = landmark.LandmarkID,
        //            transport_id = landmark.TransportId,
        //            transport_time = landmark.TransportTime,
        //            transport_distance = landmark.TransportDistance
        //        };

        //        dcProduct.tbl_product_landmarks.InsertOnSubmit(productLandmark);
        //        dcProduct.SubmitChanges();
        //        return ret + productLandmark.landmark_id;
        //    }
        //    else
        //    {

        //        return ret;
        //    }
        //}


        //public bool Update()
        //{
        //    return ProductLandmark.UpdateProductLandmarkStatic(this.LandmarkID, this.ProductID, this.TransportId, this.TransportTime, this.TransportDistance);
        //}
        //public static bool UpdateProductLandmarkStatic(int intLandmarkId, int intProductId,byte bytTransportId, byte bytTransportTime, short shrTransportDistance)
        //{
        //    ProductLandmark cProductLandmark = new ProductLandmark
        //    {
        //        LandmarkID = intLandmarkId,
        //        ProductID = intProductId,
        //        TransportId = bytTransportId,
        //        TransportTime = bytTransportTime,
        //        TransportDistance = shrTransportDistance
        //    };

        //    return cProductLandmark.UpdateProductLandmark(cProductLandmark);
        //}


        //public bool UpdateProductLandmark(ProductLandmark landmark)
        //{
        //    var ResultUpdate = dcProduct.tbl_product_landmarks.SingleOrDefault(pl => pl.landmark_id == landmark.LandmarkID && pl.product_id == landmark.ProductID);
        //    ResultUpdate.transport_id = landmark.TransportId; 
        //    ResultUpdate.transport_time = landmark.TransportTime;
        //    ResultUpdate.transport_distance = landmark.TransportDistance;
        //    dcProduct.SubmitChanges();

        //    int ret = 1;
        //    return (ret ==1);
        //}

        //public bool DeleteProductLandmark(int intLandmarkId, int intProductId)
        //{
        //    var ResultDel = dcProduct.tbl_product_landmarks.SingleOrDefault(pl => pl.product_id == intProductId && pl.landmark_id == intLandmarkId);
        //    if (ResultDel == null)
        //        return false;
        //    else
        //    {
        //        dcProduct.tbl_product_landmarks.DeleteOnSubmit(ResultDel);
        //        dcProduct.SubmitChanges();
        //        return true;
        //    }
        //}
    } 
}