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
using System.Web.SessionState;



/// <summary>
/// Summary description for BasePage
/// </summary>
/// 
namespace Hotels2thailand.UI
{
    public class Hotels2BasePageExtra_Ajax : System.Web.UI.Page
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

        public string AppendQueryString
        {
            get
            {
                string AppendQueryString = string.Empty;
                if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qSupplierId))
                    AppendQueryString = "&pid=" + this.qProductId + "&supid=" + this.qSupplierId;
                return AppendQueryString;
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
        public int CurrentCookieLog
        {
            get
            {
                return int.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["LogKey"]);
            }
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
            //----------
            byte StaffCat = this.CurrentStaffobj.Cat_Id;
            HttpSessionState Hotels2SessionMethod = HttpContext.Current.Session;
            if (StaffCat == 6)
            {
                Staffs.StaffSessionAuthorize.Hotels2ThailandAuthorizationExtra_ajax();

            }
            else
            {

                Staffs.StaffSessionAuthorize.CreateCookieSessionExtra_bluehouse(this.CurrentCookieLog, this.CurrentProductActiveExtra, this.CurrentSupplierId, this.CurrentCookieLogLangActive);
                Staffs.StaffSessionAuthorize.Hotels2ThailandAuthorization_BlueHouse_ajax();

            }
            //--------

            base.OnLoad(e);

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

        //===========================================================


        

        /// <summary>
        /// ReDirect to Hotels2ErrorPage
        /// </summary>
        public static void RequestLogin()
        {
            HttpContext.Current.Response.Redirect("staff-notification.aspx?error=loginfailed");
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

        public string getPageList(int Total, int intPageStart)
        {
            string result = string.Empty;
            if (Total > 25)
            {
                int totalPage = (int)Math.Ceiling((double)Total / 25);
                

                result = result + "";

                string strClassactive = "";
                for (int i = 1; i <= totalPage; i++)
                {
                    int IndexPage = (25 * i) - 25;

                    if (intPageStart == i)
                        strClassactive = "page_list_active";
                    else
                        strClassactive = "page_list";

                    result = result + "<a id=\"strStatusbookingProduct_" + i + "\" class=\"" + strClassactive + "\"  title=\"" + i + "\" href=\"#\" onclick=\"getBookingPage('" + i + "');return false;\">" + i + "</a>";
                }

            }

            return result;
        }
    }
}
