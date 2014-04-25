using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.Staffs;
using Hotels2thailand.LinqProvider.Production;

/// <summary>
/// Summary description for ProductContent
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class LandmarkName : Hotels2BaseClass
    {
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();
        public int LandmarkID { get; set; }
        public byte LanguageID { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }

        public int Insert(LandmarkName landmark)
        {
            tbl_landmark_name landmarkName = new tbl_landmark_name
            {
                landmark_id = landmark.LandmarkID,
                lang_id = landmark.LanguageID,
                title = landmark.Title,
                detail = landmark.Detail
            };

            dcProduct.tbl_landmark_names.InsertOnSubmit(landmarkName);
            dcProduct.SubmitChanges();

            int intLandMarkId = landmarkName.landmark_id;

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_detail, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductId, "tbl_landmark_name", "landmark_id,lang_id,title,detail", "landmark_id,lang_id", landmark.LandmarkID, landmark.LanguageID);
            //========================================================================================================================================================

            return intLandMarkId;
        }

        public bool Update(LandmarkName landmark)
        {
            tbl_landmark_name rsLandmarkName = dcProduct.tbl_landmark_names.Single(ln=>ln.landmark_id==landmark.LandmarkID && ln.lang_id == landmark.LanguageID);
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep(rsLandmarkName.title, rsLandmarkName.detail);
            //============================================================================================================================
            
            rsLandmarkName.title = landmark.Title;
            rsLandmarkName.detail = landmark.Detail;
                try
                {
                    dcProduct.SubmitChanges();
                    int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
                    //#Staff_Activity_Log================================================================================================ STEP 2 ============
                    StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_detail, StaffLogActionType.Update, StaffLogSection.Product, ProductId, "tbl_landmark_name",
                        "title,detail", arroldValue, "landmark_id,lang_id", landmark.LandmarkID, landmark.LanguageID);
                    //==================================================================================================================== COMPLETED ========
                    return true;
                }
                catch
                {
                    return false;
                }
        }
        public List<object> GetLandMarkNameByProductID(int ProductID)
        {
            var result = from item in dcProduct.tbl_landmark_names
                         select item;
            return MappingObjectFromDataContextCollection(result);
        }

        public LandmarkName GetLandmarkByID(int landmarkID)
        {
            var result = from item in dcProduct.tbl_landmark_names
                         where item.landmark_id == landmarkID
                         select item;
            return (LandmarkName)MappingObjectFromDataContext(result);
        }
    }
}