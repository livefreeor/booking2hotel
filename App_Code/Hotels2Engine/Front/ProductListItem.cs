using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;
/// <summary>
/// Summary description for ProductListItem
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class ProductListItem:Hotels2BaseClass
    {
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string TitleDefault { get; set; }
        public string Title { get; set; }
        public string ShortDetail { get; set; }
        public decimal Rate { get; set; }
        public float Star { get; set; }
        public bool IsInternetFree { get; set; }
        public string FilenameMain { get; set; }
        public string FileDestination { get; set; }
        public string FolderDestination { get; set; }
        public short DestinationID { get; set; }
        public string DestinationTitle { get; set; }
        public string DestinationTitleDefault { get; set; }
        private byte _langID = 1;

        public byte LangID
        {
            set { _langID = value; }
        }

        public ProductListItem()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public List<ProductListItem> GetProductList(byte categoryID)
        {
            string fieldFilename = string.Empty;
            switch (categoryID)
            {
                case 32:
                    fieldFilename = "file_name_golf";
                    break;
                case 34:
                    fieldFilename = "file_name_day_trip";
                    break;
                case 36:
                    fieldFilename = "file_name_water_activity";
                    break;
                case 38:
                    fieldFilename = "file_name_show_event";
                    break;
                case 39:
                    fieldFilename = "file_name_health_check_up";
                    break;
                case 40:
                    fieldFilename = "file_name_spa";
                    break;

            }

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                //string sqlCommand = "select p.product_id,p.product_code,pc.title,MIN(pocp.rate) as rate,p.destination_id,dn.title as destination_title,d.folder_destination,dn." + fieldFilename + ",p.star,p.is_internet_free,pc.file_name_main,pc.detail_short";
                //sqlCommand = sqlCommand + " from tbl_destination d,tbl_destination_name dn,tbl_product p,tbl_product_content pc,tbl_product_period pp,tbl_product_option po,tbl_product_option_condition poc,tbl_product_option_condition_price pocp";
                //sqlCommand = sqlCommand + " where p.destination_id=d.destination_id and d.destination_id=dn.destination_id and p.product_id=pp.product_id and p.product_id=pc.product_id and p.product_id=po.product_id and po.option_id=poc.option_id and poc.condition_id=pocp.condition_id and pocp.period_id=pp.period_id";
                //sqlCommand = sqlCommand + " and p.cat_id=" + categoryID + " and p.status=1 and pp.date_end>GETDATE()";
                //sqlCommand = sqlCommand + " and p.destination_id="+destinationID+" and pc.lang_id=1 and dn.lang_id=1";
                //sqlCommand = sqlCommand + " group by p.product_id,p.product_code,pc.title,p.destination_id,dn.title,d.folder_destination,dn." + fieldFilename + ",p.star,p.is_internet_free,pc.file_name_main,pc.detail_short";
                //sqlCommand = sqlCommand + " having MIN(pocp.rate)>0";



                string sqlCommand = " select p.product_id,p.product_code,pc.title,";
                sqlCommand = sqlCommand + " (select spc.title from tbl_product_content spc where spc.product_id=p.product_id and spc.lang_id=" + _langID + ") as second_lang,";
                sqlCommand = sqlCommand + " (select spc.detail_short from tbl_product_content spc where spc.product_id=p.product_id and spc.lang_id=" + _langID + ") as detail_second_lang,";
                sqlCommand = sqlCommand + " (select sdn.title from tbl_destination_name sdn where d.destination_id=sdn.destination_id and sdn.lang_id=1) as destination_title_default,";
                sqlCommand = sqlCommand + " isnull((select min(spocp.rate) ";
                sqlCommand = sqlCommand + " from tbl_product_period spp,tbl_product_option spo,tbl_product_option_condition spoc,tbl_product_option_condition_price spocp ";
                sqlCommand = sqlCommand + " where spp.product_id=p.product_id and spp.supplier_id=p.supplier_price and spo.product_id=p.product_id and spo.option_id=spoc.option_id and spoc.condition_id=spocp.condition_id and spocp.period_id=spp.period_id  and spp.date_end>GETDATE() and spo.cat_id IN (48,52,53,54,55,56)  and spo.status=1 and spoc.status=1 and spocp.status=1";
                sqlCommand = sqlCommand + " ),0) as rate,p.destination_id,dn.title as destination_title,d.folder_destination,dn." + fieldFilename + ",p.star,p.is_internet_free,d.folder_destination+'-'+pcat.folder_cat+'/'+ pc.file_name_main as file_name_main,pc.detail_short ";
                sqlCommand = sqlCommand + " from tbl_destination d,tbl_destination_name dn,tbl_product p,tbl_product_content pc,tbl_product_category pcat";
                sqlCommand = sqlCommand + " where p.destination_id=d.destination_id and d.destination_id=dn.destination_id and p.product_id=pc.product_id and p.cat_id=pcat.cat_id ";
                sqlCommand = sqlCommand + " and p.cat_id=" + categoryID + " and p.status=1 ";
                sqlCommand = sqlCommand + " and pc.lang_id=1 and dn.lang_id="+_langID;
                sqlCommand = sqlCommand + " order by destination_title,point_popular desc";

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<ProductListItem> Items = new List<ProductListItem>();

                string productTitle=string.Empty;
                string productDetail = string.Empty;
                string filePath=string.Empty;

                while (reader.Read())
                {
                    filePath = reader["file_name_main"].ToString();
                    if(_langID==1)
                    {
                        productTitle = reader["title"].ToString();
                        productDetail=reader["detail_short"].ToString();
                        
                    }else{
                        productTitle = reader["second_lang"].ToString();
                        filePath = filePath.Replace(".asp", "-th.asp");
                        productDetail=reader["detail_second_lang"].ToString();
                        if (string.IsNullOrEmpty(productTitle))
                        {
                            productTitle = reader["title"].ToString();
                            productDetail = reader["detail_short"].ToString();
                        }
                        
                            
                       
                    }
                    Items.Add(new ProductListItem
                    {
                        ProductID = (int)reader["product_id"],
                        ProductCode = reader["product_code"].ToString(),
                        TitleDefault=reader["title"].ToString(),
                        Title = productTitle,
                        ShortDetail = productDetail,
                        Rate = (decimal)reader["rate"],
                        Star = (float)reader["star"],
                        IsInternetFree = (bool)reader["is_internet_free"],
                        FilenameMain = filePath,
                        FileDestination = reader[fieldFilename].ToString(),
                        FolderDestination = reader["folder_destination"].ToString(),
                        DestinationID = (short)reader["destination_id"],
                        DestinationTitle = reader["destination_title"].ToString(),
                        DestinationTitleDefault = reader["destination_title_default"].ToString()
                    });
                }
                return Items;
            }

            
        }
    }
}