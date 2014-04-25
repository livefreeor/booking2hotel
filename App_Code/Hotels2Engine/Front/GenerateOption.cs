using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand.Front;
using Hotels2thailand.DataAccess;
using System.IO;
using Hotels2thailand;
/// <summary>
/// Summary description for GenerateOption
/// </summary>
public class GenerateOption:Hotels2BaseClass
{
	public GenerateOption()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void GenertarateRoomDetail(int productID,byte langID)
    {
        string RoomLayout = string.Empty;
        string ProductTitle = string.Empty;
        string ProductCode = string.Empty;
        int ProductID = 0;
        string Filename = string.Empty;
        string FolderName = string.Empty;
        string layoutPath = string.Empty;
        layoutPath = HttpContext.Current.Server.MapPath("/Layout/room_detail.html");

        StreamReader objReader = new StreamReader(layoutPath);
        string read = objReader.ReadToEnd();
        objReader.Close();


        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        {
            string sqlCommand = "select p.product_id,p.product_code,pc.title,";
            sqlCommand=sqlCommand+" (";
            sqlCommand=sqlCommand+" select spc.title  from tbl_product_content spc where spc.product_id=p.product_id and spc.lang_id="+langID;
            sqlCommand = sqlCommand + " )as second_lang,";
            sqlCommand=sqlCommand+" d.folder_destination+'-'+pcat.folder_cat+'/'+pc.file_name_main as file_name,d.folder_destination+'-'+pcat.folder_cat as folder_name,p.is_new_pic";
            sqlCommand = sqlCommand + " from tbl_product p,tbl_product_content pc,tbl_destination d,tbl_product_category pcat";
            sqlCommand = sqlCommand + " where p.product_id=pc.product_id and p.destination_id=d.destination_id and p.cat_id=pcat.cat_id  and pc.lang_id=1 and p.cat_id=29 and p.product_id=" + productID;
            sqlCommand = sqlCommand + " order by product_id asc";
            //HttpContext.Current.Response.Write(sqlCommand+"<br>");
            SqlCommand cmd = new SqlCommand(sqlCommand, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            RoomDetail roomDetail = new RoomDetail();
            List<RoomDetail> roomDetailList;
            string roomContent = string.Empty;
            string roomLayout = string.Empty;
            string keyword = string.Empty;
            string Header = string.Empty;
            string imgThumb = string.Empty;
            string thumbImage = string.Empty;
            string imgLarge = string.Empty;
            int countThumb = 0;
            FrontOptionFacility Facility = new FrontOptionFacility();
            List<FrontOptionFacility> FacilityList;

            string pageNotFound = string.Empty;
            pageNotFound = pageNotFound + "<html xmlns=\"http://www.w3.org/1999/xhtml\">\n";
            pageNotFound = pageNotFound + "<head>\n";
            pageNotFound = pageNotFound + "<meta name=\"robots\" content=\"noindex\">\n";
            pageNotFound = pageNotFound + "</head>\n";

            pageNotFound = pageNotFound + "<body>\n";
            pageNotFound = pageNotFound + "</body>\n";
            pageNotFound = pageNotFound + "</html>\n";

            while (reader.Read())
            {
                ProductID = (int)reader["product_id"];
                ProductTitle = reader["title"].ToString();
                Filename = reader["file_name"].ToString();
                if (langID==2)
                {
                    ProductTitle = reader["second_lang"].ToString();
                    Filename = reader["file_name"].ToString().Replace(".asp","-th.asp");

                    if(string.IsNullOrEmpty(ProductTitle))
                    {
                        ProductTitle = reader["title"].ToString();
                    }
                }
                
                ProductCode = reader["product_code"].ToString();
                
                FolderName = reader["folder_name"].ToString();
                roomDetailList = roomDetail.LoadOptionByProductID(ProductID,langID);

                foreach (RoomDetail item in roomDetailList)
                {
                    if (item.Status)
                    {
                        roomLayout = read;
                        roomContent = "";
                        Header = "";
                        countThumb = 0;
                        thumbImage = "";
                        FacilityList = Facility.LoadFacilityByOptionID(item.OptionID, langID);

                        keyword = Utility.GetKeywordReplace(roomLayout, "<!--###RoomHeaderContentStart###-->", "<!--###RoomHeaderContentEnd###-->");

                        Header = Header + "<div class=\"room_title\">\n";
                        //Header = Header + "<img src=\"/thailand-hotels-pic/" + ProductCode + "_logo.gif\" />\n";
                        Header = Header + "<div class=\"hotels_name\"><a href=\"#\">" + ProductTitle + "</a></div>\n";
                        Header = Header + "<br class=\"clear-all\" />\n";
                        Header = Header + "<div class=\"stork\"></div>\n";
                        Header = Header + "</div>\n";


                        roomLayout = roomLayout.Replace(keyword, Header);



                        keyword = Utility.GetKeywordReplace(roomLayout, "<!--###RoomcontentStart###-->", "<!--###RoomcontentEnd###-->");

                        roomContent = roomContent + "<div class=\"room_content\">\n";
                        FrontProductPicture productPicture = new FrontProductPicture();
                        List<FrontProductPicture> pictureList = new List<FrontProductPicture>();
                        pictureList = productPicture.GetOptionImageList(item.OptionID, 2, 7);


                        //HttpContext.Current.Response.End();
                        if ((bool)reader["is_new_pic"] && pictureList.Count > 0)
                        {

                            roomContent = roomContent + "<img src=\"" + pictureList[0].PicturePath.Replace("thumb_45_40", "larg_300_200") + "\" class=\"main_pic\">\n";

                            roomContent = roomContent + "<div class=\"thumbnail\">\n";
                            foreach (FrontProductPicture itemPic in pictureList)
                            {
                                imgThumb = itemPic.PicturePath;
                                imgLarge = itemPic.PicturePath.Replace("thumb_45_40", "larg_300_200");

                                if (File.Exists(HttpContext.Current.Server.MapPath(imgThumb)))
                                {
                                    thumbImage = thumbImage + "<a href=\"" + imgLarge + "\" class=\"imgFloat\"><img src=\"" + imgThumb + "\" /></a>\n";
                                    countThumb = countThumb + 1;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            roomContent = roomContent + thumbImage + "</div>\n";
                        }
                        else
                        {
                            if (File.Exists(HttpContext.Current.Server.MapPath("/thailand-hotels-pic/" + ProductCode + "_" + item.OptionID + "_a.jpg")))
                            {
                                //Response.Write("<img src=\"/thailand-hotels-pic/" + ProductCode + "_" + item.OptionID + "_a.jpg\" class=\"main_pic\">");
                                roomContent = roomContent + "<img src=\"/thailand-hotels-pic/" + ProductCode + "_" + item.OptionID + "_a.jpg\" class=\"main_pic\">\n";

                                roomContent = roomContent + "<div class=\"thumbnail\">\n";
                                for (int count = 1; count <= 40; count++)
                                {
                                    imgThumb = "/thailand-hotels-pic/" + ProductCode + "_" + item.OptionID + "_b_" + count + ".jpg";
                                    imgLarge = "/thailand-hotels-pic/" + ProductCode + "_" + item.OptionID + "_c_" + count + ".jpg";

                                    if (File.Exists(HttpContext.Current.Server.MapPath(imgThumb)))
                                    {
                                        thumbImage = thumbImage + "<a href=\"" + imgLarge + "\" class=\"imgFloat\"><img src=\"" + imgThumb + "\" /></a>\n";
                                        countThumb = countThumb + 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                }
                                roomContent = roomContent + thumbImage + "</div>\n";
                            }
                        }



                        roomContent = roomContent + "<p>" +  item.Detail.Hotels2XMlReader_IgnoreError() + "</p>\n";


                        roomContent = roomContent + "<br class=\"clear-all\" />\n";
                        roomContent = roomContent + "<div class=\"room_col\">\n";
                        if(langID==1)
                        {
                        roomContent = roomContent + "<p><span>Room Amenities</span></p> \n";
                        }else{
                        roomContent = roomContent + "<p><span>สิ่งอำนวยความสะดวกในห้องพัก</span></p> \n";
                        }
                        
                        roomContent = roomContent + "<ul>\n";
                        foreach (FrontOptionFacility facItem in FacilityList)
                        {
                            roomContent = roomContent + "<li>" + facItem.Title + "</li>\n";
                        }
                        roomContent = roomContent + "</ul>\n";
                        roomContent = roomContent + "</div>\n";
                        roomContent = roomContent + "<div class=\"room_col\">\n";
                        if(langID==1)
                        {
                        roomContent = roomContent + "<p><span>Other " + ProductTitle + " Room(s)</span></p> \n";
                        }else{
                        roomContent = roomContent + "<p><span>ห้องพักอื่นๆภายในโรงแรม" + ProductTitle + "</span></p> \n";
                        }
                        
                        roomContent = roomContent + "<ul>\n";
                        foreach (RoomDetail items in roomDetailList)
                        {
                            if (item.OptionID != items.OptionID)
                            {
                                roomContent = roomContent + "<li> <a href=\"/" + Filename.Replace(".asp", "_room_" + items.OptionID + ".asp") + "\">" + items.Title + "</a></li>\n";
                            }
                        }
                        roomContent = roomContent + "</ul>\n";
                        roomContent = roomContent + "</div>\n";
                        roomContent = roomContent + "<br class=\"clear-all\" />\n";
                        roomContent = roomContent + "</div>\n";

                        roomLayout = roomLayout.Replace(keyword, roomContent);
                        string pathFile = string.Empty;

                       
                        pathFile=HttpContext.Current.Server.MapPath("/" + Filename.Replace(".asp", "_room_" + item.OptionID + ".asp"));
                        //HttpContext.Current.Response.Write(Filename.Replace(".asp", "_room_" + item.OptionID + ".asp")+"<br>");
                       
                        
                        GenerateFile gf;
                        gf = new GenerateFile(pathFile, roomLayout);
                        
                    }
                    else
                    {
                        string pathFile = string.Empty;
                        
                            pathFile = HttpContext.Current.Server.MapPath("/" + Filename.Replace(".asp", "_room_" + item.OptionID + ".asp"));
                        
                        
                        GenerateFile gf;
                        gf = new GenerateFile(pathFile, pageNotFound);
                    }
                }

            }
        }
        
    }
}