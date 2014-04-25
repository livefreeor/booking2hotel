using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;

/// <summary>
/// Summary description for ProductPicType
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public enum Hotels2PicType : byte
    {
        OverView = 1,
        OverView_Small = 2,
        Product_browse = 3,
        Feature_hotel = 4,
        Popular_small = 5,
        Popular_larg = 6,
        Thump = 7,
        Larg = 8
    }
    public class ProductPicType : Hotels2BaseClass
    {
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();

        public static string getTypeTitleById(byte bytTypeId)
        {
            ProductPicType cpicType = new ProductPicType();
            using (SqlConnection cn = new SqlConnection(cpicType.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT type_id,title FROM tbl_product_pic_type WHERE type_id=@type_id", cn);
                cmd.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = bytTypeId;
                cn.Open();
                string strNull = "No Item";
                IDataReader reader = cpicType.ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return reader[1].ToString();
                else
                    return strNull;

            }
            
            
        }


        public Dictionary<byte, string> getPictureTypeIsHaveRecord(byte bytCatId,int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT type_id,title FROM tbl_product_pic_type", cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                Dictionary<byte, string> dicLsit = new Dictionary<byte, string>();
                ProductPic cProductPic = new ProductPic();
                while (reader.Read())
                {
                    var Pic = cProductPic.getProductPicList(bytCatId, (byte)reader[0], intProductId);
                    if (Pic.Count > 0)
                    {
                        dicLsit.Add((byte)reader[0], reader[1].ToString());
                    }
                }
                return dicLsit;
            }
            
        }

        public Dictionary<byte, string> getPictureTypeIsHaveRecord(byte bytCatId, int intProductId, int intOptionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT type_id,title FROM tbl_product_pic_type", cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                Dictionary<byte, string> dicLsit = new Dictionary<byte, string>();
                ProductPic cProductPic = new ProductPic();
                while (reader.Read())
                {
                    var Pic = cProductPic.getProductPicList(bytCatId, (byte)reader[0], intProductId, intOptionId);
                    if (Pic.Count > 0)
                    {
                        dicLsit.Add((byte)reader[0], reader[1].ToString());
                    }
                }
                return dicLsit;
            }
            
        }

        public Dictionary<byte, string> getPictureTypeIsHaveRecordConstruction(byte bytCatId, int intProductId, int intConstruction)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT type_id,title FROM tbl_product_pic_type", cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                Dictionary<byte, string> dicLsit = new Dictionary<byte, string>();
                ProductPic cProductPic = new ProductPic();
                while (reader.Read())
                {
                    var Pic = cProductPic.getProductPicListConstruction(bytCatId, (byte)reader[0], intProductId, intConstruction);
                    if (Pic.Count > 0)
                    {
                        dicLsit.Add((byte)reader[0], reader[1].ToString());
                    }
                }
                return dicLsit;
            }
        }

        public Dictionary<byte, string> getPictureTypeIsHaveRecordItinerary(byte bytCatId, int intProductId, int intItinerary)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT type_id,title FROM tbl_product_pic_type", cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                Dictionary<byte, string> dicLsit = new Dictionary<byte, string>();
                ProductPic cProductPic = new ProductPic();
                while (reader.Read())
                {
                    var Pic = cProductPic.getProductPicList(bytCatId, (byte)reader[0], intProductId, intItinerary);
                    if (Pic.Count > 0)
                    {
                        dicLsit.Add((byte)reader[0], reader[1].ToString());
                    }
                }
                return dicLsit;
            }
        }

        public Dictionary<byte, string> getPictureTypeAll(byte bytImageCat)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                Dictionary<byte, string> dicLsit = new Dictionary<byte, string>();
                StringBuilder query = new StringBuilder();
                query.Append("SELECT type_id,title FROM tbl_product_pic_type");
                
                switch (bytImageCat)
                {
                    //Product
                    case 1:
                        query.Append(" WHERE type_id NOT IN (2)");
                        
                        break;
                    //option
                    case 2:
                        query.Append(" WHERE type_id NOT IN (1, 3, 4, 5, 6)");
                        
                        break;
                    //construction
                    case 3:
                        query.Append(" WHERE type_id NOT IN (1, 2, 3, 5, 6)");
                        
                        break;
                    //itinerary
                    case 4:
                        query.Append(" WHERE type_id NOT IN (1, 2, 3, 5, 6)"); ;
                        
                        break;
                }
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    dicLsit.Add((byte)reader[0], reader[1].ToString());
                }
                
                return dicLsit;
            }
            
        }


    }
}