using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using Hotels2thailand;
using Hotels2thailand.Staffs;


namespace Hotels2thailand.ProductOption
{
/// <summary>
/// Summary description for PromotionContent
/// </summary>
    public class PromotionContent:Hotels2BaseClass
    {
        public int PromotionId { get; set; }
        public byte langId { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
	    public PromotionContent()
	    {
		    //
		    // TODO: Add constructor logic here
		    //
	    }

        public int InsertAndUpdateContentLang(PromotionContent cProcontent)
        {
            StringBuilder QueryString = new StringBuilder();
            QueryString.Append("INSERT INTO tbl_promotion_content(promotion_id,lang_id,title,detail)");
            QueryString.Append(" VALUES(@promotion_id,@lang_id,@title,@detail)");
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(QueryString.ToString(), cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = cProcontent.PromotionId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = cProcontent.langId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = cProcontent.Title;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = cProcontent.Detail;
                cn.Open();
                ExecuteNonQuery(cmd);
                
            }
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_promotion, StaffLogActionType.Insert, StaffLogSection.Product,
                int.Parse(HttpContext.Current.Request.QueryString["pid"]), "tbl_promotion_content", "promotion_id,lang_id,title,detail", "promotion_id,lang_id", cProcontent.PromotionId, cProcontent.langId);
            //========================================================================================================================================================
            return ret;
        }

        public PromotionContent GetPromotionContentbyProIdANdLangId(int intProId, byte bytLangId)
        {
           
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT promotion_id,lang_id,title,detail FROM tbl_promotion_content WHERE promotion_id=@promotion_id AND lang_id=@lang_id", cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intProId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return (PromotionContent)MappingObjectFromDataReader(reader);
                }
                else
                {
                    return null;
                }
            }

            
        }

        public bool UpdatePromotionContent(PromotionContent cProcontent)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_promotion_content", "title,detail", "promotion_id,lang_id", cProcontent.PromotionId, cProcontent.langId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_promotion_content SET title=@title, detail=@detail WHERE promotion_id=@promotion_id AND lang_id=@lang_id", cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = cProcontent.PromotionId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = cProcontent.langId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = cProcontent.Title;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = cProcontent.Detail;
                
                cn.Open();

               ret = ExecuteNonQuery(cmd);
                
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_promotion, StaffLogActionType.Update, StaffLogSection.Product, int.Parse(HttpContext.Current.Request.QueryString["pid"]),
                "tbl_promotion_content", "title,detail", arroldValue, "promotion_id,lang_id", cProcontent.PromotionId, cProcontent.langId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        //Static Method============

        //public static bool UpdateProContent(int intProId, byte bytLangId, string strTitle, string strDetail)
        //{
        //    PromotionContent cProContent = new PromotionContent
        //    {
        //        PromotionId = intProId,
        //        langId = bytLangId,
        //        Title = strTitle,
        //        Detail = strDetail
        //    };

        //    return cProContent.UpdatePromotionContent(cProContent);
        //}

        public static int InsertOrUpdateProContent(int intProId, byte bytLangId, string strTitle, string strDetail)
        {
            PromotionContent cProContent = new PromotionContent
            {
                PromotionId = intProId,
                langId = bytLangId,
                Title = strTitle,
                Detail = strDetail
            };

            if (cProContent.GetPromotionContentbyProIdANdLangId(intProId, bytLangId) == null)
            {
                cProContent.InsertAndUpdateContentLang(cProContent);
            }
            else
            {
                cProContent.UpdatePromotionContent(cProContent);
            }
            int ret = 1;
            return ret;
        }
    }
}