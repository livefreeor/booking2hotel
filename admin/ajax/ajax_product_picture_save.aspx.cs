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


public partial class ajax_product_picture_save : System.Web.UI.Page
{
    public string qProductId
    {
        get { return Request.QueryString["pid"]; }
    }

    public string qImageCat
    {
        get { return Request.QueryString["imgcat_id"]; }
    }
    public string qMaxrow
    {
        get { return Request.QueryString["maxrow"]; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.qProductId))
        {
            //Response.Write(Request.Url.ToString());
            Response.Write(SavePicture());
            Response.End();
        }
    }

    public string SavePicture()
    {
        
        string result = "False";
        ProductPic cProductPic = new ProductPic();
        byte intImageCat = byte.Parse(this.qImageCat);
        String PicFileName = string.Empty;
        for (int count = 0; count < int.Parse(this.qMaxrow); count++)
        {
            byte bytType = byte.Parse(Request.Form["dropPictype_" + (count + 1 + "")]);
            string strFileName = Request.Form["txtfilename_" + (count + 1) + ""];
            string strPicName = Request.Form["hd_picname_" + (count + 1) + ""];
            
            PicFileName = Hotels2thailand.Hotels2FolderAndPath.GetPicturePath(int.Parse(this.qProductId)) + strFileName;
            switch (intImageCat)
            {
                //case image cat == product
                case 1:

                    cProductPic.InsertNewPicProduct(intImageCat, bytType, int.Parse(this.qProductId), PicFileName, strPicName, Convert.ToByte(count + 1));
                    break;
                case 2:
                    string OptionId = Request.Form["ctl00$ContentPlaceHolder1$drpOptionList"];
                    cProductPic.InsertNewPicOption(intImageCat, bytType, int.Parse(this.qProductId), int.Parse(OptionId), PicFileName, strPicName, Convert.ToByte(count + 1));
                    break;
                case 3:
                    string ConstructionID = Request.Form["ctl00$ContentPlaceHolder1$drpConstrucTionList"];
                    cProductPic.InsertNewPicConstruction(intImageCat, bytType, int.Parse(this.qProductId), int.Parse(ConstructionID), PicFileName, strPicName, Convert.ToByte(count + 1));
                    break;
                case 4:
                    //DropDownList dropOptionListItinerary = (DropDownList)this.Parent.FindControl("dropItinerary");
                    //cProductPic.InsertNewPicItinerary(intImageCat, insert_box.Type_SeletectdValue, intProductId, int.Parse(dropOptionListItinerary.SelectedValue), PicFileName, insert_box.PictureName, Convert.ToByte(count + 1));
                    break;
            }
            result = "True";

        }

        return result;
    }
    

    
}