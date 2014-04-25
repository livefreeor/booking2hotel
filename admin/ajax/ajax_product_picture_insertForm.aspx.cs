using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;


public partial class ajax_product_picture_insertForm : System.Web.UI.Page
{
    public string qProductId
    {
        get { return Request.QueryString["pid"]; }
    }

    public string qImageCat
    {
        get { return Request.QueryString["imgcat_id"]; }
    }
    public string qRowNum
    {
        get { return Request.QueryString["row"]; }
    }
    public string qimgType
    {
        get { return Request.QueryString["imgtype_id"]; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.qProductId))
        {
            Response.Write(PictureInsertForm());
            Response.End();
        }
    }

    public string PictureInsertForm()
    {
        StringBuilder result = new StringBuilder();
        for (int i = 0; i < int.Parse(this.qRowNum); i++)
        {
            int RowCurrent = (i+1);
            string Cat_Title = ProductPicCategory.getCatTitleById(byte.Parse(this.qImageCat));

            //string typeTitle = string.Empty;
            //if (string.IsNullOrEmpty(this.qimgType))
            //{
            //    typeTitle = ProductPicType.getTypeTitleById(1);
            //}
            //else
            //{
            //    typeTitle = ProductPicType.getTypeTitleById(byte.Parse(drppicType.SelectedValue));
            //}
            string typeTitle = ProductPicType.getTypeTitleById(byte.Parse(this.qimgType));
            string PicName = string.Empty;
            string PicFileName = string.Empty;
            Product cProduct = new Product();
            ProductContent cProductContent = new ProductContent();
            cProductContent = cProductContent.GetProductContentById(int.Parse(this.qProductId),1);
            cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
            switch (byte.Parse(this.qImageCat))
            {
                //Product
                case 1:
                    PicName = Cat_Title + "-" + cProduct.Title + "-" + typeTitle;
                    PicFileName = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProductContent.Title, typeTitle, (i + 1).ToString());
                    break;
                //cOption
                case 2:
                    string OptionId = Request.Form["ctl00$ContentPlaceHolder1$drpOptionList"];
                    if (!string.IsNullOrEmpty(OptionId))
                    {
                        
                        ProductOptionContent cOptionContent = new ProductOptionContent();
                        string OptionTitle = cOptionContent.GetProductOptionContentById(int.Parse(OptionId), 1).Title;
                        PicName = Cat_Title + "-" + OptionTitle + "-" + typeTitle;
                        //this.FileName = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title + "_" + drpOptionList.SelectedItem.ToString(), typeTitle, drpDownNumBerOfPic.SelectedValue.ToString());
                        PicFileName = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProductContent.Title + "_" + OptionTitle, typeTitle, (i + 1).ToString());
                    }
                    break;
                //construction
                case 3:
                    string ConstructionID = Request.Form["ctl00$ContentPlaceHolder1$drpConstrucTionList"];

                    if (!string.IsNullOrEmpty(ConstructionID))
                    {
                        ProductConstruction cConstruction = new ProductConstruction();
                        string ConstructionTitle = cConstruction.GetConstructionContentByID(int.Parse(ConstructionID), 1).Title;
                        PicName = Cat_Title + "-" + ConstructionTitle + "-" + typeTitle;
                        //this.FileName = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title + "_" + drpOptionListConstruction.SelectedItem.ToString(), typeTitle, drpDownNumBerOfPic.SelectedValue.ToString());
                        PicFileName = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProductContent.Title + "_" + ConstructionTitle, typeTitle, (i + 1).ToString());
                    }
                    break;
                //Itinerary
                //case 4:
                //    DropDownList drpOptionListItinerary = (DropDownList)this.Page.Master.FindControl("ContentPlaceHolder1").FindControl("dropItinerary");
                //    if (!string.IsNullOrEmpty(drpOptionListItinerary.SelectedValue))
                //    {
                //        PicName = Cat_Title + "-" + drpOptionListItinerary.SelectedItem + "-" + typeTitle;
                //        //this.FileName = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title + "_" + drpOptionListItinerary.SelectedItem.ToString(), typeTitle, drpDownNumBerOfPic.SelectedValue.ToString());
                //        PicFileName = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title + "_" + drpOptionListItinerary.SelectedItem.ToString(), typeTitle, (i + 1).ToString());
                //    }
                //    break;
            }

            result.Append("<table class=\"pic_insert_box\" id=\"pic_form_" + RowCurrent + "\" >");
            result.Append("<tr>");
            result.Append("<td colspan=\"3\">");
            result.Append("<p class=\"p_insert_box\">Picture Title : <span  class=\"span_insert_box\">" + PicName + "</span></p>");
            result.Append("<input type=\"hidden\" name=\"hd_picname_" + RowCurrent + "\" value=\"" + PicName + "\"/>");
            result.Append("</td>");
            result.Append("</tr>");
            result.Append(" <tr>");
            result.Append("<td width=\"20%\">");
            result.Append("<p>Picture Type</p>");
            result.Append("<select name=\"dropPictype_" + RowCurrent + "\" id=\"dropPictype_" + RowCurrent + "\" style=\"width:200px\" class=\"dropStyle\" onchange=\"dropPictypeChang('" + RowCurrent + "');\">");
            ProductPicType cProductPicType = new ProductPicType();

            foreach (KeyValuePair<byte, string> cPictype in cProductPicType.getPictureTypeAll(byte.Parse(this.qImageCat)))
            {
                if (this.qimgType == cPictype.Key.ToString())
                    result.Append("<option value=\"" + cPictype.Key + "\" selected=\"selected\" >" + cPictype.Value + "</option>");
                else
                    result.Append("<option value=\"" + cPictype.Key + "\"  >" + cPictype.Value + "</option>");

            }

            result.Append("</select>");
            result.Append("</td>");
            result.Append("<td width=\"10%\">");
            result.Append("<p>Pic Number</p>");
            result.Append("<select name=\"dropNumPic_" + RowCurrent + "\" id=\"dropNumPic_" + RowCurrent + "\" style=\"width:60px\" class=\"dropStyle\" onchange=\"dropNumPicChang('" + RowCurrent + "');\">");

            for (int num = 1; num < 30; num++)
            {
                if (i + 1 == num)
                    result.Append("<option value=\"" + num + "\" selected=\"selected\" >" + num + "</option>");
                else
                    result.Append("<option value=\"" + num + "\"  >" + num + "</option>");
            }

            result.Append("</select>");

            result.Append("</td>");
            result.Append("<td width=\"70%\">");
            result.Append("<p>file Name</p>");
            result.Append("<input type=\"text\" id=\"txtfilename_" + RowCurrent + "\" alt=\"" + typeTitle + "\" style=\"width:630px; padding:2px; color:red;font-size:11px; background-color:#faffbd;\" name=\"txtfilename_" + RowCurrent + "\" class=\"TextBox_Extra_normal_small\" value=\"" + PicFileName + "\"/>");
            
            result.Append("</td>");
            result.Append("</tr></table>");
        }

        result.Append("<br/><input type=\"button\" id=\"btnSave_picForm\" class=\"btStyleGreen\" name=\"btnSave_picForm\" onclick=\"SavePicForm();return false;\"  value=\"Save\"/>");
        return result.ToString();
    }




    //public void Binding()
    //{

    //    //txtfilename.Text = string.Empty;
    //    byte bytcatId = 1;
    //    if (!string.IsNullOrEmpty((this.Page as Hotels2BasePage).qimageCat_id))
    //    {
    //        bytcatId = byte.Parse((this.Page as Hotels2BasePage).qimageCat_id);
    //    }
        
    //}
    
}