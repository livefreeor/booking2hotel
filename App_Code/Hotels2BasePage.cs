using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Hotels2thailand.Staffs;



/// <summary>
/// Summary description for BasePage
/// </summary>
/// 
namespace Hotels2thailand.UI
{
    public class Hotels2BasePage : System.Web.UI.Page, ICallbackEventHandler
    {
        public string qProductId
        {
            get{ return Request.QueryString["pid"]; }
        }

        //cat_id
        public string qProductCat
        {
            get { return Request.QueryString["pdcid"]; }
        }

        public string qSupplierId
        {
            get { return Request.QueryString["supid"]; }
        }

        public string qOptionId
        {
            get { return Request.QueryString["oid"]; }
        }
        public string qConditionId
        {
            get { return Request.QueryString["cdid"]; }
        }

        public string qimageCat_id
        {
            get{ return Request.QueryString["imgcat_id"]; }
        }

        public string qAnnc_Id
        {
            get { return Request.QueryString["ancid"]; }
        }

        public string qConstructionId
        {
            get { return Request.QueryString["cons"]; }
        }

        public string qPolicyId
        {
            get { return Request.QueryString["polid"]; }
        }
        public string qcontactId
        {
            get { return Request.QueryString["contactId"]; }
        }

        public string qPromotionId
        {
            get { return Request.QueryString["proId"]; }
        }

        public string qMarketId
        {
            get { return Request.QueryString["mrid"]; }
        }

        public string qreview
        {
            get
            { return Request.QueryString["reviews"]; }
        }

        public string qreviewtype
        {
            get
            { return Request.QueryString["revType"]; }
        }

        public string qProductPage { get { return Request.QueryString["page"]; } }


        private int _sessionLogId = 0;

        public int SessionId
        {
            get 
            {
                if (Session["staff"] != null)
                {
                    _sessionLogId = int.Parse(Session["staff"].ToString());
                }
                return _sessionLogId; 
            }

        }

        private string Member_module
        {
            get { return ConfigurationManager.AppSettings["hotel_member_list"].ToString(); }
        }
        private string Newsletter_module
        {
            get { return ConfigurationManager.AppSettings["hotel_newsletter_list"].ToString(); }
        }

        public string CurrentIP_ADDRESS
        {
            get { return HttpContext.Current.Request.UserHostAddress; }
        }

        public byte CurrenStafftLangId
        {
            get
            {
                StaffSessionAuthorize cStaffAuthorize = new StaffSessionAuthorize();
                return cStaffAuthorize.CurrentLangId;
            }
        }
        public string CurrentCookieLogLangActive
        {
            get
            {
                return HttpContext.Current.Request.Cookies["SessionKey"]["LangActive"];
            }
        }
        public short CurrentStaffId
        {
            get
            {
                StaffSessionAuthorize cStaffAuthorize = new StaffSessionAuthorize();
                return cStaffAuthorize.CurrentStaffId;
            }
        }

        public Staff CurrentStaffobj
        {
            get
            {
                StaffSessionAuthorize cStaffAuthorize = new StaffSessionAuthorize();
                return cStaffAuthorize.CurrentClassStaff;
            }
        }

        public string LangShowtitle(byte bytLangId)
        {
            string strLangTitle = string.Empty;
            switch (bytLangId)
            {
                case 1:
                    strLangTitle = "ENG";
                    break;
                case 2:
                    strLangTitle = "TH";
                    break;
            }
            return strLangTitle;
        }


