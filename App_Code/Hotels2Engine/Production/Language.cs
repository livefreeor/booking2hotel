using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using Hotels2thailand.LinqProvider.Production;

/// <summary>
/// Summary description for ProductNearByGroup
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class Language : Hotels2BaseClass
    {
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();

        public byte LanguageID { get; set; }
        public string Title { get; set; }
        public bool LangStaus { get; set; }

        public List<object> GetLanguageAll()
        {
            var result = from item in dcProduct.tbl_langs
                         where item.status == true
                         select item;

            return MappingObjectFromDataContextCollection(result);
        }

        public string LangTitle(byte bytLangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string title = string.Empty;
                SqlCommand cmd = new SqlCommand("SELECT title FROM tbl_lang WHERE lang_id=@lang_id", cn);
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    title = reader[0].ToString();
                }
                return title;
            }
        }
        //public Language GetLanguageById(int ProductOptionId)
        //{
        //    var result = dcProduct.tbl_product_option_contents.SingleOrDefault(poc => poc.option_id == OptionID && poc.lang_id == LanguageID);
        //    return (Language)MappingObjectFromDataContext(result);
        //}
    }
}