using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Hotels2thailand.Staffs;
/// <summary>
/// Summary description for ReviewManage
/// </summary>
/// 
namespace Hotels2thailand.Reviews
{
    //public enum ListType : int
    //{
    //    Approve = 0,
    //    NotApprove = 1,
    //    Bin = 2
    //}

    public class ReviewManage:Hotels2BaseClass
    {

        public ReviewManage()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //private static string QuerybyProduct(int ProductId)
        //{
        //    StringBuilder query = new StringBuilder();
        //    query.Append("SELECT r.review_id, r.product_id, p.title As ProductTitle, r.cus_id, r.country_id, ISNULL((SELECT c.title FROM tbl_country c WHERE c.country_id=r.country_id),'Not Identify') AS CountryTitle, r.recommend_id, rc.title As Recommedtitle, r.from_id, rf.title As FromTitle, r.title,");
        //    query.Append(" r.cat_id, r.detail, ISNULL(r.positive,'not Identify'), ISNULL(r.negative,'not Identify'), r.full_name, r.review_from, ISNULL(r.rate_overall,0), ISNULL(r.rate_service,0), ISNULL(r.rate_location,0),");
        //    query.Append(" ISNULL(r.rate_room,0), ISNULL(r.rate_clean,0), ISNULL(r.rate_money,0), ISNULL(r.rate_fairway,0), ISNULL(r.rate_green,0), ISNULL(r.rate_difficult,0), ISNULL(r.rate_speed,0), ISNULL(r.rate_caddy,0), ISNULL(r.rate_clubhouse,0), ISNULL(r.rate_food,0), ISNULL(r.rate_performance,0), ISNULL(r.rate_punctuality,0),");
        //    query.Append(" ISNULL(r.rate_diagnose_ability,0), ISNULL(r.rate_pronunciation,0), ISNULL(r.rate_knowledge,0), r.vote_all, r.vote_usefull, r.date_travel, r.date_submit, r.status, r.status_bin");
        //    query.Append(" FROM tbl_review_all r, tbl_product p, tbl_review_recommend rc, tbl_review_from rf");
        //    query.Append(" WHERE r.product_id = p.product_id AND rc.recommend_id = r.recommend_id AND rf.from_id = r.from_id");
        //    query.Append(" AND r.status= 1 AND r.status_bin=1 AND r.cat_id=@cat_id AND r.product_id=@product_id");
        //    query.Append(" ORDER BY date_submit DESC");

        //    return query.ToString();
        //}

        public static string Query(bool IsFieldFull, byte bytCatId, bool SingleById, bool IsPaging, bool IsCount)
        {
            StringBuilder query = new StringBuilder();
                    if (IsPaging)
                    {
                        if (IsFieldFull)
                        {
                            query.Append("SELECT review_id, product_id, ProductTitle, file_name_main, file_name_review, cus_id, country_id, CountryTitle, recommend_id, Recommedtitle, from_id, FromTitle, title, cat_id,");
                            query.Append(" detail, positive, negative, full_name, review_from, rate_overall, rate_service, rate_location,");
                            query.Append(" rate_room, rate_clean, rate_money, rate_fairway, rate_green, rate_difficult, rate_speed, rate_caddy, rate_clubhouse,");
                            query.Append(" rate_food, rate_performance, rate_punctuality, rate_diagnose_ability, rate_pronunciation, rate_knowledge, vote_all, vote_usefull, date_travel, date_submit, status, status_bin FROM (");
                        }
                        else
                        {
                            query.Append("SELECT review_id,product_id, ProductTitle, title, cat_id, full_name, review_from,");
                            query.Append(" rate_overall, ");
                            query.Append(" date_submit FROM (");
                        }
                    }
                    
                    query.Append(" SELECT");
                    if (IsCount)
                        query.Append(" COUNT(review_id)");
                    else
                    {
                        if (SingleById)
                        {
                            query.Append(" r.review_id, r.product_id, p.title As ProductTitle, pc.file_name_main, pc.file_name_review, co.folder_destination, r.cus_id, r.country_id, ISNULL((SELECT c.title FROM tbl_country c WHERE c.country_id=r.country_id),'Not Identify') AS CountryTitle, r.recommend_id, rc.title As Recommedtitle, r.from_id, rf.title As FromTitle, r.title,");
                            query.Append(" r.cat_id, r.detail, ISNULL(r.positive,'not Identify'), ISNULL(r.negative,'not Identify'), r.full_name, r.review_from, ISNULL(r.rate_overall,0), ISNULL(r.rate_service,0), ISNULL(r.rate_location,0),");
                            query.Append(" ISNULL(r.rate_room,0), ISNULL(r.rate_clean,0), ISNULL(r.rate_money,0), ISNULL(r.rate_fairway,0), ISNULL(r.rate_green,0), ISNULL(r.rate_difficult,0), ISNULL(r.rate_speed,0), ISNULL(r.rate_caddy,0), ISNULL(r.rate_clubhouse,0), ISNULL(r.rate_food,0), ISNULL(r.rate_performance,0), ISNULL(r.rate_punctuality,0),");
                            query.Append(" ISNULL(r.rate_diagnose_ability,0), ISNULL(r.rate_pronunciation,0), ISNULL(r.rate_knowledge,0), r.vote_all, r.vote_usefull, r.date_travel, r.date_submit, r.status, r.status_bin");

                        }
                        else
                        {
                            if (IsFieldFull)
                            {
                                query.Append(" r.review_id, r.product_id, pc.title As ProductTitle, pc.file_name_main, pc.file_name_review, co.folder_destination, r.cus_id, r.country_id, ISNULL((SELECT c.title FROM tbl_country c WHERE c.country_id=r.country_id),'Not Identify') AS CountryTitle, r.recommend_id, rc.title As Recommedtitle, r.from_id, rf.title As FromTitle, r.title,");
                                query.Append(" r.cat_id, r.detail, ISNULL(r.positive,'not Identify'), ISNULL(r.negative,'not Identify'), r.full_name, r.review_from, ISNULL(r.rate_overall,0), ISNULL(r.rate_service,0), ISNULL(r.rate_location,0),");
                                query.Append(" ISNULL(r.rate_room,0), ISNULL(r.rate_clean,0), ISNULL(r.rate_money,0), ISNULL(r.rate_fairway,0), ISNULL(r.rate_green,0), ISNULL(r.rate_difficult,0), ISNULL(r.rate_speed,0), ISNULL(r.rate_caddy,0), ISNULL(r.rate_clubhouse,0), ISNULL(r.rate_food,0), ISNULL(r.rate_performance,0), ISNULL(r.rate_punctuality,0),");
                                query.Append(" ISNULL(r.rate_diagnose_ability,0), ISNULL(r.rate_pronunciation,0), ISNULL(r.rate_knowledge,0), r.vote_all, r.vote_usefull, r.date_travel, r.date_submit, r.status, r.status_bin");
                            }
                            else
                            {
                                query.Append(" r.review_id, r.product_id, p.title As ProductTitle, r.title, r.cat_id, r.cus_id, r.full_name, r.review_from,");
                                query.Append(" r.rate_overall,");
                                query.Append(" r.date_submit");
                            }
                         }
                    }
                    

                    if (IsPaging)
                    {
                        query.Append(", ROW_NUMBER()OVER(ORDER BY r.date_submit DESC ) AS RowNum ");
                    }

                    query.Append(" FROM tbl_review_all r, tbl_product p, tbl_product_content pc, tbl_destination co, tbl_review_recommend rc, tbl_review_from rf");
                    query.Append(" WHERE r.product_id = p.product_id AND rc.recommend_id = r.recommend_id AND rf.from_id = r.from_id");
                    query.Append(" AND p.product_id = pc.product_id AND pc.lang_id = 1 AND p.destination_id = co.destination_id");

                    if (!SingleById)
                    {
                        query.Append(" AND r.status= @status AND r.status_bin=@status_bin AND r.cat_id=@cat_id");
                    }

                    if (IsPaging)
                    {
                        query.Append(" ) AS Temp WHERE RowNum > @StartRowIndex AND RowNum <= (@StartRowIndex + @MaximumRows)");
                    }

                    if (SingleById)
                        query.Append(" AND review_id=@review_id");
                    if (!IsCount)
                        query.Append(" ORDER BY date_submit DESC");



                    
            return query.ToString();

        }

        
        private static object getObjectProductCat(byte bytCatId, bool IsFieldFull)
        {
            object Reivews = null;
            
