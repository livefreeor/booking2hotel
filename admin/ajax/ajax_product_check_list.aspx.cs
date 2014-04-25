using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.Suppliers;
using Hotels2thailand;
using Hotels2thailand.Staffs;
namespace Hotels2thailand.UI
{
    public partial class ajax_product_check_list : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {


                if (!string.IsNullOrEmpty(Request.QueryString["pid"]) && !string.IsNullOrEmpty(Request.QueryString["pt"]))
                {
                    ProductBookingEngine cProductEngine = new ProductBookingEngine();
                    cProductEngine = cProductEngine.GetProductbookingEngine(int.Parse(Request.QueryString["pid"]));
                    string SiteName = cProductEngine.WebsiteName;
                    string folder = cProductEngine.Folder;
                    string Url = string.Empty;
                    switch (Request.QueryString["pt"])
                    {
                        case "1":
                            Url = SiteName + "/" + folder + "_book.html";
                            break;
                        case "2":
                            Url = SiteName + "/" + folder + "_map.html";
                            break;
                        case "3":
                            Url = SiteName + "/" + folder + "_review.html";
                            break;
                        case "4":
                            Url = SiteName + "/" + folder + "_review_write.html";
                            break;
                        case "5":
                            Url = SiteName + "/" + folder + "_thankyou.html";
                            break;
                    }

                    Response.Write(CheckUrlExist(Url));
                    
                    Response.End();
                }
            }
        }

        public string CheckUrlExist(string url)
        {
            string ret = "";
            //bool returnValue = false;

            if (url.Length > 0)
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "HEAD";
                
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        response.GetResponseStream();
                        ret = (response.StatusCode == HttpStatusCode.OK) + "," + url;
;                        //ret = "{'datareturn':'" + (response.StatusCode == HttpStatusCode.OK) + "', 'urldes':'" + url + "}"; 
                    }
                }
                catch (WebException)
                {
                    ret = false + "," + url;
                    
                }
            }

            return ret;
        }






    }
}