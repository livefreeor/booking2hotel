using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
/// <summary>
/// Summary description for GenerateFile
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class GenerateFile
    {
        private int ItemStart;
        private int ItemEnd;
        public string Path { get; set; }
        public string Filename { get; set; }
        public string Content { get; set; }
        private byte _langID = 1;

        public byte LangID {
            set { _langID = value; }
        }

        public GenerateFile()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        
        public GenerateFile(string path,string filename,string content) 
        {
            this.Path = path;
            this.Filename = filename;
            this.Content = content;
        }
        public GenerateFile(string path, string content)
        {
            content.Replace("http://174.36.32.56","http://www.hotels2thailand.com");

            StreamWriter StrWer = default(StreamWriter);
            try
            {
                //HttpContext.Current.Server.MapPath("")
                StrWer = File.CreateText(path);
                StrWer.Write(content);

                StrWer.Close();
                //HttpContext.Current.Response.Write(path+ "<br>");
            }
            catch (Exception ex)
            {
                //HttpContext.Current.Response.Write("<font color=\"red\">Could not create file :" + ex.Message+"</font>");
                //StrWer.Close();
                //HttpContext.Current.Response.End();
            }
        }
        protected string CreatePageNav() {
            string navHtml = "";
            return navHtml;
        }

        public bool CreateFile()
        {
            Content=Content.Replace("http://174.36.32.56", "http://www.hotels2thailand.com");
            Content = Content.Replace("hotels2thailandnew.com","hotels2thailand.com");
            Content = Content.Replace("<!--##GoogleAnalytics##-->", WebStatPlugIn.GoogleAnalytics(_langID));
            Content = Content.Replace("<!--##ClickTailTop##-->", WebStatPlugIn.ClickTailTopPart());
            Content = Content.Replace("<!--##ClickTailBottom##-->", WebStatPlugIn.ClickTailBottomPart());
            StreamWriter StrWer = default(StreamWriter);
            try
            {
                //HttpContext.Current.Server.MapPath("")
                StrWer = File.CreateText(Path+"/"+Filename);
                StrWer.Write(Content);

                StrWer.Close();
                HttpContext.Current.Response.Write(Path + "/" + Filename+"<br>");
                return true;
            }
            catch (Exception ex)
            {
               // HttpContext.Current.Response.Write("<font color=\"red\">Could not create file :" + Path + "/" + Filename+"</font>");
                //StrWer.Close();
                //HttpContext.Current.Response.End();
                return false;
            }

        }

        public string GetDataFromURL(string UrlPath)
        {
            String strResult;
            WebResponse objResponse;
            WebRequest objRequest = HttpWebRequest.Create(UrlPath);
            objResponse = objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                strResult = sr.ReadToEnd();
                sr.Close();
            }
            return strResult;
        }
    }
}