        protected void AddControls(ControlCollection page, ArrayList controlList)
        {
            foreach (Control c in page)
            {
                if (c.ID != null)
                {
                    controlList.Add(c);
                }

                if (c.HasControls())
                {
                    AddControls(c.Controls, controlList);
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {

            StaffSessionAuthorize.CheckCooikie();
            Staffs.StaffSessionAuthorize.Hotels2ThailandAuthorization();

            //HttpContext.Current.Response.Redirect("~/admin/attention.aspx");

           base.OnLoad(e);
        }

        public string QueryStringFilter(string Url, string QueryKey)
        {
            string QuerySTring = Url.Split('?')[1];
            NameValueCollection filtered = HttpUtility.ParseQueryString(QuerySTring);
            string[] ArrQueryKey = QueryKey.Split(',');
            foreach (string str in ArrQueryKey)
            {
              
                filtered.Remove(str);
            }
            

            List<String> items = new List<String>();
            foreach (String name in filtered)
                items.Add(String.Concat(name, "=", System.Web.HttpUtility.UrlEncode(filtered[name])));
           
            return "&" + String.Join("&", items.ToArray());

        }

        /// <summary>
        /// Return int if -1 == Not value
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="QueryKeyFind"></param>
        /// <returns></returns>
        public int IsHaveQueryString(string Url, string QueryKeyFind)
        {
            string QuerySTring = Url.Split('?')[1];
            NameValueCollection filtered = HttpUtility.ParseQueryString(QuerySTring);
            int ret = -1;
            if (!string.IsNullOrEmpty(filtered[QueryKeyFind]))
            {
                ret = int.Parse(System.Web.HttpUtility.UrlEncode(filtered[QueryKeyFind]));
            }

            return ret;
        }

        public string AppendCurrentQueryString()
        {
            StringBuilder strUrl = new StringBuilder();

            if (!string.IsNullOrEmpty(this.qSupplierId))
                strUrl.Append("&supid=" + this.qSupplierId);
                
            
            if (!string.IsNullOrEmpty(this.qProductCat))
                strUrl.Append("&pdcid=" + this.qProductCat);
               
            
            if (!string.IsNullOrEmpty(this.qProductId))
                strUrl.Append("&pid=" + this.qProductId);
                
            
            if (!string.IsNullOrEmpty(this.qMarketId))
                strUrl.Append("&mrid=" + this.qMarketId);
                
           
            if (!string.IsNullOrEmpty(this.qreview))
                strUrl.Append("&reviews=" + this.qreview);
                
            
            if (!string.IsNullOrEmpty(this.qreviewtype))
                strUrl.Append("&revType=" + this.qreviewtype);

            
            if (!string.IsNullOrEmpty(this.qProductPage))
                strUrl.Append("&page=" + this.qProductPage);


               
            return strUrl.ToString();
        }

        /// <summary>
        /// Implement ICallbackEventHandler Interface
        /// </summary>
        string callbackResult = "";
           
        public string GetCallbackResult()
        {
            return callbackResult;
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
           
        }
        //===========================================================

        /// <summary>
        /// Show Alert By Javascript
        /// </summary>
        /// <param name="message"></param>
        public  void ShowAlert(string message)
        {
            //Cleans the message to allow single quotation marks 
            string cleanMessage = message.Replace("'", "\'");
            string script = "<script type=text/javascript>alert('" + cleanMessage + "');</script>";

            //Gets the executing web page
            Page page = (Page)HttpContext.Current.CurrentHandler;

            //' Checks if the handler is a Page and that the script isn't allready on the Page
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), null, script, false);
               //ScriptManager.RegisterClientScriptBlock(typeof(Hotels2BasePage),null, "alert", script, true);
                
            }
        }

        protected string GetPostBackControl()
        {
            string Outupt = "";

            //get the __EVENTTARGET of the Control that cased a PostBack(except Buttons and ImageButtons)
            string targetCtrl = Page.Request.Params.Get("__EVENTTARGET");
            if (targetCtrl != null && targetCtrl != string.Empty)
            {
                //Outupt = targetCtrl + " button make Postback";
                Outupt = targetCtrl;
            }
            else
            {
                //if button is cased a postback
                foreach (string str in Request.Form)
                {
                    Control Ctrl = Page.FindControl(str);
                    if (Ctrl is Button)
                    {
                        Outupt = Ctrl.ID;
                        break;
                    }
                }
            }
            return Outupt;
        }

        /// <summary>
        /// Return Path from Root 
        /// </summary>
        public string BaseUrl
        {
            get
            {
                string url = this.Request.ApplicationPath;
                if (url.EndsWith("/"))
                    return url;
                else
                    return url + "/";
            }
        }

        /// <summary>
        /// ReDirect to Hotels2ErrorPage
        /// </summary>
        public static void RequestLogin()
        {
            HttpContext.Current.Response.Redirect("accessdenie.aspx?error=loginfailed");
        }

         
        public bool fileExist(string Pathfilename)
        {
           FileInfo fileFileName = new FileInfo(Pathfilename);

           if (fileFileName.Exists)
           {
               return true;
           }
           else
           {
               return false;
           }
            
        }
        public Dictionary<int, string> dicGetNumber(int intStart, int intNum)
        {
            Dictionary<int, string> iDic = new Dictionary<int, string>();

            for (int count = intStart; count <= intNum; count++)
            {
                iDic.Add(count, count.ToString());
            }
            return iDic;
        }
        public Dictionary<int, string> dicGetNumber(int intNum)
        {
            Dictionary<int, string> iDic = new Dictionary<int, string>();

            for (int count = 1; count <= intNum; count++)
            {
                iDic.Add(count, count.ToString());
            }
            return iDic;
        }

        

        public Dictionary<int, string> dicGetNumberstart0(int intNum)
        {
            Dictionary<int, string> iDic = new Dictionary<int, string>();

            for (int count = 0; count <= intNum; count++)
            {
                
                    iDic.Add(count, count.ToString());
                
                
            }
            return iDic;
        }
        public Dictionary<int, string> dicGetTimEHrs(int intNum)
        {
            Dictionary<int, string> iDic = new Dictionary<int, string>();

            for (int count = 0; count <= intNum; count++)
            {
                if (count < 10)
                {
                    iDic.Add(count, "0" + count.ToString());
                }
                else
                {
                    iDic.Add(count, count.ToString());
                }

            }
            return iDic;
        }

        public Dictionary<int, string> dicGetYear()
        {
            Dictionary<int, string> iDic = new Dictionary<int, string>();

            for (int count = 1905; count <= 2011; count++)
            {
                iDic.Add(count, count.ToString());
            }
            return iDic;
        }
        
    }
}
