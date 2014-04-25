using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Text;
/// <summary>
/// Summary description for ReviewManage
/// </summary>
/// 
namespace Hotels2thailand.Affiliate
{
    public class SiteStat_Tracker:Hotels2BaseClass
    {

        protected string qAff_site 
        { 
            get 
            { 
                return HttpContext.Current.Request.QueryString["psid"];
            } 
        }
        protected string IP_ADDRESS 
        { 
            get 
            {
                return HttpContext.Current.Request.UserHostAddress; 
            } 
        }

        public SiteStat_Tracker()
        {
            
        }


        private  int InsertSiteStat(int intSiteId, DateTime dDateView, string strIP, string strURL_Ref )
        {
           
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_sites_stat (site_id,main_site_id,date_view,referer_ip,referer_url) VALUES (@site_id,'1',@date_view,@referer_ip,@referer_url)", cn);
                cmd.Parameters.Add("@site_id", SqlDbType.Int).Value = intSiteId;
                cmd.Parameters.Add("@date_view", SqlDbType.SmallDateTime).Value = dDateView;
                cmd.Parameters.Add("@referer_ip", SqlDbType.VarChar).Value = strIP;
                cmd.Parameters.Add("@referer_url", SqlDbType.VarChar).Value = strURL_Ref;
                cn.Open();
                int ret = this.ExecuteNonQuery(cmd);
                return ret;
            }
        }

        public int CountSite(int intSiteId)
        {
            using(SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(site_id) FROM tbl_aff_sites WHERE site_id=@site_id",cn);
                cmd.Parameters.Add("@site_id", SqlDbType.Int).Value = intSiteId;
                cn.Open();
                int ret  = (int)ExecuteScalar(cmd);
                return ret ;
            }
        }

        public ArrayList URLIsMatch(string Url_ref)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT site_id FROM tbl_aff_sites WHERE LOWER(url) = @url AND status=1", cn);
                cmd.Parameters.Add("@url", SqlDbType.VarChar).Value = Url_ref;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                ArrayList ret = new ArrayList();
                if (reader.Read())
                {
                    ret.Add((int)reader[0]);
                }
                return ret;
            }
        }

        private int IsHaveQueryString(string Url, string QueryKeyFind)
        {
            string QuerySTring = Url.Split('?')[1];
            NameValueCollection filtered = HttpUtility.ParseQueryString(QuerySTring);
            int ret = -1;
            if (!string.IsNullOrEmpty(filtered[QueryKeyFind]) && filtered[QueryKeyFind].Count() > 0)
            {
                ret = int.Parse(System.Web.HttpUtility.UrlEncode(filtered[QueryKeyFind]));
            }

            return ret;
        }

        private static void CreateCookieSession(int intKey)
        {
            HttpCookie cookiesSessionKey = new HttpCookie("site_id");

            cookiesSessionKey.Value = intKey.ToString();
            cookiesSessionKey.Expires = DateTime.Now.Hotels2ThaiDateTime().AddYears(1);
            HttpContext.Current.Response.Cookies.Add(cookiesSessionKey);
        }

        public string TrackingCheckAff_Site(string UrlRef)
        {
            string Result = "NOT COMPLETEDdd";

            //Step 1 Check Is Sub folder From Hotels2thailand URL ''http:// wwww.hotels2thailand.com/XXXX/YYY?psid=N
           
                if (!string.IsNullOrEmpty(this.qAff_site))
                {
                    try
                    {
                        if (this.CountSite(int.Parse(this.qAff_site)) > 0)
                        {
                            this.InsertSiteStat(int.Parse(this.qAff_site), DateTime.Now.Hotels2ThaiDateTime(), this.IP_ADDRESS, UrlRef);
                            //InSert Cookies
                            CreateCookieSession(int.Parse(this.qAff_site));
                        }

                        Result = "COMPLETED !!! ---FROM Sub Folder Psid" + this.qAff_site;
                    }
                    catch (Exception ex)
                    {
                        Result = "NOT COMPLETED ERROR 1" + ex.Message.ToString();
                    }
            
                    

                }

                else
                {
                    string Url_Ref_filtered = string.Empty;
                    
                    try
                    {

                        Url_Ref_filtered = UrlRef.Split('?')[0].Replace("http://", "").Replace("www.", "").Trim().ToLower().Split('/')[0];
                        ArrayList Match_siteId = this.URLIsMatch(Url_Ref_filtered);
                        

                        if (Match_siteId != null && Match_siteId.Count == 1)
                        {
                            this.InsertSiteStat((int)Match_siteId[0], DateTime.Now.Hotels2ThaiDateTime(), this.IP_ADDRESS, UrlRef);
                            
                            //InSert Cookies
                            CreateCookieSession((int)Match_siteId[0]);
                            Result = "COMPLETED !!! ---FROM URL REF refUrl=" + UrlRef + " AND SiteId=" + Match_siteId[0];
                        }
                        else
                        {
                            Result = "NOT URL PARTNER SITE !!! ---url reference not match in aff_sites";

                        }
                    }
                    catch (Exception ex)
                    {
                        Result = "NOT COMPLETED ERROR 1" + ex.Message.ToString() + UrlRef + "ssssss" + Url_Ref_filtered + "SITEID=";
                    }
                }

            return Result;

        }


        public static void ClearCookie()
        {
            //HttpCookie objCookie = HttpContext.Current.Request.Cookies["site_id"];
            //if (objCookie != null)
            //{
            //    objCookie.Expires = DateTime.Now.Hotels2ThaiDateTime().AddDays(-1D);
            //    HttpContext.Current.Response.Cookies.Add(objCookie);
            //}

            if (HttpContext.Current.Request.Cookies["site_id"] != null)
            {
                HttpCookie objCookie = new HttpCookie("site_id");
                //objCookie.Domain = "www.hotels2thailand.com";
                objCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(objCookie);
            }
            
            HttpContext.Current.Response.Redirect("http://www.hotels2thailand.com");

        }

        public static void ClearCookie_Only()
        {
            //HttpCookie cookiesSessionKey = new HttpCookie("site_id");
            //if (cookiesSessionKey != null)
            //    cookiesSessionKey.Expires = DateTime.Now.Hotels2ThaiDateTime().AddDays(-1D);

            //HttpCookie objCookie = HttpContext.Current.Request.Cookies["site_id"];
            //if (objCookie != null)
            //{
            //    objCookie.Expires = DateTime.Now.Hotels2ThaiDateTime().AddDays(-1D);
            //    HttpContext.Current.Response.Cookies.Add(objCookie);
            //}
            if (HttpContext.Current.Request.Cookies["site_id"] != null)
            {
                HttpCookie objCookie = new HttpCookie("site_id");
                //objCookie.Domain = "www.hotels2thailand.com";
                objCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(objCookie);
            }

            //HttpContext.Current.Response.Redirect("http://hotels2thailand.com");

        }
    }
}