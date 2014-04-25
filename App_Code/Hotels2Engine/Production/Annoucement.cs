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
/// Summary description for Annoucement
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class Annoucement : Hotels2BaseClass
    {
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();

        public int AnnoucementID { get; set; }
        public int ProductID { get; set; }
        public string Title { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool Status { get; set; }

        //public int ProductId
        //{
        //    get { return int.Parse(HttpContext.Current.Request.QueryString["pid"]); }
        //}
        
        public List<object> GetAnnoucementAll()
        {
            var result = from item in dcProduct.tbl_annoucements
                         select item;
            return MappingObjectFromDataContextCollection(result);
        }

        public List<object> getAnnounceMentListByProductId(int intProductId)
        {
            var Result = from pa in dcProduct.tbl_annoucements
                         where pa.product_id == intProductId 
                         orderby pa.status descending
                         select pa;
        return MappingObjectFromDataContextCollection(Result);
        }

        public List<object> getAnnounceMentListByProductIdFront(int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT  product_id,title,date_start,date_end,status FROM tbl_annoucement WHERE product_id=@product_id AND status=1 AND date_end >= GETDATE()", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> getAnnounceMentListByProductId(int intProductId, DateTime dDateStart , DateTime dDateEnd)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT  product_id,title,date_start,date_end,status FROM tbl_annoucement WHERE product_id=@product_id AND date_start=@date_start AND date_end=@date_end AND status=1", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cn.Open();
                
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
                

                
            }
        }

        public Annoucement getAnnouncementById(int intAnncId)
        {
            
            var Result = dcProduct.tbl_annoucements.SingleOrDefault(an => an.annoucement_id == intAnncId);
            if (Result == null)
                return null;
            else
            {
                return (Annoucement)MappingObjectFromDataContext(Result);
            }
        }

        public int Insert(Annoucement annouce)
        {
            tbl_annoucement annoucement = new tbl_annoucement
            {
                product_id = annouce.ProductID,
                title = annouce.Title,
                date_start = annouce.DateStart,
                date_end = annouce.DateEnd,
                status = annouce.Status
                
            };
            dcProduct.tbl_annoucements.InsertOnSubmit(annoucement);
            dcProduct.SubmitChanges();

            int intret = annoucement.annoucement_id;
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);

            //=== STAFF ACTIVITY ================================================================================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_announcement, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductId, "tbl_annoucement", "product_id,date_start,date_end,title,status", "annoucement_id", intret);
            //===================================================================================================================================================================================================  

            return intret;
        }

        public static int InsertAnnounceMentFirstStep(int intProductId, string strTitle)
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            
            var insert = new tbl_annoucement
            {
                 product_id = intProductId,
                 title = strTitle,
                 date_start = DateTime.Now,
                 date_end = DateTime.Now.AddDays(1),
                 status = true
            };
            dcProduct.tbl_annoucements.InsertOnSubmit(insert);
            dcProduct.SubmitChanges();

            int intret = insert.annoucement_id;
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);

            //=== STAFF ACTIVITY ================================================================================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_announcement, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductId, "tbl_annoucement", "product_id,date_start,date_end,title,status", "annoucement_id", intret);
            //===================================================================================================================================================================================================
            return intret;
        }

        public static bool UpdateDateRange(int intAnncId, DateTime dDateStart, DateTime dDateEnd)
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();

            //StaffActivity.ActionUpdateMethodStaff_log_FirstStep(dDateStart, dDateEnd);
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_annoucement", "date_start,date_end", "annoucement_id", intAnncId);
            //============================================================================================================================

            int Update = dcProduct.ExecuteCommand("UPDATE tbl_annoucement SET date_start ={0},date_end ={1} WHERE annoucement_id = {2}", dDateStart, dDateEnd, intAnncId);
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_announcement, StaffLogActionType.Update, StaffLogSection.Product, ProductId, "tbl_annoucement", "date_start,date_end", arroldValue, "annoucement_id", intAnncId);
            //==================================================================================================================== COMPLETED ========
            return (Update==1);
        }

        public static bool UpdateStatus(int intAnncId, bool bolStatus)
        {

            LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_annoucement", "status", "annoucement_id", intAnncId);
            //============================================================================================================================

            int Update = dcProduct.ExecuteCommand("UPDATE tbl_annoucement SET status = {0} WHERE annoucement_id = {1}", bolStatus, intAnncId);

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_announcement, StaffLogActionType.Update, StaffLogSection.Product, ProductId, "tbl_annoucement", "status", arroldValue, "annoucement_id", intAnncId);
            //==================================================================================================================== COMPLETED ========
            return (Update == 1);
        }

        public static bool UpdateTitle(int intAnncId, string strTitle)
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_annoucement", "title", "annoucement_id", intAnncId);
            //============================================================================================================================

            int Update = dcProduct.ExecuteCommand("UPDATE tbl_annoucement SET title = {0} WHERE annoucement_id = {1}", strTitle, intAnncId);

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_announcement, StaffLogActionType.Update, StaffLogSection.Product, ProductId, "tbl_annoucement", "title", arroldValue, "annoucement_id", intAnncId);
            //==================================================================================================================== COMPLETED ========
            return (Update == 1);
        }

        
        //========================= ANNOUNCEMENT CONTENT  ==================================
        public static ArrayList getAnnouncementcontentBYIdAndLangId(int intAnncId, byte bytLangId)
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            var Result = dcProduct.tbl_announcement_contents.SingleOrDefault(an => an.annoucement_id == intAnncId && an.lang_id == bytLangId);
            if (Result == null)
                return null;
            else
            {
                ArrayList arryItem = new ArrayList();
                arryItem.Add(Result.title);
                arryItem.Add(Result.detail);
                return arryItem;
            }
        }

        public static int InsertAnnouncementContent(int intAnncId, byte bytLangId, string strTitle, string Detail )
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            int Insert = dcProduct.ExecuteCommand("INSERT INTO tbl_announcement_content (annoucement_id,lang_id,title,detail) VALUES({0},{1},{2},{3})",intAnncId,bytLangId,strTitle,Detail);

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);

            //=== STAFF ACTIVITY ================================================================================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_announcement, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductId, "tbl_announcement_content", "annoucement_id,lang_id,title,detail", "annoucement_id,lang_id", intAnncId, bytLangId);
            //===================================================================================================================================================================================================
            return Insert;
        }

        public static bool UpdateAnnouncementContent(int intAnncId, byte bytLangId, string strTitle, string Detail)
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_announcement_content", "title,detail", "annoucement_id,lang_id", intAnncId, bytLangId);
            //============================================================================================================================

            var Update = dcProduct.ExecuteCommand("UPDATE tbl_announcement_content SET title={0},detail={1} WHERE annoucement_id={2} AND lang_id={3}", strTitle, Detail, intAnncId, bytLangId);

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_announcement, StaffLogActionType.Update, StaffLogSection.Product, ProductId, "tbl_announcement_content", "title,detail", arroldValue, "annoucement_id,lang_id", intAnncId, bytLangId);
            //==================================================================================================================== COMPLETED ========

            return (Update == 1);
        }
    }
}