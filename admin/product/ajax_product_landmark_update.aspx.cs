using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_landmark_update : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        public string qProductCat
        {
            get { return Request.QueryString["pdcid"]; }
        }

        public string qLanmarkId
        {
            get { return Request.QueryString["landMarkId"]; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qProductCat) && !string.IsNullOrEmpty(this.qLanmarkId))
            {
                ProductLandmark cProductLandMark = new ProductLandmark();
                byte bytTransId = byte.Parse(Request.Form["transport_cat_" + this.qLanmarkId]);
                byte bythrs = byte.Parse(Request.Form["txthrs_" + this.qLanmarkId]);
                byte bytmins = byte.Parse(Request.Form["txtmins_" + this.qLanmarkId]);
                short bytKM = short.Parse(Request.Form["txtKm_" + this.qLanmarkId]);
                short bytMETE = short.Parse(Request.Form["txtMeter_" + this.qLanmarkId]);

                byte bytTotalTime = CalculateTime(bythrs, bytmins);
                short shrTotalDistance = CalculateKilometer(bytKM, bytMETE);
                
                Response.Write(cProductLandMark.UpdateProductLandmark(int.Parse(this.qProductId), int.Parse(this.qLanmarkId), bytTransId, bytTotalTime, shrTotalDistance));
                Response.End();
            }
            
        }


        protected short CalculateKilometer(short shrKm, short shrM)
        {
            // Base Ratio
            short UnitRatio = 1000;
            //short shrMeteAmount = 0;
            short shrToMete = (short)(shrKm * UnitRatio);
            short shrMeteAmount = (short)(shrToMete + shrM);
            return shrMeteAmount;
        }

        protected byte CalculateTime(byte bytHrs, byte bytMinute)
        {
            // Base Ratio mins Per Hrs
            byte UnitRatio = 60;
            byte bytToMins = (byte)(bytHrs * UnitRatio);
            byte bytMinAmount = (byte)(bytToMins + bytMinute);
            return bytMinAmount;
        } 
        
        
    }
}