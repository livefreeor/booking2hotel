using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_country_market : Hotels2BasePage
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                PanelEditdataBind();
                lnkMarket.NavigateUrl = "country_market_group.aspx?pid=" + (this.Page as Hotels2BasePage).qProductId + "&pdcid=" + (this.Page as Hotels2BasePage).qProductCat;
            }
        }
        
        public void PageDataBind()
        {
        }


        //panelInssert===========================================================
        public  void PanelInsertDataBind()
        {
        }

        public void btnInsert_Onclick(object sender, EventArgs e)
        {
            CountryMarket cMarket = new CountryMarket();
            int MarketID = cMarket.InsertNewMarket(txtTitle.Text);
            Response.Redirect("country_market.aspx?mrid="  + MarketID);
        }


        //panelEdit===========================================================
        public void PanelEditdataBind()
        {
            dropMarketDataBind();
            GVContinentDataBind();
            chkListGroupDataBind();
            GvForGVExceptDataBind();
        }
        public void dropMarketDataBind()
        {
            dropMarket.DataSource = CountryMarket.getMarketALL();
            dropMarket.DataTextField = "Value";
            dropMarket.DataValueField = "Key";
            dropMarket.DataBind();
            if (!string.IsNullOrEmpty(Request.QueryString["mrid"]))
            {
                dropMarket.SelectedValue = Request.QueryString["mrid"];
            }
        }

        public void dropMarket_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("country_market.aspx?mrid="  + dropMarket.SelectedValue);
        }
        public void GVContinentDataBind()
        {
            GVContinent.DataSource = Country.GetContinent();
            GVContinent.DataBind();
        }

        public void GVContinent_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBoxList chkCountryList = e.Row.Cells[0].FindControl("chkCountryList") as CheckBoxList;
                byte ContinentId = (byte)GVContinent.DataKeys[e.Row.RowIndex].Value;

                CountryMarket cCountrymarket = new CountryMarket();
                byte bytMarketId = byte.Parse(dropMarket.SelectedValue);
                IList<object> Result = cCountrymarket.getMarketGroupByMarketId(bytMarketId);
                StringBuilder stringParam = new StringBuilder();
                int count = 1;

                foreach (CountryMarket item in Result)
                {
                    if (item.GroupId == null)
                    {
                        stringParam.Append(item.CountryId + ",");
                    }
                    count = count + 1;
                }
               

                Country cCountry = new Country();
                chkCountryList.DataSource = cCountry.GetCountryByCOntinentID(ContinentId, stringParam.ToString().Hotels2RightCrl(1));
                chkCountryList.DataTextField = "Title";
                chkCountryList.DataValueField = "CountryID";
                chkCountryList.DataBind();

                //    byte GroupId = byte.Parse(dropGroup.SelectedValue);

                //    ArrayList CountryResult = CountryMarket.getCountryMappingByGroupId(GroupId);
                //    //CountryResult = CountryMarket.getCountryMappingByGroupId(GroupId);

                //    foreach (ListItem chkitem in chkCountryList.Items)
                //    {

                //        chkitem.Attributes.Add("onclick", "javascript:fnCheckUnCheckcolor();");
                //        if (!string.IsNullOrEmpty(dropGroup.SelectedValue))
                //        {
                //            foreach (byte countryItem in CountryResult)
                //            {
                //                if (chkitem.Value == countryItem.ToString())
                //                {
                //                    chkitem.Selected = true;

                //                    // chkitem.Attributes.Add("class", "list_market");
                //                }
                //            }
                //        }
                //    }
            }

            //Response.Write("FR");
            //Response.End();
        }

        public void chkListGroupDataBind()
        {
            CountryMarket cCountrymarket = new CountryMarket();
            byte bytMarketId = byte.Parse(dropMarket.SelectedValue);
            IList<object> Result = cCountrymarket.getMarketGroupByMarketId(bytMarketId);
            StringBuilder stringParam = new StringBuilder();
            int count = 1;

            foreach (CountryMarket item in Result)
            {
                if (item.CountryId == null)
                {
                    stringParam.Append(item.GroupId + ",");
                }
                count = count + 1;
            }

            

            chkListGroup.DataSource = CountryMarket.getMarketGroupALL(stringParam.ToString().Hotels2RightCrl(1));
            chkListGroup.DataTextField = "Value";
            chkListGroup.DataValueField = "Key";
            chkListGroup.DataBind();
        }

        public void  GvForGVExceptDataBind()
        {
            byte bytMarketId = byte.Parse(dropMarket.SelectedValue);
            CountryMarket cCountrymarket = new CountryMarket();

            txttitledit.Text = cCountrymarket.GetMarkettitleById(bytMarketId);

            GvFor.DataSource = cCountrymarket.getMarketGroupByMarketId(bytMarketId,false);
            GvFor.DataBind();

            GVExcept.DataSource = cCountrymarket.getMarketGroupByMarketId(bytMarketId, true);
            GVExcept.DataBind();
            
        }


        public void GvFor_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                Nullable<byte> CountryId = (Nullable<byte>)DataBinder.Eval(e.Row.DataItem, "CountryId");
                Nullable<byte> GroupId = (Nullable<byte>)DataBinder.Eval(e.Row.DataItem, "GroupId");
                Label lbltitle = e.Row.Cells[0].FindControl("lblmarketSelection") as Label;

                

                if (CountryId != null)
                {
                    Country cCountry = new Country();
                    string _GroupTitle = cCountry.GetCountryById((byte)CountryId).Title;

                    lbltitle.Text = "[Country] : " + _GroupTitle;
                }

                if (GroupId != null)
                {
                    CountryMarket cmarket = new CountryMarket();
                    string _CountryTitle = cmarket.GetGroptitleById((byte)GroupId);
                    lbltitle.Text = "[Group] : " + _CountryTitle;

                }
            }
        }


        public void GVExcept_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                Nullable<byte> CountryId = (Nullable<byte>)DataBinder.Eval(e.Row.DataItem, "CountryId");
                Nullable<byte> GroupId = (Nullable<byte>)DataBinder.Eval(e.Row.DataItem, "GroupId");

                Label lbltitle = e.Row.Cells[0].FindControl("lblmarketSelection") as Label;

                if (CountryId != null)
                {
                    Country cCountry = new Country();
                    string _GroupTitle = cCountry.GetCountryById((byte)CountryId).Title;

                    lbltitle.Text = "[Country] : " + _GroupTitle;
                }

                if (GroupId != null)
                {
                    CountryMarket cmarket = new CountryMarket();
                    string _CountryTitle = cmarket.GetGroptitleById((byte)GroupId);
                    lbltitle.Text = "[Group] : " + _CountryTitle;

                }
            }
        }

        public void imgbtn_Onclick(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            string[] argument = btn.CommandArgument.Split(',');
            byte bytMargetId = byte.Parse(argument[0]);
            Nullable<byte> bytCountryId;
            Nullable<byte> bytGroupId;
            //= byte.Parse(argument[1]);
            if (!string.IsNullOrEmpty(argument[1]))
                bytCountryId = byte.Parse(argument[1]);
            else
                bytCountryId = null;


            if (!string.IsNullOrEmpty(argument[2]))
                bytGroupId = byte.Parse(argument[2]);
            else
                bytGroupId = null;
            

            //Response.Write(bytMargetId + " " + bytCountryId + " " + bytGroupId);
            //Response.End();
            CountryMarket cCountryMarket = new CountryMarket();
            if (btn.CommandName == "RemoveRateFor")
            {
                cCountryMarket.DeleteMargetMapping(bytMargetId, bytCountryId, bytGroupId);
            }

            if (btn.CommandName == "RemoveExcept")
            {
                cCountryMarket.DeleteMargetMapping(bytMargetId, bytCountryId, bytGroupId);
            }

            Response.Redirect("country_market.aspx?mrid=" + dropMarket.SelectedValue);
            //Response.Redirect(Request.Url.ToString());
        }

        public void btnEdit_Onclick(object sender, EventArgs e)
        {
            byte bytMarketId = byte.Parse(dropMarket.SelectedValue);
            CountryMarket cMarket = new CountryMarket();
            cMarket.UPdateMarket(bytMarketId, txttitledit.Text);
            Response.Redirect("country_market.aspx?mrid=" + dropMarket.SelectedValue);
           // Response.Redirect(Request.Url.ToString());
        }

        public void btnCountry_OnClick(object sender, EventArgs e)
        {
            byte bytMarketId = byte.Parse(dropMarket.SelectedValue);

            foreach (GridViewRow GVRow in GVContinent.Rows)
            {
                CheckBoxList chkList = GVContinent.Rows[GVRow.RowIndex].Cells[0].FindControl("chkCountryList") as CheckBoxList;
                foreach (ListItem countryId in chkList.Items)
                {
                    if (countryId.Selected)
                    {
                        if (ViewState["IsExcept"] != null)
                        {
                            bool Isexcept = bool.Parse(ViewState["IsExcept"].ToString());
                            CountryMarket.InsertMappingCountryMarket_country(bytMarketId, byte.Parse(countryId.Value), Isexcept);
                        }
                    }

                }
            }

            Response.Redirect("country_market.aspx?mrid=" + dropMarket.SelectedValue);
            //Response.Redirect(Request.Url.ToString());
        }

        public void btnGroupsave_OnClick(object sender, EventArgs e)
        {
            byte bytMarketId = byte.Parse(dropMarket.SelectedValue);
            foreach (ListItem GroupId in chkListGroup.Items)
            {
                if (GroupId.Selected)
                {
                    if (ViewState["IsExcept"] != null)
                    {
                        bool Isexcept = bool.Parse(ViewState["IsExcept"].ToString());
                        CountryMarket.InsertMappingCountryMarket_Group(bytMarketId, byte.Parse(GroupId.Value), Isexcept);
                    }
                }
                
            }
            Response.Redirect("country_market.aspx?mrid=" + dropMarket.SelectedValue);
            //Response.Redirect(Request.Url.ToString());
        }

        public void MarketSelection_Onclick(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int argument = int.Parse(btn.CommandArgument);
            ViewState["IsExcept"] = string.Empty;
            if (btn.CommandName == "RateFor")
            {
                if (argument == 1)
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>showDiv('countrySel');</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>showDiv('groupSelect');</script>", false);
                }
                ViewState["IsExcept"] = "false";
            }

            if (btn.CommandName == "Except")
            {
                if (argument == 1)
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>showDiv('countrySel');</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>showDiv('groupSelect');</script>", false);
                }
                ViewState["IsExcept"] = "true";   
            }
            
        }
    }
}