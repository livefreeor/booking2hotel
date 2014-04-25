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
using Hotels2thailand.Newsletters;



/// <summary>
/// Summary description for BasePage
/// </summary>
/// 
namespace Hotels2thailand.UI
{
    public class Hotels2BasePageExtra : System.Web.UI.Page
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

        private string Member_module
        {
            get { return ConfigurationManager.AppSettings["hotel_member_list"].ToString(); }
        }
        private string Newsletter_module
        {
            get { return ConfigurationManager.AppSettings["hotel_newsletter_list"].ToString(); }
        }

        public int CurrentProductActiveExtra
        {
            get
            {
                Staff cStaff = this.CurrentStaffobj;
                if (cStaff.Cat_Id == 6)
                {
                    return int.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["ProductActive"]);
                }
                else
                    return short.Parse(this.qProductId);
                
            }
        }

        public short CurrentSupplierId
        {
            get
            {
                Staff cStaff = this.CurrentStaffobj;
                if (cStaff.Cat_Id == 6)
                {
                    return short.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["SupplierActive"]);
                    //return this.CurrentStaffobj.Supplier_Id;
                }
                else
                    return short.Parse(this.qSupplierId);
                
                    
            }
        }
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

        private int _method_id = 0;
        public int Current_Staff_Method
        {
            get
            {

                if (Session["Method"] != null)
                {
                    _method_id = int.Parse(Session["Method"].ToString());
                }
                return _method_id; 
            }
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
        public string BaseModultPath
        {
            get { return ConfigurationManager.AppSettings["AuthorizeBaseURL_Extra"].ToString(); }
        }
        public string BaseModultPath_BlueHouse
        {
            get { return ConfigurationManager.AppSettings["AuthorizeBaseURL_Extra_BluehouseStaff"].ToString(); }
        }
        public short CurrentStaffId
        {
            get
            {
                StaffSessionAuthorize cStaffAuthorize = new StaffSessionAuthorize();
                return cStaffAuthorize.CurrentStaffId;
            }
        }
        public int CurrentCookieLog
        {
            get
            {
                return int.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["LogKey"]);
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


        protected override void OnLoad(EventArgs e)
        {
            StaffSessionAuthorize.CheckCooikie();

            
           // if(this.Request.QueryString)

            byte StaffCat = this.CurrentStaffobj.Cat_Id;

            ///--------
            if (string.IsNullOrEmpty(this.qProductId) && string.IsNullOrEmpty(this.qSupplierId) && StaffCat == 6)
                Staffs.StaffSessionAuthorize.Hotels2ThailandAuthorizationExtra();
            else if (string.IsNullOrEmpty(this.qProductId) && string.IsNullOrEmpty(this.qSupplierId) && StaffCat != 6)
            {
                Response.Redirect("~/extranet/staff-notification.aspx?error=productmiss");
            }
            else
            {

                Staffs.StaffSessionAuthorize.CreateCookieSessionExtra_bluehouse(this.CurrentCookieLog, int.Parse(this.qProductId), short.Parse(this.qSupplierId), this.CurrentCookieLogLangActive);
                Staffs.StaffSessionAuthorize.Hotels2ThailandAuthorization();
            }
            //---------

            //Service Newsletter
           // Newsletter.SendServiceNewsletter();
            //HttpContext.Current.Response.Redirect("~/extranet/staff-notifications.aspx");
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
                
            
               
            return strUrl.ToString();
        }

        

        /// <summary>
        /// ReDirect to Hotels2ErrorPage
        /// </summary>
        public static void RequestLogin()
        {
            HttpContext.Current.Response.Redirect("staff-notification.aspx?error=loginfailed");
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

        public bool IsStaffAdd()
        {
            bool result = false;
            Staff cStaff = this.CurrentStaffobj;
            if (cStaff.Cat_Id == 6)
            {
                switch (this.Current_Staff_Method)
                {
                    case 4:
                        result = true;
                        break;
                    case 5:
                        result = false;
                        break;
                    case 6:
                        result = true;
                        break;
                    case 7:
                        result = true;
                        break;
                }
            }
            else
            {
                result = true;
            }

            return result;
        }

        public bool IsStaffEdit()
        {
            bool result = false;
            Staff cStaff = this.CurrentStaffobj;
            if (cStaff.Cat_Id == 6)
            {
                switch (this.Current_Staff_Method)
                {
                    case 4:
                        result = true;
                        break;
                    case 5:
                        result = false;
                        break;
                    case 6:
                        result = true;
                        break;
                    case 7:
                        result = true;
                        break;
                }
            }
            else
            {
                result = true;
            }

            return result;
        }

        public bool IsstaffDelete()
        {
            bool result = false;
            Staff cStaff = this.CurrentStaffobj;
            if (cStaff.Cat_Id == 6)
            {
                switch (this.Current_Staff_Method)
                {
                    case 4:
                        result = true;
                        break;
                    case 5:
                        result = false;
                        break;
                    case 6:
                        result = false;
                        break;
                    case 7:
                        result = true;
                        break;
                }
            }
            else
            {
                result = true;
            }
            return result;
        }
             
    }
}
