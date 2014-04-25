using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_market_content_save : System.Web.UI.Page
    {
        public string qMarketId
        {
            get { return Request.QueryString["mrid"]; }
        }

        
        public byte Current_StaffLangId
        {
            get
            {
                Hotels2BasePage cBasePage = new Hotels2BasePage();
                return cBasePage.CurrenStafftLangId;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qProductCat))
                //{
                CountryMarketContent cMarketContent = new CountryMarketContent();

                bool IsCompleted = false;

                if (cMarketContent.IsHaveContentrecord(byte.Parse(this.qMarketId), this.Current_StaffLangId) > 0)
                {
                    IsCompleted = cMarketContent.UpdateMarketContent(byte.Parse(this.qMarketId), this.Current_StaffLangId, Request.Form["txtTitle"],
                    Request.Form["txtdetail"]);
                }
                else
                {

                    int ret = cMarketContent.Insert(byte.Parse(this.qMarketId), this.Current_StaffLangId, Request.Form["txtTitle"], Request.Form["txtdetail"]);
                    IsCompleted = (ret == 1);
                }

                Response.Write(IsCompleted);
                Response.End();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.End();
            }
            //}
            
        }

        
    }
}