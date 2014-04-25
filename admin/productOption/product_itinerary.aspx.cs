using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI
{
    public partial class admin_product_itinerary : Hotels2BasePage
    {
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
             if (!this.Page.IsPostBack)
             {
                 Product cProduct = new Product();
                 cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                 txthead.Text = cProduct.ProductCode + "&nbsp;::&nbsp;" + cProduct.Title;

                 radioIsTimeCheckNo.Attributes.Add("onclick", "javascript:EnableDiv('DivTimeService');");
                 radioIsTimeCheckYes.Attributes.Add("onclick", "javascript:DisableDiv('DivTimeService');");
                 DropTimeDataBind();
                 GVItinerayListDataBind();

                 int intConditionId = int.Parse(this.qOptionId);
                 if (ProductItinerary.getDefaultItinerary(intConditionId) != null)
                 {
                   IsDefault.SelectedValue = ProductItinerary.getDefaultItinerary(intConditionId).ToString();
                 }

                 if (ProductItinerary.ItineraryCountByOptionId(int.Parse(this.qOptionId)) == 0)
                 {
                     panelIsDefault.Visible = false;
                 }

                 if (!string.IsNullOrEmpty(this.qOptionId))
                 {
                     Option cOption = new Option();
                     cOption.getOptionById(int.Parse(this.qOptionId));
                     lblOptionTitle.Text = cOption.Title;
                 }
             }
             else
             {
                
             }
          }



         public void DropTimeDataBind()
         {
             drpHrsStart.DataSource = this.dicGetNumber(23);
             drpHrsStart.DataTextField = "Value";
             drpHrsStart.DataValueField = "Key";
             drpHrsStart.DataBind();

             drpMinsStart.DataSource = this.dicGetTimEHrs(60);
             drpMinsStart.DataTextField = "Value";
             drpMinsStart.DataValueField = "Key";
             drpMinsStart.DataBind();

             drpHrsEnd.DataSource = this.dicGetNumber(23);
             drpHrsEnd.DataTextField = "Value";
             drpHrsEnd.DataValueField = "Key";
             drpHrsEnd.DataBind();

             drpMinsEnd.DataSource = this.dicGetTimEHrs(60);
             drpMinsEnd.DataTextField = "Value";
             drpMinsEnd.DataValueField = "Key";
             drpMinsEnd.DataBind();

             drpHrsStart.SelectedValue = DateTime.Now.Hour.ToString();
             drpMinsStart.SelectedValue = DateTime.Now.Minute.ToString();

             drpHrsEnd.SelectedValue = DateTime.Now.Hour.ToString();
             drpMinsEnd.SelectedValue = DateTime.Now.Minute.ToString();
         }

        


        public void IsDefault_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int intOptionId = int.Parse(this.qOptionId);
            bool Isdefault = bool.Parse(IsDefault.SelectedValue);

            if (ProductItinerary.getDefaultItinerary(intOptionId) != null)
            {
                ProductItinerary.UpdateItinerary(intOptionId, Isdefault);
            }
            else
            {
                ProductItinerary.InsertItinerary(intOptionId, Isdefault);
            }

            controltitleLang.DataBind();
        }

        public void GVItinerayListDataBind()
        {

            if (!string.IsNullOrEmpty(this.qOptionId))
            {
                int intConditionId = int.Parse(this.qOptionId);
                ProductItinerary cItinerary = new ProductItinerary();

                GVItinerayList.DataSource = cItinerary.GetIteneraryItemByItineraryIdAndConditionId(intConditionId);
                GVItinerayList.DataBind();
            }
        }

        public void GVItinerayList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTime = e.Row.Cells[1].FindControl("lblTimePrograme") as Label;
                int intItinerayr = (int)DataBinder.Eval(e.Row.DataItem, "ItineraryItemID");
                //Response.Write(intItinerayr+ "----");
                Hotels2thailand.UI.Controls.Control_Lang_Itinerary_Content_Box controlLang = e.Row.Cells[2].FindControl("ContentLang") as Hotels2thailand.UI.Controls.Control_Lang_Itinerary_Content_Box;
                controlLang.ItemId = intItinerayr;
                
                DateTime dTimeStart = (DateTime)DataBinder.Eval(e.Row.DataItem, "TimeStart");
                Nullable<DateTime> dTimeEnd = (Nullable<DateTime>)DataBinder.Eval(e.Row.DataItem, "TimeEnd");
                bool bolStatus = (bool)DataBinder.Eval(e.Row.DataItem, "Status");

                if (dTimeEnd == null)
                {
                    lblTime.Text = dTimeStart.ToShortTimeString();
                }
                else
                {
                    lblTime.Text = dTimeStart.ToShortTimeString() + " - " + DateTime.Parse(dTimeEnd.ToString()).ToShortTimeString();
                }
                

            }
        }

        public void imgBtDelete_Onclick(object sender, EventArgs e)
        {
            ImageButton imgbt = (ImageButton)sender;
            if (imgbt.CommandName == "ItiDel")
            {
                string[] Argument = imgbt.CommandArgument.Split(',');
                int intId = int.Parse(Argument[0]);
                int RowIndex = int.Parse(Argument[1]);

                foreach (GridViewRow item in GVItinerayList.Rows)
                {
                    if (item.RowType == DataControlRowType.DataRow)
                    {
                        if (RowIndex == item.RowIndex)
                        {
                            bool Del = ProductItinerary.DelItinerary(intId, this.Current_StaffLangId);
                        }
                    }
                }

            }

            Response.Redirect(Request.Url.ToString());
         }

         public void btnSave_Onclick(object sender, EventArgs e)
         {
             if (!string.IsNullOrEmpty(this.qProductId))
             {
                 int intConditionId = int.Parse(this.qOptionId);
                 ArrayList Result = ProductItinerary.GetItinerarytitleByItinerary(int.Parse((this.Page as Hotels2BasePage).qOptionId));
                 if (radioIsTimeCheckNo.Checked)
                 {
                     DateTime dTimeStart = new DateTime(1900, 09, 09, int.Parse(drpHrsStart.SelectedValue), int.Parse(drpMinsStart.SelectedValue), 0);
                     DateTime dTimeEnd = new DateTime(1900, 09, 09, int.Parse(drpHrsEnd.SelectedValue), int.Parse(drpMinsEnd.SelectedValue), 0);
                     
                     ProductItinerary.InsertNewItineary((int)Result[0], dTimeStart, dTimeEnd);
                 }

                 if (radioIsTimeCheckYes.Checked)
                 {
                     DateTime dTimeStart = new DateTime(1900, 09, 09, int.Parse(drpHrsStart.SelectedValue), int.Parse(drpMinsStart.SelectedValue), 0);

                     ProductItinerary.InsertNewItineary((int)Result[0], dTimeStart);
                 }

                 
             }

             Response.Redirect(Request.Url.ToString());
         }


         




        
    }
}