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
using System.Net.Mail;

public partial class ajax_product_picture_list_priority : System.Web.UI.Page
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
            Response.Write(SavePriority());
            Response.End();
        }
    }

    protected bool SavePriority()
    {
        bool Result = false;

        byte bytimgCat = byte.Parse(this.qImageCat);
        int intProductId = int.Parse(this.qProductId);
        ProductPicType cPicType = new ProductPicType();
        ProductPic cProductPic = new ProductPic();

        Dictionary<byte, string> objList = new Dictionary<byte, string>();
        List<object> picObject = new List<object>();
        switch (bytimgCat)
        {
            //case image cat == product
            case 1:
                objList = cPicType.getPictureTypeIsHaveRecord(bytimgCat, intProductId);
                break;
            case 2:
                string OptionId = Request.Form["ctl00$ContentPlaceHolder1$drpOptionList"];
                if (!string.IsNullOrEmpty(OptionId))
                {
                    objList = cPicType.getPictureTypeIsHaveRecord(bytimgCat, intProductId, int.Parse(OptionId));
                }
                break;
            case 3:
                string ConstructionID = Request.Form["ctl00$ContentPlaceHolder1$drpConstrucTionList"];
                if (!string.IsNullOrEmpty(ConstructionID))
                {
                    objList = cPicType.getPictureTypeIsHaveRecordConstruction(bytimgCat, intProductId, int.Parse(ConstructionID));
                }
                break;
            case 4:

                break;
        }
        if (objList.Count > 0)
        {
            
        }
        
        foreach (KeyValuePair<byte, string> Item in objList)
        {
            
            switch (bytimgCat)
            {
                //case image cat == product
                case 1:
                    picObject = cProductPic.getProductPicList(bytimgCat, Item.Key, intProductId);
                    break;
                case 2:
                    string OptionId = Request.Form["ctl00$ContentPlaceHolder1$drpOptionList"];
                    if (!string.IsNullOrEmpty(OptionId))
                    {
                        picObject = cProductPic.getProductPicList(bytimgCat, Item.Key, intProductId, int.Parse(OptionId));
                    }
                    break;
                case 3:
                    string ConstructionID = Request.Form["ctl00$ContentPlaceHolder1$drpConstrucTionList"];
                    if (!string.IsNullOrEmpty(ConstructionID))
                    {
                        picObject = cProductPic.getProductPicListConstruction(bytimgCat, Item.Key, intProductId, int.Parse(ConstructionID));
                    }
                    break;
                case 4:
                    //cProductPic.getProductPicListItinerary(bytImageCat, TypeId, intProductId, int.Parse(dropItinerary.SelectedValue));
                    break;
            }
            //Table Child Show you for PicList
            
            
            foreach (ProductPic picitem in picObject)
            {
               byte bytPriority = byte.Parse(Request.Form["drop_priority_" + picitem.PicID + ""]);
               if (bytPriority != picitem.Priority)
               {
                   Result = cProductPic.UpdatePriorityPic(picitem.PicID, bytPriority);
               }
            }

            
        }
        return Result;
    }
    

    
}