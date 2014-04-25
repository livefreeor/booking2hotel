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
    public class Captcha_mail_tracking
    {
        public Captcha_mail_tracking()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void CreateCaptcha(int intBookingId, byte bytConfirmCat)
        {
            //get Random String to create captcha
            //string Captcha = Hotels2String.Hotels2RandomStringNuM(5);

            ////Create Session From Random string
            //HttpSessionState Hotels2Session = HttpContext.Current.Session;
            //Hotels2Session["Captcha"] = Captcha;

            // create JPEG Captcha
            Hotels2thailand.Booking.BookingConfirmEngine cBookingCon = new Hotels2thailand.Booking.BookingConfirmEngine();
            cBookingCon.UpdateConfirmMailtracking(intBookingId, bytConfirmCat);
            captchas();
            
            
        }

        public static bool CaptchaCheck(string input)
        {
            bool result = false;
            HttpSessionState Hotels2Session = HttpContext.Current.Session;

            //HttpContext.Current.Response.Write(HttpContext.Current.Session["Captcha"].ToString().ToUpper() + "-------" + input.Trim().ToUpper());
            //HttpContext.Current.Response.End();
            if (HttpContext.Current.Session["Captcha"].ToString().ToUpper().Trim() == input.Trim().ToUpper())
                result = true;

            return result;
        }
        
        

        private static void captchas()
        {
            Bitmap bgCaptcha = new Bitmap(HttpContext.Current.Server.MapPath("/images/mt.jpg"));

            //Bitmap bgCaptcha = new Bitmap(250,50,PixelFormat.Format32bppArgb);
            //Graphics Grp = Graphics.FromImage(bgCaptcha);
            ////80's hero;
            ////Action Jackson;
            //Font objFont = new Font("80's hero", 40, FontStyle.Bold);
            //string TextCaptchar = strCapt;
            //PointF objpoint = new PointF(20F, -2F);
            ////SolidBrush brush = new SolidBrush(Color.FromArgb(57,96,146));
            ////HatchBrush br = new HatchBrush(HatchStyle.DiagonalCross, Color.Red, Color.White);
            ////Grp.FillEllipse(br, 1, 1, 250, 50);
            //Grp.DrawString(TextCaptchar, objFont,  Brushes.White, objpoint);
            HttpContext.Current.Response.ContentType = "image/jpeg";
            bgCaptcha.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);
            bgCaptcha.Dispose();
        }

        
    }
}