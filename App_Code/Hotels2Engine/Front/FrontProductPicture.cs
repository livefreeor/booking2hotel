using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;

/// <summary>
/// Summary description for FrontProductPicture
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public enum GalleryDisplay : byte
    {
        Product = 1,
        Option = 2,
        Constuction = 3,
        Itinerary = 4

    }
    public class FrontProductPicture:Hotels2BaseClass
    {

        public int PictureID { get; set; }
        public string PicturePath { get; set; }
        public string Title { get; set; }
        public int ProductID { get; set; }
        public int? OptionID { get; set; }
        public int? ConstructionID { get; set; }
        public int? Itinerary { get; set; }
        public byte Priority { get; set; }

        private string errorMessage = string.Empty;

        public FrontProductPicture()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<FrontProductPicture> GetProductImageLarge(int productID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select pp.pic_id,pp.product_id,isnull(pp.option_id,0) as option_id,isnull(pp.construction_id,0) as construction_id,isnull(pp.itinerary_id,0) as itinerary_id,pp.pic_code,pp.priority,";
                sqlCommand = sqlCommand + " (";
                sqlCommand = sqlCommand + " select ppc.title from tbl_product_pic_content ppc where ppc.pic_id=pp.pic_id and ppc.lang_id=1";
                sqlCommand = sqlCommand + " )as title";
                sqlCommand = sqlCommand + " from tbl_product_pic pp";
                sqlCommand = sqlCommand + " where pp.type_id=8 and pp.product_id=" + productID;
                sqlCommand = sqlCommand + " order by priority";

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                List<FrontProductPicture> result = new List<FrontProductPicture>();

                while (reader.Read())
                {
                    result.Add(new FrontProductPicture
                    {
                        PictureID = (int)reader["pic_id"],
                        PicturePath = reader["pic_code"].ToString(),
                        Title = reader["title"].ToString(),
                        ProductID = (int)reader["product_id"],
                        OptionID = (int)reader["option_id"],
                        ConstructionID = (int)reader["construction_id"],
                        Itinerary = (int)reader["itinerary_id"],
                        Priority = (byte)reader["priority"]
                    });
                }
                return result;
            }
            

        }
        

        public string GenGelleryTable(List<FrontProductPicture> pictureList,GalleryDisplay categoryDisplay)
        {
            string result = string.Empty;
            string galleryList = string.Empty;
            string pictureTemp = string.Empty;

            int countPicture = 0;
            
            result = result + "<div class=\"hotel_thumb_old\">\n";
            result = result + "<ul>\n";
            result = result + "<!###Gallery###>";
            result = result + "</ul> \n";
            result = result + "</div>\n";
            result = result + "<br class=\"clear-all\" /><br />\n"; 
           
            switch (categoryDisplay)
            {
                case GalleryDisplay.Product:
                    result ="<h4 class=\"name\">Overview</h4>\n"+result;
                    foreach(FrontProductPicture item in pictureList)
                    {
                        if (item.PicturePath != pictureTemp)
                        {
                            if (item.OptionID == 0 && item.Itinerary == 0 && item.ConstructionID == 0)
                            {
                                galleryList = galleryList + "<li><img src=\"http://www.hotels2thailand.com" + item.PicturePath + "\" alt=\"" + item.Title + "\" /><br />" + item.Title + "</li>\n";
                                countPicture = countPicture + 1;
                            }
                        }
                        pictureTemp = item.PicturePath;

                        
                    }
                    if (countPicture != 0)
                    {
                        result = result.Replace("<!###Gallery###>", galleryList);
                    }
                    else
                    {
                        result = "";
                    }
                    break;
                case GalleryDisplay.Option:
                    result = "<h4 class=\"accom\">Accommodation</h4>\n" + result;
                    pictureList = pictureList.OrderByDescending(x => x.OptionID).ThenBy(x => x.Priority).ToList();
                    foreach(FrontProductPicture item in pictureList)
                    {
                        if (item.PicturePath != pictureTemp)
                        {
                            if (item.OptionID != 0)
                            {
                                galleryList = galleryList + "<li><img src=\"http://www.hotels2thailand.com" + item.PicturePath + "\" alt=\"" + item.Title + "\" /><br />" + item.Title + "</li>\n";
                                countPicture = countPicture + 1;
                            }
                        }
                        pictureTemp = item.PicturePath;
                       
                    }
                    if (countPicture != 0)
                    {
                        result = result.Replace("<!###Gallery###>", galleryList);
                    }
                    else
                    {
                        result = "";
                    }
                    break;
                case GalleryDisplay.Constuction:
                    result = "<h4 class=\"pool\">Construction</h4>\n" + result;
                    pictureList = pictureList.OrderByDescending(x => x.ConstructionID).ThenBy(x => x.Priority).ToList();
                    foreach(FrontProductPicture item in pictureList)
                    {
                        if (item.PicturePath != pictureTemp)
                        {
                            if (item.ConstructionID != 0)
                            {
                                galleryList = galleryList + "<li><img src=\"http://www.hotels2thailand.com" + item.PicturePath + "\" alt=\"" + item.Title + "\" /><br />" + item.Title + "</li>\n";
                                countPicture = countPicture + 1;
                            }
                        }
                        pictureTemp = item.PicturePath;
                        
                    }
                    if (countPicture != 0)
                    {
                        result = result.Replace("<!###Gallery###>", galleryList);
                    }
                    else
                    {
                        result = "";
                    }
                    break;
                case GalleryDisplay.Itinerary:
                    result = "<h4  class=\"fac\">Itinerary</h4>\n" + result;
                    pictureList = pictureList.OrderByDescending(x => x.Itinerary).ThenBy(x => x.Priority).ToList();
                    foreach(FrontProductPicture item in pictureList)
                    {
                        if (item.PicturePath != pictureTemp)
                        {
                            if (item.Itinerary != 0)
                            {
                                galleryList = galleryList + "<li><img src=\"http://www.hotels2thailand.com" + item.PicturePath + "\" alt=\"" + item.Title + "\" /><br />" + item.Title + "</li>\n";
                                countPicture = countPicture + 1;
                            }
                        }
                        pictureTemp = item.PicturePath;
                        
                    }

                    if (countPicture != 0)
                    {
                        result = result.Replace("<!###Gallery###>", galleryList);
                    }
                    else 
                    {
                        result = "";
                    }
                    
                    break;
            }
            return result;
        }

        public List<FrontProductPicture> GetProductImageList(int productID, byte cateID, byte typeID)
        {
            DataConnect objConn = new DataConnect();
            //string sqlCommand = "select pic_id,pic_code,title from tbl_product_pic where product_id=" + productID + " and cat_id=" + cateID + " and type_id=" + typeID;
            //sqlCommand = sqlCommand + " and status=1 order by priority asc";

            string sqlCommand = string.Empty;
            sqlCommand = sqlCommand + " select pp.pic_id,pp.pic_code,pp.priority,";
            sqlCommand = sqlCommand + " (select ppc.title from tbl_product_pic spp,tbl_product_pic_content ppc where spp.pic_id=ppc.pic_id and spp.pic_code=replace(pp.pic_code,'thumb_45_40','larg_300_200') and lang_id=1) as title";
            sqlCommand = sqlCommand + " from tbl_product_pic pp where pp.product_id=" + productID + " and pp.cat_id=" + cateID + " and pp.type_id=" + typeID;
            sqlCommand = sqlCommand + " and pp.status=1 order by pp.priority asc";


            SqlDataReader reader = objConn.GetDataReader(sqlCommand);
            List<FrontProductPicture> result = new List<FrontProductPicture>();

            while(reader.Read())
            {
                result.Add(new FrontProductPicture {
                    PictureID = (int)reader["pic_id"],
                    PicturePath = reader["pic_code"].ToString(),
                    Title = reader["title"].ToString(),
                    Priority = (byte)reader["priority"]
                });
            }
            return result;
        }

        public List<FrontProductPicture> GetOptionImageList(int optionID, byte cateID, byte typeID)
        {
            DataConnect objConn = new DataConnect();
            //string sqlCommand = "select pic_id,pic_code,title from tbl_product_pic where option_id=" + optionID + " and cat_id=" + cateID + " and type_id=" + typeID;
            //sqlCommand = sqlCommand + " and status=1 order by priority asc";

            string sqlCommand = string.Empty;
            sqlCommand = sqlCommand + " select pp.pic_id,pp.pic_code,pp.priority,";
            sqlCommand = sqlCommand + " (select ppc.title from tbl_product_pic spp,tbl_product_pic_content ppc where spp.pic_id=ppc.pic_id and spp.pic_code=replace(pp.pic_code,'thumb_45_40_1','larg_300_200') and lang_id=1) as title";
            sqlCommand = sqlCommand + " from tbl_product_pic pp where pp.option_id=" + optionID + " and pp.cat_id=" + cateID + " and pp.type_id=" + typeID;
            sqlCommand = sqlCommand + " and pp.status=1 order by pp.priority asc";

            //HttpContext.Current.Response.Write(sqlCommand+"<hr>");
            //HttpContext.Current.Response.Flush();
            SqlDataReader reader = objConn.GetDataReader(sqlCommand);
            List<FrontProductPicture> result = new List<FrontProductPicture>();

            while (reader.Read())
            {
                result.Add(new FrontProductPicture
                {
                    PictureID = (int)reader["pic_id"],
                    PicturePath = reader["pic_code"].ToString(),
                    Title = reader["title"].ToString(),
                    Priority = (byte)reader["priority"]
                });
            }
            return result;
        }
    }
}