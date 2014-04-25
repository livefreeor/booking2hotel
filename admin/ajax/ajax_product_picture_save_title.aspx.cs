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

public partial class ajax_product_picture_save_title : System.Web.UI.Page
{
    public string qProductId
    {
        get { return Request.QueryString["pid"]; }
    }

    public string qImageCat
    {
        get { return Request.QueryString["imgcat_id"]; }
    }
    public string qPicId
    {
        get { return Request.QueryString["picId"]; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.qProductId))
        {
            //Response.Write(Request.Url.ToString());
            Response.Write(SavePictureTitle());
            Response.End();
        }
    }

    public string SavePictureTitle()
    {
        
        string result = "False";
        
        ProductPic cProductPic = new ProductPic();
        
        String PicFileName = string.Empty;
        Language cLang = new Language();
        try
        {
            foreach (Language lang in cLang.GetLanguageAll())
            {
                ProductPicName cPicNameEn = new ProductPicName();
                cPicNameEn = cPicNameEn.getProductPicNameByIdAndLang(int.Parse(this.qPicId), lang.LanguageID);
                if (cPicNameEn != null)
                {
                    if (lang.LanguageID == 1)
                    {
                        cPicNameEn.Title = Request.Form["text_EN_" + this.qPicId + ""];
                    }
                    if (lang.LanguageID == 2)
                    {
                        cPicNameEn.Title = Request.Form["text_TH_" + this.qPicId + ""];
                    }
                    cPicNameEn.Update();
                }
                else
                {
                    if (lang.LanguageID == 1 && !string.IsNullOrEmpty(Request.Form["text_EN_" + this.qPicId + ""]))
                    {
                        ProductPicName cPicNameinsert = new ProductPicName
                        {
                            PicID = int.Parse(this.qPicId),
                            LangId = lang.LanguageID,
                            Title = Request.Form["text_EN_" + this.qPicId + ""]
                        };
                        cPicNameinsert.insertNewProductPicContent(cPicNameinsert);
                    }
                    if (lang.LanguageID == 2 && !string.IsNullOrEmpty(Request.Form["text_TH_" + this.qPicId + ""]))
                    {
                        ProductPicName cPicNameinsert = new ProductPicName
                        {
                            PicID = int.Parse(this.qPicId),
                            LangId = lang.LanguageID,
                            Title = Request.Form["text_TH_" + this.qPicId + ""]
                        };
                        cPicNameinsert.insertNewProductPicContent(cPicNameinsert);
                    }
                }
                result = "True";
            }
        }
        catch (Exception ex) { result = ex.Message; }

        return result;
    }
    

    
}