using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
using System.Text;

/// <summary>
/// Summary description for Country
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class Country:Hotels2BaseClass
    {
        public byte CountryID { get; set; }
        public byte ContinentID { get; set; }
        public string CountryCode { get; set; }
        public string Title { get; set; }
        public string CodePhone { get; set; }
        public bool IsExtranet { get; set; }

        //private LinqProductionDataContext dcProduct = new LinqProductionDataContext();

        public IList<object> iListGetCountryAll()
        {
            Dictionary<byte, string> dataList = new Dictionary<byte, string>();

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT country_id,title FROM tbl_country ORDER BY title", cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

        }


        //optional key with code
        public Dictionary<string, string> GetCountryAllWithKeyCode()
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT (CONVERT(varchar(100), country_id)+','+ ISNULL( country_code,'')) AS countryKey,title FROM tbl_country ORDER BY title", cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    dataList.Add(reader["countryKey"].ToString(), reader["title"].ToString());
                }
            }

            return dataList;
        }
        public Dictionary<byte, string> GetCountryAll()
        {
            Dictionary<byte, string> dataList = new Dictionary<byte, string>();

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT country_id,title FROM tbl_country ORDER BY title", cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    dataList.Add((byte)reader["country_id"], reader["title"].ToString());
                }
            }

            return dataList;
        }

       
        public Country GetCountryById(int intCountryId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_country WHERE country_id=@country_id", cn);
                cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = intCountryId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return (Country)MappingObjectFromDataReader(reader);
                }
                else
                    return null;
            }
        }

        public  List<object> GetCountryByCOntinentID(byte continentId)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_country WHERE continent_id=@continent_id", cn);
                cmd.Parameters.Add("@continent_id", SqlDbType.TinyInt).Value = continentId;
                cn.Open();
                
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> GetCountryPromotionExtranet(int intPromotionId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT * FROM tbl_country WHERE Isextranet= 1 ");
            query.Append(" AND country_id NOT IN (");
            query.Append(" SELECT country_id FROM tbl_promotion_country_extra_net WHERE promotion_id = @promotion_id)");
            //query.Append("  tbl_product_option_condition_extra_net cone, tbl_product_option_condition_rate_plan_extra_net opcr");
            //query.Append(" WHERE op.product_id = @product_id AND ops.option_id = op.option_id AND ops.supplier_id = @supplier_id");
            //query.Append(" AND op.status = 1 AND op.cat_id = 38 AND cone.option_id = op.option_id AND cone.status = 1");
            //query.Append(" AND cone.condition_id = opcr.condition_id AND opcr.status = 1");

            //query.Append(") ORDER BY title");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                //cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }


        public List<object> GetCountryExtranet(short shrSupplierId, int intProductId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT * FROM tbl_country WHERE Isextranet= 1 ");
            query.Append(" AND country_id NOT IN ("); 
            query.Append(" SELECT DISTINCT(opcr.country_id) FROM tbl_product_option op , tbl_product_option_supplier ops,");
            query.Append("  tbl_product_option_condition_extra_net cone, tbl_product_option_condition_rate_plan_extra_net opcr");
            query.Append(" WHERE op.product_id = @product_id AND ops.option_id = op.option_id AND ops.supplier_id = @supplier_id");
            query.Append(" AND op.status = 1 AND op.cat_id = 38 AND cone.option_id = op.option_id AND cone.status = 1");
            query.Append(" AND cone.condition_id = opcr.condition_id AND opcr.status = 1");
         
            query.Append(") ORDER BY title");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> GetCountryExtranetAll()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_country WHERE Isextranet = 1 ORDER BY title", cn);
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public  IList<object> GetCountryByCOntinentID(byte continentId, string strParam)
        {
            
            string Query = string.Empty;
            if (string.IsNullOrEmpty(strParam))
                Query = "SELECT country_id, continent_id,country_code,title,code_phone FROM tbl_country WHERE continent_id=@continent_id";
            else
                Query = "SELECT country_id, continent_id,country_code,title,code_phone FROM tbl_country WHERE continent_id=@continent_id AND country_id NOT IN (" + strParam + ")";

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Query, cn);
                cmd.Parameters.Add("@continent_id", SqlDbType.TinyInt).Value = continentId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

      

        

        //============= CONTINENT =================
        public static IDictionary<byte, string> GetContinent()
        {
            Country cCountry = new Country();
            IDictionary<byte, string> Idic = new Dictionary<byte, string>();

            using (SqlConnection cn = new SqlConnection(cCountry.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT continent_id, title FROM tbl_continent", cn);
                cn.Open();
                IDataReader reader = cCountry.ExecuteReader(cmd);
                while (reader.Read())
                {
                    Idic.Add((byte)reader[0], reader[1].ToString());
                }
            }

            return Idic;
        }

    }
}