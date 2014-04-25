using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for Product
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class Landmark : Hotels2BaseClass
    {
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();
        public int LandmarkID { get; set; }
        public byte LandmarkCategoryID { get; set; }
        public short DestinationID { get; set; }
        public string Title { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public List<object> GetLandmarkAll()
        {
            var result = from item in dcProduct.tbl_landmarks
                         select item;

            return MappingObjectFromDataContextCollection(result);
        }

        public List<object> GetLanfmarkByCatAndDesId(byte bytCategory, short shrDesId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT landmark_id,landmark_cat_id,destination_id,title,coor_lat,coor_lon FROM tbl_landmark WHERE destination_id=@destination_id AND landmark_cat_id=@landmark_cat_id ORDER BY title", cn);
                cmd.Parameters.Add("@landmark_cat_id", SqlDbType.TinyInt).Value = bytCategory;
                cmd.Parameters.Add("@destination_id", SqlDbType.SmallInt).Value = shrDesId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
            
        }

        public List<object> GetLanfmarkByCat(byte bytCategory)
        {
            var result = from item in dcProduct.tbl_landmarks
                         where item.landmark_cat_id == bytCategory 
                         orderby item.title
                         select item;

            return MappingObjectFromDataContextCollection(result);
        }

        public Landmark GetLandmarkById(int intLandmarkId)
        {
            var result = dcProduct.tbl_landmarks.SingleOrDefault(l => l.landmark_id == intLandmarkId);
            return (Landmark)MappingObjectFromDataContext(result);
        }

        public int Insert(Landmark landmark) 
        {
            
            tbl_landmark mark = new tbl_landmark
            {
                landmark_cat_id = landmark.LandmarkCategoryID,
                destination_id = landmark.DestinationID,
                title = landmark.Title,
                coor_lat = landmark.Latitude,
                coor_lon = landmark.Longitude
            };
            dcProduct.tbl_landmarks.InsertOnSubmit(mark);
            dcProduct.SubmitChanges();

            int intLandMId = mark.landmark_id;

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_detail, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductId, "tbl_landmark", "landmark_cat_id,destination_id,title,coor_lat,coor_lon", "landmark_id", intLandMId);
            //========================================================================================================================================================

            return intLandMId;

        }

        public bool Update(Landmark landmark)
        { 
            tbl_landmark rsLandmark=dcProduct.tbl_landmarks.Single(l=>l.landmark_id==landmark.LandmarkID);

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep(rsLandmark.landmark_cat_id, rsLandmark.destination_id, rsLandmark.title, rsLandmark.coor_lat, rsLandmark.coor_lon);
            //============================================================================================================================

            rsLandmark.landmark_cat_id=landmark.LandmarkCategoryID;
            rsLandmark.destination_id=landmark.DestinationID;
            rsLandmark.title = landmark.Title;
            rsLandmark.coor_lat = landmark.Latitude;
            rsLandmark.coor_lon = landmark.Longitude;
            try
            {
                dcProduct.SubmitChanges();
                int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
                //#Staff_Activity_Log================================================================================================ STEP 2 ============
                StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_detail, StaffLogActionType.Update, StaffLogSection.Product, ProductId, "tbl_landmark",
                    "landmark_cat_id,destination_id,title,coor_lat,coor_lon", arroldValue, "market_id", rsLandmark.landmark_id);
                //==================================================================================================================== COMPLETED ========
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}