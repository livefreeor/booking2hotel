using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI
{
    public partial class admin_product_picture_generate_product : Hotels2BasePage
    {
         protected void Page_Load(object sender, EventArgs e)
         {
             if (!this.Page.IsPostBack)
             {

                 Product cProduct = new Product();
                 cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                 txthead.Text = cProduct.ProductCode + "&nbsp;::&nbsp;" + cProduct.Title;


                 string Path = Request.QueryString["path"].Hotels2DecryptedData();
                 DirectoryInfo ProductPicFolder = new DirectoryInfo(Server.MapPath(Path));
                 int NumPic = ProductPicFolder.GetFiles().Count();

                 if (NumPic > 0)
                 {
                     StringBuilder OverView = new StringBuilder();
                     StringBuilder product_browse = new StringBuilder();
                     StringBuilder feature_hotel = new StringBuilder();
                     StringBuilder popular_small = new StringBuilder();
                     StringBuilder popular_large = new StringBuilder();
                     StringBuilder large = new StringBuilder();
                     StringBuilder thumb = new StringBuilder();

                     foreach (FileInfo fileItem in ProductPicFolder.GetFiles())
                     {
                         if (Regex.IsMatch(fileItem.Name, "^overview."))
                         {
                             OverView.Append("<div class=\"pic_item_gen\">");
                             OverView.Append("<img src=\"" + Path + "/" + fileItem.Name + "\" />");
                             OverView.Append("</div>");
                         }

                         if (Regex.IsMatch(fileItem.Name, "^product_browse."))
                         {
                             product_browse.Append("<div class=\"pic_item_gen\">");
                             product_browse.Append("<img src=\"" + Path + "/" + fileItem.Name + "\" />");
                             product_browse.Append("</div>");
                         }

                         if (Regex.IsMatch(fileItem.Name, "^feature_hotel."))
                         {
                             feature_hotel.Append("<div class=\"pic_item_gen\">");
                             feature_hotel.Append("<img src=\"" + Path + "/" + fileItem.Name + "\" />");
                             feature_hotel.Append("</div>");
                         }

                         if (Regex.IsMatch(fileItem.Name, "^popular_small."))
                         {
                             popular_small.Append("<div class=\"pic_item_gen\">");
                             popular_small.Append("<img src=\"" + Path + "/" + fileItem.Name + "\" />");
                             popular_small.Append("</div>"); ;
                         }

                         if (Regex.IsMatch(fileItem.Name, "^popular_large."))
                         {
                             popular_large.Append("<div class=\"pic_item_gen\">");
                             popular_large.Append("<img src=\"" + Path + "/" + fileItem.Name + "\" />");
                             popular_large.Append("</div>");
                         }

                         if (Regex.IsMatch(fileItem.Name, "^large."))
                         {
                             large.Append("<div class=\"pic_item_gen\">");
                             large.Append("<img src=\"" + Path + "/" + fileItem.Name + "\" />");
                             large.Append("</div>");
                         }

                         if (Regex.IsMatch(fileItem.Name, "^thumb."))
                         {
                             thumb.Append("<div class=\"pic_item_gen\">");
                             thumb.Append("<img src=\"" + Path + "/" + fileItem.Name + "\" />");
                             thumb.Append("</div>");
                         }
                     }

                     lblOver.Text = OverView.ToString();
                     lblproduct_browse.Text = product_browse.ToString();
                     lblfeature_hotel.Text = feature_hotel.ToString();
                     lblpopular_small.Text = popular_small.ToString();
                     lblpopular_large.Text = popular_large.ToString();
                     lbllarge.Text = large.ToString();
                     lblthumb.Text = thumb.ToString();
                 }
                 else
                 {
                     panelEmpty.Visible = true;
                     panelinsert.Visible = false;
                 }

                 
            }
         }


         public void GenPic_ONclick(object sender, EventArgs e)
         {

             string Path = Request.QueryString["path"].Hotels2DecryptedData();
             DirectoryInfo ProductPicFolder = new DirectoryInfo(Server.MapPath(Path));
             
             int intProductId = int.Parse(this.qProductId);
             int Process = Hotels2thailand.Hotels2FolderAndPath.PicturefolderAndPathGenerate(intProductId);
             //Response.Write(Process);
             //Response.End();
             if (Process == 102)
             {
                 // error : Destination Folder Name Are Empty!! Please Insert
                 Response.Redirect("~/admin/hotels2exception.aspx");
                 return;
             }
             if (Process == 103)
             {
                 // error : There are no record Location in this product, Please Insert Lacation before
                 Response.Redirect("~/admin/hotels2exception.aspx");
                 return;
             }
             if (Process == 104)
             {
                 // error : Location Folder Name Are Empty!! Please Insert
                 Response.Redirect("~/admin/hotels2exception.aspx");
                 return;
             }
             if (Process == 0)
             {
                Product cProduct = new Product();
                cProduct = cProduct.GetProductById(int.Parse(this.qProductId));

                
                 // Product Module
                string typeTitle = string.Empty;
                string Cat_Title = ProductPicCategory.getCatTitleById(1);

               
                ProductPic cProductPic = new ProductPic();
                ProductPicName cProductPicContent = new ProductPicName();

                string PicFileName = string.Empty;
                string PicFileNamePath = string.Empty;

                string PicTitle = string.Empty;
                int PicId = 0;
                int countoverview = 1;
                int countproduct_browse = 1;
                int countfeature_hotel = 1;
                int countpopular_small = 1;
                int countpopular_large = 1;
                int countlarge = 1;
                int countThumb = 1;

                foreach (FileInfo fileItem in ProductPicFolder.GetFiles())
                {
                    
                    if (Regex.IsMatch(fileItem.Name, "^overview"))
                    {
                        typeTitle = ProductPicType.getTypeTitleById(1);
                        PicTitle = Cat_Title + "-" + cProduct.Title + "-" + typeTitle;

                        PicFileName = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title, typeTitle, countoverview.ToString());
                        PicFileNamePath = Hotels2thailand.Hotels2FolderAndPath.GetPicturePath(intProductId);

                       PicId =  cProductPic.InsertNewPicProduct(1, 1, intProductId, PicFileNamePath + PicFileName, PicTitle, 1);
                       cProductPicContent.insertNewProductPicContent(PicId, 1, cProduct.Title + "OverAll");
                       fileItem.MoveTo(Server.MapPath("/" + PicFileNamePath + PicFileName));

                        countoverview = countoverview + 1;
                    }

                    
                    if (Regex.IsMatch(fileItem.Name, "^product_browse."))
                    {
                        typeTitle = ProductPicType.getTypeTitleById(3);
                        PicTitle = Cat_Title + "-" + cProduct.Title + "-" + typeTitle;

                        PicFileName = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title, typeTitle, countproduct_browse.ToString());
                        PicFileNamePath = Hotels2thailand.Hotels2FolderAndPath.GetPicturePath(intProductId);
                        PicId = cProductPic.InsertNewPicProduct(1, 3, intProductId, PicFileNamePath + PicFileName, PicTitle, 1);
                        //cProductPicContent.insertNewProductPicContent(PicId, 1, cProduct.Title + "OverAll");
                        fileItem.MoveTo(Server.MapPath("/" + PicFileNamePath + PicFileName));

                        countproduct_browse = countproduct_browse + 1;
                    }

                   
                    if (Regex.IsMatch(fileItem.Name, "^feature_hotel."))
                    {
                        typeTitle = ProductPicType.getTypeTitleById(4);
                        PicTitle = Cat_Title + "-" + cProduct.Title + "-" + typeTitle;

                        PicFileName = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title, typeTitle, countfeature_hotel.ToString());
                        PicFileNamePath = Hotels2thailand.Hotels2FolderAndPath.GetPicturePath(intProductId);
                        PicId = cProductPic.InsertNewPicProduct(1, 4, intProductId, PicFileNamePath + PicFileName, PicTitle, 1);
                        //cProductPicContent.insertNewProductPicContent(PicId, 1, cProduct.Title + "OverAll");
                        fileItem.MoveTo(Server.MapPath("/" + PicFileNamePath + PicFileName));

                        countfeature_hotel = countfeature_hotel + 1;
                    }

                    
                    if (Regex.IsMatch(fileItem.Name, "^popular_small."))
                    {
                        typeTitle = ProductPicType.getTypeTitleById(5);
                        PicTitle = Cat_Title + "-" + cProduct.Title + "-" + typeTitle;

                        PicFileName = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title, typeTitle, countpopular_small.ToString());
                        PicFileNamePath = Hotels2thailand.Hotels2FolderAndPath.GetPicturePath(intProductId);
                        PicId = cProductPic.InsertNewPicProduct(1, 5, intProductId, PicFileNamePath + PicFileName, PicTitle, 1);
                        //cProductPicContent.insertNewProductPicContent(PicId, 1, cProduct.Title + "OverAll");
                        fileItem.MoveTo(Server.MapPath("/" + PicFileNamePath + PicFileName));

                        countpopular_small = countpopular_small + 1;
                    }

                    
                    if (Regex.IsMatch(fileItem.Name, "^popular_large."))
                    {
                        typeTitle = ProductPicType.getTypeTitleById(6);
                        PicTitle = Cat_Title + "-" + cProduct.Title + "-" + typeTitle;

                        PicFileName = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title, typeTitle, countpopular_large.ToString());
                        PicFileNamePath = Hotels2thailand.Hotels2FolderAndPath.GetPicturePath(intProductId);
                        PicId = cProductPic.InsertNewPicProduct(1, 6, intProductId, PicFileNamePath + PicFileName, PicTitle, 1);
                        cProductPicContent.insertNewProductPicContent(PicId, 1, cProduct.Title + "OverAll");
                        fileItem.MoveTo(Server.MapPath("/" + PicFileNamePath + PicFileName));

                        countpopular_large = countpopular_large + 1;
                    }

                    
                    if (Regex.IsMatch(fileItem.Name, "^large."))
                    {
                        typeTitle = ProductPicType.getTypeTitleById(7);
                        PicTitle = Cat_Title + "-" + cProduct.Title + "-" + typeTitle;

                        PicFileName = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title, typeTitle, countlarge.ToString());
                        PicFileNamePath = Hotels2thailand.Hotels2FolderAndPath.GetPicturePath(intProductId);
                        PicId = cProductPic.InsertNewPicProduct(1, 7, intProductId, PicFileNamePath + PicFileName, PicTitle, 1);
                        cProductPicContent.insertNewProductPicContent(PicId, 1, cProduct.Title + "OverAll");
                        fileItem.MoveTo(Server.MapPath("/" + PicFileNamePath + PicFileName));

                        countlarge = countlarge + 1;
                    }

                    
                    if (Regex.IsMatch(fileItem.Name, "^thumb."))
                    {
                        typeTitle = ProductPicType.getTypeTitleById(8);
                        PicTitle = Cat_Title + "-" + cProduct.Title + "-" + typeTitle;

                        PicFileName = Hotels2thailand.Hotels2FolderAndPath.GeneratePictureFileName(cProduct.Title, typeTitle, countThumb.ToString());
                        PicFileNamePath = Hotels2thailand.Hotels2FolderAndPath.GetPicturePath(intProductId);
                        PicId = cProductPic.InsertNewPicProduct(1, 8, intProductId, PicFileNamePath + PicFileName, PicTitle, 1);
                        //cProductPicContent.insertNewProductPicContent(PicId, 1, cProduct.Title + "OverAll");
                        fileItem.MoveTo(Server.MapPath("/" + PicFileNamePath + PicFileName));
                        countThumb = countThumb + 1;
                    }
                }
 
                 
             }


             Response.Redirect(Request.Url.ToString());
             Response.End();
         }
        

         

        

        

         
    }
}