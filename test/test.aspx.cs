using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test_test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        StringBuilder mycode = new StringBuilder();
            mycode.Remove(0, mycode.Length);
            string txt = "";
            txt = "1234567890";

            mycode.Append(txt);

            string totaltxt = mycode.ToString();
            char[] myChar = totaltxt.ToCharArray();
            int MyLength = totaltxt.Length;

            Random myRandom = new Random();
            mycode.Remove(0, mycode.Length);

            for (int i = 0; i < 4; i++)
            {
                mycode.Append(myChar[myRandom.Next(0, MyLength)]);
            }
            //string result = mycode.ToString();

            Response.Write(mycode.ToString());
            Response.End();
    }

    //protected void ajaxFileUpload_OnUploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    //{
    //    if (e.ContentType.Contains("jpg") || e.ContentType.Contains("gif")
    //        || e.ContentType.Contains("png") || e.ContentType.Contains("jpeg"))
    //    {
    //        Session["fileContentType_" + e.FileId] = e.ContentType;
    //        Session["fileContents_" + e.FileId] = e.GetContents();
    //    }

    //    // Set PostedUrl to preview the uploaded file.         
    //    e.PostedUrl = string.Format("?preview=1&fileId={0}", e.FileId);
    //}    
}