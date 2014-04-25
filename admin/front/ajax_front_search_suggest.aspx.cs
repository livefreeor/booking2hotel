using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_front_search_suggest : System.Web.UI.Page
    {


        public string qProductCat
        {
            get { return Request.QueryString["keys"]; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                StringBuilder Result = new StringBuilder();
                SearchEngine c = new SearchEngine();
                ArrayList List = c.GetListSearchListSuggest();
                //["c++", "java", "php", "coldfusion", "javascript", "asp", "ruby", "python", "c", "scala", "groovy", "haskell", "pearl"]
                //int count = 1;
                //Result.Append("[");
                foreach (string strItem in List)
                {
                   Result.Append(strItem + ";!;");
                }
                
                Response.Write(Result.ToString().Hotels2RightCrl(1));
                Response.End();
            }
        }

        

        

       


       
        

    }
}