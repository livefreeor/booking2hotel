using System;
using System.Text;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
/// <summary>
/// Summary description for ProductPicCategory
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductPicGenerate : Hotels2BaseClass
    {

        
        private string _source_path;

        public string SourcePath
        {
            get { return _source_path; }
            set { _source_path = value; }
        }


        private string _destination_path;

        public string DestinationPath
        {
            get { return _destination_path; }
            set { _destination_path = value; }
        }


        private string _location_path;

        public string Location_Path
        {
            get { return _location_path; }
            set { _location_path = value; }
        }
        

        //Constructor Require Parameter
        public ProductPicGenerate()
        {
        }
        //public ProductPicGenerate(string strSourcePath, string strDetinationPath, string strLocationPath)
        //{
        //    this.SourcePath = strSourcePath;
        //    this.DestinationPath = strDetinationPath;
        //    this.Location_Path = strLocationPath;
        //}
        public ProductPicGenerate(string strSourcePath, string strDetinationPath, string strLocationPath)
        {
            this.SourcePath = strSourcePath;
            this.DestinationPath = strDetinationPath;
            this.Location_Path = strLocationPath;
        }

        public DirectoryInfo GetDefaulSourcePath()
        {
            DirectoryInfo dirResult = null;
            DirectoryInfo dirSourcePath = new DirectoryInfo(HttpContext.Current.Server.MapPath("../../" +this.SourcePath));
            if (dirSourcePath.Exists)
            {

                DirectoryInfo dirSourcePath_Destination = new DirectoryInfo(HttpContext.Current.Server.MapPath("../../" + this.SourcePath+ "/" + this.DestinationPath));
                if (dirSourcePath_Destination.Exists)
                {
                    ProductLocation cProductLocation = new ProductLocation();
                    if (cProductLocation.getTopOneLactionInProduct(int.Parse(HttpContext.Current.Request.QueryString["pid"])) != null)
                    {
                        if (string.IsNullOrEmpty(cProductLocation.getDefaultLocationPath(int.Parse(HttpContext.Current.Request.QueryString["pid"]))))
                        {
                            cProductLocation.InsertDefaultLocationPath(int.Parse(HttpContext.Current.Request.QueryString["pid"]), cProductLocation.getTopOneLactionInProduct(int.Parse(HttpContext.Current.Request.QueryString["pid"])).LocationID);
                        }

                        int count = 0;
                        string FolderName = string.Empty;
                        foreach (DirectoryInfo dirLocation in dirSourcePath_Destination.GetDirectories())
                        {
                            if (this.Location_Path.HotelsStringIsMatch(dirLocation.ToString()))
                            {
                                FolderName = dirLocation.ToString();
                                count = count + 1;
                                break;
                            }
                            
                            
                        }
                        //HttpContext.Current.Response.Write(FolderName + " :::::" + this.Location_Path + "<br/>");
                        //HttpContext.Current.Response.End();
                        if (count > 0)
                        {
                            DirectoryInfo dirSourcePath_Location = new DirectoryInfo(HttpContext.Current.Server.MapPath("../../" + this.SourcePath + "/" + this.DestinationPath + "/" + FolderName));
                            if (dirSourcePath_Location.Exists)
                            {
                                dirResult = dirSourcePath_Location;
                            }
                            
                        }
                        else
                        {
                            // No folder Location Source
                            //Error = 203;
                            HttpContext.Current.Response.Redirect("~/admin/hotels2ErrorPage.aspx?error=203");
                        }
                    }
                    else
                    {
                        // Product Dont Set Location
                        //Error = 204;
                        HttpContext.Current.Response.Redirect("~/admin/hotels2ErrorPage.aspx?error=204");
                    }
                }
                else
                {
                    // No folder Destination Source
                    //Error = 202;
                    HttpContext.Current.Response.Redirect("~/admin/hotels2ErrorPage.aspx?error=202");
                }
            }
            else
            {
                // No Folder Picture Sorce
                //Error = 201;
                HttpContext.Current.Response.Redirect("~/admin/hotels2ErrorPage.aspx?error=201");
            }

            return dirResult;
        }

        public DirectoryInfo GetDirectorySource()
        {
            DirectoryInfo dirSourcePath_Location = new DirectoryInfo(HttpContext.Current.Server.MapPath("../../" + this.SourcePath + "/" + this.DestinationPath + "/" + this.Location_Path));

            return dirSourcePath_Location;

        }


    }
}