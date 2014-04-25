using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Booking;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;
using Hotels2thailand;


namespace Hotels2thailand.UI
{
    public partial class extranet_ordercenter_popup_edit_option_detail : Hotels2BasePageExtra
    {
        public string qBookingId
        {
            get
            {
                return Request.QueryString["bid"];
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                int intOptionId = int.Parse(this.qOptionId);
                Option cOption = new Option();
                cOption = cOption.getOptionById(intOptionId);

                ProductOptionContent cOptionContent = new ProductOptionContent();
                cOptionContent = cOptionContent.GetProductOptionContentById(intOptionId, 1);

                txtTitle.Text = cOption.Title;
                txtDetail.Text = cOptionContent.Detail;
                txtSize.Text = cOption.Size.ToString();
                txtPri.Text = cOption.Priority.ToString();
                radioStatus.SelectedValue = cOption.Status.ToString();

                string ProductTpye = string.Empty;
                string ProductDetail = string.Empty;

                switch (cOption.CategoryID)
                {
                    case 38:
                        ProductTpye = "Room Type Title: ";
                        ProductDetail = "Room Type Detail: ";
                        break;
                    case 44:
                        ProductTpye = "Transfer: ";
                        ProductDetail = "Transfer Detail: ";
                        break;
                    case 39:
                        ProductTpye = "Extra bed: ";
                        ProductDetail = "Detail: ";
                        break;
                    case 58:
                        ProductTpye = "Meal: ";
                        ProductDetail = "Meal Detail: ";
                        break;
                }

                ltProductType.Text = ProductTpye;
                ltProductDetail.Text = ProductDetail;
                if (cOption.CategoryID == 38)
                {
                    panel_amen.Visible = true;

                    ProductOptionFacility cFac = new ProductOptionFacility();

                    StringBuilder result = new StringBuilder();

                    IList<object> iListAmen = cFac.getOptionFacilityByOptionId(intOptionId, 1);
                    if (iListAmen.Count > 0)
                    {
                        foreach (ProductOptionFacility fac in iListAmen)
                        {
                            result.Append("<p id=\"amen_" + fac.FacilityId + "\"><input type=\"checkbox\" style=\"display:none;\" checked=\"checked\" value=\"" + fac.FacilityId + "\" name=\"amenresult\" /><input type=\"text\" name=\"txt_amen_" + fac.FacilityId + "\" style=\"display:none;\" class=\"Extra_textbox\" value=\"" + fac.Title + "\" />" + fac.Title + "&nbsp;<img src=\"/images_extra/del.png\" onclick=\"del('" + fac.FacilityId + "');return false;\" />&nbsp;</p>");
                        }
                    }
                    else
                    {
                        result.Append("<div class=\"box_empty\">");
                        result.Append("");
                        result.Append("<p><label>No</label> amenities in this room.</p>");
                        result.Append("");
                        result.Append("</div>");
                    }


                    ltAmenlist.Text = result.ToString();
                    ltAmen.Visible = true;
                    ltSize.Visible = true;
                    txtSize.Visible = true;
                }
                else
                {
                    ltSize.Visible = false;
                    ltAmen.Visible = false;
                    txtSize.Visible = false;
                }

            }
        }

        public void btnSaveOption_Onclick(object sender, EventArgs e)
        {
            int intOptionId = int.Parse(this.qOptionId);
            ProductOptionContent cOptionContent = new ProductOptionContent();
            cOptionContent.UpdateOptionContentLangExtranet(this.CurrentProductActiveExtra, intOptionId, 1, txtTitle.Text, txtDetail.Text);
            Option cOption = new Option();
            cOption = cOption.getOptionById(intOptionId);
            cOption.Title = txtTitle.Text;
            cOption.Size = byte.Parse( txtSize.Text);
            cOption.Priority = byte.Parse(txtPri.Text);
            cOption.Status = bool.Parse(radioStatus.SelectedValue);
            cOption.Update();
            //ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>DarkmanPopUpComfirm('450','Mail was sent successfully.<br/> would like to close this window now !?','ParentFucntion();');</script>", false);

            ProductOptionFacility cFacdel = new ProductOptionFacility();
            cFacdel.DeleteFacByOptionId(intOptionId,1);
            if (!string.IsNullOrEmpty(Request.Form["amenresult"]))
            {
                foreach (string facKey in Request.Form["amenresult"].Split(','))
                {
                    ProductOptionFacility cFac = new ProductOptionFacility
                    {
                        OptionId = intOptionId,
                        LangId = 1,
                        Title = Request.Form["txt_amen_" + facKey]
                    };
                    cFac.InsertNewOptionFacility_Extra(cFac, this.CurrentProductActiveExtra);
                }

               
            }


            ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>ParentFucntion();</script>", false);
            
            
             
        }
    }
}