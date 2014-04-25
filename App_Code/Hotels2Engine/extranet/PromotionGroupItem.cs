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
    public class PromotionGroupItem : Hotels2BaseClass
    {
        public short ProItem { get; set; }
        public byte ProGroup { get; set; }
        public string ProGroupTitle { get; set; }
        public string Title { get; set; }
        public byte LangId { get; set; }
        public bool Status { get; set; }

        public List<object> getProItemByProGroupAndLangId(byte bytGroupId, byte bytLangId)
        {
            using(SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT pg.pro_group_item_id, pg.pro_group_id, pge.title, pgc.title, pgc.lang_id, pg.status FROM  tbl_promotion_group_item_extra_net pg, tbl_promotion_group_item_lang_extra_net pgc , tbl_promotion_group_extra_net pge WHERE pg.pro_group_id=pge.pro_group_id AND pg.pro_group_item_id = pgc.pro_group_item_id AND pgc.lang_id = @lang_id AND pg.pro_group_id=@pro_group_id AND pg.status = 1", cn);
                cmd.Parameters.Add("@pro_group_id", SqlDbType.TinyInt).Value = bytGroupId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public PromotionGroupItem getProgroupItemById(short ProGroupItem, byte byteLangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT pg.pro_group_item_id, pg.pro_group_id, pge.title, pgc.title, pgc.lang_id, pg.status FROM  tbl_promotion_group_item_extra_net pg, tbl_promotion_group_item_lang_extra_net pgc , tbl_promotion_group_extra_net pge WHERE pg.pro_group_id=pge.pro_group_id AND pg.pro_group_item_id = pgc.pro_group_item_id AND pgc.lang_id = @lang_id AND pg.pro_group_item_id=@pro_group_item_id", cn);

                cmd.Parameters.Add("@pro_group_item_id", SqlDbType.SmallInt).Value = ProGroupItem;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = byteLangId;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (PromotionGroupItem)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }

        public PromotionGroupItem getProgroupItemByPromotionId(int PromotionId, byte byteLangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT pg.pro_group_item_id, pg.pro_group_id, pge.title, pgc.title, pgc.lang_id, pg.status FROM  tbl_promotion_group_item_extra_net pg, tbl_promotion_group_item_lang_extra_net pgc , tbl_promotion_group_extra_net pge, tbl_promotion_extra_net pro WHERE pg.pro_group_id=pge.pro_group_id AND pg.pro_group_item_id = pgc.pro_group_item_id AND pgc.lang_id = @lang_id AND pg.pro_group_item_id=pro.pro_group_item_id AND pro.promotion_id = @promotion_id", cn);

                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = PromotionId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = byteLangId;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (PromotionGroupItem)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }

    }

    
}