using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Web.SessionState;

/// <summary>
/// Summary description for Captcha
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class Captcha
    {
        public Captcha()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void CreateCaptcha()
        {
            //get Random String to create captcha
            string Captcha = Hotels2String.Hotels2RandomStringNuM(5);

            //Create Session From Random string
            HttpSessionState Hotels2Session = HttpContext.Current.Session;
            Hotels2Session["Captcha"] = Captcha;

            // create JPEG Captcha
            captchas(Captcha);
            
            
        }

        public static bool CaptchaCheck(string input)
        {
            bool result = false;
            HttpSessionState Hotels2Session = HttpContext.Current.Session;

            if (HttpContext.Current.Session["Captcha"].ToString() == input)
                result = true;

            return result;
        }
        
        private static string RandomChatchaBG()
        {
            StringBuilder mycode = new StringBuilder();
            mycode.Remove(0, mycode.Length);
            string txt = "";
            txt = "123";

            mycode.Append(txt);

            string totaltxt = mycode.ToString();
            char[] myChar = totaltxt.ToCharArray();
            int MyLength = totaltxt.Length;

            Random myRandom = new Random();
            mycode.Remove(0, mycode.Length);

            for (int i = 0; i < 1; i++)
            {
                mycode.Append(myChar[myRandom.Next(0, MyLength)]);
            }
            string result = mycode.ToString();
            return result;
        }

        private static void captchas(string strCapt)
        {
            Bitmap bgCaptcha = new Bitmap(HttpContext.Current.Server.MapPath("/images/captcha_" + RandomChatchaBG()+ ".jpg"));

            //Bitmap bgCaptcha = new Bitmap(250,50,PixelFormat.Format32bppArgb);
            Graphics Grp = Graphics.FromImage(bgCaptcha);
            //80's hero;
            //Action Jackson;
            Font objFont = new Font("80's hero", 40, FontStyle.Bold);
            string TextCaptchar = strCapt;
            PointF objpoint = new PointF(20F, -2F);
            //SolidBrush brush = new SolidBrush(Color.FromArgb(57,96,146));
            //HatchBrush br = new HatchBrush(HatchStyle.DiagonalCross, Color.Red, Color.White);
            //Grp.FillEllipse(br, 1, 1, 250, 50);
            Grp.DrawString(TextCaptchar, objFont,  Brushes.White, objpoint);
            HttpContext.Current.Response.ContentType = "image/jpeg";
            bgCaptcha.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);
            bgCaptcha.Dispose();
        }

        //public static void LogoutStaffNotActivate()
        //{
        //    HttpCookie objCookie = HttpContext.Current.Request.Cookies["SessionKey"];
        //    objCookie.Expires = DateTime.Now.Hotels2ThaiDateTime().AddDays(-1D);
        //    HttpContext.Current.Response.Cookies.Add(objCookie);

            
        //    HttpSessionState Hotels2Session = HttpContext.Current.Session;

        //    int intLogKey = int.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["LogKey"]);
        //    UpdateSessionLogout(intLogKey);

        //    Hotels2Session.Clear();
        //    HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=noactivate");
        //}
    }
}