using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI
{
    public partial class admin_product_product_picture :  Hotels2BasePage
    {
        //Declare Default ImageCat = 1 'mean Cat product
        public byte ImageCatId
        {
            get
            {
                if (!string.IsNullOrEmpty(this.qimageCat_id))
                {
                    return byte.Parse(this.qimageCat_id);
                }
                else
                {
                    return 1;
                };
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            paneloptionList.Visible = false;
            panelConstructionList.Visible = false;
            panelItineraryList.Visible = false;
            paneloptionEmpty.Visible = false;
            Option cOption = new Option();
            int intProductId = int.Parse(this.qProductId);
            switch(this.ImageCatId)
            {
                case 1 :
                    break;
                case 2:
                    
                    paneloptionList.Visible = true;
                    
                    if (cOption.GetProductOptionByCatIdSpecifyCat(intProductId).Count > 0)
                    {
                        drpOptionList.DataSource = cOption.GetProductOptionByCatIdSpecifyCat(intProductId);
                        drpOptionList.DataTextField = "Title";
                        drpOptionList.DataValueField = "OptionID";
                        drpOptionList.DataBind();
                    }
                    else
                    {
                        drpOptionList.Visible = false;
                        paneloptionEmpty.Visible = true;
                        panelpictureList.Visible = false;
                        panelpicture_insert.Visible = false;
                    }
                    break;
                case 3:
                    ProductConstruction cProductCon = new ProductConstruction();
                    panelConstructionList.Visible = true;
                    //Response.Write(cProductCon.GetConstructionByProductID(intProductId).Count);
                    //Response.End();
                    if (cProductCon.GetConstructionByProductID(intProductId).Count > 0)
                    {
                        drpConstrucTionList.DataSource = cProductCon.GetConstructionByProductID(intProductId);
                        drpConstrucTionList.DataTextField = "Title";
                        drpConstrucTionList.DataValueField = "ConstructionID";
                        drpConstrucTionList.DataBind();
                    }
                    else
                    {
                        drpConstrucTionList.Visible = false;
                        panelconEmpty.Visible = true;
                        panelpictureList.Visible = false;
                        panelpicture_insert.Visible = false;
                    }
                    break;
                case 4:
                        drpConstrucTionList.Visible = false;
                        panelconEmpty.Visible = true;
                        panelpictureList.Visible = false;
                        panelpicture_insert.Visible = false;
                    //paneloptionList.Visible = true;
                    //if (cOption.GetProductOptionByCatIdSpecifyCat(intProductId).Count > 0)
                    //{
                    //    drpOptionList.DataSource = cOption.GetProductOptionByCatIdSpecifyCat(intProductId);
                    //    drpOptionList.DataTextField = "Title";
                    //    drpOptionList.DataValueField = "OptionID";
                    //    drpOptionList.DataBind();
                    //}
                    //else
                    //{
                    //    drpOptionList.Visible = false;
                    //    paneloptionEmpty.Visible = true;
                    //    panelpictureList.Visible = false;
                    //    panelpicture_insert.Visible = false;
                    //}
                    //ProductItinerary cProductItinerary = new ProductItinerary();
                    //panelItineraryList.Visible = true;
                    //if (cProductItinerary.(intProductId).Count > 0)
                    //{

                    //    dropItinerary.DataSource = cProductItinerary.GetIteneraryByProduct(intProductId);
                    //    dropItinerary.DataTextField = "Title";
                    //    dropItinerary.DataValueField = "ItineraryID";
                    //    dropItinerary.DataBind();
                    //}
                    //else
                    //{
                    //    dropItinerary.Visible = false;
                    //    panelItineraryEmpty.Visible = true;
                    //    panelpictureList.Visible = false;
                    //    panelpicture_insert.Visible = false;
                    //}
                    break;
            }
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Product cProduct = new Product();
                cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                Destitle.Text = cProduct.DestinationTitle;
                txthead.Text = cProduct.Title;

                raioNewPic.SelectedValue = cProduct.IsnewPic.ToString();
                
                DropCatBinding();

                IDictionary<int, string> iDic = new Dictionary<int, string>();

                for (int count = 1; count <= 16; count++)
                {
                    iDic.Add(count, count.ToString());
                }
                numpicinsrt.DataSource = iDic;
                numpicinsrt.DataTextField = "Value";
                numpicinsrt.DataValueField = "Key";
                numpicinsrt.DataBind();

                //GridViewTypeParentBinding();
                lblPathUpLoad.Text = Hotels2thailand.Hotels2FolderAndPath.GetPicturePath(int.Parse(this.qProductId));
            }
        }

        public void raioNewPic_OnChange(object sender, EventArgs e)
        {
            Product cProduct = new Product();
            cProduct.UpdateProductNewPic(int.Parse(this.qProductId), bool.Parse(raioNewPic.SelectedValue));

            Response.Redirect(Request.Url.ToString());
        }
        public void DropCatBinding()
        {
            //drppicType.DataSource
            int intProductId = int.Parse((this.Page as Hotels2BasePage).qProductId);

            byte bytImageCat = 1;
            if (!string.IsNullOrEmpty((this.Page as Hotels2BasePage).qimageCat_id))
            {
                bytImageCat = byte.Parse((this.Page as Hotels2BasePage).qimageCat_id);
            }

            ProductPicType cProductPicType = new ProductPicType();
            drppicType.DataSource = cProductPicType.getPictureTypeAll(bytImageCat);
            drppicType.DataBind();
        }

       
    }
}