                    if (IsFieldFull)
                        Reivews = new ProductReviews();
                    else
                        Reivews = new ProductReviewsShort();
          
            return Reivews;
        }

        public static int ReviewListTotal_BHTmanage(byte bytCatId, byte ListTpye)
        {
            bool Status = false;
            bool StatusBin = false;

            switch (ListTpye)
            {
                //Approved
                case 0:
                    Status = true;
                    StatusBin = true;
                    break;
                //New&Not Approve
                case 1:
                    Status = false;
                    StatusBin = true;
                    break;
                //Bin
                case 2:
                    Status = false;
                    StatusBin = false;
                    break;

            }
            ReviewManage ReviewsManages = new ReviewManage();
            
            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(review_id) FROM tbl_review_all r, tbl_product p, tbl_product_content pc, tbl_destination co, tbl_review_recommend rc, tbl_review_from rf,tbl_product_booking_engine pen WHERE r.product_id = p.product_id AND rc.recommend_id = r.recommend_id AND rf.from_id = r.from_id AND p.product_id=pen.product_id AND p.product_id = pc.product_id AND pc.lang_id = 1 AND p.destination_id = co.destination_id AND pen.manage_id=2 AND r.status= @status AND r.status_bin=@status_bin AND r.cat_id=@cat_id", cn);
               

                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = Status;
                cmd.Parameters.Add("@status_bin", SqlDbType.Bit).Value = StatusBin;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cn.Open();
                int ret  = (int)ReviewsManages.ExecuteScalar(cmd);
                return ret;
            }
        }
        public static int ReviewListTotal_HotelManage(byte bytCatId, byte ListTpye, int intProductId)
        {
            bool Status = false;
            bool StatusBin = false;

            switch (ListTpye)
            {
                //Approved
                case 0:
                    Status = true;
                    StatusBin = true;
                    break;
                //New&Not Approve
                case 1:
                    Status = false;
                    StatusBin = true;
                    break;
                //Bin
                case 2:
                    Status = false;
                    StatusBin = false;
                    break;

            }
            ReviewManage ReviewsManages = new ReviewManage();

            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("SELECT COUNT(review_id) FROM tbl_review_all r, tbl_product p, tbl_product_content pc, tbl_destination co, tbl_review_recommend rc, tbl_review_from rf WHERE r.product_id = p.product_id AND rc.recommend_id = r.recommend_id AND rf.from_id = r.from_id AND p.product_id = pc.product_id AND pc.lang_id = 1 AND p.destination_id = co.destination_id AND r.status= @status AND r.status_bin=@status_bin AND r.cat_id=@cat_id AND r.product_id=@product_id", cn);
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = Status;
                cmd.Parameters.Add("@status_bin", SqlDbType.Bit).Value = StatusBin;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                int ret = (int)ReviewsManages.ExecuteScalar(cmd);
                return ret;
            }
        }
        public static IList<object> GetReivewListBhtManage(bool IsFieldFull, byte bytCatId, byte ListTpye, bool IsPaging, int StartRowIndex, Nullable<byte> PageSize)
        {
            bool Status = false;
            bool StatusBin = false;

            switch (ListTpye)
            {
                //Approved
                case 0:
                    Status = true;
                    StatusBin = true;
                    break;
                //New&Not Approve
                case 1:
                    Status = false;
                    StatusBin = true;
                    break;
                //Bin
                case 2:
                    Status = false;
                    StatusBin = false;
                    break;

            }

            ReviewManage ReviewsManages = new ReviewManage();


            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {


                SqlCommand cmd = new SqlCommand("SELECT review_id,product_id, ProductTitle, title, cat_id, full_name, review_from, rate_overall, date_submit FROM ( SELECT r.review_id, r.product_id, p.title As ProductTitle, r.title, r.cat_id, r.cus_id, r.full_name, r.review_from, r.rate_overall, r.date_submit, ROW_NUMBER()OVER(ORDER BY r.date_submit DESC ) AS RowNum FROM tbl_review_all r, tbl_product p, tbl_product_content pc, tbl_destination co, tbl_review_recommend rc, tbl_review_from rf , tbl_product_booking_engine pen WHERE r.product_id = p.product_id AND rc.recommend_id = r.recommend_id AND rf.from_id = r.from_id AND p.product_id = pc.product_id AND pc.lang_id = 1 AND p.destination_id = co.destination_id AND r.status= @status AND r.status_bin=@status_bin AND r.cat_id=@cat_id  AND r.product_id = pen.product_id AND pen.manage_id=2) AS Temp WHERE RowNum > @StartRowIndex AND RowNum <= (@StartRowIndex + @MaximumRows) ORDER BY date_submit DESC", cn);
                //HttpContext.Current.Response.Write(ReviewManage.Query(IsFieldFull, bytCatId, false, IsPaging, false));
                //HttpContext.Current.Response.End();
                //cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = Status;
                cmd.Parameters.Add("@status_bin", SqlDbType.Bit).Value = StatusBin;
                if (PageSize != null && IsPaging == true)
                {
                    cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = StartRowIndex;
                    cmd.Parameters.Add("@MaximumRows", SqlDbType.Int).Value = (byte)PageSize;
                }
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cn.Open();

                return ReviewsManages.MappingObjectCollectionFromDataReader(ReviewsManages.ExecuteReader(cmd), ReviewManage.getObjectProductCat(bytCatId, IsFieldFull));
            }
        }

        public static IList<object> GetReivewListHotelManage(bool IsFieldFull, byte bytCatId, byte ListTpye, bool IsPaging, int StartRowIndex, Nullable<byte> PageSize, int intProductId)
        {
            bool Status = false;
            bool StatusBin = false;

            switch (ListTpye)
            {
                    //Approved
                case 0:
                    Status = true;
                    StatusBin = true;
                    break;
                    //New&Not Approve
                case 1:
                    Status = false;
                    StatusBin = true;
                    break;
                    //Bin
                case 2:
                    Status = false;
                    StatusBin = false;
                    break;

            }

             ReviewManage ReviewsManages = new ReviewManage();

             
             using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
             {


                 SqlCommand cmd = new SqlCommand("SELECT review_id,product_id, ProductTitle, title, cat_id, full_name, review_from, rate_overall, date_submit FROM ( SELECT r.review_id, r.product_id, p.title As ProductTitle, r.title, r.cat_id, r.cus_id, r.full_name, r.review_from, r.rate_overall, r.date_submit, ROW_NUMBER()OVER(ORDER BY r.date_submit DESC ) AS RowNum FROM tbl_review_all r, tbl_product p, tbl_product_content pc, tbl_destination co, tbl_review_recommend rc, tbl_review_from rf WHERE r.product_id = p.product_id AND rc.recommend_id = r.recommend_id AND rf.from_id = r.from_id AND p.product_id = pc.product_id AND pc.lang_id = 1 AND p.destination_id = co.destination_id AND r.status= @status AND r.status_bin=@status_bin AND r.cat_id=@cat_id  AND r.product_id = @product_id) AS Temp WHERE RowNum > @StartRowIndex AND RowNum <= (@StartRowIndex + @MaximumRows) ORDER BY date_submit DESC", cn);
                 //HttpContext.Current.Response.Write(ReviewManage.Query(IsFieldFull, bytCatId, false, IsPaging, false));
                 //HttpContext.Current.Response.End();
                 cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                 cmd.Parameters.Add("@status", SqlDbType.Bit).Value = Status;
                 cmd.Parameters.Add("@status_bin", SqlDbType.Bit).Value = StatusBin;
                 if (PageSize != null && IsPaging == true)
                 {
                     cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = StartRowIndex;
                     cmd.Parameters.Add("@MaximumRows", SqlDbType.Int).Value = (byte)PageSize;
                 }
                 cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                 cn.Open();

                 return ReviewsManages.MappingObjectCollectionFromDataReader(ReviewsManages.ExecuteReader(cmd), ReviewManage.getObjectProductCat(bytCatId, IsFieldFull));
             }
         }



        public static ProductReviews GetreviewById(int intReviewId)
        {
            ReviewManage ReviewsManages = new ReviewManage();
            object obj = new ProductReviews();
            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(ReviewManage.Query(true,29,true,false,false), cn);
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intReviewId;
                cn.Open();
                IDataReader reader = ReviewsManages.ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return (ProductReviews)ReviewsManages.MappingObjectFromDataReader(reader, obj);
                }
                else
                {
                    return null;
                }
                
            }
        }




        //================= Update STatus Review 
        //Update Version 1;
        public static bool HotelsReviewUpdateStatus(int intReviewId, bool Status, bool StatusBin)
        {
            int ProductId = ReviewManage.GetreviewById(intReviewId).ProductId;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_review_all", "status,status_bin", "review_id", intReviewId);
            //============================================================================================================================
            ReviewManage ReviewsManages = new ReviewManage();
            object obj = new ProductReviews();
            int ret = 0;
            
            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_review_all SET status=@status , status_bin=@status_bin WHERE review_id=@review_id", cn);
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = Status;
                cmd.Parameters.Add("@status_bin", SqlDbType.Bit).Value = StatusBin;
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intReviewId;
                cn.Open();
                ret = ReviewsManages.ExecuteNonQuery(cmd);
                
            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Review, StaffLogActionType.Update, StaffLogSection.F_Review, ProductId, "tbl_review_all", "status,status_bin", arroldValue, "review_id", intReviewId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }
        //Update Version 2;
        //public static bool HotelsReviewUpdateStatus(int intReviewId, bool Status, bool StatusBin)
        //{
        //    ReviewManage ReviewsManages = new ReviewManage();
        //    object obj = new ProductReviews();
        //    using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("UPDATE tbl_review_all SET status=@status , status_bin=@status_bin WHERE review_id=@review_id", cn);
        //        cmd.Parameters.Add("@status", SqlDbType.Bit).Value = Status;
        //        cmd.Parameters.Add("@status_bin", SqlDbType.Bit).Value = StatusBin;
        //        cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intReviewId;
        //        cn.Open();
        //        int ret = ReviewsManages.ExecuteNonQuery(cmd);
        //        return (ret == 1);
        //    }
        //}


        public static int HotelREviewInsert(int intProduct_id, int? intCudId, byte? bytCountryId, byte? bytRecMId, byte? bytFromId, string strtitle,
            string strDetail, string strPositive, string strNagative, string strFullName, string strReviewFrom, byte bytRateOverAll,
            byte bytRateService, byte bytRateLocation, byte bytRateRoom, byte bytRateClean, byte bytRateMoney)
        {
            ReviewManage ReviewsManages = new ReviewManage();
            StringBuilder query = new StringBuilder();

            query.Append("INSERT INTO tbl_review_all (product_id,cus_id,country_id,recommend_id,from_id,cat_id,title,detail,positive,negative,full_name,review_from,rate_overall,");

            query.Append(" rate_service,rate_location,rate_room,rate_clean,rate_money,status,status_bin,date_submit) VALUES (");

            query.Append(" @product_id,@cus_id,@country_id,@recommend_id,@from_id,'29',@title,@detail,@positive,@negative,@full_name,@review_from,@rate_overall,");

            query.Append(" @rate_service,@rate_location,@rate_room,@rate_clean,@rate_money,'0','1',@date_submit)");
            //HttpContext.Current.Response.Write("HELLO");
            //HttpContext.Current.Response.End();
            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct_id;
                if (intCudId.HasValue)
                    cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = (int)intCudId;
                else
                    cmd.Parameters.AddWithValue("@cus_id", DBNull.Value);

                if (bytCountryId.HasValue)
                    cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = (byte)bytCountryId;
                else
                    cmd.Parameters.AddWithValue("@country_id", DBNull.Value);

                //HttpContext.Current.Response.Write(bytCountryId);
                //HttpContext.Current.Response.End();

                if (bytRecMId.HasValue)
                    cmd.Parameters.Add("@recommend_id", SqlDbType.TinyInt).Value = (byte)bytRecMId;
                else
                    cmd.Parameters.AddWithValue("@recommend_id", DBNull.Value);

                if (bytFromId.HasValue)
                    cmd.Parameters.Add("@from_id", SqlDbType.TinyInt).Value = (byte)bytFromId;
                else
                    cmd.Parameters.AddWithValue("@from_id", DBNull.Value);



                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strtitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cmd.Parameters.Add("@positive", SqlDbType.NVarChar).Value = strPositive;
                cmd.Parameters.Add("@negative", SqlDbType.NVarChar).Value = strNagative;
                cmd.Parameters.Add("@full_name", SqlDbType.NVarChar).Value = strFullName;
                cmd.Parameters.Add("@review_from", SqlDbType.NVarChar).Value = strReviewFrom;
                cmd.Parameters.Add("@rate_overall", SqlDbType.TinyInt).Value = bytRateOverAll;

                cmd.Parameters.Add("@rate_service", SqlDbType.TinyInt).Value = bytRateService;
                cmd.Parameters.Add("@rate_location", SqlDbType.TinyInt).Value = bytRateLocation;
                cmd.Parameters.Add("@rate_room", SqlDbType.TinyInt).Value = bytRateRoom;
                cmd.Parameters.Add("@rate_clean", SqlDbType.TinyInt).Value = bytRateClean;
                cmd.Parameters.Add("@rate_money", SqlDbType.TinyInt).Value = bytRateMoney;
                cmd.Parameters.Add("@date_submit", SqlDbType.DateTime).Value = DateTime.Now.Hotels2ThaiDateTime();

                cn.Open();
                int ret = ReviewsManages.ExecuteNonQuery(cmd);
                return ret;
            }
        }



        public static bool HotelReviewUpdate(int intReviewId, byte bytCountryId, byte bytRecMId, byte bytFromId, string strtitle,
            string strDetail, string strPositive, string strNagative, string strFullName, string strReviewFrom, byte bytRateOverAll,
            byte bytRateService, byte bytRateLocation, byte bytRateRoom, byte bytRateClean, byte bytRateMoney)
        {
            StringBuilder str = new StringBuilder();
            str.Append("country_id,recommend_id,from_id,title,detail,positive,");
            str.Append("negative,full_name,review_from,rate_overall,rate_service,rate_location,");
            str.Append("rate_room,rate_clean,rate_money");

            int ProductId = ReviewManage.GetreviewById(intReviewId).ProductId;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_review_all", str.ToString(), "review_id", intReviewId);
            //============================================================================================================================
            int ret= 0;
            ReviewManage ReviewsManages = new ReviewManage();
            object obj = new ProductReviews();
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE tbl_review_all SET country_id=@country_id , recommend_id=@recommend_id,  from_id=@from_id, title=@title,");
            query.Append(" detail=@detail, positive=@positive, negative=@negative, full_name=@full_name, review_from=@review_from, rate_overall=@rate_overall,");
            query.Append(" rate_service=@rate_service, rate_location=@rate_location, rate_room=@rate_room, rate_clean=@rate_clean, rate_money=@rate_money");
            query.Append(" WHERE review_id=@review_id");
            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intReviewId;

                if (bytCountryId == 0)
                    cmd.Parameters.AddWithValue("@country_id", DBNull.Value);
                    
                else
                    cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = bytCountryId;

                cmd.Parameters.Add("@recommend_id", SqlDbType.TinyInt).Value = bytRecMId;
                cmd.Parameters.Add("@from_id", SqlDbType.TinyInt).Value = bytFromId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strtitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cmd.Parameters.Add("@positive", SqlDbType.NVarChar).Value = strPositive;
                cmd.Parameters.Add("@negative", SqlDbType.NVarChar).Value = strNagative;
                cmd.Parameters.Add("@full_name", SqlDbType.NVarChar).Value = strFullName;
                cmd.Parameters.Add("@review_from", SqlDbType.NVarChar).Value = strReviewFrom;
                cmd.Parameters.Add("@rate_overall", SqlDbType.TinyInt).Value = bytRateOverAll;
                cmd.Parameters.Add("@rate_service", SqlDbType.TinyInt).Value = bytRateService;
                cmd.Parameters.Add("@rate_location", SqlDbType.TinyInt).Value = bytRateLocation;
                cmd.Parameters.Add("@rate_room", SqlDbType.TinyInt).Value = bytRateRoom;
                cmd.Parameters.Add("@rate_clean", SqlDbType.TinyInt).Value = bytRateClean;
                cmd.Parameters.Add("@rate_money", SqlDbType.TinyInt).Value = bytRateMoney;
                
                
                cn.Open();
                 ret = ReviewsManages.ExecuteNonQuery(cmd);
                
            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Review, StaffLogActionType.Update, StaffLogSection.F_Review, ProductId, "tbl_review_all", str.ToString(), arroldValue, "review_id", intReviewId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        //===========================================================================
        public static int GolfREviewInsert(int intProduct_id, int? intCudId, byte? bytCountryId, byte? bytRecMId, byte? bytFromId, string strtitle,
            string strDetail, string strPositive, string strNagative, string strFullName, string strReviewFrom, byte bytRateOverAll,
            byte bytFairWay, byte bytGreen, byte bytDifficult, byte bytSpeed, byte bytCaddy, byte bytClub, byte bytFood, byte bytmoney)
        {
            ReviewManage ReviewsManages = new ReviewManage();
            StringBuilder query = new StringBuilder();

            query.Append("INSERT INTO tbl_review_all (product_id,cus_id,country_id,recommend_id,from_id,cat_id,title,detail,positive,negative,full_name,review_from,rate_overall,");

            query.Append(" rate_fairway,rate_green,rate_difficult,rate_speed,rate_caddy,rate_clubhouse,rate_food,rate_money,status,status_bin,date_submit) VALUES (");

            query.Append(" @product_id,@cus_id,@country_id,@recommend_id,@from_id,'32',@title,@detail,@positive,@negative,@full_name,@review_from,@rate_overall,");

            query.Append(" @rate_fairway,@rate_green,@rate_difficult,@rate_speed,@rate_caddy,@rate_clubhouse,@rate_food,@rate_money,'0','1',@date_submit)");
            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct_id;
                if (intCudId.HasValue)
                    cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = (int)intCudId;
                else
                    cmd.Parameters.AddWithValue("@cus_id", DBNull.Value);

                if (bytCountryId.HasValue)
                    cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = (byte)bytCountryId;
                else
                    cmd.Parameters.AddWithValue("@country_id", DBNull.Value);

                if (bytRecMId.HasValue)
                    cmd.Parameters.Add("@recommend_id", SqlDbType.TinyInt).Value = (byte)bytRecMId;
                else
                    cmd.Parameters.AddWithValue("@recommend_id", DBNull.Value);

                if (bytFromId.HasValue)
                    cmd.Parameters.Add("@from_id", SqlDbType.TinyInt).Value = (byte)bytFromId;
                else
                    cmd.Parameters.AddWithValue("@from_id", DBNull.Value);

                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strtitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cmd.Parameters.Add("@positive", SqlDbType.NVarChar).Value = strPositive;
                cmd.Parameters.Add("@negative", SqlDbType.NVarChar).Value = strNagative;
                cmd.Parameters.Add("@full_name", SqlDbType.NVarChar).Value = strFullName;
                cmd.Parameters.Add("@review_from", SqlDbType.NVarChar).Value = strReviewFrom;
                cmd.Parameters.Add("@rate_overall", SqlDbType.TinyInt).Value = bytRateOverAll;

                cmd.Parameters.Add("@rate_fairway", SqlDbType.TinyInt).Value = bytFairWay;
                cmd.Parameters.Add("@rate_green", SqlDbType.TinyInt).Value = bytGreen;
                cmd.Parameters.Add("@rate_difficult", SqlDbType.TinyInt).Value = bytDifficult;
                cmd.Parameters.Add("@rate_speed", SqlDbType.TinyInt).Value = bytSpeed;
                cmd.Parameters.Add("@rate_caddy", SqlDbType.TinyInt).Value = bytCaddy;
                cmd.Parameters.Add("@rate_clubhouse", SqlDbType.TinyInt).Value = bytClub;
                cmd.Parameters.Add("@rate_food", SqlDbType.TinyInt).Value = bytFood;
                cmd.Parameters.Add("@rate_money", SqlDbType.TinyInt).Value = bytmoney;
                cmd.Parameters.Add("@date_submit", SqlDbType.DateTime).Value = DateTime.Now.Hotels2ThaiDateTime();

                cn.Open();
                int ret = ReviewsManages.ExecuteNonQuery(cmd);
                return ret;
            }
        }


        public static bool GolfReviewUpdate(int intReviewId, byte bytCountryId, byte bytRecMId, byte bytFromId, string strtitle,
            string strDetail, string strPositive, string strNagative, string strFullName, string strReviewFrom, byte bytRateOverAll, byte bytFairWay, byte bytGreen,
            byte bytDifficult, byte bytSpeed, byte bytCaddy, byte bytClub, byte bytFood, byte bytmoney)
        {
            StringBuilder str = new StringBuilder();
            str.Append("country_id,recommend_id,from_id,title,detail,positive,");
            str.Append("negative,full_name,review_from,rate_overall,");
            str.Append("rate_fairway,rate_green,rate_difficult,rate_speed,rate_caddy,rate_clubhouse,rate_food,rate_money");

            int ProductId = ReviewManage.GetreviewById(intReviewId).ProductId;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_review_all", str.ToString(), "review_id", intReviewId);
            //============================================================================================================================
            int ret = 0;

            ReviewManage ReviewsManages = new ReviewManage();
            object obj = new ProductReviews();
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE tbl_review_all SET country_id=@country_id , recommend_id=@recommend_id,  from_id=@from_id, title=@title,");
            query.Append(" detail=@detail, positive=@positive, negative=@negative, full_name=@full_name, review_from=@review_from, rate_overall=@rate_overall,");

            query.Append(" rate_fairway=@rate_fairway, rate_green=@rate_green, rate_difficult=@rate_difficult,");
            query.Append(" rate_speed=@rate_speed, rate_caddy=@rate_caddy, rate_clubhouse=@rate_clubhouse,");
            query.Append(" rate_food=@rate_food, rate_money=@rate_money");

            query.Append(" WHERE review_id=@review_id");
            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intReviewId;
                if (bytCountryId == 0)
                    cmd.Parameters.AddWithValue("@country_id", DBNull.Value);

                else
                    cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = bytCountryId;
                cmd.Parameters.Add("@recommend_id", SqlDbType.TinyInt).Value = bytRecMId;
                cmd.Parameters.Add("@from_id", SqlDbType.TinyInt).Value = bytFromId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strtitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cmd.Parameters.Add("@positive", SqlDbType.NVarChar).Value = strPositive;
                cmd.Parameters.Add("@negative", SqlDbType.NVarChar).Value = strNagative;
                cmd.Parameters.Add("@full_name", SqlDbType.NVarChar).Value = strFullName;
                cmd.Parameters.Add("@review_from", SqlDbType.NVarChar).Value = strReviewFrom;
                cmd.Parameters.Add("@rate_overall", SqlDbType.TinyInt).Value = bytRateOverAll;

                cmd.Parameters.Add("@rate_fairway", SqlDbType.TinyInt).Value = bytFairWay;
                cmd.Parameters.Add("@rate_green", SqlDbType.TinyInt).Value = bytGreen;
                cmd.Parameters.Add("@rate_difficult", SqlDbType.TinyInt).Value = bytDifficult;
                cmd.Parameters.Add("@rate_speed", SqlDbType.TinyInt).Value = bytSpeed;
                cmd.Parameters.Add("@rate_caddy", SqlDbType.TinyInt).Value = bytCaddy;
                cmd.Parameters.Add("@rate_clubhouse", SqlDbType.TinyInt).Value = bytClub;
                cmd.Parameters.Add("@rate_food", SqlDbType.TinyInt).Value = bytFood;
                cmd.Parameters.Add("@rate_money", SqlDbType.TinyInt).Value = bytmoney;

                cn.Open();
                ret = ReviewsManages.ExecuteNonQuery(cmd);
                
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Review, StaffLogActionType.Update, StaffLogSection.F_Review, ProductId, "tbl_review_all", str.ToString(), arroldValue, "review_id", intReviewId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        //===========================================================================

        public static int WaterREviewInsert(int intProduct_id, int? intCudId, byte? bytCountryId, byte? bytRecMId, byte? bytFromId, string strtitle,
            string strDetail, string strPositive, string strNagative, string strFullName, string strReviewFrom, byte bytRateOverAll,
            byte bytKnowledge, byte bytService, byte bytPron, byte Punci, byte bytMoney, byte bytfood)
        {
            ReviewManage ReviewsManages = new ReviewManage();
            StringBuilder query = new StringBuilder();

            query.Append("INSERT INTO tbl_review_all (product_id,cus_id,country_id,recommend_id,from_id,cat_id,title,detail,positive,negative,full_name,review_from,rate_overall,");

            query.Append(" rate_knowledge,rate_service,rate_pronunciation,rate_punctuality,rate_money,rate_food,status,status_bin,date_submit) VALUES (");

            query.Append(" @product_id,@cus_id,@country_id,@recommend_id,@from_id,'36',@title,@detail,@positive,@negative,@full_name,@review_from,@rate_overall,");

            query.Append(" @rate_knowledge,@rate_service,@rate_pronunciation,@rate_punctuality,@rate_money,@rate_food,'0','1',@date_submit)");
            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct_id;
                if (intCudId.HasValue)
                    cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = (int)intCudId;
                else
                    cmd.Parameters.AddWithValue("@cus_id", DBNull.Value);

                if (bytCountryId.HasValue)
                    cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = (byte)bytCountryId;
                else
                    cmd.Parameters.AddWithValue("@country_id", DBNull.Value);

                if (bytRecMId.HasValue)
                    cmd.Parameters.Add("@recommend_id", SqlDbType.TinyInt).Value = (byte)bytRecMId;
                else
                    cmd.Parameters.AddWithValue("@recommend_id", DBNull.Value);

                if (bytFromId.HasValue)
                    cmd.Parameters.Add("@from_id", SqlDbType.TinyInt).Value = (byte)bytFromId;
                else
                    cmd.Parameters.AddWithValue("@from_id", null);

                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strtitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cmd.Parameters.Add("@positive", SqlDbType.NVarChar).Value = strPositive;
                cmd.Parameters.Add("@negative", SqlDbType.NVarChar).Value = strNagative;
                cmd.Parameters.Add("@full_name", SqlDbType.NVarChar).Value = strFullName;
                cmd.Parameters.Add("@review_from", SqlDbType.NVarChar).Value = strReviewFrom;
                cmd.Parameters.Add("@rate_overall", SqlDbType.TinyInt).Value = bytRateOverAll;

                cmd.Parameters.Add("@rate_knowledge", SqlDbType.TinyInt).Value = bytKnowledge;
                cmd.Parameters.Add("@rate_service", SqlDbType.TinyInt).Value = bytService;
                cmd.Parameters.Add("@rate_pronunciation", SqlDbType.TinyInt).Value = bytPron;
                cmd.Parameters.Add("@rate_punctuality", SqlDbType.TinyInt).Value = Punci;
                cmd.Parameters.Add("@rate_money", SqlDbType.TinyInt).Value = bytMoney;
                cmd.Parameters.Add("@rate_food", SqlDbType.TinyInt).Value = bytfood;
                cmd.Parameters.Add("@date_submit", SqlDbType.DateTime).Value = DateTime.Now.Hotels2ThaiDateTime();

                cn.Open();
                int ret = ReviewsManages.ExecuteNonQuery(cmd);
                return ret;
            }
        }


        public static bool WaterReviewUpdate(int intReviewId, byte bytCountryId, byte bytRecMId, byte bytFromId, string strtitle,
            string strDetail, string strPositive, string strNagative, string strFullName, string strReviewFrom, byte bytRateOverAll,
            byte bytKnowledge, byte bytService, byte bytPron, byte Punci, byte bytMoney, byte bytfood)
        {
            StringBuilder str = new StringBuilder();
            str.Append("country_id,recommend_id,from_id,title,detail,positive,");
            str.Append("negative,full_name,review_from,rate_overall,");
            str.Append("rate_knowledge,rate_service,rate_pronunciation,rate_punctuality,rate_money,rate_food");

            int ProductId = ReviewManage.GetreviewById(intReviewId).ProductId;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_review_all", str.ToString(), "review_id", intReviewId);
            //============================================================================================================================
            int ret = 0;

            ReviewManage ReviewsManages = new ReviewManage();
            object obj = new ProductReviews();
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE tbl_review_all SET country_id=@country_id , recommend_id=@recommend_id,  from_id=@from_id, title=@title,");
            query.Append(" detail=@detail, positive=@positive, negative=@negative, full_name=@full_name, review_from=@review_from, rate_overall=@rate_overall,");

            query.Append(" rate_knowledge=@rate_knowledge, rate_service=@rate_service, rate_pronunciation=@rate_pronunciation,");
            query.Append(" rate_punctuality=@rate_punctuality, rate_money=@rate_money, rate_food=@rate_food");
            //query.Append(" rate_food=@rate_food,rate_money@rate_money");

            query.Append(" WHERE review_id=@review_id");
            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intReviewId;
                if (bytCountryId == 0)
                    cmd.Parameters.AddWithValue("@country_id", DBNull.Value);

                else
                    cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = bytCountryId;
                cmd.Parameters.Add("@recommend_id", SqlDbType.TinyInt).Value = bytRecMId;
                cmd.Parameters.Add("@from_id", SqlDbType.TinyInt).Value = bytFromId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strtitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cmd.Parameters.Add("@positive", SqlDbType.NVarChar).Value = strPositive;
                cmd.Parameters.Add("@negative", SqlDbType.NVarChar).Value = strNagative;
                cmd.Parameters.Add("@full_name", SqlDbType.NVarChar).Value = strFullName;
                cmd.Parameters.Add("@review_from", SqlDbType.NVarChar).Value = strReviewFrom;
                cmd.Parameters.Add("@rate_overall", SqlDbType.TinyInt).Value = bytRateOverAll;

                cmd.Parameters.Add("@rate_knowledge", SqlDbType.TinyInt).Value = bytKnowledge;
                cmd.Parameters.Add("@rate_service", SqlDbType.TinyInt).Value = bytService;
                cmd.Parameters.Add("@rate_pronunciation", SqlDbType.TinyInt).Value = bytPron;
                cmd.Parameters.Add("@rate_punctuality", SqlDbType.TinyInt).Value = Punci;
                cmd.Parameters.Add("@rate_money", SqlDbType.TinyInt).Value = bytMoney;
                cmd.Parameters.Add("@rate_food", SqlDbType.TinyInt).Value = bytfood;
                

                cn.Open();
                ret = ReviewsManages.ExecuteNonQuery(cmd);
                
            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Review, StaffLogActionType.Update, StaffLogSection.F_Review, ProductId, "tbl_review_all", str.ToString(), arroldValue, "review_id", intReviewId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        //===========================================================================
        public static int DayTripsREviewInsert(int intProduct_id, int? intCudId, byte? bytCountryId, byte? bytRecMId, byte? bytFromId, string strtitle,
            string strDetail, string strPositive, string strNagative, string strFullName, string strReviewFrom, byte bytRateOverAll,
            byte bytKnowledge, byte bytService, byte bytPron, byte Punci, byte bytMoney, byte bytfood)
        {
            ReviewManage ReviewsManages = new ReviewManage();
            StringBuilder query = new StringBuilder();

            query.Append("INSERT INTO tbl_review_all (product_id,cus_id,country_id,recommend_id,from_id,cat_id,title,detail,positive,negative,full_name,review_from,rate_overall,");

            query.Append(" rate_knowledge,rate_service,rate_pronunciation,rate_punctuality,rate_money,rate_food,status,status_bin,date_submit) VALUES (");

            query.Append(" @product_id,@cus_id,@country_id,@recommend_id,@from_id,'34',@title,@detail,@positive,@negative,@full_name,@review_from,@rate_overall,");

            query.Append(" @rate_knowledge,@rate_service,@rate_pronunciation,@rate_punctuality,@rate_money,@rate_food,'0','1',@date_submit)");
            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct_id;
                if (intCudId.HasValue)
                    cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = (int)intCudId;
                else
                    cmd.Parameters.AddWithValue("@cus_id", DBNull.Value);

                if (bytCountryId.HasValue)
                    cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = (byte)bytCountryId;
                else
                    cmd.Parameters.AddWithValue("@country_id", DBNull.Value);

                if (bytRecMId.HasValue)
                    cmd.Parameters.Add("@recommend_id", SqlDbType.TinyInt).Value = (byte)bytRecMId;
                else
                    cmd.Parameters.AddWithValue("@recommend_id", DBNull.Value);

                if (bytFromId.HasValue)
                    cmd.Parameters.Add("@from_id", SqlDbType.TinyInt).Value = (byte)bytFromId;
                else
                    cmd.Parameters.AddWithValue("@from_id", null);

                

                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strtitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cmd.Parameters.Add("@positive", SqlDbType.NVarChar).Value = strPositive;
                cmd.Parameters.Add("@negative", SqlDbType.NVarChar).Value = strNagative;
                cmd.Parameters.Add("@full_name", SqlDbType.NVarChar).Value = strFullName;
                cmd.Parameters.Add("@review_from", SqlDbType.NVarChar).Value = strReviewFrom;
                cmd.Parameters.Add("@rate_overall", SqlDbType.TinyInt).Value = bytRateOverAll;

                cmd.Parameters.Add("@rate_knowledge", SqlDbType.TinyInt).Value = bytKnowledge;
                cmd.Parameters.Add("@rate_service", SqlDbType.TinyInt).Value = bytService;
                cmd.Parameters.Add("@rate_pronunciation", SqlDbType.TinyInt).Value = bytPron;
                cmd.Parameters.Add("@rate_punctuality", SqlDbType.TinyInt).Value = Punci;
                cmd.Parameters.Add("@rate_money", SqlDbType.TinyInt).Value = bytMoney;
                cmd.Parameters.Add("@rate_food", SqlDbType.TinyInt).Value = bytfood;
                cmd.Parameters.Add("@date_submit", SqlDbType.DateTime).Value = DateTime.Now.Hotels2ThaiDateTime();

                cn.Open();
                int ret = ReviewsManages.ExecuteNonQuery(cmd);
                return ret;
            }
        }


        public static bool DayTripsReviewUpdate(int intReviewId, byte bytCountryId, byte bytRecMId, byte bytFromId, string strtitle,
            string strDetail, string strPositive, string strNagative, string strFullName, string strReviewFrom, byte bytRateOverAll,
            byte bytKnowledge, byte bytService, byte bytPron, byte Punci, byte bytMoney, byte bytfood)
        {

            StringBuilder str = new StringBuilder();
            str.Append("country_id,recommend_id,from_id,title,detail,positive,");
            str.Append("negative,full_name,review_from,rate_overall,");
            str.Append("rate_knowledge,rate_service,rate_pronunciation,rate_punctuality,rate_money,rate_food");

            int ProductId = ReviewManage.GetreviewById(intReviewId).ProductId;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_review_all", str.ToString(), "review_id", intReviewId);
            //============================================================================================================================
            int ret = 0;

            ReviewManage ReviewsManages = new ReviewManage();
            object obj = new ProductReviews();
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE tbl_review_all SET country_id=@country_id , recommend_id=@recommend_id,  from_id=@from_id, title=@title,");
            query.Append(" detail=@detail, positive=@positive, negative=@negative, full_name=@full_name, review_from=@review_from, rate_overall=@rate_overall,");

            query.Append(" rate_knowledge=@rate_knowledge, rate_service=@rate_service, rate_pronunciation=@rate_pronunciation,");
            query.Append(" rate_punctuality=@rate_punctuality, rate_money=@rate_money, rate_food=@rate_food");
            //query.Append(" rate_food=@rate_food,rate_money@rate_money");

            query.Append(" WHERE review_id=@review_id");
            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intReviewId;
                if (bytCountryId == 0)
                    cmd.Parameters.AddWithValue("@country_id", DBNull.Value);

                else
                    cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = bytCountryId;
                cmd.Parameters.Add("@recommend_id", SqlDbType.TinyInt).Value = bytRecMId;
                cmd.Parameters.Add("@from_id", SqlDbType.TinyInt).Value = bytFromId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strtitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cmd.Parameters.Add("@positive", SqlDbType.NVarChar).Value = strPositive;
                cmd.Parameters.Add("@negative", SqlDbType.NVarChar).Value = strNagative;
                cmd.Parameters.Add("@full_name", SqlDbType.NVarChar).Value = strFullName;
                cmd.Parameters.Add("@review_from", SqlDbType.NVarChar).Value = strReviewFrom;
                cmd.Parameters.Add("@rate_overall", SqlDbType.TinyInt).Value = bytRateOverAll;

                cmd.Parameters.Add("@rate_knowledge", SqlDbType.TinyInt).Value = bytKnowledge;
                cmd.Parameters.Add("@rate_service", SqlDbType.TinyInt).Value = bytService;
                cmd.Parameters.Add("@rate_pronunciation", SqlDbType.TinyInt).Value = bytPron;
                cmd.Parameters.Add("@rate_punctuality", SqlDbType.TinyInt).Value = Punci;
                cmd.Parameters.Add("@rate_money", SqlDbType.TinyInt).Value = bytMoney;
                cmd.Parameters.Add("@rate_food", SqlDbType.TinyInt).Value = bytfood;

                cn.Open();
                ret = ReviewsManages.ExecuteNonQuery(cmd);
                
            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Review, StaffLogActionType.Update, StaffLogSection.F_Review, ProductId, "tbl_review_all", str.ToString(), arroldValue, "review_id", intReviewId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        //===========================================================================
        public static int ShowREviewInsert(int intProduct_id, int? intCudId, byte? bytCountryId, byte? bytRecMId, byte? bytFromId, string strtitle,
            string strDetail, string strPositive, string strNagative, string strFullName, string strReviewFrom, byte bytRateOverAll,
            byte bytPerform, byte bytPunc, byte bytService, byte bytmoney)
        {
            ReviewManage ReviewsManages = new ReviewManage();
            StringBuilder query = new StringBuilder();

            query.Append("INSERT INTO tbl_review_all (product_id,cus_id,country_id,recommend_id,from_id,cat_id,title,detail,positive,negative,full_name,review_from,rate_overall,");

            query.Append(" rate_performance,rate_punctuality,rate_service,rate_money,status,status_bin) VALUES (");

            query.Append(" @product_id,@cus_id,@country_id,@recommend_id,@from_id,'38',@title,@detail,@positive,@negative,@full_name,@review_from,@rate_overall,");

            query.Append(" @rate_performance,@rate_punctuality,@rate_service,@rate_money,'0','1')");
            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct_id;
                if (intCudId.HasValue)
                    cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = (int)intCudId;
                else
                    cmd.Parameters.AddWithValue("@cus_id", DBNull.Value);

                if (bytCountryId.HasValue)
                    cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = (byte)bytCountryId;
                else
                    cmd.Parameters.AddWithValue("@country_id", DBNull.Value);

                if (bytRecMId.HasValue)
                    cmd.Parameters.Add("@recommend_id", SqlDbType.TinyInt).Value = (byte)bytRecMId;
                else
                    cmd.Parameters.AddWithValue("@recommend_id", DBNull.Value);

                if (bytFromId.HasValue)
                    cmd.Parameters.Add("@from_id", SqlDbType.TinyInt).Value = (byte)bytFromId;
                else
                    cmd.Parameters.AddWithValue("@from_id", null);

                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strtitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cmd.Parameters.Add("@positive", SqlDbType.NVarChar).Value = strPositive;
                cmd.Parameters.Add("@negative", SqlDbType.NVarChar).Value = strNagative;
                cmd.Parameters.Add("@full_name", SqlDbType.NVarChar).Value = strFullName;
                cmd.Parameters.Add("@review_from", SqlDbType.NVarChar).Value = strReviewFrom;
                cmd.Parameters.Add("@rate_overall", SqlDbType.TinyInt).Value = bytRateOverAll;

                cmd.Parameters.Add("@rate_performance", SqlDbType.TinyInt).Value = bytPerform;
                cmd.Parameters.Add("@rate_punctuality", SqlDbType.TinyInt).Value = bytPunc;
                cmd.Parameters.Add("@rate_service", SqlDbType.TinyInt).Value = bytService;
                cmd.Parameters.Add("@rate_money", SqlDbType.TinyInt).Value = bytmoney;

                cn.Open();
                int ret = ReviewsManages.ExecuteNonQuery(cmd);
                return ret;
            }
        }


        public static bool ShowReviewUpdate(int intReviewId, byte bytCountryId, byte bytRecMId, byte bytFromId, string strtitle,
            string strDetail, string strPositive, string strNagative, string strFullName, string strReviewFrom, byte bytRateOverAll,
           byte bytPerform, byte bytPunc, byte bytService, byte bytmoney)
        {
            StringBuilder str = new StringBuilder();
            str.Append("country_id,recommend_id,from_id,title,detail,positive,");
            str.Append("negative,full_name,review_from,rate_overall,");
            str.Append("rate_performance,rate_punctuality,rate_service,rate_money");

            int ProductId = ReviewManage.GetreviewById(intReviewId).ProductId;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_review_all", str.ToString(), "review_id", intReviewId);
            //============================================================================================================================
            int ret = 0;

            ReviewManage ReviewsManages = new ReviewManage();
            object obj = new ProductReviews();
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE tbl_review_all SET country_id=@country_id , recommend_id=@recommend_id,  from_id=@from_id, title=@title,");
            query.Append(" detail=@detail, positive=@positive, negative=@negative, full_name=@full_name, review_from=@review_from, rate_overall=@rate_overall,");

            query.Append(" rate_performance=@rate_performance, rate_punctuality=@rate_punctuality,");
            query.Append(" rate_service=@rate_service, rate_money=@rate_money");
            //query.Append(" rate_food=@rate_food,rate_money@rate_money");

            query.Append(" WHERE review_id=@review_id");
            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intReviewId;
                if (bytCountryId == 0)
                    cmd.Parameters.AddWithValue("@country_id", DBNull.Value);

                else
                    cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = bytCountryId;
                cmd.Parameters.Add("@recommend_id", SqlDbType.TinyInt).Value = bytRecMId;
                cmd.Parameters.Add("@from_id", SqlDbType.TinyInt).Value = bytFromId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strtitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cmd.Parameters.Add("@positive", SqlDbType.NVarChar).Value = strPositive;
                cmd.Parameters.Add("@negative", SqlDbType.NVarChar).Value = strNagative;
                cmd.Parameters.Add("@full_name", SqlDbType.NVarChar).Value = strFullName;
                cmd.Parameters.Add("@review_from", SqlDbType.NVarChar).Value = strReviewFrom;
                cmd.Parameters.Add("@rate_overall", SqlDbType.TinyInt).Value = bytRateOverAll;

                cmd.Parameters.Add("@rate_performance", SqlDbType.TinyInt).Value = bytPerform;
                cmd.Parameters.Add("@rate_punctuality", SqlDbType.TinyInt).Value = bytPunc;
                cmd.Parameters.Add("@rate_service", SqlDbType.TinyInt).Value = bytService;
                cmd.Parameters.Add("@rate_money", SqlDbType.TinyInt).Value = bytmoney;
                

                cn.Open();
                ret = ReviewsManages.ExecuteNonQuery(cmd);
                
            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Review, StaffLogActionType.Update, StaffLogSection.F_Review, ProductId, "tbl_review_all", str.ToString(), arroldValue, "review_id", intReviewId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        //===========================================================================
        public static int HealthREviewInsert(int intProduct_id, int? intCudId, byte? bytCountryId, byte? bytRecMId, byte? bytFromId, string strtitle,
            string strDetail, string strPositive, string strNagative, string strFullName, string strReviewFrom, byte bytRateOverAll,
            byte bytClean, byte bytdiagnose, byte bytService, byte bytmoney)
        {
            ReviewManage ReviewsManages = new ReviewManage();
            StringBuilder query = new StringBuilder();

            query.Append("INSERT INTO tbl_review_all (product_id,cus_id,country_id,recommend_id,from_id,cat_id,title,detail,positive,negative,full_name,review_from,rate_overall,");

            query.Append(" rate_clean,rate_diagnose_ability,rate_service,rate_money,status,status_bin,date_submit) VALUES (");

            query.Append(" @product_id,@cus_id,@country_id,@recommend_id,@from_id,'39',@title,@detail,@positive,@negative,@full_name,@review_from,@rate_overall,");

            query.Append(" @rate_clean,@rate_diagnose_ability,@rate_service,@rate_money,'0','1',@date_submit)");
            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct_id;
                if (intCudId.HasValue)
                    cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = (int)intCudId;
                else
                    cmd.Parameters.AddWithValue("@cus_id", DBNull.Value);

                if (bytCountryId.HasValue)
                    cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = (byte)bytCountryId;
                else
                    cmd.Parameters.AddWithValue("@country_id", DBNull.Value);

                if (bytRecMId.HasValue)
                    cmd.Parameters.Add("@recommend_id", SqlDbType.TinyInt).Value = (byte)bytRecMId;
                else
                    cmd.Parameters.AddWithValue("@recommend_id", DBNull.Value);

                if (bytFromId.HasValue)
                    cmd.Parameters.Add("@from_id", SqlDbType.TinyInt).Value = (byte)bytFromId;
                else
                    cmd.Parameters.AddWithValue("@from_id", DBNull.Value);

                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strtitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cmd.Parameters.Add("@positive", SqlDbType.NVarChar).Value = strPositive;
                cmd.Parameters.Add("@negative", SqlDbType.NVarChar).Value = strNagative;
                cmd.Parameters.Add("@full_name", SqlDbType.NVarChar).Value = strFullName;
                cmd.Parameters.Add("@review_from", SqlDbType.NVarChar).Value = strReviewFrom;
                cmd.Parameters.Add("@rate_overall", SqlDbType.TinyInt).Value = bytRateOverAll;

                cmd.Parameters.Add("@rate_clean", SqlDbType.TinyInt).Value = bytClean;
                cmd.Parameters.Add("@rate_diagnose_ability", SqlDbType.TinyInt).Value = bytdiagnose;
                cmd.Parameters.Add("@rate_service", SqlDbType.TinyInt).Value = bytService;
                cmd.Parameters.Add("@rate_money", SqlDbType.TinyInt).Value = bytmoney;
                cmd.Parameters.Add("@date_submit", SqlDbType.DateTime).Value = DateTime.Now.Hotels2ThaiDateTime();

                cn.Open();
                int ret = ReviewsManages.ExecuteNonQuery(cmd);
                return ret;
            }
        }


        public static bool HealthReviewUpdate(int intReviewId, byte bytCountryId, byte bytRecMId, byte bytFromId, string strtitle,
            string strDetail, string strPositive, string strNagative, string strFullName, string strReviewFrom, byte bytRateOverAll,
           byte bytClean, byte bytdiagnose, byte bytService, byte bytmoney)
        {
            StringBuilder str = new StringBuilder();
            str.Append("country_id,recommend_id,from_id,title,detail,positive,");
            str.Append("negative,full_name,review_from,rate_overall,");
            str.Append("rate_clean,rate_diagnose_ability,rate_service,rate_money");

            int ProductId = ReviewManage.GetreviewById(intReviewId).ProductId;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_review_all", str.ToString(), "review_id", intReviewId);
            //============================================================================================================================
            int ret = 0;


            ReviewManage ReviewsManages = new ReviewManage();
            object obj = new ProductReviews();
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE tbl_review_all SET country_id=@country_id , recommend_id=@recommend_id,  from_id=@from_id, title=@title,");
            query.Append(" detail=@detail, positive=@positive, negative=@negative, full_name=@full_name, review_from=@review_from, rate_overall=@rate_overall,");

            query.Append(" rate_clean=@rate_clean, rate_diagnose_ability=@rate_diagnose_ability, rate_service=@rate_service,");
            query.Append(" rate_money=@rate_money");
            //query.Append(" rate_food=@rate_food,rate_money@rate_money");

            query.Append(" WHERE review_id=@review_id");
            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intReviewId;
                if (bytCountryId == 0)
                    cmd.Parameters.AddWithValue("@country_id", DBNull.Value);

                else
                    cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = bytCountryId;
                cmd.Parameters.Add("@recommend_id", SqlDbType.TinyInt).Value = bytRecMId;
                cmd.Parameters.Add("@from_id", SqlDbType.TinyInt).Value = bytFromId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strtitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cmd.Parameters.Add("@positive", SqlDbType.NVarChar).Value = strPositive;
                cmd.Parameters.Add("@negative", SqlDbType.NVarChar).Value = strNagative;
                cmd.Parameters.Add("@full_name", SqlDbType.NVarChar).Value = strFullName;
                cmd.Parameters.Add("@review_from", SqlDbType.NVarChar).Value = strReviewFrom;
                cmd.Parameters.Add("@rate_overall", SqlDbType.TinyInt).Value = bytRateOverAll;

                cmd.Parameters.Add("@rate_clean", SqlDbType.TinyInt).Value = bytClean;
                cmd.Parameters.Add("@rate_diagnose_ability", SqlDbType.TinyInt).Value = bytdiagnose;
                cmd.Parameters.Add("@rate_service", SqlDbType.TinyInt).Value = bytService;
                cmd.Parameters.Add("@rate_money", SqlDbType.TinyInt).Value = bytmoney;
               

                cn.Open();
                ret = ReviewsManages.ExecuteNonQuery(cmd);
                
            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Review, StaffLogActionType.Update, StaffLogSection.F_Review, ProductId, "tbl_review_all", str.ToString(), arroldValue, "review_id", intReviewId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }


        //===========================================================================
        public static int SpaREviewInsert(int intProduct_id, int? intCudId, byte? bytCountryId, byte? bytRecMId, byte? bytFromId, string strtitle,
            string strDetail, string strPositive, string strNagative, string strFullName, string strReviewFrom, byte bytRateOverAll,
            byte bytService, byte bytLocation, byte bytRoom, byte bytClean, byte bytmoney)
        {
            ReviewManage ReviewsManages = new ReviewManage();
            StringBuilder query = new StringBuilder();

            query.Append("INSERT INTO tbl_review_all (product_id,cus_id,country_id,recommend_id,from_id,cat_id,title,detail,positive,negative,full_name,review_from,rate_overall,");

            query.Append(" rate_service,rate_location,rate_room,rate_clean,rate_money,status,status_bin,date_submit) VALUES (");

            query.Append(" @product_id,@cus_id,@country_id,@recommend_id,@from_id,'40',@title,@detail,@positive,@negative,@full_name,@review_from,@rate_overall,");

            query.Append(" @rate_service,@rate_location,@rate_room,@rate_clean,@rate_money,'0','1',@date_submit)");
            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct_id;
                if (intCudId.HasValue)
                    cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = intCudId;
                else
                    cmd.Parameters.AddWithValue("@cus_id", DBNull.Value);

                if (bytCountryId.HasValue)
                    cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = bytCountryId;
                else
                    cmd.Parameters.AddWithValue("@country_id", DBNull.Value);

                if (bytRecMId.HasValue)
                    cmd.Parameters.Add("@recommend_id", SqlDbType.TinyInt).Value = bytRecMId;
                else
                    cmd.Parameters.AddWithValue("@recommend_id", DBNull.Value);

                if (bytFromId.HasValue)
                    cmd.Parameters.Add("@from_id", SqlDbType.TinyInt).Value = bytFromId;
                else
                    cmd.Parameters.AddWithValue("@from_id", DBNull.Value);

                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strtitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cmd.Parameters.Add("@positive", SqlDbType.NVarChar).Value = strPositive;
                cmd.Parameters.Add("@negative", SqlDbType.NVarChar).Value = strNagative;
                cmd.Parameters.Add("@full_name", SqlDbType.NVarChar).Value = strFullName;
                cmd.Parameters.Add("@review_from", SqlDbType.NVarChar).Value = strReviewFrom;
                cmd.Parameters.Add("@rate_overall", SqlDbType.TinyInt).Value = bytRateOverAll;

                cmd.Parameters.Add("@rate_service", SqlDbType.TinyInt).Value = bytService;
                cmd.Parameters.Add("@rate_location", SqlDbType.TinyInt).Value = bytLocation;
                cmd.Parameters.Add("@rate_room", SqlDbType.TinyInt).Value = bytRoom;
                cmd.Parameters.Add("@rate_clean", SqlDbType.TinyInt).Value = bytClean;
                cmd.Parameters.Add("@rate_money", SqlDbType.TinyInt).Value = bytmoney;
                cmd.Parameters.Add("@date_submit", SqlDbType.DateTime).Value = DateTime.Now.Hotels2ThaiDateTime();

                cn.Open();
                int ret = ReviewsManages.ExecuteNonQuery(cmd);
                return ret;
            }
        }

        public static bool SpaReviewUpdate(int intReviewId, byte bytCountryId, byte bytRecMId, byte bytFromId, string strtitle,
            string strDetail, string strPositive, string strNagative, string strFullName, string strReviewFrom, byte bytRateOverAll,
            byte bytService, byte bytLocation, byte bytRoom, byte bytClean, byte bytmoney)
        {

            StringBuilder str = new StringBuilder();
            str.Append("country_id,recommend_id,from_id,title,detail,positive,");
            str.Append("negative,full_name,review_from,rate_overall,");
            str.Append("rate_service,rate_location,rate_room,rate_clean,rate_money");

            int ProductId = ReviewManage.GetreviewById(intReviewId).ProductId;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_review_all", str.ToString(), "review_id", intReviewId);
            //============================================================================================================================
            int ret = 0;

            ReviewManage ReviewsManages = new ReviewManage();
            object obj = new ProductReviews();
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE tbl_review_all SET country_id=@country_id , recommend_id=@recommend_id,  from_id=@from_id, title=@title,");
            query.Append(" detail=@detail, positive=@positive, negative=@negative, full_name=@full_name, review_from=@review_from, rate_overall=@rate_overall,");

            query.Append(" rate_service=@rate_service, rate_location=@rate_location, rate_room=@rate_room,");
            query.Append(" rate_clean=@rate_clean, rate_money=@rate_money");
            //query.Append(" rate_food=@rate_food,rate_money@rate_money");

            query.Append(" WHERE review_id=@review_id");
            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@review_id", SqlDbType.Int).Value = intReviewId;
                if (bytCountryId == 0)
                    cmd.Parameters.AddWithValue("@country_id", DBNull.Value);

                else
                    cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = bytCountryId;
                cmd.Parameters.Add("@recommend_id", SqlDbType.TinyInt).Value = bytRecMId;
                cmd.Parameters.Add("@from_id", SqlDbType.TinyInt).Value = bytFromId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strtitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cmd.Parameters.Add("@positive", SqlDbType.NVarChar).Value = strPositive;
                cmd.Parameters.Add("@negative", SqlDbType.NVarChar).Value = strNagative;
                cmd.Parameters.Add("@full_name", SqlDbType.NVarChar).Value = strFullName;
                cmd.Parameters.Add("@review_from", SqlDbType.NVarChar).Value = strReviewFrom;
                cmd.Parameters.Add("@rate_overall", SqlDbType.TinyInt).Value = bytRateOverAll;

                cmd.Parameters.Add("@rate_service", SqlDbType.TinyInt).Value = bytService;
                cmd.Parameters.Add("@rate_location", SqlDbType.TinyInt).Value = bytLocation;
                cmd.Parameters.Add("@rate_room", SqlDbType.TinyInt).Value = bytRoom;
                cmd.Parameters.Add("@rate_clean", SqlDbType.TinyInt).Value = bytClean;
                cmd.Parameters.Add("@rate_money", SqlDbType.TinyInt).Value = bytmoney;

                cn.Open();
                ret = ReviewsManages.ExecuteNonQuery(cmd);
                
            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Review, StaffLogActionType.Update, StaffLogSection.F_Review, ProductId, "tbl_review_all", str.ToString(), arroldValue, "review_id", intReviewId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        
        //================================ Review From 
        public static IDictionary<byte, string> getReViewFrom()
        {
            ReviewManage ReviewsManages = new ReviewManage();
            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT from_id,title FROM tbl_review_from", cn);
                cn.Open();
                IDataReader reader = ReviewsManages.ExecuteReader(cmd);
                IDictionary<byte, string> IdicResult = new Dictionary<byte, string>();
                while (reader.Read())
                {
                    IdicResult.Add((byte)reader["from_id"], reader["title"].ToString());
                }
                return IdicResult;
            }
        }


        
   

        //================================ Recommend From
        public static IDictionary<byte, string> getReViewRecommend()
        {
            ReviewManage ReviewsManages = new ReviewManage();
            using (SqlConnection cn = new SqlConnection(ReviewsManages.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT recommend_id,title FROM tbl_review_recommend", cn);
                cn.Open();
                IDataReader reader = ReviewsManages.ExecuteReader(cmd);
                IDictionary<byte, string> IdicResult = new Dictionary<byte, string>();

                while (reader.Read())
                {
                    IdicResult.Add((byte)reader["recommend_id"], reader["title"].ToString());
                }

                return IdicResult;
            }
        }

    }
}