using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class affiliate_include_RoomDetail : System.Web.UI.Page
{
    private string connString = WebConfigurationManager.ConnectionStrings["booking2hotelXConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        int optionID=int.Parse(Request.QueryString["opid"]);
        string roomTitle = string.Empty;
        double roomSize = 0;
        string picture = string.Empty;
        int totalImage = 0;
        using(SqlConnection cn = new SqlConnection(connString))
        {
            string strCommand = "select po.title,po.size,";
            strCommand=strCommand+" (select top 1 pic_code from tbl_product_pic where type_id=8 and option_id=po.option_id) as picture,";
            strCommand = strCommand + "(select count(pic_code) from tbl_product_pic where type_id=8 and option_id=po.option_id) as total_image";
            strCommand=strCommand+" from tbl_product_option po";
            strCommand = strCommand + " where po.option_id="+optionID;

            SqlCommand cmd = new SqlCommand(strCommand, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                roomTitle = reader["title"].ToString();
                roomSize = (double)reader["size"];
                picture=reader["picture"].ToString();
                totalImage=(int)reader["total_image"];
            }
        }

        FrontOptionFacility fac = new FrontOptionFacility();
        List<FrontOptionFacility> facList = fac.LoadFacilityByOptionID(optionID, 1);


        string result = string.Empty;

        //new
        result = result + "<div id=\"RoomDetailMain\">";
        result = result + "<div class=\"imageMain\">";
        result = result + "<img src=\"http://www.booking2hotels.com" + picture + "\" id=\"displayImageMain\"/>";
        result = result + "<h1 class=\"header1\">"+roomTitle+"</h1>";
        if (roomSize != 0)
        {
            result = result + "<span class=\"txtLight\">Size: " + roomSize + " sq.m.</span><br />";
        }
        result = result + "</div>";
        result = result + "<div class=\"thumbImagePan\">";

        int rowMaxImage = 1;
        if(totalImage>0)
        {
            for (int countImage = 1; countImage <= totalImage;countImage++ )
            {
                if(rowMaxImage%3==0){
                    result = result + "<br/>";
                }
                result = result + "<img src=\"http://www.booking2hotels.com" + picture.Replace("1.jpg",countImage+".jpg") + "\" width=\"80\" height=\"53\" class=\"thumbnailOption\" />";
            }
        }

        result = result + "<br />";

        result = result + "<span class=\"header3\">Room Facilities</span>";
        result = result + "<ul class=\"noBullet\">";
        foreach (FrontOptionFacility item in facList)
        {
            result = result + "<li>" + item.Title + "</li>";
        }
        
        result = result + "</ul>";
        result = result + "<a href=\"javascript:void(0)\" onclick=\"ModalBoxClose();\" style=\"display:block; width:120px; height:30px; line-height:30px; background-color:#06C; text-align:center; text-decoration:none; color:#fff; border:1px solid #09C;float:left;\">Close</a>";
        result = result + "</div>";
        result = result + "<br class=\"clear_all\"/>";
        result = result + "</div>";
        //new

        //result = result + "<div id=\"b2hFacilityBox\">";
        //result=result+"<h1>"+roomTitle+"</h1>";
        //if(!string.IsNullOrEmpty(picture))
        //{
        //    result = result + "<img src=\"http://www.booking2hotels.com"+picture+"\" class=\"mainImage\" />";
        //}
        //result=result+"<div id=\"b2hRoomDetailBox\">";
        //if(roomSize!=0)
        //{
        //    result = result + "<span>Size: </span>" + roomSize + " mm<sup>2</sup>";
        //    result = result + "<br /><br />";
        //}
        
        //result=result+"<h2>Room Facility: </h2>";
        //result=result+"<ul id=\"b2hFacility\">";

        //foreach (FrontOptionFacility item in facList)
        //{
        //    result=result+"<li>"+item.Title+"</li>";
        //}

        //result=result+"</ul>";
        //result=result+"</div>";
        //result=result+"<br class=\"clearAll\" />";
        //result=result+"<br />";
        //result = result + "<a href=\"javascript:void(0)\" onclick=\"ModalBoxClose();\" style=\"display:block; width:120px; height:30px; line-height:30px; background-color:#06C; text-align:center; text-decoration:none; color:#fff; border:1px solid #09C;float:right;\">Close</a>";
        //result=result+"</div>";



        
        //new
        string addEventToThumb = string.Empty;
        Response.Write("modalBox(600, '"+result+"');");
        addEventToThumb = addEventToThumb + "$('.thumbnailOption').each(function(index, element) {";
        addEventToThumb = addEventToThumb + "$(this).hover(function(){";
        addEventToThumb = addEventToThumb + "$('#displayImageMain').attr('src',$(this).attr('src'));";
        addEventToThumb = addEventToThumb + "});";
        addEventToThumb = addEventToThumb + "});";
        Response.Write(addEventToThumb);
        //Response.Write("alert('hello');");
    }
}