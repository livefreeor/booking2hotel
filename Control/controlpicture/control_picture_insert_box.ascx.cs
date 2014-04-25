using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI.Controls
{
    public partial class Control_controlpicture_control_picture_insert_box : System.Web.UI.UserControl
    {
        private string _num_selected_value;
        public string NumSeletedValued
        {
            get { return _num_selected_value; }
            set { _num_selected_value = value; }
        }

        string _file_name = string.Empty;
        public string FileName
        {
            get
            {
                _file_name = txtfilename.Text;
            return _file_name;
            }
            set { _file_name = value; }
        }

        private byte _type_Selected;
        public byte Type_SeletectdValue
        {
            get { _type_Selected = byte.Parse(drppicType.SelectedValue);
            return _type_Selected;
            }
        }

        public string PictureName
        {
            get { return txtpicName.Text;}
        }

       
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {
                DropCatBinding();
                NumBinding();
                Binding();

            }
        }
        public void NumBinding()
        {
            drpDownNumBerOfPic.DataSource = (this.Page as Hotels2BasePage).dicGetNumber(30);
            drpDownNumBerOfPic.DataTextField = "Value";
            drpDownNumBerOfPic.DataValueField = "Key";
            drpDownNumBerOfPic.DataBind();
            drpDownNumBerOfPic.SelectedValue = this.NumSeletedValued;
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


        
        public void Binding()
        {

            //txtfilename.Text = string.Empty;
            byte bytcatId = 1;
            if (!string.IsNullOrEmpty((this.Page as Hotels2BasePage).qimageCat_id))
            {
                bytcatId = byte.Parse((this.Page as Hotels2BasePage).qimageCat_id);
            }
            string Cat_Title = ProductPicCategory.getCatTitleById(bytcatId);
            
            string typeTitle = string.Empty;
            if (string.IsNullOrEmpty(drppicType.SelectedValue))
            {
               typeTitle = ProductPicType.getTypeTitleById(1);
            }
            else
            {
                typeTitle = ProductPicType.getTypeTitleById(byte.Parse(drppicType.SelectedValue));
            }
            //string typeTitle = ProductPicType.getTypeTitleById(1);

            Product cProduct = new Product();
            cProduct = cProduct.GetProductById(int.Parse((this.Page as Hotels2BasePage).qProductId));
            switch (bytcatId)
            {
                //Product
                case 1:
                    
                    txtpicName.Text = Cat_Title + "-" + cProduct.Title + "-" + typeTitle;
                    txtfilename.Text = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title, typeTitle, drpDownNumBerOfPic.SelectedValue.ToString());
                    break;
                //cOption
                case 2:
                    DropDownList drpOptionList = (DropDownList)this.Page.Master.FindControl("ContentPlaceHolder1").FindControl("drpOptionList");
                    if (!string.IsNullOrEmpty(drpOptionList.SelectedValue))
                    {
                        txtpicName.Text = Cat_Title + "-" + drpOptionList.SelectedItem + "-" + typeTitle;
                        this.FileName = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title + "_" + drpOptionList.SelectedItem.ToString(), typeTitle, drpDownNumBerOfPic.SelectedValue.ToString());
                        txtfilename.Text = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title + "_" + drpOptionList.SelectedItem.ToString(), typeTitle, drpDownNumBerOfPic.SelectedValue.ToString());
                    }
                    break;
                //construction
                case 3:
                    DropDownList drpOptionListConstruction = (DropDownList)this.Page.Master.FindControl("ContentPlaceHolder1").FindControl("drpConstrucTionList");
                    if (!string.IsNullOrEmpty(drpOptionListConstruction.SelectedValue))
                    {
                        txtpicName.Text = Cat_Title + "-" + drpOptionListConstruction.SelectedItem + "-" + typeTitle;
                        this.FileName = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title + "_" + drpOptionListConstruction.SelectedItem.ToString(), typeTitle, drpDownNumBerOfPic.SelectedValue.ToString());
                        txtfilename.Text = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title + "_" + drpOptionListConstruction.SelectedItem.ToString(), typeTitle, drpDownNumBerOfPic.SelectedValue.ToString());
                    }
                    break;
                //Itinerary
                case 4:
                    DropDownList drpOptionListItinerary = (DropDownList)this.Page.Master.FindControl("ContentPlaceHolder1").FindControl("dropItinerary");
                    if (!string.IsNullOrEmpty(drpOptionListItinerary.SelectedValue))
                    {
                        txtpicName.Text = Cat_Title + "-" + drpOptionListItinerary.SelectedItem + "-" + typeTitle;
                        this.FileName = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title + "_" + drpOptionListItinerary.SelectedItem.ToString(), typeTitle, drpDownNumBerOfPic.SelectedValue.ToString());
                        txtfilename.Text = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title + "_" + drpOptionListItinerary.SelectedItem.ToString(), typeTitle, drpDownNumBerOfPic.SelectedValue.ToString());
                    }
                    break;
            }
            
        }
        

        public void drppicType_OnSeletedIndexChange(object sender, EventArgs e)
        {
           txtfilename.Text =  string.Empty;
           Binding();
        }

        public void drpDownNumBerOfPic_OnseletedIndexChange(object sender, EventArgs e)
        {
           txtfilename.Text = string.Empty;
           Binding();
        }
    }
}