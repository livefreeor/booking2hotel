using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;

/// <summary>
/// Summary description for FrontProductOptionWeekDay
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontProductOptionWeekDay:Hotels2BaseClass
    {
        public int WeekDayId { get; set; }
        public int OptionID { get; set; }
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }

        public FrontProductOptionWeekDay()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<FrontProductOptionWeekDay> GetOptionWeekDayList(int ProductID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select	pow.weekdayId,pow.option_id,pow.day_sun,pow.day_mon,pow.day_tue,pow.day_wed,pow.day_thu,pow.day_fri,pow.day_sat";
                sqlCommand = sqlCommand + " from tbl_product_option po,tbl_product_option_is_week_day_all pow";
                sqlCommand = sqlCommand + " where po.option_id=pow.option_id and po.status=1 and po.product_id=" + ProductID;

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                List<FrontProductOptionWeekDay> result = new List<FrontProductOptionWeekDay>();
                while (reader.Read())
                {
                    result.Add(new FrontProductOptionWeekDay
                    {
                        WeekDayId = (int)reader["weekdayId"],
                        OptionID = (int)reader["option_id"],
                        Sunday = (bool)reader["day_sun"],
                        Monday = (bool)reader["day_mon"],
                        Tuesday = (bool)reader["day_tue"],
                        Wednesday = (bool)reader["day_wed"],
                        Thursday = (bool)reader["day_thu"],
                        Friday = (bool)reader["day_fri"],
                        Saturday = (bool)reader["day_sat"]
                    });
                }
                return result;
            }

            
        }
    }
}