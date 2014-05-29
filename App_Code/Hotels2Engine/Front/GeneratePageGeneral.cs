using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.Front;
using System.IO;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;
using Hotels2thailand;

/// <summary>
/// Summary description for GeneratePageGeneral
/// </summary>
public class GeneratePageGeneral
{
    private byte categoryID;
    private short destinationID;
    public byte LangID { get; set; }

	public GeneratePageGeneral()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string GetStreamReader(string path)
    {
        StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath(path));
        string read = objReader.ReadToEnd();
        objReader.Close();
        return read;
    }

    public void GenPageIndex()
    {
        //string seo = SEO_GenerateV2.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Home, LangID, 0,"","", "", "", "","", "", "");

        //HttpContext.Current.Response.Write("Hello"+seo);
        //HttpContext.Current.Response.End();
        FrontLastReview review = new FrontLastReview();
        review.LangID = LangID;
        List<FrontLastReview> lastreview = review.GetLastReview(5);

       

        string reviewLast = string.Empty;
        string reviewContent = string.Empty;
        string fileName = string.Empty;

        int countReview = 1;
        foreach (FrontLastReview item in lastreview)
        {
            if (countReview <= 2)
            {
                fileName = item.Filename.Replace(".asp", "_review.asp");
                reviewContent = item.Detail;
                if (reviewContent.Length > 120)
                {
                    reviewContent = reviewContent.ToString().Substring(0, 60) + "...\n";
                }
                reviewLast = reviewLast + "<li> <a href=\"javascript:void(0)\" onclick=\"window.location.href='" + item.Filename.Replace("'","\'") + "'\">" + item.Title + "</a><br class=\"clear-all\" />\n";
                reviewLast = reviewLast + "<div class=\"class_star_s_" + Utility.GetHotelClassImage(Convert.ToDouble(item.Star), 1) + "\"></div> <br class=\"clear-all\" />\n";
                reviewLast = reviewLast + "<p>" + reviewContent;
                if(LangID==1)
                {
                reviewLast = reviewLast + "<br/><a href=\"javascript:void(0)\"  class=\"read_more\" onclick=\"window.location.href='" + item.Filename + "'\">Read More</a>\n";
                }else{
                reviewLast = reviewLast + "<br/><a href=\"javascript:void(0)\"  class=\"read_more\" onclick=\"window.location.href='" + item.Filename + "'\">อ่านต่อ</a>\n";
                }
                
                reviewLast = reviewLast + "</p>\n";
                reviewLast = reviewLast + "</li>\n";
            }
            else {
                break;
            }
            countReview = countReview + 1;
            
        }

        string layout = string.Empty;
        if(LangID==1)
        {
            layout = GetStreamReader("/Layout/index.html");
        }else{
            layout = GetStreamReader("/Layout-TH/index.html");
        }
            
        //Response.End();

        string Keyword = Utility.GetKeywordReplace(layout, "<!--##@TopHotelDestinationStart##-->", "<!--##@TopHotelDestinationEnd##-->");

        string hotDestination = string.Empty;
        if(LangID==1)
        {
            hotDestination = GetStreamReader("/Layout/top_hotel_destination.html");
        }else{
            hotDestination = GetStreamReader("/Layout-TH/top_hotel_destination.html");
        }
        

        layout = layout.Replace(Keyword, hotDestination);
        Keyword = Utility.GetKeywordReplace(layout, "<!--##reviewLastStart##-->", "<!--##reviewLastEnd##-->");
        layout = layout.Replace(Keyword, reviewLast);

        string reviewOther = string.Empty;
        reviewOther = reviewOther + "<div class=\"latest_reviews_more\">";

        if (LangID == 1)
        {
            reviewOther = reviewOther + "<h3>Latest Reviews</h3>";
        }
        else {
            reviewOther = reviewOther + "<h3>รีวิวล่าสุด</h3>";
        }

        



        countReview = 1;
        foreach (FrontLastReview item in lastreview)
        {

            if(countReview>2)
            {
                if (countReview % 2 == 0)
                {
                    if(countReview==lastreview.Count)
                    {
                        reviewOther = reviewOther + "<div class=\"review_b_end\">";
                    }else{
                        reviewOther = reviewOther + "<div class=\"review\">";
                    }
                }else {
                    if(countReview==lastreview.Count)
                    {
                        reviewOther = reviewOther + "<div class=\"review_b_end\">";
                    }else{
                        reviewOther = reviewOther + "<div class=\"review_b\">";
                    }
                    
                }

                

                if(LangID==1)
                {
                    reviewOther = reviewOther + "<div class=\"title_review\"><a href=\"javascript:void(0)\" onclick=\"window.location.href='" + item.Filename + "'\">" + item.Title + "</a></div>";
                    reviewOther = reviewOther + "<div class=\"detail_review\">";
                    reviewOther = reviewOther + "<div class=\"by_review\"><span>by : </span>"+item.Fullname+"</div>";
                    reviewOther = reviewOther + "<div class=\"by_review\"><span>from : </span>"+item.ReviewFrom+"</div>";
                    reviewOther = reviewOther + "<div class=\"by_review\"><span>date: </span>" + item.DateSubmit.ToString("MMMM dd yyyy") + "</div>";
                    reviewOther = reviewOther + "</div>";
                }else{
                    reviewOther = reviewOther + "<div class=\"title_review\"><a href=\"javascript:void(0)\" onclick=\"window.location.href='" + item.Filename + "'\">" + item.Title + "</a></div>";
                    reviewOther = reviewOther + "<div class=\"detail_review\">";
                    reviewOther = reviewOther + "<div class=\"by_review\"><span>รีวิวโดย : </span>"+item.Fullname+"</div>";
                    reviewOther = reviewOther + "<div class=\"by_review\"><span>จาก : </span>"+item.ReviewFrom+"</div>";
                    reviewOther = reviewOther + "<div class=\"by_review\"><span>วันที่: </span>" + item.DateSubmit.ToString("MMMM dd yyyy") + "</div>";
                    reviewOther = reviewOther + "</div>";
                }
                reviewOther = reviewOther + "<div class=\"content_review\">"+item.Detail+"</div>";
                reviewOther = reviewOther + "<div class=\"clear-all\"></div>";
                reviewOther = reviewOther + "</div>";

               

            }
            countReview = countReview + 1;
            
        }

        reviewOther = reviewOther + "</div>";

        Keyword = Utility.GetKeywordReplace(layout, "<!--###ReviewOtherStart###-->", "<!--###ReviewOtherEnd###-->");
        layout = layout.Replace(Keyword, reviewOther);
        Keyword = Utility.GetKeywordReplace(layout, "<!--###FlagLanguageStart###-->", "<!--###FlagLanguageEnd###-->");
        string flagLanguage = string.Empty;
        //if (LangID == 1)
        //{
            flagLanguage = "<a href=\"http://www.hotels2thailand.com/\" alt=\"English\"><img src=\"../theme_color/blue/images/flag/flagENG.png\" width=\"25\" height=\"20\" style=\"float:left; margin:0 7px 0 25px;\" /></a>";
            flagLanguage = flagLanguage + "<a href=\"http://thai.hotels2thailandnew.com/\" alt=\"Thai\"><img src=\"../theme_color/blue/images/flag/flagTH.png\"  style=\"float:left; margin:0px;\" /></a>";

        //}
        //else
        //{
            //flagLanguage = "<a href=\"index-th.asp\"><img src=\"../theme_color/blue/images/flag/flagENG.png\" width=\"25\" height=\"20\" style=\"float:left; margin:0 7px 0 25px;\" /></a>";
            //flagLanguage = flagLanguage + "<a href=\"index.asp\"><img src=\"../theme_color/blue/images/flag/flagTH.png\"  style=\"float:left; margin:0px;\" /></a>";
        //}
        layout = layout.Replace(Keyword, flagLanguage);
        layout = layout.Replace("<!--##IncludeASPFile##-->", WebStatPlugIn.TrackingIncludeFunction());
        layout = layout.Replace("<!--##Tracking##-->", WebStatPlugIn.TrackingScript(1, 0, 1));

        //HttpContext.Current.Response.Write(layout);
        //HttpContext.Current.Response.End();
        GenerateFile gf;
        if(LangID==1)
        {
            gf = new GenerateFile(HttpContext.Current.Server.MapPath("/"), "index.asp", layout);
        }else{
            gf = new GenerateFile(HttpContext.Current.Server.MapPath("/"), "index-th.asp", layout);
        }
        
        gf.CreateFile();

    }

    public void GenPageListLocationNew()
    {
        string MetaTag = "";
        string MetaTitle = "";
        string MetaAlt = "";
        string MetaH1 = "";

        string sqlCommand = "select l.location_id,l.destination_id,ln.title as location_title,ln.file_name,dn.title as destination_title";
        sqlCommand = sqlCommand + " ,(SELECT COUNT(spl.product_id) FROM tbl_product_location spl, tbl_product sp WHERE l.location_id=spl.location_id AND sp.product_id=spl.product_id AND sp.status=1 AND sp.cat_id=29) AS num_hotel";
        sqlCommand = sqlCommand + " from tbl_location l,tbl_location_name ln,tbl_destination d,tbl_destination_name dn";
        sqlCommand = sqlCommand + " where l.location_id=ln.location_id and ln.lang_id=" + LangID + " and d.destination_id=dn.destination_id and l.destination_id=d.destination_id and dn.lang_id=" + LangID + " and l.status=1";
        sqlCommand = sqlCommand + " order by ln.file_name asc";

        Navigator nav = new Navigator();
        nav.LangID = LangID;
        nav.LoadLocationLink();

        //StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath("/Layout/list_old_version.html"));
        //string layout = objReader.ReadToEnd();
        //objReader.Close();
        string layout = string.Empty;
        if(LangID==1)
        {
            layout = GetStreamReader("/Layout/list_old_version.html");
        }else{
            layout = GetStreamReader("/Layout-th/list_old_version.html");
        }
        
        //Response.Write(layout);
        //Response.End();
        DataConnect objConn = new DataConnect();
        SqlDataReader reader = objConn.GetDataReader(sqlCommand);

        short destinationID = 0;
        short locationID = 0;
        byte categoryID = 0;


        string Keyword = string.Empty;
        string Content = string.Empty;
        string flagLanguage = string.Empty;
        GenerateFile gf;
        while (reader.Read())
        {
            destinationID = (short)reader["destination_id"];
            locationID = (short)reader["location_id"];
            categoryID = 29;

            Keyword = Utility.GetKeywordReplace(layout, "<!--##@NavigatorStart##-->", "<!--##@NavigatorEnd##-->");
            Content = layout.Replace(Keyword, nav.GenNavigator(categoryID, destinationID, locationID));

            if(LangID==1)
            {
                Content = Content.Replace("<!--##SearchType###-->", reader["destination_title"].ToString() + " hotels");
            }else{
                Content = Content.Replace("<!--##SearchType###-->", reader["destination_title"].ToString());
            }
            
            Keyword = Utility.GetKeywordReplace(Content, "<!--##@HotelListStart##-->", "<!--##@HotelListEnd##-->");
            Content = Content.Replace(Keyword, "<div id=\"pList\"></div>");

            Keyword = Utility.GetKeywordReplace(Content, "<!--##@PopularDestinationStart##-->", "<!--##@PopularDestinationEnd##-->");

            Content = Content.Replace(Keyword, "");


            Keyword = Utility.GetKeywordReplace(Content, "<!--##@PopularLocationStart##-->", "<!--##@PopularLocationEnd##-->");
            Content = Content.Replace(Keyword, GetDestinationProductPopular(destinationID, categoryID));
            //Content = Content.Replace(Keyword, "");

            Keyword = Utility.GetKeywordReplace(Content, "<!--##@dateDefaultStart##-->", "<!--##@dateDefaultEnd##-->");

            Keyword = Utility.GetKeywordReplace(layout, "<!--###FlagLanguageStart###-->", "<!--###FlagLanguageEnd###-->");
            if (LangID == 1)
            {
                flagLanguage = "<a href=\"" + reader["file_name"].ToString() + "\" alt=\"English\"><img src=\"../theme_color/blue/images/flag/flagENG.png\" width=\"25\" height=\"20\" style=\"float:left; margin:0 7px 0 25px;\" /></a>";
                flagLanguage = flagLanguage + "<a href=\"" + reader["file_name"].ToString().Replace(".asp", "-th.asp") + "\" alt=\"Thai\"><img src=\"../theme_color/blue/images/flag/flagTH.png\"  style=\"float:left; margin:0px;\" /></a>";

            }
            else
            {
                flagLanguage = "<a href=\"http://www.hotels2thailandnew.com/" + reader["file_name"].ToString().Replace("-th.asp", ".asp") + "\" alt=\"English\"><img src=\"../theme_color/blue/images/flag/flagENG.png\" width=\"25\" height=\"20\" style=\"float:left; margin:0 7px 0 25px;\" /></a>";
                flagLanguage = flagLanguage + "<a href=\"http://thai.hotels2thailandnew.com/" + reader["file_name"].ToString() + "\" alt=\"Thai\"><img src=\"../theme_color/blue/images/flag/flagTH.png\"  style=\"float:left; margin:0px;\" /></a>";
            }

            Content = Content.Replace(Keyword, flagLanguage);
            //string hiddenDate = "<br class=\"clear-all\" /><li><input type=\"text\" name=\"dateStart\" id=\"dateStartci\"  rel=\"datepicker\" style=\"width:145px; background:#edf2fa; height:18px; float:left; line-height:25px;\"></li><br class=\"clear-all\" />";
            //if (categoryID == 29)
            //{
            //    hiddenDate = hiddenDate + "<br class=\"clear-all\" /><li><input type=\"text\" name=\"dateEnd\" id=\"dateStartco\"  rel=\"\" style=\"width:145px; background:#edf2fa; height:18px; float:left;\" /></li><br class=\"clear-all\" />";
            //}

            //Content = Content.Replace(Keyword, hiddenDate);

            MetaTag = SEO_GenerateV2.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Location, LangID, categoryID, reader["destination_title"].ToString(), reader["location_title"].ToString(), "", "", "", reader["num_hotel"].ToString(), "", "");
            if(LangID==1)
            {
                MetaTag = MetaTag + SEO_GenerateV2.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Location, categoryID, "http://www.hotels2thailand.com/" + reader["file_name"].ToString());
            }else{
                MetaTag = MetaTag + SEO_GenerateV2.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Location, categoryID, "http://thai.hotels2thailand.com/" + reader["file_name"].ToString());
            }
            
            MetaTitle = SEO_GenerateV2.Function_gen_optimize_string(SEO_StringType.Title, SEO_PageType.Location, LangID, categoryID, reader["destination_title"].ToString(), reader["location_title"].ToString(), "", "", "", reader["num_hotel"].ToString(), "", "");
            MetaAlt = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Link_Back_To_Home, SEO_PageType.Location, categoryID, "", reader["location_title"].ToString(), "");
            MetaH1 = SEO_Generate.Function_gen_optimize_string(SEO_StringType.H1, SEO_PageType.Location, categoryID, reader["destination_title"].ToString(), reader["location_title"].ToString(), "");

            Keyword = Utility.GetKeywordReplace(Content, "<!--##@TopHotelDestinationStart##-->", "<!--##@TopHotelDestinationEnd##-->");
            Content = Content.Replace(Keyword, GetLocationInDestination((short)reader["destination_id"], (short)reader["location_id"]));
            Content = Content.Replace("<!--##title_tag##-->", MetaTitle);
            Content = Content.Replace("<!--##meta_tag##-->", MetaTag);
            Content = Content.Replace("<!--##AltIndex##-->", MetaAlt);
            Content = Content.Replace("<!--##H1##-->", MetaH1);
            Content = Content.Replace("<!--##@DestDefault##-->", "<input type=\"hidden\" name=\"page_type\" id=\"page_type\" value=\"list\" />\n<input type=\"hidden\" name=\"destDefault\" id=\"destDefault\" value=\"" + destinationID + "\" />");
            Content = Content.Replace("<!--##@LocDefault##-->", "<input type=\"hidden\" name=\"locDefault\" id=\"locDefault\" value=\"" + locationID + "\" />");
            Content = Content.Replace("<!--##@CateDefault##-->", "<input type=\"hidden\" name=\"category\" id=\"category\" value=\"" + categoryID + "\" />");

            Content = Content.Replace("<!--##IncludeASPFile##-->", WebStatPlugIn.TrackingIncludeFunction());
            Content = Content.Replace("<!--##Tracking##-->", WebStatPlugIn.TrackingScript(15, locationID, 1));



            //Content=Content.Replace("http://www.hotels2thailand.com","http://174.36.32.56");
            gf = new GenerateFile(HttpContext.Current.Server.MapPath("/"), reader["file_name"].ToString(), Content);
            gf.CreateFile();

            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.End();
        }
        objConn.Close();
    }

    public void GenProductDestinationAllPage()
    {
       
        string indexPath = "index.asp";

        if(LangID==2)
        {
            indexPath = "index-th.asp";
        }

        string sqlCommand = "select d.destination_id,dn.title,d.folder_destination+'-hotels/"+indexPath+"' as file_name ";
        sqlCommand=sqlCommand+" from tbl_destination d,tbl_destination_name dn";
        sqlCommand = sqlCommand + " where d.destination_id=dn.destination_id and dn.lang_id="+LangID;

        
        Navigator nav = new Navigator();
        nav.LangID = LangID;
        nav.LoadDestinationLink(29);

        string layoutPath = string.Empty;
            if(LangID==1)
            {
                layoutPath = HttpContext.Current.Server.MapPath("/Layout/list_old_version.html");
            }else{
                layoutPath = HttpContext.Current.Server.MapPath("/Layout-TH/list_old_version.html");
            }
            StreamReader objReader = new StreamReader(layoutPath);
        string layout = objReader.ReadToEnd();
        objReader.Close();

        //Response.Write(layout);
        //Response.End();
        DataConnect objConn = new DataConnect();
        SqlDataReader reader = objConn.GetDataReader(sqlCommand);

        short destinationID = 0;
        byte categoryID = 0;


        string Keyword = string.Empty;
        string Content = string.Empty;
        string flagLanguage = string.Empty;
        GenerateFile gf;
        while (reader.Read())
        {
            HttpContext.Current.Response.Write(reader["title"]+"<br>");
            destinationID = (short)reader["destination_id"];
            categoryID = 29;

            Keyword = Utility.GetKeywordReplace(layout, "<!--##@NavigatorStart##-->", "<!--##@NavigatorEnd##-->");
            Content = layout.Replace(Keyword, nav.GenNavigator(categoryID, destinationID));

            if(LangID==1)
            {
                Content = Content.Replace("<!--##SearchType###-->", reader["title"].ToString() + " hotels");
            }else{
                Content = Content.Replace("<!--##SearchType###-->", reader["title"].ToString() + "");
            }
           

            Keyword = Utility.GetKeywordReplace(Content, "<!--##@HotelListStart##-->", "<!--##@HotelListEnd##-->");
            Content = Content.Replace(Keyword, "<div id=\"pList\"></div>");

            Keyword = Utility.GetKeywordReplace(Content, "<!--##@PopularDestinationStart##-->", "<!--##@PopularDestinationEnd##-->");

            Content = Content.Replace(Keyword, "");


            Keyword = Utility.GetKeywordReplace(Content, "<!--##@PopularLocationStart##-->", "<!--##@PopularLocationEnd##-->");
            Content = Content.Replace(Keyword, GetDestinationProductPopular(destinationID, categoryID));
            //Content = Content.Replace(Keyword, "");

            Keyword = Utility.GetKeywordReplace(Content, "<!--##@dateDefaultStart##-->", "<!--##@dateDefaultEnd##-->");

            //string hiddenDate = "<br class=\"clear-all\" /><li><input type=\"text\" name=\"dateStart\" id=\"dateStartci\"  rel=\"datepicker\" style=\"width:145px; background:#edf2fa; height:18px; float:left; line-height:25px;\"></li><br class=\"clear-all\" />";
            //if (categoryID == 29)
            //{
            //    hiddenDate = hiddenDate + "<br class=\"clear-all\" /><li><input type=\"text\" name=\"dateEnd\" id=\"dateStartco\"  rel=\"\" style=\"width:145px; background:#edf2fa; height:18px; float:left;\" /></li><br class=\"clear-all\" />";
            //}

            //Content = Content.Replace(Keyword, hiddenDate);

           
            Keyword = Utility.GetKeywordReplace(layout, "<!--###FlagLanguageStart###-->", "<!--###FlagLanguageEnd###-->");

            if (LangID==1)
            {
                flagLanguage = "<a href=\"http://www.hotels2thailandnew.com//" + reader["file_name"].ToString() + "\" alt=\"English\"><img src=\"../theme_color/blue/images/flag/flagENG.png\" width=\"25\" height=\"20\" style=\"float:left; margin:0 7px 0 25px;\" /></a>";
                flagLanguage = flagLanguage + "<a href=\"http://thai.hotels2thailandnew.com/" + reader["file_name"].ToString().Replace(".asp", "-th.asp") + " alt=\"Thai\"><img src=\"../theme_color/blue/images/flag/flagTH.png\"  style=\"float:left; margin:0px;\" /></a>";
            }else{
                flagLanguage = "<a href=\"http://www.hotels2thailand.com//" + reader["file_name"].ToString().Replace("-th.asp", ".asp") + "\" alt=\"English\"><img src=\"../theme_color/blue/images/flag/flagENG.png\" width=\"25\" height=\"20\" style=\"float:left; margin:0 7px 0 25px;\" /></a>";
                flagLanguage = flagLanguage + "<a href=\"http://thai.hotels2thailandnew.com/" + reader["file_name"].ToString() + "\" alt=\"Thai\"><img src=\"../theme_color/blue/images/flag/flagTH.png\"  style=\"float:left; margin:0px;\" /></a>";
            }

            
            




            Content = Content.Replace(Keyword, flagLanguage);

            Keyword = Utility.GetKeywordReplace(Content, "<!--##@TopHotelDestinationStart##-->", "<!--##@TopHotelDestinationEnd##-->");
            Content = Content.Replace(Keyword, GetLocationInDestination((short)reader["destination_id"]));
            string MetaTag = string.Empty;
            MetaTag = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Destination, categoryID, reader["title"].ToString(), "", "");
            if(LangID==1)
            {
                MetaTag = MetaTag + SEO_Generate.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Destination, categoryID, "http://www.hotels2thailand.com/" + reader["file_name"].ToString());
            }else{
                MetaTag = MetaTag + SEO_Generate.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Destination, categoryID, "http://thai.hotels2thailand.com/" + reader["file_name"].ToString());
            }
            

            Content = Content.Replace("<!--##meta_tag##-->", MetaTag);
            Content = Content.Replace("<!--##title_tag##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Title, SEO_PageType.Destination, categoryID, reader["title"].ToString(), "", ""));
            Content = Content.Replace("<!--##AltIndex##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Link_Back_To_Home, SEO_PageType.Destination, categoryID, reader["title"].ToString(), "", ""));
            Content = Content.Replace("<!--##H1##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.H1, SEO_PageType.Destination, categoryID, reader["title"].ToString(), "", ""));


            Content = Content.Replace("<!--##@DestDefault##-->", "<input type=\"hidden\" name=\"page_type\" id=\"page_type\" value=\"list-all\" />\n<input type=\"hidden\" name=\"destDefault\" id=\"destDefault\" value=\"" + destinationID + "\" />");
            Content = Content.Replace("<!--##@LocDefault##-->", "<input type=\"hidden\" name=\"locDefault\" id=\"locDefault\" value=\"0\" />");
            Content = Content.Replace("<!--##@CateDefault##-->", "<input type=\"hidden\" name=\"category\" id=\"category\" value=\"" + categoryID + "\" />");

            //Content = Content.Replace("<!--##IncludeASPFile##-->", WebStatPlugIn.TrackingIncludeFunction());
            Content = Content.Replace("<!--##IncludeASPFile##-->","");
            //Content = Content.Replace("<!--##Tracking##-->", WebStatPlugIn.TrackingScript(15, locationID, 1));

            //Content=Content.Replace("http://www.hotels2thailand.com","http://174.36.32.56");
            //HttpContext.Current.Response.Write(reader["file_name"]);
            gf = new GenerateFile(HttpContext.Current.Server.MapPath("/"), reader["file_name"].ToString(), Content);
            gf.CreateFile();

            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.End();
        }
        objConn.Close();
    }

    public void GenSearchPage()
    {

        StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath("/Layout/list_old_version.html"));
        string layout = objReader.ReadToEnd();
        objReader.Close();


        string Keyword = string.Empty;
        string Content = string.Empty;

        GenerateFile gf;


        categoryID = 29;
        Keyword = Utility.GetKeywordReplace(layout, "<!--##@NavigatorStart##-->", "<!--##@NavigatorEnd##-->");
        Content = layout.Replace(Keyword, "Search Result");

        Keyword = Utility.GetKeywordReplace(Content, "<!--##@HotelListStart##-->", "<!--##@HotelListEnd##-->");
        Content = Content.Replace(Keyword, "<div id=\"pList\"></div>");

        Keyword = Utility.GetKeywordReplace(Content, "<!--##@PopularDestinationStart##-->", "<!--##@PopularDestinationEnd##-->");
        //Content = Content.Replace(Keyword, GetDestinationProductPopular(30, 29));

        //Keyword = Utility.GetKeywordReplace(Content,"<!--##@sortbyIncludeStart##-->","<!--##@sortbyIncludeEnd##-->");
        //Content = Content.Replace(Keyword, "");

        StreamReader objReaderDestPopular = new StreamReader(HttpContext.Current.Server.MapPath("/Layout/thailand-destination-menu.html"));
        string destinationPopular = "<div class=\"head_warp\"></div>\n";
        destinationPopular = destinationPopular + "<div id=\"head_pop\"> <h3>Most popular<br /> hotels in Thailand</h3> </div>\n";
        destinationPopular = destinationPopular + objReaderDestPopular.ReadToEnd() + "</div>";
        objReaderDestPopular.Close();

        Keyword = Utility.GetKeywordReplace(Content, "<!--##@PopularLocationStart##-->", "<!--##@PopularLocationEnd##-->");
        Content = Content.Replace(Keyword, destinationPopular);

        Keyword = Utility.GetKeywordReplace(layout, "<!--##@TopHotelDestinationStart##-->", "<!--##@TopHotelDestinationEnd##-->");
        Content = Content.Replace(Keyword, "");

        Keyword = Utility.GetKeywordReplace(layout, "<!--##Sort BoxStart##-->", "<!--##Sort BoxEnd##-->");
        Content = Content.Replace(Keyword, "");

        Keyword = Utility.GetKeywordReplace(Content, "<!--##@dateDefaultStart##-->", "<!--##@dateDefaultEnd##-->");


        //string hiddenDate = "<br class=\"clear-all\" /><li><input type=\"text\" name=\"dateStart\" id=\"dateStartci\"  rel=\"datepicker\" style=\"width:145px; background:#edf2fa; height:18px; float:left; line-height:25px;\"></li><br class=\"clear-all\" />";
        //if (categoryID == 29)
        //{
        //    hiddenDate = hiddenDate + "<br class=\"clear-all\" /><li><input type=\"text\" name=\"dateEnd\" id=\"dateStartco\"  rel=\"\" style=\"width:145px; background:#edf2fa; height:18px; float:left;\" /></li><br class=\"clear-all\" />";
        //}

        //Content = Content.Replace(Keyword, hiddenDate);

        //Keyword = Utility.GetKeywordReplace(Content, "<!--##@TopHotelDestinationStart##-->", "<!--##@TopHotelDestinationEnd##-->");
        //Content = Content.Replace(Keyword,"");
        Content = Content.Replace("<!--##title_tag##-->", "Thailand Hotels Bangkok,Phuket Hotels Discount Hotel in Thailand");
        Content = Content.Replace("<!--##@DestDefault##-->", "<input type=\"hidden\" name=\"destDefault\" id=\"destDefault\" value=\"30\" />");
        Content = Content.Replace("<!--##@LocDefault##-->", "<input type=\"hidden\" name=\"locDefault\" id=\"locDefault\" value=\"0\" />");
        Content = Content.Replace("<!--##@CateDefault##-->", "<input type=\"hidden\" name=\"category\" id=\"category\" value=\"29\" />");
        Content = Content.Replace("<!--##IncludeASPFile##-->", "");
        Content = Content.Replace("<!--##Tracking##-->",WebStatPlugIn.TrackingQuickSearch());
        //Response.Write(Content);
        gf = new GenerateFile(HttpContext.Current.Server.MapPath("/"), "hotels_search.asp", Content);
        gf.CreateFile();
        //Response.End();
    }

    public void GenPageListLocation()
    {
        categoryID = 29;
        DataConnect objConn = new DataConnect();

        string sqlCommand = "select ln.location_id,ln.file_name,l.destination_id,l.title";
        sqlCommand = sqlCommand + " from tbl_location l,tbl_location_name ln";
        sqlCommand = sqlCommand + " where l.location_id=ln.location_id and ln.lang_id=1";
        sqlCommand = sqlCommand + " and (select count(pl.product_id) from tbl_product_location pl where pl.location_id=l.location_id)>0";
        SqlDataReader reader = objConn.GetDataReader(sqlCommand);
        string layout = GetStreamReader("/Layout/list_old_version.html");
        string Keyword = Utility.GetKeywordReplace(layout, "<!--##@TopHotelDestinationStart##-->", "<!--##@TopHotelDestinationEnd##-->");

        string hotDestination = GetStreamReader("/Layout/top_hotel_destination.html");
        //hotDestination = hotDestination.Replace("www.hotels2thailand.com", "174.36.32.56");
        layout = layout.Replace(Keyword, hotDestination);

        string content = string.Empty;

        while (reader.Read())
        {

            Keyword = Utility.GetKeywordReplace(layout, "<!--##@NavigatorStart##-->", "<!--##@NavigatorEnd##-->");

            Navigator nav = new Navigator();
            nav.LoadLocationLink();

            content = layout.Replace(Keyword, nav.GenNavigator(categoryID, (short)reader["destination_id"], (short)reader["location_id"]));

            Keyword = Utility.GetKeywordReplace(content, "<!--##@HotelListStart##-->", "<!--##@HotelListEnd##-->");
            ProductList list = new ProductList();
            list.MaxRecord = 10;
            content = content.Replace(Keyword, "<div id=\"Product-list\"></div>");
            GenerateFile gf;

            Keyword = Utility.GetKeywordReplace(content, "<!--##@PopularDestinationStart##-->", "<!--##@PopularDestinationEnd##-->");


            content = content.Replace(Keyword, list.RenderPopularDestination((short)reader["destination_id"], categoryID));

            Keyword = Utility.GetKeywordReplace(content, "<!--##@JSListFunctionStart##-->", "<!--##@JSListFunctionEnd##-->");
            string jsInclude = "<script language=\"javascript\" type=\"text/javascript\">";
            jsInclude = jsInclude + "$(function () {";
            jsInclude = jsInclude + "$(\"#Product-list\").html('<img src=\"/images/loader.gif\">');";
            jsInclude = jsInclude + "$(\"#Product-list\").load(\"/vGenerator/ProductList.aspx?dest=" + reader["destination_id"] + "&loc=" + reader["location_id"] + "&cate=" + categoryID + "\");";
            jsInclude = jsInclude + "});";
            jsInclude = jsInclude + "</script>";

            content = content.Replace(Keyword, jsInclude);
            content = content.Replace("<!--##@DestDefault##-->", "<input type=\"hidden\" name=\"destDefault\" id=\"destDefault\" value=\"" + reader["destination_id"] + "\" />");
            content = content.Replace("<!--##@LocDefault##-->", "<input type=\"hidden\" name=\"locDefault\" id=\"locDefault\" value=\"" + reader["location_id"] + "\" />");

            content = content.Replace("<!--##LocationProductText##-->", reader["title"] + " " + Utility.GetProductType(categoryID)[0, 1]);

            gf = new GenerateFile(HttpContext.Current.Server.MapPath("/"), reader["file_name"].ToString(), content);
            gf.CreateFile();
        }
        objConn.Close();
    }
    public void GenPageDestinationList()
    {
        LinkGenerator link = new LinkGenerator(LangID);
        List<LocationLink> locationList = link.GetLocationList(29);

        HttpContext.Current.Response.Write("Destination List");
        //int destinationTotal = 0;
        int destinationIDTemp = 0;


        destinationIDTemp = 0;

        string data = string.Empty;

        data = data + "<div class=\"total_hotel_bg\">\n";
        data = data + "<img src=\"/theme_color/blue/images/layout/list_total-hotel1.jpg\" class=\"bg_tap\" />\n";
        data = data + "<div class=\"total_hotel\">\n";
        if(LangID==1)
        {
        data = data + "<h4>Browse By Hotel Destination</h4></div>\n";
        }else{
            data = data + "<h4>เลือกโรงแรมตามจังหวัด</h4></div>\n";
        }
        
        data = data + "<br class=\"clear-all\" /> \n";
        data = data + "</div>\n";

        //data = data + "<div class=\"total_hotel\"><h4>Browse By Hotel Destination</h4></div>\n";
        data = data + "<br class=\"clear-all\" /><!--pim-->\n";

        data = data + "<div id=\"hotel_lsit\">\n";

        data = data + "<div id=\"bg_hotel_list\"> \n";
        data = data + "<div class=\"bg_box_top\"></div> \n";

        data = data + "<div id=\"destinations_hotel\">  \n";

        //data = data + "<ul><!-- Column 1-->\n";
        //data = data + "<a href=\"#\" class=\"pin\">Ayutthaya</a>\n";
        //  data = data + "<li >                	
        //      data = data + "<a href=\"#\">Ayutthaya City</a> \n";
        //      data = data + "<a href=\"#\">Ayutthaya City</a> \n";
        //  data = data + "</li>
        // data = data + "</ul><!-- Column 1--> \n";

        foreach (LocationLink item in locationList)
        {
            if (item.DestinationID != destinationIDTemp)
            {
                if (destinationIDTemp != 0)
                {
                    data = data + "</li>\n";
                    data = data + "</ul>";
                }
                data = data + "<ul>\n";
                data = data + "<a href=\"" + link.GetDestinationPath(item.DestinationID) + "\" class=\"pin\">" + item.DestinationTitle + "</a><br>\n";
                data = data + "<li style=\"padding:10px;\">";

            }
            data = data + "<a href=\"" + link.GetLocationPath(item.LocationID) + "\">" + item.LocationTitle + "</a>";
            destinationIDTemp = item.DestinationID;

        }
        data = data + "</li>\n";
        data = data + "</ul>";

        data = data + "<br class=\"clear-all\" /> \n";
        data = data + "</div><!--destinations-->\n";

        data = data + "<br class=\"clear-all\" /> \n";
        data = data + "<div class=\"bg_down_destination\"></div>	\n";
        data = data + "</div>\n";


        data = data + "</div><!--hotels list-->\n";

        data = data + "<div id=\"end_hotels_list\"> \n";

        //--
        //data = data + "<div class=\"total_hotel\"><h4>Browse By Hotel Destination</h4></div>\n";
        //data = data + "<br class=\"clear-all\" /><!--pim-->\n";
        //data = data + "<div id=\"hotel_lsit\">\n";
        //data = data + "<div id=\"bg_hotel_list\">\n";
        //data = data + "<div class=\"bg_box_top\"></div> \n";
        //data = data + "<div id=\"destinations_hotel\"> \n";

        //foreach (LocationLink item in locationList)
        //{
        //    if(item.DestinationID!=destinationIDTemp){
        //        if(destinationIDTemp!=0){
        //            data = data + "</li>\n";
        //            data = data + "</ul>";
        //        }
        //        data = data + "<ul>\n";
        //        data = data + "<li style=\"padding:10px;\">";
        //        data = data + "<a href=\""+link.GetDestinationPath(item.DestinationID)+"\" class=\"pin\">"+item.DestinationTitle+"</a><br>\n";
        //    }
        //    data = data + "<a href=\"" + link.GetLocationPath(item.LocationID) + "\">" + item.LocationTitle + "</a>";
        //    destinationIDTemp = item.DestinationID;

        //}
        //data = data + "</li>\n";
        //data = data + "</ul>";
        //data = data + "<br class=\"clear-all\" />  \n";
        //data = data + "</div><!--destinations-->\n";

        //data = data + "<br class=\"clear-all\" />\n";
        //data = data + "<div class=\"bg_down_destination\"></div>\n";
        //data = data + "</div>\n";


        //data = data + "</div><!--hotels list-->";

        //data = data + "<div id=\"end_hotels_list\">";

        string layout = string.Empty; 
        if(LangID==1)
        {
            layout = GetStreamReader("/Layout/hotel_destination.html");
        }else{
            layout = GetStreamReader("/Layout-th/hotel_destination.html");
        }
        
        //string Keyword = Utility.GetKeywordReplace(layout, "<!--##@TopHotelDestinationStart##-->", "<!--##@TopHotelDestinationEnd##-->");

        //string hotDestination = GetStreamReader("../Layout/top_hotel_destination.html");

        //layout = layout.Replace(Keyword, hotDestination);

        string Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductDestinationListStart##-->", "<!--##@ProductDestinationListEnd##-->");


        Navigator nav = new Navigator();
        nav.LangID = LangID;
        nav.LoadLocationLink();
        string content = string.Empty;

        content = layout.Replace(Keyword, data);
        string flagLanguage = string.Empty;
        Keyword = Utility.GetKeywordReplace(layout, "<!--###FlagLanguageStart###-->", "<!--###FlagLanguageEnd###-->");
        
            flagLanguage = "<a href=\"http://www.hotels2thailandnew.com/thailand-hotels.asp\" alt=\"English\"><img src=\"../theme_color/blue/images/flag/flagENG.png\" width=\"25\" height=\"20\" style=\"float:left; margin:0 7px 0 25px;\" /></a>";
            flagLanguage = flagLanguage + "<a href=\"http://thai.hotels2thailandnew.com/thailand-hotels-th.asp\" alt=\"Thai\"><img src=\"../theme_color/blue/images/flag/flagTH.png\"  style=\"float:left; margin:0px;\" /></a>";




            content = content.Replace(Keyword, flagLanguage);
        //Keyword = Utility.GetKeywordReplace(content, "<!--##@PopularDestinationStart##-->", "<!--##@PopularDestinationEnd##-->");

        //content = content.Replace(Keyword, list.RenderPopularDestination(destinationID, categoryID));
        content = content.Replace("<!--##IncludeASPFile##-->", WebStatPlugIn.TrackingIncludeFunction());
        content = content.Replace("<!--##Tracking##-->", WebStatPlugIn.TrackingScript(14, categoryID, 1));
        GenerateFile gf;
        if(LangID==1)
        {
         gf = new GenerateFile(HttpContext.Current.Server.MapPath("/"), "thailand-hotels.asp", content);
        }else{
         gf = new GenerateFile(HttpContext.Current.Server.MapPath("/"), "thailand-hotels-th.asp", content);
        }
       
        gf.CreateFile();
        //Response.Write(data);
        //Response.End();
    }

    public void GenPageBrowse(byte categoryID)
    {

        string featureProduct = string.Empty;
        string popularProduct = string.Empty;
        string MetaTag = "";
        string MetaTitle = "";
        string MetaAlt = "";
        string MetaH1 = "";

        string flagLanguage = string.Empty;

        DataConnect objConn = new DataConnect();

        string sqlCommand = "select d.destination_id,dn.file_name,dn.title,d.folder_destination";
        sqlCommand = sqlCommand + " ,(SELECT COUNT(sp.product_id) FROM tbl_product sp WHERE d.destination_id=sp.destination_id AND cat_id=29 AND status=1) AS num_hotel";
        sqlCommand = sqlCommand + " from tbl_destination d,tbl_destination_name dn";
        sqlCommand = sqlCommand + " where d.destination_id=dn.destination_id and dn.lang_id=" + LangID + " and d.status=1 and";
        sqlCommand = sqlCommand + " (";
        sqlCommand = sqlCommand + " select COUNT(destination_id) from tbl_location sl where sl.destination_id=d.destination_id";
        sqlCommand = sqlCommand + " )>0";

        SqlDataReader reader = objConn.GetDataReader(sqlCommand);
        while (reader.Read())
        {
            categoryID = 29;
            destinationID = (short)reader["destination_id"];

            
            //MetaTag = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Destination, categoryID, reader["title"].ToString(), "", "");
            //MetaTag = MetaTag + SEO_Generate.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Destination, categoryID, "http://www.hotels2thailand.com/" + reader["file_name"].ToString());
            //MetaTitle = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Title, SEO_PageType.Destination, categoryID, reader["title"].ToString(), "", "");
            //MetaAlt = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Link_Back_To_Home, SEO_PageType.Destination, categoryID, reader["title"].ToString(), "", "");
            //MetaH1 = SEO_Generate.Function_gen_optimize_string(SEO_StringType.H1, SEO_PageType.Destination, categoryID, reader["title"].ToString(), "", "");

            MetaTag = SEO_GenerateV2.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Destination, LangID, categoryID, reader["title"].ToString(), "", "", "", "", reader["num_hotel"].ToString(), "", "");
            if(LangID==1)
            {
            MetaTag = MetaTag + SEO_GenerateV2.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Destination, categoryID, "http://www.hotels2thailand.com/" + reader["file_name"].ToString());
            }else{
            MetaTag = MetaTag + SEO_GenerateV2.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Destination, categoryID, "http://thai.hotels2thailand.com/" + reader["file_name"].ToString());
            }
            
            MetaTitle = SEO_GenerateV2.Function_gen_optimize_string(SEO_StringType.Title, SEO_PageType.Destination, LangID, categoryID, reader["title"].ToString(), "", "", "", "", reader["num_hotel"].ToString(), "", "");
            MetaAlt = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Link_Back_To_Home, SEO_PageType.Destination, categoryID, reader["title"].ToString(), "", "");
            MetaH1 = SEO_Generate.Function_gen_optimize_string(SEO_StringType.H1, SEO_PageType.Destination, categoryID, reader["title"].ToString(), "", "");

            string layout = string.Empty;
            if(LangID==1)
            {
                layout=GetStreamReader("/Layout/browse_old_version.html");
            }else{
                layout=GetStreamReader("/Layout-TH/browse_old_version.html");
            }
            
            layout = layout.Replace("<!--##meta_tag##-->", MetaTag);
            layout = layout.Replace("<!--##title_tag##-->", MetaTitle);
            layout = layout.Replace("<!--##AltIndex##-->", MetaAlt);
            layout = layout.Replace("<!--##H1##-->", MetaH1);

            string Keyword = Utility.GetKeywordReplace(layout, "<!--##@FeatureHotelStart##-->", "<!--##@FeatureHotelEnd##-->");

            //featureProduct="<div id=\"content_left_right\">\n";
            //featureProduct=featureProduct+"<div class=\"content_top_bg\">\n";
            //featureProduct=featureProduct+"<div class=\"head_featured\"><h3>Featured Hotels in "+reader["title"]+"</h3></div>\n";
            //featureProduct = featureProduct + "<div class=\"head_landmarks\"><strong>All Location in " + reader["title"] + "</strong></div>\n";
            //featureProduct=featureProduct+"</div>\n";
            //featureProduct=featureProduct+"<div id=\"content_left\"></div>\n";
            featureProduct = "<div id=\"content_left_right\">\n";
            featureProduct = featureProduct + "<div class=\"content_top_bg\">\n";
            featureProduct = featureProduct + "<div class=\"head_featured_bg\">\n";
            if(LangID==1)
            {
                featureProduct = featureProduct + "<div class=\"head_featured\"><h3>Featured Hotels in " + reader["title"] + "</h3></div>\n";
                featureProduct = featureProduct + "<div class=\"see_all\"><a href=\"/" + reader["folder_destination"] + "-hotels/\">See all</a></div>";
            }else{
                featureProduct = featureProduct + "<div class=\"head_featured\"><h3>โรงแรมน่าสนใจใน" + reader["title"] + "</h3></div>\n";
                featureProduct = featureProduct + "<div class=\"see_all\"><a href=\"/" + reader["folder_destination"] + "-hotels/index-th.asp\">ดูโรงแรมทั้งหมดใน" + reader["title"] + ".</a></div>";
            }
            
            
            featureProduct = featureProduct + "<br class=\"clear-all\">";
            featureProduct = featureProduct + "</div>\n";

            featureProduct = featureProduct + "<div class=\"head_landmarks\">\n";
            if (LangID == 1)
            {
                featureProduct = featureProduct + "<div class=\"head_featured\"><strong>All Location in " + reader["title"] + "</strong></div>\n";
            }
            else {
                featureProduct = featureProduct + "<div class=\"head_featured\"><strong>ที่ตั้งโรงแรมทั้งหมดใน " + reader["title"] + "</strong></div>\n";
            }
            
            featureProduct = featureProduct + "<img src=\"/theme_color/blue/images/layout/browse_header_featured2.jpg\" />\n";
            featureProduct = featureProduct + "</div>\n";
            featureProduct = featureProduct + "</div>\n";
            featureProduct = featureProduct + "<div id=\"content_left\"></div>\n";

            ProductList list = new ProductList(LangID);



            layout = layout.Replace(Keyword, featureProduct);


            Keyword = Utility.GetKeywordReplace(layout, "<!--##@NavigatorStart##-->", "<!--##@NavigatorEnd##-->");


            Navigator nav = new Navigator();
            nav.LangID = LangID;
            nav.LoadLocationLink();

            layout = layout.Replace(Keyword, nav.GenNavigator(categoryID, destinationID));

            list.MaxRecord = 0;
            list.PageSize = 999;

            list.GetProductList(destinationID, 0, categoryID, "''", "''", 4);
           
            Keyword = Utility.GetKeywordReplace(layout, "<!--##@AllHotelDestinationStart##-->", "<!--##@AllHotelDestinationEnd##-->");
            layout = layout.Replace(Keyword, list.RenderAllProductDestination());

            //Response.Write(list.RenderAllProductDestination());
            //Response.End();
            Keyword = Utility.GetKeywordReplace(layout, "<!--##@PopularRightNowStart##-->", "<!--##@PopularRightNowEnd##-->");

            //popularProduct = "<div id=\"popular_header\"><strong>Popular Right Now in "+reader["title"]+"</strong></div>\n"; 
            //popularProduct = popularProduct + "<div class=\"last_viewed\">\n";
            //popularProduct = popularProduct + "<div class=\"bg_top_colright\"></div>\n"; 
            //popularProduct = popularProduct + "<div id=\"popular_list\">\n"; 
            //popularProduct = popularProduct + "</div><!--popular_list-->\n";  
            //popularProduct = popularProduct + "<div class=\"bg_down_colrigh\"></div> \n";
            //popularProduct = popularProduct + "</div>\n";

            popularProduct = "<div id=\"last_viewed_header\"> \n";
            if(LangID==1)
            {
                popularProduct = popularProduct + "<div class=\"head_featured\"><strong>Popular Right Now in " + reader["title"] + "</strong></div> \n";
            
            }else{
                popularProduct = popularProduct + "<div class=\"head_featured\"><strong>โรงแรมยอดฮิตที่สุดใน" + reader["title"] + "</strong></div> \n";
            
            }
            
            popularProduct = popularProduct + "<img src=\"/theme_color/blue/images/layout/browse_header_featured2.jpg\" /> \n";
            popularProduct = popularProduct + "</div> \n";
            popularProduct = popularProduct + "<br class=\"clear-all\" /> \n";

            popularProduct = popularProduct + "<div class=\"last_viewed\"> \n";
            popularProduct = popularProduct + "<div class=\"bg_top_colright\"></div> \n";

            popularProduct = popularProduct + "<div id=\"popular_list\">\n";
            popularProduct = popularProduct + "</div><!--popular_list-->\n";
            popularProduct = popularProduct + "<div class=\"bg_down_colrigh\"></div> \n";
            popularProduct = popularProduct + "</div>\n";

            //list.MaxRecord = 5;
            //list.GetProductList(destinationID, 0, categoryID, "''","''", 5);
            layout = layout.Replace(Keyword, popularProduct);

            Keyword = Utility.GetKeywordReplace(layout, "<!--@##LandmarkStart##-->", "<!--@##LandmarkEnd##-->");
            layout = layout.Replace(Keyword, nav.RenderLocationList(destinationID));



            Keyword = Utility.GetKeywordReplace(layout, "<!--##@LastViewStart##-->", "<!--##@LastViewEnd##-->");
            

            //layout = layout.Replace(Keyword, "<div id=\"recent_view\"></div>");
            layout = layout.Replace(Keyword, "");
           
            Keyword = Utility.GetKeywordReplace(layout, "<!--###FlagLanguageStart###-->", "<!--###FlagLanguageEnd###-->");
            if(LangID==1)
            {
                flagLanguage = "<a href=\"http://www.hotels2thailandnew.com/" + reader["file_name"].ToString() + "\" alt=\"English\"><img src=\"../theme_color/blue/images/flag/flagENG.png\" width=\"25\" height=\"20\" style=\"float:left; margin:0 7px 0 25px;\" /></a>";
                flagLanguage = flagLanguage + "<a href=\"http://thai.hotels2thailandnew.com/" + reader["file_name"].ToString().Replace(".asp", "-th.asp") + "\" alt=\"Thai\"><img src=\"../theme_color/blue/images/flag/flagTH.png\"  style=\"float:left; margin:0px;\" /></a>";

            }else{
                flagLanguage = "<a href=\"http://www.hotels2thailandnew.com/" + reader["file_name"].ToString().Replace("-th.asp", ".asp") + "\" alt=\"English\"><img src=\"../theme_color/blue/images/flag/flagENG.png\" width=\"25\" height=\"20\" style=\"float:left; margin:0 7px 0 25px;\" /></a>";
                flagLanguage = flagLanguage + "<a href=\"http://thai.hotels2thailandnew.com/" + reader["file_name"].ToString() + "\" alt=\"Thai\"><img src=\"../theme_color/blue/images/flag/flagTH.png\"  style=\"float:left; margin:0px;\" /></a>";
            }

            layout = layout.Replace(Keyword, flagLanguage);
            layout = layout.Replace("<!--##@DestTitleDefault##-->", reader["title"].ToString());
            layout = layout.Replace("<!--##@DestDefault##-->", "<input type=\"hidden\" name=\"page_type\" id=\"page_type\" value=\"browse\" />\n<input type=\"hidden\" name=\"destDefault\" id=\"destDefault\" value=\"" + destinationID + "\" />");
            layout = layout.Replace("<!--##@LocDefault##-->", "");
            layout = layout.Replace("<!--##IncludeASPFile##-->", WebStatPlugIn.TrackingIncludeFunction());
            layout = layout.Replace("<!--##Tracking##-->", WebStatPlugIn.TrackingScript(14, destinationID, 1));
            GenerateFile gf;
            if(LangID==1)
            {
                gf = new GenerateFile(HttpContext.Current.Server.MapPath("/"), reader["file_name"].ToString(), layout);
            }else{
                gf = new GenerateFile(HttpContext.Current.Server.MapPath("/"),reader["file_name"].ToString(), layout);
            }
            
            gf.CreateFile();
           //HttpContext.Current.Response.Flush();
           //HttpContext.Current.Response.End();
        }
        objConn.Close();
    }

    public void GenBrowseOtherProduct(byte categoryID)
    {
        string layout = GetStreamReader("/Layout/day_trips.html");
        HttpContext.Current.Response.Write(layout);
    }

    //public void GenerateHotelLocationPage()
    //{ 
    //    string sqlCommand="select l.location_id,l.destination_id,ln.file_name";
    //    sqlCommand=sqlCommand+" from tbl_location l,tbl_location_name ln";
    //    sqlCommand = sqlCommand + " where l.location_id=ln.location_id and ln.lang_id=1";

    //    DataConnect objConn = new DataConnect();
    //    SqlDataReader reader = objConn.GetDataReader(sqlCommand);

    //    GenerateFile gf=new GenerateFile(Server.MapPath("/"),"","");
    //    while(reader.Read())
    //    {
    //        gf.Filename = reader["file_name"].ToString();
    //        gf.Content = gf.GetDataFromURL("http://174.36.32.56/vGenerator/product-test.aspx?destG=" + reader["destination_id"] + "&locG=" + reader["location_id"] + "&cateG=29");
    //        gf.CreateFile();
    //        Response.Write("http://174.36.32.56/vGenerator/product-test.aspx?destG=" + reader["destination_id"] + "&locG=" + reader["location_id"] + "&cateG=29" + "<br/>");
    //        Response.Flush();
    //    }
    //}

    public string GetDestinationProductPopular(short destinationID, byte categoryID)
    {
        DataConnect objConn = new DataConnect();
        string sqlCommand = "select top 5 p.product_code,p.cat_id,pc.title,";
        sqlCommand = sqlCommand + " (select spc.title  from tbl_product_content spc where spc.product_id=p.product_id and spc.lang_id="+LangID+") as second_lang,";
        sqlCommand = sqlCommand + " p.star,dn.title as destination_title,(d.folder_destination+'-'+pcat.folder_cat+'/'+pc.file_name_main) as file_name_main,";
        sqlCommand = sqlCommand + " (select top 1 spp.pic_code from tbl_product_pic spp where spp.product_id=p.product_id and spp.cat_id=1 and spp.type_id=5 and spp.status=1) as picture";
        sqlCommand = sqlCommand + " from tbl_product p,tbl_product_content pc,tbl_destination d,tbl_destination_name dn,tbl_product_category pcat";
        sqlCommand = sqlCommand + " where d.destination_id=dn.destination_id and p.destination_id=d.destination_id and p.product_id=pc.product_id and p.cat_id=pcat.cat_id and dn.lang_id=" + LangID + " and pc.lang_id=1 and p.cat_id=" + categoryID + " and d.destination_id=" + destinationID;
        sqlCommand = sqlCommand + " order by p.point_popular desc";

        SqlDataReader reader = objConn.GetDataReader(sqlCommand);

        int countRow = 0;

        string result = "<div class=\"head_warp\"></div>\n";
        string imagePath = string.Empty;
        string categoryTitle = string.Empty;
        string productTitle = string.Empty;
        string filePath = string.Empty;

        while (reader.Read())
        {

            if (countRow == 0)
            {
                if(LangID==1)
                {
                    result = result + "<div id=\"head_pop\"> <h3>Most popular <br>" + reader["destination_title"] + " " + Utility.GetProductType((byte)reader["cat_id"])[0, 3] + "</h3> </div>\n";
                }else{

                    switch ((byte)reader["cat_id"])
                    {
                        case 29:
                            categoryTitle = "โรงแรม";
                            break;
                        case 32:
                            categoryTitle = "สนามกอล์ฟ";
                            break;
                        case 34:
                            categoryTitle = "กิจกรรมท่องเที่ยว";
                            break;
                        case 36:
                            categoryTitle = "กิจกรรมทางน้ำ";
                            break;
                        case 38:
                            categoryTitle = "การแสดงโชว์";
                            break;
                        case 39:
                            categoryTitle = "สถานที่ตรวจสุขภาพ";
                            break;
                        case 40:
                            categoryTitle = "สปา";
                            break;
                    }
                    result = result + "<div id=\"head_pop\"> <h3>" + categoryTitle + "ยอดฮิตใน<br>" + reader["destination_title"] +"</h3> </div>\n";
                }
                
                result = result + "<ul class=\"end_pop_location\">\n";
            }

            imagePath = "<img src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType((byte)reader["cat_id"])[0, 1] + "-pic/" + reader["product_code"] + "_b_1.jpg\" width=\"45\" height=\"40\"/>";

            if (!string.IsNullOrEmpty(reader["picture"].ToString()))
            {
                imagePath = "<img src=\"" + reader["picture"] + "\" alt=\"" + reader["title"] + "\">";
            
            }

            if (countRow % 2 == 0)
            {
                result = result + "<li>" + imagePath;
            }
            else
            {
                result = result + "<li class=\"bg_pop\">" + imagePath;
            }
            filePath = reader["file_name_main"].ToString();
            if(LangID==1)
            {
                productTitle = reader["title"].ToString();
            }else{
                productTitle = reader["second_lang"].ToString();
                filePath = reader["file_name_main"].ToString().Replace(".asp","-th.asp");
                if(string.IsNullOrEmpty(productTitle))
                {
                    productTitle = reader["title"].ToString();
                    filePath = reader["file_name_main"].ToString();
                }
            }
            result = result + "<h4><a href=\"/" + filePath + "\">" + productTitle + "</a></h4>";
            result = result + "<div class=\"clear-all\"></div>\n";
            result = result + "</li>\n";
            countRow = countRow + 1;
        }
        result = result + "</ul></div>\n";


        //string result = "<div class=\"head_warp\"></div>";



        //while (reader.Read())
        //{
        //    if (countRow == 0)
        //    {
        //        result = result + "<div id=\"head_pop\">";
        //        result = result = "<h3>Most Popular <br />" + reader["destination_title"] + " " + Utility.GetProductType(categoryID)[0, 1] + "</h3>";
        //        //result = result + "<div class=\"title\">Popular Destination</div> <div class=\"txt\">Most Popular " + Utility.GetProductType(categoryID)[0, 1] + " in " + reader["destination_title"] + "</div> </div>";
        //        result = result + "<ul class=\"end_pop_location\">";
        //    }

        //    string imagePopular = "<img src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType((byte)reader["cat_id"])[0, 1] + "-pic/" + reader["product_code"] + "_b_1.jpg\" />";

        //    if (categoryID != 29)
        //    {
        //        imagePopular = "";
        //    }

        //    if (countRow % 2 == 0)
        //    {
        //        result = result + "<li>" + imagePopular;
        //    }
        //    else
        //    {
        //        result = result + "<li class=\"bg_pop\">" + imagePopular;
        //    }



        //    result = result + "<h4><a href=\"" + reader["file_name_main"] + "\">" + reader["title"] + "</a></h4>";
        //    if (categoryID == 29)
        //    {
        //        result = result + "<div class=\"class_star_s_" + Utility.GetHotelClassImage((float)reader["star"], 1) + "\"></div>";
        //    }

        //    result = result + "<div class=\"clear-all\"></div>";
        //    result = result + "</li>";
        //    countRow = countRow + 1;

        //}
        //result = result + "<div class=\"clear-all\"></div>";
        //result = result + "</ul>";

        objConn.Close();
        return result;
    }

    public string GetLocationInDestination(int destinationID, int locationID)
    {
        DataConnect objConn = new DataConnect();
        string sqlCommand = "select p.product_id,pc.title,";
        sqlCommand = sqlCommand + " (select spc.title from tbl_product_content spc where spc.product_id=p.product_id and spc.lang_id="+LangID+") as second_lang,";
        sqlCommand = sqlCommand + " d.folder_destination+'-'+pcat.folder_cat+'/'+pc.file_name_main as file_name,ln.title as location_title";
        sqlCommand = sqlCommand + " from tbl_product p,tbl_product_content pc,tbl_destination d,tbl_product_category pcat,tbl_product_location pl,tbl_location l,tbl_location_name ln";
        sqlCommand = sqlCommand + " where	p.product_id=pc.product_id and p.destination_id=d.destination_id and p.cat_id=pcat.cat_id and p.product_id=pl.product_id and pl.location_id=l.location_id and l.location_id=ln.location_id";
        sqlCommand = sqlCommand + " and pc.lang_id=1 and ln.lang_id=" + LangID + " and pl.location_id=" + locationID + " and p.cat_id=29 and p.status=1 and l.status=1";
        sqlCommand = sqlCommand + " order by pc.title asc";

        SqlDataReader rdDestination = objConn.GetDataReader(sqlCommand);
        string resultDestination = string.Empty;
        int colCheck = 0;

        int productTmp = 0;
        string fileName = string.Empty;
        string productTitle = string.Empty;

        while (rdDestination.Read())
        {
            if ((int)rdDestination["product_id"] != productTmp)
            {
                if (colCheck == 0)
                {
                    if(LangID==1)
                    {
                        resultDestination = resultDestination + "<div id=\"tophotel_footer\"><h3>All Hotels in " + rdDestination["location_title"] + "</h3> </div>\n"; 
                    }else{
                        resultDestination = resultDestination + "<div id=\"tophotel_footer\"><h3>โรงแรมทั้งหมดใน" + rdDestination["location_title"] + "</h3> </div>\n";
                    }
                    resultDestination = resultDestination + "<table id=\"content_tophotel_footer_b\"><tr>\n";
                }

                if (colCheck % 3 == 0)
                {
                    resultDestination = resultDestination + "</tr><tr>\n";
                }

                if(LangID==1)
                {
                    fileName = rdDestination["file_name"].ToString();
                    productTitle = rdDestination["title"].ToString();
                }else{
                    fileName = rdDestination["file_name"].ToString().Replace(".asp","-th.asp");
                    productTitle = rdDestination["second_lang"].ToString();
                    if(string.IsNullOrEmpty(productTitle))
                    {
                        productTitle = rdDestination["title"].ToString();
                    }
                }
                resultDestination = resultDestination + "<td class=\"all_col_b\">  <a href=\"/" + fileName + "\">" +productTitle + "</a> </td>\n";
                colCheck = colCheck + 1;
            }
            productTmp = (int)rdDestination["product_id"];


        }
        resultDestination = resultDestination + "</tr>\n";
        resultDestination = resultDestination + "</table>\n";
        resultDestination = resultDestination + "<div id=\"bg_end_footer\"></div><br />\n";

        sqlCommand = "select l.location_id,ln.title,l.destination_id,ln.file_name,dn.title as destination_title";
        sqlCommand = sqlCommand + " from tbl_location l,tbl_location_name ln,tbl_destination d,tbl_destination_name dn";
        sqlCommand = sqlCommand + " where d.destination_id=dn.destination_id and d.destination_id=l.destination_id and l.location_id=ln.location_id and dn.lang_id="+LangID+" and ln.lang_id="+LangID+" and l.destination_id=" + destinationID;


        SqlDataReader reader = objConn.GetDataReader(sqlCommand);

        string result = string.Empty;
        colCheck = 0;
        while (reader.Read())
        {
            if (colCheck == 0)
            {
                if(LangID==1){
                    result = result + "<div id=\"tophotel_footer\"><h3>All Location in " + reader["destination_title"] + "</h3> </div>";
                }else{
                    result = result + "<div id=\"tophotel_footer\"><h3>สถานที่ตั้งโรงแรมใน" + reader["destination_title"] + "</h3> </div>";
                }
                
                result = result + "<table id=\"content_tophotel_footer_b\">";
                result = result + "<tr>";

            }

            if (colCheck % 3 == 0)
            {
                result = result + "</tr><tr>";
            }
            result = result + "<td class=\"all_col_b\">  <a href=\"/" + reader["file_name"] + "\">" + reader["title"] + "</a> </td>";
            colCheck = colCheck + 1;
        }
        result = result + "</tr>";
        result = result + "</table> ";
        result = result + "<div id=\"bg_end_footer\"></div>\n";

        sqlCommand = "select dn.title,dn.file_name from tbl_destination d,tbl_destination_name dn";
        sqlCommand = sqlCommand + " where d.destination_id=dn.destination_id";
        sqlCommand = sqlCommand + " and dn.lang_id="+LangID+" order by dn.title";
        reader = objConn.GetDataReader(sqlCommand);



        result = result + "<div id=\"tophotel_footer\">\n";
        result = result + "<h4>จังหวัดทั้งหมดของประเทศไทย</h4> </div>\n";
        result = result + "<table id=\"content_tophotel_footer_b\">\n";
        result = result + "<tr valign=\"top\">\n";

        int countItem = 0;
        while (reader.Read())
        {
            if (countItem % 3 == 0)
            {
                result = result + "</tr><tr>\n";
            }
            if(LangID==1)
            {
                result = result + "<td class=\"all_col\"> <a href=\"/" + reader["file_name"] + "\">" + reader["title"] + " Hotels</a> </td>\n";
            }else{
                result = result + "<td class=\"all_col\"> <a href=\"/" + reader["file_name"] + "\">" + reader["title"] + "</a> </td>\n";
            }
            
            countItem = countItem + 1;
        }
        result = result + "</tr>\n";
        result = result + "</table>\n";



        objConn.Close();
        return resultDestination + result;
    }

    public string GetLocationInDestination(int destinationID)
    {
        DataConnect objConn = new DataConnect();
        string sqlCommand = "select p.product_id,pc.title,d.folder_destination+'-'+pcat.folder_cat+'/'+pc.file_name_main as file_name,ln.title as location_title";
        sqlCommand = sqlCommand + " from tbl_product p,tbl_product_content pc,tbl_destination d,tbl_product_category pcat,tbl_product_location pl,tbl_location l,tbl_location_name ln";
        sqlCommand = sqlCommand + " where	p.product_id=pc.product_id and p.destination_id=d.destination_id and p.cat_id=pcat.cat_id and p.product_id=pl.product_id and pl.location_id=l.location_id and l.location_id=ln.location_id";
        sqlCommand = sqlCommand + " and pc.lang_id=1 and ln.lang_id="+LangID+" and p.cat_id=29 and l.status=1";
        sqlCommand = sqlCommand + " order by pc.title asc";

        int colCheck = 0;

        int productTmp = 0;
        

        sqlCommand = "select l.location_id,ln.title,l.destination_id,ln.file_name,dn.title as destination_title";
        sqlCommand = sqlCommand + " from tbl_location l,tbl_location_name ln,tbl_destination d,tbl_destination_name dn";
        sqlCommand = sqlCommand + " where d.destination_id=dn.destination_id and d.destination_id=l.destination_id and l.location_id=ln.location_id and dn.lang_id="+LangID+" and ln.lang_id="+LangID+" and l.destination_id=" + destinationID;


        SqlDataReader reader = objConn.GetDataReader(sqlCommand);

        string result = string.Empty;
        colCheck = 0;
        while (reader.Read())
        {
            if (colCheck == 0)
            {
                if(LangID==1)
                {
                    result = result + "<div id=\"tophotel_footer\"><h3>All Location in " + reader["destination_title"] + "</h3> </div>";
                }else{
                    result = result + "<div id=\"tophotel_footer\"><h3>สถานที่ตั้งโรงแรมทั้งหมดใน" + reader["destination_title"] + "</h3> </div>";
                }

                
                result = result + "<table id=\"content_tophotel_footer_b\">";
                result = result + "<tr>";

            }

            if (colCheck % 3 == 0)
            {
                result = result + "</tr><tr>";
            }
            result = result + "<td class=\"all_col_b\">  <a href=\"/" + reader["file_name"] + "\">" + reader["title"] + "</a> </td>";
            colCheck = colCheck + 1;
        }
        result = result + "</tr>";
        result = result + "</table> ";
        result = result + "<div id=\"bg_end_footer\"></div>\n";

        sqlCommand = "select dn.title,dn.file_name from tbl_destination d,tbl_destination_name dn";
        sqlCommand = sqlCommand + " where d.destination_id=dn.destination_id";
        sqlCommand = sqlCommand + " and dn.lang_id="+LangID+" order by dn.title";
        reader = objConn.GetDataReader(sqlCommand);



        result = result + "<div id=\"tophotel_footer\">\n";
        if(LangID==1)
        {
            result = result + "<h4>All Thailand Hotels Destination</h4> </div>\n";
        }else{
            result = result + "<h4>สถานที่ตั้งโรงแรมทั้งหมดในประเทศไทย</h4> </div>\n";
        }
        
        result = result + "<table id=\"content_tophotel_footer_b\">\n";
        result = result + "<tr valign=\"top\">\n";

        int countItem = 0;
        while (reader.Read())
        {
            if (countItem % 3 == 0)
            {
                result = result + "</tr><tr>\n";
            }
            result = result + "<td class=\"all_col\"> <a href=\"/" + reader["file_name"] + "\">โรงแรมในจังหวัด" + reader["title"] + "</a> </td>\n";
            countItem = countItem + 1;
        }
        result = result + "</tr>\n";
        result = result + "</table>\n";



        objConn.Close();
        return  result;
    }

    public string GetSortBox(byte categoryID)
    {
        string result = string.Empty;
        result = result + "<div id=\"sortby\">";
        result = result + "<p>Sort By   :</p> ";

        result = result + "<div class=\"sortby_form\">";
        result = result + " <ul>";
        result = result + "<li><input type=\"radio\" name=\"order_by\" value=\"1\" checked=\"checked\">Popular " + Utility.GetProductType(categoryID)[0, 1] + "</li>  ";
        result = result + "</ul>";
        result = result + "<ul>";
        result = result + "<li><input type=\"radio\" name=\"order_by\" value=\"3\">Price Low to High</li> ";
        result = result + "<li><input type=\"radio\" name=\"order_by\" value=\"2\">Price High to Low</li> ";
        result = result + "</ul> ";
        result = result + "<ul>";
        result = result + "<li><input type=\"radio\" name=\"order_by\" value=\"4\">Name A-Z</li>";
        result = result + "<li><input type=\"radio\" name=\"order_by\" value=\"5\">Name Z-A</li>";
        result = result + "</ul> ";
        if (categoryID == 29)
        {
            result = result + "<ul>";
            result = result + "<li><input type=\"radio\" name=\"order_by\" value=\"7\">Hotel Star Low to High</li> ";
            result = result + "<li><input type=\"radio\" name=\"order_by\" value=\"6\">Hotel Star High to Low</li>";
            result = result + "</ul>";
        }
        result = result + "</div>";
        result = result + "</div><!--sortby--> ";

        return result;
    }
}