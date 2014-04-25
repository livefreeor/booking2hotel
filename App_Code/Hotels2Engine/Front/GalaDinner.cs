using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using Hotels2thailand.DataAccess;

/// <summary>
/// Summary description for ProductPriceList
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class GalaDinner:Hotels2BaseClass
    {
        public int OptionID { get; set; }
        public int ConditionID { get; set; }
        public string Title { get; set; }
        public string ConditionTitle { get; set; }
        public decimal Rate { get; set; }
        public decimal RateOwn { get; set; }
        public decimal RateRack { get; set; }
        public DateTime DateUseStart { get; set; }
        public DateTime DateUseEnd { get; set; }
        public bool RequireAdult { get; set; }
        public bool RequireChild { get; set; }
        public byte DefaultGala { get; set; }
        public byte NumAdult { get; set; }
        public byte NumChild { get; set; }

        private int ProductID;
        private DateTime DateStartCheck;
        private DateTime DateEndCheck;
        private byte _langID = 1;
        public byte LangID
        {
            set { _langID = value; }
        }
        public GalaDinner()
        { 
        
        }
        public GalaDinner(int productID,DateTime dateStart, DateTime dateEnd)
        {
            ProductID = productID;
            DateStartCheck = dateStart;
            DateEndCheck = dateEnd.AddDays(-1);
        }

        public List<GalaDinner> GetGala()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select po.option_id,po.title,";
                sqlCommand = sqlCommand + " (select top 1 spoc.title from tbl_product_option_content spoc where spoc.option_id=po.option_id and spoc.lang_id="+_langID+") as second_lang,";
                sqlCommand = sqlCommand + " pocp.rate,pocp.rate_own,pocp.rate_rack,pog.date_start,pog.date_end,pog.require_adult,pog.require_child,pog.default_gala,poc.title as condition_title,poc.condition_id,poc.num_adult,poc.num_children";
                sqlCommand = sqlCommand + " from tbl_product_option po,tbl_product_option_gala pog,tbl_product_option_condition poc,tbl_product_option_condition_price pocp,tbl_product_period pp";
                sqlCommand = sqlCommand + " where po.option_id=pog.option_id and po.option_id=poc.option_id and poc.condition_id=pocp.condition_id and pp.period_id=pocp.period_id and po.product_id=" + ProductID + " and po.cat_id=47";
                sqlCommand = sqlCommand + " and ((pog.date_start<=" + DateStartCheck.Hotels2DateToSQlString() + " and pog.date_end>=" + DateStartCheck.Hotels2DateToSQlString() + ") or (pog.date_start<=" + DateEndCheck.Hotels2DateToSQlString() + " and pog.date_end>=" + DateEndCheck.Hotels2DateToSQlString() + ") or (pog.date_start>=" + DateStartCheck.Hotels2DateToSQlString() + " and pog.date_end<=" + DateEndCheck.Hotels2DateToSQlString() + "))";
                sqlCommand = sqlCommand + " and ((pog.date_start between pp.date_start and pp.date_end) or (pog.date_end between pp.date_start and pp.date_end))";
                sqlCommand = sqlCommand + " and po.status=1";
                //HttpContext.Current.Response.Write(sqlCommand);
                List<GalaDinner> gala = new List<GalaDinner>();

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                string optionTitle = string.Empty;

                while (reader.Read())
                {
                    
                    if(_langID==1)
                    {
                        optionTitle = reader["title"].ToString();
                    }else{
                        optionTitle = reader["second_lang"].ToString();
                        if (string.IsNullOrEmpty(optionTitle))
                        {
                            optionTitle = reader["title"].ToString();
                        }
                    }

                    gala.Add(new GalaDinner
                    {
                        OptionID = (int)reader["option_id"],
                        
                        ConditionID = (int)reader["condition_id"],
                        Title = optionTitle,
                        ConditionTitle = reader["condition_title"].ToString(),
                        Rate = (decimal)reader["rate"],
                        RateOwn = (decimal)reader["rate_own"],
                        RateRack = (decimal)reader["rate_rack"],
                        DateUseStart = (DateTime)reader["date_start"],
                        DateUseEnd = (DateTime)reader["date_end"],
                        RequireAdult = (bool)reader["require_adult"],
                        RequireChild = (bool)reader["require_child"],
                        DefaultGala = (byte)reader["default_gala"],
                        NumAdult=(byte)reader["num_adult"],
                        NumChild=(byte)reader["num_children"]
                    });

                }
                return gala;
            }
            
        }
        public List<GalaDinner> GetGalaExtranet()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                //string sqlCommand = "select po.option_id,po.title,pocp.rate,pocp.rate_own,pocp.rate_rack,pog.date_start,pog.date_end,pog.require_adult,pog.require_child,pog.default_gala,poc.title as condition_title,poc.condition_id,poc.num_adult,poc.num_children";
                //sqlCommand = sqlCommand + " from tbl_product_option po,tbl_product_option_gala pog,tbl_product_option_condition poc,tbl_product_option_condition_price pocp,tbl_product_period pp";
                //sqlCommand = sqlCommand + " where po.option_id=pog.option_id and po.option_id=poc.option_id and poc.condition_id=pocp.condition_id and pp.period_id=pocp.period_id and po.product_id=" + ProductID + " and po.cat_id=47";
                //sqlCommand = sqlCommand + " and ((pog.date_start<=" + DateStartCheck.Hotels2DateToSQlString() + " and pog.date_end>=" + DateStartCheck.Hotels2DateToSQlString() + ") or (pog.date_start<=" + DateEndCheck.Hotels2DateToSQlString() + " and pog.date_end>=" + DateEndCheck.Hotels2DateToSQlString() + ") or (pog.date_start>=" + DateStartCheck.Hotels2DateToSQlString() + " and pog.date_end<=" + DateEndCheck.Hotels2DateToSQlString() + "))";
                //sqlCommand = sqlCommand + " and ((pog.date_start between pp.date_start and pp.date_end) or (pog.date_end between pp.date_start and pp.date_end))";
                //sqlCommand = sqlCommand + " and po.status=1";

                string sqlCommand = "select po.option_id,po.title,pocp.price as rate,pog.date_start,pog.date_end,pog.require_adult,pog.require_child,pog.default_gala,poc.title as condition_title,poc.condition_id,poc.num_adult,poc.num_children";
                sqlCommand = sqlCommand + " from tbl_product_option po,tbl_product_option_gala pog,tbl_product_option_condition_extra_net poc,tbl_product_option_condition_price_extranet pocp";
                sqlCommand = sqlCommand + " where po.option_id=pog.option_id and po.option_id=poc.option_id and poc.condition_id=pocp.condition_id  and po.product_id=" + ProductID + " and po.cat_id=47";
                sqlCommand = sqlCommand + " and ((pog.date_start between " + DateStartCheck.Hotels2DateToSQlString() + " and " + DateEndCheck.Hotels2DateToSQlString() + ") or (pog.date_end between " + DateStartCheck.Hotels2DateToSQlString() + " and " + DateEndCheck.Hotels2DateToSQlString() + "))";
                sqlCommand = sqlCommand + " and po.status=1";
                //HttpContext.Current.Response.Write(sqlCommand);
                List<GalaDinner> gala = new List<GalaDinner>();

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    gala.Add(new GalaDinner
                    {
                        OptionID = (int)reader["option_id"],
                        ConditionID = (int)reader["condition_id"],
                        Title = reader["title"].ToString(),
                        ConditionTitle = reader["condition_title"].ToString(),
                        Rate = (decimal)reader["rate"],
                        RateOwn = 0,
                        RateRack = 0,
                        DateUseStart = (DateTime)reader["date_start"],
                        DateUseEnd = (DateTime)reader["date_end"],
                        RequireAdult = (bool)reader["require_adult"],
                        RequireChild = (bool)reader["require_child"],
                        DefaultGala = (byte)reader["default_gala"],
                        NumAdult = (byte)reader["num_adult"],
                        NumChild = (byte)reader["num_children"]
                    });

                }
                return gala;
            }

        }
    }
}