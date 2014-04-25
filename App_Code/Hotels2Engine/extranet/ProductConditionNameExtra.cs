using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Hotels2thailand;
using Hotels2thailand.Staffs;


/// <summary>
/// Summary description for ProductCategory
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductConditionNameExtra : Hotels2BaseClass
    {

        public byte ConditionNameId { get; set; }
        public byte LangId { get; set; }
        public string Title { get; set; }
        public bool Status { get; set; }

        public List<object> GetConditionNameList(byte LangId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT cn.condition_name_id, cnl.lang_id, cnl.title,  cn.status FROM tbl_product_option_condition_name_extra_net cn, tbl_product_option_condition_name_lang_extra_net cnl");
            query.Append(" WHERE cn.condition_name_id = cnl.condition_name_id");
            query.Append(" AND cn.status = 1 AND cnl.lang_id = @lang_id");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = LangId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }


        public ProductConditionNameExtra GetConditionNameById(byte condition_name_id, byte bytLangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option_condition_name_lang_extra_net WHERE condition_name_id = @condition_name_id AND lang_id = @lang_id", cn);
                cmd.Parameters.Add("@condition_name_id", SqlDbType.TinyInt).Value = condition_name_id;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return (ProductConditionNameExtra)MappingObjectFromDataReader(reader);
                }
                else
                    return null;
                
            }
        }
    }
}