using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using System.Data.SqlClient;

/// <summary>
/// Summary description for FrontRatePlanExtranet
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontRatePlanExtranet : Hotels2BaseClass
    {
        public int ConditionID { get; set; }
        public byte CountryID { get; set; }
        public byte RateCategory { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public decimal RateValue { get; set; }
        private List<FrontRatePlanExtranet> ratePlanList=new List<FrontRatePlanExtranet>();
        public int countItem
        {
            get { return ratePlanList.Count(); }
        }

        public void  LoadRatePlanByConditionGroup(string conditionGroup, DateTime dateStart, DateTime dateEnd)
        {
            ratePlanList = new List<FrontRatePlanExtranet>();

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select condition_id,country_id,rate_plan_cat_id,date_start,date_end,rate_value";
                sqlCommand = sqlCommand + " from tbl_product_option_condition_rate_plan_extra_net";
                sqlCommand = sqlCommand + " where condition_id IN (" + conditionGroup + ") and status=1";
                //sqlCommand = sqlCommand + " and ((date_start<=" + dateStart.Hotels2DateToSQlString() + " and date_end>=" + dateStart.Hotels2DateToSQlString() + ") or (date_start<=" + dateEnd.Hotels2DateToSQlString() + " and date_end>=" + dateEnd.Hotels2DateToSQlString() + ") or (date_start>=" + dateStart.Hotels2DateToSQlString() + " and date_end<=" + dateEnd.Hotels2DateToSQlString() + "))";

                //HttpContext.Current.Response.Write(sqlCommand);
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ratePlanList.Add(new FrontRatePlanExtranet
                    {
                        ConditionID = (int)reader["condition_id"],
                        CountryID = (byte)reader["country_id"],
                        RateCategory = (byte)reader["rate_plan_cat_id"],
                        DateStart = (reader["date_start"]== DBNull.Value? DateTime.Now :(DateTime)reader["date_start"]),
                        DateEnd = (reader["date_end"] == DBNull.Value ? DateTime.Now : (DateTime)reader["date_end"]),
                        RateValue = (decimal)reader["rate_value"]
                    });
                }
            }

        }

        public decimal CalculateRatePlan(byte countryID,decimal rateBase,int conditionID,DateTime dateCheck)
        {
            decimal result = rateBase;
            //HttpContext.Current.Response.Write(countryID+" "+rateBase+" "+conditionID+" "+dateCheck+"<Br>");
            foreach (FrontRatePlanExtranet item in this.ratePlanList)
            {
                //if (item.CountryID==countryID && item.ConditionID == conditionID && dateCheck.Subtract(item.DateStart).Days >= 0 && item.DateEnd.Subtract(dateCheck).Days >= 0)
                if (item.CountryID == countryID && item.ConditionID == conditionID)
                {
                    
                    switch(item.RateCategory)
                    {
                        case 1:
                            //discount baht
                            
                            result = rateBase - item.RateValue;
                            break;
                        case 2:
                            //discount percent
                            
                            result = (decimal)((float)rateBase * ((float)(100 - item.RateValue) / 100));
                            break;
                        case 3:
                            //charge baht
                            
                            result = rateBase + item.RateValue;
                            break;
                        case 4:
                            //charge percent
                            
                            result = (decimal)((float)rateBase * ((float)(100 + item.RateValue) / 100));
                            break;
                    }
                }
            }

            return result;
        }

    }
}