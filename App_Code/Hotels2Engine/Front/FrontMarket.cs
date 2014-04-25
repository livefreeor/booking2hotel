using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;

/// <summary>
/// Summary description for FrontMarket
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontMarket:Hotels2BaseClass
    {
        public byte MarketID { get; set; }
        public byte GroupID { get; set; }
        public int CountryID { get; set; }
        public int ConditionID { get; set; }
        public string Title { get; set; }
        public string CountryTitle { get; set; }
        public bool IsExcept { get; set; }

        public FrontMarket()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<FrontMarket> getMarketCountry(string conditionGroup)
        {
            string sqlCommand = string.Empty;
            List<FrontMarket> result = new List<FrontMarket>();

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
               
                sqlCommand = sqlCommand + " select cm.market_id,isnull(cm.group_id,0) as group_id,crm.title,cmg.country_id,c.title as country_title,cm.Isexecpt,poc.condition_id ";
                sqlCommand = sqlCommand + " from tbl_country_market cm,tbl_country_rate_market_content crm,tbl_country_market_group_country cmg,tbl_country c,tbl_product_option_condition poc";
                sqlCommand = sqlCommand + " where cm.market_id=crm.market_id and cm.group_id=cmg.group_id and cmg.country_id=c.country_id and cm.market_id=poc.market_id";
                sqlCommand = sqlCommand + " and crm.lang_id=1";
                sqlCommand = sqlCommand + " and poc.condition_id IN (" + conditionGroup + ")";
                sqlCommand = sqlCommand + " and cm.group_id<>12";

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                

                while (reader.Read())
                {
                    result.Add(new FrontMarket
                    {
                        MarketID = (byte)reader["market_id"],
                        GroupID = (byte)reader["group_id"],
                        Title = reader["title"].ToString(),
                        CountryID = (byte)reader["country_id"],
                        ConditionID = (int)reader["condition_id"],
                        CountryTitle = reader["country_title"].ToString(),
                        IsExcept = (bool)reader["Isexecpt"]
                    });
                }
            }
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                sqlCommand = "select cm.market_id,isnull(cm.group_id,0) as group_id,crm.title,cm.country_id,c.title as country_title,cm.Isexecpt,poc.condition_id";
                sqlCommand = sqlCommand + " from tbl_country_market cm,tbl_country_rate_market_content crm,tbl_country c,tbl_product_option_condition poc ";
                sqlCommand = sqlCommand + " where cm.market_id=crm.market_id and cm.country_id=c.country_id and cm.market_id=poc.market_id";
                sqlCommand = sqlCommand + " and crm.lang_id=1 and poc.condition_id IN (" + conditionGroup + ")";
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new FrontMarket
                    {
                        MarketID = (byte)reader["market_id"],
                        GroupID = (byte)reader["group_id"],
                        Title = reader["title"].ToString(),
                        CountryID = (byte)reader["country_id"],
                        ConditionID = (int)reader["condition_id"],
                        CountryTitle = reader["country_title"].ToString(),
                        IsExcept = (bool)reader["Isexecpt"]
                    });
                }
                //HttpContext.Current.Response.Write(sqlCommand + "<br/>");
                return result;
            }
            
        }

    }
}