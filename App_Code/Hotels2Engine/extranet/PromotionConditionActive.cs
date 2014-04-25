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
    public class PromotionConditionActive : Hotels2BaseClass
    {
        public int OptionId { get; set; }
        public string OptionTitle { get; set; }
        public int ConditionId { get; set; }
        public string ConditionTitle { get; set; }
        public byte NumAdult { get; set; }
        public byte NumABF { get; set; }




        public IList<object> getActiveConditionPromotion(int intPromotionId, int intProductId, short shrSupplierId, byte bytLangId)
        {
            StringBuilder query = new StringBuilder();

            query.Append("SELECT op.option_id , opcon.title,  opc.condition_id , opt.title, opc.num_adult, opc.breakfast");
            query.Append(" FROM tbl_product_option op, tbl_product_option_content opcon, tbl_product_option_supplier os,  tbl_product_option_condition_extra_net opc,");
            query.Append(" tbl_promotion_condition_extra_net proco,");
            query.Append(" tbl_product_option_condition_title_lang_extra_net opt");

            query.Append(" WHERE op.option_id = os.option_id AND os.supplier_id=@supplier_id  AND opc.option_id = op.option_id");
            query.Append(" AND proco.condition_id = opc.condition_id AND proco.promotion_id = @promotion_id");
            query.Append(" AND opcon.option_id = op.option_id AND opcon.lang_id = @lang_id");
            query.Append(" AND opt.condition_id = opc.condition_id AND opt.lang_id = @lang_id");
            query.Append(" AND op.product_id=@product_id AND op.status=1 AND op.cat_id IN (38) AND opc.status = 1 AND proco.status = 1");
            query.Append(" ORDER BY op.title , opc.condition_id");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;

                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        

    }

    
}