using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_country_market_list : Hotels2BasePage
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                MarketListDataBind();
                imgLinkNewMarket.NavigateUrl = "country_market.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat ;
            }
        }


        public void MarketListDataBind()
        {
            GVMarketList.DataSource = CountryMarket.getMarketALL();
            GVMarketList.DataBind();
        }


    }
}