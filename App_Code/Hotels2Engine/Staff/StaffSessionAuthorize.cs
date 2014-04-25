using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand;
using System.Web.SessionState;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Text;
using System.Text.RegularExpressions;
//using Hotels2thailand.LinqProvider.Staff;



/// <summary>
/// Summary description for StaffSession
/// </summary>
/// 
namespace Hotels2thailand.Staffs
{
    public class StaffSessionAuthorize : Hotels2BaseClass
    {
        public int SessionLogId { get; set; }
        public short Staff_Id { get; set; }
        public byte Lang_Id { get; set; }
        public string Browser { get; set; }
        public string LastAccessURL { get; set; }
        public string StaffIP { get; set; }
        public DateTime AccessTime { get; set; }
        public Nullable<DateTime> LeaveTime { get; set; }

        private string AuthorizeBaseUrl
        {
            get { return ConfigurationManager.AppSettings["AuthorizeBaseURL"].ToString(); }
        }

        private string AuthorizeBaseURL_Extra
        {
            get { return ConfigurationManager.AppSettings["AuthorizeBaseURL_Extra"].ToString(); }
        }
        private string AuthorizeBaseURL_Extra_BlueHouse_Staff
        {
            get { return ConfigurationManager.AppSettings["AuthorizeBaseURL_Extra_BluehouseStaff"].ToString(); }
        }
        private short _current_staff_Id = 0;
        public short getCurrentStaff_Id
        {
            get
            {
                int intLogKey = int.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["LogKey"]);
                if (intLogKey > 0)
                {
                    StaffSessionAuthorize cSession = new StaffSessionAuthorize();
                    _current_staff_Id = cSession.GetSessionRecord(intLogKey).Staff_Id;
                }
                return _current_staff_Id;
            }
        }

