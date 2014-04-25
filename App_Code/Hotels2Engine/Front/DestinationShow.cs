using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text;
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
    public class DestinationShow : Hotels2BaseClass
    {
        public short DesId { get; set; }
        public string Title { get; set; }
        public string FileName_Hotel { get; set; }
        public string FileName_DayTrips { get; set; }
        public string FileName_Golf { get; set; }
        public string FileName_Heath { get; set; }
        public string FileName_Show { get; set; }
        public string FileName_Spa { get; set; }
        public string FileName_Water { get; set; }
        

        public List<object> GetDestinationShowPage_ByCatId(byte bytCatId, byte LangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();

                query.Append("select d.destination_id,dn.title, dn.file_name, dn.file_name_day_trip, dn.file_name_golf, dn.file_name_health_check_up, dn.file_name_show_event, dn.file_name_spa, dn.file_name_water_activity");
                query.Append(" from tbl_destination d, tbl_destination_name dn");
                query.Append(" where d.destination_id=dn.destination_id and dn.lang_id=@lang_id and (");
                query.Append(" select count(p.product_id)");
                query.Append(" from tbl_product  p");
                query.Append(" where p.cat_id=@cat_id  and p.destination_id=d.destination_id and p.status=1");
                query.Append(" )>0");
                query.Append(" order by dn.title");


                //query.Append("SELECT DISTINCT(dn.destination_id) , dn.title, dn.file_name, dn.file_name_day_trip, dn.file_name_golf, dn.file_name_health_check_up, dn.file_name_show_event, dn.file_name_spa, dn.file_name_water_activity");
                //query.Append(" FROM tbl_destination d, tbl_destination_name dn, tbl_product p, tbl_product_category pc");
                //query.Append(" WHERE d.destination_id = dn.destination_id AND d.destination_id = p.destination_id AND p.cat_id = pc.cat_id");
                //query.Append(" AND pc.cat_id = @cat_id AND dn.lang_id = @lang_id ORDER BY dn.title");
                ArrayList arrRet = new ArrayList();
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = LangId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));

               
            }
        }
    }
}