using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class country_market_group : Hotels2BasePage
    {
        public string qGroupId
        {
            get { return Request.QueryString["mgid"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                dropGroupDataBind();
                GVContinentDataBind();

                byte GroupId = byte.Parse(dropGroup.SelectedValue);
                CountryMarket cMarketGroup = new CountryMarket();
                txttitleEdit.Text = cMarketGroup.GetGroptitleById(GroupId);

                this.Page.ClientScript.RegisterStartupScript(Page.GetType(), null, "<script>fnCheckUnCheckDefault();</script>", false);
                //IDictionary<byte, byte> CountryResult = CountryMarket.getCountryMappingByGroupId(GroupId);
                //Response.Write(CountryResult[1]);
                //Response.End();
            }
        }


        public void btnInsert_Onclick(object sender, EventArgs e)
        {
            CountryMarket cMarketGroup = new CountryMarket();
            cMarketGroup.InsertNewMarketGroup(txtTitle.Text);
            Response.Redirect(Request.Url.ToString());
        }

        public void btnEdit_Onclick(object sender, EventArgs e)
        {
            CountryMarket cMarketGroup = new CountryMarket();
            byte GroupId = byte.Parse(dropGroup.SelectedValue);
            cMarketGroup.UPdateNewMarketGroup(GroupId, txttitleEdit.Text);
            Response.Redirect(Request.Url.ToString());
        }

        public void dropGroupDataBind()
        {
            dropGroup.DataSource = CountryMarket.getMarketGroupALL();
            dropGroup.DataTextField = "Value";
            dropGroup.DataValueField = "Key";
            dropGroup.DataBind();
        }
        public void GVContinentDataBind()
        {
           GVContinent.DataSource= Country.GetContinent();
           GVContinent.DataBind();
        }

        public void GVContinent_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBoxList chkCountryList = e.Row.Cells[0].FindControl("chkCountryList") as CheckBoxList;
                byte ContinentId = (byte)GVContinent.DataKeys[e.Row.RowIndex].Value;

                Country cCountry = new Country();

                chkCountryList.DataSource = cCountry.GetCountryByCOntinentID(ContinentId);
                chkCountryList.DataTextField = "Title";
                chkCountryList.DataValueField = "CountryID";
                chkCountryList.DataBind();
                
                    byte GroupId = byte.Parse(dropGroup.SelectedValue);
                
                ArrayList CountryResult = CountryMarket.getCountryMappingByGroupId(GroupId);
                //CountryResult = CountryMarket.getCountryMappingByGroupId(GroupId);

                foreach (ListItem chkitem in chkCountryList.Items)
                {

                    chkitem.Attributes.Add("onclick", "javascript:fnCheckUnCheckcolor();");
                    if (!string.IsNullOrEmpty(dropGroup.SelectedValue))
                    {
                        foreach (byte countryItem in CountryResult)
                        {
                            if (chkitem.Value == countryItem.ToString())
                            {
                                chkitem.Selected = true;
                                
                               // chkitem.Attributes.Add("class", "list_market");
                            }
                        }
                    }
                }
            }

            //Response.Write("FR");
            //Response.End();
        }

        public void dropGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
            GVContinentDataBind();

            byte GroupId = byte.Parse(dropGroup.SelectedValue);
            CountryMarket cMarketGroup = new CountryMarket();
            txttitleEdit.Text = cMarketGroup.GetGroptitleById(GroupId);

            this.Page.ClientScript.RegisterStartupScript(Page.GetType(), null, "<script>fnCheckUnCheckDefault();</script>", false);
        }

        public void btnSaveSelection_OnClick(object sender, EventArgs e)
        {
            byte GroupId = byte.Parse(dropGroup.SelectedValue);
            foreach (GridViewRow GvRow in GVContinent.Rows)
            {
                CheckBoxList chkCountryList = GVContinent.Rows[GvRow.RowIndex].Cells[0].FindControl("chkCountryList") as CheckBoxList;
                foreach (ListItem chkitem in chkCountryList.Items)
                {
                    byte countryId = byte.Parse(chkitem.Value);
                    if (chkitem.Selected)
                    {
                        CountryMarket.inSertAndUpdateCountryMargetGroup(GroupId, countryId, true);
                    }
                    else
                    {
                        CountryMarket.inSertAndUpdateCountryMargetGroup(GroupId, countryId, false);
                    }

                   
                }
            }

            Response.Redirect(Request.Url.ToString());
            
        }

        
    }
}