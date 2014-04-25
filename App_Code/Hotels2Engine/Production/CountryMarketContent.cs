using System;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
using System.Text;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for Country
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class CountryMarketContent : Hotels2BaseClass
    {
        public byte MarketId { get; set; }
        public byte LangId { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }


        public CountryMarketContent getMarketContentbyId(byte bytMarket, byte bytLangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_country_rate_market_content WHERE market_id=@market_id AND  lang_id=@lang_id", cn);
                cmd.Parameters.Add("@market_id", SqlDbType.TinyInt).Value = bytMarket;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return (CountryMarketContent)MappingObjectFromDataReader(reader);
                }
                else
                {
                    return null;
                }
                
            }
        }
        public int IsHaveContentrecord(byte bytMarket, byte bytLangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tbl_country_rate_market_content WHERE market_id=@market_id AND  lang_id=@lang_id", cn);
                cmd.Parameters.Add("@market_id", SqlDbType.TinyInt).Value = bytMarket;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();
                int ret = (int)ExecuteScalar(cmd);
                return ret;
            }
        }

        public int Insert(byte bytMarket, byte bytLangId, string strTitle, string strDetail)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("INSERT INTO tbl_country_rate_market_content (market_id,lang_id,title,detail)");
                query.Append(" VALUES(@market_id,@lang_id,@title,@detail)");
                

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@market_id", SqlDbType.TinyInt).Value = bytMarket;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strTitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                

                cn.Open();

                int ret = ExecuteNonQuery(cmd);
                //=== STAFF ACTIVITY ================================================================================================================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_detail, StaffLogActionType.Insert, StaffLogSection.NULL,
                    null, "tbl_country_rate_market_content", "market_id,lang_id,title,detail",
                    "market_id,lang_id", bytMarket, bytLangId);
                //===================================================================================================================================================================================================

                return ret;

            }

        }
        public bool UpdateMarketContent(byte bytMarket, byte bytLangId, string strTitle, string strDetail)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_country_rate_market_content", "market_id,lang_id,title,detail", "market_id,lang_id", bytMarket, bytLangId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("UPDATE tbl_country_rate_market_content SET title=@title, detail=@detail");
                query.Append(" WHERE market_id=@market_id AND  lang_id=@lang_id");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@market_id", SqlDbType.TinyInt).Value = bytMarket;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strTitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_detail, StaffLogActionType.Update, StaffLogSection.NULL,
                null, "tbl_country_rate_market_content", "market_id,lang_id,title,detail", arroldValue, "market_id,lang_id", bytMarket, bytLangId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);

        }

    }
}