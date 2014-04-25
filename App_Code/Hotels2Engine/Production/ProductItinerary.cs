using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.Staffs;
/// <summary>
/// Summary description for Product
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductItinerary:Hotels2BaseClass
    {
        public int ItineraryItemID { get; set; }
        public int ConditionId { get; set; }
        public DateTime TimeStart { get; set; }
        public Nullable<DateTime> TimeEnd { get; set; }
        public bool Status { get; set; }
       

       
        public static int InsertNewItineary(int intConditionId, DateTime dTimeStart, DateTime dTimeEnd)
        {
            //LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            ProductItinerary cItinerary = new ProductItinerary();

            using (SqlConnection cn = new SqlConnection(cItinerary.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_itinerary_item (itinerary_id,time_start,time_end,status) VALUES (@itinerary_id,@time_start,@time_end,@status); SET @itinerary_item_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@itinerary_id", SqlDbType.Int).Value = intConditionId;
                cmd.Parameters.Add("@time_start", SqlDbType.SmallDateTime).Value = dTimeStart;
                cmd.Parameters.Add("@time_end", SqlDbType.SmallDateTime).Value = dTimeEnd;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@itinerary_item_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                int ret = cItinerary.ExecuteNonQuery(cmd);
                int itemId = (int)cmd.Parameters["@itinerary_item_id"].Value;

                int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
                //=== STAFF ACTIVITY ================================================================================================================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_Itinerary, StaffLogActionType.Insert, StaffLogSection.Product,
                    ProductId, "tbl_product_itinerary_item", "itinerary_id,time_start,time_end,status", "itinerary_item_id", itemId);
                //===================================================================================================================================================================================================
                return ret;
            }
            
        }

        public static int InsertNewItineary(int intConditionId, DateTime dTimeStart)
        {
            ProductItinerary cItinerary = new ProductItinerary();

            using (SqlConnection cn = new SqlConnection(cItinerary.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_itinerary_item (itinerary_id,time_start,status) VALUES (@itinerary_id,@time_start,@status); SET @itinerary_item_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@itinerary_id", SqlDbType.Int).Value = intConditionId;
                cmd.Parameters.Add("@time_start", SqlDbType.SmallDateTime).Value = dTimeStart;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@itinerary_item_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                cn.Open();
                int ret = cItinerary.ExecuteNonQuery(cmd);
                int itemId = (int)cmd.Parameters["@itinerary_item_id"].Value;
                int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
                //=== STAFF ACTIVITY ================================================================================================================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_Itinerary, StaffLogActionType.Insert, StaffLogSection.Product,
                    ProductId, "tbl_product_itinerary_item", "itinerary_id,status", "itinerary_item_id", itemId);
                //===================================================================================================================================================================================================
                return ret;
            }
            
        }

        public static bool DelItinerary(int intTineraryID, byte bytLangId)
        {
            ProductItinerary cItinerary = new ProductItinerary();
            IList<object[]> arroldValue1 = null;
            IList<object[]> arroldValue2 = null;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            arroldValue1 = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_itinerary_content", "itinerary_item_id,lang_id", intTineraryID, bytLangId);
            //============================================================================================================================

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            arroldValue2 = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_itinerary_item", "itinerary_item_id", intTineraryID);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(cItinerary.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_itinerary_content WHERE itinerary_item_id=@itinerary_item_id", cn);
                cmd.Parameters.Add("@itinerary_item_id", SqlDbType.Int).Value = intTineraryID;
                ret = 1;
                SqlCommand cmd2 = new SqlCommand("DELETE FROM tbl_product_itinerary_item WHERE itinerary_item_id=@itinerary_item_id", cn);
                cmd2.Parameters.Add("@itinerary_item_id", SqlDbType.Int).Value = intTineraryID;
                cn.Open();
                cItinerary.ExecuteNonQuery(cmd);
                cItinerary.ExecuteNonQuery(cmd2);
                
            }


            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ==
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_Itinerary, StaffLogActionType.Delete, StaffLogSection.Product, ProductId,
                "tbl_product_itinerary_item", arroldValue1, "itinerary_item_id,lang_id", intTineraryID, bytLangId);
            //============================================================================================================================

            
            //#Staff_Activity_Log================================================================================================ STEP 2 ==
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_Itinerary, StaffLogActionType.Delete, StaffLogSection.Product, ProductId,
                "tbl_product_itinerary_item", arroldValue2, "itinerary_item_id", intTineraryID);
            //============================================================================================================================
            return (ret == 1);
            
            
        }

        public static bool UpdateItinerary(int intItinearary, DateTime dTimeStart, Nullable<DateTime> dTimeEnd)
        {
            ProductItinerary cItinerary = new ProductItinerary();
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_itinerary", "time_start,time_end", "itinerary_item_id", intItinearary);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(cItinerary.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_itinerary SET time_start=@time_start,time_end=@time_end WHERE itinerary_item_id=@itinerary_item_id");
                cmd.Parameters.Add("@itinerary_item_id", SqlDbType.Int).Value = intItinearary;
                cmd.Parameters.Add("@time_start", SqlDbType.SmallDateTime).Value = dTimeStart;
                cmd.Parameters.Add("@time_end", SqlDbType.SmallDateTime).Value = dTimeEnd;
                cn.Open();
                ret = cItinerary.ExecuteNonQuery(cmd);
                
            }
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Itinerary, StaffLogActionType.Update, StaffLogSection.Product, ProductId,
                "tbl_product_itinerary", "time_start,time_end", arroldValue, "itinerary_item_id", intItinearary);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }

        public ProductItinerary GetIteneraryItemByID(int IteneraryID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT itinerary_item_id,itinerary_id,time_start,time_end,status FROM tbl_product_itinerary_item WHERE itinerary_id=@itinerary_id", cn);
                cmd.Parameters.Add("@itinerary_id", SqlDbType.Int).Value = IteneraryID;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd);
                if (reader.Read())
                {
                    return (ProductItinerary)MappingObjectFromDataReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }

        

        public List<object> GetIteneraryItemByItineraryIdAndConditionId(int intConditionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT iti.itinerary_item_id,iti.itinerary_id,iti.time_start,iti.time_end,iti.status FROM tbl_product_itinerary it, tbl_product_itinerary_item iti WHERE it.itinerary_id = iti.itinerary_id AND it.option_id = @option_id ", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intConditionId;
                
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
             
        }

        public static ArrayList GetItineraryByItineraryItemAndLangId(int ItineraryItemId, byte bytLangId)
        {
            ProductItinerary cItinerary = new ProductItinerary();
            ArrayList arrItem = new ArrayList();
            using (SqlConnection cn = new SqlConnection(cItinerary.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT title,detail FROM tbl_product_itinerary_content WHERE itinerary_item_id=@itinerary_item_id AND lang_id=@lang_id", cn);
                cmd.Parameters.Add("@itinerary_item_id", SqlDbType.Int).Value = ItineraryItemId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;

                cn.Open();
                IDataReader reader = cItinerary.ExecuteReader(cmd);
                if (reader.Read())
                {
                    arrItem.Add(reader[0].ToString());
                    arrItem.Add(reader[1].ToString());
                    return arrItem;
                }
                else
                {
                    return null;
                }
               
            }
        }


        //========================= PRODUCT ITINERARY  ====================================
        //tbl_product_itinerary

        public static Nullable<bool> getDefaultItinerary(int intCondition)
        {
            ProductItinerary cItinerary = new ProductItinerary();
            using (SqlConnection cn = new SqlConnection(cItinerary.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT show_default FROM tbl_product_itinerary WHERE option_id = @option_id", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intCondition;
                cn.Open();

                Nullable<bool> Result;

                IDataReader reader = cItinerary.ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Result = (bool)reader[0];
                }
                else
                {
                    Result = null;
                }
                return Result;
            }
        }

        public static int ItineraryCountByOptionId(int intOptionId)
        {
            ProductItinerary cItinerary = new ProductItinerary();
            using (SqlConnection cn = new SqlConnection(cItinerary.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tbl_product_itinerary WHERE option_id = @option_id", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cn.Open();

                int Result = (int)cItinerary.ExecuteScalar(cmd);
                return Result;
            }


        }

        public static object ItineraryIdByOptionId(int intOptionId)
        {
            ProductItinerary cItinerary = new ProductItinerary();
            using (SqlConnection cn = new SqlConnection(cItinerary.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT itinerary_id FROM tbl_product_itinerary WHERE option_id = @option_id", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cn.Open();

                IDataReader reader = cItinerary.ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return  (int)reader[0];
                }
                else
                {
                    return 0;
                }
             }


        }

        public static int InsertItinerary(int intOptionId , bool bolShowDefault)
        {
            ProductItinerary cItinerary = new ProductItinerary();
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(cItinerary.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_itinerary (option_id, show_default) VALUES (@option_id, @show_default);SET @itinerary_id = SCOPE_IDENTITY()", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@show_default", SqlDbType.Bit).Value = bolShowDefault;
                cmd.Parameters.Add("@itinerary_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();

                cItinerary.ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@itinerary_id"].Value;
                
            }
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_Itinerary, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductId, "tbl_product_itinerary", "option_id,show_default", "itinerary_id", ret);
            //========================================================================================================================================================

            return ret;
        }

        public static bool UpdateItinerary(int intOptionId, bool bolShoeDefault)
        {
            ProductItinerary cItinerary = new ProductItinerary();
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_itinerary", "show_default", "option_id", intOptionId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(cItinerary.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_itinerary SET show_default=@show_default WHERE option_id=@option_id", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@show_default", SqlDbType.Bit).Value = bolShoeDefault;
                cn.Open();

                ret = cItinerary.ExecuteNonQuery(cmd);
                
            }
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Itinerary, StaffLogActionType.Update, StaffLogSection.Product, ProductId,
                "tbl_product_itinerary", "show_default", arroldValue, "option_id", intOptionId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }

        public static ArrayList GetItinerarytitleByItinerary(int intCondition)
        {
            ProductItinerary cItinerary = new ProductItinerary();
            ArrayList arrItem = new ArrayList();
            using (SqlConnection cn = new SqlConnection(cItinerary.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT itinerary_id FROM tbl_product_itinerary  WHERE option_id = @option_id", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intCondition;
               

                cn.Open();
                IDataReader reader = cItinerary.ExecuteReader(cmd);
                if (reader.Read())
                {
                    arrItem.Add((int)reader[0]);
                    
                    return arrItem;
                }
                else
                {
                    return null;
                }

            }



        }
        
        //========================= PRODUCT ITINERARY Title CONTENT ====================================
        //tbl_product_itinerary_title_content

        public static int InsertItineraryTitleContent(int intOptionId, byte bytLangId, string strTitle)
        {
            
            ProductItinerary cItinerary = new ProductItinerary();
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(cItinerary.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_itinerary_title_content (itinerary_id,lang_id,title) VALUES(@itinerary_id,@lang_id,@title)", cn);
                cmd.Parameters.Add("@itinerary_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strTitle;
                cn.Open();
                ret = cItinerary.ExecuteNonQuery(cmd);
                
            }
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_Itinerary, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductId, "tbl_product_itinerary_title_content", "itinerary_id,lang_id,title", "itinerary_id,lang_id", intOptionId, bytLangId);
            //========================================================================================================================================================

            return ret;
        }

        public static bool UpdateItineraryTitleContent(int intOptionId, byte bytLangId, string strTitle)
        {
            ProductItinerary cItinerary = new ProductItinerary();
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_itinerary_title_content", "title", "itinerary_id,lang_id", intOptionId, bytLangId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(cItinerary.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_itinerary_title_content SET title=@title WHERE itinerary_id=@itinerary_id AND lang_id=@lang_id", cn);
                cmd.Parameters.Add("@itinerary_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strTitle;
                cn.Open();
                ret = cItinerary.ExecuteNonQuery(cmd);
                
            }

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Itinerary, StaffLogActionType.Update, StaffLogSection.Product, ProductId,
                "tbl_product_itinerary_title_content", "title", arroldValue, "itinerary_id,lang_id", intOptionId, bytLangId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }
        
        public static ArrayList GetItinerarytitleByItineraryAndLangId(int intCondition, byte bytLangId)
        {
            ProductItinerary cItinerary = new ProductItinerary();
            ArrayList arrItem = new ArrayList();
            using (SqlConnection cn = new SqlConnection(cItinerary.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT itc.title, itc.itinerary_id FROM tbl_product_itinerary it, tbl_product_itinerary_title_content itc WHERE it.itinerary_id = itc.itinerary_id AND it.option_id = @option_id AND itc.lang_id = @lang_id ", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intCondition;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;

                cn.Open();
                IDataReader reader = cItinerary.ExecuteReader(cmd);
                if (reader.Read())
                {
                    arrItem.Add(reader[0].ToString());
                    arrItem.Add((int)reader[1]);
                    return arrItem;
                }
                else
                {
                    return null;
                }
                
            }



        }
        
        //========================= PRODUCT ITINERARY CONTENT ====================================
        //tbl_product_itinerary_content

        public static int InsertItineraryContent(int intItineraryId, byte bytLangId, string strTitle, string strDetail)
        {
            ProductItinerary cItinerary = new ProductItinerary();

            using (SqlConnection cn = new SqlConnection(cItinerary.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_itinerary_content (itinerary_item_id,lang_id,title,detail) VALUES (@itinerary_item_id,@lang_id,@title,@detail)", cn);
                cmd.Parameters.Add("@itinerary_item_id", SqlDbType.Int).Value = intItineraryId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strTitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cn.Open();

                int ret = cItinerary.ExecuteNonQuery(cmd);
                int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);

                //=== STAFF ACTIVITY =====================================================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_Itinerary, StaffLogActionType.Insert, StaffLogSection.Product,
                    ProductId, "tbl_product_itinerary_content", "itinerary_item_id,lang_id,title,detail", "itinerary_item_id,lang_id", ret, bytLangId);
                //========================================================================================================================================================
                return ret;
            }
        }

        public static bool UpdateItineraryContent(int intItineraryId, byte bytLangId, string strTitle, string strDetail)
        {
            ProductItinerary cItinerary = new ProductItinerary();
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_itinerary_content", "title,detail", "itinerary_item_id,lang_id", intItineraryId, bytLangId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(cItinerary.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_itinerary_content SET   title=@title, detail=@detail WHERE itinerary_item_id=@itinerary_item_id AND lang_id=@lang_id", cn);
                cmd.Parameters.Add("@itinerary_item_id", SqlDbType.Int).Value = intItineraryId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strTitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cn.Open();


                ret = cItinerary.ExecuteNonQuery(cmd);

                
            }

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Itinerary, StaffLogActionType.Update, StaffLogSection.Product, ProductId,
                "tbl_product_itinerary_content", "title,detail", arroldValue, "itinerary_item_id,lang_id", intItineraryId, bytLangId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        public static ArrayList GetItineraryByItineraryAndLangId(int ItineraryId, byte bytLangId)
        {
            ProductItinerary cItinerary = new ProductItinerary();
            ArrayList arrItem = new ArrayList();
            using (SqlConnection cn = new SqlConnection(cItinerary.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT title,detail FROM tbl_product_itinerary_content WHERE itinerary_item_id=@itinerary_item_id AND lang_id=@lang_id", cn);
                cmd.Parameters.Add("@itinerary_item_id", SqlDbType.Int).Value = ItineraryId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                
                cn.Open();
                IDataReader reader = cItinerary.ExecuteReader(cmd);
                if (reader.Read())
                {
                    arrItem.Add(reader[0].ToString());
                    arrItem.Add(reader[1].ToString());
                    return arrItem;
                }
                else
                {
                    return null;
                }
                    
            }
            
            
                         
        }

        
        
    }
}