        public int CurrentProductActive_Extranet
        {
            get
            {
                return int.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["ProductActive"]);
            }
        }

        private Staff _classStaff = null;
        public Staff ClassStaff
        {
            get
            {
                if (_classStaff == null)
                {
                    Staff clStaff = new Staff();
                    _classStaff = clStaff.getStaffById(this.Staff_Id);
                }
                return _classStaff;
            }
        }

        private Staff _currentclassStaff = null;
        public Staff CurrentClassStaff
        {
            get
            {
                if (_currentclassStaff == null)
                {
                    Staff clStaff = new Staff();
                    _currentclassStaff = clStaff.getStaffById(this.CurrentStaffId);
                }
                return _currentclassStaff;
            }
        }

        public string SessionId
        {
            get
            {
                HttpSessionState Hotels2Session = HttpContext.Current.Session;
                return Hotels2Session.SessionID.ToString();
            }
        }

        private string _currenthotelsessionitem = string.Empty;
        public string HotelsSessionItem
        {
            get
            {
                if (HttpContext.Current.Session["staff"] != null)
                {
                    _currenthotelsessionitem = HttpContext.Current.Session["staff"].ToString();
                }
                return _currenthotelsessionitem;
            }
        }

        public string CurrentURL
        {
            get
            {
                return HttpContext.Current.Request.Url.ToString();
            }
        }

        public string CurrentURL_ref
        {
            get
            {
                return HttpContext.Current.Request.UrlReferrer.ToString();
            }
        }

        //return Current Lang Id
        public byte CurrentLangId
        {
            get 
            {
                return this.IsHaveSessionRecord(this.CurrentCookieLog).Lang_Id;
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
                return this.IsHaveSessionRecord(this.CurrentCookieLog).Staff_Id;
            }
        }

        
        public int CurrentCookieLog
        {
            get
            {
                return  int.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["LogKey"]);
            }
        }

        public int CurrentMethod { get; set;}

        //================= Method ===========================

        
        //declare Instant Linq for Staff_Category Module
        //LinqStaffDataContext dcStaffSession = new LinqStaffDataContext();

        public StaffSessionAuthorize IsHaveSessionRecord(int intSessionLogId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT SessionLogId,staff_id,lang_Id,browser,last_access_url,staff_ip,access_time,leave_time FROM tbl_staff_session WHERE SessionLogId=@SessionLogId", cn);
                cmd.Parameters.Add("@SessionLogId", SqlDbType.Int).Value = intSessionLogId;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return (StaffSessionAuthorize)MappingObjectFromDataReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }



        public StaffSessionAuthorize GetSessionRecord(int intSessionLogId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT SessionLogId,staff_id,lang_Id,browser,last_access_url,staff_ip,access_time,leave_time FROM tbl_staff_session WHERE SessionLogId=@SessionLogId", cn);
                cmd.Parameters.Add("@SessionLogId", SqlDbType.Int).Value = intSessionLogId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    
                    return (StaffSessionAuthorize)MappingObjectFromDataReader(reader);
                }
                else
                {
                    
                    return null;
                }
            }
            
        }


        public int CloseOtherCurrentLogin(short shtStaffId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_staff_session SET leave_time=@leave_time WHERE SessionLogId IN ((SELECT SessionLogId FROM tbl_staff_session WHERE staff_id = @staff_id AND leave_time IS NULL))", cn);
                cmd.Parameters.Add("@leave_time", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = shtStaffId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return ret;
            }
        }
        

        private int InsertNewStaffToSesstionRecord(StaffSessionAuthorize clStaffSession)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_staff_session (staff_id,lang_Id,browser,last_access_url,staff_ip,access_time)VALUES(@staff_id,@lang_Id,@browser,@last_access_url,@staff_ip,@access_time);SET @SessionId = scope_identity()", cn);
                cmd.Parameters.Add("@staff_id", SqlDbType.Int).Value = clStaffSession.Staff_Id;
                cmd.Parameters.Add("@lang_Id", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@browser", SqlDbType.NVarChar).Value = HttpContext.Current.Request.Browser.Browser.ToString();
                cmd.Parameters.Add("@last_access_url", SqlDbType.NVarChar).Value = clStaffSession.CurrentURL;
                cmd.Parameters.Add("@staff_ip", SqlDbType.VarChar).Value = HttpContext.Current.Request.UserHostAddress;
                cmd.Parameters.Add("@access_time", SqlDbType.DateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmd.Parameters.Add("@SessionId", SqlDbType.Int).Direction = ParameterDirection.Output;
                
                cn.Open();

                int ret = ExecuteNonQuery(cmd);
                //HttpContext.Current.Response.Write((int)cmd.Parameters["@SessionId"].Value);
                //HttpContext.Current.Response.End();
                return (int)cmd.Parameters["@SessionId"].Value;
            }
        }


        public bool UpdateSessionAuthorize(StaffSessionAuthorize clStaffSession)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_staff_session SET last_access_url = @last_access_url WHERE SessionLogId=@SessionLogId", cn);
                cmd.Parameters.Add("@SessionLogId", SqlDbType.Int).Value = clStaffSession.SessionLogId;
                cmd.Parameters.Add("@last_access_url", SqlDbType.NVarChar).Value = clStaffSession.CurrentURL;
                cn.Open();

                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
              
            }
        }

        //VERSION 2 ---update all record in tbl_staff_session
        public bool UpdateSessionAuthorizeLangauge(StaffSessionAuthorize clStaffSession)
        {
            //var result = dcStaffSession.tbl_staff_sessions.SingleOrDefault(ss => ss.SessionLogId == clStaffSession.SessionLogId);
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_staff_session SET lang_Id=@lang_Id,last_access_url = @last_access_url WHERE SessionLogId=@SessionLogId", cn);
                cmd.Parameters.Add("@SessionLogId", SqlDbType.Int).Value = clStaffSession.SessionLogId;
                cmd.Parameters.Add("@last_access_url", SqlDbType.NVarChar).Value = clStaffSession.CurrentURL;
                cmd.Parameters.Add("@lang_Id", SqlDbType.TinyInt).Value = clStaffSession.Lang_Id;
                cn.Open();

                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);

            }
           
        }
        //VERSION 2 ---update spectify Leave_time Record which to Logout 
        public bool UpdateSessionAuthorizeLogOut(StaffSessionAuthorize clStaffSession)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_staff_session SET last_access_url= @last_access_url,leave_time=@leave_time WHERE SessionLogId=@SessionLogId", cn);
                cmd.Parameters.Add("@SessionLogId", SqlDbType.Int).Value = clStaffSession.SessionLogId;
                cmd.Parameters.Add("@last_access_url", SqlDbType.NVarChar).Value = clStaffSession.CurrentURL;
                cmd.Parameters.Add("@leave_time", SqlDbType.DateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cn.Open();

                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);

            }
            
        }

        

        
        private static string[] UrlPageSplit(string strToSpite)
        {
            string[] strResult = strToSpite.Split(Convert.ToChar(","));
            return strResult;
        }

        
        //============= static Method =======================
        public static int InsertNewSessionLogDB(short shrStaffId)
        {
            StaffSessionAuthorize clStaffSession = new StaffSessionAuthorize { Staff_Id = shrStaffId };
            return clStaffSession.InsertNewStaffToSesstionRecord(clStaffSession);
        }

        public static bool UpdateSessionLog(int intSesstionId, byte bytLangId)
        {
            StaffSessionAuthorize clStaffSession = new StaffSessionAuthorize
            {
                SessionLogId = intSesstionId,
                Lang_Id = bytLangId
            };

            return clStaffSession.UpdateSessionAuthorizeLangauge(clStaffSession);
        }

        public static bool UpdateSessionStatus(int intSesstionId)
        {
            StaffSessionAuthorize clStaffSession = new StaffSessionAuthorize
            {
                SessionLogId = intSesstionId,
            };
            return clStaffSession.UpdateSessionAuthorize(clStaffSession);
        }

        public static bool UpdateSessionLogout(int intSesstionId)
        {
            StaffSessionAuthorize clStaffSession = new StaffSessionAuthorize
            {
                SessionLogId = intSesstionId,
            };
            return clStaffSession.UpdateSessionAuthorizeLogOut(clStaffSession);
        }


        public static void SessionCreate(short shrStaffId)
        {
            StaffSessionAuthorize clStaffSession = new StaffSessionAuthorize { Staff_Id = shrStaffId };
            HttpSessionState Hotels2Session = HttpContext.Current.Session;
            Hotels2Session["staff"] = clStaffSession.ClassStaff.Cat_Id.ToString();

            int Key =  clStaffSession.InsertNewStaffToSesstionRecord(clStaffSession);

            //creat Cookie for Reference user Login Life Time
            CreateCookieSession(Key);

            Staff cStaff = new Staff();
            cStaff.updateLastAccess(shrStaffId);

            HttpContext.Current.Response.Redirect("~/admin/mainpanel.aspx"); 
        }

        public static void SessionCreateExtra(short shrStaffId)
        {
            StaffSessionAuthorize clStaffSession = new StaffSessionAuthorize { Staff_Id = shrStaffId };
            HttpSessionState Hotels2Session = HttpContext.Current.Session;
            Hotels2Session["staff"] = clStaffSession.ClassStaff.Cat_Id.ToString();

            int Key = clStaffSession.InsertNewStaffToSesstionRecord(clStaffSession);

            StaffProduct_Extra cStaffProductExtra = new StaffProduct_Extra();
            cStaffProductExtra = cStaffProductExtra.GetProductTopDetaultByStaffId(shrStaffId);
            int ProductID = cStaffProductExtra.ProductID;
            short shrSupplierId = cStaffProductExtra.SupplierId;

            //creat Cookie for Reference user Login Life Time
            CreateCookieSessionExtra(Key, ProductID, shrSupplierId);

            Staff cStaff = new Staff();
            cStaff.updateLastAccess(shrStaffId);

            HttpContext.Current.Response.Redirect("~/extranet/mainextra.aspx");
        }

        public static void UpdateCookieSessionExtra_ProductActive(string ProductId, string shrSupplierId)
        {

            string LogKEy = HttpContext.Current.Request.Cookies["SessionKey"]["LogKey"];
            HttpContext.Current.Response.Cookies["SessionKey"]["LogKey"] = LogKEy;
            HttpContext.Current.Response.Cookies["SessionKey"]["ProductActive"] = ProductId;
            HttpContext.Current.Response.Cookies["SessionKey"]["SupplierActive"] = shrSupplierId;
            HttpContext.Current.Response.Cookies["SessionKey"].Expires = DateTime.Now.Hotels2ThaiDateTime().AddMonths(1);
            
            
        }

        public static void CreateCookieSession(int intKey)
        {
            HttpCookie cookiesSessionKey = new HttpCookie("SessionKey");

            cookiesSessionKey.Values["LogKey"] = intKey.ToString() ;
            cookiesSessionKey.Values["LangActive"] = "1";
            cookiesSessionKey.Expires = DateTime.Now.Hotels2ThaiDateTime().AddMonths(1);
            HttpContext.Current.Response.Cookies.Add(cookiesSessionKey);
        }

        public static void CreateCookieSessionExtra(int intKey, int ProductID, short shrSupplierId)
        {
            HttpCookie cookiesSessionKey = new HttpCookie("SessionKey");

            cookiesSessionKey.Values["LogKey"] = intKey.ToString();
            cookiesSessionKey.Values["LangActive"] = "1";
            cookiesSessionKey.Values["ProductActive"] = ProductID.ToString();
            cookiesSessionKey.Values["SupplierActive"] = shrSupplierId.ToString();
            cookiesSessionKey.Expires = DateTime.Now.Hotels2ThaiDateTime().AddMonths(1);
            HttpContext.Current.Response.Cookies.Add(cookiesSessionKey);
        }

        public static void CreateCookieSessionExtra_bluehouse(int intKey, int ProductID, short shrSupplierId, string LangActive)
        {
            HttpCookie cookiesSessionKey = new HttpCookie("SessionKey");

            cookiesSessionKey.Values["LogKey"] = intKey.ToString();
            cookiesSessionKey.Values["LangActive"] = LangActive;
            cookiesSessionKey.Values["ProductActive"] = ProductID.ToString();
            cookiesSessionKey.Values["SupplierActive"] = shrSupplierId.ToString();
            cookiesSessionKey.Expires = DateTime.Now.Hotels2ThaiDateTime().AddMonths(1);
            HttpContext.Current.Response.Cookies.Add(cookiesSessionKey);
        }
       
        public static void Logout()
        {
            HttpCookie objCookie = HttpContext.Current.Request.Cookies["SessionKey"];
            objCookie.Expires = DateTime.Now.Hotels2ThaiDateTime().AddDays(-1D);
            HttpContext.Current.Response.Cookies.Add(objCookie);

            HttpSessionState Hotels2Session = HttpContext.Current.Session;

            int intLogKey = int.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["LogKey"]);
            UpdateSessionLogout(intLogKey);

            Hotels2Session.Clear();
            HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=logout");

        }

        public static void Logout_Extra()
        {
            
            HttpCookie objCookie = HttpContext.Current.Request.Cookies["SessionKey"];
            objCookie.Expires = DateTime.Now.Hotels2ThaiDateTime().AddDays(-1D);
            HttpContext.Current.Response.Cookies.Add(objCookie);

            HttpSessionState Hotels2Session = HttpContext.Current.Session;

            int intLogKey = int.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["LogKey"]);
            UpdateSessionLogout(intLogKey);

            Hotels2Session.Clear();
            HttpContext.Current.Response.Redirect("~/extranet/logoutcompleted.aspx");
        }

        public static void LogoutStaffNotActivate()
        {
            HttpCookie objCookie = HttpContext.Current.Request.Cookies["SessionKey"];
            objCookie.Expires = DateTime.Now.Hotels2ThaiDateTime().AddDays(-1D);
            HttpContext.Current.Response.Cookies.Add(objCookie);

            HttpSessionState Hotels2Session = HttpContext.Current.Session;

            int intLogKey = int.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["LogKey"]);
            UpdateSessionLogout(intLogKey);

            Hotels2Session.Clear();
            HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=noactivate");
        }

        private int IsAuthorizeModuleExtra_ajax()
        {

            //bool condition = false;
            int intMethodID = -1;
            //Loop in record in URL in case Detected!!

            if (this.AuthorizeBaseURL_Extra == "*")
            {
                intMethodID = 0;
            }
            else
            {
                StaffPageAuthorizeExtra cstaffmodule = new StaffPageAuthorizeExtra();
                List<object> listStaffModule = cstaffmodule.GetModuleByStaffID(this.CurrentStaffId);


                if (this.CurrentURL_ref == (this.AuthorizeBaseURL_Extra + "mainextra.aspx"))
                {
                    intMethodID = 0;
                }
                else
                {

                    if (listStaffModule.Count > 0)
                    {

                        foreach (StaffPageAuthorizeExtra module in listStaffModule)
                        {
                            //string Pattern = this.AuthorizeBaseURL_Extra + module.Split('!')[0] + "/" + module.Split('!')[1];


                            if (HttpContext.Current.Request.UrlReferrer.ToString().Replace("http://", " ").Split('/')[2].Trim().ToLower() == module.ModuleFolderName.Trim().ToLower())
                            {
                                // condition = true;
                                intMethodID = module.MethodId;
                                break;
                            }
                        }
                    }
                }
            }


            return intMethodID;

        }
        private int IsAuthorizeModuleExtra()
        {

            //bool condition = false;
            int intMethodID = -1;
            //Loop in record in URL in case Detected!!
           
            StaffPageAuthorizeExtra cstaffmodule = new StaffPageAuthorizeExtra();
            List<object> listStaffModule = cstaffmodule.GetModuleByStaffID(this.CurrentStaffId);

            if (this.CurrentURL == (this.AuthorizeBaseURL_Extra + "mainextra.aspx"))
            {
                intMethodID = 0;
            }
            else
            {
                
                if (listStaffModule.Count > 0)
                {
                   
                    foreach (StaffPageAuthorizeExtra module in listStaffModule)
                    {
                        //string Pattern = this.AuthorizeBaseURL_Extra + module.Split('!')[0] + "/" + module.Split('!')[1];

                        
                        if (this.CurrentURL.Replace("http://", " ").Split('/')[2].Trim().ToLower() == module.ModuleFolderName.Trim().ToLower())
                        {
                            // condition = true;
                            intMethodID = module.MethodId;
                            break;
                        }
                    }
                }
            }


            return intMethodID;

        }

        private bool IsAuthorizePage()
        {

            bool condition = false;
            //Loop in record in URL in case Detected!!
            if (this.AuthorizeBaseUrl == "*")
            {
                condition = true;
            }
            else
            {
                StaffPageAuthorize staffPage = new StaffPageAuthorize();
                ArrayList dicPage = staffPage.getListPageByCatId(byte.Parse(this.HotelsSessionItem));

                if (dicPage.Count > 0)
                {
                    if (this.CurrentURL.Replace("http://", " ").Trim().Split('/').Count() >= 4)
                    {

                        foreach (string page in dicPage)
                        {
                            string Pattern = this.AuthorizeBaseUrl + page.Split('!')[0] + "/" + page.Split('!')[1];

                            string PatternExtra = this.AuthorizeBaseURL_Extra_BlueHouse_Staff + page.Split('!')[0] + "/" + page.Split('!')[1];


                            if (this.CurrentURL.Split('?')[0] == Pattern || !string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["t"]))
                            {
                                condition = true;
                                break;
                            }


                            if (this.CurrentURL.Split('?')[0] == PatternExtra || !string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["t"]))
                            {
                                condition = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        condition = true;
                    }

                }
            }
            
            return  condition;
            
        }
        private bool IsAuthorizePage_ajax()
        {

            bool condition = false;
            //Loop in record in URL in case Detected!!

            if (this.AuthorizeBaseUrl == "*")
            {
                condition = true;
            }
            else
            {
                StaffPageAuthorize staffPage = new StaffPageAuthorize();
                ArrayList dicPage = staffPage.getListPageByCatId(byte.Parse(this.HotelsSessionItem));

                if (dicPage.Count > 0)
                {
                    if (HttpContext.Current.Request.UrlReferrer.ToString().Replace("http://", " ").Trim().Split('/').Count() >= 4)
                    {
                        foreach (string page in dicPage)
                        {
                            string Pattern = this.AuthorizeBaseUrl + page.Split('!')[0] + "/" + page.Split('!')[1];

                            string PatternExtra = this.AuthorizeBaseURL_Extra_BlueHouse_Staff + page.Split('!')[0] + "/" + page.Split('!')[1];

                            // if Page Is match in database So The staff can not access 
                            // it's mean Super user there is no Page in list page 
                            if (HttpContext.Current.Request.UrlReferrer.ToString().Split('?')[0] == Pattern)
                            {
                                condition = true;
                                break;
                            }


                            if (HttpContext.Current.Request.UrlReferrer.ToString().Split('?')[0] == PatternExtra)
                            {
                                condition = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        condition = true;
                    }

                }
            }

            return condition;

        }

        public static void CheckCooikie()
        {
            HttpCookie objCookie = HttpContext.Current.Request.Cookies["SessionKey"];
            object objSession = HttpContext.Current.Session["staff"];

            string CurrentUrl = HttpContext.Current.Request.Url.ToString().Replace("http://", "").Trim().Split('/')[1].Trim();
                //.Split('/')[1].Trim();
            if (objSession == null)
            {
                if (objCookie == null)
                {
                    //if No cookie Return to Accessdenie Page to Login
                    if (CurrentUrl == "admin")
                    HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=requestlogin_111");

                    //if No cookie Return to Accessdenie Page to Login
                    if (CurrentUrl == "extranet")
                    HttpContext.Current.Response.Redirect("~/extranet/staff-notification.aspx?error=requestlogin");
                }
            }
            else
            {
                if (objCookie == null)
                {
                   //if No cookie Return to Accessdenie Page to Login
                    if (CurrentUrl == "admin")
                    HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=requestlogin_444");

                    //if No cookie Return to Accessdenie Page to Login
                    if (CurrentUrl == "extranet")
                        HttpContext.Current.Response.Redirect("~/extranet/staff-notification.aspx?error=requestlogin");


                   
                }
            }
        }

      

        public static void Hotels2ThailandAuthorization()
        {
            
            
            object objSession = HttpContext.Current.Session["staff"];
            HttpCookie objCookie = HttpContext.Current.Request.Cookies["SessionKey"];
            StaffSessionAuthorize clStaffSesssion = new StaffSessionAuthorize();

           

            //if User never Login 
            if (objSession == null)
            {
                //then if Check Cookie
                if (objCookie == null)
                {
                    //if No cookie Return to Accessdenie Page to Login
                    HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=requestlogin_111");
                   
                }
                // if Have a Cookie To Do..Thing    
                else
                {
                    //HttpContext.Current.Response.Write("Have Session1");
                    //HttpContext.Current.Response.End();
                    //define Int Variable to Cookie Key
                    int intLogKey = int.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["LogKey"]);
                    StaffSessionAuthorize clTblStaffSession = clStaffSesssion.IsHaveSessionRecord(intLogKey);
                    //HttpContext.Current.Response.Write(intLogKey);
                    //HttpContext.Current.Response.End();
                    if (clTblStaffSession != null)
                    {
                        if (clTblStaffSession.ClassStaff.Status != false)
                        {
                            if (clTblStaffSession.LeaveTime == null)
                            {
                                clStaffSesssion.Staff_Id = clTblStaffSession.Staff_Id;
                                HttpSessionState Hotels2Session = HttpContext.Current.Session;
                                Hotels2Session["staff"] = clStaffSesssion.ClassStaff.Cat_Id.ToString();

                                UpdateSessionStatus(intLogKey);
                            }
                            else
                            {
                                HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=requestlogin_222");
                            }

                        }
                        else
                        {
                            LogoutStaffNotActivate();
                        }
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=requestlogin_333");
                    }
                 }
            }
            //if Session Not Time Out
            else
            {
               
                //then if Check Cookie
                if (objCookie == null)
                {
                    //if No cookie Return to Accessdenie Page to Login
                    HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=requestlogin_444");

                   
                }
                // if Have a Cookie To Do..Thing    
                else
                {
                    //HttpContext.Current.Response.Write("Have Session2");
                    //HttpContext.Current.Response.End();
                    //define Int Variable to Cookie Key
                    int intLogKey = int.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["LogKey"]);
                    //HttpContext.Current.Response.Write(intLogKey);
                    //HttpContext.Current.Response.End();
                    StaffSessionAuthorize clTblStaffSession = clStaffSesssion.IsHaveSessionRecord(intLogKey);
                    if (clTblStaffSession != null)
                    {
                        //HttpContext.Current.Response.Write(clTblStaffSession.Staff_Id);
                        //HttpContext.Current.Response.End();
                        if (clTblStaffSession.ClassStaff.Status != false)
                        {
                            if (clTblStaffSession.LeaveTime == null)
                            {
                                clStaffSesssion.Staff_Id = clTblStaffSession.Staff_Id;
                                HttpSessionState Hotels2Session = HttpContext.Current.Session;
                                Hotels2Session["staff"] = clStaffSesssion.ClassStaff.Cat_Id.ToString();

                                UpdateSessionStatus(intLogKey);
                            }
                            else
                            {
                                HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=requestlogin_555");
                            }
                            
                        }
                        else
                        {
                            LogoutStaffNotActivate();
                        }
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=requestlogin_666");
                    }
                }

               
            }

            
            if (!clStaffSesssion.IsAuthorizePage())
            {
                HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=accessdenied");
            }
            else
            {
                HttpSessionState Hotels2SessionMethod = HttpContext.Current.Session;
                Hotels2SessionMethod["Method"] = clStaffSesssion.HotelsSessionItem + ";null";
            }
            

            

        }

        public static void Hotels2ThailandAuthorization_BlueHouse_ajax()
        {


            object objSession = HttpContext.Current.Session["staff"];
            HttpCookie objCookie = HttpContext.Current.Request.Cookies["SessionKey"];
            StaffSessionAuthorize clStaffSesssion = new StaffSessionAuthorize();



            //if User never Login 
            if (objSession == null)
            {
                //then if Check Cookie
                if (objCookie == null)
                {
                    //if No cookie Return to Accessdenie Page to Login
                    HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=requestlogin_111");

                }
                // if Have a Cookie To Do..Thing    
                else
                {

                    //define Int Variable to Cookie Key
                    int intLogKey = int.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["LogKey"]);
                    StaffSessionAuthorize clTblStaffSession = clStaffSesssion.IsHaveSessionRecord(intLogKey);
                    //HttpContext.Current.Response.Write(intLogKey);
                    //HttpContext.Current.Response.End();
                    if (clTblStaffSession != null)
                    {
                        if (clTblStaffSession.ClassStaff.Status != false)
                        {
                            if (clTblStaffSession.LeaveTime == null)
                            {
                                clStaffSesssion.Staff_Id = clTblStaffSession.Staff_Id;
                                HttpSessionState Hotels2Session = HttpContext.Current.Session;
                                Hotels2Session["staff"] = clStaffSesssion.ClassStaff.Cat_Id.ToString();

                                //UpdateSessionStatus(intLogKey);
                            }
                            else
                            {
                                HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=requestlogin_222");
                            }

                        }
                        else
                        {
                            LogoutStaffNotActivate();
                        }
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=requestlogin_333");
                    }
                }
            }
            //if Session Not Time Out
            else
            {

                //then if Check Cookie
                if (objCookie == null)
                {
                    //if No cookie Return to Accessdenie Page to Login
                    HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=requestlogin_444");


                }
                // if Have a Cookie To Do..Thing    
                else
                {
                    //define Int Variable to Cookie Key
                    int intLogKey = int.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["LogKey"]);
                    //HttpContext.Current.Response.Write(intLogKey);
                    //HttpContext.Current.Response.End();
                    StaffSessionAuthorize clTblStaffSession = clStaffSesssion.IsHaveSessionRecord(intLogKey);
                    if (clTblStaffSession != null)
                    {
                        //HttpContext.Current.Response.Write(clTblStaffSession.Staff_Id);
                        //HttpContext.Current.Response.End();
                        if (clTblStaffSession.ClassStaff.Status != false)
                        {
                            if (clTblStaffSession.LeaveTime == null)
                            {
                                clStaffSesssion.Staff_Id = clTblStaffSession.Staff_Id;
                                HttpSessionState Hotels2Session = HttpContext.Current.Session;
                                Hotels2Session["staff"] = clStaffSesssion.ClassStaff.Cat_Id.ToString();

                                //UpdateSessionStatus(intLogKey);
                            }
                            else
                            {
                                HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=requestlogin_555");
                            }

                        }
                        else
                        {
                            LogoutStaffNotActivate();
                        }
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=requestlogin_666");
                    }
                }


            }


            if (!clStaffSesssion.IsAuthorizePage_ajax())
            {
                HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=accessdenied");
            }
            else
            {
                HttpSessionState Hotels2SessionMethod = HttpContext.Current.Session;
                Hotels2SessionMethod["Method"] = clStaffSesssion.HotelsSessionItem + ";null";
            }




        }

        public static void Hotels2ThailandAuthorizationExtra()
        {

            object objSession = HttpContext.Current.Session["staff"];
            HttpCookie objCookie = HttpContext.Current.Request.Cookies["SessionKey"];
            StaffSessionAuthorize clStaffSesssion = new StaffSessionAuthorize();



            //if User never Login 
            if (objSession == null)
            {
                //then if Check Cookie
                if (objCookie == null)
                {
                    //if No cookie Return to Accessdenie Page to Login
                    HttpContext.Current.Response.Redirect("~/extranet/staff-notification.aspx?error=requestlogin");

                }
                // if Have a Cookie To Do..Thing    
                else
                {

                    //define Int Variable to Cookie Key
                    int intLogKey = int.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["LogKey"]);

                    
                    StaffSessionAuthorize clTblStaffSession = clStaffSesssion.IsHaveSessionRecord(intLogKey);
                    if (clTblStaffSession != null)
                    {
                        if (clTblStaffSession.ClassStaff.Status != false)
                        {
                            if (clTblStaffSession.LeaveTime == null)
                            {
                                clStaffSesssion.Staff_Id = clTblStaffSession.Staff_Id;
                                HttpSessionState Hotels2Session = HttpContext.Current.Session;
                                Hotels2Session["staff"] = clStaffSesssion.ClassStaff.Cat_Id.ToString();

                                UpdateSessionStatus(intLogKey);
                            }
                            else
                            {
                                HttpContext.Current.Response.Redirect("~/extranet/staff-notification.aspx?error=requestlogin");
                            }

                        }
                        else
                        {
                            LogoutStaffNotActivate();
                        }
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("~/extranet/staff-notification.aspx?error=requestlogin");
                    }
                }
            }
            //if Session Not Time Out
            else
            {

                //then if Check Cookie
                if (objCookie == null)
                {
                    //if No cookie Return to Accessdenie Page to Login
                    HttpContext.Current.Response.Redirect("~/extranet/staff-notification.aspx?error=requestlogin");


                }
                // if Have a Cookie To Do..Thing    
                else
                {
                    //define Int Variable to Cookie Key
                    int intLogKey = int.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["LogKey"]);
                    StaffSessionAuthorize clTblStaffSession = clStaffSesssion.IsHaveSessionRecord(intLogKey);
                    if (clTblStaffSession != null)
                    {
                        //HttpContext.Current.Response.Write(clTblStaffSession.Staff_Id);
                        //HttpContext.Current.Response.End();
                        if (clTblStaffSession.ClassStaff.Status != false)
                        {
                            if (clTblStaffSession.LeaveTime == null)
                            {
                                clStaffSesssion.Staff_Id = clTblStaffSession.Staff_Id;
                                HttpSessionState Hotels2Session = HttpContext.Current.Session;
                                Hotels2Session["staff"] = clStaffSesssion.ClassStaff.Cat_Id.ToString();

                                UpdateSessionStatus(intLogKey);
                            }
                            else
                            {
                                HttpContext.Current.Response.Redirect("~/extranet/staff-notification.aspx?error=requestlogin");
                            }

                        }
                        else
                        {
                            LogoutStaffNotActivate();
                        }
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("~/extranet/staff-notification.aspx?error=requestlogin");
                    }
                }


            }

            int Method = clStaffSesssion.IsAuthorizeModuleExtra();
            
            if (Method < 0)
            {

                HttpContext.Current.Response.Redirect("~/extranet/staff-notification.aspx?error=accessdenied");
            }
            else
            {
                HttpSessionState Hotels2SessionMethod = HttpContext.Current.Session;
                //Hotels2SessionMethod["Method"] = clStaffSesssion.HotelsSessionItem + ";" + Method;
                Hotels2SessionMethod["Method"] = Method;
                
                clStaffSesssion.CurrentMethod = Method;
                
            }




        }

        public static void Hotels2ThailandAuthorizationExtra_ajax()
        {


            object objSession = HttpContext.Current.Session["staff"];
            HttpCookie objCookie = HttpContext.Current.Request.Cookies["SessionKey"];
            StaffSessionAuthorize clStaffSesssion = new StaffSessionAuthorize();



            //if User never Login 
            if (objSession == null)
            {
                //then if Check Cookie
                if (objCookie == null)
                {
                    //if No cookie Return to Accessdenie Page to Login
                    HttpContext.Current.Response.Redirect("~/extranet/staff-notification.aspx?error=requestlogin");

                }
                // if Have a Cookie To Do..Thing    
                else
                {

                    //define Int Variable to Cookie Key
                    int intLogKey = int.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["LogKey"]);


                    StaffSessionAuthorize clTblStaffSession = clStaffSesssion.IsHaveSessionRecord(intLogKey);
                    if (clTblStaffSession != null)
                    {
                        if (clTblStaffSession.ClassStaff.Status != false)
                        {
                            if (clTblStaffSession.LeaveTime == null)
                            {
                                clStaffSesssion.Staff_Id = clTblStaffSession.Staff_Id;
                                HttpSessionState Hotels2Session = HttpContext.Current.Session;
                                Hotels2Session["staff"] = clStaffSesssion.ClassStaff.Cat_Id.ToString();

                                //UpdateSessionStatus(intLogKey);
                            }
                            else
                            {
                                HttpContext.Current.Response.Redirect("~/extranet/staff-notification.aspx?error=requestlogin");
                            }

                        }
                        else
                        {
                            LogoutStaffNotActivate();
                        }
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("~/extranet/staff-notification.aspx?error=requestlogin");
                    }
                }
            }
            //if Session Not Time Out
            else
            {

                //then if Check Cookie
                if (objCookie == null)
                {
                    //if No cookie Return to Accessdenie Page to Login
                    HttpContext.Current.Response.Redirect("~/extranet/staff-notification.aspx?error=requestlogin");


                }
                // if Have a Cookie To Do..Thing    
                else
                {
                    //define Int Variable to Cookie Key
                    int intLogKey = int.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["LogKey"]);
                    StaffSessionAuthorize clTblStaffSession = clStaffSesssion.IsHaveSessionRecord(intLogKey);
                    if (clTblStaffSession != null)
                    {
                        //HttpContext.Current.Response.Write(clTblStaffSession.Staff_Id);
                        //HttpContext.Current.Response.End();
                        if (clTblStaffSession.ClassStaff.Status != false)
                        {
                            if (clTblStaffSession.LeaveTime == null)
                            {
                                clStaffSesssion.Staff_Id = clTblStaffSession.Staff_Id;
                                HttpSessionState Hotels2Session = HttpContext.Current.Session;
                                Hotels2Session["staff"] = clStaffSesssion.ClassStaff.Cat_Id.ToString();

                                //UpdateSessionStatus(intLogKey);
                            }
                            else
                            {
                                HttpContext.Current.Response.Redirect("~/extranet/staff-notification.aspx?error=requestlogin");
                            }

                        }
                        else
                        {
                            LogoutStaffNotActivate();
                        }
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("~/extranet/staff-notification.aspx?error=requestlogin");
                    }
                }


            }

            int Method = clStaffSesssion.IsAuthorizeModuleExtra_ajax();

            if (Method < 0)
            {

                HttpContext.Current.Response.Redirect("~/extranet/staff-notification.aspx?error=accessdenied");
            }
            else
            {
                HttpSessionState Hotels2SessionMethod = HttpContext.Current.Session;
                //Hotels2SessionMethod["Method"] = clStaffSesssion.HotelsSessionItem + ";" + Method;
                Hotels2SessionMethod["Method"] = Method;

                clStaffSesssion.CurrentMethod = Method;

            }




        }
        

    }
}