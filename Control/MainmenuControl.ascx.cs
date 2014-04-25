using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotels2thailand.UI.Controls
{
    public partial class Control_MainmenuControl : System.Web.UI.UserControl
    { //proudct_id
        private string qProductId
        {
            get
            { return Request.QueryString["pid"]; }
        }

        //cat_id
        private string qProductCat
        {
            get { return Request.QueryString["pdcid"]; }
        }

        private string qSupplierId
        {
            get { return Request.QueryString["supid"]; }
        }

        private string qOptionId
        {
            get { return Request.QueryString["oid"]; }
        }

        private string _lv_seleted = null;
        public string LevelSelected
        {
            get { return _lv_seleted; }
            
            set { _lv_seleted = value; }
        }
        // Product List // NOT Complete
        private string strProductListLevel = "Product List";
        private string strProductLevel = "Product Manage";
        private string strOptionLevel = "Option List";
        private string strOptionDetailLavel = "Option Add-Edit";
       

        protected void Page_Load(object sender, EventArgs e)
        {
            string serverPath = "~/admin/";
            if (!this.Page.IsPostBack)
            {
                panelProductListLevel.Visible = false;
                panelProductLevel.Visible = false;
                panelOptionLevel.Visible = false;
                panelOptionDetailLevel.Visible = false;

                if (CheckLevelDirectivePage(strProductLevel))
                {
                    if (!string.IsNullOrEmpty(this.qProductId))
                    {
                        panelProductListLevel.Visible = true;
                        // Menu Link -----
                        //lnkOptionList.NavigateUrl = serverPath + "productOption/product_option_list.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat;
                        //lnkAnnoucement.NavigateUrl = serverPath + "product/annoucement_list.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat;
                        //lnkPicture.NavigateUrl = serverPath + "product/product_picture.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat;
                        //lnkPictureGen.NavigateUrl = serverPath + "product/product_picture_generate.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat;
                        //lnkPolicy.NavigateUrl = serverPath + "product/policy_list.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat;
                        //lnkPaymentPlan.NavigateUrl = serverPath + "product/paymentPlan_list.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat;
                        //lnkGalaDinner.NavigateUrl = serverPath + "productOption/product_option_gala.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat;
                        //lnkConStruction.NavigateUrl = serverPath + "product/product_construction.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat;
                        //lnkPolicy.NavigateUrl = serverPath + "product/product_policy.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat;
                        //lnkProductMinStay.NavigateUrl = serverPath + "product/product_minimum_stay.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat;
                        //lnkItinerary.NavigateUrl = serverPath + "product/product_itinerary.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat;
                    }
                }

                if (CheckLevelDirectivePage(strProductListLevel))
                {
                    panelProductLevel.Visible = true;
                    lnkMarket.NavigateUrl = serverPath + "product/country_market_list.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat;
                    lnkPublickHolidays.NavigateUrl = serverPath + "product/public_holidays.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat;
                    lnkFac.NavigateUrl = serverPath + "product/facility_manage.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat;
                    //lnkLandmark.NavigateUrl = serverPath + "product/landmark_list.aspx";
                    //lnkDestination.NavigateUrl = serverPath + "product/destination_list.aspx?pid";
                    //lnkLocation.NavigateUrl = serverPath + "product/location_list.aspx?pid";
                }



                if (CheckLevelDirectivePage(strOptionLevel))
                {
                    panelOptionLevel.Visible = true;
                    lnkRate.NavigateUrl = serverPath + "productOption/product_option_rate.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat;
                    lnkHoliday.NavigateUrl = serverPath + "productOption/product_option_holidays_supplement.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat;
                    lnkWeekday.NavigateUrl = serverPath + "productOption/product_option_weekday.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat;
                    lnkAllotment.NavigateUrl = serverPath + "productOption/product_option_allotment.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat;
                    lnkPromotion.NavigateUrl = serverPath + "productOption/product_option_promotion_list.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat;
                }

                if (CheckLevelDirectivePage(strOptionDetailLavel))
                {
                    panelOptionDetailLevel.Visible = true;
                    OptionItinerary.NavigateUrl = serverPath + "productOption/product_itinerary.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat + "&oid=" + (this.Page as Hotels2BasePage).qOptionId;
                    
                }
            }
        }

        

        public bool CheckLevelDirectivePage(string strLevel)
        {
            bool result = false;
            SiteMapNode current = SiteMap.CurrentNode;
            SiteMapNode root = SiteMap.RootNode;
           
            if (current.Title == strLevel)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            
            return result;
        }
    }
}
