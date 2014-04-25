using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for FrontItinerary
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontItinerary:Hotels2BaseClass
    {

        public int OptionID { get; set; }
        public string Title { get; set; }
        public DateTime Timestart { get; set; }
        public DateTime Timeend { get; set; }
        public string SubTitle { get; set; }
        public string Detail { get; set; }

        public FrontItinerary()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<object> GetItineraryProgram(int OptionID)
        { 
            string sqlCommand="select pri.option_id,pritc.title,prit.time_start,prit.time_end,pric.title,pric.detail";
            sqlCommand=sqlCommand+" from tbl_product_itinerary pri,tbl_product_itinerary_title_content pritc,tbl_product_itinerary_item prit,tbl_product_itinerary_content pric";
            sqlCommand=sqlCommand+" where pri.itinerary_id=pritc.itinerary_id ";
            sqlCommand=sqlCommand+" and pri.itinerary_id=prit.itinerary_id ";
            sqlCommand=sqlCommand+" and prit.itinerary_item_id=pric.itinerary_item_id";
            sqlCommand = sqlCommand + " and pri.option_id="+OptionID+" and pritc.lang_id=1 and prit.status=1 and pric.lang_id=1";
            sqlCommand = sqlCommand + " order by time_start asc";

            using(SqlConnection cn=new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
    }
}