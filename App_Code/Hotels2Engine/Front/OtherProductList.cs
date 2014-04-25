using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;


/// <summary>
/// Summary description for OtherProductList
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class OtherProductList
    {
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public byte CategoryID { get; set; }
        public string Title { get; set; }
        public short DestinationID { get; set; }
        public string FileNameMain { get; set; }
        private byte _langID = 1;

        public byte LangID
        {
            set { _langID = value; }
        }

        public OtherProductList()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        
        public List<OtherProductList> LoadProductList(byte categoryID)
        {
            DataConnect objConn = new DataConnect();
            string sqlCommand = "select distinct p.product_id,p.product_code,p.cat_id,pc.title,";
            sqlCommand = sqlCommand + " (select spc.title  from tbl_product_content spc where spc.product_id=p.product_id and spc.lang_id="+_langID+") as second_lang,";
            sqlCommand = sqlCommand + " d.destination_id,pc.file_name_main";
            sqlCommand = sqlCommand + " from tbl_destination d,tbl_product p,tbl_product_content pc,tbl_product_period pp,tbl_product_option po,tbl_product_option_condition poc,tbl_product_option_condition_price pocp";
            sqlCommand = sqlCommand + " where d.destination_id=p.destination_id and p.product_id=pp.product_id and p.product_id=pc.product_id and p.product_id=po.product_id and po.option_id=poc.option_id and poc.condition_id=pocp.condition_id and pocp.period_id=pp.period_id";
            sqlCommand = sqlCommand + " and p.cat_id=" + categoryID + " and p.status=1 and pc.lang_id=1";
            sqlCommand = sqlCommand + " order by d.destination_id";

            //HttpContext.Current.Response.Write(sqlCommand);

            SqlDataReader reader = objConn.GetDataReader(sqlCommand);
            string productTitle=string.Empty;
            string filePath=string.Empty;

            List<OtherProductList> Items = new List<OtherProductList>();
            while (reader.Read())
            {
                filePath = reader["file_name_main"].ToString();
                if(_langID==1)
                {
                    productTitle = reader["title"].ToString();
                    
                }else{
                    productTitle = reader["second_lang"].ToString();
                    filePath = filePath.Replace(".asp", "-th.asp");
                    if (string.IsNullOrEmpty(productTitle))
                    {
                        productTitle = reader["title"].ToString();
                    }
                    
                    
                }
                Items.Add(new OtherProductList
                {
                    ProductID = (int)reader["product_id"],
                    ProductCode=reader["product_code"].ToString(),
                    CategoryID = (byte)reader["cat_id"],
                    Title = productTitle,
                    DestinationID = (short)reader["destination_id"],
                    FileNameMain = filePath,
                });
            }
            objConn.Close();
            return Items;
        }
    }
}